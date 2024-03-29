﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Events
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

    <h2>Events</h2>

    <table id="dashboard">
    <tr>
    <td>
    <h3> New Event </h3>
    <%= ViewData["ReturnMessage"] %>
    <%: Html.ActionLink("Create New Event", "Create", "Events", new { @class="createeventlink" })%>
    </td>
    <td>
    <h3> Calendar </h3>
    <div id="calendar"></div> 
    </td>
    </tr>
    <tr>
    <td>
    <div id="manageevents">
        <h3> Existing Events </h3>
        Below is a list of all your current events.
            <ul id="eventlist">
            <%if (ViewData.Model != null)
              {
                  foreach (var e in ViewData.Model)
                  { %>           
            <li class="eventlistitem">
            <a href="Details?id=<%= e.EventID%>">
            Event Id: <%= e.EventID%> <br />
            Event Name: <%= e.EventName%> <br />
            Event Description: <%= e.EventDesc%> <br />
            </a>
            </li>
            
            <%}
              }%>
        </ul>
    </div>
    </td>
    </tr>

    </table>

        <div class="tourparagraph" id="tour_pagetext" title="Manage Events">
        <p>
        This page lets you manage all your existing events and create new events. Click 'Create New Event' to make a new event. Use the calendar
        or list of events to browse and view event details.
        </p>
    </div>
<script type="text/javascript">
    var ontour = readCookie('ontour');
    $(document).ready(function () {
        if (ontour == 1) {
            $('#tour_pagetext').dialog();
        }
    });
</script>
</asp:Content>
