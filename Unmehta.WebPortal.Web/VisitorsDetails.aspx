<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisitorsDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.VisitorsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Visitors
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .visitor-list .visitor-item {
            -webkit-box-shadow: none;
            box-shadow: none;
            margin-bottom: 30px;
            text-align: center;
            background-color: transparent;
        }
            .visitor-list .visitor-item .visitor_icon {
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
                -webkit-box-pack: center;
                -ms-flex-pack: center;
                justify-content: center;
                width: 90px;
                height: 90px;
                font-size: 40px;
                border-radius: 50%;
                margin-bottom: 10px;
                color: #213360;
                background-color: #dddddd;
                -webkit-transition: all 0.3s linear;
                transition: all 0.3s linear;
                display: inline-flex;
            }

            .visitor-list .visitor-item .visitor_title {
                font-size: 15px;
            }

            .visitor-list .visitor-item:hover .visitor_icon {
                -webkit-box-shadow: 0 0 0 9px rgba(0, 0, 0, 0.1);
                box-shadow: 0 0 0 9px rgba(0, 0, 0, 0.1);
                -webkit-transform: translateY(-7px);
                transform: translateY(-7px);
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li><a href="#">Visitors</a></li>
            </ul>
        </div>
    </div>
    <!-- Page Content -->
    <!-- About Section -->
    <div class="content">
        <div class="container">
            <div class="row">
                <%=strVisitorsMain%>
                <!-- Doctor Details Tab -->
             <%--   <div class="col-lg-9">
                    <div class="card">
                        <div class="card-body ourdept">
                            <!-- Tab Menu -->
                            <nav class="user-tabs mb-4">
                                <ul class="nav nav-tabs nav-tabs-bottom">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#doc_overview" data-toggle="tab">Facilities</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#doc_locations" data-toggle="tab">Visiting Hours</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#doc_reviews" data-toggle="tab">Do’s & Don’ts</a>
                                    </li>
                                </ul>
                            </nav>
                            <!-- /Tab Menu -->
                            <!-- Tab Content -->
                            <div class="tab-content pt-0">
                                <!-- Overview Content -->
                                <div role="tabpanel" id="doc_overview" class="tab-pane fade active show">
                                    <div class="visitor-list list-unstyled mb-0 row flex-wrap justify-content-between">
                                        <%=strListOfImages %>
                                    </div>
                                </div>
                                <!-- /Overview Content -->

                                <!-- Locations Content -->
                                <div role="tabpanel" id="doc_locations" class="tab-pane fade">

                                    <div class="widget business-widget">
                                        <div class="widget-content">
                                            <div class="listing-hours">
                                                <%=strListOfVisitingHoursDesc %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /Locations Content -->
                                <!-- Reviews Content -->
                                <div role="tabpanel" id="doc_reviews" class="tab-pane fade">
                                    <div class="course-overview">
                                        <div class="inner-box">
                                            <%=strListOfDDDescription %>
                                        </div>
                                    </div>
                                </div>
                                <!-- /Reviews Content -->
                            </div>
                        </div>
                    </div>
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
                </div>--%>
                <!-- /Doctor Details Tab -->
            </div>
        </div>
    </div>
    <!-- End About Section -->
    <!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <%=strListOfImagespopup %>
</asp:Content>
