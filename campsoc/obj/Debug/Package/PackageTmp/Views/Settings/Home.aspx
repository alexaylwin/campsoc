<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Settings</h2>

    <%: Html.ActionLink("Manage your third-party account settings", "Accounts", "Settings")%> <br />
    <%: Html.ActionLink("Change home screen text", "HomescreenText", "Settings")%>

</asp:Content>
