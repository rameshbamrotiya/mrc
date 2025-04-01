using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using BAL.Admission;
using Unmehta.WebPortal.Web.Common;
using BO.Admission;
using BO;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Payresponse;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Unmehta.WebPortal.Web
{
    public partial class Course : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //GetUpdateOfPayment("0", "230627144933714505713", "100.00", "2023-06-27");

            //GetUpdateOfPayment("0", "230627164640146487258", "100.00", "2023-06-27");
            //GetUpdateOfPayment("0", "230628095825419852243", "100.00", "2023-06-28");
            //GetUpdateOfPayment("0", "230627165051617958784", "100.00", "2023-06-27");
            //GetUpdateOfPayment("0", "230627175017034229766", "100.00", "2023-06-28");
            try
            {
                if (SessionWrapper.StudentRegistration.Username == null)
                {
                    Response.Redirect("~/Admission/", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                Response.Redirect("~/Admission/", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (!IsPostBack)
            {
                GetDetails();
            }
        }

        private void GetDetails()
        {
            StringBuilder strBuilder = new StringBuilder();
            StudentCourseBAL objBAL = new StudentCourseBAL();
            DataSet ds = objBAL.GetAllActiveStudentCourseMaster();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                gvCourse.DataSource = ds;
                gvCourse.DataBind();
            }
        }

        protected void lnkApply_Click(object sender, EventArgs e)
        {

            //int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            ////long studentId = SessionWrapper.StudentRegistration.Id;
            //long studentId = Convert.ToInt64(SessionWrapper.StudentRegistration.Id);
            //long courseId = Convert.ToInt64(gvCourse.DataKeys[rowindex]["Id"].ToString());
            //StudentRegistrationBO objBO = new StudentRegistrationBO();
            //string CourseName = gvCourse.Rows[rowindex].Cells[1].Text.ToString();
            //StudentCourseBAL objBAL = new StudentCourseBAL();
            //DataSet ds = objBAL.Student_CheckFinalSubmitFlag(studentId, courseId);
            //if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && (ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "4" || ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "2"))
            //    {
            //        string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + courseId + "|CourseName=" + CourseName));
            //        string url = ResolveUrl("~/Admission/StudentDetails.aspx?" + strEndQueryString);
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already applied for this course...');window.location.href='" + url + "';", true);
            //    }
            //    //else if (ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "5")
            //    //{
            //    //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + courseId + "|CourseName=" + CourseName));
            //    //    string url = ResolveUrl("~/Admission/StudentRegistration.aspx?" + strEndQueryString);
            //    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Your application reject apply new application');window.location.href='" + url + "';", true);
            //    //}
            //    else
            //    {
            //        string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + courseId + "|CourseName=" + CourseName));
            //        Response.Redirect("~/Admission/StudentRegistration.aspx?" + strEndQueryString);
            //    }
            //}

            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

                //hdnRecid.Value = grdDetails.DataKeys[rowindex]["courseid"].ToString();
                long lgId = Convert.ToInt16(gvCourse.DataKeys[rowindex]["Id"].ToString());
                StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL();

                var dataTa = objStudentCourseBAL.GetAllStudentCourseConfiguration();
                var data = Functions.ToListof<StudentCourseConfigurationBO>(dataTa).FirstOrDefault(x => x.CourseId == lgId);
                if (data != null)
                {
                    hTitle.InnerHtml = data.CourseName;
                    //btnOPenModel.InnerHtml = data.CourseName;
                    hfcourseId.Value = data.CourseId.ToString();
                    dvDetails.InnerHtml = HttpUtility.HtmlDecode(data.Desciption);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alertasd", "<script> $(document).ready(function () {\r\n            var $j = jQuery.noConflict(); $j(\"#"+ btnOPenModel.ClientID + "\").click(); });  </script> ", false);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gvCourse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int id = Convert.ToInt32(gvCourse.DataKeys[e.Row.RowIndex].Values[0]);
                bool IsEnableApply = Convert.ToBoolean(gvCourse.DataKeys[e.Row.RowIndex].Values[1]);

                long courseId = Convert.ToInt64(id);

                LinkButton lnkDetails = e.Row.FindControl("lnkDetails") as LinkButton;
                LinkButton lnkapply = e.Row.FindControl("lnkapply") as LinkButton;
                Label lblPaymentStatus = e.Row.FindControl("lblPaymentStatus") as Label;
                LinkButton ibtn_Payment = e.Row.FindControl("ibtn_Payment") as LinkButton;
                LinkButton lnkPriview = e.Row.FindControl("lnkPriview") as LinkButton;
                StudentCourseBAL objBAL = new StudentCourseBAL();
                long studentId = Convert.ToInt64(SessionWrapper.StudentRegistration.Id);
                DataSet dsas = objBAL.Student_CheckFinalSubmitFlag(studentId, courseId);
                StudentRegistrationDetailsBAL objstudBal = new StudentRegistrationDetailsBAL();
                System.Data.DataTable dsCheckpayment = objstudBal.GetAllRegistratedStudentPaymentByCourse(studentId, courseId);
                if (dsas.Tables != null && dsas.Tables[0].Rows.Count > 0)
                {
                    if (dsas.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && dsas.Tables[0].Rows[0]["PaymentStatus"].ToString().ToUpper().Contains("SUCCESS") && (dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "4" || dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "2"))
                    {
                        lnkapply.Visible = false;                                                

                    }
                   else if (dsas.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && (dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "4" || dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "2"))
                    {
                        lnkapply.Visible = false;
                    }
                    else
                    {
                        lnkapply.Visible = true;
                        if(dsas.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False")
                        {

                            if ((dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "3"))
                            {
                                lnkapply.Text = "Edit";
                            }
                            else
                            {
                                lnkapply.Text = "Apply&nbsp;Now";

                            }
                        }
                    }
                }
                lblPaymentStatus.Visible = false;

                if (dsCheckpayment.Rows.Count>0)
                {
                    lblPaymentStatus.Text = "";
                    if (dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToUpper().Contains("SUCCESS"))
                    {
                        ibtn_Payment.Visible = false;
                        lnkPriview.Visible = true;
                    }
                    else
                    {
                        //if (dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToLower().Contains("retry") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToLower().Contains("aborted") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToLower().Contains("in process") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToLower().Contains("pending") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToLower().Contains("failed"))
                        {
                            lblPaymentStatus.Visible = true;
                            lblPaymentStatus.Text = "Last Status of Payment is " + dsCheckpayment.Rows[0]["PaymentStatus"].ToString();
                            // ibtn_Payment.Text = "Last Status of Payment is " + dsCheckpayment.Rows[0]["PaymentStatus"].ToString();
                            ibtn_Payment.Visible = true;
                            lnkPriview.Visible = false;
                        }

                    }
                }
                else
                {
                    ibtn_Payment.Visible = false;
                    lnkPriview.Visible = false;
                }

                if (!IsEnableApply)
                {
                    if(lnkapply.Text != "Edit")
                    {
                        lnkapply.Visible = false;
                    }
                    lnkDetails.Visible = false;
                }

            }
            //your code here
        }

        protected void ibtn_Payment_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int id = Convert.ToInt32(gvCourse.DataKeys[row.RowIndex].Values[0].ToString());
            long courseId = Convert.ToInt64(id);
            decimal strPrice = 0;
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAllStudentCourseConfiguration();
                if (dataOfUserName != null)
                {
                    List<StudentCourseConfigurationBO> data = Functions.ToListof<StudentCourseConfigurationBO>(dataOfUserName);
                    if (data != null)
                    {
                        strPrice = (decimal)data.Where(x => x.CourseId == courseId).FirstOrDefault().EntryFees;
                        //data;
                    }
                }
            }
            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id)).Where(x => x.CourseId == courseId).FirstOrDefault();
                if (data != null)
                {
                    if (data.PaymentStatus == "Pending" || data.PaymentStatus == "Retry" || data.PaymentStatus.ToLower().Contains("failed"))
                    {
                        var trno = Functions.GetRandomNumberStringForPayment();
                        DateTime d = DateTime.Now;
                        string strMerchTxnId = d.ToString("yyMMddHHmmss") + trno;
                        if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(data.RegistrationId, strMerchTxnId, (float)strPrice, "In Process"))
                        {
                            //"StudentId=1|CourseId=1|CourseName=Blood Banking";
                            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName+ "|MerchTxnId=" + strMerchTxnId + "|Email=" + data.Email + "|Mobile=" + data.Mobile));

                            Response.Redirect("~/Admin/Admission/Payment.aspx?" + strEndQueryString, false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        GetUpdateOfPayment(data.RegistrationId,data.TxnId);

                    }
                }
            }
        }

        protected void lnkPriview_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gvCourse.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id)).Where(x => x.CourseId == Id).FirstOrDefault();


                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId));
                string strpath=ResolveUrl("~/Admin/Admission/PrintApplication?" + strEndQueryString);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strpath + "','_newtab');", true);

            }
        }

        protected void lnkDetails_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

                //hdnRecid.Value = grdDetails.DataKeys[rowindex]["courseid"].ToString();
                long lgId=Convert.ToInt16(gvCourse.DataKeys[rowindex]["Id"].ToString());
                StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL();

                var dataTa = objStudentCourseBAL.GetAllStudentCourseConfiguration();
                var data = Functions.ToListof<StudentCourseConfigurationBO>(dataTa).FirstOrDefault(x => x.CourseId == lgId);
                if (data != null)
                {
                    hTitle.InnerHtml = data.CourseName;
                    hfcourseId.Value = data.CourseId.ToString();
                    dvDetails.InnerHtml = HttpUtility.HtmlDecode(data.Desciption);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alertasd", "<script> $(document).ready(function () {\r\n            var $j = jQuery.noConflict(); $j(\"#" + btnOPenModel.ClientID + "\").click(); }); </script> ", false);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
                StudentCourseBAL objBAL = new StudentCourseBAL();
            long studentId = Convert.ToInt64(SessionWrapper.StudentRegistration.Id);
            DataSet ds = objBAL.Student_CheckFinalSubmitFlag(studentId, Convert.ToInt32(hfcourseId.Value));
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && (ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "4" || ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "2"))
                {
                    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + hfcourseId.Value + "|CourseName=" + hTitle.InnerHtml));
                    string url = ResolveUrl("~/Admission/StudentDetails.aspx?" + strEndQueryString);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already applied for this course...');window.location.href='" + url + "';", true);
                }
                //else if (ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() != "False" && ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "5")
                //{
                //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + courseId + "|CourseName=" + CourseName));
                //    string url = ResolveUrl("~/Admission/StudentRegistration.aspx?" + strEndQueryString);
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Your application reject apply new application');window.location.href='" + url + "';", true);
                //}
                else
                {
                    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + hfcourseId.Value + "|CourseName=" + hTitle.InnerHtml));

                    Response.Redirect("~/Admission/StudentRegistration.aspx?" + strEndQueryString, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + studentId + "|CourseId=" + hfcourseId.Value + "|CourseName=" + hTitle.InnerHtml));

                Response.Redirect("~/Admission/StudentRegistration.aspx?" + strEndQueryString, false);
                Context.ApplicationInstance.CompleteRequest();
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

        public void GetUpdateOfPayment(string RegistrationId,string MerchTxnId)
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


                PaymentBAL objBAL = new PaymentBAL();
                DataSet ds = new DataSet();
                ds = objBAL.GetStudentRegistrationByMerchTxnIdDetail(MerchTxnId);
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
                        if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(RegistrationId, MerchTxnId, (float)objresBo.amount, objresBo.message))
                        {
                            Functions.MessagePopup(this, "Payment Status is " + objresBo.message, PopupMessageType.warning);
                            GetDetails();
                        }
                    }

                    bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
                }
                else
                {
                    using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                    {
                        if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(RegistrationId, MerchTxnId, (float)Convert.ToDecimal(pd.amount), "Pending"))
                        {
                            Functions.MessagePopup(this, "Payment Status is " + "Pending", PopupMessageType.warning);
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
                    //using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                    //{
                    //    if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)objresBo.amount, objresBo.message))
                    //    {
                    //        Functions.MessagePopup(this, RegistrationId + " Payment Status is " + objresBo.message, PopupMessageType.warning);
                    //    }
                    //}

                    bool APITxnStatusResFlag = new PaymentBAL().InsertTransactionStatusResponseData(objresBo);
                }
                else
                {
                    //using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                    //{
                    //    if (objCandidateDetailsRepository.UpdateStudentRegistrationPayments(RegistrationId, MerchTxnId, (float)Convert.ToDecimal(pd.amount), "Details Not Available."))
                    //    {
                    //        Functions.MessagePopup(this, RegistrationId + " Payment Status is Details Not Available.", PopupMessageType.warning);
                    //    }
                    //}
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