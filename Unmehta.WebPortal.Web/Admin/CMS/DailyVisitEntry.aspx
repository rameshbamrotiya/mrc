<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DailyVisitEntry.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.DailyVisitEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Daily Visit Entry</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Daily Visit Entry</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Daily Visit Entry</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfId" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Entry Type <span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtShortDescription">Entry Title</label>
                            <div class="form-check ">
                                <asp:TextBox ID="txtTitle" runat="server" CssClass=" form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtShortDescription">Entry Date</label>
                            <div class="form-check ">
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass=" form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtShortDescription">Visit Count</label>
                            <div class="form-check ">
                                <asp:TextBox ID="txtVisitCount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>&nbsp;</label>
                        <div class="form-group form-control">
                            <asp:CheckBox ID="chkIsActive" runat="server" />
                            <label for="txtShortDescription">Active</label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuImage" runat="server" CssClass="form-control" />
                            <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfLeftImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" id="aRemoveLeft" runat="server" class="fa fa-trash-o btn btn-primary"  style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Select PDF Upload<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".pdf" ID="fuPDFUpload" runat="server" CssClass="form-control" />
                            <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfRightImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" id="aRemoveRight" runat="server" class="fa fa-trash-o btn btn-primary"  style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>

                    <%--<div class="col-md-12">
                        <div class="form-group">
                            <label for="txtAffilationName">Affilation Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>');
                            </script>
                        </div>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtCastName">&nbsp;</label>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                        <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                            <i class="fa fa-remove">&nbsp;Cancel</i>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class=" controls">
                                <div class="input-group">
                                    <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowSorting="false" PageSize="10" OnPageIndexChanging="grdUser_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="DailyCatagoryName" HeaderText="DailyCatagoryName" SortExpression="DailyCatagoryName" />
                                    <asp:BoundField DataField="EntryName" HeaderText="Title" SortExpression="EntryName" />
                                    <asp:BoundField DataField="VisitCount" HeaderText="VisitCount" SortExpression="VisitCount" />
                                    <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EntryDate" />
                                    <asp:BoundField DataField="IsVisable" HeaderText="Active" SortExpression="IsVisable" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <% }
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>

        $(document).ready(function () {
            ClosePreloder();
            var StartDate = document.getElementById('<%=txtEntryDate.ClientID%>');
            CreateDatePicker(StartDate);

        });
    </script>
</asp:Content>
