<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ICUOnWheel.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.ICUOnWheel" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    ICU On Wheel Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>ICU On Wheel Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">ICU On Wheel Master</li>
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
                <asp:HiddenField ID="hfMainDetailsId" runat="server" value="0"/>
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtAcademicsName">ICU On Wheel Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtICUOnWheelName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtICUOnWheelName" ValidationGroup="main" ErrorMessage="Enter ICU On Wheel Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="form-group col-md-12">
                        <h3>Sub List </h3>
                    </div>
                    <asp:HiddenField ID="hfSubId" runat="server" />
                    <asp:HiddenField ID="hfSubIndexId" runat="server" />

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtSubTitle">ICU On Wheel Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSubTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtSubTitle" ValidationGroup="submain" ErrorMessage="Enter Sub Title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtShortDescription">Image Upload</label>
                            <asp:FileUpload ID="fuSubList" class="form-control-file" runat="server" ValidationGroup="submain" />
                            
                            <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfLeftImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtSubDescription">Sub Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSubDescription" TextMode="MultiLine" Rows="4" runat="server" ValidationGroup="submain" CssClass="form-control"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtSubDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <br />
                        <asp:Button runat="server" ID="btnSubDetails" CssClass="btn btn-primary " Text="Submit Sub Details" ValidationGroup="submain" OnClick="btnSubDetails_Click" Style="margin-top: 7px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-left: 10px;">
                        <div class="form-group">
                            <asp:GridView ID="gvSubDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SubTitle" HeaderText="Sub Title" SortExpression="SubTitle" />
                                    <asp:BoundField DataField="SubDescription" HeaderText="Sub Description" SortExpression="SubDescription" />
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# Eval("ImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_SubmitSubDetailsEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_SubmitSubDetailsEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <% }
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    {%>
                                                <asp:LinkButton ID="ibtn_SubmitSubDetailsDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_SubmitSubDetailsDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <% } %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label for="txtCastName">&nbsp;</label>
                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                            { %>
                        <asp:Button runat="server" ID="btn_Update" CssClass="btn btn-primary " Text="Update" OnClick="btn_Save_Click" ValidationGroup="main" />
                        <% }
                            if (SessionWrapper.UserPageDetails.CanAdd)
                            {%>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" ValidationGroup="main" />
                        <% } %>
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
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtAcademicDesc">Academics Description<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                                <script type="text/javascript">
                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                    var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                    });
                                </script>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button runat="server" ID="btnMainPageDescription" CssClass="btn btn-primary " Text="Save Main Description" OnClick="btnMainPageDescription_Click" ValidationGroup="main1" />
                        </div>

                        <div class="row">
                            <div class="form-group col-md-12">
                                <h3>Image List </h3>
                            </div>
                            <asp:HiddenField ID="hfSubImageId" runat="server" />
                            <asp:HiddenField ID="hfIndexId" runat="server" />

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtShortDescription">Pic Or Video Upload</label>
                                    <asp:FileUpload ID="fuPic" class="form-control" runat="server" ValidationGroup="Profile" />
                                    <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <br />
                                <asp:Button runat="server" ID="btnSubmitImage" CssClass="btn btn-primary " Text="Submit Image" ValidationGroup="Profile" OnClick="btnSubmitImage_Click" Style="margin-top: 7px;" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="margin-left: 10px;">
                                <div class="form-group">
                                    <asp:GridView ID="gvSubmitImage" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View File">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblImageName" Text='<%#Eval("ImageName") %>' Visible="false"></asp:Label>
                                                    <a id="afile" href='<%# Eval("ImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <%if (SessionWrapper.UserPageDetails.CanDelete)
                                                            {%>
                                                        <asp:LinkButton ID="ibtn_SubmitImageDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_SubmitImageDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        <% } %>
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
                <br />
                <br />
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
                                <% } %>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12" style="margin-left: 10px;">
                        <div class="form-group">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowDataBound="grdUser_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ICUName" HeaderText="ICU Name" SortExpression="ICUName" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <% }
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    {%>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <% }%>
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
</asp:Content>
