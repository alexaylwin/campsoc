<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Homescreen Text
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Homescreen Text</h2>

    <% using (Html.BeginForm())
       { %>
       <div class="createform" id="hometext">
       <table class="itemtable">
       <tr><td class="itemheader">New home screen text:</td></tr><tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("Hometext", (string)ViewData["currhometext"], new { @class = "longtext textinput" })%></td></tr>
       </table>
        <div id="submitdiv">
        <button type="submit">
        <a href="#" id="submit_link" class="cssbutton_med">Set Text</a>
        </button>
        </div>
        </div>
    <%} %>
</asp:Content>
