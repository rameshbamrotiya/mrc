<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="Unmehta.WebPortal.Web.SiteMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UNMEHTA- SiteMap
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
                <li><a href="#">SiteMap</a></li>
            </ul>
        </div>
    </div>

    <section class="content">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="content_newdata">
							<div class="widget-content sitemap">
								<div class="row clearfix">

                                    <%=strSiteMapMenuDetails %>

									<div class="col-lg-3 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Department</h4>
										</div>
										<ul class="links clearfix pb-50">
                                            <%=strDepartment %>
										</ul>
									</div>

									<div class="col-lg-3 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Other Specialties</h4>
										</div>
										<ul class="links clearfix pb-50">
                                            <%=strSpecialties %>
										</ul>
									</div>

									<div class="col-lg-4 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Scheme</h4>
										</div>
										<ul class="links clearfix pb-50">                                            
                                            <%=strScheme %>
										</ul>
									</div>

									<div class="col-lg-2 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Researches</h4>
										</div>
										<ul class="links clearfix pb-50">
                                            <%=strResearches %>
										</ul>
									</div>

									<div class="col-lg-2 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Academics</h4>
										</div>
										<ul class="links clearfix pb-50">
											<li><i class="far fa-hand-point-right"></i><a href="<%=ResolveUrl("~/AcademicMedical") %>">Medical</a></li>
											<li><i class="far fa-hand-point-right"></i><a href="<%=ResolveUrl("~/AcademicParaMedical") %>">Para medical</a></li>
										</ul>
									</div>

									<%=strInnerPage %>

                                    <%=strHiddenMenu %>

									<div class="col-lg-3 col-md-6 col-sm-12 column">
										<div class="about-widget">
											<h4 class="widget-title">Site Info</h4>
										</div>
										<ul class="links clearfix pb-50">
											<li><i class="far fa-hand-point-right"></i><a href="<%=ResolveUrl("~/Sitemap") %>">Sitemap</a>
											</li>
											<li><i class="far fa-hand-point-right"></i><a href="<%=ResolveUrl("~/PrivacyPolicy") %>">Privacy Policy</a>
											</li>
											<li><i class="far fa-hand-point-right"></i><a href="<%=ResolveUrl("~/TermsandConditions") %>">Conditions of Use</a>
											</li>
											<li><i class="far fa-hand-point-right"></i><a href="#">Important Public Notices</a>
											</li>
											<li><i class="far fa-hand-point-right"></i><a href="https://gujhealth.gujarat.gov.in/" target="_blank">Health and Family Welfare
													Department</a>
											</li>
										</ul>
									</div>
								</div>

							</div>
						</div>
					</div>

				</div>
			</div>
		</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
