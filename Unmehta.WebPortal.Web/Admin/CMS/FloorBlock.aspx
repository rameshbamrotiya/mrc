<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FloorBlock.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.FloorBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Block Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Block Master</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Block Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-3" style="display: none;">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Block Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtBlockname" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBlockName" CssClass="validationmsg" runat="server" ControlToValidate="txtBlockname"
                                ErrorMessage="Enter block name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpstatus" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtsequence" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">

                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" />
                    <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                        <i class="fa fa-floppy-o">&nbsp;Update</i>
                    </button>
                    <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                        <i class="fa fa-remove">&nbsp;Cancel</i>
                    </button>
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
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control"></asp:TextBox>
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
                        <div class="form-group">
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" DataSourceID="sqlds" CssClass="table table-bordered table-hover table-striped" DataKeyNames="BlockID,Recid,LanguageID" EmptyDataText="Record does not exist..."
                                AllowSorting="false" OnRowCommand="grdDetails_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Sequencing">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                    CommandArgument='<%# Eval("recid") + "," + Eval("Block_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                    CommandArgument='<%# Eval("recid") + "," + Eval("Block_level_id") + "," +   "down" %>'
                                                    runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ibtn_Delete" OnClick="ibtn_Delete_Click" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("BlockName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllBlockDetails]" SelectCommandType="StoredProcedure" FilterExpression="BlockName like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="BlockName" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnRecid" runat="server" />
        <asp:HiddenField ID="hdnCatID" runat="server" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
