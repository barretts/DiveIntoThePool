<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Trace="true" AutoEventWireup="true" CodeFile="SearchGet.aspx.cs" Inherits="SearchGet" Title="DiveIntoThePool.com" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    I'm a 
    <asp:DropDownList ID="SearchSex" runat="server">
        <asp:ListItem Value="w">Woman</asp:ListItem>
        <asp:ListItem Value="m">Man</asp:ListItem>
    </asp:DropDownList>
     seeking a 
    <asp:DropDownList ID="SeekingSex" runat="server">
        <asp:ListItem Value="w">Woman</asp:ListItem>
        <asp:ListItem Value="m" Selected>Man</asp:ListItem>
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
     and arrange by 
    <asp:DropDownList ID="ArrangeBy" runat="server">
        <asp:ListItem Value="last">Recently Active</asp:ListItem>
        <asp:ListItem Value="newest">Newest Members</asp:ListItem>
        <asp:ListItem Value="distance">Distance</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="SubmitSearch" runat="server" Text="Find Profiles" OnClick="GetSearchResults_Click" />
    <div>Want to refine your search a little more? Try the <a href="SearchAdvanced.aspx">Advanced Search</a> page!</div>
    <div class="clearer"></div>
    <hr />
    <asp:Panel ID="SampleSearchDisplay" runat="server" Visible="false">
    <h2 class="h-top">Take a look at some of our last active members in the pool!</h2>
    </asp:Panel>
    <div class="right"><img src="images/160x600.gif" /></div>
    <asp:Panel ID="SearchDisplay" runat="server" Visible="false">
        <asp:Repeater ID="SearchRepeater" runat="server">
            <ItemTemplate>
                <div class="searchResultRow searchWidth">
                <a href="DisplayProfile.aspx?user=<%#DataBinder.Eval(Container.DataItem, "UserName")%>">
                    <div class="searchResultImage">
                        <asp:Image ID="thumbnailImage" runat="server" ImageUrl=<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + "_thumb.jpg" %> AlternateText=<%# DataBinder.Eval(Container.DataItem, "Caption") %> Width="120" Height="100" />
                    </div>
                    <div class="searchResultDetails">
                        <%#DataBinder.Eval(Container.DataItem, "UserName")%>, <%#DataBinder.Eval(Container.DataItem, "Age")%><br />
                        <span class="search-headline">&ldquo;<%#DataBinder.Eval(Container.DataItem, "Headline")%>&rdquo;</span><br />
                        <div class="search-description"><%#truncateString(DataBinder.Eval(Container.DataItem, "Description").ToString(), 150)%></div>
                        <div class="search-distance"><%#DataBinder.Eval(Container.DataItem, "DistanceRounded")%></div>
                        <%#truncateString(UserProfile.GetGender(DataBinder.Eval(Container.DataItem, "Gender").ToString()), 1)%>S<%#truncateString(UserProfile.GetSeeking(DataBinder.Eval(Container.DataItem, "Seeking").ToString()), 1)%> for <%#UserProfile.GetLookingFor(DataBinder.Eval(Container.DataItem, "LookingFor").ToString())%>
                        <%/*#DataBinder.Eval(Container.DataItem, "LastActivityDate")*/%>
                    </div>
                </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="SearchPaging" runat="server" Visible="false">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="RegisterDisplay" runat="server" Visible="false">
    <div class="left" style="width:690px">
        <h2>Register now to jump into the pool!</h2>
        <p>Thanks for searching Dive Into The Pool to find members that meet your criteria but in order to see more results you'll need to register. Registration is <font color="green"><strong>QUICK, EASY</strong></font> and the site is <font color="red"><strong>100% FREE</strong></font> so don't wait any longer to find your perfect match <strong><a href="Register.aspx?source=SearchProfiles">REGISTER NOW</a></strong>!</p>
        <h2>Already a member?<br />Dive back in!</h2>
        <asp:LoginView ID="LoginView1" runat="server">
            <LoggedInTemplate>
                Welcome <asp:Label runat="server" ID="labelName"></asp:Label>
            </LoggedInTemplate>
            <AnonymousTemplate>
                <asp:Login ID="Login1" runat="server">
                </asp:Login>
            </AnonymousTemplate>
        </asp:LoginView>
    </div>
    </asp:Panel>
    <div class="clearer"></div>
</asp:Content>