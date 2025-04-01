<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/Registration.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.ChangePassword" %>


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
                    <h3 class="change_psw_title mb-0 text-center">Change Password</h3>
                </div>
                <div class="card-body p-4">
                    <div class="row align-items-center">
                        <div class="col-md-12 col-12">
                            <div class="form-group mb-0">
                                <label for="exampleInputFile">User Name<span class="req-field text-danger">*</span></label>
                                <asp:TextBox ID="txtUserName" MaxLength="50" runat="server" CssClass="form-control" ValidationGroup="VgChangePassword" ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUserName" ValidationGroup="VgChangePassword"
                                    ErrorMessage="Enter Username" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12 col-12" id="Div1" runat="server">
                            <div class="form-group mb-0">
                                <label for="exampleInputFile">Old Password<span class="req-field text-danger">*</span></label>
                                <asp:TextBox ID="txtOldPassword" TextMode="Password" onpaste="return false"
                                    oncut="return false" oncopy="return false" autocomplete="off" runat="server" ValidationGroup="VgChangePassword" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtOldPassword" ForeColor="Red" ValidationGroup="VgChangePassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtOldPassword"
                                    Display="Dynamic" ErrorMessage="Enter Old Password" />

                                <asp:CompareValidator runat="server" ID="cmOldPassword" ControlToValidate="txtOldPassword" ValidationGroup="VgChangePassword"
                                    Operator="Equal" ForeColor="Red" Type="String" ErrorMessage="Old Password incorrect" />
                            </div>
                        </div>
                        <div class="col-md-10 col-10" id="pass_visible" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">New Password<span class="req-field text-danger">*</span></label>
                                <asp:TextBox ID="txtPassword" TextMode="Password" onpaste="return false"
                                    oncut="return false" oncopy="return false"  runat="server" CssClass="form-control" ValidationGroup="VgChangePassword"></asp:TextBox>
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
                            <asp:Button runat="server" ID="btn_Save" ValidationGroup="VgChangePassword" CssClass="btn commonbtn" Text="Change Password" OnClick="btn_Save_Click" />
                            <label for="txtCastName">&nbsp;</label>
                            <asp:Button runat="server" ID="btnLogin" ValidationGroup="VgLogin" CssClass="btn commonbtn" Text="Login Page" OnClick="btnLogin_Click" />
                        </div>
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
                            <h3 style="    color: white;">Your password has been change successfully!</h3>
                        </div>
                        <div class="text-center">
                            <h2><a href="<%=ResolveUrl("~/Admission/default.aspx") %>" style="    color: burlywood;">Click here for Login</a></h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.center -->
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

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
</asp:Content>
