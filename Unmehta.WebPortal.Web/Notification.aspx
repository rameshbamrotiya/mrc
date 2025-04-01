<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="Unmehta.WebPortal.Web.Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Notification
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Notification</li>
            </ul>
        </div>
    </div>
    <div class="content">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-9">
						<div class="tab-content opdtiming">
							<div class="tab-pane active" id="tab_a">
								<div class="section-main-title">
									<h3>All News &amp; Updates</h3>
								</div>
								<!-- About Details -->
								<div class="widget review-listing">
									<ul class="comments-list">
										<!-- Comment List -->
										<%=strNews %>
										<!-- /Comment List -->
									</ul>
								</div>
								<!-- /Clinic Timing -->
							</div>
							<div class="tab-pane" id="tab_b">
								<div class="section-main-title">
									<h3>Tender Notices</h3>
								</div>
								<div class="widget review-listing">
									<ul class="comments-list">
										<!-- Comment List -->
										<%=strTenders %>
										<!-- /Comment List -->
									</ul>
								</div>
							</div>
							<div class="tab-pane" id="tab_c">
								<div class="section-main-title">
									<h3>Career Announcements</h3>
								</div>
								<div class="widget review-listing">
									<ul class="comments-list">
										<!-- Comment List -->
                                        <%=strCareerAnnouncements %>
									</ul>
								</div>
							</div>
						</div>
					</div>
					<div class="col-lg-3">
						<div class="sidebar">
							<div class="card widget-categories">
								<div class="card-header">
									<h4 class="card-title">Notifications</h4>
								</div>
								<div class="card-body">
									<ul class="categories nav nav-pills nav-stacked flex-column">
										<li>
											<a href="#tab_a" class="active" data-toggle="pill">All News &amp; Updates
											</a>
										</li>
										<li>
											<a href="#tab_b" data-toggle="pill">Tender Notices</a>
										</li>
										<li>
											<a href="#tab_c" data-toggle="pill">Career Announcements</a>
										</li>
									</ul>
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
</asp:Content>
