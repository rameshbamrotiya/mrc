<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.StudentRegistration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Apply Now</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Student Registration</a></li>
                <li>/</li>
                <li>Apply Now</li>
            </ul>

        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
                <div class="form-row pl-md-2">
                    <div class="form-group">
                           <asp:Button ID="btnBackToGrid" runat="server" Text="Back" CssClass="btn backtolist_btn submit-btn" OnClick="btnBackToGrid_Click" style="float:right" CausesValidation="false"/>
                    </div>
                </div>
            <div class="card content_block">
                <div class="card card-info">
                
                    <div class="card-header">
                        <h3 class="card-title mb-0"><i class="fas fa-users mr-2"></i>Personal Information
                        </h3>
                    </div>
                    <asp:Panel ID="PanelPersonalInformation" runat="server" Enabled="false">
                        <div class="card-body">
                            <div class="row form-row">
                                <asp:HiddenField ID="hfPhotographName" runat="server" />
                                <asp:HiddenField ID="hfSignatureName" runat="server" />
                                <asp:HiddenField ID="hfDOBProofName" runat="server" />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hfStudentDetailsId" runat="server" Value="0" />
                                            <label>Course Code Applied For </label>
                                            <asp:Label ID="lblPostAppliedFor" runat="server" type="text" class="form-control" AutoPostBack="true" BackColor="LightGray"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Title<span class="req-field">*</span></label>
                                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control select">
                                                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Dr." Value="Dr."></asp:ListItem>
                                                <asp:ListItem Text="Prof." Value="Prof."></asp:ListItem>
                                                <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                <asp:ListItem Text="Ms." Value="Ms."></asp:ListItem>
                                                <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTitle" CssClass="validationmsg" runat="server" ControlToValidate="ddlTitle"
                                                ErrorMessage="Select title." SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Your Name</label>
                                            <asp:TextBox ID="lblFirstName" runat="server" class="form-control" placeholder="Your Name" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ErrorMessage="Enter Your Name*" Display="Dynamic" ForeColor="Red" ControlToValidate="lblFirstName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                            <%-- <asp:Label ID="lblFirstName" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Father/Husband Name</label>
                                            <asp:TextBox ID="lblMiddleName" runat="server" class="form-control" placeholder="Father/Husband Name" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ErrorMessage="Enter Father/Husband Name*" Display="Dynamic" ForeColor="Red" ControlToValidate="lblMiddleName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                            <%--<asp:Label ID="lblMiddleName" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Surname </label>
                                            <%--<asp:Label ID="lblLastName" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                            <asp:TextBox ID="lblLastName" runat="server" class="form-control" placeholder="Surname" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Enter Surname*" Display="Dynamic" ForeColor="Red" ControlToValidate="lblLastName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Email</label>
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email" ValidationGroup="Main"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Enter Email*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Date of Birth</label>
                                            <div class="cal-icon">
                                                <asp:TextBox ID="lblDateOfBirth" runat="server" class="form-control" placeholder="Birth Date" ValidationGroup="Main" OnTextChanged="lblDateOfBirth_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ErrorMessage="Select Birth Date*" Display="Dynamic" ForeColor="Red" ControlToValidate="lblDateOfBirth" ValidationGroup="Main"></asp:RequiredFieldValidator>
                                                <%--<asp:Label ID="lblDateOfBirth" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Age</label>
                                            <asp:HiddenField ID="hfAge" runat="server" />
                                            <asp:Label ID="lblAge" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Place of Birth</label>
                                            <asp:TextBox ID="txtBirthPlace" MaxLength="100" CssClass="form-control" onpaste="return OnlyAllowedlettersWithSpaceOnlyPaste(this);" onkeypress="return OnlyAllowedlettersWithSpaceOnly(event)" runat="server" placeholder="Enter place of birth"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBirthPlace" CssClass="validationmsg" runat="server" ControlToValidate="txtBirthPlace"
                                                ErrorMessage="Enter place of birth." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Caste</label>
                                            <asp:DropDownList ID="ddlCast" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCast" CssClass="validationmsg" runat="server" ControlToValidate="ddlCast"
                                                ErrorMessage="Select caste." SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Marital Status</label>
                                            <asp:DropDownList ID="ddlMaritalStatus" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>
                                            <%--<asp:Label ID="lblMaritalStatus" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Gender</label>
                                            <asp:DropDownList ID="ddlGender" CssClass="form-control select" runat="server"></asp:DropDownList>
                                            <%--<asp:Label ID="lblGender" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-12">
                                        <div class="form-group">
                                            <label>Religion</label>
                                            <asp:DropDownList ID="ddlReligion" CssClass="form-control select" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReligion" CssClass="validationmsg" runat="server" ControlToValidate="ddlReligion"
                                                ErrorMessage="Select religion." SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12" id="DivAststusPIMain" runat="server" visible="false">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Application Status</label>
                                            <asp:Label ID="lblstatus" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-10" id="LblRemerksPI" runat="server">
                                        <div class="form-group">
                                            <label>Remerks</label>
                                            <asp:TextBox ReadOnly="true" ID="txtremerks" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                            <%--<asp:TextBox TextMode="MultiLine" ID="lblremerks" runat="server" CssClass="form-control" BackColor="LightGray"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card  card-info">
                    <div class="card-header">
                        <h3 class="card-title mb-0"><i class="fas fa-edit mr-2"></i>Address Details</h3>
                    </div>
                    <asp:Panel ID="PanelAddressDetails" runat="server" Enabled="false">
                        <div class="card-body">
                            <div class="row col-md-12">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-5">
                                </div>
                            </div>
                          <%--  <asp:UpdatePanel ID="upAnnexure1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <label>Present Address <span class="text-danger">*</span></label>
                                            <div class="card">
                                                <div class="card-body p-0">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" placeholder="Enter present address"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPresentAddress" CssClass="validationmsg" runat="server" ControlToValidate="txtPresentAddress"
                                                                    ErrorMessage="Enter present address." Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Country<span class="text-danger">*</span></label>
                                                                <asp:DropDownList ID="ddlPresentCountry" CssClass="form-control select2bs4" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPresentCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPresentCountry" CssClass="validationmsg" runat="server" ControlToValidate="ddlPresentCountry"
                                                                    ErrorMessage="Select present country." SetFocusOnError="true" Display="Dynamic" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>State<span class="text-danger">*</span></label>
                                                                <asp:DropDownList ID="ddlPresentState" CssClass="form-control select2bs4" AutoPostBack="false" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPresentState" CssClass="validationmsg" runat="server" ControlToValidate="ddlPresentState"
                                                                    ErrorMessage="Select present state." Display="Dynamic" SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>District<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="ddlPresentCity" CssClass="form-control select2bs4" runat="server"></asp:TextBox>
                                                              <%--  <asp:DropDownList ID="ddlPresentCity" CssClass="form-control select2bs4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentCity_SelectedIndexChanged">
                                                                </asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="rfvPresentCity" CssClass="validationmsg" runat="server" ControlToValidate="ddlPresentCity"
                                                                    ErrorMessage="Select present city." Display="Dynamic" SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Taluka<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="ddlPresentTaluka" CssClass="form-control select2bs4" runat="server"></asp:TextBox>
                                                               <%-- <asp:DropDownList ID="ddlPresentTaluka" CssClass="form-control select2bs4" runat="server">
                                                                </asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="ddlPresentCity"
                                                                    ErrorMessage="Select present Taluka." Display="Dynamic" SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Pin Code<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="txtPresentPincode" MaxLength="6" CssClass="form-control " onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter pin code"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPresentPincode" CssClass="validationmsg" Display="Dynamic" runat="server" ControlToValidate="txtPresentPincode"
                                                                    ErrorMessage="Enter pin code." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Phone Number (R)</label>
                                                                <asp:TextBox ID="txtPresentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter phone number (R)"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvPresentRPhoneNumber" CssClass="validationmsg" runat="server" ControlToValidate="txtPresentRPhoneNumber"
                                                ErrorMessage="Enter phone number (R)." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Phone Number (M)</label>
                                                                <asp:TextBox ID="txtPresentMPhoneNumber" Enabled="false" ReadOnly="true" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter phone number (M)"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPresentMPhoneNumber" CssClass="validationmsg" Display="Dynamic" runat="server" ControlToValidate="txtPresentMPhoneNumber"
                                                                    ErrorMessage="Enter present phone number (M)." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2 mb-2" style="text-align: center; text-align: center; display: flex; align-items: center; justify-content: center;">
                                            <asp:Button ID="PresentAddress" runat="server" CssClass="btn btn-info" Text=">>" ClientIDMode="Static" CausesValidation="false" ToolTip="Same as Present Address." OnClick="PresentAddress_Click" />
                                       
                                        </div>
                                        <div class="col-md-5">
                                            <label>Permanent Address <span class="text-danger">*</span></label>
                                            <div class="card">
                                                <div class="card-body p-0">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtPermenentAddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter permenent address"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentAddress" CssClass="validationmsg" runat="server" ControlToValidate="txtPermenentAddress"
                                                                    ErrorMessage="Enter permanent address." Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Country<span class="text-danger">*</span></label>
                                                                <asp:DropDownList ID="ddlPermenentCountry" CssClass="form-control select2bs4" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPermenentCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentCountry" CssClass="validationmsg" runat="server" ControlToValidate="ddlPermenentCountry"
                                                                    ErrorMessage="Select permanent country." SetFocusOnError="true" Display="Dynamic" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>State<span class="text-danger">*</span></label>
                                                                <asp:DropDownList ID="ddlPermenentState" CssClass="form-control select2bs4" AutoPostBack="false" runat="server" >
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentState" CssClass="validationmsg" runat="server" ControlToValidate="ddlPermenentState"
                                                                    ErrorMessage="Select permanent state." SetFocusOnError="true" Display="Dynamic" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>District<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="ddlPermenentCity" CssClass="form-control select2bs4" runat="server"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="ddlPermenentCity" CssClass="form-control select2bs4" runat="server" OnSelectedIndexChanged="ddlPermenentCity_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentCity" CssClass="validationmsg" runat="server" ControlToValidate="ddlPermenentCity"
                                                                    ErrorMessage="Select permanent city." SetFocusOnError="true" Display="Dynamic" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Taluka<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="ddlPermenentTaluka" CssClass="form-control select2bs4" runat="server"></asp:TextBox>
                                                               <%-- <asp:DropDownList ID="ddlPermenentTaluka" CssClass="form-control select2bs4" runat="server">
                                                                </asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="ddlPresentCity"
                                                                    ErrorMessage="Select present Taluka." Display="Dynamic" SetFocusOnError="true" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Pin Code<span class="text-danger">*</span></label>
                                                                <asp:TextBox ID="txtPermenentPincode" MaxLength="6" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter pin code"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentPincode" CssClass="validationmsg" runat="server" ControlToValidate="txtPermenentPincode"
                                                                    ErrorMessage="Enter permanent pin code." SetFocusOnError="true" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Phone Number (R)</label>
                                                                <asp:TextBox ID="txtPermenentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter phone number (R)"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvPermenentRPhoneNumber" CssClass="validationmsg" runat="server" ControlToValidate="txtPermenentRPhoneNumber"
                                                ErrorMessage="Enter permenent phone number (R)." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="form-group">
                                                                <label>Phone Number (M)</label>
                                                                <asp:TextBox ID="txtPermenentMPhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" placeholder="Enter phone number (M)"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPermenentMPhoneNumber" CssClass="validationmsg" runat="server" ControlToValidate="txtPermenentMPhoneNumber"
                                                                    ErrorMessage="Enter permanent phone number (M)." SetFocusOnError="true" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlPresentCountry" EventName="SelectedIndexChanged" />
                                   <asp:AsyncPostBackTrigger ControlID="ddlPresentState" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlPermenentCountry" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlPermenentState" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <div class="row col-md-12" id="DivAststusaddMain" runat="server" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Application Status</label>
                                        <asp:Label ID="LblstatusAdd" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-10" id="DivRemerksadd" runat="server">
                                    <div class="form-group">
                                        <label>Remerks</label>
                                        <asp:TextBox ID="txtremerksAdd" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                        <%--<asp:Label ID="LblremerksAdd" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <!-- /Basic Information -->
                <!-- File Upload Section -->
                <div class="card  card-info">
                    <div class="card-header">
                        <h3 class="card-title mb-0"><i class="fas fa-upload mr-2"></i>Document Upload
                        </h3>
                    </div>
                    <asp:Panel ID="PanelDocumentUpload" runat="server" Enabled="false">
                        <div class="card-body">
                            <p style="color: red;">Note : Quality of Document Upload should be Good Enough to Be Identifiable and Acceptable.</p>
                            <div class="education-info">
                                <div class="row form-row education-cont">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-md-4 col-12">
                                                <div class="form-group">
                                                    <label><b>Upload Photograph</b></label><br />
                                                    <asp:FileUpload ID="fuPhotograph" runat="server" onchange="ShowPhotographPreview(this);" />
                                                    <asp:Label ID="lblPhotograph" runat="server" Style="display: none;" ForeColor="Red"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvPhotograph" CssClass="validationmsg" runat="server" ControlToValidate="fuPhotograph" Display="Dynamic" EnableClientScript="true"
                                                        ErrorMessage="Upload photograph." SetFocusOnError="true"><p class="help-block" style="color:red;">Width: 280 px, Height: 300 px (Only .Jpeg or .Jpg or .png)</p></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                                                        ControlToValidate="fuPhotograph"
                                                        ErrorMessage="Only .Jpeg or .Jpg or .png" Font-Bold="True" ForeColor="Red"
                                                        ValidationExpression="(.*?)\.(jpg|jpeg|png|JPG|JPEG|PNG)$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2 col-12">
                                                <asp:Button ID="btnUploadPhotograph" runat="server" CssClass="btn btn-success btn_general" OnClick="btnUploadPhotograph_Click" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Image ID="PhotographPreview" runat="server" CssClass="img-thumbnail" AlternateText="Photograph" Width="150" Height="175" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4 col-12">
                                                <div class="form-group">
                                                    <label><b>Upload Signature</b></label><br />
                                                    <asp:FileUpload ID="fuSignature" runat="server" onchange="return ShowSignaturePreview(this);" />
                                                    <asp:Label ID="lblSignature" runat="server" Style="display: none;" ForeColor="Red"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvSignature" CssClass="validationmsg" runat="server" ControlToValidate="fuSignature" Display="Dynamic" EnableClientScript="true"
                                                        ErrorMessage="Upload signature." SetFocusOnError="true"><p class="help-block" style="color:red;">Width: 280 px, Height: 300 px ( Only .Jpeg or .Jpg or .png)</p></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="fuSignature"
                                                        ErrorMessage="Only .Jpeg or .Jpg or .png" Font-Bold="True" ForeColor="Red"
                                                        ValidationExpression="(.*?)\.(jpg|jpeg|png|JPG|JPEG|PNG)$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2 col-12">
                                                <asp:Button ID="btnUploadSignature" runat="server" CssClass="btn btn-success btn_general" OnClick="btnUploadSignature_Click" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Image ID="SignaturePriview" runat="server" CssClass="img-thumbnail" AlternateText="Signature" Width="150" Height="175" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4 col-12">
                                                <div class="form-group">
                                                    <label><b>Upload Identity Proof</b></label>
                                                    <asp:FileUpload ID="fuDOB" runat="server" onchange="ShowDOBPreview(this);" /><br />
                                                    <p style="color: red;">please upload only <b>Aadhar card</b> in .Jpeg/.Jpg/png format</p>
                                                    <asp:Label ID="lblDOB" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvDOB" CssClass="validationmsg" runat="server" ControlToValidate="fuDOB" Display="Dynamic" EnableClientScript="true"
                                                        ErrorMessage="Upload date of birth proof." SetFocusOnError="true"><p class="help-block" style="color:red;">( Only .Jpeg or .Jpg or .png or .pdf)</p></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                        ControlToValidate="fuDOB"
                                                        ErrorMessage="Only .Jpeg or .Jpg or .png" Font-Bold="True" ForeColor="Red"
                                                        ValidationExpression="(.*?)\.(jpg|jpeg|png|pdf|JPG|JPEG|PNG|PDF)$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2 col-12">
                                                <asp:Button ID="btnDOBUpload" runat="server" CssClass="btn btn-success btn_general" OnClick="btnDOBUpload_Click" OnClientClick="return validate();" CausesValidation="false" Text="Upload" Style="display: none; margin-top: 22px;" />
                                            </div>
                                            <div class="col-md-4 col-12">
                                                <div class="form-group">
                                                    <asp:Image ID="ImageIdentityProof" runat="server" CssClass="img-thumbnail" AlternateText="Identity Proof" Width="150" Height="175" Visible="false" />

                                                    <%--<asp:Button ID="btnViewDOBFile" runat="server" CssClass="btn btn-success btn_general" CausesValidation="false" Text="View Upload File" OnClick="btnViewDOBFile_Click" Visible="false" Style="margin-top: 22px;" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row col-md-12" id="DivDocumentUploadMain" runat="server" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Application Status</label>
                                        <asp:Label ID="LblstatusDU" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-10" id="DivRemerksDU" runat="server">
                                    <div class="form-group">
                                        <label>Remerks</label>
                                        <asp:TextBox ID="txtRemerksDU" ReadOnly="true" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                        <%--<asp:Label ID="LblRemerksDU" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <!-- /File Upload Section -->

                <!-- Education -->
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title mb-0"><i class="fas fa-house-user mr-2"></i>Family Member Information
                        </h3>
                    </div>
                    <asp:Panel ID="PanelFamilyMemberInformation" runat="server" Enabled="false">
                        <div class="card-body">
                            <div class="education-info">
                                <div class="row form-row education-cont">
                                    <div class="col-12 col-md-12 col-lg-12">
                                        <div class="row form-row">
                                            <asp:HiddenField ID="hfRelativeId" runat="server" Value="0" />
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Name</label>
                                                    <asp:TextBox ID="txtFamilyPersonName" runat="server" onpaste="return OnlyAllowedlettersWithSpaceOnlyPaste(this);" onkeypress="return OnlyAllowedlettersWithSpaceOnly(event)" placeholder="Enter name" CssClass="form-control "></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtFamilyPersonName" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtFamilyPersonName"
                                                        ErrorMessage="Enter name." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Age</label>
                                                    <asp:TextBox ID="txtFamilyAge" MaxLength="3" CssClass="form-control" onkeypress="return isNumberKey(event)" placeholder="Enter age" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtFamilyAge" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtFamilyAge"
                                                        ErrorMessage="Enter age." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Relation</label>
                                                    <asp:TextBox ID="txtRelation" CssClass="form-control" placeholder="Enter relation" runat="server"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlFamilyRelation" CssClass="form-control select" placeholder="Enter relation" runat="server">
                                                    </asp:DropDownList>--%>
                                                    <asp:RequiredFieldValidator ID="rfvtxtRelation" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtRelation"
                                                        ErrorMessage="Enter relation." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Occupation</label>
                                                    <asp:TextBox ID="txtFamilyOccupation" runat="server" placeholder="Enter occupation" onpaste="return OnlyAllowedlettersWithSpaceOnlyPaste(this);" onkeypress="return OnlyAllowedlettersWithSpaceOnly(event)" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtFamilyOccupation" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtFamilyOccupation"
                                                        ErrorMessage="Enter occupation." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Monthly Income</label>
                                                    <asp:TextBox ID="txtFamilyMonthlyIncome" placeholder="Enter monthly income" runat="server" CssClass="form-control" onkeypress="return isNumberKeyWithDot(event)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtFamilyMonthlyIncome" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtFamilyMonthlyIncome"
                                                        ErrorMessage="Enter age." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <label>Contact Number</label>
                                                    <asp:TextBox ID="txtfamilycontactno" runat="server" onkeypress="return isNumberKey(event)" MaxLength="12" placeholder="Enter Contact Number" CssClass="form-control "></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtfamilycontactno" ValidationGroup="btnMember" CssClass="validationmsg" runat="server" ControlToValidate="txtfamilycontactno"
                                                        ErrorMessage="Enter Contact Number." ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-3">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnMember" runat="server" CssClass="btn btn-success submit-btn" Style="margin-top: 10px;" Text="Add Member" OnClick="btnMember_Click" CausesValidation="true" ValidationGroup="btnMember" />
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger submit-btn" Style="margin-top: 10px;" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-12">
                                        <asp:GridView ID="gvFamilyMember" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,RelationId" CssClass="table-responsive table table-bordered table-hover table-striped"
                                            EmptyDataText="Record does not exist...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr&nbsp;no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                                <asp:BoundField DataField="MemberName" HeaderText="Name" SortExpression="MemberName" />
                                                <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                                <asp:BoundField DataField="RelationName" HeaderText="Relation Name" SortExpression="RelationName" />
                                                <asp:BoundField DataField="Occupation" HeaderText="Occupation" SortExpression="Occupation" />
                                                <asp:BoundField DataField="MonthlyIncome" HeaderText="Monthly Income" SortExpression="MonthlyIncome" />
                                                <asp:BoundField DataField="FamilyContactNumber" HeaderText="Contact Number" SortExpression="FamilyContactNumber" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="lnkFamilyMemberEdit" runat="server" OnClick="lnkFamilyMemberEdit_Click" CausesValidation="false" CssClass="btn edit_btn btn-success btn-sm show-tooltip mr-2">Edit</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkFamilyMemberDelete" runat="server" OnClick="lnkFamilyMemberDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" CssClass="btn btn-sm btn-danger delete_btn show-tooltip">Delete</asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row col-md-12" id="DivFamilyMemberInformationMain" runat="server" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Application Status</label>
                                        <asp:Label ID="LblstatusFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-10" id="DivRemerksFMI" runat="server">
                                    <div class="form-group">
                                        <label>Remerks</label>
                                        <asp:TextBox ID="txtRemerksFMI" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                        <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <!-- /Education -->
                <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important; margin-bottom: 5px;">
                    <div class="row form-row">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSaveAndNext" runat="server" Text="Save And Next" CssClass="mb-4 btn submit-btn btn-info" OnClick="btnSaveAndNext_Click" />
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
        
            </div>
        </div>
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <script>
        $(document).ready(function () {

        });
        function ShowPhotographPreview(input) {
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

<%--            if (input.files[0].size > 102400) {
                document.getElementById("<%=lblPhotograph.ClientID%>").innerHTML = "File size must not exceed 100 KB";
                document.getElementById("<%=lblPhotograph.ClientID%>").style.display = "block";
                document.getElementById("<%=btnUploadPhotograph.ClientID%>").style.display = "none";
                document.getElementById("<%=fuPhotograph.ClientID %>").value = "";
                return false;
            }
            else--%>
            {
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

            <%--if (input.files[0].size > 102400) {
                document.getElementById("<%=lblSignature.ClientID%>").innerHTML = "File size must not exceed 100 KB";
                document.getElementById("<%=lblSignature.ClientID%>").style.display = "block";
                document.getElementById("<%=btnUploadSignature.ClientID%>").style.display = "none";
                document.getElementById("<%=fuSignature.ClientID %>").value = "";
                return false;
            }
            else--%>
            {
                document.getElementById("<%=lblSignature.ClientID%>").innerHTML = "";
                document.getElementById("<%=lblSignature.ClientID%>").style.display = "none";
                document.getElementById("<%=btnUploadSignature.ClientID%>").style.display = "block";
                return true;
            }
        }

        function ShowDOBPreview(input) {
            var array = ['jpg', 'jpeg', 'png', 'pdf', 'JPG', 'JPEG', 'PNG', 'PDF'];
            var xyz = input.files[0].name;
            var Extension = input.files[0].name.split('.')[1].toLowerCase();

            if (array.indexOf(Extension) <= -1) {
                document.getElementById("<%=btnDOBUpload.ClientID%>").style.display = "none";
                document.getElementById("<%=fuDOB.ClientID %>").value = "";
                document.getElementById("<%=fuDOB.ClientID %>").focus();
                alert("Upload only jpg, jpeg, png or pdf extension file...!!!");
                return false;
            }

            if (input.files && input.files[0]) {
                document.getElementById("<%=btnDOBUpload.ClientID%>").style.display = "block";
            }
        }
    </script>
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

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('.select2bs4').select2({
                        theme: 'bootstrap4'
                    })
                }
            });
        };
    </script>
    <link rel="shortcut icon" href="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/favicon.png")%>" />

    <!-- Plugin styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.css")%>" type="text/css" />

    <!-- DataTable -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/dataTables.min.css")%>" type="text/css" />

    <!-- Prism -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/prism/prism.css")%>" type="text/css" />

    <!-- Datepicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.css?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>" />

    <!-- Clockpicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/clockpicker/bootstrap-clockpicker.min.css?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>" type="text/css" />

    <!-- Css -->
    <link href="<%= ResolveUrl("~/Admin/Script/jquery-ui.css")%>" rel="stylesheet" type="text/css" />


    <!-- Vmap -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/jqvmap.min.css")%>" />

    <!-- App styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/app.min.css")%>" type="text/css" />
    <script lang="JavaScript" type="text/javascript">

        $(document).ready(function () {
            ClosePreloder();
            var ageLimitCalOn = document.getElementById("Body_lblDateOfBirth");
            $(ageLimitCalOn).datepicker({
                maxDate: new Date(),
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy'
            });

        });
    </script>
</asp:Content>
