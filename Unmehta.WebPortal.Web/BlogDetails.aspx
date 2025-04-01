<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlogDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.BlogDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	<%=strType %> Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
	<!-- Breadcrumb -->
		<div class="page-title">
			<img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
			<div class="container">
				<ul class="page-breadcrumb">
				<li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
					<li>/</li>
					<li><a href="<%=ResolveUrl("~/Blogs") %>"><%=strType %></a></li>
					<li>/</li>
					<li><%=strType %> Details</li>
				</ul>
			</div>
		</div>
		<!-- /Breadcrumb -->

		<!-- Page Content -->
		<div class="content">
			<div class="container">
				<div class="row">
					<div class="col-lg-9 col-md-9">
						<div class="blog-view">
							<div class="blog blog-single-post">
								<div class="blog-image">
									<a href="javascript:void(0);"><img alt="" id="imgData" runat="server" src="" class="img-fluid"></a>
								</div>
								<h4 class="widget-title" id="blogName" runat="server"></h4>
								<div class="blog-info clearfix">
									<div class="post-left">
										<ul>
											<li><i class="far fa-calendar"></i><span id="lblBlogDate" runat="server"></span></li>
											<li><i class="fa fa-tags"></i><span id="lblType" runat="server"></span></li>
										</ul>
									</div>
								</div>
								<div class="blog-content" id="blogContent" runat="server">

								</div>
							</div>

							<div class="card blog-share clearfix">
								<div class="card-header">
									<h4 class="card-title">Share the post</h4>
								</div>
								<div class="card-body">
									<ul class="social-share" id="socialShare" runat="server">
										
									</ul>
								</div>
							</div>
						</div>
					</div>
					<!-- Blog Sidebar -->
					<div class="col-lg-3 col-md-12 sidebar-right ">
						<!-- Latest Posts -->
						<div class="card post-widget">
							<div class="card-header">
								<h4 class="card-title">Latest</h4>
							</div>
							<div class="card-body">
								<ul class="latest-posts" id="LatestBlogList" runat="server">
									
									</ul>
							</div>
							<br>
							<div class="load-more text-center mb-0">
								<a class="readon" href="<%=ResolveUrl("~/Blogs") %>">View More</a>
							</div>
							<br>
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
