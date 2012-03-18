<%@ Page Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Campus Social
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="pageheader">Welcome to Campus Social</h2>
        <div class="tagline">Campus Social is an event promotion and event management service that is built for students, by students. <br /> It gives your student club the tools it needs to get higher attendance at events and connect with more students, more easily.</div>
        <div style="font-size: 1.1em">
        <br />Campus Social focuses on: <br />
        <strong>&nbsp;&nbsp;Customizability:</strong> Each student club is different, and is going to have different needs to be successful. <br />
        <strong>&nbsp;&nbsp;Cost:</strong> Club budgets are tight. Enough said. <br />
        <strong>&nbsp;&nbsp;Support and experience:</strong> I know how hard it can be to run student clubs, because I have before. I know what it takes, and I know how to help.<br />
        </div>

        <div id="signupform">
        <% using (Html.BeginForm()) { %>
            Interested? Submit your email for more information: <%: Html.TextBox("email") %> <br /> <br />
             <button type="submit">
            <a href="#" id="submit_link" class="cssbutton_med">I want info!</a>
            </button>
        <%} %>
        </div>
        
        <div id="buttons">
        <div id="text"><strong> Learn more:</strong></div>
        <%: Html.ActionLink("about", "About", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("tour", "Tour", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        <%: Html.ActionLink("contact", "Contact", null, new { @class="cssbutton_big", @id="aboutbutton"})%>
        </div>
        <br />

</asp:Content>
