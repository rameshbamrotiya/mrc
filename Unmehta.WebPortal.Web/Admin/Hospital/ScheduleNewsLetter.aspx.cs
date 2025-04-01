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
    public partial class ScheduleNewsLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    if (SessionWrapper.UserDetails.UserName == null)
                    {
                        Response.Redirect("~/LoginPortal",false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/LoginPortal", false);
                }

                ShowHideControl(VisibityType.GridView);

                BindGridView();
            }
        }
        
        protected void BindDropDown()
        {
            ddlDocument.Items.Clear();
            SendSubscribeNewsletterBAL objBALPOst = new SendSubscribeNewsletterBAL();
            DataSet ds1 = new DataSet();
            ds1 = objBALPOst.GetAllDocument("PROC_SendSubscribeNewsletterDOC_Dropdown");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlDocument.DataSource = ds1.Tables[0];
                    ddlDocument.DataTextField = "SSN_Name";
                    ddlDocument.DataValueField = "id";
                    ddlDocument.DataBind();
                    ddlDocument.Items.Insert(0, new ListItem("Select Document", ""));
                }
            }
            ddlDocument.SelectedIndex = 0;

            ddlTriggerDate.Items.Clear();
            ddlTriggerDate.Items.Insert(0, new ListItem("Select Trigger Date", ""));
            ddlTriggerDate.Items.Insert(1, new ListItem("1", "1"));
            ddlTriggerDate.Items.Insert(2, new ListItem("2", "2"));
            ddlTriggerDate.Items.Insert(3, new ListItem("3", "3"));
            ddlTriggerDate.Items.Insert(4, new ListItem("4", "4"));
            ddlTriggerDate.Items.Insert(5, new ListItem("5", "5"));
            ddlTriggerDate.Items.Insert(6, new ListItem("6", "6"));
            ddlTriggerDate.Items.Insert(7, new ListItem("7", "7"));
            ddlTriggerDate.Items.Insert(8, new ListItem("8", "8"));
            ddlTriggerDate.Items.Insert(9, new ListItem("9", "9"));
            ddlTriggerDate.Items.Insert(10, new ListItem("10", "10"));
            ddlTriggerDate.Items.Insert(11, new ListItem("11", "11"));
            ddlTriggerDate.Items.Insert(12, new ListItem("12", "12"));
            ddlTriggerDate.Items.Insert(13, new ListItem("13", "13"));
            ddlTriggerDate.Items.Insert(14, new ListItem("14", "14"));
            ddlTriggerDate.Items.Insert(15, new ListItem("15", "15"));
            ddlTriggerDate.Items.Insert(16, new ListItem("16", "16"));
            ddlTriggerDate.Items.Insert(17, new ListItem("17", "17"));
            ddlTriggerDate.Items.Insert(18, new ListItem("18", "18"));
            ddlTriggerDate.Items.Insert(19, new ListItem("19", "19"));
            ddlTriggerDate.Items.Insert(20, new ListItem("20", "20"));
            ddlTriggerDate.Items.Insert(21, new ListItem("21", "21"));
            ddlTriggerDate.Items.Insert(22, new ListItem("22", "22"));
            ddlTriggerDate.Items.Insert(23, new ListItem("23", "23"));
            ddlTriggerDate.Items.Insert(24, new ListItem("24", "24"));
            ddlTriggerDate.Items.Insert(25, new ListItem("25", "25"));
            ddlTriggerDate.Items.Insert(26, new ListItem("26", "26"));
            ddlTriggerDate.Items.Insert(27, new ListItem("27", "27"));
            ddlTriggerDate.Items.Insert(28, new ListItem("28", "28"));
                                         
            ddlTriggerDate.SelectedIndex = 0;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
            {
                GetAllScheduleNewsLetterMasterResult objBo = new GetAllScheduleNewsLetterMasterResult();
                if (LoadControlsAdd(objBo))
                {
                    if (!objBlogCategoryMasterRepository.InsertOrUpdateScheduleNewsLetterMaster(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        return;
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
        }

        private bool LoadControlsAdd(GetAllScheduleNewsLetterMasterResult objBo)
        {
            
            if (string.IsNullOrWhiteSpace(hfRowId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfRowId.Value);
            }

            if(string.IsNullOrWhiteSpace(txtTabName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Subject", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MailSubject = txtTabName.Text;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                Functions.MessagePopup(this, "Please Enter Mail Body Description", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MailDescription = HttpUtility.HtmlDecode(txtDescription.Text);
            }

            if(ddlTriggerDate.SelectedIndex>0)
            {
                objBo.StartDate = Convert.ToInt32(ddlTriggerDate.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Date", PopupMessageType.error);
                return false;
            }

            if (ddlDocument.SelectedIndex > 0)
            {
                objBo.DocId = Convert.ToInt32(ddlDocument.SelectedValue);
            }
            else
            {
                objBo.DocId = null;
            }

            objBo.IsActive = chkEnable.Checked;

            return true;
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
            {
                GetAllScheduleNewsLetterMasterResult objBo = new GetAllScheduleNewsLetterMasterResult();
                if (LoadControlsAdd(objBo))
                {
                    if (!objBlogCategoryMasterRepository.InsertOrUpdateScheduleNewsLetterMaster(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        return;
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
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
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            BindGridView();
            hfRowId.Value = "0";
            txtDescription.Text = "";
            txtTabName.Text = "";
            ddlDocument.SelectedIndex = 0;
            ddlTriggerDate.SelectedIndex =0;
            chkEnable.Checked = false;
        }

        private void BindGridView()
        {
            BindDropDown();

            grdUser.DataSourceID = "";
            using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
            {
                var Data = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMaster().ToList();
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    grdUser.DataSource = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMaster().Where(x => x.MailSubject.Contains(txtSearch.Text)).ToList();
                }
                else
                {
                    grdUser.DataSource = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMaster().ToList();
                }
                grdUser.DataBind();
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
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

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfRowId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
            {
                var dataModel = objBlogCategoryMasterRepository.GetScheduleNewsLetterMaster(Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString()));
                if (dataModel != null)
                {
                    txtTabName.Text = dataModel.MailSubject;
                    txtDescription.Text = HttpUtility.HtmlDecode(dataModel.MailDescription);
                    chkEnable.Checked = dataModel.IsActive.HasValue? ((bool) dataModel.IsActive):false;
                    ddlDocument.SelectedValue = dataModel.DocId.ToString();
                    ddlTriggerDate.SelectedValue = dataModel.StartDate.ToString();
                }
            }            
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
                {
                    objBlogCategoryMasterRepository.RemoveScheduleNewsLetterMaster(rowId, out errorMessage);
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
    }
    
}