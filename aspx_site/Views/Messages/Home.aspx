<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Messages
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages</h2>

    <table id="dashboard">
    <tr>
    <td>
    <h3>New Message</h3>
    <%: Html.ActionLink("Create New Message", "Create", "Messages", new { @class="createmessagelink" })%></p>
    </td>
    <td>
    <div id="managemessages">
     <h3>Sent Messages</h3>
         <ul id="messagelist">
         <%foreach (var e in ViewData.Model)
           { %>           
         <li class="messagelistitem">
         <a href="Details?id=<%= e.MessageID%>">
         <%= e.MessageTitle %> <br />
         <%= e.MessageContents %> <br />
         (Sent: <%= e.MessageDate %>) <br />
         </a>
         </li>
         <%} %>
        </ul>
    </div>
    </td>
    </tr>
    </table>

        <div class="tourparagraph" id="tour_pagetext" title="Manage Messages">
        <p>
        This page lets you manage all your existing messages and create new messages. Click 'Create New Message' to send a new message.
        </p>
    </div>
<script type="text/javascript">
    var ontour = readCookie('ontour');
    $(document).ready(function () {
        if (ontour == 1) {
            $('#tour_pagetext').dialog();
        }
    });
</script>
</asp:Content>
