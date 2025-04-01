<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ApplicationReceived.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.ApplicationReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CMS | Application Received</title>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Application Received</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">ApplicationReceived</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">            
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label style="font-weight:bold;">Post Name :</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlJobList" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <label style="font-weight:bold;">Is Final Submit :</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlFinalSubmit" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>&nbsp;&nbsp;</label>
                    <div class="form-group">
                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
                            <i class="fa fa-search">&nbsp;Search</i>
                        </button>
                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                            <i class="fa fa-remove">&nbsp;Cancel</i>
                        </button>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button ID="btnExportApplicationList" runat="server" CssClass="btn btn-primary right" OnClick="btnExportApplicationList_Click" Text="Export" Style="float: right;" />
                        <br />
                        <hr />
                        <asp:GridView ID="gvApplicationReceived" runat="server" AutoGenerateColumns="False" DataKeyNames="PostId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            DataSourceID="sqlds" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PostName" HeaderText="Post Name" />
                                <asp:TemplateField HeaderText="Total Application Received">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApplicationReceived" CommandName="eView" runat="server" data-original-title="View" ForeColor="#3c8dbc" CssClass="btn btn-sm show-tooltip" OnClick="lnkApplicationReceived_Click"><%# Eval("TotalAppRecevie") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle Font-Bold="true" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card" id="divCandidateDetails" runat="server" visible="false">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" /><hr />
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10" OnRowCommand="gView_RowCommand" OnPageIndexChanging="gView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RegisrationId" HeaderText="RegistarionId" />
                                <asp:BoundField DataField="PostName" HeaderText="Applied Post" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                                <asp:BoundField DataField="DateOfBirth" HeaderText="Date Of Birth" />
                                <asp:BoundField DataField="Nationality" HeaderText="Religion" />
                                <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Staus" />
                                <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                <asp:BoundField DataField="ParmenentAddress" HeaderText="Permanent Address" />
                                <asp:BoundField DataField="ParmenentPhoneM" HeaderText="Contact No" />
                                <asp:TemplateField HeaderText="Photograph Name">
                                    <ItemTemplate>
                                        <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("PhotographName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Signature Name">
                                    <ItemTemplate>
                                        <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("SignatureName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
