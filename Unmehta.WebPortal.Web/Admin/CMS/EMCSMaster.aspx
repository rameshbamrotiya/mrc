<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EMCSMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.EMCSMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Emergency Cardiac Medical Services Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Emergency Cardiac Medical Services</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Emergency Cardiac Medical Services</li>
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
                <asp:HiddenField ID="hdnLmt" runat="server" Value="2" />
                <!-- END Bootstrap alert -->
                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                    SelectCommand="PROC_EMCSMasterdetails_Search" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                <div class="col-md-6" style="display:none;">
                                    <div class="form-group">
                                        <label for="exampleInputFile">EMCS Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtemcsname" autocomplete="off" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtemcsname"
                                            ErrorMessage="Please enter EMCS name." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-6" style="display:none;">
                                    <div class="form-group">
                                        <label for="exampleInputFile">EMCS Sequence No.<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtsequenceno" autocomplete="off" TabIndex="2" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtsequenceno"
                                            ErrorMessage="Please enter description." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Statistics<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlstatistics" CssClass="form-control" OnSelectedIndexChanged="ddlstatistics_SelectedIndexChanged" AutoPostBack="true" TabIndex="4" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" id="Statisticsname" runat="server">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Statistics Name<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlstatisticsname" CssClass="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">EMCS Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtemcsdesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtemcsdesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Image File</label>
                                        <asp:FileUpload ID="fuImg" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-primary" Text="+ Add" OnClick="btnAdd_Click" ValidationGroup="Accordion" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvImg" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnPageIndexChanging="gvImg_PageIndexChanging" OnRowDeleting="gvImg_RowDeleting"
                                    AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemTemplate>
                                                <a id="afile" data-toggle="tooltip" title="Click to open" href='<%# Eval("ImgURL") %>' target="_blank" runat="server" class="fa fa-picture-o" style="font-size: 20px"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Link" HeaderText="Action" ShowDeleteButton="true"
                                            DeleteText="<i class='fa fa-trash-o' style='font-size:20px;color:red'></i>" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <%-- <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>--%>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%--<%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>--%>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%--<%} %>--%>
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
