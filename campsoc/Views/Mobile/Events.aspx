﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Events
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ul data-role="listview" data-inset="true" data-theme="c">
    <%
        string ename = "";
        int eid = 0;
        string edate = "";
        foreach (var e in ViewData.Model)
        {
            ename = e.EventName;
            eid = e.EventID;
            edate = e.EventStart.ToShortDateString() + " " + e.EventStart.ToShortTimeString();
            if (ename == null || ename.Length == 0)
            {
                ename = "-";
            }
            %>
          
        <li>
        <%: Html.ActionLink(ename,"EventDetails", new { id=eid }) %>
        <div id="eventlistdate"><%=edate %></div>
        </li>
            
    <%} %>
    </ul>

</asp:Content>
