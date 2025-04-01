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
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Data;
using BAL;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class BlogCategory : System.Web.UI.Page
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
                BindGridView();
            }
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

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
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    ddlLanguage.Enabled = false;
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
            txtBlogName.Text = "";
            txtBlogger.Text = "";
            txtBlogDate.Text = "";
            txtShortDescription.Text = "";
            txtDescription.Text = "";
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView()
        {
            grdUser.DataSourceID = "";
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
                var Data = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(1).Where(x => x.TypeDetail == "Blog").ToList();
                if(!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    grdUser.DataSource = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(1).Where(x => x.TypeDetail == "Blog" && x.BlogName.Contains(txtSearch.Text)).ToList();

                }
                else
                {
                    grdUser.DataSource = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(1).Where(x => x.TypeDetail == "Blog").ToList();

                }
                grdUser.DataBind();
            }
        }

        private bool LoadControlsAdd(BlogCategoryMasterGridModel objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);

            if (!string.IsNullOrEmpty(txtShortDescription.Text))
                objBo.ShortDescription = txtShortDescription.Text;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                objBo.Description = (txtDescription.Text.Trim());

            if (!string.IsNullOrEmpty(txtBlogName.Text))
                objBo.BlogName = txtBlogName.Text;

            if (!string.IsNullOrEmpty(txtBlogger.Text))
                objBo.Blogger = txtBlogger.Text;

            if (!string.IsNullOrEmpty(txtBlogger.Text))
                objBo.Blogger = txtBlogger.Text;

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;

            if (!string.IsNullOrEmpty(txtBlogDate.Text))
                objBo.BlogDate = Convert.ToDateTime(txtBlogDate.Text);

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
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
                BlogCategoryMasterGridModel objBO = new BlogCategoryMasterGridModel();
                if (fuFileUpload.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                    if (!filePath.Contains("|"))
                    {
                        //if (fuFileUpload.PostedFile.ContentLength > 210000000)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBO.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                        objBO.ImagePath = filePath + "/" + objBO.ImageName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + objBO.ImageName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBO.ImageName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension.ToUpper();
                        if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                        {
                            // Check to see if a file already exists with the
                            // same name as the file to upload.
                            if (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                File.Delete(pathToCheck1);
                            }

                            //Save selected file into specified location
                            fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + objBO.ImageName);
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
                    objBO.ImagePath = System.IO.Path.GetFileName(hfLeftImage.Value);
                }
                if (LoadControlsAdd(objBO))
                {
                    if (!objBlogCategoryMasterRepository.InsertOrUpdateTblBlogCategoryMaster(objBO, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        return;
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
            txtBlogName.Text = grdUser.Rows[rowindex].Cells[1].Text.Trim();
            txtBlogger.Text = grdUser.Rows[rowindex].Cells[2].Text.Trim();
            txtBlogDate.Text = string.IsNullOrWhiteSpace(grdUser.Rows[rowindex].Cells[3].Text.Trim())?"": grdUser.Rows[rowindex].Cells[3].Text.Trim().Replace("-","/");
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
               var dataModel= objBlogCategoryMasterRepository.GetTblBlogCategoryMasterById(Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString()));
               if(dataModel!=null)
                {
                    txtMetaTitle.Text = dataModel.MetaTitle;
                    txtMetaDescription.Text = dataModel.MetaDescription;

                    if (!string.IsNullOrWhiteSpace(dataModel.ImageName))
                    {

                        hfLeftImage.Value = dataModel.ImagePath;
                        lblLeftImage.Text = dataModel.ImagePath;
                        aRemoveLeft.Visible = true;

                    }
                }
            }
            txtShortDescription.Text = grdUser.Rows[rowindex].Cells[4].Text.Trim();
            txtDescription.Text = HttpUtility.HtmlDecode(grdUser.Rows[rowindex].Cells[5].Text.Trim());
            Session["updateFileName"] = grdUser.DataKeys[rowindex]["ImageName"].ToString();
            
            chkEnable.Checked = Convert.ToBoolean(grdUser.Rows[rowindex].Cells[6].Text.Trim());
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
                using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
                {
                    objBlogCategoryMasterRepository.RemoveTblBlogCategoryMaster(rowId, out errorMessage);
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
                BlogCategoryMasterGridModel objBo = new BlogCategoryMasterGridModel();
                if (fuFileUpload.HasFile)
                {
                    string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                    if (!filePath.Contains("|"))
                    {
                        //if (fuFileUpload.PostedFile.ContentLength > 1048576000)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        objBo.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                        objBo.ImagePath = filePath + "/" + objBo.ImageName;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + objBo.ImageName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(objBo.ImageName);
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
                            fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + objBo.ImageName);
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
                    objBo.ImageName = Path.GetFileName(hfLeftImage.Value);
                    objBo.ImagePath = hfLeftImage.Value;
                }
                if (LoadControlsAdd(objBo))
                {
                    using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (!objBlogCategoryMasterRepository.InsertOrUpdateTblBlogCategoryMaster(objBo, out errorMessage))
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
                txtSearch.Text = string.Empty;
                BindGridView();
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
                txtSearch.Text = string.Empty;
                BindGridView();
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
            txtSearch.Text = string.Empty;
            ShowHideControl(VisibityType.GridView);
        }
    }
}