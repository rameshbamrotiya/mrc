<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ExtraDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Faculty.ExtraDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Course Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Faculty Extra Details</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Faculty Extra Details</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/CMS/CK/ckeditor.js") %>"></script>
    <section class="content">
        <div class="card">
            <div class="card-body">
                <!-- Bootstrap alert -->
                <div class="row">
                    <div class="form-group col-md-12">
                        <div class="messagealert" id="alert_container">
                        </div>
                    </div>
                </div>
                <!-- END Bootstrap alert -->
                <asp:Panel ID="pnlView" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <h3>Education Details</h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="pull-right">
                                        <div class="input-group">
                                            <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_Click" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EducationName" HeaderText="Education Name" SortExpression="EducationName" />
                                                <asp:BoundField DataField="FromYear" HeaderText="From Year" SortExpression="FromYear" />
                                                <asp:BoundField DataField="ToYear" HeaderText="To Year" SortExpression="ToYear" />
                                                <asp:BoundField DataField="DegreeName" HeaderText="Degree Name" SortExpression="DegreeName" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("EducationName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <h3>Area Experience Details</h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="pull-right">
                                        <div class="input-group">
                                            <button runat="server" id="btnAddAreaExperience" class="btn btn-primary" title="Add" onserverclick="btnAddAreaExperience_ServerClick" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gViewAreaExperience" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gViewAreaExperience_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployerName" HeaderText="Education Name" SortExpression="EducationName" />
                                                <asp:BoundField DataField="FromYear" HeaderText="From Year" SortExpression="FromYear" />
                                                <asp:BoundField DataField="ToYear" HeaderText="To Year" SortExpression="ToYear" />
                                                <asp:BoundField DataField="IsPresent" HeaderText="Is Present" SortExpression="IsPresent" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("EmployerName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <br />
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <h3>Publication Research Description</h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="pull-right">
                                        <div class="input-group">
                                            <button runat="server" id="btnPublicationResearch" class="btn btn-primary" title="Add" onserverclick="btnPublicationResearch_ServerClick" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gViewPublicationResearch" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gViewPublicationResearch_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                <asp:BoundField DataField="FromYear" HeaderText="From Year" SortExpression="FromYear" />
                                                <asp:BoundField DataField="ToYear" HeaderText="To Year" SortExpression="ToYear" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <h3>Awards Details</h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="pull-right">
                                        <div class="input-group">
                                            <button runat="server" id="btnAddAwards" class="btn btn-primary" title="Add" onserverclick="btnAddAwards_ServerClick" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gViewAwards" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gViewAwards_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Awards Title" SortExpression="Title" />
                                                <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" />
                                                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                                                <asp:BoundField DataField="AwardsDescription" HeaderText="AwardsDescription" SortExpression="Awards Description" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-9" id="tblSearch">
                                    <h3>Service Details</h3>
                                </div>
                                <div class="col-md-3">
                                    <div class="pull-right">
                                        <div class="input-group">
                                            <button runat="server" id="btnAddService" class="btn btn-primary" title="Add" onserverclick="btnAddService_ServerClick" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gViewService" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            AllowPaging="true" AllowSorting="false" OnRowCommand="gViewService_RowCommand" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("ServiceName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Education <span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEducation" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEducation" CssClass="validationmsg" runat="server" ControlToValidate="txtEducation"
                                            ErrorMessage="Please enter education" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">From Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtFromYear" TabIndex="2" MaxLength="50" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                      
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">To Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtToYear" TabIndex="3" MaxLength="50" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Degree<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtDegree" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDegree" CssClass="validationmsg" runat="server" ControlToValidate="txtDegree"
                                            ErrorMessage="Please enter to degree." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_Click">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlAreaExperienceEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Employer Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtEmployerName" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmployerName" CssClass="validationmsg" runat="server" ControlToValidate="txtEmployerName"
                                            ErrorMessage="Please enter employer name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfAreaExperienceId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">From Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAreaFromYear" TabIndex="2" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAreaFromYear" CssClass="validationmsg" runat="server" ControlToValidate="txtAreaFromYear"
                                            ErrorMessage="Please enter from year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divYear" runat="server" visible="true">
                                    <div class="form-group">
                                        <label for="exampleInputFile">To Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAreaToYear" TabIndex="3" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAreaToYear" CssClass="validationmsg" runat="server" ControlToValidate="txtAreaToYear"
                                            ErrorMessage="Please enter to year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Is Present<span class="req-field">*</span></label>
                                        <asp:CheckBox ID="chkIsPresent" runat="server" CssClass="form-control" TabIndex="4" OnCheckedChanged="chkIsPresent_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btnAddAreaExperienceDetails" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnAddAreaExperienceDetails_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btnUpdateAreaExperienceDetails" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btnUpdateAreaExperienceDetails_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btnCancelAreaExperienceDetails" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btnCancelAreaExperienceDetails_ServerClick" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPublicationResearchEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <asp:HiddenField ID="hfPublicationResearch" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">From Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtPublicationResearchFromYear" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtPublicationResearchFromYear"
                                            ErrorMessage="Please enter from year." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3" id="div2" runat="server" visible="true">
                                    <div class="form-group">
                                        <label for="exampleInputFile">To Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtPublicationResearchToYear" TabIndex="2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtPublicationResearchToYear"
                                            ErrorMessage="Please enter to year." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtPublicationResearchDescription" runat="server" CssClass="form-control" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription" CssClass="validationmsg" runat="server" ControlToValidate="txtPublicationResearchDescription"
                                            ErrorMessage="Please enter to description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btnPublicationResearchSave" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnPublicationResearchSave_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btnPublicationResearchUpdate" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btnPublicationResearchUpdate_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btnPublicationResearchCancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btnPublicationResearchCancel_ServerClick" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlAwardsEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Awards Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAwardsTitle" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtAwardsTitle"
                                            ErrorMessage="Please enter awards title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfAwardsId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Month<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMonth" TabIndex="2" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMonth" CssClass="validationmsg" runat="server" ControlToValidate="txtMonth"
                                            ErrorMessage="Please enter month." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" id="div1" runat="server" visible="true">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Year<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtYear" TabIndex="3" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvYear" CssClass="validationmsg" runat="server" ControlToValidate="txtYear"
                                            ErrorMessage="Please enter to year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Awards Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAwardsDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtAwardsDescription.ClientID%>');
                                        </script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btnSaveAwards" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnSaveAwards_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btnUpdateAwards" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btnUpdateAwards_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btnCancelAwards" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btnCancelAwards_ServerClick" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlServiceEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Service Name<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvServiceName" CssClass="validationmsg" runat="server" ControlToValidate="txtServiceName"
                                            ErrorMessage="Please enter service name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfServiceId" Value="0" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btnSaveService" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnSaveService_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btnUpdateService" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btnUpdateService_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                                <button runat="server" id="btnCancelService" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btnCancelService_ServerClick" causesvalidation="false">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
