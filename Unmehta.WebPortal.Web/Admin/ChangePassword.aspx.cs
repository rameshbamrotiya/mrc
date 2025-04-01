using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
            }
            if (!IsPostBack)
            {
                if (SessionWrapper.UserDetails != null)
                {
                    txtUserName.Text = SessionWrapper.UserDetails.UserName;
                    cmOldPassword.ValueToCompare = Functions.Decrypt(SessionWrapper.UserDetails.UserPassword);
                }
                else
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
        }

        private void LoadControlsAdd(UserMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(txtConfirmPassword.Text))
                objBo.Password = Functions.Encrypt(txtConfirmPassword.Text);
            
            using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objUserMasterRepository.GetAllUserMaster().Where(x => x.Id == SessionWrapper.UserDetails.Id).FirstOrDefault();
                if (data != null)
                {
                    objBo.Username = data.UserName;
                    objBo.Id = data.Id;
                    objBo.RoleId = data.RoleId;
                    objBo.Email = data.Email;
                    objBo.Phoneno = data.PhoneNo;
                    objBo.FirstName = data.FirstName;
                    objBo.LastName = data.LastName;
                }
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
                {
                    UserMasterModel objBO = new UserMasterModel();
                    LoadControlsAdd(objBO);
                    if (!objUserMasterRepository.InsertUserMaster(objBO, out errorMessage))
                    {
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
    }
}