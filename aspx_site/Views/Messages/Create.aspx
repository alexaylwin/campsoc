<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat=server>
	Create Message
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat=server>
 <div class="createform" id="messages">
 <% using (Html.BeginForm()) { %>
 <div class="leftcol" id="messageinfo">
 <div class="formheader">Send New Message</div>
    <table class="itemtable" id="">
        <tr style="display:none;"><td class="itemheader">Message ID:</td><td> <%: Html.TextBox("MessageID", ViewData["MessageID"],new { @class = "textinput" }) %></td></tr>
        <tr><td class="itemheader">Message Title: </td></tr><tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextBox("MessageTitle", "" ,new { @class = "textinput" })%></td></tr>
        <tr><td class="itemheader">Message Description:</td></tr><tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("MessageContents","", new { @class = "longtext textinput" })%></td></tr>
   </table>
   </div>

   <div class="rightcol" id="linkedaccounts">
   <div class="formheader">Linked Accounts</div>
        <div class="subheader collapsible" id='twittersubheader'>Post to Twitter <%: Html.CheckBox("PostToTwitter",false, new { @class="checkboxinput"}) %></div>
        <table class="itemtable" id="twitter">
            <%if(ViewData["twitterRegistered"] != null && (bool)ViewData["twitterRegistered"] == true)
              {
           %>
            <tr><td class="itemheader">Tweet <span id="tweetchars">(max 140 chars):</span></td></tr>
            <tr><td><%: Html.TextArea("TweetText", new {@class="longtext textinput"})%></td></tr>
            <tr><td>Include link to Facebok wall post (20 characters): <%: Html.CheckBox("TwitterEventLink",false, new { @class="checkboxinput"}) %></td></tr>
            <%} else { %>
            <tr><td>To send tweets along with your event, you can <%: Html.ActionLink("link your Twitter account by clicking here!", "Accounts", "Settings")%></td></tr>
            <%} %>
        </table>
        <br />

        <div class="subheader collapsible" id='facebooksubheader'>Create Facebook Wall Post <%: Html.CheckBox("PostToFacebook",false, new { @class="checkboxinput"}) %></div>
         <table class="itemtable" id="facebook">
            <%if(ViewData["facebookRegistered"] != null && (bool)ViewData["facebookRegistered"] == true){
                %>
            <tr><td class="itemheader">Wall Post</td></tr> <tr><td>&nbsp;&nbsp;&nbsp;<%: Html.TextArea("FacebookWallPost", new { @class = "longtext textinput" })%></td></tr>
            <tr><td>Campus Social is linked to <a href="http://www.facebook.com/profile.php?id=<%= ViewData["facebookUserId"] %>"><%= ViewData["facebookUserName"] %>'s</a> Facebook account.
            <%if (ViewData["facebookPageId"] != null)
            {  %>
            <br /> Post will be posted on the <a href="http://www.facebook.com/profile.php?id=<%= ViewData["facebookPageId"] %>"><%= ViewData["facebookPageName"] %></a> page.
            <%} %>
            </td></tr>
            <%} else { %>
            <tr><td>To create a Facebook event at the same time as your event, you can <%: Html.ActionLink("link your Facebook account by clicking here!", "Accounts", "Settings")%></td></tr>
            <%} %>
         </table>
   </div>
 

<div id="submitdiv">
<button type="submit">
<a href="#" id="submit_link" class="cssbutton_med">Send Message</a>
</button>
</div>
<%} %>
</div>

    <script src="../../Scripts/slideFade.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //this collapses the linked accounts by default
            $('.collapsible').next().slideToggle(10);

            //datepicker javascript
            $(function () {
                $(".dateinput").datepicker({ dateFormat: 'dd/mm/yy' });
                $('.dateinput').change(function () {
                    $(this).val($(this).val());
                });
            });

            $('#PostToFacebook').click(function (e) {
                if ($(this).is(':checked') && !($(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                } else if ((!($(this).is(':checked')) && $(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                }
                e.stopImmediatePropagation();
            });

            $('#PostToTwitter').click(function (e) {
                if ($(this).is(':checked') && !($(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                } else if ((!($(this).is(':checked')) && $(this).parent().next().is(':visible'))) {
                    $(this).parent().next().slideToggle(10);
                }
                e.stopImmediatePropagation();
            });

            //collapsible javascript
            $(".collapsible").click(function () {
                $(this).next().slideToggle(10);
            });

            //twitter character javascript
            $('#TweetText').keyup(function () {
                var charLength = $(this).val().length;
                if ($('#TwitterEventLink').attr('checked')) {
                    charLength = charLength + 20;
                }
                if (charLength == 0) {
                    $('#tweetchars').html('(max 140 chars):');
                } else if (charLength > 140) {
                    $('#tweetchars').html('(<font color="red">' + (140 - charLength) + ' characters left</font>):');
                } else {
                    $('#tweetchars').html('(' + (140 - charLength) + ' characters left):');
                }
            });

            $('#TwitterEventLink').mouseup(function () {
                var charLength = $('#TweetText').val().length;
                if (!$(this).attr('checked')) {
                    charLength = charLength + 20;
                }
                if (charLength > 140) {
                    $('#tweetchars').html('(<font color="red">' + (140 - charLength) + ' characters left</font>):');
                } else {
                    $('#tweetchars').html('(' + (140 - charLength) + ' characters left):');
                }
            });
        });
    </script>

 </asp:Content>