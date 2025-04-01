using BAL;
using BO;
using com.itextpdf.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Appointment
{
    public partial class ConfigurUnitMaster : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            if (!Page.IsPostBack)
            {
                BindGridView();

                ClearControlValues();
                BindDepartmentByLangId(1);
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

        private void BindDepartmentByLangId(int LangId)
        {
            ddlSpecialization.Items.Clear();
            ddlWeekDropDown.Items.Clear();
            Array colors = Enum.GetValues(typeof(DayOfWeek));
            //ddlWeekDropDown.Items.Add(new ListItem("Select Week Day", null));
            foreach (DayOfWeek color in colors)
            {
                ddlWeekDropDown.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {
                ddlSpecialization.DataSource = objDepartmentRepository.GetAllTblDepartment(LangId);
                ddlSpecialization.DataValueField = "Id";
                ddlSpecialization.DataTextField = "DepartmentName";
                ddlSpecialization.DataBind();
                ddlSpecialization.Items.Insert(0, new ListItem("Select", "-1"));
            }
            //OPDTimingsBAL objBAL = new OPDTimingsBAL();
            //facultyNameForUnitConfig objbo = new facultyNameForUnitConfig();
            //objbo.LanguageId = 1;
            //DataSet ds = objBAL.GetFacultylistforUnitConfig(objbo);
            //DataTable data = new DataTable();
            //data = ds.Tables[0];
            //if (data.Rows.Count > 0)
            //{
            //    ddlSpecialization.DataSource = data;
            //    ddlSpecialization.DataTextField = "FacultyName";
            //    ddlSpecialization.DataValueField = "Id";
            //    ddlSpecialization.DataBind();
            //    ddlSpecialization.Items.Insert(0, new ListItem("Select", "-1"));
            //}
        }
        //private void BindSpecializationDropDown()
        //{


        //    ddlWeekDropDown.Items.Clear();
        //    Array colors = Enum.GetValues(typeof(DayOfWeek));
        //    //ddlWeekDropDown.Items.Add(new ListItem("Select Week Day", null));
        //    foreach (DayOfWeek color in colors)
        //    {
        //        ddlWeekDropDown.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
        //    }


        //    using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
        //    {
        //        ddlSpecialization.DataSource = objRoleMasterRepository.GetAllSpecialization().Where(x => x.IsActive == true).ToList();
        //        ddlSpecialization.DataTextField = "DepartmentName";
        //        ddlSpecialization.DataValueField = "Id";
        //        ddlSpecialization.DataBind();
        //        ddlSpecialization.Items.Insert(0, new ListItem
        //        {
        //            Value = null,
        //            Text = "Select DepartmentName"
        //        });
        //    }
        //}

        private void ClearControlValues()
        {
            txtmaxSlot.Text = "";
            txtSlotName.Text = "";
            txtUnitName.Text = "";
            txtSunStartTimeHour.Text = "";
            txtSunStartTimeMin.Text = "";
            ddlSpecialization.SelectedIndex = 0;
            ddlSunStartTimeTT.SelectedIndex = 0;
            hfId.Value = "";
            txtSunEndTimeHour.Text = "";
            txtSunEndTimeMin.Text = "";
            ddlSunEndTimeTT.SelectedIndex = 0;
            hdnDoctorList.Value = "0";
            chkIsActive.Checked = false;
            ddlWeekDropDown.SelectedIndex = 0;
            ddlDoctorList.SelectedIndex = 0;
            GetListValueDoctor = null;
            GetListSloteDetails = null;
            BindDoctorDropDown();
            BindgvDoctor();
            BindgvSlote();
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

                //using (IUnitTeamRepository objEdu = new UnitTeamRepository(Functions.strSqlConnectionString))
                //{
                //var objDatas = objEdu.GetAllUnit().Where(x => x.SpecilizationId == (Convert.ToInt32(ddlSpecialization.SelectedValue))).ToList();
                //foreach (var data in objDatas)
                //{
                //    if ((DayOfWeek)Convert.ToInt32(ddlWeekDropDown.SelectedValue) == (DayOfWeek)data.WeekNo)
                //    {
                //        Functions.MessagePopup(this, "Selected Specialization already have same day of Week", PopupMessageType.error);
                //        return false;
                //    }
                //}
                //}
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
                return false;
            }
            //List<string> selectedValues = new List<string>();

            //foreach (ListItem item in ddlDoctorList.Items)
            //{
            //    if (item.Selected)
            //    {
            //        selectedValues.Add(item.Value);
            //    }
            //}

            string resultDoctor = hdnDoctorList.Value;
            string resultWeekend = hdnweekday.Value;


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

            if (ddlWeekDropDown.SelectedIndex > 0)
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

            if (!string.IsNullOrEmpty(txtSlotName.Text.Trim()))
            {
                objBo.SloteName = txtSlotName.Text;
            }

            if (!string.IsNullOrEmpty(txtmaxSlot.Text.Trim()))
            {
                objBo.maxSlot = Convert.ToInt32(txtmaxSlot.Text);
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


        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
            {
                UnitTeamModel objBO = new UnitTeamModel();
                if (LoadControlsAdd(objBO))
                {
                    SessionUserModel objssn = new SessionUserModel();


                    ConfigUnitMasterBO objsave = new ConfigUnitMasterBO();
                    objsave.Id = objBO.Id;
                    objsave.SpecilizationId = objBO.SpecilizationId;
                    objsave.UnitName = objBO.UnitName;
                    objsave.IsActive = objBO.IsActive;
                    objsave.IsDelete = objBO.IsDelete;
                    objsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);


                    if (new ConfigUnitBAL().InsertRecord(objsave))
                    //    if (!objUnitTeamRepository.InsertOrUpdateUnit(objBO, out errorMessage))
                    {
                        new ConfigUnitDoctorDetailMasterBAL().DeleteRecord(objsave.Id);
                        foreach (string item in hdnDoctorList.Value.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(item.Trim()))
                            {
                                ConfigUnitDoctorDetailMasterBO objdocsave = new ConfigUnitDoctorDetailMasterBO();
                                objdocsave.Id = objBO.Id;
                                objdocsave.DoctorId = Convert.ToInt64(item.Trim());// objBO.SpecilizationId;
                                objdocsave.UnitId = Convert.ToInt32(objsave.Id);
                                objdocsave.IsActive = objBO.IsActive;
                                objdocsave.IsDelete = objBO.IsDelete;
                                objdocsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);
                                new ConfigUnitDoctorDetailMasterBAL().InsertRecord(objdocsave);

                            }
                        }

                        //List<string> selectedweekday = new List<string>();

                        //foreach (string item in hdnweekday.Value.Split(','))
                        //{
                        //    if (!string.IsNullOrWhiteSpace(item.Trim()))
                        //    {
                        new ConfigUnitSlotDetailMasterBAL().DeleteRecord(objsave.Id);
                        if (GetListSloteDetails.Rows.Count > 0)
                        {
                            foreach (DataRow row in GetListSloteDetails.Rows)
                            {
                                //selectedweekday.Add(item.Value);
                                ConfigUnitSlotDetailMasterBO objdsslotsave = new ConfigUnitSlotDetailMasterBO();
                                objdsslotsave.WeekNo = Convert.ToInt32(row["WeekId"].ToString());// objBO.SpecilizationId;
                                objdsslotsave.SloteName = row["SloteName"].ToString();
                                objdsslotsave.maxSlot = Convert.ToInt32(row["MaxSlote"].ToString());
                                objdsslotsave.Id = Convert.ToInt32(objsave.Id);
                                objdsslotsave.StartTimeHour = row["SunStartTimeHour"].ToString();
                                objdsslotsave.StartTimeMin = row["SunStartTimeMin"].ToString();
                                objdsslotsave.StartTimeTT = row["SunStartTimeTT"].ToString();
                                objdsslotsave.EndTimeHour = row["SunEndTimeHour"].ToString();
                                objdsslotsave.EndTimeMin = row["SunEndTimeMin"].ToString();
                                objdsslotsave.EndTimeTT = row["SunEndTimeTT"].ToString();
                                objdsslotsave.IsActive = objBO.IsActive;
                                objdsslotsave.IsDelete = objBO.IsDelete;
                                objdsslotsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);

                                new ConfigUnitSlotDetailMasterBAL().InsertRecord(objdsslotsave);

                            }
                        }
                        //if (GetListValueDoctor.Rows.Count > 0)
                        //{
                        //    foreach (DataRow row in GetListValueDoctor.Rows)
                        //    {
                        //        if (row["Id"].ToString().Contains("Temp_"))
                        //        {
                        //            if (objUnitTeamRepository.InsertUnitDoctor(new UnitDoctorMasterModel { UnitId = objBO.Id, LangId = objBO.LangId, DoctorId = Convert.ToInt32(row["DoctorId"].ToString()) }, out errorMessage))
                        //            {
                        //                return;
                        //            }
                        //        }
                        //    }
                        //}

                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, "Record Update Successfully", PopupMessageType.success);
                    }
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            var dataweeks = hdnweekday.Value;
            var datadocs = hdnDoctorList.Value;
            var selecteddays = ddlWeekDropDown.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
            var selecteddoctors = ddlDoctorList.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();

            string errorMessage = "";
            //using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
            //{
            UnitTeamModel objBO = new UnitTeamModel();
            if (LoadControlsAdd(objBO))
            {
                SessionUserModel objssn = new SessionUserModel();


                ConfigUnitMasterBO objsave = new ConfigUnitMasterBO();
                objsave.Id = objBO.Id;
                objsave.SpecilizationId = objBO.SpecilizationId;
                objsave.UnitName = objBO.UnitName;
                objsave.IsActive = objBO.IsActive;
                objsave.IsDelete = objBO.IsDelete;
                objsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);


                if (new ConfigUnitBAL().InsertRecord(objsave))
                //    if (!objUnitTeamRepository.InsertOrUpdateUnit(objBO, out errorMessage))
                {


                    foreach (string item in hdnDoctorList.Value.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Trim()))
                        {
                            ConfigUnitDoctorDetailMasterBO objdocsave = new ConfigUnitDoctorDetailMasterBO();
                            objdocsave.Id = objBO.Id;
                            objdocsave.DoctorId = Convert.ToInt64(item.Trim());// objBO.SpecilizationId;
                            objdocsave.UnitId = Convert.ToInt32(objsave.Id);
                            objdocsave.IsActive = objBO.IsActive;
                            objdocsave.IsDelete = objBO.IsDelete;
                            objdocsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);
                            new ConfigUnitDoctorDetailMasterBAL().InsertRecord(objdocsave);

                        }
                    }

                    //List<string> selectedweekday = new List<string>();

                    //foreach (string item in hdnweekday.Value.Split(','))
                    //{
                    //    if (!string.IsNullOrWhiteSpace(item.Trim()))
                    //    {
                    if (GetListSloteDetails.Rows.Count > 0)
                    {
                        foreach (DataRow row in GetListSloteDetails.Rows)
                        {
                            //selectedweekday.Add(item.Value);
                            ConfigUnitSlotDetailMasterBO objdsslotsave = new ConfigUnitSlotDetailMasterBO();
                            objdsslotsave.WeekNo = Convert.ToInt32(row["WeekId"].ToString());// objBO.SpecilizationId;
                            objdsslotsave.SloteName = row["SloteName"].ToString();
                            objdsslotsave.maxSlot = Convert.ToInt32(row["MaxSlote"].ToString());
                            objdsslotsave.Id = Convert.ToInt32(objsave.Id);
                            objdsslotsave.StartTimeHour = row["SunStartTimeHour"].ToString();
                            objdsslotsave.StartTimeMin = row["SunStartTimeMin"].ToString();
                            objdsslotsave.StartTimeTT = row["SunStartTimeTT"].ToString();
                            objdsslotsave.EndTimeHour = row["SunEndTimeHour"].ToString();
                            objdsslotsave.EndTimeMin = row["SunEndTimeMin"].ToString();
                            objdsslotsave.EndTimeTT = row["SunEndTimeTT"].ToString();
                            objdsslotsave.IsActive = objBO.IsActive;
                            objdsslotsave.IsDelete = objBO.IsDelete;
                            objdsslotsave.Username = Convert.ToInt32(SessionWrapper.UserDetails.Id);

                            new ConfigUnitSlotDetailMasterBAL().InsertRecord(objdsslotsave);

                        }
                    }




                    //if (GetListValueDoctor.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in GetListValueDoctor.Rows)
                    //    {
                    //        if (row["Id"].ToString().Contains("Temp_"))
                    //        {
                    //            if (objUnitTeamRepository.InsertUnitDoctor(new UnitDoctorMasterModel { UnitId = objBO.Id, LangId = objBO.LangId, DoctorId = Convert.ToInt32(row["DoctorId"].ToString()) }, out errorMessage))
                    //            {
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}

                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                    Functions.MessagePopup(this, "Record Save Successfully", PopupMessageType.success);
                }
            }
            // }
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
                ClearControlValues();
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
                //var dataInfo = objAcc.GetUnitByLangId((Convert.ToInt32(hfId.Value)), 1);
                var dataInfo = objAcc.GetConfigUnitByLangId((Convert.ToInt32(hfId.Value)), 1);
                if (dataInfo != null)
                {
                    BindDoctorDropDown(dataInfo.Id);
                    txtUnitName.Text = dataInfo.UnitName;
                    //txtSunStartTimeHour.Text = dataInfo.StartTimeHour;
                    //txtSunStartTimeMin.Text = dataInfo.StartTimeMin;
                    //ddlSunStartTimeTT.SelectedValue = dataInfo.StartTimeTT;
                    hdnDoctorList.Value = dataInfo.doctorid;
                    ddlSpecialization.SelectedValue = dataInfo.SpecilizationId.ToString();
                    //ddlWeekDropDown.SelectedValue = dataInfo.WeekNo.ToString();
                    chkIsActive.Checked = dataInfo.IsActive == null ? false : (bool)dataInfo.IsActive;

                    //txtSunEndTimeHour.Text = dataInfo.EndTimeHour;
                    //txtSunEndTimeMin.Text = dataInfo.EndTimeMin;
                    //ddlSunEndTimeTT.SelectedValue = dataInfo.EndTimeTT;
                    //GetListValueDoctor = Functions.ToDataTable(objAcc.GetAllUnitDoctorMasterByLangId(dataInfo.Id, Convert.ToInt32(1)));
                    ConfigSloteDetailsByidforEdit objdsslotsave = new ConfigSloteDetailsByidforEdit();
                    objdsslotsave.Id = (Convert.ToInt32(hfId.Value));
                    DataSet ds = new ConfigUnitBAL().SelectUnitSloteRecord(objdsslotsave);
                    GetListSloteDetails = (ds.Tables[0]);
                    BindgvSlote();
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
            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                //IEnumerable<long> sunData = null;
                //List<GetAllDoctorByLanguageIdResult> data = objAcc.GetAllAcademicMedicalDoctor(Convert.ToInt32(1));
                //if (AccId != 0)
                //{
                //    //using (IUnitTeamRepository objUnitTeamRepository = new UnitTeamRepository(Functions.strSqlConnectionString))
                //    //{
                //    //    sunData = objUnitTeamRepository.GetAllUnitDoctorMasterByLangId(AccId, Convert.ToInt32(1)).Select(x => ((long)x.DoctorId)).ToList().Distinct();
                //    //}
                //}

                //DataTable dt = GetListValueDoctor;
                //foreach (DataRow row in dt.Rows)
                //{
                //    sunData.ToList().Add(Convert.ToInt32(row["DoctorId"].ToString()));
                //}
                //if (sunData != null)
                //{
                //    data = data.Where(x => !sunData.ToList().Distinct().Contains(x.Id)).ToList();
                //}
                OPDTimingsBAL objBAL = new OPDTimingsBAL();
                //OPDTimingsBO objbo = new OPDTimingsBO();
                facultyNameForUnitConfig objbo = new facultyNameForUnitConfig();
                objbo.LanguageId = 1;
                DataSet ds = objBAL.GetFacultylistforUnitConfig(objbo);
                DataTable data = new DataTable();
                data = ds.Tables[0];
                if (data.Rows.Count > 0)
                {
                    ddlDoctorList.DataSource = data;
                    ddlDoctorList.DataTextField = "FacultyName";
                    ddlDoctorList.DataValueField = "Id";
                    ddlDoctorList.DataBind();
                }
                // ddlDoctorList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", null));
            }
        }

        private void BindgvDoctor()
        {

            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
        }
        private void BindgvSlote()
        {

            grivewslote.DataSource = GetListSloteDetails;
            grivewslote.DataBind();
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
        #region Slot entry 
        private DataTable GetListSloteDetails
        {
            get
            {
                DataTable result = new DataTable();

                if (ViewState["tblSloteDetails"] != null)
                {
                    result = (DataTable)ViewState["tblSloteDetails"];
                }
                else
                {
                    result = new DataTable("tblgvSlote");
                    result.Columns.Add(new DataColumn("Id", typeof(string)));
                    result.Columns.Add(new DataColumn("WeekId", typeof(string)));
                    result.Columns.Add(new DataColumn("WeekName", typeof(string)));
                    result.Columns.Add(new DataColumn("MaxSlote", typeof(string)));
                    result.Columns.Add(new DataColumn("SloteName", typeof(string)));
                    result.Columns.Add(new DataColumn("SunStartTimeHour", typeof(string)));
                    result.Columns.Add(new DataColumn("SunStartTimeMin", typeof(string)));
                    result.Columns.Add(new DataColumn("SunStartTimeTT", typeof(string)));
                    result.Columns.Add(new DataColumn("SunEndTimeHour", typeof(string)));
                    result.Columns.Add(new DataColumn("SunEndTimeMin", typeof(string)));
                    result.Columns.Add(new DataColumn("SunEndTimeTT", typeof(string)));
                    result.Columns.Add(new DataColumn("StartTime", typeof(string)));
                    result.Columns.Add(new DataColumn("EndTime", typeof(string)));
                }

                return result;
            }
            set
            {
                ViewState["tblSloteDetails"] = value;
            }
        }
        protected void btnAddSlot_Click(object sender, EventArgs e)
        {
            bool isNoError = true;
            if (string.IsNullOrEmpty(txtSlotName.Text))
            {
                Functions.MessagePopup(this, "Enter Slote Name", PopupMessageType.error);
                return;
            }
            if (!string.IsNullOrEmpty(txtSunStartTimeHour.Text))
            {
                string inputStart = txtSunStartTimeHour.Text + " " + txtSunStartTimeMin.Text + " " + ddlSunStartTimeTT.SelectedItem; // example input
                string inputendtime = txtSunEndTimeHour.Text + " " + txtSunEndTimeMin.Text + " " + ddlSunEndTimeTT.SelectedItem; // example input
                DateTime Starttime;
                DateTime Endtime;

                try
                {
                    Starttime = DateTime.ParseExact(inputStart, "hh mm tt", CultureInfo.InvariantCulture);
                    string Starttimew = Starttime.ToString("HH:mm");
                    TimeSpan timeSpanStart = TimeSpan.Parse(Starttimew);
                    Endtime = DateTime.ParseExact(inputendtime, "hh mm tt", CultureInfo.InvariantCulture);
                    string Endtimew = Endtime.ToString("HH:mm");
                    TimeSpan timeSpanEndtime = TimeSpan.Parse(Endtimew);
                    if (timeSpanStart >= timeSpanEndtime && timeSpanEndtime <= timeSpanStart)
                    {
                        Functions.MessagePopup(this, "Enter valid time", PopupMessageType.error);
                        return;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid format.");
                }
            }
            if (isNoError)
            {
                DataTable dt = null;
                dt = GetListSloteDetails;
                DataRow dr = dt.NewRow();
                int rowCount = 0;
                if (dt.Rows.Count > 0)
                {
                    rowCount = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Id"].ToString());
                }
                dr["Id"] = (rowCount + 1);
                dr["WeekId"] = ddlWeekDropDown.SelectedValue;
                dr["WeekName"] = ddlWeekDropDown.SelectedItem;
                dr["MaxSlote"] = txtmaxSlot.Text;
                dr["SloteName"] = txtSlotName.Text;
                dr["SunStartTimeHour"] = txtSunStartTimeHour.Text;
                dr["SunStartTimeMin"] = txtSunStartTimeMin.Text;
                dr["SunStartTimeTT"] = ddlSunStartTimeTT.SelectedItem;
                dr["SunEndTimeHour"] = txtSunEndTimeHour.Text;
                dr["SunEndTimeMin"] = txtSunEndTimeMin.Text;
                dr["SunEndTimeTT"] = ddlSunEndTimeTT.SelectedItem;
                dr["StartTime"] = txtSunStartTimeHour.Text + ":" + txtSunStartTimeMin.Text + ":" + ddlSunStartTimeTT.SelectedItem;
                dr["EndTime"] = txtSunEndTimeHour.Text + ":" + txtSunEndTimeMin.Text + ":" + ddlSunEndTimeTT.SelectedItem;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                GetListSloteDetails = dt;
            }
            grivewslote.DataSource = GetListSloteDetails;
            grivewslote.DataBind();
            ClearSloteControlValues();
            //ShowHideControl(VisibityType.SaveAndAdd);
        }

        protected void ibtn_SloteDelete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (grivewslote.DataKeys[rowindex]["Id"].ToString());
            GridViewRow gvRow = grivewslote.Rows[rowindex];

            DataTable dt = null;
            dt = GetListSloteDetails;
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
            GetListSloteDetails = dt;
            grivewslote.DataSource = GetListSloteDetails;
            grivewslote.DataBind();
            //ShowHideControl(VisibityType.Edit);
        }
        private void ClearSloteControlValues()
        {
            txtmaxSlot.Text = "";
            txtSlotName.Text = "";
            txtSunStartTimeHour.Text = "";
            txtSunStartTimeMin.Text = "";
            ddlSunStartTimeTT.SelectedIndex = 0;
            txtSunEndTimeHour.Text = "";
            txtSunEndTimeMin.Text = "";
            ddlSunEndTimeTT.SelectedIndex = 0;
            ddlWeekDropDown.SelectedIndex = 0;
        }
        #endregion
    }
}