<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Facilites.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.Facilites" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Facilites</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Facilites</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Facilites</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Facilites Name</label>
                            <asp:TextBox ID="txtFacilitesName" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group form-check">
                            <br />
                            <br />
                            <asp:CheckBox ID="chkEnable" runat="server" />
                            <label class="form-check-label" for="chkEnable">Active</label>
                        </div>
                    </div>
                    <hr />
                    <div class="" runat="server" id="dvSubDEtails">

                        <asp:HiddenField ID="hfSubDetails" runat="server" />
                        <div class="col-md-12">
                            <h3>Image Details</h3>
                            <div class="form-group">
                                <label for="exampleInputFile">Facilites File Info</label>
                                <asp:TextBox ID="txtFacilitesImageInfo" ValidateRequestMode="Disabled" ValidationGroup="Profile" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                <script type="text/javascript">
                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                    var editor = CKEDITOR.replace('<%=txtFacilitesImageInfo.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                    });
                                </script>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="fuFileUpload">Upload Image</label>
                                <asp:HiddenField ID="hfFilName" runat="server" />
                                <asp:FileUpload CssClass="form-control" ID="fuFileUpload" runat="server" ValidationGroup="Profile" />
                                <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtSquanceNo" ValidationGroup="Profile"
                                    ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-3" id="dvDrpSwapSequance" runat="server">
                            <div class="form-group">
                                <label for="drpChangeSequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                                <asp:DropDownList ID="drpChangeSequanceMethod" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                                    <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="dvSwapSequance" runat="server">
                            <div class="form-group">
                                <label for="txtSwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtSwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile"
                                    ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rgVSwapSequanseNo" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group form-check">
                                <br />
                                <br />
                                <asp:CheckBox ID="chkImageEnable" ValidationGroup="Profile" runat="server" />
                                <label class="form-check-label" for="chkEnable">Active</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <br />
                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                { %>
                            <asp:Button runat="server" ID="btnAddDoctor" CssClass="btn btn-primary " Text="Save" ValidationGroup="Profile" OnClick="btnAddDoctor_Click" Style="margin-top: 7px;" />
                            <%} %>
                            <button runat="server" id="btnClearDetails" class="btn btn-inverse" title="Cancel" onserverclick="btnClearDetails_ServerClick" causesvalidation="false">
                                Clear
                            </button>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 10px; margin-left: 10px;">
                        <div class="form-group">
                            <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="false" PageSize="10" OnPageIndexChanging="gvDoctor_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FacilitesFileInfo" HeaderText="Facilites File Info" SortExpression="FacilitesFileInfo" />
                                    <asp:BoundField DataField="SequanceNo" HeaderText="Sequence No" SortExpression="SequanceNo" />
                                    <asp:BoundField DataField="IsVisible" HeaderText="Status" SortExpression="IsVisible" />
                                    <asp:TemplateField HeaderText="Image File">
                                        <ItemTemplate>
                                            <a id="afileq" href='<%# Eval("FacilitesFilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_DoctorEdit" CommandName="eEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_DoctorEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_DoctorDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_DoctorDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                { %>
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
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
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
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
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,LanguageId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FacilitesName" HeaderText="Facilites Name" SortExpression="FacilitesName" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
