<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StentPrice.aspx.cs" Inherits="Unmehta.WebPortal.Web.StentPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Stent Price
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
                <li>Stent Price</li>
            </ul>
        </div>
    </div>
    <!-- About Section -->
    <section class="content">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="section-main-title">
                        <h2>Coronary Stent Price</h2>
                    </div>
                    <div class="card  mb-0">
                        <div class="card-body">
                                    <%=strPageDetails %>
                        </div>
                    </div>

                </div>
                <div class="col-lg-3" style="display:none;">
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
