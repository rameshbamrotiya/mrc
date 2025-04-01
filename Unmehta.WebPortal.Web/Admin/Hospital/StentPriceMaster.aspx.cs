using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class StentPriceMaster : System.Web.UI.Page
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
                ddlLanguage.Enabled = false;
                ClearControlValues();
                BindDataByLangId(1);
            }
        }

        private void BindDataByLangId(int LangId)
        {
            using (IStentPriceTypeRepository objBlogCategoryMasterRepository = new StentPriceTypeRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllStentPriceTypeMasterByLanguageId(LangId).FirstOrDefault();
                if (dataMain != null)
                {
                    hfID.Value = dataMain.Id.ToString();
                    txtAboutUsDescription.Text = HttpUtility.HtmlDecode(dataMain.StentPriceDesc);
                    chkIsActive.Checked = dataMain.IsActive.HasValue? dataMain.IsActive.Value : false;
                    chkIsVisableInQuickLink.Checked = dataMain.IsVisableInQuickLink.HasValue ? dataMain.IsVisableInQuickLink.Value : false;
                    ddlLanguage.Enabled = true;
                }
                else
                {
                    txtAboutUsDescription.Text = "";
                    chkIsActive.Checked = false;
                    chkIsVisableInQuickLink.Checked = false;
                }
            }
        }

        private void ClearControlValues()
        {

            txtAboutUsDescription.Text = "";
            chkIsActive.Checked = false;
            chkIsVisableInQuickLink.Checked = false;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

            try
            {
                string errorMessage = "";
                GetAllStentPriceTypeMasterByLanguageIdResult objBo = new GetAllStentPriceTypeMasterByLanguageIdResult();

                long langId = 0;
                if (ddlLanguage.SelectedIndex > 0)
                {
                    langId = Convert.ToInt64(ddlLanguage.SelectedValue);
                }
                else
                {
                    Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                    return;
                }

                if (LoadControlsAdd(objBo))
                {
                    using (IStentPriceTypeRepository objBlogCategoryMasterRepository = new StentPriceTypeRepository(Functions.strSqlConnectionString))
                    {
                        if (!objBlogCategoryMasterRepository.InsertOrUpdateSentPriceTypeMaster(objBo, langId, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                    }
                    ClearControlValues();
                    BindDataByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        private bool LoadControlsAdd(GetAllStentPriceTypeMasterByLanguageIdResult objBo)
        {
            if (!string.IsNullOrEmpty(txtAboutUsDescription.Text))
            {
                objBo.StentPriceDesc = HttpUtility.HtmlEncode(txtAboutUsDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Description", PopupMessageType.error);
                return false;
            }
            
            objBo.IsActive = chkIsActive.Checked;
            objBo.IsVisableInQuickLink = chkIsVisableInQuickLink.Checked;


            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
            return true;
        }
    }
}