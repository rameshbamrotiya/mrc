using BAL;
using BO;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strCourseName;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Admission/Course.aspx",false);
                    }
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


                    SessionWrapper.BasicDetailsFlag = 0;
                    SessionWrapper.sendConfirmationMailFlag = 0;
                    Bind_DocGrid();
                    if (SessionWrapper.FinalDetailsFlag == 1)
                    {
                        btnProfessionalDivPrivious.Visible = false;
                        btnFinalSubmit.Visible = false;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        btnProfessionalDivPrivious.Visible = true;
                        btnFinalSubmit.Visible = true;
                        btnPrint.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void Bind_DocGrid()
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admission/Course.aspx", false);
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


            ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();

            int Id = Convert.ToInt32(strStudentId);
            long CourseId = Convert.ToInt64(strCourseId);
            DataSet ds = new DataSet();
            ds = objBAL.Candidate_Select(Id, CourseId);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                if (ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() == "True" && ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "2")
                {
                    chkdeclaration.Checked = true;
                    btnProfessionalDivPrivious.Attributes.Add("style", "display:none");
                    //btnProfessionalDivPrivious.Visible = false;
                    btnFinalSubmit.Attributes.Add("style", "display:none");
                    //btnFinalSubmit.Visible = false;
                }
                else
                {
                    chkdeclaration.Checked = false;
                    btnProfessionalDivPrivious.Attributes.Remove("style");
                    //btnProfessionalDivPrivious.Visible = true;
                    btnFinalSubmit.Attributes.Remove("style");
                    //btnFinalSubmit.Visible = true;
                }
                if (ds.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "4" && ds.Tables[0].Rows[0]["IsFinalSubmit"].ToString() == "True")
                {
                    chkdeclaration.Checked = true;
                    btnProfessionalDivPrivious.Attributes.Add("style", "display:none");
                    //btnProfessionalDivPrivious.Visible = false;
                    btnFinalSubmit.Attributes.Add("style", "display:none");
                    //btnFinalSubmit.Visible = false;
                }
                gView.DataSourceID = string.Empty;
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
            }
            else
            {
                gView.DataSourceID = string.Empty;
                gView.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0 && ds != null)
            {
                gvEducationDetails.DataSourceID = string.Empty;
                gvEducationDetails.DataSource = ds.Tables[1];
                gvEducationDetails.DataBind();
            }
            else
            {
                gvEducationDetails.DataSourceID = string.Empty;
                gvEducationDetails.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0 && ds != null)
            {
                gvCertificateCourseDetails.DataSourceID = string.Empty;
                gvCertificateCourseDetails.DataSource = ds.Tables[2];
                gvCertificateCourseDetails.DataBind();
            }
            else
            {
                gvCertificateCourseDetails.DataSourceID = string.Empty;
                gvCertificateCourseDetails.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0 && ds != null)
            {
                gvProfessionalExperience.DataSourceID = string.Empty;
                gvProfessionalExperience.DataSource = ds.Tables[3];
                gvProfessionalExperience.DataBind();
            }
            else
            {
                gvProfessionalExperience.DataSourceID = string.Empty;
                gvProfessionalExperience.DataBind();
            }

            if (ds.Tables[4].Rows.Count > 0 && ds != null)
            {
                Gridstudentcerti.DataSourceID = string.Empty;
                Gridstudentcerti.DataSource = ds.Tables[4];
                Gridstudentcerti.DataBind();
            }
            else
            {
                Gridstudentcerti.DataSourceID = string.Empty;
                Gridstudentcerti.DataBind();
            }

            if (ds.Tables[5].Rows.Count > 0 && ds != null)
            {
                gvieweducationDoc.DataSourceID = string.Empty;
                gvieweducationDoc.DataSource = ds.Tables[5];
                gvieweducationDoc.DataBind();
            }
            else
            {
                gvieweducationDoc.DataSourceID = string.Empty;
                gvieweducationDoc.DataBind();
            }
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admission/Course.aspx",false);
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");

            Response.Redirect("~/PrintApplication.aspx?StudentIdateId=" + Convert.ToInt32(strStudentId).ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void lnkBasicDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.BasicDetailsFlag = 1;
            Response.Redirect("~/Career/StudentIdateRegistration.aspx", false);
        }

        protected void lnkEducationDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.EducationDetailsFlag = 1;
            Response.Redirect("~/Career/StudentIdateEducationDetails.aspx",false);
        }

        protected void lnkCertificateDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.EducationDetailsFlag = 1;
            Response.Redirect("~/Career/StudentIdateEducationDetails.aspx", false);
        }

        protected void lnkProfessionalDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.ProfessionalDetailsFlag = 1;
            Response.Redirect("~/Career/StudentIdateEducationDetails.aspx",false);
        }

        protected void btnFinalSubmit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admission/Course.aspx", false);
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


                StudentFamilyDetailsBAL objBAL1 = new StudentFamilyDetailsBAL();
                StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL();
                if (chkdeclaration.Checked == false)
                {
                    Functions.MessagePopup(this, "Select declaration first.", PopupMessageType.warning);
                    return;
                }
                ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();
                bool LanguageResult = objBAL.InsertFinalSubmit(Convert.ToInt32(strStudentId), Convert.ToInt64(strCourseId), 1);
                int PersonalInformationId = 0;
                int ApplicationStatus = 2;
                int strStudentId1 = Convert.ToInt32(strStudentId);
                int strCourseId1 = Convert.ToInt32(strCourseId);
                DataSet ds = new DataSet();
                ds = objBAL.GetStudentDetail(strStudentId1, strCourseId1);
                string username = SessionWrapper.StudentRegistration.Username;
                DataSet dsverification = objBAL1.GetStudentVerificationByStudentIdandCourseId(Convert.ToInt32(strStudentId), Convert.ToInt32(strCourseId));
                if (dsverification != null && dsverification.Tables[0].Rows.Count > 0)
                {
                    PersonalInformationId = Convert.ToInt32(dsverification.Tables[0].Rows[0]["WorkFlowInstanceID"].ToString());
                }
                try
                {
                    using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                    {
                        var sms = configDetailsRepository.GetSMSTemplateByName("Application Submitted SMS");
                        if (sms != null)
                        {
                            string strLink = ConfigDetailsValue.StudentLogInLink;
                            string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{StudentName}}", SessionWrapper.StudentRegistration.FirstName).Replace("{{RegNo}}",Convert.ToString(ds.Tables[0].Rows[0]["RegistrationId"]))));
                            string strTemplateId = sms.SMSTemplateId;
                            string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                            string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                            string senderid2 = ConfigDetailsValue.senderid2;
                            string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                            string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, SessionWrapper.StudentRegistration.Mobile, strMessage, SMSAPI2, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }
                objData.InsertWorkflowRecordstudent(strStudentId1, strCourseId1, ApplicationStatus, PersonalInformationId, username);
                //string strStudentIdateId = HttpUtility.UrlEncode(Functions.Encrypt(SessionWrapper.StudentIdateId.ToString()));
                //Response.Redirect("~/PrintApplication.aspx?StudentIdateId=" + strStudentIdateId);
                SessionWrapper.sendConfirmationMail = 0;
                //string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
                //Response.Redirect("~/Admission/PrintApplication?" + strEndQueryString);

                Response.Redirect("~/Admission/Course.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btnProfessionalDivPrivious_ServerClick(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admission/Course.aspx",false);
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            Response.Redirect("~/Admission/ExtraInfofroAdmission?" + strEndQueryString1,false);
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admission/Course.aspx", false);
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


                SessionWrapper.sendConfirmationMailFlag = 1;
                string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
                Response.Redirect("~/Admission/PrintApplication?" + strEndQueryString1, false);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw;
            }
        }
    }
}