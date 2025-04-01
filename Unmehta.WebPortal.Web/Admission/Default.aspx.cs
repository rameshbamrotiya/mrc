using BAL;
using BAL.Admission;
using BO;
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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string value = Functions.Decrypt("6nBndNv3aDkiDQw5oqYT5w==");
                if (!IsPostBack)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    GeneralinstructionBAL objStudentAdvertisementBAL = new GeneralinstructionBAL();
                    var dataOfUserName = objStudentAdvertisementBAL.GetAllInstruction();
                    if (dataOfUserName != null)
                    {
                        List<GeneralinstructionBO> data = Functions.ToListof<GeneralinstructionBO>(dataOfUserName).Where(x=> x.IsVisible).ToList();
                        if (data != null)
                        {
                            foreach(GeneralinstructionBO row in data)
                            {
                                string strPath = "<a href=\"#\" target=\"_blank\"><img style=\"width: 20px;\" src=\"/Hospital/assets/img/pdf.png\"></a>";
                                if (string.IsNullOrWhiteSpace(row.DocPath))
                                {
                                    strPath = "";
                                }
                                else
                                {
                                    strPath = "<a href=\""+ResolveUrl( row.DocPath ) + "\" target=\"_blank\"><img  style=\"width: 20px;\" src=\"/Hospital/assets/img/pdf.png\" /></a>";
                                }
                                stringBuilder.Append("<li>\r\n<i class=\"fa fa-angle-double-right mr-1\" aria-hidden=\"true\"></i>"+ HttpUtility.HtmlDecode( row.Desciption) +" &nbsp; "+ strPath+"\r\n</li>");
                            }
                            ulInstruction.InnerHtml= stringBuilder.ToString();
                        }
                    }
                    FillCapctha();
                    // //txtCaptcha.Text = Session["captcha"].ToString();
                    //string value = Functions.Decrypt("f6nZL3XHt5yseNsaEvUkoA==");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                string combination = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
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
                        if (data != null)
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
                                    FillCapctha();
                                    txtCaptcha.Text = string.Empty;
                                }
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Username And Password not found.", PopupMessageType.error);
                                FillCapctha();
                                txtCaptcha.Text = string.Empty;

                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Username And Password not found.", PopupMessageType.error);
                            FillCapctha();
                            txtCaptcha.Text = string.Empty;
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