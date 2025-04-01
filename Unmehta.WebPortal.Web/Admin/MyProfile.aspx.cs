using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class MyProfile : System.Web.UI.Page
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
                    BindData();
                    using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
                    {
                        var data = objUserMasterRepository.GetAllUserMaster().Where(x => x.Id == SessionWrapper.UserDetails.Id).FirstOrDefault();
                        if (data != null)
                        {
                            hfID.Value = SessionWrapper.UserDetails.Id.ToString();
                            txtFirstName.Text = data.FirstName;
                            txtLastName.Text = data.LastName;
                            txtEmail.Text = data.Email;
                            txtMobile.Text = data.PhoneNo;
                            txtUserName.Text = data.UserName;
                            //drpDesignation.SelectedValue = data.Designation;
                            //ddlDept.SelectedValue = data.DepartmentId.ToString();
                            drpRole.SelectedValue = data.RoleId.ToString();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
        }

        private void BindData()
        {
            DataSet ds = new DataSet();
            UserMasterModel objbo = new UserMasterModel();
            using (IRoleMasterRepositry objIUserMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
            {
                drpRole.DataSource = objIUserMasterRepository.GetAllRoleMasterActiveList();
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "Id";
                drpRole.DataBind();
                drpRole.SelectedIndex = 0;
            }

            //using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            //{
            //    ddlDept.DataSource = objDepartmentRepository.GetAllTblDepartment(1);
            //    ddlDept.DataTextField = "DepartmentName";
            //    ddlDept.DataValueField = "Id";
            //    ddlDept.DataBind();
            //    ddlDept.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "0"));
            //    ddlDept.SelectedIndex = 0;
            //}
            //using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            //{
            //    drpDesignation.DataSource = objDesignationRepository.GetAllTblDesignation();
            //    drpDesignation.DataTextField = "DesignationName";
            //    drpDesignation.DataValueField = "Id";
            //    drpDesignation.DataBind();
            //    drpDesignation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "0"));
            //    drpDesignation.SelectedIndex = 0;
            //}
        }

        private void LoadControlsAdd(UserMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text))
                objBo.Username = txtUserName.Text;

            if (!string.IsNullOrEmpty(txtFirstName.Text))
                objBo.FirstName = txtFirstName.Text;
            if (!string.IsNullOrEmpty(txtLastName.Text))
                objBo.LastName = txtLastName.Text;
            if (!string.IsNullOrEmpty(txtMobile.Text))
                objBo.Phoneno = txtMobile.Text;
            if (!string.IsNullOrEmpty(txtEmail.Text))
                objBo.Email = txtEmail.Text;


            //if (drpDesignation.SelectedIndex > 0)
            //{
            //    objBo.DesignationId = Convert.ToInt32(drpDesignation.SelectedValue);
            //}
            //if (ddlDept.SelectedIndex > 0)
            //{
            //    objBo.DeptId = Convert.ToInt32(ddlDept.SelectedValue);
            //}

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
            objBo.IsActive = true;
            //if (drpRole.SelectedIndex > 0)
            {
                objBo.RoleId = Convert.ToInt16(drpRole.SelectedValue.ToString());
            }
            using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objUserMasterRepository.GetAllUserMaster().Where(x => x.Id == SessionWrapper.UserDetails.Id).FirstOrDefault();
                if (data != null)
                {
                    objBo.Password = data.UserPassword;
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
                        Functions.MessagePopup(this, "Profile update Successfully", PopupMessageType.success);
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