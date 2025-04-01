<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="PaymentStatus.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Admission.PaymentStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <div class="content-details-area pt-50 pb-50">
                        <div class="row form-row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnBackToGrid" runat="server" Text="Back To Course" CssClass="btn btn-info submit-btn" OnClick="btnBackToGrid_Click"   Style="float: right" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="section-main-title">
                                    <h2>Payment Status</h2>
                                    <hr />
                                    <div class="row form-row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Course Name</label>:
                                                <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Course Fee</label>:
                                                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Payment Status</label>:
                                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
