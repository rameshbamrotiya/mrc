
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System;
using BAL;
using BO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using DAL;

namespace Unmehta.WebPortal.Web.Hospital.Payment
{
    public partial class Response : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string passphrase1 = "";
                string salt1 = "";
                //try
                //{
                //    string[] request = Convert.ToString(HttpContext.Current.Request["msg"]).Split('|');
                //    string strResponseMessage = "";
                //    string strResponseStatus = "";
                //    string returnUrl = "";
                //    if (BilldeskPayment.PaymentResponseFromBillDesk(request,out returnUrl, out strResponseMessage, out strResponseStatus))
                //    {
                //        string strURL = Functions.GetReturnPayment(returnUrl), strQueryString= Functions.Base64Decode(HttpUtility.UrlDecode(request[19].ToString().Replace("PP", "%")));
                //        string[] strArry;
                //        if(strURL.Contains("?"))
                //        {
                //            strArry = strURL.Split('?');
                //            strURL = strArry[0];
                //            strQueryString = strArry[1];
                //        }
                //        lblResponseMessage.Text = strResponseMessage;
                //        lblResponseStatus.Text = strResponseStatus;
                //        Response.Redirect( strURL + "?"+ HttpUtility.UrlEncode(Functions.Base64Encode( (string.IsNullOrWhiteSpace(strQueryString)? "": strQueryString + "|" )+ "Status=" + lblResponseStatus.Text+"|"+"Message="+ strResponseMessage)));
                //    }
                //}
                //catch (Exception ex)
                //{
                //    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                //}

                if (PaymentConfigDetailsValue.PaymentMode == "0")
                {

                    //MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

                    //PaymentURL = PaymentConfigDetailsValue.PaymentRequestAPIURLUAT;

                    //userId = PaymentConfigDetailsValue.PaymentRequestUserIdUAT;

                    //password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;

                    //product = PaymentConfigDetailsValue.PaymentRequestProductUAT;

                    //custAccNo = PaymentConfigDetailsValue.PaymentRequestCustAccNoUAT;

                    //passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseUAT;

                    //salt = PaymentConfigDetailsValue.PaymentRequestEncsaltUAT;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseUAT;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltUAT;

                }
                else
                {

                //    MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdLive;

                //    PaymentURL = PaymentConfigDetailsValue.PaymentRequestAPIURLLive;

                //    userId = PaymentConfigDetailsValue.PaymentRequestUserIdLive;

                //    password = PaymentConfigDetailsValue.PaymentRequestPasswordLive;

                //    product = PaymentConfigDetailsValue.PaymentRequestProductLive;

                //    custAccNo = PaymentConfigDetailsValue.PaymentRequestCustAccNoLive;

                //    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseLive;

                //    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltLive;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseLive;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltLive;
                }

                NameValueCollection nvc = Request.Form;
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;
                // string plaintext = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"payMode\":\"SL\",\"channel\":\"ECOMM\",\"api\":\"SALE\",\"stage\":1,\"platform\":\"WEB\"},\"merchDetails\":{\"merchId\":317159,\"userId\":\"\",\"password\":\"Test@123\",\"merchTxnId\":\"1234567890\",\"merchType\":\"R\",\"mccCode\":562,\"merchTxnDate\":\"2019-12-24 20:46:00\"},\"payDetails\":{\"prodDetails\":[{\"prodName\": \"NSE\",\"prodAmount\": 10.00}],\"amount\":10.00,\"surchargeAmount\":0.00,\"totalAmount\":10.00,\"custAccNo\":null,\"custAccIfsc\":null,\"clientCode\":\"12345\",\"txnCurrency\":\"INR\",\"remarks\":null,\"signature\":\"7c643bbd9418c23e972f5468377821d9f0486601e1749930816c409fddbc7beb5d2943d832b6382d3d4a8bd7755e914922fb85aa8c234210bf2993566686a46a\"},\"responseUrls\":{\"returnUrl\":\"http://172.21.21.136:9001/payment/ots/v1/merchresp\",\"cancelUrl\":null,\"notificationUrl\":null},\"payModeSpecificData\":{\"subChannel\":[\"BQ\"],\"bankDetails\":null,\"emiDetails\":null,\"multiProdDetails\":null,\"cardDetails\":null},\"extras\":{\"udf1\":null,\"udf2\":null,\"udf3\":null,\"udf4\":null,\"udf5\":null},\"custDetails\":{\"custFirstName\":null,\"custLastName\":null,\"custEmail\":\"test@gm.com\",\"custMobile\":null,\"billingInfo\":null}}} ";
                string hashAlgorithm = "SHA1";
                string encdata = nvc["encdata"];
                string Decryptval = PaymentEncDec.decrypt(encdata, passphrase1, salt1, iv, iterations);

