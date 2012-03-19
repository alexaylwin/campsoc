<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Calendar scripts -->
     <script src="../../Content/fullcalendar/fullcalendar.js" type="text/javascript"></script>
    <script src="../../Content/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#calendar').fullCalendar({
                editable: true,
                aspectRatio: 1.65,
                theme: true,
                events:  <%= ViewData["eventlistjson"]%>
            });

        });
    </script>
    <!-- end calendar script -->

    <form id="form1" runat="server">

    <h2>Dashboard</h2>

    <p>
    Welcome back to Campus Social <%= Page.User.Identity.Name %>!<!-- <%= ViewData["appid"] %>, <%= ViewData["controlleruserid"] %>!-->
    </p>
    <div id="dashboardbuttons"><span>Quick Tasks:</span> <a href="../Events/Create" class="cssbutton_small">Create Event</a><a href="../Messages/Create" class="cssbutton_small">Send Message</a><a href="../Settings/Accounts" class="cssbutton_small">Link Social Accounts</a><a href="../mobile/home?appid=<%=ViewData["hashedappid"] %>" class="cssbutton_small">View Mobile Site</a></div>

    
    <table id="dashboard">
        <tr>
            <td>
            <div id="events">
                <h3>Recent Events</h3>
                <ul id="eventlist">
                <%foreach (var e in (List<aspx_site.Models.novaevent>)ViewData["eventlist"])
                  { %>
                <li class="eventlistitem">
                <a href="../Events/Details?id=<%= e.EventID%>">
                <div class="header"><%= e.EventName%></div>
                <%= e.EventStart%> <br />
                </a>
                </li>
                <%} %>
                </ul>
            </div>
            </td>
            <td>
            <h3>Calendar of Events</h3>
		    <div id="calendar"></div>         
            </td>
            <!--<td>
            <div id="users">
                <h3>Users</h3>
                 <ul id="userlist">
                <%foreach (var u in (List<aspx_site.Models.appuser>)ViewData["userlist"])
                  { %>
                <li class="userlistitem">
                User ID: <%= u.UserID%> <br />
                User Handset Model: <%= u.HandsetModel%> <br />
                </li>
                <%} %>
                </ul>
                <br />
            </div>
            </td>-->

        </tr>
        <tr>
            <td>
            <div id="messages">
                <h3>Recent Messages</h3>
                <ul id="messagelist">
                <%foreach (var m in (List<aspx_site.Models.message>)ViewData["messagelist"])
                  { %>
                <li class="messagelistitem">
                <a href="../Messages/Details?id=<%= m.MessageID%>">
                <div class="header"><%= m.MessageTitle%></div>
                Sent on: <%= m.MessageDate%> <br />
                </li>
                </a>
                <%} %>
                </ul>
            </div>
            </td>
            <td>
            <div id="feedback">
               <h3>Recent Feedback</h3>
                <ul id="feedbacklist">
                <%if (ViewData["feedbackcount"] != null)
                  {
                      for (int i = 0; i < (int)ViewData["feedbackcount"]; i++)
                      { %>
                <li class="feedbacklistitem">
                <a href="../Events/Feedback?id=<%= ((string [])ViewData["feedbackeventids"])[i] %>">
                <div class="header"><%= ((DateTime[])ViewData["feedbacksubmittimes"])[i]%></div>
                Survey for <%= ((string[])ViewData["feedbackeventnames"])[i]%> submitted <br />
                </li>
                </a>
                <%}
                  } %>
                </ul>


               <br />
            </div>
            </td>
        </tr>
    </table>

    </form>
    <div class="tourparagraph" id="tour_pagetext" title="Welcome to Campus Social">
        <p>Welcome to the Campus Social dashboard! From here, you can see and manage all aspects of your app. 
        You can see your recent events, messages and feedback from users. The tabs along the top of the page take you to different sections of the site.
        </p>
        <p>
        <b>Click on any element on the page outlined in red to find out more about it!</b>
        </p>
    </div>

    <div class="tourparagraph" id="tour_quickbartext" title="Quick Tasks">
        <p>
        These buttons let you take shortcuts to the most common Campus Social tasks. Click one to navigate to that section of the site.
        </p>
    </div>

    <div class="tourparagraph" id="tour_recenteventstext" title="Recent Events">
        <p>
        This panel shows the most recent events you've created. You can click on one to view the details of that event, or to edit it.
        </p>
    </div>

    <div class="tourparagraph" id="tour_recentmessagestext" title="Recent Messages">
        <p>
        This panel shows the most recent messages you've created. You can click on one to view the details of that message.
        </p>
    </div>

     <div class="tourparagraph" id="tour_recentfeedbacktext" title="Recent Feedback">
        <p>
        This panel shows the most recent feedback you've recieved from users. This would include RSVPs, comments, and survey results. Click on an element
        to be taken to the feedback page for that event.
        </p>
    </div>

<script type="text/javascript">
    var ontour = readCookie('ontour');
    $(document).ready(function () {
        if (ontour == 1) {

            $('#dashboardbuttons').addClass("tourelement");
            $('#events:first-child').addClass("tourelement");
            $('#messages:first-child').addClass("tourelement");
            $('#feedback:first-child').addClass("tourelement");

            $('#tour_pagetext').dialog();

            $('#dashboardbuttons').click(function (e) {
                if ($('#dashboardbuttons').hasClass("tourelement")) {
                    $('#tour_quickbartext').dialog();
                    $('#dashboardbuttons').removeClass("tourelement");
                }
            });

            $('#events:first-child').click(function (e) {
                if ($('#events:first-child').hasClass("tourelement")) {
                    $('#tour_recenteventstext').dialog();
                    $('#events:first-child').removeClass("tourelement");
                }
            });

            $('#messages:first-child').click(function (e) {
                if ($('#messages:first-child').hasClass("tourelement")) {
                    $('#tour_recentmessagestext').dialog();
                    $('#messages:first-child').removeClass("tourelement");
                }
            });

            $('#feedback:first-child').click(function (e) {
                if ($('#feedback:first-child').hasClass("tourelement")) {
                    $('#tour_recentfeedbacktext').dialog();
                    $('#feedback:first-child').removeClass("tourelement");
                }
            });
        }
    });
</script>

</asp:Content>
