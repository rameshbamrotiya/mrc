using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Data;
using BAL;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class History : System.Web.UI.Page
    {
        #region Page Functions
        static string[] mediaExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF" //etc
        };
        static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        private void BindYear()
        {
            ddlHistoryYear.Items.Clear();

            for (int i = 1960; i <= DateTime.Now.Year; i++)
            {
                ddlHistoryYear.Items.Add(i.ToString());
            }
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    filepathtext.Visible = false;
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
                    ddlLanguage.Enabled = false;
                    btn_Save.Visible = true;
                    filepathtext.Visible = false;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    ddlLanguage.Enabled = true;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    filepathtext.Visible = true;
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            aRemoveLeft.Visible = false;
            hfID.Value = "0";
            txtHistoryTitle.Text = "";
            BindYear();
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            txtHistoryDescription.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private bool LoadControlsAdd(HistoryMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);

            if (!string.IsNullOrEmpty(ddlHistoryYear.SelectedValue))
                objBo.Year = ddlHistoryYear.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;


            if (fuImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddHistoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    //if (fuImage.PostedFile.ContentLength > 1048576)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.HistoryPhotoName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuImage.FileName);
                    objBo.HistoryPhotoPath = filePath + "/" + objBo.HistoryPhotoName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.HistoryPhotoName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.HistoryPhotoName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuImage.SaveAs(Server.MapPath(filePath) + "/" + objBo.HistoryPhotoName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                objBo.HistoryPhotoName = hfFilName.Value;
                objBo.HistoryPhotoPath = string.IsNullOrWhiteSpace(hfFilName.Value) ? "" : (ConfigDetailsValue.AddAboutUsFileUploadPath + "/" + hfFilName.Value);
            }

            if (!string.IsNullOrEmpty(txtHistoryTitle.Text))
                objBo.HistoryTitle = txtHistoryTitle.Text;

            if (string.IsNullOrWhiteSpace(txtHistoryDescription.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter History Description", PopupMessageType.error);
                return false;
            }
            else
            {
                //objBo.HistoryDescription = txtHistoryDescription.Text.Trim();
                objBo.HistoryDescription = HttpUtility.HtmlEncode(txtHistoryDescription.Text);
            }

            //if (!string.IsNullOrEmpty(txtHistoryDescription.Text))
            //    objBo.HistoryDescription = HttpUtility.HtmlEncode(txtHistoryDescription.Text);

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
        #endregion

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
                ddlLanguage.Enabled = false;
                BindYear();
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {

                string errorMessage = "";
                using (IHistoryRepository objHistoryRepository = new HistoryRepository(Functions.strSqlConnectionString))
                {
                    HistoryMasterModel objBO = new HistoryMasterModel();
                    if (fuImage.HasFile)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        decimal size = Math.Round(((decimal)fuImage.PostedFile.ContentLength / (decimal)1024), 2);
                        //if (height != 2848 || width != 42880)
                        //{
                        //    Functions.MessagePopup(this, "Please upload 4288px*2848px.", PopupMessageType.error);
                        //    return;
                        //}
                    }
                    if (LoadControlsAdd(objBO))
                    {
                        if (!objHistoryRepository.InsertOrUpdateTblHistory(objBO, out errorMessage))
                        {
                            ClearControlValues();
                            ShowHideControl(VisibityType.GridView);
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IHistoryRepository objHistoryRepository = new HistoryRepository(Functions.strSqlConnectionString))
            {
                HistoryMasterModel objBO = new HistoryMasterModel();
                if (fuImage.HasFile)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round(((decimal)fuImage.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (height != 2848 || width != 42880)
                    //{
                    //    Functions.MessagePopup(this, "Please upload 4288px*2848px.", PopupMessageType.error);
                    //    return;
                    //}
                }
                if (LoadControlsAdd(objBO))
                {
                    if (!objHistoryRepository.InsertOrUpdateTblHistory(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                    BindGridView();
                }
            }
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

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                txtSearch.Text = "";
                BindGridView();
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

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            ddlLanguage.SelectedValue = grdUser.DataKeys[rowindex]["LanguageId"].ToString();
            ddlHistoryYear.SelectedValue = grdUser.Rows[rowindex].Cells[1].Text.Trim();
            txtHistoryTitle.Text = grdUser.Rows[rowindex].Cells[2].Text.Trim();
            using (IHistoryRepository objHistoryRepository = new HistoryRepository(Functions.strSqlConnectionString))
            {
                var dataModel = objHistoryRepository.GetTblHistoryById(Convert.ToInt32(hfID.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataModel != null)
                {
                    txtMetaTitle.Text = dataModel.MetaTitle;
                    ddlLanguage.Enabled = true;
                    txtMetaDescription.Text = dataModel.MetaDescription;
                    txtHistoryDescription.Text = HttpUtility.HtmlDecode(dataModel.HistoryDescription);
                    chkEnable.Checked = (bool)dataModel.IsVisible;
                    if (!string.IsNullOrEmpty(dataModel.HistoryPhotoName))
                    {
                        imgProfile.ImageUrl = ConfigDetailsValue.AddHistoryFileUploadPath + dataModel.HistoryPhotoName;
                        filepathtext.Visible = true;
                        filepathtext.InnerText = ConfigDetailsValue.AddHistoryFileUploadPath + dataModel.HistoryPhotoName;
                        hfFilName.Value = dataModel.HistoryPhotoName;
                        aRemoveLeft.Visible = true;
                    }
                    else
                    {
                        aRemoveLeft.Visible = false;
                    }
                }
            }




            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IHistoryRepository objHistoryRepository = new HistoryRepository(Functions.strSqlConnectionString))
                {
                    objHistoryRepository.RemoveTblHistory(rowId, out errorMessage);
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
        #endregion

        #region History Image Details

        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lgLang, lgId;
            if (long.TryParse(ddlLanguage.SelectedValue, out lgLang) && long.TryParse(hfID.Value, out lgId))
            {
                if (lgId > 0 && lgLang > 0)
                {
                    using (IHistoryRepository objHistoryRepository = new HistoryRepository(Functions.strSqlConnectionString))
                    {
                        var dataModel = objHistoryRepository.GetTblHistoryById(Convert.ToInt32(hfID.Value), lgLang);
                        if (dataModel != null)
                        {
                            ddlHistoryYear.SelectedValue = dataModel.Year;
                            txtHistoryTitle.Text = dataModel.HistoryTitle;
                            txtMetaTitle.Text = dataModel.MetaTitle;
                            ddlLanguage.Enabled = true;
                            txtMetaDescription.Text = dataModel.MetaDescription;
                            txtHistoryDescription.Text = HttpUtility.HtmlDecode(dataModel.HistoryDescription);
                            chkEnable.Checked = (bool)dataModel.IsVisible;
                            imgProfile.ImageUrl = ConfigDetailsValue.AddHistoryFileUploadPath + dataModel.HistoryPhotoName;
                            hfFilName.Value = dataModel.HistoryPhotoName;
                        }
                        else
                        {

                            ddlHistoryYear.SelectedIndex = 0;
                            txtHistoryTitle.Text = "";
                            txtMetaTitle.Text = "";
                            txtMetaDescription.Text = "";
                            txtHistoryDescription.Text = "";
                            chkEnable.Checked = false;
                            imgProfile.ImageUrl = "";
                            hfFilName.Value = "";

                        }
                    }
                }
            }
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}