                //   Decryptval = "{\"merchDetails\":{\"merchId\":317159,\"merchTxnId\":\"test000123\",\"merchTxnDate\":\"2021-12-03T15:24:35\"},\"payDetails\":{\"atomTxnId\":11000000174314,\"prodDetails\":[{\"prodName\":\"NSE\",\"prodAmount\":100.0}],\"amount\":100.00,\"surchargeAmount\":1.18,\"totalAmount\":101.18,\"custAccNo\":\"213232323\",\"clientCode\":\"1234\",\"txnCurrency\":\"INR\",\"signature\":\"2b12c8bfc0e3a8268eddb6f406bf4187d4d0a0064d0355446986511453922c27e38367a97fff85863d48c147a8218e9e2d5003ab121f6f61ce3914030c60caac\",\"txnInitDate\":\"2021-12-03 15:24:36\",\"txnCompleteDate\":\"2021-12-03 15:24:40\"},\"payModeSpecificData\":{\"subChannel\":[\"NB\"],\"bankDetails\":{\"otsBankId\":2001,\"bankTxnId\":\"qjUiPQ2bMQhjPXmzE1on\",\"otsBankName\":\"Atom Bank\"}},\"extras\":{\"udf1\":\"\",\"udf2\":\"\",\"udf3\":\"\",\"udf4\":\"\",\"udf5\":\"\"},\"custDetails\":{\"custEmail\":\"sagar.gopale@atomtech.in\",\"custMobile\":\"8976286911\",\"billingInfo\":{}},\"responseDetails\":{\"statusCode\":\"OTS0000\",\"message\":\"SUCCESS\",\"description\":\"TRANSACTION IS SUCCESSFUL.\"}}";
                Payresponse.Rootobject root = new Payresponse.Rootobject();
                Payresponse.Parent objectres = new Payresponse.Parent();
                objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payresponse.Parent>(Decryptval);

                //bool flagup = new PaymentBAL().UpdateAtomTxnIDForRequest(Tok_id, strMercTxnId);

                string message = objectres.payInstrument.responseDetails.message;
                string statusCode = objectres.payInstrument.responseDetails.statusCode;
                string bankTxnId = objectres.payInstrument.payModeSpecificData.bankDetails.bankTxnId;
                string atomTxnId = objectres.payInstrument.payDetails.atomTxnId;
                string txnCompleteDate = objectres.payInstrument.payDetails.txnCompleteDate;
                string amount = objectres.payInstrument.payDetails.amount;
                string description = objectres.payInstrument.responseDetails.description;

                string returnUrl = objectres.payInstrument.extras.udf1;
                string strQuerys = objectres.payInstrument.extras.udf2;
                string strStudentId = objectres.payInstrument.extras.udf3;
                string strMercTxnId = objectres.payInstrument.extras.udf4;
                string strCourseId = objectres.payInstrument.extras.udf5;

                txtatomTxnId.Text = atomTxnId;
                txtbankTxnId.Text = bankTxnId;
                txtamount.Text = amount;
                txtdate.Text = txnCompleteDate;
                txtstatusCode.Text = statusCode;
                txtmessage.Text = message;

                //Insert payment response summary in database
                APIResponsePaymentBO objBo = new APIResponsePaymentBO();
                objBo.atomTokenId = atomTxnId;
                objBo.txnStatusCode = statusCode;
                objBo.txnMessage = message;
                objBo.txnDescription = description;
                objBo.TransectionDatetime = txnCompleteDate;
                objBo.IpAddress = PaymentConfigDetailsValue.PaymentRequestIPUAT;
                objBo.Contribution_Id = bankTxnId;
                
                bool APIResponseFlag = new PaymentBAL().InsertAPIResponseRecord(objBo);

