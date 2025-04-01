<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/Registration.Master" AutoEventWireup="true" CodeBehind="ForgotPasswordaspx.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.ForgotPasswordaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Student Academic Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Student Acedemic Details</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="login-box" id="Changepass" runat="server" visible="true">
        <div class="form_style">
            <div class="card">
                <div class="pt-3">
                    <h3 class="change_psw_title mb-0 text-center">Reset Password</h3>
                </div>
                <div class="card-body p-4">
                    <div class="row align-items-center">
                        <asp:HiddenField ID="hfDettails" runat="server" />
                        <div class="col-md-12">
                            <div class="form-group text-center">
                                <asp:Label ID="lblError1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:TextBox ID="txtUserName" MaxLength="10" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" placeholder="Enter registered mobile no" ValidationGroup="OTP"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ValidationGroup="OTP" ControlToValidate="txtUserName"
                                    ErrorMessage="Enter MobileNo" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div id="divbtnOTP" class="col-md-4 text-center">
                            <asp:Button runat="server" ID="lnkbtnOTPs" ValidationGroup="VgChangePassword" CssClass="btn commonbtn" Text="Send OTP" OnClick="lnkbtnOTP_Click" />
                        </div>
                        <div id="divContactNo2" class="col-md-4">
                            <div class="justify-content-between d-flex">
                                <asp:TextBox ID="txtOpt" Visible="false" CssClass="form-control" ValidationGroup="ValidateOTP" placeholder="OTP" runat="server" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                            </div>
                        </div>
                        <div id="divbtngo" class="col-md-4">
                            <asp:LinkButton ID="lnkbtngo" Visible="false" runat="server" CssClass="btn btn-primary gopay" ValidationGroup="ValidateOTP" OnClick="lnkbtngo_Click">Verify OTP</asp:LinkButton>
                        </div>

                    </div>

                    <%-- <div class="row">
                    <div class="col-md-12" id="Div1" runat="server">
                        <div class="form-group">
                            <label for="exampleInputFile">Old Password<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtOldPassword" TextMode="Password" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtOldPassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtOldPassword"
                                Display="Dynamic" ErrorMessage="Enter Old Password" />

                            <asp:CompareValidator runat="server" ID="cmOldPassword" ControlToValidate="txtOldPassword"
                                Operator="Equal" ForeColor="Red" Type="String" ErrorMessage="Old Password incorrect" />
                        </div>
                    </div>
                </div>--%>

                    <div class="row align-items-center" id="pass_visible" runat="server">

                        <div class="col-md-10 col-10" id="Div1" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">New Password<span class="req-field text-danger">*</span></label>
                                <asp:TextBox ID="txtPassword" TextMode="Password" onpaste="return false"
                                    oncut="return false" oncopy="return false" runat="server" CssClass="form-control" ValidationGroup="VgChangePassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassWord" CssClass="validationmsg" ValidationGroup="VgChangePassword" ForeColor="Red" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtPassword"
                                    Display="Dynamic" ErrorMessage="Enter New Password" />
                            </div>
                        </div>
                        <div class="col-md-2 col-2" id="Div3" runat="server">
                            <div class="form-group mb-0 mt-2">
                                <button id="btnNewPassword" class="btn psw_btn" onclick="passwordChange();" type="button">
                                    <span id="sppass" class="fa fa-eye-slash icon"></span>
                                </button>
                            </div>
                        </div>
                        <div class="col-md-10 col-10" id="cnpass_visible" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Confirm New Password<span class="req-field text-danger">*</span></label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" onpaste="return false"
                                    oncut="return false" oncopy="return false" TextMode="Password" ValidationGroup="VgChangePassword" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" ForeColor="Red" ValidationGroup="VgChangePassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtConfirmPassword"
                                    Display="Dynamic" ErrorMessage="Enter New confirm  Password" />
                                <asp:CompareValidator runat="server" ID="cmpPassword" ForeColor="Red" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                                    Operator="Equal" Type="String" ErrorMessage="Password mismatch" />
                            </div>
                        </div>
                        <div class="col-md-2 col-2" id="Div2" runat="server">
                            <div class="form-group">
                                <button id="btnconPassword" class="btn psw_btn" type="button" onclick="conpasswordFunction();">
                                    <span id="spconpass" class="fa fa-eye-slash icon_close"></span>
                                </button>
                            </div>
                        </div>
                        <input type="checkbox" style="display: none" onclick="myFunction()">
                        <div class="col-md-12 col-12" style="text-align: center;">
                            <label for="txtCastName">&nbsp;</label>
                            <asp:Button runat="server" ID="Button1" ValidationGroup="VgChangePassword" CssClass="btn commonbtn" Text="Change Password" OnClick="btn_Save_Click" />
                            <label for="txtCastName">&nbsp;</label>

                        </div>
                        <div class="col-md-12" style="text-align: center;">
                        </div>
                    </div>
                    <div class="col-md-12" style="text-align: center;">
                        <label for="txtCastName">&nbsp;</label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="lockscreen-wrapper col-md-12" id="Loginpageredirect" runat="server" visible="false">
        <div class="card">
            <div class="card-body box box-solid">
                <div class="box-body">
                    <div class="lockscreen-wrapper">

                        <div class="help-block text-center">
                            <h3 style="color: white;">Your password has been change successfully!</h3>
                        </div>
                        <div class="text-center">
                            <h2><a href="<%=ResolveUrl("~/Admission/default.aspx") %>" style="color: burlywood;">Click here for Login</a></h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.center -->
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
 <%--   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>

    <script type="text/javascript">

        function passwordChange() {

            var x = document.getElementById("Body_txtPassword");
            if (x.type === "password") {
                x.type = "text";
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            } else {
                x.type = "password";
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            }
        }

        function conpasswordFunction() {
            var x = document.getElementById("Body_txtConfirmPassword");
            if (x.type === "password") {
                x.type = "text";
                $('.icon_close').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            } else {
                x.type = "password";
                $('.icon_close').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            }
        }

    </script>
    <script>
        $('#txtConfirmPassword').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    </script>
    <script>
        function myFunction() {
            var x = document.getElementById("Body_txtPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
            var x1 = document.getElementById("Body_txtConfirmPassword");
            if (x1.type === "password") {
                x1.type = "text";
            } else {
                x1.type = "password";
            }
        }
    </script>
</asp:Content>
