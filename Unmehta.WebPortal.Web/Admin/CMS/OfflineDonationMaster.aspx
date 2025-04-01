<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OfflineDonationMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.OfflineDonationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Offline Donation Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Offline Donation Master</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Offline Donation Master</li>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="drpLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">First Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFname" CssClass="validationmsg" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="Please enter First Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Last Name<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLname" CssClass="validationmsg" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="Please enter Last Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Email<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtemail" TextMode="Email" type="email" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" CssClass="validationmsg" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="Please enter email." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Mobile No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtMoNo" TextMode="Phone" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvmono" CssClass="validationmsg" runat="server" ControlToValidate="txtMoNo"
                                ErrorMessage="Please enter Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Pan No.<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtPanno" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationExpression="([A-Z]){5}([0-9]){4}([A-Z]){1}$" ID="rfvpanno" CssClass="validationmsg" runat="server" ControlToValidate="txtPanno"
                                ErrorMessage="Please enter Pan Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Amount<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtamount" TextMode="Number" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvamount" CssClass="validationmsg" runat="server" ControlToValidate="txtamount"
                                ErrorMessage="Please enter Ammount." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlPaidUnpaid" CssClass="form-control" TabIndex="3" runat="server" Style="width: 100%">
                                <asp:ListItem Value="1" Selected="True" Text="Paid"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Unpaid"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Address<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtaddress" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvadd" CssClass="validationmsg" runat="server" ControlToValidate="txtaddress"
                                ErrorMessage="Please enter Address." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="exampleInputFile">Upload Recipt<span class="req-field">*</span></label>
                                <asp:FileUpload accept=".png,.jpg,.jpeg,.gif,.pdf,.doc" ID="reciptDoc" runat="server" TabIndex="2" CssClass="form-control" />
                                <label visible="false" style="font-weight: normal;" id="ReciptImgfilename" runat="server"></label>
                            </div>

                        </div>
                    </div>
                </div>
                <%-- <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputFile">Sequence No.<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtsequence" TabIndex="1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>--%>
                <div class="col-md-4">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Save" OnClick="btn_Save_Click" />
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
                        <div class="form-group" style="overflow-x: scroll;">
                            <asp:GridView ID="grdOfflineDonation" runat="server" DataSourceID="sqlds" AutoGenerateColumns="False" DataKeyNames="ODId,recid,Languageid" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                OnRowCommand="grdOfflineDonation_RowCommand" AllowSorting="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                    <asp:BoundField DataField="Lastname" HeaderText="Last Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="MoNo" HeaderText="Mobile No" />
                                    <asp:BoundField DataField="PanNo" HeaderText="Pan No" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:TemplateField HeaderText="Document">
                                        <ItemTemplate>
                                            <a id="LogoFile" href='<%# Eval("ReciptPath") %>' target="_blank" runat="server" tooltip="View Img" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                    { %>
                                                <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (SessionWrapper.UserPageDetails.CanDelete)
                                                    { %>
                                                <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("FirstName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                                <%-- <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                    CommandArgument='<%# Eval("SSId") + "," + Eval("SS_level_id") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                    CommandArgument='<%# Eval("SSId") + "," + Eval("SS_level_id") + "," +   "down" %>'
                                                    runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                                </asp:LinkButton>--%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                SelectCommand="[GetAllOfflineDonation]" SelectCommandType="StoredProcedure" FilterExpression="FirstName like '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter ControlID="txtSearch" Name="FirstName" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="LastName" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="Email" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="MoNo" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="PanNo" />
                                    <asp:ControlParameter ControlID="txtSearch" Name="Amount" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                            <%-- <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:ConString %>"
                                        SelectCommand="[tbl_Menu_MasterSelectAll]" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnRecid" runat="server" />
        <asp:HiddenField ID="hdnODID" runat="server" />

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
