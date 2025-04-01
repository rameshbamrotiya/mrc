<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StudentPaymentPendingStatus.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.StudentPaymentPendingStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Payment Pending Status</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Apply Student List</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Student Payment Pending Status</li>
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
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCourceList" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
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
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <%--<asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" />--%><hr />
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentId,CourseId,FirstName,RegistrationId,TxnId,amount,TransactionDate" CssClass="table table-bordered table-hover table-striped" OnRowDataBound="gView_RowDataBound" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RegistrationId">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="lnkPriview_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus fa-lg"></i><%# Eval("RegistrationId") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TxnId" HeaderText="TxnId" />
                                <asp:BoundField DataField="amount" HeaderText="Amount" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                <asp:BoundField DataField="CourseName" HeaderText="CourseName" />
                                <asp:BoundField DataField="Mobile" HeaderText="Contact No" />
                                <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" />
                                <%--<asp:BoundField DataField="PhotographName" HeaderText="Photograph Name" SortExpression="Doc_Name" />--%>
                              <%--  <asp:TemplateField HeaderText="Photograph Name">
                                    <ItemTemplate>
                                        <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("PhotographName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="SignatureName" HeaderText="Signature Name" SortExpression="Doc_Name" />--%>
                               <%-- <asp:TemplateField HeaderText="Signature Name">
                                    <ItemTemplate>
                                        <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("SignatureName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Application Status Links" Visible="false">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkPriview" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="lnkPriview_Click"><i class="fa fa-print"></i></asp:LinkButton>
                                         </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ibtn_Download" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_Download_Click" OnClientClick="SetTarget();"><i class="fa fa-download fa-lg"></i></asp:LinkButton>
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
