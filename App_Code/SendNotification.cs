using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for SendNotification
/// </summary>
public class SendNotification
{
    public static void SendWelcome(string toEmail, string toUserName)
	{
        string msgSubject = "Welcome to Diveintothepool.com!";

        string msgText = toUserName+" welcome to the latest and greatest online dating website.";

        SendEmail(toEmail, toUserName, msgSubject, msgText);
    }

    public static void SendMessageNotification(string toEmail, string toUserName)
    {
        string msgSubject = "You've recieved a message at Diveintothepool.com!";

        string msgText = toUserName+", I wanted to let you know that you recieved a new message!";

        SendEmail(toEmail, toUserName, msgSubject, msgText);
    }

    public static void SendEmail(string toEmail, string toUserName, string msgSubject, string msgText)
    {
        MailAddress from = new MailAddress("notify@diveintothepool.com", "Diveintothepool.com Notification");
        MailAddress to = new MailAddress(toEmail, toUserName);
        MailMessage msg = new MailMessage(from, to);

        msg.Subject = msgSubject;
        msg.Body = msgText;

        //(3) Create the SmtpClient object
        SmtpClient smtp = new SmtpClient();

        //(4) Send the MailMessage (will use the Web.config settings)
        smtp.Send(msg);
    }
}
