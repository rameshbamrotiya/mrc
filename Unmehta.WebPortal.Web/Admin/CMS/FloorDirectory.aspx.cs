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
using Unmehta.WebPortal.Common;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class FloorDirectory : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
                FillBlock();
                FillFloor();
            }
        }
        #endregion
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
                    ddlLanguage.SelectedValue = "1";
                    ddlLanguage.Enabled = false;
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
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);

        }
        private void FillBlock()
        {
            DataSet ds = new DataSet();
            BlockBAL objBAL = new BlockBAL();
            BlockBO objbo = new BlockBO();
            objbo.LanguageId = Convert.ToInt16(ddlLanguage.SelectedValue.ToString());
            ds = objBAL.SelectBlockByLanguage(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpblock, dt, "BlockName", "BlockID", true);

        }

        private void FillFloor()
        {
            DataSet ds = new DataSet();
            FloorMasterBAL objBAL = new FloorMasterBAL();
            FloorMasterBO objbo = new FloorMasterBO();
            objbo.LanguageId = Convert.ToInt16(ddlLanguage.SelectedValue.ToString());
            ds = objBAL.SelectFloorByLanguage(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlFloorName, dt, "FloorName", "FloorId", true);

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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
            FillBlock();
            FillFloor();
        }
        private void FillControls(int CategoryID, int languageid)
        {
            try
            {
                FloorDirectoryBO objbo = new FloorDirectoryBO();
                FloorDirectoryBAL objBAL = new FloorDirectoryBAL();
                objbo.FloorDirectoryId = CategoryID;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(CategoryID).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectFloorDetailsById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    ddlLanguage.SelectedValue = dr["Language_id"].ToString();
                    FillBlock();
                    drpblock.SelectedValue = dr["blockid"].ToString();
                    FillFloor();
                    ddlFloorName.SelectedValue = dr["FloorId"].ToString();
                    txtCellValue.Text= dr["CellValue"].ToString();
                    txtToolTip.Text = dr["ToolTip"].ToString();
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    ddlLanguage.SelectedValue = languageid.ToString();
                }
                

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }


        }
        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                // hdnRecid.Value = GrdFutureVision.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["FloorDirectoryId"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["Language_id"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {

        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBlock();
            FillFloor();
            FillControls(Convert.ToInt16(hdnCatID.Value), Convert.ToInt16(ddlLanguage.SelectedValue.ToString()));
        }
        private void LoadControls(FloorDirectoryBO objBo)
        {
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.BlockID =Convert.ToInt16( drpblock.SelectedValue.ToString());
            objBo.FloorId = Convert.ToInt32(ddlFloorName.SelectedValue.ToString());
            objBo.CellValue = txtCellValue.Text;
            objBo.ToolTip = txtToolTip.Text;
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            objBo.Enabled = true;
        }
        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            try
            {   
                FloorDirectoryBO objBo = new FloorDirectoryBO();
                LoadControls(objBo);
                if (new FloorDirectoryBAL().InsertRecord(objBo))
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

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            FloorDirectoryBO objBo = new FloorDirectoryBO();
            LoadControls(objBo);
            objBo.FloorDirectoryId = Convert.ToInt16(hdnCatID.Value);
            if (new FloorDirectoryBAL().UpdateRecord(objBo))
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

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(grdDetails.DataKeys[intIndex].Values["FloorID"]);
                    if (e.CommandName == "eDelete")
                    {
                        FloorDirectoryBO objBo = new FloorDirectoryBO();
                        objBo.FloorDirectoryId = bytID;
                        new FloorDirectoryBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
    }
}