<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tour
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tour</h2>
    <p>
        To try out Campus Social, please proceed to the login page using the following credentials: <br />
        Username: demouser <br />
        Password: campusdemo <br />
        <a href="../Account/LogOn">Login Here!</a>

    </p>

        <div id="buttons">
        <div id="text"><strong> Learn more:</strong></div>
        <%: Html.ActionLink("home", "Index", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("about", "About", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("contact", "Contact", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
    </div>

</asp:Content>
