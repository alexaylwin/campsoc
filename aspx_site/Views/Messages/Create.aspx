<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat=server>
 <% using (Html.BeginForm()) { %>
    <table id="createform">
        <tr><td>Message ID:</td><td> <%: Html.TextBox("MessageID", ViewData["MessageID"],new { @class = "textinput" }) %></td></tr>
        <tr><td>Message Title: </td><td><%: Html.TextBox("MessageTitle", "" ,new { @class = "textinput" })%></td></tr>
        <tr><td>Message Description:</td><td><%: Html.TextArea("MessageContents","", new { @class = "longtext textinput" })%></td></tr>
   </table> <br />
    <input type="submit" name="create" value="Create Message" />
 <%} %>
 </asp:Content>