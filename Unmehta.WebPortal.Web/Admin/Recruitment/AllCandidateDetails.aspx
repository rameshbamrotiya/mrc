<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AllCandidateDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.AllCandidateDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Candidate List As Per Advertisement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Advertisement as Candidate List </h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Advertisement as Candidate List </li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">

            <div class="row">
                <div class="col-md-12" id="tblSearch" style="margin-bottom: 10px;">
                    <div class="form-group">
                        <div class="col-md-3 controls">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 controls">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlJobList" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 controls">
                            <div class="form-group">
                                <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
                                    <i class="fa fa-search">&nbsp;Search</i>
                                </button>
                                <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" /><hr />
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." >
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RegistarionId">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_View_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus fa-lg"></i><%# Eval("RegisrationId") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PostName" HeaderText="Applied Post" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                                <asp:BoundField DataField="DateOfBirth" HeaderText="Date Of Birth" />
                                <asp:BoundField DataField="CasteName" HeaderText="Caste Name" />
                                <asp:BoundField DataField="ReligionName" HeaderText="Religion Name" />
                                <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                <asp:BoundField DataField="ParmenentAddress" HeaderText="Permanent Address" />
                                <asp:BoundField DataField="Mobile" HeaderText="Contact No" />
                                <%--<asp:BoundField DataField="PhotographName" HeaderText="Photograph Name" SortExpression="Doc_Name" />--%>
                                <asp:TemplateField HeaderText="Photograph Name">
                                    <ItemTemplate>
                                        <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("PhotographName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="SignatureName" HeaderText="Signature Name" SortExpression="Doc_Name" />--%>
                                <asp:TemplateField HeaderText="Signature Name">
                                    <ItemTemplate>
                                        <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("SignatureName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Form">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <%--<asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkDownload" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="lnkDownload_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUnlockProfile" CommandName="eView" runat="server" data-original-title="View" OnClientClick='<%# Eval("RegisrationId", "return confirm(\"Are you sure want to Unlock : {0} ? \")") %>' CssClass="btn btn-sm show-tooltip" OnClick="lnkUnlockProfile_Click"><i class="fa fa-key"></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" OnClientClick='<%# Eval("Id", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>--%>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
