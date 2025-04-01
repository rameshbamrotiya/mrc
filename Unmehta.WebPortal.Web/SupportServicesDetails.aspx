<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupportServicesDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.SupportServicesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Support Services Details
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
                <li>Support Services</li>
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
                                <%=strDescription %>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">Support Services</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories nav nav-pills nav-stacked flex-column">
                                    <%=strRightsSideTabs %>


                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
