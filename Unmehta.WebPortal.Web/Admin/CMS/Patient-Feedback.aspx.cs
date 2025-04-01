using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Data.Hospital;
using System.Collections.Generic;
using ClassLib.BO;
using ClassLib.BL;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class Patient_Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            if (!IsPostBack)
            {
                FillLanguage();
                FillControls(1);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PatientFeedbackContentDetailsBO objBo = new PatientFeedbackContentDetailsBO();
                LoadControls(objBo);
                if (new PatientFeedbackContentDetailsBAL().InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    //clearControls();
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private void clearControls()
        {
            drpLanguage.SelectedIndex = -1;
            txtMetaTitle.Text = "";
            txtMetaDesc.Text = "";
            ddlActiveInactive.SelectedIndex = -1;
            ckPatientFeedback.Text = "";
            ckPatientHospitalGuide.Text = "";
        }

        private void LoadControls(PatientFeedbackContentDetailsBO objBo)
        {
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;

            if (!string.IsNullOrEmpty(ckPatientFeedback.Text))
                objBo.PatientFeedback =  HttpUtility.HtmlEncode(ckPatientFeedback.Text.ToString());

            if (!string.IsNullOrEmpty(ckPatientHospitalGuide.Text))
                objBo.PatientHospitalGuide = HttpUtility.HtmlEncode(ckPatientHospitalGuide.Text.ToString());

            objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
            objBo.added_by = Convert.ToString(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }

        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", false);
        }

        private void FillControls(int languageid)
        {
            try
            {
                string ddlLan = Convert.ToString(drpLanguage.SelectedValue);
                clearControls();
                drpLanguage.SelectedValue = ddlLan;
                PatientFeedbackContentDetailsBO objbo = new PatientFeedbackContentDetailsBO();
                PatientFeedbackContentDetailsBAL objBAL = new PatientFeedbackContentDetailsBAL();
                objbo.LanguageId = languageid;
                DataTable dt = new DataTable();
                dt = objBAL.SelectRecord(objbo);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDesc.Text =  dr["MetaDescription"].ToString();
                    ckPatientFeedback.Text = HttpUtility.HtmlDecode(dr["PatientFeedback"].ToString());
                    ckPatientHospitalGuide.Text = HttpUtility.HtmlDecode(dr["PatientHospitalGuide"].ToString());
                    ddlActiveInactive.SelectedValue = dr["enabled"].ToString().ToLower() == "true" ? "1" : "0";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillControls(Convert.ToInt32(drpLanguage.SelectedValue));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}