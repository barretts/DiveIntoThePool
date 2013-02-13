using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchSimple : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateDropDownLists();
    }

    protected void populateDropDownLists()
    {
        SelectEthnicity.DataSource = UserProfile.ListEthnicity();
        SelectEthnicity.DataBind();
        SelectEthnicity.Items.Insert(0, "No Preference");
        SelectEthnicity.SelectedIndex = 0;

        SelectBodyType.DataSource = UserProfile.ListBodyType();
        SelectBodyType.DataBind();
        SelectBodyType.Items.Insert(0, "No Preference");
        SelectBodyType.SelectedIndex = 0;

        SelectHaveChildren.DataSource = UserProfile.ListHaveChildren();
        SelectHaveChildren.DataBind();
        SelectHaveChildren.Items.Insert(0, "No Preference");
        SelectHaveChildren.SelectedIndex = 0;

        SelectMaritalStatus.DataSource = UserProfile.ListMaritalStatus();
        SelectMaritalStatus.DataBind();
        SelectMaritalStatus.Items.Insert(0, "No Preference");
        SelectMaritalStatus.SelectedIndex = 0;

        SelectHeight.DataSource = UserProfile.ListHeight();
        SelectHeight.DataBind();
        SelectHeight.Items.Insert(0, "No Preference");
        SelectHeight.SelectedIndex = 0;

        SelectLookingFor.DataSource = UserProfile.ListLookingFor();
        SelectLookingFor.DataBind();
        SelectLookingFor.Items.Insert(0, "No Preference");
        SelectLookingFor.SelectedIndex = 0;

        SelectSeeking.DataSource = UserProfile.ListSeeking();
        SelectSeeking.DataBind();
        SelectSeeking.Items.Insert(0, "No Preference");
        SelectSeeking.SelectedIndex = 0;

        SelectHairColor.DataSource = UserProfile.ListHairColor();
        SelectHairColor.DataBind();
        SelectHairColor.Items.Insert(0, "No Preference");
        SelectHairColor.SelectedIndex = 0;

        SelectEyeColor.DataSource = UserProfile.ListEyeColor();
        SelectEyeColor.DataBind();
        SelectEyeColor.Items.Insert(0, "No Preference");
        SelectEyeColor.SelectedIndex = 0;

        SelectReligion.DataSource = UserProfile.ListReligion();
        SelectReligion.DataBind();
        SelectReligion.Items.Insert(0, "No Preference");
        SelectReligion.SelectedIndex = 0;

        SelectRecreationalDrugs.DataSource = UserProfile.ListRecreationalDrugs();
        SelectRecreationalDrugs.DataBind();
        SelectRecreationalDrugs.Items.Insert(0, "No Preference");
        SelectRecreationalDrugs.SelectedIndex = 0;

        SelectSmoking.DataSource = UserProfile.ListSmoking();
        SelectSmoking.DataBind();
        SelectSmoking.Items.Insert(0, "No Preference");
        SelectSmoking.SelectedIndex = 0;

        SelectAlcohol.DataSource = UserProfile.ListAlcohol();
        SelectAlcohol.DataBind();
        SelectAlcohol.Items.Insert(0, "No Preference");
        SelectAlcohol.SelectedIndex = 0;

        SelectWantChildren.DataSource = UserProfile.ListWantChildren();
        SelectWantChildren.DataBind();
        SelectWantChildren.Items.Insert(0, "No Preference");
        SelectWantChildren.SelectedIndex = 0;
    }
}
