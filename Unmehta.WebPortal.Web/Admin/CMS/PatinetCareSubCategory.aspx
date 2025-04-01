<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PatinetCareSubCategory.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.PatinetCareSubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Patient Care Sub Category Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Patient Care Sub Category Master</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Patient Care Sub Category Master</li>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label for="exampleInputFile">Category<span class="req-field">*</span></label>
                        <asp:DropDownList ID="drpCategory" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Sub Category Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtCategory" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCategory" CssClass="validationmsg" runat="server" ControlToValidate="txtCategory"
                                ErrorMessage="Enter sub Category Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpstatus" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtVisionMissionName">PatinetCare Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                    <div class="form-group">
                        <p style="">You Can Add Multiple/Single images using <i class="fa fa-plus-circle"></i>.</p>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Image Upload<span class="req-field">*</span></label>
                            <asp:FileUpload ID="fuImageUpload" runat="server" TabIndex="2" CssClass="form-control" accept=".png,.jpg,.jpeg,.gif" />
                            <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Image Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtimgtitle" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtCategory"
                                ErrorMessage="Enter sub Category Name" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <button class="fa fa-plus-circle" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" />
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none;">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactiveimg" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none;">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlisddownload" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="True" Selected="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="False" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Img_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="false" OnRowCommand="GridView1_RowCommand" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ImageField DataImageUrlField="ImagePath" HeaderText="Photo" ControlStyle-Height="50" ControlStyle-Width="50">
                                    </asp:ImageField>
                                    <asp:BoundField DataField="ImageTitle" HeaderText="Image Title" SortExpression="ImageTitle" />
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("ImagePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("ImagePath") %>'></a>
                                            <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("ImagePath") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="is_active_tamd" HeaderText="Is Active" SortExpression="is_active_tamd" />
                                    <asp:BoundField DataField="is_download" HeaderText="Is Download" SortExpression="is_download" />
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
                                    <asp:TemplateField HeaderText="Download Y/N">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:DropDownList ID="ddlIsdownload" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
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
                                                <asp:LinkButton ID="ibtn_Delete" CausesValidation="false" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("ImageTitle", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                    <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" DataSourceID="sqlds" CssClass="table table-bordered table-hover table-striped" DataKeyNames="SubCategoryID,categoryid,Recid,LanguageID" EmptyDataText="Record does not exist..."
                                AllowSorting="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="SubCategoryname" HeaderText="Sub Category" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="categoryname" HeaderText="Category" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ibtn_Delete" OnClick="ibtn_Delete_Click" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("SubCategoryname", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllPatientSubCareCategory]" SelectCommandType="StoredProcedure" FilterExpression="Categoryname like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="Categoryname" />

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
