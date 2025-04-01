<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EducationDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.EducationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="col-md-12 col-lg-12">
        <!-- Basic Information -->
        <div class="card  card-info">
            <div class="card-header">
                <h5 class="card-title"><i class="fa fa-school"></i><b>Academic Details : (Starting from SSC/Equivalent)</b>
                </h5>
            </div>
            <div class="card-body">
                <%--  <div class="row form-row">
                    <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                    <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Degree/Diploma <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdegree" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                    </div>--%>
                <%-- <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlOtherSpeciality">Education Type</label>
                            <asp:DropDownList ID="ddlEductionType" AutoPostBack="true" OnSelectedIndexChanged="ddlEductionType_SelectedIndexChanged" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEductionType" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEductionType"
                                ErrorMessage="Select EductionType." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlOtherSpeciality">Education Name</label>
                            <asp:DropDownList ID="ddlEducationName" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEducationName" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEducationName"
                                ErrorMessage="Select EducationName." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Name of School/College<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtschoolname" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtschoolname"
                                ErrorMessage="Please enter Name of School/Colege." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Board/University</label>
                            <asp:TextBox ID="txtboard" runat="server" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtboard"
                                ErrorMessage="Please enter Board/University." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Year of passing</label>
                            <asp:TextBox ID="txtyear" runat="server" type="number" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationmsg" runat="server" ControlToValidate="txtyear"
                                ErrorMessage="Please enter Year of passing." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                <%-- <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Passing Month<span class="req-field">*</span></label>
                            <div class="">
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Month" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="Jan"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="Feb"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="Mar"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="Apr"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="May"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="Jun"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="Jul"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="Aug"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="Sep"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="Oct"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="Nov"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="Dec"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlHistoryYear">Passing Year</label>
                            <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAcademicYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlAcademicYear"
                                ErrorMessage="Select Passing year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="ddlStream">Stream</label>
                            <asp:DropDownList ID="ddlStream" CssClass="form-control" runat="server">
                                <asp:ListItem Value="-1" Text="Select" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Arts" Text="Arts"></asp:ListItem>
                                <asp:ListItem Value="Commerce " Text="Commerce "></asp:ListItem>
                                <asp:ListItem Value="Science" Text="Science"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStream" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlStream"
                                ErrorMessage="Select stream." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Division & %</label>
                            <asp:TextBox ID="txtdivision" runat="server" CssClass="form-control decimal"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationmsg" runat="server" ControlToValidate="txtdivision"
                                ErrorMessage="Please enter Division." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="ddlNoOfTrials">No Of Trials</label>
                            <asp:DropDownList ID="ddlNoOfTrials" CssClass="form-control" runat="server">
                                <asp:ListItem Value="-1" Text="Select" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                <asp:ListItem Value="2 " Text="2 "></asp:ListItem>
                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlNoOfTrials"
                                ErrorMessage="Select no of trials." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnKnowPersonDetails" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Details" OnClick="btnKnowPersonDetails_Click" />
                    </div>--%>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 10px;">
                        <asp:GridView ID="gvAcademicDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                <asp:BoundField DataField="TypeName" HeaderText="Education Type" SortExpression="TypeName" />
                                <asp:BoundField DataField="EducationDetailName" HeaderText="Education Name" SortExpression="EducationDetailName" />
                                <asp:BoundField DataField="NameOfSchoolCollege" HeaderText="Name Of SchoolCollege" SortExpression="NameOfSchoolCollege" />
                                <asp:BoundField DataField="BoardUniversity" HeaderText="Board - University" SortExpression="BoardUniversity" />
                                <asp:BoundField DataField="PassingMonth" HeaderText="PassingMonth" SortExpression="PassingYear" />
                                <asp:BoundField DataField="PassingYear" HeaderText="PassingYear" SortExpression="PassingYear" />
                                <asp:BoundField DataField="Stream" HeaderText="Stream" SortExpression="MajorSubjects" />
                                <asp:BoundField DataField="Division" HeaderText="Division" SortExpression="Division" />
                                <asp:BoundField DataField="NoOfTrials" HeaderText="Trials" SortExpression="NoOfTrials" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("MarksheetPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkbtnAcademics" CommandName="eEdit" OnClick="lnkbtnAcademics_Click" CausesValidation="false" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDeleteAcademics" CommandName="eDelete" runat="server" OnClick="lnkbtnDeleteAcademics_Click" SkinID="lDelete" CausesValidation="false" OnClientClick='<%# Eval("NameOfSchoolCollege", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hdnAcademicID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
      
        <div class="card  card-info">
            <div class="card-header">
                <h5 class="card-title"><i class="fa fa-users"></i><b>Academic Document</b>
                </h5>

            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvieweducationDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId,DocPath" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                <asp:BoundField DataField="EducationDetailName" HeaderText="Education Type" SortExpression="EducationDetailName" />
                                <%--<asp:BoundField DataField="DocumentName" HeaderText="Document Name" SortExpression="DocumentName" />--%>
                                <%--<asp:BoundField DataField="EducationDetailName" HeaderText="Education Name" SortExpression="EducationDetailName" />--%>
                                <asp:BoundField DataField="DocName" HeaderText="Name of Education document" SortExpression="DocName" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("DocPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                    </div>
                </div>

            </div>
        </div>
          <div class="card  card-info">
            <div class="card-header">
                <h5 class="card-title"><i class="fa fa-desktop"></i><b>Computer Literacy</b>
                </h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Computer Literacy</label>
                            <asp:DropDownList ID="ddlcomputerlit" CssClass="form-control select" TabIndex="3" runat="server" Style="width: 100%" Enabled="false">
                                <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="form-group mb-0">
                            <label>Remarks</label>
                            <textarea id="txtremerks" runat="server" class="form-control" readonly="readonly"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card  card-info">
            <div class="card-header">
                <h5 class="card-title"><i class="fa fa-users"></i><b>Courses / Training Attended</b>
                </h5>

            </div>
            <%-- <div class="row form-row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Subject / Course Title</label>
                            <asp:TextBox ID="txtSubjectCourseTitle" runat="server" CssClass="form-control" placeholder="Subject / Course Title" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Duration / Year</label>
                            <asp:TextBox ID="txtDurationYear" runat="server" CssClass="form-control" placeholder="Duration / Year"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Organizing Institution / Organization</label>
                            <asp:TextBox ID="txtOrganizingInstitution" runat="server" CssClass="form-control" placeholder="Organizing Institution / Organization" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Location</label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="Location" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnAddCourses" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Courses" OnClick="btnAddCourses_Click" CausesValidation="false" />
                    </div>
                </div>--%>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:HiddenField ID="hfCoursesId" runat="server" Value="0" />
                        <asp:Label ID="Label1" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,CandidateId" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CourseTitle" HeaderText="Subject / Course Title" />
                                <asp:BoundField DataField="Duration" HeaderText="Duration / Year" />
                                <asp:BoundField DataField="InstituteName" HeaderText="Organizing Institution / Organization" />
                                <asp:BoundField DataField="City" HeaderText="Location" />
                                <%--<asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lbtnEditCourses" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnEditCourses_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDeleteCourses" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnDeleteCourses_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hdnCourses" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card  card-info">
            <div class="card-header">
                <h5 class="card-title"><i class="fa fa-globe"></i><b>Language</b>
                </h5>
            </div>
            <div class="card-body" runat="server">
                <div class="education-info">
                    <div class="row form-row education-cont">
                        <div class="col-12 col-md-12 col-lg-12">
                            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" id="divRights" runat="server">
                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th>Language
                                                </th>
                                                <th>
                                                    <label>Read</label>
                                                    <%-- <asp:CheckBox ID="chkbview" runat="server" Text="Read" EnableViewState="true"
                                                        OnCheckedChanged="chkbView_CheckedChanged" />--%>
                                                </th>
                                                <th>
                                                    <label>Write</label>
                                                    <%--<asp:CheckBox ID="chkbadd" runat="server" Text="Write" EnableViewState="true"
                                                        OnCheckedChanged="chkbAdd_CheckedChanged" />--%>
                                                </th>
                                                <th>
                                                    <label>Speek</label>
                                                    <%--<asp:CheckBox ID="chkbupdate" runat="server" Text="Speek" EnableViewState="true"
                                                        OnCheckedChanged="chkbupdate_CheckedChanged" />--%>
                                                </th>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptUserRights">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="first" runat="server" Text='<%# HttpUtility.HtmlDecode(Eval("Language_Name").ToString()) %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" Enabled="false"
                                                                ID="chk1" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" Enabled="false"
                                                                ID="chk2" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" Enabled="false"
                                                                ID="chk3" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblLanguageID" runat="server" Text='<%# HttpUtility.HtmlDecode(Eval("id").ToString()) %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Education -->
        <!-- Experience -->

        <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
            <div class="row form-row">
                <div class="col-md-5">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnBack" runat="server" CausesValidation="false" CssClass="btn btn-primary submit-btn" OnClick="btnBack_Click" Text="Back" />
                    <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" CssClass="btn btn-primary submit-btn" OnClick="btnSubmit_Click" Text="Next" />
                </div>
                <div class="col-md-4">
                </div>
            </div>

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
