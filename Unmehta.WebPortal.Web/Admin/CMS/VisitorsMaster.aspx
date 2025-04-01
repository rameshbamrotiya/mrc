<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="VisitorsMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.VisitorsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Visitors Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Visitors</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Visitors</li>
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
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactiveOS" CssClass="form-control" TabIndex="2" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="txtMetaTitle">Meta Title</label>
                                        <asp:TextBox ID="txtMetaTitle" ValidationGroup="main" runat="server" TabIndex="3" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMetaTitle" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle" Display="Dynamic"
                                            ErrorMessage="Enter Meta Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label for="txtMetaDescription">Meta Description</label>
                                        <asp:TextBox ID="txtMetaDescription" ValidationGroup="main" TextMode="MultiLine" Rows="5" TabIndex="4" runat="server" CssClass="form-control" placeholder="Enter Meta Description"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txtMetaDescription" Display="Dynamic"
                                            ErrorMessage="Enter Meta Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Visiting Hours Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtVisitingHoursdesc" ValidationGroup="main" TextMode="MultiLine" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtVisitingHoursdesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Do’s & Don’ts Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtDDDesc" TextMode="MultiLine" ValidationGroup="main" runat="server" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtDDDesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="txtCastName">&nbsp;</label>
                                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                                            { %>
                                        <button runat="server" id="btn_Save" class="btn btn-primary " title="Save" onserverclick="btn_Save_Click" ><i class="fa fa-remove">&nbsp;Save</i></button>
                                        <%} %>
                                        <button runat="server" id="Button2" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                            <i class="fa fa-remove">&nbsp;Cancel</i>
                                        </button>
                                    </div>
                                </div>
                                <div class="row" id="divphoto" runat="server">
                                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                    <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                                        <div class="form-group">
                                            <p style="">Visitors Facilities.</p>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtImgTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="facility" ID="RfvtxtImgTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtImgTitle"
                                                ErrorMessage="Please image title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Image Icon<span class="req-field">*</span></label>
                                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuIcon" runat="server" TabIndex="2" CssClass="form-control" />
                                            <p class="help-block">( Image should be 64px*64px )</p>
                                             <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                                             <asp:HiddenField ID="hfInnerImage" runat="server" />
                                             <a onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInner','bodyPart_hfInnerImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveInner" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                            <asp:DropDownList ID="ddlActiveInactiveimg" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Popup Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtPopupdesc" ValidationGroup="facility" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                            <script type="text/javascript">
                                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                var editor = CKEDITOR.replace('<%=txtPopupdesc.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                            </script>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button runat="server" ID="btnAddToList" CssClass="btn btn-primary " Text="Add To List" OnClick="btnAddToList_Click" ValidationGroup="main" Style="margin-top: 7px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divgrid" runat="server">
                            <div class="col-md-12">
                                <div class="form-group" style="overflow-x: scroll;">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ImgTitle" HeaderText="Title" SortExpression="ImgTitle" />
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Iconpath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                    <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Iconpath") %>'></a>
                                                    <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Iconpath") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="IsActive" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" CommandName="eEdit" runat="server" OnClick="ibtn_Edit_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <%} %>
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" CausesValidation="false" ToolTip="Delete" OnClick="ibtn_Delete_Click" CommandName="eDelete" OnClientClick='<%# Eval("ImgTitle", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <br />
                    <%--<div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" causesvalidation="false" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>--%>

                </asp:Panel>
            </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
