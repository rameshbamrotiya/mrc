using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class CoursePaymentPage : System.Web.UI.Page
    {
        public static string strStudentId;
        public static string strCourseId;
        public static string strPrice;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=1|CourseId=1|CourseName=Blood Banking"));
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Admission/Course.aspx?" + strEndQueryString, false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    string strReturnUrl = "";
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strPrice= strQuery[2].ToString().Replace("CourseName=", "");
                    

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
                    string strQu = HttpUtility.UrlEncode(Functions.Base64Encode(strQueryString.Replace("StudentId=", "").Replace("CourseId=", "").Replace("CourseName=", "") + "|Price=" + strPrice));
                    //SessionWrapper.PaymentReturnUrl = ("~/Admission/CoursePaymentStatusPage.aspx?" + (strQueryString + "|Price=" + strPrice));
                    
                    strReturnUrl = "Test";
                    string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|Price=" + strPrice + "|ReturnUrl="+ strReturnUrl+ "|Query="+ strQu));
                    Response.Redirect("~/Hospital/Payment/Request.aspx?"+ strdQuery, false);
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                    Response.Redirect("~/Admission/Defult.aspx", false);
                }
            }
        }
    }
}