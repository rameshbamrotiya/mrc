<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BannerMaster.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.BannerMaster" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Banner Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Banner Master</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Admin</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Banner Master</li>
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
                    <div class="row">
                        <div class="col-md-9" id="tblSearch">
                            <div class="form-group">
                                <div class="controls">
                                    <div class="input-group">
                                        <%--<span class="input-group-addon"><i class="fa fa-search"></i></span>--%>

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
                                    <button runat="server" id="btn_Add" class="btn btn-primary" title="Add" onserverclick="btn_Add_Click" causesvalidation="false">
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
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="banner_id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" OnRowCommand="gView_RowCommand" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="banner_rank" HeaderText="Rank" SortExpression="banner_desc" />
                                        <asp:BoundField DataField="banner_title" HeaderText="Title" SortExpression="banner_title" />
                                         <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <a id="afile" href='<%# ResolveUrl(Eval("banner_url").ToString()) %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                                    <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" OnClientClick='<%# Eval("banner_desc", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <%} %>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                    SelectCommand="[PROC_BannerMaster_Search]" SelectCommandType="StoredProcedure" FilterExpression="banner_title like '%{0}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter ControlID="txtSearch" Name="banner_title" />

                                    </FilterParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlEntry" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <asp:HiddenField ID="hfID" Value="0" runat="server" />
                                <asp:HiddenField ID="hfSubId" Value="0" runat="server" />
                                <asp:HiddenField ID="hfFilePOst" Value="" runat="server" />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Rank<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtBannerRank" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtBannerRank" CssClass="validationmsg" runat="server" ControlToValidate="txtBannerRank"  ValidationGroup="main"
                                            ErrorMessage="Banner Rank" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ID="revBannerRank" ValidationExpression="^[0-9]{0,10}$" ControlToValidate="txtBannerRank" CssClass="validationmsg" ErrorMessage="Only numeric allowed" SetFocusOnError="true" ValidationGroup="main"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Title<span class="req-field">*</span></label>
                                        <asp:TextBox ID="txtBannerTitle" runat="server" CssClass="form-control" ValidationGroup="main"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvBannerTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtBannerTitle"
                                            ErrorMessage="Banner Title" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Banner Image<span class="req-field">*</span></label>
                                        <asp:Image ID="imgddo_photo" runat="server" ImageAlign="AbsMiddle" style="width:100%;"  ValidationGroup="main"/>
                                        <asp:FileUpload ID="fuBannerImage" runat="server" CssClass="form-control"  ValidationGroup="main"/>
                                        <asp:RequiredFieldValidator ID="rfvBannerImage" CssClass="validationmsg" ControlToValidate="fuBannerImage" runat="server" Display="Dynamic" EnableClientScript="true" ErrorMessage="Banner Image"  ValidationGroup="main"/>
                                        <p class="help-block">( Only .Jpeg or .Jpg or .PNG or .png)</p>
                                        <p class="help-block">( Image should be 1500px*400px )</p>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Status<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlActiveInactive" CssClass="form-control" runat="server" Style="width: 100%" ValidationGroup="main">
                                            <asp:ListItem Value="1" Selected="True" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Description</label>
                                        <asp:TextBox ID="txtbannerdesc" runat="server" TextMode="MultiLine" Height="100" CssClass="form-control" ValidationGroup="sub"></asp:TextBox>
                                         <script type="text/javascript">
                                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                                            var editor = CKEDITOR.replace('<%=txtbannerdesc.ClientID%>', {
                                                extraPlugins: 'tableresize'
                                            });
                                        </script>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputFile">X Axis<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlXAxis" CssClass="form-control" runat="server" Style="width: 100%" ValidationGroup="sub">
                                            <asp:ListItem Value="left" Selected="True" Text="Left"></asp:ListItem>
                                            <asp:ListItem Value="right" Text="Right"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Y Axis<span class="req-field">*</span></label>
                                        <asp:DropDownList ID="ddlYAxis" CssClass="form-control" runat="server" Style="width: 100%" ValidationGroup="sub">
                                            <asp:ListItem Value="top" Selected="True" Text="Top"></asp:ListItem>
                                            <asp:ListItem Value="center" Text="Center"></asp:ListItem>
                                            <asp:ListItem Value="bottom" Text="Bottom"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                    <div class="col-md-2">
                        <br />
                        <asp:Button runat="server" ID="btnAddDoctor" CssClass="btn btn-primary " Text="Add To Desc List" OnClick="btnAddDoctor_Click" Style="margin-top: 7px;"  ValidationGroup="sub"/>
                    </div>
                    <div class="col-md-12" style="margin-left: 10px;">
                        <div class="form-group">
                            <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="StudentName" ItemStyle-Width="100%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# HttpUtility.HtmlDecode( Eval("BannerDescription").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="StudentName" HeaderText="Student Name" SortExpression="StudentName" />--%>
                                    <asp:BoundField DataField="TextXPosition" HeaderText="Text X Position" SortExpression="TextXPosition" />
                                    <asp:BoundField DataField="TextYPosition" HeaderText="Text Y Position" SortExpression="TextYPosition" />

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">

                                                <asp:LinkButton ID="ibtn_DoctorEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_DoctorEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ibtn_DoctorDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_DoctorDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>

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
                    <br />
                    <div class="row">
                        <div class="col-lg-12 BtnGrp">
                            <div class="form-group">
                                <% if (SessionWrapper.UserPageDetails.CanAdd)
                                    { %>
                                <button runat="server" id="btn_Save" class="btn btn-primary" title="Save" onserverclick="btn_Save_Click"  ValidationGroup="main">
                                    <i class="fa fa-floppy-o">&nbsp;Save</i>
                                </button>
                                <%}
                                    if (SessionWrapper.UserPageDetails.CanUpdate)
                                    { %>
                                <button runat="server" id="btn_Update" class="btn btn-primary" title="Update" onserverclick="btn_Update_Click"  ValidationGroup="main">
                                    <i class="fa fa-floppy-o">&nbsp;Update</i>
                                </button>
                                <%} %>
                                &nbsp;
                                            <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_Click" causesvalidation="false">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                            </div>
                        </div>
                    </div>
                    <%--</div>--%>
                </asp:Panel>

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
