using BAL;
using BO;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class OPDTimingsMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
                ViewState["Tunitdoctor"] = null;
                LanguageMasterBAL objBo = new LanguageMasterBAL();
                ddlLanguage.DataSource = objBo.GetAllLanguage();
                ddlLanguage.DataTextField = "Name";
                ddlLanguage.DataValueField = "Id";
                ddlLanguage.DataBind();
                BindDepartmentByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
                FillCountry(Convert.ToInt32(ddlLanguage.SelectedValue));
                Fillunitdropdown();
            }
        }
        private void BindDepartmentByLangId(int LangId)
        {
            ddlDepartment.Items.Clear();
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {
                ddlDepartment.DataSource = objDepartmentRepository.GetAllTblDepartment(LangId);
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }
        private void FillCountry(int LangId)
        {
            DataSet ds = new DataSet();
            DataSet dsunit = new DataSet();
            OPDTimingsBAL objBAL = new OPDTimingsBAL();
            OPDTimingsBO objbo = new OPDTimingsBO();
            objbo.LanguageId = LangId;
            ds = objBAL.SelectDoctorListByLanguage(objbo);
            dsunit = objBAL.SelectUnitListByLanguage(objbo);
            DataTable dtDoctor = ds.Tables[0];
            DataTable dtunit = dsunit.Tables[0];
            PopulateDropDownList(ddlDoctorList, dtDoctor, "DoctorName", "Id", true);
            PopulateDropDownList(ddlunitdoctor, dtDoctor, "DoctorName", "Id", true);
            PopulateDropDownList(ddlunit, dtunit, "UnitName", "Id", true);
        }
        private void Fillunitdropdown()
        {
            ddlWeekDropDown.Items.Clear();
            Array colors = Enum.GetValues(typeof(DayOfWeek));
            ddlWeekDropDown.Items.Add(new ListItem("Select Week Day", null));
            foreach (DayOfWeek color in colors)
            {
                ddlWeekDropDown.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
        }

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region GridView Operation
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["OPD_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        OPDTimingsBO objBo = new OPDTimingsBO();
                        objBo.OPD_id = bytID;
                        new OPDTimingsBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            gvDoctor.Visible = true;
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            OPDTimingsBO objBo = new OPDTimingsBO();
            objBo.OPD_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new OPDTimingsBAL().SelectRecord(objBo);
            DataSet DsDoctor = new OPDTimingsBAL().SelectRecordDoctor(objBo);
            DataSet DsUnit = new OPDTimingsBAL().SelectRecordUnit(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtOPDName.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            gvDoctor.Columns[1].Visible = true;
            gvDoctor.DataSource = DsDoctor.Tables[0];
            gvDoctor.DataBind();
            gvDoctor.Columns[1].Visible = false;
            gviewunitdoctor.Columns[1].Visible = true;
            gviewunitdoctor.DataSource = DsUnit.Tables[0];
            gviewunitdoctor.DataBind();
            gviewunitdoctor.Columns[1].Visible = false;
            ViewState["T017PDetails"] = DsDoctor.Tables[0];
            ViewState["Tunitdoctor"] = DsUnit.Tables[0];
            if (dr["OPD_name"] != DBNull.Value)
                txtOPDName.Text = dr["OPD_name"].ToString();
            if (dr["StartTimeHH"] != DBNull.Value)
                txtSunStartTimeHour.Text = dr["StartTimeHH"].ToString();
            if (dr["StartTimeMM"] != DBNull.Value)
                txtSunStartTimeMin.Text = dr["StartTimeMM"].ToString();
            if (dr["StartTimeTT"] != DBNull.Value)
                ddlSunStartTimeTT.SelectedValue = dr["StartTimeTT"].ToString();
            if (dr["EndTimeHH"] != DBNull.Value)
                txtSunEndTimeHour.Text = dr["EndTimeHH"].ToString();
            if (dr["EndTimeMM"] != DBNull.Value)
                txtSunEndTimeMin.Text = dr["EndTimeMM"].ToString();
            if (dr["EndTimeTT"] != DBNull.Value)
                ddlSunEndTimeTT.SelectedValue = dr["EndTimeTT"].ToString();
            if (dr["Department_id"] != DBNull.Value)
                ddlDepartment.SelectedValue = dr["Department_id"].ToString();
            if (dr["UnitId"] != DBNull.Value)
                ddlunit.SelectedValue = dr["UnitId"].ToString();
            if (dr["Week"] != DBNull.Value)
                ddlWeekDropDown.SelectedValue = dr["Week"].ToString();
            if (dr["Department_id"] != DBNull.Value)
                ddlDepartment.SelectedValue = dr["Department_id"].ToString();
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInActive.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(OPDTimingsBO objBo)
        {
            if (!string.IsNullOrEmpty(txtOPDName.Text))
                objBo.OPDName = txtOPDName.Text;
            if (!string.IsNullOrEmpty(txtSunStartTimeHour.Text))
                objBo.StartTimeHH = txtSunStartTimeHour.Text;
            if (!string.IsNullOrEmpty(txtSunStartTimeMin.Text))
                objBo.StartTimeMM = txtSunStartTimeMin.Text;
            objBo.StartTimeTT = ddlSunStartTimeTT.SelectedValue;
            if (!string.IsNullOrEmpty(txtSunEndTimeHour.Text))
                objBo.EndTimeHH = txtSunEndTimeHour.Text;
            if (!string.IsNullOrEmpty(txtSunEndTimeMin.Text))
                objBo.EndTimeMM = txtSunEndTimeMin.Text;
            objBo.EndTimeTT = ddlSunEndTimeTT.SelectedValue;
            objBo.UnitId = ddlunit.SelectedValue;
            objBo.UnitName = ddlunit.SelectedItem.Text;
            objBo.Week = ddlWeekDropDown.SelectedValue;
            objBo.DepartmentId = ddlDepartment.SelectedValue;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.IsActive = ddlActiveInActive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["T017PDetails"] = null;
                ViewState["Tunitdoctor"] = null;
                ShowHideControl(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            if (gvDoctor.Rows.Count > 0)
            {
                dt.Columns.Add("Doctor_Name");
                dt.Columns.Add("Doctor_Id");
                foreach (GridViewRow row in gvDoctor.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["Doctor_Name"] = row.Cells[2].Text;
                    dr["Doctor_Id"] = row.Cells[1].Text;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        private DataTable GetGridViewDataUnit()
        {
            DataTable dt = new DataTable();
            if (gviewunitdoctor.Rows.Count > 0)
            {

                dt.Columns.Add("Doctor_id");
                dt.Columns.Add("DoctorName");
                foreach (GridViewRow row in gviewunitdoctor.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["Doctor_id"] = row.Cells[1].Text;
                    dr["DoctorName"] = row.Cells[2].Text;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

            }
            return dt;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                OPDTimingsBO objBo = new OPDTimingsBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                DataTable dtunit = new DataTable();
                dtunit = GetGridViewDataUnit();
                if (dt.Rows.Count > 0 && dtunit.Rows.Count > 0)
                {
                    if (new OPDTimingsBAL().InsertRecord(objBo, dt, dtunit))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        ViewState["T017PDetails"] = null;
                        ViewState["Tunitdoctor"] = null;
                        gviewunitdoctor.DataSource = (DataTable)ViewState["Tunitdoctor"];
                        gviewunitdoctor.DataBind();
                        gvDoctor.DataSource = (DataTable)ViewState["T017PDetails"];
                        gvDoctor.DataBind();
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                        return;
                    }
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
                else
                {
                    Functions.MessagePopup(this, "Please Add Doctor and UnitDoctor.", PopupMessageType.warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                OPDTimingsBO objBo = new OPDTimingsBO();
                LoadControls(objBo);
                objBo.id = Convert.ToInt32(ViewState["PKImg"]);
                objBo.OPD_id = Convert.ToInt32(ViewState["PK"]);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                DataTable dtunit = new DataTable();
                dtunit = GetGridViewDataUnit();
                if (dt.Rows.Count > 0 && dtunit.Rows.Count > 0)
                {
                    if (new OPDTimingsBAL().UpdateRecord(objBo, dt, dtunit))
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
                else
                {
                    Functions.MessagePopup(this, "Please Add Doctor and UnitDoctor.", PopupMessageType.warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
                ViewState["Tunitdoctor"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region ShowHideControl || Notification
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    gvDoctor.Visible = false;
                    ClearControlValues(pnlEntry);
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

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            gvDoctor.Visible = true;
            DataTable dt = null;
            if (ViewState["T017PDetails"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("Doctor_Id");
                dt.Columns.Add("DoctorName");
            }
            else
            {
                dt = (DataTable)(ViewState["T017PDetails"]);
            }
            DataRow dr = dt.NewRow();
            dr["Doctor_Id"] = ddlDoctorList.SelectedValue;
            dr["DoctorName"] = ddlDoctorList.SelectedItem;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            gvDoctor.Columns[1].Visible = true;
            gvDoctor.DataSource = dt;
            gvDoctor.DataBind();
            gvDoctor.Columns[1].Visible = false;
            ViewState["T017PDetails"] = dt;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(gvDoctor.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["T017PDetails"] as DataTable;
                        dt.Rows[index].Delete();
                        ViewState["T017PDetails"] = dt;
                        gvDoctor.DataSource = dt;
                        gvDoctor.DataBind();
                    }
                    else
                    {
                        Int32 bytID;
                        bytID = Convert.ToInt32(gvDoctor.DataKeys[intIndex].Values["id"]);
                        if (e.CommandName == "eDelete")
                        {
                            OPDTimingsBO objBo = new OPDTimingsBO();
                            DataTable dt = ViewState["T017PDetails"] as DataTable;
                            dt.Rows[intIndex].Delete();
                            ViewState["T017PDetails"] = dt;
                            objBo.OPD_id = bytID;
                            new OPDTimingsBAL().DeleteRecordDoctor(objBo);
                            BindGridView();
                            gvDoctor.DataBind();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        ClearControlValues(pnlEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
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
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void AddUnitDoctor_ServerClick(object sender, EventArgs e)
        {
            gviewunitdoctor.Visible = true;
            DataTable dt = null;
            if (ViewState["Tunitdoctor"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("Doctor_id");
                dt.Columns.Add("DoctorName");
            }
            else
            {
                dt = (DataTable)(ViewState["Tunitdoctor"]);
            }
            DataRow dr = dt.NewRow();
            dr["Doctor_id"] = ddlunitdoctor.SelectedValue;
            dr["DoctorName"] = ddlunitdoctor.SelectedItem;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            gviewunitdoctor.Columns[1].Visible = true;
            gviewunitdoctor.DataSource = dt;
            gviewunitdoctor.DataBind();
            gviewunitdoctor.Columns[1].Visible = false;
            ViewState["Tunitdoctor"] = dt;
        }

        protected void gviewunitdoctor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(gviewunitdoctor.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["Tunitdoctor"] as DataTable;
                        dt.Rows[index].Delete();
                        ViewState["Tunitdoctor"] = dt;
                        gviewunitdoctor.DataSource = dt;
                        gviewunitdoctor.DataBind();
                    }
                    else
                    {
                        Int32 bytID;
                        bytID = Convert.ToInt32(gviewunitdoctor.DataKeys[intIndex].Values["id"]);
                        if (e.CommandName == "eDelete")
                        {
                            OPDTimingsBO objBo = new OPDTimingsBO();
                            DataTable dt = ViewState["Tunitdoctor"] as DataTable;
                            dt.Rows[intIndex].Delete();
                            ViewState["Tunitdoctor"] = dt;
                            objBo.OPD_id = bytID;
                            new OPDTimingsBAL().DeleteRecordUnit(objBo);
                            BindGridView();
                            gviewunitdoctor.DataBind();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        ClearControlValues(pnlEntry);
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