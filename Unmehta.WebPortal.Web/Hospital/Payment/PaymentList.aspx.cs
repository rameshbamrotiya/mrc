using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using Unmehta.WebPortal.Web.Common;
using System.Data;
using Unmehta.WebPortal.Repository.Interface.Payment;
using Unmehta.WebPortal.Repository.Repository.Payment;
using BAL;
using BO;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using HMACDotNet;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Unmehta.WebPortal.Web.Hospital.Payment
{
    public class HmacShaNew512
    {
        private readonly HMac _hmac;

        public HmacShaNew512(byte[] key)
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
    public partial class PaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    PaymentBAL objBAL = new PaymentBAL();

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

        protected void grdpayView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField sts = (e.Row.FindControl("hdnpaystatus") as HiddenField);
                Label lblmsg = (e.Row.FindControl("lblpaystatus") as Label);
                LinkButton lnkbtn = (e.Row.FindControl("lnkpaystatus") as LinkButton);
                if (sts.Value.ToString().Trim() == "SUCCESS")
                {
                    lblmsg.Visible = true;
                    lnkbtn.Visible = false;
                }
                else
                {
                    lblmsg.Visible = false;
                    lnkbtn.Visible = true;
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

            try
            {
                string passphrase = string.Empty;
                string salt = string.Empty;
                string passphrase1 = string.Empty;
                string salt1 = string.Empty;
                string passphrasesign = string.Empty;
                string StatusAPIUrl = string.Empty;

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
                }
                else
                {
                    passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseLive;

                    salt = PaymentConfigDetailsValue.PaymentRequestEncsaltLive;

                    passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseLive;

                    salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltLive;

                    passphrasesign = PaymentConfigDetailsValue.PaymentStatusSignKeyLive;

                    StatusAPIUrl = PaymentConfigDetailsValue.PaymentStatusAPIURLLive;
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
                    signat = ds.Tables[0].Rows[0]["merchId"].ToString().Trim() + ds.Tables[0].Rows[0]["password"].ToString().Trim() + ds.Tables[0].Rows[0]["merchTxnId"].ToString().Trim() + ds.Tables[0].Rows[0]["amount"].ToString().Trim() + ds.Tables[0].Rows[0]["txnCurrency"].ToString().Trim() + hd.api;
                    string Encryptsignat = Hash(signat, passphrasesign);

                    AtomTxnId = ds.Tables[0].Rows[0]["atomTxnId"].ToString();

                    md.merchId = ds.Tables[0].Rows[0]["merchId"].ToString();
                    md.password = ds.Tables[0].Rows[0]["password"].ToString();
                    md.merchTxnId = ds.Tables[0].Rows[0]["merchTxnId"].ToString();
                    md.merchTxnDate = ds.Tables[0].Rows[0]["merchTxnDate"].ToString();

                    pd.amount = ds.Tables[0].Rows[0]["amount"].ToString();
                    pd.txnCurrency = ds.Tables[0].Rows[0]["txnCurrency"].ToString();
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
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                request.ContentType = "application/json";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                Encoding encoding = new UTF8Encoding();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string responseFromServer = reader.ReadToEnd().Trim();
                string encData = responseFromServer.Split('&')[0].Replace("encData=", "");

                string Decryptval = PaymentEncDec.decrypt(encData, passphrase1, salt1, iv, iterations);

                ReCheckRoot myDeserializedClass = JsonConvert.DeserializeObject<ReCheckRoot>(Decryptval);
                PayInstrument Listresult = new PayInstrument();
                foreach (PayInstrument item in myDeserializedClass.payInstrument)
                {
                    if (item.payDetails.atomTxnId.ToString().Trim() == AtomTxnId.Trim())
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
                objresBo.description = objectres.responseDetails.description;

                bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "alert(" + objresBo.description + "');", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void lnkrefund_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                //string AtomTxnId = row.Cells[0].Text;
                string AtomTxnId = row.Cells[0].Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "alert('Your transaction ID is : " + AtomTxnId + "');", true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}