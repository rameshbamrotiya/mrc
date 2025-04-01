<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.Payment.Request" %>

<!DOCTYPE html>
<% 
    if (Unmehta.WebPortal.Web.Common.PaymentConfigDetailsValue.PaymentMode == "0")
{ %>
 <script src="https://pgtest.atomtech.in/staticdata/ots/js/atomcheckout.js" type="text/javascript"></script>
<%}
    else { 
    %>
 <script src="https://psa.atomtech.in/staticdata/ots/js/atomcheckout.js" type="text/javascript"></script>

<% } %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function openPay(rb, merchId, custEmail, custMobile) {
            //var stggingurl = "https://staging-gil.gujarat.gov.in/unmehtapay/";
            //alert(rb + '-' + merchId + '-' + custEmail + '-' + custMobile);
            //var localurl = "http://localhost:55624/Hospital/";
            //alert("mesage:"+rb);
            const options = {
                "atomTokenId": rb,
                "merchId": merchId,
                "custEmail": custEmail,
                "custMobile": custMobile,
                "returnUrl": "<%=(Unmehta.WebPortal.Web.Common.PaymentConfigDetailsValue.PaymentMode == "0")?Unmehta.WebPortal.Web.Common.PaymentConfigDetailsValue.PaymentRequestReturnURLUAT:Unmehta.WebPortal.Web.Common.PaymentConfigDetailsValue.PaymentRequestReturnURLLive%>"
              //"returnUrl": "https://staging-gil.gujarat.gov.in/unmehtapay/Hospital/Payment/Response.aspx"
            }
            let atom = new AtomPaynetz(options, 'uat');
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:label runat="server" text="" ID="lblError"></asp:label>
    <asp:Button ID="btnback" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Back to Main Page" OnClick="btnback_Click" />
    <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-primary nextBtn btn-lg" Text="Refresh" OnClick="btnRefresh_Click" />
    </div>
    </form>
</body>
</html>

