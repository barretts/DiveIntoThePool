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

public partial class SearchProfiles : System.Web.UI.Page
{
    protected System.Data.DataTable zipTable = null;
    protected System.Data.DataTable profileTable = null;
    protected DataView dv;

    protected String SearchZip_Value = "";
    protected String SearchMiles_Value = "";
    protected String SearchSex_Value = "";
    protected String SeekingSex_Value = "";

    protected int currentPage = 0;
    protected int pageSize = 5;
    protected double pageCount = 0;
    protected int resultCount = 0;
    protected bool hasLinks = false;
    protected bool freshSearch = false;

    protected String SearchOptions = "";
    protected String SearchBy = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchZip_Value = SearchZip.Text;
        SearchMiles_Value = SearchMiles.SelectedValue;
        SearchSex_Value = SearchSex.SelectedValue;
        SeekingSex_Value = SeekingSex.SelectedValue;

        switch (SearchSex_Value + SeekingSex_Value)
        {
            case "mw":
                SearchOptions = " AND UserProfileTable.Gender = '1' AND UserProfileTable.Seeking = '0'";
                break;
            case "ww":
                SearchOptions = " AND UserProfileTable.Gender = '1' AND UserProfileTable.Seeking = '1'";
                break;
            case "mm":
                SearchOptions = " AND UserProfileTable.Gender = '0' AND UserProfileTable.Seeking = '0'";
                break;
            default:
                SearchOptions = " AND UserProfileTable.Gender = '0' AND UserProfileTable.Seeking = '1'";
                break;
        }

        switch (ArrangeBy.SelectedValue)
        {
            case "newestMembers":
                SearchBy = "CreatedDate DESC";
                break;
            case "distanceFrom":
                SearchBy = "Distance ASC";
                break;
            default:
                SearchBy = "LastActivityDate DESC";
                break;
        }

