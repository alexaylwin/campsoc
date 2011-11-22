<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Event Details</h2>
    <b>Event ID:</b> <%= ViewData.Model.EventID %> <br />
    <b>Event SyncID:</b> <%= ViewData.Model.SyncID%> <br />
    <b>Event Name:</b> <%= ViewData.Model.EventName%> <br />
    <b>Event Location:</b> <%= ViewData.Model.Location%> <br />
    <b>Event Start:</b> <%= ViewData.Model.EventStart%> <br />
    <b>Event End:</b> <%= ViewData.Model.EventEnd%> <br /> <br />
    <b>Event Description:</b> <br /><%= ViewData.Model.EventDesc%><br /><br />
    <b>Attending:</b> <%= ViewData.Model.Attending%> <b>Not Attending</b>: <%= ViewData.Model.NotAttending%>
    <br /><br />
    <span style="font-size:1.1em;font-weight:bold;background-color: #E0E0E0;padding: 5px;"><a href="Edit?id=<%= ViewData.Model.EventID%>"> Edit this event </a></span>
</asp:Content>
