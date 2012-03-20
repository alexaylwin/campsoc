<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Accounts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Accounts</h2>

    <h3> Twitter </h3>
    <p><%= ViewData["ReturnMessageTwitter"]%></p>
    <% using (Html.BeginForm("TwitterAccount","Settings", FormMethod.Post))
       { %>
       <% if (!(bool)ViewData["TwitterRegistered"])
          { %>
       <p>Adding your club's twitter account allows Campus Social to post event links to your Twitter account as soon as they appear on the app.</p>
       <input type="submit" name="submit" value="Link Twitter Account" />
       <%}
          else
          {%>

          <p> Twitter account linked! </p>

          <input type="submit" name="submit" value="Unlink Twitter Account" />

       <%} %>
    <%} %>

    <h3> Facebook </h3>
    <p><%= ViewData["ReturnMessageFacebook"]%></p>
    <% using (Html.BeginForm("FacebookAccount", "Settings", FormMethod.Post))
       { %>
       <% if (!(bool)ViewData["FacebookRegistered"])
          { %>
       <p>Adding your club's Facebook account allows Campus Social to post events to your Facebook account or Page as soon as they appear on the app.</p>
       <input type="submit" name="submit" value="Link Facebook Account" />
       <%}
          else
          {%>
          <p> Facebook account linked as <a href="http://www.facebook.com/profile.php?id=<%= ViewData["FacebookUserId"] %>"><%= ViewData["FacebookUserName"] %> </a><br />
           <%if (ViewData["FacebookLinkedPageName"] != null)
             { %>
             Currently, you have linked the <a href="http://www.facebook.com/profile.php?id=<%= ViewData["FacebookLinkedPageId"] %>"><%= ViewData["FacebookLinkedPageName"]%> page </a> to your CampSoc.com account.
           <%} %>
           To post events and messages as a different Facebook page, please select a page below:
          <ul id="facebookaccountlist">
          <%foreach (string accountName in (string[])ViewData["FacebookPageNames"])
            { %>
          <li><%: Html.ActionLink(accountName, "FacebookAccountPage", new { pageName = accountName })  %></li>
          <%} %>
          </ul>
          </p>

          <input type="submit" name="submit" value="Unlink Facebook Account" />
          <%} %>
    <%} %>


        <div class="tourparagraph" id="tour_pagetext" title="Manage Accounts">
        <p>
        From here, you can link a Twitter and Facebook account to your Campus Social account. This will let you post events to Facebook and Twitter
        from Campus Social.
        </p>
        <p>
        Linking an account is easy. For Twitter, just click the 'Link Account' button. You'll be redirected to Twitter where you can sign in to the right account
        and give permission for Campus Social to make posts.
        </p>
        <p>
        With Facebook, you start by linking any personal Facebook account that can manage your club's Facebook page. After that, you'll be brought back here, and you'll
        see a list of pages that the linked account can manage. You can click on any of these pages, and the events will be posted as that page. Please note that Campus Social cannot
        access an individuals Facebook after their account has been linked, only the pages they manage.
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


