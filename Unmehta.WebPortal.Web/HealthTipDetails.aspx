<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HealthTipDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.HealthTipDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Health Tip Details
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
                <li><a href="<%=ResolveUrl("~/HealthTips") %>">Health Tips</a></li>
                <li>/</li>
                <li>Health Tip Details</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->
    <div class="content">
			<div class="container">
				<div class="row">
					<div class="col-lg-9 col-md-12">
						<div class="blog-view">
							<div class="blog blog-single-post">
								<%=strHealthTipsDetail %>
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
					<!-- Blog Sidebar -->
					<div class="col-lg-3 col-md-12 sidebar-right ">
						<!-- Latest Posts -->
						<div class="card post-widget">
							<div class="card-header">
								<h4 class="card-title">Health Tips</h4>
							</div>
							<div class="card-body">
								<ul class="latest-posts">
									<%=strHealthTips %>
								</ul>
							</div>
                            <br>
							<div class="load-more text-center mb-0">
								<a class="readon" href="<%=ResolveUrl("~/HealthTips") %>">View More</a>
							</div>
							<br>
						</div>
					</div>
					<!-- /Blog Sidebar -->

				</div>
			</div>

		</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
