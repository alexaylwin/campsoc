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
    <div class="tourparagraph" id="tour_pagetext">
        <p>Welcome to the Campus Social dashboard! From here, you can see and manage all aspects of your app. 
        You can see your recent events, messages and feedback from users. You can also take shortcuts to common
        tasks with the 'quick tasks' buttons. The tabs along the top of the page take you to different sections of the site.
        </p>
    </div>

<script type="text/javascript">
    var tourpage = <%=ViewData["tourpage"] %>
    $(document).ready(function () {
        if(tourpage == 1)
        {
            $('#tour_pagetext').dialog();
        }
    });
</script>

</asp:Content>
