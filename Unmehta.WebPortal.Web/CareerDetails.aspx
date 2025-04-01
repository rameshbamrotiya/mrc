<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CareerDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.CareerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Career Details
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
				<li>CareerDetails</li>
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
								<%=strCareerDetails%>
								</div>
							</div>
						</div>
					</div>

					<!-- Blog Sidebar -->
					<div class="col-lg-3 col-md-12 sidebar-tenders">
						<!-- Latest Posts -->
						<div class="card post-widget">
							<div class="card-header">
								<h4 class="card-title">Walk-In List</h4>
							</div>
							<div class="card-body">
								<ul class="latest-tenders">
									<%=strcareerwalkinside%>
								</ul>
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
