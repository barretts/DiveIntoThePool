<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pmView.aspx.cs" Inherits="PrivateMessages" Title="DiveIntoThePool.com" EnableEventValidation="false" Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="navContainer">
        <ul class="navlist">
            <li><a href="pmInbox.aspx">Inbox</a></li>
            <li><a href="pmSent.aspx">Sent Items</a></li>
            <li><a href="pmSend.aspx">Send Private Message</a></li>
        </ul>
    </div>
    <div class="pmControls">
        <asp:Button ID="ReplyButton" runat="server" Text="Reply" OnClick="ReplyToMessage" />
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClick="DeleteMessage" />
    </div>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "MessageId")%>' />
            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ToViewed"))%>' />
            <div>
                <div class="viewHeader">
                    <%#pm.getUserName(DataBinder.Eval(Container.DataItem, "FromUserId") as string)%> - 
                    <%#DataBinder.Eval(Container.DataItem, "Title")%> - 
                    <%#DataBinder.Eval(Container.DataItem, "Created")%>
                </div>
                <div class="viewMessage">
                    <%#DataBinder.Eval(Container.DataItem, "Message")%>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
