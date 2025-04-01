<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MedicalTourismAccordionMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.MedicalTourismAccordionMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MedicalTourismAccordion Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>MedicalTourismAccordion Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">MedicalTourismAccordion Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
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
                                                <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                                <div class="col-md-12" style="margin-left: 10px;">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="MTA_id,MTA_level_id,id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                <asp:BoundField DataField="MetaTitle" HeaderText="Meta Title" SortExpression="MetaTitle" />
                                                <asp:BoundField DataField="MetaDescription" HeaderText="Meta Description" SortExpression="MetaDescription" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                                CommandArgument='<%# Eval("MTA_id") + "," + Eval("MTA_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                                CommandArgument='<%# Eval("MTA_id") + "," + Eval("MTA_level_id") + "," +   "down" %>'
                                                                runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                            </asp:LinkButton>
                                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_MedicalTourismAccordion_Search]" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <asp:HiddenField ID="hfSubMTADetailsId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtTitle" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtTitle"
                                            ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtsequence" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                            ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                            ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:HiddenField ID="hfId" Value="0" runat="server" />
                        <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                            <div class="form-group">
                                <p style="">You Can Add Multiple/Single Accredation List using <i class="fa fa-plus-circle"></i>.</p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtSubTitle" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Price<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtPrice" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddl">MTA Type</label>
                                <asp:DropDownList ID="ddlMTAType" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAccredationYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlMTAType"
                                    ErrorMessage="Select MTA Type." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>                        
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="exampleInputFile">Description<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                <script type="text/javascript">
                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                    var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                        extraPlugins: 'tableresize'
                                    });
                                </script>
                            </div>
                        </div>
                        <div class="col-md-4" style="display: none;">
                            <div class="form-group">
                                <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                <p class="help-block">( Image should be 1200px*800px )</p>
                                <label visible="false" style="font-weight: normal;" id="filename" runat="server"></label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                    <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>&nbsp;</label>
                            <div class="form-group">
                                <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" style="cursor: pointer;" title="Add" id="btnSubDetailsAdd" runat="server" onserverclick="btnSubDetailsAdd_ServerClick" causesvalidation="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" style="overflow-x: scroll;">
                                <asp:GridView ID="subgridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,MTAId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" OnPageIndexChanging="subgridView_PageIndexChanging"
                                    OnRowCommand="subgridView_RowCommand" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SubTitle" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                                        <asp:BoundField DataField="MTAId" HeaderText="MTA Id" SortExpression="MTAId" />
                                        <asp:BoundField DataField="MTAType" HeaderText="MTA Type" SortExpression="MTAType" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:TemplateField HeaderText="Image" Visible="false">
                                            <ItemTemplate>
                                                <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Img_path") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Img_path") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Img_path" HeaderText="View" SortExpression="Img_path" />--%>
                                        <asp:BoundField DataField="Is_active" HeaderText="Status" SortExpression="Is_active" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <%} %>
                                                    <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" Visible="false"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                    <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("SubTitle", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btnFinalSave" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnFinalSave_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Final Save</i>
                                </button>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <button runat="server" id="Button1" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="Button1_ServerClick" causesvalidation="false">
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
