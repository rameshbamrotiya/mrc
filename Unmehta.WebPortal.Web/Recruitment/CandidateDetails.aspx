<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="CandidateDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.CandidateDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Candidate Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Candidate Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Candidate Details</li>
            </ul>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
     <div class="contact-area register-card-body pt-50 pb-70">
        <div class="container">
            <div class="row justify-content-md-center">
                <div class="col-lg-12">
                    <section class="content">
                        <div class="card">
                            <div class="section-main-title" style="background-color:#c20e3e; text-align:center;">
                                <h2 style="margin-top:10px;color:white;">Basic Details</h2>
                                <h3>
                                    <asp:Label ID="lblPostName" runat="server" Style="text-align: right !important; color: red;"></asp:Label>
                                </h3>
                            </div>
                            <div class="card-body">
                                <!-- Bootstrap alert -->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Id" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1  %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="FullName" HeaderText="Full&nbsp;Name" />
                                                    <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                                                    <asp:BoundField DataField="DateOfBirth" HeaderText="Date&nbsp;Of&nbsp;Birth" />
                                                    <%--<asp:BoundField DataField="Nationality" HeaderText="Nationality" />--%>
                                                    <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Staus" />
                                                    <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                                    <asp:BoundField DataField="ParmenentAddress" HeaderText="Permanent Address" />
                                                    <asp:BoundField DataField="ParmenentPhoneM" HeaderText="Contact No" />
                                                    <asp:BoundField DataField="PhotographName" HeaderText="Photograph Name" SortExpression="Doc_Name" Visible="false" />
                                                    <asp:TemplateField HeaderText="Photograph">
                                                        <ItemTemplate>
                                                            <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-image"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SignatureName" HeaderText="Signature Name" SortExpression="Doc_Name" Visible="false" />
                                                    <asp:TemplateField HeaderText="Signature">
                                                        <ItemTemplate>
                                                            <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-image"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="DOB Proof" HeaderText="DOB Proof Name" SortExpression="Doc_Name" Visible="false" />
                                                    <asp:TemplateField HeaderText="View File">
                                                        <ItemTemplate>
                                                            <a id="afileDateofBirthProofPath" href='<%# Eval("DateofBirthProofPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <%--<asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkBasicDetailsEdit" runat="server" OnClick="lnkBasicDetailsEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" Style="margin-left: 50px !important; display: none;" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_View_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                                <%--<asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" OnClientClick='<%# Eval("Id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>--%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <%--<asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:ConString %>"
                                                SelectCommand="[PROC_HolidayMaster_Search]" SelectCommandType="StoredProcedure" FilterExpression="h_decription like '%{0}%' ">
                                                <FilterParameters>
                                                    <asp:ControlParameter ControlID="txtSearch" Name="h_decription" />

                                                </FilterParameters>
                                            </asp:SqlDataSource>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                           <div class="section-main-title" style="background-color:#c20e3e; text-align:center;">
                                <h2 style="margin-top:10px;color:white;">Education Details</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="gvEducationDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped " EmptyDataText="Record does not exist..."
                                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                                <Columns>
                                                    <asp:BoundField DataField="NameOfSchoolCollege" HeaderText="Name Of SchoolCollege" />
                                                    <asp:BoundField DataField="EducationName" HeaderText="Degree Name" />
                                                    <asp:BoundField DataField="PercentageOrPercentile" HeaderText="Percentage Or Percentile" />
                                                    <%--<asp:BoundField DataField="ClassName" HeaderText="Class" />
                                                    <asp:BoundField DataField="ExamBody" HeaderText="University" />
                                                    <asp:BoundField DataField="NoOfTrials" HeaderText="No Of Trials" />--%>
                                                    <asp:BoundField DataField="MajorSubjects" HeaderText="Major Subjects" />
                                                    <asp:BoundField DataField="PassingYear" HeaderText="Passing Year" />
                                                    <%--<asp:BoundField DataField="PercentageOrPercentile" HeaderText="Percentage Or Percentile" />--%>
                                                    <asp:BoundField DataField="Division" HeaderText="Division" />
                                                    <asp:TemplateField HeaderText="Certificate">
                                                        <ItemTemplate>
                                                            <a id="afilePhotographPath" href='<%# Eval("CertificateFilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-file"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="lnkEducationDetailsEdit" runat="server" OnClick="lnkEducationDetailsEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
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
                        <div class="card">
                            <div class="section-main-title" style="background-color:#c20e3e; text-align:center;">
                                <h2 style="margin-top:10px;color:white;">Extra Certificate Details</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="gvCertificateCourseDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                                <Columns>
                                                    <asp:BoundField DataField="CourseTitle" HeaderText="CertificateCourseName" />
                                                    <asp:BoundField DataField="Duration" HeaderText="Duration" />
                                                    <asp:BoundField DataField="InstituteName" HeaderText="InstituteName" />
                                                    <asp:BoundField DataField="City" HeaderText="City" />
                                                    <%--<asp:BoundField DataField="UplodCertificateCourseName" HeaderText="Upload File Name" Visible="false" />
                                                    <asp:TemplateField HeaderText="Extra Certificate">
                                                        <ItemTemplate>
                                                            <a id="afilePhotographPath" href='<%# Eval("UplodCertificateCoursePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="lnkCertificateDetailsEdit" runat="server" OnClick="lnkCertificateDetailsEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
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
                        <div class="card">
                            <div class="section-main-title" style="background-color:#c20e3e; text-align:center;">
                                <h2 style="margin-top:10px;color:white;">Professional Experience Details</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <asp:GridView ID="gvProfessionalExperience" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                                AllowPaging="true" AllowSorting="false" PageSize="10">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1  %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="JobTypeName" HeaderText="Job Name" />
                                                    <asp:BoundField DataField="FromDate" HeaderText="From&nbsp;Date" />
                                                    <asp:BoundField DataField="ToDate" HeaderText="To&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="OrganizationName" HeaderText="OrganizationName" />
                                                    <asp:BoundField DataField="OrganizationAddress" HeaderText="OrganizationAddress" />
                                                    <asp:BoundField DataField="ReportingAuthority" HeaderText="ReportingAuthority" />
                                                    <asp:BoundField DataField="SalaryPerMonth" HeaderText="SalaryPerMonth" />
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />
                                                    <asp:BoundField DataField="PostTypeId" HeaderText="PostTypeId" />
                                                    <asp:BoundField DataField="ExperienceCertificateFilePath" HeaderText="Certificate Name" Visible="false" />
                                                    <asp:TemplateField HeaderText="Experience Certificate">
                                                        <ItemTemplate>
                                                            <a id="aCertificateFile" href='<%# Eval("ExperienceCertificateFilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-file"></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="lnkProfessionalDetailsEdit" runat="server" OnClick="lnkProfessionalDetailsEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
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
                        <div class="row">
                            <!-- /.col -->
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="col-xs-2">
                                <button runat="server" id="btnProfessionalDivPrivious" class="btn btn-primary btn_general" onserverclick="btnProfessionalDivPrivious_ServerClick" causesvalidation="false" title="Search">
                                    Previous
                                </button>
                                <button runat="server" id="btnFinalSubmit" class="btn btn-primary btn_general" tabindex="4" title="Final Submit" onserverclick="btnFinalSubmit_ServerClick">
                                    Final Submit
                                </button>
                                <button runat="server" id="btnPrint" class="btn btn-primary btn_general" onserverclick="btnPrint_ServerClick" causesvalidation="false" visible="false" title="Search">
                                    Print
                                </button>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
