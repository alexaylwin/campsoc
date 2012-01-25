﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content/mobilesites/masterpages/macengsociety.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Event
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    /*$(document).delegate('#submitthank', 'click', function () {  
        $(this).simpledialog({
            'mode': 'blank',
            'prompt': false,
            'forceInput': false,
            'useModal': true,
            onClose: function(event, target){
                $('#feedback').submit();
            },
            'fullHTML': 
            "Thank you for your submission! <a rel='close' data-role='button' href='#' id='simpleclose'>Close</a>"
        })
    });*/
</script>
    <h3 id="eventname"><%=ViewData["EventName"] %></h3>
    <span id="eventdate"><%=ViewData["EventDate"] %></span> <br /> 
    <span id="eventlocation">At <%=ViewData["EventLocation"] %></span> <br /> <br />
    <div id="eventdescription"><%=ViewData["EventDescription"] %></div>

    <%if(ViewData["survey"] != null && (int)ViewData["survey"] == 1){ %>
    <hr />
    <div id="eventfeedback">
        <p> Please fill out this survey to help us plan future events!</p>
        <% using (Html.BeginForm("EventDetails","Mobile",FormMethod.Post,new{@id="feedback"}))
           { %>
           <%if(ViewData["survey_Q1"] != null){
                 if(ViewData["survey_Q1C"] != null && (int)ViewData["survey_Q1C"] == 3){
            %>
            <fieldset data-role="controlgroup">
                <legend><%=ViewData["survey_Q1"] %></legend>
                <%: Html.RadioButton("Q1_R", "1", new {@id = "Q1_R1"}) %>
                <label for="Q1_R1">Excellent</label>
                <%: Html.RadioButton("Q1_R", "2", new { @id = "Q1_R2"})%>
                <label for="Q1_R2">Fair</label>
                <%: Html.RadioButton("Q1_R", "3", new { @id = "Q1_R3"})%>
                <label for="Q1_R3">Poor</label>
            </fieldset>
                <%} else {%>
                <label for="Q1_1"><%=ViewData["survey_Q1"] %></label>
                <%: Html.TextBox("Q1_R", "", new {@id="Q1_R"}) %>
                <%} %>
        <%} %>

           <%if(ViewData["survey_Q2"] != null){
                 if(ViewData["survey_Q2C"] != null && (int)ViewData["survey_Q2C"] == 3){
            %>
            <fieldset data-role="controlgroup">
                <legend><%=ViewData["survey_Q2"] %></legend>
                <%: Html.RadioButton("Q2_R", "1", new { @id = "Q2_R1"})%>
                <label for="Q2_R1">Excellent</label>
                <%: Html.RadioButton("Q2_R", "2", new { @id = "Q2_R2"})%>
                <label for="Q2_R2">Fair</label>
                <%: Html.RadioButton("Q2_R", "3", new { @id = "Q2_R3"})%>
                <label for="Q2_R3">Poor</label>
            </fieldset>
                <%} else {%>
                <label for="Q2_1"><%=ViewData["survey_Q2"] %></label>
                <%: Html.TextBox("Q2_R", "", new {@id="Q2_R"}) %>
                <%} %>
        <%} %>
           <%if(ViewData["survey_Q3"] != null){
                 if(ViewData["survey_Q3C"] != null && (int)ViewData["survey_Q3C"] == 3){
            %>
            <fieldset data-role="controlgroup">
                <legend><%=ViewData["survey_Q3"] %></legend>
                <%: Html.RadioButton("Q3_R", "1", new { @id = "Q3_R1"})%>
                <label for="Q3_R1">Excellent</label>
                <%: Html.RadioButton("Q3_R", "2", new { @id = "Q3_R2" })%>
                <label for="Q3_R2">Fair</label>
                <%: Html.RadioButton("Q3_R", "3", new { @id = "Q3_R3"})%>
                <label for="Q3_R3">Poor</label>
            </fieldset>
                <%} else {%>
                <label for="Q3_1"><%=ViewData["survey_Q3"] %></label>
                <%: Html.TextBox("Q3_R", "", new {@id="Q3_R"}) %>
                <%} %>
        <%} %>
           <%if(ViewData["survey_Q4"] != null){
                 if(ViewData["survey_Q4C"] != null && (int)ViewData["survey_Q4C"] == 3){
            %>
            <fieldset data-role="controlgroup">
                <legend><%=ViewData["survey_Q4"] %></legend>
                <%: Html.RadioButton("Q4_R", "1", new { @id = "Q4_R1"})%>
                <label for="Q4_R1">Excellent</label>
                <%: Html.RadioButton("Q4_R", "2", new { @id = "Q4_R2"})%>
                <label for="Q4_R2">Fair</label>
                <%: Html.RadioButton("Q4_R", "3", new { @id = "Q4_R3"})%>
                <label for="Q4_R3">Poor</label>
            </fieldset>
                <%} else {%>
                <label for="Q4_1"><%=ViewData["survey_Q4"] %></label>
                <%: Html.TextBox("Q4_R", "", new {@id="Q4_R"}) %>
                <%} %>
        <%} %>
        <%: Html.Hidden("EventID", ViewData["EventID"]) %>
        <input type="submit" value="Submit Survey" id="submitthank"/>
    <%} %>

    </div>
    <%} %>

</asp:Content>
