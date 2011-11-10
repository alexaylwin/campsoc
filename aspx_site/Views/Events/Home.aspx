<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Events</h2>
    <p><%: Html.ActionLink("Create New Event", "Create", "Events")%></p>
    <div id="manageevents">
        <p><b>Manage Existing Events</b></p>
        Below is a list of all your current events.
            <ul id="eventlist">
            <%foreach (var e in ViewData.Model)
              { %>           
            <li class="eventlistitem">
            <a href="Details?id=<%= e.EventID%>">
            Event Id: <%= e.EventID%> <br />
            Event Name: <%= e.EventName %> <br />
            Event Description: <%= e.EventDesc %> <br />
            </a>
            </li>
            
            <%} %>
        </ul>
    </div>

</asp:Content>
