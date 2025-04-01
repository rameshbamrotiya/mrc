using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class UnitTeamMaster : System.Web.UI.Page
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
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearControlValues();
                BindSpecializationDropDown();
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


        private void BindSpecializationDropDown()
        {
            ddlWeekDropDown.Items.Clear();
            Array colors = Enum.GetValues(typeof(DayOfWeek));
            ddlWeekDropDown.Items.Add(new ListItem("Select Week Day", null));
            foreach (DayOfWeek color in colors)
            {
                ddlWeekDropDown.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {
                ddlSpecialization.DataSource = objRoleMasterRepository.GetAllSpecialization().Where(x => x.IsActive == true).ToList();
                ddlSpecialization.DataTextField = "DepartmentName";
                ddlSpecialization.DataValueField = "Id";
                ddlSpecialization.DataBind();
                ddlSpecialization.Items.Insert(0, new ListItem
                {
                    Value = null,
                    Text = "Select Specialization"
                });
            }
        }

        private void ClearControlValues()
        {
            txtUnitName.Text = "";
            txtSunStartTimeHour.Text = "";
            txtSunStartTimeMin.Text = "";
            ddlSpecialization.SelectedIndex = 0;
            ddlSunStartTimeTT.SelectedIndex = 0;

            txtSunEndTimeHour.Text = "";
            txtSunEndTimeMin.Text = "";
            ddlSunEndTimeTT.SelectedIndex = 0;
                        
            chkIsActive.Checked = false;
            ddlWeekDropDown.SelectedIndex = 0;

            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            GetListValueDoctor = null;
            BindDoctorDropDown();
            BindgvDoctor();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private bool LoadControlsAdd(UnitTeamModel objBo)
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Unit Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.UnitName = txtUnitName.Text.Trim();
            }

            if (ddlSpecialization.SelectedIndex > 0)
            {
                objBo.SpecilizationId = Convert.ToInt32(ddlSpecialization.SelectedValue);

                using (IUnitTeamRepository objEdu = new UnitTeamRepository(Functions.strSqlConnectionString))
                {
                    var objDatas = objEdu.GetAllUnit().Where(x => x.SpecilizationId == (Convert.ToInt32(ddlSpecialization.SelectedValue))).ToList();
                    foreach (var data in objDatas)
                    {
                        if ((DayOfWeek)Convert.ToInt32(ddlWeekDropDown.SelectedValue) == (DayOfWeek)data.WeekNo)
                        {
                            Functions.MessagePopup(this, "Selected Specialization already have same day of Week", PopupMessageType.error);
                            return false;
                        }
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
                return false;
            }

            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtSunStartTimeHour.Text.Trim()))
            {
                objBo.StartTimeHour = txtSunStartTimeHour.Text;
            }

            if (!string.IsNullOrEmpty(txtSunStartTimeMin.Text.Trim()))
            {
                objBo.StartTimeMin = txtSunStartTimeMin.Text;
            }
            if (!string.IsNullOrEmpty(ddlSunStartTimeTT.SelectedValue))
            {
                objBo.StartTimeTT = ddlSunStartTimeTT.SelectedValue;
            }

            if (ddlWeekDropDown.SelectedIndex>0)
            {
                objBo.WeekNo = Convert.ToInt32(ddlWeekDropDown.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtSunEndTimeHour.Text.Trim()))
            {
                objBo.EndTimeHour = txtSunEndTimeHour.Text;
            }

            if (!string.IsNullOrEmpty(txtSunEndTimeMin.Text.Trim()))
            {
                objBo.EndTimeMin = txtSunEndTimeMin.Text;
            }
            if (!string.IsNullOrEmpty(ddlSunEndTimeTT.SelectedValue))
            {
                objBo.EndTimeTT = ddlSunEndTimeTT.SelectedValue;
            }

            objBo.IsActive = chkIsActive.Checked;

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }
            return true;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objUnitTeamRepository.GetUnitByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (dataInfo != null)
                    {
                        BindDoctorDropDown(dataInfo.Id);
                        txtUnitName.Text = dataInfo.UnitName;
                        
                        GetListValueDoctor = Functions.ToDataTable(objUnitTeamRepository.GetAllUnitDoctorMasterByLangId(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)));
                        BindgvDoctor();
                        ShowHideControl(VisibityType.Edit);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language First", PopupMessageType.warning);
                return;
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
            {
                UnitTeamModel objBO = new UnitTeamModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objUnitTeamRepository.InsertOrUpdateUnit(objBO, out errorMessage))
                    {
                        if (GetListValueDoctor.Rows.Count > 0)
                        {
                            foreach (DataRow row in GetListValueDoctor.Rows)
                            {
                                if (row["Id"].ToString().Contains("Temp_"))
                                {
                                    if (objUnitTeamRepository.InsertUnitDoctor(new UnitDoctorMasterModel { UnitId = objBO.Id, LangId = objBO.LangId, DoctorId = Convert.ToInt32(row["DoctorId"].ToString()) }, out errorMessage))
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
            {
                UnitTeamModel objBO = new UnitTeamModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objUnitTeamRepository.InsertOrUpdateUnit(objBO, out errorMessage))
                    {
                        if (GetListValueDoctor.Rows.Count > 0)
                        {
                            foreach (DataRow row in GetListValueDoctor.Rows)
                            {
                                if (row["Id"].ToString().Contains("Temp_"))
                                {
                                    if (objUnitTeamRepository.InsertUnitDoctor(new UnitDoctorMasterModel { UnitId = objBO.Id, LangId = objBO.LangId, DoctorId = Convert.ToInt32(row["DoctorId"].ToString()) }, out errorMessage))
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }


        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(VisibityType.GridView);
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
        
        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IUnitTeamRepository objAcc = new UnitTeamRepository(Functions.strSqlConnectionString))
            {
                ddlLanguage.SelectedIndex = 1;
                var dataInfo = objAcc.GetUnitByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    ddlLanguage.Enabled = true;
                    BindDoctorDropDown(dataInfo.Id);
                    txtUnitName.Text = dataInfo.UnitName;
                    txtSunStartTimeHour.Text = dataInfo.StartTimeHour;
                    txtSunStartTimeMin.Text = dataInfo.StartTimeMin;
                    ddlSunStartTimeTT.SelectedValue = dataInfo.StartTimeTT;

                    ddlSpecialization.SelectedValue = dataInfo.SpecilizationId.ToString();
                    ddlWeekDropDown.SelectedValue = dataInfo.WeekNo.ToString();
                    chkIsActive.Checked = dataInfo.IsActive==null?false :(bool)dataInfo.IsActive;

                    txtSunEndTimeHour.Text = dataInfo.EndTimeHour;
                    txtSunEndTimeMin.Text = dataInfo.EndTimeMin;
                    ddlSunEndTimeTT.SelectedValue = dataInfo.EndTimeTT;
                    GetListValueDoctor = Functions.ToDataTable(objAcc.GetAllUnitDoctorMasterByLangId(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)));
                    BindgvDoctor();
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
                using (IUnitTeamRepository objAcc = new UnitTeamRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveUnit(rowId, out errorMessage))
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

        #region Bind Doctor Detail
        private DataTable GetListValueDoctor
        {
            get
            {
                DataTable result = new DataTable();

                if (ViewState["someID"] != null)
                {
                    result = (DataTable)ViewState["someID"];
                }
                else
                {
                    result = new DataTable("tblgvDoctor");
                    result.Columns.Add(new DataColumn("Id", typeof(string)));
                    result.Columns.Add(new DataColumn("DoctorId", typeof(string)));
                    result.Columns.Add(new DataColumn("DoctorName", typeof(string)));
                }

                return result;
            }
            set
            {
                ViewState["someID"] = value;
            }
        }

        private void BindDoctorDropDown(long AccId = 0)
        {
            if (ddlLanguage.SelectedIndex <= 0)
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return;
            }
            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                IEnumerable<long> sunData = null;
                List<GetAllDoctorByLanguageIdResult> data = objAcc.GetAllAcademicMedicalDoctor(Convert.ToInt32(ddlLanguage.SelectedValue));
                if (AccId != 0)
                {
                    using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
                    {
                        sunData = objUnitTeamRepository.GetAllUnitDoctorMasterByLangId(AccId, Convert.ToInt32(ddlLanguage.SelectedValue)).Select(x => ((long)x.DoctorId)).ToList().Distinct();
                    }
                }

                DataTable dt = GetListValueDoctor;
                foreach (DataRow row in dt.Rows)
                {
                    sunData.ToList().Add(Convert.ToInt32(row["DoctorId"].ToString()));
                }
                if (sunData != null)
                {
                    data = data.Where(x => !sunData.ToList().Distinct().Contains(x.Id)).ToList();
                }
                if (data != null)
                {
                    ddlDoctorList.DataSource = data;
                    ddlDoctorList.DataTextField = "Name";
                    ddlDoctorList.DataValueField = "Id";
                    ddlDoctorList.DataBind();
                }
                ddlDoctorList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", null));
            }
        }

        private void BindgvDoctor()
        {

            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            bool isNoError = true;
            if (ddlDoctorList.SelectedIndex <= 0)
            {
                Functions.MessagePopup(this, "Please Doctor From List", PopupMessageType.error);
                return;
            }
            if (isNoError)
            {
                DataTable dt = null;
                dt = GetListValueDoctor;
                DataRow dr = dt.NewRow();
                int rowCount = 0;
                if (dt.Rows.Count > 0)
                {
                    rowCount = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Id"].ToString());
                }
                dr["Id"] = "Temp_" + (rowCount + 1);
                dr["DoctorId"] = ddlDoctorList.SelectedValue;
                dr["DoctorName"] = ddlDoctorList.SelectedItem;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                GetListValueDoctor = dt;
            }
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            ddlDoctorList.SelectedIndex = 0;
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (gvDoctor.DataKeys[rowindex]["Id"].ToString());
            GridViewRow gvRow = gvDoctor.Rows[rowindex];

            DataTable dt = null;
            dt = GetListValueDoctor;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Id"].ToString() == rowId)
                    dr.Delete();
            }
            dt.AcceptChanges();

            if (hfId.Value != "0" && string.IsNullOrWhiteSpace(hfId.Value) && !rowId.Contains("Temp_"))
            {
                using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
                {
                    if (!objUnitTeamRepository.RemoveUnitDoctor(Convert.ToInt32(rowId), out strError))
                    {

                        Functions.MessagePopup(this, strError, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.error);

                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Record Removed Done", PopupMessageType.success);
            }
            GetListValueDoctor = dt;
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            ShowHideControl(VisibityType.Edit);
        }
        #endregion
    }
}