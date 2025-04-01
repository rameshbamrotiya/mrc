using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class FooterQuickLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtPageName.Text))
                {
                    Functions.MessagePopup(this, "Please enter Page Name.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtPageLink.Text))
                {
                    Functions.MessagePopup(this, "Please enter Page Link.", PopupMessageType.warning);
                    return;
                }

                GetAllFooterQuickLinkMasterResult objBo = new GetAllFooterQuickLinkMasterResult();

                if (!Isvalidate(ref objBo))
                {
                    using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
                    {
                        string strMessage = "";
                        if (!objFooterQuickLinkRepository.InsertOrUpdateFooterQuickLinkMaster(objBo, out strMessage))
                        {
                            Functions.MessagePopup(this, strMessage, PopupMessageType.success);

                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                        else
                        {
                            Functions.MessagePopup(this, strMessage, PopupMessageType.error);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private bool Isvalidate(ref GetAllFooterQuickLinkMasterResult objBo)
        {

            if (string.IsNullOrEmpty(txtPageName.Text))
            {
                Functions.MessagePopup(this, "Please enter Title Page Name.", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.NameMenu = txtPageName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtPageLink.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Page Link.", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.InternalLink = txtPageLink.Text;
            }


            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.FooterId = 0;
            }
            else
            {
                objBo.FooterId = Convert.ToInt32(hfTemplateId.Value);
            }

            objBo.InternalOrExternal = ddlInternalOrExternal.SelectedValue;

            objBo.DisplaySection = ddlDisplaySection.SelectedValue;

            objBo.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);

            objBo.IsActive = (ddlActiveInactiveOS.SelectedValue) == "1" ? true : false;


            return false;
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtPageName.Text))
                {
                    Functions.MessagePopup(this, "Please enter Page Name.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtPageLink.Text))
                {
                    Functions.MessagePopup(this, "Please enter Page Link.", PopupMessageType.warning);
                    return;
                }

                GetAllFooterQuickLinkMasterResult objBo = new GetAllFooterQuickLinkMasterResult();

                if (!Isvalidate(ref objBo))
                {
                    using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
                    {
                        string strMessage = "";
                        if (!objFooterQuickLinkRepository.InsertOrUpdateFooterQuickLinkMaster(objBo, out strMessage))
                        {
                            Functions.MessagePopup(this, strMessage, PopupMessageType.success);

                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                        else
                        {
                            Functions.MessagePopup(this, strMessage, PopupMessageType.error);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues(pnlEntry);
            Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
            ShowHideControl(VisibityType.GridView);
        }


        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    long FooterId = Convert.ToInt32(gView.DataKeys[intIndex].Values["FooterId"]);
                    if (e.CommandName == "eDelete")
                    {
                        using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
                        {
                            string strMessage = "";
                            if (!objFooterQuickLinkRepository.RemoveFooterQuickLinkMaster(FooterId, out strMessage))
                            {
                                Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, strMessage, PopupMessageType.success);
                            }
                        }

                        BindGridView();
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);

                    long lgLangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                    if(lgLangId==0)
                    {
                        lgLangId = 1;
                    }

                    if (FillControls(bytID, lgLangId))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }

        }

        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            string strMesssage = "";
            using (IFooterQuickLinkRepository objOtherFacilitiesRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
            {
                if (objOtherFacilitiesRepository.FooterQuickLinkMasterSwap(cmd, Convert.ToDecimal(col_menu_level), Convert.ToInt32(col_parent_id), out strMesssage))
                {

                }
            }
        }

        private bool FillControls(long bytID, long LanguageId = 1)
        {
            bool IsError = false;
            using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
            {
                var data = objFooterQuickLinkRepository.GetFacilitesMasterById(bytID, LanguageId);
                if (data != null)
                {
                    hfTemplateId.Value = data.Id.ToString();
                    ddlLanguage.SelectedValue= LanguageId.ToString();
                    txtPageLink.Text = data.InternalLink;
                    txtPageName.Text = data.NameMenu;
                    ddlInternalOrExternal.SelectedValue = data.InternalOrExternal;
                    ddlActiveInactiveOS.SelectedValue = Convert.ToBoolean(data.IsActive) ? "1" : "0";
                    ddlDisplaySection.SelectedValue= data.DisplaySection;
                    IsError = true;
                }
                else
                {
                    ddlLanguage.SelectedValue = LanguageId.ToString();
                    txtPageLink.Text = "";
                    txtPageName.Text = "";
                    ddlInternalOrExternal.SelectedIndex = 0;
                    ddlActiveInactiveOS.SelectedIndex = 0;
                    IsError = true;
                }
            }
            return IsError;
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
                    hfTemplateId.Value = "0";
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

        private void BindGridView()
        {

            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
            ddlLanguage.SelectedIndex = 1;

            using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    gView.DataSource = objFooterQuickLinkRepository.GetAllFacilitesMaster(1);
                }
                else
                {
                    gView.DataSource = objFooterQuickLinkRepository.GetAllFacilitesMaster(1).Where(x => x.NameMenu.Contains(txtSearch.Text)).ToList();

                }
                gView.DataBind();
            }
        }

        public void ClearControlValues(Control container)
        {
            try
            {
                foreach (Control ctrl in container.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                        ((TextBox)ctrl).Text = "";
                    if (ctrl.GetType() == typeof(DropDownList))
                        ((DropDownList)ctrl).SelectedIndex = -1;
                    if (ctrl.GetType() == typeof(CheckBox))
                        ((CheckBox)ctrl).Checked = false;
                    if (ctrl.GetType() == typeof(System.Web.UI.WebControls.Image))
                        ((System.Web.UI.WebControls.Image)ctrl).ImageUrl = string.Empty;
                    if (ctrl.GetType() == typeof(GridView))
                        ((GridView)ctrl).DataSource = null;
                    if (ctrl.Controls.Count > 0)
                        ClearControlValues(ctrl);
                    if (ctrl is ListControl && ctrl.GetType().Name != "DropDownList")
                    {
                        ListControl listControl = ctrl as ListControl;
                        foreach (ListItem listItem in listControl.Items)
                            listItem.Selected = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lgMainId = 0;
            if (long.TryParse(hfTemplateId.Value, out lgMainId))
            {
                if (ddlLanguage.SelectedIndex > 0)
                {
                    FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
                }
            }
        }
    }
}