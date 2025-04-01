using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using System.Data;
using BAL;
using BO;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Hospital.Payment
{
    public class HmacSha512
    {
        private readonly HMac _hmac;

        public HmacSha512(byte[] key)
        {
            _hmac = new HMac(new Sha256Digest());
            _hmac.Init(new KeyParameter(key));
        }

        public byte[] ComputeHash(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");

            byte[] resBuf = new byte[_hmac.GetMacSize()];
            _hmac.BlockUpdate(value, 0, value.Length);
            _hmac.DoFinal(resBuf, 0);

            return resBuf;
        }
    }
    public partial class PaymentStatus : System.Web.UI.Page
    {
        public static string strBoard;

        public static string strHeaderImage;

        public static string strOutSide;

        public static string strOfflineDetails;

        public static string strQuickLink;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    strHeaderImage = GetHeaderImage();
                    strOutSide = GetDataDetails();
                    strQuickLink = Functions.CreateQuickLink("Cares", "Contribution");
                    PaymentBAL objBAL = new PaymentBAL();
                    lblStatusDiscription.Text = "";
                    string strEndQueryString = Request.QueryString.ToString();
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));
                    string[] strQuery = strQueryString.Split('|').ToArray();

                    string strMobile = strQuery[0].ToString().Replace("Mobile=", "");

                    DataSet ds = new DataSet();
                    ds = objBAL.GetPayment(strMobile);

                    if (ds.Tables[0].Rows.Count > 0 && ds != null)
                    {
                        grdpayView.DataSourceID = string.Empty;
                        grdpayView.DataSource = ds.Tables[0];
                        grdpayView.DataBind();
                    }
                    else
                    {
                        grdpayView.DataSourceID = string.Empty;
                        grdpayView.DataBind();
                    }

                }

                catch (Exception ex)
                {


                }

            }
        }

        private string GetDataDetails()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllContributionMasterDetails(languageId).Where(x => x.Is_active == true).FirstOrDefault();

                if (dataMain != null)
                {
                LableData:
                    strBoardOfDirector = new StringBuilder();
                    strOfflineDetails = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dataMain.OfflineDonationDescription));
                    if (!string.IsNullOrWhiteSpace(dataMain.PageDescription))
                    {
                        strBoardOfDirector.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dataMain.PageDescription)));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetAllContributionMasterDetails(languageId).FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();

        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Contribution").FirstOrDefault();

                if (dataMain != null)
                {
                LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Contribution").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
        protected void grdpayView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField sts = (e.Row.FindControl("hdnpaystatus") as HiddenField);
                Label lblmsg = (e.Row.FindControl("lblpaystatus") as Label);
                LinkButton lnkbtn = (e.Row.FindControl("lnkpaystatus") as LinkButton);
                //LinkButton lnkbtnrf = (e.Row.FindControl("lnkrefund") as LinkButton);
                if (sts.Value.ToString().Trim().ToUpper() == "SUCCESS")
                {
                    lblmsg.Visible = true;
                    lnkbtn.Visible = false;
                  //  lnkbtnrf.Visible = false;
                }
                else
                {
                    lblmsg.Visible = false;
                    lnkbtn.Visible = true;
                   // lnkbtnrf.Visible = true;
                }
            }
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public string Hash(string value, string key)
        {
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("value");
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");

            var valueBytes = System.Text.Encoding.ASCII.GetBytes(value);
            var keyBytes = System.Text.Encoding.ASCII.GetBytes(key);

            var alg = new System.Security.Cryptography.HMACSHA512(keyBytes);
            var hash = alg.ComputeHash(valueBytes);

            var result = ByteArrayToString(hash);
            return result;
        }

        protected void lnkpaystatus_Click(object sender, EventArgs e)
        {
            lblStatusDiscription.Text = "";
            try
            {
                string passphrase = string.Empty;
                string salt = string.Empty;
                string passphrase1 = string.Empty;
                string salt1 = string.Empty;
                string passphrasesign = string.Empty;
                string StatusAPIUrl = string.Empty;
                string MerchId = string.Empty;
                string Password = string.Empty;


                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                string MerchTxnId = row.Cells[2].Text;

                PaymentBAL objBAL = new PaymentBAL();
                DataSet ds = new DataSet();
                ds = objBAL.GetPaymentDetail(MerchTxnId);
                string AtomTxnId = string.Empty;

                NameValueCollection nvc = Request.Form;
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;
                string hashAlgorithm = "SHA1";

                //   string encdata = nvc["encdata"];
                if (PaymentConfigDetailsValue.PaymentMode == "0")
                {
                    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseUAT;

                    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltUAT;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseUAT;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltUAT;

                    passphrasesign = PaymentConfigDetailsValue.PaymentStatusSignKeyUAT;

                    StatusAPIUrl = PaymentConfigDetailsValue.PaymentStatusAPIURLUAT;

                    MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

                    Password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;
                }
                else
                {
                    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseLive;

                    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltLive;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseLive;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltLive;

                    passphrasesign = PaymentConfigDetailsValue.PaymentStatusSignKeyLive;

                    StatusAPIUrl = PaymentConfigDetailsValue.PaymentStatusAPIURLLive;

                    MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdLive;

                    Password = PaymentConfigDetailsValue.PaymentRequestPasswordLive;
                }

                //generate json
                Payrequest.RootObjectStatus rt = new Payrequest.RootObjectStatus();
                Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
                Payrequest.HeadDetails1 hd = new Payrequest.HeadDetails1();
                Payrequest.MerchDetails1 md = new Payrequest.MerchDetails1();
                Payrequest.PayDetails1 pd = new Payrequest.PayDetails1();

                Payrequest.Statusrequest sr = new Payrequest.Statusrequest();

                hd.api = PaymentConfigDetailsValue.PaymentStatusAPI;
                hd.source = PaymentConfigDetailsValue.PaymentStatusSource;
                string signat = string.Empty;
                if (ds != null)
                {
                    // string passphrasesign = "58BE879B7DD635698764745511C704AB";
                    signat = MerchId + Password + MerchTxnId + ds.Tables[0].Rows[0]["amount"].ToString().Trim() + PaymentConfigDetailsValue.PaymentRequestTxnCurrency + hd.api;
                    string Encryptsignat = Hash(signat, passphrasesign);

                    //AtomTxnId = ds.Tables[0].Rows[0]["atomTxnId"].ToString();

                    md.merchId = MerchId;
                    md.password = Password;
                    md.merchTxnId = MerchTxnId;
                    md.merchTxnDate = ds.Tables[0].Rows[0]["merchTxnDate"].ToString();

                    pd.amount = ds.Tables[0].Rows[0]["amount"].ToString();
                    pd.txnCurrency = PaymentConfigDetailsValue.PaymentRequestTxnCurrency;
                    pd.signature = Encryptsignat.ToLower();// ds.Tables[0].Rows[0]["signature"].ToString();

                    sr.headDetails = hd;
                    sr.merchDetails = md;
                    sr.payDetails = pd;

                    rt.payInstrument = sr;
                }


                var json = new JavaScriptSerializer().Serialize(rt);


                //Insert API Request in transaction status
                APITransactionStatusRequest objBo = new APITransactionStatusRequest();
                objBo.api = hd.api;
                objBo.source = hd.source;
                objBo.merchId = md.merchId;
                objBo.password = md.password;
                objBo.merchTxnId = md.merchTxnId;
                objBo.merchTxnDate = md.merchTxnDate;
                objBo.amount = pd.amount;
                objBo.signature = signat;
                objBo.txnCurrency = pd.txnCurrency;

                bool APITxnStatusRequestFlag = new PaymentBAL().InsertTransactionStatusRequestData(objBo);

                string Encryptval = PaymentEncDec.Encrypt(json, passphrase, salt, iv, iterations);
                string Link = StatusAPIUrl + "merchId=" + md.merchId + "&encData=" + Encryptval;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);

                var myproxy = new WebProxy
                {
                    Address = new Uri($"http://10.10.2.248:8080"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                };
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                myproxy.BypassProxyOnLocal = false;
                request.Proxy = myproxy;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                Encoding encoding = new UTF8Encoding();
                try
                {
                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                  
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
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string responseFromServer = reader.ReadToEnd().Trim();
                string encData = responseFromServer.Split('&')[0].Replace("encData=", "");

                string Decryptval = PaymentEncDec.decrypt(encData, passphrase1, salt1, iv, iterations);
                string ErrDesc = string.Empty;

                ReCheckRoot myDeserializedClass = JsonConvert.DeserializeObject<ReCheckRoot>(Decryptval);
                PayInstrument Listresult = new PayInstrument();
                foreach (PayInstrument item in myDeserializedClass.payInstrument)
                {
                    if (item.merchDetails != null)
                    {
                        if (item.merchDetails.merchTxnId == MerchTxnId.Trim())
                        {
                            Listresult = item;
                        }
                    }
                    else
                    {
                        Listresult = item;
                    }
                }

                var jsonlist = JsonConvert.SerializeObject(Listresult);
                string Decryptvalstr = jsonlist.ToString();
                var result = Decryptvalstr.Replace("[", "");
                result = result.Replace("]", "");
                PayInstrument objectres = new PayInstrument();
                objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PayInstrument>(result);

                if (objectres.responseDetails.statusCode == "OTS0401")
                {
                    ErrDesc = objectres.responseDetails.description.ToString().Trim();
                }
                else
                {
                    ErrDesc = new PaymentBAL().GetStatuscodeDetail(objectres.responseDetails.statusCode.ToString().Trim(), objectres.responseDetails.description.ToString());
                }

                if (objectres.merchDetails != null && objectres.settlementDetails != null && objectres.payDetails != null && objectres.payModeSpecificData != null && objectres.responseDetails != null)
                {
                    //Insert API Response in transaction status
                    APITransactionStatusResponse objresBo = new APITransactionStatusResponse();

                    objresBo.reconStatus = objectres.settlementDetails.reconStatus;
                    objresBo.merchTxnId = objectres.merchDetails.merchTxnId;
                    objresBo.merchTxnDate = objectres.merchDetails.merchTxnDate;
                    objresBo.atomTxnId = objectres.payDetails.atomTxnId.ToString();
                    objresBo.product = objectres.payDetails.product;
                    objresBo.amount = Convert.ToDecimal(objectres.payDetails.amount.ToString().Trim());
                    objresBo.surchargeAmount = Convert.ToDecimal(objectres.payDetails.surchargeAmount.ToString().Trim());
                    objresBo.totalAmount = Convert.ToDecimal(objectres.payDetails.totalAmount.ToString().Trim());
                    objresBo.subChannel = objectres.payModeSpecificData.subChannel;
                    objresBo.otsBankId = objectres.payModeSpecificData.bankDetails.bankTxnId;  // this value not getting in response
                    objresBo.bankTxnId = objectres.payModeSpecificData.bankDetails.bankTxnId;
                    objresBo.cardMaskNumber = objectres.payModeSpecificData.bankDetails.cardMaskNumber;
                    objresBo.statusCode = objectres.responseDetails.statusCode;
                    objresBo.message = objectres.responseDetails.message;
                    objresBo.description = ErrDesc;

                    bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
                }

                lblStatusDiscription.Text = ErrDesc;
              //  Functions.MessagePopup(this, ErrDesc, PopupMessageType.error);

                  ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "alert('" + ErrDesc + "');", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //protected void lnkrefund_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string passphrase = string.Empty;
        //        string salt = string.Empty;
        //        string passphrase1 = string.Empty;
        //        string salt1 = string.Empty;
        //        string passphrasesign = string.Empty;
        //        string StatusAPIUrl = string.Empty;
        //        string MerchId = string.Empty;
        //        string Password = string.Empty;

        //        LinkButton btn = (LinkButton)sender;
        //        GridViewRow row = (GridViewRow)btn.NamingContainer;

        //        //string AtomTxnId = row.Cells[0].Text;
        //        string MerchTxnId = row.Cells[2].Text;

        //        PaymentBAL objBAL = new PaymentBAL();
        //        DataSet ds = new DataSet();
        //        ds = objBAL.GetPaymentDetail(MerchTxnId);

        //        if (PaymentConfigDetailsValue.PaymentMode == "0")
        //        {
        //            passphrase = PaymentConfigDetailsValue.PaymentRefundEncpassphraseUAT;

        //            salt = PaymentConfigDetailsValue.PaymentRefundEncsaltUAT;

        //            passphrase1 = PaymentConfigDetailsValue.PaymentRefundDecpassphraseUAT;

        //            salt1 = PaymentConfigDetailsValue.PaymentRefundDecsaltUAT;

        //            passphrasesign = PaymentConfigDetailsValue.PaymentRefundSignKeyUAT;

        //            StatusAPIUrl = PaymentConfigDetailsValue.PaymentRefundAPIURLUAT;

        //            MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

        //            Password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;
        //        }
        //        else
        //        {
        //            passphrase = PaymentConfigDetailsValue.PaymentRefundEncpassphraseLive;

        //            salt = PaymentConfigDetailsValue.PaymentRefundEncsaltLive;

        //            passphrase1 = PaymentConfigDetailsValue.PaymentRefundDecpassphraseLive;

        //            salt1 = PaymentConfigDetailsValue.PaymentRefundDecsaltLive;

        //            passphrasesign = PaymentConfigDetailsValue.PaymentRefundSignKeyLive;

        //            StatusAPIUrl = PaymentConfigDetailsValue.PaymentRefundAPIURLLive;

        //            MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

        //            Password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;
        //        }

        //        string signat = string.Empty;

        //        if (ds != null)
        //        {
        //            signat = MerchId + Password + MerchTxnId + PaymentConfigDetailsValue.paymentmodeRD + ds.Tables[0].Rows[0]["amount"].ToString().Trim() + PaymentConfigDetailsValue.PaymentRequestTxnCurrency + PaymentConfigDetailsValue.PaymentRefundAPI;
        //            string Encryptsignat = Hash(signat, passphrasesign);
        //        }

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "alert('Your transaction ID is : " + MerchTxnId + "');", true);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}