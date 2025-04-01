<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OtherFacilities.aspx.cs" Inherits="Unmehta.WebPortal.Web.OtherFacilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Other Facilities
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
                <li><a href="#">Other Facilities</a></li>
            </ul>
        </div>
    </div>
    <div class="content">
			<div class="container">
				<div class="row">
					<!-- Doctor Details Tab -->
					<div class="col-lg-9">
						<div class="section-main-title">
							<h3 id="lblTitleH3" runat="server"></h3>
						</div>
						<!-- Location List -->
                        <%=strPageDetails %>
						<!-- /Location List -->
					</div>
					<div class="col-lg-3">
						<div class="sidebar">
							<div class="card  widget-categories">
								<div class="card-header">
									<h4 class="card-title"  id="lblTitleH4" runat="server"></h4>
								</div>
								<div class="card-body">
									<iframe width="100%" height="190"  id="iFreamURLH4" runat="server"  title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen=""></iframe>
									<img src="assets/img/ads.jpg"  id="imgURLH4" runat="server"  class="img-fluid" />
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
