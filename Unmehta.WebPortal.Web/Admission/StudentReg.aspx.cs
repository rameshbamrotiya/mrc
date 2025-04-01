using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class StudentReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDropDown();
                FillCapctha();
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
            if (string.IsNullOrEmpty(txtOtpNo.Text))
            {
                lblotpno.Text = "Please enter valid OTP.";
                lblotpno.Visible = true;
                lblOTPVerify.Visible = false;
                lblMessage.Visible = false;
                return;
            }
            else
            {
                lblotpno.Text = "";
                lblotpno.Visible = false;
                lblMessage.Visible = false;
                lblOTPVerify.Visible = false;
            }
            StudentRegistrationBAL objBAL = new StudentRegistrationBAL();
            string Result = objBAL.checkOTP(txtEmail.Text, txtOtpNo.Text);
            if (Result == "0")
            {
                lblOTPVerify.Text = "OTP verification successfully...";
                lblOTPVerify.ForeColor = System.Drawing.Color.Green;
                lblOTPVerify.Visible = true;
                btnRegistration.Visible = true;
            }
            else if (Result == "1")
            {
                lblOTPVerify.Text = "Your OTP does not match.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
                lblOTPVerify.Visible = true;
            }
            else
            {
                lblOTPVerify.Text = "Your OTP has expired.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
                btnSendOtp.Text = "ReSend OTP";
                lblOTPVerify.Visible = true;
            }
            StudentRegistrationBO objBo = new StudentRegistrationBO();
            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            {
                if (FillForm(objBo))
                {
                    if (objData.SignUpStudent(objBo))
                    {
                        string strSubject = "U.N.Mehta Institute of Cardiology & Research Centre Student Registration";
                        string strBody = "Dear " + objBo.FirstName + " " + objBo.LastName.ToString() + "," +
                                    "<br/><b> Username : " + objBo.Username + "</b>" +
                                    "<br/><b> Password : " + Functions.Decrypt(objBo.Password) + "</b>" +
                                    "<br/> This is an auto-generated mail. Please do not reply to this mail." +
                                    "<br/> Regards," +
                                    "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                        if (Functions.SendEmail(objBo.Email, strSubject, strBody, out strError, true))
                        {
                            Registarationform.Visible = false;
                            Loginpageredirect.Visible = true;
                            //Response.Redirect("~/Admission/LoginAdmission.aspx");
                            Functions.MessagePopup(this, "User Created Please Check Email", PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, strError, PopupMessageType.error);
                        }
                    }
                }
                FillCapctha();
            }
        }

        private bool FillForm(StudentRegistrationBO objBo)
        {
            if (ddlTitle.SelectedValue == "0")
            {
                Functions.MessagePopup(this, "Select Title.", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.NamePrefix = ddlTitle.SelectedValue.ToString();
            }
            if (string.IsNullOrWhiteSpace(txtFirstname.Text))
            {
                Functions.MessagePopup(this, "Enter Surname.", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.FirstName = txtFirstname.Text;
            }
            //if (string.IsNullOrWhiteSpace(txtMiddleName.Text))
            //{
            //    Functions.MessagePopup(this, "Enter Name.", PopupMessageType.error);
            //    return false;
            //}
            //else
            //{
                objBo.MiddleName = "";
            //}
            //if (string.IsNullOrWhiteSpace(txtLastName.Text))
            //{
            //    Functions.MessagePopup(this, "Enter Father/Husband Name.", PopupMessageType.error);
            //    return false;
            //}
            //else
            //{
                objBo.LastName = "";
            //}
            if (string.IsNullOrWhiteSpace(txtPhoneNo.Text))
            {
                Functions.MessagePopup(this, "Enter Mobile", PopupMessageType.error);
                return false;
            }
            else
            {
                if (txtPhoneNo.Text.Length > 10)
                {
                    Functions.MessagePopup(this, "Enter Valid Mobile Number.", PopupMessageType.error);
                    return false;
                }
                else
                {
                    objBo.Mobile = txtPhoneNo.Text;
                }
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
                Functions.MessageFrontPopup(this, "Enter EmailId", PopupMessageType.error);
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
                    Functions.MessageFrontPopup(this, "Enter Valid EmailId", PopupMessageType.error);
                    return false;
                }
            }
            string strError;
            DateTime? dt;
            if (string.IsNullOrWhiteSpace(txtBirthDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Select Date Of Birth", PopupMessageType.error);
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
                Functions.MessageFrontPopup(this, "Select Gender", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Gender = ddlGender.SelectedValue;
            }
            if (ddlMaritalStatus.SelectedIndex == 0)
            {
                Functions.MessageFrontPopup(this, "Select Marital Status", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MaritalStatus = ddlMaritalStatus.SelectedValue;
            }
            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            {
                var dataOfEmail = objData.GetAllStudentRegistrationMaster();
                if (dataOfEmail != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfEmail);
                    if (data != null)
                    {
                        if (data.Where(x => x.Email.Trim() == objBo.Email.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Email + " Already User exist with same email", PopupMessageType.error);
                            return false;
                        }
                    }
                }
                var dataOfMobile = objData.GetAllStudentRegistrationMaster();
                if (dataOfMobile != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfMobile);
                    if (data != null)
                    {
                        if (data.Where(x => x.Mobile.Trim() == objBo.Mobile.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Mobile + " Already User exist with same Mobile No", PopupMessageType.error);
                            return false;
                        }
                    }
                }
                //userName: string strUserName = (txtFirstname.Text.Trim().ToLower() + txtLastName.Text.Trim().ToLower() + Convert.ToDateTime(dt).ToString("ddMMyyyy") + Functions.GetRandomNumberString());
                userName: string strUserName = (txtPhoneNo.Text.Trim());
                var dataOfUserName = objData.GetAllStudentRegistrationMaster();
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
                txtCaptcha.Text = "";
                //string combination = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
                string combination = "0123456789";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 4; i++)
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
        public static string GetRandomNumberString()
        {
            Random generator = new Random();
            string r = generator.Next(0, 1000000).ToString("D6");
            return r;
        }
        private void LoadControls(EmailOTPBO objBo)
        {
            string otpNo = GetRandomNumberString();
            objBo.MobileNo = txtEmail.Text.Trim();
            objBo.otp = otpNo.ToString();
        }
        protected void btnSendOtp_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    string strError = "";
                    EmailOTPBO objBo = new EmailOTPBO();
                    LoadControls(objBo);
                    StudentRegistrationBAL objBAL = new StudentRegistrationBAL();
                    bool Result = objBAL.InsertRecordOTP(objBo);
                    string strSubject = "U.N.Mehta Institute of Cardiology & Research Centre Student Registration";
                    string strBody = "<br/> Your OTP  is " + objBo.otp + ". This OTP will expired after 5 minutes";
                    Functions.SendEmail(txtEmail.Text, strSubject, strBody, out strError, true);
                    lblMessage.Visible = true;
                    lblotpno.Visible = false;
                    lblOTPVerify.Visible = false;
                    lblMessage.Text = "OTP sent successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    txtOtpNo.Enabled = true;
                    btnVerifyOtp.Enabled = true;
                }
                else
                {
                    Functions.MessageFrontPopup(this, " Enter email.", PopupMessageType.error);
                    return;
                }                
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Unable to Send OTP try after Some Time....";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                txtOtpNo.Enabled = false;
                btnVerifyOtp.Enabled = false;
                lblotpno.Visible = false;
                lblOTPVerify.Visible = false;
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),Page);
            }
        }

        protected void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOtpNo.Text))
            {
                lblotpno.Text = "Please enter valid OTP.";
                lblotpno.Visible = true;
                lblOTPVerify.Visible = false;
                lblMessage.Visible = false;
                return;
            }
            else
            {
                lblotpno.Text = "";
                lblotpno.Visible = false;
                lblMessage.Visible = false;
                lblOTPVerify.Visible = false;
            }
            StudentRegistrationBAL objBAL = new StudentRegistrationBAL();
            string Result = objBAL.checkOTP(txtEmail.Text, txtOtpNo.Text);
            if (Result == "0")
            {
                lblOTPVerify.Text = "OTP verification successfully...";
                lblOTPVerify.ForeColor = System.Drawing.Color.Green;
                lblOTPVerify.Visible = true;
                btnRegistration.Visible = true;
            }
            else if (Result == "1")
            {
                lblOTPVerify.Text = "Your OTP does not match.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
                lblOTPVerify.Visible = true;
            }
            else
            {
                lblOTPVerify.Text = "Your OTP has expired.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
                btnSendOtp.Text = "ReSend OTP";
                lblOTPVerify.Visible = true;
            }
        }
    }
}