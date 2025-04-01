<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Rights.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Rights Manager </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">

    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>User Rights Manager</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Admin</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">User Rights Manager</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="content-wrapper">



        <!-- Main content -->
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
                    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:Panel ID="pnlEntry" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Role<span class="req-field">*</span></label>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <asp:DropDownList ID="drpRole" CssClass="form-control" runat="server" Style="width: 100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9" style="display: none;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Parent menu<span class="req-field">*</span></label>
                                            </div>
                                        </div>
                                        <div class="col-md-8" style="display: none;">
                                            <div class="form-group">
                                                <asp:DropDownList ID="drpParentMenu" CssClass="form-control" runat="server" Style="width: 100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12 BtnGrp">
                                        <div class="form-group">
                                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                { %>
                                            <button runat="server" id="btnFetch" class="btn btn-primary" onserverclick="btnFetch_ServerClick" title="Fetch">
                                                <i class="fa fa-floppy-o">&nbsp;Fetch</i>
                                            </button>
                                            <%} %>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive" id="divRights" runat="server">
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <th>Page Name
                                        </th>
                                        <th>Page URL
                                        </th>
                                        <th>
                                            <asp:CheckBox ID="chkbview" runat="server" Text="View" AutoPostBack="true" EnableViewState="true"
                                                OnCheckedChanged="chkbView_CheckedChanged" />
                                        </th>
                                        <th>
                                            <asp:CheckBox ID="chkbadd" runat="server" Text="Add" AutoPostBack="true" EnableViewState="true"
                                                OnCheckedChanged="chkbAdd_CheckedChanged" />
                                        </th>
                                        <th>
                                            <asp:CheckBox ID="chkbupdate" runat="server" Text="Update" AutoPostBack="true" EnableViewState="true"
                                                OnCheckedChanged="chkbupdate_CheckedChanged" />
                                        </th>
                                        <th>
                                            <asp:CheckBox ID="chkbdelete" runat="server" Text="Delete" AutoPostBack="true" EnableViewState="true"
                                                OnCheckedChanged="chkbdelete_CheckedChanged" />
                                        </th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptUserRights" OnItemDataBound="rptUserRights_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="display: none">
                                                    <asp:Label ID="lblparent" Visible="False" runat="server" Text='<%# Eval("col_parent_id") %>'
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="display: none">
                                                    <asp:Label ID="lblmenuid" Visible="False" runat="server" Text='<%# Eval("col_menu_id") %>'
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="first" runat="server" Text='<%# HttpUtility.HtmlDecode(Eval("col_menu_name").ToString()) %>'
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <a id='<%# Eval("col_menu_id") %>' href='../Pages/<%# Eval("col_menu_url") %>'>
                                                        <%# Eval("col_menu_url") %></a>
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk1_OnCheckedChanged"
                                                        ID="chk1" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk2_OnCheckedChanged"
                                                        ID="chk2" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk3_OnCheckedChanged"
                                                        ID="chk3" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk4_OnCheckedChanged"
                                                        ID="chk4" />
                                                </td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptUserRightschild" OnItemDataBound="rptUserRightschild_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="display: none">
                                                            <asp:Label ID="lblparent" Visible="False" runat="server" Text='<%# Eval("col_parent_id") %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td style="display: none">
                                                            <asp:Label ID="lblmenuid" Visible="False" runat="server" Text='<%# Eval("col_menu_id") %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="second" runat="server" Text='<%# "-->" + HttpUtility.HtmlDecode(Eval("col_menu_name").ToString()) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <a id='<%# Eval("col_menu_id") %>' href='../Pages/<%# Eval("col_menu_url") %>'>&nbsp;&nbsp;
                                                <%# Eval("col_menu_url") %></a>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk11_OnCheckedChanged"
                                                                ID="chk1" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk21_OnCheckedChanged"
                                                                ID="chk2" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk31_OnCheckedChanged"
                                                                ID="chk3" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk41_OnCheckedChanged"
                                                                ID="chk4" />
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rptUserRightschild1" OnItemDataBound="rptUserRightschild1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblparent" Visible="False" runat="server" Text='<%# Eval("col_parent_id") %>'
                                                                        Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblmenuid" Visible="False" runat="server" Text='<%# Eval("col_menu_id") %>'
                                                                        Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="third" runat="server" Text='<%#"---->" + HttpUtility.HtmlDecode(Eval("col_menu_name").ToString()) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a id='<%# Eval("col_menu_id") %>' href='../Pages/<%# Eval("col_menu_url") %>'>&nbsp;&nbsp;
                                                        <%# Eval("col_menu_url") %></a>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk12_OnCheckedChanged"
                                                                        ID="chk1" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk22_OnCheckedChanged"
                                                                        ID="chk2" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk32_OnCheckedChanged"
                                                                        ID="chk3" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="chk42_OnCheckedChanged"
                                                                        ID="chk4" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" onserverclick="btn_Save_ServerClick" title="Save">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%} %>
                                &nbsp;
                                          
                            </div>
                        </div>
                    </div>

                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
