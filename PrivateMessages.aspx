<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PrivateMessages.aspx.cs" Inherits="PrivateMessages" Title="DiveIntoThePool.com" EnableEventValidation="false" Trace="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Inbox</h2>
    <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="DeleteMessagesFrom_Inbox" />
    <asp:Button ID="Button2" runat="server" Text="Mark as read" OnClick="MarkMessages_Read" />
    <asp:Button ID="Button3" runat="server" Text="Mark as unread" OnClick="MarkMessages_Unread" />
    <br />
    Select: <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Select_All">All</asp:LinkButton>, <asp:LinkButton ID="LinkButton2" runat="server" OnClick="Select_None">None</asp:LinkButton>, <asp:LinkButton ID="LinkButton3" runat="server" OnClick="Select_Read">Read</asp:LinkButton>, <asp:LinkButton ID="LinkButton4" runat="server" OnClick="Select_Unread">Unread</asp:LinkButton>
    <br />
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "MessageId")%>' />
            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ToViewed"))%>' />
            <div class="<%#ReadStatus(Convert.ToString(DataBinder.Eval(Container.DataItem, "ToViewed")))%>_text">
                <asp:CheckBox ID="CheckBox1" runat="server" /> 
                <%#pm.getUserName(DataBinder.Eval(Container.DataItem, "FromUserId") as string)%> - 
                <%#DataBinder.Eval(Container.DataItem, "Title")%> - 
                <%#DataBinder.Eval(Container.DataItem, "Created")%>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <h2>Sent Items</h2>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <%#pm.getUserName(DataBinder.Eval(Container.DataItem, "ToUserId") as string)%> - <%#DataBinder.Eval(Container.DataItem, "Title")%> - <%#DataBinder.Eval(Container.DataItem, "Created")%><br />
        </ItemTemplate>
    </asp:Repeater>
    <hr />
    <asp:Label ID="SendError" runat="server"></asp:Label><br />
    <asp:TextBox ID="SendTo" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="SendTitle" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="SendMessage" runat="server"></asp:TextBox><br />
    <asp:Button ID="SendSubmit" runat="server" OnClick="SendSubmit_Click" Text="Send Message" />
</asp:Content>
