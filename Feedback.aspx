<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" Style="left: 100px; position: absolute; top: 100px" runat="server">From:   

        </asp:Label>

        <asp:TextBox ID="txtFrom" Style="left: 200px; position: absolute; top: 100px"

          runat="server"></asp:TextBox>

        <asp:RequiredFieldValidator ID="FromValidator1" Style="left: 100px; position: absolute;

            top: 375px" runat="server" ErrorMessage="Please Enter the Email From." Width="200px"

            Height="23px" ControlToValidate="txtFrom"></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="FromValidator2" Style="left: 100px; position: absolute;

            top: 400px" runat="server" ErrorMessage="Please Enter a Valid From Email address"

            ControlToValidate="txtFrom" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

        <asp:Label ID="Label2" Style="left: 100px; position: absolute; top: 125px" runat="server">To: 

        </asp:Label>

        <asp:TextBox ID="txtTo" Style="left: 200px; position: absolute; top: 125px"

          runat="server"></asp:TextBox>

        <asp:RequiredFieldValidator ID="ToValidator1" Style="left: 100px; position: absolute;

            top: 425px" runat="server" ErrorMessage="Please Enter the Email To." Width="200px"

            Height="23px" ControlToValidate="txtTo"></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="ToValidator2" Style="left: 100px; position: absolute;

            top: 450px" runat="server" ErrorMessage="Please Enter a Valid To Email address"

            ControlToValidate="txtTo" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

        <asp:Label ID="Label3" Style="left: 100px; position: absolute; top: 150px"

          runat="server">Subject</asp:Label>

        <asp:TextBox ID="txtSubject" Style="left: 200px; position: absolute; top: 150px"

            runat="server"></asp:TextBox>

        <asp:Label ID="Label4" Style="left: 100px; position: absolute; top: 175px" runat="server">Mail:

        </asp:Label>

        <textarea runat="server" id="txtContent" style="left: 200px; width: 400px; position: absolute;

            top: 175px; height: 125px" rows="7" cols="24">

                </textarea>

        <asp:Button ID="btnSend" Style="left: 200px; position: absolute; top: 350px" runat="server"

            Text="Send" OnClick="btnSend_Click"></asp:Button>

        <asp:Label ID="lblStatus" Style="left: 250px; position: absolute; top: 350px" runat="server"> 

        </asp:Label>
    </form>
</body>
</html>
