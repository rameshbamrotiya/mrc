using System;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using BO;
using Unmehta.WebPortal.Web.Common;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class ClinicalMaterialMaster : System.Web.UI.Page
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
            }
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ddlLanguage.SelectedIndex = 1;
                FillControls(Convert.ToInt16(ddlLanguage.SelectedValue));
            }
        }
        private bool LoadControls(ClinicalMaterialMasterBO objBo)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LanguageId = Convert.ToInt16(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                objBo.description = HttpUtility.HtmlEncode(txtDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter  Description", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.metatitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.metadescription = txtMetaDescription.Text;

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.CMId = 0;
            }
            else
            {
                objBo.CMId = Convert.ToInt32(hfID.Value);
            }
            objBo.IsVisible = chkEnable.Checked;
            objBo.user_id = SessionWrapper.UserDetails.UserName;
            return true;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                ClinicalMaterialMasterBO objbo = new ClinicalMaterialMasterBO();
                LoadControls(objbo);
                if (new ClinicalMaterialMasterBAL().InsertRecord(objbo))
                {
                    FillControls(Convert.ToInt16(ddlLanguage.SelectedValue));
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void FillControls(int LanguageId)
        {
            try
            {
                ClinicalMaterialMasterBO objbo = new ClinicalMaterialMasterBO();
                ClinicalMaterialMasterBAL objBal = new ClinicalMaterialMasterBAL();
                objbo.LanguageId = LanguageId;
                DataSet ds = new DataSet();
                ds = objBal.SelectRecord(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    ddlLanguage.SelectedValue = dr["languageid"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDescription.Text = dr["MetaDescription"].ToString();
                    hfID.Value = dr["CMId"].ToString();
                    chkEnable.Checked = Convert.ToBoolean(dr["IsVisible"].ToString());
                }
                else
                {
                    txtDescription.Text = "";
                    txtMetaDescription.Text = "";
                    txtMetaTitle.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(ddlLanguage.SelectedValue));
        }
    }
}