<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StatisticsChartMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.StatisticsChartMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Statistics Chart Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Patient Testimonial Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Hospital</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Patient Testimonial Master</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="hfID" runat="server" />
                <div class="row">

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtChartName">Chart Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtChartName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtChartName" ValidationGroup="main"
                                ErrorMessage="Enter Chart Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>


                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtChartName">X Column Chart Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtXColumnChartName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>



                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtChartName">Y Column Chart Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtYColumnChartName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    

                    <div class="col-md-4">
                        <label>&nbsp;</label>
                        <div class="form-group form-control">
                            <asp:CheckBox ID="chkActive" runat="server" />
                            <label for="txtChartName">Is Active</label>
                            
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtXColumnName">Chart Type<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlChartType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChartType_SelectedIndexChanged">
                                <asp:ListItem Text="Select Chart Type" Value=""></asp:ListItem>
                                <asp:ListItem Text="Column Chart" Value="ColumnChart"></asp:ListItem>
                                <asp:ListItem Text="Column Charts with Multiple Axes" Value="ColumnChartswithMultipleAxes"></asp:ListItem>
                                <asp:ListItem Text="PIE Chart" Value="PIEChart"></asp:ListItem>
                                <asp:ListItem Text="Line Chart" Value="LineChart"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4" id="dvChartXFormate" runat="server">
                        <div class="form-group">
                            <label for="txtChartName">Chart X Column Date<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtXDateFormate" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <span><b>Note :-</b> Please add Formate Like "YYYY","DD MMM YYYY"</span>
                        </div>
                    </div>
                </div>

                <hr />
                <h3>Create Column Chart </h3>

                <div class="row">
                    <asp:HiddenField ID="hfColumnId" runat="server" />

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtColumnName">Column Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtColumnName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtColumnName" ValidationGroup="mainSub1"
                                ErrorMessage="Enter Chart Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtXColumnName">Column Chart Type<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlColumnChartType" runat="server" CssClass="form-control" ValidationGroup="mainSub1">
                                <asp:ListItem Text="Select Column Chart Type" Value=""></asp:ListItem>
                                <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Button ID="btnSubmitColumn" CssClass="btn btn-primary" runat="server" Text="Submit Column Form" OnClick="btnSubmitColumn_Click" ValidationGroup="mainSub1" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear Column Form" OnClick="btnClear_Click" CausesValidation="false" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <asp:GridView ID="gvColumnChart" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="TypeColumn" HeaderText="Type Column" SortExpression="TypeColumn" />
                                <asp:BoundField DataField="ColName" HeaderText="Column Name" SortExpression="ColName" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtnColumnDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtnColumnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

                <hr />

                <h3>Column Chart Data</h3>
                <div class="row">
                    <asp:HiddenField ID="hfRowIndexid" runat="server" />

                    <asp:Repeater ID="dlColumnForm" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblColumnName" for="txtColumnName" runat="server" Text='<%# Eval("ColumnName") %>'></asp:Label>
                                    <asp:HiddenField ID="hfHiddenFieldId" runat="server" Value='<%# Eval("SequanceNo") %>' />
                                    <asp:TextBox ID="txtColumnName" MaxLength="50" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfValidator" CssClass="validationmsg" runat="server" ControlToValidate="txtColumnName" ValidationGroup="mainSub2" Display="Dynamic"
                                        ErrorMessage="Enter Column Data" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfTypeColumn" runat="server" Value='<%#Eval("TypeColumn") %>' />
                            <div class="col-md-4" <%# Eval("TypeColumn").ToString()=="X"?"style=\"display: block\"":"style=\"display: none\"" %>>
                                <div class="form-group">
                                    <asp:Label ID="lblColumnAliasName" for="txtColumnNameAlias" runat="server" Text='Alias Name'></asp:Label>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("SequanceNo") %>' />
                                    <asp:TextBox ID="txtColumnNameAlias" MaxLength="50" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="rfAliasValidator" CssClass="validationmsg" runat="server" ControlToValidate="txtColumnNameAlias" ValidationGroup="mainSub2" Display="Dynamic"
                                        ErrorMessage="Enter Column Data" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="col-md-12">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Button ID="btnSubmitData" CssClass="btn btn-primary" runat="server" Text="Submit Chart Data" OnClick="btnSubmitData_Click" ValidationGroup="mainSub2" />
                            <asp:Button ID="btnClearData" CssClass="btn btn-secondary" runat="server" Text="Clear Chart Form" OnClick="btnClearData_Click" CausesValidation="false" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <asp:GridView ID="gvChartData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowDataBound="gvChartData_RowDataBound">
                            <Columns>


                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkSwapUp" CommandName="eSwapUp" runat="server" SkinID="lSwapUp" CausesValidation="false" OnClick="lnkSwapUp_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-arrow-circle-up"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkSwapDown" CommandName="eSwapDown" runat="server" SkinID="lSwapDown" CausesValidation="false" OnClick="lnkSwapDown_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-arrow-circle-down"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtnColumnDataEdit" CommandName="eEdit" runat="server" SkinID="lEdit" CausesValidation="false" OnClick="ibtnColumnDataEdit_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtnColumnDataDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtnColumnDataDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>

                            <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />

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
                                    <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control"></asp:TextBox>
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
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" DataKeyNames="Id" EmptyDataText="Record does not exist..."
                                AllowPaging="true" OnPageIndexChanging="gvDetails_PageIndexChanging" OnRowCommand="gvDetails_RowCommand" >
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="ChartName" HeaderText="Chart Name" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="ChartType" HeaderText="Chart Type" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                
                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                CommandArgument='<%# Eval("Id") + "," + Eval("SequanceNo") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                CommandArgument='<%# Eval("Id") + "," + Eval("SequanceNo") + "," +   "down" %>'
                                                runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                            </asp:LinkButton>

                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} %>
                                                <% if (SessionWrapper.UserPageDetails.CanView)
                                                    { %>

                                                <asp:LinkButton ID="lnkView" CommandName="eView" runat="server" SkinID="lView" CausesValidation="false" OnClick="lnkView_Click" OnClientClick="javascript:return true;" data-original-title="View" CssClass="btn btn-sm  show-tooltip"><i class="fa fa-search-plus"></i></asp:LinkButton>
                                                <%} %>
                                                <% if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Remove" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lnkMenu_Remove_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <%-- <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:ConString %>"
                                        SelectCommand="[tbl_Menu_MasterSelectAll]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div id="chartContainer" runat="server" style="height: 180px">
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script src="<%=ResolveUrl("~/Scripts/canvasjs.min.js") %>"></script>
</asp:Content>
