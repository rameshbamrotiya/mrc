<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogInPortal.aspx.cs" Inherits="Unmehta.WebPortal.Web.LogInPortal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="description" content="#">
    <!-- Favicon -->
    <link rel="shortcut icon" href="<%= ResolveUrl("~/Admin/Template/html/assets/media/image/favicon.png")%>" />

    <!-- Plugin styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.css")%>" type="text/css">

    <!-- DataTable -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/dataTable/dataTables.min.css")%>" type="text/css">

    <!-- Prism -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/prism/prism.css")%>" type="text/css">

    <%-- <!-- Datepicker -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/datepicker/daterangepicker.css")%>">--%>

    <!-- Css -->
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <!-- Vmap -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/vmap/jqvmap.min.css")%>">

    <!-- App styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/assets/css/app.min.css")%>" type="text/css">



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
    <script language="JavaScript" type="text/javascript">

        javascript: window.history.forward(1);

    </script>
    <style>
        body.form-membership .form-wrapper {
            width: 30%;
        }
    </style>
</head>
<body class="form-membership">
    <form id="form1" runat="server">
        <div>
            <div class="form-wrapper">

                <!-- logo -->
                <div id="logo">
                    <%--<img class="logo" src="/Admin/Template/html/assets/media/image/logo.png" alt="image">
                    <img class="logo-dark" src="/Admin/Template/html/assets/media/image/logo-dark.png" alt="image">--%>
                    <h3>U.N. Mehta</h3>
                </div>
                <!-- ./ logo -->

                <h5>Sign in</h5>

                <!-- form -->
                <div class="form-group">
                    <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username" autofocus></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Please enter user name." Display="Dynamic" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="efvPassword" runat="server" ErrorMessage="Please enter password." Display="Dynamic" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Image ID="imgCaptcha" runat="server" Style="float: left" />

                    <button runat="server" id="btnRun" class="btn btn-mini" causesvalidation="false" onserverclick="btnRun_ServerClick" style="float: left" title="Refresh">
                        <i class="fa fa-refresh"></i>

                    </button>
                </div>
                <div class="form-group clearfix"></div>
                <div class="form-group">
                    <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" class="form-control" placeholder="Captcha"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Please enter captcha." Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:RequiredFieldValidator>
                    <%--<asp:CompareValidator ID="cvCaptcha" runat="server" ErrorMessage="Captcha Not Match" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCaptcha"></asp:CompareValidator>--%>
                </div>
                <asp:Button ID="btnSignIn" runat="server" class="btn btn-primary btn-block" Text="Sign in" OnClick="btnSignIn_Click" />

                <%--  <p class="text-muted">Don't have an account?</p>
                    <a href="./register.html" class="btn btn-outline-light btn-sm">Register now!</a>--%>
                <!-- ./ form -->

            </div>
        </div>

        <script src="<%= ResolveUrl("~/Admin/Script/App.js")%>"></script>

    </form>
</body>
</html>
