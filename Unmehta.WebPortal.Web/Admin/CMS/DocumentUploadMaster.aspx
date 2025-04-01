<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DocumentUploadMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.DocumentUploadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DocumentUpload Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>DocumentUpload Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">DocumentUpload Master</li>
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
                <asp:Panel ID="pnlView" runat="server">
                    <div class="row">
                        <div class="col-md-9" id="tblSearch">
                            <div class="form-group">
                                <div class="col-md-9 controls">
                                    <div class="input-group">
                                        <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                                <i class="fa fa-search">&nbsp;Search</i>
                                            </button>
                                        </span>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_Click">
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
                                    <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_Click" causesvalidation="false">
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
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Doc_Name" HeaderText="Document Name" SortExpression="Doc_Name" />
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("Doc_Path") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                 <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Doc_Path") %>'></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Doc_id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <%} %>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                    SelectCommand="[PROC_DocumentMaster_Search]" SelectCommandType="StoredProcedure" FilterExpression="Doc_Name like '%{0}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter ControlID="txtSearch" Name="Doc_Name" />

                                    </FilterParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Document Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtDocName" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtDocName"
                                            ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                    <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-9">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Document<span class="req-field">*</span></label>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:FileUpload ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvDocUpload" CssClass="validationmsg" ControlToValidate="fuDocUpload" runat="server" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please select any document." />
                                        </div>
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
                                    <%} if (SessionWrapper.UserPageDetails.CanUpdate)
                                        { %>
                                    <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                        <i class="fa fa-floppy-o">&nbsp;Update</i>
                                    </button>
                                    <%} %>
                                    &nbsp;
                                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>

            </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
     <script type="text/javascript">


        $('.copy_text').click(function (e) {
            e.preventDefault();
            var copyText = $(this).attr('href');
            document.addEventListener('copy', function (e) {
                e.clipboardData.setData('text/plain', copyText);
                e.preventDefault();
            }, true);
            document.execCommand('copy');
            console.log('copied text : ', copyText);
        });


    </script>
</asp:Content>
