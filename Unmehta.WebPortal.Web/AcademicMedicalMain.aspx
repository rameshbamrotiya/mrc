<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicMedicalMain.aspx.cs" Inherits="Unmehta.WebPortal.Web.AcademicMedicalMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Academic Medical
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
                <li><a href="<%=ResolveUrl("~/Academic") %>">Academic</a></li>
                <li>/</li>
                <li><a href="#">Academic Medical</a></li>
            </ul>
        </div>

    </div>
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-lg-8">
                    <div class="section-main-title">
                        <h2>Academic Medical List</h2>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="search-widget">
                        <div class="cardsearch">
                            <div class="search-form">
                                <div class="input-group">
                                    <input type="text" placeholder="Search..." class="form-control" runat="server" id="txtSearch" />
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="LinkButton" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ><i class="fa fa-search"></i></asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <%=strAcademicTabDetails %>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="alert alert-primary alert-dismissible fade show" role="alert">
                        <%=strParaMedicalDescription %>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
