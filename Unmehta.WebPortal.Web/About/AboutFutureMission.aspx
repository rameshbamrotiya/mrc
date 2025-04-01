<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutFutureMission.aspx.cs" Inherits="Unmehta.WebPortal.Web.AboutFutureMission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
   UnMehta - Future &amp; Mission
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
                <li><a href="<%=ResolveUrl("~/About/") %>">About</a></li>
                <li>/</li>
					<li>Future Vision</li>
				</ul>
			</div>
		</div>

    <section class="content">
			<div class="container">
					<%--<div class="col-md-6 col-lg-4 col-sm-12">
						<!-- Blog Post -->
						<div class="blog grid-blog">
							<div class="blog-image">
								<a href="#"><img class="img-fluid" src="assets/img/vision.jpg" alt="Post Image"></a>
							</div>
							<div class="blog-content">
								<h3 class="blog-title text-center"><a href="#">Vision</a>
								</h3>
								<p>Our vision is to be recognized as World Class facility in quality cardiac care and as a center of
									excellence in
									Cardiovascular Research.</p>
							</div>
						</div>
						<!-- /Blog Post -->
					</div>
					<div class="col-md-6 col-lg-4 col-sm-12">
						<!-- Blog Post -->
						<div class="blog grid-blog">
							<div class="blog-image">
								<a href="#"><img class="img-fluid" src="assets/img/mission.jpg" alt="Post Image"></a>
							</div>
							<div class="blog-content">
								<h3 class="blog-title text-center"><a href="#">Mission</a></h3>
								<p>To Offer World Class Quality Care in cardiology at No cost or Concessional cost and to provide free
									super specialty
									higher education in Cardiology, Cardiovascular Thoracic
									Surgery &amp; Cardiac Anesthesia.
								</p>
							</div>
						</div>
						<!-- /Blog Post -->
					</div>
					<div class="col-md-6 col-lg-4 col-sm-12">
						<!-- Blog Post -->
						<div class="blog grid-blog">
							<div class="blog-image">
								<a href="#"><img class="img-fluid" src="assets/img/value.jpg" alt="Post Image"></a>
							</div>
							<div class="blog-content">
								<h3 class="blog-title text-center"><a href="#">Values</a></h3>
								<p>We value patient above all, particularly the economically poorest of the poor patients by treating
									them with compassion,
									dignity and respect.
									<span id="dots">...</span><span id="more">We value excellence, in our treatment of patients with
										integrity and state of art equipment, with best
										quality care
										services.We value life over of everything else, we make all
										efforts to save as many lives of the patients as possible, being
										tertiary care cardiac hospital, we spare no efforts in trying our best even in extremely critical
										situation to as lives.
										We value the outcome, the results of our cardiac procedures &amp; surgeries exceed most of our patients’
										expectation. And
										they are at par with National / International level.
										We value compassion, in treating all patients including the poorest of poor patients and their
										families with utmost care
										and kindness.
										We value quality; we try to recruit the best cardiology doctors, anesthetists, cardiac surgeons, and
										the best possible
										nursing staff to deliver the best quality care to all our patients.</span>
								</p>
								<div class="load-more mb-0">
									<a class="btn btn-primary btn-sm text-white" onclick="myFunction()" id="myBtn">Read
										More</a>
								</div>
								<!-- <button onclick="myFunction()" id="myBtn">Read more</button> -->
							</div>

						</div>
						<!-- /Blog Post -->

					</div>--%>

                    <%=strVisionMission %>
			</div>
		</section>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
