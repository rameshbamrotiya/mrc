<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ExecutiveLeadership.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.ExecutiveLeadership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Executive Leadership</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Executive Leadership</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Executive Leadership</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <h4>About Us</h4>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Description</label>
                        <asp:TextBox ID="txtAboutUsDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtAboutUsDescription.ClientID%>', {
                            extraPlugins: 'tableresize'
                            });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" runat="server" Style="width: 100%">
                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr style="border-color: black !important;" />
            <h4>Doctor Details</h4>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtDoctorName">Doctor Name</label>
                        <asp:TextBox ID="txtDoctorName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter doctor name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDoctorName"
                            ErrorMessage="Enter doctor name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuDoctorImage">Image Upload</label>
                        <asp:HiddenField ID="hfDoctorFileName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuDoctorImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="from-group">
                        <label for="txtDoctorMessage">Doctor Message</label>
                        <asp:TextBox ID="txtDoctorMessage" aria-describedby="emailHelp" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="Enter doctor message" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDoctorMessage" ForeColor="Red" runat="server" ControlToValidate="txtDoctorMessage"
                            ErrorMessage="Enter doctor message." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <hr style="border-color: black !important;" />
            <h4>Awards</h4>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Awards Description</label>
                        <asp:TextBox ID="txtAwards" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtAwards.ClientID%>', {
                            extraPlugins: 'tableresize'
                            });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuAwardsImage">Image Upload</label>
                        <asp:HiddenField ID="hfAwardsFileName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuAwardsImage" runat="server" />
                    </div>
                </div>
            </div>
            <hr style="border-color: black !important;" />
            <h4>Accredations</h4>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Accredations Description</label>
                        <asp:TextBox ID="txtAccredations" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtAccredations.ClientID%>', {
                            extraPlugins: 'tableresize'
                            });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuAccredationsImage">Image Upload</label>
                        <asp:HiddenField ID="hfAccredationsFileName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuAccredationsImage" runat="server" />
                    </div>
                </div>
            </div>
            <hr style="border-color: black !important;" />
            <div class="row">
                <div class="col-md-3">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                    <div class="form-group">
                        <label for="txtExecutiveName">Executive Name</label>
                        <asp:TextBox ID="txtExecutiveName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Executive name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExecutiveName" ForeColor="Red" runat="server" ControlToValidate="txtExecutiveName"
                            ErrorMessage="Enter executive name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlDesignation">Designation</label>
                        <asp:DropDownList ID="ddlDesignation" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDesignation" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDesignation"
                            ErrorMessage="Select designation." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuImage">&nbsp;&nbsp;</label>
                        <asp:HiddenField ID="hfFilName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuImage">Message</label>
                        <asp:TextBox ID="txtMessage" aria-describedby="emailHelp" TextMode="MultiLine" CssClass="form-control" placeholder="Enter message" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMessage" ForeColor="Red" runat="server" ControlToValidate="txtMessage"
                            ErrorMessage="Enter mesage." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>&nbsp;</label>
                    <div class="form-group form-check form-control">
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label class="form-check-label" for="chkEnable">Active</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <%} %>
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card" id="divGrid" runat="server" visible="false">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="pull-right">
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <div class="input-group">
                            <a href="#" title="Add New" class="btn btn-primary" id="foo" style="color: white"><i class="fa fa-plus-square">&nbsp;Add New</i></a>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-md-12" id="gvInnerGridView" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="/Admin/Script/ExecutiveLeadership.js"></script>
</asp:Content>

