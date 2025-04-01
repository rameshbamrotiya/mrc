<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StarOfHospital.aspx.cs" Inherits="Unmehta.WebPortal.Web.StarOfHospital" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=strTitle %>
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
                <li> <%=strTitle %></li>
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
                                    <div class="tab-pane active" id="tab_a">
                                        <div class="widget about-widget">
                                            <div class="accordion-box">
                                                <div class="title-box">
                                                    <h6><%=strAccordWeekTab %></h6>
                                                </div>
                                                <ul class="accordion-inner">
                                                    <%=strWeek %>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_b">
                                        <div class="widget about-widget">
                                            <div class="accordion-box">
                                                <div class="title-box">
                                                    <h6><%=strAccordMonthTab %></h6>
                                                </div>
                                                <ul class="accordion-inner">
                                                   
                                                    <%=strMonth %>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="sidebar">
                            <div class="card widget-categories">
                                <div class="card-header">
                                    <h4 class="card-title"><%=strTitle %></h4>
                                </div>
                                <div class="card-body">
                                    <ul class="categories nav nav-pills nav-stacked flex-column">
                                        <li>
                                            <a href="#tab_a" class="active" data-toggle="pill"><%=strWeekTab %>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab_b" data-toggle="pill"><%=strMonthTab %></a>
                                        </li>
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
