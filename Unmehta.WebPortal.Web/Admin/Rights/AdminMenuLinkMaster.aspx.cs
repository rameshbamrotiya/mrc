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
    public partial class LinkMaster : System.Web.UI.Page
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
            if (!Page.IsPostBack)
                {
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
           

        }
        private void MenuListBind()
        {
            if (drpMenutype.SelectedValue == "0")
            {
                drpParentMenu.Enabled = false;
            }
            else
            {
                drpParentMenu.Enabled = true;
            }
            using (IAdminMenuLinkMasterRepository objAdminMenuLinkMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
            {
                drpParentMenu.DataSource = objAdminMenuLinkMasterRepository.GetAllAdminMenuMaster().Where(x => x.IsActive == true).ToList();
                drpParentMenu.DataTextField = "MenuName";
                drpParentMenu.DataValueField = "Id";
                drpParentMenu.DataBind();
            }
        }

        private void BindGridView()
        {
            MenuListBind();
            grdUser.DataBind();
        }

        private void LoadControlsAdd(AdminMenuLinkModel objBo)
        {
            if (!string.IsNullOrEmpty(txtMenuName.Text))
                objBo.MenuName = txtMenuName.Text;
            
            if (!string.IsNullOrEmpty(txtPageUrl.Text))
                objBo.MenuUrl = txtPageUrl.Text;

            if(drpMenutype.SelectedValue == "0")
            {
                objBo.ParentId = 0;
            }
            else
            {
                objBo.ParentId = Convert.ToInt32(drpParentMenu.SelectedValue);
            }

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
       

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IAdminMenuLinkMasterRepository objRoleMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
            {
                var dataInfo= objRoleMasterRepository.GetAllAdminMenuMaster().Where(x => x.Id == Convert.ToInt32(hfID.Value)).FirstOrDefault();
                if(dataInfo!=null)
                {

                    txtMenuName.Text = dataInfo.MenuName;
                    txtPageUrl.Text = dataInfo.MenuUrl;
                    drpMenutype.SelectedValue = (dataInfo.ParentId.ToString() == "0" ? "0" : "1");
                    if(dataInfo.ParentId.ToString() != "0")
                    {
                        drpParentMenu.SelectedValue = (dataInfo.ParentId.ToString() == "0" ? "0" : dataInfo.ParentId.ToString().Trim());
                    }
                    else
                    {
                        drpParentMenu.SelectedIndex = 0;
                    }
                    ddlActiveInactive.SelectedValue = (dataInfo.IsActive.ToString() == "True" ? 1 : 0).ToString();

                    MenuListBind();
                    ShowHideControl(VisibityType.Edit);
                }
            }

            }
            

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IAdminMenuLinkMasterRepository objRoleMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveAdminMenuLinkMaster(rowId, "", out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }
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
            using (IAdminMenuLinkMasterRepository objRoleMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
            {
                AdminMenuLinkModel objBO = new AdminMenuLinkModel();
                LoadControlsAdd(objBO);
                if (!objRoleMasterRepository.InsertOrUpdateAdminMenuLinkMaster(objBO, out errorMessage))
                {
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
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
            hfID.Value = "0";
            txtSearch.Text = "";
            txtMenuName.Text = "";
            txtPageUrl.Text = "";
            ddlActiveInactive.SelectedIndex = 0;
            //ddlActiveInactive.SelectedIndex = 0;
            drpMenutype.SelectedIndex = 0;
            drpParentMenu.SelectedIndex = 0;
            BindGridView();
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                AdminMenuLinkModel objBo = new AdminMenuLinkModel();
                LoadControlsAdd(objBo);
                using (IAdminMenuLinkMasterRepository objRoleMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateAdminMenuLinkMaster(objBo, out errorMessage))
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

        protected void drpMenutype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}