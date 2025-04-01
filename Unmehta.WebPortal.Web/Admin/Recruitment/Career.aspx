<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Career.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.Career" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CMS | Career</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Career</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Career</li>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddllanguage">Language</label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                                ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="form-group">
                            <label for="ddlDepartment">Department</label>
                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDepartment"
                                ErrorMessage="Select department." SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                            <label for="txtTotalVacancy">Total Vacancy</label>
                            <asp:TextBox ID="txtTotalVacancy" runat="server" TextMode="Number" CssClass="form-control" placeholder="Enter total vacancy"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTotalVacancy" ForeColor="Red" runat="server" ControlToValidate="txtTotalVacancy"
                                ErrorMessage="Enter total vacancy." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtShortDescription">Short Description</label>
                            <asp:TextBox ID="txtShortDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter short description"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvShortDescription" ForeColor="Red" runat="server" ControlToValidate="txtShortDescription"
                                ErrorMessage="Enter short description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtDescription">Description</label>
                            <asp:TextBox ID="txtDescription" aria-describedby="emailHelp" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Enter description" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" ForeColor="Red" runat="server" ControlToValidate="txtDescription"
                                ErrorMessage="Enter description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label for="ddlRequirementType">Requirement Type</label>
                        <asp:DropDownList ID="ddlRequirementType" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRequirementType" ForeColor="Red" InitialValue="Select Requirement Type" runat="server" ControlToValidate="ddlRequirementType"
                            ErrorMessage="Select requirement type." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="fuFileUpload">&nbsp;&nbsp;</label>
                            <asp:HiddenField ID="hfFilName" runat="server" />
                            <asp:FileUpload CssClass="form-control" ID="fuFileUpload" runat="server" />
                            <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group form-check">
                            <br />
                            <br />
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
                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                            <%} if (SessionWrapper.UserPageDetails.CanUpdate)
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
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,LanguageId,DesignationId,DepartmentId,FileName" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DesignationName" HeaderText="Designation" SortExpression="isactive" />
                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="isactive" />                                    
                                    <asp:BoundField DataField="ShortDescription" HeaderText="Short Description" SortExpression="RoleName" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="RoleName" />
                                    <asp:BoundField DataField="TotalVacancy" HeaderText="Total Vacancy" SortExpression="RoleName" />
                                    <asp:BoundField DataField="RequirementType" HeaderText="Requirement Type" SortExpression="RoleName" />
                                    <asp:BoundField DataField="IsVisible" HeaderText="Status" SortExpression="RoleName" />
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# Eval("FilePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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

                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllCareerMaster]" SelectCommandType="StoredProcedure" FilterExpression="RequirementType like '%{0}%' ">
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
        $(document).ready(function () {
            //document.getElementById('<%= ddlLanguage.ClientID %>').removeAttribute('disabled');
        });
    </script>
</asp:Content>
