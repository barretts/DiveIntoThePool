<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Trace="false" AutoEventWireup="true" CodeFile="DisplayProfile.aspx.cs" Inherits="DisplayProfile" Title="DiveIntoThePool.com" %>
<asp:Content ID="JavaScript1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="js/jcarousellite_1.0.1.pack.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $(function() {
                $(".profileImages").jCarouselLite({
                    btnNext: ".next",
                    btnPrev: ".prev",
                    vertical: true,
                    visible: 2,
                    speed: 300
                });
            });
        });

        function viewFullImage(imageSrc) {
            $('#full_img').attr({ src: imageSrc });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="ReturnTo" runat="server" Visible="false">
    <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
    <br /><br />
    </asp:Panel>
    <asp:Repeater ID="ProfileDataList" runat="server" DataSourceID="ProfileDataSource" OnItemDataBound="ProfileDataList_ItemDataBound">
        <ItemTemplate>
            <span class="display-title">Get to know <%# Eval("UserName") %>! A <%# UserProfile.GetAge(Eval("BirthDate").ToString()) %> year old <%# UserProfile.GetEthnicity((string) Eval("Ethnicity")) %> <%# UserProfile.GetGender((string) Eval("Gender")) %> in <%# Eval("City") %>, <%# UserProfile.GetState((string) Eval("State")) %> seeking a <%# UserProfile.GetSeeking((string)Eval("Seeking"))%> for <%# UserProfile.GetLookingFor((string) Eval("LookingFor")) %></span>
            <br />
            <span class="display-headline">&ldquo;<%# Eval("Headline") %>&rdquo;</span>
            <br /><br />
            <div class="clearer"></div>
            <div class="left">
                <asp:Repeater ID="ProfileImage" runat="server" DataSourceID="ProfileImageSource">
                    <ItemTemplate>
                        <img id="full_img" src="<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + ".jpg" %>" alt="<%# DataBinder.Eval(Container.DataItem, "Caption") %>" width="300" height="250" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="left" style="width: 320px; height: 250px; overflow: hidden">
                <asp:Panel ID="ProfileImageSelector" runat="server" Visible="false">
                <div class="prev">prev</div>
                <div class="profileImages">
                    <ul>
                        <asp:Repeater ID="ProfileImages" runat="server" DataSourceID="ProfileImagesSource">
                            <ItemTemplate>
                                <li><img class="thumbnailImage" src="<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + "_thumb.jpg" %>" alt="<%# DataBinder.Eval(Container.DataItem, "Caption") %>" width="120" height="100" onclick="javascript:viewFullImage('<%# "uploads/" +  DataBinder.Eval(Container.DataItem, "BaseFileName") + ".jpg" %>');" /></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="next">next</div>
                </asp:Panel>
            </div>
            <div class="left"><img src="images/300x250.gif" /></div>
            <div class="clearer"></div>
            <a href="pmSend.aspx?SendTo=<%# Eval("UserName") %>">Send <%# Eval("UserName") %> a message!</a>
            <br /><br />
            <fieldset class="left fieldSetContentWidth">
                <legend>All About <%# Eval("UserName") %></legend>
                <div class="fieldSetContent fieldSetDisplayWidth">
                    <span class="display-label">A little about myself:</span>
                    <%# Eval("Description") %>
                    <br />
                    <span class="display-label">My interests include:</span>
                    <%# Eval("Interests") %>
                    <br />
                    <span class="display-label">My deal breakers include:</span>
                    <%# Eval("DealBreakers") %>
                    <br />
                    <span class="display-label">I am seeking a:</span>
                    <%# UserProfile.GetSeeking((string) Eval("Seeking")) %>
                    <br />
                    <span class="display-label">I am looking for:</span>
                    <%# UserProfile.GetLookingFor((string) Eval("LookingFor")) %>
                    <br />
                    <span class="display-label">What's your marital status?</span>
                    <%# UserProfile.GetMaritalStatus((string) Eval("MaritalStatus")) %>
                    <br />
                    <span class="display-label">Do you have children?</span>
                    <%# UserProfile.GetHaveChildren((string) Eval("HaveChildren")) %>
                    <br />
                    <span class="display-label">Do you want children?</span>
                    <%# UserProfile.GetWantChildren((string) Eval("WantChildren")) %>
                    <br />
                    <span class="display-label">Do you practice a religion?</span>
                    <%# UserProfile.GetReligion((string) Eval("Religion")) %>
                    <br />
                    <span class="display-label">What do you do professionally?</span>
                    <%# Eval("Profession") %>
                    <br />
                    <span class="display-label">Do you smoke?</span>
                    <%# UserProfile.GetSmoking((string) Eval("Smoking")) %>
                    <br />
                    <span class="display-label">Do you drink alcohol?</span>
                    <%# UserProfile.GetAlcohol((string) Eval("Alcohol")) %>
                    <br />
                    <span class="display-label">Do you use drugs recreationally?</span>
                    <%# UserProfile.GetRecreationalDrugs((string) Eval("RecreationalDrugs")) %>
                </div>
            </fieldset>
            <fieldset class="left fieldSetLocationWidth">
                <legend>Looks & Location</legend>
                <div class="fieldSetContent">
                    <span class="display-label">Gender:</span>
                    <%# UserProfile.GetGender((string) Eval("Gender")) %>
                    <br />
                    <span class="display-label">Age:</span>
                    <%# UserProfile.GetAge(Eval("BirthDate").ToString()) %>
                    <br />
                    <span class="display-label">Ethnicity:</span>
                    <%# UserProfile.GetEthnicity((string) Eval("Ethnicity")) %>
                    <br />
                    <span class="display-label">Height:</span>
                    <%# UserProfile.GetHeight((string) Eval("Height")) %>
                    <br />
                    <span class="display-label">Body Type:</span>
                    <%# UserProfile.GetBodyType((string) Eval("BodyType")) %>
                    <br />
                    <span class="display-label">Hair Color:</span>
                    <%# UserProfile.GetHairColor((string) Eval("HairColor")) %>
                    <br />
                    <span class="display-label">Eye Color:</span>
                    <%# UserProfile.GetEyeColor((string) Eval("EyeColor")) %>
                    <br />
                    <span class="display-label">City, State:</span>
                    <%# Eval("City") %>, 
                    <%# UserProfile.GetState((string) Eval("State")) %>
                </div>
            </fieldset>
        </ItemTemplate>
    </asp:Repeater>
    <div class="clearer"></div>
    <asp:SqlDataSource ID="ProfileDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MainDatabase %>" 
        
        SelectCommand="SELECT [UserName], [Headline], [Gender], [BirthDate], [Ethnicity], [Height], [MaritalStatus], [Zip], [State], [City], [Country], [BodyType], [HairColor], [EyeColor], [Seeking], [LookingFor], [HaveChildren], [Interests], [Religion], [RecreationalDrugs], [Smoking], [Alcohol], [WantChildren], [Description], [DealBreakers], [Profession], [CreatedDate] FROM [UserProfileTable] WHERE ([UserName] = @UserName)">
        <SelectParameters>
            <asp:QueryStringParameter Name="UserName" QueryStringField="user" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ProfileImageSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MainDatabase %>"             
        
        SelectCommand="SELECT [BaseFileName], [Caption] FROM [UserProfileImages] WHERE (([IsMain] = @IsMain) AND ([UserName] = @UserName))">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="IsMain" Type="Byte" />
            <asp:QueryStringParameter Name="UserName" QueryStringField="user" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ProfileImagesSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MainDatabase %>"             
        
        SelectCommand="SELECT [BaseFileName], [Caption] FROM [UserProfileImages] WHERE ([UserName] = @UserName) ORDER BY [IsMain] ASC">
        <SelectParameters>
            <asp:QueryStringParameter Name="UserName" QueryStringField="user" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>