<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="HeaderFooterMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.HeaderFooterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Header Footer Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Executive Leadership</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Home Page</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Header Footer Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <h4>Header Footer Details</h4>
            <div class="row">
                <asp:HiddenField ID="hfId" runat="server" />
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="<%=txtHeaderDetails.ClientID%>">Header Details</label>
                        <asp:TextBox ID="txtHeaderDetails" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtHeaderDetails.ClientID%>' ,{
                            extraPlugins: 'tableresize'
                            });
                            editor.allowedContent = '*(*)';

                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="<%=txtFooterDetails.ClientID%>">Footer Details</label>
                        <asp:TextBox ID="txtFooterDetails" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtFooterDetails.ClientID%>' ,{
                                extraPlugins: 'tableresize'
                            });

                            editor.allowedContent = '*(*)';
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="exampleInputFile">Header Logo<span class="req-field">*</span></label>
                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuHeaderLogo" runat="server" CssClass="form-control" />
                          <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                           <asp:HiddenField ID="hfLeftImage" runat="server" />
                           <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                          
                        <%--<p class="help-block">( Image should be 566px*260px )</p>--%>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="exampleInputFile">Image Logo<span class="req-field">*</span></label>
                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" CssClass="form-control" />
                         <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                           <asp:HiddenField ID="hfRightImage" runat="server" />
                           <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveRight" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                        <%--<p class="help-block">( Image should be 566px*260px )</p>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>

                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
