<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchAdvanced.aspx.cs" Inherits="SearchSimple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="searchType" runat="server" Value="advanced" />
    <fieldset>
        <legend>Advanced Search Options</legend>
        <div class="fieldSetContent">
            Find a 
            <asp:DropDownList ID="SearchSex" runat="server">
                <asp:ListItem Value="mw">Man looking for a Woman</asp:ListItem>
                <asp:ListItem Value="wm">Woman looking for a Man</asp:ListItem>
                <asp:ListItem Value="ww">Woman looking for a Woman</asp:ListItem>
                <asp:ListItem Value="mm">Man looking for a Man</asp:ListItem>
            </asp:DropDownList>
             within 
            <asp:DropDownList ID="SearchMiles" runat="server">
                <asp:ListItem Value="25">25 miles</asp:ListItem>
                <asp:ListItem Value="50">50 miles</asp:ListItem>
                <asp:ListItem Value="100">100 miles</asp:ListItem>
                <asp:ListItem Value="150">150 miles</asp:ListItem>
                <asp:ListItem Value="200">200 miles</asp:ListItem>
            </asp:DropDownList>
             of the zipcode 
            <asp:TextBox ID="SearchZip" runat="server" maxlength="5" size="5"></asp:TextBox>
            <br />
            <br />
            Ethnicity:&nbsp;
            <asp:DropDownList ID="SelectEthnicity" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            BodyType:&nbsp;
            <asp:DropDownList ID="SelectBodyType" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            HairColor:&nbsp;
            <asp:DropDownList ID="SelectHairColor" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            EyeColor:&nbsp;
            <asp:DropDownList ID="SelectEyeColor" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            HaveChildren:&nbsp;
            <asp:DropDownList ID="SelectHaveChildren" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            MaritalStatus:&nbsp;
            <asp:DropDownList ID="SelectMaritalStatus" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            Religion:&nbsp;
            <asp:DropDownList ID="SelectReligion" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            RecreationalDrugs:&nbsp;
            <asp:DropDownList ID="SelectRecreationalDrugs" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            Smoking:&nbsp;
            <asp:DropDownList ID="SelectSmoking" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            Alcohol:&nbsp;
            <asp:DropDownList ID="SelectAlcohol" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            WantChildren:&nbsp;
            <asp:DropDownList ID="SelectWantChildren" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            Height:&nbsp;
            <asp:DropDownList ID="SelectHeight" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            LookingFor:&nbsp;
            <asp:DropDownList ID="SelectLookingFor" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <br />
            <br />
            Seeking:&nbsp;
            <asp:DropDownList ID="SelectSeeking" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
        </div>
    </fieldset>
    <asp:Button ID="Button1" Text="Find Profiles" PostBackUrl="SearchProfiles.aspx" runat="server" />
</asp:Content>

