<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">User Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Enter Username" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">First Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" CssClass="validationmsg" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="Enter FirstName" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Last Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" CssClass="validationmsg" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="Enter Lastname" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                  <%--  <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Department<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlDept" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Designation<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpDesignation" CssClass="form-control" runat="server" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Role<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpRole" CssClass="form-control" runat="server" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Email<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                Display="Dynamic" ErrorMessage="Invalid email address" />
                            <asp:RequiredFieldValidator ID="rfvEmail" CssClass="validationmsg" runat="server" SetFocusOnError="true" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="Enter Email adddress" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Mobile No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                ControlToValidate="txtMobile" CssClass="validationmsg" ErrorMessage="Eneter valid mobile number"
                                ValidationExpression="[0-9]{10}" SetFocusOnError="true"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvMobile" CssClass="validationmsg" runat="server" SetFocusOnError="true" ControlToValidate="txtMobile"
                                Display="Dynamic" ErrorMessage="Enter mobile number" />
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
