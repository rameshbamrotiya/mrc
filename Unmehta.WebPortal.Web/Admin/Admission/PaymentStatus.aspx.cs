using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Admission
{
    public partial class PaymentStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strPrice;
            string strMessage;
            string strInnerMessage;
            string merchTxnId;
            if (!IsPostBack)
            {
                string strEndQueryString = Request.QueryString.ToString();
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));


                string[] strQuery = strQueryString.Split('|').ToArray();

                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strPrice = strQuery[3].ToString().Replace("Price=", "");
                strMessage = strQuery[4].ToString().Replace("Status=", "");
                merchTxnId = strQuery[6].ToString().Replace("merchTxnId=", "");

                lblCourseName.Text = strCourseName;
                lblMessage.Text = strMessage;
                lblPrice.Text = strPrice;
                using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                {
                    StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.StudentId.ToString() == strStudentId && x.CourseId.ToString() == strCourseId).FirstOrDefault();
                    if (strMessage.Contains("Success"))
                    {
                        if (data.PaymentStatus == "In Process")
                        {
                            if (objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(data.RegistrationId, merchTxnId, (float)Convert.ToDecimal(strPrice), "Success"))
                            {
                                //"StudentId=1|CourseId=1|CourseName=Blood Banking";
                                Functions.MessagePopup(this, "Payment Done Successfully.", PopupMessageType.success);
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Payment Status is " + data.PaymentStatus, PopupMessageType.success);
                        }
                    }
                    else
                    {
                        objCandidateDetailsRepository.UpdateStudentRegistrationDetailsRegIds(data.RegistrationId, merchTxnId, (float)Convert.ToDecimal(strPrice), strMessage);
                        Functions.MessagePopup(this, "Payment Status is " + strMessage, PopupMessageType.error);
                    }
                }
            }
        }
        protected void btnBackToGrid_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Admission/Course.aspx"),false);
        }
    }
}