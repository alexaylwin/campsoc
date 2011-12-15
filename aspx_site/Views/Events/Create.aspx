<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create new event
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat=server>
<h2>Create New Event</h2>
<%List<SelectListItem> timelist = new List<SelectListItem>();
  DateTime dt = new DateTime(1, 1, 1, 00, 0, 0);
  for (int i = 0; i < 24; i++)
  {
      timelist.Add(new SelectListItem { Text = dt.ToString("h:mm"), Value = dt.ToString("h:mm") });
      dt = dt.AddMinutes(30);
  };
  timelist[0].Selected = true;
  
  List<SelectListItem> ampm = new List<SelectListItem>();
  ampm.Add(new SelectListItem { Text = "AM", Value = "AM", Selected = true });
  ampm.Add(new SelectListItem { Text = "PM", Value = "PM" });
 %>
 <% using (Html.BeginForm()) { %>
 <table class="createform">
    <tr><td class="header">Event ID</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput"})%></td></tr>
    <tr><td class="header">Event Name</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventName", "", new { @class = "textinput" })%></td></tr>
    <tr><td class="header">Event Location</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventLocation", "", new {@class="textinput"}) %></td></tr>
    <tr><td class="header">Event Description</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("EventDescription", new { @class = "longtext textinput" })%></td></tr>
    <tr><td class="header">Runs from</td></tr> <tr><td><%: Html.TextBox("EventStartDate", ViewData["EventStartDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventStartTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventStartTimeAMPM", ampm, new { @class = "timeinput ampm" })%> to <%: Html.TextBox("EventEndDate", ViewData["EventEndDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventEndTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventEndTimeAMPM", ampm, new { @class = "timeinput ampm" })%></td></tr>
    <tr><td class="header">Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("Attending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td class="header">Not Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("NotAttending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td>Do not publish:<%: Html.CheckBox("Disabled",false, new { @class="checkboxinput"}) %></tr>
 </table>
 <br />
    <input type="submit" name="create" value="Create Event" />
    <script type="text/javascript">
        $(function () {
            $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
            $('.dateinput').change(function () {
                $(this).val($(this).val());
            });
        });
    </script>
 <%} %>
 </asp:Content>