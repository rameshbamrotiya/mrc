<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.Payment.PaymentList" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function close() {
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="grdpayView" OnRowDataBound="grdpayView_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped table-responsive" EmptyDataText="Record does not exist..."
                                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                                    <Columns>
                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                                        <asp:BoundField DataField="TxnId" HeaderText="Tnx. Id" />
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
                                                        <asp:TemplateField HeaderText="Refund">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkrefund" ForeColor="#000099" OnClick="lnkrefund_Click" CssClass='btn btn-sm' runat="server">REFUND</asp:LinkButton>
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
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

