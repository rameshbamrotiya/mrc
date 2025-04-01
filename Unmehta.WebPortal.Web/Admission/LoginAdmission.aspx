<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/Registration.Master" AutoEventWireup="true" CodeBehind="LoginAdmission.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.LoginAdmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    U.N.Mehta
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h3>U.N.Mehta</h3>
            <ul class="page-breadcrumb">
                <li>Sign in</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <%--<div class="col-md-3 col-lg-3">
        </div>--%>
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="login-box">
                <div class="card card-outline card-primary">
                    <div class="card-header text-center">
                        <h3><b>Sign in</b></h3>
                    </div>
                    <div class="card-body">
                        <div class="contact-area pt-50 pb-70">
                            <div class="container" style="text-align: -webkit-center;">
                                <%--<h3 style="text-align: center;">Sign in</h3>--%>
                                <div class="col-md-8 form-group">
                                    <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username" autofocus></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Enter username." Display="Dynamic" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-8 form-group">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="efvPassword" runat="server" ErrorMessage="Enter password." Display="Dynamic" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-8 form-group">
                                    <asp:Image ID="imgCaptcha" runat="server" Style="float: left" />
                                    <button runat="server" id="btnRun" class="btn btn-block btn-outline-primary" causesvalidation="false" onserverclick="btnRun_ServerClick" style="float: left; width: auto; margin-left: 10px; margin-top: 5px;" title="Refresh">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                </div>
                                <div class="form-group clearfix"></div>
                                <div class="col-md-8 form-group">
                                    <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" MaxLength="4" class="form-control" onkeypress="return isNumberKey(event)" placeholder="Captcha"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Enter captcha." Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator ID="cvCaptcha" runat="server" ErrorMessage="Captcha Not Match" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:CompareValidator>--%>
                                </div>
                                <div class="col-md-8 form-group">
                                    <asp:Button ID="btnSignIn" runat="server" class="btn btn-primary btn-block" Text="Sign in" OnClick="btnSignIn_Click" />
                                    Don't have an account?
                                    <a href="<%= ResolveUrl("~/Admission/StudentReg")%>">Register now!</a>
                                    <a href="<%= ResolveUrl("~/Admission/ForgotPasswordaspx") %>">Forgot Password</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="col-md-3 col-lg-3">
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
