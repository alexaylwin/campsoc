<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Event
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
     
     List<SelectListItem> strtimelist = new List<SelectListItem>();
     List<SelectListItem> endtimelist = new List<SelectListItem>();
     DateTime dt = new DateTime(1, 1, 1, 00, 0, 0);
     for (int i = 0; i < 24; i++)
     {
         strtimelist.Add(new SelectListItem { Text = dt.ToString("h:mm"), Value = dt.ToString("h:mm") });
         endtimelist.Add(new SelectListItem { Text = dt.ToString("h:mm"), Value = dt.ToString("h:mm") });
         string st = Convert.ToString(ViewData["EventStartTime"]);
         string en = Convert.ToString(ViewData["EventEndTime"]);
         if (strtimelist[i].Value == st)
         {
             strtimelist[i].Selected = true;
         }
         if (endtimelist[i].Value == en)
         {
             endtimelist[i].Selected = true;
         }
         dt = dt.AddMinutes(30);
     };

     List<SelectListItem> strampm = new List<SelectListItem>();
     List<SelectListItem> endampm = new List<SelectListItem>();
     strampm.Add(new SelectListItem { Text = "AM", Value = "AM" });
     strampm.Add(new SelectListItem { Text = "PM", Value = "PM" });

     if (Convert.ToString(ViewData["EventStartAMPM"]) == "AM")
     {
         strampm[0].Selected = true;
     }
     else
     {
         strampm[1].Selected = true;
     }
     
     endampm.Add(new SelectListItem { Text = "AM", Value = "AM" });
     endampm.Add(new SelectListItem { Text = "PM", Value = "PM" });
     if (Convert.ToString(ViewData["EventEndAMPM"]) == "AM")
     {
         endampm[0].Selected = true;
     }
     else
     {
         endampm[1].Selected = true;
     }

     bool disabled = false;
     if ((int)ViewData["EventDisabled"] == 1)
     {
         disabled = true;
     }

     bool noend = false;
     if ((int)ViewData["NoEndDate"] == 1)
     {
         noend = true;
     }
 %>
 <div class="createform">
 <% using (Html.BeginForm()) { %>
 <table class="itemtable">
    <tr><td class="header">Event ID</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput"})%></td></tr>
    <tr><td class="header">Event Name</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventName", ViewData["EventName"], new { @class = "textinput" })%></td></tr>
    <tr><td class="header">Event Location</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventLocation", ViewData["EventLocation"], new {@class="textinput"}) %></td></tr>
    <tr><td class="header">Event Description</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("EventDescription", new { @class = "longtext textinput" })%></td></tr>
    <tr><td class="header">Runs from</td></tr> <tr><td><%: Html.TextBox("EventStartDate", ViewData["EventStartDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventStartTime", strtimelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventStartTimeAMPM", strampm, new { @class = "timeinput ampm" })%> to <%: Html.TextBox("EventEndDate", ViewData["EventEndDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventEndTime", endtimelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventEndTimeAMPM", endampm, new { @class = "timeinput ampm" })%></td></tr>
    <tr><td>&nbsp;&nbsp;&nbsp;No end date:<%: Html.CheckBox("NoEndDate",noend, new { @class="checkboxinput"}) %></td></tr>
    <tr><td class="header">Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("Attending", ViewData["Attending"], new { @class = "numberinput" })%></td></tr>
    <tr><td class="header">Not Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("NotAttending", ViewData["NotAttending"], new { @class = "numberinput" })%></td></tr>
    <tr><td>Do not publish:<%: Html.CheckBox("Disabled",disabled, new { @class="checkboxinput"}) %></td></tr>
 </table>
 </div>
 <br />
    <input type="submit" name="create" value="Submit Changes" />
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