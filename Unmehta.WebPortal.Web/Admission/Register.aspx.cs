using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class Register : System.Web.UI.Page
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDropDown();
                FillCapctha();
                //TempSMS("BIMAL S","gotawalabimal39@gmail.com","8866639713", "Testing");
            }
        }

        private void TempSMS(string FirstName,string Email, string Mobile, string Password)
        {
            string strError = "";
            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
            {


                string strBody = "Dear " + FirstName + " " + FirstName.ToString() + "," +
                            "<br/><br/> Username : " + Mobile +
                            "<br/><br/> Password : " +  Password + "." +
                            "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                            "<br/><br/> Regards," +
                            "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                try
                {
                    ErrorLogger.ERROR("\r\n Email", Email, this);
                    ErrorLogger.ERROR("\r\n Subject", "Registration Email", this);
                    ErrorLogger.ERROR("\r\n strBody", strBody, this);

                    string smtpserver = "";
                    string smtpPassword = "";
                    string fromemail = "";
                    string smtpaccount = "";
                    string smtpPort = "";
                    string smtpIsSecure = "";

                    if (ConfigDetailsValue.SMTPIsTest != "1")
                    {
                        smtpserver = ConfigDetailsValue.SMTPServer;
                        smtpPassword = ConfigDetailsValue.SMTPPassword;
                        fromemail = ConfigDetailsValue.SMTPFromEmail;
                        smtpaccount = ConfigDetailsValue.SMTPAccount;
                        smtpPort = ConfigDetailsValue.SMTPPort;
                        smtpIsSecure = ConfigDetailsValue.SMTPIsSecure;
                    }
                    else
                    {
                        smtpserver = ConfigDetailsValue.TestSMTPServer;
                        smtpPassword = ConfigDetailsValue.TestSMTPPassword;
                        fromemail = ConfigDetailsValue.TestSMTPFromEmail;
                        smtpaccount = ConfigDetailsValue.TestSMTPAccount;
                        smtpPort = ConfigDetailsValue.TestSMTPPort;
                        smtpIsSecure = ConfigDetailsValue.TestSMTPIsSecure;
                    }

                    ErrorLogger.ERROR("\r\n smtpserver", smtpserver, this);
                    ErrorLogger.ERROR("\r\n smtpPassword", smtpPassword, this);
                    ErrorLogger.ERROR("\r\n fromemail", fromemail, this);
                    ErrorLogger.ERROR("\r\n smtpaccount", smtpaccount, this);
                    ErrorLogger.ERROR("\r\n smtpPort", smtpPort, this);
                    ErrorLogger.ERROR("\r\n smtpIsSecure", smtpIsSecure, this);

                    Functions.MessagePopup(this, "Registration successful! Your username and password has been to email and mobile no", PopupMessageType.success);

                    if (Functions.SendEmail(Email, "Registration Email", strBody, out strError, true))
                    {


                        var sms = configDetailsRepository.GetSMSTemplateByName("Registration SMS");
                        if (sms != null)
                        {
                            string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{UserID}}", Mobile).Replace("{{Password}}", Password), Encoding.UTF8).Replace("+", "%20"));
                            string strTemplateId = sms.SMSTemplateId;

                            string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                            string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                            string senderid2 = ConfigDetailsValue.senderid2;
                            string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                            string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, Mobile, strMessage, SMSAPI2, 0);
                        }

                        string url = ResolveUrl("~/Admission/");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful! Your username and password has been to email and mobile no.');window.location.href='" + url + "';", true);
                    }
                    else
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.error);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }
            }
        }

        private void GetDropDown()
        {
            ddlGender.DataSource = GetAll<GenderType>();
            ddlGender.DataTextField = "Value";
            ddlGender.DataValueField = "Value";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("-Select Gender-"));
            ddlGender.SelectedIndex = 0;

            ddlMaritalStatus.DataSource = GetAll<MaritalStatusType>();
            ddlMaritalStatus.DataTextField = "Value";
            ddlMaritalStatus.DataValueField = "Value";
            ddlMaritalStatus.DataBind();
            ddlMaritalStatus.Items.Insert(0, new ListItem("-Select Marital Status-"));
            ddlMaritalStatus.SelectedIndex = 0;
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            string strError = "";
            StudentRegistrationBO objBo = new StudentRegistrationBO();
            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
            {
                if (FillForm(objBo))
                {

                    if (objData.SignUpStudent(objBo))
                    {

                        string strBody = "Dear " + objBo.FirstName + " " + objBo.LastName.ToString() + "," +
                                    "<br/><br/> Username : " + objBo.Mobile +
                                    "<br/><br/> Password : " + Functions.Decrypt(objBo.Password) + "." +
                                    "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                                    "<br/><br/> Regards," +
                                    "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                        try
                        {
                            ErrorLogger.ERROR("\r\n Email", objBo.Email, this);
                            ErrorLogger.ERROR("\r\n Subject", "Registration Email", this);
                            ErrorLogger.ERROR("\r\n strBody", strBody, this);

                            string smtpserver = "";
                            string smtpPassword = "";
                            string fromemail = "";
                            string smtpaccount = "";
                            string smtpPort = "";
                            string smtpIsSecure = "";

                            if (ConfigDetailsValue.SMTPIsTest != "1")
                            {
                                smtpserver = ConfigDetailsValue.SMTPServer;
                                smtpPassword = ConfigDetailsValue.SMTPPassword;
                                fromemail = ConfigDetailsValue.SMTPFromEmail;
                                smtpaccount = ConfigDetailsValue.SMTPAccount;
                                smtpPort = ConfigDetailsValue.SMTPPort;
                                smtpIsSecure = ConfigDetailsValue.SMTPIsSecure;
                            }
                            else
                            {
                                smtpserver = ConfigDetailsValue.TestSMTPServer;
                                smtpPassword = ConfigDetailsValue.TestSMTPPassword;
                                fromemail = ConfigDetailsValue.TestSMTPFromEmail;
                                smtpaccount = ConfigDetailsValue.TestSMTPAccount;
                                smtpPort = ConfigDetailsValue.TestSMTPPort;
                                smtpIsSecure = ConfigDetailsValue.TestSMTPIsSecure;
                            }

                            ErrorLogger.ERROR("\r\n smtpserver", smtpserver, this);
                            ErrorLogger.ERROR("\r\n smtpPassword", smtpPassword, this);
                            ErrorLogger.ERROR("\r\n fromemail", fromemail, this);
                            ErrorLogger.ERROR("\r\n smtpaccount", smtpaccount, this);
                            ErrorLogger.ERROR("\r\n smtpPort", smtpPort, this);
                            ErrorLogger.ERROR("\r\n smtpIsSecure", smtpIsSecure, this);

                            Functions.MessagePopup(this, "Registration successful! Your username and password has been to email and mobile no", PopupMessageType.success);

                            if (Functions.SendEmail(objBo.Email, "Registration Email", strBody, out strError, true))
                            {


                                var sms = configDetailsRepository.GetSMSTemplateByName("Registration SMS");
                                if (sms != null)
                                {
                                    string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{UserID}}", objBo.Mobile).Replace("{{Password}}", Functions.Decrypt(objBo.Password)), Encoding.UTF8).Replace("+", "%20"));
                                    string strTemplateId = sms.SMSTemplateId;

                                    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                    string senderid2 = ConfigDetailsValue.senderid2;
                                    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, objBo.Mobile, strMessage, SMSAPI2, 0);
                                }

                                string url = ResolveUrl("~/Admission/");
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful! Your username and password has been to email and mobile no.');window.location.href='" + url + "';", true);
                            }
                            else
                            {
                                Functions.MessagePopup(this, strError, PopupMessageType.error);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                            Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                        }
                    }
                }
                FillCapctha();
            }
        }

        private bool FillForm(StudentRegistrationBO objBo)
        {
            long lgAadhar;
            if (string.IsNullOrWhiteSpace(txtAadharCard.Text))
            {
                Functions.MessagePopup(this, "Please Enter AadharCard", PopupMessageType.error);
                return false;
            }
            else if(!string.IsNullOrWhiteSpace(txtAadharCard.Text) && !long.TryParse(txtAadharCard.Text,out lgAadhar) && txtAadharCard.Text.Length != 12)
            {
                Functions.MessagePopup(this, "Please Enter Valid AadharCard", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.FirstName = txtAadharCard.Text;
            }
            if (string.IsNullOrWhiteSpace(txtFirstname.Text))
            {
                Functions.MessagePopup(this, "Please Enter First Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.FirstName = txtFirstname.Text;
            }
            if (string.IsNullOrWhiteSpace(txtMiddleName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Middle Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MiddleName = txtMiddleName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Last Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.LastName = txtLastName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtPhoneNo.Text))
            {
                Functions.MessagePopup(this, "Please Enter Mobile", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Mobile = txtPhoneNo.Text;
            }
            if (string.IsNullOrWhiteSpace(txtAadharCard.Text))
            {
                Functions.MessagePopup(this, "Please Enter Aadharcard No.", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.AadharCard = txtAadharCard.Text;
            }
            //if (string.IsNullOrWhiteSpace(txtPassword.Text))
            //{
            //    Functions.MessagePopup(this, "Please Enter Password", PopupMessageType.error);
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            //{
            //    Functions.MessagePopup(this, "Please Enter Confirm Password", PopupMessageType.error);
            //    return false;
            //}
            //else
            //{
            //    if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
            //    {
            //        objBo.Password = Functions.Encrypt(txtConfirmPassword.Text.Trim());
            //    }
            //    else
            //    {
            //        Functions.MessagePopup(this, "Enter Passwprd and Confirm Password mismatch", PopupMessageType.error);
            //        return false;
            //    }
            //}
            objBo.Password = Functions.Encrypt(Functions.GetRandomNumberStringForPayment());
            if (string.IsNullOrWhiteSpace(txtEmail.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter EmailId", PopupMessageType.error);
                return false;
            }
            else
            {

                if (Functions.ValidateEmailId(txtEmail.Text.Trim()))
                {
                    objBo.Email = txtEmail.Text.Trim();
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Please Enter Valid EmailId", PopupMessageType.error);
                    return false;
                }
            }
            string strError;
            DateTime? dt;
            if (string.IsNullOrWhiteSpace(txtBirthDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                return false;
            }
            else
            {
                if (!Functions.GetDateFromString(txtBirthDate.Text.Trim(), out dt, out strError))
                {
                    objBo.DateOfBirth = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    return false;
                }
            }
            if (ddlGender.SelectedIndex == 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Gender", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Gender = ddlGender.SelectedValue;
            }
            if (ddlMaritalStatus.SelectedIndex == 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Marital Status", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MaritalStatus = ddlMaritalStatus.SelectedValue;
            }
            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            {
                var dataOfUserName = objData.GetAllStudentRegistrationMaster();

            userName: string strUserName = (txtFirstname.Text.Trim().ToLower() + txtLastName.Text.Trim().ToLower() + Convert.ToDateTime(dt).ToString("ddMMyyyy") + Functions.GetRandomNumberString());
                if (dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.Username == strUserName).Count() > 0)
                        {
                            goto userName;
                        }
                        else
                        {
                            objBo.Username = strUserName;
                        }
                    }
                }
                if (dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.Email.Trim() == objBo.Email.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Email + " Already User exist with same email", PopupMessageType.error);
                            return false;
                        }
                    }
                }
                if (dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.Mobile.Trim() == objBo.Mobile.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Mobile + " Already User exist with same Mobile No", PopupMessageType.error);
                            return false;
                        }
                    }
                }

                if (dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.AadharCard == objBo.AadharCard.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.AadharCard + " Already User exist with same Aadhar Card", PopupMessageType.error);
                            return false;
                        }
                    }
                }

            }
            return true;
        }

        protected void btnRun_ServerClick(object sender, EventArgs e)
        {
            FillCapctha();
        }

        protected void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();
                    imgCaptcha.ImageUrl = ResolveUrl("~/GenerateCaptcha.aspx?") + DateTime.Now.Ticks.ToString();
                    //cvCaptcha.ValueToCompare= captcha.ToString();
                }
            }

            catch
            {
                throw;
            }
        }
    }
}