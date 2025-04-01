<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicMedical.aspx.cs" Inherits="Unmehta.WebPortal.Web.AcademicMedical" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Academic Medical Detail
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li><a href="<%=ResolveUrl("~/Academic") %>">Academic</a></li>
                <li>/</li>
                <li><a href="<%=ResolveUrl("~/AcademicMedicalMain") %>">Academic Medical</a></li>
                <li>/</li>
                <li><a href="#">Academic Medical Detail</a></li>
            </ul>
        </div>
    </div>
    <div class="content">
            <div class="container">
                <div class="row">
                    <!-- Doctor Details Tab -->
                    <div class="col-lg-9">
                        <div class="card">
                            <div class="card-body pt-0">
                                <div class="tab-content opdtiming">
                                    <%=strAcademicTabDetails %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="sidebar">
                            <div class="card widget-categories">
                                <div class="card-header">
                                    <h4 class="card-title">Medical</h4>
                                </div>
                                <div class="card-body">
                                    <ul class="categories">
                                        <%=strAcademicTab %>
                                        
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /Doctor Details Tab -->
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
