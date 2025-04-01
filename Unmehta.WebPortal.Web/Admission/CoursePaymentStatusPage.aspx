<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/Student.Master" AutoEventWireup="true" CodeBehind="CoursePaymentStatusPage.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.CoursePaymentStatusPage" %>

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
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="section-main-title">
                                    <h2>Payment Status</h2>
                                    <hr />
                                    <div class="row form-row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Course Name</label>:
                                                <%=strCourseName %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Course Fee</label>:
                                                <%=strPrice %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label for="ddlOtherSpeciality">Payment Status</label>:
                                                <%=strMessage %>
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
