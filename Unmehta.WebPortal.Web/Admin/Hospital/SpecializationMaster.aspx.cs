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
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class SpecializationMaster : System.Web.UI.Page
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
            }
            //if (HttpContext.Current.Session["UserName"] != null)
            {
                if (!Page.IsPostBack)
                {
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private void LoadControlsAdd(SpecializationMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(txtSpecializationName.Text))
                objBo.DepartmentName = txtSpecializationName.Text;

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

        private void LoadControlsAdd(SpecializationScheduleModel objBo)
        {

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }

            objBo.IsSunday = chkIsSunday.Checked;
            objBo.Interval = Convert.ToInt32(ddlInterval.SelectedValue);
            objBo.SunStartTimeHour = string.IsNullOrWhiteSpace(txtSunStartTimeHour.Text)? "":(txtSunStartTimeHour.Text);
            objBo.SunStartTimeMin = string.IsNullOrWhiteSpace(txtSunStartTimeMin.Text) ? "":(txtSunStartTimeMin.Text);
            objBo.SunStartTimeTT = ddlSunStartTimeTT.SelectedValue;
            objBo.SunEndTimeHour = string.IsNullOrWhiteSpace(txtSunEndTimeHour.Text) ? "":(txtSunEndTimeHour.Text);
            objBo.SunEndTimeMin = string.IsNullOrWhiteSpace(txtSunEndTimeMin.Text) ? "":(txtSunEndTimeMin.Text);
            objBo.SunEndTimeTT = ddlSunEndTimeTT.SelectedValue;
            objBo.SunLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtSunLaunchStartTimeHour.Text) ? "":(txtSunLaunchStartTimeHour.Text);
            objBo.SunLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtSunLaunchStartTimeMin.Text) ? "":(txtSunLaunchStartTimeMin.Text);
            objBo.SunLaunchStartTimeTT = ddlSunLaunchStartTimeTT.SelectedValue;
            objBo.SunLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtSunLaunchEndTimeHour.Text) ? "":(txtSunLaunchEndTimeHour.Text);
            objBo.SunLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtSunLaunchEndTimeMin.Text) ? "":(txtSunLaunchEndTimeMin.Text);
            objBo.SunLaunchEndTimeTT = ddlSunLaunchEndTimeTT.SelectedValue;
            
            objBo.IsMonday = chkIsMonday.Checked;
            objBo.MonStartTimeHour = string.IsNullOrWhiteSpace(txtMondayStartTimeHour.Text) ? "":(txtMondayStartTimeHour.Text);
            objBo.MonStartTimeMin = string.IsNullOrWhiteSpace(txtMondayStartTimeMin.Text) ? "":(txtMondayStartTimeMin.Text);
            objBo.MonStartTimeTT = ddlMondayStartTimeTT.SelectedValue;
            objBo.MonEndTimeHour = string.IsNullOrWhiteSpace(txtMondayEndTimeHour.Text) ? "":(txtMondayEndTimeHour.Text);
            objBo.MonEndTimeMin = string.IsNullOrWhiteSpace(txtMondayEndTimeMin.Text) ? "":(txtMondayEndTimeMin.Text);
            objBo.MonEndTimeTT = ddlMondayEndTimeTT.SelectedValue;
            objBo.MonLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtMondayLaunchStartTimeHour.Text) ? "":(txtMondayLaunchStartTimeHour.Text);
            objBo.MonLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtMondayLaunchStartTimeMin.Text) ? "":(txtMondayLaunchStartTimeMin.Text);
            objBo.MonLaunchStartTimeTT = ddlMondayLaunchStartTimeTT.SelectedValue;
            objBo.MonLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtMondayLaunchEndTimeHour.Text) ? "":(txtMondayLaunchEndTimeHour.Text);
            objBo.MonLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtMondayLaunchEndTimeMin.Text) ? "":(txtMondayLaunchEndTimeMin.Text);
            objBo.MonLaunchEndTimeTT = ddlMondayLaunchEndTimeTT.SelectedValue;
            
            objBo.IsTuesday = chkIsTuesday.Checked;
            objBo.TueStartTimeHour = string.IsNullOrWhiteSpace(txtTuesdayStartTimeHour.Text) ? "":(txtTuesdayStartTimeHour.Text);
            objBo.TueStartTimeMin = string.IsNullOrWhiteSpace(txtTuesdayStartTimeMin.Text) ? "":(txtTuesdayStartTimeMin.Text);
            objBo.TueStartTimeTT = ddlTuesdayStartTimeTT.SelectedValue;
            objBo.TueEndTimeHour = string.IsNullOrWhiteSpace(txtTuesdayEndTimeHour.Text) ? "":(txtTuesdayEndTimeHour.Text);
            objBo.TueEndTimeMin = string.IsNullOrWhiteSpace(txtTuesdayEndTimeMin.Text) ? "":(txtTuesdayEndTimeMin.Text);
            objBo.TueEndTimeTT = ddlTuesdayEndTimeTT.SelectedValue;
            objBo.TueLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtTuesdayLaunchStartTimeHour.Text) ? "":(txtTuesdayLaunchStartTimeHour.Text);
            objBo.TueLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtTuesdayLaunchStartTimeMin.Text) ? "":(txtTuesdayLaunchStartTimeMin.Text);
            objBo.TueLaunchStartTimeTT = ddlTuesdayLaunchStartTimeTT.SelectedValue;
            objBo.TueLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtTuesdayLaunchEndTimeHour.Text) ? "":(txtTuesdayLaunchEndTimeHour.Text);
            objBo.TueLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtTuesdayLaunchEndTimeMin.Text) ? "":(txtTuesdayLaunchEndTimeMin.Text);
            objBo.TueLaunchEndTimeTT = ddlTuesdayLaunchEndTimeTT.SelectedValue;

            objBo.IsWednesday = chkIsWednesday.Checked;
            objBo.WenStartTimeHour = string.IsNullOrWhiteSpace(txtWednesdayStartTimeHour.Text) ? "":(txtWednesdayStartTimeHour.Text);
            objBo.WenStartTimeMin = string.IsNullOrWhiteSpace(txtWednesdayStartTimeMin.Text) ? "":(txtWednesdayStartTimeMin.Text);
            objBo.WenStartTimeTT = ddlWednesdayStartTimeTT.SelectedValue;
            objBo.WenEndTimeHour = string.IsNullOrWhiteSpace(txtWednesdayEndTimeHour.Text) ? "":(txtWednesdayEndTimeHour.Text);
            objBo.WenEndTimeMin = string.IsNullOrWhiteSpace(txtWednesdayEndTimeMin.Text) ? "":(txtWednesdayEndTimeMin.Text);
            objBo.WenEndTimeTT = ddlWednesdayEndTimeTT.SelectedValue;
            objBo.WenLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtWednesdayLaunchStartTimeHour.Text) ? "":(txtWednesdayLaunchStartTimeHour.Text);
            objBo.WenLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtWednesdayLaunchStartTimeMin.Text) ? "":(txtWednesdayLaunchStartTimeMin.Text);
            objBo.WenLaunchStartTimeTT = ddlWednesdayLaunchStartTimeTT.SelectedValue;
            objBo.WenLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtWednesdayLaunchEndTimeHour.Text) ? "":(txtWednesdayLaunchEndTimeHour.Text);
            objBo.WenLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtWednesdayLaunchEndTimeMin.Text) ? "":(txtWednesdayLaunchEndTimeMin.Text);
            objBo.WenLaunchEndTimeTT = ddlWednesdayLaunchEndTimeTT.SelectedValue;

            objBo.IsThursday = chkIsThursday.Checked;
            objBo.ThuStartTimeHour = string.IsNullOrWhiteSpace(txtThursdayStartTimeHour.Text) ? "":(txtThursdayStartTimeHour.Text);
            objBo.ThuStartTimeMin = string.IsNullOrWhiteSpace(txtThursdayStartTimeMin.Text) ? "":(txtThursdayStartTimeMin.Text);
            objBo.ThuStartTimeTT = ddlThursdayStartTimeTT.SelectedValue;
            objBo.ThuEndTimeHour = string.IsNullOrWhiteSpace(txtThursdayEndTimeHour.Text) ? "":(txtThursdayEndTimeHour.Text);
            objBo.ThuEndTimeMin = string.IsNullOrWhiteSpace(txtThursdayEndTimeMin.Text) ? "":(txtThursdayEndTimeMin.Text);
            objBo.ThuEndTimeTT = ddlThursdayEndTimeTT.SelectedValue;
            objBo.ThuLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtThursdayLaunchStartTimeHour.Text) ? "":(txtThursdayLaunchStartTimeHour.Text);
            objBo.ThuLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtThursdayLaunchStartTimeMin.Text) ? "":(txtThursdayLaunchStartTimeMin.Text);
            objBo.ThuLaunchStartTimeTT = ddlThursdayLaunchStartTimeTT.SelectedValue;
            objBo.ThuLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtThursdayLaunchEndTimeHour.Text) ? "":(txtThursdayLaunchEndTimeHour.Text);
            objBo.ThuLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtThursdayLaunchEndTimeMin.Text) ? "":(txtThursdayLaunchEndTimeMin.Text);
            objBo.ThuLaunchEndTimeTT = ddlThursdayLaunchEndTimeTT.SelectedValue;
            
            objBo.IsFriday = chkIsFriday.Checked;
            objBo.FriStartTimeHour = string.IsNullOrWhiteSpace(txtFridayStartTimeHour.Text) ? "":(txtFridayStartTimeHour.Text);
            objBo.FriStartTimeMin = string.IsNullOrWhiteSpace(txtFridayStartTimeMin.Text) ? "":(txtFridayStartTimeMin.Text);
            objBo.FriStartTimeTT = ddlFridayStartTimeTT.SelectedValue;
            objBo.FriEndTimeHour = string.IsNullOrWhiteSpace(txtFridayEndTimeHour.Text) ? "":(txtFridayEndTimeHour.Text);
            objBo.FriEndTimeMin = string.IsNullOrWhiteSpace(txtFridayEndTimeMin.Text) ? "":(txtFridayEndTimeMin.Text);
            objBo.FriEndTimeTT = ddlFridayEndTimeTT.SelectedValue;
            objBo.FriLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtFridayLaunchStartTimeHour.Text) ? "":(txtFridayLaunchStartTimeHour.Text);
            objBo.FriLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtFridayLaunchStartTimeMin.Text) ? "":(txtFridayLaunchStartTimeMin.Text);
            objBo.FriLaunchStartTimeTT = ddlFridayLaunchStartTimeTT.SelectedValue;
            objBo.FriLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtFridayLaunchEndTimeHour.Text) ? "":(txtFridayLaunchEndTimeHour.Text);
            objBo.FriLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtFridayLaunchEndTimeMin.Text) ? "":(txtFridayLaunchEndTimeMin.Text);
            objBo.FriLaunchEndTimeTT = ddlFridayLaunchEndTimeTT.SelectedValue;
            
            objBo.IsSaturday = chkIsSaturday.Checked;
            objBo.SatStartTimeHour = string.IsNullOrWhiteSpace(txtSaturdayStartTimeHour.Text) ? "":(txtSaturdayStartTimeHour.Text);
            objBo.SatStartTimeMin = string.IsNullOrWhiteSpace(txtSaturdayStartTimeMin.Text) ? "":(txtSaturdayStartTimeMin.Text);
            objBo.SatStartTimeTT = ddlSaturdayStartTimeTT.SelectedValue;
            objBo.SatEndTimeHour = string.IsNullOrWhiteSpace(txtSaturdayEndTimeHour.Text) ? "":(txtSaturdayEndTimeHour.Text);
            objBo.SatEndTimeMin = string.IsNullOrWhiteSpace(txtSaturdayEndTimeMin.Text) ? "":(txtSaturdayEndTimeMin.Text);
            objBo.SatEndTimeTT = ddlSaturdayEndTimeTT.SelectedValue;
            objBo.SatLaunchStartTimeHour = string.IsNullOrWhiteSpace(txtSaturdayLaunchStartTimeHour.Text) ? "":(txtSaturdayLaunchStartTimeHour.Text);
            objBo.SatLaunchStartTimeMin = string.IsNullOrWhiteSpace(txtSaturdayLaunchStartTimeMin.Text) ? "":(txtSaturdayLaunchStartTimeMin.Text);
            objBo.SatLaunchStartTimeTT = ddlSaturdayLaunchStartTimeTT.SelectedValue;
            objBo.SatLaunchEndTimeHour = string.IsNullOrWhiteSpace(txtSaturdayLaunchEndTimeHour.Text) ? "":(txtSaturdayLaunchEndTimeHour.Text);
            objBo.SatLaunchEndTimeMin = string.IsNullOrWhiteSpace(txtSaturdayLaunchEndTimeMin.Text) ? "":(txtSaturdayLaunchEndTimeMin.Text);
            objBo.SatLaunchEndTimeTT = ddlSaturdayLaunchEndTimeTT.SelectedValue;
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objRoleMasterRepository.GetSpecializationById(Convert.ToInt32(hfID.Value));
                if (dataInfo != null)
                {
                    txtSpecializationName.Text = dataInfo.DepartmentName;
                    ddlActiveInactive.SelectedValue = (dataInfo.IsActive.ToString().Trim() == "True" ? 1 : 0).ToString();
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
                using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveSpecialization(rowId, out errorMessage))
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
            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {
                SpecializationMasterModel objBO = new SpecializationMasterModel();
                LoadControlsAdd(objBO);
                if (!objRoleMasterRepository.InsertOrUpdateSpecialization(objBO, out errorMessage))
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
                    pnlScheduling.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    pnlScheduling.Visible = false;

                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    pnlScheduling.Visible = false;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    pnlScheduling.Visible = false;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    pnlScheduling.Visible = false;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    break;
                case VisibityType.Schedule:
                    pnlView.Visible = false;
                    pnlEntry.Visible = false;
                    pnlScheduling.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    break;
                default:
                    pnlView.Visible = true;
                    pnlScheduling.Visible = false;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtSearch.Text = "";
            txtSpecializationName.Text = "";
            BindGridView();
        }

        private void ClearScheduleControlValues()
        {
            txtCommanStartTimeHour.Text="";
            txtCommanStartTimeMin.Text = "";
            ddlCommanStartTT.SelectedIndex=0;
            txtCommanEndTimeHour.Text = "";
            txtCommanEndTimeMin.Text = "";
            ddlCommanEndTT.SelectedIndex = 0;
            txtCommonLaunchStartTimeHour.Text = "";
            txtCommonLaunchStartTimeMin.Text = "";
            ddlCommonLaunchStartTimeTT.SelectedIndex = 0;
            txtCommonLaunchEndTimeHour.Text = "";
            txtCommonLaunchEndTimeMin.Text = "";
            ddlCommonLaunchEndTimeTT.SelectedIndex = 0;


            chkIsSunday.Checked = false;
            txtSunStartTimeHour.Text = "";
            txtSunStartTimeMin.Text = "";
            ddlSunStartTimeTT.SelectedIndex = 0;
            txtSunEndTimeHour.Text = "";
            txtSunEndTimeMin.Text = "";
            ddlSunEndTimeTT.SelectedIndex = 0;
            txtSunLaunchStartTimeHour.Text = "";
            txtSunLaunchStartTimeMin.Text = "";
            ddlSunLaunchStartTimeTT.SelectedIndex = 0;
            txtSunLaunchEndTimeHour.Text = "";
            txtSunLaunchEndTimeMin.Text = "";
            ddlSunLaunchEndTimeTT.SelectedIndex = 0;

            chkIsMonday.Checked = false;
            txtMondayStartTimeHour.Text = "";
            txtMondayStartTimeMin.Text = "";
            ddlMondayStartTimeTT.SelectedIndex = 0;
            txtMondayEndTimeHour.Text = "";
            txtMondayEndTimeMin.Text = "";
            ddlMondayEndTimeTT.SelectedIndex = 0;
            txtMondayLaunchStartTimeHour.Text = "";
            txtMondayLaunchStartTimeMin.Text = "";
            ddlMondayLaunchStartTimeTT.SelectedIndex = 0;
            txtMondayLaunchEndTimeHour.Text = "";
            txtMondayLaunchEndTimeMin.Text = "";
            ddlMondayLaunchEndTimeTT.SelectedIndex = 0;

            chkIsTuesday.Checked = false;
            txtTuesdayStartTimeHour.Text = "";
            txtTuesdayStartTimeMin.Text = "";
            ddlTuesdayStartTimeTT.SelectedIndex = 0;
            txtTuesdayEndTimeHour.Text = "";
            txtTuesdayEndTimeMin.Text = "";
            ddlTuesdayEndTimeTT.SelectedIndex = 0;
            txtTuesdayLaunchStartTimeHour.Text = "";
            txtTuesdayLaunchStartTimeMin.Text = "";
            ddlTuesdayLaunchStartTimeTT.SelectedIndex = 0;
            txtTuesdayLaunchEndTimeHour.Text = "";
            txtTuesdayLaunchEndTimeMin.Text = "";
            ddlTuesdayLaunchEndTimeTT.SelectedIndex = 0;


            chkIsWednesday.Checked = false;
            txtWednesdayStartTimeHour.Text = "";
            txtWednesdayStartTimeMin.Text = "";
            ddlWednesdayStartTimeTT.SelectedIndex = 0;
            txtWednesdayEndTimeHour.Text = "";
            txtWednesdayEndTimeMin.Text = "";
            ddlWednesdayEndTimeTT.SelectedIndex = 0;
            txtWednesdayLaunchStartTimeHour.Text = "";
            txtWednesdayLaunchStartTimeMin.Text = "";
            ddlWednesdayLaunchStartTimeTT.SelectedIndex = 0;
            txtWednesdayLaunchEndTimeHour.Text = "";
            txtWednesdayLaunchEndTimeMin.Text = "";
            ddlWednesdayLaunchEndTimeTT.SelectedIndex = 0;

            chkIsThursday.Checked = false;
            txtThursdayStartTimeHour.Text = "";
            txtThursdayStartTimeMin.Text = "";
            ddlThursdayStartTimeTT.SelectedIndex = 0;
            txtThursdayEndTimeHour.Text = "";
            txtThursdayEndTimeMin.Text = "";
            ddlThursdayEndTimeTT.SelectedIndex = 0;
            txtThursdayLaunchStartTimeHour.Text = "";
            txtThursdayLaunchStartTimeMin.Text = "";
            ddlThursdayLaunchStartTimeTT.SelectedIndex = 0;
            txtThursdayLaunchEndTimeHour.Text = "";
            txtThursdayLaunchEndTimeMin.Text = "";
            ddlThursdayLaunchEndTimeTT.SelectedIndex = 0;

            chkIsFriday.Checked = false;
            txtFridayStartTimeHour.Text = "";
            txtFridayStartTimeMin.Text = "";
            ddlFridayStartTimeTT.SelectedIndex = 0;
            txtFridayEndTimeHour.Text = "";
            txtFridayEndTimeMin.Text = "";
            ddlFridayEndTimeTT.SelectedIndex = 0;
            txtFridayLaunchStartTimeHour.Text = "";
            txtFridayLaunchStartTimeMin.Text = "";
            ddlFridayLaunchStartTimeTT.SelectedIndex = 0;
            txtFridayLaunchEndTimeHour.Text = "";
            txtFridayLaunchEndTimeMin.Text = "";
            ddlFridayLaunchEndTimeTT.SelectedIndex = 0;

            chkIsSaturday.Checked = false;
            txtSaturdayStartTimeHour.Text = "";
            txtSaturdayStartTimeMin.Text = "";
            ddlSaturdayStartTimeTT.SelectedIndex = 0;
            txtSaturdayEndTimeHour.Text = "";
            txtSaturdayEndTimeMin.Text = "";
            ddlSaturdayEndTimeTT.SelectedIndex = 0;
            txtSaturdayLaunchStartTimeHour.Text = "";
            txtSaturdayLaunchStartTimeMin.Text = "";
            ddlSaturdayLaunchStartTimeTT.SelectedIndex = 0;
            txtSaturdayLaunchEndTimeHour.Text = "";
            txtSaturdayLaunchEndTimeMin.Text = "";
            ddlSaturdayLaunchEndTimeTT.SelectedIndex = 0;

            EnableDisableRequiredValidation();
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                SpecializationMasterModel objBo = new SpecializationMasterModel();
                LoadControlsAdd(objBo);
                using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateSpecialization(objBo, out errorMessage))
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

        protected void lnkSchedule_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {
                var dataInfo=objRoleMasterRepository.GetSpecializationScheduleById(Convert.ToInt32(hfID.Value));
                if(dataInfo!=null)
                {
                    ddlInterval.SelectedValue = (dataInfo.Interval==null? 15 : ((int)dataInfo.Interval==0 ? 15 : (int)dataInfo.Interval)).ToString();

                    chkIsSunday.Checked = dataInfo.IsSunday==null? false : (bool)dataInfo.IsSunday;
                    txtSunStartTimeHour.Text = dataInfo.SunStartTimeHour;
                    txtSunStartTimeMin.Text = dataInfo.SunStartTimeMin;
                    ddlSunStartTimeTT.SelectedValue= string.IsNullOrWhiteSpace(dataInfo.SunStartTimeTT)?"AM": dataInfo.SunStartTimeTT;
                    txtSunEndTimeHour.Text = dataInfo.SunEndTimeHour;
                    txtSunEndTimeMin.Text = dataInfo.SunEndTimeMin;
                    ddlSunEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SunEndTimeTT)?"PM": dataInfo.SunEndTimeTT;
                    txtSunLaunchStartTimeHour.Text = dataInfo.SunLaunchStartTimeHour;
                    txtSunLaunchStartTimeMin.Text = dataInfo.SunLaunchStartTimeMin;
                    ddlSunLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SunLaunchStartTimeTT)?"AM": dataInfo.SunLaunchStartTimeTT;
                    txtSunLaunchEndTimeHour.Text = dataInfo.SunLaunchEndTimeHour;
                    txtSunLaunchEndTimeMin.Text = dataInfo.SunLaunchEndTimeMin;
                    ddlSunLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SunLaunchEndTimeTT)?"PM": dataInfo.SunLaunchEndTimeTT;

                    chkIsMonday.Checked = dataInfo.IsMonday == null ? false : (bool)dataInfo.IsMonday;
                    txtMondayStartTimeHour.Text = dataInfo.MonStartTimeHour;
                    txtMondayStartTimeMin.Text = dataInfo.MonStartTimeMin;
                    ddlMondayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.MonStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtMondayEndTimeHour.Text = dataInfo.MonEndTimeHour;
                    txtMondayEndTimeMin.Text = dataInfo.MonEndTimeMin;
                    ddlMondayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.MonEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtMondayLaunchStartTimeHour.Text = dataInfo.MonLaunchStartTimeHour;
                    txtMondayLaunchStartTimeMin.Text = dataInfo.MonLaunchStartTimeMin;
                    ddlMondayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.MonLaunchStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtMondayLaunchEndTimeHour.Text = dataInfo.MonLaunchEndTimeHour;
                    txtMondayLaunchEndTimeMin.Text = dataInfo.MonLaunchEndTimeMin;
                    ddlMondayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.MonLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;

                    chkIsTuesday.Checked = dataInfo.IsTuesday == null ? false : (bool)dataInfo.IsTuesday;
                    txtTuesdayStartTimeHour.Text = dataInfo.TueStartTimeHour;
                    txtTuesdayStartTimeMin.Text = dataInfo.TueStartTimeMin;
                    ddlTuesdayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.TueStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtTuesdayEndTimeHour.Text = dataInfo.TueEndTimeHour;
                    txtTuesdayEndTimeMin.Text = dataInfo.TueEndTimeMin;
                    ddlTuesdayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.TueEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtTuesdayLaunchStartTimeHour.Text = dataInfo.TueLaunchStartTimeHour;
                    txtTuesdayLaunchStartTimeMin.Text = dataInfo.TueLaunchStartTimeMin;
                    ddlTuesdayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.TueLaunchStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtTuesdayLaunchEndTimeHour.Text = dataInfo.TueLaunchEndTimeHour;
                    txtTuesdayLaunchEndTimeMin.Text = dataInfo.TueLaunchEndTimeMin;
                    ddlTuesdayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.TueLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;

                    chkIsWednesday.Checked = dataInfo.IsWednesday == null ? false : (bool)dataInfo.IsWednesday;
                    txtWednesdayStartTimeHour.Text = dataInfo.WenStartTimeHour;
                    txtWednesdayStartTimeMin.Text = dataInfo.WenStartTimeMin;
                    ddlWednesdayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.WenStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtWednesdayEndTimeHour.Text = dataInfo.WenEndTimeHour;
                    txtWednesdayEndTimeMin.Text = dataInfo.WenEndTimeMin;
                    ddlWednesdayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.WenEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtWednesdayLaunchStartTimeHour.Text = dataInfo.WenLaunchStartTimeHour;
                    txtWednesdayLaunchStartTimeMin.Text = dataInfo.WenLaunchStartTimeMin;
                    ddlWednesdayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.WenStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtWednesdayLaunchEndTimeHour.Text = dataInfo.WenLaunchEndTimeHour;
                    txtWednesdayLaunchEndTimeMin.Text = dataInfo.WenLaunchEndTimeMin;
                    ddlWednesdayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.WenLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;


                    chkIsThursday.Checked = dataInfo.IsThursday == null ? false : (bool)dataInfo.IsThursday;
                    txtThursdayStartTimeHour.Text = dataInfo.ThuStartTimeHour;
                    txtThursdayStartTimeMin.Text = dataInfo.ThuStartTimeMin;
                    ddlThursdayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.ThuStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtThursdayEndTimeHour.Text = dataInfo.ThuEndTimeHour;
                    txtThursdayEndTimeMin.Text = dataInfo.ThuEndTimeMin;
                    ddlThursdayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.ThuEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtThursdayLaunchStartTimeHour.Text = dataInfo.ThuLaunchStartTimeHour;
                    txtThursdayLaunchStartTimeMin.Text = dataInfo.ThuLaunchStartTimeMin;
                    ddlThursdayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.ThuLaunchStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtThursdayLaunchEndTimeHour.Text = dataInfo.ThuLaunchEndTimeHour;
                    txtThursdayLaunchEndTimeMin.Text = dataInfo.ThuLaunchEndTimeMin;
                    ddlThursdayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.ThuLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;

                    chkIsFriday.Checked = dataInfo.IsFriday == null ? false : (bool)dataInfo.IsFriday;
                    txtFridayStartTimeHour.Text = dataInfo.FriStartTimeHour;
                    txtFridayStartTimeMin.Text = dataInfo.FriStartTimeMin;
                    ddlFridayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.FriStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtFridayEndTimeHour.Text = dataInfo.FriEndTimeHour;
                    txtFridayEndTimeMin.Text = dataInfo.FriEndTimeMin;
                    ddlFridayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.FriEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtFridayLaunchStartTimeHour.Text = dataInfo.FriLaunchStartTimeHour;
                    txtFridayLaunchStartTimeMin.Text = dataInfo.FriLaunchStartTimeMin;
                    ddlFridayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.FriLaunchStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtFridayLaunchEndTimeHour.Text = dataInfo.FriLaunchEndTimeHour;
                    txtFridayLaunchEndTimeMin.Text = dataInfo.FriLaunchEndTimeMin;
                    ddlFridayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.FriLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;

                    chkIsSaturday.Checked = dataInfo.IsSaturday == null ? false : (bool)dataInfo.IsSaturday;
                    txtSaturdayStartTimeHour.Text = dataInfo.SatStartTimeHour;
                    txtSaturdayStartTimeMin.Text = dataInfo.SatStartTimeMin;
                    ddlSaturdayStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SatStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtSaturdayEndTimeHour.Text = dataInfo.SatEndTimeHour;
                    txtSaturdayEndTimeMin.Text = dataInfo.SatEndTimeMin;
                    ddlSaturdayEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SatEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;
                    txtSaturdayLaunchStartTimeHour.Text = dataInfo.SatLaunchStartTimeHour;
                    txtSaturdayLaunchStartTimeMin.Text = dataInfo.SatLaunchStartTimeMin;
                    ddlSaturdayLaunchStartTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SatLaunchStartTimeTT) ? "AM" : dataInfo.SunLaunchEndTimeTT;
                    txtSaturdayLaunchEndTimeHour.Text = dataInfo.SatLaunchEndTimeHour;
                    txtSaturdayLaunchEndTimeMin.Text = dataInfo.SatLaunchEndTimeMin;
                    ddlSaturdayLaunchEndTimeTT.SelectedValue = string.IsNullOrWhiteSpace(dataInfo.SatLaunchEndTimeTT) ? "PM" : dataInfo.SunLaunchEndTimeTT;

                    EnableDisableRequiredValidation();
                    BindGridView();
                    ShowHideControl(VisibityType.Schedule);
                }
            }
        }

        protected void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {

                string errorMessage = "";
                SpecializationScheduleModel objBo = new SpecializationScheduleModel();
                LoadControlsAdd(objBo);
                {
                    if (!objRoleMasterRepository.UpdateSpecializationSchedule(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
                ClearScheduleControlValues();
                ClearControlValues();
                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void chkIsSunday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();
        }

        protected void chkIsMonday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        protected void chkIsTuesday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        protected void chkIsWednesday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        protected void chkIsThursday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        protected void chkIsFriday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        protected void chkIsSaturday_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableRequiredValidation();

        }

        private void EnableDisableRequiredValidation()
        {
            if(chkIsSunday.Checked)
            {
                rfvtxtSunStartTimeHour.Enabled = true;
                rfvtxtSunStartTimeMin.Enabled = true;
                rfvtxtSunEndTimeHour.Enabled = true;
                rfvtxtSunEndTimeMin.Enabled = true;
                rfvtxtSunLaunchStartTimeHour.Enabled = true;
                rfvtxtSunLaunchStartTimeMin.Enabled = true;
                rfvtxtSunLaunchEndTimeHour.Enabled = true;
                rfvtxtSunLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtSunStartTimeHour.Enabled = false;
                rfvtxtSunStartTimeMin.Enabled = false;
                rfvtxtSunEndTimeHour.Enabled = false;
                rfvtxtSunEndTimeMin.Enabled = false;
                rfvtxtSunLaunchStartTimeHour.Enabled = false;
                rfvtxtSunLaunchStartTimeMin.Enabled = false;
                rfvtxtSunLaunchEndTimeHour.Enabled = false;
                rfvtxtSunLaunchEndTimeMin.Enabled = false;
            }


            if (chkIsMonday.Checked)
            {
                rfvtxtMondayStartTimeHour.Enabled = true;
                rfvtxtMondayStartTimeMin.Enabled = true;
                rfvtxtMondayEndTimeHour.Enabled = true;
                rfvtxtMondayEndTimeMin.Enabled = true;
                rfvtxtMondayLaunchStartTimeHour.Enabled = true;
                rfvtxtMondayLaunchStartTimeMin.Enabled = true;
                rfvtxtMondayLaunchEndTimeHour.Enabled = true;
                rfvtxtMondayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtMondayStartTimeHour.Enabled = false;
                rfvtxtMondayStartTimeMin.Enabled = false;
                rfvtxtMondayEndTimeHour.Enabled = false;
                rfvtxtMondayEndTimeMin.Enabled = false;
                rfvtxtMondayLaunchStartTimeHour.Enabled = false;
                rfvtxtMondayLaunchStartTimeMin.Enabled = false;
                rfvtxtMondayLaunchEndTimeHour.Enabled = false;
                rfvtxtMondayLaunchEndTimeMin.Enabled = false;
            }


            if (chkIsTuesday.Checked)
            {
                rfvtxtTuesdayStartTimeHour.Enabled = true;
                rfvtxtTuesdayStartTimeMin.Enabled = true;
                rfvtxtTuesdayEndTimeHour.Enabled = true;
                rfvtxtTuesdayEndTimeMin.Enabled = true;
                rfvtxtTuesdayLaunchStartTimeHour.Enabled = true;
                rfvtxtTuesdayLaunchStartTimeMin.Enabled = true;
                rfvtxtTuesdayLaunchEndTimeHour.Enabled = true;
                rfvtxtTuesdayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtTuesdayStartTimeHour.Enabled = false;
                rfvtxtTuesdayStartTimeMin.Enabled = false;
                rfvtxtTuesdayEndTimeHour.Enabled = false;
                rfvtxtTuesdayEndTimeMin.Enabled = false;
                rfvtxtTuesdayLaunchStartTimeHour.Enabled = false;
                rfvtxtTuesdayLaunchStartTimeMin.Enabled = false;
                rfvtxtTuesdayLaunchEndTimeHour.Enabled = false;
                rfvtxtTuesdayLaunchEndTimeMin.Enabled = false;
            }

            if (chkIsWednesday.Checked)
            {
                rfvtxtWednesdayStartTimeHour.Enabled = true;
                rfvtxtWednesdayStartTimeMin.Enabled = true;
                rfvtxtWednesdayEndTimeHour.Enabled = true;
                rfvtxtWednesdayEndTimeMin.Enabled = true;
                rfvtxtWednesdayLaunchStartTimeHour.Enabled = true;
                rfvtxtWednesdayLaunchStartTimeMin.Enabled = true;
                rfvtxtWednesdayLaunchEndTimeHour.Enabled = true;
                rfvtxtWednesdayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtWednesdayStartTimeHour.Enabled = false;
                rfvtxtWednesdayStartTimeMin.Enabled = false;
                rfvtxtWednesdayEndTimeHour.Enabled = false;
                rfvtxtWednesdayEndTimeMin.Enabled = false;
                rfvtxtWednesdayLaunchStartTimeHour.Enabled = false;
                rfvtxtWednesdayLaunchStartTimeMin.Enabled = false;
                rfvtxtWednesdayLaunchEndTimeHour.Enabled = false;
                rfvtxtWednesdayLaunchEndTimeMin.Enabled = false;
            }

            if (chkIsThursday.Checked)
            {
                rfvtxtThursdayStartTimeHour.Enabled = true;
                rfvtxtThursdayStartTimeMin.Enabled = true;
                rfvtxtThursdayEndTimeHour.Enabled = true;
                rfvtxtThursdayEndTimeMin.Enabled = true;
                rfvtxtThursdayLaunchStartTimeHour.Enabled = true;
                rfvtxtThursdayLaunchStartTimeMin.Enabled = true;
                rfvtxtThursdayLaunchEndTimeHour.Enabled = true;
                rfvtxtThursdayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtThursdayStartTimeHour.Enabled = false;
                rfvtxtThursdayStartTimeMin.Enabled = false;
                rfvtxtThursdayEndTimeHour.Enabled = false;
                rfvtxtThursdayEndTimeMin.Enabled = false;
                rfvtxtThursdayLaunchStartTimeHour.Enabled = false;
                rfvtxtThursdayLaunchStartTimeMin.Enabled = false;
                rfvtxtThursdayLaunchEndTimeHour.Enabled = false;
                rfvtxtThursdayLaunchEndTimeMin.Enabled = false;
            }

            if (chkIsFriday.Checked)
            {
                rfvtxtFridayStartTimeHour.Enabled = true;
                rfvtxtFridayStartTimeMin.Enabled = true;
                rfvtxtFridayEndTimeHour.Enabled = true;
                rfvtxtFridayEndTimeMin.Enabled = true;
                rfvtxtFridayLaunchStartTimeHour.Enabled = true;
                rfvtxtFridayLaunchStartTimeMin.Enabled = true;
                rfvtxtFridayLaunchEndTimeHour.Enabled = true;
                rfvtxtFridayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtFridayStartTimeHour.Enabled = false;
                rfvtxtFridayStartTimeMin.Enabled = false;
                rfvtxtFridayEndTimeHour.Enabled = false;
                rfvtxtFridayEndTimeMin.Enabled = false;
                rfvtxtFridayLaunchStartTimeHour.Enabled = false;
                rfvtxtFridayLaunchStartTimeMin.Enabled = false;
                rfvtxtFridayLaunchEndTimeHour.Enabled = false;
                rfvtxtFridayLaunchEndTimeMin.Enabled = false;
            }

            if (chkIsSaturday.Checked)
            {
                rfvtxtSaturdayStartTimeHour.Enabled = true;
                rfvtxtSaturdayStartTimeMin.Enabled = true;
                rfvtxtSaturdayEndTimeHour.Enabled = true;
                rfvtxtSaturdayEndTimeMin.Enabled = true;
                rfvtxtSaturdayLaunchStartTimeHour.Enabled = true;
                rfvtxtSaturdayLaunchStartTimeMin.Enabled = true;
                rfvtxtSaturdayLaunchEndTimeHour.Enabled = true;
                rfvtxtSaturdayLaunchEndTimeMin.Enabled = true;
            }
            else
            {
                rfvtxtSaturdayStartTimeHour.Enabled = false;
                rfvtxtSaturdayStartTimeMin.Enabled = false;
                rfvtxtSaturdayEndTimeHour.Enabled = false;
                rfvtxtSaturdayEndTimeMin.Enabled = false;
                rfvtxtSaturdayLaunchStartTimeHour.Enabled = false;
                rfvtxtSaturdayLaunchStartTimeMin.Enabled = false;
                rfvtxtSaturdayLaunchEndTimeHour.Enabled = false;
                rfvtxtSaturdayLaunchEndTimeMin.Enabled = false;
            }
        }

        protected void btnApplyAll_Click(object sender, EventArgs e)
        {
            chkIsSunday.Checked = true;
            txtSunStartTimeHour.Text              = txtCommanStartTimeHour.Text;
            txtSunStartTimeMin.Text               = txtCommanStartTimeMin.Text;
            ddlSunStartTimeTT.SelectedValue       = ddlCommanStartTT.Text;
            txtSunEndTimeHour.Text                = txtCommanEndTimeHour.Text;
            txtSunEndTimeMin.Text                 = txtCommanEndTimeMin.Text;
            ddlSunEndTimeTT.SelectedValue         = ddlCommanEndTT.Text;
            txtSunLaunchStartTimeHour.Text        = txtCommonLaunchStartTimeHour.Text;
            txtSunLaunchStartTimeMin.Text         = txtCommonLaunchStartTimeMin.Text;
            ddlSunLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtSunLaunchEndTimeHour.Text          = txtCommonLaunchEndTimeHour.Text;
            txtSunLaunchEndTimeMin.Text           = txtCommonLaunchEndTimeMin.Text;
            ddlSunLaunchEndTimeTT.SelectedValue   = ddlCommonLaunchEndTimeTT.Text;

            chkIsMonday.Checked = true;
            txtMondayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtMondayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlMondayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtMondayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtMondayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlMondayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtMondayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtMondayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlMondayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtMondayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtMondayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlMondayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;
            
            chkIsTuesday.Checked = true;
            txtTuesdayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtTuesdayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlTuesdayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtTuesdayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtTuesdayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlTuesdayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtTuesdayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtTuesdayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlTuesdayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtTuesdayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtTuesdayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlTuesdayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;

            chkIsWednesday.Checked = true;
            txtWednesdayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtWednesdayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlWednesdayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtWednesdayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtWednesdayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlWednesdayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtWednesdayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtWednesdayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlWednesdayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtWednesdayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtWednesdayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlWednesdayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;

            chkIsThursday.Checked = true;
            txtThursdayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtThursdayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlThursdayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtThursdayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtThursdayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlThursdayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtThursdayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtThursdayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlThursdayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtThursdayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtThursdayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlThursdayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;
            
            chkIsFriday.Checked = true;
            txtFridayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtFridayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlFridayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtFridayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtFridayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlFridayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtFridayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtFridayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlFridayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtFridayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtFridayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlFridayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;


            chkIsSaturday.Checked = true;
            txtSaturdayStartTimeHour.Text = txtCommanStartTimeHour.Text;
            txtSaturdayStartTimeMin.Text = txtCommanStartTimeMin.Text;
            ddlSaturdayStartTimeTT.SelectedValue = ddlCommanStartTT.Text;
            txtSaturdayEndTimeHour.Text = txtCommanEndTimeHour.Text;
            txtSaturdayEndTimeMin.Text = txtCommanEndTimeMin.Text;
            ddlSaturdayEndTimeTT.SelectedValue = ddlCommanEndTT.Text;
            txtSaturdayLaunchStartTimeHour.Text = txtCommonLaunchStartTimeHour.Text;
            txtSaturdayLaunchStartTimeMin.Text = txtCommonLaunchStartTimeMin.Text;
            ddlSaturdayLaunchStartTimeTT.SelectedValue = ddlCommonLaunchStartTimeTT.Text;
            txtSaturdayLaunchEndTimeHour.Text = txtCommonLaunchEndTimeHour.Text;
            txtSaturdayLaunchEndTimeMin.Text = txtCommonLaunchEndTimeMin.Text;
            ddlSaturdayLaunchEndTimeTT.SelectedValue = ddlCommonLaunchEndTimeTT.Text;
        }
    }
}