<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Career.aspx.cs" Inherits="Unmehta.WebPortal.Web.Career" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	UnMehta - Career
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<style>
		ul.datetime {
			list-style: none;
		}
	</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
	<!-- Breadcrumb -->
	<div class="page-title">
		<img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
		<div class="container-fluid">
			<ul class="page-breadcrumb">
				<li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
				<li>/</li>
				<li>Career</li>
			</ul>
		</div>
	</div>
	<!-- /Breadcrumb -->
	<!-- Page Content -->
	<!-- About Section -->
	<section class="content">
		<div class="section home-tile-section mb-30">
			<div class="container">
				<div class="row">
					<div class="col-md-12">
						<div class="row">
							<div class="col-lg-3 mb-3">
								<div class="card text-center doctor-book-card">
									<img src="<%=ResolveUrl("~/Hospital/assets/img/medical/Anesthesia.jpg") %>" alt="" class="img-fluid">
									<div class="doctor-book-card-content tile-card-content-1">
										<div>
											<h3 class="card-title mb-0"><%=strWhyJoinUNMICRC %></h3>
											<a href="<%=ResolveUrl("~/WhyJoinUInmicrc") %>" class="btn book-btn_career px-3 py-2 mt-3">Read More</a>
										</div>
									</div>
								</div>
							</div>
							<div class="col-lg-3 mb-3">
								<div class="card text-center doctor-book-card">
									<img src="<%=ResolveUrl("~/Hospital/assets/img/medical/Anesthesia.jpg") %>" alt="" class="img-fluid">
									<div class="doctor-book-card-content tile-card-content-1">
										<div>
											<h3 class="card-title mb-0"><%=strEmployeecareatUNMICRC %></h3>
											<a href="<%=ResolveUrl("~/GrowthatUnmicrc") %>" class="btn book-btn_career px-3 py-2 mt-3">Read More</a>
										</div>
									</div>
								</div>
							</div>
							<div class="col-lg-3 mb-3">
								<div class="card text-center doctor-book-card">
									<img src="<%=ResolveUrl("~/Hospital/assets/img/medical/Anesthesia.jpg") %>" alt="" class="img-fluid" />
									<div class="doctor-book-card-content tile-card-content-1">
										<div>
											<h3 class="card-title mb-0"><%=strGrowthatUNMICRC %></h3>
											<a href="<%=ResolveUrl("~/EmployeCareUnmicrc") %>" class="btn book-btn_career px-3 py-2 mt-3">Read
													More</a>
										</div>
									</div>
								</div>
							</div>
							<div class="col-lg-3 mb-3">
								<div class="card text-center doctor-book-card">
									<img src="<%=ResolveUrl("~/Hospital/assets/img/medical/Anesthesia.jpg") %>" alt="" class="img-fluid">
									<div class="doctor-book-card-content tile-card-content-1">
										<div>
											<h3 class="card-title mb-0"><%=strStarofUNMICRC %></h3>
											<a href='<%=ResolveUrl("~/StarOfHospital") %>' class="btn book-btn_career px-3 py-2 mt-3">Read
													More</a>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- job category wrapper start-->
		<div class="jb_category_wrapper jb_cover">
			<div class="sec-title mb-20 text-center">
				<h1 class="title mb-0">Current Opening</h1>
				<img src="<%=ResolveUrl("~/Hospital/assets/img/Icon_team.png") %>" alt="line" class="med_bottompadder20_4">
			</div>
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="row">
							<div class="career-slider owl-theme owl-carousel owl-loaded">
								<%=strCareer %>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- job category wrapper end-->
		<div class="container">
			<div class="row mt-50">
				<div class="col-md-12 col-lg-4 col-xl-3">
					<div class="StickySidebar">
						<div class="card search-filter">
							<div class="card-header">
								<h4 class="card-title mb-0">Search Filter</h4>
							</div>
							<div class="card-body">
								<div class="form-group">
									<h4>Mode of Apply</h4>
									<asp:DropDownList ID="ddlRecruitmentType" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
								</div>
								<div class="form-group">
									<h4>Designation</h4>
									<asp:DropDownList ID="ddldesignation" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
								</div>
								<div class="form-group">
									<h4>Clinical/ Non-Clinical</h4>
									<asp:DropDownList ID="ddlPostType" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
								</div>
								<div class="btn-search">
									<asp:Button ID="btnsearch" CssClass="btn btn-block" runat="server" Text="Search" OnClick="btnsearch_Click" />
									<%--<button type="button" runat="server"  class="btn btn-block">Search</button>--%>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="col-lg-9 col-md-12 col-sm-12 col-12">
					<div class="card">
						<div class="card-body">
							<div class="job_listing_left_side jb_cover">
								<%=strCareerjoblist %>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!--Accordion wrapper-->

		<div class="container">
			<div class="sec-title mb-20 mt-30 text-center">
				<h1 class="title mb-0">Walk in - For Immediate Placement </h1>
				<img src="<%=ResolveUrl("~/Hospital/assets/img/Icon_team.png") %>" alt="line" class="med_bottompadder20_4">
			</div>
			<div class="accordion md-accordion accordion-blocks" id="accordionEx78" role="tablist"
				aria-multiselectable="true">
				<%=strCareerjobwalkinlist %>
			</div>
		</div>
		<div class="home-intro  pb-50 pt-50" id="home-intro">
			<div class="container">
				<div class="row align-items-center">
					<div class="col-lg-8">
						<h1>Would you like to work with us?
							<br>
							Contact us and Submit your resume.
						</h1>
					</div>
					<div class="col-lg-4">
						<div class="get-started text-start text-lg-end">
							<a href="#" class="btn btn-dark btn-lg text-3 font-weight-semibold px-4 py-3" type="button" data-toggle="modal" data-target="#uploadcv" data-whatever="@mdo" data-original-title="" title="">UPLOAD CV</a>
						</div>
					</div>
				</div>

			</div>
		</div>
	</section>
	<!-- End About Section -->

	<!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
	<div class="modal fade" id="uploadcv" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
		aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel2">Can not find an opening? Leave us your resume!</h5>
					<button class="close" type="button" data-dismiss="modal" aria-label="Close" data-original-title=""
						title="">
						<span aria-hidden="true">×</span></button>
				</div>
				<div class="modal-body">

					<div class="row form-row">
						<div class="col-md-12">
							<div class="form-group text-center">
								<asp:Label ID="lblMessage" runat="server" Visible="false" Font-Bold="true"></asp:Label>
								<asp:Label ID="llblwarningmess" ForeColor="Red" runat="server" Visible="false" Font-Bold="true"></asp:Label>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Full Name" ValidationGroup="caree" onkeypress="return lettersWithSpaceOnly(event)"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvFullName" runat="server" ErrorMessage="Enter Full Name" ValidationGroup="caree" ForeColor="Red" ControlToValidate="txtFullName"></asp:RequiredFieldValidator>
								<%--<input type="text" class="form-control" placeholder="Full Name">--%>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Enter Email Address" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="caree"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="caree"
									CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
									Display="Dynamic" ErrorMessage="Invalid Email." />
								<%--<input type="text" class="form-control" placeholder="Email">--%>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile Number" onkeypress="return isNumberKey(event)"  MaxLength="15" ValidationGroup="caree"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ErrorMessage="Enter Mobile Number" ForeColor="Red" ControlToValidate="txtMobileNo" ValidationGroup="caree"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator ID="REFmoblie" runat="server" ValidationGroup="caree"
									ErrorMessage="Please enter valid mobile no" ControlToValidate="txtMobileNo" CssClass="validate" ValidationExpression="^[\d+]{0,15}$"></asp:RegularExpressionValidator>
								<%--<input type="text" class="form-control" placeholder="Mobile Number">--%>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Designation"  ValidationGroup="caree"></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Designation" ForeColor="Red" ControlToValidate="txtDesignation" ValidationGroup="caree"></asp:RequiredFieldValidator>
								<%--<input type="text" class="form-control" placeholder="Current Location">--%>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<asp:TextBox ID="txtlocation" runat="server" CssClass="form-control" placeholder="Location" ValidationGroup="caree"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvlocation" runat="server" ErrorMessage="Enter location" ForeColor="Red" ControlToValidate="txtlocation" ValidationGroup="caree"></asp:RequiredFieldValidator>
								<%--<input type="text" class="form-control" placeholder="Current Location">--%>
							</div>
						</div>
						<div class="col-lg-12">
							<div class="upload-img">
								<div class="change-photo-btn">
									<span><i class="fa fa-upload"></i>Upload Resume</span>
									<asp:FileUpload accept=".doc,.docx,.pdf" ID="cvfile" runat="server" TabIndex="2" CssClass="form-control upload" />
									<%--<input type="file" id="cvfile" runat="server" class="upload">--%>
								</div>
								<small class="form-text text-muted text-center">Allowed Only PDF File.</small>
							</div>
						</div>
						<div class="col-md-12 mt-15">
							<div class="form-group text-center">
								<asp:Button ID="btnCvSubmit" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Submit" OnClick="btnCvSubmit_Click" ValidationGroup="caree" />
								<%--<button class="btn btn-primary nextBtn btn-lg" type="button">Submit</button>--%>
							</div>
						</div>
					</div>
				</div>

			</div>
		</div>
	</div>
	<script type="text/javascript">
		function ShowPopupcareer() {
			$("#uploadcv").modal("show");
		}
	</script>
</asp:Content>
