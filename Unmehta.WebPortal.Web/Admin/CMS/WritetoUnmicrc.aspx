<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="WritetoUnmicrc.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.WritetoUnmicrc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Write to Unmicrc Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Write to Unmicrc Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Write to Unmicrc</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Write to Unmicrc Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class="controls">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                            <i class="fa fa-search">&nbsp;Search</i>
                                        </button>
                                    </span>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                                            <i class="fa fa-remove">&nbsp;Cancel</i>
                                        </button>
                                    </span>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right; margin-bottom: 2px;" />
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10" DataSourceID="sqlds">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                <asp:BoundField DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                <asp:BoundField DataField="FeedbackDescription" HeaderText="FeedbackDescription" SortExpression="FeedbackDescription" />
                                <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                            SelectCommand="GetAllFeedbackMasterIsUnmicrc" SelectCommandType="StoredProcedure" FilterExpression="FullName like'%{0}%' or EmailId like'%{0}%' or MobileNo like'%{0}%' or Country like'%{0}%' or State like'%{0}%' or City like'%{0}%' or FeedbackDescription like'%{0}%' or EntryDate like'%{0}%' ">
                            <FilterParameters>
                                <asp:ControlParameter ControlID="txtSearch" Name="FullName" />
                                <asp:ControlParameter ControlID="txtSearch" Name="EmailId" />
                                <asp:ControlParameter ControlID="txtSearch" Name="MobileNo" />
                                <asp:ControlParameter ControlID="txtSearch" Name="Country" />
                                <asp:ControlParameter ControlID="txtSearch" Name="State" />
                                <asp:ControlParameter ControlID="txtSearch" Name="City" />
                                <asp:ControlParameter ControlID="txtSearch" Name="FeedbackDescription" />
                                <asp:ControlParameter ControlID="txtSearch" Name="EntryDate" />
                            </FilterParameters>
                        </asp:SqlDataSource>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
