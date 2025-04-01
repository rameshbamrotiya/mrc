<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResearchDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.ResearchDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Research Details
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
                <li><a href="<%=ResolveUrl("~/Research") %>">Research</a></li>
                <li>/</li>     
                <li><%=strPageName %></li>
            </ul>
        </div>
    </div>

    <!-- Page Content -->
    <!-- About Section -->
    <div class="content">
        <div class="container">
                <%--<asp:ScriptManager runat="server"></asp:ScriptManager>--%>
            <!-- Doctor Details Tab -->
            <!-- Tab Menu -->
         <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="row">
                        <div class="col-md-9 col-lg-9">
                            <!-- About Details -->
                            <div class="tab-content opdtiming pt-0">

                                <div class="row form-row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTitle" CssClass="form-control" placeholder="Title" runat="server" AutoPostBack="true" OnTextChanged="txtTitle_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAuthor" CssClass="form-control" placeholder="Author" runat="server" AutoPostBack="true" OnTextChanged="txtTitle_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlArticalType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtTitle_TextChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <%=strListOfSubSectionDescription %>
                                <!-- Content Column -->
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="sidebar">
                                <div class="card widget-categories">
                                    <div class="card-header">
                                        <h4 class="card-title">Quick Links</h4>
                                    </div>
                                    <div class="card-body">
                                        <ul class="categories nav nav-pills nav-stacked flex-column">

                                            <%=strRightsSideTabs %>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
           <%--     </ContentTemplate>
            </asp:UpdatePanel>--%>

            <!-- /Tab Menu -->
        </div>
    </div>
    <!-- End About Section -->
    <!-- /Page Content -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
