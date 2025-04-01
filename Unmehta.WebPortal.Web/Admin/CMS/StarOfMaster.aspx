<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StarOfMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.StarOfMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Star Of Master</title>
    <style>
        .radiobtn input[type="radio"] {
            margin-left: 5px !important;
            margin-right: 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Star Of Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Star Of Master</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <div class="card">
        <div class="card-body">
            <asp:HiddenField ID="hfID" runat="server" />
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" runat="server" ValidationGroup="main">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Page Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvname" CssClass="validationmsg" runat="server" ControlToValidate="txtName" ValidationGroup="main"
                            ErrorMessage="Enter Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Month Tab Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtMonthTabName" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtMonthTabName" ValidationGroup="main"
                            ErrorMessage="Enter Month Tab Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Week Tab Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtWeekTabName" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtWeekTabName" ValidationGroup="main"
                            ErrorMessage="Enter Week Tab Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Accord Month Tab Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtAccordMonthTitle" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" CssClass="validationmsg" runat="server" ControlToValidate="txtAccordMonthTitle" ValidationGroup="main"
                            ErrorMessage="Enter Accord Month Tab Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Accord Week Tab Name<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtAccordWeekTitle" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" CssClass="validationmsg" runat="server" ControlToValidate="txtAccordWeekTitle" ValidationGroup="main"
                            ErrorMessage="Enter Accord Week Tab Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row" id="dvSubDetails" runat="server">
                <asp:HiddenField ID="hfType5Command" Value="0" runat="server" />
                <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                    <div class="form-group">
                        <p style="">You Can Add Multiple/Single SubDetails <i class="fa fa-plus-circle"></i>.</p>
                    </div>
                </div>
                <asp:HiddenField ID="hfSequanceId" runat="server" />
                <asp:HiddenField ID="hfSubId" runat="server" />

                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">Accord Title<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtAccordTitle" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSubTitle" CssClass="validationmsg" runat="server" Display="Dynamic" ControlToValidate="txtAccordTitle" ValidationGroup="Sub"
                            ErrorMessage="Enter Accord Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Sequence No<span class="req-field">*</span></label>
                                
                                <asp:TextBox ID="txtSequenceNo" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" CssClass="validationmsg" runat="server" ControlToValidate="txtSequenceNo" ValidationGroup="Sub"
                                    ErrorMessage="Enter Sequance No." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label for="rblGender">For Star of the ?</label>
                        <div class="form-group">
                            <asp:RadioButtonList ID="rblstar" runat="server" CssClass="form-control radiobtn" RepeatLayout="Flow" RepeatDirection="Horizontal" ValidationGroup="Sub">
                                <asp:ListItem Value="1" Text="Week"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Month"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvGender" InitialValue="" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="rblstar" ValidationGroup="Sub"
                                ErrorMessage="Select Star Of the?." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>&nbsp;</label>
                    <div class="form-group form-control">
                        <asp:CheckBox ID="chkParentIsVisible" runat="server" ValidationGroup="slider" />
                        <label for="exampleInputFile">Visible</label>
                    </div>
                </div>

                <div class="container-fluid" id="divphoto" runat="server">
                    <asp:HiddenField ID="hfRowId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfCommand" Value="0" runat="server" />
                    <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                    <div class="row">
                        <div class="col-md-12" style="border-top: 1px dashed; border-color: #d2d6de;">
                            <div class="form-group">
                                <p style="">You Can Add Multiple/Single images using Below <i class="fa fa-plus-circle"></i>.</p>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtSquanceNo">Image Upload<span class="req-field">*</span></label>
                                <asp:HiddenField ID="hfType5PopUpImage" runat="server" />
                                <asp:FileUpload ID="fuType5PopupImage" CssClass="form-control" runat="server" ValidationGroup="slider" />
                            </div>
                        </div>                        


                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Name<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtSubName" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validationmsg" runat="server" ControlToValidate="txtSubName" ValidationGroup="slider"
                                    ErrorMessage="Enter Sub Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputFile">Description<span class="req-field">*</span></label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" ValidationGroup="Sub"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationmsg" runat="server" ControlToValidate="txtDescription" ValidationGroup="slider"
                                    ErrorMessage="Enter Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group form-control">
                                <asp:CheckBox ID="chkIsVisible" runat="server" ValidationGroup="slider" />
                                <label for="exampleInputFile">Visible</label>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnType5Save" CssClass="btn btn-primary " Text="Save Image" OnClick="btnType5Save_Click" ValidationGroup="slider" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-group" style="overflow-x: scroll;">
                                <asp:GridView ID="gvType5" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvType5_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="IsVisible" HeaderText="Is Visible" />
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemTemplate>
                                                <a id="afile" href='<%# Eval("ImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="ibtn_Type5Edit" CausesValidation="false" ToolTip="Edit" OnClick="ibtn_Type5Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ibtn_Type5Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type5Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

                <div class="col-md-4">
                    <label for="txtCastName">&nbsp;</label>
                    <asp:Button runat="server" ID="btnSubDetailsSave" CssClass="btn btn-primary " Text="Save Sub Details" OnClick="btnSubDetailsSave_Click" />
                </div>
                <div class="col-md-12">
                    <br />
                    <div class="form-group">
                        <asp:GridView ID="gvSubDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StarId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            OnRowCommand="gvSubDetails_RowCommand" AllowSorting="false">
                            <Columns>

                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="AccordTitle" HeaderText="Accord Title" />
                                <asp:BoundField DataField="TypeDetail" HeaderText="Type Detail" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Seq No." />
                                <asp:BoundField DataField="IsVisible" HeaderText="Is Visible" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                { %>
                                            <asp:LinkButton ID="lnkMenu_Edit" CausesValidation="false" ToolTip="Edit" OnClick="lnkMenu_Edit_Click" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <%}
                                                if (SessionWrapper.UserPageDetails.CanDelete)
                                                { %>
                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("AccordTitle", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            <%} %>
                                            <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                CommandArgument='<%# Eval("Id") + "," + Eval("SequanceNo") + ","+ "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-up"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                CommandArgument='<%# Eval("Id") + "," + Eval("SequanceNo") + "," +   "down" %>'
                                                runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                            <i class="fa fa-arrow-circle-down"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-4">
                    <label for="txtCastName">&nbsp;</label>
                    <% if (SessionWrapper.UserPageDetails.CanAdd)
                        { %>
                    <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " OnClientClick="return validate();" Text="Final Save" OnClick="btn_Save_Click" ValidationGroup="main" />

                    <%} %>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
