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
    public String ViewMessageId;

    protected void Page_Load(object sender, EventArgs e)
    {
        DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this message?');");

        if (User.Identity.IsAuthenticated)
        {
            MembershipUser userObject = Membership.GetUser();
            string UserID = userObject.ProviderUserKey.ToString();

            pm = new MessageControl(UserID);

            if (pm.getCreatedProfile(User.Identity.Name) != "1")
            {
                Response.Redirect("CreateProfile.aspx");
            }

            if (Request.QueryString["messageId"] != null && Request.QueryString["source"] != null)
            {
                ViewMessageId = Request.QueryString["messageId"];
                Repeater1.DataSource =  pm.getMessage(ViewMessageId);
                Repeater1.DataBind();

                if (Request.QueryString["source"] == "to")
                {
                    pm.toViewed(ViewMessageId, 1);
                }

                if (Request.QueryString["source"] == "from")
                {
                    pm.fromViewed(ViewMessageId, 1);
                    ReplyButton.Visible = false;
                }
            }
            else
            {
                Response.Redirect("pmInbox.aspx");
            }
        }
        else
        {
            Response.Redirect("Login.aspx?ReturnUrl=pmInbox.aspx");
        }
    }

    protected void ReplyToMessage(object sender, EventArgs e)
    {
        Response.Redirect("pmSend.aspx?messageId=" + ViewMessageId);
    }

    protected void DeleteMessage(object sender, EventArgs e)
    {
        if (Request.QueryString["source"] == "to")
        {
            pm.toDeleted(ViewMessageId, 1);
            Response.Redirect("pmInbox.aspx");
        }

        if (Request.QueryString["source"] == "from")
        {
            pm.fromDeleted(ViewMessageId, 1);
            Response.Redirect("pmSent.aspx");
        }
    }

    protected void DeleteMessageFrom_Inbox(object sender, CommandEventArgs e)
    {
        string MessageId = (string)e.CommandArgument;
        Trace.Write("DeleteMessageFrom_Inbox", MessageId);
        pm.toDeleted(MessageId, 1);
    }

    protected void DeleteMessagesFrom_Inbox(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField messageIdField = rpItem.FindControl("HiddenField1") as HiddenField;
            if (chkbx.Checked)
            {
                pm.toDeleted(messageIdField.Value, 1);
            }
        }

        Repeater1.DataSource = pm.getMessages("inbox");
        Repeater1.DataBind();
    }

    protected void MarkMessages_Read(object sender, EventArgs e)
    {
        Trace.Write("MarkMessages_Read", "start");
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField messageIdField = rpItem.FindControl("HiddenField1") as HiddenField;
            if (chkbx.Checked)
            {
                Trace.Write("MarkMessages_Read", "success");
                pm.toViewed(messageIdField.Value, 1);
            }
        }

        Repeater1.DataSource = pm.getMessages("inbox");
        Repeater1.DataBind();
    }

    protected void MarkMessages_Unread(object sender, EventArgs e)
    {
        Trace.Write("MarkMessages_Unread", "start");
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField messageIdField = rpItem.FindControl("HiddenField1") as HiddenField;
            if (chkbx.Checked)
            {
                Trace.Write("MarkMessages_Unread", "success");
                pm.toViewed(messageIdField.Value, 0);
            }
        }

        Repeater1.DataSource = pm.getMessages("inbox");
        Repeater1.DataBind();
    }

    protected String ReadStatus(String ReadValue)
    {
        Trace.Write("ReadStatus", ReadValue);
        string ReadClass = "unread";

        if (ReadValue == "True")
        {
            ReadClass = "read";
        }

        return ReadClass;
    }

    protected void Select_All(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            chkbx.Checked = true;
        }
    }

    protected void Select_None(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            chkbx.Checked = false;
        }
    }

    protected void Select_Read(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField readStatus = rpItem.FindControl("HiddenField2") as HiddenField;

            if (readStatus.Value == "True")
            {
                chkbx.Checked = true;
            }
            else
            {
                chkbx.Checked = false;
            }
        }
    }

    protected void Select_Unread(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField readStatus = rpItem.FindControl("HiddenField2") as HiddenField;

            if (readStatus.Value == "False")
            {
                chkbx.Checked = true;
            }
            else
            {
                chkbx.Checked = false;
            }
        }
    }
}
