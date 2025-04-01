<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentStatusPage.aspx.cs" Inherits="Unmehta.WebPortal.Web.PaymentStatusPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <script>
        javascript: window.history.forward(1);


    </script>
    <script>

        function startTimer(duration, display) {
            var timer = duration, minutes, seconds;
            var end = setInterval(function () {
                minutes = parseInt(timer / 60, 10)
                seconds = parseInt(timer % 60, 10);

                minutes = minutes < 10 ? "0" + minutes : minutes;
                seconds = seconds < 10 ? "0" + seconds : seconds;

                display.textContent = minutes + ":" + seconds;

                if (--timer < 0) {
                    window.location = "<%=ResolveUrl("~/Contribution")%>";
                    clearInterval(end);
                }
            }, 1000);
        }

        window.onload = function () {
            var fiveMinutes = 5,
                display = document.querySelector('#time');
            startTimer(fiveMinutes, display);
        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <!-- Basic Information -->
            <div class="col-md-8">
                <div class="content-details-area pt-50 pb-50">
                    <div class="section-main-title text-center">
                        <h2>Payment Status</h2>
                        <hr />
                        <div class="row text-left">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality"><b>Name :</b></label>
                                                <asp:Label ID="lblPersonName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality"><b>Reference Id :</b></label>
                                                  <asp:Label ID="lblTxnid" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row text-left">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality"><b>Amount :</b></label>
                                                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddlOtherSpeciality"><b>Payment Status :</b></label>
                                                <span id="lblMessage" runat="server"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
