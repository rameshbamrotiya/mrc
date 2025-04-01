<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaroftheUnmicrc.aspx.cs" Inherits="Unmehta.WebPortal.Web.StaroftheUnmicrc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
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
                <li>Career</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->
    <!-- Page Content -->
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
                                        <div class="widget about-widget">
                                            <div class="accordion-box">
                                                <div class="title-box">
                                                    <h6>Star of the Week 12-Nov-2021</h6>
                                                </div>
                                                <ul class="accordion-inner">
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>Saturday</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">

                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/star_ofthe_month_1.jpeg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/star_ofthe_month_1.jpeg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>Monday</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>Tuesday</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>Saturday</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_b">
                                        <div class="widget about-widget">
                                            <div class="accordion-box">
                                                <div class="title-box">
                                                    <h6>Star of the Month</h6>
                                                </div>
                                                <ul class="accordion-inner">
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>02-08-2021</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>05-09-2021</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>10-10-2021</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>05-11-2021</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li class="accordion block">
                                                        <div class="acc-btn">
                                                            <div class="icon-outer"></div>
                                                            <h6>06-12-2021</h6>
                                                        </div>
                                                        <div class="acc-content">
                                                            <div class="row">
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/D-8.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/D-8.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Hiren Vyas
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-lg-6">
                                                                    <div class="gallery-box-layout1">
                                                                        <img src="assets/img/hospital/02_Home-Page.jpg"
                                                                            alt="Feature" class="img-fluid">
                                                                        <div class="item-icon">
                                                                            <a href="assets/img/hospital/02_Home-Page.jpg"
                                                                                data-fancybox="gallery"
                                                                                class="lightbox-image">
                                                                                <i class="fas fa-search-plus"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="item-content">
                                                                            <h3 class="item-title">Devang Patel
                                                                            </h3>
                                                                            <span class="title-ctg">IT Depat</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
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
                                    <h4 class="card-title">Star of UNMICRC</h4>
                                </div>
                                <div class="card-body">
                                    <ul class="categories nav nav-pills nav-stacked flex-column">
                                        <li>
                                            <a href="#tab_a" class="active" data-toggle="pill">Star of the Week
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab_b" data-toggle="pill">Star of Month</a>
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
        <!-- End About Section -->
        <!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
