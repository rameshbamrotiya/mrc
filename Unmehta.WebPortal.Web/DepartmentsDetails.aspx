<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentsDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.DepartmentsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Department Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<style type="text/css">
		.page_enabled, .page_disabled {
			display: inline-block;
			min-width: 20px;
			line-height: 20px;
			text-align: center;
			text-decoration: none;
			border: 1px solid #ccc;
		}

		.page_enabled {
			background-color: #eee;
			color: #000;
		}

		.canvasbg {
			position: relative;
			background: #FFFFFF;
			display: inline-block;
			z-index: 999;
			width: 65px;
			height: 15px;
			top: 405px;
		}

		.page_disabled {
			background-color: #6C6C6C;
			color: #fff !important;
		}
	</style>
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
				<li><a href="<%=ResolveUrl("~/Departments") %>">Department</a></li>
				<li>/</li>
				<li id="liDeptName" runat="server">Department Details</li>
			</ul>
		</div>
	</div>

	<!-- Page Content -->

	<div class="content">
		<div class="container">
			<div class="row">
				<!-- Doctor Details Tab -->
				<div class="col-lg-9">
					<%=strDepartmentDetails %>
				</div>
				<div class="col-lg-3" id="dvRightSideMenu" runat="server">
					<div class="sidebar">
						<div class="card category-widget">
							<div class="card-header">
								<h4 class="card-title">Department</h4>
							</div>
							<br>
							<!-- <div class="wrapper"><iframe width="100%" src="https://www.youtube.com/embed/tgbNymZ7vqY">
									</iframe>
								</div> -->
							<div class="card-body">
								<div class="iframe-parent-class" style="height: auto;">
									<iframe width="100%" height="190" id="dvExternalVieoLink" runat="server" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen=""></iframe>
								</div>
								<br>
								<br>
								<%=strBackURL %>
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

	<script>

		<%=strScript%>

	</script>

	<script>
		/*Floating Code for Iframe Start*/
		if (
			jQuery(
				'iframe[src*="https://www.youtube.com/embed/"],iframe[src*="https://player.vimeo.com/"],iframe[src*="https://player.vimeo.com/"]'
			).length > 0
		) {
			/*Wrap (all code inside div) all vedio code inside div*/
			jQuery(
				'iframe[src*="https://www.youtube.com/embed/"],iframe[src*="https://player.vimeo.com/"]'
			).wrap("<div class='iframe-parent-class'></div>");
			/*main code of each (particular) vedio*/
			jQuery(
				'iframe[src*="https://www.youtube.com/embed/"],iframe[src*="https://player.vimeo.com/"]'
			).each(function (index) {
				/*Floating js Start*/
				var windows = jQuery(window);
				var iframeWrap = jQuery(this).parent();
				var iframe = jQuery(this);
				var iframeHeight = iframe.outerHeight();
				var iframeElement = iframe.get(0);
				windows.on("scroll", function () {
					var windowScrollTop = windows.scrollTop();
					var iframeBottom = iframeHeight + iframeWrap.offset().top;
					//alert(iframeBottom);

					if (windowScrollTop > iframeBottom) {
						iframeWrap.height(iframeHeight);
						iframe.addClass("stuck");
						jQuery(".scrolldown").css({ display: "none" });
					} else {
						iframeWrap.height("auto");
						iframe.removeClass("stuck");
					}
				});
				/*Floating js End*/
			});
		}

		/*Floating Code for Iframe End*/

		function ChangePage(pageName) {
			$.ajax({
				type: "POST",
				url: "<%=ResolveUrl("~/DepartmentsDetails.aspx")%>/GetDataDetails",
				contentType: "application/json; charset=utf-8",
				dataType: "json", data: "{ PageId : " + pageName + " }",
				success: function (response) {

					document.getElementById("<%=strFacultyTab%>").innerHTML = response.d;
				},
				failure: function (response) {
					alert(response.d);
				}
			});
			}
	</script>
	<%=strPopupDetails%>
</asp:Content>
