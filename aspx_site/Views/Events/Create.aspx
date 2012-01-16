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
 <div class="createform" id="events">
 <% using (Html.BeginForm()) { %>
 <div class="leftcol" id="eventinfo">
 <div class="formheader">1. Event Information</div>
 <table class="itemtable" id="eventinfo">
    <tr style="display:none;"><td class="itemheader">Event ID</td></tr> <tr style="display:none"><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput"})%></td></tr>
    <tr><td class="itemheader">Event Name</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventName", "", new { @class = "textinput" })%></td></tr>
    <tr><td class="itemheader">Event Location</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventLocation", "", new {@class="textinput"}) %></td></tr>
    <tr><td class="itemheader">Event Description</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("EventDescription", new { @class = "longtext textinput" })%></td></tr>
    <tr><td class="itemheader">Runs from</td></tr> <tr><td><%: Html.TextBox("EventStartDate", ViewData["EventStartDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventStartTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventStartTimeAMPM", ampm, new { @class = "timeinput ampm" })%> to <%: Html.TextBox("EventEndDate", ViewData["EventEndDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("EventEndTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("EventEndTimeAMPM", ampm, new { @class = "timeinput ampm" })%></td></tr>
    <tr><td class="itemheader">Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("Attending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td class="itemheader">Not Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("NotAttending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td>Do not publish:<%: Html.CheckBox("Disabled",false, new { @class="checkboxinput"}) %></td></tr>
 </table>
 </div>

 <div class="rightcol" id="linkedaccounts">
 <div class="formheader">2. Linked Accounts</div>
  <div class="subheader collapsible">Twitter</div>
 <table class="itemtable" id="twitter">
    <%if(ViewData["twitterRegistered"] != null && (bool)ViewData["twitterRegistered"] == true){
           %>
    <tr><td>Tweet <span id="tweetchars">(max 140 chars):</span></td></tr>
    <tr><td><%: Html.TextArea("TweetText", new {@class="longtext textinput"})%></td></tr>
    <tr><td>Include link to event (20 characters): <%: Html.CheckBox("TwitterEventLink",false, new { @class="checkboxinput"}) %></td></tr>
    <%} else { %>
    <tr><td>To send tweets along with your event, you can <%: Html.ActionLink("link your Twitter account by clicking here!", "Accounts", "Settings")%></td></tr>
    <%} %>
 </table>

 <div class="subheader collapsible">Facebook</div>
 <table class="itemtable" id="facebook">
     <%if(ViewData["facebookRegistered"] != null && (bool)ViewData["facebookRegistered"] == true){
           %>
    <tr><td>facebook</td></tr>
    <tr><td><%: Html.TextArea("TweetText", new {@class="longtext textinput"})%></td></tr>
    <%} else { %>
    <tr><td>To create a Facebook event at the same time as your event, you can <%: Html.ActionLink("link your Facebook account by clicking here!", "Accounts", "Settings")%></td></tr>
    <%} %>
 
 </table>
</div>
 
 
 <div id="submitdiv">
 <button type="submit" class="submitbutton">
    Create Event<!--<a href="#" id="submit_link" class="button">Create Event</a>-->
</button>
</div>
<!-- <div id="submitdiv"><input type="submit" name="create" id="createbutton" value="Create Event" /></div> -->
    
    <script type="text/javascript">
        //datepicker javascript
        $(function () {
            $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
            $('.dateinput').change(function () {
                $(this).val($(this).val());
            });
        });
    </script>

    <script src="../../Scripts/slideFade.js" type="text/javascript"></script>
    <script type="text/javascript">
        //collapsible javascript
        $(".collapsible").click(function () {
            $(this).next().slideToggle(100);
        });
    </script>
    <script type="text/javascript">
        //twitter character javascript
        $('#TweetText').keyup(function () {
            var charLength = $(this).val().length;
            if ($('#TwitterEventLink').attr('checked')) {
                charLength = charLength + 20;
            }
            if (charLength == 0) {
                $('#tweetchars').html('(max 140 chars):');
            } else if (charLength > 140) {
                $('#tweetchars').html('(<font color="red">' + (140 - charLength) + ' characters left</font>):');
            } else {
                $('#tweetchars').html('(' + (140 - charLength) + ' characters left):');
            }
        });

        $('#TwitterEventLink').mouseup(function () {
            var charLength = $('#TweetText').val().length;
            if (!$(this).attr('checked')) {
                charLength = charLength + 20;
            }
            if (charLength > 140) {
                $('#tweetchars').html('(<font color="red">' + (140 - charLength) + ' characters left</font>):');
            } else {
                $('#tweetchars').html('(' + (140 - charLength) + ' characters left):');
            }
        });
    
    </script>
 <%} %>
 </div>
 </asp:Content>