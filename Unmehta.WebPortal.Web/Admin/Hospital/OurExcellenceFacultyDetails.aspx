<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OurExcellenceFacultyDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.OurExcellenceFacultyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Our Excellence Faculty Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Our Excellence Details</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Our Excellence Faculty Details</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <div class="card">
        <div class="card-body">
            <div class="row">
                
                <div class="col-md-6">
                    <h6 class="card-title">Faculty</h6>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:HiddenField ID="hfFacultyId" Value="0" runat="server" />
                                <label for="txtFirstName">Faculty</label>
                                <asp:DropDownList ID="ddlFaculty" CssClass=" form-control" runat="server" ValidationGroup="Faculty">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtFacultySquanceNo" runat="server" CssClass="form-control" ValidationGroup="Faculty" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFacultySquanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtFacultySquanceNo" ValidationGroup="Faculty"
                                    ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvFacultySquanceNo" ControlToValidate="txtFacultySquanceNo" ValidationGroup="Faculty" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-12" id="dvDrpFacultySwapSequance" runat="server">
                            <div class="form-group">
                                <label for="drpChangeFacultySequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                                <asp:DropDownList ID="drpChangeFacultySequanceMethod" CssClass=" form-control" runat="server">
                                    <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                                    <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12" id="dvSwapFacultySequance" runat="server">
                            <div class="form-group">
                                <label for="txtFacultySwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtFacultySwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Faculty" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFacultySwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtFacultySwapSquanceNo" ValidationGroup="Faculty"
                                    ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rgVFacultySwapSequanseNo" ControlToValidate="txtFacultySwapSquanceNo" ValidationGroup="Faculty" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                              <% if (SessionWrapper.UserPageDetails.CanAdd)
                                  { %>
                            <asp:Button runat="server" ID="btnFacultySave" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save Faculty" OnClick="btnFacultySave_Click" ValidationGroup="Faculty" />
                            <%} %>
                            <button runat="server" id="btnFacultyClear" class="btn btn-inverse" title="Cancel" onserverclick="btnFacultyClear_ServerClick" causesvalidation="false">
                                <i class="fa fa-remove">&nbsp;Clear Faculty</i>
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvFaculty" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="true" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" SortExpression="FacultyName" />
                                    <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No" SortExpression="SequenceNo" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                  <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                      { %>
                                                <asp:LinkButton ID="ibtn_FacultyEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_FacultyEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                  <%} if (SessionWrapper.UserPageDetails.CanDelete)
                                                      { %>
                                                <asp:LinkButton ID="ibtn_FacultyDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_FacultyDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
