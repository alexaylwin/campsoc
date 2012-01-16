<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
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
    Welcome back to Campus Social, <%: Page.User.Identity.Name %>!
    </p>
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
               <br />
            </div>
            </td>
        </tr>
    </table>

    </form>
</asp:Content>
