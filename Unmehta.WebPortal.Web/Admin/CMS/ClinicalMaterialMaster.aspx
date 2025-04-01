<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ClinicalMaterialMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.ClinicalMaterialMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Clinical Material Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Clinical Material Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Clinical Material Master</li>
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
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfDesignationId" runat="server" />
                <asp:HiddenField ID="hfAboutUsId" runat="server" />
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
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
                        <label for="exampleInputFile">Description</label>
                        <asp:TextBox ID="txtDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group form-control">
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label for="chkEnable">Active</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">                        
                        <%
                            if (SessionWrapper.UserPageDetails.CanAdd)
                            {%>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
