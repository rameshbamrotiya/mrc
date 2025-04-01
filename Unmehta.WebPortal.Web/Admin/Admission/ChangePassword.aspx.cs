using BAL.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.StudentRegistration.Username == null)
                {
                    Response.Redirect("~/Admission/");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Admission/");
            }
            if (!IsPostBack)
            {
                if (SessionWrapper.StudentRegistration != null)
                {
                    txtUserName.Text = SessionWrapper.StudentRegistration.Mobile;
                    cmOldPassword.ValueToCompare = Functions.Decrypt(SessionWrapper.StudentRegistration.Password);
                }
                else
                {
                    Response.Redirect("~/Admission/");
                }
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
                using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
                {
                    if (objData.ChangeStudent(txtUserName.Text, Functions.Encrypt(txtConfirmPassword.Text.Trim())))
                    {
                        Changepass.Visible = false;
                        Loginpageredirect.Visible = true;
                        Functions.MessagePopup(this, "Password Change Successfully", PopupMessageType.success);

                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.ToString(), PopupMessageType.error);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admission/");
        }
    }
}