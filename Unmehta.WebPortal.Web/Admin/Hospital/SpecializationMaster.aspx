<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SpecializationMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.SpecializationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Specialization Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Specialization Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Hospital</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Specialization Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Specialization Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSpecializationName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtSpecializationName"
                                ErrorMessage="Enter Specialization" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtCastName">&nbsp;</label>
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnLogin_Click" />
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
        </div>

    </asp:Panel>
    <asp:Panel ID="pnlScheduling" runat="server">
        <div class="card">
            <div class="card-body">
                <div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtCommanStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommanStartTimeHour" ControlToValidate="txtCommanStartTimeHour" ValidationGroup="ApplyAll" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtCommanStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommanStartTimeMin" ControlToValidate="txtCommanStartTimeMin" ValidationGroup="ApplyAll" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCommanStartTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rvtxtCommanStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="ApplyAll" ForeColor="Red" ControlToValidate="txtCommanStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rvtxtCommanStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="ApplyAll" ForeColor="Red" ControlToValidate="txtCommanStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtCommanEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommanEndTimeHour" ControlToValidate="txtCommanEndTimeHour" ValidationGroup="ApplyAll" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtCommanEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommanEndTimeMin" ControlToValidate="txtCommanEndTimeMin" ValidationGroup="ApplyAll" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCommanEndTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rvtxtCommanEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="ApplyAll" ForeColor="Red" ControlToValidate="txtCommanEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rvtxtCommanEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="ApplyAll" ForeColor="Red" ControlToValidate="txtCommanEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtCommonLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommonLaunchStartTimeHour" ControlToValidate="txtCommonLaunchStartTimeHour" ValidationGroup="ApplyAll" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtCommonLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommonLaunchStartTimeMin" ControlToValidate="txtCommonLaunchStartTimeMin" Enabled="false" ValidationGroup="ApplyAll" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCommonLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtCommonLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="ApplyAll" ForeColor="Red" Type="Integer" ControlToValidate="txtCommonLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtCommonLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="ApplyAll" ForeColor="Red" Type="Integer" ControlToValidate="txtCommonLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtCommonLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommonLaunchEndTimeHour" ControlToValidate="txtCommonLaunchEndTimeHour" ValidationGroup="ApplyAll" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtCommonLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCommonLaunchEndTimeMin" ControlToValidate="txtCommonLaunchEndTimeMin" Enabled="false" ValidationGroup="ApplyAll" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCommonLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtCommonLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="ApplyAll" ForeColor="Red" Type="Integer" ControlToValidate="txtCommonLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtCommonLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="ApplyAll" ForeColor="Red" Type="Integer" ControlToValidate="txtCommonLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">

                            <div class="form-group">
                                <label for="exampleInputFile">Interval</label>
                                <asp:DropDownList ID="ddlInterval" CssClass="form-control " runat="server">
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="65" Value="65"></asp:ListItem>
                                    <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                    <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                    <asp:ListItem Text="85" Value="85"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:Button runat="server" ID="btnApplyAll" CssClass="btn btn-primary " Text="Apply To All" OnClick="btnApplyAll_Click" ValidationGroup="ApplyAll" />
                </div>

                <div style="margin-top: 10px;">
                    <asp:CheckBox ID="chkIsSunday" OnCheckedChanged="chkIsSunday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Sunday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSunStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunStartTimeHour" ControlToValidate="txtSunStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSunStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunStartTimeMin" ControlToValidate="txtSunStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSunStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSunStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSunStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSunEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunEndTimeHour" ControlToValidate="txtSunEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSunEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunEndTimeMin" ControlToValidate="txtSunEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSunEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSunEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSunEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSunLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunLaunchStartTimeHour" ControlToValidate="txtSunLaunchStartTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSunLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunLaunchStartTimeMin" ControlToValidate="txtSunLaunchStartTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSunLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSunLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSunLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSunLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSunLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSunLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunLaunchEndTimeHour" ControlToValidate="txtSunLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSunLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSunLaunchEndTimeMin" ControlToValidate="txtSunLaunchEndTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSunLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSunLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSunLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSunLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSunLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsMonday" OnCheckedChanged="chkIsMonday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Monday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtMondayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayStartTimeHour" ControlToValidate="txtMondayStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtMondayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayStartTimeMin" ControlToValidate="txtMondayStartTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlMondayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtMondayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtMondayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtMondayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtMondayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtMondayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayEndTimeHour" ControlToValidate="txtMondayEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtMondayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayEndTimeMin" ControlToValidate="txtMondayEndTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlMondayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtMondayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtMondayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtMondayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtMondayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtMondayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayLaunchStartTimeHour" ControlToValidate="txtMondayLaunchStartTimeHour" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtMondayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayLaunchStartTimeMin" ControlToValidate="txtMondayLaunchStartTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlMondayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtMondayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtMondayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtMondayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtMondayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtMondayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayLaunchEndTimeHour" ControlToValidate="txtMondayLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtMondayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtMondayLaunchEndTimeMin" ControlToValidate="txtMondayLaunchEndTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlMondayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtMondayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtMondayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtMondayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtMondayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsTuesday" OnCheckedChanged="chkIsTuesday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Tuesday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtTuesdayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayStartTimeHour" ControlToValidate="txtTuesdayStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtTuesdayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayStartTimeMin" ControlToValidate="txtTuesdayStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlTuesdayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtTuesdayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtTuesdayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtTuesdayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtTuesdayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtTuesdayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayEndTimeHour" ControlToValidate="txtTuesdayEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtTuesdayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayEndTimeMin" ControlToValidate="txtTuesdayEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlTuesdayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtTuesdayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtTuesdayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtTuesdayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtTuesdayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtTuesdayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayLaunchStartTimeHour" ControlToValidate="txtTuesdayLaunchStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtTuesdayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayLaunchStartTimeMin" ControlToValidate="txtTuesdayLaunchStartTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlTuesdayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtTuesdayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtTuesdayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtTuesdayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtTuesdayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtTuesdayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayLaunchEndTimeHour" ControlToValidate="txtTuesdayLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtTuesdayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtTuesdayLaunchEndTimeMin" ControlToValidate="txtTuesdayLaunchEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlTuesdayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtTuesdayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtTuesdayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtTuesdayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtTuesdayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsWednesday" OnCheckedChanged="chkIsWednesday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Wednesday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtWednesdayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayStartTimeHour" ControlToValidate="txtWednesdayStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtWednesdayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayStartTimeMin" ControlToValidate="txtWednesdayStartTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlWednesdayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtWednesdayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtWednesdayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtWednesdayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtWednesdayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtWednesdayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayEndTimeHour" ControlToValidate="txtWednesdayEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtWednesdayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayEndTimeMin" ControlToValidate="txtWednesdayEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlWednesdayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtWednesdayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtWednesdayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtWednesdayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtWednesdayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtWednesdayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayLaunchStartTimeHour" ControlToValidate="txtWednesdayLaunchStartTimeHour" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtWednesdayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayLaunchStartTimeMin" ControlToValidate="txtWednesdayLaunchStartTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlWednesdayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtWednesdayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtWednesdayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtWednesdayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtWednesdayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtWednesdayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayLaunchEndTimeHour" ControlToValidate="txtWednesdayLaunchEndTimeHour" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtWednesdayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtWednesdayLaunchEndTimeMin" ControlToValidate="txtWednesdayLaunchEndTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlWednesdayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtWednesdayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtWednesdayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtWednesdayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtWednesdayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsThursday" OnCheckedChanged="chkIsThursday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Thursday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtThursdayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayStartTimeHour" ControlToValidate="txtThursdayStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtThursdayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayStartTimeMin" ControlToValidate="txtThursdayStartTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlThursdayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtThursdayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtThursdayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtThursdayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtThursdayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtThursdayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayEndTimeHour" ControlToValidate="txtThursdayEndTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtThursdayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayEndTimeMin" ControlToValidate="txtThursdayEndTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlThursdayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtThursdayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtThursdayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtThursdayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtThursdayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtThursdayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayLaunchStartTimeHour" ControlToValidate="txtThursdayLaunchStartTimeHour" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtThursdayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayLaunchStartTimeMin" ControlToValidate="txtThursdayLaunchStartTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlThursdayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtThursdayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtThursdayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtThursdayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtThursdayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtThursdayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayLaunchEndTimeHour" ControlToValidate="txtThursdayLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtThursdayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtThursdayLaunchEndTimeMin" ControlToValidate="txtThursdayLaunchEndTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlThursdayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtThursdayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtThursdayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtThursdayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtThursdayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsFriday" OnCheckedChanged="chkIsFriday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Friday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtFridayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayStartTimeHour" ControlToValidate="txtFridayStartTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtFridayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayStartTimeMin" ControlToValidate="txtFridayStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlFridayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtFridayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtFridayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtFridayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtFridayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtFridayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayEndTimeHour" ControlToValidate="txtFridayEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtFridayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayEndTimeMin" ControlToValidate="txtFridayEndTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlFridayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtFridayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtFridayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtFridayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtFridayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtFridayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayLaunchStartTimeHour" ControlToValidate="txtFridayLaunchStartTimeHour" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtFridayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayLaunchStartTimeMin" ControlToValidate="txtFridayLaunchStartTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlFridayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtFridayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtFridayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtFridayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtFridayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtFridayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayLaunchEndTimeHour" ControlToValidate="txtFridayLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtFridayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtFridayLaunchEndTimeMin" ControlToValidate="txtFridayLaunchEndTimeMin" Enabled="false" ValidationGroup="Main" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlFridayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtFridayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtFridayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtFridayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtFridayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:CheckBox ID="chkIsSaturday" OnCheckedChanged="chkIsSaturday_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" /><h4>Saturday</h4>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSaturdayStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayStartTimeHour" ControlToValidate="txtSaturdayStartTimeHour" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSaturdayStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayStartTimeMin" ControlToValidate="txtSaturdayStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSaturdayStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSaturdayStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSaturdayStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSaturdayStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSaturdayStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSaturdayEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayEndTimeHour" ControlToValidate="txtSaturdayEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSaturdayEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayEndTimeMin" ControlToValidate="txtSaturdayEndTimeMin" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSaturdayEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSaturdayEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSaturdayEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSaturdayEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSaturdayEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">Start Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSaturdayLaunchStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayLaunchStartTimeHour" ControlToValidate="txtSaturdayLaunchStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSaturdayLaunchStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayLaunchStartTimeMin" ControlToValidate="txtSaturdayLaunchStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSaturdayLaunchStartTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSaturdayLaunchStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSaturdayLaunchStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSaturdayLaunchStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSaturdayLaunchStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputFile">End Launch Time<span class="req-field">*</span></label>
                                <div class="row">
                                    <asp:TextBox ID="txtSaturdayLaunchEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayLaunchEndTimeHour" ControlToValidate="txtSaturdayLaunchEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <span>:</span><asp:TextBox ID="txtSaturdayLaunchEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtSaturdayLaunchEndTimeMin" ControlToValidate="txtSaturdayLaunchEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSaturdayLaunchEndTimeTT" CssClass="form-control col-4" runat="server">
                                        <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="rangValidatxtSaturdayLaunchEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSaturdayLaunchEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                    <asp:RangeValidator ID="rangeValidatxtSaturdayLaunchEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" Type="Integer" ControlToValidate="txtSaturdayLaunchEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                    { %>
                <asp:Button runat="server" ID="btnUpdateSchedule" CssClass="btn btn-primary " Text="Save" OnClick="btnUpdateSchedule_Click" ValidationGroup="Main" />
                <%} %>
                <button runat="server" id="Button1" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                    <i class="fa fa-remove">&nbsp;Cancel</i>
                </button>
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
                                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
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
                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DepartmentName" HeaderText="Menu Name" SortExpression="DepartmentName" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="isactive" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkSchedule" CausesValidation="false" runat="server" data-original-title="Schedule" OnClick="lnkSchedule_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-calendar"></i></asp:LinkButton>
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllSpecializationMasterGrid]" SelectCommandType="StoredProcedure" FilterExpression="DepartmentName like '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="DepartmentName" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
