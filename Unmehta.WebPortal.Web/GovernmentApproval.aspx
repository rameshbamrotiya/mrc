<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GovernmentApproval.aspx.cs" Inherits="Unmehta.WebPortal.Web.GovernmentApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Government Approval
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <!-- Breadcrumb -->
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Government Approval</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->

    <!-- Page Content -->
    <!-- About Section -->
    <section class="content" style="min-height: 178.016px;">
        <div class="container">
            <div class="row">
                <div class="col-md-9 col-lg-9">
                    <%=strBoard %>
                </div>
                <div class="col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">Quick Links</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories">
                                    <%=strQuickLink %>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- End About Section -->
    <!-- /Page Content -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
