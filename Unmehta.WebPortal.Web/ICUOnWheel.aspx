<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ICUOnWheel.aspx.cs" Inherits="Unmehta.WebPortal.Web.ICUOnWheel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    ICU on Wheels
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
                <li><a href="#">ICU On Wheels</a></li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="container">
            <section class="bg-no-repeat bg-img-right" data-tm-bg-img="<%=ResolveUrl("~/Hospital/assets/img/map.png") %>" style="background-image: url(<%=ResolveUrl("~/Hospital/assets/img/map.png") %>);">
                <div class="container pt-lg-90 pb-60 pb-lg-30">
                    <div class="section-content mb-100 mb-md-0">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="tm-sc tm-sc-animated-layer-advanced pt-0 mb-md-100 mb-sm-80">
                                    <div class="animated-layer-advanced-inner clearfix">
                                        <div class="tm-about-box">
                                            <div class="layer-image-wrapper tm-animation move-up z-index-1 text-center">
                                                <div id="carouselExampleSlidesOnly" class="carousel slide" data-ride="carousel">
                                                    <div class="carousel-inner">
                                                        <%=strPageSlider %>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <%=strPageMainDesc %>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <div class="row">
                <!-- Doctor Details Tab -->
                <div class="col-lg-9">

                    <div class="">
                        <div class="pt-0">
                            <div class="tab-content opdtiming">
                                <%=strListOfSubSectionDescription %>
                               <%-- <div class="tab-pane active" id="tab_a">
                                    <!-- About Details -->
                                    <div class="accordion-box">
                                        <div class="title-box">
                                            <h6>All ICU on Wheels
                                                    Equipment’s (in-built)</h6>
                                        </div>
                                        <ul class="accordion-inner">
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Multi-para monitor</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <div class="about-author">
                                                                <div class="author-img-wrap">
                                                                    <a href="#">
                                                                        <img class="img-fluid" alt="" src="assets/img/features/feature-01.jpg"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="wpb_wrapper">
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Total number:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>35</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Indication:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    Intra Aortic Balloon Pump (IABP)
                                                                                        is used in cardiogenic shock to
                                                                                        decrease
                                                                                        myocardial Oxygen demand and
                                                                                        improve cardiac output.

                                                                                        Advantage: It consists of a
                                                                                        cylindrical polyurethane balloon
                                                                                        which sits
                                                                                        in the aorta and
                                                                                        inflates and deflates via
                                                                                        counter pulsations.
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Speciality:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    True one button start-up with
                                                                                        automatic calibration.

                                                                                        Automatically evaluates and
                                                                                        selects the best lead and
                                                                                        trigger
                                                                                        source

                                                                                        Automatically adjusts to changes
                                                                                        in patient conditions without
                                                                                        clinician
                                                                                        intervention
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block active-block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Defibrillator</h6>
                                                </div>
                                                <div class="acc-content current" style="display: none;">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <div class="about-author">
                                                                <div class="author-img-wrap">
                                                                    <a href="#">
                                                                        <img class="img-fluid" alt="" src="assets/img/features/feature-01.jpg"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="wpb_wrapper">
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Total number:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>35</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Indication:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    Intra Aortic Balloon Pump (IABP)
                                                                                        is used in cardiogenic shock to
                                                                                        decrease
                                                                                        myocardial Oxygen demand and
                                                                                        improve cardiac output.

                                                                                        Advantage: It consists of a
                                                                                        cylindrical polyurethane balloon
                                                                                        which sits
                                                                                        in the aorta and
                                                                                        inflates and deflates via
                                                                                        counter pulsations.
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Speciality:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    True one button start-up with
                                                                                        automatic calibration.

                                                                                        Automatically evaluates and
                                                                                        selects the best lead and
                                                                                        trigger
                                                                                        source

                                                                                        Automatically adjusts to changes
                                                                                        in patient conditions without
                                                                                        clinician
                                                                                        intervention
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>SpO2</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Syringe Pumps</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>ECG Machine</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Ventilator (Invasive / Non-Invasive)</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Oxygen Supply</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>

                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Suction</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Nebulizer</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Medicine Storage Rack</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>
                                <div class="tab-pane" id="tab_b">
                                    <div class="accordion-box">
                                        <div class="title-box">
                                            <h6>Advanced Cardiac ICU Equipment’s</h6>
                                        </div>
                                        <ul class="accordion-inner">
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>High quality Medical RO plant for HD – 50 lit/hr</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <div class="about-author">
                                                                <div class="author-img-wrap">
                                                                    <a href="#">
                                                                        <img class="img-fluid" alt="" src="assets/img/features/feature-01.jpg"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="wpb_wrapper">
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Total number:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>35</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Indication:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    Intra Aortic Balloon Pump (IABP)
                                                                                        is used in cardiogenic shock to
                                                                                        decrease
                                                                                        myocardial Oxygen demand and
                                                                                        improve cardiac output.

                                                                                        Advantage: It consists of a
                                                                                        cylindrical polyurethane balloon
                                                                                        which sits
                                                                                        in the aorta and
                                                                                        inflates and deflates via
                                                                                        counter pulsations.
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Speciality:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    True one button start-up with
                                                                                        automatic calibration.

                                                                                        Automatically evaluates and
                                                                                        selects the best lead and
                                                                                        trigger
                                                                                        source

                                                                                        Automatically adjusts to changes
                                                                                        in patient conditions without
                                                                                        clinician
                                                                                        intervention
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block active-block">
                                                <div class="acc-btn active">
                                                    <div class="icon-outer"></div>
                                                    <h6>Arterial Blood Gas Analysis (ABG)</h6>
                                                </div>
                                                <div class="acc-content current">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <div class="about-author">
                                                                <div class="author-img-wrap">
                                                                    <a href="#">
                                                                        <img class="img-fluid" alt="" src="assets/img/features/feature-01.jpg"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="wpb_wrapper">
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Total number:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>35</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Indication:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    Intra Aortic Balloon Pump (IABP)
                                                                                        is used in cardiogenic shock to
                                                                                        decrease
                                                                                        myocardial Oxygen demand and
                                                                                        improve cardiac output.

                                                                                        Advantage: It consists of a
                                                                                        cylindrical polyurethane balloon
                                                                                        which sits
                                                                                        in the aorta and
                                                                                        inflates and deflates via
                                                                                        counter pulsations.
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="wpb_text_column wpb_content_element ">
                                                                            <h4>Speciality:</h4>
                                                                            <div class="wpb_wrapper">
                                                                                <p>
                                                                                    True one button start-up with
                                                                                        automatic calibration.

                                                                                        Automatically evaluates and
                                                                                        selects the best lead and
                                                                                        trigger
                                                                                        source

                                                                                        Automatically adjusts to changes
                                                                                        in patient conditions without
                                                                                        clinician
                                                                                        intervention
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>High Energy Cautery</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Ceiling mount Lights High Flow</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="accordion block">
                                                <div class="acc-btn">
                                                    <div class="icon-outer"></div>
                                                    <h6>Oxygen delivery System</h6>
                                                </div>
                                                <div class="acc-content">
                                                    <div class="text">
                                                        <p>
                                                            Phasellus hendrerit. Pellentesque aliquet nibh nec urna.
                                                                In nisi neque,
                                                                aliquet vel, dapibus id mattis vel
                                                                nisi.
                                                        </p>
                                                    </div>
                                                </div>
                                            </li>

                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>
                                <div class="tab-pane" id="tab_c">
                                    <div class="section-main-title">
                                        <h3>Advance Cardiac ICU Equipment’s facilities (if required)</h3>
                                    </div>
                                    <!-- About Details -->
                                    <div class="widget about-widget">
                                        <ul class="format-list">
                                            <li>Temporary Pacemaker (TPI)</li>
                                            <li>Intra-Aortic Balloon Pump (IABP)</li>
                                            <li>Extra Corporeal Membrane Oxygenator (ECMO)</li>
                                            <li>Haemo Dialysis (HD)</li>
                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>
                                <div class="tab-pane" id="tab_d">
                                    <div class="section-main-title">
                                        <h3>Advanced Cardiac ICU facilities</h3>
                                    </div>
                                    <!-- About Details -->
                                    <div class="widget about-widget">
                                        <ul class="format-list">
                                            <li>Refrigerator</li>
                                            <li>Blood Storage Unit</li>
                                            <li>Surgical Equipment’s with Storage rack</li>
                                            <li>Scrub facility – Scrub station</li>
                                            <li>ICU Bed – movable</li>
                                            <li>Hydraulic Bed Lift – platform</li>
                                            <li>Doctor’s Work station</li>
                                            <li>Telemedicine Port with Live Data Transfer</li>
                                            <li>Emergency Exit with facility to take out stretcher</li>
                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>
                                <div class="tab-pane" id="tab_e">
                                    <div class="section-main-title">
                                        <h3>Special Features</h3>
                                    </div>
                                    <!-- About Details -->
                                    <div class="widget about-widget">
                                        <ul class="format-list">
                                            <li>Modular ICU walls and Medical Grade Antibacterial Flooring</li>
                                            <li>Electric Power Generator – 7.5 KV</li>
                                            <li>Solar roof top panels – 2.5 KV</li>
                                            <li>Hydraulic Lift – 2000 Kg – for patient bed oxygen and Equipments
                                            </li>
                                            <li>Oxygen Cylinder – external loading with Manifold attachments</li>
                                            <li>Noise less Compressor for central Suction</li>
                                            <li>Medical Grade Air purifying unit</li>
                                            <li>Air conditioning – 40 KV</li>
                                            <li>Water storage tank 450 lit. and waste water collection area</li>
                                            <li>Resting area for Medical Team with Pantry and W/C facility</li>
                                            <li>Facility for relatives and other staff to seat</li>
                                            <li>Wi-Fi connectivity through High speed Router</li>
                                            <li>CC Camera – 8 for inside and external area monitoring</li>
                                            <li>Tubeless Tyres with compressor to refill air in Tyres</li>
                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>
                                <div class="tab-pane" id="tab_f">
                                    <div class="section-main-title">
                                        <h3>Facility that can be created – Need
                                                based speciality</h3>
                                    </div>
                                    <!-- About Details -->
                                    <div class="widget about-widget">
                                        <ul class="format-list">
                                            <li>Neonatal shifting</li>
                                            <li>Operation Theatre – while stationed in a place
                                                    <ul class="format_list_one">
                                                        <li>OT Table – replacing ICU Bed</li>
                                                        <li>Anaesthesia apparatus</li>
                                                        <li>Endoscopy facility with Endoscopy Trolley</li>
                                                        <li>Required additional Equipments for surgery</li>
                                                    </ul>
                                            </li>

                                        </ul>
                                    </div>
                                    <!-- /About Details -->
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">ICU on Wheels</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories nav nav-pills nav-stacked flex-column">
                                    <%=strListOfSubSection %>                                    
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
