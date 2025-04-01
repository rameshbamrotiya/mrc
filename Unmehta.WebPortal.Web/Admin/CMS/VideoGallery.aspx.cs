using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class VideoGallery : System.Web.UI.Page
    {
        #region Page Load
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
            try
            {
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    FillVideoCategory();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        private void FillVideoCategory()
        {
            DataSet ds = new DataSet();
            VideoGalleryMasterBAL objBAL = new VideoGalleryMasterBAL();
            VideoGalleryMasterBO objbo = new VideoGalleryMasterBO();
            objbo.LanguageId = Convert.ToInt16(ddlLanguage.SelectedValue.ToString());
            ds = objBAL.SelectVideoCategoryByLanguage(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlVideoCategory, dt, "VideoCategoryName", "id", true);
        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region GridView Operation
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Video_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
                        objBo.Video_id = bytID;
                        new VideoGalleryMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
                else
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                    {
                        string col_parent_id = commandArgs[0];
                        string col_menu_level = commandArgs[1];
                        string cmd = commandArgs[2];
                        switch (cmd)
                        {
                            case "up":
                                SetPageOrder(cmd, col_menu_level, col_parent_id);
                                break;
                            case "down":
                                SetPageOrder(cmd, col_menu_level, col_parent_id);
                                break;
                        }
                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new VideoGalleryMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {
            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {

            aRemoveMain.Visible = false;
            aRemoveThumbnail.Visible = false;
            lblMainImage.Text = "";
            lblThumbnailImage.Text = "";
            hfMainImage.Value = "";
            hfThumbnailImage.Value = "";

            VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
            objBo.Video_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new VideoGalleryMasterBAL().SelectRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtVideoName.Text = "";
                txtvideodesc.Text = "";
                txtMetaDesc.Text = "";
                txtMetaTitle.Text = "";

                aRemoveMain.Visible = false;
                aRemoveThumbnail.Visible = false;
                lblMainImage.Text = "";
                lblThumbnailImage.Text = "";
                hfMainImage.Value = "";
                hfThumbnailImage.Value = "";

                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            ViewState["T017PDetails"] = ds.Tables[0];
            if (dr["Video_name"] != DBNull.Value)
                txtVideoName.Text = dr["Video_name"].ToString();
            if (dr["Video_desc"] != DBNull.Value)
                txtvideodesc.Text = dr["Video_desc"].ToString();
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["Video_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Video_level_id"].ToString();
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            if (dr["Is_download"] != DBNull.Value)
                ddlDownloadstatus.SelectedValue = Convert.ToString(dr["Is_download"]);
            if (dr["Link_Video_Upload"] != DBNull.Value)
                ddlvideoupload.SelectedValue = Convert.ToString(dr["Link_Video_Upload"]);
            FillVideoCategory();
            if (dr["VideoCategoryid"] != DBNull.Value)
                ddlVideoCategory.SelectedValue = Convert.ToString(dr["VideoCategoryid"]);
            
            if (dr["Link_Video_Upload"].ToString() == "True")
            {

                string imgPath = dr["Video_path"].ToString();
                hfMainImage.Value = imgPath.ToString();
                lblMainImage.Text = imgPath.ToString();
                if (!string.IsNullOrWhiteSpace(dr["Video_path"].ToString()))
                {
                    aRemoveMain.Visible = true;
                }

                string ThumbimgPath = dr["ThumbImg_path"].ToString();
                hfThumbnailImage.Value = ThumbimgPath.ToString();
                lblThumbnailImage.Text = ThumbimgPath.ToString();

                if (!string.IsNullOrWhiteSpace(dr["ThumbImg_path"].ToString()))
                {
                    aRemoveThumbnail.Visible = true;
                }

                externallink.Visible = false;
                //thumbnilexternal.Visible = true;
                internalvideo.Visible = true;
            }
            else
            {
                externallink.Visible = true;
                //thumbnilexternal.Visible = false;
                internalvideo.Visible = false;
                txtexternallink.Text = dr["Video_path"].ToString();

                string ThumbimgPath = dr["ThumbImg_path"].ToString();
                hfThumbnailImage.Value = ThumbimgPath.ToString();
                lblThumbnailImage.Text = ThumbimgPath.ToString();

            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(VideoGalleryMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtVideoName.Text))
                objBo.Video_Name = txtVideoName.Text;
            if (!string.IsNullOrEmpty(txtvideodesc.Text))
                objBo.Video_desc = txtvideodesc.Text;
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Video_level_id = Convert.ToInt32(txtsequence.Text);
            if (ddlvideoupload.Text == "False")
            {
                if (!string.IsNullOrEmpty(txtexternallink.Text))
                    objBo.Video_Path = txtexternallink.Text;
                if (futhumbnill.HasFile)
                {
                    string ThumbnillPath = string.Empty;
                    ThumbnillPath = SaveFile(futhumbnill);
                    if (!string.IsNullOrEmpty(ThumbnillPath))
                        objBo.Thumbnill_Path = ThumbnillPath;
                }
                else
                {
                    objBo.Thumbnill_Path = hfThumbnailImage.Value;
                }
            }
            else
            {
                if (fuDocUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile(fuDocUpload);
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.Video_Path = documentfile;
                }
                else
                {
                    objBo.Video_Path = hfMainImage.Value;
                }
                if (futhumbnill.HasFile)
                {
                    string ThumbnillPath = string.Empty;
                    ThumbnillPath = SaveFile(futhumbnill);
                    if (!string.IsNullOrEmpty(ThumbnillPath))
                        objBo.Thumbnill_Path = ThumbnillPath;
                }
                else
                {
                    objBo.Thumbnill_Path = hfThumbnailImage.Value;
                }
            }
            objBo.VideoCategoryid = Convert.ToInt32(ddlVideoCategory.SelectedValue);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active_Video = ddlActiveInactive.SelectedValue.ToString();
            objBo.Is_download = ddlDownloadstatus.SelectedValue.ToString();
            objBo.Link_Video_Upload = ddlvideoupload.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);            
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                FillVideoCategory();
                DataSet ds = new VideoGalleryMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Video_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Video_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    //if (fuDocUpload.PostedFile.ContentLength > 209715200)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 200 mb.", PopupMessageType.success);
                    //    return;
                    //}
                    //else
                    {
                        VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
                        LoadControls(objBo);
                        if (new VideoGalleryMasterBAL().InsertRecord(objBo))
                        {
                            Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                            return;
                        }
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                    }
                }
                else
                {
                    VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
                    LoadControls(objBo);
                    if (new VideoGalleryMasterBAL().InsertRecord(objBo))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                        return;
                    }
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    //if (fuDocUpload.PostedFile.ContentLength > 209715200)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 200 mb.", PopupMessageType.error);
                    //    return;
                    //}
                    //else
                    {
                        VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
                        LoadControls(objBo);
                        objBo.Video_id = Convert.ToInt32(ViewState["PK"]);
                        if (new VideoGalleryMasterBAL().UpdateRecord(objBo))
                        {
                            Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                            return;
                        }
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                    }
                }
                else
                {
                    VideoGalleryMasterBO objBo = new VideoGalleryMasterBO();
                    LoadControls(objBo);
                    objBo.Video_id = Convert.ToInt32(ViewState["PK"]);
                    if (new VideoGalleryMasterBAL().UpdateRecord(objBo))
                    {
                        Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                        return;
                    }
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }


        private string SaveFile(FileUpload fuUpload)
        {
            try
            {
                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.VideoUploadPath;
                    var fname = Path.GetExtension(fuUpload.FileName);
                    var count = fuUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuUpload.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuUpload.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fuUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    aRemoveMain.Visible = false;
                    aRemoveThumbnail.Visible = false;
                    lblMainImage.Text = "";
                    lblThumbnailImage.Text = "";
                    hfMainImage.Value = "";
                    hfThumbnailImage.Value = "";
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
                    aRemoveMain.Visible = false;
                    aRemoveThumbnail.Visible = false;
                    lblMainImage.Text = "";
                    lblThumbnailImage.Text = "";
                    hfMainImage.Value = "";
                    hfThumbnailImage.Value = "";
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    externallink.Visible = false;
                    ddlLanguage.Enabled = false;
                    internalvideo.Visible = true;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    aRemoveMain.Visible = false;
                    aRemoveThumbnail.Visible = false;
                    lblMainImage.Text = "";
                    lblThumbnailImage.Text = "";
                    hfMainImage.Value = "";
                    hfThumbnailImage.Value = "";

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
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void ddlvideoupload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlvideoupload.Text == "False")
            {
                externallink.Visible = true;
                //thumbnilexternal.Visible = false;
                internalvideo.Visible = false;
            }
            else
            {
                //thumbnilexternal.Visible = true;
                externallink.Visible = false;
                internalvideo.Visible = true;
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}