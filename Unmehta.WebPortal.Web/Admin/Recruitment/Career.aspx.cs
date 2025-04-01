using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data;
using BAL;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class Career : System.Web.UI.Page
    {
        #region Page Method
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
                ShowHideControl(VisibityType.GridView);
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ddlLanguage.SelectedIndex = 1;
                BindRequirementType();
                BindDesignationByLangId(1);
                BindDepartmentByLangId(1);
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
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
            hfID.Value = "0";
            txtShortDescription.Text = "";
            txtDescription.Text = "";
            txtTotalVacancy.Text = "";
            hfFilName.Value = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private bool LoadControlsAdd(CareerMasterGridModel objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);

            if (!string.IsNullOrEmpty(ddlDesignation.SelectedValue))
                objBo.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
                objBo.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (!string.IsNullOrEmpty(txtShortDescription.Text))
                objBo.ShortDescription = txtShortDescription.Text;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                objBo.Description = txtDescription.Text;

            if (!string.IsNullOrEmpty(txtTotalVacancy.Text))
                objBo.TotalVacancy = Convert.ToInt32(txtTotalVacancy.Text);

            if (!string.IsNullOrEmpty(ddlRequirementType.SelectedValue))
                objBo.RequirementType = ddlRequirementType.SelectedValue;

            objBo.IsVisible = chkEnable.Checked;
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
            return true;
        }

        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICareerMasterRepository objCareerMasterRepository = new CareerMasterRepository(Functions.strSqlConnectionString))
            {
                CareerMasterGridModel objBO = new CareerMasterGridModel();
                if (fuFileUpload.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddCareerFileUploadPath;

                    if (!filePath.Contains("|"))
                    {
                        //if (fuFileUpload.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBO.FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                        objBO.FilePath = filePath + "/" + objBO.FileName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + objBO.FileName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBO.FileName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            // Check to see if a file already exists with the
                            // same name as the file to upload.
                            if (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                File.Delete(pathToCheck1);
                            }

                            //Save selected file into specified location
                            fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + objBO.FileName);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                            return;
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                        return;
                    }
                }
                else
                {
                    objBO.FilePath = System.IO.Path.GetFileName(imgProfile.ImageUrl);
                }
                if (LoadControlsAdd(objBO))
                {
                    if (!objCareerMasterRepository.InsertOrUpdateTblCareer(objBO, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            ddlLanguage.SelectedValue = grdUser.DataKeys[rowindex]["LanguageId"].ToString();
            BindDepartmentByLangId(Convert.ToInt32(grdUser.DataKeys[rowindex]["LanguageId"].ToString()));
            BindDesignationByLangId(Convert.ToInt32(grdUser.DataKeys[rowindex]["LanguageId"].ToString()));
            ddlDesignation.SelectedValue = grdUser.DataKeys[rowindex]["DesignationId"].ToString();
            ddlDepartment.SelectedValue = grdUser.DataKeys[rowindex]["DepartmentId"].ToString();

            txtShortDescription.Text = grdUser.Rows[rowindex].Cells[3].Text.Trim();
            txtDescription.Text = grdUser.Rows[rowindex].Cells[4].Text.Trim();
            txtTotalVacancy.Text = grdUser.Rows[rowindex].Cells[5].Text.Trim();
            BindRequirementType();
            ddlRequirementType.SelectedValue = grdUser.Rows[rowindex].Cells[6].Text.Trim();
            Session["updateFileName"] = grdUser.DataKeys[rowindex]["FileName"].ToString();
            imgProfile.ImageUrl = ConfigDetailsValue.AddCareerFileUploadPath + grdUser.Rows[rowindex].Cells[8].Text.Trim();
            chkEnable.Checked = Convert.ToBoolean(grdUser.Rows[rowindex].Cells[7].Text.Trim());
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (ICareerMasterRepository objCareerMasterRepository = new CareerMasterRepository(Functions.strSqlConnectionString))
                {
                    objCareerMasterRepository.RemoveTblCareer(rowId, out errorMessage);
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

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                CareerMasterGridModel objBo = new CareerMasterGridModel();
                if (fuFileUpload.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddCareerFileUploadPath;

                    if (!filePath.Contains("|"))
                    {
                        //if (fuFileUpload.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBo.FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                        objBo.FilePath = filePath + "/" + objBo.FileName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + objBo.FileName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBo.FileName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            // Check to see if a file already exists with the
                            // same name as the file to upload.
                            if (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                File.Delete(pathToCheck1);
                            }

                            //Save selected file into specified location
                            fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + objBo.FileName);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                            return;
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                        return;
                    }
                }
                else
                {
                    objBo.FileName = Session["updateFileName"].ToString();
                    objBo.FilePath = imgProfile.ImageUrl + "/" + objBo.FileName;
                }
                if (LoadControlsAdd(objBo))
                {
                    using (ICareerMasterRepository objCareerMasterRepository = new CareerMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (!objCareerMasterRepository.InsertOrUpdateTblCareer(objBo, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
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
        #endregion

        #region Page Method
        private void BindRequirementType()
        {
            ddlRequirementType.Items.Clear();
            Array colors = Enum.GetValues(typeof(RequirtmentTypes));
            ddlRequirementType.Items.Add(new ListItem("Select Requirement Type", null));
            foreach (RequirtmentTypes color in colors)
            {
                ddlRequirementType.Items.Add(new ListItem(color.ToString(), color.ToString()));
            }
        }

        private void BindDepartmentByLangId(int LangId)
        {
            ddlDepartment.Items.Clear();
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {
                ddlDepartment.DataSource = objDepartmentRepository.GetAllTblDepartment(LangId);
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }

        private void BindDesignationByLangId(int LangId)
        {
            ddlDesignation.Items.Clear();
            using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            {
                ddlDesignation.DataSource = objDesignationRepository.GetAllTblDesignationLang(LangId);
                ddlDesignation.DataValueField = "Id";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartmentByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
            BindDesignationByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}