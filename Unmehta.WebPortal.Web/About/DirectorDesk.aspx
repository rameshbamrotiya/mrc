<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectorDesk.aspx.cs" Inherits="Unmehta.WebPortal.Web.About.DirectorDesk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">    
    UnMehta - Director Desk
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
		.type2 {
			margin: 5px auto;
			border: 2px dotted #ccc;
			border-radius: 5px;
			padding: 20px;
			font-family: 'Lucida Handwriting Italic';
			font-weight: normal;
			font-size: 20px;
			text-align: justify;
			font-style: italic;
		}
	</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner">
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>From Director's Desk</li>
            </ul>
        </div>
    </div>

  <%--   <section class="page-title" style="background-image: url('<%= ResolveUrl("~/"+strHeaderImage) %>');">
        <div class="auto-container">
            <h1>About Us</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Director Desk</li>
            </ul>
        </div>
    </section>--%>

  <section class="content" style="min-height: 116.859px;">
			<div class="container">
				<div class="row">
					<!-- Content Column -->
					<!-- <div class="images-column col-lg-4 col-md-12 col-sm-12">
						<div class="inner-column">
							<figure class="image"><img src="assets/img/dr-slider.png" alt="" class="img-fluid"></figure>
						</div>
					</div> -->
					<div class="content-column col-lg-12 col-md-12 col-sm-12"  id="dvDesc" runat="server">
						<div class="inner-column">

						</div>	
					</div>
				</div>
			</div>
		</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
	<script src="<%= ResolveUrl("~/Hospital/assets/plugins/jquery.min.js")%>"></script>
	    <script src="<%= ResolveUrl("~/Hospital/assets/js/typetext.js")%>"></script>
   <script>
       $(function () {
           $(".type2").typeText();
			//$('.type2').typetext();
		});
	</script>
</asp:Content>
