<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="AcademicDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.AcademicDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Academic Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Academic Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Acedemic Details</li>
            </ul>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="col-md-7 col-lg-12">
        <!-- Basic Information -->
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Academic Details: (Starting from SSC/Equivalent)</h4>
                <div class="row form-row">
                    <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                    <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Degree/Diploma <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdegree" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlOtherSpeciality">Education Type</label>
                            <asp:DropDownList ID="ddlEductionType" OnSelectedIndexChanged="ddlEductionType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEductionType"
                                ErrorMessage="Select EductionType." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlOtherSpeciality">Education Name</label>
                            <asp:DropDownList ID="ddlEducationName" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlEducationName"
                                ErrorMessage="Select EducationName." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Name of School/Colege <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtschoolname" runat="server" type="text" class="form-control txtletterUpper"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtschoolname"
                                ErrorMessage="Please enter Name of School/Colege." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Board/University</label>
                            <asp:TextBox ID="txtboard" runat="server" class="form-control txtletterUpper"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtboard"
                                ErrorMessage="Please enter Board/University." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="col-md-3">
                        <div class="form-group">
                            <label>Year of passing</label>
                            <asp:TextBox ID="txtyear" runat="server" type="number" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationmsg" runat="server" ControlToValidate="txtyear"
                                ErrorMessage="Please enter Year of passing." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Passing Month<span class="req-field">*</span></label>
                            <div class="">
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Month" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="Jan"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="Feb"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="Mar"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="Apr"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="May"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="Jun"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="Jul"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="Aug"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="Sep"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="Oct"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="Nov"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="Dec"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlHistoryYear">Passing Year</label>
                            <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAcademicYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlAcademicYear"
                                ErrorMessage="Select Passing year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Major Subject</label>
                            <asp:TextBox ID="txtmsubject" runat="server" class="form-control txtletterUpper"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationmsg" runat="server" ControlToValidate="txtmsubject"
                                ErrorMessage="Please enter Major Subject." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Division & %</label>
                            <asp:TextBox ID="txtdivision" runat="server" CssClass="form-control decimal"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationmsg" runat="server" ControlToValidate="txtdivision"
                                ErrorMessage="Please enter Division." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnKnowPersonDetails" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" OnClick="btnAdd_ServerClick" Text="Add Details" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 10px;">
                        <asp:GridView ID="gvAcademicDetails" runat="server" OnRowCommand="gView_RowCommand" AutoGenerateColumns="False" DataKeyNames="Id,CandidateId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                <asp:BoundField DataField="TypeName" HeaderText="Education Type" SortExpression="TypeName" />
                                <asp:BoundField DataField="EducationDetailName" HeaderText="Education Name" SortExpression="EducationDetailName" />
                                <asp:BoundField DataField="NameOfSchoolCollege" HeaderText="Name Of SchoolCollege" SortExpression="NameOfSchoolCollege" />
                                <asp:BoundField DataField="BoardUniversity" HeaderText="BoardUniversity" SortExpression="BoardUniversity" />
                                <asp:BoundField DataField="PassingYear" HeaderText="PassingYear" SortExpression="PassingYear" />
                                <asp:BoundField DataField="MajorSubjects" HeaderText="MajorSubjects" SortExpression="MajorSubjects" />
                                <asp:BoundField DataField="Division" HeaderText="Division" SortExpression="Division" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_FamilyEdit" CommandName="eEdit" CausesValidation="false" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_FamilyDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClientClick='<%# Eval("NameOfSchoolCollege", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Computer Literacy</label>
                            <asp:DropDownList ID="ddlcomputerlit" CssClass="form-control select" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="form-group mb-0">
                            <label>Remerks</label>
                            <textarea id="txtremerks" runat="server" class="form-control txtletterUpper"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Img_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnRowCommand="GridView1_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ImageField DataImageUrlField="Image_desc" HeaderText="Photo" ControlStyle-Height="50" ControlStyle-Width="50">
                                            </asp:ImageField>
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <a id="afile" data-toggle="tooltip" title="View" href='<%# Eval("Image_desc") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"></a>
                                                    <a class="copy_text fa fa-clipboard" data-toggle="tooltip" title="Copy to Clipboard" href='<%# Eval("Image_desc") %>'></a>
                                                    <asp:Label runat="server" Style="display: none" ID="ImagrUrl" Text='<%# Eval("Image_desc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="is_active_tamd" HeaderText="Is Active" SortExpression="is_active_tamd" />
                                            <asp:BoundField DataField="is_download" HeaderText="Is Download" SortExpression="is_download" />
                                            <asp:TemplateField HeaderText="Active/Inactive">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:DropDownList ID="ddlGrdActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                            <asp:ListItem Value="True" Text="Active"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="InActive"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download Y/N">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:DropDownList ID="ddlIsDownload" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                            <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>--%>
        <!-- /Basic Information -->
        <!-- Education -->
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Language</h4>
                <div class="education-info">
                    <div class="row form-row education-cont">
                        <div class="col-12 col-md-10 col-lg-11">
                            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" id="divRights" runat="server">
                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th>Language
                                                </th>
                                                <th>
                                                    <label>Read</label>
                                                    <%-- <asp:CheckBox ID="chkbview" runat="server" Text="Read" EnableViewState="true"
                                                        OnCheckedChanged="chkbView_CheckedChanged" />--%>
                                                </th>
                                                <th>
                                                    <label>Write</label>
                                                    <%--<asp:CheckBox ID="chkbadd" runat="server" Text="Write" EnableViewState="true"
                                                        OnCheckedChanged="chkbAdd_CheckedChanged" />--%>
                                                </th>
                                                <th>
                                                    <label>Speek</label>
                                                    <%--<asp:CheckBox ID="chkbupdate" runat="server" Text="Speek" EnableViewState="true"
                                                        OnCheckedChanged="chkbupdate_CheckedChanged" />--%>
                                                </th>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptUserRights">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="first" runat="server" Text='<%# HttpUtility.HtmlDecode(Eval("Language_Name").ToString()) %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server"
                                                                ID="chk1" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server"
                                                                ID="chk2" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server"
                                                                ID="chk3" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblLanguageID" runat="server" Text='<%# HttpUtility.HtmlDecode(Eval("id").ToString()) %>'
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Education -->
        <!-- Experience -->
        <%--<div class="card">
                <div class="card-body">
                    <h4 class="card-title">Upload your Resume</h4>
                    <div class="form-group">
                        <div class="change-avatar">
                            <div class="upload-img">
                                <div class="change-photo-btn">
                                    <span><i class="fa fa-upload"></i>Choose File</span>
                                    <input type="file" class="upload">
                                </div>
                                <small class="form-text text-muted">Allowed PDF Max size of 10MB</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
        <!-- /Experience -->
        <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
            <div class="row form-row">
                <div class="col-md-5">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnPrivious" runat="server" Text="Previous" CssClass="btn btn-primary submit-btn" OnClick="btnPrivious_Click" CausesValidation="false" />
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CssClass="btn btn-primary submit-btn" OnClick="btnSubmit_ServerClick" Text="Next" />
                </div>
                <div class="col-md-4">
                </div>
            </div>

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
