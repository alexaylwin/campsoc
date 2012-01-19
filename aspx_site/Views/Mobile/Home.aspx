<%@ Page Title="" Language="C#" MasterPageFile="~/Content/mobilesites/masterpages/macengsociety.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
   <%= ViewData["HomeText"] %>
    
    </p>

</asp:Content>
