<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentStatus.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.Payment.PaymentStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .maintable tbody tr th {
            border-bottom: 1px solid rgba(0,0,0,.03);
            background: #273c66;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="contact-area register-card-body pt-50 pb-70">
        <div class="container">
            <div class="row justify-content-md-center">
                <div class="col-lg-12">
                    <section class="content">
                        <div class="card">
                            <div class="section-main-title" style="background-color: #c20e3e; text-align: center;">
                                <h2 style="margin-top: 10px; color: white;">Payment Details</h2>
                                <h3>
                                    <asp:Label ID="lblPostName" runat="server" Style="text-align: right !important; color: red;"></asp:Label>
                                </h3>
                            </div>
                            <div class="card-body">
                                <!-- Bootstrap alert -->
                                <div class="table-responsive">
                                    <div class="form-group">
                                        <asp:GridView ID="grdpayView" OnRowDataBound="grdpayView_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover maintable  table-striped" EmptyDataText="Record does not exist..."
                                            DataSourceID="sqlds" AllowPaging="false" AllowSorting="false" PageSize="1000">
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                                <asp:BoundField DataField="TxnId" HeaderText="Transaction Id" />
                                                <asp:BoundField DataField="Amount" HeaderText="Donation Amount" />
                                                <asp:BoundField DataField="TxnDatetime" HeaderText="Txn. Date" />

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnpaystatus" runat="server" Value='<%# Bind("Status") %>' />
                                                        <asp:HiddenField ID="hdnatomTxnId" runat="server" Value='<%# Bind("TxnId") %>' />
                                                        <asp:Label Visible="false" ID="lblpaystatus" ForeColor="#009900" runat="server">SUCCESS</asp:Label>
                                                        <asp:LinkButton Visible="false" ID="lnkpaystatus" ForeColor="#ff0000" OnClick="lnkpaystatus_Click" CssClass='btn btn-sm' runat="server">FAIL</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--     <asp:TemplateField HeaderText="Refund">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkrefund" ForeColor="#000099" OnClick="lnkrefund_Click" CssClass='btn btn-sm' runat="server">REFUND</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <h6>
                                            <center>
                                                <asp:Label ID="lblStatusDiscription" Text="" runat="server" Style="text-align: right !important; color: red;"></asp:Label>
                                            </center>
                                            
                                        </h6>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
