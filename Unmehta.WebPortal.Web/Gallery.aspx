<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="Unmehta.WebPortal.Web.Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Photo Gallery
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .schedule-nav .nav-tabs li a.active {
            background: #ff4877;
            border: 1px solid #ff4877 !important;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li><a href="<%=ResolveUrl("~/Gallery") %>">Gallery</a></li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="container">
            <div class="row">
                <!-- Doctor Details Tab -->
                <div class="col-lg-9">
                    <div class="tab-content opdtiming">
                        <div class="tab-pane active" id="tab_a">
                            <div class="section-main-title">
                                <h3>Photo Gallery</h3>
                            </div>
                            <!-- Gallery Area Start Here -->
                            <div class="row">
                                <div class="col-md-12 text-center mb-20  ltn__gallery-filter-menu schedule-nav">
                                    <%--<ul class="nav nav-tabs nav-justified">
                                            <li class="nav-item">
                                                <a class="active nav-link btn-outline-success filter-button"
                                                    data-toggle="tab" data-filter="all" href="#slot_sunday">See All</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link filter-button btn-outline-success" data-toggle="tab"
                                                    data-filter="Hospital" href="#slot_monday">BY
                                                    alphabet</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link btn-outline-success" data-toggle="tab"
                                                    data-filter="CME" href="#slot_tuesday">By Act No</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link btn-outline-success" data-filter="Otherevent"
                                                    data-toggle="tab" href="#slot_wednesday">By Free
                                                    Text</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link btn-outline-success" data-filter="Heart"
                                                    data-toggle="tab" href="#slot_thursday">By
                                                    Keyword</a>
                                            </li>
                                        </ul> --%>
                                    <a class="btn btn-outline-success filter-button  active" data-filter="all">See
                                            All</a>

                                    <%=strMainTagList %>
                                    <%-- <a class="btn btn-outline-success filter-button" data-filter="Hospital">Hospital</a>
                                        <a class="btn btn-outline-success filter-button" data-filter="CME">CME</a>
                                        <a class="btn btn-outline-success filter-button" data-filter="Seminar">Seminar</a>
                                        <a class="btn btn-outline-success filter-button" data-filter="Heart">Heart
                                            Day</a>
                                        <a class="btn btn-outline-success filter-button" data-filter="Workshop">Workshop</a>
                                        <a class="btn btn-outline-success filter-button" data-filter="Otherevent">Other
                                            Event</a>--%>
                                </div>
                            </div>
                            <div class="row mt-4  ltn__gallery-style-1">

                                <%=strListOfSubSectionDescription %>
                                <%-- <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6  filter Hospital">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1157.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- gallery-item -->
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6 filter CME Hospital">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1157.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6 filter Workshop">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1157.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- gallery-item -->
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6 filter Hospital">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1157.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>--%>
                                <!-- gallery-item -->
                            </div>
                            <!-- Gallery Area End Here -->
                        </div>
                        <div class="tab-pane" id="tab_b">
                            <div class="section-main-title">
                                <h3>Video Gallery</h3>
                            </div>
                            <!-- Gallery Area Start Here -->
                            <div class="row">
                                <div class="col-md-12 text-center mb-20 ltn__gallery-filter-menu">
                                    <a class="btn btn-outline-success filter-button active" data-filter="all">See
                                            All</a>
                                    <%=strVideoMainTagList %>
                                    <%--<button class="btn btn-outline-success filter-button" data-filter="Hospital">Acadmic</button>
                                        <button class="btn btn-outline-success filter-button" data-filter="CME">Hospital</button>--%>
                                </div>
                            </div>
                            <div class="row mt-4  ltn__gallery-style-1">
                                <!-- gallery-item -->

                                <%=strVideoListOfSubSectionDescription %>
                                <%-- <div class="ltn__gallery-item filter_category_3 col-md-4 col-sm-6 col-12">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1339.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">Portfolio Link </a></h4>
                                                <p>Web Design &amp; Development, Branding</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- gallery-item -->
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6  filter CME">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1339.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6  filter Workshop">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1339.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- gallery-item -->
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6 filter Seminar">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1339.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- gallery-item -->
                                    <div class="ltn__gallery-item filter_category_3 col-lg-4 col-sm-6 filter Hospital">
                                        <div class="ltn__gallery-item-inner">
                                            <div class="ltn__gallery-item-img">
                                                <a href="gallery_album.html">
                                                    <img src="assets/img/hospital/1339.jpg" alt="Image" class="img-fluid">
                                                    <span class="ltn__gallery-action-icon">
                                                        <p>View Album</p>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="ltn__gallery-item-info">
                                                <h4><a href="gallery_album.html">WORLD HEART DAY - 2019 UNMICRC</a>
                                                </h4>
                                                <p>Cardiology</p>
                                            </div>
                                        </div>
                                    </div>--%>
                                <!-- gallery-item -->
                            </div>
                            <!-- Gallery Area End Here -->
                        </div>

                    </div>
                    <!-- Gallery area start -->
                </div>
                <div class="col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">Gallery</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories nav nav-pills nav-stacked flex-column">
                                    <li>
                                        <a href="#tab_a" class="active" data-toggle="pill">Photo
                                                Gallery
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#tab_b" data-toggle="pill">Video Gallery</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End About Section -->
        </div>
        <!-- End Go Top -->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script>
        $(document).ready(function () {
            $(".filter-button").click(function () {
                var value = $(this).attr('data-filter');

                if (value == "all") {
                    //$('.filter').removeClass('hidden');
                    $('.filter').show('1000');
                }
                else {
                    //            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
                    //            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
                    $(".filter").not('.' + value).hide('3000');
                    $('.filter').filter('.' + value).show('3000');

                }
            });

            // if ($(".filter-button").removeClass("active")) {
            //     $(this).removeClass("active");
            // }
            // $(this).addClass("active");

        });
        /* --------------------------------------------------------
              13. Isotope Gallery Active  ( Gallery / Portfolio )
          -------------------------------------------------------- */
        var $ltnGalleryActive = $('.ltn__gallery-active'),
            $ltnGalleryFilterMenu = $('.ltn__gallery-filter-menu');
        /*Filter*/
        $ltnGalleryFilterMenu.on('click', 'button, a', function () {
            var $this = $(this),
                $filterValue = $this.attr('data-filter');
            $ltnGalleryFilterMenu.find('button, a').removeClass('active');
            $this.addClass('active');
            // $ltnGalleryActive.isotope({ filter: $filterValue });
        });
        /*Grid*/
        $ltnGalleryActive.each(function () {
            var $this = $(this),
                $galleryFilterItem = '.ltn__gallery-item';
            $this.imagesLoaded(function () {
                $this.isotope({
                    itemSelector: $galleryFilterItem,
                    percentPosition: true,
                    masonry: {
                        columnWidth: '.ltn__gallery-sizer',
                    }
                });
            });
        });



    </script>
</asp:Content>
