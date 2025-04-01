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

namespace Unmehta.WebPortal.Web.Admin.Appointment
{
    public partial class DoctorRegisterMaster : System.Web.UI.Page
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
                //BindGridView();
                ClearControlValues();

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

        private void BindDoctorDropDown(long AccId = 0)
        {

            ddlDoctorList.Items.Clear();
            ddlDoctorList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Doctor", ""));

            if (ConfigDetailsValue.DoctorRoleId == SessionWrapper.UserDetails.RoleId.ToString())
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    ddlDoctorList.Items.Add(new System.Web.UI.WebControls.ListItem(SessionWrapper.UserDetails.FirstName, Convert.ToString(SessionWrapper.UserDetails.DoctorId)));
                    dvMain.Visible = false;
                    ddlDoctorList.SelectedValue = Convert.ToString(SessionWrapper.UserDetails.DoctorId);
                }
            }
            else
            {
                dvMain.Visible = true;
            }
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

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDoctorDropDown();
        }

        protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUnitsByLangId();
        }

        public void ClearControlValues() 
        {
            BindDepartmentByLangId();
            BindUnitsByLangId();
            BindDoctorDropDown();
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
        }

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            bool isError = false;
            bool isAvailable = false;
            string errorMessage = "";
            DoctorRegisterModel doctorRegisteerMaster = new DoctorRegisterModel();
            if (!ValidateForm(ref doctorRegisteerMaster, out errorMessage))
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    if (!objEducationQualificationRepository.InsertOrUpdateUserWithFacultyDetails(doctorRegisteerMaster, out errorMessage))
                    {
                        ClearControlValues();


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "<script> TosterMessage('" + errorMessage + "','" + PopupMessageType.success + "'); </script>", false);
                        //Functions.MessagePopup(this, errorMessage, PopupMessageType.success);

                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
            }
        }

        private bool ValidateForm(ref DoctorRegisterModel checkSloatAlreadyBookOrNotResult, out string errorMessage)
        {
            bool isError = false;
            errorMessage = "";
            DateTime dt = DateTime.Now;
            if (ddlSpecialization.SelectedIndex <= 0)
            {
                isError = true;
                errorMessage = "Select Specialization";
            }
            if (!isError && ddlUnit.SelectedIndex <= 0)
            {
                isError = true;
                errorMessage = "Select Unit";
            }
            if (!isError && ddlDoctorList.SelectedIndex <= 0)
            {
                isError = true;
                errorMessage = "Select Doctor";
            }
            if (!isError && string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                isError = true;
                errorMessage = "enter User Name";
            }
            if (!isError && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                isError = true;
                errorMessage = "enter Password";
            }
            if (!isError && string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                isError = true;
                errorMessage = "enter Confirm password";
            }
            if (!isError)
            {
                checkSloatAlreadyBookOrNotResult.UserName = txtUserName.Text;
                checkSloatAlreadyBookOrNotResult.Password =Functions.Encrypt( txtPassword.Text);
                checkSloatAlreadyBookOrNotResult.Id = Convert.ToInt32(ddlDoctorList.SelectedValue);
                checkSloatAlreadyBookOrNotResult.RoleId = Convert.ToInt32(ConfigDetailsValue.DoctorRoleId);
                checkSloatAlreadyBookOrNotResult.ChangeBy = SessionWrapper.UserDetails.Id.ToString();
            }
            return isError;
        }
    }
}