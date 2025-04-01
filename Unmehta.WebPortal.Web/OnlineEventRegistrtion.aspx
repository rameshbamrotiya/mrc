<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OnlineEventRegistrtion.aspx.cs" Inherits="Unmehta.WebPortal.Web.OnlineEventRegistrtion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Online Event Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <!-- Datepicker -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Online Event Registration</li>
            </ul>
        </div>
    </div>
    <section class="content">
        <div class="container" data-select2-id="23">
            <div class="row justify-content-center" data-select2-id="22">

                <div class="col-lg-8" data-select2-id="21">
                    <div class="appointment-form-ma" data-select2-id="20">

                        <div class='blog-view'>
                            <div class='blog blog-single-post text-center'>
                                <h3 class='blog-title'>
                                    <asp:Label ID="lblEventName" runat="server" Text="Label"></asp:Label></h3>
                                <div class='blog-info clearfix'>
                                    <div class='post-left'>
                                        <ul style='justify-content: center;'>
                                            <li><i class='far fa-calendar'></i>
                                                <asp:Label ID="lblEventStartDate" runat="server" Text="Label"></asp:Label>
                                            </li>
                                            <li><i class='far fa-clock'></i>
                                                <asp:Label ID="lblEventStartTime" runat="server" Text="Label"></asp:Label>
                                                Onwards</li>
                                            <li><i class='fas fa-map-marker-alt'></i>
                                                <asp:Label ID="lblLocation" runat="server" Text="Label"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divAboutMe" runat="server">
                            <h4 class="card-title">About Me</h4>
                        </div>
                        <div class="row form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>First Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtFirstName" runat="server" onkeypress="return lettersOnly()" CssClass="form-control name-val"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please Enter FirstName"  ForeColor="Red" CssClass="validationmsg" Display="Dynamic" ControlToValidate="txtFirstName" ValidationGroup="OnlineEvent"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6" id="divLastName" runat="server">
                                <div class="form-group">
                                    <label>Last Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtLastName" runat="server" onkeypress="return lettersOnly()" CssClass="form-control name-val"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divSurName" runat="server">
                                <div class="form-group">
                                    <label>Surname <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtSurName" runat="server" onkeypress="return lettersOnly()" CssClass="form-control name-val"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validationmsg" ForeColor="Red" ErrorMessage="Please Enter Email" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="OnlineEvent"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="OnlineEvent"
                                        CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid Email." />
                                </div>
                            </div>
                            <div class="col-md-6" id="divPhoneNumber" runat="server">
                                <div class="form-group">
                                    <label>Phone Number</label>
                                    <asp:TextBox ID="txtPhoneNumber" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="OnlineEvent"
                                        ErrorMessage="Please enter valid Phone no" ControlToValidate="txtPhoneNumber" CssClass="validate" ForeColor="Red" ValidationExpression="^[\d+]{0,15}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Mobile Number</label>
                                    <asp:TextBox ID="txtMobileNumber" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validationmsg" ErrorMessage="Please Enter Mobile Number" ForeColor="Red" Display="Dynamic" ControlToValidate="txtMobileNumber" ValidationGroup="OnlineEvent"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REFmoblie" runat="server" ValidationGroup="OnlineEvent"
                                        ErrorMessage="Please enter valid mobile no" ControlToValidate="txtMobileNumber" CssClass="validate" ForeColor="Red" ValidationExpression="^[\d+]{0,15}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6" data-select2-id="18" id="divGender" runat="server">
                                <div class="form-group" data-select2-id="17">
                                    <label>Gender</label>
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control select">
                                        <asp:ListItem Value="" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                        <asp:ListItem Value="Transgender" Text="Trans-gender"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6" id="divBirthdate" runat="server">
                                <div class="form-group">
                                    <label>Birth-date</label>
                                    <div class="cal-icon">
                                        <asp:TextBox ID="txtBirthDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Select Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" id="divPhysicalDisability" runat="server">
                                <div class="form-group mb-0">
                                    <label>PHYSICAL DISABILITY (YES, NO)</label>
                                    <div id="pricing_select">
                                        <asp:RadioButtonList ID="rblPhysicalDisability" runat="server" CssClass="form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnTextChanged="rblPhysicalDisability_TextChanged">
                                            <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" id="divExplainTypeofDisability" runat="server">
                                <div class="form-group">
                                    <label>Explain Type of Disability</label>
                                    <asp:TextBox ID="txtExplainTypeofDisability" runat="server" onkeypress="return lettersWithSpaceOnly()" CssClass="form-control" placeholder="Explain Type of Disability"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" data-select2-id="37" id="divPhysicalActivity" runat="server">
                                <div class="form-group" data-select2-id="36">
                                    <label>Physical Activity</label>
                                    <asp:DropDownList ID="ddlPhysicalActivity" runat="server" CssClass="form-control select">
                                        <asp:ListItem Value="None" Text="None" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Regular gym user" Text="Regular gym user"></asp:ListItem>
                                        <asp:ListItem Value="Daily Walker" Text="Daily Walker"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-12" id="divAddressInfo" runat="server">
                                <h4 class="card-title">Address Info</h4>
                            </div>
                            <div class="col-md-6" data-select2-id="32" id="divIdentity" runat="server">
                                <div class="form-group" data-select2-id="31">
                                    <label>Select Identity</label>
                                    <asp:DropDownList ID="ddlIdentity" runat="server" CssClass="form-control select">
                                        <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Aadhaar Card" Text="Aadhaar Card"></asp:ListItem>
                                        <asp:ListItem Value="Pan Card" Text="Pan Card"></asp:ListItem>
                                        <asp:ListItem Value="Election Card" Text="Election Card"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6" id="divIdentityNumber" runat="server">
                                <div class="form-group">
                                    <label>Identity Number</label>
                                    <asp:TextBox ID="txtIdentityNumber" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" placeholder="Identity Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" data-select2-id="46" id="divResidential" runat="server">
                                <div class="form-group" data-select2-id="45">
                                    <label>RESIDENTIAL</label>
                                    <asp:DropDownList ID="ddlResidential" runat="server" CssClass="form-control select">
                                        <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="India" Text="India"></asp:ListItem>
                                        <asp:ListItem Value="Non India" Text="Non India"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6" id="divCountry" runat="server">
                                <div class="form-group">
                                    <label class="control-label">Country</label>
                                    <asp:TextBox ID="txtCountry" runat="server" onkeypress="return lettersOnly()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divState" runat="server">
                                <div class="form-group">
                                    <label class="control-label">State / Province</label>
                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divCity" runat="server">
                                <div class="form-group">
                                    <label class="control-label">City</label>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divPostalCode" runat="server">
                                <div class="form-group">
                                    <label class="control-label">Postal Code</label>
                                    <asp:TextBox ID="txtPostalCode" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-lg-12" id="divWorkInfo" runat="server">
                                <h4 class="card-title">Work Info</h4>
                            </div>
                            <div class="col-md-6" id="divEducationQualification" runat="server">
                                <div class="form-group">
                                    <label>Education Qualification</label>
                                    <asp:TextBox ID="txtEducationQualification" runat="server" onkeypress="return lettersWithSpaceOnly()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divOrganizationName" runat="server">
                                <div class="form-group">
                                    <label>Organization Name</label>
                                    <asp:TextBox ID="txtOrganizationName" runat="server" onkeypress="return lettersWithSpaceOnly()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divDesignation" runat="server">
                                <div class="form-group">
                                    <label>Designation</label>
                                    <asp:TextBox ID="txtDesignation" runat="server" onkeypress="return lettersWithSpaceOnly()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divEmployeeId" runat="server">
                                <div class="form-group">
                                    <label>Employee Id</label>
                                    <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" id="divJoiningDate" runat="server">
                                    <label>Joining Date</label>
                                    <div class="cal-icon">
                                        <asp:TextBox ID="txtJoiningDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Select Joining Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" id="divNoOfOrganization" runat="server">
                                <div class="form-group">
                                    <label>No Of Organization you work with</label>
                                    <asp:TextBox ID="txtNoOfOrganization" runat="server" onkeypress="return lettersWithSpaceOnly()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divNoOfCNE" runat="server">
                                <div class="form-group">
                                    <label>No Of CNE you Attend</label>
                                    <asp:TextBox ID="txtNoOfCNE" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divNoOfCME" runat="server">
                                <div class="form-group">
                                    <label>No Of CME you Attend</label>
                                    <asp:TextBox ID="txtNoOfCME" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divWorkExperience" runat="server">
                                <div class="form-group">
                                    <label>Work Experience</label>
                                    <asp:TextBox ID="txtWorkExperience" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" id="divGNCNo" runat="server">
                                <div class="form-group">
                                    <label>GNC No</label>
                                    <asp:TextBox ID="txtGNCNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6" id="divWorkProfession" runat="server">
                                <div class="form-group" data-select2-id="42">
                                    <label>Work Profession</label>
                                    <asp:DropDownList ID="ddlWorkProfession" runat="server" CssClass="form-control select">
                                        <asp:ListItem Value="" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Office Work" Text="Office Work"></asp:ListItem>
                                        <asp:ListItem Value="Field Work" Text="Field Work"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:Button ID="lnkSubmit" OnClick="lnkSubmit_Click" CssClass="btn btn-apfm" runat="server" Text="Book Event" ValidationGroup="OnlineEvent" />
                                    <%--<asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-apfm" OnClick="lnkSubmit_Click">Book Event</asp:LinkButton>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">

    <div class="modal fade" id="AlertOfRegestration" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close" data-original-title=""
                        title="">
                        <span aria-hidden="true">×</span></button>
                </div>
                <div class="modal-body">
                    <div class="card success-card">
                        <div class="card-body">
                            <div class="success-cont">
                                <i class="fas fa-check"  id="dvSuccess" runat="server"></i>
                                <h3 id="lblMessageBox" runat="server">
                                    <asp:Label ID="lblMessage" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="llblwarningmess" ForeColor="Red" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <script lang="JavaScript" type="text/javascript">
        $(document).ready(function () {
            var options = {
                format: 'dd/mm/yyyy',
                yearRange: "1930:2100"
            };
            $("#<%=txtBirthDate.ClientID%>").datepicker(options);
			$("#<%=txtJoiningDate.ClientID%>").datepicker(options);
		});


		function ShowPopupAlertOfRegestration() {
		    $("#AlertOfRegestration").modal("show");
		}
    </script>
</asp:Content>
