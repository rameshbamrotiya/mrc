<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BasicDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.BasicDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card card-info">
                <div class="card-header">
                    <h5 class="card-title"><i class="fa fa-users"></i>Personal Information
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row form-row">
                        <asp:HiddenField ID="hfPhotographName" runat="server" />
                        <asp:HiddenField ID="hfSignatureName" runat="server" />
                        <asp:HiddenField ID="hfDOBProofName" runat="server" />
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:HiddenField ID="hfStudentDetailsId" runat="server" Value="0" />
                                 <asp:HiddenField ID="hfStudentWorkflowId" runat="server" Value="0" />
                                <label>Course Code Applied For </label>
                                <asp:Label ID="lblPostAppliedFor" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Title</label>
                                    <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>Full Name </label>
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <%--<div class="col-md-4">
                                <div class="form-group">
                                    <label>Middle Name </label>
                                    <asp:Label ID="lblMiddleName" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>--%>
                            <%--<div class="col-md-4">
                                <div class="form-group">
                                    <label>Last Name </label>
                                    <asp:Label ID="lblLastName" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>--%>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Date of Birth</label>
                                    <div class="cal-icon">
                                        <asp:Label ID="lblDateOfBirth" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Age</label>
                                    <asp:HiddenField ID="hfAge" runat="server" />
                                    <asp:Label ID="lblAge" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Place of Birth</label>
                                    <asp:TextBox ID="txtBirthPlace" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                    <%--                                    <asp:TextBox ID="txtBirthPlace" MaxLength="12" CssClass="form-control" ReadOnly="true" onkeypress="return lettersWithSpaceOnly(event)" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvBirthPlace" CssClass="validationmsg" runat="server" ControlToValidate="txtBirthPlace"
                                        ErrorMessage="Enter place of birth." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Cast</label>
                                    <asp:DropDownList ID="ddlCast" CssClass="form-control select" Enabled="false" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvCast" CssClass="validationmsg" runat="server" ControlToValidate="ddlCast"
                                        ErrorMessage="Select cast." SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Marital Status</label>
                                    <asp:Label ID="lblMaritalStatus" runat="server" ReadOnly="true" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Gender</label>
                                    <asp:Label ID="lblGender" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Religion</label>
                                    <asp:DropDownList ID="ddlReligion" CssClass="form-control select" Enabled="false" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvReligion" CssClass="validationmsg" runat="server" ControlToValidate="ddlReligion"
                                        ErrorMessage="Select religion." SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblveri" runat="server" Text="Verification"></asp:Label>
                                <asp:DropDownList ID="ddlPersonalInformationId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonalInformationId_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group">
                                <asp:Label ID="lblremarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtremarks" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card  card-info">
                <div class="card-header">
                    <h5 class="card-title"><i class="fa fa-edit"></i>Address Details</h5>
                </div>

                <div class="card-body">
                    <asp:Panel ID="pnlname" runat="server" Enabled="false">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Present Address <span class="text-danger">*</span></label>
                                            <%--<asp:LinkButton ID="PresentAddress" runat="server" Style="float: right; color: blue;" OnClick="PresentAddress_Click" CausesValidation="false">--></asp:LinkButton>--%>
                                            <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Pin Code<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPresentPincode" MaxLength="6" CssClass="form-control " onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Country<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPresentCountry" CssClass="form-control select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPresentCountry_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>State<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPresentState" CssClass="form-control select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPresentState_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>City<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPresentCity" CssClass="form-control select" runat="server" OnSelectedIndexChanged="ddlPresentCity_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Taluka<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPresentTaluka" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Phone Number (R)</label>
                                            <asp:TextBox ID="txtPresentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Phone Number (M)</label>
                                            <asp:TextBox ID="txtPresentMPhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 ">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Permenent Address <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPermenentAddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Pin Code<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPermenentPincode" MaxLength="6" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Country<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPermenentCountry" CssClass="form-control select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPermenentCountry_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>State<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPermenentState" CssClass="form-control select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPermenentState_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>City<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPermenentCity" CssClass="form-control select" runat="server" OnSelectedIndexChanged="ddlPermenentCity_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Taluka<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPermenentTaluka" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Phone Number (R)</label>
                                            <asp:TextBox ID="txtPermenentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Phone Number (M)</label>
                                            <asp:TextBox ID="txtPermenentMPhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- </asp:Panel>--%>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblAddressId" runat="server" Text="Verification"></asp:Label>
                                <asp:DropDownList ID="ddlAddressId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressId_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group">
                                <asp:Label ID="lblAddressRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtAddressRemarks" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                </div>

            </div>

            <!-- /Basic Information -->
            <!-- File Upload Section -->
            <div class="card  card-info">
                <div class="card-header">
                    <h5 class="card-title"><i class="fa fa-upload"></i>Document Upload
                    </h5>
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
                                            <%--  <asp:FileUpload ID="fuPhotograph" runat="server" onchange="ShowPhotographPreview(this);" />
                                            <asp:Label ID="lblPhotograph" runat="server" Style="display: none;" ForeColor="Red"></asp:Label><br />--%>
                                            <%--  <asp:RequiredFieldValidator ID="rfvPhotograph" CssClass="validationmsg" runat="server" ControlToValidate="fuPhotograph" Display="Dynamic" EnableClientScript="true"
                                                ErrorMessage="Upload photograph." SetFocusOnError="true"><p class="help-block" style="color:red;">Width: 280 px, Height: 300 px (Only .Jpeg or .Jpg or .png)</p></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                                                ControlToValidate="fuPhotograph"
                                                ErrorMessage="Only .Jpeg or .Jpg or .png" Font-Bold="True" ForeColor="Red"
                                                ValidationExpression="(.*?)\.(jpg|jpeg|png|JPG|JPEG|PNG)$"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2">
                                        <asp:Button ID="btnUploadPhotograph" runat="server" CssClass="btn btn-primary btn_general" OnClick="btnUploadPhotograph_Click" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                    </div>--%>



                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><b>Signature</b></label><br />
                                            <asp:Image ID="SignaturePriview" runat="server" CssClass="img-thumbnail" AlternateText="Signature" Width="150" Height="175" Visible="false" />
                                            <%-- <asp:FileUpload ID="fuSignature" runat="server" onchange="return ShowSignaturePreview(this);" />
                                            <asp:Label ID="lblSignature" runat="server" Style="display: none;" ForeColor="Red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvSignature" CssClass="validationmsg" runat="server" ControlToValidate="fuSignature" Display="Dynamic" EnableClientScript="true"
                                                ErrorMessage="Upload signature." SetFocusOnError="true"><p class="help-block" style="color:red;">Width: 280 px, Height: 300 px ( Only .Jpeg or .Jpg or .png)</p></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="fuSignature"
                                                ErrorMessage="Only .Jpeg or .Jpg or .png" Font-Bold="True" ForeColor="Red"
                                                ValidationExpression="(.*?)\.(jpg|jpeg|png|JPG|JPEG|PNG)$"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2">
                                        <asp:Button ID="btnUploadSignature" runat="server" CssClass="btn btn-primary btn_general" OnClick="btnUploadSignature_Click" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                    </div>--%>
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            
                                        </div>
                                    </div>--%>


                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><b>Upload Proof</b></label>
                                            <br />
                                            <asp:Button ID="btnViewDOBFile" runat="server" CssClass="btn btn-primary btn_general" CausesValidation="false" Text="View Upload File" OnClick="btnViewDOBFile_Click" Visible="false" Style="margin-top: 22px;" />
                                            <%--<asp:FileUpload ID="fuDOB" runat="server" onchange="ShowDOBPreview(this);" />--%><br />
                                            <%-- Like: Aadhar Card, Leaving Certificate, Birth Certificate Or
                                            <br />
                                            any issued certificate from government authority.--%>

                                            <%-- <asp:Label ID="lblDOB" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvDOB" CssClass="validationmsg" runat="server" ControlToValidate="fuDOB" Display="Dynamic" EnableClientScript="true"
                                                ErrorMessage="Upload date of birth proof." SetFocusOnError="true"><p class="help-block" style="color:red;">( Only .Jpeg or .Jpg or .png or .pdf)</p></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                ControlToValidate="fuDOB"
                                                ErrorMessage="Only .Jpeg or .Jpg or .png or .pdf" Font-Bold="True" ForeColor="Red"
                                                ValidationExpression="(.*?)\.(jpg|jpeg|png|pdf|JPG|JPEG|PNG|PDF)$"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2">
                                        <asp:Button ID="btnDOBUpload" runat="server" CssClass="btn btn-primary btn_general" OnClick="btnDOBUpload_Click" OnClientClick="return validate();" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                    </div>--%>
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblDocumentId" runat="server" Text="Verification"></asp:Label>
                                            <asp:DropDownList ID="ddlDocumentId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentId_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:Label ID="lblDocumentRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtDocumentRemarks" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /File Upload Section -->
            <!-- Education -->
            <div class="card card-info">
                <div class="card-header">
                    <h5 class="card-title"><i class="fa fa-house-user"></i>Family Member Information
                    </h5>
                </div>
                <div class="card-body">
                    <div class="education-info">
                        <div class="row form-row education-cont">
                            <%-- <div class="col-12 col-md-10 col-lg-11">
                                <div class="row form-row">
                                    <asp:HiddenField ID="hfRelativeId" runat="server" Value="0" />
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtFamilyPersonName" runat="server" onkeypress="return lettersWithSpaceOnly(event)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Age</label>
                                            <asp:TextBox ID="txtFamilyAge" MaxLength="3" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Relation</label>
                                            <asp:DropDownList ID="ddlFamilyRelation" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Occupation</label>
                                            <asp:TextBox ID="txtFamilyOccupation" runat="server" onkeypress="return lettersOnly(event)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Monthly Income</label>
                                            <asp:TextBox ID="txtFamilyMonthlyIncome" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnMember" runat="server" CssClass="btn btn-primary submit-btn" Style="margin-top: 10px;" Text="Add Member" OnClick="btnMember_Click" CausesValidation="false" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary submit-btn" Style="margin-top: 10px;" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 col-md-12 col-lg-12">
                                        <asp:GridView ID="gvFamilyMember" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,RelationId" CssClass="table table-bordered table-hover table-striped"
                                            EmptyDataText="Record does not exist...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr&nbspno" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                                <asp:BoundField DataField="MemberName" HeaderText="Name" SortExpression="MemberName" />
                                                <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                                <asp:BoundField DataField="RelationName" HeaderText="RelationName" SortExpression="RelationName" />
                                                <asp:BoundField DataField="Occupation" HeaderText="Occupation" SortExpression="Occupation" />
                                                <asp:BoundField DataField="MonthlyIncome" HeaderText="Monthly Income" SortExpression="MonthlyIncome" />

                                                <%--<asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkFamilyMemberEdit" runat="server" OnClick="lnkFamilyMemberEdit_Click" CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkFamilyMemberDelete" runat="server" OnClick="lnkFamilyMemberDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblFamilyMemberId" runat="server" Text="Verification"></asp:Label>
                                            <asp:DropDownList ID="ddlFamilyMemberId" CssClass="form-control select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFamilyMemberId_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Correction" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Approve" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:Label ID="lblFamilyMemberRemarks" runat="server" Text="Remarks" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtFamilyMemberRemarks" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
            <!-- /Education -->
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBack" runat="server" CausesValidation="false" CssClass="btn btn-primary submit-btn" OnClick="btnBack_Click" Text="Back" />
                        <asp:Button ID="btnSaveAndNext" runat="server" Text="Next" CssClass="btn btn-primary submit-btn" OnClick="btnSaveAndNext_Click" />
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>
        $(document).ready(function () {

        });
   <%--     function ShowPhotographPreview(input) {
            var array = ['jpg', 'jpeg', 'png', 'JPG', 'JPEG', 'PNG'];
            var xyz = input.files[0].name;
            var Extension = input.files[0].name.split('.')[1].toLowerCase();

            if (array.indexOf(Extension) <= -1) {
                document.getElementById("<%=btnUploadPhotograph.ClientID%>").style.display = "none";
                document.getElementById("<%=fuPhotograph.ClientID %>").value = "";
                document.getElementById("<%=fuPhotograph.ClientID %>").focus();
                alert("Upload only jpg, jpeg or png extension file...!!!");
                return false;
            }

            if (input.files[0].size > 102400) {
                document.getElementById("<%=lblPhotograph.ClientID%>").innerHTML = "File size must not exceed 100 KB";
                document.getElementById("<%=lblPhotograph.ClientID%>").style.display = "block";
                document.getElementById("<%=btnUploadPhotograph.ClientID%>").style.display = "none";
                document.getElementById("<%=fuPhotograph.ClientID %>").value = "";
                return false;
            }
            else {
                document.getElementById("<%=lblPhotograph.ClientID%>").innerHTML = "";
                document.getElementById("<%=lblPhotograph.ClientID%>").style.display = "none";
                document.getElementById("<%=btnUploadPhotograph.ClientID%>").style.display = "block";
                return true;
            }
        }

        function ShowSignaturePreview(input) {
            var array = ['jpg', 'jpeg', 'png', 'JPG', 'JPEG', 'PNG'];
            var xyz = input.files[0].name;
            var Extension = input.files[0].name.split('.')[1].toLowerCase();

            if (array.indexOf(Extension) <= -1) {
                document.getElementById("<%=btnUploadSignature.ClientID%>").style.display = "none";
                document.getElementById("<%=fuSignature.ClientID %>").value = "";
                document.getElementById("<%=fuSignature.ClientID %>").focus();
                alert("Upload only jpg, jpeg or png extension file...!!!");
                return false;
            }

            if (input.files[0].size > 102400) {
                document.getElementById("<%=lblSignature.ClientID%>").innerHTML = "File size must not exceed 100 KB";
                document.getElementById("<%=lblSignature.ClientID%>").style.display = "block";
                document.getElementById("<%=btnUploadSignature.ClientID%>").style.display = "none";
                document.getElementById("<%=fuSignature.ClientID %>").value = "";
                return false;
            }
            else {
                document.getElementById("<%=lblSignature.ClientID%>").innerHTML = "";
                document.getElementById("<%=lblSignature.ClientID%>").style.display = "none";
                document.getElementById("<%=btnUploadSignature.ClientID%>").style.display = "block";
                return true;
            }
        }--%>

        function ShowDOBPreview(input) {
            var array = ['jpg', 'jpeg', 'png', 'pdf', 'JPG', 'JPEG', 'PNG', 'PDF'];
            var xyz = input.files[0].name;
            var Extension = input.files[0].name.split('.')[1].toLowerCase();

            if (array.indexOf(Extension) <= -1) {
                <%-- document.getElementById("<%=btnDOBUpload.ClientID%>").style.display = "none";--%>
               <%-- document.getElementById("<%=fuDOB.ClientID %>").value = "";
                document.getElementById("<%=fuDOB.ClientID %>").focus();--%>
                alert("Upload only jpg, jpeg, png or pdf extension file...!!!");
                return false;
            }

            if (input.files && input.files[0]) {
                <%--document.getElementById("<%=btnDOBUpload.ClientID%>").style.display = "block";--%>
            }
        }
    </script>
</asp:Content>
