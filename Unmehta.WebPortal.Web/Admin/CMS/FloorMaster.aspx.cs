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

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class FloorMaster : System.Web.UI.Page
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
        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnCatID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FloorMasterBO objBo = new FloorMasterBO();
                LoadControls(objBo);
                objBo.FloorId = Convert.ToInt16(hdnCatID.Value);
                if (new FloorMasterBAL().UpdateRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);
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
                ShowHideControl(VisibityType.GridView);
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
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new FloorMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Floor_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Floor_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void FillControls(int FloorId, int languageid)
        {
            try
            {
                FloorMasterBO objbo = new FloorMasterBO();
                FloorMasterBAL objBAL = new FloorMasterBAL();
                objbo.FloorId = FloorId;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(FloorId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectFloorDetailsById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtFloorName.Text = dr["FloorName"].ToString();
                    hdnRecid.Value = dr["Id"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    if (dr["Floor_level_id"] != DBNull.Value)
                        txtsequence.Text = dr["Floor_level_id"].ToString();
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = languageid.ToString();
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
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["FloorId"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageID"].ToString()));
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            FloorMasterBO objBo = new FloorMasterBO();
            objBo.FloorId = Convert.ToInt32(grdDetails.DataKeys[rowindex]["FloorId"].ToString());
            new FloorMasterBAL().DeleteRecord(objBo);
            BindGridView();
            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
            return;
        }
        private void BindGridView()
        {
            grdDetails.DataBind();
        }
        private void LoadControls(FloorMasterBO objBo)
        {

            objBo.Enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.FloorName = txtFloorName.Text.Trim();
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Floor_level_id = Convert.ToInt32(txtsequence.Text);

        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                FloorMasterBO objBo = new FloorMasterBO();
                LoadControls(objBo);
                if (new FloorMasterBAL().InsertRecord(objBo))
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

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new FloorMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDetails.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}