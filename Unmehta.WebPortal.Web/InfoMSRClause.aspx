<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoMSRClause.aspx.cs" Inherits="Unmehta.WebPortal.Web.InfoMSRClause" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Information Under MSR Clause B.1.11
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .canvasbg {
            position: relative;
            background: #FFFFFF;
            display: inline-block;
            z-index: 999;
            width: 65px;
            height: 15px;
            top: 405px;
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
                <li>Information Under MSR Clause B.1.11</li>
            </ul>
        </div>
    </div>
    <section class="content">
        <div class="container">
            <div class="row">
                <div class="col-md-9">
                    <div class="section-main-title">
                        <h5 class="text-left" id="PageTitle" runat="server">Information under MSR Clause B.1.11 As On <%=strDate %></h5>
                    </div>

                    <%=strListOfTableDescription %>
                    <br />
                    <br />
                    <div class="dailyOPD" id="divIPD">
                        <%=strClinicalMaterialMasterDetails %>
                        <%--<div class="row">
                            <div class="col-md-12">
                                <div class=" schedule-widget mb-0">
                                    <center><b><p style="font-size:40px"><u>Clinical Material Dashboard</u></p></b>
                                        <br />
                                    <p style="font-size:20px">On date: 12.07.2022</p>
                                    
                                    <br /><p style="font-size:35px"><u>OPD</u> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<u>IPD</u></p>
                                    <br /><p style="font-size:35px">930 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;126</p>
                                    </center>
                                </div>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md-12">
                                <div class=" schedule-widget mb-0">
                                    <div class="section-main-title">
                                        <br />
                                        <h2 class="text-center">DAILY ATTENDANCE DASHBOARD As On <%=strDate2 %></h2>
                                    </div>
                                    <!-- Schedule Nav -->
                                    <div class="schedule-nav">
                                        <ul class="nav nav-tabs nav-justified">
                                            <%=strTotalHeadli %>
                                        </ul>
                                    </div>
                                    <!-- /Schedule Nav -->

                                    <!-- Schedule Content -->
                                    <div class="schedule-cont">
                                        <!-- Sunday Slot -->
                                        <div class="canvasbg"></div>
                                        <div id="chartContainer" style="height: 400px; position: relative; width: 100%;" runat="server">
                                        </div>
                                        <br />
                                        <section class="departments-wrap-layout8 bg-light-accent100">
                                            <div class="container">
                                            </div>
                                        </section>
                                        <div class="accordion-box" id="dvAttandance" runat="server">
                                            <div class="title-box">
                                                <h6>Attendance Sheets PDF Format</h6>
                                            </div>
                                            <ul class="accordion-inner">
                                                <%=strAccordHeadli %>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">Quick Links</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories">
                                    <%=strQuickLink %>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script src="<%=ResolveUrl("~/Scripts/canvasjs.min.js") %>"></script>
    <script>
        <%=strScript%>
    </script>
</asp:Content>
