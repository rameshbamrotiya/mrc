<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TenderDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.TenderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Tender Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <style>
        .invoice header {
            padding: 10px 0;
            margin-bottom: 20px;
            border-bottom: 1px solid #444444;
        }

        .invoice .contacts {
            margin-bottom: 20px;
        }

        .invoice-area .report ul li {
            list-style-type: none;
            display: block;
            margin-bottom: 15px;
            padding-bottom: 10px;
            display: flex;
            justify-content: space-between;
            border: 1px solid rgba(0, 0, 0, .4);
            padding: 10px;
        }

            .invoice-area .report ul li h5 {
                display: flex;
                font-weight: 500;
                color: #444444;
                margin: 0;
                align-items: center;
                font-size: 16px;
            }

        .invoice-area .report .icon-box {
            position: relative;
            display: inline-block;
            align-items: start;
            text-align: center;
            padding-left: 16px;
            flex: none;
        }

            .invoice-area .report .icon-box:before {
                position: absolute;
                content: '';
                background: #e5e5e5;
                width: 1px;
                height: 100%;
                top: 0px;
                left: 0px;
            }

        .biddata {
            text-align: center;
            padding: 20px;
            font-weight: bold;
            position: relative;
        }

            .biddata p {
                border-bottom: 1px solid rgb(189 189 189);
                display: inline-block;
                width: 11%;
                padding-top: 0;
            }

        .invoice .invoice-details {
            text-align: right;
        }

        .invoice .Note {
            margin-bottom: 10px;
        }

        .invoice .invoice-details .invoice-id {
            margin-top: 0;
            color: #444444;
        }

        .invoice main .notices {
            padding-left: 6px;
            border-left: 6px solid #3989c6;
        }

        .invoice table {
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0;
            margin-bottom: 20px;
        }

            .invoice table td,
            .invoice table th {
                padding: 15px;
                border-bottom: 1px solid #444444;
            }

            .invoice table th {
                white-space: nowrap;
                font-weight: 400;
                font-size: 16px;
                border: 1px solid rgba(0, 0, 0, .4);
            }

            .invoice table td {
                border: 1px solid rgba(0, 0, 0, .4);
            }

                .invoice table td h3 {
                    margin: 0;
                    font-weight: 500;
                    color: #444444;
                    font-size: 16px;
                }

        .invoice-to h5 {
            text-align: justify;
            color: #022E6B;
            font-weight: bold;
            font-size: 18px;
        }

        .invoice-details .date {
            line-height: 25px;
        }

        .invoice footer {
            width: 100%;
            text-align: center;
            color: #444444;
            border-top: 1px solid #aaa;
            padding: 8px 0;
        }

        @media print {
            .invoice {
                font-size: 11px !important;
                overflow: hidden !important;
            }

                .invoice footer {
                    position: absolute;
                    bottom: 10px;
                    page-break-after: always;
                }

                .invoice > div:last-child {
                    page-break-before: always;
                }
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Tender Details</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Tender Details</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">
                <div class="row">                    
                    <div class="col-md-12">
                        <div class="content_newdata" >
                        <asp:DataList ID="dtlstTenderDetails" runat="server" Width="100%" OnItemDataBound="dtlstTenderDetails_ItemDataBound">
                            <ItemTemplate>

                                <div id="invoice">
                                    <div class="invoice">
                                        <div class="card">
                                            <div class="card-header datedisp">
                                                <h4 class="card-title"><%#Eval("Title")%> </h4>
                                                <div><%#DataBinder.Eval(Container.DataItem, "PublishDates", "{0:dd/MM/yyyy}")%></div>
                                            </div>
                                            <div class="card-body">
                                                <main>

                                                    <div  id="dvDocumentDate" runat="server">
                                                    <div class="biddata">
                                                        <h3 class="widget-title">Bid Details</h3>
                                                    </div>
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td class="text-left">
                                                                    <h3>Bid End Date/Time</h3>
                                                                </td>
                                                                <td class="total"><%# DataBinder.Eval(Container.DataItem, "LastDate", "{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-left">
                                                                    <h3>Bid Opening Date/Time</h3>
                                                                </td>

                                                                <td class="total"><%# DataBinder.Eval(Container.DataItem, "OpeningDate", "{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-left">
                                                                    <h3>Date of Opening of Technical Bid</h3>
                                                                </td>

                                                                <td class="total"><%# DataBinder.Eval(Container.DataItem, "PBMeetingDate", "{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    </div>
                                                    <div id="dvDocument" runat="server">
                                                        <div class="biddata">
                                                            <h3 class="widget-title">Document</h3>
                                                        </div>
                                                        <div class="invoice-area">
                                                            <div class="report widget-item">
                                                                <ul>

                                                                    <asp:Repeater ID="dtlstDocument" runat="server">
                                                                        <ItemTemplate>

                                                                            <li>
                                                                                <h5><%#Eval("DocName")%></h5>
                                                                                <div class="icon-box">
                                                                                    <a href="<%# ResolveUrl( Eval("DocPath").ToString()) %>" target="_blank" rel="alternate noopener noreferrer">
                                                                                        <img src='<%= ResolveUrl("~/Hospital/assets/img/pdf.png")%>' alt=""></a>
                                                                                </div>
                                                                            </li>

                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br>
                                                    <div class="note">
                                                        <p><%#Eval("DocDetails")%></p>
                                                    </div>
                                                    <hr>
                                                    <div class="row contacts">
                                                        <div class="col-lg-12 col-md-12  col-xs-12 invoice-to">
                                                            <p><strong><%#Eval("Details")%></strong></p>
                                                        </div>
                                                    </div>
                                                </main>
                                                <footer>
                                                    U. N. Mehta Institute of Cardiology and Research Centre<br>
                                                    Civil Hospital, Asarwa Ahmedabad-380016<br>
                                                    E-mail: unmicrc@gmail.com&nbsp;&nbsp;
                                        Phone: +91 (079) 22684200, 22684220&nbsp;&nbsp;
                                        Fax: +91 (079) 22684200, 22684220
                                   
                                                </footer>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="toolbar hidden-print" style="display: none;">
                                    <div class="text-right">
                                        <button onclick="printDiv('invoice')" class="btn btn-success">
                                            <i class="bx bx-printer"></i>
                                            Print</button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>

                        <!-- Container -->
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
