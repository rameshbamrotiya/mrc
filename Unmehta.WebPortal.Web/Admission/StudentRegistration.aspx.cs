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
using System.Globalization;
using BO.Admission;
using Org.BouncyCastle.Asn1.Cmp;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class StudentRegistration : System.Web.UI.Page
    {
        #region Page Event and Veriable
        //public static string strStudentId;
        //public static string strCourseId;
        //public static string strCourseName;
        StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (SessionWrapper.StudentRegistration.Username == null)
                    {
                        Response.Redirect("~/Admission/", false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Admission/", false);
                }
                if (!IsPostBack)
                {
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {

                        Response.Redirect("~/Admission/Course.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }

                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");

                    SessionWrapper.FileUploadDetails = new SessionFileUploadModel();
                    BindRelationDropDown();
                    BindCastDropDown();
                    lblPostAppliedFor.Text = strCourseName;
                    BindFamilyGridView();
                    GetDropDown();
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

        private void GetDropDown()
        {
            ddlGender.DataSource = GetAll<GenderType>();
            ddlGender.DataTextField = "Value";
            ddlGender.DataValueField = "Value";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("-Select Gender-"));
            ddlGender.SelectedIndex = 0;

            ddlMaritalStatus.DataSource = GetAll<MaritalStatusType>();
            ddlMaritalStatus.DataTextField = "Value";
            ddlMaritalStatus.DataValueField = "Value";
            ddlMaritalStatus.DataBind();
            ddlMaritalStatus.Items.Insert(0, new ListItem("-Select Marital Status-"));
            ddlMaritalStatus.SelectedIndex = 0;
        }
        private void FillStudentDetailsByStudentId()
        {
            try
            {

                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admission/Course.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");

                StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
                objbo.Id = Convert.ToInt64(strStudentId);
                objbo.CourseId = Convert.ToInt64(strCourseId);
                DataSet ds = objBAL.GetStudentRegistrationDetailsByStudentId(objbo);
                DataSet dsverification = objBAL.GetStudentVerificationByStudentIdandCourseId(Convert.ToInt32(strStudentId), Convert.ToInt32(strCourseId));
                if (dsverification != null && dsverification.Tables[0].Rows.Count > 0)
                {
                    string PersonalInformationId = dsverification.Tables[0].Rows[0]["PersonalInformationId"].ToString();
                    if (PersonalInformationId == "1")
                    {
                        DivAststusPIMain.Visible = true;
                        lblstatus.Text = "Correction";
                        lblstatus.ForeColor = System.Drawing.Color.Yellow;
                        LblRemerksPI.Visible = true;
                        txtremerks.Text = dsverification.Tables[0].Rows[0]["PersonalInformationRemarks"].ToString();
                        PanelPersonalInformation.Enabled = true;
                    }
                    else if (PersonalInformationId == "0")
                    {
                        DivAststusPIMain.Visible = true;
                        lblstatus.Text = "Approve";
                        lblstatus.BackColor = System.Drawing.Color.Green;
                        LblRemerksPI.Visible = false;
                        PanelPersonalInformation.Enabled = false;
                    }
                    else
                    {
                        PanelPersonalInformation.Enabled = true;
                    }
                    string AddressId = dsverification.Tables[0].Rows[0]["AddressId"].ToString();
                    if (AddressId == "1")
                    {
                        DivAststusaddMain.Visible = true;
                        LblstatusAdd.Text = "Correction";
                        LblstatusAdd.BackColor = System.Drawing.Color.Yellow;
                        DivRemerksadd.Visible = true;
                        txtremerksAdd.Text = dsverification.Tables[0].Rows[0]["AddressRemarks"].ToString();
                        PanelAddressDetails.Enabled = true;
                    }
                    else if (AddressId == "0")
                    {
                        DivAststusaddMain.Visible = true;
                        LblstatusAdd.Text = "Approve";
                        LblstatusAdd.ForeColor = System.Drawing.Color.Green;
                        DivRemerksadd.Visible = false;
                        PanelAddressDetails.Enabled = false;
                    }
                    else
                    {
                        PanelAddressDetails.Enabled = true;
                    }
                    string DocumentId = dsverification.Tables[0].Rows[0]["DocumentId"].ToString();
                    if (DocumentId == "1")
                    {
                        DivDocumentUploadMain.Visible = true;
                        LblstatusDU.Text = "Correction";
                        LblstatusDU.BackColor = System.Drawing.Color.Yellow;
                        DivRemerksDU.Visible = true;
                        txtRemerksDU.Text = dsverification.Tables[0].Rows[0]["DocumentRemarks"].ToString();
                        PanelDocumentUpload.Enabled = true;
                    }
                    else if (DocumentId == "0")
                    {
                        DivDocumentUploadMain.Visible = true;
                        LblstatusDU.Text = "Approve";
                        LblstatusDU.ForeColor = System.Drawing.Color.Green;
                        DivRemerksDU.Visible = false;
                        PanelDocumentUpload.Enabled = false;
                    }
                    else
                    {
                        PanelDocumentUpload.Enabled = true;
                    }
                    string FamilyMemberId = dsverification.Tables[0].Rows[0]["FamilyMemberId"].ToString();
                    if (FamilyMemberId == "1")
                    {
                        DivFamilyMemberInformationMain.Visible = true;
                        LblstatusFMI.BackColor = System.Drawing.Color.Yellow;
                        LblstatusFMI.Text = "Correction";
                        DivRemerksFMI.Visible = true;
                        txtRemerksFMI.Text = dsverification.Tables[0].Rows[0]["FamilyMemberRemarks"].ToString();
                        PanelFamilyMemberInformation.Enabled = true;
                    }
                    else if (FamilyMemberId == "0")
                    {
                        DivFamilyMemberInformationMain.Visible = true;
                        LblstatusFMI.ForeColor = System.Drawing.Color.Green;
                        LblstatusFMI.Text = "Approve";
                        DivRemerksFMI.Visible = false;
                        PanelFamilyMemberInformation.Enabled = false;
                    }
                    else
                    {
                        PanelFamilyMemberInformation.Enabled = true;
                    }
                }
                else
                {
                    PanelPersonalInformation.Enabled = true;
                    DivAststusPIMain.Visible = false;

                    PanelAddressDetails.Enabled = true;
                    DivAststusaddMain.Visible = false;

                    PanelDocumentUpload.Enabled = true;
                    DivDocumentUploadMain.Visible = false;

                    PanelFamilyMemberInformation.Enabled = true;
                    DivFamilyMemberInformationMain.Visible = false;
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                 {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["NamePrefix"].ToString()))
                    {
                        ddlTitle.SelectedValue = ds.Tables[0].Rows[0]["NamePrefix"].ToString();
                    }
 
                    lblFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    lblMiddleName.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                    lblLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                    lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();

                    txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtEmail.ReadOnly = true;
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateOfBirth"].ToString()))
                    {
                        DateTime? dt;
                        string strError;
                        if (!Functions.GetDateFromString(lblDateOfBirth.Text.Trim(), out dt, out strError))
                        {
                            lblAge.Text = ((DateTime)dt).ToAgeString(DateTime.Now).Years.ToString();
                        }
                        //lblAge.Text = ds.Tables[0].Rows[0]["age"].ToString();
                    }



                    ddlGender.SelectedValue= ds.Tables[0].Rows[0]["Gender"].ToString();
                    ddlMaritalStatus.SelectedValue = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["mobile"].ToString()))
                    {
                        txtPresentMPhoneNumber.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
                    }

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
                    ddlPresentCity.Text = ds.Tables[1].Rows[0]["PresentCity"].ToString();
                    ddlPresentTaluka.Text = ds.Tables[1].Rows[0]["PresentTaluka"].ToString();
                    //if (ddlPresentState.SelectedIndex > 0)
                    //{
                    //    BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                    //    ddlPresentCity.SelectedValue = ds.Tables[1].Rows[0]["PresentCity"].ToString();
                    //}
                    //if (ddlPresentCity.SelectedIndex > 0)
                    //{
                    //    BindTalukaPresentCode();
                    //    ddlPresentTaluka.SelectedValue = ds.Tables[1].Rows[0]["PresentTaluka"].ToString();
                    //}
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

                    ddlPermenentCity.Text = ds.Tables[1].Rows[0]["ParmenentCity"].ToString();
                    ddlPermenentTaluka.Text = ds.Tables[1].Rows[0]["ParmenentTaluka"].ToString();
                    //if (ddlPermenentState.SelectedIndex > 0)
                    //{
                    //    BindCityDropDOwn(ddlPermenentCity, ddlPermenentState.SelectedValue);
                    //    ddlPermenentCity.SelectedValue = ds.Tables[1].Rows[0]["ParmenentCity"].ToString();
                    //}
                    //if (ddlPermenentCity.SelectedIndex > 0)
                    //{
                    //    BindTalukaPermenentCode();
                    //    ddlPermenentTaluka.SelectedValue = ds.Tables[1].Rows[0]["ParmenentTaluka"].ToString();
                    //}
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
                            rfvPhotograph.Enabled = false;
                            RegExValFileUploadFileType.Enabled = false;
                        }
                    }
                    else
                    {
                        rfvPhotograph.Enabled = true;
                        RegExValFileUploadFileType.Enabled = true;
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
                            rfvSignature.Enabled = false;
                            RegularExpressionValidator1.Enabled = false;
                        }
                    }
                    else
                    {
                        rfvSignature.Enabled = false;
                        RegularExpressionValidator1.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DOBFileName"].ToString()))
                    {
                        hfDOBProofName.Value = ds.Tables[1].Rows[0]["DOBFileName"].ToString();
                        obj.dobUploadPath = ConfigDetailsValue.StudentDateofBirthProof;
                        obj.dobUploadName = hfDOBProofName.Value;
                        btnDOBUpload.Attributes.Add("style", "display:none");
                        //btnViewDOBFile.Visible = true;

                        string dobFilePath = ConfigDetailsValue.StudentDateofBirthProof.Replace("~/Admission/", "") + SessionWrapper.FileUploadDetails.dobUploadName;

                        ImageIdentityProof.ImageUrl = dobFilePath;
                        ImageIdentityProof.Visible = true;
                        rfvDOB.Enabled = false;
                        RegularExpressionValidator2.Enabled = false;
                    }
                    else
                    {
                        rfvDOB.Enabled = true;
                        RegularExpressionValidator2.Enabled = true;
                    }
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
                    ddlPresentState.SelectedIndex = 0;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentState.ClientID + "').focus();", true);
                }
                else
                {
                    if (ddlPresentState.Items.Count > 0)
                    {
                        var firstitem = ddlPresentState.Items[0];
                        ddlPresentState.Items.Clear();
                        ddlPresentState.Items.Add(firstitem);
                    }
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentState.ClientID + "').focus();", true);
                }
                ddlPresentState_SelectedIndexChanged(ddlPresentState, EventArgs.Empty);
            }
        }
        protected void ddlPresentState_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                //if (ddlPresentState.SelectedIndex > 0)
                //{
                //    BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                //    ddlPresentCity.SelectedIndex = 0;
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentCity.ClientID + "').focus();", true);
                //}
                //else
                //{
                //    if (ddlPresentCity.Items.Count > 0)
                //    {
                //        var firstitem = ddlPresentCity.Items[0];
                //        ddlPresentCity.Items.Clear();
                //        ddlPresentCity.Items.Add(firstitem);
                //    }
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentCity.ClientID + "').focus();", true);
                //}
                //ddlPresentCity_SelectedIndexChanged(ddlPresentCity, EventArgs.Empty);
            }
        }
        protected void ddlPermenentCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                if (ddlPermenentCountry.SelectedIndex > 0)
                {
                    BindStateDropDOwn(ddlPermenentState, ddlPermenentCountry.SelectedValue);
                    ddlPermenentState.SelectedIndex = 0;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentState.ClientID + "').focus();", true);
                }
                else
                {
                    if (ddlPermenentState.Items.Count > 0)
                    {
                        var firstitem = ddlPermenentState.Items[0];
                        ddlPermenentState.Items.Clear();
                        ddlPermenentState.Items.Add(firstitem);
                    }
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentState.ClientID + "').focus();", true);
                }
                ddlPermenentState_SelectedIndexChanged(ddlPermenentState, EventArgs.Empty);
            }
        }
        protected void ddlPermenentState_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IConfigDetailsRepository objConfigDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
            {
                //if (ddlPermenentState.SelectedIndex > 0)
                //{
                //    BindCityDropDOwn(ddlPermenentCity, ddlPermenentState.SelectedValue);
                //    ddlPermenentCity.SelectedIndex = 0;
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentCity.ClientID + "').focus();", true);
                //}
                //else
                //{
                //    if (ddlPermenentCity.Items.Count > 0)
                //    {
                //        var firstitem = ddlPermenentCity.Items[0];
                //        ddlPermenentCity.Items.Clear();
                //        ddlPermenentCity.Items.Add(firstitem);
                //    }
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentCity.ClientID + "').focus();", true);
                //}
                //ddlPermenentCity_SelectedIndexChanged(ddlPermenentCity, EventArgs.Empty);
            }
        }
        #endregion

        #region File Upload Section
        protected void btnUploadPhotograph_Click(object sender, EventArgs e)
        {
            SessionFileUploadModel obj = new SessionFileUploadModel();
            obj = SessionWrapper.FileUploadDetails;
            var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_Photo";
            string filePath = ConfigDetailsValue.StudentPhotograph;
            var fname = Path.GetExtension(fuPhotograph.FileName);
            Savefile(fuPhotograph, regId, filePath, "0");
            var filename = regId + fname;
            obj.photoUploadpath = filePath;
            obj.photoUploadName = filename;
            SessionWrapper.FileUploadDetails = obj;
            PhotographPreview.ImageUrl = filePath + filename;
            PhotographPreview.Visible = true;
            rfvPhotograph.Enabled = false;
            RegExValFileUploadFileType.Enabled = false;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + fuSignature.ClientID + "').focus();", true);
        }
        protected void btnUploadSignature_Click(object sender, EventArgs e)
        {
            SessionFileUploadModel obj = new SessionFileUploadModel();
            obj = SessionWrapper.FileUploadDetails;
            var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_Signature";
            string filePath = ConfigDetailsValue.StudentSignature;
            var fname = Path.GetExtension(fuSignature.FileName);
            Savefile(fuSignature, regId, filePath, "0");
            var filename = regId + fname;
            obj.signatureUploadPath = filePath;
            obj.signatureUploadName = filename;
            SessionWrapper.FileUploadDetails = obj;
            SignaturePriview.ImageUrl = filePath + filename;
            SignaturePriview.Visible = true;
            rfvSignature.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + fuDOB.ClientID + "').focus();", true);
        }
        protected void btnDOBUpload_Click(object sender, EventArgs e)
        {
            SessionFileUploadModel obj = new SessionFileUploadModel();
            obj = SessionWrapper.FileUploadDetails;
            var regId = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_DOB";
            string filePath = ConfigDetailsValue.StudentDateofBirthProof;
            var fname = Path.GetExtension(fuDOB.FileName);
            Savefile(fuDOB, regId, filePath, "1");
            var filename = regId + fname;
            obj.dobUploadPath = filePath;
            obj.dobUploadName = filename;

            SessionWrapper.FileUploadDetails = obj;

            string dobFilePath = ConfigDetailsValue.StudentDateofBirthProof.Replace("~/Admission/", "") + SessionWrapper.FileUploadDetails.dobUploadName;

            ImageIdentityProof.ImageUrl = dobFilePath;
            ImageIdentityProof.Visible = true;
            //btnViewDOBFile.Visible = true;
            rfvDOB.Enabled = false;
            RegularExpressionValidator2.Enabled = false;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        }
        protected void btnViewDOBFile_Click(object sender, EventArgs e)
        {
            string dobFilePath = ConfigDetailsValue.StudentDateofBirthProof.Replace("~/Admission/", "") + SessionWrapper.FileUploadDetails.dobUploadName;


            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();window.open('" + dobFilePath + "','_newtab');", true);
        }
        private void Savefile(FileUpload fuFile, string regId, string filePath, string flag)
        {
            if (fuFile.HasFile)
            {
                string PhotographName = "";
                string PhotographPath = "";

                if (!filePath.Contains("|"))
                {
                    PhotographName = regId + System.IO.Path.GetExtension(fuFile.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));

                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + PhotographName;
                    // Create a temporary file name to use for checking duplicates.                   
                    if (File.Exists(Server.MapPath(pathToCheck1)))
                    {
                        File.Delete(pathToCheck1);
                    }

                    PhotographPath = filePath + PhotographName;
                    //Save selected file into specified location
                    fuFile.SaveAs(Server.MapPath(filePath) + PhotographName);
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                }
            }
        }
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
            string PresentCity = ddlPresentCity.Text;
            string PresentTaluka = ddlPresentTaluka.Text;

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
                //BindCityDropDOwn(ddlPermenentCity, PresentState);
            }
            if (!string.IsNullOrWhiteSpace(PresentCity) )
            {
                ddlPermenentCity.Text = PresentCity;
                //BindTalukaPermenentCode();
            }
            if (!string.IsNullOrWhiteSpace(PresentTaluka) )
            {
                ddlPermenentTaluka.Text = PresentTaluka;
            }
        }
        #endregion

        #region Save&Next Section
        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {

            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admission/Course.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


            // string strError = "";
            if (gvFamilyMember.Rows.Count < 1)
            {
                Functions.MessagePopup(this, "Add at list one family member information.", PopupMessageType.warning);
                txtFamilyPersonName.Focus();
                return;
            }
            StudentRegistrationDetailsBO objBo = new StudentRegistrationDetailsBO();
            StudentRegistrationBO objRegBo = new StudentRegistrationBO();
            using (StudentRegistrationDetailsBAL objData = new StudentRegistrationDetailsBAL())
            {
                if (FillForm(objBo, objRegBo))
                {
                    if (Page.IsValid)
                    {
                        if (objData.InsertRecord(objBo, objRegBo))
                        {
                            int PersonalInformationId = 0;
                            int ApplicationStatus = 1;
                            int strStudentId1 = Convert.ToInt32(strStudentId);
                            int strCourseId1 = Convert.ToInt32(strCourseId);
                            string username = SessionWrapper.StudentRegistration.Username;
                            DataSet dsverification = objBAL.GetStudentVerificationByStudentIdandCourseId(Convert.ToInt32(strStudentId), Convert.ToInt32(strCourseId));
                            if (dsverification != null && dsverification.Tables[0].Rows.Count > 0)
                            {
                                PersonalInformationId = Convert.ToInt32(dsverification.Tables[0].Rows[0]["WorkFlowInstanceID"].ToString());
                            }
                            else
                            {
                                objData.InsertWorkflowRecordstudent(strStudentId1, strCourseId1, ApplicationStatus, PersonalInformationId, username);
                            }
                            Functions.MessagePopup(this, "Student details insert sucessfully .....!", PopupMessageType.success);
                            ClearControl();
                            string strEndsQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
                            Response.Redirect("~/Admission/StudentAcademics.aspx?" + strEndsQueryString, false);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
                        }
                    }
                }
            }
        }
        private bool FillForm(StudentRegistrationDetailsBO objBo, StudentRegistrationBO objRegBo)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(lblFirstName.Text))
                {
                    Functions.MessagePopup(this, "Please Enter First Name", PopupMessageType.error);
                    lblFirstName.Focus();
                    return false;
                }
                else
                {
                    objRegBo.FirstName = lblFirstName.Text;
                }
                if (string.IsNullOrWhiteSpace(lblMiddleName.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Middle Name", PopupMessageType.error);
                    lblMiddleName.Focus();
                    return false;
                }
                else
                {
                    objRegBo.MiddleName = lblMiddleName.Text;
                }
                if (string.IsNullOrWhiteSpace(lblLastName.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Last Name", PopupMessageType.error);
                    lblLastName.Focus();
                    return false;
                }
                else
                {
                    objRegBo.LastName = lblLastName.Text;
                }            
                
                if (string.IsNullOrWhiteSpace(txtEmail.Text.Trim()))
                {
                    Functions.MessageFrontPopup(this, "Please Enter EmailId", PopupMessageType.error);
                    txtEmail.Focus();
                    return false;
                }
                else
                {

                    if (Functions.ValidateEmailId(txtEmail.Text.Trim()))
                    {
                        objRegBo.Email = txtEmail.Text.Trim();
                    }
                    else
                    {
                        Functions.MessageFrontPopup(this, "Please Enter Valid EmailId", PopupMessageType.error);
                    txtEmail.Focus();
                        return false;
                    }
                }
                string strError;
                DateTime? dt;
                if (string.IsNullOrWhiteSpace(lblDateOfBirth.Text.Trim()))
                {
                    Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                    lblDateOfBirth.Focus();
                    return false;
                }
                else
                {
                    if (!Functions.GetDateFromString(lblDateOfBirth.Text.Trim(), out dt, out strError))
                    {
                        objRegBo.DateOfBirth = (DateTime)dt;
                    }
                    else
                    {
                        Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    lblDateOfBirth.Focus();
                        return false;
                    }
                }
                if (ddlGender.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Please Select Gender", PopupMessageType.error);
                    ddlGender.Focus();
                    return false;
                }
                else
                {
                    objRegBo.Gender = ddlGender.SelectedValue;
                }
                if (ddlMaritalStatus.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Please Select Marital Status", PopupMessageType.error);
                    ddlMaritalStatus.Focus();
                    return false;
                }
                else
                {
                    objRegBo.MaritalStatus = ddlMaritalStatus.SelectedValue;
                }

                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admission/Course.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");




                objBo.StudentId = Convert.ToInt64(strStudentId);
                objBo.CourseId = Convert.ToInt64(strCourseId);
               
                objBo.RegistrationId = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
                SessionWrapper.RegistrationId = objBo.RegistrationId;
                //objBo.PostTypeId = Convert.ToInt32(ddlPostType.SelectedValue);
                if (!string.IsNullOrEmpty(lblAge.Text))
                    objBo.Age = Convert.ToInt64(lblAge.Text);
                //if (string.IsNullOrWhiteSpace(txtBirthPlace.Text.Trim()))
                //{
                //    Functions.MessageFrontPopup(this, "Enter place Of birth", PopupMessageType.error);
                //    txtBirthPlace.Focus();
                //    return false;
                //}
                //else
                //{
                objBo.PlaceOfBirth = txtBirthPlace.Text;
                //}
                if (ddlTitle.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select title", PopupMessageType.error);
                    ddlTitle.Focus();
                    return false;
                }
                else
                {
                    objBo.Title = ddlTitle.SelectedItem.Text.Trim().ToString();
                }
                
                if (ddlCast.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select caste", PopupMessageType.error);
                    ddlCast.Focus();
                    return false;
                }
                else
                {
                    objBo.CasteId = Convert.ToInt64(ddlCast.SelectedValue);
                }

                if (ddlReligion.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select religion", PopupMessageType.error);
                    ddlReligion.Focus();
                    return false;
                }
                else
                {
                    objBo.Religion = ddlReligion.SelectedValue;
                }
                if (!string.IsNullOrEmpty(txtBirthPlace.Text))
                {
                    objBo.PlaceOfBirth = txtBirthPlace.Text;
                }
                else
                {
                    objBo.PlaceOfBirth = "";
                }
                if (string.IsNullOrEmpty(txtPresentAddress.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter present address", PopupMessageType.error);
                    txtPresentAddress.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentAddress = txtPresentAddress.Text;
                }
                if (string.IsNullOrEmpty(txtPresentPincode.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter present pincode", PopupMessageType.error);
                    txtPresentPincode.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentPincode = txtPresentPincode.Text;
                }
                if (ddlPresentCountry.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select present country", PopupMessageType.error);
                    ddlPresentCountry.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentCountry = ddlPresentCountry.SelectedValue;
                }

                if (ddlPresentState.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select present state", PopupMessageType.error);
                    ddlPresentState.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentState = ddlPresentState.SelectedValue;
                }

                if (string.IsNullOrEmpty(ddlPresentCity.Text) )
                {
                    Functions.MessageFrontPopup(this, "Enter present city", PopupMessageType.error);
                    ddlPresentCity.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentCity = ddlPresentCity.Text;
                }
                if (string.IsNullOrEmpty(ddlPresentTaluka.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter present Taluka", PopupMessageType.error);
                    ddlPresentTaluka.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentTaluka = ddlPresentTaluka.Text;
                }

                if (!string.IsNullOrEmpty(txtPresentRPhoneNumber.Text))
                {
                    objBo.PresentPhoneR = txtPresentRPhoneNumber.Text;
                }
                else
                {
                    objBo.PresentPhoneR = "";
                }

                if (string.IsNullOrEmpty(txtPresentMPhoneNumber.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter phone number (M).", PopupMessageType.error);
                    txtPresentMPhoneNumber.Focus();
                    return false;
                }
                else
                {
                    objBo.PresentPhoneM = txtPresentMPhoneNumber.Text;
                }

                if (string.IsNullOrEmpty(txtPermenentAddress.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter permenent address", PopupMessageType.error);
                    txtPermenentAddress.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentAddress = txtPermenentAddress.Text;
                }
                if (string.IsNullOrEmpty(txtPermenentPincode.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter permenent pincode", PopupMessageType.error);
                    txtPermenentPincode.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentPincode = txtPermenentPincode.Text;
                }
                if (ddlPermenentCountry.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select permenent country", PopupMessageType.error);
                    ddlPermenentCountry.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentCountry = ddlPermenentCountry.SelectedValue;
                }
                if (ddlPermenentState.SelectedIndex == 0)
                {
                    Functions.MessageFrontPopup(this, "Select permenent state", PopupMessageType.error);
                    ddlPermenentState.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentState = ddlPermenentState.SelectedValue;
                }
                if (string.IsNullOrEmpty(ddlPermenentCity.Text))
                {
                    Functions.MessageFrontPopup(this, "Select permenent city", PopupMessageType.error);
                    ddlPermenentCity.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentCity = ddlPermenentCity.Text;
                }
                if (string.IsNullOrEmpty(ddlPermenentTaluka.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter permenent taluka", PopupMessageType.error);
                    ddlPermenentTaluka.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentTaluka = ddlPermenentTaluka.Text;
                }

                if (!string.IsNullOrEmpty(txtPermenentRPhoneNumber.Text))
                {
                    objBo.ParmenentPhoneR = txtPermenentRPhoneNumber.Text;
                }
                else
                {
                    objBo.ParmenentPhoneR = "";
                }

                if (string.IsNullOrEmpty(txtPermenentMPhoneNumber.Text))
                {
                    Functions.MessageFrontPopup(this, "Enter permenent phone number (M)", PopupMessageType.error);
                    txtPermenentMPhoneNumber.Focus();
                    return false;
                }
                else
                {
                    objBo.ParmenentPhoneM = txtPermenentMPhoneNumber.Text;
                }
                if (!string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.photoUploadName) || !string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.photoUploadpath))
                {
                    objBo.PhotographName = SessionWrapper.FileUploadDetails.photoUploadName;
                    objBo.PhotographPath = SessionWrapper.FileUploadDetails.photoUploadpath;
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Upload Photograph.", PopupMessageType.error);
                    fuPhotograph.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.signatureUploadName) || !string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.signatureUploadPath))
                {
                    objBo.SignatureName = SessionWrapper.FileUploadDetails.signatureUploadName;
                    objBo.SignaturePath = SessionWrapper.FileUploadDetails.signatureUploadPath;
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Upload Signature.", PopupMessageType.error);
                    fuSignature.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.dobUploadName) || !string.IsNullOrEmpty(SessionWrapper.FileUploadDetails.dobUploadPath))
                {
                    objBo.DOBFileName = SessionWrapper.FileUploadDetails.dobUploadName;
                    objBo.DOBFilePath = SessionWrapper.FileUploadDetails.dobUploadPath;
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Upload Proof Like: Aadhar Card, Leaving Certificate, Birth Certificate Orany issued certificate from government authority.", PopupMessageType.error);
                    fuDOB.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(hfStudentDetailsId.Value))
                {
                    objBo.Id = 0;
                }
                else
                {
                    objBo.Id = Convert.ToInt64(hfStudentDetailsId.Value);
                }
                if (!string.IsNullOrEmpty(SessionWrapper.StudentRegistration.Username))
                {
                    objBo.UserName = SessionWrapper.StudentRegistration.Username;
                }
                else
                {
                    Response.Redirect("~/Admission/LoginAdmission.aspx", false);
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
        protected void btnMember_Click(object sender, EventArgs e)
        {
            string strError = "";
            StudentFamilyDetailsBO objBo = new StudentFamilyDetailsBO();
            using (StudentFamilyDetailsBAL objData = new StudentFamilyDetailsBAL())
            {
                if (FillSubPageDetails(objBo))
                {
                    if (objData.InsertRecord(objBo))
                    {
                        Functions.MessagePopup(this, "Member Added Successfully .....!", PopupMessageType.success);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
                        BindFamilyGridView();
                        ClearFamilyDetails();
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.error);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
                    }
                }
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFamilyDetails();
        }
        private bool FillSubPageDetails(StudentFamilyDetailsBO objBo)
        {
            string strEndQueryString = Request.QueryString.ToString();
            if (string.IsNullOrWhiteSpace(strEndQueryString))
            {
                Response.Redirect("~/Admission/Course.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

            string[] strQuery = strQueryString.Split('|').ToArray();

            string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
            string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
            string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");


            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfRelativeId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt64(hfRelativeId.Value);
            }
            objBo.StudentId = Convert.ToInt64(strStudentId);
            objBo.CourseId = Convert.ToInt64(strCourseId);
            if (!string.IsNullOrWhiteSpace(txtFamilyPersonName.Text.Trim()))
            {
                objBo.MemberName = txtFamilyPersonName.Text.Trim();
            }
            else
            {
                Functions.MessageFrontPopup(this, "Enter member name", PopupMessageType.error);
                txtFamilyPersonName.Focus();
                return false;
            }
            long lgAge;
            if (string.IsNullOrWhiteSpace(txtFamilyAge.Text.Trim()) || !long.TryParse(txtFamilyAge.Text.Trim(), out lgAge))
            {
                Functions.MessageFrontPopup(this, "Enter age of family member.", PopupMessageType.error);
                txtFamilyAge.Focus();
                return false;
            }
            else
            {
                objBo.Age = lgAge;
            }
            if (string.IsNullOrWhiteSpace(txtRelation.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Enter relation", PopupMessageType.error);
                txtRelation.Focus();
                return false;
            }
            else
            {
                objBo.Relation = txtRelation.Text.Trim();
                objBo.RelationId = txtRelation.Text.Trim();
            }
            //if (ddlFamilyRelation.SelectedIndex <= 0)
            //{
            //    Functions.MessageFrontPopup(this, "Please Select Family Relation", PopupMessageType.error);
            //    return false;
            //}
            //else
            //{
            //    objBo.RelationId = Convert.ToInt32(ddlFamilyRelation.SelectedValue);
            //}
            if (string.IsNullOrWhiteSpace(txtFamilyOccupation.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Enter occupation", PopupMessageType.error);
                txtFamilyOccupation.Focus();
                return false;
            }
            else
            {
                objBo.Occupation = txtFamilyOccupation.Text.Trim();
            }
            if (string.IsNullOrWhiteSpace(txtFamilyMonthlyIncome.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Enter monthly income", PopupMessageType.error);
                txtFamilyMonthlyIncome.Focus();
                return false;
            }
            else
            {
                decimal dcIncome = 0;
                if (decimal.TryParse(txtFamilyMonthlyIncome.Text.Trim(), out dcIncome))
                {
                    objBo.MonthlyIncome = dcIncome;
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Enter valid monthly income", PopupMessageType.error);
                    txtFamilyMonthlyIncome.Focus();
                    return false;
                }
            }
            if (txtfamilycontactno.Text.Length > 12)
            {
                Functions.MessageFrontPopup(this, "Enter valid Contact Number.", PopupMessageType.error);
                txtfamilycontactno.Focus();
                return false;
            }
            else if (txtfamilycontactno.Text == txtPresentMPhoneNumber.Text)
            {
                Functions.MessageFrontPopup(this, "Enter valid Contact Number.", PopupMessageType.error);
                txtfamilycontactno.Focus();
                return false;
            }
            else
            {
                objBo.FamilyContactNumber = txtfamilycontactno.Text.ToString();
            }
            objBo.IsVisible = true;
            return true;
        }
        private void ClearFamilyDetails()
        {
            hfRelativeId.Value = "0";
            txtFamilyAge.Text = "";
            txtFamilyMonthlyIncome.Text = "";
            txtFamilyOccupation.Text = "";
            txtFamilyPersonName.Text = "";
            //ddlFamilyRelation.SelectedIndex = 0;
            txtRelation.Text = "";
            btnMember.Text = "Add Member";
            txtfamilycontactno.Text = "";
            BindFamilyGridView();
        }
        private void BindFamilyGridView()
        {
            try
            {

                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Admission/Course.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                string strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                string strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                string strCourseName = strQuery[2].ToString().Replace("CourseName=", "");



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
        protected void lnkFamilyMemberEdit_Click(object sender, EventArgs e)
        {
            //FillStudentDetailsByStudentId();
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            txtFamilyPersonName.Text = gvFamilyMember.Rows[rowindex].Cells[2].Text.Trim();
            txtFamilyAge.Text = gvFamilyMember.Rows[rowindex].Cells[3].Text.Trim();
            //ddlFamilyRelation.SelectedValue = gvFamilyMember.DataKeys[rowindex]["RelationId"].ToString();
            txtRelation.Text = gvFamilyMember.Rows[rowindex].Cells[4].Text.Trim();
            txtFamilyOccupation.Text = gvFamilyMember.Rows[rowindex].Cells[5].Text.Trim();
            txtFamilyMonthlyIncome.Text = gvFamilyMember.Rows[rowindex].Cells[6].Text.Trim();
            txtfamilycontactno.Text = gvFamilyMember.Rows[rowindex].Cells[7].Text.Trim();
            hfRelativeId.Value = gvFamilyMember.DataKeys[rowindex]["Id"].ToString();
            btnMember.Text = "Update Member";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
        }
        protected void lnkFamilyMemberDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvFamilyMember.DataKeys[rowindex]["Id"].ToString());
                StudentFamilyDetailsBO objbo = new StudentFamilyDetailsBO();
                StudentFamilyDetailsBAL objBAL = new StudentFamilyDetailsBAL();
                objbo.Id = Convert.ToInt64(rowId);
                if (objBAL.DeleteRecord(objbo))
                {
                    Functions.MessagePopup(this, "Member Delete Successfully .....!", PopupMessageType.success);
                    BindFamilyGridView();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
                }
                else
                {
                    Functions.MessagePopup(this, "Something went wrong please try again ....!", PopupMessageType.success);
                    BindFamilyGridView();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtFamilyPersonName.ClientID + "').focus();", true);
                }

            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion
        //private void BindTalukaPresentCode()
        //{
        //    try
        //    {
        //        CountryBAL objBal = new CountryBAL();
        //        ddlPresentTaluka.DataSource = objBal.SelectTalukaCode(Convert.ToInt32(ddlPresentCity.SelectedValue), 1);
        //        ddlPresentTaluka.DataTextField = "Name";
        //        ddlPresentTaluka.DataValueField = "Id";
        //        ddlPresentTaluka.DataBind();
        //        ddlPresentTaluka.Items.Insert(0, new ListItem("-Select Taluka Code-"));
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
        //    }
        //}

        //private void BindTalukaPermenentCode()
        //{
        //    try
        //    {
        //        CountryBAL objBal = new CountryBAL();
        //        ddlPermenentTaluka.DataSource = objBal.SelectTalukaCode(Convert.ToInt32(ddlPermenentCity.SelectedValue), 1);
        //        ddlPermenentTaluka.DataTextField = "Name";
        //        ddlPermenentTaluka.DataValueField = "Id";
        //        ddlPermenentTaluka.DataBind();
        //        ddlPermenentTaluka.Items.Insert(0, new ListItem("-Select Taluka Code-"));
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
        //    }
        //}


        //protected void ddlPresentCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPresentCity.SelectedIndex > 0)
        //    {
        //        BindTalukaPresentCode();
        //        //BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
        //        ddlPresentTaluka.SelectedIndex = 0;
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentTaluka.ClientID + "').focus();", true);
        //    }
        //    else
        //    {
        //        if (ddlPresentTaluka.Items.Count > 0)
        //        {
        //            var firstitem = ddlPresentTaluka.Items[0];
        //            ddlPresentTaluka.Items.Clear();
        //            ddlPresentTaluka.Items.Add(firstitem);
        //        }
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPresentTaluka.ClientID + "').focus();", true);
        //    }

        //}

        //protected void ddlPermenentCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPermenentCity.SelectedIndex > 0)
        //    {
        //        BindTalukaPermenentCode();
        //        //BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
        //        ddlPermenentTaluka.SelectedIndex = 0;
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentTaluka.ClientID + "').focus();", true);
        //    }
        //    else
        //    {
        //        if (ddlPermenentTaluka.Items.Count > 0)
        //        {
        //            var firstitem = ddlPermenentTaluka.Items[0];
        //            ddlPermenentTaluka.Items.Clear();
        //            ddlPermenentTaluka.Items.Add(firstitem);
        //        }
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + ddlPermenentTaluka.ClientID + "').focus();", true);
        //    }
        //}

        protected void lblDateOfBirth_TextChanged(object sender, EventArgs e)
        {

            DateTime? dt;
            if (string.IsNullOrWhiteSpace(lblDateOfBirth.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                return ;
            }
            else
            {

                string strError;
                if (!Functions.GetDateFromString(lblDateOfBirth.Text.Trim(), out dt, out strError))
                {
                    lblAge.Text =((DateTime)dt).ToAgeString(DateTime.Now).Years.ToString();
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    return ;
                }
            }
        }

        protected void btnBackToGrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admission/Course.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}