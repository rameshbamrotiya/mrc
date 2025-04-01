<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schema.aspx.cs" Inherits="Unmehta.WebPortal.Web.Schema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Schema 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<style>
		
		.canvasbg {
	position: relative;
	background: #FFFFFF;
	display: inline-block;
	z-index: 999;
	width: 65px;
	height: 15px;
	top: 405px;
}
	</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
	<div class="page-title">
		<img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
		<div class="container-fluid">
			<ul class="page-breadcrumb">
				 <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
				<li>/</li>
				<li><a href="<%=ResolveUrl("~/SchemaDetails") %>">Schema Details</a></li>
				<li>/</li>
				<li><%=strSchemaName%></li>
			</ul>
		</div>
	</div>
	<div class="content">
		<div class="container">
			<div class="row">
				<div class="col-lg-9 col-md-12">
					<div class="blog-view">
						<div class="blog blog-single-post">
							<div class="blog-content">
								<div class="row" >
									<div class="col-lg-8" >
										<h3 class="blog-title" id="dvBlogTitle" runat="server"></h3>
										<p style="text-align: justify;" id="dvDesc" runat="server"> 
										</p>
									</div>
									<div class="col-lg-4">
										<div class="scheme-img">
											<img src="assets/img/scheme/ma_yojna.jpg" id="imgLogo" runat="server" class="img-fluid" alt="User Image">
										</div>
									</div>
								</div>
							</div>
							<br>

							<div class="card" id="dvSubDetails" runat="server">
								<div class="card-body">
									<div class="doc-info">
										<div class="doc-info-cont">
											<p class="doc-speciality mb-0">Contact Person</p>
											<h4 class="doc-name" id="contactPersonName" runat="server"></h4>
										</div>
										<div class="doc-info-cont">
											<p class="doc-speciality mb-0">Phone Number</p>
											<h4 class="doc-name" id="contactPersonMobile" runat="server"></h4>
										</div>
										<div class="doc-info-cont">
											<p class="doc-speciality mb-0">Website</p>
											<h4 class="doc-name" id="contactPersonWebSite" runat="server"></h4>
										</div>
										<div class="doc-info-cont">
											<p class="doc-speciality mb-0">Location</p>
											<h4 class="doc-name" id="contactPersonLocation" runat="server"></h4>
										</div>
									</div>
								</div>
							</div>

								<%=strChartTable %>

						</div>
					</div>
				</div>
				<div class="col-lg-3">
					<div class="sidebar">
						<div class="card widget-categories">
							<div class="card-header">
								<h4 class="card-title">Schemes</h4>
							</div>
							<div class="card-body">
								<ul class="categories nav nav-pills nav-stacked flex-column" id="liSchemaList" runat="server">
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
	<script src="<%=ResolveUrl("~/Scripts/canvasjs.min.js") %>"></script>
	<script>
		<%=strScript%>
	</script>
</asp:Content>
