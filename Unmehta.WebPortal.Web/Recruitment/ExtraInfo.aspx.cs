using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class ExtraInfo : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //string strEndQueryString = "Sm9iSWQ9MXxSZWdJZD0yMDIxMDYzMDA3MzU0OHxDYW5kSWQ9MQ%3d%3d";
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Recruitment/Careers.aspx");
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
                    FillDataForEditMode();
                    BindProfessonalReferalGrid();
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);

                    Response.Redirect("~/Recruitment/Careers");
                }
            }
        }

        protected void btnKnowPersonDetails_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICandidateDetailsRepository objRecruitmentCastRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateProfessionalReferralDetailsModel objBO = new CandidateProfessionalReferralDetailsModel();
                if (LoadControlsForReferalDetails(objBO))
                {
                    if (!objRecruitmentCastRepository.InsertOrUpdateTblCandidateProfessionalReferralDetails(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindProfessonalReferalGrid();
                        //Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }

            }
        }
        private void BindProfessonalReferalGrid()
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<CandidateProfessionalReferralDetailsModel> lstData = new List<CandidateProfessionalReferralDetailsModel>();
                    lstData = objCandidateDetailsRepository.GetAllCandidateProfessionalReferralDetailsByCandId(Convert.ToInt32(strCandId));
                    grdReferalDetails.DataSource = lstData;
                    grdReferalDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private void FillDataForEditMode()
        {
            try
            {
                CandidateDetailsModel objBo = new CandidateDetailsModel();
                using (ICandidateDetailsRepository objAcc = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    objBo = objAcc.GetTblCandidateDetailsById(Convert.ToInt32(strCandId));
                    if (objBo != null)
                    {
                        //hfReferralId.Value = objBo.Id.ToString();
                        

                        txtExtraActivities.Text = objBo.ExtraActivities;
                        txtMemberShip.Text = objBo.MemberShipOfAnySpecialBodies;
                        txtChronicillness.Text = objBo.ChronicIllnessDetails;

                        txtLawOffence.Text = objBo.OffneceDetails;
                        txtselfDetails.Text = objBo.DescribeYourself;                      
                        txtAchievement.Text = objBo.BiggestAchivement;
                        txtFrustratingExperinece.Text = objBo.YourBiggestFailure;
                        txtFutreVision.Text = objBo.VisionInNextYears;
                        
                        txtEmergencyContactNo.Text = objBo.EmergencyContactNo;
                        txtEmergencyContact.Text = objBo.EmergencyContactPersonName;
                        txtPersoninUNMICR.Text = objBo.UNMICRCContact;

                      
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private bool LoadControlsForReferalDetails(CandidateProfessionalReferralDetailsModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfReferralId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfReferralId.Value);
            }

            objBo.CandidateId = Convert.ToInt32(strCandId);
            objBo.Name = txtKnownPersonName.Text.ToString();
            objBo.Position = txtPosition.Text.ToString();
            objBo.MobileNo = txtTelePhone.Text.Trim();
            objBo.RelationShip = txtRelatioship.Text.ToString();
            objBo.YearsKnown = Convert.ToDouble(txtYearsKnown.Text.ToString());
            objBo.Address = txtAddress.Text.ToString();
            objBo.Regid = strRegId;
            return true;
        }
        private void ClearControlValues()
        {
            txtKnownPersonName.Text = "";
            txtPosition.Text = "";
            txtTelePhone.Text = "";
            txtRelatioship.Text = "";
            txtYearsKnown.Text = "";
            txtAddress.Text = "";
            hfReferralId.Value = "";
            btnKnowPersonDetails.Text = "Add Details";
            //BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICandidateDetailsRepository objRecruitmentCastRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateDetailsModel objBO = new CandidateDetailsModel();
                if (LoadControlsForExtraDetails(objBO))
                {
                    if (!objRecruitmentCastRepository.UpdateCandidateDetailsExtraInfo(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        //BindGridView();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/ResearchTeachingPosts?" + strEndQueryString);
        }
        private bool LoadControlsForExtraDetails(CandidateDetailsModel objBo)
        {
            objBo.Id = Convert.ToInt32(strCandId);
            objBo.RoleInLastEmployment = null;
            objBo.CurrentSalaryPerMonth = 0;
            objBo.ExpectedSalary = 0;
            objBo.ExtraActivities = txtExtraActivities.Text.ToString();
            objBo.MemberShipOfAnySpecialBodies = txtMemberShip.Text.ToString();
            objBo.HaveChronicIllness = true;
            objBo.ChronicIllnessDetails = txtChronicillness.Text.Trim();
            objBo.IsOffenceRegistered = true;
            objBo.OffneceDetails = txtLawOffence.Text.ToString();
            objBo.DescribeYourself = txtselfDetails.Text.ToString();
            objBo.BiggestAchivement = txtAchievement.Text.ToString();
            objBo.YourBiggestFailure = txtFrustratingExperinece.Text.ToString();
            objBo.VisionInNextYears = txtFutreVision.Text.ToString();
            objBo.EmergencyContactNo = txtEmergencyContactNo.Text.ToString();
            objBo.EmergencyContactPersonName = txtEmergencyContact.Text.ToString();
            objBo.UNMICRCContact = txtPersoninUNMICR.Text.Trim();
            objBo.Remarks = null;
            objBo.AdvertisementAwarenessSource = "1";
            objBo.RegistrationId = strRegId;
            return true;
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfReferralId.Value = grdReferalDetails.DataKeys[rowindex]["id"].ToString();
            txtKnownPersonName.Text = grdReferalDetails.Rows[rowindex].Cells[1].Text.ToString();
            txtPosition.Text = grdReferalDetails.Rows[rowindex].Cells[2].Text.ToString();            
            txtTelePhone.Text = grdReferalDetails.Rows[rowindex].Cells[3].Text.ToString();
            txtRelatioship.Text = grdReferalDetails.Rows[rowindex].Cells[4].Text.ToString();
            txtAddress.Text = grdReferalDetails.Rows[rowindex].Cells[5].Text.ToString().Replace("&nbsp;", "");
            txtYearsKnown.Text = grdReferalDetails.Rows[rowindex].Cells[6].Text.ToString();
            btnKnowPersonDetails.Text = "Update Details";
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            //
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            long Recid = Convert.ToInt64(grdReferalDetails.DataKeys[rowindex]["id"].ToString());
            string errorMessage = "";
            using (ICandidateDetailsRepository objRecruitmentCastRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateProfessionalReferralDetailsModel objBO = new CandidateProfessionalReferralDetailsModel();

                if (!objRecruitmentCastRepository.RemoveTblCandidateProfessionalReferralDetails(Recid, out errorMessage))
                {
                    ClearControlValues();
                    BindProfessonalReferalGrid();
                    //Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
            }
        }

        protected void btnPrivious_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/ProfessionalExperience?" + strEndQueryString);
        }
    }
}