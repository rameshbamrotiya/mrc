<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EventMaster.aspx.cs" ValidateRequest="false" Inherits="Unmehta.WebPortal.Web.Admin.CMS.EventMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Event Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Event Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Event Master</li>
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
                                    <div class="form-group" style="overflow:scroll;">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="EventId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EventName" HeaderText="Event Name" SortExpression="EventName" />
                                                <asp:BoundField DataField="EventTypeName" HeaderText="Event Type Name" SortExpression="EventTypeName" />
                                                <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                                <asp:BoundField DataField="Venue" HeaderText="Venue" SortExpression="Venue" />
                                                <asp:BoundField DataField="EventStartDates" HeaderText="Start Date" SortExpression="EventStartDates" />
                                                <asp:BoundField DataField="EventEndDates" HeaderText="End Date" SortExpression="EventEndDates" />
                                                <asp:BoundField DataField="Location" HeaderText="Event Location" SortExpression="Location" />
                                                <asp:TemplateField HeaderText="Form Design">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <a id="aFileIntroductionEntry" href='<%# ResolveUrl("~/Admin/CMS/EventMasterForm?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("EventId").ToString())) %>' target="_blank" runat="server" tooltip="Update Other Details" class=""><i class="fa fa-edit"></i></a>

                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry Details">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <a id="aFileIntroductionEntry1" href='<%# ResolveUrl("~/Admin/CMS/EventFormEntry?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("EventId").ToString())) %>' target="_blank" runat="server" tooltip="Update Other Details" class=""><i class="fa fa-edit"></i></a>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("EventName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_EventMasterdetails_Search]" SelectCommandType="StoredProcedure" FilterExpression="EventName like '%{0}%' OR EventTypeName like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="EventName" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="EventTypeName" />
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
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInActive" CssClass="form-control" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="ddlDepartment">Event Type</label>
                                        <asp:DropDownList ID="ddlEventType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEventType"
                                            ErrorMessage="Select EventType." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Event Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDocName" CssClass="validationmsg" runat="server" ControlToValidate="txtEventName"
                                            ErrorMessage="Please Event Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Event Start Date<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtStartDate" autocomplete="off" runat="server" CssClass="form-control pull-right dtpicker"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtStartDate"
                                            ErrorMessage="Please Event Start Date." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Event End Date<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEndDate" autocomplete="off" runat="server" CssClass="form-control pull-right dtpicker"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtEndDate"
                                            ErrorMessage="Please Event End Date." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>&nbsp;</label>
                                    <div class="form-group form-control">
                                        <asp:CheckBox ID="chkIsOnlineRegistration" runat="server" />
                                        <label for="exampleInputFile">On-line Registration</label>

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
                                        <label for="exampleInputFile">Event Location<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEventLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvlocation" CssClass="validationmsg" runat="server" ControlToValidate="txtEventLocation"
                                            ErrorMessage="Please Event Location." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Seats<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtseats" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Event Organizer<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEventOrganizer" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfveventOrganizer" CssClass="validationmsg" runat="server" ControlToValidate="txtEventOrganizer"
                                            ErrorMessage="Please Event Organizer." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Phone No.<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvphone" CssClass="validationmsg" runat="server" ControlToValidate="txtphoneno"
                                            ErrorMessage="Please Phone No." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Email<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvemail" CssClass="validationmsg" runat="server" ControlToValidate="txtemail"
                                            ErrorMessage="Please Email." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Website Link<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtweblink" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvweblink" CssClass="validationmsg" runat="server" ControlToValidate="txtweblink"
                                            ErrorMessage="Please Website Link." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Gallery Link<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtGallery" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Event Venue<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEventVenue" runat="server" Rows="10" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtEventVenue.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                        </script>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label for="exampleInputFile">Organized by<span class="req-field">*</span></label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtOrganizedby" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript">
                                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                var editor = CKEDITOR.replace('<%=txtOrganizedby.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                            </script>
                                            <br />
                                            <%--<CKEditor:CKEditorControl ID="CKEditorControl1" HtmlEncodeOutput="true" runat="server"></CKEditor:CKEditorControl>--%>
                                           <%-- <asp:RequiredFieldValidator ID="rfvOrganizedby" CssClass="validationmsg" runat="server" ControlToValidate="txtOrganizedby"
                                                ErrorMessage="Please Enter Organized by" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label for="exampleInputFile">Event Details<span class="req-field">*</span></label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtEventDetails" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript">
                                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                var editor = CKEDITOR.replace('<%=txtEventDetails.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                            </script>
                                            <br />
                                            <%--<CKEditor:CKEditorControl ID="CKEditorControl1" HtmlEncodeOutput="true" runat="server"></CKEditor:CKEditorControl>--%>
                                           <%-- <asp:RequiredFieldValidator ID="rfveventdetails" CssClass="validationmsg" runat="server" ControlToValidate="txtEventDetails"
                                                ErrorMessage="Please Enter Event Details" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <h3>Patron list </h3>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Patron list<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtPatronlist" runat="server" Rows="10" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtPatronlist.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                        </script>
                                    </div>
                                </div>
                                <%--  <div class="col-md-3">
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle" data-toggle="tooltip" validationgroup="b" style="cursor: pointer; margin-top: 36px" title="Add" id="btnAdd" runat="server" onserverclick="btnAddPatronlist_ServerClick" />
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gvPatronlist" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnRowCommand="GridView1_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PatronlistName" HeaderText="Patronlist Name" SortExpression="PatronlistName" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("PatronlistName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        <%} %>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>--%>
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <h3>Leaflet Details </h3>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Leaflet Language Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtSocialmedianame" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="c" CssClass="validationmsg" runat="server" ControlToValidate="txtSocialmedianame"
                                            ErrorMessage="Please enter Social Media Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Leaflet File<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".pdf" ID="fuLeaflet" runat="server" CssClass="form-control" />
                                        <asp:HiddenField ID="hfLeafletFile" runat="server" />
                                        <asp:Label ID="lblhfLeafletFile" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" validationgroup="c" style="cursor: pointer;" title="Add" id="AddUnitDoctor" runat="server" onserverclick="AddSocialMediaLink_ServerClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gviewSocialmediaLinks" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnRowCommand="gviewSocialmediaLinks_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SocialMediaName" HeaderText="Language" SortExpression="SocialMediaName" />
                                            <asp:BoundField DataField="SocialMediaLink" HeaderText="FilePath" SortExpression="SocialMediaLink" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("SocialMediaName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fumainimg" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblMainImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfMainImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblMainImage','bodyPart_aRemoveMain','bodyPart_hfMainImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveMain" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        
                                    
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Inner Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuinnerimg" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfInnerImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInner','bodyPart_hfInnerImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveInner" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        
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
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var $j = jQuery.noConflict();

            $j('#<%=txtStartDate.ClientID%>').datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $j('#<%=txtEndDate.ClientID%>').datepicker("option", "minDate", dt);
                }
            }).on('changeDate', function (ev) {
                $j(this).blur();
                $j(this).datepicker('hide');
            });

            $j('#<%=txtEndDate.ClientID%>').datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $j('#<%=txtStartDate.ClientID%>').datepicker("option", "maxDate", dt);
                }
            }).on('changeDate', function (ev) {
                $j(this).blur();
                $j(this).datepicker('hide');
            });


            function GetDateFromstring(strDate) {
                var parts = strDate.split("/");
                var dt = new Date(parseInt(parts[2], 10),
                    parseInt(parts[1], 10) - 1,
                    parseInt(parts[0], 10));
                return dt;
            }
        });
    </script>
</asp:Content>

