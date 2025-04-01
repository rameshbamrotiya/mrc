using BAL;
using BO;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strCourseName;
        //public static string strRegistrationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string strEndQueryString = Request.QueryString.ToString();
            //if (string.IsNullOrWhiteSpace(strEndQueryString))
            //{
            //    Response.Redirect("~/Admin/Student");
            //}
            SessionWrapper.FileUploadDetails = new SessionFileUploadModel();


            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");



            //string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            //string[] strQuery = strQueryString.Split('|').ToArray();
            //if (strQuery.Count() == 4)
            //{
            //    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            //    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            //    strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            //    strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            //}
            //else
            //{
            //    Response.Redirect("~/Admin/Student/StudentApplyList");
            //}
            if (!IsPostBack)
            {
                FillAllCheckBox();
                BindDocGrid();
                FillDataForEditMode();
                BindProfessonalReferalGrid();
                FillStudentDetailsByStudentId();
            }
            if (SessionWrapper.FinalDetailsFlag == 1)
            {
                btnPrint.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
            }
        }

        protected void BindDocGrid()
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();

            int Id = Convert.ToInt32(strStudentId);
            long CourseId = Convert.ToInt64(strCourseId);
            DataSet ds = new DataSet();
            ds = objBAL.Candidate_Select(Id, CourseId);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
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
                SessionFileUploadModel obj = new SessionFileUploadModel();
                obj = SessionWrapper.FileUploadDetails;
                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["PhotographPath"].ToString()))
                {
                    hfPhotographName.Value = ds.Tables[1].Rows[0]["PhotographPath"].ToString();
                    //obj.photoUploadpath = ConfigDetailsValue.StudentPhotograph;
                    //obj.photoUploadName = hfPhotographName.Value;
                    if (!string.IsNullOrWhiteSpace(hfPhotographName.Value))
                    {
                        PhotographPreview.ImageUrl = hfPhotographName.Value;
                        PhotographPreview.Visible = true;
                        //rfvPhotograph.Enabled = false;
                        // RegExValFileUploadFileType.Enabled = false;
                    }
                }
                else
                {
                    // rfvPhotograph.Enabled = true;
                    // RegExValFileUploadFileType.Enabled = true;
                }

                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["SignaturePath"].ToString()))
                {
                    hfSignatureName.Value = ds.Tables[1].Rows[0]["SignaturePath"].ToString();
                    //obj.signatureUploadPath = ConfigDetailsValue.StudentSignature;
                    //obj.signatureUploadName = hfSignatureName.Value;
                    if (!string.IsNullOrWhiteSpace(hfSignatureName.Value))
                    {
                        SignaturePriview.ImageUrl = hfSignatureName.Value;
                        SignaturePriview.Visible = true;
                    }
                }
                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DOBFilePath"].ToString()))
                {
                    hfSignatureName.Value = ds.Tables[1].Rows[0]["DOBFilePath"].ToString();


                    hfDOBProofName.Value = ds.Tables[1].Rows[0]["DOBFilePath"].ToString();
                    if (!string.IsNullOrWhiteSpace(hfSignatureName.Value))
                    {
                        AadharcardPreview.ImageUrl = hfDOBProofName.Value;
                        AadharcardPreview.Visible = true;
                    }
                    obj.dobUploadPath = ConfigDetailsValue.StudentDateofBirthProof;
                    obj.dobUploadName = hfDOBProofName.Value;
                    // btnViewDOBFile.Visible = true;
                }
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

        protected void btnRegDetails_Click(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName + "|RegistrationId=" + strRegistrationId));
            Response.Redirect("~/Admin/Student/BasicDetails?" + strEndQueryString1);
        }

        protected void btnFamily_Click(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName + "|RegistrationId=" + strRegistrationId));
            Response.Redirect("~/Admin/Student/BasicDetails?" + strEndQueryString1);
        }

        protected void btnEducation_Click(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName + "|RegistrationId=" + strRegistrationId));
            Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString1);
        }

        protected void btnCourse_Click(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName + "|RegistrationId=" + strRegistrationId));
            Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString1);
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName + "|RegistrationId=" + strRegistrationId));
            Response.Redirect("~/Admin/Student/PrintPage?" + strEndQueryString1);
        }

        protected void btnViewDOBFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfDOBProofName.Value))
            {
                string dobFilePath = ResolveUrl(hfDOBProofName.Value);
                Response.Redirect(dobFilePath);
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "document.getElementById('" + btnViewDOBFile.ClientID + "').focus();window.open('" + dobFilePath + "','_newtab');", true);
        }

        private void BindProfessonalReferalGrid()
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student");
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
            string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

            ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
            objBo.CandidateId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt64(strCourseId);
            ExtraInfoforAdmissionBAL objbal = new ExtraInfoforAdmissionBAL();
            DataSet ds = objbal.SelectRecordReferral(objBo);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.AcceptChanges();
                grdReferalDetails.DataSource = dt;
                grdReferalDetails.DataBind();
            }
            else
            {
                grdReferalDetails.DataBind();
            }
        }

        private void FillAllCheckBox()
        {
            try
            {
                {
                    ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();
                    DataSet ds = objBAL.SelectAdvertisementSourceWise();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chk1.DataSource = ds.Tables[0];
                        chk1.DataTextField = "Advertisement_Name";
                        chk1.DataValueField = "id";
                        chk1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private bool FillDataForEditMode()
        {
            try
            {
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admin/Student");
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

                ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
                objBo.CandidateId = Convert.ToInt32(strStudentId);
                objBo.CourseId = Convert.ToInt64(strCourseId);
                //objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
                ExtraInfoforAdmissionBAL objbal = new ExtraInfoforAdmissionBAL();
                DataSet ds = objbal.SelectRecord(objBo);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr.HasErrors) return false;
                    if (dr["UNMICRCContactYN"] != DBNull.Value)
                        RBLknowperson.SelectedValue = dr["UNMICRCContactYN"].ToString();
                    if (RBLknowperson.SelectedValue == "yes")
                    {
                        txtUNMICRCPerson.Visible = true;
                        txtUNMICRCPerson.Focus();
                    }
                    else
                    {
                        txtUNMICRCPerson.Visible = false;
                        RBLknowperson.Focus();
                    }
                    if (dr["UNMICRCContact"] != DBNull.Value)
                        txtUNMICRCPerson.Text = dr["UNMICRCContact"].ToString();
                    if (dr["courseheard"] != DBNull.Value)
                        txtcourseheard.InnerText = dr["courseheard"].ToString();
                    if (dr["ChronicalIllnessYN"] != DBNull.Value)
                        RBLilleness.SelectedValue = dr["ChronicalIllnessYN"].ToString();
                    if (RBLilleness.SelectedValue == "yes")
                    {
                        txtChronicillness.Visible = true;
                        txtChronicillness.Focus();
                    }
                    else
                    {
                        txtChronicillness.Visible = false;
                        RBLilleness.Focus();
                    }
                    if (dr["ChronicalIllness"] != DBNull.Value)
                        txtChronicillness.Text = dr["ChronicalIllness"].ToString();
                    if (dr["BloodGroup"] != DBNull.Value)
                        txtbloodgroup.Text = dr["BloodGroup"].ToString();
                    if (dr["MonthYear"] != DBNull.Value)
                    {
                        DateTime dti = DateTime.ParseExact(dr["MonthYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
                        ddlExtrInfoYear.SelectedValue = dti.Year.ToString();
                        ddlMonth.SelectedValue = dti.ToString("MMM");
                    }
                    if (dr["AllergicYN"] != DBNull.Value)
                        RBLAllergicto.SelectedValue = dr["AllergicYN"].ToString();
                    if (RBLAllergicto.SelectedValue == "yes")
                    {
                        txtmajorillness.Visible = true;
                        txtmajorillness.Focus();
                    }
                    else
                    {
                        txtmajorillness.Visible = false;
                        RBLAllergicto.Focus();
                    }
                    if (dr["Allergic"] != DBNull.Value)
                        txtmajorillness.Text = dr["Allergic"].ToString();
                    if (dr["EmergencyContactNo"] != DBNull.Value)
                        txtcontactno.Text = dr["EmergencyContactNo"].ToString();
                    if (dr["EmergencyContactPersonName"] != DBNull.Value)
                        txtconperson.Text = dr["EmergencyContactPersonName"].ToString();
                    if (dr["EmergencyContactPersonRelation"] != DBNull.Value)
                        txtrelation.Text = dr["EmergencyContactPersonRelation"].ToString();
                    if (dr["EmergencyContactPersonAdd"] != DBNull.Value)
                        txtaddress.Text = dr["EmergencyContactPersonAdd"].ToString();
                    if (dr["CourtOfLawYN"] != DBNull.Value)
                        rblcourtoflaw.SelectedValue = dr["CourtOfLawYN"].ToString();
                    if (rblcourtoflaw.SelectedValue == "YES".Trim())
                    {
                        txtcourtoflaw.Visible = true;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtcourtoflaw.ClientID + "').focus();", true);
                    }
                    else
                    {
                        txtcourtoflaw.Visible = false;
                        rblaccommodation.Focus();
                        rblaccommodation.SelectedValue = "Own";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + rblaccommodation.ClientID + "').focus();", true);
                    }
                    if (dr["CourtOfLaw"] != DBNull.Value)
                        txtcourtoflaw.Text = dr["CourtOfLaw"].ToString();

                    if (dr["ExtraActivities"] != DBNull.Value)
                        txtextraactivities.Text = dr["ExtraActivities"].ToString();
                    if (dr["Accommodation"] != DBNull.Value)
                        rblaccommodation.Text = dr["Accommodation"].ToString();
                    //if (dr["AboutCourse"] != DBNull.Value)
                    //string chk = dr["AboutCourse"].ToString();
                    CheckBoxList c = (CheckBoxList)chk1.FindControl("chk1");
                    string checkBoxValues = dr["AboutCourse"].ToString();
                    char sep = ',';
                    String[] s = checkBoxValues.Split(sep);

                    for (int i = 0; i < s.Length; i++)
                    {

                        for (int j = 0; j < c.Items.Count; j++)
                        {

                            if (c.Items[j].Value == s[i])
                            {
                                c.Items[j].Selected = true;
                            }
                        }
                    }
                    if (dr["ExtraActivitiesSocial"] != DBNull.Value)
                        txtsocialactivities.Text = dr["ExtraActivitiesSocial"].ToString();
                    //if (dr["AboutCourseOther"] != DBNull.Value)
                    //    txtothercourse.Text = dr["AboutCourseOther"].ToString();
                    if (dr["SurgeryInfoYN"] != DBNull.Value)
                        RBLSurgery.SelectedValue = dr["SurgeryInfoYN"].ToString();
                    if (RBLSurgery.SelectedValue == "yes")
                    {
                        txtsurgeryinfo.Visible = true;
                        DIVSurgeryMonth.Visible = true;
                        DIVSurgeryYear.Visible = true;
                        txtsurgeryinfo.Focus();
                    }
                    else
                    {
                        txtsurgeryinfo.Visible = false;
                        DIVSurgeryMonth.Visible = false;
                        DIVSurgeryYear.Visible = false;
                        RBLSurgery.Focus();
                    }
                    if (dr["SurgeryInfo"] != DBNull.Value)
                        txtsurgeryinfo.Text = dr["SurgeryInfo"].ToString();
                    string message = "";
                    foreach (System.Web.UI.WebControls.ListItem item in chk1.Items)
                    {
                        if (item.Selected)
                        {
                            message += item.Text.ToLower();
                            if (message.Contains("other"))
                            {
                                CLRemarks.Visible = true;
                            }
                            else
                            {
                                CLRemarks.Visible = false;
                            }
                        }
                        else
                        {
                            CLRemarks.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
            return true;
        }

        protected void ddlPersonalInformationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPersonalInformationId.SelectedValue == "1")
            {
                txtremarks.Visible = true;
                lblremarks.Visible = true;
                txtremarks.Focus();
            }
            else
            {
                txtremarks.Visible = false;
                lblremarks.Visible = false;
            }
        }

        protected void ddlAddressId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAddressId.SelectedValue == "1")
            {
                txtAddressRemarks.Visible = true;
                lblAddressRemarks.Visible = true;
                txtAddressRemarks.Focus();
            }
            else
            {
                txtAddressRemarks.Visible = false;
                lblAddressRemarks.Visible = false;
            }
        }

        protected void ddlDocumentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocumentId.SelectedValue == "1")
            {
                txtDocumentRemarks.Visible = true;
                lblDocumentRemarks.Visible = true;
                txtDocumentRemarks.Focus();
            }
            else
            {
                txtDocumentRemarks.Visible = false;
                lblDocumentRemarks.Visible = false;
            }
        }

        protected void ddlFamilyMemberId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFamilyMemberId.SelectedValue == "1")
            {
                txtFamilyMemberRemarks.Visible = true;
                lblFamilyMemberRemarks.Visible = true;
                txtFamilyMemberRemarks.Focus();
            }
            else
            {
                txtFamilyMemberRemarks.Visible = false;
                lblFamilyMemberRemarks.Visible = false;
            }
        }

        private bool FillForm(StudentRegistrationDetailsBO objBo)
        {
            try
            {
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admin/Student");
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

                if (string.IsNullOrWhiteSpace(hfStudentWorkflowId.Value))
                {
                    objBo.hfStudentWorkflowId = 0;
                }
                else
                {
                    objBo.hfStudentWorkflowId = Convert.ToInt64(hfStudentWorkflowId.Value);
                }
                objBo.StudentId = Convert.ToInt64(strStudentId);
                objBo.CourseId = Convert.ToInt64(strCourseId);
                objBo.RegistrationId = strRegistrationId;
                if (ddlPersonalInformationId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlPersonalInformationId.Focus();
                    return false;
                }
                if (ddlPersonalInformationId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtremarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter personal information remarks", PopupMessageType.error);
                        txtremarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.PersonalInformationId = ddlPersonalInformationId.SelectedValue;
                        objBo.PersonalInformationRemarks = txtremarks.Text;
                    }
                }
                else
                {
                    objBo.PersonalInformationId = ddlPersonalInformationId.SelectedValue;
                }
                if (ddlAddressId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlAddressId.Focus();
                    return false;
                }
                if (ddlAddressId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtAddressRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter address remarks", PopupMessageType.error);
                        txtAddressRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.AddressId = ddlAddressId.SelectedValue;
                        objBo.AddressRemarks = txtAddressRemarks.Text;
                    }
                }
                else
                {
                    objBo.AddressId = ddlAddressId.SelectedValue;
                }
                if (ddlDocumentId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlDocumentId.Focus();
                    return false;
                }
                if (ddlDocumentId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtDocumentRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter document remarks", PopupMessageType.error);
                        txtDocumentRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.DocumentId = ddlDocumentId.SelectedValue;
                        objBo.DocumentRemarks = txtDocumentRemarks.Text;
                    }
                }
                else
                {
                    objBo.DocumentId = ddlDocumentId.SelectedValue;
                }

                if (ddlFamilyMemberId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlFamilyMemberId.Focus();
                    return false;
                }
                if (ddlFamilyMemberId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtFamilyMemberRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter family member information", PopupMessageType.error);
                        txtFamilyMemberRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.FamilyMemberId = ddlFamilyMemberId.SelectedValue;
                        objBo.FamilyMemberRemarks = txtFamilyMemberRemarks.Text;
                    }
                }
                else
                {
                    objBo.FamilyMemberId = ddlFamilyMemberId.SelectedValue;
                }

                if (ddlAcademicId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlAcademicId.Focus();
                    return false;
                }
                if (ddlAcademicId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtAcademicRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtAcademicRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.AcademicId = ddlAcademicId.SelectedValue;
                        objBo.AcademicRemarks = txtAcademicRemarks.Text;
                    }
                }
                else
                {
                    objBo.AcademicId = ddlAcademicId.SelectedValue;
                }

                if (ddlEducationDocId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlEducationDocId.Focus();
                    return false;
                }
                if (ddlEducationDocId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtEducationDocRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtEducationDocRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.EducationDocId = ddlEducationDocId.SelectedValue;
                        objBo.EducationDocRemarks = txtEducationDocRemarks.Text;
                    }
                }
                else
                {
                    objBo.EducationDocId = ddlEducationDocId.SelectedValue;
                }

                if (ddlCoursesId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlCoursesId.Focus();
                    return false;
                }
                if (ddlCoursesId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtCoursesRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtCoursesRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.CoursesId = ddlCoursesId.SelectedValue;
                        objBo.CoursesRemarks = txtCoursesRemarks.Text;
                    }
                }
                else
                {
                    objBo.CoursesId = ddlCoursesId.SelectedValue;
                }

                if (ddlOtherInfoId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlOtherInfoId.Focus();
                    return false;
                }
                if (ddlOtherInfoId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtOtherInfoRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtOtherInfoRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.OtherInfoId = ddlOtherInfoId.SelectedValue;
                        objBo.OtherInfoRemarks = txtOtherInfoRemarks.Text;
                    }
                }
                else
                {
                    objBo.OtherInfoId = ddlOtherInfoId.SelectedValue;
                }

                if (ddllawId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddllawId.Focus();
                    return false;
                }
                if (ddllawId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtlawReamrks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtlawReamrks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.lawId = ddllawId.SelectedValue;
                        objBo.lawReamrks = txtlawReamrks.Text;
                    }
                }
                else
                {
                    objBo.lawId = ddllawId.SelectedValue;
                }


                if (ddlEmergencyId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlEmergencyId.Focus();
                    return false;
                }
                if (ddlEmergencyId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtEmergencyRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtEmergencyRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.EmergencyId = ddlEmergencyId.SelectedValue;
                        objBo.EmergencyRemarks = txtEmergencyRemarks.Text;
                    }
                }
                else
                {
                    objBo.EmergencyId = ddlEmergencyId.SelectedValue;
                }

                if (ddlReferencesId.SelectedValue == "-1")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlReferencesId.Focus();
                    return false;
                }
                if (ddlReferencesId.SelectedValue == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtReferencesRemarks.Text))
                    {
                        Functions.MessagePopup(this, "Enter Reamrks", PopupMessageType.error);
                        txtReferencesRemarks.Focus();
                        return false;
                    }
                    else
                    {
                        objBo.ReferencesId = ddlReferencesId.SelectedValue;
                        objBo.ReferencesRemarks = txtReferencesRemarks.Text;
                    }
                }
                else
                {
                    objBo.ReferencesId = ddlReferencesId.SelectedValue;
                }

                using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
                {
                    StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.StudentId == objBo.StudentId && x.CourseId == objBo.CourseId).FirstOrDefault();

                    string strMobileNO = "";
                    if (!string.IsNullOrWhiteSpace(data.Mobile) && data != null)
                    {
                        strMobileNO = data.Mobile;
                    }

                    if (ddlPersonalInformationId.SelectedValue == "1" || ddlAddressId.SelectedValue == "1" || ddlDocumentId.SelectedValue == "1" || ddlFamilyMemberId.SelectedValue == "1" || ddlAcademicId.SelectedValue == "1" || ddlEducationDocId.SelectedValue == "1" || ddlCoursesId.SelectedValue == "1" || ddlOtherInfoId.SelectedValue == "1" || ddllawId.SelectedValue == "1" || ddlEmergencyId.SelectedValue == "1" || ddlReferencesId.SelectedValue == "1")
                    {
                        objBo.ApplicationStatus = "3";
                        try
                        {
                            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                            {
                                var sms = configDetailsRepository.GetSMSTemplateByName("Correction SMS");
                                if (sms != null)
                                {
                                    string strLink = ConfigDetailsValue.StudentLogInLink;
                                    string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{Link}}", ConfigDetailsValue.StudentLogInLink)));
                                    string strTemplateId = sms.SMSTemplateId;
                                    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                    string senderid2 = ConfigDetailsValue.senderid2;
                                    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, strMobileNO, strMessage, SMSAPI2, 0);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                        }
                    }
                    else
                    {
                        objBo.ApplicationStatus = "4";
                        //int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
                        long Id = Convert.ToInt64(hfStudentWorkflowId.Value);
                        long couId = Convert.ToInt64(strCourseId);

                        objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Accept");
                        string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                        try
                        {

                            SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Accept");
                            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                            {
                                var sms = configDetailsRepository.GetSMSTemplateByName("Payment of Fees SMS");
                                if (sms != null)
                                {
                                    string strLink = ConfigDetailsValue.StudentLogInLink;
                                    string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{Link}}", ConfigDetailsValue.StudentLogInLink)));
                                    string strTemplateId = sms.SMSTemplateId;
                                    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                    string senderid2 = ConfigDetailsValue.senderid2;
                                    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, strMobileNO, strMessage, SMSAPI2, 0);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StudentRegistrationDetailsBO objBo = new StudentRegistrationDetailsBO();
            using (StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL())
            {
                if (FillForm(objBo))
                {
                    if (objData.InsertWorkflowRecord(objBo))
                    {
                        //Response.Redirect("~/Admin/Student/StudentApplyList");
                        string dobFilePath = ResolveUrl("~/Admin/Student/StudentApplyList");
                        Page.Response.Write(@"<script language='javascript'>alert('Record saved successfully.');document.location.href='" + dobFilePath + "'; </script>");

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Record saved successfully.');document.location.href('" + dobFilePath + "');", true);
                        //Functions.MessagePopup(this, "Details insert sucessfully .....!", PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
                    }
                }
            }
        }

        private void FillStudentDetailsByStudentId()
        {
            try
            {
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admin/Student");
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                string strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");

                StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
                StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
                objbo.Id = Convert.ToInt64(strStudentId);
                objbo.CourseId = Convert.ToInt64(strCourseId);
                DataSet ds = objBAL.GetStudentWorkflowByStudentId(objbo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    hfStudentWorkflowId.Value = ds.Tables[0].Rows[0]["StudentId"].ToString();
                    ddlPersonalInformationId.SelectedValue = ds.Tables[0].Rows[0]["PersonalInformationId"].ToString();
                    if (ddlPersonalInformationId.SelectedValue == "1")
                    {
                        lblremarks.Visible = true;
                        txtremarks.Visible = true;
                        txtremarks.Text = ds.Tables[0].Rows[0]["PersonalInformationRemarks"].ToString();
                    }
                    ddlAddressId.SelectedValue = ds.Tables[0].Rows[0]["AddressId"].ToString();
                    if (ddlAddressId.SelectedValue == "1")
                    {
                        lblAddressRemarks.Visible = true;
                        txtAddressRemarks.Visible = true;
                        txtAddressRemarks.Text = ds.Tables[0].Rows[0]["AddressRemarks"].ToString();
                    }
                    ddlDocumentId.SelectedValue = ds.Tables[0].Rows[0]["DocumentId"].ToString();
                    if (ddlDocumentId.SelectedValue == "1")
                    {
                        lblDocumentRemarks.Visible = true;
                        txtDocumentRemarks.Visible = true;
                        txtDocumentRemarks.Text = ds.Tables[0].Rows[0]["DocumentRemarks"].ToString();
                    }
                    ddlFamilyMemberId.SelectedValue = ds.Tables[0].Rows[0]["FamilyMemberId"].ToString();
                    if (ddlFamilyMemberId.SelectedValue == "1")
                    {
                        lblFamilyMemberRemarks.Visible = true;
                        txtFamilyMemberRemarks.Visible = true;
                        txtFamilyMemberRemarks.Text = ds.Tables[0].Rows[0]["FamilyMemberRemarks"].ToString();
                    }
                    ddlAcademicId.SelectedValue = ds.Tables[0].Rows[0]["AcademicId"].ToString();
                    if (ddlAcademicId.SelectedValue == "1")
                    {
                        lblAcademicRemarks.Visible = true;
                        txtAcademicRemarks.Visible = true;
                        txtAcademicRemarks.Text = ds.Tables[0].Rows[0]["AcademicRemarks"].ToString();
                    }
                    ddlEducationDocId.SelectedValue = ds.Tables[0].Rows[0]["EducationDocId"].ToString();
                    if (ddlEducationDocId.SelectedValue == "1")
                    {
                        lblEducationDocRemarks.Visible = true;
                        txtEducationDocRemarks.Visible = true;
                        txtEducationDocRemarks.Text = ds.Tables[0].Rows[0]["EducationDocRemarks"].ToString();
                    }
                    ddlCoursesId.SelectedValue = ds.Tables[0].Rows[0]["CoursesId"].ToString();
                    if (ddlCoursesId.SelectedValue == "1")
                    {
                        lblCoursesRemarks.Visible = true;
                        txtCoursesRemarks.Visible = true;
                        txtCoursesRemarks.Text = ds.Tables[0].Rows[0]["CoursesRemarks"].ToString();
                    }
                    ddlOtherInfoId.SelectedValue = ds.Tables[0].Rows[0]["OtherInfoId"].ToString();
                    if (ddlOtherInfoId.SelectedValue == "1")
                    {
                        lblOtherInfoRemarks.Visible = true;
                        txtOtherInfoRemarks.Visible = true;
                        txtOtherInfoRemarks.Text = ds.Tables[0].Rows[0]["OtherInfoRemarks"].ToString();
                    }
                    ddllawId.SelectedValue = ds.Tables[0].Rows[0]["lawId"].ToString();
                    if (ddllawId.SelectedValue == "1")
                    {
                        lbllawReamrks.Visible = true;
                        txtlawReamrks.Visible = true;
                        txtlawReamrks.Text = ds.Tables[0].Rows[0]["lawReamrks"].ToString();
                    }
                    ddlEmergencyId.SelectedValue = ds.Tables[0].Rows[0]["EmergencyId"].ToString();
                    if (ddlEmergencyId.SelectedValue == "1")
                    {
                        lblEmergencyRemarks.Visible = true;
                        txtEmergencyRemarks.Visible = true;
                        txtEmergencyRemarks.Text = ds.Tables[0].Rows[0]["EmergencyRemarks"].ToString();
                    }
                    ddlReferencesId.SelectedValue = ds.Tables[0].Rows[0]["ReferencesId"].ToString();
                    if (ddlReferencesId.SelectedValue == "1")
                    {
                        lblReferencesRemarks.Visible = true;
                        txtReferencesRemarks.Visible = true;
                        txtReferencesRemarks.Text = ds.Tables[0].Rows[0]["ReferencesRemarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        protected void ddlAcademicId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAcademicId.SelectedValue == "1")
            {
                txtAcademicRemarks.Visible = true;
                lblAcademicRemarks.Visible = true;
                txtAcademicRemarks.Focus();
            }
            else
            {
                txtAcademicRemarks.Visible = false;
                lblAcademicRemarks.Visible = false;
            }
        }

        protected void ddlEducationDocId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEducationDocId.SelectedValue == "1")
            {
                txtEducationDocRemarks.Visible = true;
                lblEducationDocRemarks.Visible = true;
                txtEducationDocRemarks.Focus();
            }
            else
            {
                txtEducationDocRemarks.Visible = false;
                lblEducationDocRemarks.Visible = false;
            }
        }

        protected void ddlCoursesId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCoursesId.SelectedValue == "1")
            {
                txtCoursesRemarks.Visible = true;
                lblCoursesRemarks.Visible = true;
                txtCoursesRemarks.Focus();
            }
            else
            {
                txtCoursesRemarks.Visible = false;
                lblCoursesRemarks.Visible = false;
            }
        }

        protected void ddlOtherInfoId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOtherInfoId.SelectedValue == "1")
            {
                txtOtherInfoRemarks.Visible = true;
                lblOtherInfoRemarks.Visible = true;
                txtOtherInfoRemarks.Focus();
            }
            else
            {
                txtOtherInfoRemarks.Visible = false;
                lblOtherInfoRemarks.Visible = false;
            }
        }

        protected void ddllawId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddllawId.SelectedValue == "1")
            {
                txtlawReamrks.Visible = true;
                lbllawReamrks.Visible = true;
                txtlawReamrks.Focus();
            }
            else
            {
                txtlawReamrks.Visible = false;
                lbllawReamrks.Visible = false;
            }
        }

        protected void ddlEmergencyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmergencyId.SelectedValue == "1")
            {
                txtEmergencyRemarks.Visible = true;
                lblEmergencyRemarks.Visible = true;
                txtEmergencyRemarks.Focus();
            }
            else
            {
                txtEmergencyRemarks.Visible = false;
                lblEmergencyRemarks.Visible = false;
            }
        }

        protected void ddlReferencesId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReferencesId.SelectedValue == "1")
            {
                txtReferencesRemarks.Visible = true;
                lblReferencesRemarks.Visible = true;
                txtReferencesRemarks.Focus();
            }
            else
            {
                txtReferencesRemarks.Visible = false;
                lblReferencesRemarks.Visible = false;
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
                if (Status == "Accept")
                {
                    strBody = "Dear " + FullName.ToString() + "," +
                                       "<br/><br/> Course Name : " + CourseName +
                                       "<br/><br/> Application Confirmation Number : " + ApplicationNumber +
                                       "<br/><br/> Your Application Confirmation Status :<b> " + Status + "</b>." +
                                       "<br/><br/> Click Link for Payment :<a href='" + ConfigDetailsValue.StudentLogInLink + "' target='_blank'> Payment</a>." +
                                       "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                                       "<br/><br/> Regards," +
                                       "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                }
                else
                {

                    strBody = "Dear " + FullName.ToString() + "," +
                                       "<br/><br/> Course Name : " + CourseName +
                                       "<br/><br/> Application Confirmation Number : " + ApplicationNumber +
                                       "<br/><br/> Your Application Confirmation Status :<b> " + Status + "</b>." +
                                       "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                                       "<br/><br/> Regards," +
                                       "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
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

        protected void btnreject_Click(object sender, EventArgs e)
        {
            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                string username = SessionWrapper.UserDetails.UserName;
                long Id = Convert.ToInt64(hfStudentWorkflowId.Value);
                int hfid = Convert.ToInt32(hfStudentWorkflowId.Value);
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.StudentId == Id).FirstOrDefault();

                StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL();
                objData.InsertWorkflowRecordstudent((int)data.StudentId, (int)data.CourseId, 5, hfid, username);
                objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Reject");
                //BindPageViewData();
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Reject");
                string dobFilePath = ResolveUrl("~/Admin/Student/StudentApplyList");
                Page.Response.Write(@"<script language='javascript'>alert('Record rejected successfully.');document.location.href='" + dobFilePath + "'; </script>");

            }
        }
    }
}