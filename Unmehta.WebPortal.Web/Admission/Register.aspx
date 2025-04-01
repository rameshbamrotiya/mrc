<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.Register" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="image/x-icon" href="<%= ResolveUrl("~/Hospital/assets/img/favicon.png")%>" rel="icon" />
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Register</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="description" content="#">
    <!-- Favicon -->
    <link rel="shortcut icon" href="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/favicon.png")%>" />

    <!-- Plugin styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.css")%>" type="text/css" />

    <!-- DataTable -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/dataTables.min.css")%>" type="text/css" />

    <!-- Prism -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/prism/prism.css")%>" type="text/css" />

    <!-- Datepicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.css?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>">

    <!-- Clockpicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/clockpicker/bootstrap-clockpicker.min.css?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>" type="text/css">

    <!-- Css -->
    <link href="<%= ResolveUrl("~/Admin/Script/jquery-ui.css")%>" rel="stylesheet" type="text/css" />


    <!-- Vmap -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/jqvmap.min.css")%>" />

    <!-- App styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/app.min.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/owlcarousel.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/owlthemedefault.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~//Admin/Template/html/assets/css/customestyle.css")%>" type="text/css" />
    <style>
        .login_register .form-wrapper {
            width: 100% !important;
            max-width: 600px;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 999;
            margin: 0 auto !important;
            padding: 30px !important;
            border-top: 3px solid var(--maroon);
            border-radius: 4px !important;
        }

        .login_register label {
            text-align: left;
            display: block;
        }
        #loading{height:100%;width:100%;position:fixed;z-index:1;margin-top:0;top:0;z-index:999999;background:#FFF}#loading.icon-preloader .loader{position:absolute;width:auto;height:auto;top:50%;left:50%;transform:translate(-50%,-50%);text-align:center;background:transparent}#loading.icon-preloader .loader i:before{font-size:80px;color:#fff}
  
    </style>
</head>
<body class="register login_register form-membership">

    <!-- begin::preloader-->
    <div id="loading" class="icon-preloader">
        <div class="loader">
            <div class="animate3">
                <img src="<%= ResolveUrl("~/Hospital/assets/img/loader.gif")%>" alt="Preloader Image animate3">
            </div>
        </div>
    </div>
    <!-- end::preloader -->
    <div class="bg_slider owl-carousel carousel-nav owl-theme">
        <div>
            <img src="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/slide-img2.jpg")%>" class="d-block" alt="Slider Image">
        </div>
        <div>
            <img src="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/slide-img1.jpg")%>" class="d-block" alt="Slider Image">
        </div>
        <div>
            <img src="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/slide-img3.jpg")%>" class="d-block" alt="Slider Image">
        </div>
    </div>
    <form id="form1" runat="server">
        <div>
            <div class="form-wrapper">
                <div class="top_logo">
                    <img src="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/unm-logo.png")%>" />
                </div>
                <div id="logo" class="mb-0 mt-0">
                    <%--<img class="logo" src="/Admin/Template/html/assets/media/image/logo.png" alt="image">
                    <img class="logo-dark" src="/Admin/Template/html/assets/media/image/logo-dark.png" alt="image">--%>
                    <h3>U.N. Mehta Student Registration</h3>
                </div>
                <!-- ./ logo -->

                <!-- form -->
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Your Name</label>--%>
                            <asp:TextBox ID="txtAadharCard" runat="server" autocomplete="off" class="form-control" placeholder="Your AadharCard" ValidationGroup="Main" MaxLength="12" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Your AadharCard*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtAadharCard" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Your Name</label>--%>
                            <asp:TextBox ID="txtFirstname" runat="server" autocomplete="off" class="form-control" placeholder="Your Name" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ErrorMessage="Enter Your Name*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFirstname" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Father/Husband Name</label>--%>
                            <asp:TextBox ID="txtMiddleName" runat="server" autocomplete="off" class="form-control" placeholder="Father/Husband Name" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ErrorMessage="Enter Father/Husband Name*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtMiddleName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Surname</label>--%>
                            <asp:TextBox ID="txtLastName" runat="server" autocomplete="off" class="form-control" placeholder="Surname" ValidationGroup="Main" onkeypress="return lettersOnly(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Enter Surname*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Email</label>--%>
                            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" class="form-control" placeholder="Email" ValidationGroup="Main"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Enter Email*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Phone No</label>--%>
                            <asp:TextBox ID="txtPhoneNo" runat="server" autocomplete="off" class="form-control" placeholder="Phone No" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)" ValidationGroup="Main"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ErrorMessage="Enter Phone No*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNo" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Birth Date</label>--%>
                            <asp:TextBox ID="txtBirthDate" runat="server" autocomplete="off" class="form-control" placeholder="Birth Date" ValidationGroup="Main"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ErrorMessage="Select Birth Date*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBirthDate" ValidationGroup="Main"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Gender</label>--%>
                            <asp:DropDownList ID="ddlGender" CssClass="form-control select" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <%--<label>Marital Status</label>--%>
                            <asp:DropDownList ID="ddlMaritalStatus" CssClass="form-control select" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <asp:Image ID="imgCaptcha" runat="server" Style="float: left" />
                            <button runat="server" id="btnRun" class="btn btn-mini" causesvalidation="false" onserverclick="btnRun_ServerClick" style="float: left" title="Search">
                                <i class="fa fa-refresh"></i>
                            </button>
                        </div>
                        <div class="form-group clearfix"></div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" CssClass="form-control" placeholder="Captcha"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Enter Captcha*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha" ValidationGroup="Main"></asp:RequiredFieldValidator>
                            <%--<asp:CompareValidator ID="cvCaptcha" runat="server" ErrorMessage="Captcha Not Match" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:CompareValidator>--%>
                        </div>
                    </div>
                </div>

                <asp:Button ID="btnRegistration" runat="server" class="btn text-white signin_btn btn-block" Text="Register" ValidationGroup="Main" OnClick="btnRegistration_Click" />

                <p class="text-muted">Already have an account?</p>
                <a href="<%= ResolveUrl("~/Admission/")%>" class="register_btn btn-sm d-block text-right font-weight-bold">Log In!</a>
                <!-- ./ form -->
             <%--    <marquee width="100%" direction="left" style="    font-weight: bold;color: darkblue;font-size: 17px;margin-top: 8px;">
