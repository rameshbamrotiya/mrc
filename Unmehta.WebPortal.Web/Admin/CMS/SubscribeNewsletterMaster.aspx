<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SubscribeNewsletterMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.SubscribeNewsletterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Subscribe Newsletter Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Subscribe Newsletter Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Subscribe Newsletter Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">
                <!-- Bootstrap alert -->
                <div class="row">
                    <div class="form-group col-md-12">
                        <div class="messagealert" id="alert_container">
                        </div>
                    </div>
                </div>
                <!-- END Bootstrap alert -->
                <asp:Panel ID="pnlView" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control pull-right datepicker-demo" placeholder="dd/mm/yyyy"></asp:TextBox>
                                    <asp:CompareValidator ID="cvDateFrom" ControlToValidate="txtDateFrom" ControlToCompare="txtDateTo" Display="Dynamic" ForeColor="Red"
                                        Operator="LessThanEqual" Type="Date" Text="End date must be less than To Date!" runat="Server" />
                                    <asp:RegularExpressionValidator ID="revDateFrom" runat="server" ControlToValidate="txtDateFrom" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" />
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control pull-right datepicker-demo" placeholder="dd/mm/yyyy"></asp:TextBox>
                                    <asp:CompareValidator ID="cvDateTo" ControlToValidate="txtDateTo" ControlToCompare="txtDateFrom" Display="Dynamic"
                                        ForeColor="Red" Text="End date must be greater than From Date!" Operator="GreaterThanEqual" Type="Date" runat="Server" />
                                    <asp:RegularExpressionValidator ID="revDateTo" runat="server" ControlToValidate="txtDateTo" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" />
                                </div>
                                <div class="col-md-3" id="tblSearch">
                                    <div class="form-group">
                                        <div class="controls">
                                            <div class="input-group">
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-primary" ForeColor="White" OnClick="lnkSearch_Click"><i class="fa fa-search">&nbsp;Search</i></asp:LinkButton>
                                                </span>
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_Click">
                                                        <i class="fa fa-remove">&nbsp;Cancel</i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right; margin-bottom: 2px;" />
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" PageSize="10" OnPageIndexChanging="gView_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr&nbsp;No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="EmailId" HeaderText="Email Address" SortExpression="EmailId" />
                                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_GetAllSubscribeNewsletterMaster]" SelectCommandType="StoredProcedure" FilterExpression="FullName like '%{0}%' OR EmailId like '%{0}%' OR MobileNo like '%{0}%'">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="FullName" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="EmailId" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="MobileNo" />
                                            </FilterParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
