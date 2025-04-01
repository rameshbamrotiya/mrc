<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="badrequest.aspx.cs" Inherits="Unmehta.WebPortal.Web.badrequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%= ResolveUrl("~/Hospital/assets/css/bootstrap.min.css")%>" />
        <script src="<%= ResolveUrl("~/Hospital/assets/js/bootstrap.min.js")%>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="page-wrap d-flex flex-row align-items-center">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-12 text-center">
                    <span class="display-1 d-block">400</span>
                    <div class="mb-4 lead">Oops! Something went wrong.</div>
                    <div class="mb-4 lead">An error occured while processing your request.</div>
                    <a href="<%=ResolveUrl("~/") %>" class="btn btn-link">Back to Home</a>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
