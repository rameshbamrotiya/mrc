<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blogs.aspx.cs" Inherits="Unmehta.WebPortal.Web.Blogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Blogs
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
                <li>Blogs</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->

    <div class="content">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-4">
                    <div class="search-widget">
                        <div class="cardsearch">
                            <div class="search-form">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" placeholder="Search by Title..." class="form-control" runat="server"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button runat="server" id="btn_SearchCancel" class="btn btn-primary" title="Search" onserverclick="btn_SearchCancel_ServerClick">
                                            <i class="fa fa-search"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="select form-control" OnSelectedIndexChanged="btn_SearchCancel_ServerClick" AutoPostBack="true">
                            <asp:ListItem Text="Search News & Blog" Value=""></asp:ListItem>
                            <asp:ListItem Text="Blog" Value="Blog"></asp:ListItem>
                            <asp:ListItem Text="Media" Value="Media"></asp:ListItem>
                            <asp:ListItem Text="General Notice" Value="General Notice"></asp:ListItem>
                            <asp:ListItem Text="Notice" Value="Notice"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>

            </div>
            <div class="row justify-content-between">
                <div class="col-lg-4">
                    <div class="search-widget">
                        <div class="cardsearch">
                        </div>
                    </div>
                </div>

            </div>
            <div class="row clearfix" id="BlogListDetails" runat="server">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
