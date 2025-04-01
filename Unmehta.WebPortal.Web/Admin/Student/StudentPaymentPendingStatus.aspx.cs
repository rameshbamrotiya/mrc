using BAL;
using BAL.Admission;
using BO;
using BO.Admission;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class StudentPaymentPendingStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    try
                    {
                        if (SessionWrapper.UserDetails.UserName == null)
                        {
                            Response.Redirect("~/LoginPortal");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/LoginPortal");
                    }
                    BindPageViewData();

                    GetUpdateOfPayment("0", "230627165051617958784", "100.00", "2023/06/27");
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void BindPageViewData()
        {
            try
            {
                using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(objStudentAdvertisementBAL.GetAll()).Where(x => x.IsVisible == true).ToList();
                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            ddlCourceList.DataSource = data;
                            ddlCourceList.DataTextField = "Name";
                            ddlCourceList.DataValueField = "Id";
                            ddlCourceList.DataBind();
                            ddlCourceList.Items.Insert(0, new ListItem("Select Cource", ""));
                        }
                    }

                }
                BindGridViewData();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        private void BindGridViewData()
        {
            using (StudentCourseBAL objCandidateDetailsRepository = new StudentCourseBAL())
            {
                List<StudentAllRegistratedBO> data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllStudentRegistrationPaymentStatusDetails());

                if (ddlCourceList.SelectedIndex > 0)
                {
                    data = data.Where(x => x.CourseId == Convert.ToInt32(ddlCourceList.SelectedValue)).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    data = data.Where(x => x.FirstName.Contains(txtSearch.Text) || x.MiddleName.Contains(txtSearch.Text) || x.LastName.Contains(txtSearch.Text) || x.Mobile.Contains(txtSearch.Text) || x.Email.Contains(txtSearch.Text) || x.RegistrationId.Contains(txtSearch.Text)).ToList();
                }
                //if (data != null)
                {
                    lblCount.Text = "Total No Of Candidate : " + Convert.ToString(data.Count);
                    gView.DataSourceID = string.Empty;
                    gView.DataSource = data;
                    gView.DataBind();
                }
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridViewData();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                ddlCourceList.SelectedIndex = 0;
                BindGridViewData();

            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
        }

        protected void lnkRejectApplication_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);



            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();

                objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Reject");
                BindPageViewData();
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Reject");
            }
        }

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strStatus = e.Row.Cells[11].Text;
                //LinkButton lnkRejectApplication = (LinkButton)e.Row.Cells[14].FindControl("lnkRejectApplication");
                //LinkButton lnkAcceptApplication = (LinkButton)e.Row.Cells[14].FindControl("lnkAcceptApplication");
                //if (strStatus == "Pending")
                //{
                //    lnkRejectApplication.Visible = false;
                //    lnkAcceptApplication.Visible = false;
                //}
                //else
                //{
                //    lnkRejectApplication.Visible = false;
                //    lnkAcceptApplication.Visible = false;
                //}
            }
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAcceptApplication_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);



            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();

                objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Accept");
                BindPageViewData();
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Accept");
            }
        }

        public void SendConfirmationMail(string email, string Name, string Course, string AppNo, string AppStatus)
        {
            string emailId = email;
            string FullName = Name;
            string CourseName = Course;
            string ApplicationNumber = AppNo;
            string Status = AppStatus;
            CareerMasterBAL objBAL = new CareerMasterBAL();
            DataSet ds = new DataSet();
            ds = objBAL.MailCreditials();
            if (ds != null && ds.Tables.Count > 0)
            {
                string strError = "";
                string strBody = "";

                
                {

                    strBody = "Dear " + FullName.ToString() + "," +
                              "<br/><br/> Course Name : " + CourseName +
                              "<br/><br/> Application Confirmation Number : " + ApplicationNumber +
                              "<br/><br/> Your Application Confirmation Status :<b> " + Status + "</b>." +
                              "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                              "<br/><br/> Regards," +
                              "<br/><br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                }
                if (Functions.SendEmail(emailId, "Your Application Confirmation Status For The Course Of " + CourseName, strBody, out strError, true, null))
                {

                }
                else
                {
                    ErrorLogger.ERROR("Print Page Erroe =>" + strError, "", this);
                }

            }
        }

        protected void lnkPriview_Click(object sender, EventArgs e)
        {

            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId));
                //Response.Redirect("~/Admin/Student/StudentDetails?" + strEndQueryString);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Admin/Student/StudentDetails?" + strEndQueryString + "','_newtab');", true);

            }
        }

        protected void ibtn_Download_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            string RegistrationId = Convert.ToString(gView.DataKeys[rowIndex].Values["RegistrationId"]);
            string TxnId = Convert.ToString(gView.DataKeys[rowIndex].Values["TxnId"]);
            string amount = Convert.ToString(gView.DataKeys[rowIndex].Values["amount"]);
            string TransactionDate = Convert.ToString(gView.DataKeys[rowIndex].Values["TransactionDate"]);
            //int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            //int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);
            //string studentName = gView.DataKeys[rowIndex].Values["FirstName"].ToString();

            GetUpdateOfPayment(RegistrationId, TxnId, amount, TransactionDate);
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

        public void GetUpdateOfPayment(string RegistrationId, string MerchTxnId, string amount, string TransactionDate)
        {

            APITransactionStatusResponse objresBo = new APITransactionStatusResponse();
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
                    signat = MerchId + Password + MerchTxnId + amount + PaymentConfigDetailsValue.PaymentRequestTxnCurrency + hd.api;
                    string Encryptsignat = Hash(signat, passphrasesign);

                    //AtomTxnId = ds.Tables[0].Rows[0]["atomTxnId"].ToString();

                    md.merchId = MerchId;
                    md.password = Password;
                    md.merchTxnId = MerchTxnId;
                    md.merchTxnDate = TransactionDate;

                    pd.amount = amount;
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
                    //request.Proxy.Credentials = CredentialCache.DefaultCredentials;

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
                BO.PayInstrument Listresult = new BO.PayInstrument();
                foreach (BO.PayInstrument item in myDeserializedClass.payInstrument)
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
                    objresBo.amount = Convert.ToDecimal(pd.amount);
                    objresBo.surchargeAmount = Convert.ToDecimal(objectres.payDetails.surchargeAmount.ToString().Trim());
                    objresBo.totalAmount = Convert.ToDecimal(objectres.payDetails.totalAmount.ToString().Trim());
                    objresBo.subChannel = objectres.payModeSpecificData.subChannel;
                    objresBo.otsBankId = objectres.payModeSpecificData.bankDetails.bankTxnId;  // this value not getting in response
                    objresBo.bankTxnId = objectres.payModeSpecificData.bankDetails.bankTxnId;
                    objresBo.cardMaskNumber = objectres.payModeSpecificData.bankDetails.cardMaskNumber;
                    objresBo.statusCode = objectres.responseDetails.statusCode;
                    objresBo.message = objectres.responseDetails.message;
                    objresBo.description = ErrDesc;
                    using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                    {
                        if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)objresBo.amount, objresBo.message))
                        {
                            Functions.MessagePopup(this, RegistrationId+" Payment Status is " + objresBo.message, PopupMessageType.warning);
                            BindGridViewData();
                        }
                    }

                    bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
                }
                else
                {
                    using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                    {
                        if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)Convert.ToDecimal(pd.amount), "Details Not Available."))
                        {
                            Functions.MessagePopup(this, RegistrationId +" Payment Status is Details Not Available.", PopupMessageType.warning);
                        }
                    }
                }
                //  Functions.MessagePopup(this, ErrDesc, PopupMessageType.error);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}