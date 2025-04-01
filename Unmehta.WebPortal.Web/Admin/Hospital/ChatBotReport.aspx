<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ChatBotReport.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.ChatBotReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Chat Bot Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Chat Bot Report</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Hospital</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Chat Bot Report</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
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
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" DataKeyNames="Id" EmptyDataText="Record does not exist..."
                                AllowPaging="true" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Font-Bold="true" />
                                    
                                    <asp:TemplateField HeaderText="EmailId">
                                        <ItemTemplate>
                                            <%# (!Convert.ToBoolean(Eval("IsSkipEmail"))? Eval("EmailId").ToString():"") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="YouLocation" HeaderText="Location" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" ItemStyle-Font-Bold="true" />
                                    
                                    <asp:TemplateField HeaderText="Past Medical">
                                        <ItemTemplate>
                                            <%# (!Convert.ToBoolean(Eval("IsSkipPastMedicalHistory"))? Eval("PastMedicalHistory").ToString():"") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Present Medical">
                                        <ItemTemplate>
                                            <%# (!Convert.ToBoolean(Eval("IsSkipPresentMedicalHistory"))? Eval("PresentMedicalHistory").ToString():"") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WriteQuery" HeaderText="Write Query" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString = "{0:dd/MM/yyyy}" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">                                         
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
