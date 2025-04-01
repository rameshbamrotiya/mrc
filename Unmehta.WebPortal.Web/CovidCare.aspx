<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CovidCare.aspx.cs" Inherits="Unmehta.WebPortal.Web.CovidCare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Covid Care
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
                <li>Covid Care</li>
            </ul>
        </div>
    </div>
    <section class="content">
        <div class="coronaabout">
            <div class="container">
                <div class="row">
                    <%=strCovidCareDetails %>                    
                </div>
            </div>
            <div class="shape-1">
                <img src="<%=ResolveUrl("~/Hospital/assets/img/prevention-shape-1.png")%>" alt="Image" />
            </div>
            <div class="shape-2">
                <img src="<%=ResolveUrl("~/Hospital/assets/img/prevention-shape-1.png")%>" alt="Image" />
            </div>
        </div>
        <div class="counter-area two pt-50 pb-50">
            <section class="watch-video">                
                <div class="video-main">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-6  col-md-6 col-sm-6">
                                <%=strCovidCareLeftVideo %>  
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <%=strCovidCareRightVideo %>                                  
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <div class="cornafaq pt-50 pb-50">
            <div class="container">
                <div class="section-title text-center pb-30">
                    <span class="top-title" id="spnTitlt" runat="server"></span>
                    <h2><asp:Label ID="lblFAQsTitle" runat="server"></asp:Label></h2>
                </div>
                <div class="row">
                    <%=strFAQsDetails %> 
                </div>
            </div>
            <div class="shape-3">
                <img src="<%=ResolveUrl("~/Hospital/assets/img/prevention-shape-1.png")%>" alt="Image" />
            </div>
            <div class="shape-4">
                <img src="<%=ResolveUrl("~/Hospital/assets/img/prevention-shape-1.png")%>" alt="Image" />
            </div>

        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
