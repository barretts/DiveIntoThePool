using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownList SelectGender = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectGender");
            DropDownList SelectBirthMonth = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthMonth");
            DropDownList SelectBirthDay = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthDay");
            DropDownList SelectBirthYear = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthYear");
            DropDownList SelectEthnicity = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectEthnicity");

            SelectGender.DataSource = UserProfile.ListGender();
            SelectGender.DataBind();

            SelectBirthMonth.DataSource = UserProfile.ListMonths();
            SelectBirthMonth.DataBind();

            SelectBirthDay.DataSource = UserProfile.ListDays();
            SelectBirthDay.DataBind();

            SelectBirthYear.DataSource = UserProfile.ListYears();
            SelectBirthYear.DataBind();

            SelectEthnicity.DataSource = UserProfile.ListEthnicity();
            SelectEthnicity.DataBind();

            SelectEthnicity.Items.FindByValue("3").Selected = true;
        }
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        TextBox SelectEmail = (TextBox)CreateUserWizardStep0.ContentTemplateContainer.FindControl("Email");
        DropDownList SelectGender = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectGender");
        DropDownList SelectBirthMonth = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthMonth");
        DropDownList SelectBirthDay = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthDay");
        DropDownList SelectBirthYear = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectBirthYear");
        DropDownList SelectEthnicity = (DropDownList)CreateUserWizardStep0.ContentTemplateContainer.FindControl("SelectEthnicity");

        string BirthDateString = SelectBirthMonth.SelectedValue + "/" + SelectBirthDay.SelectedValue + "/" + SelectBirthYear.SelectedValue;

        string UserName = CreateUserWizard1.UserName;
        string UserId = Membership.GetUser(UserName).ProviderUserKey.ToString();
        string CreatedDate = DateTime.Now.ToString("s");

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = cnn;
        string sql;

        sql = "INSERT INTO UserProfileTable (UserId, UserName, Gender, BirthDate, Ethnicity, Country, City, State, Zip, MaritalStatus, Height, BodyType, HairColor, EyeColor, Seeking, LookingFor, HaveChildren, WantChildren, Alcohol, Smoking, RecreationalDrugs, Religion, Interests, Headline, Description, DealBreakers, Profession, CreatedDate) ";
        sql += "VALUES ('" + UserId + "', '" + UserName + "', '" + SelectGender.SelectedValue + "', '" + BirthDateString + "', '" + SelectEthnicity.SelectedValue + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '" + CreatedDate + "')";

        InsertCommand.CommandText = sql;

        SendNotification.SendWelcome(SelectEmail.Text, UserName);

        try
        {
            cnn.Open();
            InsertCommand.ExecuteNonQuery();
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

    protected void CompleteWizardStep1_Activate(object sender, EventArgs e)
    {
        Response.Redirect("~/CreateProfile.aspx");
    }
}
