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
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Configuration;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class ArticleDepartmentMaster : System.Web.UI.Page
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
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["AD_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        ArticleDepartmentBO objBo = new ArticleDepartmentBO();
                        objBo.AD_id = bytID;
                        new ArticleDepartmentBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            ArticleDepartmentBO objBo = new ArticleDepartmentBO();
            objBo.AD_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new ArticleDepartmentBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0))
            {
                txtArticleDepartment.Text = "";
                txtArticleDepTitle.Text = "";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr.HasErrors) return false;
                if (dr["AD_Name"] != DBNull.Value)
                    txtArticleDepartment.Text = Convert.ToString(dr["AD_Name"]);
                if (dr["AD_Title"] != DBNull.Value)
                    txtArticleDepTitle.Text = Convert.ToString(dr["AD_Title"]);
                if (dr["is_active"] != DBNull.Value)
                    ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(ArticleDepartmentBO objBo)
        {
            string documentfilemain = string.Empty;
            documentfilemain = SaveFileMain();
            if (!string.IsNullOrEmpty(documentfilemain))
                objBo.Imgpath = documentfilemain;

            string documentfileicon = string.Empty;
            documentfileicon = SaveFileMainIcon();
            if (!string.IsNullOrEmpty(documentfileicon))
                objBo.Iconpath = documentfileicon;
            objBo.AD_Name = txtArticleDepartment.Text;
            objBo.AD_Title = txtArticleDepTitle.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
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
                if (string.IsNullOrEmpty(txtArticleDepartment.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility title.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtArticleDepTitle.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility short description.", PopupMessageType.warning);
                    return;
                }
                if (!FUmainimg.HasFile)
                {
                    Functions.MessagePopup(this, "Please Select Main Image.", PopupMessageType.warning);
                    return;
                }
                else
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(FUmainimg.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)FUmainimg.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 1200 && height != 800)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 1200px*800px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                if (!Fuimgicon.HasFile)
                {
                    Functions.MessagePopup(this, "Please Select Image Icon.", PopupMessageType.warning);
                    return;
                }
                else
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(Fuimgicon.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)Fuimgicon.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 64 && height != 64)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 64px*64px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                ArticleDepartmentBO objBo = new ArticleDepartmentBO();
                LoadControls(objBo);
                if (new ArticleDepartmentBAL().InsertRecord(objBo))
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtArticleDepartment.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility title.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtArticleDepTitle.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility short description.", PopupMessageType.warning);
                    return;
                }
                if (FUmainimg.HasFile)
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(FUmainimg.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)FUmainimg.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 1200 && height != 800)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 1200px*800px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                if (Fuimgicon.HasFile)
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(Fuimgicon.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)Fuimgicon.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 64 && height != 64)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 64px*64px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                ArticleDepartmentBO objBo = new ArticleDepartmentBO();
                LoadControls(objBo);
                objBo.AD_id = Convert.ToInt32(ViewState["PK"]);
                if (new ArticleDepartmentBAL().UpdateRecord(objBo))
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
        #endregion

        #region ShowHideControl || Notification
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
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues(pnlEntry);
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
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }
        #endregion
        private string SaveFileMainIcon()
        {
            try
            {
                if (Fuimgicon.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.ResearchImage;
                    var fname = Path.GetExtension(Fuimgicon.FileName);
                    var count = Fuimgicon.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < Fuimgicon.FileName.Split('.').Length; i++)
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
                        var filename1 = Fuimgicon.FileName.Replace(" ", "_");
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
                        Fuimgicon.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
        private string SaveFileMain()
        {
            try
            {
                if (FUmainimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.ResearchImage;
                    var fname = Path.GetExtension(FUmainimg.FileName);
                    var count = FUmainimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUmainimg.FileName.Split('.').Length; i++)
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
                        var filename1 = FUmainimg.FileName.Replace(" ", "_");
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
                        FUmainimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}