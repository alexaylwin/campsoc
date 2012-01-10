<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Home</h2>

    <p><%: Html.ActionLink("Create New Message", "Create", "Messages")%></p>
    <div id="manageevents">
     <p><b>Manage Existing Events</b></p>
     Below is a list of all your current events.
         <ul id="messagelist">
         <%foreach (var e in ViewData.Model)
           { %>           
         <li class="messagelistitem">
         <a href="Details?id=<%= e.MessageID%>">
         Event Id: <%= e.MessageID%> <br />
         <%= e.MessageTitle %> <br />
         <%= e.MessageDate %> <br />
         <%= e.MessageContents %> <br />
         </a>
         </li>
         <%} %>
        </ul>
    </div>
</asp:Content>
