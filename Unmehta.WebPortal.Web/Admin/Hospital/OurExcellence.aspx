<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OurExcellence.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.OurExcellence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Department</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Department</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Department</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                    <div class="form-group">
                        <label for="ddlDepartment">Department</label>
                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDepartment"
                            ErrorMessage="Select department." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fuFileUpload">Image Upload</label>
                        <asp:FileUpload CssClass="form-control" ID="fuFileUpload" accept=".png,.jpg,.jpeg,.gif" runat="server" />
                        <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfLeftImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtMetaTitle">Side Image</label>

                        <asp:FileUpload CssClass="form-control" ID="fuSideImage" accept=".png,.jpg,.jpeg,.gif" runat="server" />
                     <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfRightImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveRight" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                   </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtMetaTitle">Side Image URL</label>
                        <asp:TextBox ID="txtSideImageURL" runat="server" CssClass="form-control" placeholder="Enter Side Image URL"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtMetaTitle">External Video Link</label>
                        <asp:TextBox ID="txtExternalVideoLink" runat="server" CssClass="form-control" placeholder="Enter External Video Link"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
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

                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtMetaTitle">Meta Title</label>
                        <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMetaTitle" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle" Display="Dynamic"
                            ErrorMessage="Enter Meta Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtMetaDescription">Meta Description</label>
                        <asp:TextBox ID="txtMetaDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter Meta Description"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txtMetaDescription" Display="Dynamic"
                            ErrorMessage="Enter Meta Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group form-check form-control">
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label for="chkEnable">Active</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group form-check form-control">
                        <asp:CheckBox ID="chkAddIn" runat="server" />
                        <label for="chkAddIn">Is Faculty Tab Visible</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group form-check form-control">
                        <asp:CheckBox ID="chkAddInOtherDepartment" runat="server" />
                        <label for="chkAddInOtherDepartment">Is Add in Other Department</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <%} %>
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card" id="divGrid" runat="server" visible="false">
        <div class="card-body">
            <div class="row">
                <div class="col-md-9" id="tblSearch">
                    <div class="form-group">
                        <div class=" controls">
                            <div class="input-group">
                                <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="SequanceNo" SortExpression="SequanceNo" />
                                
                                <asp:TemplateField HeaderText="Tab List">
                                    <ItemTemplate>
                                        <a id="aFileIntroductionEntry" href='<%# ResolveUrl("~/Admin/Hospital/DepartmentTabMaster?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("Id").ToString()+"|"+Eval("DepartmentId").ToString()) ) %>' target="_blank" runat="server" tooltip="Update Other Details" class=""> <i class="fa fa-edit"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField> 

                                <asp:BoundField DataField="IsVisible" HeaderText="Visable" SortExpression="IsVisible" />


                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# (Eval("FileFullPath")!=null? ResolveUrl((string)Eval("FileFullPath").ToString()):"#")  %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="/Admin/Script/Recruitment/OurExcellence.js"></script>
</asp:Content>
