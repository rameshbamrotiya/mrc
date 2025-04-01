<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OPDTimingsMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.OPDTimingsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>OPD Timing Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>OPD Timing</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">OPD Timing</li>
                </ol>
            </nav>
        </div>
    </div>
    <% Array colors = Enum.GetValues(typeof(DayOfWeek)); %>
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
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="OPD_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OPD_name" HeaderText="OPD Name" SortExpression="OPD_name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                                                <asp:TemplateField HeaderText="Week Name." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Enum.GetName(typeof(DayOfWeek), Convert.ToInt32(Eval("Week"))) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                                <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("OPD_name", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_OPDMasterdetails_Search]" SelectCommandType="StoredProcedure" FilterExpression="OPD_name like '%{0}%' OR DepartmentName like '%{0}%'">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="OPD_name" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="DepartmentName" />
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">OPD Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtOPDName" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtOPDName"
                                            ErrorMessage="Please OPDName name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="ddlDepartment">Department</label>
                                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDepartment"
                                            ErrorMessage="Select department." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Unit<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlunit" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
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
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInActive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
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
                                <div class="col-md-6">
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
                                <div class="form-group col-md-12">
                                    <h3>Doctor Place Incharge </h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Doctor Name<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlDoctorList" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Doctor_Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnRowCommand="GridView1_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Doctor_Id" HeaderText="Id" SortExpression="Doctor_Id" />
                                            <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" SortExpression="DoctorName" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("DoctorName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <h3>Doctor Consulting </h3>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Doctor Name<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlunitdoctor" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="AddUnitDoctor" runat="server" onserverclick="AddUnitDoctor_ServerClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gviewunitdoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Doctor_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnRowCommand="gviewunitdoctor_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Doctor_id" HeaderText="Doctor Id" SortExpression="Doctor_id" />
                                            <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" SortExpression="DoctorName" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("DoctorName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
            </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
