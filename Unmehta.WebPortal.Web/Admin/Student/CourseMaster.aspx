<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CourseMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.CourseMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Course Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Course</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Course</li>
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
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-Font-Bold="true" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowCommand="gView_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
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
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Name", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Name<span class="req-field">*</span></label>
                                        <asp:TextBox autocomplete="off" ID="txtName" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdvertisementName" CssClass="validationmsg" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Please enter Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Code<span class="req-field">*</span></label>
                                        <asp:TextBox autocomplete="off" ID="txtCode" TabIndex="2" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdvertisementCode" CssClass="validationmsg" runat="server" ControlToValidate="txtCode"
                                            ErrorMessage="Please enter Code." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Type<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                        </asp:DropDownList>
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
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <h3>Sub Course List </h3>
                                        </div>
                                        <asp:HiddenField ID="hfSubId" runat="server" />
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Sub Course Name<span class="req-field">*</span></label>
                                                <asp:TextBox ID="txtSubCourseName" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Sub Course Code<span class="req-field">*</span></label>
                                                <asp:TextBox ID="txtSubCourseCode" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Course Duration<span class="req-field">*</span></label>
                                                <asp:TextBox ID="txtCourseDuration" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Total Seat<span class="req-field">*</span></label>
                                                <asp:TextBox ID="txtTotalSeat" MaxLength="50" runat="server" CssClass="form-control " onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Information<span class="req-field"></span></label>
                                                <asp:TextBox ID="txtInformation" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <script type="text/javascript">
                                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                    var editor = CKEDITOR.replace('<%=txtInformation.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                    });
                                                </script>

                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Course Fee Description <span class="req-field"></span></label>
                                                <asp:TextBox ID="txtFeesDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <script type="text/javascript">
                                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                    var editor = CKEDITOR.replace('<%=txtFeesDescription.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                    });
                                                </script>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Course Note<span class="req-field"></span></label>
                                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <script type="text/javascript">
                                                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                    var editor = CKEDITOR.replace('<%=txtNote.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                    });
                                                </script>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="exampleInputFile">Upload Image<span class="req-field">*</span></label>
                                                <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                                  <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                                                  <asp:HiddenField ID="hfLeftImage" runat="server" />
                                                  <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                                 
                                                <p class="help-block">( Image should be 259px*172px )</p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <br />
                                            <asp:Button runat="server" ID="btnAddSubCourse" CssClass="btn btn-primary " Text="Submit Sub Course" OnClick="btnAddSubCourse_Click" Style="margin-top: 7px;" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CourseName" ItemStyle-Width="100%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# HttpUtility.HtmlDecode( Eval("CourseName").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="StudentName" HeaderText="Student Name" SortExpression="StudentName" />--%>
                                                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
                                                <asp:BoundField DataField="TotalSeat" HeaderText="Seat" SortExpression="TotalSeat" />
                                                <asp:BoundField DataField="CourseDuration" HeaderText="Course Duration" SortExpression="CourseDuration" />
                                               
                                                <asp:TemplateField HeaderText="View File">
                                                    <ItemTemplate>
                                                        <a id="afile" href='<%# Eval("ImagePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_CourseEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_CourseEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_CourseDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_CourseDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
