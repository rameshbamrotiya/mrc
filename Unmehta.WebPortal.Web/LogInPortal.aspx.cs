using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class LogInPortal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string strData = Functions.Decrypt("sFulzOwc8z6jaAUKiJ03Vg==");
                FillCapctha();
                //txtCaptcha.Text = Session["captcha"].ToString();
            }
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
                using (IUserLogInRepository objData = new UserLogInRepository(Functions.strSqlConnectionString))
                {


                    SessionUserModel session = new SessionUserModel();
                    if (!objData.LogInUsernamePassword(txtUserName.Text.Trim(), Functions.Encrypt(txtPassword.Text.Trim()), out session, out strError))
                    {
                        if (session.Id > 0)
                        {
                            SessionWrapper.UserDetails = session;
                            Functions.MessagePopup(this, "Successfully Login", PopupMessageType.success);
                            ClearForm();
                            Response.Redirect("~/Admin/Default.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();

                        }
                    }
                    else
                    {
                        ErrorLogger.ERROR(strError,"", this);
                        Functions.MessagePopup(this, strError, PopupMessageType.error);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
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
                    imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
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
    }
}