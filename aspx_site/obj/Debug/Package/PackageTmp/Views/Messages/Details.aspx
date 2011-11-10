<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Message Details</h2>

    <b>Message ID:</b> <%= ViewData.Model.MessageID %> <br />
    <b>Message Title:</b> <%= ViewData.Model.MessageTitle%> <br />
    <b>Sent On:</b> <%= ViewData.Model.MessageDate%> <br />
    <b>Contents:</b> <br /> <%= ViewData.Model.MessageContents%> <br />
</asp:Content>
