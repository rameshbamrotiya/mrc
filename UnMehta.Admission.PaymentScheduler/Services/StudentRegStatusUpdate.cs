using BAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using UnMehta.Admission.PaymentScheduler.Services.Data;
using UnMehta.WebPortal.EmailScheduler.Comman;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace UnMehta.Admission.PaymentScheduler.Services
{
    public class StudentRegStatusUpdate
    {

        StudentRegDataContext dataContext   = new StudentRegDataContext(Functions.strSqlConnectionString);

        public void StartEmail()
        {
            ErrorLogger.ERROR("strWebHostedPath=>  " + Functions.strWebHostedPath, " start");
            ErrorLogger.ERROR("strSqlConnectionString=>  " + Functions.strSqlConnectionString, " start");
            string strPath = Functions.strWebHostedPath;
            try
            {

                using (dataContext)
                {
                    int currentDay = DateTime.Now.Day;

                    var allSchedule = dataContext.GetAllStudentRegistrationDetailsInProcessPaymentStatus().ToList();

                    foreach (var schedule in allSchedule)
                    {
                        string strError = "";

                        ErrorLogger.ERROR("RegistrationId=>  " + schedule.RegistrationId, "");
                        ErrorLogger.ERROR("TxnId=>  " + schedule.TxnId, "");
                        ErrorLogger.ERROR("amount=>  " + schedule.amount, "");
                        ErrorLogger.ERROR("TransactionDate=>  " + schedule.TransactionDate, "");
                        //var findAllLog = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMasterLog()
                        //    .Where(x => x.ScheduleId == schedule.Id && x.TriggerDateTime.Value.Date == DateTime.Now.Date).Count();


                        GetUpdateOfPayment(schedule.RegistrationId, schedule.TxnId, (decimal)schedule.amount, schedule.TransactionDate);
                    }

                    var allScheduleLis = dataContext.GetAllRegistrationDetailsListPending().ToList();

                    foreach (var schedule in allScheduleLis)
                    {
                        var mainList= dataContext.GetAllRegistrationDetailsListPendingByRegId(schedule.RegistrationId).ToList();
                        if(mainList.Count(x=> x.PaymentStatus.ToLower().Contains("success"))>0)
                        {
                            var mainmodel = mainList.FirstOrDefault(x => x.PaymentStatus.ToLower().Contains("success"));
                            dataContext.UpdateStudentRegistrationDetailsRegistrationId(schedule.RegistrationId, mainmodel.TxnId, mainmodel.amount, mainmodel.PaymentStatus);
                            ErrorLogger.ERROR("Update RegistrationId=> UP " + schedule.RegistrationId + " MerchTxnId=> " + mainmodel.TxnId + " amount" + mainmodel.amount, mainmodel.PaymentStatus);
                        }
                    }

                }
                ErrorLogger.ERROR("strWebHostedPath=>  " + Functions.strWebHostedPath, " end");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR("ex=>  " + ex, " end");
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

        public void GetUpdateOfPayment(string RegistrationId, string MerchTxnId,decimal? amount,DateTime? TransactionDate)
        {

            BO.APITransactionStatusResponse objresBo = new BO.APITransactionStatusResponse();
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


                //PaymentBAL objBAL = new PaymentBAL();
                //DataSet ds = new DataSet();
                //ds = objBAL.GetStudentRegistrationByMerchTxnIdDetail(MerchTxnId);
                string AtomTxnId = string.Empty;

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
                //if (ds != null)
                {
                    // string passphrasesign = "58BE879B7DD635698764745511C704AB";
                    signat = MerchId + Password + MerchTxnId + amount.ToString() + PaymentConfigDetailsValue.PaymentRequestTxnCurrency + hd.api;
                    string Encryptsignat = Hash(signat, passphrasesign);

                    //AtomTxnId = ds.Tables[0].Rows[0]["atomTxnId"].ToString();

                    md.merchId = MerchId;
                    md.password = Password;
                    md.merchTxnId = MerchTxnId;
                    md.merchTxnDate = TransactionDate.Value.ToString("yyyy-MM-dd");

                    pd.amount = amount.ToString();
                    pd.txnCurrency = PaymentConfigDetailsValue.PaymentRequestTxnCurrency;
                    pd.signature = Encryptsignat.ToLower();// ds.Tables[0].Rows[0]["signature"].ToString();

                    sr.headDetails = hd;
                    sr.merchDetails = md;
                    sr.payDetails = pd;

                    rt.payInstrument = sr;
                }


                var json = new JavaScriptSerializer().Serialize(rt);


                //Insert API Request in transaction status
                BO.APITransactionStatusRequest objBo = new BO.APITransactionStatusRequest();
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
                //request.Proxy = myproxy; 
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //       | SecurityProtocolType.Tls11
                //       | SecurityProtocolType.Tls12
                //       | SecurityProtocolType.Ssl3;
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
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
                    ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, exx.ToString());
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string responseFromServer = reader.ReadToEnd().Trim();
                string encData = responseFromServer.Split('&')[0].Replace("encData=", "");

                string Decryptval = PaymentEncDec.decrypt(encData, passphrase1, salt1, iv, iterations);
                string ErrDesc = string.Empty;

                BO.ReCheckRoot myDeserializedClass = JsonConvert.DeserializeObject<BO.ReCheckRoot>(Decryptval);
                if (myDeserializedClass.payInstrument.Count() > 1)
                {

                }
                BO.PayInstrument Listresult = new BO.PayInstrument();
                foreach (BO.PayInstrument item in myDeserializedClass.payInstrument)
                {
                    if (item.merchDetails != null)
                    {
                        if (item.merchDetails.merchTxnId == MerchTxnId.Trim())
                        {
                            Listresult = item;
                            if (item.responseDetails.message.ToLower().Contains("success"))
                            {
                                break;
                            }
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
                BO.PayInstrument objectres = new BO.PayInstrument();
                objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<BO.PayInstrument>(result);

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
                     //StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL();
                    {
                        //if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)objresBo.amount, objresBo.message))
                        //{
                        //    //Functions.MessagePopup(this, "Payment Status is " + objresBo.message, PopupMessageType.warning);
                        //}

                        //using (dataContext)
                        {
                            dataContext.UpdateStudentRegistrationPaymentDetails(0, 0, RegistrationId, MerchTxnId, objresBo.amount, objresBo.message);
                        }
                        ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, objresBo.message);

                    }
                    bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
                }
                else
                {
                    //StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL();
                    {
                        //if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)objresBo.amount, ErrDesc))
                        //{
                        //    //Functions.MessagePopup(this, "Payment Status is " + "Pending", PopupMessageType.warning);
                        //}

                        //using (dataContext)
                        {
                            dataContext.UpdateStudentRegistrationPaymentDetails(0, 0, RegistrationId, MerchTxnId, objresBo.amount, objectres.responseDetails.message);
                        }
                        ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, objectres.responseDetails.message);
                    }
                }
                //  Functions.MessagePopup(this, ErrDesc, PopupMessageType.error);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, ex.ToString());
                ErrorLogger.ERROR(" Request : ", ex.ToString());
            }
            //try
            //{
            //    string passphrase = string.Empty;
            //    string salt = string.Empty;
            //    string passphrase1 = string.Empty;
            //    string salt1 = string.Empty;
            //    string passphrasesign = string.Empty;
            //    string StatusAPIUrl = string.Empty;
            //    string MerchId = string.Empty;
            //    string Password = string.Empty;


            //    PaymentBAL objBAL = new PaymentBAL();
            //    DataSet ds = new DataSet();
            //    //ds = objBAL.GetStudentRegistrationByMerchTxnIdDetail(MerchTxnId);
            //    string AtomTxnId = string.Empty;

            //    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //    int iterations = 65536;
            //    int keysize = 256;
            //    string hashAlgorithm = "SHA1";

            //    //   string encdata = nvc["encdata"];
            //    if (PaymentConfigDetailsValue.PaymentMode == "0")
            //    {
            //        passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseUAT;

            //        salt = PaymentConfigDetailsValue.PaymentRequestEncsaltUAT;

            //        passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseUAT;

            //        salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltUAT;

            //        passphrasesign = PaymentConfigDetailsValue.PaymentStatusSignKeyUAT;

            //        StatusAPIUrl = PaymentConfigDetailsValue.PaymentStatusAPIURLUAT;

            //        MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdUAT;

            //        Password = PaymentConfigDetailsValue.PaymentRequestPasswordUAT;
            //    }
            //    else
            //    {
            //        passphrase = PaymentConfigDetailsValue.PaymentRequestEncpassphraseLive;

            //        salt = PaymentConfigDetailsValue.PaymentRequestEncsaltLive;

            //        passphrase1 = PaymentConfigDetailsValue.PaymentRequestDecpassphraseLive;

            //        salt1 = PaymentConfigDetailsValue.PaymentRequestDecsaltLive;

            //        passphrasesign = PaymentConfigDetailsValue.PaymentStatusSignKeyLive;

            //        StatusAPIUrl = PaymentConfigDetailsValue.PaymentStatusAPIURLLive;

            //        MerchId = PaymentConfigDetailsValue.PaymentRequestMerchIdLive;

            //        Password = PaymentConfigDetailsValue.PaymentRequestPasswordLive;
            //    }

            //    //generate json
            //    Payrequest.RootObjectStatus rt = new Payrequest.RootObjectStatus();
            //    Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
            //    Payrequest.HeadDetails1 hd = new Payrequest.HeadDetails1();
            //    Payrequest.MerchDetails1 md = new Payrequest.MerchDetails1();
            //    Payrequest.PayDetails1 pd = new Payrequest.PayDetails1();

            //    Payrequest.Statusrequest sr = new Payrequest.Statusrequest();

            //    hd.api = PaymentConfigDetailsValue.PaymentStatusAPI;
            //    hd.source = PaymentConfigDetailsValue.PaymentStatusSource;
            //    string signat = string.Empty;
            //    //if (ds != null)
            //    {
            //        // string passphrasesign = "58BE879B7DD635698764745511C704AB";
            //        signat = MerchId + Password + MerchTxnId + amount + PaymentConfigDetailsValue.PaymentRequestTxnCurrency + hd.api;
            //        string Encryptsignat = Hash(signat, passphrasesign);

            //        //AtomTxnId = ds.Tables[0].Rows[0]["atomTxnId"].ToString();

            //        md.merchId = MerchId;
            //        md.password = Password;
            //        md.merchTxnId = MerchTxnId;
            //        md.merchTxnDate = TransactionDate.Value.ToString("yyyy-MM-dd");

            //        pd.amount = amount.ToString();
            //        pd.txnCurrency = PaymentConfigDetailsValue.PaymentRequestTxnCurrency;
            //        pd.signature = Encryptsignat.ToLower();// ds.Tables[0].Rows[0]["signature"].ToString();

            //        sr.headDetails = hd;
            //        sr.merchDetails = md;
            //        sr.payDetails = pd;

            //        rt.payInstrument = sr;
            //    }


            //    var json = new JavaScriptSerializer().Serialize(rt);


            //    //Insert API Request in transaction status
            //    BO.APITransactionStatusRequest objBo = new BO.APITransactionStatusRequest();
            //    objBo.api = hd.api;
            //    objBo.source = hd.source;
            //    objBo.merchId = md.merchId;
            //    objBo.password = md.password;
            //    objBo.merchTxnId = md.merchTxnId;
            //    objBo.merchTxnDate = md.merchTxnDate;
            //    objBo.amount = pd.amount;
            //    objBo.signature = signat;
            //    objBo.txnCurrency = pd.txnCurrency;

            //    bool APITxnStatusRequestFlag = new PaymentBAL().InsertTransactionStatusRequestData(objBo);

            //    string Encryptval = PaymentEncDec.Encrypt(json, passphrase, salt, iv, iterations);
            //    string Link = StatusAPIUrl + "merchId=" + md.merchId + "&encData=" + Encryptval;
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);

            //    var myproxy = new WebProxy
            //    {
            //        Address = new Uri($"http://10.10.2.248:8080"),
            //        BypassProxyOnLocal = false,
            //        UseDefaultCredentials = false,
            //    };
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //    myproxy.BypassProxyOnLocal = false;
            //    request.Proxy = myproxy;
            //    ServicePointManager.Expect100Continue = true;
            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //    Encoding encoding = new UTF8Encoding();
            //    try
            //    {
            //        request.Proxy.Credentials = CredentialCache.DefaultCredentials;

            //        byte[] data = encoding.GetBytes(json);
            //        request.ProtocolVersion = HttpVersion.Version11;
            //        request.Method = "POST";
            //        request.ContentType = "application/json";
            //        request.ContentLength = data.Length;
            //        Stream stream = request.GetRequestStream();
            //        stream.Write(data, 0, data.Length);
            //        stream.Close();
            //    }
            //    catch (Exception exx)
            //    {
            //        ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, exx.ToString());
            //    }

            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    Stream resStream = response.GetResponseStream();
            //    StreamReader reader = new StreamReader(resStream);
            //    string responseFromServer = reader.ReadToEnd().Trim();
            //    string encData = responseFromServer.Split('&')[0].Replace("encData=", "");

            //    string Decryptval = PaymentEncDec.decrypt(encData, passphrase1, salt1, iv, iterations);
            //    string ErrDesc = string.Empty;

            //    BO.ReCheckRoot myDeserializedClass = JsonConvert.DeserializeObject<BO.ReCheckRoot>(Decryptval);
            //    BO.PayInstrument Listresult = new BO.PayInstrument();

            //    foreach (BO.PayInstrument item in myDeserializedClass.payInstrument)
            //    {
            //        if (item.merchDetails != null)
            //        {
            //            if (item.merchDetails.merchTxnId == MerchTxnId.Trim())
            //            {
            //                Listresult = item;
            //            }
            //        }
            //        else
            //        {
            //            Listresult = item;
            //        }
            //    }

            //    var jsonlist = JsonConvert.SerializeObject(Listresult);
            //    string Decryptvalstr = jsonlist.ToString();
            //    var result = Decryptvalstr.Replace("[", "");
            //    result = result.Replace("]", "");
            //    BO.PayInstrument objectres = new BO.PayInstrument();
            //    objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<BO.PayInstrument>(result);

            //    if (objectres.responseDetails.statusCode == "OTS0401")
            //    {
            //        ErrDesc = objectres.responseDetails.description.ToString().Trim();
            //    }
            //    else
            //    if (objectres.responseDetails.statusCode == "OTS0551")
            //    {
            //        ErrDesc = objectres.responseDetails.description.ToString().Trim();
            //    }
            //    else
            //    {
            //        ErrDesc = new PaymentBAL().GetStatuscodeDetail(objectres.responseDetails.statusCode.ToString().Trim());
            //    }

            //    if (objectres.merchDetails != null && objectres.settlementDetails != null && objectres.payDetails != null && objectres.payModeSpecificData != null && objectres.responseDetails != null)
            //    {
            //        //Insert API Response in transaction status

            //        objresBo.reconStatus = objectres.settlementDetails.reconStatus;
            //        objresBo.merchTxnId = objectres.merchDetails.merchTxnId;
            //        objresBo.merchTxnDate = objectres.merchDetails.merchTxnDate;
            //        objresBo.atomTxnId = objectres.payDetails.atomTxnId.ToString();
            //        objresBo.product = objectres.payDetails.product;
            //        objresBo.amount = Convert.ToDecimal(objectres.payDetails.amount.ToString().Trim());
            //        objresBo.surchargeAmount = Convert.ToDecimal(objectres.payDetails.surchargeAmount.ToString().Trim());
            //        objresBo.totalAmount = Convert.ToDecimal(objectres.payDetails.totalAmount.ToString().Trim());
            //        objresBo.subChannel = objectres.payModeSpecificData.subChannel;
            //        objresBo.otsBankId = objectres.payModeSpecificData.bankDetails.bankTxnId;  // this value not getting in response
            //        objresBo.bankTxnId = objectres.payModeSpecificData.bankDetails.bankTxnId;
            //        objresBo.cardMaskNumber = objectres.payModeSpecificData.bankDetails.cardMaskNumber;
            //        objresBo.statusCode = objectres.responseDetails.statusCode;
            //        objresBo.message = objectres.responseDetails.message;
            //        objresBo.description = ErrDesc;
            //        using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            //        {
            //            if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(RegistrationId, MerchTxnId, (float)objresBo.amount, objresBo.message))
            //            {
            //                //Functions.MessagePopup(this, "Payment Status is " + objresBo.message, PopupMessageType.warning);
            //            }

            //            using (dataContext)
            //            {
            //                dataContext.UpdateStudentRegistrationPaymentDetails(0, 0, RegistrationId, MerchTxnId, objresBo.amount, objresBo.message);
            //            }
            //            ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId+ " MerchTxnId=> "+ MerchTxnId + " amount"+ objresBo.amount, objresBo.message);

            //        }
            //        bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
            //    }
            //    else
            //    {
            //        using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            //        {
            //            if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(RegistrationId, MerchTxnId, (float)objresBo.amount, "Pending"))
            //            {
            //                //Functions.MessagePopup(this, "Payment Status is " + "Pending", PopupMessageType.warning);
            //            }

            //            using (dataContext)
            //            {
            //                dataContext.UpdateStudentRegistrationPaymentDetails(0, 0, RegistrationId, MerchTxnId, objresBo.amount, "Pending");
            //            }
            //            ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, "Pending");
            //        }
            //    }
            //    //  Functions.MessagePopup(this, ErrDesc, PopupMessageType.error);

            //}
            //catch (Exception ex)
            //{
            //    ErrorLogger.ERROR("Update RegistrationId=>  " + RegistrationId + " MerchTxnId=> " + MerchTxnId + " amount" + objresBo.amount, ex.ToString());
            //    ErrorLogger.ERROR(" Request : ", ex.ToString());
            //}
        }

    }
}
