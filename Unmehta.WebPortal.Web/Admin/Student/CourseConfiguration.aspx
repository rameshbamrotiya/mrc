<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CourseConfiguration.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.CourseConfiguration" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Course Configuration</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Course Configuration</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Course Configuration</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <section class="content">
        <div class="card">
            <div class="card-body">
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
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowCommand="gView_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="Name" />
                                                <asp:BoundField DataField="AdvertisementName" HeaderText="Advertisement Name" SortExpression="AdvertisementName" />
                                                <asp:BoundField DataField="MinAge" HeaderText="MinAge" Visible="false" SortExpression="MinAge" />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" />
                                                <asp:BoundField DataField="IsVisible" HeaderText="Is Visible" SortExpression="IsVisible" />
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("CourseName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Course<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Advertisement<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlAdvertisement" CssClass="form-control" TabIndex="4" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4" style="display:none">
                                    <div class="form-group">
                                        <label for="txtAgeTo">Min Age</label>
                                        <asp:TextBox ID="txtAgeTo" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Min age" TextMode="Number" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="txtStartDate">Start Date</label>
                                        <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter start date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="txtStartDate">Start Time</label>
                                        <asp:TextBox ID="txtStartTime" aria-describedby="emailHelp" CssClass="form-control clockpicker-demo" placeholder="Enter start Time" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="txtEndDate">End Date</label>
                                        <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter end date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="txtEndDate">End Time</label>
                                        <asp:TextBox ID="txtEndTime" aria-describedby="emailHelp" CssClass="form-control clockpicker-demo" placeholder="Enter start Time" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtAcademicDesc">Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="4" TabIndex="5" runat="server" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtAgeTo">Fees</label>
                                        <asp:TextBox ID="txtFees" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Fees" TextMode="Number" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="True" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="dlChkMinEducationType">Min. Education Qualification</label>
                                        <asp:DataList ID="dlChkMinEducationType" runat="server" RepeatDirection="Vertical">
                                            <ItemTemplate>
                                                <div id="test1_<%#Eval("TypeName").ToString().Replace(" ","_")%>" style="margin-left: 5px;">
                                                    <asp:Label ID="LblEducationDetailName" Visible="false" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" Text='<%#Eval("TypeName")%>' value='<%#Eval("TypeName")%>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="dlVisableEducation">Select Visible Education in Form</label>
                                        <asp:DataList ID="dlVisableEducation" runat="server" RepeatDirection="Vertical">
                                            <ItemTemplate>
                                                <div id="test_<%#Eval("EducationDetailName").ToString().Replace(" ","_")%>" style="margin-left: 5px;">
                                                    <asp:Label ID="LblEducationDetailName" Visible="false" runat="server" Text='<%# Eval("EducationDetailName") %>'></asp:Label>
                                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" Text='<%#Eval("EducationDetailName")%>' value='<%#Eval("EducationDetailName")%>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
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
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="7" title="Save" onserverclick="btn_Save_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="7" title="Update" onserverclick="btn_Update_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>                                
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="8" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
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
    <script>
        $(document).ready(function () {
            ClosePreloder();
            var fromDate = document.getElementById('bodyPart_txtStartDate');
            var toDate = document.getElementById('bodyPart_txtEndDate');

            $(fromDate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $(toDate).datepicker("option", "minDate", dt);
                }
            });

            $(toDate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $(fromDate).datepicker("option", "maxDate", dt);
                }
            });
        });
    </script>
</asp:Content>
