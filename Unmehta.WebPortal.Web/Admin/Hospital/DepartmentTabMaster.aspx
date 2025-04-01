<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DepartmentTabMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.DepartmentTabMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Department Tab Master </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Department Tab Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Department Tab Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card" id="divForm" runat="server">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtBlogName">Tab Name</label>
                            <asp:TextBox ID="txtTabName" runat="server" CssClass="form-control" placeholder="Enter Tab name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvblogname" ForeColor="Red" runat="server" ControlToValidate="txtTabName" Display="Dynamic"
                                ErrorMessage="Enter Tab name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtBlogName">Parent Tab Name</label>
                            <asp:DropDownList ID="ddlTabMaster" CssClass="form-control" ValidationGroup="Profile" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtBlogName">Tab type</label>
                            <asp:DropDownList ID="ddlTabType" CssClass="form-control" ValidationGroup="Profile" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtSquanceNo" ValidationGroup="Profile"
                                ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                        </div>
                    </div>
                    <div class="col-md-4" id="dvDrpSwapSequance" runat="server">
                        <div class="form-group">
                            <label for="drpChangeSequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpChangeSequanceMethod" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                                <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4" id="dvSwapSequance" runat="server">
                        <div class="form-group">
                            <label for="txtSwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile"
                                ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rgVSwapSequanseNo" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                        </div>
                    </div>

                    <div class="col-md-1">
                        <div class="form-group form-check">
                            <br />
                            <br />
                            <asp:CheckBox ID="chkEnable" runat="server" />
                            <label class="form-check-label" for="chkEnable">Active</label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />

                            <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                                <i class="fa fa-floppy-o">&nbsp;Update</i>
                            </button>

                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                <i class="fa fa-remove">&nbsp;Cancel</i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class=" controls">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
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
                    <div class="col-md-3">
                        <div class="pull-right">
                            <div class="input-group">
                                <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick" causesvalidation="false">
                                    <i class="fa fa-plus-square">&nbsp;Add new</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdUser_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TabName" HeaderText="Tab Name" SortExpression="TabName" />
                                    <asp:BoundField DataField="TabTypeName" HeaderText="Tab Type Name" SortExpression="TabTypeName" />
                                    <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                                    <%--<asp:BoundField DataField="BlogDate" HeaderText="Blog Date" SortExpression="BlogDate" DataFormatString="{0:dd/MM/yyyy}" />--%>

                                    <asp:TemplateField HeaderText="Tab Details" >
                                        <ItemTemplate>
                                            <div style='<%# (Eval("TabVisable").ToString() == "False" )?"display:none;": "display:Block;" %>'>
                                                <a id="aFileIntroductionEntry" href='<%# ResolveUrl("~/Admin/Hospital/DepartmentTabDetailsEntry?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("Id").ToString()+"|"+Eval("OurExcId").ToString()+"|"+Eval("DepartmentId").ToString()) ) %>' target="_blank" runat="server" tooltip="Update Other Details" class=""><i class="fa fa-edit"></i><%#Eval("TabVisable").ToString() %></a>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ParentTabName" HeaderText="Parent Tab Name" SortExpression="ParentTabName" />
                                    <asp:BoundField DataField="IsVisable" HeaderText="Status" SortExpression="IsVisable" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>

                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
