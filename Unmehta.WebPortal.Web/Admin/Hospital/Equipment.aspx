<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Equipment.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.Equipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Equipment</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Equipment</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Equipment</li>
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
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:HiddenField ID="hfRowId" runat="server" />
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Equipment Name</label>
                        <asp:TextBox ID="txtEquipmentName" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtEquipmentName.ClientID%>', {
                            extraPlugins: 'tableresize'
                            });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="fuFileUpload">Upload Image</label>
                        <asp:HiddenField ID="hfFilName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuFileUpload" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <label>&nbsp;</label>
                    <div class="form-group form-check form-control">                        
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label class="form-check-label" for="chkEnable">Active</label>
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
                <div class="col-md-12">
                    <div class="pull-right">
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <div class="input-group">
                            <a href="#" title="Add New" class="btn btn-primary" id="foo" style="color: white"><i class="fa fa-plus-square">&nbsp;Add New</i></a>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-md-12" id="gvInnerGridView" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="/Admin/Script/CMS/Equipment.js"></script>
</asp:Content>
