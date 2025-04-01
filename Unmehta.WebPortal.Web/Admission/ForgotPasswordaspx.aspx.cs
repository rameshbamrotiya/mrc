using BAL;
using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class ForgotPasswordaspx : System.Web.UI.Page
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (SessionWrapper.StudentRegistration.Username == null)
                //{
                //    Response.Redirect("~/Admission/LoginAdmission");
                //}
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Admission/LoginAdmission", false);
            }
            if (!IsPostBack)
            {
                Div1.Visible = false;
                Div3.Visible = false;
                cnpass_visible.Visible = false;
                pass_visible.Visible = false;
                Div2.Visible = false;
                //if (!string.IsNullOrEmpty(txtUserName.Text))
                //{
                //    txtUserName.Text = SessionWrapper.StudentRegistration.Username;
                //    //cmOldPassword.ValueToCompare = Functions.Decrypt(SessionWrapper.StudentRegistration.Password);
                //}
                //else
                //{
                //    Response.Redirect("~/Admission/LoginAdmission");
                //}
            }
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Regex rxValidate = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,30}$");

                if (txtPassword.Text.Length < 6 && txtConfirmPassword.Text.Length < 6)
                {
                    Functions.MessagePopup(this, "Password minimum 6 character required.", PopupMessageType.warning);
                    return;
                }
                if (!rxValidate.IsMatch(txtPassword.Text))
                {
                    Functions.MessagePopup(this, "Minimum 6 and maximum 30 characters, at least one uppercase letter, one lowercase letter, one number and one special character..", PopupMessageType.warning);
                    return;
                }
                if (txtPassword.Text.ToString() != txtConfirmPassword.Text.ToString())
                {
                    Functions.MessagePopup(this, "New Password and Confirm New Password not match.", PopupMessageType.warning);
                    return;
                }
                string errorMessage = "";
                StudentRegistrationBO objBo = new StudentRegistrationBO();
                objBo.Username = txtUserName.Text.Trim();
                using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
                {
                    var dataOfEmail = objData.GetAllStudentRegistrationMaster();
                    if (dataOfEmail != null)
                    {
                        List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfEmail);
                        if (data != null)
                        {
                            if (data.Where(x => x.Mobile.Trim() == objBo.Username.Trim()).Count() > 0)
                            {
                                using (StudentRegistrationBAL objData1 = new StudentRegistrationBAL())
                                {
                                    if (objData1.ChangeStudent(txtUserName.Text, Functions.Encrypt(txtConfirmPassword.Text.Trim())))
                                    {
                                        Div1.Visible = false;
                                        Div3.Visible = false;
                                        cnpass_visible.Visible = false;
                                        pass_visible.Visible = false;
                                        Div2.Visible = false;

                                        Changepass.Visible = false;
                                        Loginpageredirect.Visible = true;
                                        Functions.MessagePopup(this, "Password Change Successfully", PopupMessageType.success);

                                        var dataOfEmaisdl = objData.GetAllStudentRegistrationMaster();
                                        List<StudentRegistrationBO> dataOfEmaisdls = Functions.ToListof<StudentRegistrationBO>(dataOfEmaisdl);
                                        var mainModel = dataOfEmaisdls.Where(x => x.Mobile.Trim() == objBo.Username.Trim()).FirstOrDefault();

                                        if (mainModel != null)
                                        {
                                            string strError = "";
                                            string strBody = "Dear " + mainModel.FirstName + " " + mainModel.LastName.ToString() + "," +
                                                        "<br/><br/> Username : " + mainModel.Mobile +
                                                        "<br/><br/> Password : " + Functions.Decrypt(mainModel.Password) + "." +
                                                        "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                                                        "<br/><br/> Regards," +
                                                        "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                                            try
                                            {
                                                ErrorLogger.ERROR("\r\n Email", objBo.Email, this);
                                                ErrorLogger.ERROR("\r\n Subject", "Reset Registration Details", this);
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

                                                if (Functions.SendEmail(objBo.Email, "Reset Registration Details", strBody, out strError, true))
                                                {


                                                    //var sms = configDetailsRepository.GetSMSTemplateByName("Registration SMS");
                                                    //if (sms != null)
                                                    //{
                                                    //    string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{UserID}}", objBo.Mobile).Replace("{{Password}}", Functions.Decrypt(objBo.Password)), Encoding.UTF8).Replace("+", "%20"));
                                                    //    string strTemplateId = sms.SMSTemplateId;

                                                    //    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                                    //    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                                    //    string senderid2 = ConfigDetailsValue.senderid2;
                                                    //    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                                    //    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, objBo.Mobile, strMessage, SMSAPI2, 0);
                                                    //}

                                                    //string url = ResolveUrl("~/Admission/");
                                                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful! Your username and password has been to email and mobile no.');window.location.href='" + url + "';", true);
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
                                    else
                                    {
                                        Div1.Visible = true;
                                        Div3.Visible = true;
                                        cnpass_visible.Visible = true;
                                        pass_visible.Visible = true;
                                        Div2.Visible = true;
                                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                    }
                                }
                                return;
                            }
                            else
                            {
                                Div1.Visible = true;
                                Div3.Visible = true;
                                cnpass_visible.Visible = true;
                                pass_visible.Visible = true;
                                Div2.Visible = true;
                                Functions.MessageFrontPopup(this, objBo.Email + "User does not exist register first.", PopupMessageType.error);
                                return;
                            }
                        }
                    }
                    //var dataOfMobile = objData.GetAllStudentRegistrationMaster();
                    //if (dataOfMobile != null)
                    //{
                    //    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfMobile);
                    //    if (data != null)
                    //    {
                    //        if (data.Where(x => x.Mobile.Trim() == objBo.Mobile.Trim()).Count() > 0)
                    //        {
                    //            Functions.MessageFrontPopup(this, objBo.Mobile + " Already User exist with same Mobile No", PopupMessageType.error);
                    //            return;
                    //        }
                    //    }
                    //}
                    //userName: string strUserName = (txtFirstname.Text.Trim().ToLower() + txtLastName.Text.Trim().ToLower() + Convert.ToDateTime(dt).ToString("ddMMyyyy") + Functions.GetRandomNumberString());
                    //userName: string strUserName = (txtPhoneNo.Text.Trim());
                    //var dataOfUserName = objData.GetAllStudentRegistrationMaster();
                    //if (dataOfUserName != null)
                    //{
                    //    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    //    if (data != null)
                    //    {
                    //        if (data.Where(x => x.Username == strUserName).Count() > 0)
                    //        {
                    //            goto userName;
                    //        }
                    //        else
                    //        {
                    //            objBo.Username = strUserName;
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.ToString(), PopupMessageType.error);
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        protected void lnkbtnOTP_Click(object sender, EventArgs e)
        {

            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            {
                var dataOfUserName = objData.GetAllStudentRegistrationMaster();

                if (txtUserName.Text.Trim() == "")
                {
                    lblError1.Text = "Please Enter Contact no!";
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (txtUserName.Text.Trim().Length != 10)
                {
                    lblError1.Text = "Invalid Contact no!";
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if(dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.Mobile.Trim() == txtUserName.Text.Trim()).Count() > 0)
                        {

                            string Mobile = txtUserName.Text.Trim();
                            //DataSet ds = new DataSet();
                            //PaymentBAL objBAL = new PaymentBAL();
                            //ds = objBAL.GetPayment(Mobile);

                            //if (ds.Tables[0].Rows.Count > 0 && ds != null)
                            {
                                lblError1.Text = "";
                                string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                string senderid2 = ConfigDetailsValue.senderid2;
                                string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                string Templateid2 = ConfigDetailsValue.Templateid2;
                                string massage = "";
                                string OTP = Functions.GenerateOTP();

                                using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
                                {
                                    var sms = configDetailsRepository.GetSMSTemplateByName("Forget Password");
                                    if (sms != null)
                                    {
                                        massage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{OTP}}", OTP), Encoding.UTF8).Replace("+", "%20"));
                                        string strTemplateId = sms.SMSTemplateId;
                                    }
                                    hfDettails.Value = Base64Encode(OTP);
                                    //SessionWrapper.ForgetStudentOTP = Convert.ToInt32(OTP);
                                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, Mobile, massage, SMSAPI2, Convert.ToInt32(OTP));
                                    if (strRtn == "1")
                                    {
                                        //lnkbtnOTPs.Enabled = false;
                                        txtOpt.Visible = true;
                                        txtOpt.Enabled = true;
                                        lnkbtngo.Visible = true;
                                    }
                                    else
                                    {
                                        //lnkbtnOTPs.Enabled = true;
                                        txtOpt.Enabled = false;
                                        txtOpt.Visible = false;
                                        lnkbtngo.Visible = false;
                                    }
                                }
                            }
                            //else
                            //{
                            //    lblError1.Text = "Alert: No Donation History Found For This Mobile Number.!";
                            //    lblError1.ForeColor = System.Drawing.Color.Red;
                            //    return;
                            //}
                        }
                    }
                }
                else
                {
                    Functions.MessageFrontPopup(this, txtUserName.Text.Trim() + " User does not exist with Mobile No", PopupMessageType.error);
                    return;
                }
            }
        }

        protected void lnkbtngo_Click(object sender, EventArgs e)
        {

            if (txtUserName.Text.Trim() == "")
            {
                lblError1.Text = "Please Enter Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtUserName.Text.Trim().Length != 10)
            {
                lblError1.Text = "Invalid Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtOpt.Text.Trim() == "")
            {
                lblError1.Text = "Please Enter OTP!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtOpt.Text.Trim().Length != 6)
            {
                lblError1.Text = "Invalid OTP!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {

                if (Base64Decode(hfDettails.Value) == (txtOpt.Text.Trim()))
                {
                    string Mobile = txtUserName.Text.Trim();
                    Div1.Visible = true;
                    Div3.Visible = true;
                    cnpass_visible.Visible = true;
                    pass_visible.Visible = true;
                    Div2.Visible = true;
                    lblError1.Text = "";
                }
                else 
                {
                    lblError1.Text = "Invalid OTP!";
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }
    }
}