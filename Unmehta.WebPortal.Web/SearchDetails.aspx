<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.SearchDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <section class="content">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-main-title">
                        <h2>Search Result "<%=strSearch %>"</h2>
                    </div>
                </div>
                <%=strLists %>
                <%--<div class="col-md-6">
                        <div class="card  mb-0">
                            <div class="card-body">
                                <h5 class="card-title">BJMC AFFILIATION</h5>
                                <p>
                                    YU.N.Mehta Institute of Cardiology and Research Center has following super specialty courses in
									affiliation with
									B.J.Medical College, Ahmedabad, Gujarat University. All seats are MCI Recognized and free without
									any donation on merit
									basis through NEET.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card  mb-0">
                            <div class="card-body">
                                <h5 class="card-title">BJMC AFFILIATION</h5>
                                <p>
                                    YU.N.Mehta Institute of Cardiology and Research Center has following super specialty courses in
									affiliation with
									B.J.Medical College, Ahmedabad, Gujarat University. All seats are MCI Recognized and free without
									any donation on merit
									basis through NEET.
                                </p>
                            </div>
                        </div>
                    </div>--%>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
