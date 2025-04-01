
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class PaymentStatusPage : System.Web.UI.Page
    {
        public static string strTransactioNo;
        public static string strTxnId;
        public static string strPersonName;
        public static string strPrice;
        public static string strMessage;
        public static string strMessage1;
        public static string strInnerMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        string strEndQueryString = Request.QueryString.ToString();
                        string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));


                        string[] strQuery = strQueryString.Split('|').ToArray();

                        strTransactioNo = strQuery[0].ToString().Replace("StudentId=", "");
                        strTxnId = strQuery[1].ToString().Replace("CourseId=", "");
                        strPersonName = strQuery[2].ToString().Replace("CourseName=", "");
                        strPrice = strQuery[3].ToString().Replace("Price=", "");
                        strMessage = strQuery[4].ToString().Replace("Status=", "");

                        long Id = 0;

                        decimal dcAmount = 0;
                        if (long.TryParse(strTransactioNo, out Id))
                        {
                            if (Id > 0)
                            {

                                using (IHomePageRepository objCandidateDetailsRepository = new HomePageRepository(Functions.strSqlConnectionString))
                                {
                                    var data = objCandidateDetailsRepository.GetAllContributionTransaction().Where(x => x.Id == Id).FirstOrDefault();
                                    if (strMessage.ToLower().Contains("success"))
                                    {
                                        if (data != null)
                                        {
                                            if (data.Status == "Pending")
                                            {
                                                if (objCandidateDetailsRepository.UpdateContributionTransactionStatus(Id, "Success", out strMessage))
                                                {
                                                    lblMessage.InnerHtml = strMessage;
                                                    lblMessage.Style.Add("color", "Red");
                                                }
                                                else
                                                {

                                                    lblPersonName.Text = data.Name;
                                                    lblMessage.InnerHtml = strMessage + " <span id=\"time\">05:00</span>";
                                                    lblPrice.Text = data.Amount;
                                                    lblTxnid.Text = data.TxnId;

                                                    lblMessage.InnerHtml = "Payment Done Successfully.";
                                                    lblMessage.Style.Add("color", "Green");
                                                }
                                            }
                                            else
                                            {
                                                lblMessage.InnerHtml = "You are trying multiple Time so please take screen shot and provide required details.." + " <span id=\"time\">05:00</span>";
                                                lblMessage.Style.Add("color", "Red");
                                            }
                                        }
                                        else
                                        {
                                            lblMessage.InnerHtml = "Payment Record not exist please try after some time." + " <span id=\"time\">05:00</span>";
                                            lblMessage.Style.Add("color", "Red");
                                        }
                                        //{
                                        //    StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.StudentId.ToString() == strStudentId && x.CourseId.ToString() == strCourseId).FirstOrDefault();

                                        //    if (data.PaymentStatus == "In Process")
                                        //    {
                                        //        if (objCandidateDetailsRepository.UpdateStudentRegistrationStatus(data.RegistrationId, "Success"))
                                        //        {
                                        //            //"StudentId=1|CourseId=1|CourseName=Blood Banking";
                                        //            Functions.MessagePopup(this, "Payment Done Successfully.", PopupMessageType.error);
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        Functions.MessagePopup(this, "Payment Status is " + data.PaymentStatus, PopupMessageType.error);
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        objCandidateDetailsRepository.UpdateContributionTransactionStatus(Id, "Fail", out strMessage1);

                                        lblPersonName.Text = data.Name;
                                        lblMessage.InnerHtml = strMessage + " <span id=\"time\">05:00</span>";
                                        lblPrice.Text = data.Amount;
                                        lblTxnid.Text = data.TxnId;

                                        lblMessage.InnerHtml = "Payment Status is " + strMessage + " <span id=\"time\">05:00</span>";
                                        lblMessage.Style.Add("color", "Red");
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.InnerHtml = "Payment Status is " + strMessage + " please Try After Some 5 min Time.." + " <span id=\"time\">05:00</span>";
                                lblMessage.Style.Add("color", "Red");
                            }
                        }
                        else
                        {
                            lblMessage.InnerHtml = "Payment Status is " + strMessage + " please Try After Some Time.." + " <span id=\"time\">05:00</span>";
                            lblMessage.Style.Add("color", "Red");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                    Response.Redirect("~/Admission/Defult.aspx");
                }
            }
        }
    }
}