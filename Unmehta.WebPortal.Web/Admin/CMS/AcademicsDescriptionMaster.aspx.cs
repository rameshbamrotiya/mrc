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

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class AcademicsDescriptionMaster : System.Web.UI.Page
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
                BindGridView();                
            }
        }
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void LoadControls(AcademicsDescriptionMasterBO objBo)
        {
            objBo.Language_id = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text;
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            objBo.ParamedicalMainDescription = HttpUtility.HtmlEncode(txtParamedicalMainDescription.Text.ToString());
            objBo.MedicalMainDescription = HttpUtility.HtmlEncode(txtMedicalMainDescription.Text.ToString());
            objBo.ParamedicalInnerDescription = HttpUtility.HtmlEncode(txtParamedicalInnerDescription.Text.ToString());
            objBo.MedicalInnerDescription = HttpUtility.HtmlEncode(txtMedicalInnerDescription.Text.ToString());
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.AlumniVisible = ddlAlumniVisible.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                AcademicsDescriptionMasterBO objBo = new AcademicsDescriptionMasterBO();
                LoadControls(objBo);
                if (new AcademicsDescriptionMasterBAL().InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void BindGridView()
        {
            //AcademicsDescriptionMasterBAL objbal = new AcademicsDescriptionMasterBAL();
            //DataSet ds = objbal.SelectAcademicsDescriptionDetails();
            //DataTable dt = ds.Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    dt.Rows[0]["ParamedicalMainDescription"] = HttpUtility.HtmlDecode(dt.Rows[0]["ParamedicalMainDescription"].ToString());
            //    dt.Rows[0]["MedicalMainDescription"] = HttpUtility.HtmlDecode(dt.Rows[0]["MedicalMainDescription"].ToString());
            //    dt.Rows[0]["ParamedicalInnerDescription"] = HttpUtility.HtmlDecode(dt.Rows[0]["ParamedicalInnerDescription"].ToString());
            //    dt.Rows[0]["MedicalInnerDescription"] = HttpUtility.HtmlDecode(dt.Rows[0]["MedicalInnerDescription"].ToString());
            //    dt.AcceptChanges();
            //    gView.DataSource = dt;
            //    gView.DataBind();
            //}
            //else
            //{
            //    gView.DataSource = null;
            //    gView.DataBind();
            //}
            gView.DataBind();
            if (gView.Rows.Count == 0)
            {
                ShowHideControl(VisibityType.Insert);
            }
            else
            {
                ShowHideControl(VisibityType.GridView);
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                AcademicsDescriptionMasterBO objBo = new AcademicsDescriptionMasterBO();
                LoadControls(objBo);
                objBo.AcademicsId = Convert.ToInt16(hdPKId.Value.ToString());
                if (new AcademicsDescriptionMasterBAL().UpdateRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdPKId.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void FillControls(int PKId, int Languageid)
        {
            try
            {
                AcademicsDescriptionMasterBO objbo = new AcademicsDescriptionMasterBO();
                AcademicsDescriptionMasterBAL objBAL = new AcademicsDescriptionMasterBAL();
                objbo.AcademicsId = PKId;
                objbo.Language_id = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectAcademicsDescriptionByID(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];                    
                    drpLanguage.SelectedValue = dr["LanguageId"].ToString();
                    txtTitle.Text = dr["Title"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();                    
                    txtParamedicalMainDescription.Text = HttpUtility.HtmlDecode(dr["ParamedicalMainDescription"].ToString());
                    txtMedicalMainDescription.Text = HttpUtility.HtmlDecode(dr["MedicalMainDescription"].ToString());
                    txtParamedicalInnerDescription.Text = HttpUtility.HtmlDecode(dr["ParamedicalInnerDescription"].ToString());
                    txtMedicalInnerDescription.Text = HttpUtility.HtmlDecode(dr["MedicalInnerDescription"].ToString());
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["enabled"].ToString()) ? "1" : "0";
                    ddlAlumniVisible.SelectedValue = Convert.ToBoolean(dr["AlumniVisible"]) ? "1" : "0";
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = Languageid.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                FillControls(Convert.ToInt16(gView.DataKeys[rowindex]["Id"].ToString()), Convert.ToInt16(gView.DataKeys[rowindex]["LanguageId"].ToString()));
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["AcademicsId"].ToString());
                AcademicsDescriptionMasterBO objBo = new AcademicsDescriptionMasterBO();
                objBo.AcademicsId = rowId;
                new AcademicsDescriptionMasterBAL().DeleteRecord(objBo);
                Functions.MessagePopup(this, "Record deleted successfully..", PopupMessageType.success);
                BindGridView();                
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}