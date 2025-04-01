<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DoctorRegisterMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Appointment.DoctorRegisterMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>Doctor Register</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Appointment Detail</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Appointment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Appointment Detail</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
    <div class="card-body">
        <asp:HiddenField ID="hfId" runat="server" />
        <div class="row" id="dvMain" runat="server">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="txtFirstName">Select Department</label>
                    <asp:DropDownList ID="ddlSpecialization" class="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="ddlUnit">Select Unit  </label>
                    <asp:DropDownList ID="ddlUnit" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="exampleInputFile">
                        Select Doctor   
                    </label>
                    <asp:DropDownList ID="ddlDoctorList" CssClass="doc1 form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            

        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label for="exampleInputFile">User Name<span class="req-field">*</span></label>
                    <asp:TextBox ID="txtUserName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="Enter Username" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3" id="pass_visible" runat="server">
                <div class="form-group">
                    <label for="exampleInputFile">Password<span class="req-field">*</span></label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassWord" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                        ControlToValidate="txtPassword"
                        Display="Dynamic" ErrorMessage="Enter Password" />
                </div>
            </div>
            <div class="col-md-3" id="cnpass_visible" runat="server">
                <div class="form-group">
                    <label for="exampleInputFile">Confirm Password<span class="req-field">*</span></label>

                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                        ControlToValidate="txtConfirmPassword"
                        Display="Dynamic" ErrorMessage="Enter confirm  Password" />
                    <asp:CompareValidator runat="server" ID="cmpPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                        Operator="Equal" Type="String" ErrorMessage="Password mismatch" />
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-md-12 controls">
                <div class="form-group">
                    <button runat="server" id="btn_Save" class="btn btn-primary" title="Search" onserverclick="btn_Save_ServerClick">
                        <i class="fa fa-search">&nbsp;Save</i>
                    </button>
                    <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                        <i class="fa fa-remove">&nbsp;Clear</i>
                    </button>
                </div>
            </div>
        </div>
        
    </div>
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
