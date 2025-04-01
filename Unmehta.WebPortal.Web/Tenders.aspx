<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tenders.aspx.cs" Inherits="Unmehta.WebPortal.Web.Tenders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Tenders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">

    <style>
        .maintable div div table thead tr th {
            border-bottom: 1px solid rgba(0, 0, 0, 0.03);
            background: #273c66;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li><a href="#">Tenders</a></li>
            </ul>
        </div>
    </div>
    <section class="content">
        <div class="container">
            <div class="row" id="Msg" runat="server">
                <%=StringBuilderMsg%>
            </div>
            <div class="row" id="Details" runat="server">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row mb-20">
                                <div class="table-responsive maintable">
                                    <div>
                                        <asp:GridView ID="gvTenderList" CssClass="table table-striped table-bordered" CellSpacing="0" rules="all" border="1" AutoGenerateColumns="False" Style="border-collapse: collapse;" runat="server" EmptyDataText="There is no active tenders in process." OnRowDataBound="gvTenderList_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Announcements of Bids">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title").ToString() %>'></asp:Label>
                                                        <asp:Label ID="lblIcon" runat="server" Text='<%# ((Eval("IsNewIcon").ToString()=="1")?"<img src=\""+ ResolveUrl("~/Hospital/assets/img/new_blink.gif")+"\" />":"" ) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PublishDate" HeaderText="Publish Date" SortExpression="PublishDate" />
                                                <asp:BoundField DataField="PBMeetingDate" HeaderText="Pre Bid Meeting" SortExpression="PBMeetingDate" />
                                                <asp:BoundField DataField="LastDate" HeaderText="Last Date of Submission" SortExpression="LastDate" />
                                                <asp:BoundField DataField="OpeningDate" HeaderText="Opening of Bid" SortExpression="OpeningDate" />
                                                <asp:TemplateField HeaderText="More">
                                                    <ItemTemplate>
                                                        <a href='<%# ResolveUrl( "~/TenderDetails?"+Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("TenderID").ToString())) %>' target="_blank" class="innercontent_text_green">Click </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
