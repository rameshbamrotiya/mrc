<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DirectorMessageMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.DirectorMessageMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DirectorMessage Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>DirectorMessage</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">DirectorMessage</li>
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
                    <div class="row">
                        <div class="col-md-9" id="tblSearch">
                            <div class="form-group">
                                <%--<div class="col-md-9 controls">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>

                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                                <i class="fa fa-search">&nbsp;Search</i>
                                            </button>
                                        </span>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_Click">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                                        </span>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="pull-right">
                                <div class="input-group">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_Click" causesvalidation="false">
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
                            <div class="form-group">

                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="DMId" DataSourceID="SqlDataSource1"
                                    CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="True" AllowSorting="false" OnRowCommand="gView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("DOCPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-file"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="enabled" HeaderText="Status" ReadOnly="True" SortExpression="enabled" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <%}
                                                        if (SessionWrapper.UserPageDetails.CanDelete)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Remove" CommandName="eDelete" runat="server" data-original-title="Remove" OnClientClick='<%# Eval("DMId", "return confirm(\"Are you sure want to delete ? \")") %>' CssClass="btn btn-sm show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <%} %>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>" SelectCommand="GetAllDirectorMessageMasterSearch" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlEntry" runat="server">

                    <asp:HiddenField ID="hfActiveView" Value="" runat="server"></asp:HiddenField>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <div class="box" style="border-top: 0px !important">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>DirectorMessage Detail</b></h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <asp:View ID="View1" runat="server">
                                        <div class="row">
                                            <asp:HiddenField ID="hfDMId" Value="0" runat="server" />
                                            <div class="col-md-3">
                                                <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                                    ErrorMessage="Language Details" SetFocusOnError="true" Display="Dynamic" ValidationGroup="FormDetail"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                                    <asp:TextBox ID="txtMetaTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                                        ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                                    <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                                        ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none;">
                                                <div id="internalvideo" runat="server">
                                                    <div class="form-group">
                                                        <label for="exampleInputFile">Select Video<span class="req-field">*</span></label>
                                                        <asp:FileUpload ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                                        <label visible="false" style="font-weight: normal;" id="filename" runat="server"></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="cbEnable" runat="server" />
                                                    <label for="exampleInputFile">Active</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="exampleInputFile">DirectorMessage Details<span class="req-field">*</span></label>
                                                <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                                <script type="text/javascript">
                                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                    var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>', {
                                                        extraPlugins: 'tableresize'
                                                    });
                                                </script>
                                                <br />
                                                <%--<CKEditor:CKEditorControl ID="CKEditorControl1"  runat="server"></CKEditor:CKEditorControl>--%>
                                                <%--<asp:RequiredFieldValidator ID="rfvCKeditor" CssClass="validationmsg" runat="server" ControlToValidate="CKEditorControl1"
                                                    ErrorMessage="DirectorMessage Details" SetFocusOnError="true" Display="Dynamic" ValidationGroup="FormDetail"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                    { %>
                                                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-primary" Text="Save" Font-Bold="True" ValidationGroup="FormDetail" />&nbsp;&nbsp;
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-primary" Text="Update" Font-Bold="True" ValidationGroup="FormDetail" />&nbsp;&nbsp;
                                                <%} %>
                                                <asp:Button ID="btn_Cancel" runat="server" CausesValidation="false" OnClick="btn_Cancel_ServerClick" CssClass="btn btn-inverse" Text="Cancel" Font-Bold="True" />
                                            </div>
                                        </div>
                                    </asp:View>
                                </div>
                            </div>
                        </div>
                    </asp:MultiView>
                </asp:Panel>

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
