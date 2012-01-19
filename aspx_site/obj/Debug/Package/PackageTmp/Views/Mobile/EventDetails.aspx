<%@ Page Title="" Language="C#" MasterPageFile="~/Content/mobilesites/masterpages/macengsociety.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Event
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3 id="eventname"><%=ViewData["EventName"] %></h3>
    <span id="eventdate"><%=ViewData["EventDate"] %></span> <br /> 
    <span id="eventlocation">At <%=ViewData["EventLocation"] %></span> <br /> <br />
    <div id="eventdescription"><%=ViewData["EventDescription"] %></div>

</asp:Content>
