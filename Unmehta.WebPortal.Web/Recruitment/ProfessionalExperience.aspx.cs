using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class ProfessionalExperience : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SessionWrapper.UserDetails.UserName))
                {
                    Response.Redirect("~/Recruitment/Careers");
                }
                //string strEndQueryString = "Sm9iSWQ9MXxSZWdJZD0yMDIxMDYzMDA3MzU0OHxDYW5kSWQ9MQ%3d%3d";
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Recruitment/Careers");
                }

                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();
                if (strQuery.Count() == 3)
                {
                    strJobId = strQuery[0].ToString().Replace("JobId=", "");
                    strRegId = strQuery[1].ToString().Replace("RegId=", "");
                    strCandId = strQuery[2].ToString().Replace("CandId=", "");
                }
                else
                {
                    Response.Redirect("~/Recruitment/Careers");
                }
                if (!IsPostBack)
                {
                    //cfValidatorFromDateExp.ValueToCompare = DateTime.Now.ToShortDateString();
                    BindGridView();
                    BindCoursesGridView();
                    FillExtraExperienceDetails();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                Response.Redirect("~/Recruitment/Careers");
            }
        }
        #endregion

        #region Common Function
        private void ClearControlValues()
        {
            hfExperienceID.Value = "0";
            hfFilName.Value = "";
            txtOrganizationName.Text = "";
            txtOrganizationAddress.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtDesignation.Text = "";
            txtReportingAuthority.Text = "";
            txtReasonForChange.Text = "";
            txtSalaryPM.Text = "";
            txtDepartment.Text = "";
            btnAddExperience.Text = "Add Experience";
            BindGridView();
        }
        private void ClearCoursesControlValues()
        {
            hfCoursesId.Value = "0";
            txtSubjectCourseTitle.Text = "";
            txtDurationYear.Text = "";
            txtOrganizingInstitution.Text = "";
            txtLocation.Text = "";
            btnAddCourses.Text = "Add Courses";
            BindCoursesGridView();
        }
        private void BindGridView()
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<CandidateExperienceDetailsModel> lstData = new List<CandidateExperienceDetailsModel>();
                    lstData = objCandidateDetailsRepository.GetAllCandidateExperienceDetailsByCandId(Convert.ToInt64(strCandId));
                    gvExperience.DataSource = lstData;
                    gvExperience.DataBind();

                    foreach (GridViewRow row in gvExperience.Rows)
                    {
                        Session["ToDate"] = Convert.ToString(gvExperience.Rows[row.RowIndex].Cells[4].Text);
                        if (row.RowIndex > 0)
                        {

                        }
                    }

                    if (lstData.Count > 0 && lstData != null)
                    {
                        GetCandidateExperienceTotalModel totalExperience = new GetCandidateExperienceTotalModel();
                        totalExperience = objCandidateDetailsRepository.GetTblCandidateExperienceTotalDetailsByCandId(Convert.ToInt64(strCandId));
                        string strYears = Convert.ToString(totalExperience.Years) + " Years ";
                        string strMonths = Convert.ToString(totalExperience.Months) + " Months ";
                        string strDays = Convert.ToString(totalExperience.Days) + " Days";
                        lblCount.Text = "Total No Of Experience : " + strYears + strMonths + strDays;
                        lblCount.Visible = true;
                        long MinExperience = Convert.ToInt64(SessionWrapper.UserDetails.PostMinExperiance);
                        if (totalExperience.Years >= MinExperience)
                        {
                            btnNext.Visible = true;
                        }
                        else
                        {
                            btnNext.Visible = false;
                        }
                    }
                    else
                    {
                        lblCount.Visible = false;
                    }
                }

                using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                {
                    ddlPostType.DataSource = objEducationQualificationRepository.GetAllPostTypeMasterDetails();
                    ddlPostType.DataTextField = "PostName";
                    ddlPostType.DataValueField = "Id";
                    ddlPostType.DataBind();
                    ddlPostType.Items.Insert(0, "Select");
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        private void BindCoursesGridView()
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<CandidateCourseDetailsModel> lstData = new List<CandidateCourseDetailsModel>();
                    lstData = objCandidateDetailsRepository.GetAllCandidateCourseDetailsByCandId(Convert.ToInt64(strCandId));
                    gvCourses.DataSource = lstData;
                    gvCourses.DataBind();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        private void FillExtraExperienceDetails()
        {
            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateDetailsForExperienceModel objBO = new CandidateDetailsForExperienceModel();
                objBO = objCandidateDetailsRepository.GetCandidateDetailsForExtraExperienceByCanId(Convert.ToInt64(strCandId));
                if (objBO != null)
                {
                    txtLastEmploymentDescription.Text = objBO.RoleInLastEmployment;
                    txtPresentSalary.Text = Convert.ToString(objBO.CurrentSalaryPerMonth);
                    txtExpectedSalary.Text = Convert.ToString(objBO.ExpectedSalary);
                }
            }
        }
        #endregion

        #region Save || Update || Cancel For Experience
        public static bool checkValidDate(string fromDate, string toDate)
        {
            bool result = true;
            if (fromDate != null)
            {
                int fromDateValues = Convert.ToInt32(fromDate.Replace("/", "").Replace("-", ""));
                int toDateValues = Convert.ToInt32(toDate.Replace("/", "").Replace("-", ""));
                result = fromDateValues <= toDateValues;
            }
            return result;
        }
        private bool LoadControlsExperience(CandidateExperienceDetailsModel objBo)
        {
            //if(checkValidDate(txtFromDate.Text, Session["ToDate"].ToString()) == true)
            //{

            //}
            objBo.CandidateId = Convert.ToInt64(strCandId);
            objBo.PostTypeId = Convert.ToInt32(ddlPostType.SelectedValue);
            if (!string.IsNullOrEmpty(txtOrganizationName.Text))
                objBo.OrganizationName = txtOrganizationName.Text;
            if (!string.IsNullOrEmpty(txtOrganizationAddress.Text))
                objBo.OrganizationAddress = txtOrganizationAddress.Text;
            if (!string.IsNullOrEmpty(txtFromDate.Text))
                objBo.FromDate = Convert.ToDateTime(txtFromDate.Text);
            if (!string.IsNullOrEmpty(txtToDate.Text))
                objBo.ToDate = Convert.ToDateTime(txtToDate.Text);
            if (!string.IsNullOrEmpty(txtDesignation.Text))
                objBo.Designation = txtDesignation.Text;
            if (!string.IsNullOrEmpty(txtReportingAuthority.Text))
                objBo.ReportingAuthority = txtReportingAuthority.Text;
            if (!string.IsNullOrEmpty(txtReasonForChange.Text))
                objBo.ReasonForChange = txtReasonForChange.Text;
            if (!string.IsNullOrEmpty(txtSalaryPM.Text))
                objBo.SalaryPerMonth = Convert.ToDecimal(txtSalaryPM.Text);
            if (!string.IsNullOrEmpty(txtDepartment.Text))
                objBo.DepartmentName = txtDepartment.Text;

            if (objBo.FromDate > objBo.ToDate)
            {
                Functions.MessagePopup(this, "From date less then To date.", PopupMessageType.error);
                return false;

            }

            objBo.JobType = 1;

            if (string.IsNullOrWhiteSpace(hfExperienceID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfExperienceID.Value);
            }
            return true;
        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }
        protected void btnAddExperience_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateExperienceDetailsModel objBO = new CandidateExperienceDetailsModel();
                    if (fuFileUpload.HasFile)
                    {
                        string filePath = ConfigDetailsValue.CandidateExperienceCertificate;

                        if (!filePath.Contains("|"))
                        {
                            objBO.ExperienceCertificateFileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                            objBO.ExperienceCertificateFilePath = filePath + "/" + objBO.ExperienceCertificateFileName;
                            bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                            if (!exists)
                                System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                            // Create the path and file name to check for duplicates.
                            var pathToCheck1 = "/" + filePath + objBO.ExperienceCertificateFileName;

                            // Create a temporary file name to use for checking duplicates.
                            //var tempfileName1 = "";
                            string fileName = Path.GetFileName(objBO.ExperienceCertificateFileName);
                            FileInfo fi = new FileInfo(fileName);
                            string ext = fi.Extension.ToLower();
                            if (ext == ".pdf")
                            {
                                // Check to see if a file already exists with the
                                // same name as the file to upload.
                                if (File.Exists(Server.MapPath(pathToCheck1)))
                                {
                                    File.Delete(pathToCheck1);
                                }

                                //Save selected file into specified location
                                fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + objBO.ExperienceCertificateFileName);
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Upload only .pdf extention file.", PopupMessageType.warning);
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                            return;
                        }
                    }
                    else
                    {
                        objBO.ExperienceCertificateFileName = hfFilName.Value;
                        objBO.ExperienceCertificateFilePath = ConfigDetailsValue.CandidateExperienceCertificate + hfFilName.Value;
                    }
                    if (LoadControlsExperience(objBO))
                    {
                        if (!objCandidateDetailsRepository.InsertOrUpdateTblCandidateExperienceDetails(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        rfvFileUpload.Enabled = true;
                        revFileUpload.Enabled = true;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtOrganizationName.ClientID + "').focus();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                txtOrganizationName.Text = gvExperience.Rows[rowindex].Cells[1].Text.Trim();
                txtOrganizationAddress.Text = gvExperience.Rows[rowindex].Cells[2].Text.Trim();
                txtFromDate.Text = gvExperience.Rows[rowindex].Cells[3].Text.Trim();
                txtToDate.Text = gvExperience.Rows[rowindex].Cells[4].Text.Trim();
                txtDesignation.Text = gvExperience.Rows[rowindex].Cells[6].Text.Trim();
                txtReportingAuthority.Text = gvExperience.Rows[rowindex].Cells[7].Text.Trim();
                txtReasonForChange.Text = gvExperience.Rows[rowindex].Cells[8].Text.Trim();
                txtSalaryPM.Text = gvExperience.Rows[rowindex].Cells[9].Text.Trim();
                txtDepartment.Text = gvExperience.Rows[rowindex].Cells[11].Text.Trim();
                ddlPostType.SelectedValue = gvExperience.DataKeys[rowindex]["PostTypeId"].ToString();
                hfExperienceID.Value = gvExperience.DataKeys[rowindex]["Id"].ToString();
                hfFilName.Value = gvExperience.DataKeys[rowindex]["ExperienceCertificateFileName"].ToString();
                rfvFileUpload.Enabled = false;
                revFileUpload.Enabled = false;
                btnAddExperience.Text = "Update Experience";
                ShowHideControl(VisibityType.Edit);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtOrganizationName.ClientID + "').focus();", true);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvExperience.DataKeys[rowindex]["Id"].ToString());
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    objCandidateDetailsRepository.RemoveTblCandidateExperienceDetails(rowId, out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion

        #region Save || Update || Cancel For Courses
        private bool LoadControlsCourses(CandidateCourseDetailsModel objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            if (!string.IsNullOrEmpty(txtSubjectCourseTitle.Text))
                objBo.CourseTitle = txtSubjectCourseTitle.Text;
            if (!string.IsNullOrEmpty(txtDurationYear.Text))
                objBo.Duration = txtDurationYear.Text;
            if (!string.IsNullOrEmpty(txtOrganizingInstitution.Text))
                objBo.InstituteName = txtOrganizingInstitution.Text;
            if (!string.IsNullOrEmpty(txtLocation.Text))
                objBo.City = txtLocation.Text;
            objBo.IsVisible = true;

            if (string.IsNullOrWhiteSpace(hfCoursesId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfCoursesId.Value);
            }
            return true;
        }
        protected void btnAddCourses_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateCourseDetailsModel objBO = new CandidateCourseDetailsModel();
                    if (LoadControlsCourses(objBO))
                    {
                        if (!objCandidateDetailsRepository.InsertOrUpdateTblCandidateCourseDetails(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        ClearCoursesControlValues();
                        BindCoursesGridView();
                        ShowHideCoursesControl(VisibityType.GridView);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnEditCourses_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                txtSubjectCourseTitle.Text = gvCourses.Rows[rowindex].Cells[1].Text.Trim();
                txtDurationYear.Text = gvCourses.Rows[rowindex].Cells[2].Text.Trim();
                txtOrganizingInstitution.Text = gvCourses.Rows[rowindex].Cells[3].Text.Trim();
                txtLocation.Text = gvCourses.Rows[rowindex].Cells[4].Text.Trim();
                hfCoursesId.Value = gvCourses.DataKeys[rowindex]["Id"].ToString();
                btnAddCourses.Text = "Update Courses";
                ShowHideCoursesControl(VisibityType.Edit);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void lbtnDeleteCourses_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvCourses.DataKeys[rowindex]["Id"].ToString());
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    objCandidateDetailsRepository.RemoveTblCandidateCourseDetails(rowId, out errorMessage);
                    ClearCoursesControlValues();
                    BindCoursesGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.Insert:
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    break;
                case VisibityType.SaveAndAdd:
                    ClearControlValues();
                    break;
                default:
                    break;
            }
        }

        private void ShowHideCoursesControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.Insert:
                    ClearCoursesControlValues();
                    break;
                case VisibityType.Edit:
                    break;
                case VisibityType.SaveAndAdd:
                    ClearCoursesControlValues();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Final Submit 
        private bool LoadControlsUpdateExperience(CandidateDetailsForExperienceModel objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            if (!string.IsNullOrEmpty(txtLastEmploymentDescription.Text))
                objBo.RoleInLastEmployment = txtLastEmploymentDescription.Text;
            if (!string.IsNullOrEmpty(txtPresentSalary.Text))
                objBo.CurrentSalaryPerMonth = Convert.ToDecimal(txtPresentSalary.Text);
            if (!string.IsNullOrEmpty(txtExpectedSalary.Text))
                objBo.ExpectedSalary = Convert.ToDecimal(txtExpectedSalary.Text);
            return true;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateDetailsForExperienceModel objBO = new CandidateDetailsForExperienceModel();
                    if (LoadControlsUpdateExperience(objBO))
                    {
                        if (!objCandidateDetailsRepository.UpdateCandidateDetailsForExperience(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                            txtLastEmploymentDescription.Text = "";
                            txtPresentSalary.Text = "";
                            txtExpectedSalary.Text = "";
                        }
                    }
                }
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
                Response.Redirect("~/Recruitment/ExtraInfo?" + strEndQueryString);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void btnPrivious_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/AcademicDetails?" + strEndQueryString);
        }
        #endregion        
    }
}