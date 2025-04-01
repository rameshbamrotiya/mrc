<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.Default" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="image/x-icon" href="<%= ResolveUrl("~/Hospital/assets/img/favicon.png")%>" rel="icon" />
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login</title>
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

    <%-- <!-- Datepicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.css")%>">--%>

    <!-- Css -->
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <!-- Vmap -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/jqvmap.min.css")%>" />

    <!-- App styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/app.min.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/owlcarousel.css")%>" type="text/css" />
     <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/owlthemedefault.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/customestyle.css")%>" type="text/css" />
    

  
</head>
<body  class="login_register form-membership">
    <div class="bg_slider owl-carousel carousel-nav owl-theme">
        <div>
            <img src="../Admin/Template/html/assets/media/image/slide-img2.jpg" class="d-block" alt="Slider Image">
        </div>
        <div>
             <img src="../Admin/Template/html/assets/media/image/slide-img1.jpg" class="d-block" alt="Slider Image">
        </div>
        <div>
            <img src="../Admin/Template/html/assets/media/image/slide-img3.jpg" class="d-block" alt="Slider Image">
        </div>
    </div>
    <form id="form1" runat="server">

        <div class="login_content">
             <div class="container">
            <div class="row">
               
                <div class="col-lg-6 col-md-6 col-sm-12 col-12 left_block">
                    <div class="latest_info">
                        <div class="latest_info_wrap">
                            <h3 class="mb-4 imp_news">Important News/Instructions</h3>
                            <ul id="ulInstruction" runat="server">
                                <li>
                                    <i class="fa fa-angle-double-right mr-1" aria-hidden="true"></i><a href="#">Lorem Ipsum is simply dummy text of the printing and typesetting industry</a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-double-right mr-1" aria-hidden="true"></i> <a href="#">Lorem Ipsum is simply dummy text of the printing and typesetting industry</a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-double-right mr-1" aria-hidden="true"></i> <a href="#">Lorem Ipsum is simply dummy text of the printing and typesetting industry</a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-double-right mr-1" aria-hidden="true"></i> <a href="#">Lorem Ipsum is simply dummy text of the printing and typesetting industry</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                 <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                     <div class="form-wrapper">
                    <div class="top_logo">
                        <img src="../Admin/Template/html/assets/media/image/unm-logo.png" />
                    </div>
                    <!-- logo -->
                    <div id="logo" class="mb-0 mt-0">
                        <%--<img class="logo" src="/Admin/Template/html/assets/media/image/logo.png" alt="image">
                        <img class="logo-dark" src="/Admin/Template/html/assets/media/image/logo-dark.png" alt="image">--%>
                        <h3> U.N. Mehta</h3>
                    </div>
                    <!-- ./ logo -->

                   <%-- <h5>Sign in</h5>--%>

                    <!-- form -->
                        <div class="form-group">
                            <asp:TextBox ID="txtUserName" runat="server"  class="form-control" placeholder="Username"  autofocus></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" placeholder="Password"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="efvPassword"  runat="server" ErrorMessage="*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                           <asp:Image ID="imgCaptcha" runat="server" style="float:left" />      
                            <button runat="server" id="btnRun" class="btn btn-mini" CausesValidation="false" onserverclick="btnRun_ServerClick" style="float:left"  title="Search">
                                <i class="fa fa-refresh"></i>
                            </button>
                        </div>
                        <div class="form-group clearfix"></div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" class="form-control" placeholder="Captcha"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="*" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:RequiredFieldValidator>
                            <%--<asp:CompareValidator ID="cvCaptcha" runat="server" ErrorMessage="Captcha Not Match" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:CompareValidator>--%>
                        </div>
                        <asp:Button ID="btnSignIn" runat="server"  class="btn signin_btn btn-block text-white" Text="Sign in" OnClick="btnSignIn_Click" />
                   
                        <p clss="text-muted">Don't have an account?</p>
                        <a href="<%= ResolveUrl("~/Admission/ForgotPasswordaspx")%>" class="register_btn btn-sm d-block text-right">Forgot Password!</a>
                        <a href="<%= ResolveUrl("~/Admission/Register")%>" class="register_btn btn-sm d-block text-right">Register now!</a>
                         <%--
                         <div class="imp_info mt-3">
                             <div class="info_wrap d-flex align-items-center">
                                 <div class="single_item mr-4">
                                    <i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                                     <a href="#">Download Link</a>
                                 </div>
                                 <div class="single_item mr-4">
                                     <i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                                     <a href="#">Download Link</a>
                                 </div>
                                 <div class="single_item mr-4">
                                     <i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                                     <a href="#">Download Link</a>
                                 </div>
                             </div>
                         </div>
                         --%>
                    <!-- ./ form -->

                </div>
                </div>
            </div>
              <%--   <marquee width="100%" direction="left" style="    font-weight: bold;color: darkblue;font-size: 17px;margin-top: 8px;">
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

    <%--    <!-- Datepicker -->
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.js")%>"></script>--%>

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
    <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/owl-custom.js")%>"></script>
    <script src="<%= ResolveUrl("~/Admin/Template/html/assets/js/owlcarouselmin.js")%>"></script>
    <script language="JavaScript" type="text/javascript">

        javascript: window.history.forward(1);



    </script> 
        <script src="<%= ResolveUrl("~/Admin/Script/App.js")%>"></script>
        <script>
            /*=============================================
          =    		schemes slider 	         =
      =============================================*/
            $(document).ready(function () {
                $('.bg_slider').owlCarousel({
                    loop: true,
                    autoplay: true,
                    autoplayTimeout: 3000,
                    autoplayHoverPause: true,
                    margin: 15,
                    responsiveClass: true,
                    navText: ["<i class='fa-solid fa-angle-left'></i>",
                        "<i class='fa-solid fa-angle-right'></i>"],
                    responsive: {
                        0: {
                            items: 1,
                            nav: false
                        },
                        600: {
                            items: 1,
                            nav: false
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