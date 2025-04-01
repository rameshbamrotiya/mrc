using BAL;
using BO;
using ClassLib.BL;
using ClassLib.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class e_citizen : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strFeedback;
        public static string strPackagestab;
        public static string strPackagesModels;
        public static string strQuickLink;
        public static string strHospitalGuide;
        public static string strRTIDesc;
        public static string strcomitteeDesc;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                    lblError.Visible = false;
                BindPatienFeedbackContent();
                //strQuickLink = Functions.CreateQuickLink("HiddenPage", "E-Citizen");
            }
        }


        private void BindPatienFeedbackContent()
        {

            PatientFeedbackContentDetailsBO objBo = new PatientFeedbackContentDetailsBO();
            //int languageId = Functions.LanguageId;
            objBo.LanguageId = 1;
            DataTable dt = new PatientFeedbackContentDetailsBAL().selectFeedbackDetails(objBo);
            if (dt != null)
            {
                if (!dt.Rows.Count.Equals(0))
                {
                    strFeedback = HttpUtility.HtmlDecode(dt.Rows[0]["PatientFeedback"].ToString());
                    strHospitalGuide = HttpUtility.HtmlDecode(dt.Rows[0]["PatientHospitalGuide"].ToString());
                }
            }


            RighttoInformationMasterBO objRIMBo = new RighttoInformationMasterBO();
            objRIMBo.LanguageID = 1;
            objRIMBo.Type = "RTI";
            DataSet dsRightToInfo = new RighttoInformationMasterBAL().SelectContaint(objRIMBo);
            if (dsRightToInfo != null)
            {
                if (!dsRightToInfo.Tables[0].Rows.Count.Equals(0))
                {
                    strRTIDesc = HttpUtility.HtmlDecode(dsRightToInfo.Tables[0].Rows[0]["Description"].ToString());
                }
                else
                {
                    strRTIDesc = string.Empty;
                }
                if (!dsRightToInfo.Tables[1].Rows.Count.Equals(0))
                {
                    rptRTI.DataSource = dsRightToInfo.Tables[1];
                    rptRTI.DataBind();
                }
            }


            RighttoInformationMasterBO objdsCOMMITTEEBo = new RighttoInformationMasterBO();
            objdsCOMMITTEEBo.LanguageID = 1;
            objdsCOMMITTEEBo.Type = "COMMITTEE";
            DataSet dsCOMMITTEE = new RighttoInformationMasterBAL().SelectContaint(objdsCOMMITTEEBo);
            if (dsCOMMITTEE != null)
            {
                if (!dsCOMMITTEE.Tables[0].Rows.Count.Equals(0))
                {
                    strcomitteeDesc = HttpUtility.HtmlDecode(dsCOMMITTEE.Tables[0].Rows[0]["Description"].ToString());
                }
                else
                {
                    strcomitteeDesc = string.Empty;
                }
                if (!dsCOMMITTEE.Tables[1].Rows.Count.Equals(0))
                {
                    rptCommitte.DataSource = dsCOMMITTEE.Tables[1];
                    rptCommitte.DataBind();
                }
            }

        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "E-Citizen").FirstOrDefault();

                if (dataMain != null)
                {
                    LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "E-Citizen").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                FeedbackBO objBo = new FeedbackBO();
                LoadControls(objBo);
                if (new FeedbackBAL().InsertRecord(objBo))
                {

                    string pathToHTMLFile = Server.MapPath("~/html/Patient Feedback.html");
                    string htmlString = File.ReadAllText(pathToHTMLFile);

                    htmlString = htmlString.Replace("{{FullName}}", objBo.FullName);
                    htmlString = htmlString.Replace("{{EmailId}}", objBo.EmailId);
                    htmlString = htmlString.Replace("{{VisitType}}", objBo.VisitType);
                    htmlString = htmlString.Replace("{{VisitNumber}}", objBo.VisitNumber);
                    htmlString = htmlString.Replace("{{MobileNo}}", objBo.MobileNo);
                    htmlString = htmlString.Replace("{{Country}}", objBo.Country);
                    htmlString = htmlString.Replace("{{State}}", objBo.State);
                    htmlString = htmlString.Replace("{{City}}", objBo.City);
                    htmlString = htmlString.Replace("{{FeedbackDescription}}", objBo.FeedbackDescription);
                    string Message = "";

                    Functions.SendEmail(ConfigDetailsValue.ToMailGetFeedback, "Patient Feedback From "+ objBo.FullName, htmlString, out Message,true);

                    //lblError.ForeColor = System.Drawing.Color.Green;
                    //lblError.Text = "Feedback submitted successfully";
                    //lblError.Visible = true;
                    Response.Write("<script>alert('Thank You for Your Valuable Feedback.');</script>");
                    ClearChildControl();
                }
            }
            catch (Exception ex)
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = ex.ToString();

                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void ClearChildControl()
        {
            txtfullname.Text = "";
            txtEmail.Text = "";
            ddlVisitType.SelectedIndex = -1;
            txtVisitNumber.Text = "";
            txtMno.Text = "";
            txtCountry.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtMsg.Text = "";
        }

        private void LoadControls(FeedbackBO objBo)
        {

            if (!string.IsNullOrEmpty(txtfullname.Text))
                objBo.FullName = txtfullname.Text;

            if (!string.IsNullOrEmpty(txtEmail.Text))
                objBo.EmailId = txtEmail.Text;

            if (ddlVisitType.SelectedIndex > 0)
                objBo.VisitType = ddlVisitType.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(txtVisitNumber.Text))
                objBo.VisitNumber = txtVisitNumber.Text;

            if (!string.IsNullOrEmpty(txtMno.Text))
                objBo.MobileNo = txtMno.Text;

            if (!string.IsNullOrEmpty(txtCountry.Text))
                objBo.Country = txtCountry.Text;

            if (!string.IsNullOrEmpty(txtState.Text))
                objBo.State = txtState.Text;
            objBo.unmericfeedback = false;
            if (!string.IsNullOrEmpty(txtCity.Text))
                objBo.City = txtCity.Text;

            if (!string.IsNullOrEmpty(txtMsg.Text))
                objBo.FeedbackDescription = txtMsg.Text;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearChildControl();
        }
    }
}