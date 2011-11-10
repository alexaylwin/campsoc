<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat=server>
 <% using (Html.BeginForm()) { %>
    Message ID: <%: Html.TextBox("MessageID") %> <br />
    Message Name: <%: Html.TextBox("MessageTitle") %> <br />
    Message Description: <%: Html.TextBox("MessageContents") %> </br >
    <input type="submit" name="create" value="Create Message" />
 <%} %>
 </asp:Content>