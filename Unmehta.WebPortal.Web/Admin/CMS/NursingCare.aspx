<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="NursingCare.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.NursingCare" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Nursing Care</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Nursing Care</li>
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
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select MainImage<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fumainimage" runat="server" TabIndex="2" CssClass="form-control" />
                            <p class="help-block">( Image should be 8256px*5504px )</p>
                            <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfInnerImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInner','bodyPart_hfInnerImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveInner" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                            
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Description<span class="req-field"></span></label>
                            <asp:TextBox ID="txtDescription" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>');
                            </script>
                            <br />
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
                        <label>&nbsp;</label>
                        <div class="form-group">
                            <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" style="cursor: pointer;" title="Add" id="btnImage" runat="server" onserverclick="btnImage_ServerClick" />
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
                                            <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Img_path") %>'></a>
                                            <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Img_path") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="is_active" HeaderText="Is Active" SortExpression="is_active_tamd" />
                                    <asp:TemplateField HeaderText="Active/Inactive">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:DropDownList ID="ddlGrdActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                    <asp:ListItem Value="True" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" CausesValidation="true" />
                    <%}
                        if (SessionWrapper.UserPageDetails.CanUpdate)
                        { %>
                    <%--<button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                        <i class="fa fa-floppy-o">&nbsp;Update</i>
                    </button>--%>
                    <%} %>
                    <%--<button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                        <i class="fa fa-remove">&nbsp;Cancel</i>
                    </button>--%>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--  <asp:Panel ID="pnlView" runat="server">
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
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick" causesvalidation="false">
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
                            <asp:GridView ID="gvNursingCare" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,NursingCare_id,Language_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowSorting="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Nursing Care?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
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
    </asp:Panel>--%>
    <asp:HiddenField ID="hdPKId" runat="server" />
    <asp:HiddenField ID="hdNursingCareId" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
