using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.IO;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model;
using System.Data;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class BasicDetails : System.Web.UI.Page
    {

        #region Page Event and Veriable
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();
                    if (strQuery.Count() == 4)
                    {
                        strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                        strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                        strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                        strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
                    }
                    SessionWrapper.FileUploadDetails = new SessionFileUploadModel();
                    BindRelationDropDown();
                    BindCastDropDown();
                    lblPostAppliedFor.Text = strCourseName;
                    BindFamilyGridView();                   
                    FillStudentDetailsByStudentId();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        #endregion

        #region Page Functions
        private void FillStudentDetailsByStudentId()
        {
            try
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
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();
                if (strQuery.Count() == 4)
                {
                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                    strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
                }

                StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
                StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
                objbo.Id = Convert.ToInt64(strStudentId);
                objbo.CourseId = Convert.ToInt64(strCourseId);
                DataSet ds = objBAL.GetStudentRegistrationDetailsByStudentId(objbo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblTitle.Text = ds.Tables[0].Rows[0]["NamePrefix"].ToString();
                    lblFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    // lblMiddleName.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                    // lblLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                    lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                    lblAge.Text = Convert.ToString(GetAge(Convert.ToDateTime(lblDateOfBirth.Text)));
                    lblGender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                    lblMaritalStatus.Text = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                }

                if (ds.Tables[1].Rows.Count > 0 && ds != null)
                {
                    txtPresentAddress.Text = ds.Tables[1].Rows[0]["PresentAddress"].ToString();
                    txtPresentPincode.Text = ds.Tables[1].Rows[0]["PresentPincode"].ToString();
                    ddlPresentCountry.SelectedValue = ds.Tables[1].Rows[0]["PresentCountry"].ToString();
                    if (ddlPresentCountry.SelectedIndex > 0)
                    {
                        BindStateDropDOwn(ddlPresentState, ddlPresentCountry.SelectedValue);
                        ddlPresentState.SelectedValue = ds.Tables[1].Rows[0]["PresentState"].ToString();
                    }
                    if (ddlPresentState.SelectedIndex > 0)
                    {
                        BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                        ddlPresentCity.SelectedValue = ds.Tables[1].Rows[0]["PresentCity"].ToString();
                    }
                    if (ddlPresentCity.SelectedIndex > 0)
                    {
                        BindTalukaPresentCode();
                        ddlPresentTaluka.SelectedValue = ds.Tables[1].Rows[0]["PresentTaluka"].ToString();
                    }
                    txtPresentRPhoneNumber.Text = ds.Tables[1].Rows[0]["PresentPhoneR"].ToString();
                    txtPresentMPhoneNumber.Text = ds.Tables[1].Rows[0]["PresentPhoneM"].ToString();

                    txtPermenentAddress.Text = ds.Tables[1].Rows[0]["ParmenentAddress"].ToString();
                    txtPermenentPincode.Text = ds.Tables[1].Rows[0]["ParmenentPincode"].ToString();
                    ddlPermenentCountry.SelectedValue = ds.Tables[1].Rows[0]["ParmenentCountry"].ToString();
                    if (ddlPermenentCountry.SelectedIndex > 0)
                    {
                        BindStateDropDOwn(ddlPermenentState, ddlPermenentCountry.SelectedValue);
                        ddlPermenentState.SelectedValue = ds.Tables[1].Rows[0]["ParmenentState"].ToString();
                    }
                    if (ddlPermenentState.SelectedIndex > 0)
                    {
                        BindCityDropDOwn(ddlPermenentCity, ddlPermenentState.SelectedValue);
                        ddlPermenentCity.SelectedValue = ds.Tables[1].Rows[0]["ParmenentCity"].ToString();
                    }
                    if (ddlPermenentCity.SelectedIndex > 0)
                    {
                        BindTalukaPermenentCode();
                        ddlPermenentTaluka.SelectedValue = ds.Tables[1].Rows[0]["ParmenentTaluka"].ToString();
                    }
                    txtPermenentRPhoneNumber.Text = ds.Tables[1].Rows[0]["ParmenentPhoneR"].ToString();
                    txtPermenentMPhoneNumber.Text = ds.Tables[1].Rows[0]["ParmenentPhoneM"].ToString();
                    lblAge.Text = ds.Tables[1].Rows[0]["Age"].ToString();
                    txtBirthPlace.Text = ds.Tables[1].Rows[0]["PlaceOfBirth"].ToString();
                    ddlCast.SelectedValue = ds.Tables[1].Rows[0]["CasteId"].ToString();
                    ddlReligion.SelectedValue = ds.Tables[1].Rows[0]["Religion"].ToString();
                    SessionFileUploadModel obj = new SessionFileUploadModel();
                    obj = SessionWrapper.FileUploadDetails;
                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["PhotographName"].ToString()))
                    {
                        hfPhotographName.Value = ds.Tables[1].Rows[0]["PhotographName"].ToString();
                        obj.photoUploadpath = ConfigDetailsValue.StudentPhotograph;
                        obj.photoUploadName = hfPhotographName.Value;
                        if (!string.IsNullOrWhiteSpace(hfPhotographName.Value))
                        {
                            PhotographPreview.ImageUrl = ResolveUrl(ConfigDetailsValue.StudentPhotograph) + hfPhotographName.Value;
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

                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["SignatureName"].ToString()))
                    {
                        hfSignatureName.Value = ds.Tables[1].Rows[0]["SignatureName"].ToString();
                        obj.signatureUploadPath = ConfigDetailsValue.StudentSignature;
                        obj.signatureUploadName = hfSignatureName.Value;
                        if (!string.IsNullOrWhiteSpace(hfSignatureName.Value))
                        {
                            SignaturePriview.ImageUrl = ResolveUrl(ConfigDetailsValue.StudentSignature) + hfSignatureName.Value;
                            SignaturePriview.Visible = true;
                            //rfvSignature.Enabled = false;
                            //RegularExpressionValidator1.Enabled = false;
                        }
                    }
                    else
                    {
                        // rfvSignature.Enabled = false;
                        // RegularExpressionValidator1.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DOBFileName"].ToString()))
                    {
                        hfDOBProofName.Value = ds.Tables[1].Rows[0]["DOBFileName"].ToString();
                        obj.dobUploadPath = ConfigDetailsValue.StudentDateofBirthProof;
                        obj.dobUploadName = hfDOBProofName.Value;
                        //btnDOBUpload.Attributes.Add("style", "display:none");
                        btnViewDOBFile.Visible = true;
                        //rfvDOB.Enabled = false;
                        //RegularExpressionValidator2.Enabled = false;
                    }
                    else
                    {
                        // rfvDOB.Enabled = true;
                        //RegularExpressionValidator2.Enabled = true;
                    }
                    if (ds != null && ds.Tables[2].Rows.Count > 0)
                    {
                        hfStudentWorkflowId.Value = ds.Tables[2].Rows[0]["StudentId"].ToString();
                        ddlPersonalInformationId.SelectedValue= ds.Tables[2].Rows[0]["PersonalInformationId"].ToString();
                        if(ddlPersonalInformationId.SelectedValue=="1")
                        {
                            lblremarks.Visible = true;
                            txtremarks.Visible = true;
                            txtremarks.Text = ds.Tables[2].Rows[0]["PersonalInformationRemarks"].ToString();
                        }
                        
                        ddlAddressId.SelectedValue = ds.Tables[2].Rows[0]["AddressId"].ToString();
                        if (ddlAddressId.SelectedValue == "1")
                        {
                            lblAddressRemarks.Visible = true;
                            txtAddressRemarks.Visible = true;
                            txtAddressRemarks.Text = ds.Tables[2].Rows[0]["AddressRemarks"].ToString();
                        }
                           
                        ddlDocumentId.SelectedValue = ds.Tables[2].Rows[0]["DocumentId"].ToString();
                        if (ddlDocumentId.SelectedValue == "1")
                        {
                            lblDocumentRemarks.Visible = true;
                            txtDocumentRemarks.Visible = true;
                            txtDocumentRemarks.Text = ds.Tables[2].Rows[0]["DocumentRemarks"].ToString();
                        }
                        ddlFamilyMemberId.SelectedValue = ds.Tables[2].Rows[0]["FamilyMemberId"].ToString();
                        if (ddlFamilyMemberId.SelectedValue == "1")
                        {
                            lblFamilyMemberRemarks.Visible = true;
                            txtFamilyMemberRemarks.Visible = true;
                            txtFamilyMemberRemarks.Text = ds.Tables[2].Rows[0]["FamilyMemberRemarks"].ToString();
                        }
                    }
                    //else
                    //{
                    //    hfStudentWorkflowId.Value = "0";
                    //}
                    SessionWrapper.FileUploadDetails = obj;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }
        public static int GetAge(DateTime birthDate)
        {
            DateTime n = DateTime.Now; // To avoid a race condition around midnight
            int age = n.Year - birthDate.Year;

            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;

            return age;
        }
        private void BindRelationDropDown()
        {
            using (IConfigDetailsRepository objRecruitmentRelationRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                ddlPermenentCountry.DataSource = objRecruitmentRelationRepository.GetAllCoutry();
                ddlPermenentCountry.DataValueField = "Id";
                ddlPermenentCountry.DataTextField = "Name";
                ddlPermenentCountry.DataBind();
                ddlPermenentCountry.Items.Insert(0, "Select");

                ddlPresentCountry.DataSource = objRecruitmentRelationRepository.GetAllCoutry();
                ddlPresentCountry.DataValueField = "Id";
                ddlPresentCountry.DataTextField = "Name";
                ddlPresentCountry.DataBind();
                ddlPresentCountry.Items.Insert(0, "Select");
            }

            //using (IRecruitmentRelationRepository objRecruitmentRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            //{
            //    ddlFamilyRelation.DataSource = objRecruitmentRelationRepository.GetAllTblRecruitmentRelation();
            //    ddlFamilyRelation.DataValueField = "Id";
            //    ddlFamilyRelation.DataTextField = "Name";
            //    ddlFamilyRelation.DataBind();
            //    ddlFamilyRelation.Items.Insert(0, "Select");
            //}
            using (IRecruitmentReligionRepository objRecruitmentRelationRepository = new RecruitmentReligionRepository(Functions.strSqlConnectionString))
            {
                ddlReligion.DataSource = objRecruitmentRelationRepository.GetAllTblRecruitmentReligion();
                ddlReligion.DataValueField = "Id";
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataBind();
                ddlReligion.Items.Insert(0, "Select");
            }
        }
        private void BindCastDropDown()
        {
            using (IRecruitmentCastRepository objRecruitmentRelationRepository = new RecruitmentCastRepository(Functions.strSqlConnectionString))
            {
                ddlCast.DataSource = objRecruitmentRelationRepository.GetAllTblRecruitmentCast().Where(x => x.IsVisible == true).ToList();
                ddlCast.DataValueField = "Id";
                ddlCast.DataTextField = "Name";
                ddlCast.DataBind();
                ddlCast.Items.Insert(0, "Select");
            }
        }
        private void BindCountryCode()
        {
            try
            {
                CountryBAL objBal = new CountryBAL();
                ddlPresentCountry.DataSource = objBal.SelectCountryCode();
                ddlPresentCountry.DataTextField = "Country_phone_code";
                ddlPresentCountry.DataValueField = "Id";
                ddlPresentCountry.DataBind();
                ddlPresentCountry.Items.Insert(0, new ListItem("-Select Country Code-"));
                ddlPermenentCountry.DataSource = objBal.SelectCountryCode();
                ddlPermenentCountry.DataTextField = "Country_phone_code";
                ddlPermenentCountry.DataValueField = "Id";
                ddlPermenentCountry.DataBind();
                ddlPermenentCountry.Items.Insert(0, new ListItem("-Select Country Code-"));
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void BindStateDropDOwn(DropDownList ddlState, string strCountry = "")
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (!string.IsNullOrWhiteSpace(strCountry))
                {
                    ddlState.DataSource = objConfigDetailsRepository.GetAllState(Convert.ToInt32(strCountry));
                    ddlState.DataValueField = "Id";
                    ddlState.DataTextField = "Name";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "Select");
                }
            }
        }
        private void BindCityDropDOwn(DropDownList ddlCity, string strCountry = "")
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (!string.IsNullOrWhiteSpace(strCountry))
                {
                    ddlCity.DataSource = objConfigDetailsRepository.GetAllCity(Convert.ToInt32(strCountry));
                    ddlCity.DataValueField = "Id";
                    ddlCity.DataTextField = "Name";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, "Select");
                }
            }
        }
        #endregion

        #region Page DropDown SelectedIndex
        protected void ddlPresentCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (ddlPresentCountry.SelectedIndex > 0)
                {
                    BindStateDropDOwn(ddlPresentState, ddlPresentCountry.SelectedValue);
                }
            }
        }
        protected void ddlPresentState_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (ddlPresentState.SelectedIndex > 0)
                {
                    BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                }
            }
        }
        protected void ddlPermenentCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (ddlPermenentCountry.SelectedIndex > 0)
                {
                    BindStateDropDOwn(ddlPermenentState, ddlPermenentCountry.SelectedValue);
                }
            }
        }
        protected void ddlPermenentState_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (ddlPermenentState.SelectedIndex > 0)
                {
                    BindCityDropDOwn(ddlPermenentCity, ddlPermenentState.SelectedValue);
                }
            }
        }
        #endregion

        #region File Upload Section
        //protected void btnUploadPhotograph_Click(object sender, EventArgs e)
        //{
        //    SessionFileUploadModel obj = new SessionFileUploadModel();
        //    obj = SessionWrapper.FileUploadDetails;
        //    var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_Photo";
        //    string filePath = ConfigDetailsValue.StudentPhotograph;
        //    var fname = Path.GetExtension(fuPhotograph.FileName);
        //    Savefile(fuPhotograph, regId, filePath, "0");
        //    var filename = regId + fname;
        //    obj.photoUploadpath = filePath;
        //    obj.photoUploadName = filename;
        //    SessionWrapper.FileUploadDetails = obj;
        //    PhotographPreview.ImageUrl = filePath + filename;
        //    PhotographPreview.Visible = true;
        //    //rfvPhotograph.Enabled = false;
        //   // RegExValFileUploadFileType.Enabled = false;
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + fuSignature.ClientID + "').focus();", true);
        //}
        //protected void btnUploadSignature_Click(object sender, EventArgs e)
        //{
        //    SessionFileUploadModel obj = new SessionFileUploadModel();
        //    obj = SessionWrapper.FileUploadDetails;
        //    var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_Signature";
        //    string filePath = ConfigDetailsValue.StudentSignature;
        //    var fname = Path.GetExtension(fuSignature.FileName);
        //    Savefile(fuSignature, regId, filePath, "0");
        //    var filename = regId + fname;
        //    obj.signatureUploadPath = filePath;
        //    obj.signatureUploadName = filename;
        //    SessionWrapper.FileUploadDetails = obj;
        //    SignaturePriview.ImageUrl = filePath + filename;
        //    SignaturePriview.Visible = true;
        //    rfvSignature.Enabled = false;
        //    RegularExpressionValidator1.Enabled = false;
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + fuDOB.ClientID + "').focus();", true);
        //}
        //protected void btnDOBUpload_Click(object sender, EventArgs e)
        //{
        //    SessionFileUploadModel obj = new SessionFileUploadModel();
        //    obj = SessionWrapper.FileUploadDetails;
        //    var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_DOB";
        //    string filePath = ConfigDetailsValue.StudentDateofBirthProof;
        //    var fname = Path.GetExtension(fuDOB.FileName);
        //    Savefile(fuDOB, regId, filePath, "1");
        //    var filename = regId + fname;
        //    obj.dobUploadPath = filePath;
        //    obj.dobUploadName = filename;
        //    SessionWrapper.FileUploadDetails = obj;
        //    btnViewDOBFile.Visible = true;
        //    rfvDOB.Enabled = false;
        //    RegularExpressionValidator2.Enabled = false;
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //}
        protected void btnViewDOBFile_Click(object sender, EventArgs e)
        {
            //string dobFilePath = ConfigDetailsValue.StudentDateofBirthProof.Replace("~/Admission/", "") + SessionWrapper.FileUploadDetails.dobUploadName;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();window.open('" + dobFilePath + "','_newtab');", true);
            string dobFilePath = ResolveUrl(ConfigDetailsValue.StudentDateofBirthProof.Replace("~/Admin/Student/", "") + SessionWrapper.FileUploadDetails.dobUploadName);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "document.getElementById('" + txtPermenentAddress.ClientID + "').focus();window.open('" + dobFilePath + "','_newtab');", true);
        }
        //private void Savefile(FileUpload fuFile, string regId, string filePath, string flag)
        //{
        //    if (fuFile.HasFile)
        //    {
        //        string PhotographName = "";
        //        string PhotographPath = "";

        //        if (!filePath.Contains("|"))
        //        {
        //            PhotographName = regId + System.IO.Path.GetExtension(fuFile.FileName);

        //            bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

        //            if (!exists)
        //                System.IO.Directory.CreateDirectory(Server.MapPath(filePath));

        //            // Create the path and file name to check for duplicates.
        //            var pathToCheck1 = filePath + PhotographName;
        //            // Create a temporary file name to use for checking duplicates.                   
        //            if (File.Exists(Server.MapPath(pathToCheck1)))
        //            {
        //                File.Delete(pathToCheck1);
        //            }

        //            PhotographPath = filePath + PhotographName;
        //            //Save selected file into specified location
        //            fuFile.SaveAs(Server.MapPath(filePath) + PhotographName);
        //        }
        //        else
        //        {
        //            Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
        //        }
        //    }
        //}
        #endregion

        #region Copy Present Address 
        protected void PresentAddress_Click(object sender, EventArgs e)
        {
            string PresentAddress = txtPresentAddress.Text;
            string PresentPincode = txtPresentPincode.Text;
            string PresentMPhoneNumber = txtPresentMPhoneNumber.Text;
            string PresentRPhoneNumber = txtPresentRPhoneNumber.Text;
            string PresentCountry = ddlPresentCountry.SelectedValue;
            string PresentState = ddlPresentState.SelectedValue;
            string PresentCity = ddlPresentCity.SelectedValue;

            if (!string.IsNullOrWhiteSpace(PresentAddress))
            {
                txtPermenentAddress.Text = PresentAddress;
            }
            if (!string.IsNullOrWhiteSpace(PresentPincode))
            {
                txtPermenentPincode.Text = PresentPincode;
            }
            if (!string.IsNullOrWhiteSpace(PresentMPhoneNumber))
            {
                txtPermenentMPhoneNumber.Text = PresentMPhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(PresentRPhoneNumber))
            {
                txtPermenentRPhoneNumber.Text = PresentRPhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(PresentCountry) && ddlPresentCountry.SelectedIndex > 0)
            {
                ddlPermenentCountry.SelectedValue = PresentCountry;
                BindStateDropDOwn(ddlPermenentState, PresentCountry);
            }
            if (!string.IsNullOrWhiteSpace(PresentState) && ddlPresentState.SelectedIndex > 0)
            {
                ddlPermenentState.SelectedValue = PresentState;
                BindCityDropDOwn(ddlPermenentCity, PresentState);
            }
            if (!string.IsNullOrWhiteSpace(PresentCity) && ddlPresentCity.SelectedIndex > 0)
            {
                ddlPermenentCity.SelectedValue = PresentCity;
            }
        }
        #endregion

        #region Save&Next Section
        protected void btnSaveAndNext_Click(object sender, EventArgs e)
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
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();
            if (strQuery.Count() == 4)
            {
                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strRegistrationId = strQuery[3].ToString().Replace("RegistrationId=", "");
            }

            StudentRegistrationDetailsBO objBo = new StudentRegistrationDetailsBO();
            using (StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL())
            {
                if (FillForm(objBo))
                {
                    if (objData.InsertWorkflowRecord(objBo))
                    {
                        Functions.MessagePopup(this, "Student details insert sucessfully .....!", PopupMessageType.success);
                        ClearControl();
                        Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString);

                    }
                    else
                    {
                        Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
                    }
                }
            }

            //Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString);
            //string strError = "";
            //StudentRegistrationDetailsBO objBo = new StudentRegistrationDetailsBO();
            //using (StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL())
            //{
            //    if (FillForm(objBo))
            //    {
            //        if (objData.InsertRecord(objBo))
            //        {
            //            Functions.MessagePopup(this, "Student details insert sucessfully .....!", PopupMessageType.success);
            //            ClearControl();
            //            Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString);

            //        }
            //        else
            //        {
            //            Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
            //        }
            //    }
            //}
        }
        private bool FillForm(StudentRegistrationDetailsBO objBo)
        {
            try
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
                if (ddlPersonalInformationId.SelectedValue == "")
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


                if (ddlAddressId.SelectedValue == "")
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


                if (ddlDocumentId.SelectedValue == "")
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
                    objBo.DocumentRemarks = ddlDocumentId.SelectedValue;
                }

                if (ddlFamilyMemberId.SelectedValue == "")
                {
                    Functions.MessagePopup(this, "Select Verification", PopupMessageType.error);
                    ddlFamilyMemberId.Focus();
                    return false;
                }
                if (ddlDocumentId.SelectedValue == "1")
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

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                return false;
            }

        }
        private void ClearControl()
        {
            hfStudentDetailsId.Value = "0";
            hfStudentWorkflowId.Value = "0";
            txtPresentAddress.Text = "";
            txtPresentPincode.Text = "";
            txtPresentRPhoneNumber.Text = "";
            txtPresentMPhoneNumber.Text = "";
            txtPermenentAddress.Text = "";
            txtPermenentPincode.Text = "";
            txtPresentRPhoneNumber.Text = "";
            txtPermenentMPhoneNumber.Text = "";
            txtBirthPlace.Text = "";
            BindCastDropDown();
            BindRelationDropDown();
        }
        #endregion

        #region Family Member Information Section
        //protected void btnMember_Click(object sender, EventArgs e)
        //{
        //    string strError = "";
        //    StudentFamilyDetailsBO objBo = new StudentFamilyDetailsBO();
        //    using (StudentFamilyDetailsBAL objData = new StudentFamilyDetailsBAL())
        //    {
        //        if (FillSubPageDetails(objBo))
        //        {
        //            if (objData.InsertRecord(objBo))
        //            {
        //                Functions.MessagePopup(this, "Member Added Successfully .....!", PopupMessageType.success);
        //                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //                BindFamilyGridView();
        //                ClearFamilyDetails();
        //            }
        //            else
        //            {
        //                Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
        //                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //            }
        //        }
        //    }
        //}
        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearFamilyDetails();
        //}
        //private bool FillSubPageDetails(StudentFamilyDetailsBO objBo)
        //{
        //    //objBo.ChkMode = 0;
        //    if (string.IsNullOrWhiteSpace(hfRelativeId.Value))
        //    {
        //        objBo.Id = 0;
        //    }
        //    else
        //    {
        //        objBo.Id = Convert.ToInt64(hfRelativeId.Value);
        //    }

        //    objBo.StudentId = Convert.ToInt64(strStudentId);

        //    if (!string.IsNullOrWhiteSpace(txtFamilyPersonName.Text.Trim()))
        //    {
        //        objBo.MemberName = txtFamilyPersonName.Text.Trim();
        //    }
        //    else
        //    {
        //        Functions.MessageFrontPopup(this, "Please Enter Member Name", PopupMessageType.error);
        //        return false;
        //    }

        //    long lgAge;
        //    if (string.IsNullOrWhiteSpace(txtFamilyAge.Text.Trim()) || !long.TryParse(txtFamilyAge.Text.Trim(), out lgAge))
        //    {
        //        Functions.MessageFrontPopup(this, "Please Enter Age of Family Member.", PopupMessageType.error);
        //        return false;
        //    }
        //    else
        //    {
        //        objBo.Age = lgAge;
        //    }

        //    if (ddlFamilyRelation.SelectedIndex <= 0)
        //    {
        //        Functions.MessageFrontPopup(this, "Please Select Family Relation", PopupMessageType.error);
        //        return false;
        //    }
        //    else
        //    {
        //        objBo.RelationId = Convert.ToInt32(ddlFamilyRelation.SelectedValue);
        //    }


        //    if (!string.IsNullOrWhiteSpace(txtFamilyOccupation.Text.Trim()))
        //    {
        //        objBo.Occupation = txtFamilyOccupation.Text.Trim();
        //    }


        //    if (!string.IsNullOrWhiteSpace(txtFamilyMonthlyIncome.Text.Trim()))
        //    {
        //        decimal dcIncome = 0;
        //        if (decimal.TryParse(txtFamilyMonthlyIncome.Text.Trim(), out dcIncome))
        //        {
        //            objBo.MonthlyIncome = dcIncome;
        //        }
        //        else
        //        {
        //            Functions.MessageFrontPopup(this, "Please Enter Valid Monthly Income", PopupMessageType.error);
        //            return false;
        //        }
        //    }
        //    objBo.IsVisible = true;
        //    return true;
        //}
        //private void ClearFamilyDetails()
        //{
        //    hfRelativeId.Value = "0";
        //    txtFamilyAge.Text = "";
        //    txtFamilyMonthlyIncome.Text = "";
        //    txtFamilyOccupation.Text = "";
        //    txtFamilyPersonName.Text = "";
        //    ddlFamilyRelation.SelectedIndex = 0;
        //    btnMember.Text = "Add Member";
        //    BindFamilyGridView();
        //}
        private void BindFamilyGridView()
        {
            try
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

                StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
                StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
                objbo.Id = Convert.ToInt64(strStudentId);
                objbo.CourseId = Convert.ToInt64(strCourseId);
                DataSet ds = objBAL.SelectRecord(objbo);
                gvFamilyMember.DataSource = ds;
                gvFamilyMember.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }
        //protected void lnkFamilyMemberEdit_Click(object sender, EventArgs e)
        //{
        //    int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //    txtFamilyPersonName.Text = gvFamilyMember.Rows[rowindex].Cells[2].Text.Trim();
        //    txtFamilyAge.Text = gvFamilyMember.Rows[rowindex].Cells[3].Text.Trim();
        //    ddlFamilyRelation.SelectedValue = gvFamilyMember.DataKeys[rowindex]["RelationId"].ToString();
        //    txtFamilyOccupation.Text = gvFamilyMember.Rows[rowindex].Cells[5].Text.Trim();
        //    txtFamilyMonthlyIncome.Text = gvFamilyMember.Rows[rowindex].Cells[6].Text.Trim();
        //    hfRelativeId.Value = gvFamilyMember.DataKeys[rowindex]["Id"].ToString();
        //    btnMember.Text = "Update Member";
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //}
        //protected void lnkFamilyMemberDelete_Click(object sender, EventArgs e)
        //{
        //    string errorMessage = "";
        //    try
        //    {
        //        int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //        int rowId = Convert.ToInt32(gvFamilyMember.DataKeys[rowindex]["Id"].ToString());
        //        StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
        //        StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
        //        objbo.Id = Convert.ToInt64(rowId);
        //        if (objBAL.DeleteRecord(objbo))
        //        {
        //            Functions.MessagePopup(this, "Member Delete Successfully .....!", PopupMessageType.success);
        //            BindFamilyGridView();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //        }
        //        else
        //        {
        //            Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
        //            BindFamilyGridView();
        //            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Student/StudentApplyList.aspx");
        }

        protected void ddlPresentCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPresentCity.SelectedIndex > 0)
            {
                BindTalukaPresentCode();
                //BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                ddlPresentTaluka.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentTaluka.ClientID + "').focus();", true);
            }
            else
            {
                ddlPresentTaluka.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentTaluka.ClientID + "').focus();", true);
            }
        }
        private void BindTalukaPresentCode()
        {
            try
            {
                CountryBAL objBal = new CountryBAL();
                ddlPresentTaluka.DataSource = objBal.SelectTalukaCode(Convert.ToInt32(ddlPresentCity.SelectedValue), 1);
                ddlPresentTaluka.DataTextField = "Name";
                ddlPresentTaluka.DataValueField = "Id";
                ddlPresentTaluka.DataBind();
                ddlPresentTaluka.Items.Insert(0, new ListItem("-Select Taluka Code-"));
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void ddlPermenentCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPermenentCity.SelectedIndex > 0)
            {
                BindTalukaPermenentCode();
                //BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                ddlPermenentTaluka.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentTaluka.ClientID + "').focus();", true);
            }
            else
            {
                ddlPermenentTaluka.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentTaluka.ClientID + "').focus();", true);
            }
        }
        private void BindTalukaPermenentCode()
        {
            try
            {
                CountryBAL objBal = new CountryBAL();
                ddlPermenentTaluka.DataSource = objBal.SelectTalukaCode(Convert.ToInt32(ddlPermenentCity.SelectedValue), 1);
                ddlPermenentTaluka.DataTextField = "Name";
                ddlPermenentTaluka.DataValueField = "Id";
                ddlPermenentTaluka.DataBind();
                ddlPermenentTaluka.Items.Insert(0, new ListItem("-Select Taluka Code-"));
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
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

        //protected void chkPersonalInformationId_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPersonalInformationId.Checked == true)
        //    {
        //        txtremarks.Visible = true;
        //        lblremarks.Visible = true;
        //    }
        //    else
        //    {
        //        txtremarks.Visible = false;
        //        lblremarks.Visible = false;
        //    }

        //}
    }
}