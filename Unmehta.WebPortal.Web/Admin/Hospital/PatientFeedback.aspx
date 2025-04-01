<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PatientFeedback.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.PatientFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PatientFeedback</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Patient Feedback</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Patient Feedback</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="alert alert-danger" id="divError" runat="server" visible="false">
                    <button class="close" data-dismiss="alert">×</button>
                    <span id="spnError" runat="server">divError</span>
                </div>
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
                    <div class="col-md-3">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="form-group">
                            <label for="txtPatientName">Patient Name</label>
                            <asp:TextBox ID="txtPatientName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter patient name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPatientName" ForeColor="Red" runat="server" ControlToValidate="txtPatientName"
                                ErrorMessage="Enter patient name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="rblGender">Gender</label>
                            <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvGender" InitialValue="" ForeColor="Red" runat="server" ControlToValidate="rblGender"
                                ErrorMessage="Select gender." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtMobileNo">MobileNo</label>
                            <asp:TextBox ID="txtMobileNo" aria-describedby="emailHelp" TextMode="Phone" CssClass="form-control" placeholder="Enter mobileno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNo" ForeColor="Red" runat="server" ControlToValidate="txtMobileNo"
                                ErrorMessage="Enter mobileno." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ForeColor="Red"
                                ControlToValidate="txtMobileNo" CssClass="validationmsg" ErrorMessage="Eneter valid mobile number"
                                ValidationExpression="[0-9]{10}" SetFocusOnError="true"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtEmailId">EmailId</label>
                            <asp:TextBox ID="txtEmailId" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter emailid" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailId" ForeColor="Red" runat="server" ControlToValidate="txtEmailId"
                                ErrorMessage="Enter emailid." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailId" ForeColor="Red"
                                CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                Display="Dynamic" ErrorMessage="Invalid email address" />
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="form-group">
                            <label for="txtFeedBackDetails">FeedBack Details</label>
                            <asp:TextBox ID="txtFeedBackDetails" aria-describedby="emailHelp" TextMode="MultiLine" CssClass="form-control" placeholder="Enter feedBack details" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFeedBackDetails" ForeColor="Red" runat="server" ControlToValidate="txtFeedBackDetails"
                                ErrorMessage="Enter feedBack details." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtCaptcha">&nbsp;&nbsp;</label>
                            <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" MaxLength="6" CssClass="form-control" placeholder="Captcha"></asp:TextBox>
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                            <asp:RequiredFieldValidator ID="rfvCaptcha" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtCaptcha" Style="float: left;"
                                ErrorMessage="Please enter Captcha." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Image ID="imgCaptcha" runat="server" />
                            <button runat="server" id="btnRun" class="btn btn-mini" causesvalidation="false" onserverclick="btnRun_ServerClick" title="Search">
                                <i class="fa fa-refresh"></i>
                            </button>
                        </div>
                    </div>
                    <div class="col-md-1" style="display: none;">
                        <div class="form-group form-check">
                            <br />
                            <br />
                            <asp:CheckBox ID="chkEnable" runat="server" Checked="true" />
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
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                            <%}
                                if (SessionWrapper.UserPageDetails.CanUpdate)
                                { %>
                            <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_ServerClick">
                                <i class="fa fa-floppy-o">&nbsp;Update</i>
                            </button>
                            <%} %>
                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                <i class="fa fa-remove">&nbsp;Cancel</i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">

                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class=" controls">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                            <i class="fa fa-search">&nbsp;Search</i>
                                        </button>
                                    </span>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                                            <i class="fa fa-remove">&nbsp;Cancel</i>
                                        </button>
                                    </span>
                                </div>
                            </div>

                        </div>
                    </div>
                    <%--<div class="col-md-3">
                        <div class="pull-right">
                            <div class="input-group">
                                  <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                { %>
                                <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick" causesvalidation="false">
                                    <i class="fa fa-plus-square">&nbsp;Add new</i>
                                </button>
                                <%} %>
                            </div>
                        </div>
                    </div>--%>
                </div>                    
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;margin-bottom: 5px;" />
                    </div>
                    <div class="col-md-12">                        
                        <div class="form-group" style="overflow-x: scroll !important;">                            
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FullName" HeaderText="Patient Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="EmailId" HeaderText="Email" SortExpression="EmailId" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                                    <asp:BoundField DataField="VisitType" HeaderText="Visit Type" SortExpression="VisitType" />
                                    <asp:BoundField DataField="VisitNumber" HeaderText="Visit Number" SortExpression="VisitNumber" />
                                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                    <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                    <asp:BoundField DataField="FeedbackDescription" HeaderText="FeedBack Message" SortExpression="FeedbackDescription" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="Entry Date" SortExpression="CreateDate" />

                                    <%-- <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                  <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                      { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                  <%} if (SessionWrapper.UserPageDetails.CanDelete)
                                                      { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllPatientFeedback]" SelectCommandType="StoredProcedure" FilterExpression="FullName like '%{0}%' OR EmailId like '%{1}%' OR MobileNo like '%{2}%' ">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="FullName" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="EmailId" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="MobileNo" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">
        <%--$(document).ready(function () {
            document.getElementById('<%= ddlLanguage.ClientID %>').removeAttribute('disabled');
        });--%>
    </script>
</asp:Content>
