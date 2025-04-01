<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Otherspecialitydetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Other_Specialties.Otherspecialitydetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Other Specialty Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<style>
		@keyframes fade-in-up {
			0% {
				opacity: 0;
			}

			100% {
				transform: translateY(0);
				opacity: 1;
			}
		}

		.stuck {
			position: fixed;
			bottom: 20px;
			right: 20px;
			transform: translateY(100%);
			width: 260px;
			height: 145px;
			animation: fade-in-up 0.25s ease forwards;
			z-index: 999;
		}

		/*Floating CSS End*/

		@keyframes example {
			0% {
				background-color: red;
			}

			25% {
				background-color: #ff7037;
			}

			50% {
				background-color: red;
			}

			100% {
				background-color: #ff7037;
			}
		}

		p.scrolldown {
			width: 200px;
			font-size: 20px;
			font-weight: bold;
			text-align: center;
			border: 1px solid;
			background: #ff7037;
			position: fixed;
			right: 75px;
			color: #fff;
			-webkit-animation-name: example;
			/* Safari 4.0 - 8.0 */
			-webkit-animation-duration: 4s;
			/* Safari 4.0 - 8.0 */
			animation-name: example;
			animation-duration: 2s;
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
				<li><a href="<%=ResolveUrl("~/OtherSpecialties/Otherspeciality") %>">Other Specialty</a></li>
				<li>/</li>
				<li id="liTitle" runat="server"></li>
			</ul>
		</div>
	</div>
	<div class="content">
		<div class="container">
			<div class="row">
				<!-- Doctor Details Tab -->
				<div class="col-lg-9">
					<!-- Tab Menu 1 -->
					<div class="tab-pane">
						<!-- /Tab Menu -->
						<div class="row">
							<div class="col-md-12 col-lg-12">
								<!-- About Details -->
								<div class="widget about-widget">
									<%--<h4 class="widget-title">Telemedicine</h4>--%>
									<%=strListOfSubSectionDescription %>
								</div>
								<!-- /About Details -->
							</div>
						</div>
						<div class="row"  id="SubDetails" runat="server">
								<div class="col-md-12 col-lg-12">
									<!-- About Details -->
									<div class="widget about-widget">
										<!-- <h4 class="widget-title">Type of Infrastructure</h4> -->
										<div class="accordion-box">
											<div class="title-box">
												<h6>Type of Infrastructure</h6>
											</div>
											<ul class="accordion-inner">
												<li class="accordion block" id="liFacility" runat="server">
													<div class="acc-btn">
														<div class="icon-outer"></div>
														<h6>Facilities</h6>
													</div>
													<div class="acc-content">
															 <%=strListOfSubSectionDescriptionFacility %>
													</div>
												</li>
												<li class="accordion block"  id="liStaff" runat="server">
													<div class="acc-btn">
														<div class="icon-outer"></div>
														<h6>Staff Details</h6>
													</div>
													<div class="acc-content">
														<%--<div class="row">--%>
															 <%=strListOfSubSectionDescriptionStaffDetails %>
														<%--</div>--%>
													</div>
												</li>
											</ul>
										</div>
									</div>
									<!-- /About Details -->
								</div>
							</div>
						<!-- Location List -->
						<div class="Equipmentdata" id="divimagesection" runat="server">
							<h4 class="widget-title">Photos</h4>
							<div class="author-widget  mb-15">
								<div class="about-author">
									<div class="row">
										<%=strListOfImages %>
									</div>
								</div>
							</div>
						</div>
						<!-- /Location List -->

						<!-- /Location List -->
						
								<%=strChartTable %>

						
						<!-- Tab Content -->
					</div>
					<!-- /Tab Menu -->
				</div>
				<asp:Repeater runat="server" ID="dtlstDocument">
					<ItemTemplate>
						<div class="col-lg-3">
							<div class="sidebar">
								<div class="card category-widget">
									<div class="card-header">
										<h4 class="card-title">Other Specialties</h4>
									</div>
									<br>
									<div class="card-body">
										<iframe width="100%" height="190" src="<%# Page.ResolveUrl( Eval("InnerVideoLink").ToString())%>"
											title="YouTube video player" frameborder="0"
											allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
											allowfullscreen></iframe>
										<br>
										<br>
										<img src="<%# Page.ResolveUrl( Eval("InnerImgpath").ToString())%>" class="img-fluid">
									</div>
								</div>
							</div>
						</div>
					</ItemTemplate>
				</asp:Repeater>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
	<script src="<%=ResolveUrl("~/Scripts/canvasjs.min.js") %>"></script>
	<script>
		<%=strScript%>
	</script>

	<%=strListOfImagespopup %>
</asp:Content>
