<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OurExcellenceIntroductionDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.OurExcellenceIntroductionDetails" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Our Excellence Introduction Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Our Excellence Introduction Details</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Our Excellence Introduction Details</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <h6 class="card-title">Introduction</h6>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtFirstName">HOD Name</label>
                                <asp:TextBox ID="txtHODName" CssClass=" form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtFirstName">HOD Designation</label>
                                <asp:TextBox ID="txtHODDesignation" CssClass=" form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtFirstName">HOD Photo</label>
                                <asp:FileUpload ID="fuHODImage" runat="server" />
                                <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" />

                            </div>
                        </div>
                        <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Introduction Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtIntoductiondesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtIntoductiondesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
                        </div>
                    </div>
                        <div class="col-md-12">
                              <%--<% if (SessionWrapper.UserPageDetails.CanAdd)
                                                { %>--%>
                            <asp:Button runat="server" ID="btnInformationSave" CssClass="btn btn-primary " Text="Save Information" OnClick="btnInformationSave_Click" />
                            <%--<%} %>--%>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
