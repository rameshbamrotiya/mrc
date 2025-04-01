using BAL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Hospital
{
    [ScriptService]
    public partial class Appointment : System.Web.UI.Page
    {
        public static string MobileNo;

        public static int RecId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strPass = Functions.Encrypt("Admin@123");
                //string strmessage = ConfigDetailsValue.SMSTextForAppointment;
                //string strTemplate = ConfigDetailsValue.SMSForAppointmentTemplateid;

                //string strLink = ConfigDetailsValue.StudentLogInLink;
                //string strMessage = (HttpUtility.UrlEncode(strmessage.Replace("{{PatientName}}", "Jalpa").Replace("{{var}}",  " 11:25 AM to 11:25 PM")));
                //string strTemplateId = strTemplate;
                //string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                //string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                //string senderid2 = ConfigDetailsValue.senderid2;
                //string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                //string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, "7069802910", strMessage, SMSAPI2, 0);

                ClearForm();
            }
        }


        private void BindControl()
        {

            using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
            {
                ddlSpecialization.Items.Clear();
                ddlSpecialization.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select Specialization"));
                foreach (var color in objEducationQualificationRepository.GetAllDepartmentForAppointment())
                {
                    ddlSpecialization.Items.Add(new System.Web.UI.WebControls.ListItem(color.DepartmentName.ToString(), ((int)color.Id).ToString()));
                }

                ddlVisitTYpe.Items.Clear();
                foreach (var color in objEducationQualificationRepository.GetAllVisitTypeForAppointment())
                {
                    ddlVisitTYpe.Items.Add(new System.Web.UI.WebControls.ListItem(color.VisitTypeName.ToString(), ((int)color.Id).ToString()));
                }
                //updated code
            }
        }

        [WebMethod]
        public static string[] GetHolidayList()
        {
            using (IHolidayMasterRepository objHolidayMasterRepository = new HolidayMasterRepository(Functions.strSqlConnectionString))
            {
                string[] strDateList = objHolidayMasterRepository.GetAllHolidayMaster().Select(x => Convert.ToDateTime(x.h_date).ToString("d-M-yyyy")).ToArray();//{ "9-4-2021", "20-4-2021" };
                return strDateList;
            }
        }

        protected void txtAppointmentDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParseExact(txtAppointmentDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                if (dt.Date < DateTime.Now.Date)
                {
                    Functions.MessagePopup(this, "Please Select Future Date", PopupMessageType.error);
                    ddlAppintmentTime.Enabled = false;
                    txtPatientName.Enabled = false;
                    txtMobile.Enabled = false;
                    txtEmail.Enabled = false;
                    txtReasonForVisit.Enabled = false;
                    ddlVisitTYpe.Enabled = false;
                    btnAppoinment.Enabled = false;
                    return;
                }
                else
                {
                    ddlAppintmentTime.Enabled = true;
                    txtPatientName.Enabled = true;
                    txtMobile.Enabled = true;
                    txtEmail.Enabled = true;
                    txtReasonForVisit.Enabled = true;
                    ddlVisitTYpe.Enabled = true;
                    btnAppoinment.Enabled = true;

                }
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    if (ddlSpecialization.SelectedIndex > 0)
                    {
                        if (ddlUnit.SelectedIndex > 0)
                        {
                            //if (hfDocId.Value.Length > 0)
                            {
                                ddlAppintmentTime.Items.Clear();
                                ddlAppintmentTime.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Time", ""));
                                if (ddlSpecialization.SelectedIndex > 0)
                                {
                                    var ListData = objEducationQualificationRepository.GetSlotListFromWeekNoForAppointment((int)dt.DayOfWeek, Convert.ToInt32(ddlUnit.SelectedValue), dt);
                                    {
                                        foreach (var slot in ListData)
                                        {
                                            string strTime = string.Concat(slot.StartTimeHour, ":", slot.StartTimeMin, " ", slot.StartTimeTT) + " to " + string.Concat(slot.EndTimeHour, ":", slot.EndTimeMin, " ", slot.EndTimeTT);
                                            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(strTime + (slot.SlotAvailability == "Available" ? "" : " " + slot.SlotAvailability), ((int)slot.Id).ToString());

                                            // If the slot is not available, disable it
                                            if (slot.SlotAvailability != "Available")
                                            {
                                                item.Attributes["disabled"] = "disabled";
                                            }
                                            ddlAppintmentTime.Items.Add(item);

                                            //ddlAppintmentTime.Items.Add(new ListItem(color.SloteName.ToString() + (color.SlotAvailability == "Available" ? "" : (" " + color.SlotAvailability)), ((int)color.Id).ToString(), (color.SlotAvailability == "Available")));
                                        }
                                        //updated code
                                    }
                                }
                            }
                            //else
                            //{
                            //    Functions.MessagePopup(this, "Please Select Doctor", PopupMessageType.error);
                            //}
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Select Unit", PopupMessageType.error);
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select proper date", PopupMessageType.error);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        [WebMethod]
        [HttpGet]
        public static List<int> GetEnabledDays(int unitID)
        {
            // Example: Fetch enabled weekdays from a database based on the unitId
            // Here, we just return some hardcoded values for demonstration
            List<int> enabledDays = new List<int>();

            using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
            {
                foreach (var color in objEducationQualificationRepository.GetWeekNoFromDeptIdForAppointment(unitID))
                {
                    enabledDays.Add((color.WeekNo.Value));
                }
                //updated code
            }
            return enabledDays;
        }

        protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
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
            dvFooter.InnerHtml = "";
            dvDoctorList.InnerHtml = "";
        }

        private void ClearForm()
        {
            txtAppointmentDate.Text = "";
            txtEmail.Text = "";
            txtPatientName.Text = "";
            txtMobile.Text = "";
            txtReasonForVisit.Text = "";
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Unit", ""));
            ddlUnit.SelectedIndex = 0;
            ddlVisitTYpe.SelectedIndex = 0;
            ddlSpecialization.SelectedIndex = 0;
            ddlAppintmentTime.Items.Clear();
            ddlAppintmentTime.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Time", ""));
            ddlAppintmentTime.SelectedIndex = 0;
            //hfDocId.Value = "";

            dvFooter.InnerHtml = "";
            dvDoctorList.InnerHtml = "";

            txtUDNId.Text = "";
            BindControl();

            if (ddlVisitTYpe.SelectedIndex == 0)
            {
                dvUDN.Visible = false;
            }
            else
            {
                dvUDN.Visible = true;
            }
            lblError.InnerHtml = "";
        }

        protected void btnAppoinment_Click(object sender, EventArgs e)
        {
            bool isError = false;
            bool isAvailable = false;
            string errorMessage = "";
            FrontAppointmentModel checkSloatAlreadyBookOrNotResult = new FrontAppointmentModel();
            if (!ValidateForm(ref checkSloatAlreadyBookOrNotResult, out errorMessage))
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    if (!objEducationQualificationRepository.InsertOrUpdateHospitalAppointment(checkSloatAlreadyBookOrNotResult, out errorMessage))
                    {
                        try
                        {

                            string strmessage = ConfigDetailsValue.SMSTextForAppointment;
                            string strTemplate = ConfigDetailsValue.SMSForAppointmentTemplateid;
                            //using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                            {
                                string strPatientName = txtPatientName.Text;
                                //if (strPatientName.Split(' ').Count() > 0)
                                //{
                                //    strPatientName = strPatientName.Split(' ')[0];
                                //}
                                //var sms = configDetailsRepository.GetSMSTemplateByName("Application Submitted SMS");
                                //if (sms != null)
                                {
                                    string strLink = ConfigDetailsValue.StudentLogInLink;
                                    string strMessage = ((strmessage.Replace("{{PatientName}}", strPatientName).Replace("{{var}}", txtAppointmentDate.Text+" "+ Convert.ToString(ddlAppintmentTime.SelectedItem.Text))));
                                    string strTemplateId = strTemplate;
                                    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                    string senderid2 = ConfigDetailsValue.senderid2;
                                    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, txtMobile.Text, strMessage, SMSAPI2, 0);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                        }
                        ClearForm();


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "<script> TosterMessage('" + errorMessage + "','" + PopupMessageType.success + "'); $(\".myModalAppointment\").click(); </script>", false);
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

        private bool ValidateForm(ref FrontAppointmentModel checkSloatAlreadyBookOrNotResult, out string errorMessage)
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
            if (!isError && !DateTime.TryParseExact(txtAppointmentDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                isError = true;
                errorMessage = "Select a valid Appointment Date";
            }
            if (dt.Date < DateTime.Now.Date)
            {
                isError = true;
                errorMessage = "Select a valid Appointment Date";
            }
                if (!isError && string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                isError = true;
                errorMessage = "enter Patient Name";
            }
            if (!isError && string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                isError = true;
                errorMessage = "enter Email";
            }
            if (!isError && string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                isError = true;
                errorMessage = "enter Email";
            }
            if (!isError && ddlAppintmentTime.SelectedIndex <= 0)
            {
                isError = true;
                errorMessage = "Select Appointment Time";
            }
            if (!isError && ddlVisitTYpe.SelectedIndex < 0)
            {
                isError = true;
                errorMessage = "Select Visit Type";
            }

            if (!isError && ddlVisitTYpe.SelectedIndex != 0)
            {
                if (!isError && string.IsNullOrWhiteSpace(txtUDNId.Text))
                {
                    isError = true;
                    errorMessage = "enter UDN Id";
                }
            }
            if (!isError)
            {
                checkSloatAlreadyBookOrNotResult.AppointmentDate = dt;
                checkSloatAlreadyBookOrNotResult.ReasonForVisit = txtReasonForVisit.Text;
                checkSloatAlreadyBookOrNotResult.EMail = txtEmail.Text;
                checkSloatAlreadyBookOrNotResult.PatientName = txtPatientName.Text;
                checkSloatAlreadyBookOrNotResult.MobileNo = txtMobile.Text;
                checkSloatAlreadyBookOrNotResult.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                checkSloatAlreadyBookOrNotResult.SlotId = Convert.ToInt32(ddlAppintmentTime.SelectedValue);
                checkSloatAlreadyBookOrNotResult.VisitTypeId = Convert.ToInt32(ddlVisitTYpe.SelectedValue);
                //checkSloatAlreadyBookOrNotResult.DoctorId = Convert.ToInt32(hfDocId.Value);
                checkSloatAlreadyBookOrNotResult.DoctorId =null;
                checkSloatAlreadyBookOrNotResult.UNMId = txtUDNId.Text;
            }
            return isError;
        }

        protected void ddlVisitTYpe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVisitTYpe.SelectedIndex == 0)
            {
                dvUDN.Visible = false;
            }
            else
            {
                dvUDN.Visible = true;
            }
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder strPopUpbuilde = new StringBuilder();
            StringBuilder strTabContent = new StringBuilder();

            if (ddlUnit.SelectedIndex > 0)
            {
                using (IFrontAppointmentRepository objEducationQualificationRepository = new FrontAppointmentRepository(Functions.strSqlConnectionString))
                {
                    long lgPopupCount = 0;
                    foreach (var color in objEducationQualificationRepository.GetAllDoctorBySlotIdDeptIdForAppointment(Convert.ToInt32(ddlSpecialization.SelectedValue), Convert.ToInt32(ddlUnit.SelectedValue)))
                    {
                        {
                            string strimagePath = (string.IsNullOrWhiteSpace(color.ImagePath) ? "" : "https://www.unmicrc.org" + (color.ImagePath).Replace("~", ""));
                            string strimagePathicon = ("https://www.unmicrc.org/Hospital/assets/img/specialities/specialities-04.png");

                            strTabContent.Append("<div class='card'>");
                            strTabContent.Append("	<div class='card-body'>");
                            strTabContent.Append("		<div class='doctor-widget'>");
                            strTabContent.Append("			<div class='doc-info-left'>");
                            strTabContent.Append("				<div class='doctor-img'>");
                            strTabContent.Append("					<a href='#'>");
                            strTabContent.Append("						<img src='" + strimagePath + "' class='img-fluid' alt='User Image'>");
                            strTabContent.Append("					</a>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("				<div class='doc-info-cont'>");
                            strTabContent.Append("					<h4 class='doc-name'><a href='#'>" + color.FacultyName + "</a></h4>");
                            strTabContent.Append("					<p class='doc-speciality'>" + HttpUtility.HtmlDecode(color.DesignationName) + "");
                            strTabContent.Append("						(" + color.DepartmentName + ")</p>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("			</div>");
                            strTabContent.Append("			<div class='doc-info-right'>");
                            strTabContent.Append("				<div class='clinic-booking'>");
                            strTabContent.Append("					<a class='view-pro-btn' data-toggle='modal' data-target='#exampleModalLong" + lgPopupCount + "' data-original-title=''>View Profile</a>");
                            //strTabContent.Append("					<a class='view-pro-btn bookAppointment' id='#bookApp" + lgPopupCount + "' data-original-title='" + color.Id + "' data-original-name='" + color.FacultyName + "-" + color.DepartmentName + $"({ddlUnit.SelectedItem.Text})" + "'>Book Appointment</a>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("			</div>");
                            strTabContent.Append("		</div>");
                            strTabContent.Append("	</div>");
                            strTabContent.Append("</div>");

                            #region PopUp Details


                            ExtraDetailsBAL objBAL = new ExtraDetailsBAL();
                            DataSet ds = objBAL.GetFacultyExtraDetails(Convert.ToInt32(color.FacultyId), 1);
                            System.Data.DataTable dtEducation = ds.Tables[0];
                            System.Data.DataTable dtExperience = ds.Tables[1];
                            System.Data.DataTable dtPublicationResearch = ds.Tables[2];
                            System.Data.DataTable dtAwards = ds.Tables[3];
                            System.Data.DataTable dtService = ds.Tables[4];

                            strPopUpbuilde.Append("");

                            strPopUpbuilde.Append("<!-- /Main Wrapper -->");
                            strPopUpbuilde.Append("<div class='modal fade' id='exampleModalLong" + lgPopupCount + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLong" + lgPopupCount + "'");
                            strPopUpbuilde.Append("	aria-hidden='true'>");
                            strPopUpbuilde.Append("	<div class='modal-dialog modal-lg' role='document'>");
                            strPopUpbuilde.Append("		<div class='modal-content'>");
                            strPopUpbuilde.Append("			<div class='modal-header'>");
                            strPopUpbuilde.Append("				<h5 class='modal-title' id='exampleModalLongTitle'>" + color.FacultyName + "</h5>");
                            strPopUpbuilde.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                            strPopUpbuilde.Append("					title=''><span aria-hidden='true'>×</span></button>");
                            strPopUpbuilde.Append("			</div>");
                            strPopUpbuilde.Append("			<div class='modal-body'>");
                            strPopUpbuilde.Append("				<div class='col-md-12 col-lg-12'>");
                            strPopUpbuilde.Append("					<div class='doc-info-left mb-15'>");
                            strPopUpbuilde.Append("						<div class='doctor-img'>");
                            strPopUpbuilde.Append("							<a href='#'>");
                            strPopUpbuilde.Append("								<img src='" + strimagePath + "' class='img-fluid'");
                            strPopUpbuilde.Append("									alt='User Image'>");
                            strPopUpbuilde.Append("							</a>");
                            strPopUpbuilde.Append("						</div>");
                            strPopUpbuilde.Append("						<div class='doc-info-cont'>");
                            strPopUpbuilde.Append("							<h4 class='doc-name'><a href='#'>" + color.FacultyName + "</a></h4>");
                            strPopUpbuilde.Append("							<h5 class='doc-department'><img src='" + strimagePathicon + "' class='img-fluid'");
                            strPopUpbuilde.Append("							<p class='doc-department'>" + (string.IsNullOrWhiteSpace(color.Email) ? "<i class='fas fa-envelope mr-15'></i> - </p>" : " <i class='fas fa-envelope mr-15'></i> <a href = '#'> " + color.Email + " </a> ") + "</p>");
                            strPopUpbuilde.Append("							<p class='doc-department'>" + (string.IsNullOrWhiteSpace(color.MobileNumber) ? "<i class='fas fa-phone mr-15'></i> - </p>" : " <i class='fas fa-phone mr-15'></i> <a href = '#'> " + color.MobileNumber + " </a> ") + "</p>");
                            strPopUpbuilde.Append("						</div>");
                            strPopUpbuilde.Append("					</div>");
                            strPopUpbuilde.Append("					<!-- About Details -->");
                            strPopUpbuilde.Append("					<div class='widget about-widget'>");
                            strPopUpbuilde.Append("						<h4 class='widget-title'>About Me</h4>");
                            strPopUpbuilde.Append("						" + color.FacultyDescription + "");
                            strPopUpbuilde.Append("					</div>");
                            strPopUpbuilde.Append("					<!-- /About Details -->");
                            if (dtEducation != null)
                            {
                                if (dtEducation.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Education Details -->");
                                    strPopUpbuilde.Append("					<div class='widget education-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Education</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtEducation.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["EducationName"].ToString() + "</a>");
                                        strPopUpbuilde.Append("											<div>" + row["DegreeName"].ToString() + "</div>");
                                        if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if (string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + "</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Education Details -->");
                                }
                            }

                            if (dtExperience != null)
                            {
                                if (dtExperience.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Experience Details -->");
                                    strPopUpbuilde.Append("					<div class='widget experience-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Work &amp; Experience</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtExperience.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["EmployerName"].ToString() + "</a>");
                                        if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if (string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + "</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Experience Details -->");
                                }
                            }

                            if (dtPublicationResearch != null)
                            {
                                if (dtPublicationResearch.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Publication Research Details -->");
                                    strPopUpbuilde.Append("					<div class='widget experience-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Research</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtPublicationResearch.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["Description"].ToString() + "</a>");
                                        if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if (string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + "</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Publication Research Details -->");
                                }
                            }

                            if (dtAwards != null)
                            {
                                if (dtAwards.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Awards Details -->");
                                    strPopUpbuilde.Append("					<div class='widget awards-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Awards</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtAwards.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<p class='exp-year'>" + row["Month"].ToString() + " - " + row["Year"].ToString() + "</p>");
                                        strPopUpbuilde.Append("											<h4 class='exp-title'>" + row["Title"].ToString() + "</h4>");
                                        strPopUpbuilde.Append("											" + HttpUtility.HtmlDecode(row["AwardsDescription"].ToString()) + "");
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Awards Details -->");
                                }
                            }

                            if (dtService != null)
                            {
                                if (dtService.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Services List -->");
                                    strPopUpbuilde.Append("					<div class='widget awards-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Services</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtService.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<h4 class='exp-title'>" + row["ServiceName"].ToString() + "</h4>");
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Services List -->");
                                }
                            }


                            strPopUpbuilde.Append("				</div>");
                            strPopUpbuilde.Append("			</div>");
                            strPopUpbuilde.Append("		</div>");
                            strPopUpbuilde.Append("	</div>");
                            strPopUpbuilde.Append("</div>");
                            #endregion
                        }

                        //ddlDoctor.Items.Add(new System.Web.UI.WebControls.ListItem(color.FacultyName.ToString(), ((int)color.Id).ToString()));
                        lgPopupCount++;
                    }


                    dvFooter.InnerHtml = strPopUpbuilde.ToString();
                    dvDoctorList.InnerHtml = strTabContent.ToString();
                    //updated code
                }
            }
            else
            {

                dvFooter.InnerHtml = strPopUpbuilde.ToString();
                dvDoctorList.InnerHtml = strTabContent.ToString();
            }
        }

    }
}