Due to server maintenance activity, this portal will remain down on 22-JUNE-2023 from 12:30 PM to 01:30 PM.
</marquee>--%>
            </div>
        </div>

        <!-- Plugin scripts -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.js")%>"></script>

        <!-- Chartjs -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/charts/chartjs/chart.min.js")%>"></script>

        <!-- Apex chart -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/charts/apex/apexcharts.min.js")%>"></script>

        <!-- Circle progress -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/circle-progress/circle-progress.min.js")%>"></script>

        <!-- Peity -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/charts/peity/jquery.peity.min.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/examples/charts/peity.js")%>"></script>

        <!-- Clockpicker -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/clockpicker/bootstrap-clockpicker.min.js?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>"></script>

        <!-- Datepicker -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.js?"+DateTime.Now.ToString("ddMMyyyyhhmmss"))%>"></script>

        <!-- Slick -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/slick/slick.min.js")%>"></script>


        <!-- DataTable -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/jquery.dataTables.min.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/dataTables.bootstrap4.min.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/dataTables.responsive.min.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/examples/datatable.js")%>"></script>

        <!-- Prism -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/prism/prism.js")%>"></script>

        <!-- Vamp -->
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/jquery.vmap.min.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/maps/jquery.vmap.usa.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/examples/vmap.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Script/MainPage.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Script/App.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/owl-custom.js")%>"></script>
        <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/owlcarouselmin.js")%>"></script>
        <script lang="JavaScript" type="text/javascript">

            javascript: window.history.forward(1);

        </script>

        <script lang="JavaScript" type="text/javascript">

            $(document).ready(function () {
                ClosePreloder();
                var ageLimitCalOn = document.getElementById('txtBirthDate');
                $(ageLimitCalOn).datepicker({
                    maxDate: new Date(),
                    yearRange: "1947:2013",
                    changeMonth: true,
                    changeYear: true,
                    singleDatePicker: true,
                    showDropdowns: true,
                    dateFormat: 'dd/mm/yy',
                    defaultDate: "1/1/2013"
                });

            });
        </script>
        <script>
            /*=============================================
          =    		schemes slider 	         =
      =============================================*/

            $(window).on('load', function () {
                $("#loading").delay(300).fadeOut(300);
                $("#loading-center").on('click', function () {
                    $("#loading").fadeOut(300);
                })
            })


            $(document).ready(function () {
                $('.bg_slider').owlCarousel({
                    loop: true,
                    autoplay: true,
                    slideTransition: 'linear',
                    autoplayTimeout: 3000,
                    autoplayHoverPause: true,
                    margin: 15,
                    responsiveClass: true,
                    navText: ["<i class='fa-solid fa-angle-left'></i>",
                        "<i class='fa-solid fa-angle-right'></i>"],
                    responsive: {
                        0: {
                            items: 1,
                            nav: false,
                            dots: false,
                        },
                        600: {
                            items: 1,
                            nav: false,
                            dots: false,
                        },
                        1000: {
                            items: 1,
                            nav: false,
                            dots: false,
                            loop: true
                        }
                    }
                })
            });
        </script>
    </form>
</body>
</html>
