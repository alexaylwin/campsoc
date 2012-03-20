<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Contact
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Contact</h2>
            I'm Alex, and I'm a fifth year undergradate at McMaster University, studying
        Software Engineering and minoring in Philosophy. You can get in touch with me a whole bunch of ways. <br /> <br />
        Twitter: <a href="twitter.com/alexaylwin">@alexaylwin</a> <br />
        Email: AlexAylwin@gmail.com<br />
        LinkedIn: <a href="http://linkedin.com/in/alexaylwin">http://linkedin.com/in/alexaylwin</a><br />
        Web: <a href="http://alexaylwin.campsoc.com">http://alexaylwin.campsoc.com</a>

         <div id="buttons">
        <div id="text"><strong> Learn more:</strong></div>
        <%: Html.ActionLink("home", "Index", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("about", "About", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("tour", "Tour", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        </div>
        <br />

</asp:Content>
