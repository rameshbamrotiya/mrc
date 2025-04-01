using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class LoginAdmission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillCapctha();                    
                    //txtCaptcha.Text = Session["captcha"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Admission/", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void ClearForm()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
        protected void FillCapctha()
        {
            try
            {
                Random random = new Random();
                // string combination = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
                string combination = "0123456789";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();
                    imgCaptcha.ImageUrl = ResolveUrl("~/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString());
                    //cvCaptcha.ValueToCompare= captcha.ToString();
                }
            }

            catch
            {
                throw;
            }
        }

        protected void btnRun_ServerClick(object sender, EventArgs e)
        {
            FillCapctha();
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string strError = "";
            try
            {
                if (Session["captcha"].ToString() != txtCaptcha.Text)
                {
                    strError = "Captcha does not match";
                    FillCapctha();
                    txtCaptcha.Text = string.Empty;
                    Functions.MessagePopup(this, strError, PopupMessageType.error);
                    return;
                }
                using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
                {
                    DataTable dt = objData.LoginStudent(txtUserName.Text.Trim(), Functions.Encrypt(txtPassword.Text.Trim()));
                    if (dt != null)
                    {
                        List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dt);
                        if (data.Count != 0)
                        {

                            if (data.FirstOrDefault() != null && !string.IsNullOrWhiteSpace(data.FirstOrDefault().Username))
                            {
                                if (data.FirstOrDefault().Id > 0)
                                {
                                    SessionWrapper.StudentRegistration = data.FirstOrDefault();

                                    if (!SessionWrapper.StudentRegistration.IsUsernameReset)
                                    {
                                        Response.Redirect("~/Admission/Course.aspx",false);
                                    }
                                    else
                                    {
                                        Response.Redirect("~/Admin/Admission/ChangePassword.aspx", false);
                                    }
                                    Functions.MessagePopup(this, "Successfully Login", PopupMessageType.success);
                                    ClearForm();
                                }
                                else
                                {
                                    Functions.MessagePopup(this, strError, PopupMessageType.error);
                                }
                            }
                            else
                            {
                                Functions.MessagePopup(this, "User does not exists register first.", PopupMessageType.error);
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Username or Password not found", PopupMessageType.error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}