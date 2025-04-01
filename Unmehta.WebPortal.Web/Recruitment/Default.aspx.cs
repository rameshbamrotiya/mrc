using BAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class Default : System.Web.UI.Page
    {
        public static DateTime dtMinDate;
        public static int lgRequiredFromAge;
        public static int lgRequiredToAge;
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=1|RegId=20210630073548|CandId=1"));
                //    SessionWrapper.UserDetails = new Model.Common.SessionUserModel { UserName = "test" };

                try
                {
                    if (string.IsNullOrWhiteSpace(SessionWrapper.UserDetails.UserName))
                    {
                        Response.Redirect("~/Recruitment/Careers");
                    }
                    txtEmail.Value = SessionWrapper.UserDetails.UserName;
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Recruitment/Careers");
                }
                string strEndQueryString = Request.QueryString.ToString();


                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Recruitment/Careers");
                }

                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();

                strJobId = strQuery[0].ToString().Replace("JobId=", "");
                strRegId = strQuery[1].ToString().Replace("RegId=", "");
                strCandId = strQuery[2].ToString().Replace("CandId=", "");

                using (IRecruitmentAdvertisementRepository objRecruitmentAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
                {
                    var MainData = objRecruitmentAdvertisementRepository.GetTblRecruitmentAdvertisementById(Convert.ToInt32(strJobId));
                    lblPostAppliedFor.Text = MainData.AdvertisementName;
                    lgRequiredToAge = (int)MainData.MaxAge;
                    DateTime dtMin = Convert.ToDateTime(MainData.AgeLimitCalOn).AddYears((0 - (int)MainData.MaxAge));
                    dtMinDate = dtMin;
                    hfMinDate.Value = dtMinDate.ToString("yyyy/MM/dd");
                    hfJobId.Value = strJobId;
                    hfRegId.Value = strRegId;
                    DateTime dt = DateTime.Now.Date;
                    if (!(MainData.EndDate >= dt && MainData.StartDate <= dt) || MainData.IsActive != true)
                    {
                        Response.Redirect("~/Recruitment/Careers");
                    }
                }

                BindRelationDropDown();
                BindCastDropDown();
                BindMaritalStatusTypeDropDown();
                BindGenderDropDown();
                BindCountryCode();
                BindFamilyGridView();

                if (!string.IsNullOrWhiteSpace(strCandId))
                {
                    FillFormAsperRegId(strCandId);
                }

                GetSpouseDetails();
            }
        }

        protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSpouseDetails();
        }

        protected void PresentAddress_Click(object sender, EventArgs e)
        {
            string PresentAddress = txtPresentAddress.Value;
            string PresentPincode = txtPresentPincode.Text;
            string PresentMPhoneNumber = txtPresentMPhoneNumber.Text;
            string PresentRPhoneNumber = txtPresentRPhoneNumber.Text;
            string PresentCountry = ddlPresentCountry.SelectedValue;
            string PresentState = ddlPresentState.SelectedValue;
            string PresentCity = ddlPresentCity.SelectedValue;

            if (!string.IsNullOrWhiteSpace(PresentAddress))
            {
                txtPermenentAddress.Value = PresentAddress;
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

        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICandidateDetailsRepository objAcc = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateDetailsModel objBO = new CandidateDetailsModel();
                if (FillBasicModel(objBO))
                {
                    if (!objAcc.InsertOrUpdateTblCandidateDetails(objBO, out errorMessage))
                    {
                        if (GetListValueFamilyDetails.Count > 0)
                        {
                            GetListValueFamilyDetails.ForEach(x => { x.CandidateId = objBO.Id; x.RegistrationId = objBO.RegistrationId; x.IsVisible = true; });
                            foreach (var row in GetListValueFamilyDetails.ToList())
                            {
                                if (objAcc.InsertOrUpdateTblCandidateFamilyDetails(row, out errorMessage))
                                {
                                    return;
                                }
                            }
                        }
                        ClearFamilyDetails();
                        BindFamilyGridView();
                        Functions.MessageFrontPopup(this, errorMessage, PopupMessageType.success);
                        string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + objBO.Advertisementid + "|RegId=" + objBO.RegistrationId + "|CandId=" + objBO.Id));
                        Response.Redirect("~/Recruitment/AcademicDetails?" + strEndQueryString);
                    }
                    else
                    {
                        Functions.MessageFrontPopup(this, errorMessage, PopupMessageType.error);
                        return;
                    }
                }
            }
        }
        #endregion

        #region Page Functions
        private void BindGenderDropDown()
        {
            ddlGender.DataSource = GetAll<GenderType>();
            ddlGender.DataTextField = "Value";
            ddlGender.DataValueField = "Key";
            ddlGender.DataBind();
        }

        private void BindMaritalStatusTypeDropDown()
        {
            ddlMaritalStatus.DataSource = GetAll<MaritalStatusType>();
            ddlMaritalStatus.DataTextField = "Value";
            ddlMaritalStatus.DataValueField = "Value";
            ddlMaritalStatus.DataBind();
        }

        private void FillFormAsperRegId(string strRegId)
        {
            BindRelationDropDown();
            BindCountryCode();
            CandidateDetailsModel objBo = new CandidateDetailsModel();
            using (ICandidateDetailsRepository objAcc = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                objBo = objAcc.GetAllTblCandidateDetails().Where(x => x.Id == Convert.ToInt32(strRegId)).FirstOrDefault();
                if (objBo != null)
                {
                    hfCandidateId.Value = objBo.Id.ToString();
                    hfJobId.Value = objBo.Advertisementid.ToString();

                    txtFirstName.Value = objBo.FirstName;
                    txtMiddleName.Value = objBo.MiddleName;
                    txtLastName.Value = objBo.LastName;

                    txtEmail.Value = objBo.EmailId;
                    txtAadharCard.Text = objBo.AadharCard;

                    txtPermenentAddress.Value = objBo.PermenentAddress;
                    txtPermenentPincode.Text = objBo.PermenentPincode;
                    txtPermenentMPhoneNumber.Text = objBo.PermenentPhoneM;
                    txtPermenentRPhoneNumber.Text = objBo.PermenentPhoneR;

                    txtPresentAddress.Value = objBo.PresentAddress;
                    txtPresentPincode.Text = objBo.PresentPincode;
                    txtPresentMPhoneNumber.Text = objBo.PresentPhoneM;
                    txtPresentRPhoneNumber.Text = objBo.PresentPhoneR;

                    ddlPresentCountry.SelectedValue = objBo.PresentCountry;

                    if (ddlPresentCountry.SelectedIndex > 0)
                    {
                        BindStateDropDOwn(ddlPresentState, ddlPresentCountry.SelectedValue);
                        ddlPresentState.SelectedValue = objBo.PresentState;
                    }

                    if (ddlPresentState.SelectedIndex > 0)
                    {
                        BindCityDropDOwn(ddlPresentCity, ddlPresentState.SelectedValue);
                        ddlPresentCity.SelectedValue = objBo.PresentCity;
                    }

                    ddlPermenentCountry.SelectedValue = objBo.PermenentCountry;
                    if (ddlPermenentCountry.SelectedIndex > 0)
                    {
                        BindStateDropDOwn(ddlPermenentState, ddlPermenentCountry.SelectedValue);
                        ddlPermenentState.SelectedValue = objBo.PermenentState;
                    }

                    if (ddlPermenentState.SelectedIndex > 0)
                    {
                        BindCityDropDOwn(ddlPermenentCity, ddlPermenentState.SelectedValue);
                        ddlPermenentCity.SelectedValue = objBo.PermenentCity;
                    }
                    ddlCountryCode.SelectedItem.Text = objBo.CountryCode;
                    if (string.IsNullOrWhiteSpace(objBo.CountryCode))
                    {
                        BindCountryCode();
                    }
                    ddlcountrycode1.SelectedItem.Text = objBo.CountryCode1;
                    if (string.IsNullOrWhiteSpace(objBo.CountryCode1))
                    {
                        BindCountryCode();
                    }
                    //string strScript = "";

                    //strScript += "var PresentCountry = '" + objBo.PresentCountry + "';";
                    //strScript += "var PresentState = '" + objBo.PresentState + "';";
                    //strScript += "var PresentCity = '" + objBo.PresentCity + "';";

                    //strScript += "var PermenentCountry = '" + objBo.PermenentCountry + "';";
                    //strScript += "var PermenentState = '" + objBo.PermenentState + "';";
                    //strScript += "var PermenentCity = '" + objBo.PermenentCity + "';";

                    //strScript += "if (!(PresentCountry === undefined || PresentCountry === null))";
                    //strScript += "{";
                    //strScript += "    GetCountry('"+ddlPresentCountry.ClientID+ "','" + hfPresentCountry.ClientID + "', PresentCountry);";
                    //strScript += "    if (!(PresentState === undefined || PresentState === null))";
                    //strScript += "    {";
                    //strScript += "        GetState('"+ddlPresentState.ClientID+ "','" + hfPresentState.ClientID + "', PresentCountry, PresentState);";
                    //strScript += "    }";
                    //strScript += "    if (!(PresentCity === undefined || PresentCity === null))";
                    //strScript += "    {";
                    //strScript += "        GetCities('"+ddlPresentCity.ClientID+ "','" + hfPresentCity.ClientID + "', PresentState, PresentCity);";
                    //strScript += "    }";
                    //strScript += "}";

                    //strScript += "if (!(PermenentCountry === undefined || PermenentCountry === null))";
                    //strScript += "{";
                    //strScript += "    GetCountry('"+ddlPermenentCountry.ClientID+ "','" + hfPermenentCountry.ClientID + "', PermenentCountry);";
                    //strScript += "    if (!(PermenentState === undefined || PermenentState === null))";
                    //strScript += "    {";
                    //strScript += "        GetState('"+ddlPermenentState.ClientID+ "','" + hfPermenentState.ClientID + "', PermenentCountry, PermenentState);";
                    //strScript += "    }";
                    //strScript += "    if (!(PermenentCity === undefined || PermenentCity === null))";
                    //strScript += "    {";
                    //strScript += "        GetCities('"+ddlPermenentCity.ClientID+ "','" + hfPermenentCity.ClientID + "', PermenentState, PermenentCity);";
                    //strScript += "    }";
                    //strScript += "}";

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertScs", "<script>"+ strScript + "</script>", false);

                    hfSignatureName.Value = objBo.SignatureName;
                    if (!string.IsNullOrWhiteSpace(hfSignatureName.Value))
                    {
                        imgSignature.ImageUrl = ResolveUrl(ConfigDetailsValue.CandidateDetailsSignature) + hfSignatureName.Value;
                        imgSignature.Visible = true;
                    }
                    ddlReligion.SelectedValue = objBo.Religion;
                    txtDateOfBirth.Text = objBo.DateOfBirth.ToString("dd/MM/yyyy");
                    hfAge.Value = objBo.Age.ToString();
                    txtAge.InnerHtml = objBo.Age.ToString();
                    ddlCast.SelectedValue = objBo.CasteId.Value.ToString();
                    ddlGender.SelectedValue = objBo.Gender.Value.ToString();
                    ddlMaritalStatus.SelectedValue = objBo.MaritalStatus;
                    hfPhotographName.Value = objBo.PhotographName;
                    if (!string.IsNullOrWhiteSpace(hfPhotographName.Value))
                    {
                        imgPhotoGraph.ImageUrl = ResolveUrl(ConfigDetailsValue.CandidateDetailsPhotograph) + hfPhotographName.Value;
                        imgPhotoGraph.Visible = true;
                    }
                    txtSpouseFirstName.Value = objBo.SpouseFirstName;
                    txtSpouseMiddleName.Value = objBo.SpouseMiddleName;
                    txtSpouseLastName.Value = objBo.SpouseSurname;
                    txtSpousePhoneNumber.Text = objBo.SpouseContact;
                    txtSpouseDOB.Value = objBo.SpouseDOB == null ? "" : ((DateTime)objBo.SpouseDOB).ToString("dd/MM/yyyy");

                    var familyDetails = objAcc.GetAllTblCandidateFamilyDetails().Where(x => x.CandidateId == objBo.Id).ToList();

                    if (familyDetails != null)
                    {
                        GetListValueFamilyDetails = objAcc.GetAllTblCandidateFamilyDetails().Where(x => x.CandidateId == objBo.Id).ToList();
                        BindFamilyGridView();
                    }
                }
            }
        }

        private bool FillBasicModel(CandidateDetailsModel objBo)
        {
            #region Basic Details

            if (string.IsNullOrWhiteSpace(hfRegId.Value))
            {
                objBo.RegistrationId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
            else
            {
                objBo.RegistrationId = hfRegId.Value;
            }

            if (string.IsNullOrWhiteSpace(hfJobId.Value))
            {
                Functions.MessageFrontPopup(this, "Please Select Job First", PopupMessageType.error);
                return false;
            }
            else if (hfJobId.Value == "0")
            {
                Functions.MessageFrontPopup(this, "Please Select Job First", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Advertisementid = Convert.ToInt32(hfJobId.Value);
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter First Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.FirstName = txtFirstName.Value.Trim();
            }
            if (string.IsNullOrWhiteSpace(txtMiddleName.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Middle Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MiddleName = txtMiddleName.Value.Trim();
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Last Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.LastName = txtLastName.Value.Trim();
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter EmailId", PopupMessageType.error);
                return false;
            }
            else
            {

                if (Functions.ValidateEmailId(txtEmail.Value.Trim()))
                {
                    objBo.EmailId = txtEmail.Value.Trim();
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Please Enter Valid EmailId", PopupMessageType.error);
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtAadharCard.Text.Trim()))
            {
                if (txtAadharCard.Text.Trim().Length != 12)
                {
                    Functions.MessageFrontPopup(this, "Please Enter Valid Aadhar Card No", PopupMessageType.error);
                    return false;
                }
                else
                {
                    objBo.AadharCard = txtAadharCard.Text.Trim();
                }
            }
            #region Validation Address
            if (string.IsNullOrWhiteSpace(txtPermenentAddress.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Permenent Address", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PermenentAddress = txtPermenentAddress.Value.Trim();
            }

            if (string.IsNullOrWhiteSpace(txtPermenentPincode.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Permenent Pincode", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PermenentPincode = txtPermenentPincode.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtPermenentMPhoneNumber.Text.Trim()))
            {
                objBo.PermenentPhoneM = txtPermenentMPhoneNumber.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(txtPermenentRPhoneNumber.Text.Trim()))
            {
                objBo.PermenentPhoneR = txtPermenentRPhoneNumber.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(txtPresentAddress.Value.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Present Address", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PresentAddress = txtPresentAddress.Value.Trim();
            }
            if (string.IsNullOrWhiteSpace(txtPresentPincode.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter Present Pincode", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PresentPincode = txtPresentPincode.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtPresentMPhoneNumber.Text.Trim()))
            {
                objBo.PresentPhoneM = txtPresentMPhoneNumber.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(txtPresentRPhoneNumber.Text.Trim()))
            {
                objBo.PresentPhoneR = txtPresentRPhoneNumber.Text.Trim();
            }
            #endregion

            if (ddlPresentCountry.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Present Country", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PresentCountry = ddlPresentCountry.SelectedValue;
            }
            if (ddlCountryCode.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Country Code", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.CountryCode = ddlCountryCode.SelectedItem.Text.ToString();
            }
            if (ddlcountrycode1.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Country Code", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.CountryCode1 = ddlcountrycode1.SelectedItem.Text.ToString();
            }
            if (ddlPresentState.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Present State", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PresentState = ddlPresentState.SelectedValue;
            }

            if (ddlPresentCity.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Present City", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PresentCity = ddlPresentCity.SelectedValue;
            }


            if (ddlPermenentCountry.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Permenent Country", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PermenentCountry = ddlPermenentCountry.SelectedValue;
            }

            if (ddlPermenentState.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Permenent State", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PermenentState = ddlPermenentState.SelectedValue;
            }

            if (ddlPresentCity.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Permenent City", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.PermenentCity = ddlPermenentCity.SelectedValue;
            }


            if (ddlReligion.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Enter Religion", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Religion = ddlReligion.SelectedValue.Trim();
            }
            if (string.IsNullOrWhiteSpace(txtDateOfBirth.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                return false;
            }
            else
            {
                string strError;
                DateTime? dt;
                if (!Functions.GetDateFromString(txtDateOfBirth.Text.Trim(), out dt, out strError))
                {
                    objBo.DateOfBirth = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    return false;
                }
            }

            long lgAge;
            if (string.IsNullOrWhiteSpace(hfAge.Value.Trim()) || !long.TryParse(hfAge.Value.Trim(), out lgAge))
            {
                Functions.MessageFrontPopup(this, "Please Enter Age", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Age = lgAge;
                using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
                {
                    var gvData = objAdvertisementRepository.GetTblRecruitmentAdvertisementById(Convert.ToInt32(strJobId));
                    if (gvData != null)
                    {
                        if (gvData.MaxAge <= lgAge)
                        {
                            Functions.MessageFrontPopup(this, "Max Age Allow for this post is " + gvData.MaxAge, PopupMessageType.error);
                            return false;

                        }
                    }
                }
            }


            {
                objBo.CasteId = Convert.ToInt32(ddlCast.SelectedValue);
            }

            {
                objBo.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            }
            {
                objBo.MaritalStatus = (ddlMaritalStatus.SelectedItem.Text);
            }

            #region Fileupload Validation
            if (fuPhotoGraph.HasFile)
            {
                string filePath = ConfigDetailsValue.CandidateDetailsPhotograph;

                System.Drawing.Image img = System.Drawing.Image.FromStream(fuPhotoGraph.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round(((decimal)fuPhotoGraph.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 300 || width != 280)
                //{
                //    Functions.MessagePopup(this, "Please upload 280px*300px.", PopupMessageType.error);
                //    return false;
                //}

                if (!filePath.Contains("|"))
                {
                    //if (fuPhotoGraph.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.PhotographName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPhotoGraph.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + objBo.PhotographName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(fuPhotoGraph.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        objBo.PhotographPath = filePath + objBo.PhotographName;
                        //Save selected file into specified location
                        fuPhotoGraph.SaveAs(Server.MapPath(filePath) + objBo.PhotographName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(hfPhotographName.Value))
                {
                    objBo.PhotographPath = ConfigDetailsValue.CandidateDetailsPhotograph + hfPhotographName.Value;
                    objBo.PhotographName = System.IO.Path.GetFileName(ConfigDetailsValue.CandidateDetailsPhotograph + hfPhotographName.Value);
                }
                else
                {
                    Functions.MessagePopup(this, "Please upload Photograph.", PopupMessageType.warning);
                    return false;
                }
            }


            if (fuSignature.HasFile)
            {
                string filePath = ConfigDetailsValue.CandidateDetailsSignature;

                System.Drawing.Image img = System.Drawing.Image.FromStream(fuSignature.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round(((decimal)fuSignature.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 300 || width != 280)
                //{
                //    Functions.MessagePopup(this, "Please upload 280px*300px.", PopupMessageType.error);
                //    return false;
                //}

                if (!filePath.Contains("|"))
                {
                    //if (fuSignature.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.SignatureName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuSignature.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + objBo.SignatureName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(fuSignature.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }
                        objBo.SignaturePath = filePath + objBo.SignatureName;

                        //Save selected file into specified location
                        fuSignature.SaveAs(Server.MapPath(filePath) + objBo.SignatureName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(hfSignatureName.Value))
                {
                    objBo.SignaturePath = ConfigDetailsValue.CandidateDetailsSignature + hfSignatureName.Value;
                    objBo.SignatureName = System.IO.Path.GetFileName(ConfigDetailsValue.CandidateDetailsSignature + hfSignatureName.Value);
                }
                else
                {
                    Functions.MessagePopup(this, "Please upload Signature.", PopupMessageType.warning);
                    return false;
                }
            }
            #endregion

            #endregion

            #region Validation Spouse
            if (ddlMaritalStatus.SelectedIndex == 1)
            {
                if (string.IsNullOrWhiteSpace(txtSpouseFirstName.Value.Trim()))
                {
                    Functions.MessageFrontPopup(this, "Please Enter Spouse First Name", PopupMessageType.error);
                    return false;
                }
                else
                {
                    objBo.SpouseFirstName = txtSpouseFirstName.Value.Trim();
                }
                if (string.IsNullOrWhiteSpace(txtSpouseMiddleName.Value.Trim()))
                {
                    Functions.MessageFrontPopup(this, "Please Enter Spouse Middle Name", PopupMessageType.error);
                    return false;
                }
                else
                {
                    objBo.SpouseMiddleName = txtSpouseMiddleName.Value.Trim();
                }

                if (string.IsNullOrWhiteSpace(txtSpouseLastName.Value.Trim()))
                {
                    Functions.MessageFrontPopup(this, "Please Enter Spouse Last Name", PopupMessageType.error);
                    return false;
                }
                else
                {
                    objBo.SpouseSurname = txtSpouseLastName.Value.Trim();
                }
                string strError;
                DateTime? dt;
                if (!Functions.GetDateFromString(txtSpouseDOB.Value.Trim(), out dt, out strError))
                {
                    objBo.SpouseDOB = dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Spouse Date Of Birth", PopupMessageType.error);
                    return false;
                }
                objBo.SpouseContact = txtSpousePhoneNumber.Text.Trim();
            }
            #endregion

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfCandidateId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfCandidateId.Value);
            }

            return true;
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

            using (IRecruitmentRelationRepository objRecruitmentRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            {
                ddlFamilyRelation.DataSource = objRecruitmentRelationRepository.GetAllTblRecruitmentRelation();
                ddlFamilyRelation.DataValueField = "Id";
                ddlFamilyRelation.DataTextField = "Name";
                ddlFamilyRelation.DataBind();
                ddlFamilyRelation.Items.Insert(0, "Select");
            }
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
                ddlFamilyRelation.Items.Insert(0, "Select");
            }
        }

        private void GetSpouseDetails()
        {
            if (ddlMaritalStatus.SelectedIndex == 1)
            {
                SpouseFname.Visible = true;
                SpouseMname.Visible = true;
                SpouseLname.Visible = true;
                SpouseDOB.Visible = true;
                SpousePNo.Visible = true;
            }
            else
            {
                SpouseFname.Visible = false;
                SpouseMname.Visible = false;
                SpouseLname.Visible = false;
                SpouseDOB.Visible = false;
                SpousePNo.Visible = false;
            }
        }
        #endregion

        #region Sub Page Details

        #region Functions
        private bool FillSubPageDetails(CandidateFamilyDetailsModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfRelativeId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfRelativeId.Value);
            }


            if (!string.IsNullOrWhiteSpace(txtFamilyPersonName.Value.Trim()))
            {
                objBo.MemberName = txtFamilyPersonName.Value.Trim();
            }
            else
            {
                Functions.MessageFrontPopup(this, "Please Enter Member Name", PopupMessageType.error);
                return false;
            }

            long lgAge;
            if (string.IsNullOrWhiteSpace(txtFamilyAge.Text.Trim()) || !long.TryParse(txtFamilyAge.Text.Trim(), out lgAge))
            {
                Functions.MessageFrontPopup(this, "Please Enter Age of Family Member.", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Age = lgAge;
            }

            if (ddlFamilyRelation.SelectedIndex <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Family Relation", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.RelationId = Convert.ToInt32(ddlFamilyRelation.SelectedValue);
            }


            if (!string.IsNullOrWhiteSpace(txtFamilyOccupation.Value.Trim()))
            {
                objBo.Occupation = txtFamilyOccupation.Value.Trim();
            }


            if (!string.IsNullOrWhiteSpace(txtFamilyMonthlyIncome.Value.Trim()))
            {
                decimal dcIncome = 0;
                if (decimal.TryParse(txtFamilyMonthlyIncome.Value.Trim(), out dcIncome))
                {
                    objBo.MonthlyIncome = dcIncome;
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Please Enter Valid Monthly Income", PopupMessageType.error);
                    return false;
                }
            }

            return true;
        }

        private void ClearFamilyDetails()
        {
            txtFamilyAge.Text = "";
            txtFamilyMonthlyIncome.Value = "";
            txtFamilyOccupation.Value = "";
            txtFamilyPersonName.Value = "";
            ddlFamilyRelation.SelectedIndex = 0;
            btnMember.Text = "Add Member";
            BindFamilyGridView();
        }

        private void BindFamilyGridView()
        {
            using (IRecruitmentRelationRepository objRecruitmentRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            {
                GetListValueFamilyDetails.ToList().ForEach(x =>
                    x.RelationName = objRecruitmentRelationRepository.GetTblRecruitmentRelationById((long)x.RelationId).Name.ToString()
                );
                gvFamilyMember.DataSource = GetListValueFamilyDetails;
                gvFamilyMember.DataBind();
                btnSaveAndNext.Focus();
            }
        }
        private void BindCountryCode()
        {
            try
            {
                CountryBAL objBal = new CountryBAL();
                ddlCountryCode.DataSource = objBal.SelectCountryCode();
                ddlCountryCode.DataTextField = "Country_phone_code";
                ddlCountryCode.DataValueField = "Id";
                ddlCountryCode.DataBind();
                ddlCountryCode.Items.Insert(0, new ListItem("-Select Country Code-"));
                ddlcountrycode1.DataSource = objBal.SelectCountryCode();
                ddlcountrycode1.DataTextField = "Country_phone_code";
                ddlcountrycode1.DataValueField = "Id";
                ddlcountrycode1.DataBind();
                ddlcountrycode1.Items.Insert(0, new ListItem("-Select Country Code-"));
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
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

        private List<CandidateFamilyDetailsModel> GetListValueFamilyDetails
        {
            get
            {
                List<CandidateFamilyDetailsModel> result = new List<CandidateFamilyDetailsModel>();

                if (ViewState["gvFamilyMember"] != null)
                {
                    result = (List<CandidateFamilyDetailsModel>)ViewState["gvFamilyMember"];
                }
                else
                {
                    result = new List<CandidateFamilyDetailsModel>();
                }

                return result;
            }
            set
            {
                ViewState["gvFamilyMember"] = value;
            }
        }
        #endregion

        #region Page Functions
        protected void btnMember_Click(object sender, EventArgs e)
        {
            CandidateFamilyDetailsModel objBo = new CandidateFamilyDetailsModel();
            if (FillSubPageDetails(objBo))
            {
                List<CandidateFamilyDetailsModel> dt = new List<CandidateFamilyDetailsModel>();
                dt = GetListValueFamilyDetails;
                bool isExist = false;
                foreach (var row in dt.ToList())
                {
                    if (row.Id == 0 && row.MemberName == objBo.MemberName && row.Occupation == objBo.Occupation && row.RelationId == objBo.RelationId)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist && objBo.Id == 0)
                {
                    dt.Add(objBo);
                    GetListValueFamilyDetails = dt;
                    Functions.MessageFrontPopup(this, "Member Added Successfully.", PopupMessageType.success);
                    ClearFamilyDetails();
                }
                else if (objBo.Id != 0)
                {
                    var mainObject = dt.Where(x => x.Id == objBo.Id).FirstOrDefault();
                    dt.Remove(mainObject);
                    dt.Add(objBo);
                    Functions.MessageFrontPopup(this, "Member Updated Successfully.", PopupMessageType.success);
                    ClearFamilyDetails();
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Already Record Exist.", PopupMessageType.error);
                    return;
                }
            }
        }

        protected void ibtn_FamilyEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string Id = gvFamilyMember.DataKeys[rowindex]["Id"].ToString();
            int i = 0;
            foreach (var row in GetListValueFamilyDetails)
            {
                if (i == rowindex)
                {
                    hfRelativeId.Value = row.Id.ToString();
                    txtFamilyPersonName.Value = row.MemberName;
                    txtFamilyOccupation.Value = row.Occupation;
                    ddlFamilyRelation.SelectedValue = row.RelationId.ToString();
                    txtFamilyAge.Text = row.Age.ToString();
                    txtFamilyMonthlyIncome.Value = row.MonthlyIncome.ToString();
                    btnMember.Text = "Update Member";
                    btnSaveAndNext.Focus();
                    break;
                }
                i++;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFamilyDetails();
        }

        protected void ibtn_FamilyDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string Id = gvFamilyMember.DataKeys[rowindex]["Id"].ToString();
            int i = 0;
            List<CandidateFamilyDetailsModel> lstData = GetListValueFamilyDetails;
            foreach (var row in GetListValueFamilyDetails)
            {
                if (i == rowindex)
                {
                    if (row.Id.ToString() == "0")
                    {
                        lstData.Remove(GetListValueFamilyDetails.Where(x => x.MemberName == row.MemberName && x.Occupation == row.Occupation && x.RelationId == row.RelationId).FirstOrDefault());
                    }
                    else
                    {
                        using (ICandidateDetailsRepository objAcc = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                        {
                            string errorMessage = "";
                            objAcc.RemoveTblCandidateFamilyDetails(Convert.ToInt32(Id), out errorMessage);
                            lstData.Remove(GetListValueFamilyDetails.Where(x => x.Id == row.Id).FirstOrDefault());
                        }
                    }
                    break;
                }

                i++;
            }
            ClearFamilyDetails();
        }

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

        #endregion

    }
}