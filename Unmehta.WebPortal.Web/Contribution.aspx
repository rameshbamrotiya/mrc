<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contribution.aspx.cs" Inherits="Unmehta.WebPortal.Web.Contribution" EnableEventValidation="false" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Donation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <!-- Preloader -->
    <div id="loading" class="icon-preloader" style="display: none">
        <div class="loader">
            <div class="animate3">
                <img src="<%= ResolveUrl("~/Hospital/assets/img/loader.gif")%>" alt="Preloader Image animate3">
            </div>
        </div>
    </div>
    <!-- End Preloader -->

    <!-- Breadcrumb -->
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Donation</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->

    <div class="content">
        <div class="container">
            <div class="row">
                <!-- Doctor Details Tab -->
                <div class="col-lg-9">
                    <div class="card">
                        <div class="donation">
                            <%=strOutSide %>
                            <%-- <p>
                                U N Mehta Institute of Cardiology &amp; Research Centre (UNMICRC) is a charitable Institute registered
									under Bombay Public
									Trusts Act, 1950 and also registered as a society under the Societies Registration Act, 1860. The
									Institute provides
									emergency related tertiary cardiac care treatment to all classes of cardiac patients particularly the
									poorest of poor
									cardiac patients.<br>
                                <br>
                                The motto of the Institute is to give new active life and not extension of life. In the cardiac
									treatment, patients get
									new active life with cost benefit ratio of 100%. Donor will get deduction under section 80G and 35
									(i)(ii) of the Income
									Tax Act,1961.
							
                            </p>--%>
                            <div class="paymentstatus">
                                <div class="row d-flex align-items-center ">
                                    <div class="col-md-3">
                                        <div class="">
                                            <%--                                    <a class="read-more" style="font-size:14px;" onclick="" href="#">Know Your Payment Status <i class="bx bx-plus"></i></a>--%>
                                            <asp:LinkButton ID="paymentlink" runat="server" OnClick="paymentlink_Click" CssClass="read-more">Know Your Payment Status</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="divContactNo1" class="col-md-3">
                                        <div class="justify-content-between d-flex">
                                            <asp:TextBox ID="txtContactNo1" Visible="false" CssClass="form-control" placeholder="Donor’s Contact No" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>

                                        </div>
                                    </div>

                                     <div id="divbtnOTP" class="col-md-2 text-center">
                                        <asp:LinkButton ID="lnkbtnOTP" Visible="false"  runat="server" CssClass="btn btn-primary gopay" OnClick="btnOTP_Click">Send OTP</asp:LinkButton>
                                    </div>
                                      <div id="divContactNo2" class="col-md-3">
                                        <div class="justify-content-between d-flex">
                                            <asp:TextBox ID="txtOpt" Visible="false" CssClass="form-control" placeholder="OTP" runat="server" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div id="divbtngo" class="col-md-1">
                                        <asp:LinkButton ID="lnkbtngo" Visible="false"  runat="server" CssClass="btn btn-primary gopay" OnClick="btnpaystatus_Click">Go</asp:LinkButton>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group text-center">
                                            <asp:Label ID="lblError1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body">
                            <ul class="nav nav-tabs nav-tabs-solid nav-tabs-rounded nav-justified">
                                <li class="nav-item"><a class="nav-link active" href="#solid-rounded-justified-tab1" data-toggle="tab">Online Donation</a>
                                </li>
                                <li class="nav-item"><a class="nav-link" href="#solid-rounded-justified-tab2" data-toggle="tab">Offline Donation</a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane show active" id="solid-rounded-justified-tab1">
                                    <div class="row form-row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtName" CssClass="form-control Namevalidation" placeholder="Name of the Donor" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Donor’s Email Address" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtContactNo" CssClass="form-control" placeholder="Donor’s Contact No" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDonationType" CssClass="form-control select" runat="server">
                                                    <asp:ListItem Text="Donation Type" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="a. Specific - For Research or Purchase of Equipment or any other" Value="a. Specific - For Research or Purchase of Equipment or any other"></asp:ListItem>
                                                    <asp:ListItem Text="b. General - For Helping Poor or Needy Patient" Value="b. General - For Helping Poor or Needy Patient"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlPrimaryIdProof" CssClass="form-control select" runat="server">
                                                    <asp:ListItem Text="Primary ID Proof" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="PAN Card" Value="PAN Card"></asp:ListItem>
                                                    <asp:ListItem Text="Aadhar Card" Value="Aadhar Card"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtPrimaryProofNo" MaxLength="12" CssClass="form-control" placeholder="Primary ID Proof Number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:FileUpload ID="fuPrimaryProof" CssClass="form-control" runat="server" />

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="btnPrimaryProof" runat="server" CssClass="btn btn-primary" OnClick="btnPrimaryProof_Click"><i class="fas fa-upload"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:HyperLink ID="aPrimaryProof" class="btn btn-primary fa fa-eye" Style="font-size: 24px" runat="server" Target="_blank"></asp:HyperLink>
                                                <asp:HiddenField ID="hfPrimaryProof" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSecondaryIdProof" CssClass="form-control select" runat="server">
                                                    <asp:ListItem Text="Secondary ID Proof" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                    <asp:ListItem Text="Election Card" Value="Election Card"></asp:ListItem>
                                                    <asp:ListItem Text="Driving License" Value="Driving License"></asp:ListItem>
                                                    <asp:ListItem Text="Ration Card" Value="Ration Card"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtSecondaryIdProof" CssClass="form-control" placeholder="Primary ID Proof Number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:FileUpload ID="fuSecondaryIdProof" CssClass="form-control" runat="server" />

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnSecondaryIdProof" runat="server" CssClass="btn btn-primary" OnClick="btnSecondaryIdProof_Click"><i class="fas fa-upload"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:HyperLink ID="aSecondaryIdProof" class="btn btn-primary fa fa-eye" Style="font-size: 24px" runat="server" Target="_blank"></asp:HyperLink>
                                                <asp:HiddenField ID="hfSecondaryIdProof" runat="server" />
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="10" placeholder="Amount of Donation" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDonerAddress" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Donor’s Communication Address" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group text-center">
                                                <asp:Label ID="lblError" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group text-center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Make Payment" OnClick="btnSubmit_Click" OnClientClick="return ShowLoader();" />
                                                <%--  <asp:Button ID="btnpaystatus" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Payment Status" OnClick="btnpaystatus_Click" />--%>
                                                <%--  <asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Admission" OnClick="btnadd_Click" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="solid-rounded-justified-tab2">
                                    <%=strOfflineDetails %>
                                    <%--<div class="inner-column">
                                        <div class="sec-title">
                                            <ul class="format-list">
                                                <li>1. Donor will contact Cash Counter at A Block Ground floor.</li>
                                                <li>2. Donor has to fill required details in available form at Cash Counter</li>
                                                <li>3. Cheque/DD issued in the favour of “U N Mehta Institute of Cardiology &amp; research Centre”
													</li>
                                                <li>4. After payment made by Donor we will issue donation receipt.</li>
                                            </ul>
                                            <p>
                                                Bank account Details for Collection of Donation, any event fees, Course fees, Fees for
													Clinical and
													Physiotherapy
													Students, Tender Fees, EMD/Security Deposit etc.
											
                                            </p>
                                            <br>
                                            Account Holder Name – U N Mehta Institute of Cardiology &amp; Research Centre<br>
                                            Bank &amp; Branch – Bank of Baroda, Girdharnagar Branch<br>
                                            Account No. 03300200000133<br>
                                            IFSC – BARB0GIRGHA (5th Character is Zero)<br>
                                            Type of Account - Current
										
                                        </div>
                                    </div>--%>
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
                                <ul class="categories nav nav-pills nav-stacked flex-column">
                                    <%=strQuickLink %>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Doctor Details Tab -->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script lang="JavaScript" type="text/javascript">

        function HideLoader() {
            $("#loading").hide();
        }

        function ShowLoader() {
            $("#loading").show();
        }

        $(document).ready(function () {

            $("#loading").hide();
            function isAlfa(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 32 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                    return false;
                }
                return true;
            }

            $(".Namevalidation").keypress(function (e) {
                var key = e.keyCode;
                if (key >= 48 && key <= 57) {
                    e.preventDefault();
                }
            });

            //$(".emailValidation").keypress(function (e) {
            //    debugger
            //    var regex = 
            //        /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            //    if (!regex.test($('.emailValidation').val())) {
            //        return false;
            //    }
            //    else {
            //        return true;
            //    }
            //});


            function isNumberKey(e) {
                var result = false;
                try {
                    var charCode = (e.which) ? e.which : e.keyCode;
                    if ((charCode > 31) && (charCode >= 48 && charCode <= 57)) {
                        result = true;
                    }
                }
                catch (err) {
                    //console.log(err);
                }
                return result;
            }

            function Validateproof(ddlprimaryprrof) {
                debugger
                var selectedValue = ddlprimaryprrof.value;
                if (selectedValue == "0") {

                }
                else {

                }
            }

            function validatePAN(pan) {
                var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
                return regex.test(pan);
            }

        });
    </script>
</asp:Content>
