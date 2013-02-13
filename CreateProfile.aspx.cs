using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class CreateProfile : System.Web.UI.Page
{
    public MessageControl pm;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateDropDownLists();
        }
        /*
        if (User.Identity.IsAuthenticated)
        {
            if (!Page.IsPostBack)
            {
                populateDropDownLists();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        */
        Label1.Text = User.Identity.Name;

        if (Profession.Text == "")
        {
            Profession.Text = "None";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ImageFileUpload.HasFile == false)
        {
            Label2.Text = "You must select and image before trying to upload it.";
            return;
        }

        try
        {
            Bitmap testBmp = new Bitmap(ImageFileUpload.PostedFile.InputStream, true);
        }
        catch (ArgumentException errArgument)
        {
            // The file wasn't a valid jpg file
            Label2.Text = "The selected file wasn't a valid image.";
            return;
        }

        UploadImage();

        string LastUpdatedDate = DateTime.Now.ToString("s");

        List<string> updateStrings = new List<string>();

        updateStrings.Add("Interests = '" + Interests.Text + "'");
        updateStrings.Add("Headline = '" + Headline.Text + "'");
        updateStrings.Add("Description = '" + Description.Text + "'");
        updateStrings.Add("DealBreakers = '" + DealBreakers.Text + "'");
        updateStrings.Add("Profession = '" + Profession.Text + "'");
        updateStrings.Add("City = '" + City.Text + "'");
        updateStrings.Add("State = '" + SelectState.SelectedValue + "'");
        updateStrings.Add("Zip = '" + PostalCode.Text + "'");
        updateStrings.Add("Height = '" + SelectHeight.SelectedValue + "'");
        updateStrings.Add("LookingFor = '" + SelectLookingFor.SelectedValue + "'");
        updateStrings.Add("Seeking = '" + SelectSeeking.SelectedValue + "'");
        updateStrings.Add("BodyType = '" + SelectBodyType.SelectedValue + "'");
        updateStrings.Add("HairColor = '" + SelectHairColor.SelectedValue + "'");
        updateStrings.Add("EyeColor = '" + SelectEyeColor.SelectedValue + "'");
        updateStrings.Add("HaveChildren = '" + SelectHaveChildren.SelectedValue + "'");
        updateStrings.Add("MaritalStatus = '" + SelectMaritalStatus.SelectedValue + "'");
        updateStrings.Add("Religion = '" + SelectReligion.SelectedValue + "'");
        updateStrings.Add("RecreationalDrugs = '" + SelectRecreationalDrugs.SelectedValue + "'");
        updateStrings.Add("Smoking = '" + SelectSmoking.SelectedValue + "'");
        updateStrings.Add("Alcohol = '" + SelectAlcohol.SelectedValue + "'");
        updateStrings.Add("WantChildren = '" + SelectWantChildren.SelectedValue + "'");
        updateStrings.Add("CreatedProfile = '1'");

        string[] updateStringArray = updateStrings.ToArray();

        string updateString = String.Join(",", updateStringArray);

        Trace.Write("updateString", updateString);

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand UpdateCommand = new SqlCommand();
        UpdateCommand.Connection = cnn;
        string sql;

        sql = "UPDATE UserProfileTable ";
        sql += "SET " + updateString + " ";
        sql += "WHERE UserName = '" + Profile.UserName + "'";

        Trace.Write("sql", sql.ToString());

        UpdateCommand.CommandText = sql;
        try
        {
            cnn.Open();
            UpdateCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Trace.Write(ex.ToString());
        }
        finally
        {
            cnn.Close();

            Response.Redirect("Default.aspx");
        }
    }

    protected void populateDropDownLists()
    {
        SelectState.DataSource = UserProfile.ListState();
        SelectState.DataBind();

        SelectHaveChildren.DataSource = UserProfile.ListHaveChildren();
        SelectHaveChildren.DataBind();

        SelectMaritalStatus.DataSource = UserProfile.ListMaritalStatus();
        SelectMaritalStatus.DataBind();

        SelectHeight.DataSource = UserProfile.ListHeight();
        SelectHeight.DataBind();

        SelectLookingFor.DataSource = UserProfile.ListLookingFor();
        SelectLookingFor.DataBind();

        SelectSeeking.DataSource = UserProfile.ListSeeking();
        SelectSeeking.DataBind();

        SelectBodyType.DataSource = UserProfile.ListBodyType();
        SelectBodyType.DataBind();

        SelectHairColor.DataSource = UserProfile.ListHairColor();
        SelectHairColor.DataBind();

        SelectEyeColor.DataSource = UserProfile.ListEyeColor();
        SelectEyeColor.DataBind();

        SelectReligion.DataSource = UserProfile.ListReligion();
        SelectReligion.DataBind();

        SelectRecreationalDrugs.DataSource = UserProfile.ListRecreationalDrugs();
        SelectRecreationalDrugs.DataBind();

        SelectSmoking.DataSource = UserProfile.ListSmoking();
        SelectSmoking.DataBind();

        SelectAlcohol.DataSource = UserProfile.ListAlcohol();
        SelectAlcohol.DataBind();

        SelectWantChildren.DataSource = UserProfile.ListWantChildren();
        SelectWantChildren.DataBind();
    }

    protected void UploadImage()
    {
        ImageResizer fullSizeImage = new ImageResizer(ImageFileUpload.PostedFile.InputStream, 75, 300, 250);
        ImageResizer thumbnailImage = new ImageResizer(ImageFileUpload.PostedFile.InputStream, 75, 120, 100);
        string fileName = ImageFileUpload.FileName;
        int fileNameIncrement = 0;
        int splitOn = fileName.LastIndexOf('.');
        int fileNameLength = fileName.Length;
        string fileNameBefore = fileName.Substring(0, (splitOn));
        string file_ext = "jpg";
        string file = fileNameBefore + "-" + fileNameIncrement + "." + file_ext;
        string saveTo = Path.Combine(Server.MapPath("~/uploads"), file);
        string thumbNailFile = fileNameBefore + "-" + fileNameIncrement + "_thumb." + file_ext;
        string thumbNailSaveTo = Path.Combine(Server.MapPath("~/uploads"), thumbNailFile);
        FileInfo fi = new FileInfo(saveTo);
        FileInfo thumbFi = new FileInfo(thumbNailSaveTo);

        while (fi.Exists)
        {
            file = fileNameBefore + "-" + fileNameIncrement + "." + file_ext;
            saveTo = Path.Combine(Server.MapPath("~/uploads"), file);
            fi = new FileInfo(saveTo);

            thumbNailFile = fileNameBefore + "-" + fileNameIncrement + "_thumb." + file_ext;
            thumbNailSaveTo = Path.Combine(Server.MapPath("~/uploads"), thumbNailFile);
            thumbFi = new FileInfo(thumbNailSaveTo);

            if (fi.Exists)
            {
                fileNameIncrement++;
            }
        }

        fullSizeImage.Save(saveTo);
        thumbnailImage.Save(thumbNailSaveTo);

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = cnn;
        string sql;
        sql = "INSERT INTO UserProfileImages (UserName, IsMain, BaseFileName) VALUES ('" + Profile.UserName + "', '1', '" + fileNameBefore + "-" + fileNameIncrement + "')";
        InsertCommand.CommandText = sql;
        try
        {
            cnn.Open();
            InsertCommand.ExecuteNonQuery();
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
}
