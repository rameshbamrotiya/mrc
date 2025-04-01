<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="StudentAcademics.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.StudentAcademics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Student Academic Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Student Acedemic Details</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <style>
        .whitespace {
            white-space: nowrap;
        }
    </style>
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-md-12 col-lg-12">
        <!-- Basic Information -->
        <div class="content_block">
            <div class="card  card-info">
                <div class="card-header">
                    <h3 class="card-title mb-0"><i class="fas fa-graduation-cap mr-2"></i>  Academic Details : (Starting from SSC/Equivalent)
                    </h3>
                </div>
                <asp:Panel ID="PanelAcademicDetails" runat="server" Enabled="false">
                    <div class="card-body">
                        <p style="color: red;">Note : Quality of Upload Proof of document should be Good Enough to Be Identifiable and Acceptable.</p>
                        <div class="row form-row">
                            <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality">Education Type</label>
                                    <asp:DropDownList ID="ddlEductionType" AutoPostBack="true" OnSelectedIndexChanged="ddlEductionType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEductionType" InitialValue="0" ForeColor="Red" runat="server" ControlToValidate="ddlEductionType"
                                        ErrorMessage="Select EductionType." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality">Education Name</label>
                                    <asp:DropDownList ID="ddlEducationName" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEducationName" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEducationName"
                                        ErrorMessage="Select EducationName." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label>Name of School/College<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtschoolname" runat="server" type="text" placeholder="Enter Name of School/College" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtschoolname"
                                        ErrorMessage="Please enter Name of School/Colege." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label>Board/University</label>
                                    <asp:TextBox ID="txtboard" runat="server" class="form-control" placeholder="Enter Board/University"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtboard"
                                        ErrorMessage="Please enter Board/University." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label for="exampleInputFile">Passing Month<span class="req-field">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select Month" Value="1"></asp:ListItem>
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
                                        <asp:RequiredFieldValidator ID="rfvMonth" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlAcademicYear"
                                            ErrorMessage="Select Passing month." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12">
                            <div class="form-group">
                                <label for="ddlHistoryYear">Passing Year</label>
                                <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAcademicYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlAcademicYear"
                                    ErrorMessage="Select Passing year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12">
                            <div class="form-group">
                                <label for="ddlStream">Stream</label>
                                <asp:DropDownList ID="ddlStream" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="" Text="Select" ></asp:ListItem>
                                    <asp:ListItem Value="N/A" Text="N/A"></asp:ListItem>
                                    <asp:ListItem Value="Science (Group A)" Text="Science (Group A)"></asp:ListItem>
                                    <asp:ListItem Value="Science (Group B)" Text="Science (Group B)"></asp:ListItem>
                                    <asp:ListItem Value="Science (Group AB)" Text="Science (Group AB)"></asp:ListItem>
                                    <asp:ListItem Value="Commerce" Text="Commerce"></asp:ListItem>
                                    <asp:ListItem Value="Arts" Text="Arts"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvStream" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlStream"
                                    ErrorMessage="Select stream." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12">
                            <div class="form-group">
                                <label>Percentage</label>
                                <asp:TextBox ID="txtdivision" runat="server" CssClass="form-control decimal" placeholder="Enter Percentage"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationmsg" runat="server" ControlToValidate="txtdivision"
                                    ErrorMessage="Please enter Division." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12">
                            <div class="form-group">
                                <label for="ddlNoOfTrials">No Of Trials</label>
                                <asp:DropDownList ID="ddlNoOfTrials" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" ForeColor="Red" runat="server" ControlToValidate="ddlNoOfTrials"
                                    ErrorMessage="Select no of trials." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12">
                            <div class="form-group">
                                <label><b>Upload document in pdf</b></label>
                                <asp:FileUpload ID="fumarksheet" runat="server" CssClass="form-control" onchange="ShowDOBPreview(this);" />
                                <p style="display: none;" id="Fupathmarksheet" runat="server"></p>
                                <p style="color:red; margin-bottom:0px;">Note: If you have passed out selected qualification in more than one attempt, please upload all your marksheets in single pdf file.</p>
                                <%--<p style="color:red; margin-bottom:0px;">Like: Upload all Document in one pdf.</p>--%>
                                <%--<asp:Label ID="lblDOB" runat="server" Visible="false" ForeColor="Red"></asp:Label>--%>
                                <%--<asp:RequiredFieldValidator ID="rfvDOB" CssClass="validationmsg" runat="server" ControlToValidate="fumarksheet" Display="Dynamic" EnableClientScript="true"
                                ErrorMessage="Upload date of birth proof." SetFocusOnError="true"><p class="help-block" style="color:red;">( Only .Jpeg or .Jpg or .png or .pdf)</p></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator CssClass="whitespace" ID="RegularExpressionValidator2" runat="server" ControlToValidate="fumarksheet" ErrorMessage="Only .pdf" Font-Bold="True" ForeColor="Red" ValidationExpression="(.*?)\.(pdf|PDF)$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-12 addgroup">
                            <div class="form-group ">

                                    <asp:Button ID="btnKnowPersonDetails" runat="server" CssClass="btn btn-success submit-btn" Style="float: right;" Text="Add Details" OnClick="btnKnowPersonDetails_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvAcademicDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId,MarksheetPath" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
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
                                        <asp:BoundField DataField="BoardUniversity" HeaderText="BoardUniversity" SortExpression="BoardUniversity" />
                                        <asp:BoundField DataField="PassingMonth" HeaderText="PassingMonth" SortExpression="PassingYear" />
                                        <asp:BoundField DataField="PassingYear" HeaderText="PassingYear" SortExpression="PassingYear" />
                                        <asp:BoundField DataField="Stream" HeaderText="Stream" SortExpression="MajorSubjects" />
                                        <asp:BoundField DataField="Division" HeaderText="Percentage&nbsp;Or&nbsp;Percentile" SortExpression="Division" />
                                        <asp:BoundField DataField="NoOfTrials" HeaderText="No Of Trials" SortExpression="NoOfTrials" />
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("MarksheetPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkbtnAcademics" CommandName="eEdit" OnClick="lnkbtnAcademics_Click" CausesValidation="false" runat="server" data-original-title="Edit" CssClass="btn edit_btn btn-success btn-sm show-tooltip mr-2">Edit</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDeleteAcademics" CommandName="eDelete" runat="server" OnClick="lnkbtnDeleteAcademics_Click" SkinID="lDelete" CausesValidation="false" OnClientClick='<%# Eval("NameOfSchoolCollege", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' data-original-title="Delete" CssClass="delete_btn btn btn-danger btn-sm show-tooltip">Delete</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnAcademicID" runat="server" />
                            </div>
                        </div>
                        <div class="row col-md-12" id="DivAcademicDetailsMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblstatusAD" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksAD" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksAD" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="card  card-info">
                <div class="card-header">
                    <h3 class="card-title mb-0"><i class="fas fa-book mr-2"></i>  Supportive Academic Documents
                    </h3>
                </div>
                <asp:Panel ID="PanelAcademicDocument" runat="server" Enabled="false">
                    <div class="card-body">
                        <p style="color: red;">Note : Quality of Upload Education document should be Good Enough to Be Identifiable and Acceptable.</p>
                        <div class="row form-row">
                            <asp:HiddenField ID="hfacademicdoc" Value="0" runat="server" />
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality">Education Type</label>
                                    <asp:DropDownList ID="ddleducationReq" AutoPostBack="false" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" ForeColor="Red" runat="server" ControlToValidate="ddlEductionType"
                                        ErrorMessage="Select EductionType." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12" style="display:none">
                                <div class="form-group">
                                    <label>Name of Education document<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtDocname" runat="server" type="text" placeholder="Enter Name of School/College" class="form-control"></asp:TextBox>
                               
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12">
                                <div class="form-group">
                                    <label><b>Upload Education document (Only .pdf)</b></label>
                                    <asp:FileUpload ID="fueducationdoc" runat="server" onchange="ShowDOBPreview(this);" />
                                    <p id="filepathDoc" runat="server" style="display: none;"></p>
                                    <%--<asp:Label ID="lblDOB" runat="server" Visible="false" ForeColor="Red"></asp:Label>--%>
                                    <%--<asp:RequiredFieldValidator ID="rfvDOB" CssClass="validationmsg" runat="server" ControlToValidate="fumarksheet" Display="Dynamic" EnableClientScript="true"
                                    ErrorMessage="Upload date of birth proof." SetFocusOnError="true"><p class="help-block" style="color:red;">( Only .Jpeg or .Jpg or .png or .pdf)</p></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator CssClass="whitespace" ID="RegularExpressionValidator1" runat="server" ControlToValidate="fumarksheet" ErrorMessage="Only .pdf" Font-Bold="True" ForeColor="Red" ValidationExpression="(.*?)\.(pdf|PDF)$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-4 col-12 addgroup">
                                <div class="form-group">

                                    <asp:Button ID="btnAddEducationDoc" runat="server" CssClass="btn btn-success submit-btn" CausesValidation="false" Style="float: right;" Text="Add Details" OnClick="btnAddEducationDoc_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <asp:GridView ID="gvieweducationDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,EducationId,EducationTypeId,DocPath" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist...">
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
                                        <asp:BoundField DataField="DocName" HeaderText="Name of Education document" SortExpression="DocName" Visible="false" />
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("DocPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkbtnAcademicsDoc" CommandName="eEdit" OnClick="lnkbtnAcademicsDoc_Click" CausesValidation="false" runat="server" data-original-title="Edit" CssClass="edit_btn btn btn-success btn-sm show-tooltip mr-2">Edit</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDeleteAcademicsDoc" CommandName="eDelete" runat="server" OnClick="lnkbtnDeleteAcademicsDoc_Click" SkinID="lDelete" CausesValidation="false" OnClientClick='<%# Eval("DocName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' data-original-title="Delete" CssClass="delete_btn btn btn-sm btn-danger show-tooltip">Delete</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </div>
                        </div>
                        <div class="row col-md-12" id="DivAcademicDocumentMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblstatusADoc" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksADoc" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksADoc" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="upAnnexure1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="card  card-info">
                        <div class="card-header">
                            <h3 class="card-title mb-0"><i class="fas fa-desktop mr-2"></i>  Computer Literacy
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-12">
                                    <div class="form-group">
                                        <label>Computer Literacy</label>
                                        <asp:DropDownList ID="ddlcomputerlit" CssClass="form-control select" TabIndex="3" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-9" id="CLRemarks" runat="server" visible="false">
                                    <div class="form-group mb-0">
                                        <label>Remarks</label>
                                        <textarea id="txtremerks" runat="server" placeholder="Enter Remarks" class="form-control"></textarea>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlcomputerlit" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="card  card-info" style="display:none">
                <div class="card-header">
                    <h3 class="card-title"><i class="fas fa-book"></i>  Courses / Training Attended
                    </h3>
                </div>
                <asp:Panel ID="PanelCoursesTrainingAttended" runat="server" Enabled="false">
                    <div class="card-body">
                        <div class="row form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Subject / Course Title</label>
                                    <asp:TextBox ID="txtSubjectCourseTitle" runat="server" CssClass="form-control" placeholder="Enter Subject / Course Title" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSubjectCourseTitle" ValidationGroup="btnAddCourses" CssClass="validationmsg" runat="server" ControlToValidate="txtSubjectCourseTitle"
                                        ErrorMessage="Enter Subject/Course Title." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Duration / Year</label>
                                    <asp:TextBox ID="txtDurationYear" runat="server" CssClass="form-control" placeholder="Enter Duration / Year"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDurationYear" ValidationGroup="btnAddCourses" CssClass="validationmsg" runat="server" ControlToValidate="txtDurationYear"
                                        ErrorMessage="Enter Duration/Year." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Organizing Institution / Organization</label>
                                    <asp:TextBox ID="txtOrganizingInstitution" runat="server" CssClass="form-control" placeholder="Enter Organizing Institution / Organization" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtOrganizingInstitution" ValidationGroup="btnAddCourses" CssClass="validationmsg" runat="server" ControlToValidate="txtOrganizingInstitution"
                                        ErrorMessage="Enter Organizing Institution/Organization." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Location</label>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="Enter Location" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtLocation" ValidationGroup="btnAddCourses" CssClass="validationmsg" runat="server" ControlToValidate="txtLocation"
                                        ErrorMessage="Enter Location." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-2 addgroup">
                                <asp:Button ID="btnAddCourses" runat="server" CssClass="btn btn-success submit-btn" Style="float: right;" Text="Add Courses" OnClick="btnAddCourses_Click" CausesValidation="true" ValidationGroup="btnAddCourses" />
                            </div>
                        </div>
                        <div class="row form-row">
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
                                                        <asp:LinkButton ID="lbtnEditCourses" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnEditCourses_Click" CssClass="btn btn-sm show-tooltip">Edit</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeleteCourses" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnDeleteCourses_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip">Delete</asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnCourses" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row col-md-12" id="DivCoursesTrainingAttendedMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblstatusCA" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksCA" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksCA" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="card  card-info">
                <div class="card-header">
                    <h3 class="card-title mb-0"><i class="fas fa-globe mr-2"></i>  Language
                    </h3>
                </div>
                <div class="card-body" id="Languagediv" runat="server">
                    <div class="education-info">
                        <div class="row form-row education-cont">
                            <div class="col-12 col-md-12 col-lg-12">
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
                                                                <asp:CheckBox runat="server"
                                                                    ID="chk1" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox runat="server"
                                                                    ID="chk2" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox runat="server"
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
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important; margin-bottom: 5px;"">
            <div class="row form-row mb-4">
                <div class="col-md-5">
                </div>
                <div class="col-md-3" style="text-align:center;">
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-info submit-btn" CausesValidation="false" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" CssClass="btn btn-secondary submit-btn" OnClick="btnSubmit_Click" Text="Next" />
                </div>
                <div class="col-md-4">
                </div>
            </div>
        </div>
    
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <script type="text/javascript">
        document.addEventListener('contextmenu', (e) => {
            e.preventDefault();
        }
          );
        document.onkeydown = function (e) {
            if (event.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }
        }
    </script>
</asp:Content>
