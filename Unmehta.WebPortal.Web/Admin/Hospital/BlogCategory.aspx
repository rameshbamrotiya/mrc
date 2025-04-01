<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BlogCategory.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.BlogCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Blog Category</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Blog Category</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Blog Category</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card" id="divForm" runat="server">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile" 
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="fuFileUpload">Image Upload</label>
                            <asp:FileUpload CssClass="form-control" ID="fuFileUpload" runat="server" />
                            <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfLeftImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                             
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtBlogName">Blog Name</label>
                            <asp:TextBox ID="txtBlogName" runat="server" CssClass="form-control" placeholder="Enter blog name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvblogname" ForeColor="Red" runat="server" ControlToValidate="txtBlogName" Display="Dynamic"
                                ErrorMessage="Enter blog name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtBlogger">Blogger</label>
                            <asp:TextBox ID="txtBlogger" runat="server" CssClass="form-control" placeholder="Enter blogger"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBlogger" ForeColor="Red" runat="server" ControlToValidate="txtBlogger" Display="Dynamic"
                                ErrorMessage="Enter blogger." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label for="exampleInputFile">Blog Date</label>
                        <div class="input-group">                            
                            <asp:TextBox ID="txtBlogDate" autocomplete="off" runat="server" CssClass="form-control pull-right dtpicker"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvStartDate" CssClass="validationmsg" runat="server" ControlToValidate="txtBlogDate" Display="Dynamic"
                            ErrorMessage="Enter blog date" ForeColor="Red"  SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revstartDate" runat="server" ControlToValidate="txtBlogDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                            ErrorMessage="Invalid date format." Display="Dynamic" CssClass="validationmsg" SetFocusOnError="true" />
                    </div>                                      
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtMetaTitle">Meta Title</label>
                            <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMetaTitle" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle" Display="Dynamic"
                                ErrorMessage="Enter Meta Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>&nbsp;</label>
                        <div class="form-group form-control">                            
                            <asp:CheckBox ID="chkEnable" runat="server" />
                            <label for="chkEnable">Active</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtShortDescription">Short Description</label>
                            <asp:TextBox ID="txtShortDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter short description"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvShortDescription" ForeColor="Red" runat="server" ControlToValidate="txtShortDescription" Display="Dynamic"
                                ErrorMessage="Enter short description." SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                    <%--<div class="col-md-6">
                        <div class="form-group">
                            <label for="txtDescription">Long Description</label>
                            <asp:TextBox ID="txtDescription" aria-describedby="emailHelp" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Enter long description" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" ForeColor="Red" runat="server" ControlToValidate="txtDescription"
                                ErrorMessage="Enter long description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="fuImage">Long Description</label>
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
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                { %>
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                            <%} if (SessionWrapper.UserPageDetails.CanUpdate)
                                                { %>
                            <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                                <i class="fa fa-floppy-o">&nbsp;Update</i>
                            </button>
                            <%} %>
                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                <i class="fa fa-remove">&nbsp;Cancel</i>
                            </button>
                        </div>
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
                    <div class="col-md-12">
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,LanguageId,ImageName" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                 AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BlogName" HeaderText="Blog Name" SortExpression="BlogName" />
                                    <asp:BoundField DataField="Blogger" HeaderText="Blogger" SortExpression="RoleName" />
                                    <asp:BoundField DataField="BlogDate" HeaderText="Blog Date" SortExpression="BlogDate" DataFormatString = "{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="ShortDescription" HeaderText="Short Description" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="IsVisible" HeaderText="Status" SortExpression="isactive" />
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# (string.IsNullOrWhiteSpace(Eval("ImageName").ToString())?"": ResolveUrl(Eval("ImagePath").ToString())) %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/jquery/jquery-ui.min.js")%>"></script>
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/clockpicker/bootstrap-clockpicker.min.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //document.getElementById('<%= ddlLanguage.ClientID %>').removeAttribute('disabled');
            var $j = jQuery.noConflict();
            $j('#<%=txtBlogDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
</asp:Content>
