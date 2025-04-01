<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PatientTestimonial.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.PatientTestimonial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Patient Speak Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <!-- begin::page-header -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Patient Speak Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Hospital</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Patient Speak Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <h6 class="card-title">Profile</h6>
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <label for="txtPatientName">Patient Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtPatientName" ValidationGroup="Profile"
                            ErrorMessage="Enter Patient Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtExternalLink">External Link</label>
                        <asp:TextBox ID="txtExternalLink" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtCityName">City Name</label>
                        <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtDescription">Description</label>
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtShortDescription">Pic Or Video Upload</label>
                        <asp:FileUpload ID="fuPicOrVideo" class="form-control" runat="server" ValidationGroup="Profile" />
                          <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="hfLeftImage" runat="server" />
                          <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                          
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtSquanceNo" ValidationGroup="Profile"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-4" id="dvDrpSwapSequance" runat="server">
                    <div class="form-group">
                        <label for="drpChangeSequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                        <asp:DropDownList ID="drpChangeSequanceMethod" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                            <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" id="dvSwapSequance" runat="server">
                    <div class="form-group">
                        <label for="txtSwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtSwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile"
                            ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rgVSwapSequanseNo" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group form-control">
                        <asp:CheckBox ID="chkIsActive" runat="server" />
                        <label for="txtShortDescription">Active</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group">
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" ValidationGroup="Profile" />
                        <%} %>
                        <asp:Button runat="server" ID="btn_Cancel" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Clear" OnClick="btn_Cancel_Click" CausesValidation="false" ValidationGroup="Profile" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12" id="gvInnerGridView" runat="server">
                    <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                        DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1  %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PatientName" HeaderText="Patient Name" SortExpression="PatientName" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <asp:BoundField DataField="CityName" HeaderText="CityName" SortExpression="CityName" />
                            <asp:BoundField DataField="FilePath" HeaderText="Profile" Visible="false" SortExpression="FilePath" />
                            <asp:TemplateField HeaderText="View File">
                                <ItemTemplate>
                                    <a id="afile" href='<%# Eval("FileFullPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="isactive" />

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div class="btn-group">
                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                            { %>
                                        <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                        <%}
                                            if (SessionWrapper.UserPageDetails.CanDelete)
                                            { %>
                                        <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        <%} %>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                        SelectCommand="[GetAllPatientTestimonialMaster]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
