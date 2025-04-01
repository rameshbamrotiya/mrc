<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EventMasterForm.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.EventMasterForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Event Form</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <style>
        label {
            margin-left: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Event Form </h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Event Form</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">

                <div class="row">
                    <div class="col-lg-8" data-select2-id="21">
                        <div class="appointment-form-ma">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary " Text="Save form Field" OnClick="btnSave_Click" CausesValidation="false" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnBack" CssClass="btn btn-primary " Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        <div class="col-lg-12" id="divAboutMe" runat="server">
                            <h4 class="card-title">About Me</h4>  <asp:CheckBox ID="chkAboutMe" runat="server" Text="  Is Visible" />
                            <br />
                        </div>
                            <div class="row form-row">
                                <div class="col-md-6" id="divFirstName" runat="server">
                                    <div class="form-group">
                                        <label>
                                            First Name <span class="text-danger">*</span>
                                            <%--<asp:CheckBox ID="chkFirstName" runat="server" Text="  Is Visible" />--%>
                                            </label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divLastName" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Last Name <span class="text-danger">*</span>
                                            <asp:CheckBox ID="chkLastName" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divSurName" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Surname <span class="text-danger">* </span>
                                            <asp:CheckBox ID="chkSurname" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtSurName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divEmail" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Email  
                                            <%--<asp:CheckBox ID="chkEmail" runat="server" Text="  Is Visible" />--%>

                                        </label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divPhoneNumber" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Phone Number  
                                            <asp:CheckBox ID="chkPhoneNumber" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divMobileNumber" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Mobile Number  
                                            <%--<asp:CheckBox ID="chkMobileNumber" runat="server" Text="  Is Visible" />--%>

                                        </label>
                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" data-select2-id="18" id="divGender" runat="server">
                                    <div class="form-group" data-select2-id="17">
                                        <label>
                                            Gender  
                                            <asp:CheckBox ID="chkGender" runat="server" Text="  Is Visible" /></label>
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control select">
                                            <asp:ListItem Value="" Text="Select" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                            <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divBirthdate" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Birth date  
                                            <asp:CheckBox ID="chkBirthdate" runat="server" Text="  Is Visible" /></label>
                                        <div class="cal-icon">
                                            <asp:TextBox ID="txtBirthDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Select Date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divPhysicalDisability" runat="server">
                                    <div class="form-group mb-0">
                                        <label>
                                            PHYSICAL DISABILITY (YES, NO)  
                                            <asp:CheckBox ID="chkPhysicalDisability" runat="server" Text="  Is Visible" /></label>
                                        <div id="pricing_select">
                                            <asp:RadioButtonList ID="rblPhysicalDisability" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divExplainTypeofDisability" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Explain Type of Disability  
                                            <asp:CheckBox ID="chkExplainTypeofDisability" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtExplainTypeofDisability" runat="server" CssClass="form-control" placeholder="Explain Type of Disability"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" data-select2-id="37" id="divPhysicalActivity" runat="server">
                                    <div class="form-group" data-select2-id="36">
                                        <label>
                                            Physical Activity  
                                            <asp:CheckBox ID="chkPhysicalActivity" runat="server" Text="  Is Visible" /></label>
                                        <asp:DropDownList ID="ddlPhysicalActivity" runat="server" CssClass="form-control select">
                                            <asp:ListItem Value="None" Text="None" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Regular gym user" Text="Regular gym user"></asp:ListItem>
                                            <asp:ListItem Value="Daily Walker" Text="Daily Walker"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <div class="col-lg-12"  id="divAddressInfo" runat="server">
                                <h4 class="card-title">Address Info</h4>  <asp:CheckBox ID="chkAddressInfo" runat="server" Text="  Is Visible" />
                            <br />
                            </div>
                                <div class="col-md-6" data-select2-id="32" id="divIdentity" runat="server">
                                    <div class="form-group" data-select2-id="31">
                                        <label>
                                            Select Identity  
                                            <asp:CheckBox ID="chkIdentity" runat="server" Text="  Is Visible" /></label>
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
                                        <label>
                                            Identity Number  
                                            <asp:CheckBox ID="chkIdentityNumber" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtIdentityNumber" runat="server" CssClass="form-control" placeholder="Identity Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" data-select2-id="46" id="divResidential" runat="server">
                                    <div class="form-group" data-select2-id="45">
                                        <label>
                                            RESIDENTIAL  
                                            <asp:CheckBox ID="chkResidential" runat="server" Text="  Is Visible" /></label>
                                        <asp:DropDownList ID="ddlResidential" runat="server" CssClass="form-control select">
                                            <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="India" Text="India"></asp:ListItem>
                                            <asp:ListItem Value="Non India" Text="Non India"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divCountry" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">
                                            Country  
                                            <asp:CheckBox ID="chkCountry" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divState" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">
                                            State / Province  
                                            <asp:CheckBox ID="chkState" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divCity" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">
                                            City  
                                            <asp:CheckBox ID="chkCity" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divPostalCode" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">
                                            Postal Code  
                                            <asp:CheckBox ID="chkPostalCode" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                            <div class="col-lg-12"  id="divWorkInfo" runat="server">
                                <h4 class="card-title">Work Info</h4>  <asp:CheckBox ID="chkWorkInfo" runat="server" Text="  Is Visible" />
                            <br />
                            </div>

                                <div class="col-md-6" id="divEducationQualification" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Education Qualification  
                                            <asp:CheckBox ID="chkEducationQualification" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtEducationQualification" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divOrganizationName" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Organization Name  
                                            <asp:CheckBox ID="chkOrganizationName" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divDesignation" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Designation  
                                            <asp:CheckBox ID="chkDesignation" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divEmployeeId" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Employee Id  
                                            <asp:CheckBox ID="chkEmployeeId" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" id="divJoiningDate" runat="server">
                                        <label>
                                            Joining Date  
                                            <asp:CheckBox ID="chkJoiningDate" runat="server" Text="  Is Visible" /></label>
                                        <div class="cal-icon">
                                            <asp:TextBox ID="txtJoiningDate" aria-describedby="emailHelp" CssClass="form-control" placeholder="Select Joining Date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divNoOfOrganization" runat="server">
                                    <div class="form-group">
                                        <label>
                                            No Of Organization you work with  
                                            <asp:CheckBox ID="chkNoOfOrganization" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtNoOfOrganization" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divNoOfCNE" runat="server">
                                    <div class="form-group">
                                        <label>
                                            No Of CNE you Attend  
                                            <asp:CheckBox ID="chkNoOfCNE" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtNoOfCNE" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divNoOfCME" runat="server">
                                    <div class="form-group">
                                        <label>
                                            No Of CME you Attend  
                                            <asp:CheckBox ID="chkNoOfCME" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtNoOfCME" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divWorkExperience" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Work Experience  
                                            <asp:CheckBox ID="chkWorkExperience" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtWorkExperience" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divGNCNo" runat="server">
                                    <div class="form-group">
                                        <label>
                                            GNC No  
                                            <asp:CheckBox ID="chkGNCNo" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtGNCNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divRegistrtionNo" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Registration No  
                                            <asp:CheckBox ID="chkRegistrtionNo" runat="server" Text="  Is Visible" /></label>
                                        <asp:TextBox ID="txtRegistrtionNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divWorkProfession" runat="server">
                                    <div class="form-group" data-select2-id="42">
                                        <label>
                                            Work Profession  
                                            <asp:CheckBox ID="chkWorkProfession" runat="server" Text="  Is Visible" /></label>
                                        <asp:DropDownList ID="ddlWorkProfession" runat="server" CssClass="form-control select">
                                            <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Office Work" Text="Office Work"></asp:ListItem>
                                            <asp:ListItem Value="Field Work" Text="Field Work"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