        if (Page.PreviousPage != null && freshSearch == false)
        {
            ContentPlaceHolder cph = this.PreviousPage.Form.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;

            if (cph != null)
            {
                DropDownList SourceSearchSex = (DropDownList)cph.FindControl("SearchSex");
                DropDownList SourceSearchMiles = (DropDownList)cph.FindControl("SearchMiles");
                TextBox SourceSearchZip = (TextBox)cph.FindControl("SearchZip");

                SearchZip_Value = SourceSearchZip.Text;
                SearchMiles_Value = SourceSearchMiles.SelectedValue;
                SearchSex_Value = SourceSearchSex.SelectedValue;

                SearchZip.Text = SearchZip_Value;
                SearchMiles.SelectedIndex = SourceSearchMiles.SelectedIndex;
                SearchSex.SelectedIndex = SourceSearchSex.SelectedIndex;

                HiddenField searchTypeInput = (HiddenField)cph.FindControl("searchType");

                if (searchTypeInput.Value == "advanced")
                {
                    DropDownList SelectEthnicity = (DropDownList)cph.FindControl("SelectEthnicity");
                    DropDownList SelectBodyType = (DropDownList)cph.FindControl("SelectBodyType");
                    DropDownList SelectHaveChildren = (DropDownList)cph.FindControl("SelectHaveChildren");
                    DropDownList SelectMaritalStatus = (DropDownList)cph.FindControl("SelectMaritalStatus");
                    DropDownList SelectHeight = (DropDownList)cph.FindControl("SelectHeight");
                    DropDownList SelectLookingFor = (DropDownList)cph.FindControl("SelectLookingFor");
                    DropDownList SelectSeeking = (DropDownList)cph.FindControl("SelectSeeking");
                    DropDownList SelectHairColor = (DropDownList)cph.FindControl("SelectHairColor");
                    DropDownList SelectEyeColor = (DropDownList)cph.FindControl("SelectEyeColor");
                    DropDownList SelectReligion = (DropDownList)cph.FindControl("SelectReligion");
                    DropDownList SelectRecreationalDrugs = (DropDownList)cph.FindControl("SelectRecreationalDrugs");
                    DropDownList SelectSmoking = (DropDownList)cph.FindControl("SelectSmoking");
                    DropDownList SelectAlcohol = (DropDownList)cph.FindControl("SelectAlcohol");
                    DropDownList SelectWantChildren = (DropDownList)cph.FindControl("SelectWantChildren");

                    List<string> updateStrings = new List<string>();

                    if ((string)Request.Params[SelectEthnicity.UniqueID] != "No Preference")
                        updateStrings.Add("Ethnicity = '" + Request.Params[SelectEthnicity.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectBodyType.UniqueID] != "No Preference")
                        updateStrings.Add("BodyType = '" + Request.Params[SelectBodyType.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectHaveChildren.UniqueID] != "No Preference")
                        updateStrings.Add("HaveChildren = '" + Request.Params[SelectHaveChildren.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectMaritalStatus.UniqueID] != "No Preference")
                        updateStrings.Add("MaritalStatus = '" + Request.Params[SelectMaritalStatus.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectHeight.UniqueID] != "No Preference")
                        updateStrings.Add("Height = '" + Request.Params[SelectHeight.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectLookingFor.UniqueID] != "No Preference")
                        updateStrings.Add("LookingFor = '" + Request.Params[SelectLookingFor.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectSeeking.UniqueID] != "No Preference")
                        updateStrings.Add("Seeking = '" + Request.Params[SelectSeeking.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectHairColor.UniqueID] != "No Preference")
                        updateStrings.Add("HairColor = '" + Request.Params[SelectHairColor.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectEyeColor.UniqueID] != "No Preference")
                        updateStrings.Add("EyeColor = '" + Request.Params[SelectEyeColor.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectReligion.UniqueID] != "No Preference")
                        updateStrings.Add("Religion = '" + Request.Params[SelectReligion.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectRecreationalDrugs.UniqueID] != "No Preference")
                        updateStrings.Add("RecreationalDrugs = '" + Request.Params[SelectRecreationalDrugs.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectSmoking.UniqueID] != "No Preference")
                        updateStrings.Add("Smoking = '" + Request.Params[SelectSmoking.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectAlcohol.UniqueID] != "No Preference")
                        updateStrings.Add("Alcohol = '" + Request.Params[SelectAlcohol.UniqueID].Trim() + "'");

                    if ((string)Request.Params[SelectWantChildren.UniqueID] != "No Preference")
                        updateStrings.Add("WantChildren = '" + Request.Params[SelectWantChildren.UniqueID].Trim() + "'");

                    string[] updateStringArray = updateStrings.ToArray();

                    string updateString = String.Join(" AND ", updateStringArray);

                    Trace.Write("updateString", updateString);

                    if (updateString.Length > 0)
                    {
                        SearchOptions += " AND " + updateString;
                    }
                }

                GetSearchResults();
            }
        }

        if (Page.IsPostBack)
        {
            if (Session["pageCount"] != null)
            {
                pageCount = (double)Session["pageCount"];
                AddNavigationLinks((int)pageCount);
            }
        }

        if (Page.IsPostBack == false && freshSearch == false)
        {
            DataTable recentlyActiveTable = FindProfiles.getLastActive(5);
            SearchRepeater.DataSource = recentlyActiveTable.DefaultView;
            SearchRepeater.DataBind();
            SearchDisplay.Visible = true;
            SampleSearchDisplay.Visible = true;
        }
        else
        {
            SampleSearchDisplay.Visible = false;
        }

