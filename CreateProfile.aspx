<%@ Page Language="C#" MasterPageFile="~/MasterPage_menuless.master" AutoEventWireup="true" CodeFile="CreateProfile.aspx.cs" Inherits="CreateProfile" Trace="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Welcome <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>! Only a few more details to fill out and you're done!</h2>
    <fieldset>
        <legend>Profile Details</legend>
        <div class="fieldSetContent">
            <div class="edit-block">
                <span class="edit-label">Write a headline that others will see when trying to find you:</span>
                <br/>
                <asp:TextBox ID="Headline" runat="server" Width="710" MaxLength="40"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Headline" Text="<br/>Sorry but you're going to need a headline to be found in the pool." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Give others a good description of yourself, the more descriptive you are the better results you will get from your search in the pool:</span>
                <br/>
                <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Width="710" Rows="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Description" Text="<br/>You need to tell us a little more about yourself please." runat="server" SetFocusOnError="true" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="Description" ValidationExpression="^(.){0,1500}" ErrorMessage="<br/>It seems you have plenty to say but keep your description under 1500 characters please." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">What activity are you primarly looking for in the pool?</span>&nbsp;
                <asp:DropDownList ID="SelectLookingFor" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="SelectLookingFor" InitialValue="0" Text="<br/>Please let everyone know what activity you're looking to do with other pool members." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">What gender are you looking for to join you in the activity?</span>&nbsp;
                <asp:DropDownList ID="SelectSeeking" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
            <div class="edit-block">
                <span class="edit-label">List some of your interests, seperate each interests by a comma:</span>
                <br />
                <asp:TextBox ID="Interests" runat="server" Width="710"></asp:TextBox>
            </div>
            <div class="edit-block">
                <span class="edit-label">List some of your deal breakers, seperate each deal breaker by a comma:</span>
                <br />
                <asp:TextBox ID="DealBreakers" runat="server" Width="710"></asp:TextBox>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Personal Details</legend>
        <div class="fieldSetContent">
            <div class="edit-block">
                <span class="edit-label">What is your profession?</span>&nbsp;<asp:TextBox ID="Profession" MaxLength="256" runat="server"></asp:TextBox>
            </div>
            <div class="edit-block">
                <span class="edit-label">What is your current marital status?:</span>&nbsp;
                <asp:DropDownList ID="SelectMaritalStatus" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="SelectMaritalStatus" InitialValue="0" Text="<br/>Please let everyone know what your marital status is." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Do you currently have any children?</span>&nbsp;
                <asp:DropDownList ID="SelectHaveChildren" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="SelectHaveChildren" InitialValue="0" Text="<br/>Please let everyone know if you have any children." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Do you want to have any children at some point in the future?</span>&nbsp;
                <asp:DropDownList ID="SelectWantChildren" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="SelectWantChildren" InitialValue="0" Text="<br/>Please let everyone know if you have want children at some point in the future." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">What religion do you practice if any?</span>&nbsp;
                <asp:DropDownList ID="SelectReligion" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
            <div class="edit-block">
                <span class="edit-label">Do you smoke?</span>&nbsp;
                <asp:DropDownList ID="SelectSmoking" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="SelectSmoking" InitialValue="0" Text="<br/>Please let everyone know if you smoke." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Do you drink alcohol?</span>&nbsp;
                <asp:DropDownList ID="SelectAlcohol" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="SelectAlcohol" InitialValue="0" Text="<br/>Please let everyone know if you drink alcohol." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Do you use any drugs recreationally?</span>&nbsp;
                <asp:DropDownList ID="SelectRecreationalDrugs" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="SelectRecreationalDrugs" InitialValue="0" Text="<br/>Please let everyone know if you user drugs recreationally." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Physical Description</legend>
        <div class="fieldSetContent">
            <div class="edit-block">
                <span class="edit-label">Body Type:</span>&nbsp;
                <asp:DropDownList ID="SelectBodyType" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="SelectBodyType" InitialValue="0" Text="<br/>Please let everyone know what you think your body type is." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">Hair Color:</span>&nbsp;
                <asp:DropDownList ID="SelectHairColor" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
            <div class="edit-block">
                <span class="edit-label">Eye Color:</span>&nbsp;
                <asp:DropDownList ID="SelectEyeColor" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
            <div class="edit-block">
                <span class="edit-label">Height:</span>&nbsp;
                <asp:DropDownList ID="SelectHeight" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Location Information</legend>
        <div class="fieldSetContent">
            <div class="edit-block">
                <span class="edit-label">Zip:</span>&nbsp;<asp:TextBox ID="PostalCode" Columns="5" MaxLength="5" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="PostalCode" ValidationExpression="^[0-9]{5}$" ErrorMessage="<br/>The zip code must be 5 numeric digits!" runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">City:</span>&nbsp;<asp:TextBox ID="City" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="City" Text="<br/>Please let everyone know what city you live in." runat="server" SetFocusOnError="true" Display="Dynamic" />
            </div>
            <div class="edit-block">
                <span class="edit-label">State:</span>&nbsp;
                <asp:DropDownList ID="SelectState" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </div>
        </div>
    </fieldset>
    <table>
        <tr>
            <td colspan="2"><asp:Label ID="Label2" runat="server" Text=""></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ImageFileUpload" Text="Your main profile image of yourself is required, you like to see profiles with photos and so does everyone else." runat="server" SetFocusOnError="true" Display="Dynamic" /></td>
        </tr>
	    <tr>
		    <td><span class="edit-label">Select your main profile image:</span></td>
		    <td>
			    <asp:FileUpload ID="ImageFileUpload" runat="server" Width="350px" />
		    </td>
	    </tr>
    </table>
    <br />
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Finish Registration" />
</asp:Content>