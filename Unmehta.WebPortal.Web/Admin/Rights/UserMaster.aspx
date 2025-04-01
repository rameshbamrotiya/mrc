<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Rights.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>User Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Admin</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">User Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="Server">


    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">User Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtUserName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Enter Username" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">First Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" CssClass="validationmsg" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="Enter FirstName" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Last Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" CssClass="validationmsg" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="Enter Lastname" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                   <%-- <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Department<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlDept" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Designation<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpDesignation" CssClass="form-control" runat="server" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Role<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpRole" CssClass="form-control" runat="server" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Email<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                Display="Dynamic" ErrorMessage="Invalid email address" />
                            <asp:RequiredFieldValidator ID="rfvEmail" CssClass="validationmsg" runat="server" SetFocusOnError="true" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="Enter Email adddress" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Mobile No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                ControlToValidate="txtMobile" CssClass="validationmsg" ErrorMessage="Eneter valid mobile number"
                                ValidationExpression="[0-9]{10}" SetFocusOnError="true"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvMobile" CssClass="validationmsg" runat="server" SetFocusOnError="true" ControlToValidate="txtMobile"
                                Display="Dynamic" ErrorMessage="Enter mobile number" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" id="pass_visible" runat="server">
                        <div class="form-group">
                            <label for="exampleInputFile">Password<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassWord" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtPassword"
                                Display="Dynamic" ErrorMessage="Enter Password" />
                        </div>
                    </div>
                    <div class="col-md-3" id="cnpass_visible" runat="server">
                        <div class="form-group">
                            <label for="exampleInputFile">Confirm Password<span class="req-field">*</span></label>

                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" CssClass="validationmsg" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtConfirmPassword"
                                Display="Dynamic" ErrorMessage="Enter confirm  Password" />
                            <asp:CompareValidator runat="server" ID="cmpPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                                Operator="Equal" Type="String" ErrorMessage="Password mismatch" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnLogin_Click" />
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
    </asp:Panel>

    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">

                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class=" controls">
                                <div class="input-group">
                                    <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                    <div class="col-md-3">
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
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowSorting="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="isactive" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="isactive" />
                                    <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="isactive" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="isactive" Visible="false" />
                                    <asp:BoundField DataField="DepartmentId" HeaderText="Dept" SortExpression="isactive" Visible="false" />
                                    <asp:BoundField DataField="RoleId" HeaderText="Role" SortExpression="isactive" />
                                    <asp:BoundField DataField="PhoneNo" HeaderText="Contact No." SortExpression="isactive" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="isactive" />

                                    <asp:TemplateField HeaderText="Action">
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
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdnMode" Value="1" runat="server" />
    <asp:HiddenField ID="hdnRoleid" Value="1" runat="server" />

    <script type="text/javascript">
        function validate() {
            var hash = CryptoJS.SHA3(document.getElementById('<%= txtPassword.ClientID %>').value);
            document.getElementById('<%= txtPassword.ClientID %>').value = hash;
            var hashCO = CryptoJS.SHA3(document.getElementById('<%= txtConfirmPassword.ClientID %>').value);
            document.getElementById('<%= txtConfirmPassword.ClientID %>').value = hashCO;
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
