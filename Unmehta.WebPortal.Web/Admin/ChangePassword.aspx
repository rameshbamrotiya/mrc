<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">User Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtUserName" MaxLength="50" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUserName"
                            ErrorMessage="Enter Username" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4" id="Div1" runat="server">
                    <div class="form-group">
                        <label for="exampleInputFile">Old Password<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtOldPassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                            ControlToValidate="txtOldPassword"
                            Display="Dynamic" ErrorMessage="Enter Old Password" />

                        <asp:CompareValidator runat="server" ID="cmOldPassword" ControlToValidate="txtOldPassword"
                            Operator="Equal" Type="String" ErrorMessage="Password mismatch" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4" id="pass_visible" runat="server">
                    <div class="form-group">
                        <label for="exampleInputFile">New Password<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassWord" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                            ControlToValidate="txtPassword"
                            Display="Dynamic" ErrorMessage="Enter New Password" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4" id="cnpass_visible" runat="server">
                    <div class="form-group">
                        <label for="exampleInputFile">Confirm New Password<span class="req-field">*</span></label>

                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                            ControlToValidate="txtConfirmPassword"
                            Display="Dynamic" ErrorMessage="Enter New confirm  Password" />
                        <asp:CompareValidator runat="server" ID="cmpPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                            Operator="Equal" Type="String" ErrorMessage="Password mismatch" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <label for="txtCastName">&nbsp;</label>
                <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
