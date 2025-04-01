<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="City.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.City" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>City Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>City</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">City</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
     <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                    <div class="form-group">
                        <label for="txtDistrictName">District Name</label>
                        <asp:TextBox autocomplete="off" ID="txtDistrictName" aria-describedby="emailHelp" CssClass="form-control" placeholder="District Name" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlQualification">State</label>
                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>&nbsp;</label>
                    <label>&nbsp;</label>
                    <div class="form-group">
                         <% if (SessionWrapper.UserPageDetails.CanAdd)
                             { %>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <%} %>
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <iframe id="my_iframe" style="display:none;"></iframe>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12" id="gvInnerGridView" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="<%=ResolveUrl("~/Admin/Script/Recruitment/City.js") %>"></script>
</asp:Content>
