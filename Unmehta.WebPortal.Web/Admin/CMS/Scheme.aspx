<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Scheme.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.Scheme" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Scheme Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Scheme Master</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Scheme Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
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
                            <label for="exampleInputFile">Scheme Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSchemeName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSchemname" CssClass="validationmsg" runat="server" ControlToValidate="txtSchemeName"
                                ErrorMessage="Enter Scheme Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="exampleInputFile">Scheme Logo<span class="req-field">*</span></label>
                                <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuschemelogo" runat="server" TabIndex="2" CssClass="form-control" />
                                 <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                                 <asp:HiddenField ID="hfLeftImage" runat="server" />
                                 <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                 
                                <p class="help-block">( Image should be 52px*52px )</p>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Scheme Banner<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuSchemeBanner" runat="server" TabIndex="2" CssClass="form-control" />
                            <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfRightImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveRight" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                            
                            <p class="help-block">( Image should be 365px*289px )</p>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Location<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Contact Person<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtContPerson" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">HelpDesk Number<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtHelpDeskNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Website url<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtsequence" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Content Details<span class="req-field"></span></label>
                            <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="txtCastName">&nbsp;</label>
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
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdScheme" runat="server" DataSourceID="sqlds" AutoGenerateColumns="False" DataKeyNames="Schemeid,scheme_level_id,recid,Languageid" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                OnRowCommand="grdScheme_RowCommand" AllowSorting="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="scheme_level_id" HeaderText="Seq No." />
                                    <asp:BoundField DataField="Schemename" HeaderText="Scheme Name" />
                                    <asp:TemplateField HeaderText="Scheme Logo">
                                        <ItemTemplate>
                                            <a id="LogoFile" href='<%# Eval("SchemeLogo") %>' target="_blank" runat="server" tooltip="View Logo" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheme Banner">
                                        <ItemTemplate>
                                            <a id="BannerFile" href='<%# Eval("SchemeBanner") %>' target="_blank" runat="server" tooltip="View banner" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" />
                                    <asp:BoundField DataField="HelpDeskNO" HeaderText="Help DeskNo." />
                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                    <asp:BoundField DataField="WebsiteUrl" HeaderText="Web Url" />
                                     <asp:TemplateField HeaderText="Charts" >
                                        <ItemTemplate>
                                            <div>
                                                <a id="aFileIntroductionEntry" href='<%# ResolveUrl("~/Admin/CMS/SchemeCharts?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("Schemeid").ToString()) ) %>' target="_blank" runat="server" tooltip="Update Other Details" class=""><i class="fa fa-edit"></i></a>
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
                                                <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("ContactPerson", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                                <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                    CommandArgument='<%# Eval("Schemeid") + "," + Eval("scheme_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                    CommandArgument='<%# Eval("Schemeid") + "," + Eval("scheme_level_id") + "," +   "down" %>'
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
                                SelectCommand="[GetAllScheme]" SelectCommandType="StoredProcedure" FilterExpression="Schemename like '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="Schemename" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                            <%-- <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:ConString %>"
                                        SelectCommand="[tbl_Menu_MasterSelectAll]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnRecid" runat="server" />
        <asp:HiddenField ID="hdnSchemeID" runat="server" />

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
