<%@ Page Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome to Campus Social!</h2>
    <p>
        Campus Social is a mobile application project designed to help student organizations take 
        advantage of social networks. It provides a unique way to connect with a greater 
        number of students on campus, and increase interest and participation in student 
        run events and clubs. If you have an account with Campus Social, you can
         <%: Html.ActionLink("log in here.", "LogOn", "Account")%>
        If you&#39;d like more information about the service, you can email Alex at 
        Alex@campsoc.com</p>
</asp:Content>
