<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WhyJoinUInmicrc.aspx.cs" Inherits="Unmehta.WebPortal.Web.WhyJoinUInmicrc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	 UnMehta - <%=strPageName %>
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
				<li><a href="<%=ResolveUrl("~/Career") %>">Career</a></li>
				<li>/</li>
				<li><%=strPageName %></li>
			</ul>
		</div>
	</div>
	<!-- /Breadcrumb -->    
	<!-- Page Content -->
	<div class="content">
		<div class="container">
			<div class="row">
				<div class="col-lg-9 col-md-12">
					<div class="card">
						<div class="card-body pt-0">
							<!-- Tab Menu -->
							<h3 class="pt-4"><%=strPageName %></h3>
							<hr>
							<!-- /Tab Menu -->
							<!-- Tab Content -->
							<div class="tab-content pt-3">
								<!-- Overview Content -->
								<div role="tabpanel" id="doc_overview" class="tab-pane fade show active">
									<div class="row">
										<div class="col-md-12">
											<!-- About Details -->



											<!-- About Details -->

											<!-- /About Details -->
											<!-- About Details -->
											<%--<div class="widget about-widget mb-3">
												<h4 class="widget-title">Description</h4>
												<p class="mb-0">
													U. N. Mehta Institute of Cardiology & Research Centre offers a great career
														prospect in the area
														of medicine and healthcare management.
														The hospital adheres to international norms, which provides our staff with exposure to
														international standards of
														healthcare management. A great career is not only about the amount of learning and growth
														but is also about the
														remuneration & we provide the best in the market. We have several opportunities awaiting
														you; please go through them
														below:
												</p>
											</div>--%>
											<%=strPageDetails %>
											<!-- /About Details -->
											<!-- <div class="submit-section submit-btn-bottom">
													
													<button type="submit" class="btn btn-primary submit-btn">Apply Now</button>
												</div> -->

										</div>
									</div>
								</div>
								<!-- /Overview Content -->

							</div>
						</div>
					</div>
				</div>

				<!-- Blog Sidebar -->
				<div class="col-lg-3">
					<div class="sidebar">
						<div class="card widget-categories">
							<div class="card-header">
								<h4 class="card-title">Career</h4>
							</div>
							<div class="card-body">
								<ul class="categories nav nav-pills nav-stacked flex-column">
									<%=strQuickLink %>
								</ul>
							</div>
						</div>
					</div>
				</div>
				<!-- /Blog Sidebar -->
			</div>
		</div>
	</div>
	<!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
