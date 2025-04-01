using BulkSMSApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Unmehta.WebPortal.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Hospital
{
    public partial class MobileOTP : System.Web.UI.Page
    {
        public static string Mobile;
        public static int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    dvOTPEnter.Visible = false;
                if (!string.IsNullOrWhiteSpace(Appointment.MobileNo) && Appointment.RecId!=null)
                {
                    Id = Appointment.RecId;
                    txtMobileNo.Text = Appointment.MobileNo;
                    txtMobileNo.ReadOnly = true;
                }
                else
                {
                    txtMobileNo.ReadOnly = false;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (IOtpMobileRepository objData = new OtpMobileRepository(Functions.strSqlConnectionString))
            {
                try
                {
                    string strError = "";
                    var objDataDetail = objData.GetOptByMobileNo(txtMobileNo.Text,ConfigDetailsValue.SMSOTPExpire);
                    if (objDataDetail!=null)
                    {
                        if(objDataDetail.OTPNo==txtOTP.Text)
                        {
                            using (IHospitalAppointmentRepository objData1= new HospitalAppointmentRepository(Functions.strSqlConnectionString))
                            {
                                if(!objData1.UpdateAppointmentOtpStatus(Id, true, out strError))
                                {
                                    Mobile = txtMobileNo.Text;
                                    Response.Redirect("~/Hospital/AppointmentDetails.aspx");
                                }
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Not Match OTP please Try Again", PopupMessageType.error);
                            //lblError.InnerText = "Not Match OTP please Try Again";
                            //lblError.Style.Add("color", "red");
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, "OTP Expired please Try Again", PopupMessageType.error);
                        //lblError.InnerText = "OTP Expired please Try Again";
                        //lblError.Style.Add("color", "red");
                        //btnSendOTP.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }
            }
        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IOtpMobileRepository objData = new OtpMobileRepository(Functions.strSqlConnectionString))
            {
                try
                {
                    string strOTP= Functions.GetRandomNumberString();
                    if (!objData.InsertOrUpdateOtpMobileMaster(new CMSMobileOtpManage { MobileNo = txtMobileNo.Text, OTPNo = strOTP }, out errorMessage))
                    {

                        string ResponseStatus = SMSHttpPostClient.sendUnicodeSMS(ConfigDetailsValue.SMSSenderId,ConfigDetailsValue.SMSTemplateid, txtMobileNo.Text, "Your Appointment OTP is "+ strOTP+ " this OTP expired after 15 minutes");

                        if (ResponseStatus.Contains("402")) // Sent SMS Sucess
                        {
                            lblError.InnerText = errorMessage;
                            lblError.Style.Add("color", "green");
                            btnSendOTP.Visible = false;
                            dvOTPEnter.Visible = true;
                        }
                        else
                        {
                            lblError.InnerText = "Unable to Send OTP try after Some Time...";
                            lblError.Style.Add("color", "red");
                        }
                        //Response.Redirect("~/Hospital/MobileOTP.aspx");
                    }
                    else
                    {
                        lblError.InnerText = errorMessage;
                        lblError.Style.Add("color", "red");
                    }
                }
                catch(Exception ex)
                {
                    lblError.InnerText = ex.ToString();
                    lblError.Style.Add("color", "red");
                }
            }
        }
    }
}