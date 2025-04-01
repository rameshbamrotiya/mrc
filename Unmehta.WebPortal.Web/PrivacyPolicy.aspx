<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrivacyPolicy.aspx.cs" Inherits="Unmehta.WebPortal.Web.PrivacyPolicy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - PrivacyPolicy
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
                <li>PrivacyPolicy</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->
     <!-- Page Content -->
		<div class="content">
			<div class="container">
				<div class="row">
					<div class="col-12">
						<div class="terms-content">
                             <%=strTermsandConditions %>
						</div>
					</div>
				</div>
			</div>

		</div>
		<!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
