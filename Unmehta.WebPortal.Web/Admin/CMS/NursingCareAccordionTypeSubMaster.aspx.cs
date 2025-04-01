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
    public partial class NursingCareAccordionTypeSubMaster : System.Web.UI.Page
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
                    BindNursingAccrodianType();
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["AccordionSub_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        NursingCareAccordionTypeSubMasterBO objBo = new NursingCareAccordionTypeSubMasterBO();
                        objBo.AccordionSub_id = bytID;
                        new NursingCareAccordionTypeMasterBAL().DeleteAccrodianSubRecord(objBo);
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindNursingAccrodianType();
            NursingCareAccordionTypeSubMasterBO objBo = new NursingCareAccordionTypeSubMasterBO();
            objBo.AccordionSub_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new NursingCareAccordionTypeMasterBAL().SelectAccrodianSubRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtAccDesc.Text = "";
                txtAccTitle.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            ViewState["T017PDetails"] = ds.Tables[0];
            if (dr["AccordionSubTitle"] != DBNull.Value)
                txtAccTitle.Text = dr["AccordionSubTitle"].ToString();
            if (dr["AccordionSubDescription"] != DBNull.Value)
                txtAccDesc.Text = HttpUtility.HtmlDecode(dr["AccordionSubDescription"].ToString());
            if (dr["AccordionType_id"] != DBNull.Value)
                ddlAccordianType.SelectedValue = Convert.ToString(dr["AccordionType_id"]);
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            return true;
        }
        private void BindGridView()
        {
            BindNursingAccrodianType();
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(NursingCareAccordionTypeSubMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtAccTitle.Text))
                objBo.AccordionSubTitle = txtAccTitle.Text;
            if (!string.IsNullOrEmpty(txtAccDesc.Text))
                objBo.AccordionSubDescription = HttpUtility.HtmlEncode(txtAccDesc.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.AccordionType_id = Convert.ToInt32(ddlAccordianType.SelectedValue.ToString());
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
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

                NursingCareAccordionTypeSubMasterBO objBo = new NursingCareAccordionTypeSubMasterBO();
                LoadControls(objBo);
                if (new NursingCareAccordionTypeMasterBAL().InsertAccrodianSubRecord(objBo))
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

                NursingCareAccordionTypeSubMasterBO objBo = new NursingCareAccordionTypeSubMasterBO();
                LoadControls(objBo);
                objBo.AccordionSub_id = Convert.ToInt32(ViewState["PK"]);
                if (new NursingCareAccordionTypeMasterBAL().UpdateAccrodianSubRecord(objBo))
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
        }
        private void BindNursingAccrodianType()
        {
            ddlAccordianType.Items.Clear();
            NursingCareAccordionTypeSubMasterBO objBo = new NursingCareAccordionTypeSubMasterBO();
            int languageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.LanguageId = languageId;
            NursingCareAccordionTypeMasterBAL objBal = new NursingCareAccordionTypeMasterBAL();

            DataTable ds = objBal.SelectRecordAccrodianType(objBo).Tables[0];

            //DataRow[] rows= ds.Select("Is_active = true");

            //ds.Dispose();
            //if (rows.Length > 0)
            //{
            //    ds = rows.CopyToDataTable();
            //}
            ddlAccordianType.DataSource = ds ;
            ddlAccordianType.DataTextField = "Name";
            ddlAccordianType.DataValueField = "Id";
            ddlAccordianType.DataBind();
            ddlAccordianType.Items.Insert(0, new ListItem("Select", "-1"));
        }
    }
}