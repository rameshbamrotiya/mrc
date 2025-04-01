using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class UnmicrCareerMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ddlLanguage.SelectedIndex = 1;

                FillForm(Convert.ToInt32(ddlLanguage.SelectedValue));
            }
        }

        private void FillForm(int langId)
        {
            using (IUnmicrCareerRepository objUnmicrCareerRepository = new UnmicrCareerRepository(Functions.strSqlConnectionString))
            {
                var data = objUnmicrCareerRepository.GetUnmicrCareerMasterByLanguageId(langId);
                if(data!=null)
                {
                    txtEmployeeCareTitle.Text = data.UnmicrcEmployeeCareTitle;
                    txtEmployeeCareDescription.Text = HttpUtility.HtmlDecode(data.UnmicrcEmployeeCareDescription);
                    txtGroveThatTitle.Text = data.UnmicrcGroveThatTitle;
                    txtGroveThatDescription.Text = HttpUtility.HtmlDecode(data.UnmicrcGroveThatDescription);
                    txtWhyJoinTitleName.Text = data.UnmicrcWhyJoinTitle;
                    txtWhyJoinDescription.Text = HttpUtility.HtmlDecode(data.UnmicrcWhyJoinDescription);
                }
                else
                {
                    txtEmployeeCareTitle.Text = "";
                    txtEmployeeCareDescription.Text = "";
                    txtGroveThatTitle.Text = "";
                    txtGroveThatDescription.Text = "";
                    txtWhyJoinTitleName.Text = "";
                    txtWhyJoinDescription.Text = "";
                }
                if(langId==1 && data == null)
                {
                    ddlLanguage.Enabled = false;
                }
                else
                {
                    ddlLanguage.Enabled = true;
                }
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlLanguage.SelectedIndex>0)
            {
                FillForm(Convert.ToInt32(ddlLanguage.SelectedValue));
            }
            else
            {
                ClearControlValues();
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
             string errorMessage = "";
            using (IUnmicrCareerRepository objUnmicrCareerRepository = new UnmicrCareerRepository(Functions.strSqlConnectionString))
            {
                GetUnmicrCareerMasterByLanguageIdResult objBO = new GetUnmicrCareerMasterByLanguageIdResult();
                if (LoadControlsAdd(objBO))
                {
                    if (!objUnmicrCareerRepository.InsertOrUpdateUnmicrCareerMaster(objBO, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        ClearControlValues();
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        return;
                    }
                }
            }
        }

        private bool LoadControlsAdd(GetUnmicrCareerMasterByLanguageIdResult objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            
            if (!string.IsNullOrEmpty(txtWhyJoinTitleName.Text))
                objBo.UnmicrcWhyJoinTitle = (txtWhyJoinTitleName.Text);

            if (!string.IsNullOrEmpty(txtWhyJoinDescription.Text))
                objBo.UnmicrcWhyJoinDescription = HttpUtility.HtmlEncode(txtWhyJoinDescription.Text);

            if (!string.IsNullOrEmpty(txtEmployeeCareTitle.Text))
                objBo.UnmicrcEmployeeCareTitle = (txtEmployeeCareTitle.Text);

            if (!string.IsNullOrEmpty(txtEmployeeCareDescription.Text))
                objBo.UnmicrcEmployeeCareDescription = HttpUtility.HtmlEncode(txtEmployeeCareDescription.Text);

            if (!string.IsNullOrEmpty(txtGroveThatTitle.Text))
                objBo.UnmicrcGroveThatTitle = (txtGroveThatTitle.Text);

            if (!string.IsNullOrEmpty(txtGroveThatDescription.Text))
                objBo.UnmicrcGroveThatDescription = HttpUtility.HtmlEncode(txtGroveThatDescription.Text);

            return true;
        }

        private void ClearControlValues()
        {
            ddlLanguage.SelectedIndex = 1;

            FillForm(Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}