<%@ Page Title="" Language="C#" MasterPageFile="~/Content/mobilesites/masterpages/macengsociety.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Message
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3 id="messagetitle"><%=ViewData["MessageTitle"] %></h3>
    <span id="messagedate"><%=ViewData["MessageDate"] %></span> <br /> <br />
    <span id="messagecontents"><%=ViewData["MessageContents"] %></span> <br />

</asp:Content>
