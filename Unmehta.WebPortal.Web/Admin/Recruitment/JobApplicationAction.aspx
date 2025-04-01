<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="JobApplicationAction.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.JobApplicationAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Job Application Action</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Job Application Action</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Job Application Action</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">

            <div class="row">
                <div class="col-md-3 controls">
                    <div class="form-group">
                        <label>Action Type :<span class="req-field">*</span></label>
                        <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1">Select</asp:ListItem>
                            <asp:ListItem Value="1">Interview Call Letter</asp:ListItem>
                            <%--<asp:ListItem Value="2">Lock/Unlock</asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvActionType" InitialValue="-1" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlActionType"
                            ErrorMessage="Select at least one action type." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3 controls">
                    <div class="form-group">
                        <label>Interview Date :<span class="req-field">*</span></label>
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtInterviewDate" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="dd/mm/yyyy" CssClass="form-control pull-right datepicker-demo"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvInterviewDate" CssClass="validationmsg" runat="server" ControlToValidate="txtInterviewDate"
                            ErrorMessage="Select interview date." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPreBidDate" runat="server" ControlToValidate="txtInterviewDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                            ErrorMessage="Invalid date format." CssClass="validationmsg" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="col-md-3 controls">
                    <div class="form-group">
                        <label for="exampleInputFile">From Time (Hours - hh:mm) :</label>
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:TextBox ID="txtFromTime" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="hh:mm" CssClass="form-control clockpicker-demo"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvFromTime" CssClass="validationmsg " runat="server" ControlToValidate="txtFromTime"
                                    ErrorMessage="Select from time." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 controls">
                    <div class="form-group">
                        <label for="exampleInputFile">To Time (Hours - hh:mm) :</label>
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:TextBox ID="txtToTime" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="hh:mm" CssClass="form-control clockpicker-demo"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvToTime" CssClass="validationmsg" runat="server" ControlToValidate="txtToTime"
                                    ErrorMessage="Select to time." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Interview Address :<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtInterviewAddress" TextMode="MultiLine" Rows="3" runat="server" placeholder="Interview Address" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtInterviewAddress"
                            ErrorMessage="Enter interview address" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 controls">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search By Application Id"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3 controls">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlJobList" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 controls">
                    <div class="form-group">
                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" causesvalidation="false" onserverclick="btn_Search_ServerClick">
                            <i class="fa fa-search">&nbsp;Search</i>
                        </button>
                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" causesvalidation="false" onserverclick="btn_SearchCancel_ServerClick">
                            <i class="fa fa-remove">&nbsp;Cancel</i>
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <asp:Button ID="btnSendCallLetter" runat="server" CssClass="btn btn-primary right" OnClick="btnSendCallLetter_Click" Text="Send Call Letter" Style="float: right;" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                            ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator>
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="CandidateId,PostId,EmailId,AdvertiseNo" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            DataSourceID="sqlds" AllowPaging="false" AllowSorting="false" PageSize="10" OnPageIndexChanging="gView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select All">
                                    <HeaderTemplate>
                                        <%--<label>Select All</label>--%>
                                        <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" ToolTip="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CandidateRegistrationId" HeaderText="Application Id" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="PostName" HeaderText="Post Name" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    
    <script type="text/javascript">
      
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>
</asp:Content>
