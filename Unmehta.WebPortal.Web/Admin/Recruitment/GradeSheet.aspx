<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GradeSheet.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.GradeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Grade Sheet</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a>Grade Sheet</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Advertisement</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlAdvertisementCode">Advertisement No.</label>
                        <asp:DropDownList ID="ddlAdvertisementCode" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlRecruitmentType">Recruitment Post</label>
                        <asp:DropDownList ID="ddlRecruitmentType" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <iframe id="my_iframe" style="display: none;"></iframe>
                </div>
            </div>
            <div class="row">

                <%-- <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
                    <div class="row form-row">--%>
                <%--<div class="col-md-5">
                        </div>--%>
                <div class="col-md-3">

                    <div class="form-group">

                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Generate Grade Sheet" OnClick="btnSave_Click" />

                    </div>
                </div>
                <%--</div>
                    </div>--%>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
