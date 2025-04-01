<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EMCS.aspx.cs" Inherits="Unmehta.WebPortal.Web.EMCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    EMCS
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
                <li><a href="#">EMCS</a></li>
            </ul>
        </div>
    </div>
    <!-- About Section -->
        <div class="content">
            <div class="container">
                <div class="row">
                    <!-- Doctor Details Tab -->
                    <div class="col-lg-9">
                        <div class="card">
                            <div class="card-body pt-0">
                                <div class="tab-content opdtiming">
                                    <div class="tab-pane active" id="tab_a">
                                        <div class="section-main-title">
                                            <h3>About EMCS</h3>
                                        </div>
                                        <!-- About Details -->
                                        <%=strTabA %>

                                        <!-- /About Details -->

                                    </div>
                                    <div class="tab-pane" id="tab_b">
                                        <div class="section-main-title">
                                            <h3>Facility In EMCS</h3>
                                        </div>
                                        <!-- About Details -->

                                        <%=FacilityInEMCS%>
                                        <!-- /About Details -->

                                    </div>
                                    <div class="tab-pane" id="tab_c">
                                        <div class="section-main-title">
                                            <h3>Statistics</h3>
                                        </div>
                                       <%=strTabC %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="sidebar">
                            <div class="card widget-categories">
                                <div class="card-header">
                                    <h4 class="card-title">EMCS</h4>
                                </div>
                                <div class="card-body">
                                    <ul class="categories nav nav-pills nav-stacked flex-column">
                                        <li><a href="#tab_a" class="active" data-toggle="pill">About EMCS</a></li>
                                        <li><a href="#tab_b" data-toggle="pill">Facility In EMCS</a></li>
                                        <li><a href="#tab_c" data-toggle="pill">Statistics</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /Doctor Details Tab -->
                </div>
            </div>
        </div>
        <!-- End About Section -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script src="<%=ResolveUrl("~/Scripts/canvasjs.min.js") %>"></script>
    <script>
        window.onload = function () {    <%=strScript%> }
    </script>
</asp:Content>
