<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Accreditation.aspx.cs" Inherits="Unmehta.WebPortal.Web.About.Accreditation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Accreditation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
	
	<div class="page-title">
			<img src="<%=strHeaderImage%>" class="img-fluid" alt="banner">
			<div class="container-fluid">
				<ul class="page-breadcrumb">
				<li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
				<li>/</li>
				<li><a href="<%=ResolveUrl("~/About/") %>">About</a></li>
				<li>/</li>
					<li>Accreditation</li>
				</ul>
			</div>
		</div>

	<div class="content" style="min-height: 104.859px;">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-12" id="PageContent" runat="server">
						<!-- Tab Menu -->
						<nav class="user-tabs mb-4">
							<ul class="nav nav-tabs nav-tabs-bottom">
								<li class="nav-item">
									<a class="nav-link active" href="#doc_overview" data-toggle="tab">Accreditation 1</a>
								</li>
								<li class="nav-item">
									<a class="nav-link" href="#doc_locations" data-toggle="tab">Accreditation 2</a>
								</li>
							</ul>
						</nav>
						<!-- /Tab Menu -->

						<!-- Tab Content -->
						<div class="tab-content">
							<div role="tabpanel" id="doc_overview" class="tab-pane fade active show">
								<div class="row">
									<div class="col-md-8 col-lg-8">
										<!-- About Details -->
										<div class="widget about-widget">
											<h2 style="color:#c90f40;text-align: center;">National Accreditation Board for <br>Hospital and
												Health
												Care Provides (NABH)
											</h2>
											<h5>Advantages of NABH Accreditation:</h5>
											<strong style="color: #c90f40;">"Best Reputation with results of Government
												Institute"</strong><br><br>
											<ul class="format-list">
												<li>Quality assurance</li>
												<li>Attraction for medical tourism &amp; Marketing</li>
												<li>Equitable Patient care</li>
												<li>Protocol for treatments</li>
												<li>Long term cost is reduced</li>
												<li>Patients safety and satisfaction</li>
												<li>Quality care with accountability</li>
											</ul>
										</div>
										<!-- /About Details -->
									</div>
									<div class="col-lg-4">
										<div class="inner-column">
											<figure class="image"><img src="assets/img/NABH.jpg" alt="" class="img-fluid">
											</figure>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12 col-lg-12">
										<!-- Awards Details -->
										<div class="widget awards-widget">
											<h4 class="widget-title">NABH TimeLine</h4>
											<div class="experience-box">
												<ul class="experience-list">
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">12/03/2015</p>
																<h4 class="exp-title">Application To NABH</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">16/05/2015 - 17/05/2015</p>
																<h4 class="exp-title">Pre - Assessment</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">21/08/2015 - 22/08/2015 - 23/08/2015</p>
																<h4 class="exp-title">Final Assessment </h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">07/11/2015</p>
																<h4 class="exp-title">Certificate of Accreditation</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">16/06/2017 - 17/06/2017 - 18/06/2017</p>
																<h4 class="exp-title">Surveillance Assessment</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">06/11/2018</p>
																<h4 class="exp-title">Valid up to</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">26-27-28 October 2018</p>
																<h4 class="exp-title">Renewal Assessment Inspection</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">07/11/2018 to 06/11/2021</p>
																<h4 class="exp-title">Granted Continuation of Certification of Accreditation</h4>
															</div>
														</div>
													</li>
												</ul>
											</div>
										</div>
										<!-- /Awards Details -->
									</div>
								</div>
							</div>
							<div role="tabpanel" id="doc_locations" class="tab-pane fade">
								<div class="row">
									<div class="col-md-8 col-lg-8">
										<!-- About Details -->
										<div class="widget about-widget">
											<h2 style="color:#c90f40;text-align: center;">National Accreditation Board for <br>Hospital and
												Health
												Care Provides (NABH)
											</h2>
											<h5>Advantages of NABH Accreditation:</h5>
											<strong style="color: #c90f40;">"Best Reputation with results of Government
												Institute"</strong><br><br>
											<ul class="format-list">
												<li>Quality assurance</li>
												<li>Attraction for medical tourism &amp; Marketing</li>
												<li>Equitable Patient care</li>
												<li>Protocol for treatments</li>
												<li>Long term cost is reduced</li>
												<li>Patients safety and satisfaction</li>
												<li>Quality care with accountability</li>
											</ul>
										</div>
										<!-- /About Details -->
									</div>
									<div class="col-lg-4">
										<div class="inner-column">
											<figure class="image"><img src="assets/img/NABH.jpg" alt="" class="img-fluid">
											</figure>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12 col-lg-12">
										<!-- Awards Details -->
										<div class="widget awards-widget">
											<div class="experience-box">
												<ul class="experience-list">
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">12/03/2015</p>
																<h4 class="exp-title">Application To NABH</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">16/05/2015 - 17/05/2015</p>
																<h4 class="exp-title">Pre - Assessment</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">21/08/2015 - 22/08/2015 - 23/08/2015</p>
																<h4 class="exp-title">Final Assessment </h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">07/11/2015</p>
																<h4 class="exp-title">Certificate of Accreditation</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">16/06/2017 - 17/06/2017 - 18/06/2017</p>
																<h4 class="exp-title">Surveillance Assessment</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">06/11/2018</p>
																<h4 class="exp-title">Valid up to</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">26-27-28 October 2018</p>
																<h4 class="exp-title">Renewal Assessment Inspection</h4>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<p class="exp-year">07/11/2018 to 06/11/2021</p>
																<h4 class="exp-title">Granted Continuation of Certification of Accreditation</h4>
															</div>
														</div>
													</li>
												</ul>
											</div>
										</div>
										<!-- /Awards Details -->
									</div>
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
