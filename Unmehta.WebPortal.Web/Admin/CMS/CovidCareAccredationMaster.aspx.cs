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
    public partial class CovidCareAccredationMaster : System.Web.UI.Page
    {
        public static int Id, LanguageId;
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
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                string[] splitString = queryString.Split('|');
                Id = Convert.ToInt32(splitString[0].ToString().Replace("Id=", ""));
                LanguageId = Convert.ToInt32(splitString[1].ToString().Replace("LanguageId=", ""));
                BindGridView();
                ShowHideControl(VisibityType.GridView);
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
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
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
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void LoadControls(CovidCareAccredationDetailsBO objBo)
        {
            objBo.CovidCareDetails_Id = Id;
            objBo.Language_id = LanguageId;
            if (!string.IsNullOrEmpty(txtAccredationTitle.Text))
                objBo.AccredationSubTitle = txtAccredationTitle.Text;
            else
                objBo.AccredationSubTitle = "";
            objBo.AccredationDescription = HttpUtility.HtmlEncode(txtAccredationDescription.Text.ToString());
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Accredation_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                CovidCareAccredationDetailsBO objBo = new CovidCareAccredationDetailsBO();
                LoadControls(objBo);
                if (new CovidCareMasterBAL().InsertAccredationRecord(objBo))
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
            DataSet ds = new DataSet();
            CovidCareAccredationDetailsBO objBo = new CovidCareAccredationDetailsBO();
            objBo.CovidCareDetails_Id = Id;
            objBo.Language_id = LanguageId;
            CovidCareMasterBAL objBAL = new CovidCareMasterBAL();
            ds = objBAL.SelectAccredationRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gView.DataSource = ds;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                CovidCareAccredationDetailsBO objBo = new CovidCareAccredationDetailsBO();
                LoadControls(objBo);
                objBo.Accredation_Id = Convert.ToInt16(hdPKId.Value.ToString());
                if (new CovidCareMasterBAL().UpdateAccredationRecord(objBo))
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
            DataSet ds = new DataSet();
            CovidCareAccredationDetailsBO objBo = new CovidCareAccredationDetailsBO();
            objBo.CovidCareDetails_Id = Id;
            objBo.Language_id = LanguageId;
            CovidCareMasterBAL objBAL = new CovidCareMasterBAL();
            ds = objBAL.SelectAccredationRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "AccredationSubTitle LIKE '%' + '" + txtSearch.Text + "' + '%' ";
                gView.DataSource = dv;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
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
            DataSet ds = new CovidCareMasterBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["Accredation_level_id"] != DBNull.Value)
                txtsequence.Text = drs["Accredation_level_id"].ToString();
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hdPKId.Value), Convert.ToInt32(LanguageId));
        }
        private void FillControls(int PKId, int Languageid)
        {
            try
            {
                CovidCareAccredationDetailsBO objbo = new CovidCareAccredationDetailsBO();
                CovidCareMasterBAL objBAL = new CovidCareMasterBAL();
                objbo.Accredation_Id = PKId;
                objbo.Language_id = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.GetAccredationDetailsById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtAccredationTitle.Text = dr["AccredationSubTitle"].ToString();
                    txtAccredationDescription.Text = HttpUtility.HtmlDecode(dr["AccredationDescription"].ToString());
                    ddlActiveInactive.SelectedValue = dr["Is_active"].ToString() == "True" ? "1" : "0";
                    if (dr["Accredation_level_id"] != DBNull.Value)
                        txtsequence.Text = dr["Accredation_level_id"].ToString();
                }
                else
                {
                    ClearControlValues(pnlEntry);
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
                FillControls(Convert.ToInt16(gView.DataKeys[rowindex]["Accredation_Id"].ToString()), Convert.ToInt16(gView.DataKeys[rowindex]["Language_id"].ToString()));
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
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["Accredation_Id"].ToString());
                CovidCareAccredationDetailsBO objBo = new CovidCareAccredationDetailsBO();
                objBo.Accredation_Id = rowId;
                new CovidCareMasterBAL().DeleteAccredationRecord(objBo);
                Functions.MessagePopup(this, "Record deleted successfully..", PopupMessageType.success);
                BindGridView();
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
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
            if (new CovidCareMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}