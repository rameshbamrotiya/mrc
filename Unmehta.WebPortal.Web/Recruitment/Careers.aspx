<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="Careers.aspx.cs" Inherits="Unmehta.WebPortal.Web.Careers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Advertisement Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Apply Now</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Apply Now</li>
            </ul>

        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <div class="content-details-area pt-50 pb-50">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="section-main-title">
                                    <h2>Career</h2>
                                </div>
                                <div class="content_newdata">
                                    <section class="features-area  pb-70">
                                        <div class="container">
                                            <div class="row align-items-center">
                                                <%=strJobDetails %>
                                                <%--<div class="col-sm-6 col-lg-4">
                                        <div class="features-inner">
                                            <ul class="align-items-center">
                                                <li>
                                                    <i class="flaticon-strategy-in-a-labyrinth"></i>
                                                </li>
                                                <li>
                                                    <h3>Post 1 (dgm)</h3>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="appbtnnow">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <a class="common-btn btn-block" href="#">
                                                        View More</a></div>
                                                <div class="col-lg-6"><a class="common-btn btn-block" href="#">
                                                        Apply Now

                                                    </a></div>
                                            </div>
                                        </div>
                                    </div>--%>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
