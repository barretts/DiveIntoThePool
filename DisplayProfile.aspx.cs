using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class DisplayProfile : System.Web.UI.Page
{
    Panel ImageSelector;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["user"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        
        if (Request.UrlReferrer != null)
        {
            HyperLink1.NavigateUrl = Request.UrlReferrer.ToString();
            HyperLink1.Text = "<< Return to Search Results";
            ReturnTo.Visible = true;
        }
    }

    protected void ProfileDataList_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            Repeater cphRepeater = (Repeater)e.Item.FindControl("ProfileImages");
            if (cphRepeater.Items.Count > 1)
            {
                Panel ImageSelector = (Panel)e.Item.FindControl("ProfileImageSelector");
                ImageSelector.Visible = true;
            }

            Page.Header.Title = "DiveIntoThePool.com: Meet "+DataBinder.Eval(e.Item.DataItem, "UserName").ToString()+"!";
        }
    }
}
