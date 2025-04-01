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

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class CandidateDetails : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //if (!Functions.RecrutmentPageAvailable())
                    //{
                    //    Response.Redirect("~/Careers");
                    //}
                    //if (!string.IsNullOrEmpty(SessionWrapper.PostName))
                    //{

                    //if (!Functions.RecrutmentPageAvailable())
                    //{

                    //}

                    //lblPostName.Text = "Applying Post : " + SessionWrapper.PostName.ToString();
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
                //    else
                //    {
                //        Response.Redirect("~/CurrentAdvertisementsMaster.aspx");
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void Bind_DocGrid()
        {
            CareerMasterBAL objBAL = new CareerMasterBAL();
            CareerMasterBO objBO = new CareerMasterBO();

            objBO.Id = Convert.ToInt32(strCandId);

            DataSet ds = new DataSet();
            ds = objBAL.Candidate_Select(objBO);
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
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrintApplication.aspx?CandidateId=" + Convert.ToInt32(strCandId).ToString());
        }

        protected void lnkBasicDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.BasicDetailsFlag = 1;
            Response.Redirect("~/Career/CandidateRegistration.aspx");
        }

        protected void lnkEducationDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.EducationDetailsFlag = 1;
            Response.Redirect("~/Career/CandidateEducationDetails.aspx");
        }

        protected void lnkCertificateDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.EducationDetailsFlag = 1;
            Response.Redirect("~/Career/CandidateEducationDetails.aspx");
        }

        protected void lnkProfessionalDetailsEdit_Click(object sender, EventArgs e)
        {
            SessionWrapper.ProfessionalDetailsFlag = 1;
            Response.Redirect("~/Career/CandidateEducationDetails.aspx");
        }

        protected void btnFinalSubmit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                CareerMasterBAL objBAL = new CareerMasterBAL();
                bool LanguageResult = objBAL.InsertFinalSubmit(Convert.ToInt32(strCandId), 1);
                //string strCandidateId = HttpUtility.UrlEncode(Functions.Encrypt(SessionWrapper.CandidateId.ToString()));
                //Response.Redirect("~/PrintApplication.aspx?CandidateId=" + strCandidateId);
                SessionWrapper.sendConfirmationMail = 0;
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
                Response.Redirect("~/Recruitment/PrintApplication?" + strEndQueryString);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void btnProfessionalDivPrivious_ServerClick(object sender, EventArgs e)
        {
            //SessionWrapper.ProfessionalDetailsFlag = 1;
            //Response.Redirect("~/CandidateEducationDetails.aspx");
            //SessionWrapper.sendConfirmationMail = 0;
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/ResearchTeachingPosts?" + strEndQueryString);
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SessionWrapper.sendConfirmationMailFlag = 1;
                string strCandidateId = HttpUtility.UrlEncode(Functions.Encrypt(Convert.ToInt32(strCandId).ToString()));
                Response.Redirect("~/PrintApplication.aspx?CandidateId=" + strCandidateId);
                //Response.Redirect("~/PrintApplication.aspx?CandidateId=" + SessionWrapper.CandidateId.ToString());
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                throw;
            }
        }
    }
}