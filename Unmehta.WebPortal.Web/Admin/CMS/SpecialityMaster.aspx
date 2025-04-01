<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SpecialityMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.SpecialityMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Specialty Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <style>
        #bodyPart_UpdatePanel1{
                display: contents;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Specialty Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Specialty Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="OS_id,OSLevelId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                <asp:BoundField DataField="ShortDesc" HeaderText="Short Description" SortExpression="ShortDesc" />
                                                <asp:BoundField DataField="OSLevelId" HeaderText="Sequence No." SortExpression="OSLevelId" />
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                                CommandArgument='<%# Eval("OS_id") + "," + Eval("OSLevelId") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                                CommandArgument='<%# Eval("OS_id") + "," + Eval("OSLevelId") + "," +   "down" %>'
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
                                            SelectCommand="[PROC_OtherSpecialityMasterdetails_Search]" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="Title" />

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
                                        <label for="exampleInputFile">Other Specialty Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtOSTitle" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtOSTitle" ValidationGroup="main"
                                            ErrorMessage="Please enter other specility title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactiveOS" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%" ValidationGroup="main">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Other Specificity Short Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtOSShortDesc" TextMode="MultiLine" TabIndex="1" runat="server" CssClass="form-control"  ValidationGroup="main"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvAwardShortDesc" CssClass="validationmsg" runat="server" ControlToValidate="txtOSShortDesc"  ValidationGroup="main"
                                            ErrorMessage="Please enter Other Specility Short Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Main Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="FUmainimg" runat="server" TabIndex="2" CssClass="form-control" OnDataBinding="FUmainimg_DataBinding" OnPreRender="FUmainimg_PreRender" />
                                        <p class="help-block">( Image should be 1200px*800px )</p>
                                        <asp:Label ID="lblMainImage" runat="server" Text=""></asp:Label> 
                                        <asp:HiddenField ID="hfMainImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblMainImage','bodyPart_aRemoveMain','bodyPart_hfMainImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveMain" runat="server" style="margin-left:5px; cursor:pointer;color:#ffff;">&nbsp;Remove</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Image Icon<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="Fuimgicon" runat="server" TabIndex="2" CssClass="form-control" />
                                        <p class="help-block">( Image should be 64px*64px )</p>
                                        <asp:Label ID="lblIcon" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfIcon" runat="server" />
                                        <a  onclick="return RemoveImage('bodyPart_lblIcon','bodyPart_aRemoveIcon','bodyPart_hfIcon');"  id="aRemoveIcon" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left:5px; cursor:pointer;color:#ffff;">&nbsp;Remove</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select inner Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="FUinnerimg" runat="server" TabIndex="2" CssClass="form-control" />
                                        <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfInnerImage" runat="server" />
                                        <a  onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInnerImage','bodyPart_hfInnerImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveInnerImage" runat="server" style="margin-left:5px; cursor:pointer;color:#ffff;">&nbsp;Remove</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Inner Video link<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtvideolink" TabIndex="1" runat="server" CssClass="form-control"  ValidationGroup="main"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtvideolink" CssClass="validationmsg" runat="server" ControlToValidate="txtvideolink"  ValidationGroup="main"
                                            ErrorMessage="Please enter inner video link." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtsequence" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Inner Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtosinnerdesc" TextMode="MultiLine" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtosinnerdesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                    <ContentTemplate>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="exampleInputFile">Is Statistics<span class="req-field">*</span></label>
                                                    <asp:DropDownList ID="ddlIsStatistics" CssClass="form-control" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlIsStatistics_SelectedIndexChanged" runat="server" ValidationGroup="main" Style="width: 100%">
                                                        <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" id="divStatistics" runat="server">
                                                <div class="form-group">
                                                    <label for="exampleInputFile">Statistics<span class="req-field">*</span></label>
                                                    <asp:DropDownList ID="ddlStatistics" CssClass="form-control" TabIndex="3" runat="server" ValidationGroup="main" Style="width: 100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-md-4">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <label for="exampleInputFile">Is Photos Visible<span class="req-field">*</span></label>
                                        <asp:CheckBox ID="chkIsPhoto" runat="server" Text=" " />
                                    </div>
                                </div>
                                <div class="row" id="divphoto" runat="server">
                                    <asp:HiddenField ID="hfRowId" Value="0" runat="server" />
                                    <asp:HiddenField ID="hfCommand" Value="0" runat="server" />
                                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                    <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                                        <div class="form-group">
                                            <p style="">You Can Add Multiple/Single popup and images using <i class="fa fa-plus-circle"></i>.</p>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Popup Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtPopupdesc" TextMode="MultiLine" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                            <script type="text/javascript">
                                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                var editor = CKEDITOR.replace('<%=txtPopupdesc.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                            </script>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtImgTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RfvtxtImgTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtImgTitle" ValidationGroup="Sub"
                                                ErrorMessage="Please image title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Short Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtshortdesc" TabIndex="1" TextMode="MultiLine" MaxLength="500" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Rfvtxtshortdesc" CssClass="validationmsg" runat="server" ControlToValidate="txtshortdesc" ValidationGroup="Sub"
                                                ErrorMessage="Please short description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                            <p class="help-block">( Image should be 600px*600px )</p>
                                            <asp:Label ID="lblfilesubimg" runat="server" Text=""></asp:Label> 
                                        <asp:HiddenField ID="hfimgesub" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblfilesubimg','bodyPart_removesubimg','bodyPart_hfimgesub');" class="fa fa-trash-o btn btn-primary"  id="removesubimg" runat="server" style="margin-left:5px; cursor:pointer;color:#ffff;">&nbsp;Remove</a>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnAdd" runat="server" Text="Save Sub Details" CssClass="fa fa-plus-circle btn btn-primary"  style="margin-top: 36px" OnClick="btnAdd_ServerClick" ValidationGroup="Sub" />
                                            <asp:Button ID="btnClear" CssClass="fa btn btn-primary" runat="server" Text="Clear" OnClick="btnClear_Click" style="margin-top: 36px" CausesValidation="false" />
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
                            </div>
                        </div>
                        <div class="row" id="divgrid" runat="server">
                            <div class="col-md-12">
                                <div class="form-group" style="overflow-x: scroll;">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ImageTitle" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                         OnRowCommand="GridView1_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ImageTitle" HeaderText="Title" SortExpression="ImageTitle" />
                                            <asp:BoundField DataField="ImageShortDesc" HeaderText="Imgshortdesc" SortExpression="ImageShortDesc" />
                                            <asp:BoundField DataField="ImgPopupDesc" HeaderText="Popupdesc" SortExpression="ImgPopupDesc" />
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Imagepath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                    <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Imagepath") %>'></a>
                                                    <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Imagepath") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Is_active" HeaderText="Is Active" SortExpression="Is_active" />
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
                                                        <asp:DropDownList ID="ddlIsDownload" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
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
                                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <%} %>
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
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" causesvalidation="false" title="Save" onserverclick="btn_Save_Click" ValidationGroup="main">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" causesvalidation="false" onserverclick="btn_Update_Click" ValidationGroup="main">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
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
