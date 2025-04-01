using System;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;
using System.Web.UI;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientCareCategory : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
                
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
                    hfID.Value = "0";
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
        private void FillControls(int CategoryID, int languageid)
        {
            try
            {
                PatientCareCategoryBO objbo = new PatientCareCategoryBO();
                PatientCareCategoryBAL objBAL = new PatientCareCategoryBAL();
                objbo.CategoryID = CategoryID;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(CategoryID).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.GetPatinetCareTypeDetails(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtCategory.Text = dr["CategoryName"].ToString();
                    txtSequenceNo.Text = dr["SequenceNo"].ToString();
                    hdnRecid.Value = dr["recid"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    ddlFormType.SelectedValue = dr["FormType"].ToString() == "" ? "0" : dr["FormType"].ToString();
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = languageid.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }


        }
        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                // hdnRecid.Value = GrdFutureVision.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["CategoryID"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageID"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
            DataSet ds = new PatientCareCategoryBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["SequenceNo"] != DBNull.Value)
                txtSequenceNo.Text = drs["SequenceNo"].ToString();
        }
        private void BindGridView()
        {
            grdDetails.DataBind();
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private void LoadControls(PatientCareCategoryBO objBo)
        {

            objBo.Enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; 
            objBo.ip_add = GetIPAddress;
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.CategoryName = txtCategory.Text.Trim();
            objBo.SequenceNo = Convert.ToInt16(txtSequenceNo.Text);
            objBo.FormType = ddlFormType.SelectedValue;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareCategoryBO objBo = new PatientCareCategoryBO();
                PatientCareCategoryBAL objBAL = new PatientCareCategoryBAL();
                LoadControls(objBo);
                if (objBAL.InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    ClearControlValues(pnlEntry);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnCatID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PatientCareCategoryBO objBo = new PatientCareCategoryBO();
                LoadControls(objBo);
                objBo.CategoryID = Convert.ToInt16(hdnCatID.Value);
                if (new PatientCareCategoryBAL().UpdateRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);
                    ClearControlValues(pnlEntry);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName.Equals("eDelete")))
            {
                int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                Int32 bytID;
                bytID = Convert.ToInt32(grdDetails.DataKeys[intIndex].Values["CategoryID"]);
                if (e.CommandName == "eDelete")
                {
                    PatientCareCategoryBO objBo = new PatientCareCategoryBO();
                    objBo.CategoryID = bytID;
                    new PatientCareCategoryBAL().DeleteRecord(objBo);
                    BindGridView();
                    Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                    return;
                }
            }
        }
    }
}