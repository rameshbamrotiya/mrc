<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.StudentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="contact-area register-card-body pt-50 pb-70">
        <div class="container">
            <div class="row justify-content-md-center">

                <div class="col-lg-12">
                    <section class="content">
                        <asp:HiddenField ID="hfPhotographName" runat="server" />
                        <asp:HiddenField ID="hfSignatureName" runat="server" />
                        <asp:HiddenField ID="hfDOBProofName" runat="server" />
                        <asp:HiddenField ID="hfStudentWorkflowId" runat="server" Value="0" />

                        <asp:UpdatePanel ID="upAnnexure1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-user"></i>&nbsp;Personal Information</h4>
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
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblveri" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlPersonalInformationId" disabled="true" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonalInformationId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPersonalInformationId" CssClass="validationmsg" runat="server" ControlToValidate="ddlPersonalInformationId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblremarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtremarks" CssClass="form-control" TextMode="MultiLine" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-pen-alt"></i>&nbsp;Address Details </h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:GridView ID="gvEducationDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                                            <asp:BoundField DataField="PresentPincode" HeaderText="Present Pincode" />
                                                            <asp:BoundField DataField="PresentCountry" HeaderText="Present Country" />
                                                            <asp:BoundField DataField="PresentState" HeaderText="PresentState" />
                                                            <asp:BoundField DataField="PresentCity" HeaderText="PresentDistrict" />
                                                            <asp:BoundField DataField="PresentTaluka" HeaderText="PresentTaluka" />
                                                            <asp:BoundField DataField="PresentPhoneR" HeaderText="PresentPhoneR" />
                                                            <asp:BoundField DataField="PresentPhoneM" HeaderText="PresentPhoneM" />
                                                            <asp:BoundField DataField="ParmenentAddress" HeaderText="PermanentAddress" />
                                                            <asp:BoundField DataField="ParmenentPincode" HeaderText="PermanentPincode" />
                                                            <asp:BoundField DataField="ParmenentCountry" HeaderText="PermanentCountry" />
                                                            <asp:BoundField DataField="ParmenentState" HeaderText="PermanentState" />
                                                            <asp:BoundField DataField="ParmenentCity" HeaderText="PermanentDistrict" />
                                                            <asp:BoundField DataField="ParmenentTaluka" HeaderText="ParmenentTaluka" />
                                                            <asp:BoundField DataField="ParmenentPhoneR" HeaderText="PermanentPhoneR" />
                                                            <asp:BoundField DataField="ParmenentPhoneM" HeaderText="PermanentPhoneM" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblAddressId" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlAddressId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAddressId" CssClass="validationmsg" runat="server" ControlToValidate="ddlAddressId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblAddressRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtAddressRemarks" CssClass="form-control" TextMode="MultiLine" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card  card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-upload"></i>&nbsp;Document Upload</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="education-info">
                                            <div class="row form-row education-cont">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label><b>Photograph</b></label><br />
                                                                <div class="form-group">
                                                                    <asp:Image ID="PhotographPreview" runat="server" CssClass="img-thumbnail" AlternateText="Photograph" Width="150" Height="175" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label><b>Signature</b></label><br />
                                                                <asp:Image ID="SignaturePriview" runat="server" CssClass="img-thumbnail" AlternateText="Signature" Width="150" Height="175" Visible="false" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label><b>Upload Proof</b></label>
                                                                <br />
                                                                  <asp:Image ID="AadharcardPreview" runat="server" CssClass="img-thumbnail" AlternateText="Signature" Width="150" Height="175" Visible="false" />
                                                                <%--<asp:Button ID="viewfile" runat="server" OnClick="viewfile_Click" CssClass="btn btn-primary btn_general" Text="View Upload File" />--%>
                                                                <%--<asp:Button ID="btnViewDOBFile" runat="server" CssClass="btn btn-primary btn_general" CausesValidation="false" Text="View Upload File" OnClick="btnViewDOBFile_Click" OnClientClick="SetTarget();" Visible="false" Style="margin-top: 22px;" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblDocumentId" runat="server" Text="Verification"></asp:Label>
                                                                <asp:DropDownList ID="ddlDocumentId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentId_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDocumentId" CssClass="validationmsg" runat="server" ControlToValidate="ddlDocumentId"
                                                                    ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblDocumentRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txtDocumentRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-users"></i>&nbsp;Family Member Information</h4>
                                    </div>

                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:GridView ID="gvCertificateCourseDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
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
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblFamilyMemberId" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlFamilyMemberId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFamilyMemberId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFamilyMemberId" CssClass="validationmsg" runat="server" ControlToValidate="ddlFamilyMemberId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblFamilyMemberRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtFamilyMemberRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-school"></i>&nbsp;Student Education Details</h4>
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
                                                            <asp:BoundField DataField="EducationTypeId" HeaderText="Education&nbsp;Type" />
                                                            <asp:BoundField DataField="EducationName" HeaderText="Education&nbsp;Name" />
                                                            <asp:BoundField DataField="NameOfSchoolCollege" HeaderText="Name&nbsp;Of&nbsp;School/College" />
                                                            <asp:BoundField DataField="BoardUniversity" HeaderText="Board&nbsp;University" />
                                                            <asp:BoundField DataField="PassingMonth" HeaderText="Passing&nbsp;Month" />
                                                            <asp:BoundField DataField="PassingYear" HeaderText="Passing&nbsp;Year" />
                                                            <asp:BoundField DataField="Stream" HeaderText="Stream" />
                                                            <asp:BoundField DataField="PercentageOrPercentile" HeaderText="Percentage&nbsp;Or&nbsp;Percentile" />
                                                            <%--<asp:BoundField DataField="Division" HeaderText="Division" />--%>
                                                            <asp:BoundField DataField="NoOfTrials" HeaderText="No&nbsp;Of&nbsp;Trials" />
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <a id="aCertificateFile" href='<%# Eval("MarksheetPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblAcademicId" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlAcademicId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAcademicId" CssClass="validationmsg" runat="server" ControlToValidate="ddlAcademicId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblAcademicRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtAcademicRemarks"  TextMode="MultiLine"  CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-school"></i>&nbsp;Supportive Academic Documents</h4>
                                    </div>

                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    <asp:GridView ID="gvieweducationDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId,DocPath" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                                            <asp:BoundField DataField="EducationDetailName" HeaderText="Education&nbsp;Type" />
                                                            <asp:BoundField DataField="DocName" HeaderText="Name&nbsp;of&nbsp;Education&nbsp;document" Visible="false" />
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <a id="afile" href='<%# Eval("DocPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblEducationDocId" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlEducationDocId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEducationDocId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEducationDocId" CssClass="validationmsg" runat="server" ControlToValidate="ddlEducationDocId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblEducationDocRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtEducationDocRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-info" style="display:none">
                                    <div class="card-header">
                                        <h4 class="card-title"><i class="fa fa-book"></i>&nbsp;Courses / Training Attended</h4>
                                    </div>

                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    <asp:GridView ID="Gridstudentcerti" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CourseTitle" HeaderText="CourseTitle" />
                                                            <asp:BoundField DataField="Duration" HeaderText="Duration" />
                                                            <asp:BoundField DataField="InstituteName" HeaderText="InstituteName" />
                                                            <asp:BoundField DataField="City" HeaderText="City" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Label ID="lblCoursesId" runat="server" Text="Verification"></asp:Label>
                                                    <asp:DropDownList ID="ddlCoursesId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCoursesId_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCoursesId" CssClass="validationmsg" runat="server" ControlToValidate="ddlCoursesId"
                                                        ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:Label ID="lblCoursesRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtCoursesRemarks"  TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div>
                                    <div class="card card-info">
                                        <div class="card-header">
                                            <h4 class="card-title card-info"><i class="fa fa-accusoft"></i><b>&nbsp;Other Information</b>
                                            </h4>
                                        </div>
                                        <div class="card-body">
                                            <asp:Panel ID="pnlother" runat="server" Enabled="false">
                                                <div class="row form-row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Known person in UNMICRC, if any : <span class="text-danger">*</span></label>
                                                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLknowperson">
                                                                <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:TextBox ID="txtUNMICRCPerson" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Do you suffer from any chronical illness?  If yes, give details: <span class="text-danger">*</span></label>
                                                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLilleness">
                                                                <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:TextBox ID="txtChronicillness" runat="server" class="form-control"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Your current accommodation :</label>
                                                            <asp:RadioButtonList ID="rblaccommodation" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="Own" Text="Own"></asp:ListItem>
                                                                <asp:ListItem Value="Rental" Text="Rental"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Extra curricular Activities / Hobbies :</label>
                                                            <asp:TextBox ID="txtextraactivities" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Interest in Social Activities :(Specify)</label>
                                                            <asp:TextBox ID="txtsocialactivities" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>How did you heard about this Course?</label>
                                                            <table class="table table-bordered table-hover">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBoxList Width="100%" RepeatDirection="Horizontal" runat="server" ID="chk1" />
                                                                        <div class="col-md-9" id="CLRemarks" runat="server" visible="false">
                                                                            <div class="form-group mb-0">
                                                                                <label>If other please specify:</label>
                                                                                <textarea id="txtcourseheard" runat="server" readonly="readonly" class="form-control"></textarea>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblOtherInfoId" runat="server" Text="Verification"></asp:Label>
                                                        <asp:DropDownList ID="ddlOtherInfoId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOtherInfoId_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvOtherInfoId" CssClass="validationmsg" runat="server" ControlToValidate="ddlOtherInfoId"
                                                            ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblOtherInfoRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtOtherInfoRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /Basic Information -->
                                    <div class="card card-info">
                                        <div class="card-header">
                                            <h4 class="card-title card-info"><i class="fa fa-gavel"></i><b>&nbsp;Has any court of law in India or abroad ever convicted you? If yes, give details.</b></h4>
                                        </div>
                                        <div class="card-body">
                                            <asp:Panel ID="pnllaw" runat="server" Enabled="false">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                Has any court of law in India or abroad ever convicted you? If yes, give details. 
                                                        <asp:RadioButtonList ID="rblcourtoflaw" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="NO" Text="No"></asp:ListItem>
                                                        </asp:RadioButtonList></label>
                                                            <asp:TextBox Visible="false" ID="txtcourtoflaw" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbllawId" runat="server" Text="Verification"></asp:Label>
                                                        <asp:DropDownList ID="ddllawId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddllawId_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvlawId" CssClass="validationmsg" runat="server" ControlToValidate="ddllawId"
                                                            ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbllawReamrks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtlawReamrks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card card-info">
                                        <div class="card-header">
                                            <h4 class="card-title card-info"><i class="fa fa-ambulance"></i><b>&nbsp;Emergency Details </b></h4>
                                        </div>
                                        <div class="card-body">
                                            <asp:Panel ID="pnlEmergency" runat="server" Enabled="false">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Blood Group: </label>
                                                            <asp:TextBox ID="txtbloodgroup" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Allergic to </label>
                                                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLAllergicto">
                                                                <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label></label>
                                                            <asp:TextBox ID="txtmajorillness" runat="server" class="form-control" placeholder="Enter Allergic to" Visible="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Last Major illness/Surgery </label>
                                                            <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="RBLSurgery">
                                                                <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label></label>
                                                            <asp:TextBox ID="txtsurgeryinfo" TextMode="MultiLine" runat="server" class="form-control" Visible="false" placeholder="Enter Last Major illness/Surgery"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" id="DIVSurgeryMonth" visible="false">
                                                        <div class="form-group">
                                                            <label for="exampleInputFile">Surgery Month<span class="req-field">*</span></label>
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
                                                    <div class="col-md-3" runat="server" id="DIVSurgeryYear" visible="false">
                                                        <div class="form-group">
                                                            <label for="ddlHistoryYear">Surgery Year</label>
                                                            <asp:DropDownList ID="ddlExtrInfoYear" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Contact person in case of emergency:</label>
                                                                <asp:TextBox ID="txtconperson" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Contact No.:</label>
                                                                <asp:TextBox ID="txtcontactno" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="12"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Relation</label>
                                                                <asp:TextBox ID="txtrelation" runat="server" class="form-control" placeholder="Enter Relation"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Address:</label>
                                                                <asp:TextBox ID="txtaddress" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblEmergencyId" runat="server" Text="Verification"></asp:Label>
                                                        <asp:DropDownList ID="ddlEmergencyId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmergencyId_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvEmergencyId" CssClass="validationmsg" runat="server" ControlToValidate="ddlEmergencyId"
                                                            ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblEmergencyRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtEmergencyRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Education -->
                                    <div class="card card-info" style="display:none">
                                        <div class="card-header">
                                            <h4 class="card-title card-info"><i class="fa fa-trash-o"></i><b>&nbsp;Please give references of persons who know you professionally and Socially</b></h4>
                                        </div>
                                        <div class="card-body">

                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                        <asp:GridView ID="grdReferalDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1  %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Person&nbsp;Name" />
                                                                <asp:BoundField DataField="Position" HeaderText="Position" />
                                                                <asp:BoundField DataField="MobileNO" HeaderText="Mobile&nbsp;No." />
                                                                <asp:BoundField DataField="RelationShip" HeaderText="RelationShip" />
                                                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                                                <asp:BoundField DataField="YearsKnown" HeaderText="Years&nbsp;Known" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblReferencesId" runat="server" Text="Verification"></asp:Label>
                                                        <asp:DropDownList ID="ddlReferencesId" CssClass="form-control select" disabled="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReferencesId_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Approve" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvReferencesId" CssClass="validationmsg" runat="server" ControlToValidate="ddlReferencesId"
                                                            ErrorMessage="Select Verification." SetFocusOnError="true" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblReferencesRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtReferencesRemarks" TextMode="MultiLine" CssClass="form-control" Visible="false"  runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPersonalInformationId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlAddressId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlDocumentId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFamilyMemberId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlAcademicId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlEducationDocId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlCoursesId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlOtherInfoId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddllawId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlEmergencyId" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlReferencesId" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="row">
                            <!-- /.col -->
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary submit-btn" Style="background: #4eb64e;" OnClick="btnSave_Click" />
                                <%--<asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn btn-primary submit-btn" style="background: #c94646;" OnClick="btnreject_Click" OnClientClick="javascript:return confirm('Are you sure you want to reject this application?');" />--%>
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
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
     <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
</asp:Content>
