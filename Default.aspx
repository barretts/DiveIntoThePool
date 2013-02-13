<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="DiveIntoThePool.com" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="twoColumn-wide left">
        <h2 class="h-top">Welcome to the FREE online dating pool!</h2>
        <p>Thanks for joining us at the best free online dating
        website on the internet!<br />If you're ready to dive into the
        pool to find and be found by others then <a href="Register.aspx">register now</a> and
        get started! Otherwise feel free to stand on the other side
        of the fence and simply <a href="SearchProfiles.aspx">browse our current members</a> until
        you're confident enough to get you're feet wet!</p>
    </div>
    <div class="twoColumn-thin right">
        <asp:LoginView ID="LoginView1" runat="server">
            <LoggedInTemplate>
                <div class="right"><img src="images/300x250.gif" /></div>
            </LoggedInTemplate>
            <AnonymousTemplate>
                <asp:Login ID="Login1" runat="server">
                </asp:Login>
                Not yet a member? <a href="Register.aspx">Register now!</a>
            </AnonymousTemplate>
        </asp:LoginView>
    </div>
    <div class="clearer"></div>
    <hr />
    <h2>Take a look at some of our last active members in the pool!</h2>
    <asp:Panel ID="SearchDisplay" runat="server">
        <asp:Repeater ID="SearchRepeater" runat="server">
            <ItemTemplate>
                <div class="searchResultRow">
                <a href="DisplayProfile.aspx?user=<%#DataBinder.Eval(Container.DataItem, "UserName")%>">
                    <div class="searchResultImage">
                        <asp:Image ID="thumbnailImage" runat="server" ImageUrl=<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + "_thumb.jpg" %> AlternateText=<%# DataBinder.Eval(Container.DataItem, "Caption") %> Width="120" Height="100" />
                    </div>
                    <div class="searchResultDetails">
                        <%#DataBinder.Eval(Container.DataItem, "UserName")%>, <%#DataBinder.Eval(Container.DataItem, "Age")%><br />
                        <span class="search-headline">&ldquo;<%#DataBinder.Eval(Container.DataItem, "Headline")%>&rdquo;</span><br />
                        <div class="search-description"><%#truncateString(DataBinder.Eval(Container.DataItem, "Description").ToString(), 150)%></div>
                        <div class="search-end"><%#truncateString(UserProfile.GetGender(DataBinder.Eval(Container.DataItem, "Gender").ToString()), 1)%>S<%#truncateString(UserProfile.GetSeeking(DataBinder.Eval(Container.DataItem, "Seeking").ToString()), 1)%> for <%#UserProfile.GetLookingFor(DataBinder.Eval(Container.DataItem, "LookingFor").ToString())%></div>
                        <%/*#DataBinder.Eval(Container.DataItem, "LastActivityDate")*/%>
                        <%/*#DataBinder.Eval(Container.DataItem, "Distance")*/%>
                    </div>
                </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>