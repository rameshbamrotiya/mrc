<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="Unmehta.WebPortal.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	U. N.  Mehta Institute of Cardiology & Research Centre
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<link rel="stylesheet" href="<%= ResolveUrl("~/Hospital/assets/css/ChatBox.css")%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

	<!--Start rev slider wrapper-->
	<section class="rev_slider_wrapper">
		<div id="slider1" class="rev_slider" data-version="5.0">
			<ul id="slider" runat="server">
				<%--<li data-transition="rs-20">
						<img src="<%= ResolveUrl("~/Hospital/assets/img/slider/slider-2.JPG")%>" alt="" width="1920" height="500" data-bgposition="top center"
							data-bgfit="cover" data-bgrepeat="no-repeat" data-bgparallax="1">

						<div class="tp-caption  tp-resizeme" data-x="left" data-hoffset="0" data-y="top" data-voffset="220"
							data-transform_idle="o:1;"
							data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;"
							data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;"
							data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-splitin="none" data-splitout="none"
							data-responsive_offset="on" data-start="1500">
							<div class="slide-content-box mar-lft">
								<h1>Hospitals providing total<br> healthcare <span>Solutions.</span></h1>
								<p>Denouncing pleasure and praising pain was born and we will <br>give you a complete
									account of the system.</p>
								<div class="button">
									<a class="#" href="#">Read More</a>
									<a class="btn-style-two" href="#">Departments</a>
								</div>
							</div>
						</div>

					</li>--%>
			</ul>
		</div>
	</section>
	<!--End rev slider wrapper-->

	<!-- start Emergency sec -->
	<section class="contact-info py-0">
		<div class="container">
			<div class="row  boxes-wrapper">
				<div class="col-sm-12 col-md-3">
					<a href="<%= ResolveUrl("~/AdmissionDetails")%>">
						<div class="contact-box">
							<div class="contact__icon">
								<i class="fas fa-graduation-cap"></i>
							</div>
							<div class="contact__content">
								<h2 class="contact__title">Admission</h2>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-12 col-md-3">
					<a href="<%= ResolveUrl("~/OPDetails")%>">
						<div class="contact-box">
							<div class="contact__icon">
								<i class="far fa-clock"></i>
							</div>
							<div class="contact__content">
								<h2 class="contact__title">OPD Timings</h2>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-12 col-md-3">
					<a href="<%= ResolveUrl("~/InfoMSRClause")%>">
						<div class="contact-box">
							<div class="contact__icon">
								<i class="fas fa-procedures"></i>
							</div>
							<div class="contact__content">
								<h2 class="contact__title">Info MSR Clause</h2>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-12 col-md-3">
					<a href="<%= ResolveUrl("~/GovernmentApproval")%>">
						<div class="contact-box">
							<div class="contact__icon">
								<i class="fas fa-balance-scale"></i>
							</div>
							<div class="contact__content">
								<h2 class="contact__title">Government Approvals</h2>
							</div>
						</div>
					</a>
				</div>
			</div>
		</div>
	</section>
	<!-- end Emergency sec -->
	<!-- Section: About -->

	<asp:Repeater ID="dtMiddleSection" runat="server">
		<ItemTemplate>
			<section class="watch-video">
				<div class="video-main">
					<div class="layer-image-three">
						<div class="icon bg-theme-colored1">
							<img src="<%= ResolveUrl("~/Hospital/assets/img/heartbit.gif")%>" style="width: 90px;" alt="Image">
						</div>
						<div class="layer-shape-round"></div>
					</div>
					<div class="bgvideo"></div>
					<div class="container">
						<div class="row">
							<div class="col-lg-6  col-md-6 col-sm-6">
								<div class="sec-title pb-10 video-sec1">
									<%-- <h2 class="mb-0">"Story of 1251 bedded India's
								<br>
										Biggest Single Specialty Hospital"</h2>--%>
									<h2 class="mb-0"><%# Eval("LeftVideoTitle").ToString() %></h2>
								</div>

								<div class="video-bg-img video-sec text-center">
									<a 
										href="<%# ((Eval("link_Video_PathLeft").ToString()=="True"?ResolveUrl(Eval("LeftVideoURL").ToString()):Eval("LeftVideoURL").ToString()) ) %>" data-fancybox="groupleft_gallery"
										class="lightbox-image">
										<img src="<%# ResolveUrl( Eval("LeftImage").ToString()) %>" alt="Video">
										<span class="play-btn">
											<i class="fas fa-play"></i>
										</span></a>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>


								<%--                                <div class="video-bg-img video-sec text-center" data-video-id='<%# ( Eval("LeftVideoURL").ToString()) %>' data-video-format="youtube">
									<img src='<%# "https://img.youtube.com/vi/"+Eval("LeftVideoURL").ToString()+"" %>'   alt="Video" >
									<span class="play-btn">
										<i class="fas fa-play"></i>
									</span>
								</div>--%>
								<div class="link-btn text-center mb-20">
									<a href='<%# Page.ResolveUrl( Eval("LeftVideoReadMore").ToString()) %>' class="btn btn-primary nextBtn"><span>Read More<i class="icon-arrow-right"></i></span></a>
								</div>
							</div>
							<div class="col-lg-6 col-md-6 col-sm-6">
								<div class="sec-title pb-10 video-sec1">
									<%--<h2 class="mb-0">"Emergency Care Intensive Cardiac Care Unit
								<br>
										Anytime... Any where.." </h2>--%>
									<h2 class="mb-0"><%# Eval("RightVideoTitle").ToString() %></h2>
								</div>

								<div class="video-bg-img video-sec text-center">
									<a 
										href="<%# ((Eval("link_Video_PathRight").ToString()=="True"?ResolveUrl(Eval("RightVideoURL").ToString()):Eval("RightVideoURL").ToString()) ) %>" data-fancybox="groupright_gallery"
										class="lightbox-image">
										<img src="<%# ResolveUrl( Eval("RightImage").ToString()) %>" alt="Video">
										<span class="play-btn">
											<i class="fas fa-play"></i>
										</span></a>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
								<%--     <div class="video-bg-img video-sec text-center" data-video-id='<%# ( Eval("RightVideoURL").ToString()) %>' data-video-format="youtube">
								  <img src='<%# "https://img.youtube.com/vi/"+Eval("RightVideoURL").ToString()+"" %>'   alt="Video" >
									<span class="play-btn">
										<i class="fas fa-play"></i>
									</span>
								</div>--%>
								<div class="link-btn text-center mb-20">
									<a href='<%# Page.ResolveUrl( Eval("RightVideoReadMore").ToString()) %>' class="btn btn-primary nextBtn"><span>Read More<i class="icon-arrow-right"></i></span></a>
								</div>
							</div>
						</div>

					</div>
				</div>
			</section>
			<section class="funfacts-section" style="background-image: url(<%=ResolveUrl("~/Hospital/assets/img/bg-1.jpg")%>);">
				<%--<div class="bgyeartwo">
					<p class="bg_text_two">2020</p>
				</div>--%>
				<div class="container">
					<div class="row counts d-flex  justify-content-center">
						<!--Column-->

						<div class="column counter-column col-lg-2 col-md-6">
							<div class="inner">
								<!-- <div class="count">20</div> -->
								<div class="icon-outer">
									<div class="shape-one">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-39.png")%>" alt="">
									</div>
									<div class="shape-two">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-38.png")%>" alt="">
									</div>
									<div class="icon">
										<img src="<%# ResolveUrl(Eval("OPDImageIcon").ToString())%>" alt="">
									</div>
									<div class="hover-icon">
										<img src="<%# ResolveUrl(Eval("OPDImageIcon").ToString())%>" alt="">
									</div>
								</div>
								<div class="content1">
									<div class="count-outer count-box counted">
										<div><span class="counter count-text animated fadeInDownBig"><%#Eval("OpdDay")%></span>+</div>
									</div>
									<div class="text">OPD</div>
								</div>
							</div>
						</div>
						<!--Column-->
						<div class="column counter-column col-lg-2 col-md-6">
							<div class="inner">
								<!-- <div class="count">2020</div> -->
								<div class="icon-outer">
									<div class="shape-one">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-39.png")%>" alt="">
									</div>
									<div class="shape-two">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-38.png")%>" alt="">
									</div>
									<div class="icon">
										<img src="<%# ResolveUrl(Eval("IPDImageIcon").ToString())%>" alt="">
									</div>
									<div class="hover-icon">
										<img src="<%# ResolveUrl(Eval("IPDImageIcon").ToString())%>" alt="">
									</div>
								</div>
								<div class="content1">
									<div class="count-outer count-box counted">
										<div><span class="counter animated fadeInDownBig"><%#Eval("IpdDay")%></span>+</div>
									</div>
									<div class="text">IPD</div>
								</div>
							</div>
						</div>
						<!--Column-->
						<!--Column-->
						<div class="column counter-column col-lg-2 col-md-6">
							<div class="inner">
								<!-- <div class="count">2020</div> -->
								<div class="icon-outer">
									<div class="shape-one">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-39.png")%>" alt="">
									</div>
									<div class="shape-two">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-38.png")%>" alt="">
									</div>
									<div class="icon">
										<img src="<%# ResolveUrl(Eval("SurgeryImageIcon").ToString())%>" alt="">
									</div>
									<div class="hover-icon">
										<img src="<%# ResolveUrl(Eval("SurgeryImageIcon").ToString())%>" alt="">
									</div>
								</div>
								<div class="content1">
									<div class="count-outer count-box counted">
										<div class="timer count-title count-number" data-to="100" data-speed="1500"></div>
										<div><span class="counter animated fadeInDownBig"><%#Eval("SurgeryDay")%></span>+</div>
									</div>
									<div class="text">Surgery</div>
								</div>
							</div>
						</div>
						<!--Column-->
						<!--Column-->
						<div class="column counter-column col-lg-2 col-md-6">
							<div class="inner">
								<!-- <div class="count">2020</div> -->
								<div class="icon-outer">
									<div class="shape-one">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-39.png")%>" alt="">
									</div>
									<div class="shape-two">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-38.png")%>" alt="">
									</div>
									<div class="icon">
										<img src="<%# ResolveUrl(Eval("ProceduresImageIcon").ToString())%>" alt="">
									</div>
									<div class="hover-icon">
										<img src="<%# ResolveUrl(Eval("ProceduresImageIcon").ToString())%>" alt="">
									</div>
								</div>
								<div class="content1">

									<div class="count-outer count-box counted">
										<div class="timer count-title count-number" data-to="100" data-speed="1500"></div>
										<div><span class="counter animated fadeInDownBig"><%#Eval("ProceduresDay")%></span>+</div>
									</div>
									<div class="text">Procedures</div>
								</div>
							</div>
						</div>
						<!--Column-->
						<div class="column counter-column col-lg-2 col-md-6">
							<div class="inner">
								<div class="icon-outer">
									<div class="shape-one">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-39.png")%>" alt="">
									</div>
									<div class="shape-two">
										<img src="<%= ResolveUrl("~/Hospital/assets/img/shape-38.png")%>" alt="">
									</div>
									<div class="icon">
										<img src="<%# ResolveUrl(Eval("InvestigationsImageIcon").ToString())%>" alt="">
									</div>
									<div class="hover-icon">
										<img src="<%# ResolveUrl(Eval("InvestigationsImageIcon").ToString())%>" alt="">
									</div>
								</div>
								<div class="content1">
									<div class="count-outer count-box counted">
										<div class="timer count-title count-number" data-to="100" data-speed="1500"></div>
										<div><span class="counter animated fadeInDownBig"><%#Eval("InvestigationsDay")%></span>+</div>
									</div>
									<div class="text">Investigations</div>
								</div>
							</div>
						</div>

					</div>
				</div>
			</section>
		</ItemTemplate>
	</asp:Repeater>
	<%-- <!-- Clinic and Specialities -->
	<section class="section section-specialities">
		<div id="rs-about" class="rs-about style1  md-70">
			<div class="container">
				<div class="row">
					<div class="col-lg-6 ">
						<div class="about-img  wow slideInRight animated" data-wow-delay="00ms" data-wow-duration="1500ms">
							<div class="sec-title mb-40 text-center">
								<h2 class="title">Unmehta Journey</h2>
								<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
							</div>
							<img src="<%= ResolveUrl("~/Hospital/assets/img/03.jpg")%>" alt="About" class="img-fluid">
							<div class="years">
								<div class="video-wrap">
									<button class="js-modal-btn" data-video-id="lqgwMVw5l24">
										<i class="fas fa-play-circle"></i>
									</button>
								</div>
							</div>
						</div>
					</div>
					<div class="col-lg-6">
						<div class="testimonials-area wow slideInLeft animated" data-wow-delay="00ms" data-wow-duration="1500ms">
							<div class="about-part">
								<div class="sec-title mb-40 text-center">
									<h2 class="title">Awards and Achievements</h2>
									<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
								</div>
							</div>
							<div class="testimonials-slider owl-theme owl-carousel owl-loaded owl-drag">
								<div class="owl-stage-outer">
									<div class="owl-stage">
										<%--<div class="owl-item">
												<div class="tm-sc-blog tm-mediku tm-sc-blog-masonry blog-style1-current-theme">
													<article class="post type-post status-publish format-standard has-post-thumbnail">
														<div class="entry-header">
															<div class="post-thumb_award lightgallery-lightbox">
																<div class="post-thumb-inner">
																	<div class="thumb border-radius-0">
																		<img class="img-fullwidth" src="<%= ResolveUrl("~/Hospital/assets/img/award/3.jpg")%>" alt="Image">
																		<div
																			class="date bg-theme-colored1 text-white text-center text-uppercase font-size-12 letter-space-1">
																			16 Apr, 2020</div>
																	</div>
																</div>
															</div>
														</div>
														<div class="entry-content">
															<h4 class="entry-title"><a href="news-details.html" rel="bookmark">People who move due to
																	unaffordable</a>
															</h4>
															<p class="mb-0">Lorem ipsum dolor sit amet, consectetur
																notted adipisicing elit sed do eiusmod tempor
																incididunt</p>
															<div class="clearfix"></div>
														</div>
													</article>
												</div>
											</div>--%>
	<%--<%=strAwardsAndAchie %>--%>
	<%--</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
	<!-- Clinic and Specialities -->--%>

	<!--Start Medical Departments area-->
	<section class="medical-departments-area">
		<div class="department-inner">
			<div class="container">
				<div class="upperlab">
					<div class="sec-title pb-10">
						<h2 class="mb-0">Departments</h2>
						<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
					</div>
					<div class="row">
						<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
							<div class='medical-departments-carousel owl-theme owl-carousel owl-nav'>
								<%=strMedical %>
								<!--Start single item-->
								<%--  <div class="single-item text-center">
							<div class="iocn-holder">
								<img src="<%= ResolveUrl("~/Hospital/assets/img/blog/blog-01.JPG")%>" alt="blog-1">
							</div>
							<div class="text-holder">
								<h3>Cardiology<br>
									&nbsp;</h3>
							</div>
							<a class="readmore" href="#">Read More</a>
						</div>--%>
							</div>
							<!--End single item-->
							<%--<!--Start single item-->
							<div class="single-item text-center">
								<div class="iocn-holder">
									<img src="<%= ResolveUrl("~/Hospital/assets/img/blog/blog-02.JPG")%>" alt="blog-2">
								</div>
								<div class="text-holder">
									<h3>Cardio Vascular Thoracic Surgery</h3>
								</div>
								<a class="readmore" href="#">Read More</a>
							</div>
							<!--End single item-->
							<!--Start single item-->
							<div class="single-item text-center">
								<div class="iocn-holder">
									<img src="<%= ResolveUrl("~/Hospital/assets/img/blog/blog-03.JPG")%>" alt="blog-3">
								</div>
								<div class="text-holder">
									<h3>Cardiac <br>Anaesthesia</h3>
								</div>
								<a class="readmore" href="#">Read More</a>
							</div>
							<!--End single item-->
							<!--Start single item-->
							<div class="single-item text-center">
								<div class="iocn-holder">
									<img src="<%= ResolveUrl("~/Hospital/assets/img/blog/blog-04.JPG")%>" alt="blog-4">
								</div>
								<div class="text-holder">
									<h3>Paediatric<br>Cardiology</h3>
								</div>
								<a class="readmore" href="#">Read More</a>
							</div>
							<!--End single item-->
								</div>--%>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
	<!--End Medical Departments area-->
	<!--our scheme-->
	<div class="rs-degree style1 modify gray-bg pt-25 pb-25 md-pt-70 md-pb-40">
		<div class="container">
			<div class="section-header_1 pb-10">
				<div class="headingtitle">
					<h2 class="mb-0">Schemes</h2>
					<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4" />
				</div>
				<div class="load-more text-center mb-0">
					<a class="readon" href="<%= ResolveUrl("~/Schema")%>">View More</a>
				</div>
			</div>
			<div class="row  y-middle">
				<%=strSchema %>
				<%--<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service  wow flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service1.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">EMERGENCY MEDICAL CARDIAC SERVICES (EMCS)</a></h3>
												<p>Lorem ipsum dolor sit amet are consece sed do eiusmod tempor</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>--%>
				<%--<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service wow  flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service2.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">SCHOOL HEALTH CARDIAC
															SCHEME</a>
												</h3>
												<p>
													Lorem ipsum dolor sit amet are consece sed do eiusmod tempor
												</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>
							<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service wow flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service3.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">MUKHYA-MANTRI AMRUTAM (MA)
															YOJNA</a>
												</h3>
												<p>
													Lorem ipsum dolor sit amet are consece sed do eiusmod tempor
												</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>
							<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service wow flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service4.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">MUKHYA-MANTRI AMRUTAM (MA)
															VATSALYA YOJNA</a>
												</h3>
												<p>
													Lorem ipsum dolor sit amet are consece sed do eiusmod tempor
												</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>
							<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service wow flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service5.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">SCHEDULED CASTE (SC)
															CATEGORY</a></h3>
												<p>
													Lorem ipsum dolor sit amet are consece sed do eiusmod tempor
												</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>
							<div class="col-lg-4 col-sm-6">
								<div class="lgx-single-service wow flipInY animated" data-wow-delay="600ms"
									data-wow-duration="1500ms">
									<figure>
										<a class="service-img" href="#">
											<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service1.jpg")%>" alt="Service" /></a>
										<figcaption>
											<div class="link-area">
												<a href="#">
													<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png")%>" alt="link"></a>
											</div>
											<div class="service-info">
												<h3 class="title"><a href="#">SCHEDULED TRIBE (ST)
															CATEGORY</a></h3>
												<p>
													Lorem ipsum dolor sit amet are consece sed do eiusmod tempor
												</p>
												<a class="lgx-btn lgx-btn-white lgx-btn-sm" href="#"><span>Read
															More</span></a>
												<img src="<%= ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png")%>" alt="service icon">
											</div>
										</figcaption>
									</figure>
								</div>
							</div>--%>
			</div>

		</div>
		<!--//.ROW-->
	</div>

	<!--our scheme END-->
	<section class="patient-area  pt-50 pb-50">
		<div class="container">
			<div class="row">
				<div class="col-lg-5">
					<div class="sec-title mb-20">
						<h1 class="title mb-0">Cares</h1>
						<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
					</div>
					<div class=" mb-40">
						<h2 style="font-size: xx-large;">We Provide All Aspects Of Medical Practice For Your Family!</h2>
						<div class="text">
							Providing care that is respectful of and responsive to individual patient preferences,
								needs and values, ensuring that
								patients' values guide all clinical decisions. Timely: Reducing waits and sometimes harmful delays for
								both those who
								receive and provide care.
						</div>
					</div>

				</div>
				<div class="col-lg-7 col-12">

					<div class="row">
						<!-- Feature BLock -->
						<div class="feature-block-two col-lg-6 col-md-6 col-sm-12">
							<a href="<%= ResolveUrl("~/Contribution")%>">
								<div class="inner-box">
									<span class="icon fas fa-hand-holding-usd"></span>
									<h4>Donation</h4>
									<p>You have worked hard for the money that you donated to us. We appreciate it and are thankful.</p>
								</div>
							</a>
						</div>
						<!-- Feature BLock -->
						<div class="feature-block-two col-lg-6 col-md-6 col-sm-12">
							<a href="<%= ResolveUrl("~/HealthTips")%>">
								<div class="inner-box">
									<span class="icon fa fa-ambulance"></span>
									<h4>Health Tips</h4>
									<p>
										<strong>Stay at a healthy weight</strong><br>
										Being overweight can increase your risk of heart
											disease.
									</p>
								</div>
							</a>
						</div>
						<!-- Feature BLock -->
						<div class="feature-block-two col-lg-6 col-md-6 col-sm-12 mt-20">
							<a href="<%= ResolveUrl("~/NursingCare")%>">
								<div class="inner-box">
									<span class="icon fa fa-user-md"></span>
									<h4>Nursing care</h4>
									<p>Nurses are integral member of interpersonal professional teams on the UNMICRC </p>
								</div>
							</a>
						</div>
						<!-- Feature BLock -->
						<div class="feature-block-two col-lg-6 col-md-6 col-sm-12 mt-20">
							<a href="PatientCareDetails.aspx">
								<div class="inner-box">
									<span class="icon fa fa-first-aid"></span>
									<h4>Patients Care</h4>
									<p>
										We value excellence, in our treatment of patients with integrity and state of the art equipment
									</p>
								</div>
							</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
	<!-- Category Section -->
	<!-- Why Choose Us Section Start -->
	<div class="why-choose-us gray-bg  pt-30 pb-30">
		<div class="container">
			<div class="row">
				<div class="col-lg-8 lg-pr-0 md-mb-50">
					<div class="choose-us-part">
						<div class="sec-title mb-10">
							<h2 class="title">Support Services</h2>
							<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
						</div>
						<div class="row facilities-part">
							<%--<div class="col-lg-6 col-md-6 mb-10">
									<div class="single-facility">
										<div class="icon-part one">
											<img src="assets/img/category/InfectionControl.png">
										</div>
										<div class="text-part">
											<h4 class="title">Infection Control</h4>
										</div>
									</div>
								</div>--%>
							<%=strServices %>
							<%--</div>
							<div class="single-facility wow fadeInUp animated" data-wow-delay="200ms" data-wow-duration="1500ms">
								<div class="icon-part one">
									<i class="fas fa-user-md"></i>
								</div>
								<div class="text-part">
									<h4 class="title">Trusted Doctors</h4>
									<p class="desc">
										At vero eos et accusamus et iusto odio dignissimos ducimus qui
											blanditiis
											praesentium.
									</p>
								</div>
							</div>
							<div class="single-facility wow fadeInUp animated" data-wow-delay="300ms" data-wow-duration="1500ms">
								<div class="icon-part one">
									<i class="fas fa-ambulance"></i>
								</div>
								<div class="text-part">
									<h4 class="title">Emergency Treatment</h4>
									<p class="desc">
										At vero eos et accusamus et iusto odio dignissimos ducimus qui
											blanditiis
											praesentium.
									</p>
								</div>
							</div>--%>
						</div>
					</div>
				</div>
				<div class="col-lg-4 service-sidebar">
					<div class="sec-title mb-10">
						<h2 class="title">Patient Speaks</h2>
						<img src="<%= ResolveUrl("~/Hospital/assets/img/Icon_team.png")%>" alt="line" class="med_bottompadder20_4">
					</div>
					<div class="sidebar-testimonial">
						<div class="testimonial-carousel-5 owl-carousel owl-theme owl-nav-none owl-dots-none owl-loaded owl-drag">
							<div class="owl-stage-outer">
								<div class="owl-stage">
									<%=strTestimonial %>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Why Choose Us Section End -->
	<!-- Projects -->
	<div class="projects-area two section-overlay-two ltn__gallery-area mb-20">
		<div class="container-fluid">
			<!-- Tabs -->
			<section class="comp-section ltn__gallery-active ltn__gallery-style-2 ltn__gallery-info-hide---">
				<div class="row">
					<div class="col-md-12">
						<div class="card1">
							<div class="card-body pb-0">
								<div class="gallary-slider owl-theme owl-carousel">

									<%=strPhoto %>
								</div>


							</div>
						</div>
					</div>
				</div>
			</section>
			<!-- /Tabs -->
		</div>
	</div>
	<!--blog and event -->
	<!-- Events Section Start -->
	<div class="rs-event home8-style1 event-bg md-pt-70 md-pb-10 pb-10">
		<div class="container">
			<div class="row">
				<div class="col-lg-12">
					<!-- Tab Menu -->
					<nav class="user-tabs mb-4">
						<ul class="nav nav-tabs nav-tabs-bottom">
							<li class="nav-item">
								<a class="nav-link active" href="#doc_locations" data-toggle="tab">Past Event</a>
							</li>
							<li class="nav-item">
								<a class="nav-link " href="#doc_overview" data-toggle="tab">Upcoming Event</a>
							</li>
						</ul>
						<div class="text-right">
							<a class="readon viewtop" href="Event">View More</a>
						</div>
					</nav>
					<!-- /Tab Menu -->
					<!-- Tab Content -->
					<div class="tab-content">
						<%=strEvent %>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Events Section End -->

	<!-- news-section -->
	<section class="rs-blog orange-color style1 modify1 pt-30  pb-25">
		<div class="container">
			<div class="section-header_1 mb-10">
				<div class="headingtitle">
					<h2 class="title">News &amp; Blogs</h2>
					<img src="<%=ResolveUrl("~/Hospital/assets/img/Icon_team.png") %>" alt="line" class="med_bottompadder20_4">
				</div>
				<div class="load-more text-center mb-0">
					<a class="readon" href="<%=ResolveUrl("~/Blogs") %>">View More</a>
				</div>
			</div>

			<div class="row clearfix">
				<%=strBlog %>
			</div>

		</div>


	</section>
	<!--blog and event -->


	<!-- Newsletter section start -->
	<%--<div class="rs-newsletter style1 event2-bg yellow-color mb--90 wow slideInUp animated" data-wow-delay="00ms"
		data-wow-duration="1500ms">
		<div class="container">
			<div class="newsletter-wrap">
				<div class="row y-middle">
					<div class="col-lg-6 col-md-12">
						<div class="content-part">
							<div class="sec-title">
								<div class="title-icon md-mb-15">
									<img src="<%= ResolveUrl("~/Hospital/assets/img/white-newsletter3.png")%>" alt="images">
								</div>
								<h2 class="title mb-0 white-color">Subscribe to Newsletter</h2>
							</div>
						</div>
					</div>
					<div class="col-lg-6 col-md-12">
						<div class="newsletter-form">
							<input type="email" name="email" id="txtNewsletterEmail" runat="server" placeholder="Enter Your Email" />
							<asp:RequiredFieldValidator ID="rfNewsletterEmail" runat="server" ErrorMessage="*" ValidationGroup="GRp" ControlToValidate="txtNewsletterEmail" Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtNewsletterEmail"
								CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
								Display="Dynamic" ErrorMessage="Invalid email address" ValidationGroup="GRp" />
							<button type="submit" id="btnNewsLetter" runat="server" validationgroup="GRp" onserverclick="btnNewsLetter_Click">Submit</button>
						</div>
						<div class="newsletter-form">
							<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>--%>
	<!-- Newsletter section end -->

	<section class="section section-search">
		<img src="<%=ResolveUrl("~/Hospital/assets/img/haritage.jpg")%>" class="img-fluid" />
	</section>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
	<div class="modal fade" id="myModal" role="dialog">
		<div class="modal-dialog modal-lg">
			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header" style="background: #27487d;">
					<button type="button" class="close" data-dismiss="modal" style="color: #ffffff;">&times;</button>
				</div>
				<div class="modal-body" style="height:500px !important;overflow:auto;">
					<asp:Repeater ID="Popupmaster" runat="server">
						<ItemTemplate>
							<%# Unmehta.WebPortal.Web.Common.Functions.CustomHTMLDecode(Eval("Description").ToString(),this.Page)%>
						</ItemTemplate>
					</asp:Repeater>
				</div>
				<%--<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
				</div>--%>
			</div>

		</div>
	</div>

	<!-- chat box Wrapper start -->
	<div id="chat-circle" class="btn btn-raised">
		<i class="fas fa-comment-alt"></i>
	</div>
	<div class="chat-box">
		<div class="chat-box-header">
			U.N. Mehta Help Line
			<span class="chat-box-toggle"><i class="fas fa-times"></i></span>
		</div>

		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<asp:UpdatePanel runat="server">
			<ContentTemplate>
				<div class="chat-box-body">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="pt150">
									<div class="chat-logs" id="dvChatDetails">
										<%=strChatBox %>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="chat-input-1">
					<asp:HiddenField ID="hfLastFieldName" runat="server" />
					<input type="text" id="txtMessage" class="chat-input" placeholder="Send a message..." onkeypress="return runScriptChat(event);">
					<button type="submit" id="lnkSubmit" class="chat-submit" onclick=" ChatResponse();"><i class="fas fa-paper-plane"></i></button>
					<%--<asp:Button ID="Button1" runat="server" CssClass="chat-submit"  Text="Button" />--%>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>

	<!-- chat box Wrapper end -->
	<script src="<%=ResolveUrl("~/Hospital/assets/js/jquery.counterup.min.js")%>"></script>

	<script src="<%=ResolveUrl("~/Hospital/assets/js/jquery.waypoints.min.js")%>"></script>

	<%=strPopup %>

	<script>

		// vars
		'use strict'

		$(function () {


			$(document).delegate(".chat-btn", "click", function () {
				var value = $(this).attr("chat-value");
				var name = $(this).html();
				$("#chat-input").attr("disabled", false);
				generate_message(name, 'self');

				document.getElementById('cm-msg-12').scrollIntoView();

			})

			$("#chat-circle").on('click', function () {
				$("#chat-circle").toggle('scale');
				$(".chat-box").toggle('scale');

				Reset();
			})

			$(".chat-box-toggle").on('click', function () {
				$("#chat-circle").toggle('scale');
				$(".chat-box").toggle('scale');
			})

		})

		var testim = document.getElementById("testim"),
		  testimDots = Array.prototype.slice.call(document.getElementById("testim-dots").children),
		  testimContent = Array.prototype.slice.call(document.getElementById("testim-content").children),
		  testimLeftArrow = document.getElementById("left-arrow"),
		  testimRightArrow = document.getElementById("right-arrow"),
		  testimSpeed = 4500,
		  currentSlide = 0,
		  currentActive = 0,
		  testimTimer,
		  touchStartPos,
		  touchEndPos,
		  touchPosDiff,
		  ignoreTouch = 30;
		;

		window.onload = function () {

			// Testim Script
			function playSlide(slide) {
				for (var k = 0; k < testimDots.length; k++) {
					testimContent[k].classList.remove("active");
					testimContent[k].classList.remove("inactive");
					testimDots[k].classList.remove("active");
				}

				if (slide < 0) {
					slide = currentSlide = testimContent.length - 1;
				}

				if (slide > testimContent.length - 1) {
					slide = currentSlide = 0;
				}

				if (currentActive != currentSlide) {
					testimContent[currentActive].classList.add("inactive");
				}
				testimContent[slide].classList.add("active");
				testimDots[slide].classList.add("active");

				currentActive = currentSlide;

				clearTimeout(testimTimer);
				testimTimer = setTimeout(function () {
					playSlide(currentSlide += 1);
				}, testimSpeed)
			}

			testimLeftArrow.addEventListener("click", function () {
				playSlide(currentSlide -= 1);
			})

			testimRightArrow.addEventListener("click", function () {
				playSlide(currentSlide += 1);
			})

			for (var l = 0; l < testimDots.length; l++) {
				testimDots[l].addEventListener("click", function () {
					playSlide(currentSlide = testimDots.indexOf(this));
				})
			}

			playSlide(currentSlide);

			// keyboard shortcuts
			document.addEventListener("keyup", function (e) {
				switch (e.keyCode) {
					case 37:
						testimLeftArrow.click();
						break;

					case 39:
						testimRightArrow.click();
						break;

					case 39:
						testimRightArrow.click();
						break;

					default:
						break;
				}
			})

			testim.addEventListener("touchstart", function (e) {
				touchStartPos = e.changedTouches[0].clientX;
			})

			testim.addEventListener("touchend", function (e) {
				touchEndPos = e.changedTouches[0].clientX;

				touchPosDiff = touchStartPos - touchEndPos;

				console.log(touchPosDiff);
				console.log(touchStartPos);
				console.log(touchEndPos);


				if (touchPosDiff > 0 + ignoreTouch) {
					testimLeftArrow.click();
				} else if (touchPosDiff < 0 - ignoreTouch) {
					testimRightArrow.click();
				} else {
					return;
				}

			})
		}

	</script>
	<script lang="JavaScript" type="text/javascript">

		function isScrollToDown() {
			var chatWindow = document.getElementById('dvChatDetails');
			var xH = chatWindow.scrollHeight;
			chatWindow.scrollTo(0, xH);

			return true;
		}

		function Reset() {

			var varOldData = document.getElementById('dvChatDetails').innerHTML;


			if (varOldData.includes('Application Submitted')) {

				var defaultString = '<div id="cm-msg-0" class="chat-msg user">	<div class="cm-msg-text">Thanks for contacting us. Please tell something about yourself </div></div><div id="cm-msg-1" class="chat-msg user">	<div class="cm-msg-text">Your Name </div></div>'
				document.getElementById('dvChatDetails').innerHTML = defaultString;

			}
			return true;
		}

		function runScriptChat(e) {
			if (e.keyCode == 13) {

				var chatWindow = document.getElementById('txtMessage');
				var urlS = "<%=ResolveUrl("~/Default.aspx")%>/ChatResponse";
				$.ajax({
					type: "POST",
					url: urlS,
					data: "{'txtMessage':'" + chatWindow.value + "'}",
					dataType: "json",
					contentType: "application/json; charset=utf-8",
					success: function (data) {
						var returnedstring = data.d;
						var strData = returnedstring;

						var varOldData = document.getElementById('dvChatDetails').innerHTML;

						document.getElementById('dvChatDetails').innerHTML = strData.strChatBox;


						var chatWindowinner = document.getElementById('txtMessage');
						chatWindowinner.value = "";


						var chatWindows = document.getElementById('dvChatDetails');
						var xH = chatWindows.scrollHeight;
						chatWindows.scrollTo(0, xH);

						isScrollToDown();
						return false;
					}
				});
				isScrollToDown();
				return false;
				//document.getElementById("btnSearchBox").click(); //javascript
			}
		}
		
		function ChatResponseSkip(skipString,e) {

		    var chatWindows = document.getElementById('dvChatDetails');

			var urlS = "<%=ResolveUrl("~/Default.aspx")%>/ChatResponse";
			$.ajax({
				type: "POST",
				url: urlS,
				data: "{'txtMessage':'" + skipString + "'}",
				dataType: "json",
				contentType: "application/json; charset=utf-8",
				success: function (data) {
					var returnedstring = data.d;
					var strData = returnedstring;

					var varOldData = document.getElementById('dvChatDetails').innerHTML;

					document.getElementById('dvChatDetails').innerHTML = strData.strChatBox;

					var chatWindowinner = document.getElementById('txtMessage');
					chatWindowinner.value = "";


					var chatWindows = document.getElementById('dvChatDetails');
					var xH = chatWindows.scrollHeight;
					chatWindows.scrollTo(0, xH);

					return false;
				}
			});

			return false;
		}

		function ChatResponse() {
			var chatWindows = document.getElementById('dvChatDetails');

			var chatWindow = document.getElementById('txtMessage');
			var urlS = "<%=ResolveUrl("~/Default.aspx")%>/ChatResponse";
			$.ajax({
				type: "POST",
				url: urlS,
				data: "{'txtMessage':'" + chatWindow.value + "'}",
				dataType: "json",
				contentType: "application/json; charset=utf-8",
				success: function (data) {
					var returnedstring = data.d;
					var strData = returnedstring;

					var varOldData = document.getElementById('dvChatDetails').innerHTML;

					document.getElementById('dvChatDetails').innerHTML = strData.strChatBox;

					var chatWindowinner = document.getElementById('txtMessage');
					chatWindowinner.value = "";


					isScrollToDown();

					return false;
				}
			});

			isScrollToDown();
			return false;
		}

		function isNumberKey(e) {
			var result = false;
			try {
				var charCode = (e.which) ? e.which : e.keyCode;
				if ((charCode > 31) && (charCode >= 48 && charCode <= 57)) {
					result = true;
				}
			}
			catch (err) {
				//console.log(err);
			}
			return result;
		}

		function lettersWithSpaceOnly() {
			var charCode = event.keyCode;
			if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

				return true;
			else
				return false;
		}

		function ShowPopup() {
			$("#exampleModalfat").modal("show");
		}

		/* --------------------------------------------------------
							30. Counter Up
					--------------------------------------------------------- */
		// $('.ltn__counter').counterUp();

		$('.counter').counterUp({
			delay: 10,
			time: 2000
		});
		$('.counter').addClass('animated fadeInDownBig');
		$('h3').addClass('animated fadeIn');
	</script>
</asp:Content>
