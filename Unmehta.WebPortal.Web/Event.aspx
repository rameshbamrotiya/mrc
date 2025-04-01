<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="Unmehta.WebPortal.Web.Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Event 
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
                <li>Event</li>
            </ul>
        </div>
    </div>
    <!-- Page Content -->
    <!-- About Section -->
    <div class="content">
        <!-- Events Section Start -->
        <div class="rs-event home8-style1 event-bg  md-pt-70 md-pb-70 pb-50">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <!-- Overview Content -->
                        <div class="row justify-content-between">
                            <div class="col-lg-4">
                                <div class="search-widget">
                                    <div class="cardsearch">
                                        <form class="search-form">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtEventTitle" CssClass="form-control" placeholder="Title" runat="server"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <button runat="server" id="btn_SearchCancel" class="btn btn-primary" title="Search" onserverclick="btn_SearchCancel_ServerClick">
                                                        <i class="fa fa-search"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlEventtype" CssClass="select form-control" runat="server" OnSelectedIndexChanged="ddlEventtype_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <%=strListOfSubSectionDescription %>
                        </div>
                        <!-- /Overview Content -->
                    </div>
                </div>
            </div>
        </div>
        <!-- Events Section End -->
    </div>
    <!-- End About Section -->
    <!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
