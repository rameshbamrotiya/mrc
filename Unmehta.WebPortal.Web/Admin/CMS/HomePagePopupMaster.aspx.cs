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
    public partial class HomePagePopupMaster : System.Web.UI.Page
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
        private void LoadControls(HomePagePopupMasterBO objBo)
        {
            objBo.Language_id = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.Description = HttpUtility.HtmlEncode(txtContactUsDescription.Text.ToString());
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                HomePagePopupMasterBO objBo = new HomePagePopupMasterBO();
                LoadControls(objBo);
                if (new HomePagePopupMasterBAL().InsertRecord(objBo))
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
            HomePagePopupMasterBAL objbal = new HomePagePopupMasterBAL();
            DataSet ds = objbal.SelectPopupDetails();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                btn_Add.Visible = false;
                dt.Rows[0]["Description"] = HttpUtility.HtmlDecode(dt.Rows[0]["Description"].ToString());
                dt.AcceptChanges();
                gView.DataSource = dt;
                gView.DataBind();
            }
            else
            {
                btn_Add.Visible = true;
                gView.DataSource = null;
                gView.DataBind();
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HomePagePopupMasterBO objBo = new HomePagePopupMasterBO();
                LoadControls(objBo);
                objBo.Popup_Id = Convert.ToInt16(hdPKId.Value.ToString());
                if (new HomePagePopupMasterBAL().UpdateRecord(objBo))
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
                HomePagePopupMasterBO objbo = new HomePagePopupMasterBO();
                HomePagePopupMasterBAL objBAL = new HomePagePopupMasterBAL();
                objbo.Popup_Id = PKId;
                objbo.Language_id = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectPopupDetailsByID(objbo);
                DataTable dtDetails = new DataTable();                
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    drpLanguage.SelectedValue = dr["Language_id"].ToString();
                    txtContactUsDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    ddlActiveInactive.SelectedValue = dr["Is_active"].ToString() == "False" ? "0" : "1";
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
                FillControls(Convert.ToInt16(gView.DataKeys[rowindex]["Id"].ToString()), Convert.ToInt16(gView.DataKeys[rowindex]["Language_id"].ToString()));
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
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["Popup_Id"].ToString());
                HomePagePopupMasterBO objBo = new HomePagePopupMasterBO();
                objBo.Popup_Id = rowId;
                new HomePagePopupMasterBAL().DeleteRecord(objBo);
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