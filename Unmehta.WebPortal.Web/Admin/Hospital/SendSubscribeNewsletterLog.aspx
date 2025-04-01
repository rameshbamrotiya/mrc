<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SendSubscribeNewsletterLog.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.SendSubscribeNewsletterLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Send Subscribe News letter Log</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Send Subscribe News letter Log</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Send Subscribe News letter Log</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <h4>Send Subscribe News letter Log</h4>
            <div class="row">
                <div class="col-md-9" id="tblSearch">
                    <div class="form-group">
                        <div class=" controls">
                            <div class="input-group">
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
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x:scroll;">
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="grdUser_PageIndexChanging" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." >
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="MailSubject" HeaderText="MailSubject" />
                                <asp:BoundField DataField="MailDescription" HeaderText="MailDescription" />
                                <asp:BoundField DataField="flag" HeaderText="Mail Status" />
                                <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                <asp:BoundField DataField="CreateDate" HeaderText="Sent Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
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
