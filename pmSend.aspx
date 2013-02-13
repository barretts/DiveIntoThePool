<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pmSend.aspx.cs" Inherits="PrivateMessages" Title="DiveIntoThePool.com" EnableEventValidation="false" Trace="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="navContainer">
        <ul class="navlist">
            <li><a href="pmInbox.aspx">Inbox</a></li>
            <li><a href="pmSent.aspx">Sent Items</a></li>
            <li><a class="currentPage">Send Private Message</a></li>
        </ul>
    </div>
    To: <asp:TextBox ID="SendTo" runat="server"></asp:TextBox><br />
    Title: <asp:TextBox ID="SendTitle" runat="server"></asp:TextBox><br />
    Message:<br />
    <FCKeditorV2:FCKeditor ID="SendMessage" runat="server" BasePath="~/js/fckeditor/">
    </FCKeditorV2:FCKeditor>
    <asp:Label ID="SendError" runat="server"></asp:Label><br />
    <asp:Button ID="SendSubmit" runat="server" OnClick="SendSubmit_Click" Text="Send Message" />
</asp:Content>
