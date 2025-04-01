<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DoctorMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.DoctorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Doctor Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <style>
        textarea.form-control {
            height: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Doctor Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Hospital</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Doctor Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <%--<div class="card">
                <div class="card-body">
                    <ul class="nav nav-pills flex-column flex-sm-row" id="myTab" role="tablist">
                        <li class="flex-sm-fill text-sm-center nav-item">
                            <asp:LinkButton ID="lnkProfiletab" CssClass="nav-link active" data-toggle="tab" href="#bodyPart_Profile"  runat="server" role="tab" aria-selected="true" CauseValidation="false" OnClick="lnkProfiletab_Click">Profile</asp:LinkButton>
                        </li>
                        <li class="flex-sm-fill text-sm-center nav-item">
                            <asp:LinkButton ID="lnkSpecializationQualificationtab" CssClass="nav-link" data-toggle="tab" href="#bodyPart_SpecializationQualification" runat="server" CauseValidation="false" OnClick="lnkSpecializationQualificationtab_Click" role="tab" aria-selected="true">Specialization & Qualification</asp:LinkButton>
                           
                        </li>
                        <li class="flex-sm-fill text-sm-center nav-item">
                            <asp:LinkButton ID="lnkExpertiseAchievementstab" CssClass="nav-link" data-toggle="tab" href="#bodyPart_ExpertiseAchievements" runat="server" role="tab" CauseValidation="false" aria-selected="true" OnClick="lnkExpertiseAchievementstab_Click">Expertise & Achievements</asp:LinkButton>
                           
                        </li>
                        <li class="flex-sm-fill text-sm-center nav-item">
                            <asp:LinkButton ID="lnkPublicationstab" CssClass="nav-link" data-toggle="tab" href="#bodyPart_Publications" runat="server" role="tab" aria-selected="true" CauseValidation="false" OnClick="lnkPublicationstab_Click">Publications</asp:LinkButton>
                           
                        </li>
                    </ul>
                </div>
            </div>--%>

    <div class="">
        <div class="">
            <div class="tab-content" id="myTabContent">
                <div class="tab-pane fade show active" id="Profiletabpanel" runat="server" role="tabpanel">
                    <div class="card" id="dvNext" runat="server" visible="false">
                        <div class="card-body">
                            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnNext_Click" />

                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Profile</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfId" runat="server" />
                                        <label for="ddlLanguage">Language<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                                            ErrorMessage="Enter Select Language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtFirstName">First Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Profile"
                                            ErrorMessage="Enter First Name" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtMiddleName">Middle Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMiddleName" CssClass="validationmsg" runat="server" ControlToValidate="txtMiddleName" ValidationGroup="Profile"
                                            ErrorMessage="Enter Middle Name" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtLastName">Last Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLastName" CssClass="validationmsg" runat="server" ControlToValidate="txtLastName" ValidationGroup="Profile"
                                            ErrorMessage="Enter Last Name" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtShortDescription">Short Description</label>
                                        <asp:TextBox ID="txtShortDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtDescription">Description</label>
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" ValidationGroup="Profile"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtSquanceNo" ValidationGroup="Profile"
                                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" id="dvDrpSwapSequance" runat="server">
                                    <div class="form-group">
                                        <label for="drpChangeSequanceMethod">Swap Sequence No<span class="req-field">*</span></label>
                                         <asp:DropDownList ID="drpChangeSequanceMethod" CssClass="form-control" runat="server" Style="width: 100%">
                                        <%--<asp:DropDownList ID="drpChangeSequanceMethod" runat="server">--%>
                                            <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
                                            <asp:ListItem Text="Swap With Sequence" Value="Swap With Sequence"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="dvSwapSequance" runat="server">
                                    <div class="form-group">
                                        <label for="txtSwapSquanceNo">Swap Sequence No<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtSwapSquanceNo" runat="server" CssClass="form-control" ValidationGroup="Profile" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSwapSequanceNo" CssClass="validationmsg" runat="server" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile"
                                            ErrorMessage="Enter Swap Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rgVSwapSequanseNo" ControlToValidate="txtSwapSquanceNo" ValidationGroup="Profile" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtShortDescription">Profile Pic</label>
                                        <asp:FileUpload ID="fuProfilePic" class="form-control" runat="server" ValidationGroup="Profile" />
                                        <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>&nbsp;</label>
                                    <div class="form-group form-check form-control">
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                        <label for="txtShortDescription">Is Active</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-4">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" ValidationGroup="Profile" />

                                    <asp:Button runat="server" ID="btn_SaveAndNext" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save & Next" OnClick="btn_SaveAndNext_Click" ValidationGroup="Profile" />
                                    <%} %>
                                    <button runat="server" id="btn_Cancel" class="btn btn-secondary" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                        Clear
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="tab-pane fade" id="SpecializationQualificationtabpanel" runat="server" role="tabpanel">
                    <div class="card">
                        <div class="card-body">
                            <asp:Button ID="btnPerSpecialization" runat="server" CssClass="btn btn-primary" Text="Previous" OnClick="btnPerSpecialization_Click" />
                            <asp:Button ID="btnNextSpecialization" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnNextSpecialization_Click" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Specialization</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfSpRecId" Value="0" runat="server" />
                                        <label for="txtFirstName">Specialization</label>
                                        <asp:DropDownList ID="ddlSpecialization" class=" form-control" runat="server" ValidationGroup="Specialization">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btnSpecializationSave" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnSpecializationSave_Click" ValidationGroup="Profile" />
                                    <%} %>
                                    <button runat="server" id="btnSpecializationClear" class="btn btn-inverse" title="Cancel" onserverclick="btnSpecializationClear_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Clear</i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvSpecialization" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SpecializationId" HeaderText="SpecializationId" Visible="false" SortExpression="SpecializationId" />
                                            <asp:BoundField DataField="SpecializationName" HeaderText="Specialization Name" SortExpression="SpecializationName" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="ibtn_SpecializationEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_SpecializationEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ibtn_SpecializationDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_SpecializationDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Qualification</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfQualiRecId" Value="0" runat="server" />
                                        <label for="txtQualificaiton">Qualification Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtQualificaiton" runat="server" CssClass="form-control" ValidationGroup="Qualification"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtQualificaiton" ValidationGroup="Qualification"
                                            ErrorMessage="Enter Qualification Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                        <label for="txtQualificationShort">Short Name of Qualification</label>
                                        <asp:TextBox ID="txtQualificationShort" runat="server" CssClass="form-control" ValidationGroup="Qualification"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btnQualificationSave" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnQualificationSave_Click" ValidationGroup="Qualification" />
                                    <%} %>
                                    <button runat="server" id="btnQualificationClear" class="btn btn-inverse" title="Cancel" onserverclick="btnQualificationClear_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Clear</i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvQualification" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="QualificationName" HeaderText="Qualification Name" SortExpression="QualificationName" />
                                            <asp:BoundField DataField="QualificationShortName" HeaderText="Qualification ShortName" SortExpression="QualificationShortName" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="ibtn_QualificationEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_QualificationEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ibtn_QualificationDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_QualificationDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <div class="tab-pane fade" id="ExpertiseAchievementstabpanel" runat="server" role="tabpanel">
                    <div class="card">
                        <div class="card-body">
                            <asp:Button ID="btnPerAchievements" runat="server" CssClass="btn btn-primary" Text="Previous" OnClick="btnPerAchievements_Click" />
                            <asp:Button ID="btnNextAchievements" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnNextAchievements_Click" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Achievements</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfAchievementsId" Value="0" runat="server" />
                                        <label for="txtAchievements">Achievement Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAchievements" runat="server" CssClass="form-control" ValidationGroup="Achievements"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rftxtAchievements" CssClass="validationmsg" runat="server" ControlToValidate="txtAchievements" ValidationGroup="Achievements"
                                            ErrorMessage="Enter Achievement Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btnSaveAchievements" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnSaveAchievements_Click" ValidationGroup="Achievements" />
                                    <%} %>
                                    <button runat="server" id="btnClearAchievements" class="btn btn-inverse" title="Cancel" onserverclick="btnClearAchievements_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Clear</i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvAchievements" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AchievementsName" HeaderText="Achievements Name"/>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="ibtn_AchievementsEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_AchievementsEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ibtn_AchievementsDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_AchievementsDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <h6 class="card-title">Expertise</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfExpertiseId" Value="0" runat="server" />
                                        <label for="txtExpertise">Expertise Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtExpertise" runat="server" CssClass="form-control" ValidationGroup="Expertise"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rftxtExpertise" CssClass="validationmsg" runat="server" ControlToValidate="txtExpertise" ValidationGroup="Expertise"
                                            ErrorMessage="Enter Expertise Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btnSaveExpertise" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnSaveExpertise_Click" ValidationGroup="Expertise" />
                                    <%} %>
                                    <button runat="server" id="btnClearExpertise" class="btn btn-inverse" title="Cancel" onserverclick="btnClearExpertise_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Clear</i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvExpertise" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ExpertiseName" HeaderText="Expertise Name" SortExpression="ExpertiseName" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_ExpertiseEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_ExpertiseEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <%}
                                                            if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_ExpertiseDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_ExpertiseDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <div class="tab-pane fade" id="Publicationstabpanel" runat="server" role="tabpanel">
                    <div class="card">
                        <div class="card-body">
                            <asp:Button ID="btnPerPublications" runat="server" CssClass="btn btn-primary" Text="Previous" OnClick="btnPerPublications_Click" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Publications</h6>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfPublicationsId" Value="0" runat="server" />
                                        <label for="txtPublications">Publications Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtPublications" runat="server" CssClass="form-control" ValidationGroup="Publications"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rftxtPublications" CssClass="validationmsg" runat="server" ControlToValidate="txtPublications" ValidationGroup="Publications"
                                            ErrorMessage="Enter Publications Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                                        { %>
                                    <asp:Button runat="server" ID="btnSavePublications" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btnSavePublications_Click" ValidationGroup="Publications" />
                                    <%} %>
                                    <button runat="server" id="btnClearPublications" class="btn btn-inverse" title="Cancel" onserverclick="btnClearPublications_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Clear</i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvPublications" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Publications" HeaderText="Publications Name" SortExpression="Publications" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_PublicationsEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_PublicationsEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <%}
                                                            if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_PublicationsDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_PublicationsDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
        </div>
    </div>
    <asp:Panel ID="pnlView" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-9" id="tblSearch">
                        <div class="form-group">
                            <div class="controls">
                                <div class="input-group">
                                    <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
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
                    <div class="col-md-12" id="gvInnerGridView" runat="server">
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DoctorFirstName" HeaderText="First Name"/>
                                <asp:BoundField DataField="DoctorMiddleName" HeaderText="Middle Name"/>
                                <asp:BoundField DataField="DoctorLastName" HeaderText="Last Name" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequence No" />
                                <asp:BoundField DataField="DoctorShotDescription" HeaderText="Shot Description" />
                                <asp:BoundField DataField="DoctorDescription" HeaderText="Description" Visible="false" />
                                <asp:BoundField DataField="DoctorProfilePic" HeaderText="Profile" Visible="false" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />

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
                            SelectCommand="[GetAllDoctorMaster]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
