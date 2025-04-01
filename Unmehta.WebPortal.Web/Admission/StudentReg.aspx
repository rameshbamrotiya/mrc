<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/Registration.Master" AutoEventWireup="true" CodeBehind="StudentReg.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.StudentReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    U.N.Mehta
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <%--<div class="col-md-2 col-lg-2">
        </div>--%>
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="login-box" style="width: auto;">
            </div>

            <!-- general form elements disabled -->
            <div class="card card-outline card-primary" id="Registarationform" runat="server" visible="true">
                <div class="card-header">
                    <h3 class="text-center"><b>Student Registration</b></h3>
                </div>
                <!-- /.card-header -->
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Title<span class="req-field">*</span></label>
                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Dr." Value="Dr"></asp:ListItem>
                                    <asp:ListItem Text="Prof." Value="Prof"></asp:ListItem>
                                    <asp:ListItem Text="Mr." Value="Mr"></asp:ListItem>
                                    <asp:ListItem Text="Ms." Value="Ms"></asp:ListItem>
                                    <asp:ListItem Text="Mrs." Value="Mrs"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTitle" InitialValue="0" CssClass="validationmsg" runat="server" ControlToValidate="ddlTitle"
                                    ErrorMessage="Select title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <!-- text input -->
                            <div class="form-group">
                                <label>Name</label>
                                <asp:TextBox ID="txtFirstname" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Enter full name as per last marksheet" ValidationGroup="Main" onpaste="return OnlyAllowedlettersWithSpaceOnlyPaste(this);" onkeypress="return OnlyAllowedlettersWithSpaceOnly(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ErrorMessage="Enter full name as per last marksheet." Display="Dynamic" ForeColor="Red" ControlToValidate="txtFirstname" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--<div class="col-sm-3">
                            <div class="form-group">
                                <label>Name</label>
                                <asp:TextBox ID="txtMiddleName" runat="server" class="form-control" placeholder="MiddleName" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ErrorMessage="Enter name." Display="Dynamic" ForeColor="Red" ControlToValidate="txtMiddleName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Father/Husband Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="LastName" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Enter Father/Husband Name." Display="Dynamic" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <!-- textarea -->
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" AutoCompleteType="Disabled" placeholder="Email" ValidationGroup="Main"></asp:TextBox>
                                <asp:Button ID="btnSendOtp" runat="server" Text="Send OTP" CausesValidation="false" CssClass="btn btn-primary btn-inline-block mt-2" OnClick="btnSendOtp_Click" />
                                </br>
                                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format."></asp:RegularExpressionValidator>
                                </br>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Enter email." Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>OTP<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtOtpNo" CssClass="form-control" MaxLength="6" AutoCompleteType="Disabled" runat="server" Enabled="false"></asp:TextBox>
                                <asp:Button ID="btnVerifyOtp" runat="server" Text="Verify OTP" CssClass="btn btn-primary btn-inline-block mt-2" Enabled="false" CausesValidation="false" OnClick="btnVerifyOtp_Click" />
                                </br>
                                <asp:Label ID="lblotpno" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:Label ID="lblOTPVerify" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Mobile No</label>
                                <asp:TextBox ID="txtPhoneNo" runat="server" class="form-control" AutoCompleteType="Disabled" placeholder="PhoneNo" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" ValidationGroup="Main"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ErrorMessage="Enter Mobile" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNo" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group ">
                                <label>Birth Date</label>
                                <div class="input-group date" id="reservationdate" data-target-input="nearest">
                                    <asp:TextBox ID="txtBirthDate" runat="server" AutoCompleteType="Disabled" data-target="#reservationdate" class="form-control datetimepicker-input" MaxLength="10" placeholder="BirthDate" ValidationGroup="Main"></asp:TextBox>

                                    <div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ErrorMessage="Select birthdate." Display="Dynamic" ForeColor="Red" ControlToValidate="txtBirthDate" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Gender</label>
                                <asp:DropDownList ID="ddlGender" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Marital Status</label>
                                <asp:DropDownList ID="ddlMaritalStatus" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 mt-4">
                            <div class="form-group">
                                <asp:Image ID="imgCaptcha" runat="server" Style="float: left" />
                                <button runat="server" id="btnRun" class="btn btn-block btn-outline-primary" causesvalidation="false" onserverclick="btnRun_ServerClick" style="float: left; width: auto; margin-left: 7px; margin-top: 5px;" title="Search">
                                    <i class="fas fa-sync-alt"></i>
                                </button>
                            </div>
                        </div>
                        <div class="col-sm-3 mt-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" MaxLength="4" onkeypress="return isNumberKey(event)" CssClass="form-control" placeholder="Captcha"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Enter captcha." Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                <%--<asp:CompareValidator ID="cvCaptcha" runat="server" ErrorMessage="Captcha Not Match" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:CompareValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>

                    </div>
                    <div class="row " style="text-align: center;">
                        <div class="col-sm-12">
                            <asp:Button ID="btnRegistration" runat="server" class="btn btn-primary btn-inline-block" Text="Register" ValidationGroup="Main" Visible="false" OnClick="btnRegistration_Click" />
                        </div>
                        <div class="col-sm-12">
                            Already have an account?
                            <a href="<%= ResolveUrl("~/Admission/LoginAdmission")%>">Log In!</a>
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
                                    <h3>You have Register Successfully!</h3>
                                </div>
                                <div class="text-center">
                                    <h2><a href="/Admission/LoginAdmission.aspx">Click here for Login</a></h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.center -->
            </div>
            <!-- /.card-body -->
        </div>
    </div>
    <%-- <div class="col-md-2 col-lg-2">
        </div>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">

    <script lang="JavaScript" type="text/javascript">
        $(document).ready(function () {
            debugger;
            var $j = jQuery.noConflict();
            $j('#<%=txtBirthDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                language: "tr",
                yearRange: 'c-50:c+0'
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
</asp:Content>
