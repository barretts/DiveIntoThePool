using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PrivateMessages : System.Web.UI.Page
{
    public MessageControl pm;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            MembershipUser userObject = Membership.GetUser();
            string UserID = userObject.ProviderUserKey.ToString();

            pm = new MessageControl(UserID);

            if (pm.getCreatedProfile(User.Identity.Name) != "1")
            {
                Response.Redirect("CreateProfile.aspx");
            }

            if (Request.QueryString["SendTo"] != null)
            {
                SendTo.Text = Request.QueryString["SendTo"];
            }

            if (Request.QueryString["messageId"] != null)
            {
                string ViewMessageId = Request.QueryString["messageId"];
                DataSet replyMessages = pm.getMessage(ViewMessageId);
                DataRow replyMessage = replyMessages.Tables["Messages"].Rows[0] as DataRow;
                SendTo.Text = pm.getUserName(replyMessage["FromUserId"].ToString());
                SendTitle.Text = "Re: " + replyMessage["Title"].ToString();
            }
        }
        else
        {
            Response.Redirect("Login.aspx?ReturnUrl=pmInbox.aspx");
        }
    }

    protected void SendSubmit_Click(object sender, EventArgs e)
    {
        pm.sendMessage(SendTo.Text, SendTitle.Text, SendMessage.Value);
        Response.Redirect("pmInbox.aspx");
    }
}
