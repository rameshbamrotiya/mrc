<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RighttoInformationMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.RighttoInformationMaste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Right To Information Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <%--Ref Awards--%>
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Right To Information Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Right To Information Master</li>
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
                <asp:Panel ID="pnlEntry" runat="server">
                    <asp:HiddenField ID="hfRIID" runat="server" />
                      <asp:HiddenField ID="hfRIMID" runat="server" />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                        InitialValue="0" ValidationGroup="frmMain"   ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Title</label>
                                        <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Description</label>
                                        <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">RTI Description</label>
                                        <asp:TextBox ID="txtRTIDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtRTIDescription.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                        <br />
                                    </div>
                                </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                         <asp:TextBox ID="txtDocTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" ValidationGroup="doc" runat="server" ControlToValidate="txtDocTitle"
                                                    ErrorMessage="Please enter Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select File [Only PDF/DOC/DOCX file type allowed]<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".pdf,.doc,.xls,.docx" ID="fuDocUpload" runat="server" CssClass="form-control" TabIndex="5" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle btn btn-primary " data-toggle="tooltip" style="cursor: pointer;" title="Add" id="bntAddDoc" runat="server" onserverclick="bntAddDoc_ServerClick" ValidationGroup="doc" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvDoc_RowDeleting" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnPageIndexChanging="gvDoc_PageIndexChanging"
                                        AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DocTitle" HeaderText="Title"  />
                                             <asp:TemplateField HeaderText="DocURL">
                                                <ItemTemplate>
                                                    <a id="afile" data-toggle="tooltip" title="Click to open" href='<%# Eval("DocURL") %>' target="_blank" runat="server"  class="fa fa-file-o" style="font-size:20px"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Link" HeaderText="Action" ShowDeleteButton="true"
                        DeleteText="<i class='fa fa-trash-o' style='font-size:20px;color:red'></i>" />
                                            
                                          <%--  <asp:TemplateField HeaderText="Download Y/N">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Image_desc", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}%>
                                    
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
