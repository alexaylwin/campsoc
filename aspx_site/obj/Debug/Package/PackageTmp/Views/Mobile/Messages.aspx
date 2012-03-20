<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Messages
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul data-role="listview" data-inset="true" data-theme="c">
    <%
        string mtitle = "";
        int mid = 0;
        foreach (var m in ViewData.Model)
        {
            mtitle = m.MessageTitle;
            mid = m.MessageID;
            if (mtitle == null || mtitle.Length == 0)
            {
                mtitle = "-";
            }
            %>
          
        <li>
        <%: Html.ActionLink(mtitle, "MessageDetails", new { id = mid })%>
        </li>
            
    <%} %>
    </ul>
</asp:Content>
