<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create new event
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat=server>
<h2>Create New Event</h2>
 <% using (Html.BeginForm()) { %>
 <table id="createform">
    <tr><td>Event ID:</td> <td><%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput"})%></td></tr>
    <tr><td>Event Name:</td> <td><%: Html.TextBox("EventName", "", new {@class="textinput"}) %></td></tr>
    <tr><td>Event Location:</td> <td><%: Html.TextBox("EventLocation", "", new {@class="textinput"}) %></td></tr>
    <tr><td>Event Description:</td> <td><%: Html.TextArea("EventDescription", new {@class="longtext textinput"})%></td></tr>
    <tr><td>Event Start:</td> <td><%: Html.TextBox("EventStart", ViewData["EventStart"], new { @class = "dateinput" })%></td></tr>
    <tr><td>Event End:</td> <td><%: Html.TextBox("EventEnd", ViewData["EventEnd"], new { @class = "dateinput" })%></td></tr>
    <tr><td>Attending:</td> <td><%: Html.TextBox("Attending", "", new { @class = "textinput" })%></td></tr>
    <tr><td>Not Attending:</td> <td><%: Html.TextBox("NotAttending", "", new { @class = "textinput" })%></td></tr>
 </table>
 <br />
    <input type="submit" name="create" value="Create Event" />
    <script type="text/javascript">
        $(function () {
            //$("#EventStart").datepicker();
            //$("#EventEnd").datepicker();
            //$(".dateinput").datepicker();
            $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
        });

    </script>
 <%} %>
 </asp:Content>