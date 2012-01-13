<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Accounts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Accounts</h2>

    <h3> Twitter </h3>
    <% using (Html.BeginForm("TwitterAccount","SettingsController", FormMethod.Post))
       { %>
       <% if (!(bool)ViewData["TwitterRegistered"])
          { %>
       <p>Adding your club's twitter account allows Campus Social to post event links to your Twitter account as soon as they appear on the app.</p>
       <input type="submit" name="submit" value="Add Twitter Account" />
       <%}
          else
          {%>

          <p> Twitter account linked! </p>

          <input type="submit" name="submit" value="Unlink Twitter Account" />

       <%} %>
    <%} %>

    <h3> Facebook </h3>
    <% using (Html.BeginForm("FacebookAccount", "SettingsController", FormMethod.Post))
       { %>
       <p>Adding your club's twitter account allows Campus Social to post event links to your Twitter account as soon as they appear on the app.</p>
       <input type="submit" name="submit" value="Add Twitter Account" />
    <%} %>
    
</asp:Content>
