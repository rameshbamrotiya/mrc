<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.Default" %>

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
            <h1>Apply Now</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Apply Now</li>
            </ul>

        </div>
    </section>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Personal Information</h4>
                    <div class="row form-row">
                        <asp:HiddenField ID="hfJobId" runat="server" />
                        <asp:HiddenField ID="hfRegId" runat="server" />
                        <asp:HiddenField ID="hfCandidateId" runat="server" />
                        <asp:HiddenField ID="hfSignatureName" runat="server" />
                        <asp:HiddenField ID="hfMinDate" runat="server" />
                        <asp:HiddenField ID="hfPhotographName" runat="server" />
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Post Applied For: <span class="text-danger">*</span></label>
                                <asp:Label ID="lblPostAppliedFor" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row" style="width: 103%;">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>First Name <span class="text-danger">*</span></label>
                                    <input type="text" id="txtFirstName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Middle Name <span class="text-danger">*</span></label>
                                    <input type="text" id="txtMiddleName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Surname <span class="text-danger">*</span></label>
                                    <input type="text" id="txtLastName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Photograph <span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="fuPhotoGraph" runat="server" CssClass="form-control" />
                                    <asp:Image ID="imgPhotoGraph" runat="server" />
                                    <asp:RegularExpressionValidator ID="rfvPhoto" runat="server" ErrorMessage="Only png,jpg and jpeg file is allowed!" ValidationExpression="^.+(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG)$" ControlToValidate="fuPhotoGraph" Display="Dynamic" > </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block" class="row">
                            <ContentTemplate>
                                <div class="col-md-6 ">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Present Address <span class="text-danger">*</span></label><span class="">
                                                    <asp:LinkButton ID="PresentAddress" runat="server" Style="float: right; color: blue;" OnClick="PresentAddress_Click">--></asp:LinkButton>
                                                    <textarea class="form-control txtletterUpper" id="txtPresentAddress" runat="server" rows="3"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Pin Code<span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtPresentPincode" MaxLength="6" CssClass="form-control " onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Country<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPresentCountry" CssClass="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentCountry_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>State<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPresentState" CssClass="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentState_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPresentCity" CssClass="form-control select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Phone Number (R)</label>
                                                <asp:TextBox ID="txtPresentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Country Code</label>
                                                <asp:DropDownList ID="ddlCountryCode" CssClass="form-control select" runat="server"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Phone Number (M)</label>
                                                <asp:TextBox ID="txtPresentMPhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPresentMPhoneNumber" CssClass="validationmsg" runat="server" ControlToValidate="txtPresentMPhoneNumber"
                                                    ErrorMessage="Enter Phone Number (M)." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 ">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Permenent Address <span class="text-danger">*</span></label>
                                                <textarea class="form-control txtletterUpper" id="txtPermenentAddress" runat="server" rows="3"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Pin Code<span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtPermenentPincode" MaxLength="6" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Country<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPermenentCountry" CssClass="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlPermenentCountry_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>State<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPermenentState" CssClass="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlPermenentState_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlPermenentCity" CssClass="form-control select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Phone Number (R)</label>
                                                <asp:TextBox ID="txtPermenentRPhoneNumber" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Country Code</label>
                                                <asp:DropDownList ID="ddlcountrycode1" CssClass="form-control select" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Phone Number (M)</label>
                                                <asp:TextBox ID="txtPermenentMPhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMPhoneNumber" CssClass="validationmsg" runat="server" ControlToValidate="txtPermenentMPhoneNumber"
                                                    ErrorMessage="Enter Phone Number (M)." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Email</label>
                                <input type="email" id="txtEmail" runat="server" readonly="true" class="form-control txtletterUpper" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Aadhar Card Number</label>
                                <asp:TextBox ID="txtAadharCard" MaxLength="12" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date of Birth</label>
                                <div class="cal-icon">
                                    <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Block" class="row" style="width: 103%;">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Age</label>
                                        <asp:HiddenField ID="hfAge" runat="server" />
                                        <span id="txtAge" class="form-control" runat="server"></span>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Cast</label>
                                        <asp:DropDownList ID="ddlCast" CssClass="form-control select" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Marital Status</label>
                                        <asp:DropDownList ID="ddlMaritalStatus" CssClass="form-control select" OnSelectedIndexChanged="ddlMaritalStatus_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="SpouseFname" runat="server">
                                    <div class="form-group">
                                        <label>Spouse First Name <span class="text-danger">*</span></label>
                                        <input type="text" id="txtSpouseFirstName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                    </div>
                                </div>
                                <div class="col-md-4" id="SpouseMname" runat="server">
                                    <div class="form-group">
                                        <label>Spouse Middle Name <span class="text-danger">*</span></label>
                                        <input type="text" id="txtSpouseMiddleName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                    </div>
                                </div>
                                <div class="col-md-4" id="SpouseLname" runat="server">
                                    <div class="form-group">
                                        <label>Spouse Last Name <span class="text-danger">*</span></label>
                                        <input type="text" id="txtSpouseLastName" runat="server" class="form-control txtletterUpper" onkeypress="return lettersOnly(event)" />
                                    </div>
                                </div>
                                <div class="col-md-6" id="SpouseDOB" runat="server">
                                    <div class="form-group">
                                        <label>Spouse Date of Birth</label>
                                        <div class="cal-icon">
                                            <input type="text" id="txtSpouseDOB" runat="server" class="form-control datetimepicker" placeholder="Select Date" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="SpousePNo" runat="server">
                                    <div class="form-group">
                                        <label>Spouse Phone Number (M)</label>
                                        <asp:TextBox ID="txtSpousePhoneNumber" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Gender</label>
                                <asp:DropDownList ID="ddlGender" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Religion</label>
                                <asp:DropDownList ID="ddlReligion" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Signature <span class="text-danger">*</span></label>
                                <asp:FileUpload ID="fuSignature" runat="server" CssClass="form-control" />
                                    <asp:RegularExpressionValidator ID="rfvSignature" runat="server" ErrorMessage="Only png,jpg and jpeg file is allowed!" ValidationExpression="^.+(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG)$" ControlToValidate="fuSignature" Display="Dynamic" > </asp:RegularExpressionValidator>
                                <asp:Image ID="imgSignature" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Basic Information -->
            <!-- Education -->
            <div class="card">
                <asp:UpdatePanel ID="upFamily" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <h4 class="card-title">Family Member Information</h4>
                            <div class="education-info">
                                <div class="row form-row education-cont" style="width: 103%;">
                                    <div class="col-12 col-md-10 col-lg-11">
                                        <div class="row form-row">
                                            <asp:HiddenField ID="hfRelativeId" runat="server" />
                                            <asp:HiddenField ID="hfTempId" runat="server" />
                                            <asp:HiddenField ID="hfRRowId" runat="server" />
                                            <div class="col-12 col-md-4 col-lg-4">
                                                <div class="form-group">
                                                    <label>Name</label>
                                                    <input type="text" id="txtFamilyPersonName" runat="server" onkeypress="return lettersWithSpaceOnly(event)" class="form-control txtletterUpper" />
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
                                                    <input type="text" id="txtFamilyOccupation" runat="server" onkeypress="return lettersOnly(event)" class="form-control txtletterUpper" />
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-4">
                                                <div class="form-group">
                                                    <label>Monthly Income</label>
                                                    <input type="text" id="txtFamilyMonthlyIncome" runat="server" class="form-control decimal" />
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-4 col-lg-4">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnMember" runat="server" CssClass="btn btn-primary submit-btn" Style="margin-top: 10px;" Text="Add Member" OnClick="btnMember_Click" />
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary submit-btn" Style="margin-top: 10px;" Text="Clear" OnClick="btnClear_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-10 col-lg-11">
                                        <asp:GridView ID="gvFamilyMember" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
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

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_FamilyEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_FamilyEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_FamilyDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_FamilyDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /Education -->

            <div class="submit-section submit-btn-bottom">
                <asp:Button ID="btnSaveAndNext" runat="server" Text="Save And Next" CssClass="btn btn-primary submit-btn" OnClick="btnSaveAndNext_Click" />
            </div>

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <script>
        function _calculateAge(birthday) { // birthday is a date
            var from = birthday.split("/");
            var birthdateTimeStamp = new Date(from[2], from[1] - 1, from[0]);
            var cur = new Date();
            var diff = cur - birthdateTimeStamp;
            // This is the difference in milliseconds
            var currentAge = Math.floor(diff / 31557600000);
            // Divide by 1000*60*60*24*365.25
            return currentAge;
        }


        $(document).ready(function () {
            var dt = new Date(document.getElementById("Body_hfMinDate").value);

            if ($('#<%=txtDateOfBirth.ClientID%>').length > 0) {
                $('#<%=txtDateOfBirth.ClientID%>').datetimepicker({
                    minDate: dt,
                    format: 'DD/MM/YYYY',
                    icons: {
                        up: "fas fa-chevron-up",
                        down: "fas fa-chevron-down",
                        next: 'fas fa-chevron-right',
                        previous: 'fas fa-chevron-left'
                    }
                }).on('dp.change', function (e) {
                    debugger;
                    var formatedValue = document.getElementById("Body_txtDateOfBirth").value;

                    var age = _calculateAge(formatedValue);
                    if (age != "NaN") {
                        if (age > 0) {
                            document.getElementById("<%=txtAge.ClientID%>").innerHTML = age;
                            document.getElementById("<%=hfAge.ClientID%>").value = age;
                        }
                        else {
                            document.getElementById("<%=txtAge.ClientID%>").innerHTML = 0;
                            document.getElementById("<%=hfAge.ClientID%>").value = 0;
                        }
                    }
                    else {
                        document.getElementById("<%=txtAge.ClientID%>").innerHTML = 0;
                        document.getElementById("<%=hfAge.ClientID%>").value = 0;
                    }
                });;

                $('#<%=txtDateOfBirth.ClientID%>').keyup(function () {
                    debugger;
                    var formatedValue = document.getElementById("Body_txtDateOfBirth").value;
                    if (age != "NaN") {
                        if (age > 0) {
                            document.getElementById("<%=txtAge.ClientID%>").innerHTML = age;
                            document.getElementById("<%=hfAge.ClientID%>").value = age;
                        }
                        else {
                            document.getElementById("<%=txtAge.ClientID%>").innerHTML = 0;
                            document.getElementById("<%=hfAge.ClientID%>").value = 0;
                        }
                    }
                    else {
                        document.getElementById("<%=txtAge.ClientID%>").innerHTML = 0;
                        document.getElementById("<%=hfAge.ClientID%>").value = 0;
                    }
                });
            }
        });

    </script>
</asp:Content>
