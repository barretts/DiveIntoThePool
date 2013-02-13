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
        DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete the selected messages?');");

        if (User.Identity.IsAuthenticated)
        {
            MembershipUser userObject = Membership.GetUser();
            string UserID = userObject.ProviderUserKey.ToString();

            pm = new MessageControl(UserID);

            if (pm.getCreatedProfile(User.Identity.Name) != "1")
            {
                Response.Redirect("CreateProfile.aspx");
            }

            //Label1.Text = pm.getUserId("sosuke");
            //Label2.Text = pm.getUserName(Label1.Text.ToString());
            //pm.sendMessage("sosuke", "test", "omg i love you!");
            //pm.toDeleted("4f561504-964e-492b-af24-7c506528a96a", 0);
            //pm.toViewed("4f561504-964e-492b-af24-7c506528a96a", 0);

            if (!Page.IsPostBack)
            {
                Repeater1.DataSource = pm.getMessages("sent");
                Repeater1.DataBind();
            }
        }
        else
        {
            Response.Redirect("Login.aspx?ReturnUrl=pmSent.aspx");
        }
    }
    /*
    protected void SendSubmit_Click(object sender, EventArgs e)
    {
        pm.sendMessage(SendTo.Text, SendTitle.Text, SendMessage.Text);
    }
    */
    protected void DeleteMessageFrom_Sent(object sender, CommandEventArgs e)
    {
        string MessageId = (string)e.CommandArgument;
        Trace.Write("DeleteMessageFrom_Sent", MessageId);
        pm.fromDeleted(MessageId, 1);
    }

    protected void DeleteMessagesFrom_Sent(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpItem in Repeater1.Items)
        {
            CheckBox chkbx = rpItem.FindControl("CheckBox1") as CheckBox;
            HiddenField messageIdField = rpItem.FindControl("HiddenField1") as HiddenField;
            if (chkbx.Checked)
            {
                pm.fromDeleted(messageIdField.Value, 1);
            }
        }

        Repeater1.DataSource = pm.getMessages("sent");
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
                pm.fromViewed(messageIdField.Value, 1);
            }
        }

        Repeater1.DataSource = pm.getMessages("sent");
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
                pm.fromViewed(messageIdField.Value, 0);
            }
        }

        Repeater1.DataSource = pm.getMessages("sent");
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
