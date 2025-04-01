<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CareerCVDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.CareerCVDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Career CV Details Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Career CV Details Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Career CV Details</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Career CV Details Master</li>
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
                    <div class="col-md-9" id="tblSearch" runat="server" visible="false">
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
                    <div class="col-md-12 ">
                        <div class="form-group row">
                            <div class="col-md-4">
                                <%--<label for="txtStartDate">Start Date</label>--%>
                                <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter start date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <%--<label for="txtEndDate">End Date</label>--%>
                                <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter end date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <span class="input-group-btn">
                                    <button runat="server" id="btndatesearch" class="btn btn-primary" title="Search" onserverclick="btndatesearch_ServerClick">
                                        <i class="fa fa-search">&nbsp;Search</i>
                                    </button>
                                </span>
                                <span class="input-group-btn">
                                    <button runat="server" id="btndatecancle" class="btn btn-inverse" title="Cancel" onserverclick="btndatecancle_ServerClick">
                                        <i class="fa fa-remove">&nbsp;Cancel</i>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div style="text-align: end;">
                            <span class="input-group-btn">
                                <button runat="server" id="btnexportfile" class="btn btn-primary" title="Search" onserverclick="btnexportfile_ServerClick">
                                    <i class="fa fa-download">&nbsp;Export</i>
                                </button>
                                <button runat="server" id="btnExportToExcel" class="btn btn-primary" onserverclick="btnExportToExcel_ServerClick" title="Export To Excel">
                                    <i class="fa fa-file-excel-o">&nbsp;Export To Excel</i>
                                </button>
                            </span>
                        </div>
                        <br />
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10" OnPageIndexChanging="grdUser_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                                <asp:BoundField DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("FilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-file"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--<asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                            SelectCommand="GetAllCareerCVFiles" SelectCommandType="StoredProcedure" FilterExpression="FullName like'%{0}%' or EmailId like'%{0}%' or MobileNo like'%{0}%' or Location like'%{0}%' or EntryDate like'%{0}%' ">
                            <FilterParameters>
                                <asp:ControlParameter ControlID="txtSearch" Name="FullName" />
                                <asp:ControlParameter ControlID="txtSearch" Name="Designation" />
                                <asp:ControlParameter ControlID="txtSearch" Name="EmailId" />
                                <asp:ControlParameter ControlID="txtSearch" Name="MobileNo" />
                                <asp:ControlParameter ControlID="txtSearch" Name="Location" />
                                <asp:ControlParameter ControlID="txtSearch" Name="EntryDate" />
                            </FilterParameters>
                            <SelectParameters>                               
                                <asp:Parameter Name="startdate" DbType="String" />
                                <asp:Parameter Name="enddate" DbType="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>--%>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>
        $(document).ready(function () {
            ClosePreloder();
            var StartDate = document.getElementById('bodyPart_txtStartDate');
            var EndDate = document.getElementById('bodyPart_txtEndDate');
            CreateFromToDatePicker(StartDate, EndDate);
        });
    </script>
</asp:Content>
