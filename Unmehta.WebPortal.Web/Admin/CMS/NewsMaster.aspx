<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="NewsMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.NewsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>News Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>News Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">News Master</li>
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
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="news_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10" OnPageIndexChanging="gView_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="news_title" HeaderText="Name" SortExpression="news_title" />
                                        <asp:BoundField DataField="news_start_date" HeaderText="Start Date" SortExpression="news_start_date" />
                                        <asp:BoundField DataField="news_end_date" HeaderText="End Date" SortExpression="news_end_date" />
                                        <asp:BoundField DataField="newsBy" HeaderText="News By" SortExpression="newsBy" />
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
                                                    <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" OnClientClick='<%# Eval("news_title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <%} %>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                    SelectCommand="[PROC_NewsMaster_Search]" SelectCommandType="StoredProcedure" FilterExpression="news_title like '%{0}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter ControlID="txtSearch" Name="news_title" />
                                    </FilterParameters>
                                </asp:SqlDataSource>
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
                                        <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server">
                                            <asp:ListItem Selected="True" Value="1">English</asp:ListItem>
                                            <asp:ListItem Value="3">Hindi</asp:ListItem>
                                        </asp:DropDownList>
                                        <span style="visibility: hidden;">&nbsp;</span>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Type<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlNewsType" AutoPostBack="True" OnSelectedIndexChanged="ddlNewsType_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                        <span style="visibility: hidden;">&nbsp;</span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtTitle"
                                        ErrorMessage="Please enter Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Upload By<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtUploadBy" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtUploadBy"
                                        ErrorMessage="Please enter UploadBy." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Description</label>
                                        <asp:TextBox ID="txtNewsDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtNewsDescription.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div id="Fdate" runat="server" class="row">
                                <div class="col-md-3">
                                    <label for="exampleInputFile">From Date</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" autocomplete="off" runat="server" CssClass="form-control pull-right dtpicker"></asp:TextBox>
                                    </div>
                                    <%-- <asp:RequiredFieldValidator ID="rfvStartDate" CssClass="validationmsg" runat="server" ControlToValidate="txtStartDate"
                                        ErrorMessage="From Date" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="revstartDate" runat="server" ControlToValidate="txtStartDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" />
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputFile">Time (Hours - hh:mm)</label>
                                    <div class="form-group">
                                        <div class="bootstrap-timepicker">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtStartTime" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control timepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputFile">End Date</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEndDate" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control pull-right dtpicker"></asp:TextBox>
                                    </div>
                                    <%--  <asp:RequiredFieldValidator ID="rfvEndDate" CssClass="validationmsg" runat="server" ControlToValidate="txtEndDate"
                                        ErrorMessage="End Date" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="revEndDate" runat="server" ControlToValidate="txtEndDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" />
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputFile">Time (Hours - hh:mm)</label>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtToTime" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control timepicker"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" runat="server" id="Fupload">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Document Upload</label>
                                        <asp:FileUpload ID="fuNewsDoc" runat="server" CssClass="form-control" />
                                          <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfLeftImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                       
                                        <p class="help-block">( Only .Jpeg or .Jpg or .PNG or .pdf )</p>
                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Location</label>
                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" id="Bannerimage">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Banner Upload</label>
                                        <asp:FileUpload ID="FuBanner" runat="server" CssClass="form-control" />
                                        <p class="help-block">( Only .Jpeg or .Jpg or .PNG or .svg)</p>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Is New Icon<span class="req-field">*</span></label>
                                        <asp:CheckBox ID="chkIsNew" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-lg-12 BtnGrp">
                                    <div class="form-group">
                                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                                            { %>
                                        <button runat="server" id="btn_Save" class="btn btn-primary" title="Save" onserverclick="btn_Save_Click">
                                            <i class="fa fa-floppy-o">&nbsp;Save</i>
                                        </button>
                                        <%}
                                            if (SessionWrapper.UserPageDetails.CanUpdate)
                                            { %>
                                        <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_Click">
                                            <i class="fa fa-floppy-o">&nbsp;Update</i>
                                        </button>
                                        <%} %>
                                    &nbsp;
                                    <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_Click" causesvalidation="false">
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
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/jquery/jquery-ui.min.js")%>"></script>
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/clockpicker/bootstrap-clockpicker.min.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var $j = jQuery.noConflict();
            $j('#<%=txtStartDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $j('#<%=txtStartTime.ClientID%>').clockpicker({
                autoclose: true
            });

            $j('#<%=txtEndDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $j('#<%=txtToTime.ClientID%>').clockpicker({
                autoclose: true
            });
        });
    </script>
</asp:Content>
