<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="VideoGallery.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.VideoGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Video Gallery Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Video Gallery</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Video Gallery</li>
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
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <div class="form-group">
                                        <div class="controls">
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
                                <div class="col-md-12" style="margin-left: 10px;">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Video_id,Video_level_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10" OnPageIndexChanging="gView_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Video_name" HeaderText="Video Name" SortExpression="Video_name" />
                                                <asp:BoundField DataField="VideoCategoryName" HeaderText="Video Album Name" SortExpression="VideoCategoryName" />
                                                <asp:BoundField DataField="Video_desc" HeaderText="Video Description" SortExpression="Video_desc" />
                                                <asp:BoundField DataField="Video_level_id" HeaderText="Video Description" SortExpression="Video_level_id" />
                                                <asp:TemplateField HeaderText="View File">
                                                    <ItemTemplate>
                                                        <a id="afile" href='<%# Eval("Video_path") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-video-camera"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Video_name", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                                CommandArgument='<%# Eval("Video_id") + "," + Eval("Video_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                                CommandArgument='<%# Eval("Video_id") + "," + Eval("Video_level_id") + "," +   "down" %>'
                                                                runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_VideoMasterdetails_Search]" SelectCommandType="StoredProcedure" FilterExpression="Video_name like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="Video_name" />
                                            </FilterParameters>
                                        </asp:SqlDataSource>
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="dlblBlock">Video Album Name</label>
                                        <asp:DropDownList ID="ddlVideoCategory" CssClass="form-control" runat="server" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBlock" InitialValue="0" ForeColor="Red" runat="server" ControlToValidate="ddlVideoCategory"
                                            ErrorMessage="Select video album name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Video Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtVideoName" TabIndex="2" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtVideoName"
                                            ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Video Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtvideodesc" TextMode="MultiLine" TabIndex="3" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtvideodesc"
                                            ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaTitle" TabIndex="4" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle"
                                            ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="5" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtMetaDesc"
                                            ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Video Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlvideoupload" AutoPostBack="true" OnSelectedIndexChanged="ddlvideoupload_SelectedIndexChanged" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Internal Link"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="External Link"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" id="internalvideo" runat="server">
                                    <div >
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Video<span class="req-field">*</span></label>
                                            <asp:FileUpload ID="fuDocUpload" runat="server" TabIndex="7" CssClass="form-control" />
                                            <asp:Label ID="lblMainImage" runat="server" Text="" ></asp:Label>
                                            <asp:HiddenField ID="hfMainImage" runat="server" />
                                            <a onclick="return RemoveImage('bodyPart_lblMainImage','bodyPart_aRemoveMain','bodyPart_hfMainImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveMain" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="externallink" runat="server" visible="false">
                                    <div >
                                        <div class="form-group">
                                            <label for="exampleInputFile">Add Video Link<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtexternallink" TabIndex="8" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtexternallink"
                                                ErrorMessage="Please enter name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div id="thumbnilexternal" runat="server" class="col-md-6">
                                    <div>
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Thumbnail<span class="req-field">*</span></label>
                                            <asp:FileUpload ID="futhumbnill" runat="server" TabIndex="9" CssClass="form-control" />
                                            <asp:Label ID="lblThumbnailImage" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hfThumbnailImage" runat="server" />
                                            <a onclick="return RemoveImage('bodyPart_lblThumbnailImage','bodyPart_aRemoveThumbnail','bodyPart_hfThumbnailImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveThumbnail" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <label for="ddlDepartment">Department</label>
                                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDepartment"
                                            ErrorMessage="Select department." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>--%>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="10" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Download Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlDownloadstatus" CssClass="form-control" TabIndex="11" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtsequence" TabIndex="12" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
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
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="14" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="15" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="16" onserverclick="btn_Cancel_Click" causesvalidation="false">
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
