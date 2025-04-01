<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientTestimonialDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.PatientTestimonialDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Patient Speak
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
					<li>Patient Speak</li>
				</ul>
			</div>
		</div>
		<!-- /Breadcrumb -->
		<!-- Page Content -->
		<!-- About Section -->
		<div class="content">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-12">
						<div class="row row-grid">
                            <%=strPatientDetails %>

						</div>
					</div>
					<!-- /Doctor Details Tab -->
				</div>
			</div>
		</div>
		<!-- End About Section -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <%=strPatientModel %>
</asp:Content>
