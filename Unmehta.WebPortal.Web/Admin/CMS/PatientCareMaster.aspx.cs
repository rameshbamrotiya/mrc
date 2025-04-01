using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientCareMaster : System.Web.UI.Page
    {
        #region Page Load
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
            try
            {
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    FillTabType(1);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
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
        #endregion

        #region GridView Operation
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["PatientCare_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        PatientCareMasterBO objBo = new PatientCareMasterBO();
                        objBo.PatientCare_id = bytID;
                        new PatientCareMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
                else
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                    if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                    {
                        string col_parent_id = commandArgs[0];
                        string col_menu_level = commandArgs[1];
                        string cmd = commandArgs[2];

                        switch (cmd)
                        {
                            case "up":
                                SetPageOrder(cmd, col_menu_level, col_parent_id);
                                break;
                            case "down":
                                SetPageOrder(cmd, col_menu_level, col_parent_id);
                                break;

                        }
                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new PatientCareMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            PatientCareMasterBO objBo = new PatientCareMasterBO();
            objBo.PatientCare_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new PatientCareMasterBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0))
            {
                txtTabName.Text = "";
                ddlTabType.SelectedIndex = 0;
                txtMetaTitle.Text = "";
                txtMetaDescription.Text = "";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr.HasErrors) return false;
                if (dr["TabName"] != DBNull.Value)
                {
                    rfvTabName.Enabled = true;
                    divTabName.Visible = true;
                    txtTabName.Text = Convert.ToString(dr["TabName"]);
                }   
                else
                {
                    rfvTabName.Enabled = false;
                    divTabName.Visible = false;
                    txtTabName.Text = "";
                }
                if (dr["TabType"] != DBNull.Value)
                    ddlTabType.SelectedValue = Convert.ToString(dr["TabType"]);
                if (dr["MetaTitle"] != DBNull.Value)
                    txtMetaTitle.Text = Convert.ToString(dr["MetaTitle"]);
                if (dr["MetaDescription"] != DBNull.Value)
                    txtMetaDescription.Text = Convert.ToString(dr["MetaDescription"]);
                if (dr["is_active"] != DBNull.Value)
                    ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
                if (dr["PatientCare_level_id"] != DBNull.Value)
                    txtsequence.Text = dr["PatientCare_level_id"].ToString();
            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        private void FillTabType(int languageid)
        {
            DataSet ds = new DataSet();
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            PatientCareMasterBO objbo = new PatientCareMasterBO();
            objbo.LanguageId = languageid;
            ds = objBAL.GetTabType(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlTabType, dt, "Categoryname", "CategoryID", true);

        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(PatientCareMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtTabName.Text))
                objBo.TabName = txtTabName.Text;
            else
                objBo.TabName = "";
            objBo.TabType = Convert.ToInt32(ddlTabType.SelectedValue);
            objBo.MetaTitle = txtMetaTitle.Text;
            objBo.MetaDescription = txtMetaDescription.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.PatientCare_level_id = Convert.ToInt32(txtsequence.Text);
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                rfvTabName.Enabled = true;
                divTabName.Visible = true;
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new PatientCareMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["PatientCare_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["PatientCare_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareMasterBO objBo = new PatientCareMasterBO();
                LoadControls(objBo);
                if (new PatientCareMasterBAL().InsertRecord(objBo))
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
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareMasterBO objBo = new PatientCareMasterBO();
                LoadControls(objBo);
                objBo.PatientCare_id = Convert.ToInt32(ViewState["PK"]);
                if (new PatientCareMasterBAL().UpdateRecord(objBo))
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
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region ShowHideControl || Notification
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
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
        #endregion
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
            FillTabType(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void lnkAddTabDetails_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int TabTypeId = Convert.ToInt32(gView.DataKeys[rowindex]["TabTypeId"].ToString());
            int SubTabId = Convert.ToInt32(gView.DataKeys[rowindex]["PatientCare_id"].ToString());
            int LanguageId = Convert.ToInt32(gView.DataKeys[rowindex]["Language_id"].ToString());
            string FormType = gView.DataKeys[rowindex]["FormType"].ToString();
            if(FormType == "General")
            {
                string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("TabTypeId=" + TabTypeId + "|SubTabId=" + SubTabId + "|LanguageId=" + LanguageId));
                Response.Redirect("~/Admin/CMS/PatientCareGeneralDetails.aspx?" + strdQuery, false);
            }
            else if (FormType == "Brochure")
            {
                string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("TabTypeId=" + TabTypeId + "|SubTabId=" + SubTabId + "|LanguageId=" + LanguageId));
                Response.Redirect("~/Admin/CMS/PatientCareBrochureDetails.aspx?" + strdQuery, false);
            }
            else if (FormType == "Left&RightContain")
            {
                string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("TabTypeId=" + TabTypeId + "|SubTabId=" + SubTabId + "|LanguageId=" + LanguageId));
                Response.Redirect("~/Admin/CMS/PatientCareLeftRightContainDetails.aspx?" + strdQuery, false);
            }
            else if (FormType == "FloorDirectory")
            {
                string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("TabTypeId=" + TabTypeId + "|SubTabId=" + SubTabId + "|LanguageId=" + LanguageId));
                Response.Redirect("~/Admin/CMS/FloorDirectory.aspx", false);
            }
        }

        protected void ddlTabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            int Id = Convert.ToInt32(ddlTabType.SelectedValue);
            int LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            ds = objBAL.GetFormType(Id, LanguageId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormType"].ToString() == "FloorDirectory")
                {
                    rfvTabName.Enabled = false;
                    divTabName.Visible = false;
                }
                else
                {   
                    rfvTabName.Enabled = true;
                    divTabName.Visible = true;
                }
            }
        }
    }
}