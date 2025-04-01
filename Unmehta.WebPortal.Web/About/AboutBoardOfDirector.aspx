<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutBoardOfDirector.aspx.cs" Inherits="Unmehta.WebPortal.Web.AboutBoardOfDirector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
   UnMehta - Governing Board

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
					<li>Governing Board</li>
				</ul>
			</div>
		</div>
		<!-- /Breadcrumb -->
	<section class="content" style="min-height: 104.859px;">
			<div class="container">
				<div class="row">
					<div class="col-md-12 col-lg-12">
						<%=strBoard %>
					</div>
				</div>
			</div>
		</section>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
