<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GenerateMeritNumber.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.GenerateMeritNumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Group Assign </title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Student Group Assign</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Student Group Assign</li>
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
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCourceList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourceList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <hr />
                <div class="col-md-6" id="dvDivColumnName" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlColumnList" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6" id="dvDivSearch" runat="server">
                    <div class="form-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
                            <i class="fa fa-search">&nbsp;Add Into Filter</i>
                        </button>
                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                            <i class="fa fa-remove">&nbsp;Clear Filter</i>
                        </button>
                    </div>
                </div>
                <hr />
                <%if (isFiltered)
                    { %>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="serachText form-control" placeholder="Group Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <button runat="server" id="btnSave" class="btn btn-primary" title="Assign Group Name" onserverclick="btnSave_ServerClick">
                        Assign Group Name
                    </button>
                </div>
                <hr />
                <%} %>
                <div class="col-md-12">
                    <div class="input-group">
                        <button runat="server" id="btnexport" class="btn btn-primary" visible="false" onserverclick="btnexport_ServerClick" title="Generate Excel File">
                            <i class="fa fa-plus-square">&nbsp;Generate Excel File</i>
                        </button>
                    </div>
                </div>
                <div class="col-md-12">
                    <asp:GridView ID="gSearchView" runat="server" AutoGenerateColumns="true" DataKeyNames="Row" OnRowCommand="gSearchView_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Row", "return confirm(\"Are you sure want to delete Row: {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <%--<asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" />--%><hr />
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="true" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" AllowSorting="false" OnSorting="gView_Sorting" EmptyDataText="Record does not exist...">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="Address1" HeaderText="Address1" />
                                <asp:BoundField DataField="Address2" HeaderText="Address2" />
                                <asp:BoundField DataField="PresentPincode" HeaderText="Pin Code" />
                                <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                <asp:BoundField DataField="PresentPhoneR" HeaderText="Phone(R)" />
                                <asp:BoundField DataField="PresentPhoneM" HeaderText="Phone(M)" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="DateOfBirth" HeaderText="Date Of Birth" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="Age" HeaderText="Age" />
                                <asp:BoundField DataField="CastName" HeaderText="Cast Name" />
                                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                <asp:BoundField DataField="MaritalStatus" HeaderText="MaritalStatus" />--%>
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
