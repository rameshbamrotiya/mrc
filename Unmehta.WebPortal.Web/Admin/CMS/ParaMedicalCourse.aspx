<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ParaMedicalCourse.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.ParaMedicalCourse" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Paramedical Courses</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Admin</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Paramedical Courses</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Course Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtCourseName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCoursename" CssClass="validationmsg" runat="server" ControlToValidate="txtCourseName"
                                ErrorMessage="Enter Course Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Course Code<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtCourseCode" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCourseCode" CssClass="validationmsg" runat="server" ControlToValidate="txtCourseCode"
                                ErrorMessage="Enter Course code" SetFocusOnError="true"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Total Seats<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSeats" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSeats" CssClass="validationmsg" runat="server" ControlToValidate="txtSeats"
                                ErrorMessage="Enter total seats" SetFocusOnError="true"></asp:RequiredFieldValidator>
                             <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtSeats.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Course duration<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtCourseDuration" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCourseDuration" CssClass="validationmsg" runat="server" ControlToValidate="txtCourseDuration"
                                ErrorMessage="Enter Course duration" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtCourseDuration.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Description<span class="req-field"></span></label>
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtDesc.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Fees<span class="req-field"></span></label>
                            <asp:TextBox ID="txtFees" runat="server" CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtFees.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                            </script>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpStatus" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Upload Image<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                            <label visible="false" style="font-weight: normal;" id="filename" runat="server"></label>
                            <p class="help-block">( Image should be 259px*172px )</p>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" />
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

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control"></asp:TextBox>
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
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" DataKeyNames="CourseID,Languageid" EmptyDataText="Record does not exist..."
                                AllowSorting="false" DataSourceID="sqlds">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="courseCode" HeaderText="Course Code" />
                                    <asp:TemplateField HeaderText="Total Seats" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# HttpUtility.HtmlDecode( Eval("TotalSeats").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <%--<asp:BoundField DataField="TotalSeats" HeaderText="Total Seats" />--%>
                                    <asp:TemplateField HeaderText="Course Duration" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# HttpUtility.HtmlDecode( Eval("CourseDuration").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <%--<asp:BoundField DataField="CourseDuration" HeaderText="Course Duration" />--%>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# Eval("ImagePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllParaMEdicalCourses]" SelectCommandType="StoredProcedure" FilterExpression="CourseName like '%{0}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="CourseName" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnRecid" runat="server" />
        <asp:HiddenField ID="hdnColMenuID" runat="server" />

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
