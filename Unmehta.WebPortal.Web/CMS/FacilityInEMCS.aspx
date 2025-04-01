<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FacilityInEMCS.aspx.cs" Inherits="Unmehta.WebPortal.Web.CMS.FacilityInEMCS" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <%--SpecialityMaster--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Facility In EMCS</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Facility In EMCS</li>
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
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="FIEID,FIEMID" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%} %>
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
                                            SelectCommand="PROC_FacilityInECMSDetails_Search" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
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
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                    <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="exampleInputFile">Meta Title</label>
                                    <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="exampleInputFile">Meta Description</label>
                                    <asp:TextBox ID="txtMetaDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfFIEMID" runat="server" Value="" />
                            <asp:HiddenField ID="hfFIEMDID" runat="server" Value="0" />
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtSquanceNo">Title<span class="req-field">*</span></label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtType6AccordionTitle">Sub-Title</label>
                                    <asp:TextBox ID="txtSubtitle" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ValidationGroup="Accordion"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSubtitle" CssClass="validationmsg" runat="server" ControlToValidate="txtSubtitle" ValidationGroup="Accordion"
                                        ErrorMessage="Enter Sub Title" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="txtType2SubSquanceNo">Description<span class="req-field">*</span></label>
                                    <asp:TextBox ID="txtDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                    <script type="text/javascript">
                                        CKEDITOR.dtd.$removeEmpty['i'] = false;
                                        var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                            extraPlugins: 'tableresize'
                                        });
                                    </script>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtSquanceNo">Image Upload<span class="req-field">*</span></label>
                                    <asp:FileUpload ID="fuImg" runat="server" />
                                    <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfInnerImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInner','bodyPart_hfInnerImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveInner" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div>
                                        <label>&nbsp;&nbsp;</label>
                                    </div>
                                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-primary" Text="+ Add" OnClick="btnAdd_Click" ValidationGroup="Accordion" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gvFacilitydtl" runat="server" AutoGenerateColumns="False" DataKeyNames="FIEMDID,Subtitle" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnPageIndexChanging="gvFacilitydtl_PageIndexChanging" OnRowDeleting="gvFacilitydtl_RowDeleting"
                                        AllowPaging="true" PageSize="10" OnRowCommand="gvFacilitydtl_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Subtitle" HeaderText="Sub Title" SortExpression="Subtitle" />
                                            <asp:BoundField DataField="Description" HtmlEncode="false" HeaderText="Description" SortExpression="Description" />
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <a id="afile" data-toggle="tooltip" title="Click to open" href='<%# Eval("ImageUrl") %>' target="_blank" runat="server" class="fa fa-picture-o" style="font-size: 20px"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:CommandField Visible="false" ButtonType="Link" HeaderText="Action" ShowDeleteButton="true" DeleteText="<i class='fa fa-trash-o' style='font-size:20px;color:red'></i>" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                { %>
                            <button runat="server" id="btnFinal" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnFinal_Click">
                                <i class="fa fa-floppy-o">&nbsp;Save</i>
                            </button>
                            <%}

                                if (SessionWrapper.UserPageDetails.CanUpdate)
                                { %>
                            <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" causesvalidation="false" onserverclick="btn_Update_ServerClick" validationgroup="main">
                                <i class="fa fa-floppy-o">&nbsp;Update</i>
                            </button>
                            <%} %>
                                    
                                    
                                    
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
