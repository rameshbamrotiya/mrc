<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MedicalTourism.aspx.cs" Inherits="Unmehta.WebPortal.Web.MedicalTourism" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Medical Tourism
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
                <li><a href="#">Medical Tourism</a></li>
            </ul>
        </div>
    </div>
    <!-- Page Content -->
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-lg-9 col-md-9">
                    <div class="blog-view">
                        <div class="blog blog-single-post">
                            
                            <%=strIntroduction %>
                            <br>
                            <%=strAccords%>
                            <!-- Location List -->
                            <%=strFacilities%>
                            <!-- /Location List -->
                        </div>
                    </div>

                </div>
                 <asp:Repeater runat="server" ID="dtlstDocument">
                    <ItemTemplate>
                        <div class="col-lg-3">
                            <div class="sidebar">
                                <div class="card widget-categories">
                                    <div class="card-header">
                                        <h4 class="card-title"> Medical Tourism</h4>
                                    </div>
                                    <br>
                                    <div class="card-body">
                                        <iframe width="100%" height="190" src="<%# Page.ResolveUrl( Eval("MTInnerVideolink").ToString())%>"
                                            title="YouTube video player" frameborder="0"
                                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                                            allowfullscreen></iframe>
                                        <br>
                                        <br>
                                        <img src="<%# Page.ResolveUrl( Eval("MTInnerImgpath").ToString())%>" class="img-fluid">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <%=strPackagesModels %>
</asp:Content>