                //Insert payment response in details in database
                APIResponsePaymentDetailBO objDetBo = new APIResponsePaymentDetailBO();
                objDetBo.merchId = objectres.payInstrument.merchDetails.merchId; 
                objDetBo.merchTxnId = objectres.payInstrument.merchDetails.merchTxnId;
                objDetBo.merchTxnDate = objectres.payInstrument.merchDetails.merchTxnDate;

                objDetBo.atomTxnId = objectres.payInstrument.payDetails.atomTxnId;
                objDetBo.prodName = objectres.payInstrument.payDetails.prodDetails[0].prodName;
                objDetBo.prodAmount = objectres.payInstrument.payDetails.prodDetails[0].prodAmount;
                objDetBo.amount = objectres.payInstrument.payDetails.amount;
                objDetBo.surchargeAmount = objectres.payInstrument.payDetails.surchargeAmount;
                objDetBo.totalAmount = objectres.payInstrument.payDetails.totalAmount;
                objDetBo.custAccNo = objectres.payInstrument.payDetails.custAccNo;
                objDetBo.clientCode = objectres.payInstrument.payDetails.clientCode;
                objDetBo.txnCurrency = objectres.payInstrument.payDetails.txnCurrency;
                objDetBo.signature = objectres.payInstrument.payDetails.signature;
                objDetBo.txnInitDate = objectres.payInstrument.payDetails.txnInitDate;
                objDetBo.txnCompleteDate = objectres.payInstrument.payDetails.txnCompleteDate;

                objDetBo.subchannel = objectres.payInstrument.payModeSpecificData.subChannel[0];
                objDetBo.otsBankId = objectres.payInstrument.payModeSpecificData.bankDetails.otsBankId.ToString();
                objDetBo.authId = string.IsNullOrEmpty(objectres.payInstrument.payModeSpecificData.bankDetails.authId) ? null : objectres.payInstrument.payModeSpecificData.bankDetails.authId.ToString();
                objDetBo.bankTxnId = objectres.payInstrument.payModeSpecificData.bankDetails.bankTxnId.ToString();
                objDetBo.otsBankName = objectres.payInstrument.payModeSpecificData.bankDetails.otsBankName.ToString();

                objDetBo.cardType = string.IsNullOrEmpty(objectres.payInstrument.payModeSpecificData.bankDetails.cardType) ? null : objectres.payInstrument.payModeSpecificData.bankDetails.cardType.ToString(); 
                objDetBo.cardMaskNumber = string.IsNullOrEmpty(objectres.payInstrument.payModeSpecificData.bankDetails.cardMaskNumber) ? null : objectres.payInstrument.payModeSpecificData.bankDetails.cardMaskNumber.ToString(); 
                objDetBo.scheme = objectres.payInstrument.payModeSpecificData.bankDetails.scheme;

                objDetBo.statusCode = objectres.payInstrument.responseDetails.statusCode;
                objDetBo.message = objectres.payInstrument.responseDetails.message;
                objDetBo.description = objectres.payInstrument.responseDetails.description;
                objDetBo.TransectionDatetime = objectres.payInstrument.payDetails.txnCompleteDate;
                objDetBo.IpAddress = PaymentConfigDetailsValue.PaymentRequestIPUAT; 
                objDetBo.EntryBy = "SYSTEM";
                objDetBo.UpdateBy = "SYSTEM";
                objDetBo.Updatetime = null;

                strQuerys = "StudentId=" + strStudentId + "|" + "CourseId=" + strCourseId + "|CourseName=|Price=" + amount;

                bool APIResponsedetFlag = new PaymentBAL().InsertAPIResponseDetailRecord(objDetBo);

                string strURL = Functions.GetReturnPayment(returnUrl), strQueryString = strQuerys;
                ErrorLogger.ERROR(" strURL : ", strURL.ToString(), this);
                
                Response.Redirect(strURL + "?" + HttpUtility.UrlEncode(Functions.Base64Encode((string.IsNullOrWhiteSpace(strQuerys) ? "" : strQueryString + "|") + "Status=" + objDetBo.message + "|" + "Message=" + objDetBo.description+ "|" + "merchTxnId=" + objDetBo.merchTxnId)),false);
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Contribution.aspx", false);
        }
    }
}