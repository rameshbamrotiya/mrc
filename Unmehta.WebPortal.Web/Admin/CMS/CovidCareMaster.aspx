<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CovidCareMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.CovidCareMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Covid Care Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Covid Care Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/CMS/CK/ckeditor.js") %>"></script>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtTitle" TabIndex="2" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="Enter title." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label for="exampleInputFile">Image Upload<span class="req-field"></span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuImageUpload" runat="server" TabIndex="3" CssClass="form-control" />
                            <asp:Label ID="lblMainImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfMainImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblMainImage','bodyPart_aRemoveMain','bodyPart_hfMainImage');" id="aRemoveMain" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMetaTitle" TabIndex="4" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMetaTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                ErrorMessage="Enter meta title." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="form-group">
                            <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="5" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMetaDesc" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                ErrorMessage="Enter meta description." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none;">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Description<span class="req-field"></span></label>
                            <asp:TextBox ID="txtDescription" runat="server" Rows="10" TextMode="MultiLine" TabIndex="7"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Left Video Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlLeftvideoupload" AutoPostBack="false" TabIndex="8" onchange="GetLeftVideoSelectedValue(this)" CssClass="form-control" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Internal Link"></asp:ListItem>
                                <asp:ListItem Value="0" Text="External Link"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div id="divLeftInternalvideo" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Select Left Video<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuLeftDocUpload" runat="server" TabIndex="9" CssClass="form-control" />
                                <asp:Label ID="lblLeftVideoImage" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfLeftVideoImage" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblLeftVideoImage','bodyPart_aRemoveLeftVideo','bodyPart_hfLeftVideoImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeftVideo" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                          
                            </div>
                        </div>
                        <div id="divLeftExternallink" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Add Left Video Link<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtLeftExternallink" TabIndex="10" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLeftExternallink" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtLeftExternallink"
                                    ErrorMessage="Enter Add Left Video Link." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div id="thumbnilexternal" runat="server" class="col-md-4" >
                        <div>
                            <div class="form-group">
                                <label for="exampleInputFile">Select Left Thumbnail<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuLeftthumbnill" runat="server" TabIndex="11" CssClass="form-control" />
                                <asp:Label ID="lblLeftThumbnailImage" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfLeftThumbnailImage" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblLeftThumbnailImage','bodyPart_aRemoveLeftThumbnail','bodyPart_hfLeftThumbnailImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeftThumbnail" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Right Video Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlRightvideoupload" AutoPostBack="false" TabIndex="12" onchange="GetRightVideoSelectedValue(this)" CssClass="form-control" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Internal Link"></asp:ListItem>
                                <asp:ListItem Value="0" Text="External Link"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <div id="divRightInternalvideo"  runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Select Right Video<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuRightVideoUpload" runat="server" TabIndex="13" CssClass="form-control" />
                                <asp:Label ID="lblRightVideoImage" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfRightVideoImage" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblRightVideoImage','bodyPart_aRemoveRightVideo','bodyPart_hfRightVideoImage');" id="aRemoveRightVideo" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                            </div>
                        </div>
                        <div id="divRightExternallink" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Add Right Video Link<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtRightVideoLink" TabIndex="14" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRightVideoLink" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtLeftExternallink"
                                    ErrorMessage="Enter Add Right Video Link." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div id="divRightThumbnail" runat="server" class="col-md-4">
                        <div>
                            <div class="form-group">
                                <label for="exampleInputFile">Select Right Thumbnail<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuRightthumbnill" runat="server" TabIndex="15" CssClass="form-control" />
                                <asp:Label ID="lblRightThumbnailImage" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfRightThumbnailImage" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblRightThumbnailImage','bodyPart_aRemoveRightThumbnail','bodyPart_hfRightThumbnailImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveRightThumbnail" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">FAQs Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFAQsTitle" TabIndex="3" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFAQsTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtFAQsTitle"
                                ErrorMessage="Enter FAQs Title." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">FAQs Image Upload<span class="req-field"></span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuFAQsImageUpload" runat="server" TabIndex="6" CssClass="form-control" />
                            <label visible="true" style="font-weight: normal;" id="lblFAQsImageUploadPath" runat="server"></label>                            
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">FAQs Accreditation Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFAQsAccreditationTitle" TabIndex="3" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFAQsAccreditationTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtFAQsAccreditationTitle"
                                ErrorMessage="Enter FAQs Accreditation Title." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                    <%}
                        if (SessionWrapper.UserPageDetails.CanUpdate)
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
    </asp:Panel>
    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row" style="display: none;">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class=" controls">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control"></asp:TextBox>
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
                                <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick">
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
                            <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,CovidCare_Id,Language_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                DataSourceID="sqlds" AllowSorting="false" OnPageIndexChanging="gView_PageIndexChanging" PageSize="10" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:BoundField DataField="MetaTitle" HeaderText="Meta Title" SortExpression="MetaTitle" />
                                    <asp:BoundField DataField="MetaDescription" HeaderText="MetaDescription" SortExpression="MetaDescription" />
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="aImageUploadfile" href='<%# Eval("ImageUploadPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Left_Link_Video_Upload" HeaderText="LeftLink Status" SortExpression="Left_Link_Video_Upload" />
                                    <asp:TemplateField HeaderText="View Video">
                                        <ItemTemplate>
                                            <a id="aLeftVideofile" href='<%# Eval("LeftVideoPath") %>' target="_blank" runat="server" tooltip="View Video" class="fa fa-video-camera"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Thumbnail">
                                        <ItemTemplate>
                                            <a id="aLeftVideoThumbnailfile" href='<%# Eval("LeftVideoThumbnailPath") %>' target="_blank" runat="server" tooltip="View Thumbnail" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Right_Link_Video_Upload" HeaderText="RightLink Status" SortExpression="Right_Link_Video_Upload" />
                                    <asp:TemplateField HeaderText="View Video">
                                        <ItemTemplate>
                                            <a id="aRightVideofile" href='<%# Eval("RightVideoPath") %>' target="_blank" runat="server" tooltip="View Video" class="fa fa-video-camera"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Thumbnail">
                                        <ItemTemplate>
                                            <a id="aRightVideoThumbnailfile" href='<%# Eval("RightVideoThumbnailPath") %>' target="_blank" runat="server" tooltip="View Thumbnail" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FAQsTitle" HeaderText="FAQs Title" SortExpression="FAQsTitle" />
                                    <asp:BoundField DataField="FAQsAccreditationTitle" HeaderText="FAQs Accreditation Title" SortExpression="FAQsAccreditationTitle" />
                                    <asp:TemplateField HeaderText="View FAQs Image">
                                        <ItemTemplate>
                                            <a id="aFAQsImageUploadfile" href='<%# Eval("FAQsImageUploadPath") %>' target="_blank" runat="server" tooltip="View FAQs Image" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add Accredation Details">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkAddAccredation" runat="server" data-original-title="AddTAccredation" OnClick="lnkAddAccredation_Click" OnClientClick="SetTarget();"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton Visible="false" ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Contribution?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[PROC_GetAllCovidCareMasterDetails]" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="Title" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdPKId" runat="server" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
        

        function GetLeftVideoSelectedValue(ddlLeftvideoupload) {
            var selectedValue = ddlLeftvideoupload.value;
            if (selectedValue == "0") {
                document.getElementById('<%= divLeftExternallink.ClientID %>').style.display = "block";
                document.getElementById('<%= divLeftInternalvideo.ClientID %>').style.display = "none";
                document.getElementById('<%= divLeftExternallink.ClientID %>').style.visibility = "visible";
                document.getElementById('<%= divLeftInternalvideo.ClientID %>').style.visibility = "hidden";
                document.getElementById("<%=rfvLeftExternallink.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvLeftExternallink.ClientID%>").enabled = true;
            }
            else {
                document.getElementById('<%= divLeftExternallink.ClientID %>').style.display = "none";
                document.getElementById('<%= divLeftInternalvideo.ClientID %>').style.display = "block";
                document.getElementById('<%= divLeftExternallink.ClientID %>').style.visibility = "hidden";
                document.getElementById('<%= divLeftInternalvideo.ClientID %>').style.visibility = "visible";
                document.getElementById("<%=rfvLeftExternallink.ClientID%>").style.visibility = "hidden"; 
                document.getElementById("<%=rfvLeftExternallink.ClientID%>").enabled = false;
            }
        }
        function GetRightVideoSelectedValue(ddlRightvideoupload) {
            var selectedValue = ddlRightvideoupload.value;
            if (selectedValue == "0") {
                document.getElementById('<%= divRightExternallink.ClientID %>').style.display = "block";
                document.getElementById('<%= divRightInternalvideo.ClientID %>').style.display = "none";
                document.getElementById('<%= divRightExternallink.ClientID %>').style.visibility = "visible";
                document.getElementById('<%= divRightInternalvideo.ClientID %>').style.visibility = "hidden";
                document.getElementById("<%=rfvRightVideoLink.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvRightVideoLink.ClientID%>").enabled = true;
            }
            else {
                document.getElementById('<%= divRightExternallink.ClientID %>').style.display = "none";
                document.getElementById('<%= divRightInternalvideo.ClientID %>').style.display = "block";
                document.getElementById('<%= divRightExternallink.ClientID %>').style.visibility = "hidden";
                document.getElementById('<%= divRightInternalvideo.ClientID %>').style.visibility = "visible";
                document.getElementById("<%=rfvRightVideoLink.ClientID%>").style.visibility = "hidden"; 
                document.getElementById("<%=rfvRightVideoLink.ClientID%>").enabled = false;
            }
        }

    </script>
</asp:Content>
