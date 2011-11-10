<%@ Page Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>About Campus Social</h2>
    <p>
        I'm Alex, and I'm currently a fifth year undergradate at McMaster University, studying
        Software Engineering and Society. I created Campus Social initially to support and advertise
        McMaster Engineering Society (<a href="http://mes.mcmaster.ca">http://mes.mcmaster.ca</a>) events and programs. If you want to 
        get in touch with me, you can email me at <a href="mailto:AlexAylwin@gmail.com">
        AlexAylwin@gmail.com</a>, or find me on LinkedIn at 
        <a href="http://www.linkedin.com/in/alexaylwin">http://www.linkedin.com/in/alexaylwin</a>.
    </p>
    <p>
        Campus Social is a service that provides student clubs, societies, fraternities 
        and so on with an easy way to connect with their markets and advertise their 
        services and events. It also provides mechanisms for tracking the success or 
        failure of events, and assists in future event planning. If you think this 
        service would be right for you, you can get in touch with Alex via email at 
        Alex@campsoc.com
    </p>

    </form>
</asp:Content>
