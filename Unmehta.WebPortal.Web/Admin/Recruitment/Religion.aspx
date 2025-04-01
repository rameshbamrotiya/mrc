<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Religion.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.Religion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Religion</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server"><!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Cast</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Religion</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                    <div class="form-group">
                        <label for="txtReligionName">Religion Name</label>
                        <asp:TextBox ID="txtReligionName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Religion Name" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group form-check">
                        <br />
                        <br />
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label class="form-check-label" for="chkEnable">Active</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <label for="txtReligionName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                           { %>
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <%} %>
                    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body" id="divGrid" runat="server" visible="false">
            <div class="row">
                <div class="col-md-10" id="gvInnerGridView" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="/Admin/Script/Recruitment/Religion.js"></script>
</asp:Content>
