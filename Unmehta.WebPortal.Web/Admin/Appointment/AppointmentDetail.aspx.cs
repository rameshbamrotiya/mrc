using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using ClosedXML.Excel;
using System.IO;
using System.Data;
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
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;
using System.ServiceModel;
using Unmehta.WebPortal.Common;
using Org.BouncyCastle.Asn1.Cmp;

namespace Unmehta.WebPortal.Web.Admin.Appointment
{
    public partial class AppointmentDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal");
                }

                SessionUserModel sessionModel = new SessionUserModel();
                sessionModel = SessionWrapper.UserDetails;
                if (((ConfigDetailsValue.DoctorRoleId == SessionWrapper.UserDetails.RoleId.ToString() ) || ConfigDetailsValue.DoctorUnit == SessionWrapper.UserDetails.RoleId.ToString()))
                {
                    using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                    {
                        sessionModel.DoctorId = (int)objEducationQualificationRepository.GetDoctorIdByUsrId((int)SessionWrapper.UserDetails.Id).DoctorId;
                    }
                }
                SessionWrapper.UserDetails = sessionModel;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            if (!Page.IsPostBack)
            {
                //BindGridView();
                ClearControlValues();

            }
        }



        //private void BindGridView()
        //{
        //    gvApponmentList.DataBind();
        //}
        //private void BindGridView()
        //{
        //    AccredationMasterBO objBo = new AccredationMasterBO();
        //    objBo.Acc_id = Convert.ToInt32(hfId.Value);
        //    objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
        //    AccredationMasterBAL objbal = new AccredationMasterBAL();
        //    DataSet ds = objbal.SelectAccredationDetails(objBo);
        //    DataTable dt = ds.Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        dt.Rows[0]["Accredation_Name"] = (dt.Rows[0]["Accredation_Name"].ToString());
        //        dt.AcceptChanges();
        //        gView.DataSource = dt;
        //        gView.DataBind();
        //    }
        //    else
        //    {
        //        gView.DataBind();
        //    }
        //}
        private void ClearControlValues()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ddlSpecialization.SelectedIndex = 0;
            BindDepartmentByLangId();
            BindDoctorDropDown();
            BindUnitsByLangId();

            if ((ConfigDetailsValue.DoctorRoleId == SessionWrapper.UserDetails.RoleId.ToString() ) || ConfigDetailsValue.DoctorUnit == SessionWrapper.UserDetails.RoleId.ToString() )
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    dvMain.Visible = false;
                }
            }
            else
            {
                dvMain.Visible = true;
            }

            ddlUnit.SelectedValue = "0";
            ddlDoctorList.SelectedValue = "0";
            ddlSpecialization.SelectedValue = "0";

            ddlUnit.SelectedIndex = 0;
            ddlDoctorList.SelectedIndex = 0;
            ddlSpecialization.SelectedIndex = 0;

            btnExport.Visible = false;
        }
        private void BindDoctorDropDown(long AccId = 0)
        {

            ddlDoctorList.Items.Clear();
            ddlDoctorList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Doctor", ""));

            if ((ConfigDetailsValue.DoctorRoleId == SessionWrapper.UserDetails.RoleId.ToString() || ConfigDetailsValue.DoctorUnit == SessionWrapper.UserDetails.RoleId.ToString() ))
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    ddlDoctorList.Items.Add(new System.Web.UI.WebControls.ListItem(SessionWrapper.UserDetails.FirstName, Convert.ToString(SessionWrapper.UserDetails.DoctorId)));
                    ddlDoctorList.SelectedValue=Convert.ToString(SessionWrapper.UserDetails.DoctorId);
                }
            }
            else
            {
                if (ddlUnit.SelectedIndex > 0)
                {
                    using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                    {
                        long lgPopupCount = 0;
                        foreach (var color in objEducationQualificationRepository.GetAllDoctorBySlotIdDeptIdForAppointment(Convert.ToInt32(ddlSpecialization.SelectedValue), Convert.ToInt32(ddlUnit.SelectedValue)))
                        {
                            ddlDoctorList.Items.Add(new System.Web.UI.WebControls.ListItem(color.FacultyName.ToString(), ((int)color.Id).ToString()));
                        }
                    }
                }
            }

        }



        private void BindUnitsByLangId()
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Unit", ""));
            if (ddlSpecialization.SelectedIndex > 0)
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    foreach (var color in objEducationQualificationRepository.GetAllUnitByDeptIdForAppointment(Convert.ToInt32(ddlSpecialization.SelectedValue)))
                    {
                        ddlUnit.Items.Add(new System.Web.UI.WebControls.ListItem(color.UnitName.ToString(), ((int)color.Id).ToString()));
                    }
                    //updated code
                }
            }
        }

        private void BindDepartmentByLangId()
        {
            using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
            {
                ddlSpecialization.Items.Clear();
                ddlSpecialization.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select Specialization"));
                foreach (var color in objEducationQualificationRepository.GetAllDepartmentForAppointment())
                {
                    ddlSpecialization.Items.Add(new System.Web.UI.WebControls.ListItem(color.DepartmentName.ToString(), ((int)color.Id).ToString()));
                }

            }

        }
        public string  convertdate(string sDate)
        {
            //sDate = "21/04/1990";
            string stringDate = "";

            DateTime dt;
            if (DateTime.TryParseExact(sDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                stringDate = dt.ToString("yyyy-MM-dd");

            }
            return stringDate;
        }

        public void BindGrid()
        {
            DataTable data1 = new DataTable();
            AppoinmentDetaiBO objBo = new AppoinmentDetaiBO();
            if (ddlUnit.SelectedIndex > 0)
            {
                objBo.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            //else
            //{
            //    Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
            //    return;
            //}


            if ((ConfigDetailsValue.DoctorRoleId == SessionWrapper.UserDetails.RoleId.ToString() ))
            {
                objBo.DoctorId = Convert.ToInt32(SessionWrapper.UserDetails.DoctorId);
            }
            else if ((ConfigDetailsValue.DoctorUnit == SessionWrapper.UserDetails.RoleId.ToString() ))
            {
                objBo.UnitId = Convert.ToInt32(SessionWrapper.UserDetails.DoctorId);
            }
            else
            {
                if (ddlDoctorList.SelectedIndex > 0)
                {
                    objBo.DoctorId = Convert.ToInt32(ddlDoctorList.SelectedValue);
                }
            }
            if (ddlSpecialization.SelectedIndex > 0)
            {
                objBo.deptid = Convert.ToInt32(ddlSpecialization.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                String DateTyp = convertdate(txtFromDate.Text.Trim());
                objBo.FromDate = DateTyp;// txtFromDate.Text;
            }
            else { objBo.FromDate = ""; }
            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                String DateTyp = convertdate(txtToDate.Text.Trim());
                objBo.ToDate = DateTyp;
            }
            else { objBo.ToDate = ""; }


            ConfigUnitBAL objBAL = new ConfigUnitBAL();

            DataSet ds = objBAL.SelectAppoinmentRecord(objBo);

            data1 = ds.Tables[0];
            if (data1.Rows.Count > 0)
            {
                lblCount.Text = "Total No Of Candidate : " + Convert.ToString(data1.Rows.Count);
                gView.DataSourceID = string.Empty;
                gView.DataSource = data1;
                gView.DataBind();
                btnExport.Visible = true;
            }


        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            AppoinmentDetaiBO objBo = new AppoinmentDetaiBO();
            if (ddlUnit.SelectedIndex > 0)
            {
                objBo.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            else { objBo.UnitId = 0; }
            //else
            //{
            //    Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
            //    return;
            //}
            if (ddlDoctorList.SelectedIndex > 0)
            {
                objBo.DoctorId = Convert.ToInt32(ddlDoctorList.SelectedValue);
            }
            else { objBo.DoctorId = 0; }
            if (ddlSpecialization.SelectedIndex > 0)
            {
                objBo.deptid = Convert.ToInt32(ddlSpecialization.SelectedValue);
            }
            else { objBo.deptid = 0; }

            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                String DateTyp1 = convertdate(txtFromDate.Text.Trim());
                objBo.FromDate = DateTyp1;// txtFromDate.Text;
            }
            else { objBo.FromDate = ""; }
            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                String DateTyp2 = convertdate(txtToDate.Text.Trim());
                objBo.ToDate = DateTyp2;
            }
            else { objBo.ToDate = ""; }



            ConfigUnitBAL objBAL = new ConfigUnitBAL();

            DataSet ds = objBAL.SelectAppoinmentRecord(objBo);
            DataTable data = new DataTable();
            data = ds.Tables[0];
            if (data.Rows.Count > 0)
            {

                //  DataTable dt = Functions.ToDataTable(data);
                DataTable dt = data;

                dt.Columns.Remove("Id");
                dt.Columns.Remove("UnitId");
                dt.Columns.Remove("DoctorId");
                dt.Columns.Remove("AppointmentDate");
                dt.Columns.Remove("VisitTypeId");
                dt.Columns.Remove("SlotId");
                dt.Columns.Remove("IsDelete");
                dt.Columns.Remove("CreateBy");
                dt.Columns.Remove("CreateDate");
                dt.Columns.Remove("UpdateBy");
                dt.Columns.Remove("UpdateDate");
                dt.Columns.Remove("DeleteBy");
                dt.Columns.Remove("DeleteDate");
                dt.Columns.Remove("deptid");
                //dt.Columns.Remove("ToDate");
                //dt.Columns.Remove("FromDate");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Appoinment_List");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=DataTableToExcelExport.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

        }
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {

            try
            {
                ddlUnit.SelectedIndex = 0;
                ddlDoctorList.SelectedIndex = 0;
                ddlSpecialization.SelectedIndex = 0;
                lblCount.Text = "Total Appointment : " + Convert.ToString(0);
                gView.DataSourceID = string.Empty;
                gView.DataSource = null;
                gView.DataBind();
                btnExport.Visible = false;
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDoctorDropDown();
        }

        protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUnitsByLangId();
        }


        protected void lnkVisit_Click1(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int PatientId = Convert.ToInt32(gView.DataKeys[rowindex]["Id"].ToString());

            try
            {

                ConfigUnitBAL objBAL = new ConfigUnitBAL();
                if (PatientId != null)
                {

                    if (objBAL.UpdateAppoinmentRecord(PatientId, SessionWrapper.UserDetails.Id.ToString()))
                    {
                        BindGrid();
                        Functions.MessagePopup(this, "Appointment Status Updated Successfully", PopupMessageType.success);
                    }
                }
                else
                {

                    Functions.MessagePopup(this, "Record Not Found", PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.ToString(), PopupMessageType.success);

            }

        }
    }
}