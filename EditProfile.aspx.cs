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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class EditProfile : System.Web.UI.Page
{
    private bool GenderChanged = false;
    private bool BirthDateChanged = false;
    private bool EthnicityChanged = false;
    private bool CountryChanged = false;
    private bool CityChanged = false;
    private bool StateChanged = false;
    private bool ZipChanged = false;
    private bool MaritalStatusChanged = false;
    private bool HeightChanged = false;
    private bool BodyTypeChanged = false;
    private bool HairColorChanged = false;
    private bool EyeColorChanged = false;
    private bool SeekingChanged = false;
    private bool LookingForChanged = false;
    private bool HaveChildrenChanged = false;
    private bool WantChildrenChanged = false;
    private bool AlcoholChanged = false;
    private bool SmokingChanged = false;
    private bool RecreationalDrugsChanged = false;
    private bool ReligionChanged = false;
    private bool HeadlineChanged = false;
    private bool InterestsChanged = false;
    private bool DescriptionChanged = false;
    private bool DealBreakersChanged = false;
    private bool ProfessionChanged = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            MessageControl pm = new MessageControl(User.Identity.Name);

            if (pm.getCreatedProfile(User.Identity.Name) != "1")
            {
                Response.Redirect("CreateProfile.aspx");
            }

            if (!Page.IsPostBack)
            {
                populateDropDownLists();
                selectDropDownValues();
            }
        }
        else
        {
            Response.Redirect("Login.aspx?ReturnUrl=EditProfile.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string LastUpdatedDate = DateTime.Now.ToString("s");

        List<string> updateStrings = new List<string>();

        if (InterestsChanged)
            updateStrings.Add("Interests = '" + Interests.Text + "'");

        if (HeadlineChanged)
            updateStrings.Add("Headline = '" + Headline.Text + "'");

        if (DescriptionChanged)
            updateStrings.Add("Description = '" + Description.Text + "'");

        if (DealBreakersChanged)
            updateStrings.Add("DealBreakers = '" + DealBreakers.Text + "'");

        if (ProfessionChanged)
            updateStrings.Add("Profession = '" + Profession.Text + "'");

        if (GenderChanged)
            updateStrings.Add("Gender = '" + SelectGender.SelectedValue + "'");

        if (EthnicityChanged)
            updateStrings.Add("Ethnicity = '" + SelectEthnicity.SelectedValue + "'");

        if (CityChanged)
            updateStrings.Add("City = '" + City.Text + "'");

        if (StateChanged)
            updateStrings.Add("State = '" + SelectState.SelectedValue + "'");

        if (ZipChanged)
            updateStrings.Add("Zip = '" + PostalCode.Text + "'");

        if (HeightChanged)
            updateStrings.Add("Height = '" + SelectHeight.SelectedValue + "'");

        if (LookingForChanged)
            updateStrings.Add("LookingFor = '" + SelectLookingFor.SelectedValue + "'");

        if (SeekingChanged)
            updateStrings.Add("Seeking = '" + SelectSeeking.SelectedValue + "'");

        if (BodyTypeChanged)
            updateStrings.Add("BodyType = '" + SelectBodyType.SelectedValue + "'");

        if (HairColorChanged)
            updateStrings.Add("HairColor = '" + SelectHairColor.SelectedValue + "'");

        if (EyeColorChanged)
            updateStrings.Add("EyeColor = '" + SelectEyeColor.SelectedValue + "'");

        if (HaveChildrenChanged)
            updateStrings.Add("HaveChildren = '" + SelectHaveChildren.SelectedValue + "'");

        if (MaritalStatusChanged)
            updateStrings.Add("MaritalStatus = '" + SelectMaritalStatus.SelectedValue + "'");

        if (ReligionChanged)
            updateStrings.Add("Religion = '" + SelectReligion.SelectedValue + "'");

        if (RecreationalDrugsChanged)
            updateStrings.Add("RecreationalDrugs = '" + SelectRecreationalDrugs.SelectedValue + "'");

        if (SmokingChanged)
            updateStrings.Add("Smoking = '" + SelectSmoking.SelectedValue + "'");

        if (AlcoholChanged)
            updateStrings.Add("Alcohol = '" + SelectAlcohol.SelectedValue + "'");

        if (WantChildrenChanged)
            updateStrings.Add("WantChildren = '" + SelectWantChildren.SelectedValue + "'");

        string[] updateStringArray = updateStrings.ToArray();

        string updateString = String.Join(",", updateStringArray);

        Trace.Write("updateString", updateString);

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand UpdateCommand = new SqlCommand();
        UpdateCommand.Connection = cnn;
        string sql;

        sql  = "UPDATE UserProfileTable ";
        sql += "SET " + updateString + " ";
        sql += "WHERE UserName = '" + Profile.UserName + "'";
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
        }
    }

    protected void Gender_Changed(object sender, EventArgs e)
    {
        GenderChanged = true;
    }

    protected void Ethnicity_Changed(object sender, EventArgs e)
    {
        EthnicityChanged = true;
    }

    protected void State_Changed(object sender, EventArgs e)
    {
        StateChanged = true;
    }

    protected void Zip_Changed(object sender, EventArgs e)
    {
        ZipChanged = true;
    }

    protected void BodyType_Changed(object sender, EventArgs e)
    {
        BodyTypeChanged = true;
    }

    protected void HairColor_Changed(object sender, EventArgs e)
    {
        HairColorChanged = true;
    }

    protected void EyeColor_Changed(object sender, EventArgs e)
    {
        EyeColorChanged = true;
    }

    protected void HaveChildren_Changed(object sender, EventArgs e)
    {
        HaveChildrenChanged = true;
    }

    protected void Religion_Changed(object sender, EventArgs e)
    {
        ReligionChanged = true;
    }

    protected void RecreationalDrugs_Changed(object sender, EventArgs e)
    {
        RecreationalDrugsChanged = true;
    }

    protected void Smoking_Changed(object sender, EventArgs e)
    {
        SmokingChanged = true;
    }

    protected void Alcohol_Changed(object sender, EventArgs e)
    {
        AlcoholChanged = true;
    }

    protected void WantChildren_Changed(object sender, EventArgs e)
    {
        WantChildrenChanged = true;
    }

    protected void MaritalStatus_Changed(object sender, EventArgs e)
    {
        MaritalStatusChanged = true;
    }

    protected void Height_Changed(object sender, EventArgs e)
    {
        HeightChanged = true;
    }

    protected void LookingFor_Changed(object sender, EventArgs e)
    {
        LookingForChanged = true;
    }

    protected void Seeking_Changed(object sender, EventArgs e)
    {
        SeekingChanged = true;
    }

    protected void City_Changed(object sender, EventArgs e)
    {
        CityChanged = true;
    }

    protected void Headline_Changed(object sender, EventArgs e)
    {
        HeadlineChanged = true;
    }

    protected void Interests_Changed(object sender, EventArgs e)
    {
        InterestsChanged = true;
    }

    protected void Description_Changed(object sender, EventArgs e)
    {
        DescriptionChanged = true;
    }

    protected void DealBreakers_Changed(object sender, EventArgs e)
    {
        DealBreakersChanged = true;
    }

    protected void Profession_Changed(object sender, EventArgs e)
    {
        ProfessionChanged = true;
    }

    protected void populateDropDownLists()
    {
        SelectGender.DataSource = UserProfile.ListGender();
        SelectGender.DataBind();

        SelectEthnicity.DataSource = UserProfile.ListEthnicity();
        SelectEthnicity.DataBind();

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

    protected void selectDropDownValues()
    {
        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand SelectCommand = new SqlCommand();
        SelectCommand.Connection = cnn;
        string sql;
        sql = "SELECT * FROM UserProfileTable WHERE UserName = '" + Profile.UserName + "'";
        SelectCommand.CommandText = sql;
        SqlDataReader rdr = null;

        try
        {
            cnn.Open();
            rdr = SelectCommand.ExecuteReader();

            while (rdr.Read())
            {
                PostalCode.Text = (string)rdr["Zip"];
                City.Text = (string)rdr["City"];
                Interests.Text = (string)rdr["Interests"];
                Headline.Text = (string)rdr["Headline"];
                Description.Text = (string)rdr["Description"];
                DealBreakers.Text = (string)rdr["DealBreakers"];
                Profession.Text = (string)rdr["Profession"];
                SelectGender.Items.FindByValue((string)rdr["Gender"]).Selected = true;
                SelectEthnicity.Items.FindByValue((string)rdr["Ethnicity"]).Selected = true;
                SelectState.Items.FindByValue((string)rdr["State"]).Selected = true;
                SelectMaritalStatus.Items.FindByValue((string)rdr["MaritalStatus"]).Selected = true;
                SelectHeight.Items.FindByValue((string)rdr["Height"]).Selected = true;
                SelectLookingFor.Items.FindByValue((string)rdr["LookingFor"]).Selected = true;
                SelectSeeking.Items.FindByValue((string)rdr["Seeking"]).Selected = true;
                SelectBodyType.Items.FindByValue((string)rdr["BodyType"]).Selected = true;
                SelectHairColor.Items.FindByValue((string)rdr["HairColor"]).Selected = true;
                SelectEyeColor.Items.FindByValue((string)rdr["EyeColor"]).Selected = true;
                SelectHaveChildren.Items.FindByValue((string)rdr["HaveChildren"]).Selected = true;
                SelectWantChildren.Items.FindByValue((string)rdr["WantChildren"]).Selected = true;
                SelectReligion.Items.FindByValue((string)rdr["Religion"]).Selected = true;
                SelectSmoking.Items.FindByValue((string)rdr["Smoking"]).Selected = true;
                SelectAlcohol.Items.FindByValue((string)rdr["Alcohol"]).Selected = true;
                SelectRecreationalDrugs.Items.FindByValue((string)rdr["RecreationalDrugs"]).Selected = true;
            }
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
