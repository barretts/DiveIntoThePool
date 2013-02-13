<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pmSent.aspx.cs" Inherits="PrivateMessages" Title="DiveIntoThePool.com" EnableEventValidation="false" Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="navContainer">
        <ul class="navlist">
            <li><a href="pmInbox.aspx">Inbox</a></li>
            <li><a class="currentPage">Sent Items</a></li>
            <li><a href="pmSend.aspx">Send Private Message</a></li>
        </ul>
    </div>
    <div class="pmControls">
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClick="DeleteMessagesFrom_Sent" />
        <asp:Button ID="Button2" runat="server" Text="Mark as read" OnClick="MarkMessages_Read" />
        <asp:Button ID="Button3" runat="server" Text="Mark as unread" OnClick="MarkMessages_Unread" />
        <br />
        Select: <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Select_All">All</asp:LinkButton>, <asp:LinkButton ID="LinkButton2" runat="server" OnClick="Select_None">None</asp:LinkButton>, <asp:LinkButton ID="LinkButton3" runat="server" OnClick="Select_Read">Read</asp:LinkButton>, <asp:LinkButton ID="LinkButton4" runat="server" OnClick="Select_Unread">Unread</asp:LinkButton>
    </div>
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <div class="messages">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="messageRow <%# Container.ItemIndex % 2 == 0 ? "messageRowOdd" : "messageRowEven" %>">
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "MessageId")%>' />
            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ToViewed"))%>' />
                <div class="messageCheck"><asp:CheckBox ID="CheckBox1" runat="server" /></div>
                <a class="messageLink" href="pmView.aspx?messageId=<%#DataBinder.Eval(Container.DataItem, "MessageId")%>&source=to">
                <div class="<%#ReadStatus(Convert.ToString(DataBinder.Eval(Container.DataItem, "FromViewed")))%>_text">
                    <div class="messageUser">
                        <%#pm.getUserName(DataBinder.Eval(Container.DataItem, "ToUserId") as string)%>
                    </div>
                    <div class="messageHeadline">
                        <%#DataBinder.Eval(Container.DataItem, "Title")%>
                    </div>
                    <div class="messageDatetime">
                        <%#DataBinder.Eval(Container.DataItem, "Created")%>
                    </div>
                    <div class="clearer"></div>
                </div>
                </a>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            <div class="clearer"></div>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
