<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create New Survey
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    List<SelectListItem> eventlist = new List<SelectListItem>();
    foreach (var e in (List<aspx_site.Models.novaevent>)ViewData["eventlist"])
    {
        eventlist.Add(new SelectListItem { Text = e.EventName, Value = Convert.ToString(e.EventID) }) ;
    }
%>
    <h2>Create New Survey</h2>
    <div class="createform" id="surveys">
    <% using (Html.BeginForm())
       { %>
    <div id="selectevent">
        <b>Event: </b><%: Html.DropDownList("EventList", eventlist, new { @class = "dropdownlist" })%>
    </div>

    <div id="surveyquestions">
        <div class="surveyquestion" id="question1"> 
            <div class="surveyqtitle"><%: Html.CheckBox("QuestionInclude1", false, new { @class = "checkboxinput" })%>Question One</div>
            <div class="surveyqbox" id="qbox1"><%:Html.TextBox("QuestionText1", "Question Text", new { @class = "textinput" })%> Multiple Choice: <%: Html.CheckBox("QuestionMC1", false, new { @class = "checkboxinput" })%></div>
            <div class="mcoptions" id="Q1Options">
                      <%:Html.TextBox("QuestionOptionText1_1", "Option", new { @class = "textinput" })%> Include option: <%: Html.CheckBox("QuestionOptionInclude1_1", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText1_2","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude1_2", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText1_3","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude1_3", false, new { @class = "checkboxinput" })%>
            </div>
        </div>

        <div class="surveyquestion" id="question2"> 
            <div class="surveyqtitle"><%: Html.CheckBox("QuestionInclude2", false, new { @class = "checkboxinput" })%>Question Two</div>
            <div class="surveyqbox" id="qbox2"><%:Html.TextBox("QuestionText2", "Question Text", new { @class = "textinput" })%> Multiple Choice: <%: Html.CheckBox("QuestionMC2", false, new { @class = "checkboxinput" })%></div>
            <div class="mcoptions" id="Q2Options">
                      <%:Html.TextBox("QuestionOptionText2_1", "Option", new { @class = "textinput" })%> Include option: <%: Html.CheckBox("QuestionOptionInclude2_1", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText2_2","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude2_2", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText2_3","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude2_3", false, new { @class = "checkboxinput" })%>
            </div>
        </div>

        <div class="surveyquestion" id="question3"> 
            <div class="surveyqtitle"><%: Html.CheckBox("QuestionInclude3", false, new { @class = "checkboxinput" })%>Question Three </div> 
            <div class="surveyqbox" id="qbox3"><%:Html.TextBox("QuestionText3", "Question Text", new { @class = "textinput" })%> Multiple Choice: <%: Html.CheckBox("QuestionMC3", false, new { @class = "checkboxinput" })%></div>
            <div class="mcoptions" id="Q3Options">
                      <%:Html.TextBox("QuestionOptionText3_1", "Option", new { @class = "textinput" })%> Include option: <%: Html.CheckBox("QuestionOptionInclude3_1", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText3_2","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude3_2", false, new { @class = "checkboxinput" })%>
                <br /><%:Html.TextBox("QuestionOptionText3_3","Option",new { @class = "textinput" }) %> Include option: <%: Html.CheckBox("QuestionOptionInclude3_3", false, new { @class = "checkboxinput" })%>
            </div>
        </div>
    </div>



     <div id="submitdiv">
 <button type="submit">
    <a href="#" id="submit_link" class="cssbutton_med">Create Survey</a>
</button>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $("[id^=QuestionMC]").attr('checked', false);
        $("[id^=QuestionInclude]").attr('checked', false);
        $("#QuestionInclude1").attr('checked', true);
        $("#qbox1").css('display', 'block');


        $("[id^=QuestionText]").click(function () {
            if ($(this).val() == "Question Text") {
                $(this).val("");
            }
        });

        $("[id^=QuestionText]").blur(function () {
            if ($(this).val() == "") {
                $(this).val("Question Text");
            }
        });

        $("[id^=QuestionOptionText]").click(function () {
            if ($(this).val() == "Option") {
                $(this).val("");
            }
        });

        $("[id^=QuestionOptionText]").blur(function () {
            if ($(this).val() == "") {
                $(this).val("Option");
            }
        });

        $("[id^=QuestionMC]").click(function () {
            $(this).parent().next().slideToggle(30);
        });

        $("[id^=QuestionInclude]").click(function () {
            if (!$(this).is(':checked')) {
                $(this).parent().next().hide();
            } else {
                $(this).parent().next().show();
            }
        });


    });
</script>

    <%} %>
</div>

</asp:Content>
