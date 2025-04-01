<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UnitTeamMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.UnitTeamMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Unit Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Unit Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Unit Master</li>
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
                <asp:HiddenField ID="hfId" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlLanguage">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtUnitName">Unit Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtUnitName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUnitName" ValidationGroup="Main"
                                ErrorMessage="Enter Unit Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtFirstName">Specialization</label>
                            <asp:DropDownList ID="ddlSpecialization" class="form-control" runat="server" ValidationGroup="Main">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>&nbsp;</label>
                        <div class="form-group form-check form-control">
                            <asp:CheckBox ID="chkIsActive" runat="server" />
                            <label for="txtShortDescription">Is Active</label>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtFirstName">Week</label>
                            <asp:DropDownList ID="ddlWeekDropDown" class=" form-control" runat="server" ValidationGroup="Main">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Start Time<span class="req-field">*</span></label>
                            <div class="row">
                                <asp:RequiredFieldValidator ID="rfvtxtSunStartTimeHour" ControlToValidate="txtSunStartTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtSunStartTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox>
                                <span style="margin: 6px 3px;">:</span>
                                <asp:TextBox ID="txtSunStartTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSunStartTimeMin" ControlToValidate="txtSunStartTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlSunStartTimeTT" CssClass="form-control col-4" runat="server">
                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RangeValidator ID="rangValidatxtSunStartTimeHour" runat="server" MinimumValue="01" MaximumValue="12" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunStartTimeHour" ErrorMessage="Enter Start Hour between 01 to 12"></asp:RangeValidator>
                                <asp:RangeValidator ID="rangeValidatxtSunStartTimeMin" runat="server" MinimumValue="00" MaximumValue="59" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunStartTimeMin" ErrorMessage="Enter Start Minute between 00 to 59"></asp:RangeValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">End Time<span class="req-field">*</span></label>
                            <div class="row">
                                <asp:RequiredFieldValidator ID="rfvtxtSunEndTimeHour" ControlToValidate="txtSunEndTimeHour" ValidationGroup="Main" ForeColor="Red" runat="server" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtSunEndTimeHour" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox>
                                <span style="margin: 6px 3px;">:</span><asp:TextBox ID="txtSunEndTimeMin" MaxLength="2" runat="server" CssClass="form-control col-3" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSunEndTimeMin" ControlToValidate="txtSunEndTimeMin" ValidationGroup="Main" ForeColor="Red" Enabled="false" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlSunEndTimeTT" CssClass="form-control col-4" runat="server">
                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RangeValidator ID="rangValidatxtSunEndTimeHour" runat="server" MinimumValue="01" MaximumValue="12" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunEndTimeHour" ErrorMessage="Enter End Hour between 01 to 12"></asp:RangeValidator>
                                <asp:RangeValidator ID="rangeValidatxtSunEndTimeMin" runat="server" MinimumValue="00" MaximumValue="59" Type="Integer" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtSunEndTimeMin" ErrorMessage="Enter End Minute between 00 to 59"></asp:RangeValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-12">
                        <h3>Doctor List </h3>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Doctor Name<span class="req-field">*<br />
                            <br />
                            </span></label>
                            &nbsp;<asp:DropDownList ID="ddlDoctorList" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <br />
                        <asp:Button runat="server" ID="btnAddDoctor" CssClass="btn btn-primary " Text="Add To Doctor List" OnClick="btnAddDoctor_Click" Style="margin-top: 7px;" />
                    </div>
                    <div class="col-md-12" style="margin-left: 10px;">
                        <div class="form-group">
                            <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" SortExpression="DoctorName" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_DoctorDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_DoctorDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <div class="col-md-3">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                        { %>
                    <asp:Button runat="server" ID="btn_Update" CssClass="btn btn-primary " Text="Update" OnClick="btn_Update_Click" ValidationGroup="Main" />
                    <%}
                        if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" ValidationGroup="Main" />
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
                                    <asp:BoundField DataField="UnitName" HeaderText="Unit Name" SortExpression="UnitName" />
                                    <asp:BoundField DataField="SpecilizationName" HeaderText="Specilization Name" SortExpression="SpecilizationName" />
                                    <asp:BoundField DataField="WeekName" HeaderText="Week Name" SortExpression="WeekName" />
                                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                    <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />


                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
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
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>" SelectCommand="[GetAllUnitMaster]" SelectCommandType="StoredProcedure" FilterExpression="UnitName like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="UnitName" />
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
