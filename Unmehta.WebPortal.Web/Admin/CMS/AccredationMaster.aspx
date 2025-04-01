<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AccredationMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.AccredationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Accreditation Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">

    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Accreditation Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Accreditation Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
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
                                    <div class="form-group">
                                        <div class="controls">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                                        <i class="fa fa-search">&nbsp;Search</i>
                                                    </button>
                                                </span>
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_Click">
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
                                    <div class="form-group">
                                        <asp:GridView ID="gview_Accredation" runat="server" AutoGenerateColumns="False" DataKeyNames="Acc_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gview_Accredation_RowCommand" OnPageIndexChanging="gview_Accredation_PageIndexChanging" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Accredation_Title" HeaderText="Accredation Title Name" SortExpression="Accredation_Title" />
                                                <asp:BoundField DataField="IsVisible" HeaderText="Active" SortExpression="IsVisible" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%} %>
                                                            <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                            <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                                { %>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Accredation_Title", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                            <%} %>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[PROC_AccredationMaster_Search]" SelectCommandType="StoredProcedure" FilterExpression="Accredation_Title like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="Accredation_Title" />

                                            </FilterParameters>
                                        </asp:SqlDataSource>
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
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfId" Value="0" runat="server" />
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaTitle" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaTitle"
                                            ErrorMessage="Please meta title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Meta Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtMetaDesc"
                                            ErrorMessage="Please enter meta description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Accreditation Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAcc_Title" TabIndex="1" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtAcc_Title"
                                            ErrorMessage="Please Accredation title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Accreditation URL<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAccredationURL"  runat="server" CssClass="form-control"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Display In Footer?<span class="req-field">*</span></label>
                                        <asp:CheckBox ID="chkIsDisplayInFooter" runat="server" />
                                    </div>
                                </div>
                                
                                 <div class="col-md-3" style="display:none">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Display In Header?<span class="req-field">*</span></label>
                                        <asp:CheckBox ID="chkIsDisplayInHeader" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlIsVisible" CssClass="form-control" TabIndex="4" runat="server" Style="width: 100%">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Logo<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuLogo" runat="server" TabIndex="2" CssClass="form-control" />
                                        <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfLeftImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                                        <%--<p class="help-block">( Image should be 566px*260px )</p>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Select Image<span class="req-field">*</span></label>
                                        <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuDocUpload" runat="server" TabIndex="2" CssClass="form-control" />
                                        <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hfRightImage" runat="server" />
                                        <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" class="fa fa-trash-o btn btn-primary" id="aRemoveRight" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                                        <%--<p class="help-block">( Image should be 566px*260px )</p>--%>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Accreditation Description<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtAccredationDesc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtAccredationDesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12 BtnGrp">
                                        <div class="form-group">
                                            <% if (SessionWrapper.UserPageDetails.CanAdd)
                                                { %>
                                            <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_Click">
                                                <i class="fa fa-floppy-o">&nbsp;Save</i>
                                            </button>
                                            <%}%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                                        <div class="form-group">
                                            <p style="">You Can Add Multiple/Single Accreditation List using <i class="fa fa-plus-circle"></i>.</p>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Accreditation Name<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtAccredationName" TabIndex="1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="display: none">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Accreditation Month Year<span class="req-field">*</span></label>
                                            <div class="row">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control col-6">
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
                                    <div class="col-md-4" style="display: none">
                                        <div class="form-group">
                                            <label for="ddlHistoryYear">Accreditation Year</label>
                                            <asp:DropDownList ID="ddlAccredationYear" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAccredationYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlAccredationYear"
                                                ErrorMessage="Select accredation year." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                            <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                                <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Accreditation Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtADescription" TextMode="MultiLine" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvadesc" CssClass="validationmsg" runat="server" ControlToValidate="txtADescription"
                                            ErrorMessage="Please enter Accreditation description." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Accreditation Date<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtdate" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvadesc" CssClass="validationmsg" runat="server" ControlToValidate="txtADescription"
                                            ErrorMessage="Please enter Accreditation description." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>&nbsp;</label>
                                        <div class="form-group">
                                            <button class="fa fa-plus-circle btn btn-primary" data-toggle="tooltip" style="cursor: pointer;" title="Add" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="AM_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        AllowPaging="true" AllowSorting="false" OnPageIndexChanging="gView_PageIndexChanging"
                                        OnRowCommand="gView_RowCommand" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr NO." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Accredation_Name" HeaderText="Accredation Name" SortExpression="Accredation_Name" />
                                            <asp:BoundField DataField="Accredation_Description" HeaderText="Accredation Description" SortExpression="Accredation_Description" />
                                            <%--<asp:BoundField DataField="Accredation_MonthYear" HeaderText="Accredation Year" SortExpression="Accredation_MonthYear" />--%>
                                            <asp:BoundField DataField="Ac_Date" HeaderText="Date" SortExpression="Ac_Date" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <%} %>
                                                        <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                        <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                            { %>
                                                        <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("Accredation_Name", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        <%} %>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                        SelectCommand="[PROC_AccredationMasterdetails_Search]" SelectCommandType="StoredProcedure">
                                        <FilterParameters>--%>
                                    <%--<asp:ControlParameter ControlID="txtSearch" Name="Accredation_Name" />--%>

                                    <%--</FilterParameters>
                                    </asp:SqlDataSource>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 BtnGrp">
                                <div class="form-group">
                                    <button runat="server" id="btngView_PageIndexChangingfinalsave" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btnfinalsave_ServerClick">
                                        <i class="fa fa-floppy-o">&nbsp;Final Save</i>
                                    </button>
                                    <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                                        <i class="fa fa-remove">&nbsp;Cancel</i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </asp:Panel>
            </div>
            </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
