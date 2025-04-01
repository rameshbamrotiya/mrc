<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutHistory.aspx.cs" Inherits="Unmehta.WebPortal.Web.About.AboutHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - History
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
                <li><a href="<%=ResolveUrl("~/About/") %>">About</a></li>
                <li>/</li>
                <li>History</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->

    <!-- About Section -->
    <section class="content">
        <div class="container">
            <nav class="global-sidebar"></nav>
                        <%=strBoard %>

        </div>
    </section>
    <!-- End About Section -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
