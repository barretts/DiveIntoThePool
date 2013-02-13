<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="DiveIntoThePool.com" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="twoColumn-wide left">
    <h2>Register now to jump into the pool!</h2>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser" ContinueDestinationPageUrl="~/CreateProfile.aspx" DisableCreatedUser="false">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep0" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>New User Name:</td>
                            <td>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="UserName" 
                                    ErrorMessage="Username is required." />
                            </td>
                        </tr>
                        <tr>
                            <td>Password:</td>
                            <td>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="Password" 
                                    ErrorMessage="Password is required." />
                            </td>
                        </tr>
                        <tr>
                            <td>Confirm Password:</td>
                            <td>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Confirm Password is required." />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                 <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Email:</td>
                            <td>
                                <asp:TextBox runat="server" ID="Email" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="Email" 
                                    ErrorMessage="Email is required." />
                            </td>
                        </tr>
                        <tr>
                            <td>Confirm Email:</td>
                            <td>
                                <asp:TextBox runat="server" ID="ConfirmEmail" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ConfirmEmail" 
                                    ErrorMessage="Email is required." />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                 <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="Email"
                                        ControlToValidate="ConfirmEmail" Display="Dynamic" ErrorMessage="The Email and Confirmation Email must match."></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Gender:</td>
                            <td>
                                <asp:DropDownList ID="SelectGender" runat="server" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Birthdate:</td>
                            <td>
                                <asp:DropDownList ID="SelectBirthMonth" runat="server" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelectBirthDay" runat="server" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>, 
                                <asp:DropDownList ID="SelectBirthYear" runat="server" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Ethnicity:</td>
                            <td>
                                <asp:DropDownList ID="SelectEthnicity" runat="server" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server" ID="CompleteWizardStep1" OnActivate="CompleteWizardStep1_Activate">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</div>
<div class="twoColumn-thin right">
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
<div class="clearer"></div>
</asp:Content>