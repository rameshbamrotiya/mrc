using System;
using System.Linq;
using System.Web;
using Unmehta.WebPortal.Web.Common;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using BAL;
using BO;
using System.Web.UI;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.Hospital.Payment
{
    public partial class Request : System.Web.UI.Page
    {
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strPrice;
        //public static string strReturnUrl;
        //public static string strQuerys;

        //public static string strStudentId;
        //public static string strCourseId;
        //public static string CourseName;
        //public static string strMercTxnId;
        //public static string strAmount;
        //public static string strEmail;
        //public static string strMobile;
        //public static string strReturnUrl;
        //public static string strQuerys;
        //public static string MerchantID = string.Empty;
        string Tok_id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string strStudentId;
                    string strCourseId;
                    string CourseName;
                    string strMercTxnId;
                    string strAmount;
                    string strEmail;
                    string strMobile;
                    string strReturnUrl;
                    string strQuerys;
                    string MerchantID = string.Empty;
                    //    string strGenerated = BilldeskPayment.PaymentRedirectToBillDesk(strURL, strPrice, (ConfigDetailsValue.BillDeskPaymentMode=="0"? "Test": "Live")+" From Website", out id, out strError, "UnMehata", strReturnUrl, strQuerys);
                    //    if (string.IsNullOrWhiteSpace(strError) || strError == "Record Inserted Successfully")
                    //    {
                    //        Response.Write(strGenerated);
                    //    }
                    //    else
                    //    {
                    //        lblError.Text = strError;
                    //    }

                    string strEndQueryString = Request.QueryString.ToString();
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strMercTxnId = strQuery[2].ToString().Replace("MercTxnId=", "");
                    strAmount = strQuery[3].ToString().Replace("Price=", "");
                    strEmail = strQuery[4].ToString().Replace("Email=", "");
                    strMobile = strQuery[5].ToString().Replace("Mobile=", "");
                    strReturnUrl = strQuery[6].ToString().Replace("ReturnUrl=", "");
                    strQuerys = strQuery[7].ToString().Replace("Query=", "");
                    if (strQuery.Count() > 8)
                    {
                        CourseName = strQuery[8].ToString().Replace("CourseName=", "");
                    }

                    if (PaymentConfigDetailsValue.PaymentMode == "0")
                    {
                        MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;
                    }
                    else
                    {
                        MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdLive;
                    }

                    ErrorLogger.ERROR(" before_payment error: ", strQuerys.ToString(), this);
                    string strURL = (Request.Url.OriginalString).Replace(Request.RawUrl.ToString(), "").Replace(".aspx", "") + ResolveUrl(PaymentConfigDetailsValue.BillDeskReturnURL), strError = "";
                    ErrorLogger.ERROR(" strURL: ", strURL.ToString(), this);
                    string Tok_id = PaymentRedirectToATOM(strURL, strAmount, (PaymentConfigDetailsValue.PaymentMode == "0" ? "Test" : "Live") + " From Website", out strError, "UnMehata", strReturnUrl, strQuerys);
                    ErrorLogger.ERROR(" Tok_id: ", Tok_id.ToString(), this);

                    if (Tok_id != string.Empty)
                    {
                        bool flagup = new PaymentBAL().UpdateAtomTxnIDForRequest(Tok_id, strMercTxnId);
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "openPay('" + Convert.ToString(Tok_id) + "','" + MerchantID + "','" + strEmail + "', '" + strMobile + "');", true);
                }

                catch (Exception ex)
                {
                    ErrorLogger.ERROR("payment error: " + ex.Message.ToString(), ex.StackTrace.ToString(), this);
                    //Response.Redirect("~/Admission/Defult.aspx");
                }

                //try
                //{
                //    string strEndQueryString = Request.QueryString.ToString();
                //    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                //    string[] strQuery = strQueryString.Split('|').ToArray();

                //    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                //    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                //    strPrice = strQuery[2].ToString().Replace("Price=", "");
                //    strReturnUrl = strQuery[3].ToString().Replace("ReturnUrl=", "");
                //    strQuerys = strQuery[4].ToString().Replace("Query=", "");
                //    //strReturnUrl = "";
                //    //strReturnUrl = HttpUtility.UrlEncode(Functions.Base64Encode("ReturnUrl=" + strReturnUrl));
                //    //string strURL = (Request.Url.OriginalString).Replace(Request.RawUrl.ToString(), "").Replace(".aspx", "") + ConfigDetailsValue.BillDeskReturnURL, strError = "";
                //    string strURL = (Request.Url.OriginalString).Replace(Request.RawUrl.ToString(), "").Replace(".aspx", "") + ResolveUrl(ConfigDetailsValue.BillDeskReturnURL), strError = "";
                //    long id = 0;
                //    string strGenerated = BilldeskPayment.PaymentRedirectToBillDesk(strURL, strPrice, (ConfigDetailsValue.BillDeskPaymentMode=="0"? "Test": "Live")+" From Website", out id, out strError, "UnMehata", strReturnUrl, strQuerys);
                //    if (string.IsNullOrWhiteSpace(strError) || strError == "Record Inserted Successfully")
                //    {
                //        Response.Write(strGenerated);
                //    }
                //    else
                //    {
                //        lblError.Text = strError;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                //    Response.Redirect("~/Admission/Defult.aspx");
                //}
            }
        }

        private string PaymentRedirectToATOM(string ReturnURL, string feeAmount, string TransactionFor, out string strError, string ProjectName = "UNMehta", string ProjectModule = "Direct Test", string PaymentReason = " Test Page")
        {

            #region ATOM Data Declaration


            string strStudentId;
            string strCourseId;
            string CourseName;
            string strMercTxnId;
            string strAmount;
            string strEmail;
            string strMobile;
            string strReturnUrl;
            string strQuerys;
            string MerchantID = string.Empty;

            string strEndQueryString = Request.QueryString.ToString();
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            strMercTxnId = strQuery[2].ToString().Replace("MercTxnId=", "");
            strAmount = strQuery[3].ToString().Replace("Price=", "");
            strEmail = strQuery[4].ToString().Replace("Email=", "");
            strMobile = strQuery[5].ToString().Replace("Mobile=", "");
            strReturnUrl = strQuery[6].ToString().Replace("ReturnUrl=", "");
            strQuerys = strQuery[7].ToString().Replace("Query=", "");
            if (strQuery.Count() > 8)
            {
                CourseName = strQuery[8].ToString().Replace("CourseName=", "");
            }

            strError = string.Empty;
            string version = string.Empty;
            string api = string.Empty;
            string platform = string.Empty;

            string userId = string.Empty;
            string password = string.Empty;
            string merchTxnDate = string.Empty;
            string merchTxnId = string.Empty;
            string amount = string.Empty;
            string product = string.Empty;
            string custAccNo = string.Empty;
            string txnCurrency = string.Empty;
            string custEmail = string.Empty;
            string custMobile = string.Empty;
            string udf1 = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;
            string PaymentURL = string.Empty;
            string passphrase = string.Empty;
            string salt = string.Empty;
            string passphrase1 = string.Empty;
            string salt1 = string.Empty;
            #endregion

            #region Get Payment Details
            try
            {
                if (PaymentConfigDetailsValue.PaymentMode == "0")
                {

                    MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

                    PaymentURL = PaymentConfigDetailsValue.PaymentRequestAPIURLUAT;

                    userId = PaymentConfigDetailsValue.PaymentRequestUserIdUAT;

                    password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;

                    product = PaymentConfigDetailsValue.PaymentRequestProductUAT;

                    custAccNo = PaymentConfigDetailsValue.PaymentRequestCustAccNoUAT;

                    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseUAT;

                    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltUAT;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseUAT;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltUAT;

                }
                else
                {

                    MerchantID = PaymentConfigDetailsValue.PaymentRequestMerchIdLive;

                    PaymentURL = PaymentConfigDetailsValue.PaymentRequestAPIURLLive;

                    userId = PaymentConfigDetailsValue.PaymentRequestUserIdLive;

                    password = PaymentConfigDetailsValue.PaymentRequestPasswordLive;

                    product = PaymentConfigDetailsValue.PaymentRequestProductLive;

                    custAccNo = PaymentConfigDetailsValue.PaymentRequestCustAccNoLive;

                    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseLive;

                    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltLive;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseLive;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltLive;
                }

                #endregion

                #region Validate Replace
                //if (string.IsNullOrWhiteSpace(userId))
                //{
                //    userId = "NA";
                //}
                #endregion

                #region Set ATOM Param Data
                Payrequest.RootObject rt = new Payrequest.RootObject();
                Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
                Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
                Payrequest.MerchDetails md = new Payrequest.MerchDetails();
                Payrequest.PayDetails pd = new Payrequest.PayDetails();
                Payrequest.CustDetails cd = new Payrequest.CustDetails();
                Payrequest.Extras ex = new Payrequest.Extras();

                Payrequest.Payrequest pr = new Payrequest.Payrequest();

                hd.version = PaymentConfigDetailsValue.PaymentRequestVersion;
                hd.api = PaymentConfigDetailsValue.PaymentRequestAPI;
                hd.platform = PaymentConfigDetailsValue.PaymentRequestPlatform;

                md.merchId = MerchantID;
                md.userId = userId;
                md.password = password;
                md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // "2021-09-04 20:46:00";
                md.merchTxnId = strMercTxnId;


                pd.amount = strAmount;
                pd.product = product;
                pd.custAccNo = custAccNo;
                pd.txnCurrency = PaymentConfigDetailsValue.PaymentRequestTxnCurrency;

                cd.custEmail = strEmail;
                cd.custMobile = strMobile;

                //ex.udf1 = strReturnUrl;
                //ex.udf2 = strQuerys;
                //ex.udf3 = strStudentId;
                //ex.udf4 = strMercTxnId;
                //ex.udf5 = strCourseId;

                ex.udf1 = strReturnUrl;
                ex.udf2 = "";
                ex.udf3 = strStudentId;
                ex.udf4 = strMercTxnId;
                ex.udf5 = strCourseId;




                pr.headDetails = hd;
                pr.merchDetails = md;
                pr.payDetails = pd;
                pr.custDetails = cd;
                pr.extras = ex;

                rt.payInstrument = pr;
                var json = new JavaScriptSerializer().Serialize(rt);

                //ReturnURL = ConfigurationManager.AppSettings["PaymentPath"] + "PaymentResponse.aspx";
                #endregion

                #region Save Payment gateway Request API data

                //Insert API Request in database
                APIRequestPaymentBO objBo = new APIRequestPaymentBO();
                objBo.version = hd.version;
                objBo.api = hd.api;
                objBo.platform = hd.platform;
                objBo.merchId = md.merchId;
                objBo.userId = md.userId;
                objBo.password = md.password;
                objBo.merchTxnId = md.merchTxnId;
                objBo.merchTxnDate = md.merchTxnDate;
                objBo.amount = pd.amount;
                objBo.product = pd.product;
                objBo.custAccNo = pd.custAccNo;
                objBo.txnCurrency = pd.txnCurrency;
                objBo.custEmail = cd.custEmail;
                objBo.custMobile = cd.custMobile;
                objBo.udf1 = ex.udf1;
                objBo.udf2 = ex.udf2;
                objBo.udf3 = ex.udf3;
                objBo.udf4 = ex.udf4;
                objBo.udf5 = ex.udf5;
                objBo.Remarks = PaymentConfigDetailsValue.PaymentRequestRemarkUAT;
                objBo.TransectionDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // "2021-09-04 20:46:00";
                objBo.IpAddress = PaymentConfigDetailsValue.PaymentRequestIPUAT;
                objBo.Contribution_Id = PaymentConfigDetailsValue.PaymentRequestContIDUAT;


                bool APIRequestFlag = new PaymentBAL().InsertAPIRequestRecord(objBo);


                #endregion

                #region Post to ATOM Payment Gateway URL

                //      string passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseUAT;
                //      string salt = PaymentConfigDetailsValue.PaymentRequestEncsaltUAT;
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                // string plaintext = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"payMode\":\"SL\",\"channel\":\"ECOMM\",\"api\":\"SALE\",\"stage\":1,\"platform\":\"WEB\"},\"merchDetails\":{\"merchId\":317159,\"userId\":\"\",\"password\":\"Test@123\",\"merchTxnId\":\"1234567890\",\"merchType\":\"R\",\"mccCode\":562,\"merchTxnDate\":\"2019-12-24 20:46:00\"},\"payDetails\":{\"prodDetails\":[{\"prodName\": \"NSE\",\"prodAmount\": 10.00}],\"amount\":10.00,\"surchargeAmount\":0.00,\"totalAmount\":10.00,\"custAccNo\":null,\"custAccIfsc\":null,\"clientCode\":\"12345\",\"txnCurrency\":\"INR\",\"remarks\":null,\"signature\":\"7c643bbd9418c23e972f5468377821d9f0486601e1749930816c409fddbc7beb5d2943d832b6382d3d4a8bd7755e914922fb85aa8c234210bf2993566686a46a\"},\"responseUrls\":{\"returnUrl\":\"http://172.21.21.136:9001/payment/ots/v1/merchresp\",\"cancelUrl\":null,\"notificationUrl\":null},\"payModeSpecificData\":{\"subChannel\":[\"BQ\"],\"bankDetails\":null,\"emiDetails\":null,\"multiProdDetails\":null,\"cardDetails\":null},\"extras\":{\"udf1\":null,\"udf2\":null,\"udf3\":null,\"udf4\":null,\"udf5\":null},\"custDetails\":{\"custFirstName\":null,\"custLastName\":null,\"custEmail\":\"test@gm.com\",\"custMobile\":null,\"billingInfo\":null}}} ";
                string Encryptval = PaymentEncDec.Encrypt(json, passphrase, salt, iv, iterations);

                ErrorLogger.ERROR(" json : ", json.ToString(), this);
                ErrorLogger.ERROR(" jsonEnc : ", Encryptval.ToString(), this);

                //string testurleq =  "https://caller.atomtech.in/ots/aipay/auth?merchId=317159&encData=" + Encryptval;

                string testurleq = PaymentURL + "merchId=" + MerchantID + "&encData=" + Encryptval;
                ErrorLogger.Info("request_:" + testurleq, this);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
                ErrorLogger.ERROR(" jsontesturleq : ", testurleq.ToString(), this);

                //var myproxy = new WebProxy
                //{
                //    Address = new Uri($"http://10.10.2.248:8080"),
                //    BypassProxyOnLocal = false,
                //    UseDefaultCredentials = false,
                //};

                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //myproxy.BypassProxyOnLocal = false;
                //request.Proxy = myproxy;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                try
                {
                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(json);
                    request.ProtocolVersion = HttpVersion.Version11;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                catch (Exception exx)
                {
                    ErrorLogger.ERROR(" Request : ", exx.ToString(), this);
                    throw;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jsonresponse = response.ToString();
                ErrorLogger.ERROR(" jsonresponse : ", jsonresponse.ToString(), this);


                bool flagup = new PaymentBAL().UpdateAPIRequestError(1, "1_" + response.ToString(), md.merchTxnId);
                ErrorLogger.Info("jsonresponse" + jsonresponse, this);
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                flagup = new PaymentBAL().UpdateAPIRequestError(1, "2_" + jsonresponse.ToString(), md.merchTxnId);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");
                //// var result = "{\"initiateDigiOrderResponse\":{ \"msgHdr\":{ \"rslt\":\"OK\"},\"msgBdy\":{ \"sts\":\"ACPT\",\"txnId\":\"DIG2019039816365405440004\"}}}";

                // flagup = new PaymentBAL().UpdateAPIRequestError(1, result, md.merchTxnId);

                var uri = new Uri("http://atom.in?" + result);
                var query = HttpUtility.ParseQueryString(uri.Query);

                string encData = query.Get("encData");

                ErrorLogger.ERROR(" jsonresponseencData : ", encData.ToString(), this);

                // string  encData="5500FEA2F09DA7EF128CFE7D2D01F2533B8D8211ACDCEEE850A7943CF46D4A18FF153971B83983A1EBF8B48F36315222E33FED142A05BE8FD890492ED759983B173801C801A79B390C17E01354CA0752087CF1E71316E5F442FADA985C46B06DB8462928DB18BC8E7714EC6128340CB8690A185F590E47658C293FA2E73ADC77899D6E7B119E17005E625CF2258A6A74363EAA59A43FF785505A77D163DA232B1D2250C4A1A1C755E10D5991A2DB5B3C";
                //string passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseUAT;
                //string salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltUAT;

                string Decryptval = PaymentEncDec.decrypt(encData, passphrase1, salt1, iv, iterations);
                ErrorLogger.ERROR(" jsonresponse Decryptval : ", Decryptval.ToString(), this);

                flagup = new PaymentBAL().UpdateAPIRequestError(1, "3_" + Decryptval.ToString(), md.merchTxnId);

                //{ "atomTokenId":15000000085830,"responseDetails":{ "txnStatusCode":"OTS0000","txnMessage":"SUCCESS","txnDescription":"ATOM TOKEN ID HAS BEEN GENERATED SUCCESSFULLY"} }
                Payverify.Payverify objectres = new Payverify.Payverify();
                objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
                string txnMessage = objectres.responseDetails.txnMessage;
                flagup = new PaymentBAL().UpdateAPIRequestError(1, "4_" + objectres.ToString(), md.merchTxnId);

                flagup = new PaymentBAL().UpdateAPIRequestError(1, "5_" + objectres.atomTokenId.ToString(), md.merchTxnId);

                ErrorLogger.ERROR(" jsonresponse Decryptval : ", objectres.ToString(), this);


                return objectres.atomTokenId;


            }
            catch (Exception Ex)
            {
                bool flagup = new PaymentBAL().UpdateAPIRequestError(1, "Ex_" + Ex.ToString(), merchTxnId);

                strError = "Error while calling API " + Ex;
                return strError;
            }


            #endregion

        }

        protected void btnback_Click(object sender, EventArgs e)
        {

            string strStudentId;
            string strCourseId;
            string CourseName = "";
            string strMercTxnId;
            string strAmount;
            string strEmail;
            string strMobile;
            string strReturnUrl;
            string strQuerys;
            string MerchantID = string.Empty;

            string strEndQueryString = Request.QueryString.ToString();
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            strMercTxnId = strQuery[2].ToString().Replace("MercTxnId=", "");
            strAmount = strQuery[3].ToString().Replace("Price=", "");
            strEmail = strQuery[4].ToString().Replace("Email=", "");
            strMobile = strQuery[5].ToString().Replace("Mobile=", "");
            strReturnUrl = strQuery[6].ToString().Replace("ReturnUrl=", "");
            strQuerys = strQuery[7].ToString().Replace("Query=", "");
            if (strQuery.Count() > 8)
            {
                CourseName = strQuery[8].ToString().Replace("CourseName=", "");
            }

            strQuerys = "StudentId=" + strStudentId + "|" + "CourseId=" + strCourseId + "|CourseName=" + CourseName + "|Price=" + strAmount;

            string strURL = Functions.GetReturnPayment(strReturnUrl);
            strQueryString = strQuerys;
            ErrorLogger.ERROR(" strURL : ", strURL.ToString(), this);

            Response.Redirect(strURL + "?" + HttpUtility.UrlEncode(Functions.Base64Encode((string.IsNullOrWhiteSpace(strQuerys) ? "" : strQueryString + "|") + "Status= Failed By User |" + "Message= Failed By User|" + "merchTxnId=" + MerchantID)));

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}