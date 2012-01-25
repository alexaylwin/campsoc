<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Contact
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Contact</h2>

         <div id="buttons">
        <div id="text"><strong> Learn more:</strong></div>
        <%: Html.ActionLink("home", "Index", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("about", "About", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("tour", "Tour", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        </div>
        <br />

</asp:Content>
