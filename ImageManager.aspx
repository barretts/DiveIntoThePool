<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImageManager.aspx.cs" Inherits="ImageManager" Title="DiveIntoThePool.com" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="right"><img src="images/160x600.gif" /></div>
    <div style="margin-bottom: 10px">
        Welcome <asp:Label ID="Label1" runat="server" Text=""></asp:Label>! Here you can edit, add or remove your profile images.
    </div>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    Upload another image:
    <asp:FileUpload ID="ImageFileUpload" runat="server" />
    <asp:Button ID="UploadButton" runat="server" Text="Upload" onclick="UploadButton_Click" />
    <hr width="740" align="left" />
	<asp:Datalist ID="DataList1" 
	    runat="server" 
	    DataKeyField="ImageId" 
	    DataSourceID="GetImageListForUser"
	    OnDeleteCommand="DataList1_DeleteCommand"
	    OnUpdateCommand="DataList1_UpdateCommand"
	    OnItemCommand="DataList1_UpdateCaptionCommand"
	    OnItemDataBound="checkImageStatus"
	    RepeatLayout="Flow"
	    RepeatDirection="Horizontal">
        <ItemTemplate>
            <div class="left" style="padding-left: 32px; padding-right: 32px; width: 300px">
                <asp:Image ID="fullImage" runat="server" ImageUrl=<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + ".jpg" %> AlternateText=<%# DataBinder.Eval(Container.DataItem, "Caption") %> Width="300" Height="250" style="margin-bottom:10px"/><br />
                <asp:TextBox ID="Caption" runat="server" Text=<%# DataBinder.Eval(Container.DataItem, "Caption") %> />
                <asp:Button ID="UpdateCaption" runat="server" CommandName="UpdateCaption" Text="Update Caption" />
                <br />
                <asp:Button ID="ProfileImage" runat="server" CommandName="Update" Text=<%# DataBinder.Eval(Container.DataItem, "IsMain") %> />
                <br />
                <asp:Button ID="DeleteImage" runat="server" CommandName="Delete" Text="Delete Image" />
                <hr />
            </div>
        </ItemTemplate>
    </asp:Datalist>
    <asp:SqlDataSource ID="GetImageListForUser" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MainDatabase %>" 
        SelectCommand="SELECT [BaseFileName], [Caption], [ImageId], [IsMain] FROM [UserProfileImages] WHERE ([UserName] = @UserName) ORDER BY [IsMain] DESC"
        DeleteCommand="DELETE FROM [UserProfileImages] WHERE [ImageId] = @ImageId"
        UpdateCommand="UPDATE [UserProfileImages] SET [IsMain] = '1' WHERE [ImageId] = @ImageId">
        <SelectParameters>
            <asp:ProfileParameter Name="UserName" PropertyName="UserName" Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Type="Int32" 
                  Name="ImageId">
            </asp:Parameter>
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>