        if (User.Identity.IsAuthenticated)
        {
            /*
            if (SearchZip.Text == "")
            {
                ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
                SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
                SqlCommand SelectCommand = new SqlCommand();
                SelectCommand.Connection = cnn;
                string sql;
                sql = "SELECT Zip FROM UserProfileTable WHERE UserName = '" + Profile.UserName + "'";
                SelectCommand.CommandText = sql;
                SqlDataReader rdr = null;
                try
                {
                    cnn.Open();
                    rdr = SelectCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                        SearchZip.Text = (string)rdr["Zip"];
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    cnn.Close();
                }
            }
            */
        }
    }

    protected void GetSearchResults_Click(object sender, EventArgs e)
    {
        GetSearchResults();
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

        Session["SearchResults"] = profileTable;
        Session["currentPage"] = null;
        Session["pageCount"] = null;

        updatePage();
    }

    protected void updatePage()
    {
        profileTable = Session["SearchResults"] as DataTable;

        if ((profileTable.Rows.Count < 6 || currentPage > 0) && User.Identity.IsAuthenticated == false)
        {
            SampleSearchDisplay.Visible = false;
            SearchDisplay.Visible = false;
            RegisterDisplay.Visible = true;
        }
        else
        {
            dv = profileTable.DefaultView;
            dv.RowFilter = "idColumn >= " + (currentPage * pageSize) + " AND idColumn <= " + (currentPage * pageSize + pageSize - 1) + "";

            SearchRepeater.DataSource = dv;
            SearchRepeater.DataBind();
            SearchDisplay.Visible = true;
            RegisterDisplay.Visible = false;

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

        if (pageCount > 1)
        {
            Label1.Text = (currentPage + 1) + " of " + pageCount + " : " + resultCount;

            if (Session["pageCount"] == null)
            {
                AddNavigationLinks((int)pageCount);
                Session["pageCount"] = pageCount;
            }

            pageCount = (double)Session["pageCount"];
            AddNavigationLinks((int)pageCount);
        }
    }

    protected void GetPage(object sender, CommandEventArgs c)
    {
        Trace.Write("GetPage", c.CommandArgument.ToString());
        currentPage = int.Parse(c.CommandArgument.ToString()) - 1;
        Session["currentPage"] = currentPage;
        Trace.Write("currentPage", currentPage.ToString());
        updatePage();
    }

    protected void AddNavigationLinks(int pageCountTemp)
    {
        while (PlaceHolder1.Controls.Count > 0)
        {
            PlaceHolder1.Controls.RemoveAt(0);
        }

        if (1 == 1) //if (hasLinks == false)
        {
            for (int i = 1; i < pageCountTemp + 1; i++)
            {
                LinkButton PagingLink = new LinkButton();
                PagingLink.ID = "pagelink" + i.ToString();
                PagingLink.Text = i.ToString();
                PagingLink.Visible = true;

                PagingLink.CommandArgument = i.ToString(); //used to detect result page required
                PagingLink.Command += new CommandEventHandler(GetPage);

                Trace.Write("currentPage", currentPage.ToString());

                PlaceHolder1.Controls.Add(PagingLink);

                if(pageCountTemp != i)
                {
                    LiteralControl Spacing = new LiteralControl(" ");
                    PlaceHolder1.Controls.Add(Spacing);
                }

                if (i == currentPage + 1)
                {
                    PagingLink.Enabled = false;
                }
            }

            hasLinks = true;
        }
    }

    protected void UpdateNavigationLinks(int pageCountTemp)
    {
        for (int i = 1; i < pageCountTemp + 1; i++)
        {
            LinkButton NavClick = PlaceHolder1.FindControl("pagelink" + i) as LinkButton;
            if (NavClick != null)
            {
                if (i == currentPage + 1)
                {
                    NavClick.Enabled = false;
                }
                else
                {
                    NavClick.Enabled = true;
                }
            }
        }
    }

    public void getProfilesNearZip(string zip, string miles)
    {
        string zips = "";

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand cmd = new SqlCommand("GetNearZipcode", cnn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = cnn;
        cmd.Parameters.Add(
            new SqlParameter("@Zipcode", zip));
        cmd.Parameters.Add(
            new SqlParameter("@Miles", miles));

        try
        {
            cnn.Open();

            SqlDataAdapter zipsDataAdapter = new SqlDataAdapter(cmd);

            zipTable = new System.Data.DataTable();
            zipsDataAdapter.Fill(zipTable);

            int i = 0;

            foreach (DataRow row in zipTable.Rows)
            {
                if (i > 0)
                {
                    zips += ",";
                }
                zips += "'" + row["Zipcode"] + "'";
                i++;
            }

            Trace.Write("Zip list", zips);

            SqlDataAdapter profileDataAdapter = new SqlDataAdapter("SELECT * FROM UserProfileTable LEFT JOIN UserProfileImages ON UserProfileImages.UserName = UserProfileTable.UserName WHERE UserProfileTable.Zip IN (" + zips + ") AND UserProfileImages.IsMain = 1" + SearchOptions, cnn);

            Trace.Write("profileDataAdapter", "SELECT * FROM UserProfileTable LEFT JOIN UserProfileImages ON UserProfileImages.UserName = UserProfileTable.UserName WHERE UserProfileTable.Zip IN (" + zips + ") AND UserProfileImages.IsMain = 1" + SearchOptions);

            profileTable = new System.Data.DataTable();

            DataColumn dateColumn = new DataColumn("Distance", typeof(Double));
            profileTable.Columns.Add(dateColumn);

            DataColumn ageColumn = new DataColumn("Age", typeof(Int16));
            profileTable.Columns.Add(ageColumn);

            profileDataAdapter.Fill(profileTable);

            foreach (DataRow r in profileTable.Rows)
            {
                r["Distance"] = Double.Parse(getDistanceByZip(r["Zip"].ToString()));
                r["Age"] = Int16.Parse(UserProfile.GetAge(r["BirthDate"].ToString()));
            }

            profileTable = SortDataTable(profileTable, "Distance ASC");

            DataColumn idColumn = new DataColumn("idColumn", typeof(Int16));
            profileTable.Columns.Add(idColumn);

            int rowNumber = 0;

            foreach (DataRow r in profileTable.Rows)
            {
                r["idColumn"] = rowNumber;
                rowNumber++;
            }
        }
        catch (Exception ex)
        {
            Trace.Write(ex.ToString());
        }
        finally
        {
            cnn.Close();
        }
    }

    public string getDistanceByZip(string zip)
    {
        string distance = "";

        foreach (DataRow row in zipTable.Rows)
        {
            if (row["Zipcode"].ToString() == zip)
            {
                distance = row["Distance"].ToString();
                break;
            }
        }

        return distance;
    }

    private DataTable SortDataTable(DataTable GetDataTable, string sort)
    {
        DataTable _NewDataTable = GetDataTable.Clone();
        int rowCount = GetDataTable.Rows.Count;

        DataRow[] foundRows = GetDataTable.Select(null, sort);

        // Sort with Column name
        for (int i = 0; i < rowCount; i++)
        {
            object[] arr = new object[GetDataTable.Columns.Count];

            for (int j = 0; j < GetDataTable.Columns.Count; j++)
            {
                arr[j] = foundRows[i][j];
            }

            DataRow data_row = _NewDataTable.NewRow();
            data_row.ItemArray = arr;
            _NewDataTable.Rows.Add(data_row);
        }
        
        //Clear the incoming GetDataTable
        GetDataTable.Rows.Clear();

        for (int i = 0; i < _NewDataTable.Rows.Count; i++)
        {
            object[] arr = new object[GetDataTable.Columns.Count];

            for (int j = 0; j < GetDataTable.Columns.Count; j++)
            {
                arr[j] = _NewDataTable.Rows[i][j];
            }

            DataRow data_row = GetDataTable.NewRow();

            data_row.ItemArray = arr;

            GetDataTable.Rows.Add(data_row);
        }

        return _NewDataTable;
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
}
