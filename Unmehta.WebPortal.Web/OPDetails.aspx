<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OPDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.OPDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - OPD Details
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
                <li>OPD Details</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->
    <div class="content">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-9">
						<div class="card">
							<div class="card-body pt-0">
								<div class="tab-content opdtiming">
                                    <%=strOPD %>
								</div>
							</div>
						</div>
					</div>
					<div class="col-lg-3">
						<div class="sidebar">
							<div class="card widget-categories">
								<div class="card-header">
									<h4 class="card-title">OPD Timings</h4>
								</div>
								<div class="card-body">
									<ul class="categories nav nav-pills nav-stacked flex-column">
                                        <%=strOPDtabs %>
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
</asp:Content>
