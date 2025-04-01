using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Faculty;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Faculty
{
    public partial class Faculty : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
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
                //BindDesignationByLangId(1);
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
            txtFacultyName.Text = "";
            txtFacultydesc.Text = "";
            txtMoNo.Text = "";
            txtemail.Text = "";
            txtsequenceno.Text = "";
            hfFilName.Value = "";
            txtdesignation.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            //BindDesignationByLangId(1);
            BindDepartmentByLangId(1);
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private bool LoadControlsAdd(FacultyGridModel objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            if (!string.IsNullOrEmpty(txtFacultyName.Text))
                objBo.FacultyName = txtFacultyName.Text;
            if (!string.IsNullOrEmpty(txtMoNo.Text))
                objBo.MobileNumber = txtMoNo.Text;
            if (!string.IsNullOrEmpty(txtemail.Text))
                objBo.Email = txtemail.Text;
            if (!string.IsNullOrEmpty(txtsequenceno.Text))
                objBo.sequenceNo = Convert.ToDecimal(txtsequenceno.Text);
            if (!string.IsNullOrEmpty(txtFacultydesc.Text))
                objBo.FacultyDescription = txtFacultydesc.Text;
            if (!string.IsNullOrEmpty(txtdesignation.Text))
                objBo.DesignationName = txtdesignation.Text;
            //if (!string.IsNullOrEmpty(ddlDesignation.SelectedValue))
            //    objBo.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
                objBo.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

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
            using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
            {
                FacultyGridModel objBO = new FacultyGridModel();
                if (fuImage.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddFacultyFileUploadPath.Trim();

                    if (!filePath.Contains("|"))
                    {
                        //if (fuImage.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBO.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuImage.FileName);
                        objBO.ImagePath = filePath +  objBO.ImageName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + objBO.ImageName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBO.ImageName);
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
                            fuImage.SaveAs(Server.MapPath(filePath) + objBO.ImageName);
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
                    objBO.ImageName = System.IO.Path.GetFileName(hfFilName.Value);
                    objBO.ImagePath = hfFilName.Value;
                }
                if (LoadControlsAdd(objBO))
                {
                    if (!objFacultyRepository.InsertOrUpdateTblFaculty(objBO, out errorMessage))
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
            //BindDesignationByLangId(Convert.ToInt32(grdUser.DataKeys[rowindex]["LanguageId"].ToString()));
            using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
            {
                var data=objFacultyRepository.GetTblFacultyById(Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString()));
                if (data != null)
                {
                    //ddlDesignation.SelectedValue = grdUser.DataKeys[rowindex]["DesignationId"].ToString();
                    ddlDepartment.SelectedValue = data.DepartmentId.ToString();

                    txtFacultydesc.Text = HttpUtility.HtmlDecode(data.FacultyDescription);
                    txtdesignation.Text = HttpUtility.HtmlDecode(data.DesignationName);
                    txtsequenceno.Text = HttpUtility.HtmlDecode(data.sequenceNo.ToString());
                    txtMoNo.Text = data.MobileNumber;
                    txtemail.Text = data.Email;
                    txtFacultyName.Text = data.FacultyName;
                    Session["updateFileName"] = data.ImageName;
                    imgProfile.ImageUrl = ConfigDetailsValue.AddFacultyFileUploadPath + data.ImageName;
                    hfFilName.Value = ConfigDetailsValue.AddFacultyFileUploadPath + data.ImageName;
                    chkEnable.Checked= (bool) data.IsVisible;
                    hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
                    ShowHideControl(VisibityType.Edit);
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
                {
                    objFacultyRepository.RemoveTblFaculty(rowId, out errorMessage);
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
                FacultyGridModel objBo = new FacultyGridModel();
                if (fuImage.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddFacultyFileUploadPath;

                    if (!filePath.Contains("|"))
                    {
                        //if (fuImage.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBo.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmssfffff") + System.IO.Path.GetExtension(fuImage.FileName);
                        objBo.ImagePath = filePath + objBo.ImageName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 =  filePath + objBo.ImageName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBo.ImageName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension.ToLower();
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            // Check to see if a file already exists with the
                            // same name as the file to upload.
                            if (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                File.Delete(pathToCheck1);
                            }

                            //Save selected file into specified location
                            fuImage.SaveAs(Server.MapPath(filePath) + objBo.ImageName);
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
                    objBo.ImageName = System.IO.Path.GetFileName(hfFilName.Value);
                    objBo.ImagePath = hfFilName.Value;
                }
                if (LoadControlsAdd(objBo))
                {
                    using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
                    {
                        if (!objFacultyRepository.InsertOrUpdateTblFaculty(objBo, out errorMessage))
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
                txtSearch.Text = "";
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

        //private void BindDesignationByLangId(int LangId)
        //{
        //    ddlDesignation.Items.Clear();
        //    using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
        //    {
        //        ddlDesignation.DataSource = objDesignationRepository.GetAllTblDesignationLang(LangId);
        //        ddlDesignation.DataValueField = "Id";
        //        ddlDesignation.DataTextField = "DesignationName";
        //        ddlDesignation.DataBind();
        //        ddlDesignation.Items.Insert(0, new ListItem("Select", "-1"));
        //    }
        //}
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindDepartmentByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
            //BindDesignationByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            DataSet ds = new ExtraDetailsBAL().SequenceNo(DepartmentId);
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["sequenceNo"] != DBNull.Value)
                txtsequenceno.Text = drs["sequenceNo"].ToString();
        }
    }
}