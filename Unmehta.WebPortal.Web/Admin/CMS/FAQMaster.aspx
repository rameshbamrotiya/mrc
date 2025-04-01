<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FAQMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.FAQMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>FAQ Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">FAQ Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/CMS/CK/ckeditor.js") %>"></script>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="4" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMetaTitle" TabIndex="2" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                ErrorMessage="Please meta title." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="3" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                ErrorMessage="Please enter meta description." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">FAQ Description<span class="req-field"></span></label>
                            <asp:TextBox ID="txtDescription" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" />
                    <%}
                        if (SessionWrapper.UserPageDetails.CanUpdate)
                        { %>
                    <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                        <i class="fa fa-floppy-o">&nbsp;Update</i>
                    </button>
                    <%} %>
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
                <div class="row" style="display: none;">
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
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick">
                                    <i class="fa fa-plus-square">&nbsp;Add new</i>
                                </button>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,FAQ_Id,Language_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                DataSourceID="sqlds" AllowSorting="false" OnPageIndexChanging="gView_PageIndexChanging" PageSize="10" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FAQs Description" ItemStyle-Width="100%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# HttpUtility.HtmlDecode( Eval("FAQDescription").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add Accredation Details">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkAddAccredationDetails" runat="server" data-original-title="AddTAccredationDetails" OnClick="lnkAddAccredationDetails_Click" OnClientClick="SetTarget();"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton Visible="false" ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Contribution?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[PROC_GetAllFAQMasterDetails]" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="Title" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdPKId" runat="server" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
</asp:Content>
