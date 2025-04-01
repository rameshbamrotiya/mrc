<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GalleryDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.GalleryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Gallary List
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
                <li><a href="<%=ResolveUrl("~/Gallery") %>">Gallery</a></li>
                <%--<li><a href="<%=ResolveUrl("~/Gallery") %>"><i class="fas fa-home"></i>&nbsp;/ Gallery</a></li>--%>
                <li>/</li>
                <li>Gallery Details</li>
            </ul>
        </div>
    </div>

    <div class="content">
        <div class="rs-degree style1 modify gray-bg">
            <div class="container">
                <div class="section-main-title">
                    <%=strGalleryTitle %>
                </div>                
                <div class="row mt-4  ltn__gallery-style-2">
                    <!-- gallery-item -->
                    <%=strGalleryDetails %>
                    <!-- gallery-item -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
