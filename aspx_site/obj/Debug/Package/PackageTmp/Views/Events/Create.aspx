<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create Event
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
 <%=ViewData["ReturnMessage"] %>
 <div class="createform" id="events">
 <% using (Html.BeginForm()) { %>
 <div class="leftcol" id="eventinfo">
 <div class="formheader">1. Event Information</div>
 <table class="itemtable" id="">
    <tr style="display:none;"><td class="itemheader">Event ID</td></tr> <tr style="display:none"><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventID", ViewData["EventID"], new { @class = "textinput eventinfo" })%></td></tr>
    <tr><td class="itemheader">Event Name</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventName", "", new { @class = "textinput copytofacebook" })%></td></tr>
    <tr><td class="itemheader">Event Location</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventLocation", "", new { @class = "textinput copytofacebook" })%></td></tr>
    <tr><td class="itemheader">Event Description</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("EventDescription", new { @class = "longtext textinput copytofacebook" })%></td></tr>
    <tr><td class="itemheader">Runs from</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("EventStartDate", ViewData["EventStartDate"], new { @class = "dateinput copytofacebook" })%> <%: Html.DropDownList("EventStartTime", timelist, new { @class = "timeinput copytofacebook" })%> <%: Html.DropDownList("EventStartTimeAMPM", ampm, new { @class = "timeinput ampm copytofacebook" })%> <span class="enddateinputs">to <%: Html.TextBox("EventEndDate", ViewData["EventEndDate"], new { @class = "dateinput copytofacebook" })%> <%: Html.DropDownList("EventEndTime", timelist, new { @class = "timeinput copytofacebook" })%> <%: Html.DropDownList("EventEndTimeAMPM", ampm, new { @class = "timeinput ampm copytofacebook" })%></span></td></tr>
    <tr><td>&nbsp;&nbsp;&nbsp;No end date:<%: Html.CheckBox("NoEndDate",false, new { @class="checkboxinput"}) %></td></tr>
    <tr><td class="itemheader">Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("Attending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td class="itemheader">Not Attending</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("NotAttending", "", new { @class = "numberinput" })%></td></tr>
    <tr><td>Do not publish:<%: Html.CheckBox("Disabled",false, new { @class="checkboxinput"}) %></td></tr>
 </table>
 </div>

 <div class="rightcol" id="linkedaccounts">
 <div class="formheader">2. Linked Accounts</div>
  <div class="subheader collapsible" id='twittersubheader'>Post to Twitter <%: Html.CheckBox("PostToTwitter",false, new { @class="checkboxinput"}) %></div>
 <table class="itemtable" id="twitter">
    <%if(ViewData["twitterRegistered"] != null && (bool)ViewData["twitterRegistered"] == true){
           %>
    <tr><td class="itemheader">Tweet <span id="tweetchars">(max 140 chars):</span></td></tr>
    <tr><td><%: Html.TextArea("TweetText", new {@class="longtext textinput"})%></td></tr>
    <tr><td>Include link to Facebppl event (20 characters): <%: Html.CheckBox("TwitterEventLink",false, new { @class="checkboxinput"}) %></td></tr>
    <%} else { %>
    <tr><td>To send tweets along with your event, you can <%: Html.ActionLink("link your Twitter account by clicking here!", "Accounts", "Settings")%></td></tr>
    <%} %>
 </table>
 <br />
 <div class="subheader collapsible" id='facebooksubheader'>Create Facebook Event <%: Html.CheckBox("PostToFacebook",false, new { @class="checkboxinput"}) %></div>
 <table class="itemtable" id="facebook">
     <%if(ViewData["facebookRegistered"] != null && (bool)ViewData["facebookRegistered"] == true){
           %>
    <tr><td class="itemheader">Event Name</td></tr><tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("FacebookEventName", "", new { @class = "textinput" })%></td></tr>
    <tr><td class="itemheader">Event Location</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("FacebookEventLocation", "", new { @class = "textinput" })%></td></tr>
    <tr><td class="itemheader">Runs from</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("FacebookEventStartDate", ViewData["EventStartDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("FacebookEventStartTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("FacebookEventStartTimeAMPM", ampm, new { @class = "timeinput ampm" })%> <span class="enddateinputs">to <%: Html.TextBox("FacebookEventEndDate", ViewData["EventEndDate"], new { @class = "dateinput" })%> <%: Html.DropDownList("FacebookEventEndTime", timelist, new { @class = "timeinput" })%> <%: Html.DropDownList("FacebookEventEndTimeAMPM", ampm, new { @class = "timeinput ampm" })%></span></td></tr>
    <tr><td class="itemheader">Event Details</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("FacebookEventDescription", new { @class = "longtext textinput" })%></td></tr>
    <tr><td>Campus Social is linked to <a href="http://www.facebook.com/profile.php?id=<%= ViewData["facebookUserId"] %>"><%= ViewData["facebookUserName"] %>'s</a> Facebook account.
    <%if (ViewData["facebookPageId"] != null)
      {  %>
      <br /> Events will be posted as <a href="http://www.facebook.com/profile.php?id=<%= ViewData["facebookPageId"] %>"><%= ViewData["facebookPageName"] %></a>.
    <%} %>
    </td></tr>
    <%} else { %>
    <tr><td>To create a Facebook event at the same time as your event, you can <%: Html.ActionLink("link your Facebook account by clicking here!", "Accounts", "Settings")%></td></tr>
    <%} %>
 
 </table>
</div>
 
 
 <div id="submitdiv">
 <button type="submit">
    <a href="#" id="submit_link" class="cssbutton_med">Create Event</a>
</button>
</div>
    <script src="../../Scripts/slideFade.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //this collapses the linked accounts by default
            $('.collapsible').next().slideToggle(10);

            //datepicker javascript
            $(function () {
                $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
                $('.dateinput').change(function () {
                    $(this).val($(this).val());
                });
            });

            $('#PostToFacebook').click(function (e) {
                if ($(this).is(':checked') && !($(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                } else if ((!($(this).is(':checked')) && $(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                }
                e.stopImmediatePropagation();
            });

            $('#PostToTwitter').click(function (e) {
                if ($(this).is(':checked') && !($(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                } else if ((!($(this).is(':checked')) && $(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                }
                e.stopImmediatePropagation();
            });

            //collapsible javascript
            $(".collapsible").click(function () {
                $(this).next().slideToggle(10);
            });

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

            //this is to copy the info from event information to the facebook event
            $('#EventName').keyup(function () {
                $('#FacebookEventName').val($(this).val());
            });

            $('#EventLocation').keyup(function () {
                $('#FacebookEventLocation').val($(this).val());
            });

            $('#EventDescription').keyup(function () {
                $('#FacebookEventDescription').val($(this).val());
            });

            $('#EventStartDate').change(function () {
                $('#FacebookEventStartDate').val($(this).val());
            });

            $('#EventStartTime').change(function () {
                $('#FacebookEventStartTime').val($(this).val());
            });

            $('#EventStartTimeAMPM').change(function () {
                $('#FacebookEventStartTimeAMPM').val($(this).val());
            });

            $('#EventEndDate').change(function () {
                $('#FacebookEventEndDate').val($(this).val());
            });

            $('#EventEndTime').change(function () {
                $('#FacebookEventEndTime').val($(this).val());
            });

            $('#EventEndTimeAMPM').change(function () {
                $('#FacebookEventEndTimeAMPM').val($(this).val());
            });

            $('#NoEndDate').change(function () {
                if ($(this).is(':checked')) {
                    $('.enddateinputs').addClass('hide');
                } else {
                    $('.enddateinputs').removeClass('hide');
                }
            });

        });
    </script>
 <%} %>
 </div>
 </asp:Content>