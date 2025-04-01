<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Faculty.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Faculty.Faculty" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CMS | Faculty</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Faculty</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Faculty</li>
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
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="form-group">
                            <label for="txtFacultyName">Faculty Name</label>
                            <asp:TextBox ID="txtFacultyName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter faculty name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFacultyName" ForeColor="Red" runat="server" ControlToValidate="txtFacultyName"
                                ErrorMessage="Enter faculty name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlDesignation">Designation</label>
                            <asp:DropDownList ID="ddlDesignation" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesignation" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDesignation"
                                ErrorMessage="Select designation." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtdesignation">Designation</label>
                            <asp:TextBox ID="txtdesignation" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter designation name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvdesignationname" ForeColor="Red" runat="server" ControlToValidate="txtdesignation"
                                ErrorMessage="Enter designation." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddlDepartment">Department</label>
                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDepartment"
                                ErrorMessage="Select department." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="fuImage">Profile Image</label>
                            <asp:HiddenField ID="hfFilName" runat="server" />
                            <asp:FileUpload CssClass="form-control" ID="fuImage" runat="server" />
                            <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div class="form-group">
                            <label for="txtFacultyName">Faculty Mo No.</label>
                            <asp:TextBox ID="txtMoNo" TextMode="Number" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter faculty Mo no." runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfdno" ForeColor="Red" runat="server" ControlToValidate="txtMoNo"
                                ErrorMessage="Enter faculty mobile number." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rfvmono" runat="server"
                                ControlToValidate="txtMoNo" ErrorMessage="Please enter valid number"
                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <div class="form-group">
                            <label for="txtFacultyName">Faculty Email</label>
                            <asp:TextBox ID="txtemail" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter faculty Email." runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfdemail" ForeColor="Red" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="Enter faculty email." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            <br />
                            <asp:RegularExpressionValidator ID="rfdemailex" ForeColor="Red" SetFocusOnError="true" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                        <div class="form-group">
                            <label for="txtFacultyName">Sequence No.</label>
                            <asp:TextBox ID="txtsequenceno" aria-describedby="sequence" CssClass="form-control" placeholder="Enter Sequence No." runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfdemail" ForeColor="Red" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="Enter faculty email." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" SetFocusOnError="true" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label>&nbsp;</label>
                        <div class="form-group form-check form-control">
                            <asp:CheckBox ID="chkEnable" runat="server" />
                            <label class="form-check-label" for="chkEnable">Is Active</label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="exampleInputFile">Faculty Description<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFacultydesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                var editor = CKEDITOR.replace('<%=txtFacultydesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                                });
                            </script>
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
                        <div class="form-group" style="overflow-x:scroll !important;">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,LanguageId,DesignationId,DepartmentId,ImageName" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" SortExpression="RoleName" />
                                    <asp:BoundField DataField="MobileNumber" HeaderText="Faculty Number" SortExpression="MobileNumber" />
                                    <asp:BoundField DataField="Email" HeaderText="Faculty Email" SortExpression="Email" />
                                    <asp:BoundField DataField="FacultyDescription" HeaderText="Faculty Description" SortExpression="Description" />
                                    <asp:BoundField DataField="DesignationName" HeaderText="Designation name" SortExpression="DesignationName" />
                                    <%--<asp:BoundField DataField="DesignationName" HeaderText="Designation" SortExpression="isactive" />--%>
                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="isactive" />
                                    <asp:BoundField DataField="sequenceNo" HeaderText="Sequence No" SortExpression="sequenceNo" />
                                    <asp:TemplateField HeaderText="Tab List">
                                    <ItemTemplate>
                                        <a id="aExtraDetails" href='<%# ResolveUrl("~/Admin/Faculty/ExtraDetails.aspx?"+ Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("Id").ToString()+"|"+Eval("LanguageId").ToString())) %>' target="_blank" runat="server" tooltip="Add Extra Details" class=""> <i class="fa fa-edit"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# Eval("ImagePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllFacultyMaster]" SelectCommandType="StoredProcedure" FilterExpression="FacultyName like '%{0}%' Or DepartmentName like '%{0}%' Or DesignationName like '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="Name" />
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
        <%-- $(document).ready(function () {
            document.getElementById('<%= ddlLanguage.ClientID %>').removeAttribute('disabled');
        });--%>
    </script>
</asp:Content>

