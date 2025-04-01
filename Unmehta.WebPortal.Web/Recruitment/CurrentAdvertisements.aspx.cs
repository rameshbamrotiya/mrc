using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using System.Net.Mail;
using System.Net;
using BO;
using BAL;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class CurrentAdvertisements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    Bind_JobApplicationList();
                    if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
                    {
                        string querystring = Functions.Decrypt(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                        ddlJobApplication.SelectedValue = querystring;
                        ddlJobApplication_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        btnapply.Visible = false;
                        labelas.Visible = false;
                        lblMessage.Visible = false;
                    }
                    SessionWrapper.BasicDetailsFlag = 0;
                    SessionWrapper.EducationDetailsFlag = 0;
                    SessionWrapper.ProfessionalDetailsFlag = 0;
                }
            }
            catch (Exception ex)
            {
                Functions.MessageFrontPopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                //SessionWrapper.JobId = Convert.ToInt32(ddlJobApplication.SelectedValue.ToString());
                //SessionWrapper.PostName = ddlJobApplication.SelectedItem.Text.ToString();
                //SessionWrapper.OTPNo = txtOtpNo.Text.ToString();
                int jobId = Convert.ToInt32(ddlJobApplication.SelectedValue.ToString());
                CareerMasterBAL objBAL = new CareerMasterBAL();
                DataSet dsCheckFinalSubmitFlag = new DataSet();
                dsCheckFinalSubmitFlag = objBAL.Candidate_CheckFinalSubmitFlag(txtEmailId.Text.ToString(), jobId);

                var userSessionModel = new Model.Common.SessionUserModel();
                userSessionModel.UserName = txtEmailId.Text.ToString();
                userSessionModel.PostMinExperiance = dsCheckFinalSubmitFlag.Tables[1].Rows[0]["MinExperiance"].ToString();
                SessionWrapper.UserDetails = userSessionModel;

                int CandidateId = Convert.ToInt32(dsCheckFinalSubmitFlag.Tables[0].Rows[0]["Id"].ToString());
                if (dsCheckFinalSubmitFlag.Tables[0].Rows[0]["FinalSubmit"].ToString() == "1")
                {
                    string url = ResolveUrl("~/Recruitment/CandidateDetails.aspx");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already applied for this post...');window.location.href='" + url + "';", true);
                }
                else
                {
                    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId="+ jobId + "|RegId="+dsCheckFinalSubmitFlag.Tables[0].Rows[0]["RegisrationId"].ToString()+"|CandId="+ CandidateId));
                    Response.Redirect("~/Recruitment/?"+ strEndQueryString);
                }                
            }
            catch (Exception ex)
            {
                Functions.MessageFrontPopup(this,ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void Bind_JobApplicationList()
        {
            CareerMasterBAL objBAL = new CareerMasterBAL();
            //int JodId = 0;
            DataSet ds = new DataSet();
            ds = objBAL.Candidate_GetJobApplication();
            ddlJobApplication.DataSource = ds;
            ddlJobApplication.DataTextField = "AdvertisementName";
            ddlJobApplication.DataValueField = "Id";
            ddlJobApplication.DataBind();
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
            objBo.EmailId = txtEmailId.Text.Trim();
            objBo.otp = otpNo.ToString();
        }
        protected void btnSendOtp_Click(object sender, EventArgs e)
        {
            try
            {
                EmailOTPBO objBo = new EmailOTPBO();
                LoadControls(objBo);
                CareerMasterBAL objBAL = new CareerMasterBAL();
                bool Result = objBAL.InsertRecordOTP(objBo);
                SendEmailToOTP(txtEmailId.Text, objBo.otp);
                lblMessage.Visible = true;
                lblotpno.Visible = false;
                lblOTPVerify.Visible = false;
                lblMessage.Text = "OTP sent successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                txtOtpNo.Enabled = true;
                btnVerifyOtp.Enabled = true;
            }
            catch (Exception ex)
            {

                Functions.MessageFrontPopup(this, "Unable to Send OTP try after Some Time....", PopupMessageType.error);
                txtOtpNo.Enabled = false;
                btnVerifyOtp.Enabled = false;
                lblotpno.Visible = false;
                lblOTPVerify.Visible = false;
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        public void SendEmailToOTP(string emailId, string otp)
        {
            try
            {
                CareerMasterBAL objBAL = new CareerMasterBAL();
                DataSet ds = new DataSet();
                ds = objBAL.MailCreditials();
                if (ds != null && ds.Tables.Count > 0)
                {
                    //int port = 587;
                    string smtpserver = ds.Tables[0].Rows[0]["SMTPServer"].ToString();
                    string smtpPassword = ds.Tables[0].Rows[0]["SMTPPassword"].ToString();
                    string fromemail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
                    string smtpaccount = ds.Tables[0].Rows[0]["SMTPAccount"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient client = new SmtpClient(smtpserver);
                    msg.To.Add(emailId);
                    //msg.CC.Add("hardik.mistry@kcspl.co.in");      
                    // msg.CC.Add("parul@kcspl.co.in");
                    msg.IsBodyHtml = true;

                    msg.Subject = "GIL Recruitment OTP Verification";
                    msg.Body = "<br/> Your OTP for the post of  abc is " + otp + ". This OTP will expired after 5 minutes";
                    //"//+ ddlJobApplication.SelectedItem.ToString() +"


                    msg.From = new System.Net.Mail.MailAddress(fromemail);
                    client.EnableSsl = false;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpaccount, smtpPassword);
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            lblMessage.Text = "";
            if (string.IsNullOrEmpty(txtOtpNo.Text))
            {
                lblOTPVerify.Visible = true;
                lblOTPVerify.Text = "Please enter valid OTP.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblOTPVerify.Visible = false;
                lblOTPVerify.Text = "";
            }
            CareerMasterBAL objBAL = new CareerMasterBAL();
            string Result = objBAL.checkOTP(txtEmailId.Text, txtOtpNo.Text);
            if (Result == "0")
            {
                lblOTPVerify.Text = "OTP verification successfully...";
                lblOTPVerify.ForeColor = System.Drawing.Color.Green;
                lblOTPVerify.Visible = true;
                btn_Apply.Visible = true;
            }
            else if (Result == "1")
            {
                lblOTPVerify.Visible = true;
                lblOTPVerify.Text = "Your OTP does not match.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
            }
            else
            {                
                btnSendOtp.Text = "ReSend OTP";
                lblOTPVerify.Visible = true;
                lblOTPVerify.Text = "Your OTP has expired.";
                lblOTPVerify.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ddlJobApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            CareerMasterBAL objBAL = new CareerMasterBAL();
            int JodId = Convert.ToInt32(ddlJobApplication.SelectedValue.ToString());
            DataSet ds = new DataSet();
            ds = objBAL.desc_GetJobApplication(JodId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["AdvertisementName"] != DBNull.Value)
                    description.InnerHtml = HttpUtility.HtmlDecode(dr["AdvertisementName"].ToString());
                labelas.Visible = true;
                btnapply.Visible = true;
            }
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Recruitment/CurrentAdvertisements");
            descdiv.Visible = true;
            btnapply.Visible = true;
            labelas.Visible = true;
            linkapply.Visible = true;
            divOTP.Visible = false;
            ddlJobApplication.Enabled = true;
        }

        protected void btnapply_Click(object sender, EventArgs e)
        {
            descdiv.Visible = false;
            btnapply.Visible = false;
            labelas.Visible = false;
            linkapply.Visible = false;
            divOTP.Visible = true;
            btnRefresh.Visible = true;
            ddlJobApplication.Enabled = false;
        }
    }
}