<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Unmehta.WebPortal.Web.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    About Us
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
                <li>About Us</li>
            </ul>
        </div>
    </div>



    <section class="content" style="min-height: 103.859px;">
        <div class="aboutdata_content">
            <div class="container">
                <div class="row" >
                    <%=strMain %>
                   
                </div>
            </div>
        </div>
       <%-- <div class="awarddata_content">
            <div class="container">
                <div class="row">
                    <!-- Content Column -->
                    <div class="slider_content mt-30">
                        <div class="container">
                            <div class="row">
                                <div class="sec-title pb-10">
                                    <h3>Awards &amp; Recognition</h3>
                                </div>
                                <!-- Content Column -->
                                <div class="video-slider owl-theme owl-carousel owl-loaded" id="ulAward" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </section>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
