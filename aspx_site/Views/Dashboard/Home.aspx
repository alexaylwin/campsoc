<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                <b><%= e.EventName%> </b><br />
                <%= e.EventStart%> <br />
                </a>
                </li>
                <%} %>
                </ul>
            </div>
            </td>

            <td>
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
            </td>
        </tr>
        <tr>
            <td>
            <div id="messages">
                <h3>Recent Messages</h3>
                <ul id="messagelist">
                <%foreach (var m in (List<aspx_site.Models.message>)ViewData["messagelist"])
                  { %>
                <a href="../Messages/Details?id=<%= m.MessageID%>">
                <li class="messagelistitem">
                <b><%= m.MessageTitle%></b> <br />
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
</asp:Content>
