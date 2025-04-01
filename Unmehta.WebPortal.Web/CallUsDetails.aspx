<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CallUsDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.CallUsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
     UnMehta - Call Us
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
                <li><a href="<%=ResolveUrl("~/About/") %>">Call Us</a></li>
            </ul>
        </div>
    </div>
    <section class="content">
        <div class="container">
            <div class="container">
                <div class="card">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="section-main-title mt-20 ml-35">
                                <h2>Dial For Assistance</h2>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class=" search-widget">
                                <div class="card-body">
                                    <form class="search-form">
                                        <div class="input-group">
                                            <%--<input type="text" placeholder="Search..." class="form-control" />--%>
                                            <asp:TextBox ID="txtSerarch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                            <div class="input-group-append">
                                                <button runat="server" id="btn_SearchCancel" class="btn btn-primary" title="Search" onserverclick="btn_SearchCancel_ServerClick"><i class="fa fa-search">&nbsp;Search</i></button>
                                            </div>
                                            <div class="input-group-append ml-10">
                                                <button runat="server" id="btnReset" class="btn btn-primary" title="Search" onserverclick="btnReset_ServerClick"><i class="fas fa-sync-alt">&nbsp;Reset</i></button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <%=strVisionMission %>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
