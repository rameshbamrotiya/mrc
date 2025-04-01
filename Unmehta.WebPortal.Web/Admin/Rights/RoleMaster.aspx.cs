using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Unmehta.WebPortal.Model.Model.Rights;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository.Rights;

namespace Unmehta.WebPortal.Web.Admin.Rights
{
    public partial class RoleMaster : System.Web.UI.Page
    {
        public static string conn;
        public UserRightsModel ObjUserRights = new UserRightsModel();
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

            if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                }
           
        }
        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private void LoadControlsAdd(RoleMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text))
                objBo.Rolename = txtUserName.Text;
            
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
            objBo.IsActive = (ddlActiveInactive.SelectedValue.ToString() == "1" ? true : false);
          

        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            txtUserName.Text = grdUser.Rows[rowindex].Cells[1].Text.Trim();
            ddlActiveInactive.SelectedValue = (grdUser.Rows[rowindex].Cells[2].Text.Trim() == "True" ? 1 : 0).ToString();


            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IRoleMasterRepositry objRoleMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
                {
                    objRoleMasterRepository.RemoveRoleMaster(rowId, "", out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
                using (IRoleMasterRepositry objRoleMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
                {
                    RoleMasterModel objBO = new RoleMasterModel();
                    LoadControlsAdd(objBO);
                    if (!objRoleMasterRepository.InsertOrUpdateRoleMaster(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
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
                    ClearControlValues();
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
                    ClearControlValues();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            txtSearch.Text = "";
            txtUserName.Text = "";
            //ddlActiveInactive.SelectedIndex = 0;
            //ddlDept.SelectedIndex = 0;
            BindGridView();
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                RoleMasterModel objBo = new RoleMasterModel();
                LoadControlsAdd(objBo);
                using (IRoleMasterRepositry objRoleMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateRoleMaster(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
                ClearControlValues();
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}