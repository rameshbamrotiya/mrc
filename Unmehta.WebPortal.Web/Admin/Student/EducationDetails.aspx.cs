using BAL;
using BAL.Admission;
using BO;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class EducationDetails : System.Web.UI.Page
    {
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strCourseName;
        //public static string strRegistrationId;
        //public static string strEndQueryString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strStudentId;
                string strCourseId;
                string strCourseName;
                string strRegistrationId;
                string strEndQueryString;
                strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admin/Student/StudentDetails.aspx");
                }
                strCourseName = "";
                strStudentId = "";
                strCourseId = "";
                strRegistrationId = "";
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();
                if (strQuery.Count() == 4)
                {
                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                    strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
                }

                // BindEducationType();
                // BindEducationName();
                // BindYear();
                FillAllCheckBox();
                BindCoursesGridView();
                BindAcademicsData();
                BindAcademicsDocDetails();
                FillExtraDetails();
                StudentAcademicsBO objBO = new StudentAcademicsBO();
                StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                objBO.StudentId = Convert.ToInt32(strStudentId);
                objBO.CourseId = Convert.ToInt32(strCourseId);
                DataSet ds = objBAL.selectMenu(objBO);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillLanguageCheckBOx(ds.Tables[0]);
                }
            }
        }
        protected void FillExtraDetails()
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            StudentAcademicsBO objBO = new StudentAcademicsBO();
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            DataSet ds = objBAL.selectExtraDetails(objBO);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                ddlcomputerlit.SelectedValue = ds.Tables[0].Rows[0]["IsComputerLiteracy"].ToString() == "False" ? "0" : "1";
                txtremerks.InnerText = ds.Tables[0].Rows[0]["ComputerDetails"].ToString();
            }
        }
        private void FillAllCheckBox()
        {
            try
            {
                {
                    AcademicDetailsBAL objBAL = new AcademicDetailsBAL();
                    DataSet ds = objBAL.SelectMenuResourceWise();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var dtView = ds.Tables[0].DefaultView;
                        rptUserRights.DataSource = dtView;
                        rptUserRights.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void FillLanguageCheckBOx(DataTable dataTable)
        {
            try
            {
                foreach (RepeaterItem repeated in rptUserRights.Items)
                {
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk1"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk2"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk3"));
                    BindCheckBox(repeated, dataTable);
                }
            }

            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        private void BindCheckBox(RepeaterItem repeated, DataTable dataTable)
        {
            try
            {
                var filteredTable = new DataTable();
                var lblLanguageid =
                     (Label)FindControlRecursive(repeated, "lblLanguageID");
                if (lblLanguageid != null)
                {
                    var dvView = dataTable.DefaultView;
                    dvView.RowFilter = "LanguageID =" + Convert.ToInt32(lblLanguageid.Text.Trim());
                    filteredTable = dvView.ToTable();
                }
                if (filteredTable.Rows.Count > 0)
                {
                    var chkRead =
                           (CheckBox)FindControlRecursive(repeated, "chk1");
                    if (chkRead != null)
                    {
                        chkRead.Checked = (bool)filteredTable.Rows[0]["Read"];
                    }
                    var chkWrite =
                        (CheckBox)FindControlRecursive(repeated, "chk2");
                    if (chkWrite != null)
                    {
                        chkWrite.Checked = (bool)filteredTable.Rows[0]["Write"];
                    }
                    var ChkSpeak =
                        (CheckBox)FindControlRecursive(repeated, "chk3");
                    if (ChkSpeak != null)
                    {
                        ChkSpeak.Checked = (bool)filteredTable.Rows[0]["Speak"];
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        //private void BindEducationType()
        //{
        //    StudentAcademicsBAL objBal = new StudentAcademicsBAL();
        //    ddlEductionType.DataSource = objBal.GetEducationType();
        //    ddlEductionType.DataTextField = "TypeName";
        //    ddlEductionType.DataValueField = "Id";
        //    ddlEductionType.DataBind();
        //    ddlEductionType.Items.Insert(0, new ListItem("Select", "-1"));

        //}
        private void BindAcademicsDocDetails()
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            StudentAcademicsDocumentBO objBO = new StudentAcademicsDocumentBO();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetAllAcademicDocDetails(objBO);
            gvieweducationDoc.DataSource = dt.Tables[0];
            gvieweducationDoc.DataBind();
        }
        //private void BindEducationName()
        //{
        //    StudentAcademicsBAL objBal = new StudentAcademicsBAL();
        //    DataTable dt = objBal.SelectRecordEducationName();
        //    if (dt != null)
        //    {
        //        Functions.GetDatatableWithWhereCondition(dt, "IsVisible=True", out dt);
        //        Functions.GetDatatableWithWhereCondition(dt, "EducationType=" + ddlEductionType.SelectedValue, out dt);
        //    }
        //    ddlEducationName.DataSource = dt;
        //    ddlEducationName.DataTextField = "EducationDetailName";
        //    ddlEducationName.DataValueField = "Id";
        //    ddlEducationName.DataBind();
        //    ddlEducationName.Items.Insert(0, new ListItem("Select", "-1"));

        //}
        //private void BindYear()
        //{
        //    ddlAcademicYear.Items.Clear();
        //    ddlAcademicYear.Items.Insert(0, new ListItem("Select Year", "-1"));
        //    for (int i = 1960; i <= DateTime.Now.Year; i++)
        //    {
        //        ddlAcademicYear.Items.Add(i.ToString());
        //    }
        //}
        //private void LoadControlsAdd(StudentAcademicsBO objBo)
        //{
        //    if (hdnAcademicID.Value.ToString() != string.Empty)
        //    {
        //        objBo.Id = Convert.ToInt16(hdnAcademicID.Value.ToString());
        //    }
        //    else
        //    {
        //        objBo.Id = 0;
        //    }
        //    objBo.StudentId = Convert.ToInt32(strStudentId);
        //    objBo.CourseId = Convert.ToInt32(strCourseId);
        //    if (!string.IsNullOrEmpty(ddlEductionType.SelectedValue))
        //        objBo.EducationTypeId = Convert.ToInt16(ddlEductionType.SelectedValue.ToString());
        //    if (!string.IsNullOrEmpty(ddlEducationName.SelectedValue))
        //        objBo.EducationId = Convert.ToInt16(ddlEducationName.SelectedValue.ToString());

        //    if (!string.IsNullOrEmpty(txtschoolname.Text))
        //        objBo.NameOfSchoolCollege = txtschoolname.Text;

        //    if (!string.IsNullOrEmpty(txtboard.Text))
        //        objBo.BoardUniversity = txtboard.Text;

        //    if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
        //        objBo.PassingYear = ddlAcademicYear.SelectedValue;

        //    if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
        //        objBo.PassingMonth = ddlMonth.SelectedValue;

        //    if (!string.IsNullOrEmpty(ddlStream.SelectedValue))
        //        objBo.Stream = ddlStream.SelectedValue.ToString();

        //    if (!string.IsNullOrEmpty(ddlNoOfTrials.SelectedValue))
        //        objBo.NoOfTrials = Convert.ToInt32(ddlNoOfTrials.SelectedValue.ToString());

        //    if (!string.IsNullOrEmpty(txtdivision.Text))
        //        objBo.PercentageOrPercentile = txtdivision.Text;
        //    objBo.Division = txtdivision.Text;
        //    objBo.UserName = "Admin";



        //}
        //protected void btnKnowPersonDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string errorMessage = "";
        //        if (string.IsNullOrWhiteSpace(txtboard.Text.Trim()))
        //        {
        //            Functions.MessagePopup(this, "Please Enter Board/University.", PopupMessageType.warning);
        //        }

        //        if (string.IsNullOrWhiteSpace(txtschoolname.Text.Trim()))
        //        {
        //            Functions.MessagePopup(this, "Please Enter Name of School/Colege.", PopupMessageType.warning);
        //        }
        //        if (string.IsNullOrWhiteSpace(txtdivision.Text.Trim()))
        //        {
        //            Functions.MessagePopup(this, "Please Enter Division & %.", PopupMessageType.warning);
        //        }
        //        StudentAcademicsBO objbo = new StudentAcademicsBO();
        //        LoadControlsAdd(objbo);
        //        if (new StudentAcademicsBAL().InsertOrUpdateAcademicsDetails(objbo))
        //        {
        //            BindAcademicsData();
        //            clearAcademicDetails();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //    }
        //}
        private void BindAcademicsData()
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            StudentAcademicsBO objBO = new StudentAcademicsBO();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetAllAcademicDetails(objBO);
            gvAcademicDetails.DataSource = dt.Tables[0];
            gvAcademicDetails.DataBind();

        }
        //protected void ddlEductionType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindEducationName();
        //}
        private void BindCoursesGridView()
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            StudentExtraCertificationBO objBO = new StudentExtraCertificationBO();
            objBO.CandidateId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetExtraCertificationDetailsByID(objBO);
            gvCourses.DataSource = dt.Tables[0];
            gvCourses.DataBind();
        }
        //protected void btnAddCourses_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        StudentExtraCertificationBO objBO = new StudentExtraCertificationBO();
        //        LoadControlsCourses(objBO);
        //        if (new StudentAcademicsBAL().InsertOrUpdateExtraCertificationDetails(objBO))
        //        {
        //            BindCoursesGridView();
        //            clearCoursesDetails();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //    }


        //}
        //private bool LoadControlsCourses(StudentExtraCertificationBO objBo)
        //{
        //    objBo.CandidateId = Convert.ToInt32(strStudentId);
        //    objBo.CourseId = Convert.ToInt32(strCourseId);
        //    if (!string.IsNullOrEmpty(txtSubjectCourseTitle.Text))
        //        objBo.CourseTitle = txtSubjectCourseTitle.Text;
        //    if (!string.IsNullOrEmpty(txtDurationYear.Text))
        //        objBo.Duration = txtDurationYear.Text;
        //    if (!string.IsNullOrEmpty(txtOrganizingInstitution.Text))
        //        objBo.InstituteName = txtOrganizingInstitution.Text;
        //    if (!string.IsNullOrEmpty(txtLocation.Text))
        //        objBo.CITY = txtLocation.Text;
        //    objBo.IsVisible = true;
        //    objBo.UserName = "Admin";
        //    if (string.IsNullOrWhiteSpace(hdnCourses.Value))
        //    {
        //        objBo.Id = 0;
        //    }
        //    else
        //    {
        //        objBo.Id = Convert.ToInt32(hdnCourses.Value);
        //    }
        //    return true;
        //}
        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            return root.Controls.Cast<Control>()
               .Select(c => FindControlRecursive(c, id))
               .FirstOrDefault(c => c != null);
        }
        private void SetLanguageParameter(RepeaterItem repeated, StudentLanguageBO objLanguage, int count)
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            objLanguage.RegisrationId = strRegistrationId;
            //objLanguage.Advertisementid = strJobId;
            objLanguage.CandidateId = Convert.ToInt32(strStudentId);
            objLanguage.CourseId = Convert.ToInt32(strCourseId);
            objLanguage.Userid = "admin";
            var lblLanguageID =
                      (Label)FindControlRecursive(repeated, "lblLanguageID");
            if (lblLanguageID != null)
            {
                objLanguage.LanguageID = Convert.ToInt32(lblLanguageID.Text);
            }
            var chkRead =
                       (CheckBox)FindControlRecursive(repeated, "chk1");
            if (chkRead != null)
            {
                objLanguage.CanRead = chkRead.Checked;
            }
            var chkWrite =
                (CheckBox)FindControlRecursive(repeated, "chk2");
            if (chkWrite != null)
            {
                objLanguage.Canwrite = chkWrite.Checked;
            }
            var chkSpeak =
                (CheckBox)FindControlRecursive(repeated, "chk3");
            if (chkSpeak != null)
            {
                objLanguage.CanSpeak = chkSpeak.Checked;
            }

            if (chkRead.Checked == true || chkSpeak.Checked == true || chkWrite.Checked == true)
            {
                count = count + 1;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            Response.Redirect("~/Admin/Student/ExtraInfofroAdmission.aspx?" + strEndQueryString);
            //try
            //{
            //    int count = 0;
            //    using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
            //    {
            //        StudentAcademicsBO objEducation = new StudentAcademicsBO();
            //        objEducation.StudentId = Convert.ToInt16(strStudentId);
            //        objEducation.CourseId = Convert.ToInt16(strCourseId);
            //        StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            //        DataSet ds = objBAL.GetAllEducationAcademicDetails(objEducation);
            //        DataTable dt = ds.Tables[0];
            //        using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            //        {
            //            var minRec = Functions.ToListof<StudentEducationTypeDetailBO>(objStudentAdvertisementBAL.GetAlltMinimumEducationTypeDetailsById(objEducation.CourseId)).ToList().Select(x => x.QualificationTypeId).ToList();
            //            var remain = dt.AsEnumerable().Select(x => x.Field<long>("EducationTypeId")).ToList().Distinct();

            //            var result = minRec.Where(myRow => !remain.Contains(myRow)).ToList().Distinct();

            //            List<StudentEducationTypeBO> lst = Functions.ToListof<StudentEducationTypeBO>(objStudentEducationQualificationBAL.GetAllEducationType());

            //            var rowName = lst.Where(x => result.Contains(x.Id)).Select(x => x.TypeName).ToList();

            //            string RemainEducation = string.Join(",", rowName.ToArray());

            //            if (!string.IsNullOrWhiteSpace(RemainEducation))
            //            {
            //                Functions.MessagePopup(this, "Please Enter Remain education details :" + RemainEducation, PopupMessageType.error);
            //                return;
            //            }
            //        }
            //    }
            //    int countI = 0;
            //    foreach (RepeaterItem repeated2 in rptUserRights.Items)
            //    {
            //        CheckBox chk1 = (repeated2.FindControl("chk1") as CheckBox);
            //        CheckBox chk2 = (repeated2.FindControl("chk2") as CheckBox);
            //        CheckBox chk3 = (repeated2.FindControl("chk3") as CheckBox);
            //        if (chk1.Checked)
            //        {
            //            countI++;
            //        }
            //        if (chk2.Checked)
            //        {
            //            countI++;
            //        }
            //        if (chk3.Checked)
            //        {
            //            countI++;
            //        }

            //    }                
            //    if (countI > 0)
            //    {
            //        StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            //        StudentLanguageBO objLanguage = new StudentLanguageBO();
            //        foreach (RepeaterItem repeated2 in rptUserRights.Items)
            //        {

            //            SetLanguageParameter(repeated2, objLanguage, count);
            //            if (objBAL.InsertLanguageDetails(objLanguage))
            //            {
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Functions.MessagePopup(this, "Please select one checkbox check in Language section.", PopupMessageType.error);
            //        return;
            //    }

            //    StudentAcademicsBO objBO = new StudentAcademicsBO();
            //    objBO.Id = Convert.ToInt32(strStudentId.ToString());
            //    objBO.CourseId = Convert.ToInt32(strCourseId.ToString());
            //    objBO.IsComputerLiteracy = (ddlcomputerlit.SelectedValue == "1" ? true : false); ;
            //    objBO.ComputerDetails = txtremerks.InnerText.ToString();
            //    if (new StudentAcademicsBAL().UpdateCoputerDetails(objBO))
            //    {
            //        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
            //        Response.Redirect("~/Admin/Student/ExtraInfofroAdmission.aspx?" + strEndQueryString);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            //    ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            //}
        }
        //private void clearAcademicDetails()
        //{
        //    BindEducationType();
        //    BindEducationName();
        //    ddlMonth.SelectedIndex = -1;
        //    BindYear();
        //    txtschoolname.Text = "";
        //    txtboard.Text = "";
        //    ddlStream.SelectedValue = "-1";
        //    ddlNoOfTrials.SelectedValue = "-1";
        //    txtdivision.Text = "";
        //    btnKnowPersonDetails.Text = "Add Details";
        //    hdnAcademicID.Value = "";
        //}
        //protected void lnkbtnAcademics_Click(object sender, EventArgs e)
        //{
        //    int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //    hdnAcademicID.Value = gvAcademicDetails.DataKeys[rowindex]["Id"].ToString();
        //    ddlEductionType.SelectedValue = gvAcademicDetails.DataKeys[rowindex]["EducationTypeId"].ToString();
        //    BindEducationName();
        //    ddlEducationName.SelectedValue = gvAcademicDetails.DataKeys[rowindex]["EducationId"].ToString();
        //    txtschoolname.Text = gvAcademicDetails.Rows[rowindex].Cells[4].Text;
        //    txtboard.Text = gvAcademicDetails.Rows[rowindex].Cells[5].Text;
        //    ddlMonth.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[6].Text;
        //    ddlAcademicYear.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[7].Text;
        //    ddlStream.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[8].Text;
        //    txtdivision.Text = gvAcademicDetails.Rows[rowindex].Cells[9].Text;
        //    ddlNoOfTrials.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[10].Text;
        //    btnKnowPersonDetails.Text = "Update Details";
        //}

        //protected void lnkbtnDeleteAcademics_Click(object sender, EventArgs e)
        //{
        //    string errorMessage = "";
        //    try
        //    {
        //        int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //        int rowId = Convert.ToInt32(gvAcademicDetails.DataKeys[rowindex]["Id"].ToString());
        //        StudentAcademicsBO objbo = new StudentAcademicsBO();
        //        StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
        //        objbo.Id = Convert.ToInt32(rowId);
        //        objbo.UserName = "Admin";
        //        if (objBAL.DeleteRecord(objbo))
        //        {
        //            Functions.MessagePopup(this, "Academic Details Delete Successfully .....!", PopupMessageType.success);
        //            BindAcademicsData();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlEductionType.ClientID + "').focus();", true);
        //        }
        //        else
        //        {
        //            Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
        //            BindAcademicsData();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlEductionType.ClientID + "').focus();", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}
        //private void clearCoursesDetails()
        //{
        //    txtSubjectCourseTitle.Text = "";
        //    txtDurationYear.Text = "";
        //    txtOrganizingInstitution.Text = "";
        //    txtLocation.Text = "";
        //    btnAddCourses.Text = "Add Courses";
        //    hdnCourses.Value = "";
        //}
        //protected void lbtnEditCourses_Click(object sender, EventArgs e)
        //{
        //    int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //    hdnCourses.Value = gvCourses.DataKeys[rowindex]["Id"].ToString();
        //    txtSubjectCourseTitle.Text = gvCourses.Rows[rowindex].Cells[1].Text;
        //    txtDurationYear.Text = gvCourses.Rows[rowindex].Cells[2].Text;
        //    txtOrganizingInstitution.Text = gvCourses.Rows[rowindex].Cells[3].Text;
        //    txtLocation.Text = gvCourses.Rows[rowindex].Cells[4].Text;
        //    btnAddCourses.Text = "Update Courses";
        //}

        //protected void lbtnDeleteCourses_Click(object sender, EventArgs e)
        //{
        //    string errorMessage = "";
        //    try
        //    {
        //        int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //        int rowId = Convert.ToInt32(gvCourses.DataKeys[rowindex]["Id"].ToString());
        //        StudentAcademicsBO objbo = new StudentAcademicsBO();
        //        StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
        //        objbo.Id = Convert.ToInt32(rowId);
        //        objbo.UserName = "Admin";
        //        if (objBAL.DeleteCourseRecord(objbo))
        //        {
        //            Functions.MessagePopup(this, "Course Delete Successfully .....!", PopupMessageType.success);
        //            BindCoursesGridView();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
        //        }
        //        else
        //        {
        //            Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
        //            BindCoursesGridView();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            Response.Redirect("~/Admin/Student/StudentRegistration.aspx?" + strEndQueryString1);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strStudentId;
            string strCourseId;
            string strCourseName;
            string strRegistrationId;
            string strEndQueryString;
            strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admin/Student/StudentDetails.aspx");
            }
            strCourseName = "";
            strStudentId = "";
            strCourseId = "";
            strRegistrationId = "";
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            Response.Redirect("~/Admin/Student/BasicDetails?" + strEndQueryString);
        }
    }
}