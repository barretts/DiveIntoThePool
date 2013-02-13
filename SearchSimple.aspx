<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchSimple.aspx.cs" Inherits="SearchSimple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="searchType" runat="server" Value="simple" />
    <h3>Simple Search Page</h3>
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
    <asp:Button ID="Button1" Text="Submit" PostBackUrl="SearchProfiles.aspx" runat="server" />
</asp:Content>

