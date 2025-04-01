using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.Admission
{
    public partial class Payment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string strStudentId;
            string strCourseId;
            string strPrice;
            string strEmail;
            string MerchTxnId;
            string strMobile;
            string CourseName;

            if (!IsPostBack)
            {
                try
                {
                    //string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=1|CourseId=1|CourseName=Blood Banking|Email=test@gmail.com|Mobile=9320145821"));
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Admin/Admission/");
                    }
                    string strReturnUrl = "";
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    CourseName = strQuery[2].ToString().Replace("CourseName=", "");
                    MerchTxnId = strQuery[3].ToString().Replace("MerchTxnId=", "");
                    strEmail = strQuery[4].ToString().Replace("Email=", "");
                    strMobile = strQuery[5].ToString().Replace("Mobile=", "");

                    string strMerchTxnId = MerchTxnId;
                    strPrice = "";
                    using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                    {
                        var dataOfUserName = objStudentAdvertisementBAL.GetAllStudentCourseConfiguration();
                        if (dataOfUserName != null)
                        {
                            List<StudentCourseConfigurationBO> data = Functions.ToListof<StudentCourseConfigurationBO>(dataOfUserName);
                            if (data != null)
                            {
                                strPrice = data.Where(x => x.CourseId.ToString() == strCourseId).FirstOrDefault().EntryFees.ToString();
                                //data;
                            }
                        }
                    }
                    string strQu = HttpUtility.UrlEncode(Functions.Base64Encode(strStudentId + "|" + strCourseId+ "|" + CourseName + "|" + strPrice + "|" + strEmail + "|" + strMobile)).Replace("%", "PP");
                    //SessionWrapper.PaymentReturnUrl = ("~/Admission/CoursePaymentStatusPage.aspx?" + (strQueryString + "|Price=" + strPrice));

                    strReturnUrl = "Admission";
                    string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|MercTxnId=" + strMerchTxnId + "|Price=" + strPrice + "|Email=" + strEmail + "|Mobile=" + strMobile + "|ReturnUrl=" + strReturnUrl + "|Query=" + strQu + "|CourseName=" + CourseName));
                    Response.Redirect("~/Hospital/Payment/Request.aspx?" + strdQuery, false);

                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                    Response.Redirect("~/Admission/Defult.aspx");
                }
            }
        }
    }
}