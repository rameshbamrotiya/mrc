<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EducationDocumentMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.EducationDocumentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Education Qualification Document Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Education Qualification Document</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Education Qualification Document</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">
                <asp:Panel ID="pnlView" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <div class="form-group">
                                        <div class="controls">
                                            <div class="input-group">
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
                                <div class="col-md-12" style="margin-left: 10px;">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowCommand="gView_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr&nbspNo." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                                                <asp:BoundField DataField="EducationDetailName" HeaderText="Name" SortExpression="EducationDetailName" />
                                                <asp:BoundField DataField="DocumentName" HeaderText="Document Name" SortExpression="DocumentName" />
                                                <asp:BoundField DataField="isrequired" HeaderText="Document Mandatory" SortExpression="isrequired" />
                                                <asp:BoundField DataField="IsVisible" HeaderText="Is Visible" SortExpression="IsVisible" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("EducationDetailName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                </asp:Panel>
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Course<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" TabIndex="1" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlType"
                                            ErrorMessage="Select course name" InitialValue="Select" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Education Type<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlEducationType" CssClass="form-control" TabIndex="2" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEducationType" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlEducationType"
                                            ErrorMessage="Select dducation type" InitialValue="Select" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Document Name<span class="text-danger">*</span></label>
                                        <asp:TextBox autocomplete="off" ID="txtName" TabIndex="3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdvertisementName" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Enter document name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Document <span class="text-danger">*</span></label>
                                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" TabIndex="4" ID="RBLdocument">
                                            <asp:ListItem Text="Mandatory" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="Not Mandatory" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvDocument" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="RBLdocument"
                                            ErrorMessage="Select document" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
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
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="7" title="Save" onserverclick="btn_Save_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="7" title="Update" onserverclick="btn_Update_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="8" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
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
