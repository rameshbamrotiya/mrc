<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AwardDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.About.AwardDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Award & Achievements
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner">
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Award & Achievements</li>
            </ul>
        </div>
    </div>

    <div class="content">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-9">
						<div class="tab-content opdtiming">
                            <%=strAwards %>

						</div>
					</div>
					<div class="col-lg-3">
						<div class="sidebar">
							<div class="card widget-categories">
								<div class="card-header">
									<h4 class="card-title">Quick Links</h4>
								</div>
								<div class="card-body">
									<ul class="categories nav nav-pills nav-stacked flex-column">
                                        <%=strAwardTab %>
										<%--<li>
											<a href="#tab_a" class="active" data-toggle="pill">Awards
											</a>
										</li>
										<li>
											<a href="#tab_b" data-toggle="pill">Achievements</a>
										</li>--%>
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
