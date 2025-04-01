<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.LogOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
	<link type="image/x-icon" href="<%= ResolveUrl("~/Hospital/assets/img/favicon.png")%>" rel="icon" />
    <title></title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&amp;display=fallback">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../plugins/fontawesome-free/css/all.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/adminlte.min.css">
</head>
<body style="background-color:gray;">
   <div class="lockscreen-wrapper col-md-12">
    <div class="card">
        <div class="card-body box box-solid">
            <div class="box-body">
                <div class="lockscreen-wrapper">
                    
                    <div class="help-block text-center">
                        <h3>You have successfully logged out!</h3>
                    </div>
                    <div class="text-center">
                        <h2><a href="<%=ResolveUrl("~/Admission/default.aspx") %>">Click here for Login</a></h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.center -->
    </div>
</body>
</html>
