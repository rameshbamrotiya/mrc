<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GovBodyMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.GovBodyMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Governing Board Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Governing Board</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Governing Board</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <div class="card">
        <div class="card-body">
            <asp:HiddenField ID="hfID" runat="server" />
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlLanguage">Language<span class="req-field">*</span></label>
                        <asp:DropDownList ID="ddlLanguage" CssClass=" form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtMetaTitle">Meta Title</label>
                        <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
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
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtAcademicDesc">Page Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtPageDescription" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtPageDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                    <%} %>
                    <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                        <i class="fa fa-remove">&nbsp;Cancel</i>
                    </button>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="form-group col-md-12">
                    <h3>List </h3>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:HiddenField ID="hfSubId" runat="server" />
                        <label for="txtPersonName">Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtPersonName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Designation Details<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtDesignationDetails" MaxLength="50" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--  <asp:DropDownList ID="ddlDesignationList" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%" ValidationGroup="main">
                        </asp:DropDownList>--%>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtShortDescription">Pic Upload</label>
                        <asp:FileUpload ID="fuPic" class="form-control" runat="server" ValidationGroup="Profile" />
                        <p class="help-block">( Image should be 300px*300px )</p>
                        <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hfLeftImage" runat="server" />
                        <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;color: #ffff;">&nbsp;Remove</a>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:CheckBox ID="chkIsActive" runat="server" />
                        <label for="txtShortDescription">Active</label>
                    </div>
                </div>
                <div class="col-md-4" style="margin-top: 5px;">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtSquanceNo" ValidationGroup="Profile"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-4" id="dvDrpSwapSequance" runat="server">
                    <div class="form-group">
                        <label for="drpChangeSequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                        <asp:DropDownList ID="drpChangeSequanceMethod" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                            <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" id="dvSwapSequance" runat="server">
                    <div class="form-group">
                        <label for="txtSwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtSwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile"
                            ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rgVSwapSequanseNo" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>

                <div class="col-md-3">    
                    <label>&nbsp;</label>                
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddToList" CssClass="btn btn-primary " Text="Add To List" OnClick="btnAddToList_Click" ValidationGroup="main" Style="margin-top: 7px;" />
                    </div>
                </div>
            </div>
            <hr />

            <div class="row">
                <div class="col-md-12" style="margin-top: 10px;">
                    <div class="form-group">
                        <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowSorting="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DesignatedPersonName" HeaderText="Name" SortExpression="DesignatedPersonName" />
                                <asp:BoundField DataField="DesignationName" HeaderText="Designation" SortExpression="DesignationName" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequence No" SortExpression="SequanceNo" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="IsActive" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("FullPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                { %>
                                            <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
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
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
