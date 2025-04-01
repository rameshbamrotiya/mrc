<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DonationListData.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Payment.DonationListData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Donation Data</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Donation Data</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Donation</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Donation List</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3"> 
                                <div class="form-group">
                                    <label for="txtStartDate">Start Date</label>
                                    <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" MaxLength="10" CssClass="form-control datepicker-demo" placeholder="Enter start date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEndDate">End Date</label>
                                    <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" MaxLength="10" CssClass="form-control datepicker-demo" placeholder="Enter end date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <button runat="server" id="btn_Search" class="btn btn-primary" onserverclick="btn_Search_ServerClick" title="Search">
                                        <i class="fa fa-search">&nbsp;Search</i>
                                    </button>
                                    <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel">
                                        <i class="fa fa-remove">&nbsp;Cancel</i>
                                    </button>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group" style="overflow-x: scroll;">
                                    <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="true" OnPageIndexChanging="gView_PageIndexChanging" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="TxnId" HeaderText="Txn.Id" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="TransactionDate" HeaderText="Txn.Date" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="PrimaryIdProof" HeaderText="Primary Proof" />
                                            <asp:BoundField DataField="PrimaryProofNo" HeaderText="Primary Proof No" />
                                            <asp:TemplateField HeaderText="Primary Proof View">
                                                <ItemTemplate>
                                                    <%# Eval("PrimaryProofFile") %>
                                                    <a id="aPrimaryProof" href='<%# string.IsNullOrWhiteSpace(Eval("PrimaryProofFile").ToString())? "": ResolveUrl(Eval("PrimaryProofFile").ToString()) %>' target="_blank" runat="server" tooltip="View File" class="btn btn-primary fa fa-eye"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SecondaryIdProof" HeaderText="Secondary Proof" />
                                            <asp:BoundField DataField="SecondaryProofNo" HeaderText="Secondary Proof No" />
                                            <asp:TemplateField HeaderText="Secondary Proof View">
                                                <ItemTemplate>
                                                    <a id="aSecondaryProof" href='<%# string.IsNullOrWhiteSpace(Eval("SecondaryProofFile").ToString())? "": ResolveUrl(Eval("SecondaryProofFile").ToString()) %>' target="_blank" runat="server" tooltip="View File" class="btn btn-primary fa fa-eye"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEmail" runat="server" CssClass="btn btn-primary gopay">Send Mail</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="input-group">
                                    <button runat="server" id="btnexport" class="btn btn-primary" visible="false" onserverclick="btnexport_ServerClick" title="Generate Excel File">
                                        <i class="fa fa-plus-square">&nbsp;Generate Excel File</i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
