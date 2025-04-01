<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.StudentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%-- <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Student Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Student Details</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="card card-info">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-user"></i>&nbsp;Personal Information</h4>
                </div>
                <div class="card-body">
                    <!-- Bootstrap alert -->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FullName" HeaderText="Full&nbsp;Name" />
                                        <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                                        <asp:BoundField DataField="DateOfBirth" HeaderText="Date&nbsp;Of&nbsp;Birth" />
                                        <asp:BoundField DataField="MaritalStatus" HeaderText="Marital&nbsp;Staus" />
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" />
                                        <asp:BoundField DataField="PlaceOfBirth" HeaderText="Place&nbsp;Of&nbsp;Birth" />
                                        <asp:BoundField DataField="CastName" HeaderText="Caste" />
                                        <asp:BoundField DataField="ReligionName" HeaderText="Religion" />
                                        <asp:BoundField DataField="Username" HeaderText="User&nbsp;Name" />
                                        <asp:TemplateField HeaderText="Action" Visible="false">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkBasicDetailsEdit" runat="server" OnClick="lnkBasicDetailsEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" Style="margin-left: 50px !important; display: none;" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_View_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus"></i></asp:LinkButton>
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
            <div class="card card-info">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-pen-alt"></i>&nbsp;Address Details</h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvEducationDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photograph">
                                            <ItemTemplate>
                                                <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-image"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Signature">
                                            <ItemTemplate>
                                                <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-image"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date&nbsp;of&nbsp;BirthProof">
                                            <ItemTemplate>
                                                <a id="afileDateofBirthProofPath" href='<%# Eval("DOBFilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-image"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PresentAddress" HeaderText="Present&nbsp;Address" />
                                        <asp:BoundField DataField="PresentPincode" HeaderText="Present&nbsp;Pincode" />
                                        <asp:BoundField DataField="PresentCountry" HeaderText="Present&nbsp;Country" />
                                        <asp:BoundField DataField="PresentState" HeaderText="Present&nbsp;State" />
                                        <asp:BoundField DataField="PresentCity" HeaderText="Present&nbsp;City" />
                                        <asp:BoundField DataField="PresentPhoneR" HeaderText="Present&nbsp;PhoneR" />
                                        <asp:BoundField DataField="PresentPhoneM" HeaderText="Present&nbsp;PhoneM" />
                                        <asp:BoundField DataField="ParmenentAddress" HeaderText="Permanent&nbsp;Address" />
                                        <asp:BoundField DataField="ParmenentPincode" HeaderText="Permanent&nbsp;Pincode" />
                                        <asp:BoundField DataField="ParmenentCountry" HeaderText="Permanent&nbsp;Country" />
                                        <asp:BoundField DataField="ParmenentState" HeaderText="Permanent&nbsp;State" />
                                        <asp:BoundField DataField="ParmenentCity" HeaderText="Permanent&nbsp;City" />
                                        <asp:BoundField DataField="ParmenentPhoneR" HeaderText="Permanent&nbsp;PhoneR" />
                                        <asp:BoundField DataField="ParmenentPhoneM" HeaderText="Permanent&nbsp;PhoneM" />
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
            <div class="card card-info">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-users"></i>&nbsp;Family Member Information</h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvCertificateCourseDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MemberName" HeaderText="Name" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" />
                                        <asp:BoundField DataField="RelationName" HeaderText="Relation&nbsp;Name" />
                                        <asp:BoundField DataField="Occupation" HeaderText="Occupation" />
                                        <asp:BoundField DataField="MonthlyIncome" HeaderText="Monthly&nbsp;Income" />
                                        <asp:BoundField DataField="FamilyContactNumber" HeaderText="Contact&nbsp;Number" />
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
            <div class="card card-info">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-school"></i>&nbsp;Student Education Details</h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="gvProfessionalExperience" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="aCertificateFile" href='<%# Eval("MarksheetPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EducationTypeId" HeaderText="Education&nbsp;Type" />
                                        <asp:BoundField DataField="EducationName" HeaderText="Education&nbsp;Name" />
                                        <asp:BoundField DataField="NameOfSchoolCollege" HeaderText="Name&nbsp;Of&nbsp;School/College" />
                                        <asp:BoundField DataField="BoardUniversity" HeaderText="Board&nbsp;University" />
                                        <asp:BoundField DataField="PassingMonth" HeaderText="Passing&nbsp;Month" />
                                        <asp:BoundField DataField="PassingYear" HeaderText="Passing&nbsp;Year" />
                                        <asp:BoundField DataField="Stream" HeaderText="Stream" />
                                        <asp:BoundField DataField="PercentageOrPercentile" HeaderText="Percentage&nbsp;Or&nbsp;Percentile" />
                                        <%--<asp:BoundField DataField="PercentageOrPercentile" HeaderText="Percentage&nbsp;Or&nbsp;Percentile" />--%>
                                        <asp:BoundField DataField="NoOfTrials" HeaderText="No&nbsp;Of&nbsp;Trials" />
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
            <div class="card card-info">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-school"></i>&nbsp;Student Education Document</h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="gvieweducationDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId,DocPath" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View&nbsp;File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("DocPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                        <asp:BoundField DataField="EducationDetailName" HeaderText="Education&nbsp;Type" SortExpression="EducationDetailName" />
                                        <asp:BoundField DataField="DocName" HeaderText="Name&nbsp;of&nbsp;Education&nbsp;document" SortExpression="DocName" Visible="false" />
                                        <asp:TemplateField HeaderText="Action" Visible="false">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkbtnAcademicsDoc" CommandName="eEdit" CausesValidation="false" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDeleteAcademicsDoc" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClientClick='<%# Eval("DocName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <div class="card card-info" style="display:none">
                <div class="card-header">
                    <h4 class="card-title"><i class="fas fa-network-wired"></i>&nbsp;Courses/Training Attended</h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="Gridstudentcerti" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CourseTitle" HeaderText="Course&nbsp;Title" />
                                        <asp:BoundField DataField="Duration" HeaderText="Duration" />
                                        <asp:BoundField DataField="InstituteName" HeaderText="Institute&nbsp;Name" />
                                        <asp:BoundField DataField="City" HeaderText="City" />
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
            <div class="form-group mb-0">
                <div class="">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkdeclaration" runat="server" CssClass="custom-checkbox" />
                    <label class="" for="Body_chkdeclaration"> I here by state that all the information furnished above is correct and true to the best of my knowledge.</label>
                </div>
            </div>
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important; margin-bottom: 5px;">
                <!-- /.col -->
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-xs-3">
                        <button runat="server" id="btnProfessionalDivPrivious" class="btn btn-info btn_general" onserverclick="btnProfessionalDivPrivious_ServerClick" causesvalidation="false" title="Search">
                            Previous
                        </button>
                        <button runat="server" id="btnFinalSubmit" class="btn btn-success btn_general" tabindex="4" title="Final Submit" onserverclick="btnFinalSubmit_ServerClick" visible="true">
                            Submit
                        </button>
                        <button runat="server" id="btnPrint" class="btn btn-primary btn_general" onserverclick="btnPrint_ServerClick" causesvalidation="false" visible="false" title="Search">
                            Print
                        </button>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <!-- /.col -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
