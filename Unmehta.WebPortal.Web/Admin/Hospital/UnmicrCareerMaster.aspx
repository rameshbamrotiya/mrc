<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UnmicrCareerMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.UnmicrCareerMaster" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Unmicr Career Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
     <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Unmicrc Career Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Unmicr Career Master</li>
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
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile" 
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtBlogName">Why Join Title</label>
                            <asp:TextBox ID="txtWhyJoinTitleName" runat="server" CssClass="form-control" placeholder="Enter Why Join Title"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvblogname" ForeColor="Red" runat="server" ControlToValidate="txtWhyJoinTitleName" Display="Dynamic"
                                ErrorMessage="Enter Why Join Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>               
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="fuImage">Why Join Description</label>
                            <asp:TextBox ID="txtWhyJoinDescription"  runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtWhyJoinDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtBlogName">Grove That Title</label>
                            <asp:TextBox ID="txtGroveThatTitle" runat="server" CssClass="form-control" placeholder="Enter Grove That Title"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ControlToValidate="txtGroveThatTitle" Display="Dynamic"
                                ErrorMessage="Enter Grove That Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>               
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="fuImage">Grove That Description</label>
                            <asp:TextBox ID="txtGroveThatDescription"  runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtGroveThatDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtBlogName">Employee Care Title</label>
                            <asp:TextBox ID="txtEmployeeCareTitle" runat="server" CssClass="form-control" placeholder="Enter Employee Care Title"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txtEmployeeCareTitle" Display="Dynamic"
                                ErrorMessage="Enter Grove That Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>               
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="fuImage">Employee Care Description</label>
                            <asp:TextBox ID="txtEmployeeCareDescription"  runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtEmployeeCareDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>                 
                    <div class="col-md-3">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                          
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
