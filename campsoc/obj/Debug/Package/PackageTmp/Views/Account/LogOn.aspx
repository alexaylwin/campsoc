<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<aspx_site.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log in to your Campus Social account!</h2>
    <p>
        Please enter your username and password.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
            <!--
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%: Html.LabelFor(m => m.UserName) %>: <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%: Html.LabelFor(m => m.Password) %> <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                -->
                <table>
                <tr><td><%: Html.LabelFor(m => m.UserName) %>:</td><td><%: Html.TextBoxFor(m => m.UserName) %></td></tr>
                <tr><td><%: Html.LabelFor(m => m.Password) %>:</td><td><%: Html.PasswordFor(m => m.Password) %></td></tr>
                </table>
                <div class="editor-label">
                    <%: Html.CheckBoxFor(m => m.RememberMe) %>
                    <%: Html.LabelFor(m => m.RememberMe) %>
                </div>
                
                <p>
                    <input type="submit" value="Log On" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
