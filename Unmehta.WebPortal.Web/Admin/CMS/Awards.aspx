<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Awards.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.Awards" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Award & Achievements Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Award & Achievements </h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Award & Achievements </li>
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
                                <div class="col-md-9" id="tblSearch">
                                    <div class="form-group">
                                        <div class="controls">
                                            <div class="input-group">
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
                                        </div>

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
                                <div class="col-md-12" style="overflow-x: scroll;">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Award_id,Award_level_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Album_name" HeaderText="Award Name"/>
                                                <asp:BoundField DataField="Type" HeaderText="Award or Achievements"/>
                                                <asp:BoundField DataField="AwardShortdesc" HeaderText="Award Description"/>
                                                <asp:BoundField DataField="Award_level_id" HeaderText="Sequence No."/>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Album_name", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                                CommandArgument='<%# Eval("Award_id") + "," + Eval("Award_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                                CommandArgument='<%# Eval("Award_id") + "," + Eval("Award_level_id") + "," +   "down" %>'
                                                                runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_AwardMasterdetails_Search]" SelectCommandType="StoredProcedure" FilterExpression="Album_name like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="Album_name" />

                                            </FilterParameters>
                                        </asp:SqlDataSource>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Award Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAlbumName" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtAlbumName"
                                            ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Type<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactivealbum" CssClass="form-control" TabIndex="2" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Award Short Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAwardShortDesc" TextMode="MultiLine" Rows="10" runat="server" TabIndex="4"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtAwardShortDesc.ClientID%>');
                                        </script>
                                        <br />
                                        <%--<asp:RequiredFieldValidator ID="rfvAwardShortDesc" CssClass="validationmsg" runat="server" ControlToValidate="txtAwardShortDesc"
                                            ErrorMessage="Please enter Award Short Description." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-4" style="visibility: hidden; display: none;">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Award Month Year<span class="req-field">*</span></label>
                                        <div class="row">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control col-6">
                                                <asp:ListItem Text="Select Month" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Jan" Value="Jan"></asp:ListItem>
                                                <asp:ListItem Text="Feb" Value="Feb"></asp:ListItem>
                                                <asp:ListItem Text="Mar" Value="Mar"></asp:ListItem>
                                                <asp:ListItem Text="Apr" Value="Apr"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="May"></asp:ListItem>
                                                <asp:ListItem Text="Jun" Value="Jun"></asp:ListItem>
                                                <asp:ListItem Text="Jul" Value="Jul"></asp:ListItem>
                                                <asp:ListItem Text="Aug" Value="Aug"></asp:ListItem>
                                                <asp:ListItem Text="Sep" Value="Sep"></asp:ListItem>
                                                <asp:ListItem Text="Oct" Value="Oct"></asp:ListItem>
                                                <asp:ListItem Text="Nov" Value="Nov"></asp:ListItem>
                                                <asp:ListItem Text="Dec" Value="Dec"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAwardYear" runat="server" MaxLength="4" TextMode="Number" CssClass="form-control col-6"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <%--<div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtMetaTitle">Meta Title</label>
                                        <asp:TextBox ID="txtMetaTitle" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMetaTitle" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle" Display="Dynamic"
                                            ErrorMessage="Enter Meta Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtMetaDescription">Meta Description</label>
                                        <asp:TextBox ID="txtMetaDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter Meta Description"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txtMetaDescription" Display="Dynamic"
                                            ErrorMessage="Enter Meta Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>

                                <div class="col-md-12" style="visibility: hidden; display: none;">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Award Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtalbumdesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtalbumdesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtalbumdesc"
                                            ErrorMessage="Please enter Award Description." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-12" style="visibility: hidden; display: none;">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Accredation Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAccredationDesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtAccredationDesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" CssClass="form-control" TabIndex="5" />
                                        <p class="help-block">( Image should be 595px*470px )</p>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle btn btn-primary " data-toggle="tooltip" style="cursor: pointer;" title="Add" id="Button1" runat="server" onserverclick="btnAdd_ServerClick" />
                                    </div>
                                </div>

                                <div style="visibility: hidden; display: none;">
                                    <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                                        <div class="form-group">
                                            <p style="">You Can Add Multiple/Single images using <i class="fa fa-plus-circle"></i>.</p>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                            <p class="help-block">( Image should be 595px*470px )</p>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <button class="fa fa-plus-circle" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" />
                                        </div>
                                    </div>--%>
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtsequence" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtMetaTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                                ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                                ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>--%>
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
                                                <asp:ImageField DataImageUrlField="Image_desc" HeaderText="Photo" ControlStyle-Height="50" ControlStyle-Width="50">
                                                </asp:ImageField>
                                                <%--                                            <asp:BoundField DataField="Language_id" HeaderText="Language" SortExpression="Language_id" />--%>
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Image_desc") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                        <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Image_desc") %>'></a>
                                                        <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Image_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:BoundField DataField="Is_active" HeaderText="Is Active" SortExpression="Is_active" />
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
                                                --%>
                                                <asp:TemplateField HeaderText="Download Y/N">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Image?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="ibtn_Delete" OnClick="ibtn_Delete_Click" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Image_desc", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>--%>
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
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="6" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="7" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="8" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">


        $('.copy_text').click(function (e) {
            e.preventDefault();
            var copyText = $(this).attr('href');
            document.addEventListener('copy', function (e) {
                e.clipboardData.setData('text/plain', copyText);
                e.preventDefault();
            }, true);
            document.execCommand('copy');
            console.log('copied text : ', copyText);
        });


    </script>
</asp:Content>
