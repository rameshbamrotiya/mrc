<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Holiday.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.Holiday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Holiday</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Holiday</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Holiday</li>
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
                    <asp:HiddenField ID="hfID" runat="server" />
                    <div class="form-group">
                        <label for="txtHolidayDate">Holiday Date</label>
                        <asp:TextBox autocomplete="off" ID="txtHolidayDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Holiday Date" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtholidaydesc">Holiday Name</label>
                        <asp:TextBox ID="txtholidaydesc" autocomplete="off" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Holiday Description" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlQualification">Active</label>
                                <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                    <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
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
    <script src="<%= ResolveUrl("~/Admin/Script/CMS/HolidayMaster.js")%>"></script>
</asp:Content>
