<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FloorDirectory.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.FloorDirectory" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Floor Directory Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Floor Directory Master</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Floor Directory Master</li>
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
                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Language <span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage"
                                            ErrorMessage="Language Details" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfTemplateId" Value="0" runat="server" />
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Floor Name<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlFloorName" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFloorName" CssClass="validationmsg" InitialValue="-1" runat="server" ControlToValidate="ddlFloorName"
                                            ErrorMessage="Select floor." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="dlblBlock">Block Name</label>
                                        <asp:DropDownList ID="drpblock" CssClass="form-control" runat="server">
                                        </asp:DropDownList>    
                                        <asp:RequiredFieldValidator ID="rfvBlock" CssClass="validationmsg" InitialValue="-1" runat="server" ControlToValidate="drpblock"
                                            ErrorMessage="Select block." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>                                   
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div>
                                        <div class="form-group">
                                            <label for="exampleInputFile">Cell Value</label>
                                            <asp:TextBox ID="txtCellValue" runat="server" CssClass="form-control"></asp:TextBox>     
                                            <asp:RequiredFieldValidator ID="rfvCellValue" CssClass="validationmsg" runat="server" ControlToValidate="txtCellValue"
                                            ErrorMessage="Enter cell value." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div>
                                        <div class="form-group">
                                            <label for="exampleInputFile">ToolTip</label>
                                            <asp:TextBox ID="txtToolTip" runat="server" CssClass="form-control"></asp:TextBox>                                                 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <button runat="server" id="btn_Save" class="btn btn-primary" tabindex="4" title="Save" onserverclick="btn_Save_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <button runat="server" id="btn_Update" class="btn btn-primary" tabindex="5" title="Update" onserverclick="btn_Update_ServerClick">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                &nbsp;
                               <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" tabindex="6" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
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
                                        <div class="controls">
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
                                            <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_ServerClick" causesvalidation="false">
                                                <i class="fa fa-plus-square">&nbsp;Add new</i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="margin-left: 10px;">
                                    <div class="form-group">
                                        <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="FloorDirectoryId,Language_id,FloorId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" OnRowCommand="grdDetails_RowCommand" AllowPaging="true" AllowSorting="false" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FloorName" HeaderText="Floor Name" SortExpression="FloorName" />
                                                <asp:BoundField DataField="BlockName" HeaderText="Block Name" SortExpression="BlockName" />
                                                <asp:BoundField DataField="CellValue" HeaderText="Cell Value" SortExpression="CellValue" />
                                                <asp:BoundField DataField="ToolTip" HeaderText="ToolTip" SortExpression="ToolTip" />                                                
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="ibtn_Edit" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClick="ibtn_Delete_Click" OnClientClick='<%# Eval("Floorname", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                            SelectCommand="[GetFloorDirectoryDetails]" SelectCommandType="StoredProcedure" FilterExpression="Floorname like '%{0}%' OR BlockName like '%{0}%' OR CellValue like '%{0}%' ">
                                            <FilterParameters>
                                                <asp:ControlParameter ControlID="txtSearch" Name="Floorname" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="BlockName" />
                                                <asp:ControlParameter ControlID="txtSearch" Name="CellValue" />
                                            </FilterParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnCatID" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
