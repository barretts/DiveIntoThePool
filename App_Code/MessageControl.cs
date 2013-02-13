using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for MessageControl
/// </summary>
public class MessageControl
{
    private string UserId = "";
    private string[][] Messages;
    private string DateFormat = "d.m.Y - H:i";

    private ConnectionStringSettings connectionStringSettings;
    private SqlConnection cnn;

	public MessageControl(string User)
	{
        UserId = User;

        connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        cnn = new SqlConnection(connectionStringSettings.ConnectionString);
    }

    public DataSet getMessages(string type)
    {
        string sql = "";

        switch(type)
        {
            case "sent":
                sql = "SELECT * FROM Messages WHERE FromUserId = '" + UserId + "' AND FromDeleted = '0' ORDER BY Created DESC";
                break;
            default:
                sql = "SELECT * FROM Messages WHERE ToUserId = '" + UserId + "' AND ToDeleted = '0' ORDER BY Created DESC";
                break;
        }
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);

        return dataSet;
    }

    public DataSet getMessage(string messageId)
    {
        string sql = "SELECT * FROM Messages WHERE MessageId = '" + messageId + "'";

        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet, "Messages");

        return dataSet;
    }

    public int countUnreadMessages()
    {
        string sql = "";

        sql = "SELECT * FROM Messages WHERE ToViewed = '0' AND ToUserId = '" + UserId + "'";

        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);

        return dataSet.Tables[0].Rows.Count;
    }

    // Flag a message as viewed by the sender
    public void fromViewed(string messageId, int status)
    {
        SqlCommand sqlComm = new SqlCommand("UPDATE Messages SET FromViewed = " + status + ", FromVdate = getdate() WHERE MessageId = '" + messageId + "'", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();
    }

    // Flag a message as deleted by the sender
    public void fromDeleted(string messageId, int status)
    {
        SqlCommand sqlComm = new SqlCommand("UPDATE Messages SET FromDeleted = " + status + ", FromDdate = getdate() WHERE MessageId = '" + messageId + "'", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();
    }

    // Flag a message as viewed by the recipient
    public void toViewed(string messageId, int status)
    {
        SqlCommand sqlComm = new SqlCommand("UPDATE Messages SET ToViewed = " + status + ", ToVdate = getdate() WHERE MessageId = '" + messageId + "'", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();
    }

    // Flag a message as deleted by the recipient
    public void toDeleted(string messageId, int status)
    {
        SqlCommand sqlComm = new SqlCommand("UPDATE Messages SET ToDeleted = " + status + ", ToDdate = getdate() WHERE MessageId = '" + messageId + "'", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();
    }

    // Add a new personal message
    public void sendMessage(string to, string title, string message)
    {
        to = SafeSqlLiteral(to);
        title = SafeSqlLiteral(title);
        message = SafeSqlLiteral(message);
        to = getUserId(to);

        SqlCommand sqlComm = new SqlCommand("INSERT INTO Messages (MessageId, ToUserId, FromUserId, Title, Message, Created, FromViewed, ToViewed, FromDeleted, ToDeleted) VALUES (NewId(), '" + to + "', '" + UserId + "','" + title + "','" + message + "',getdate(),'0','0','0','0')", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();

        SendNotification.SendMessageNotification(getEmailFromId(to), getUserName(to));
    }

    // Return user ID from username
    public string getUserId(string UserName)
    {
        string getUserId = "";

        cnn.Open();
        SqlCommand sqlComm = new SqlCommand("SELECT TOP 1 UserId FROM aspnet_Users WHERE UserName = '"+UserName+"'", cnn);
        SqlDataReader r = sqlComm.ExecuteReader();
        while (r.Read())
        {
            getUserId = r["UserId"].ToString();
        }
        r.Close();
        cnn.Close();

        return getUserId;
    }

    // Return username from user ID
    public string getUserName(string UserId)
    {
        string getUserName = "";

        cnn.Open();
        SqlCommand sqlComm = new SqlCommand("SELECT TOP 1 UserName FROM aspnet_Users WHERE UserId = '" + UserId + "'", cnn);
        SqlDataReader r = sqlComm.ExecuteReader();
        while (r.Read())
        {
            getUserName = r["UserName"].ToString();
        }
        r.Close();
        cnn.Close();

        return getUserName;
    }

    // Return email from user ID
    public string getEmailFromId(string UserId)
    {
        string getEmail = "";

        cnn.Open();
        SqlCommand sqlComm = new SqlCommand("SELECT TOP 1 Email FROM aspnet_Membership WHERE UserId = '" + UserId + "'", cnn);
        SqlDataReader r = sqlComm.ExecuteReader();
        while (r.Read())
        {
            getEmail = r["Email"].ToString();
        }
        r.Close();
        cnn.Close();

        return getEmail;
    }

    // Return email from user ID
    public string getEmailFromUsername(string UserName)
    {
        return getEmailFromId(getUserId(UserName));
    }

    public string getWelcomeStatus(string UserName)
    {
        string getStatus = "";

        cnn.Open();
        SqlCommand sqlComm = new SqlCommand("SELECT TOP 1 SentWelcomeEmail FROM UserProfileTable WHERE UserName = '" + UserName + "'", cnn);
        SqlDataReader r = sqlComm.ExecuteReader();
        while (r.Read())
        {
            getStatus = r["SentWelcomeEmail"].ToString();
        }
        r.Close();
        cnn.Close();

        return getStatus;
    }

    public string getCreatedProfile(string UserName)
    {
        string getStatus = "";

        cnn.Open();
        SqlCommand sqlComm = new SqlCommand("SELECT TOP 1 CreatedProfile FROM UserProfileTable WHERE UserName = '" + UserName + "'", cnn);
        SqlDataReader r = sqlComm.ExecuteReader();
        while (r.Read())
        {
            getStatus = r["CreatedProfile"].ToString();
        }
        r.Close();
        cnn.Close();

        return getStatus;
    }

    public void setWelcomeStatus(string UserName)
    {
        SqlCommand sqlComm = new SqlCommand("UPDATE UserProfileTable SET SentWelcomeEmail = '1' WHERE UserName = '" + UserName + "'", cnn);

        cnn.Open();
        sqlComm.ExecuteNonQuery();
        cnn.Close();
    }

    private string SafeSqlLiteral(string inputSQL)
    {
        return inputSQL.Replace("'", "''");
    }
}
