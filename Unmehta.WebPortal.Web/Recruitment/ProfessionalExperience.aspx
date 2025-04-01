<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="ProfessionalExperience.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.ProfessionalExperience" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Professional Experience
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/assets/bower_components/font-awesome/css/font-awesome.min.css") %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Professional Experience</h1>
            <ul class="page-breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>/</li>
                <li>Professional Experience</li>
            </ul>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-7 col-lg-12">
            <!-- Employment Record -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Employment Record : (Begin with most recent Organization)</h4>
                    <hr />
                    <div class="row form-row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:HiddenField ID="hfExperienceID" runat="server" Value="0" />
                                <label>Organization Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control txtletterUpper" placeholder="Organization Name" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOrganizationName" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtOrganizationName"
                                    ErrorMessage="Enter organization mame." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Organization Address <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtOrganizationAddress" TextMode="MultiLine" Rows="1" runat="server" CssClass="form-control txtletterUpper" placeholder="Organization Address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOrganizationAddress" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtOrganizationAddress"
                                    ErrorMessage="Enter organization address." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Post Type <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPostType" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPostType" InitialValue="Select" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlPostType"
                                    ErrorMessage="Select post type." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>From Date <span class="text-danger">*</span></label>
                                <div class="cal-icon">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datetimepicker" placeholder="Select From Date"></asp:TextBox>
                                    <%--<asp:CompareValidator ID="cfValidatorFromDateExp"
                                        ControlToValidate="txtFromDate"
                                        Display="Dynamic"
                                        ForeColor="Red"                                        
                                        Type="Date"
                                        runat="Server" />--%>
                                    <asp:RequiredFieldValidator ID="rfvFromDate" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtFromDate"
                                        ErrorMessage="Enter experience from date." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>To Date <span class="text-danger">*</span></label>
                                <div class="cal-icon">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datetimepicker" placeholder="Select To Date"></asp:TextBox>
                                    <%--<asp:CompareValidator ID="CompareValidator1"
                                        ControlToValidate="txtToDate"
                                        ControlToCompare="txtFromDate"
                                        Display="Dynamic"
                                        ForeColor="Red"
                                        Text="End date must be greater than From Date!"
                                        Operator="GreaterThan"
                                        Type="Date"
                                        runat="Server" />--%>
                                    <asp:RequiredFieldValidator ID="rfvToDate" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtToDate"
                                        ErrorMessage="Enter experience to date." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Designation <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control txtletterUpper" placeholder="Designation" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDesignation" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtDesignation"
                                    ErrorMessage="Enter designation." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Reporting Authority <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtReportingAuthority" runat="server" CssClass="form-control txtletterUpper" placeholder="Reporting Authority" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReportingAuthority" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtReportingAuthority"
                                    ErrorMessage="Enter reporting authority." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Reason For Change <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtReasonForChange" runat="server" CssClass="form-control txtletterUpper" placeholder="Reason For Change" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReasonForChange" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtReasonForChange"
                                    ErrorMessage="Enter reason for change." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Salary (p.m) <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtSalaryPM" runat="server" CssClass="form-control" placeholder="Salary (p.m)" onkeypress="return isNumberKeyWithDot(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSalaryPM" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtSalaryPM"
                                    ErrorMessage="Enter salary (p.m)." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Department <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control txtletterUpper" placeholder="Department" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDepartment" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtDepartment"
                                    ErrorMessage="Enter department." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="fuFileUpload">Upload Experience Certificate</label>
                                <asp:HiddenField ID="hfFilName" runat="server" />
                                <asp:FileUpload CssClass="form-control" ID="fuFileUpload" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFileUpload" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="fuFileUpload"
                                    ErrorMessage="Upload experience certificate." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revFileUpload" runat="server"
                                    ControlToValidate="fuFileUpload"
                                    ErrorMessage="Upload only pdf extention file." Font-Bold="True" ForeColor="red"
                                    ValidationExpression="(.*?)\.(pdf|PDF)$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnAddExperience" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Experience" OnClick="btnAddExperience_Click" />
                        </div>
                    </div>
                    <div class="row form-row" style="padding-top: 5px !important;">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="gvExperience" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,CandidateId,JobType,PostTypeId,ExperienceCertificateFileName" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OrganizationName" HeaderText="Organization Name" />
                                        <asp:BoundField DataField="OrganizationAddress" HeaderText="Organization Address" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From&nbsp;Date&nbsp;&nbsp;" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Experience" HeaderText="Experience" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="ReportingAuthority" HeaderText="Reporting Authority" />
                                        <asp:BoundField DataField="ReasonForChange" HeaderText="Reason For Change" />
                                        <asp:BoundField DataField="SalaryPerMonth" HeaderText="Salary (p.m)" />
                                        <asp:BoundField DataField="PostName" HeaderText="Post Type" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("ExperienceCertificateFilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lbtnEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <!-- /Employment Record-->
            <!-- Courses / Training Attended -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Courses / Training Attended</h4>
                    <hr />
                    <div class="row form-row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Subject / Course Title</label>
                                <asp:TextBox ID="txtSubjectCourseTitle" runat="server" CssClass="form-control txtletterUpper" placeholder="Subject / Course Title" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
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
                                <asp:TextBox ID="txtOrganizingInstitution" runat="server" CssClass="form-control txtletterUpper" placeholder="Organizing Institution / Organization" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Location</label>
                                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control txtletterUpper" placeholder="Location" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnAddCourses" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Courses" OnClick="btnAddCourses_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="row form-row" style="padding-top: 5px !important;">
                        <div class="col-md-12">
                            <div class="form-group">
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
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lbtnEditCourses" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnEditCourses_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeleteCourses" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnDeleteCourses_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <!-- /Courses / Training Attended -->
            <!-- Final Bolck -->
            <div class="card">
                <div class="card-body">
                    <div class="row form-row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Functions handled by you in Last Employment</label>
                                <asp:TextBox ID="txtLastEmploymentDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control txtletterUpper" placeholder="Functions handled by you in Last Employment"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Present Salary</label>
                                <asp:TextBox ID="txtPresentSalary" runat="server" CssClass="form-control" placeholder="Present Salary" onkeypress="return isNumberKeyWithDot(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Expected Salary</label>
                                <asp:TextBox ID="txtExpectedSalary" runat="server" CssClass="form-control" placeholder="Expected Salary" onkeypress="return isNumberKeyWithDot(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnPrivious" runat="server" Text="Previous" CssClass="btn btn-primary submit-btn" OnClick="btnPrivious_Click" CausesValidation="false" />
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary submit-btn" OnClick="btnNext_Click" CausesValidation="false" Visible="false" />
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
