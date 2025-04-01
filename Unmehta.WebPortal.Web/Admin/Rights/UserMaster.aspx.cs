using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Unmehta.WebPortal.Model.Model.Rights;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.Rights
{
    public partial class UserMaster : System.Web.UI.Page
    {
        public UserRightsModel ObjUserRights = new UserRightsModel();
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            //if (HttpContext.Current.Session["UserName"] != null)
            {
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    FillRole();
                    BindGridView();
                }
            }

        }
        //protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
        //        {
        //            int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
        //            Int32 bytID;
        //            bytID = Convert.ToInt32(grdUser.DataKeys[intIndex].Values["news_id"]);
        //            if (e.CommandName == "eDelete")
        //            {
        //                NewsMasterBO objBo = new NewsMasterBO();
        //                objBo.news_id = bytID;
        //                //objBo.UpdatedBy = SessionWrapper.UserID;
        //                new NewsMasterBAL().DeleteRecord(objBo);
        //                BindGridView();
        //                ShowMessage("Record deleted successfully.", MessageType.Success);
        //                return;
        //            }

        //            ClearControlValues(pnlEntry);
        //            if (FillControls(bytID))
        //            {
        //                if (e.CommandName == "eView")
        //                    ShowHideControl(VisibityType.View);
        //                if (e.CommandName == "eEdit")
        //                {

        //                    ViewState["PK"] = bytID;
        //                    ShowHideControl(VisibityType.Edit);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //    }
        //}
        private void BindGridView()
        {
            using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
            {
                grdUser.DataSource = objUserMasterRepository.GetAllUserMaster();//.Where(x => x.Id != SessionWrapper.UserDetails.Id);
                grdUser.DataBind();
            }
        }
        private void FillRole()
        {
            DataSet ds = new DataSet();
            UserMasterModel objbo = new UserMasterModel();
            using (IRoleMasterRepositry objIUserMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
            {
                drpRole.DataSource = objIUserMasterRepository.GetAllRoleMasterActiveList();
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "Id";
                drpRole.DataBind();
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

            if (!string.IsNullOrEmpty(txtConfirmPassword.Text))
                objBo.Password = Functions.Encrypt(txtPassword.Text.Trim());

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
            objBo.IsActive = (ddlActiveInactive.SelectedValue.ToString() == "1" ? true : false);
            //if (drpRole.SelectedIndex > 0)
            {
                objBo.RoleId = Convert.ToInt16(drpRole.SelectedValue.ToString());
            }
            
        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objUserMasterRepository.GetAllUserMaster().Where(x => x.Id == Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString())).FirstOrDefault();
                if(data!=null)
                {

                    txtFirstName.Text = grdUser.Rows[rowindex].Cells[1].Text.Trim();
                    txtLastName.Text = grdUser.Rows[rowindex].Cells[2].Text.Trim();
                    txtEmail.Text = grdUser.Rows[rowindex].Cells[3].Text.Trim();
                    txtUserName.Text = grdUser.Rows[rowindex].Cells[4].Text.Trim();
                    //drpDesignation.SelectedValue = grdUser.Rows[rowindex].Cells[5].Text.Trim();
                    //drpDesignation.SelectedValue = grdUser.Rows[rowindex].Cells[6].Text.Trim();
                    if (drpRole.Items.FindByValue(grdUser.Rows[rowindex].Cells[7].Text.Trim()) != null)
                    {
                        drpRole.SelectedValue = grdUser.Rows[rowindex].Cells[7].Text.Trim();
                    }
                    txtMobile.Text = grdUser.Rows[rowindex].Cells[8].Text.Trim();
                    ddlActiveInactive.SelectedValue = (grdUser.Rows[rowindex].Cells[9].Text.Trim() == "True" ? 1 : 0).ToString();


                    hdnRoleid.Value = grdUser.Rows[rowindex].Cells[7].Text.Trim();

                }
            }
            hdnMode.Value = "0";
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
                {
                    objUserMasterRepository.RemoveUserMaster(rowId, "", out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
                {
                    UserMasterModel objBO = new UserMasterModel();
                    LoadControlsAdd(objBO);
                    if (!objUserMasterRepository.InsertUserMaster(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Password Mismatch!!!", PopupMessageType.error);
            }

        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;

                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    pass_visible.Visible = true;
                    cnpass_visible.Visible = true;
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    //pass_visible.Visible = false;
                    //cnpass_visible.Visible = false;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            txtConfirmPassword.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMobile.Text = "";
            txtPassword.Text = "";
            txtSearch.Text = "";
            txtUserName.Text = "";
            //ddlActiveInactive.SelectedIndex = 0;
            //ddlDept.SelectedIndex = 0;
            BindGridView();
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                UserMasterModel objBo = new UserMasterModel();
                LoadControlsAdd(objBo);
                objBo.RoleId = Convert.ToInt32(hdnRoleid.Value);
                using (IUserMasterRepository objUserMasterRepository = new UserMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objUserMasterRepository.InsertUserMaster(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
                ClearControlValues();
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}