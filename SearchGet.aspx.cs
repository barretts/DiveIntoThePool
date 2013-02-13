using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SearchGet : System.Web.UI.Page
{
    protected System.Data.DataTable zipTable = null;
    protected System.Data.DataTable profileTable = null;
    protected DataView dv;

    protected String SearchZip_Value = "";
    protected String SearchMiles_Value = "25";
    protected String SearchSex_Value = "";
    protected String SeekingSex_Value = "";

    protected int currentPage = 0;
    protected int pageSize = 5;
    protected double pageCount = 0;
    protected int resultCount = 0;
    protected bool hasLinks = false;
    protected bool freshSearch = false;

    protected String SearchQuery = "";
    protected String SearchOptions = "";
    protected String SearchBy = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            SearchQuery = buildSearchQuery();
            SearchOptions = buildSearchOptions();

            switch (Request.QueryString["sort"])
            {
                case "newest":
                    SearchBy = "CreatedDate DESC";
                    break;
                case "distance":
                    SearchBy = "Distance ASC";
                    break;
                default:
                    SearchBy = "LastActivityDate DESC";
                    break;
            }

            ArrangeBy.SelectedValue = Request.QueryString["sort"];

            if (Request.QueryString["zip"] != null && IsNumeric(Request.QueryString["zip"]))
            {
                if (int.Parse(Request.QueryString["zip"]) > 9999 && int.Parse(Request.QueryString["zip"]) < 100000)
                {
                    SearchZip_Value = Request.QueryString["zip"];
                }
                else
                {
                    SearchZip_Value = "";
                }

                SearchZip.Text = SearchZip_Value;

                Trace.Write("SearchZip_Value", SearchZip_Value);
            }

            if (Request.QueryString["miles"] != null && IsNumeric(Request.QueryString["miles"]))
            {
                if (int.Parse(Request.QueryString["miles"]) > 24 && int.Parse(Request.QueryString["miles"]) < 201)
                {
                    SearchMiles_Value = Request.QueryString["miles"];
                }
                else
                {
                    SearchMiles_Value = "25";
                }

                SearchMiles.SelectedValue = SearchMiles_Value;

                Trace.Write("SearchMiles_Value", SearchMiles_Value);
            }

            if (Request.QueryString["page"] != null && IsNumeric(Request.QueryString["page"]))
            {
                if (int.Parse(Request.QueryString["page"]) < 40 && int.Parse(Request.QueryString["page"]) > 0)
                {
                    currentPage = int.Parse(Request.QueryString["page"]) - 1;
                }
                else
                {
                    currentPage = 0;
                }

                Trace.Write("currentPage", currentPage.ToString());
            }

            GetSearchResults();
        }
    }

    protected void GetSearchResults_Click(object sender, EventArgs e)
    {
        List<string> SearchValues = new List<string>();
        SearchValues.Add("ima=" + SearchSex.SelectedValue);
        SearchValues.Add("seekinga=" + SeekingSex.SelectedValue);

        if (IsNumeric(SearchZip.Text) && SearchZip.Text.Length == 5)
        {
            SearchValues.Add("zip=" + SearchZip.Text);
            if (int.Parse(SearchMiles.SelectedValue) != 25)
            {
                SearchValues.Add("miles=" + SearchMiles.SelectedValue);
            }
        }

        if (ArrangeBy.SelectedValue != "last")
        {
            SearchValues.Add("sort=" + ArrangeBy.SelectedValue);
        }

        string[] SearchValuesArray = SearchValues.ToArray();

        string SearchString = String.Join("&", SearchValuesArray);
        
        Response.Redirect(Request.Path + "?" + SearchString);
    }

    protected void GetSearchResults()
    {
        SearchPaging.Visible = true;
        freshSearch = true;

        Trace.Write("SearchOptions", SearchOptions.ToString());

        if (SearchZip_Value.Length == 5)
        {
            profileTable = FindProfiles.getProfilesNearZip(SearchOptions, SearchBy, SearchZip_Value, SearchMiles_Value);
        }
        else
        {
            if (SearchBy != "Distance ASC")
            {
                profileTable = FindProfiles.getProfiles(SearchOptions, SearchBy);
            }
            else
            {
                return;
            }
        }

        //Session["SearchOptions"] = SearchOptions;
        //Session["SearchBy"] = SearchBy;
        //Session["SearchZip"] = SearchZip_Value;
        //Session["SearchMiles"] = SearchMiles_Value;
        //Session["SearchResults"] = profileTable;
        //Session["currentPage"] = null;
        //Session["pageCount"] = null;

        updatePage();
    }

    protected void updatePage()
    {
        //profileTable = Session["SearchResults"] as DataTable;

        if ((profileTable.Rows.Count < 6 || currentPage > 0) && User.Identity.IsAuthenticated == false)
        {
            SampleSearchDisplay.Visible = false;
            SearchDisplay.Visible = false;
            RegisterDisplay.Visible = true;
        }
        else
        {
            updatePageCount();
        }
    }

    protected void updatePageCount()
    {
        if (profileTable != null)
        {
            resultCount = profileTable.Rows.Count;
            double pageDouble = resultCount / pageSize;
            decimal pageTest = resultCount / pageSize;
            Trace.Write("resultCount", resultCount.ToString());
            Trace.Write("pageSize", pageSize.ToString());
            pageCount = Math.Ceiling((double)((decimal)resultCount / (decimal)pageSize));
            Trace.Write("pageCount", pageCount.ToString());
        }

        if (currentPage > pageCount - 1)
        {
            currentPage = (int)pageCount - 1;
        }

        if (pageCount > 1)
        {
            Label1.Text = (currentPage + 1) + " of " + pageCount + " : " + resultCount;

            AddNavigationLinks((int)pageCount);
        }

        displayProfiles();
    }

    protected void displayProfiles()
    {
        dv = profileTable.DefaultView;
        dv.RowFilter = "idColumn >= " + (currentPage * pageSize) + " AND idColumn <= " + (currentPage * pageSize + pageSize - 1) + "";

        SearchRepeater.DataSource = dv;
        SearchRepeater.DataBind();
        SearchDisplay.Visible = true;
        RegisterDisplay.Visible = false;
    }

    protected void AddNavigationLinks(int pageCountTemp)
    {
        while (PlaceHolder1.Controls.Count > 0)
        {
            PlaceHolder1.Controls.RemoveAt(0);
        }

        for (int i = 1; i < pageCountTemp + 1; i++)
        {
            HyperLink PagingLink = new HyperLink();
            PagingLink.ID = "pagelink" + i.ToString();
            PagingLink.Text = i.ToString();
            PagingLink.NavigateUrl = Request.Path + "?" + SearchQuery + "&page=" + i;

            if (i == currentPage + 1)
            {
                PagingLink.Enabled = false;
            }

            PlaceHolder1.Controls.Add(PagingLink);

            if(pageCountTemp != i)
            {
                LiteralControl Spacing = new LiteralControl(" ");
                PlaceHolder1.Controls.Add(Spacing);
            }
        }
    }

    public string buildSearchQuery()
    {
        String queryString = "";

        foreach (string tempVarName in Request.QueryString)
        {
            if (tempVarName.ToLower() != "page")
            {
                queryString += tempVarName + "=" + Request.QueryString[tempVarName] + "&";
            }
        }

        queryString = queryString.Substring(0, queryString.Length - 1);

        return queryString;
    }

    public string buildSearchOptions()
    {
        String searchString = "";

        switch (Request.QueryString["ima"] + Request.QueryString["seekinga"])
        {
            case "mw":
                searchString += " AND UserProfileTable.Gender = '1' AND UserProfileTable.Seeking = '0'";
                break;
            case "ww":
                searchString += " AND UserProfileTable.Gender = '1' AND UserProfileTable.Seeking = '1'";
                break;
            case "mm":
                searchString += " AND UserProfileTable.Gender = '0' AND UserProfileTable.Seeking = '0'";
                break;
            default:
                searchString += " AND UserProfileTable.Gender = '0' AND UserProfileTable.Seeking = '1'";
                break;
        }

        SearchSex.SelectedValue = Request.QueryString["ima"];
        SeekingSex.SelectedValue = Request.QueryString["seekinga"];

        return searchString;
    }

    public string truncateString(string inputString, int maxLength)
    {
        if (inputString.Length > maxLength)
        {
            if (maxLength > 40)
            {
                return inputString.Substring(0, maxLength - 4) + " ...";
            }
            else
            {
                return inputString.Substring(0, maxLength);
            }
        } else {
            return inputString;
        }
    }

    public static bool IsNumeric(object Expression)
    {
        bool isNum;
        double retNum;
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }
}
