using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string UnreadMessageCount = " (0)";
    public MessageControl pm;

    protected void Page_Load(object sender, EventArgs e)
    {/*
        if (Page.User.Identity.IsAuthenticated)
        {
            MembershipUser userObject = Membership.GetUser();
            string UserID = userObject.ProviderUserKey.ToString();

            pm = new MessageControl(UserID);

            UnreadMessageCount = pm.countUnreadMessages().ToString();

            if (UnreadMessageCount == "0")
            {
                UnreadMessageCount = "";
            }
            else
            {
                UnreadMessageCount = " (" + UnreadMessageCount + ")";
            }

            //MessageCount.text = UnreadMessageCount;
        }*/

        if (Page.GetType().Name == "register_aspx" || Page.GetType().Name == "login_aspx" || Page.GetType().Name == "createprofile_aspx")
        {
            LoginView2.Visible = false;
        }
    }
}
