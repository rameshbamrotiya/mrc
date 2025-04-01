using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL.Admission;
using BO.Admission;
using Unmehta.WebPortal.Common;
using System.IO;
using System.Globalization;
using System.Security.Policy;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class StudentAcademics : System.Web.UI.Page
    {
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strCourseName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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


                BindEducationType();
                BindEducationDocType();
                BindEducationName();
                BindYear();
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


            StudentAcademicsBO objBO = new StudentAcademicsBO();
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            DataSet ds = objBAL.selectExtraDetails(objBO);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                ddlcomputerlit.SelectedValue = ds.Tables[0].Rows[0]["IsComputerLiteracy"].ToString() == "False" ? "0" : "1";
            }
            StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
            StudentFamilyDetailsBAL objBAL1 = new StudentFamilyDetailsBAL();
            objbo.StudentId = Convert.ToInt32(strStudentId);
            objbo.CourseId = Convert.ToInt32(strCourseId);
            DataSet dsverification = objBAL1.GetStudentVerificationByStudentIdandCourseId(Convert.ToInt32(strStudentId), Convert.ToInt32(strCourseId));
            if (dsverification != null && dsverification.Tables[0].Rows.Count > 0)
            {
                string AcademicId = dsverification.Tables[0].Rows[0]["AcademicId"].ToString();
                if (AcademicId == "1")
                {
                    DivAcademicDetailsMain.Visible = true;
                    LblstatusAD.BackColor = System.Drawing.Color.Yellow;
                    LblstatusAD.Text = "Correction";
                    DivRemerksAD.Visible = true;
                    txtRemerksAD.Text = dsverification.Tables[0].Rows[0]["AcademicRemarks"].ToString();
                    PanelAcademicDetails.Enabled = true;
                }
                else if (AcademicId == "0")
                {
                    DivAcademicDetailsMain.Visible = true;
                    LblstatusAD.Text = "Approve";
                    LblstatusAD.ForeColor = System.Drawing.Color.Green;
                    DivRemerksAD.Visible = false;
                    PanelAcademicDetails.Enabled = false;
                }
                else
                {
                    PanelAcademicDetails.Enabled = true;
                }
                string EducationDocId = dsverification.Tables[0].Rows[0]["EducationDocId"].ToString();
                if (EducationDocId == "1")
                {
                    DivAcademicDocumentMain.Visible = true;
                    LblstatusADoc.BackColor = System.Drawing.Color.Yellow;
                    LblstatusADoc.Text = "Correction";
                    DivRemerksADoc.Visible = true;
                    txtRemerksADoc.Text = dsverification.Tables[0].Rows[0]["EducationDocRemarks"].ToString();
                    PanelAcademicDocument.Enabled = true;
                }
                else if (EducationDocId == "0")
                {
                    DivAcademicDocumentMain.Visible = true;
                    LblstatusADoc.ForeColor = System.Drawing.Color.Green;
                    LblstatusADoc.Text = "Approve";
                    DivRemerksADoc.Visible = false;
                    PanelAcademicDocument.Enabled = false;
                }
                else
                {
                    PanelAcademicDocument.Enabled = true;
                }
                string CoursesId = dsverification.Tables[0].Rows[0]["CoursesId"].ToString();
                if (CoursesId == "1")
                {
                    DivCoursesTrainingAttendedMain.Visible = true;
                    LblstatusCA.BackColor = System.Drawing.Color.Yellow;
                    LblstatusCA.Text = "Correction";
                    DivRemerksCA.Visible = true;
                    txtRemerksCA.Text = dsverification.Tables[0].Rows[0]["CoursesRemarks"].ToString();
                    PanelCoursesTrainingAttended.Enabled = true;
                }
                else if (CoursesId == "0")
                {
                    DivCoursesTrainingAttendedMain.Visible = true;
                    LblstatusCA.ForeColor = System.Drawing.Color.Green;
                    LblstatusCA.Text = "Approve";
                    DivRemerksCA.Visible = false;
                    PanelCoursesTrainingAttended.Enabled = false;
                }
                else
                {
                    PanelCoursesTrainingAttended.Enabled = true;
                }
            }
            else
            {
                PanelAcademicDetails.Enabled = true;
                DivAcademicDetailsMain.Visible = false;

                PanelAcademicDocument.Enabled = true;
                DivAcademicDocumentMain.Visible = false;

                PanelCoursesTrainingAttended.Enabled = true;
                DivCoursesTrainingAttendedMain.Visible = false;
            }
        }
        private void FillAllCheckBox()
        {
            try
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void BindEducationType()
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


            StudentAcademicsBAL objBal = new StudentAcademicsBAL();
            ddlEductionType.DataSource = objBal.GetEducationType(Convert.ToInt32(strCourseId));
            ddlEductionType.DataTextField = "TypeName";
            ddlEductionType.DataValueField = "Id";
            ddlEductionType.DataBind();
            ddlEductionType.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void BindEducationName()
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


            StudentAcademicsBAL objBal = new StudentAcademicsBAL();
            //DataTable dt = objBal.SelectRecordEducationName();
            DataTable dt = objBal.SelectRecordEducationNameByCourse(Convert.ToInt32(strCourseId));
            if (dt != null)
            {
                Functions.GetDatatableWithWhereCondition(dt, "IsVisible=True", out dt);
                Functions.GetDatatableWithWhereCondition(dt, "EducationType=" + ddlEductionType.SelectedValue, out dt);
            }
            ddlEducationName.DataSource = dt;
            ddlEducationName.DataTextField = "EducationDetailName";
            ddlEducationName.DataValueField = "Id";
            ddlEducationName.DataBind();
            ddlEducationName.Items.Insert(0, new ListItem("Select", "-1"));
        }
        private void BindYear()
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


            ddlAcademicYear.Items.Clear();
            ddlAcademicYear.Items.Insert(0, new ListItem("Select Year", "-1"));
            StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
            StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
            objbo.Id = Convert.ToInt64(strStudentId);
            objbo.CourseId = Convert.ToInt64(strCourseId);
            DataSet ds = objBAL.GetStudentRegistrationDetailsByStudentId(objbo);
            int year = 1960;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //DateTime Dob = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateOfBirth"].ToString());
                DateTime? dtStartDate = null;
                DateTime dStartDate = DateTime.ParseExact(ds.Tables[0].Rows[0]["DateOfBirth"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //dtStartDate = new DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day);
                //DateTime Dob = DateTime.ParseExact(ds.Tables[0].Rows[0]["DateOfBirth"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                year = dStartDate.Year + 10;
            }
            for (int i = year; i <= DateTime.Now.Year; i++)
            {
                ddlAcademicYear.Items.Add(i.ToString());
            }
        }
        private void LoadControlsAdd(StudentAcademicsBO objBo)
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


            if (hdnAcademicID.Value.ToString() != string.Empty)
            {
                objBo.Id = Convert.ToInt16(hdnAcademicID.Value.ToString());
            }
            else
            {
                objBo.Id = 0;
            }
            objBo.StudentId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt32(strCourseId);
            if (!string.IsNullOrEmpty(ddlEductionType.SelectedValue))
                objBo.EducationTypeId = Convert.ToInt16(ddlEductionType.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(ddlEducationName.SelectedValue))
                objBo.EducationId = Convert.ToInt16(ddlEducationName.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(txtschoolname.Text))
                objBo.NameOfSchoolCollege = txtschoolname.Text;

            if (!string.IsNullOrEmpty(txtboard.Text))
                objBo.BoardUniversity = txtboard.Text;

            if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
                objBo.PassingYear = ddlAcademicYear.SelectedValue;

            if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                objBo.PassingMonth = ddlMonth.SelectedValue;

            if (!string.IsNullOrEmpty(ddlStream.SelectedValue))
                objBo.Stream = ddlStream.SelectedValue.ToString();
            else
            {
                objBo.Stream = "";
            }
            if (!string.IsNullOrEmpty(ddlNoOfTrials.SelectedValue))
                objBo.NoOfTrials = Convert.ToInt32(ddlNoOfTrials.SelectedValue.ToString());
            string documentfile = string.Empty;
            documentfile = SaveFile();

            if (!string.IsNullOrEmpty(documentfile))
                objBo.MarksheetPath = documentfile;
            if (!string.IsNullOrEmpty(txtdivision.Text))
                objBo.PercentageOrPercentile = txtdivision.Text;
            objBo.Division = txtdivision.Text;
            objBo.UserName = SessionWrapper.StudentRegistration.Username;
        }
        private string SaveFile()
        {
            try
            {
                if (fumarksheet.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.MarksheetDoc;
                    var fname = Path.GetExtension(fumarksheet.FileName);
                    var count = fumarksheet.FileName.Split('.');
                    string type = "";
                    for (int i = 0; i < fumarksheet.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fumarksheet.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fumarksheet.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        protected void btnKnowPersonDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                if (string.IsNullOrEmpty(Fupathmarksheet.InnerText))
                {
                    if (!fumarksheet.HasFile)
                    {
                        Functions.MessagePopup(this, "Upload document in pdf.", PopupMessageType.warning);
                        return;
                    }
                    else
                    {
                        string ext = Path.GetExtension(fumarksheet.FileName);
                        if (ext.ToLower() != ".pdf")
                        {
                            Functions.MessagePopup(this, "Select only .pdf files.", PopupMessageType.warning);
                            return;
                        }
                    }

                }
                if (ddlEductionType.SelectedValue == "0")
                {
                    Functions.MessagePopup(this, "Select Education Type.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtboard.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Enter Board/University.", PopupMessageType.warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtschoolname.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Enter Name of School/Colege.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtdivision.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Enter percentage.", PopupMessageType.warning);
                    return;
                }
                if (ddlNoOfTrials.SelectedValue == "0")
                {
                    Functions.MessagePopup(this, "Select No Of Trials.", PopupMessageType.warning);
                    return;
                }
                if (ddlAcademicYear.SelectedValue == "1")
                {
                    Functions.MessagePopup(this, "Select Passing Year.", PopupMessageType.warning);
                    return;
                }
                if (ddlMonth.SelectedValue == "1")
                {
                    Functions.MessagePopup(this, "Select Passing Month.", PopupMessageType.warning);
                    return;
                }
                decimal number;
                if (decimal.TryParse(txtdivision.Text, out number))
                {
                    if (number <= 100)
                    {
                        //in range
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Enter percentage not in range.", PopupMessageType.warning);
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Enter valid percentage.", PopupMessageType.warning);
                    return;
                }
                StudentAcademicsBO objbo = new StudentAcademicsBO();
                LoadControlsAdd(objbo);
                if (new StudentAcademicsBAL().InsertOrUpdateAcademicsDetails(objbo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlEductionType.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists for " + ddlEductionType.SelectedItem + ".", PopupMessageType.warning);
                    return;
                }
                BindAcademicsData();
                clearAcademicDetails();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void BindAcademicsData()
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


            StudentAcademicsBO objBO = new StudentAcademicsBO();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetAllAcademicDetails(objBO);
            gvAcademicDetails.DataSource = dt.Tables[0];
            gvAcademicDetails.DataBind();

        }
        protected void ddlEductionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEducationName();
        }
        private void BindCoursesGridView()
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


            StudentExtraCertificationBO objBO = new StudentExtraCertificationBO();
            objBO.CandidateId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt64(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetExtraCertificationDetailsByID(objBO);
            gvCourses.DataSource = dt.Tables[0];
            gvCourses.DataBind();
        }
        protected void btnAddCourses_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(txtSubjectCourseTitle.Text.Trim()))
                    {
                        Functions.MessagePopup(this, "Enter Subject/Course Title.", PopupMessageType.warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtDurationYear.Text.Trim()))
                    {
                        Functions.MessagePopup(this, "Enter Duration/Year.", PopupMessageType.warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtOrganizingInstitution.Text.Trim()))
                    {
                        Functions.MessagePopup(this, "Enter Organizing Institution/Organization.", PopupMessageType.warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtLocation.Text.Trim()))
                    {
                        Functions.MessagePopup(this, "Enter Location.", PopupMessageType.warning);
                        return;
                    }
                    StudentExtraCertificationBO objBO = new StudentExtraCertificationBO();
                    LoadControlsCourses(objBO);
                    if (new StudentAcademicsBAL().InsertOrUpdateExtraCertificationDetails(objBO))
                    {
                        Functions.MessagePopup(this, "Courses/Training Attended Details Add Successfully.", PopupMessageType.success);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
                        BindCoursesGridView();
                        clearCoursesDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private bool LoadControlsCourses(StudentExtraCertificationBO objBo)
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


            objBo.CandidateId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt64(strCourseId);
            if (!string.IsNullOrEmpty(txtSubjectCourseTitle.Text))
                objBo.CourseTitle = txtSubjectCourseTitle.Text;
            if (!string.IsNullOrEmpty(txtDurationYear.Text))
                objBo.Duration = txtDurationYear.Text;
            if (!string.IsNullOrEmpty(txtOrganizingInstitution.Text))
                objBo.InstituteName = txtOrganizingInstitution.Text;
            if (!string.IsNullOrEmpty(txtLocation.Text))
                objBo.CITY = txtLocation.Text;
            objBo.IsVisible = true;
            objBo.UserName = SessionWrapper.StudentRegistration.Username;
            if (string.IsNullOrWhiteSpace(hdnCourses.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hdnCourses.Value);
            }
            return true;
        }
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


            objLanguage.RegisrationId = SessionWrapper.RegistrationId;
            objLanguage.CandidateId = Convert.ToInt32(strStudentId);
            objLanguage.CourseId = Convert.ToInt32(strCourseId);
            objLanguage.Userid = SessionWrapper.StudentRegistration.Username;
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


                int count = 0;
                using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
                {
                    StudentAcademicsBO objEducation = new StudentAcademicsBO();
                    objEducation.StudentId = Convert.ToInt16(strStudentId);
                    objEducation.CourseId = Convert.ToInt16(strCourseId);
                    StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                    DataSet ds = objBAL.GetAllEducationAcademicDetails(objEducation);
                    DataTable dt = ds.Tables[0];
                    using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                    {
                        var minRec = Functions.ToListof<StudentEducationTypeDetailBO>(objStudentAdvertisementBAL.GetAlltMinimumEducationTypeDetailsById(objEducation.CourseId)).Where(x => x.IsNonMandatory == 0).ToList().Select(x => x.QualificationTypeId).ToList();
                        var remain = dt.AsEnumerable().Select(x => x.Field<long>("EducationTypeId")).ToList().Distinct();

                        var result = minRec.Where(myRow => !remain.Contains(myRow)).ToList().Distinct();

                        List<StudentEducationTypeBO> lst = Functions.ToListof<StudentEducationTypeBO>(objStudentEducationQualificationBAL.GetAllEducationType());

                        var rowName = lst.Where(x => result.Contains(x.Id)).Select(x => x.TypeName).ToList();

                        string RemainEducation = string.Join(",", rowName.ToArray());

                        if (!string.IsNullOrWhiteSpace(RemainEducation))
                        {
                            Functions.MessagePopup(this, "Enter Remain education details :" + RemainEducation, PopupMessageType.error);
                            return;
                        }
                    }
                    StudentAcademicsDocumentBO objbo = new StudentAcademicsDocumentBO();
                    objbo.StudentId = Convert.ToInt16(strStudentId);
                    objbo.CourseId = Convert.ToInt16(strCourseId);
                    DataSet dsdoc = objBAL.GetAllAcademicDocDetails(objbo);
                    DataTable dtdoc = dsdoc.Tables[0];

                    DataTable dtdoctype = objBAL.GetEducationDocTypeCheck(objbo);
                    var minRecDoc = dtdoctype.AsEnumerable().Select(x => new { EducationTypeId = x.Field<long>("Id"), EducationType = x.Field<string>("EducationDetailName") }).ToList().Distinct();
                    var remainDoc = dtdoc.AsEnumerable().Select(x => x.Field<long>("EducationTypeId")).ToList().Distinct();

                    var resultDoc = minRecDoc.Select(x => x.EducationTypeId).Where(myRow => !remainDoc.Contains(myRow)).ToList().Distinct();

                    var rowNameDoc = minRecDoc.Where(x => resultDoc.Contains(x.EducationTypeId)).Select(x => x.EducationType).ToList();

                    string RemainEducationDoc = string.Join(",", rowNameDoc.ToArray());

                    if (!string.IsNullOrWhiteSpace(RemainEducationDoc))
                    {
                        Functions.MessagePopup(this, "Enter Remain Academic Document  :" + RemainEducationDoc, PopupMessageType.error);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                        return;
                    }
                }
                int countI = 0;
                foreach (RepeaterItem repeated2 in rptUserRights.Items)
                {
                    CheckBox chk1 = (repeated2.FindControl("chk1") as CheckBox);
                    CheckBox chk2 = (repeated2.FindControl("chk2") as CheckBox);
                    CheckBox chk3 = (repeated2.FindControl("chk3") as CheckBox);
                    if (chk1.Checked)
                    {
                        countI++;
                    }
                    if (chk2.Checked)
                    {
                        countI++;
                    }
                    if (chk3.Checked)
                    {
                        countI++;
                    }
                }
                if (countI > 0)
                {
                    StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                    StudentLanguageBO objLanguage = new StudentLanguageBO();
                    foreach (RepeaterItem repeated2 in rptUserRights.Items)
                    {

                        SetLanguageParameter(repeated2, objLanguage, count);
                        if (objBAL.InsertLanguageDetails(objLanguage))
                        {
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + Languagediv.ClientID + "').focus();", true);
                    Functions.MessagePopup(this, "Please select one checkbox check in Language section.", PopupMessageType.error);
                    return;
                }
                StudentAcademicsBO objBO = new StudentAcademicsBO();
                objBO.Id = Convert.ToInt32(strStudentId.ToString());
                objBO.CourseId = Convert.ToInt32(strCourseId.ToString());
                objBO.IsComputerLiteracy = (ddlcomputerlit.SelectedValue == "1" ? true : false); ;
                //objBO.ComputerDetails = txtremerks.InnerText.ToString();
                objBO.ComputerDetails = "";
                if (new StudentAcademicsBAL().UpdateCoputerDetails(objBO))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    string strEndQueryString1 = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
                    Response.Redirect("~/Admission/ExtraInfofroAdmission.aspx?" + strEndQueryString1, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void clearAcademicDetails()
        {
            BindEducationType();
            BindEducationName();
            ddlMonth.SelectedValue = "1";
            BindYear();
            txtschoolname.Text = "";
            txtboard.Text = "";
            ddlStream.SelectedValue = "";
            ddlNoOfTrials.SelectedValue = "0";
            txtdivision.Text = "";
            btnKnowPersonDetails.Text = "Add Details";
            hdnAcademicID.Value = "";
        }
        protected void lnkbtnAcademics_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hdnAcademicID.Value = gvAcademicDetails.DataKeys[rowindex]["Id"].ToString();
            ddlEductionType.SelectedValue = gvAcademicDetails.DataKeys[rowindex]["EducationTypeId"].ToString();
            BindEducationName();
            ddlEducationName.SelectedValue = gvAcademicDetails.DataKeys[rowindex]["EducationId"].ToString();
            txtschoolname.Text = gvAcademicDetails.Rows[rowindex].Cells[4].Text;
            txtboard.Text = gvAcademicDetails.Rows[rowindex].Cells[5].Text;
            ddlMonth.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[6].Text;
            ddlAcademicYear.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[7].Text;
            ddlStream.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[8].Text;
            txtdivision.Text = gvAcademicDetails.Rows[rowindex].Cells[9].Text;
            ddlNoOfTrials.SelectedValue = gvAcademicDetails.Rows[rowindex].Cells[10].Text;
            Fupathmarksheet.InnerText = gvAcademicDetails.DataKeys[rowindex]["MarksheetPath"].ToString();
            btnKnowPersonDetails.Text = "Update Details";
        }

        protected void lnkbtnDeleteAcademics_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvAcademicDetails.DataKeys[rowindex]["Id"].ToString());
                StudentAcademicsBO objbo = new StudentAcademicsBO();
                StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                objbo.Id = Convert.ToInt32(rowId);
                objbo.UserName = SessionWrapper.StudentRegistration.Username;
                if (objBAL.DeleteRecord(objbo))
                {
                    Functions.MessagePopup(this, "Academic Details Delete Successfully .....!", PopupMessageType.success);
                    BindAcademicsData();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlEductionType.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
                    BindAcademicsData();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlEductionType.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private void clearCoursesDetails()
        {
            txtSubjectCourseTitle.Text = "";
            txtDurationYear.Text = "";
            txtOrganizingInstitution.Text = "";
            txtLocation.Text = "";
            btnAddCourses.Text = "Add Courses";
            hdnCourses.Value = "";
        }
        protected void lbtnEditCourses_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hdnCourses.Value = gvCourses.DataKeys[rowindex]["Id"].ToString();
            txtSubjectCourseTitle.Text = gvCourses.Rows[rowindex].Cells[1].Text;
            txtDurationYear.Text = gvCourses.Rows[rowindex].Cells[2].Text;
            txtOrganizingInstitution.Text = gvCourses.Rows[rowindex].Cells[3].Text;
            txtLocation.Text = gvCourses.Rows[rowindex].Cells[4].Text;
            btnAddCourses.Text = "Update Courses";
        }

        protected void lbtnDeleteCourses_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvCourses.DataKeys[rowindex]["Id"].ToString());
                StudentAcademicsBO objbo = new StudentAcademicsBO();
                StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                objbo.Id = Convert.ToInt32(rowId);
                objbo.UserName = SessionWrapper.StudentRegistration.Username;
                if (objBAL.DeleteCourseRecord(objbo))
                {
                    Functions.MessagePopup(this, "Course Delete Successfully .....!", PopupMessageType.success);
                    BindCoursesGridView();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
                    BindCoursesGridView();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtSubjectCourseTitle.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
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
            Response.Redirect("~/Admission/StudentRegistration.aspx?" + strEndQueryString1,false);
        }

        protected void btnAddEducationDoc_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                if (string.IsNullOrEmpty(filepathDoc.InnerText))
                {
                    if (!fueducationdoc.HasFile)
                    {
                        Functions.MessagePopup(this, "Select Upload Education document in .pdf formate.", PopupMessageType.warning);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                        return;
                    }
                }
                StudentAcademicsDocumentBO objbo = new StudentAcademicsDocumentBO();
                LoadControlsAddEducationDoc(objbo);
                if (new StudentAcademicsBAL().InsertOrUpdateAcademicsDocDetails(objbo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists for " + ddlEductionType.SelectedItem + ".", PopupMessageType.warning);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                    return;
                }
                BindAcademicsDocDetails();
                clearAcademicDocDetails();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void LoadControlsAddEducationDoc(StudentAcademicsDocumentBO objBo)
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


            if (hfacademicdoc.Value.ToString() != string.Empty)
            {
                objBo.Id = Convert.ToInt16(hfacademicdoc.Value.ToString());
            }
            else
            {
                objBo.Id = 0;
            }
            objBo.StudentId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt32(strCourseId);
            if (!string.IsNullOrEmpty(ddleducationReq.SelectedValue))
                objBo.EducationTypeId = Convert.ToInt16(ddleducationReq.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(txtDocname.Text))
                objBo.DocName = txtDocname.Text;
            string documentfile = string.Empty;
            documentfile = SaveFileEducationDoc();
            if (!string.IsNullOrEmpty(documentfile))
                objBo.DocPath = documentfile;
            objBo.UserName = SessionWrapper.StudentRegistration.Username;
        }
        private void BindAcademicsDocDetails()
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


            StudentAcademicsDocumentBO objBO = new StudentAcademicsDocumentBO();
            objBO.StudentId = Convert.ToInt32(strStudentId);
            objBO.CourseId = Convert.ToInt32(strCourseId);
            StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
            DataSet dt = objBAL.GetAllAcademicDocDetails(objBO);
            gvieweducationDoc.DataSource = dt.Tables[0];
            gvieweducationDoc.DataBind();
        }
        private void clearAcademicDocDetails()
        {
            BindEducationDocType();
            txtDocname.Text = "";
            //ddleducationReq.SelectedValue = "1";
            btnAddEducationDoc.Text = "Add Details";
            hfacademicdoc.Value = "";
            filepathDoc.InnerText = "";
        }

        protected void lnkbtnAcademicsDoc_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfacademicdoc.Value = gvieweducationDoc.DataKeys[rowindex]["Id"].ToString();
            ddleducationReq.SelectedValue = gvieweducationDoc.DataKeys[rowindex]["EducationTypeId"].ToString();
            BindEducationName();
            txtDocname.Text = gvieweducationDoc.Rows[rowindex].Cells[3].Text;
            filepathDoc.InnerText = gvieweducationDoc.DataKeys[rowindex]["DocPath"].ToString();
            btnAddEducationDoc.Text = "Update Details";
        }

        protected void lnkbtnDeleteAcademicsDoc_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvieweducationDoc.DataKeys[rowindex]["Id"].ToString());
                StudentAcademicsDocumentBO objbo = new StudentAcademicsDocumentBO();
                StudentAcademicsBAL objBAL = new StudentAcademicsBAL();
                objbo.Id = Convert.ToInt32(rowId);
                objbo.UserName = SessionWrapper.StudentRegistration.Username;
                if (objBAL.DeleteEducationDocRecord(objbo))
                {
                    Functions.MessagePopup(this, "Academic Details Delete Successfully .....!", PopupMessageType.success);
                    BindAcademicsDocDetails();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
                    BindAcademicsDocDetails();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddleducationReq.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private void BindEducationDocType()
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


            StudentAcademicsDocumentBO objbo = new StudentAcademicsDocumentBO();
            StudentAcademicsBAL objBal = new StudentAcademicsBAL();
            objbo.CourseId = Convert.ToInt32(strCourseId);
            ddleducationReq.DataSource = objBal.GetEducationDocType(objbo);
            ddleducationReq.DataTextField = "DocList";
            ddleducationReq.DataValueField = "Id";
            ddleducationReq.DataBind();
            ddleducationReq.Items.Insert(0, new ListItem("Select", "0"));

        }
        private string SaveFileEducationDoc()
        {
            try
            {
                if (fueducationdoc.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.MarksheetDoc;
                    var fname = Path.GetExtension(fueducationdoc.FileName);
                    var count = fueducationdoc.FileName.Split('.');
                    string type = "";
                    for (int i = 0; i < fueducationdoc.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fueducationdoc.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fueducationdoc.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        //protected void ddlcomputerlit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlcomputerlit.SelectedValue == "1")
        //    {
        //        CLRemarks.Visible = true;
        //    }
        //    else
        //    {
        //        CLRemarks.Visible = false;
        //    }
        //}
    }
}