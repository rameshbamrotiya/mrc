<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="CurrentAdvertisements.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.CurrentAdvertisements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    CurrentAdvertisement Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Current Advertisements</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Current Advertisements</li>
            </ul>

        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <div class="contact-area pt-50 pb-70">
                        <div class="container">
                            <div class="row justify-content-md-center">
                                <div class="col-lg-12">
                                    <div class="section-main-title">
                                        <h2>Email Verification</h2>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Post<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlJobApplication" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlJobApplication_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvhdesc" InitialValue="0" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlJobApplication"
                                            ErrorMessage="Please select at least one post." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row col-md-8">
                                    <label id="labelas" runat="server" style="margin-right: 10px; font-size: 16px; font-weight: bold; color: red;">Instructions/Criteria For this post</label>
                                    <div runat="server" id="linkapply">
                                        <a target="_blank" href="<%= ResolveUrl("~/assets/img/career_user%20mannual%20(1).pdf") %>">How to apply</a>
                                    </div>
                                </div>
                                <div id="descdiv" runat="server" class="col-md-8 row">

                                    <div id="description" runat="server" class="form-group">
                                    </div>
                                    <%--<label id="description" runat="server"></label>--%>
                                </div>
                                <br />
                                <asp:Button ID="btnapply" Width="40%" runat="server" Text="Accept & Continue" CssClass="btn btn-primary submit-btn" OnClick="btnapply_Click" />
                            </div>
                            <div class="row justify-content-md-center" id="divOTP" runat="server" visible="false">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Email ID<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEmailId" CssClass="form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" CssClass="validationmsg" Font-Bold="true" ForeColor="Red" runat="server" ControlToValidate="txtEmailId"
                                            ErrorMessage="Please enter Email ID." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" Font-Bold="true" runat="server" ControlToValidate="txtEmailId" ForeColor="Red"
                                            CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Please enter valid Email ID." />
                                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">   
                                        <label>&nbsp;</label>       
                                        <br />                              
                                        <asp:Button ID="btnSendOtp" runat="server" Text="Send OTP" CssClass="btn btn-primary submit-btn" OnClick="btnSendOtp_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-12 text-center"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>OTP<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtOtpNo" CssClass="form-control" MaxLength="6" AutoCompleteType="Disabled" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblotpno" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                        <asp:Label ID="lblOTPVerify" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <br />
                                        <asp:Button ID="btnVerifyOtp" runat="server" Text="Verify OTP" CssClass="btn btn-primary submit-btn" Enabled="false" CausesValidation="false" OnClick="btnVerifyOtp_Click" />
                                        <br />

                                    </div>
                                </div>
                            </div>

                            <div class="row justify-content-md-center">
                                <!-- /.col -->
                                <div class="col-lg-2">
                                    <button runat="server" id="btnRefresh" class="btn btn-primary submit-btn" tabindex="4" title="Refresh" causesvalidation="false" onserverclick="btnRefresh_ServerClick" visible="false">
                                        Refresh
                                    </button>
                                    <button runat="server" id="btn_Apply" class="btn btn-primary submit-btn" tabindex="4" title="Apply" onserverclick="btnApply_Click" visible="false">
                                        Apply
                                    </button>
                                </div>
                                <!-- /.col -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
