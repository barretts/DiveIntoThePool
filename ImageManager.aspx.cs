using System;
using System.Collections;
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
using System.Text.RegularExpressions;

public partial class ImageManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            MessageControl pm = new MessageControl(User.Identity.Name);

            if (pm.getCreatedProfile(User.Identity.Name) != "1")
            {
                Response.Redirect("CreateProfile.aspx");
            }

            Label1.Text = Profile.UserName;
        }
        else
        {
            Response.Redirect("Login.aspx?ReturnUrl=ImageManager.aspx");
        }
    }

    protected void ImageUploadRequiredField_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = ImageFileUpload.HasFile;
    }

    protected void DataList1_UpdateCaptionCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "UpdateCaption")
        {
            int id = (int)DataList1.DataKeys[e.Item.ItemIndex];

            TextBox CaptionInput = (TextBox)e.Item.FindControl("Caption");

            Trace.Write("clicked", id.ToString());
            Trace.Write("caption", CaptionInput.Text);

            string caption = CaptionInput.Text.Replace("'", "''");

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
            SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
            SqlCommand InsertCommand = new SqlCommand();
            InsertCommand.Connection = cnn;
            string sql;
            sql = "UPDATE UserProfileImages SET Caption = '" + caption + "' WHERE ImageId = " + id.ToString();
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

        DataList1.DataBind();
    }

    protected void DataList1_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        int id = (int)DataList1.DataKeys[e.Item.ItemIndex];
        System.Web.UI.WebControls.Image fullImage = (System.Web.UI.WebControls.Image)e.Item.FindControl("fullImage");
        System.Web.UI.WebControls.Image thumbnailImage = (System.Web.UI.WebControls.Image)e.Item.FindControl("thumbnailImage");

        string fullImageFilePath = Server.MapPath(fullImage.ImageUrl);
        string thumbnailImageFilePath = Server.MapPath(thumbnailImage.ImageUrl);

        File.Delete(fullImageFilePath);
        File.Delete(thumbnailImageFilePath);

        GetImageListForUser.DeleteParameters["ImageId"].DefaultValue = id.ToString();
        GetImageListForUser.Delete();

        // Bind the data after the item is deleted.
        DataList1.DataBind();
    }

    protected void DataList1_UpdateCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        int id = (int)DataList1.DataKeys[e.Item.ItemIndex];
        
        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = cnn;
        string sql;
        sql = "UPDATE UserProfileImages SET IsMain = '1' WHERE ImageId = " + id.ToString();
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

        sql = "UPDATE UserProfileImages SET IsMain = '0' WHERE UserName = '" + Profile.UserName + "' AND ImageId != " + id.ToString();
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
        
        DataList1.DataBind();
    }

    protected void UploadButton_Click(object sender, EventArgs e)
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
        sql = "INSERT INTO UserProfileImages (UserName, BaseFileName) VALUES ('" + Profile.UserName + "', '" + fileNameBefore + "-" + fileNameIncrement + "')";
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

        DataList1.DataBind();
    }

    protected void checkImageStatus(object src, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Button btn = (Button)e.Item.FindControl("ProfileImage");
            Button delete_btn = (Button)e.Item.FindControl("DeleteImage");

            if (btn.Text == "1")
            {
                btn.Enabled = false;
                delete_btn.Enabled = false;
            }
            
            btn.Text = "Set as Profile Image";
        }
    }
}