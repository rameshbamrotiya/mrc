<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MobileOTP.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.MobileOTP" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UNMEHTA - Appointment Mobile OTP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .form-wrapper {
            margin: 12px auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row">

            <div class="form-wrapper" style="width: 50%;">
                <div class="col-12">
                    <h5>OTP Verification</h5>
                </div>
                <div class="col-12" style="margin-bottom:50px">

                    <!-- form -->
                    <div class="form-group mb--10">
                        <asp:TextBox ID="txtMobileNo" class="form-control col-6" placeholder="MobileNo" Style="float: left;" required autofocus runat="server" ValidationGroup="OTP"></asp:TextBox>
                        <asp:Button ID="btnSendOTP" class="btn btn-primary btn-block col-4 left" OnClick="btnSendOTP_Click" Style="float: left; margin-left: 54px;" runat="server" Text="Send OTP" />
                    </div>
                </div>
                <div class="col-12">

                    <div class="form-group" style="clear: both; padding-top: 10px" id="dvOTPEnter" runat="server">
                        <asp:TextBox ID="txtOTP" class="form-control" placeholder="OTP" runat="server" ValidationGroup="OTP"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOTP" runat="server" ErrorMessage="*" ControlToValidate="txtOTP" Display="Dynamic" ForeColor="Red" ValidationGroup="OTP"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-block" OnClick="btnLogin_Click" ValidationGroup="OTP" runat="server" Text="Verify" />
                    </div>


                    <span id="lblError" runat="server" style="margin-top: 50px;"></span>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">


    <%--    <!-- Plugin scripts -->
    <script src="/Admin/Template/html/vendors/bundle.js"></script>

    <!-- App scripts -->
    <script src="/Admin/Script/App.js"></script>
    <script src="/Admin/Script/MainPage.js"></script>--%>
</asp:Content>
