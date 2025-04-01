<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StudentAllocateSeats.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.StudentAllocateSeats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Student Allocate Seats</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Student Allocate Seats</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <div class="row">
                
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtAcademicFullsName">Academics Full Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtRoundName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <br />
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary " Text="Submit" OnClick="btnSubmit_Click" Style="margin-top: 7px;" />
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
