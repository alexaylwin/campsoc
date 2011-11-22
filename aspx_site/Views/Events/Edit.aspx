<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Event</h2>
 <%
     string desctext;
     if (ViewData["EventDescription"] == null)
     {
         desctext = " ";
     }
     else
     {
         desctext = ViewData["EventDescription"].ToString();
     }
 %>
 <% using (Html.BeginForm()) { %>
 <table id="createform">
    <tr><td>Event ID:</td> <td><%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput"})%></td></tr>
    <tr><td>Event Name:</td> <td><%: Html.TextBox("EventName", ViewData["EventName"], new {@class="textinput"}) %></td></tr>
    <tr><td>Event Location:</td> <td><%: Html.TextBox("EventLocation", ViewData["Location"], new {@class="textinput"}) %></td></tr>
    <tr><td>Event Description:</td> <td><%: Html.TextArea("EventDescription",desctext, new {@class="longtext textinput"})%></td></tr>
    <tr><td>Event Start:</td> <td><%: Html.TextBox("EventStart", ViewData["EventStart"], new { @class = "dateinput" })%></td></tr>
    <tr><td>Event End:</td> <td><%: Html.TextBox("EventEnd", ViewData["EventEnd"], new { @class = "dateinput" })%></td></tr>
    <tr><td>Attending:</td> <td><%: Html.TextBox("Attending", ViewData["Attending"], new { @class = "textinput" })%></td></tr>
    <tr><td>Not Attending:</td> <td><%: Html.TextBox("NotAttending", ViewData["NotAttending"], new { @class = "textinput" })%></td></tr>
 </table>
 <br />
    <input type="submit" name="create" value="Submit Changes" />
    <script type="text/javascript">
        $(function () {
            $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
            $('.dateinput').change(function () {
                $(this).val($(this).val() + ' 12:00:00 AM');
            });
        });
    </script>
 <%} %>
</asp:Content>