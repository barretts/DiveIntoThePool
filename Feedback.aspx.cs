using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSend_Click(object sender, System.EventArgs e)
    {
        MailAddress from = new MailAddress(txtFrom.Text, "Diveintothepool.com Notifications");
        MailAddress to = new MailAddress(txtTo.Text, "Jane Clayton");
        MailMessage msg = new MailMessage(from, to);

        msg.Subject = txtSubject.Text;
        msg.Body = txtContent.Value;
        lblStatus.Text = "Sending...";

        //(3) Create the SmtpClient object
        SmtpClient smtp = new SmtpClient();

        //(4) Send the MailMessage (will use the Web.config settings)
        smtp.Send(msg);

        lblStatus.Text = "Sent email (" + txtSubject.Text + ") to " + txtTo.Text;
    }
}
