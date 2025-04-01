<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PopUpMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.PopUpMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>PopUp Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>PopUp Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">PopUp Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>PopUp Master
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">PopUp Master</li>
            </ol>
        </section>
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

                    <asp:Panel ID="pnlEntry" runat="server">
                        <asp:HiddenField ID="hfActiveView" Value="" runat="server"></asp:HiddenField>
                        <asp:MultiView ID="MultiView1" runat="server">

                            <div class="box" style="border-top: 0px!important">
                                <div class="box-header with-border">
                                    <h3 class="box-title"><b>PopUp Detail</b></h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <asp:View ID="View1" runat="server">

                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <label for="exampleInputFile">PopUp Details<span class="req-field">*</span></label>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                                        <script type="text/javascript">
                                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                            var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>', {
                                                                extraPlugins: 'tableresize'
                                                            });
                                                        </script>
                                                        <br />
                                                        <%--<CKEditor:CKEditorControl ID="CKEditorControl1" HtmlEncodeOutput="true" runat="server"></CKEditor:CKEditorControl>--%>
                                                        <asp:RequiredFieldValidator ID="rfvCKeditor" CssClass="validationmsg" runat="server" ControlToValidate="CKEditorControl1"
                                                            ErrorMessage="PopUp Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlActiveInactiveStatus" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 BtnGrp">
                                                <div class="form-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                        { %>
                                                    <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-primary" Text="Save" Font-Bold="True" />&nbsp;&nbsp;
                                                    <%} %>
                                                </div>
                                            </div>
                                        </asp:View>
                                    </div>
                                </div>
                            </div>
                        </asp:MultiView>
                    </asp:Panel>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
