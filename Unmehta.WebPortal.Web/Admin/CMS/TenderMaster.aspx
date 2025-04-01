<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TenderMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.TenderMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Tender Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <style type="text/css">
        .input-group-addon {
            padding: 9px 12px;
            font-size: 14px;
            font-weight: 400;
            line-height: 1;
            color: #555;
            text-align: center;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Tender Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Tender Masters</li>
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
                                <div class="controls">
                                    <div class="input-group">
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
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="TenderID" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="TenderNo" HeaderText="Tender No." SortExpression="TenderNo" />
                                        <asp:BoundField DataField="PublishDate" HeaderText="Publish Date." SortExpression="PublishDate" />
                                        <asp:BoundField DataField="PBMeetingDate" HeaderText="Pre Bid Meeting" SortExpression="PBMeetingDate" />
                                        <asp:BoundField DataField="LastDate" HeaderText="Last Date" SortExpression="LastDate" />
                                        <asp:BoundField DataField="OpeningDate" HeaderText="Opening Date" SortExpression="OpeningDate" />
                                        <asp:BoundField DataField="AddedDate" HeaderText="Opening Date" SortExpression="AddedDate" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:TemplateField HeaderText="Sequencing">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                        CommandArgument='<%# Eval("TenderID") + "," + Eval("Tender_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                        CommandArgument='<%# Eval("TenderID") + "," + Eval("Tender_level_id") + "," +   "down" %>'
                                                        runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <%}

                                                        if (SessionWrapper.UserPageDetails.CanDelete)
                                                        { %>
                                                    <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <%} %>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                    SelectCommand="PROC_TenderMaster_SEARCH" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' or PBMeetingDate like '%{1}%' or LastDate like '%{2}%'  or OpeningDate like '%{3}%' or TenderNo like '%{4}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter ControlID="txtSearch" Name="Title" />
                                        <asp:ControlParameter ControlID="txtSearch" Name="TenderNo" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="PublishDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="PBMeetingDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="LastDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="OpeningDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="AddedDate" />
                                    </FilterParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlEntry" runat="server">
                    <asp:HiddenField ID="hfTenderId" Value="" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfActiveView" Value="" runat="server"></asp:HiddenField>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <div class="card" style="border-top: 0px!important">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>Tender Detail</b></h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:View ID="View1" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label for="exampleInputFile"></label>
                                                <div class="alert alert-info">
                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                                    <h4><i class="icon fa fa-info"></i>Useful keywords for title:</h4>
                                                    (GeM Bid) (Date Extended) (Tender Scrapped) (Corrigendum Uploaded) (Revised bid and revised scope of work)
                                                            (Corrigendum and Responses of Pre-bid Query uploaded) (nprocure Bid) (Responses to pre-bid queries has been uploaded and Date Extended) 
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                                    <asp:TextBox ID="txtTitle" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtTitle" ValidationGroup="tender"
                                                        ErrorMessage="Title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Tender No.<span class="req-field">*</span></label>
                                                    <asp:TextBox ID="txtTenderNo" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="tfvtxtTenderNo" CssClass="validationmsg" runat="server" ControlToValidate="txtTenderNo" ValidationGroup="tender"
                                                        ErrorMessage="Tender No." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label for="exampleInputFile">Tender Details<span class="req-field">*</span></label>
                                                <asp:TextBox ID="CKEditorControl1" runat="server" Rows="10" TextMode="MultiLine" ValidationGroup="tender"></asp:TextBox>
                                                <script type="text/javascript">
                                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                    var editor = CKEDITOR.replace('<%=CKEditorControl1.ClientID%>', {
                                                        extraPlugins: 'tableresize'
                                                    });
                                                </script>
                                                <br />
                                                <%--<asp:RequiredFieldValidator ID="rfvCKeditor" CssClass="validationmsg" runat="server" ControlToValidate="CKEditorControl1" ValidationGroup="tender"
                                                    ErrorMessage="Tender Details" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            </div>
                                        <div class="form-group">
                                            <div class="row">

                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Publish Date</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtPublishDate" runat="server" CssClass="form-control pull-right dtpicker" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPublishDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Pre-Bid Meeting Date</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtPreBidDate" runat="server" CssClass="form-control pull-right dtpicker" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="revPreBidDate" runat="server" ControlToValidate="txtPreBidDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Time (Hours - hh:mm)</label>
                                                    <div class="bootstrap-timepicker">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtPreBidTime" runat="server" CssClass="form-control timepicker" ValidationGroup="tender"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-clock-o"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Last Date of Submission<span class="req-field">*</span></label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtLastDateBidSub" ValidationGroup="tender" runat="server" CssClass="form-control dtpicker"></asp:TextBox>
                                                    </div>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvLastDateBidSub" CssClass="validationmsg" runat="server" ControlToValidate="txtLastDateBidSub"
                                                        ErrorMessage="Last Date of Submission" SetFocusOnError="true" ValidationGroup="tender"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="revLastDateBidSub" runat="server" ControlToValidate="txtLastDateBidSub" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Time (Hours - hh:mm)</label>
                                                    <div class="bootstrap-timepicker">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtLastDateOfBidTime" ValidationGroup="tender" runat="server" CssClass="form-control timepicker"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-clock-o"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--<asp:RequiredFieldValidator ID="rfvLastDateOfBidTime" CssClass="validationmsg" runat="server" ControlToValidate="txtLastDateOfBidTime"
                                                            ErrorMessage="Time" SetFocusOnError="true" ValidationGroup="tender"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ID="revLastDateOfBidTime" runat="server" ControlToValidate="txtLastDateOfBidTime" ValidationExpression="^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"
                                                            ErrorMessage="Invalid time format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Opening Bid Date<span class="req-field">*</span></label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtOpeningBidDate" runat="server" CssClass="form-control dtpicker" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvOpeningBidDate" CssClass="validationmsg" runat="server" ControlToValidate="txtOpeningBidDate"
                                                        ErrorMessage="Opening Bid Date" SetFocusOnError="true" ValidationGroup="tender"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="revOpeningBidDate" runat="server" ControlToValidate="txtOpeningBidDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="exampleInputFile">Time (Hours - hh:mm)</label>
                                                    <div class="bootstrap-timepicker">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtOpenBidTime" runat="server" ValidationGroup="tender" CssClass="form-control timepicker"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-clock-o"></i>
                                                                </div>
                                                            </div>
                                                            <%--  <asp:RequiredFieldValidator ID="rfvtxtOpenBidTime" CssClass="validationmsg" runat="server" ControlToValidate="txtOpenBidTime"
                                                                ErrorMessage="Time" SetFocusOnError="true" ValidationGroup="tender"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="revOpenBidDate" runat="server" ControlToValidate="txtOpenBidTime" ValidationExpression="^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"
                                                                ErrorMessage="Invalid time format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Project Estimate Value</label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtProjectEstVal" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="revEstval"
                                                        ControlToValidate="txtProjectEstVal" runat="server"
                                                        ErrorMessage="Only Numbers allowed"
                                                        ValidationExpression="^\d+(\.\d+)*$" CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Project Final Value</label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtProjFinalVal" runat="server" CssClass="form-control" ValidationGroup="tender"> </asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="revProjFinalVal"
                                                        ControlToValidate="txtProjFinalVal" runat="server"
                                                        ErrorMessage="Only Numbers allowed"
                                                        ValidationExpression="^\d+(\.\d+)*$" CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Name Of Bidder</label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtNameOfBidder" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <span style="visibility: hidden;">&nbsp;</span>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Issue Of Work Order Date</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtIssueOfWorkOrderDate" runat="server" CssClass="form-control dtpicker" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="revOrderDate" runat="server" ControlToValidate="txtIssueOfWorkOrderDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" ValidationGroup="tender" />
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                                        <asp:TextBox ID="txtMetaTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle" ValidationGroup="tender"
                                                            ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                                        <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc" ValidationGroup="tender"
                                                            ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Add NewIcon?<span class="req-field">*</span></label>
                                                    <div class="input-group">
                                                        <asp:RadioButton ID="rbtnYES" Checked="true" GroupName="IdocNew" runat="server" Text="Yes" ValidationGroup="tender" />
                                                        &nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbtnNO" runat="server" GroupName="IdocNew" Text="No" ValidationGroup="tender" />
                                                    </div>
                                                    <span style="visibility: hidden;">&nbsp;</span>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                                                        <asp:TextBox ID="txtsequence" runat="server" CssClass="form-control" ValidationGroup="tender"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-lg-12 BtnGrp">
                                            <div class="form-group">
                                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                    { %>
                                                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-primary" Text="Continue" Font-Bold="True" ValidationGroup="tender" />&nbsp;&nbsp;
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-primary" Text="Continue" Font-Bold="True" ValidationGroup="tender" />&nbsp;&nbsp;
                                                <%} %>
                                                <asp:Button ID="btn_Cancel" runat="server" CausesValidation="false" OnClick="btn_Cancel_ServerClick" CssClass="btn btn-inverse" Text="Back" Font-Bold="True" />
                                            </div>
                                        </div>
                                    </asp:View>
                                </div>
                            </div>
                        </div>
                        <div class="box" style="border-top: 0px!important">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>Document Detail</b></h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <asp:View ID="View2" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-9">
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Document Name<span class="req-field">*</span></label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ValidationGroup="doc" ControlToValidate="txtDocName"
                                                        ErrorMessage="Document Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-9">
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Document Type<span class="req-field">*</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlDocType" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Pre-Bid Document" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Other Document" Selected="True" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>

                                                    <span style="visibility: hidden;">Document Type</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-9">
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Upload Document<span class="req-field">*</span></label>
                                                </div>
                                                <div class="col-md-8">

                                                    <asp:FileUpload ID="fuDocument" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvDocument" CssClass="validationmsg" runat="server" ValidationGroup="doc" ControlToValidate="fuDocument"
                                                        ErrorMessage="Select Document" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-9">
                                                <div class="col-md-4">
                                                    <label for="exampleInputFile">Are you want NewIcon ?<span class="req-field">*</span></label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:RadioButton ID="rbnDocNewYES" GroupName="IdocNew" Checked="true" runat="server" Text="Yes" />
                                                    &nbsp;&nbsp;
                                                <asp:RadioButton ID="rbnDocNewNO" GroupName="IdocNew" runat="server" Text="No" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvDocList" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="55%" CssClass="txt">
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#E3EAEB" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNo" HeaderText="Sr. No.">
                                                            <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DocName" HeaderText="Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DocType" HeaderText="Doc Type">
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want Delete?');"
                                                                    CommandArgument='<%# Eval("DocID") %>' ImageUrl="~/images/delete.jpg" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#7C6F57" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>



                                        <div class="col-lg-12 BtnGrp">
                                            <div class="form-group">
                                                <asp:Button ID="btnAddDoc" ValidationGroup="doc" runat="server" OnClick="btnAddDoc_Click" CssClass="btn btn-primary" Text="Add Document" Font-Bold="True" />&nbsp;&nbsp;
                                            </div>
                                        </div>

                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-9">
                                            <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." AllowPaging="false" OnRowCommand="gvDoc_RowCommand1" DataKeyNames="DocID">
                                                <Columns>
                                                    <asp:BoundField DataField="RowNo" HeaderText="Sr. No." />
                                                    <asp:BoundField DataField="DocName" HeaderText="Name" />
                                                    <asp:BoundField DataField="DocType" HeaderText="Doc Type" />
                                                    <%--<asp:BoundField DataField="DocPath" HeaderText="Document" />--%>
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <a href="<%# Eval("DocPath") %>" target="_blank" title="Click to open document"><i class='fa fa-search-plus'></i></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="btnDelete" CausesValidation="false" ToolTip="Delete" CommandName="iDelete" OnClientClick='<%# Eval("DocName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label for="exampleInputFile">Documents Details <span class="req-field">*</span></label>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="CKEditorControl2" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                                    <script type="text/javascript">
                                                        CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                        var editor = CKEDITOR.replace('<%=CKEditorControl2.ClientID%>', {
                                                            extraPlugins: 'tableresize'
                                                        });
                                                    </script>
                                                    <br />
                                                    <%--<CKEditor:CKEditorControl ID="CKEditorControl2" runat="server"></CKEditor:CKEditorControl>--%>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCKEDIT2" ValidationGroup="DocD" CssClass="validationmsg" runat="server" ControlToValidate="CKEditorControl2"
                                                        ErrorMessage="Documents Details" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 BtnGrp">
                                                <div class="form-group">
                                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                        { %>
                                                    <asp:Button ID="btnFinal" runat="server" CssClass="btn btn-primary" ValidationGroup="DocD" Text="Final Save" OnClick="btnFinal_Click1" />
                                                    <%} %>
                                                    <asp:Button ID="btnBackView" runat="server" CausesValidation="false" OnClick="btnBackView_Click" CssClass="btn btn-inverse" Text="Back" Font-Bold="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>

                                </div>
                            </div>
                            <br />

                        </div>
                    </asp:MultiView>
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
            $j('#<%=txtOpeningBidDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $j('#<%=txtOpenBidTime.ClientID%>').clockpicker({
                autoclose: true
            });

            $j('#<%=txtLastDateBidSub.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $j('#<%=txtLastDateOfBidTime.ClientID%>').clockpicker({
                autoclose: true
            });

            $j('#<%=txtPreBidDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $j('#<%=txtPreBidTime.ClientID%>').clockpicker({
                autoclose: true
            });
            $j('#<%=txtIssueOfWorkOrderDate.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
            $j('#<%=txtPublishDate.ClientID%>').datepicker({
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
