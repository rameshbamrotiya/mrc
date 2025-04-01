<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TermsandConditions.aspx.cs" Inherits="Unmehta.WebPortal.Web.TermsandConditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
     UnMehta - Terms and Conditions
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
                <li>Terms and Conditions</li>
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
							<%--<div class="terms-text">
								<h3>Etiam blandit lacus</h3>
								<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
									et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
									aliquip.</p>
								<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
									et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
									aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum
									dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui
									officia deserunt mollit anim id est laborum.</p>
							</div>
							<div class="terms-text">
								<h4>Etiam sed fermentum lectus. Quisque vitae ipsum libero</h4>
								<p>Phasellus sit amet vehicula arcu. Etiam pulvinar dui libero, vitae fringilla nulla convallis in.
									Fusce sagittis cursus nisl, at consectetur elit vestibulum vestibulum:</p>
								<ul>
									<li>Nunc pulvinar efficitur interdum.</li>
									<li>Donec feugiat feugiat pulvinar.</li>
									<li>Suspendisse eu risus feugiat, pellentesque arcu eu, molestie lorem. </li>
									<li>Duis non leo commodo, euismod ipsum a, feugiat libero.</li>
								</ul>
							</div>
							<div class="terms-text">
								<h3>Etiam blandit lacus</h3>
								<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
									et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
									aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum
									dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui
									officia deserunt mollit anim id est laborum.</p>
								<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
									et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
									aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum
									dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui
									officia deserunt mollit anim id est laborum.</p>
							</div>
							<div class="terms-text">
								<h3>Maecenas sit amet</h3>
								<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore
									et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
									aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum
									dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui
									officia deserunt mollit anim id est laborum.</p>
							</div>--%>
						</div>
					</div>
				</div>
			</div>

		</div>
		<!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
