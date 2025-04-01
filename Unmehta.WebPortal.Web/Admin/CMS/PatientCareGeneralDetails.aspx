<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PatientCareGeneralDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.PatientCareGeneralDetails" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Patient Care General Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Patient Care General Details</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Home Page</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Patient Care General Details</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/CMS/CK/ckeditor.js") %>"></script>
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
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="GeneralDetailsId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TabDescription" HeaderText="Description" SortExpression="TabDescription" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("GeneralDetailsId", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Tab Description<span class="req-field"></span></label>
                                        <asp:TextBox ID="txtTabDescription" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtTabDescription.ClientID%>');
                                        </script>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                                    <div class="form-group">
                                        <p style="">You Can Add Multiple/Single images using <i class="fa fa-plus-circle"></i>.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                        <p class="help-block">( Image should be 1200px*800px )</p>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="btnImage" runat="server" onserverclick="btnImage_ServerClick" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="grdImages" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" PageSize="10" OnPageIndexChanging="grdImages_PageIndexChanging" OnRowCommand="grdImages_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Img_path") %>' target="_blank" runat="server" tooltip="View image" class="fa fa-search-plus"></a>
                                                        <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# ResolveUrl( Eval("Img_path").ToString()) %>' target="_blank"></a>
                                                        <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# ResolveUrl( Eval("Img_path").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="is_active" HeaderText="Is Active" SortExpression="is_active_tamd" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">                                                            
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdPKId" runat="server" />
                    <asp:HiddenField ID="hdNursingCareId" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
