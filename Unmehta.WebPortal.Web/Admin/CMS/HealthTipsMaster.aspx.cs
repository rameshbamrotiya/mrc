using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Unmehta.WebPortal.Model.Model.Rights;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Configuration;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class HealthTipsMaster : System.Web.UI.Page
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
                    FillCountry(Convert.ToInt32(ddlLanguage.SelectedValue));
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }
        private void FillCountry(int LangId)
        {
            DataSet ds = new DataSet();
            HealthTipsMasterBAL objBAL = new HealthTipsMasterBAL();
            HealthTipsMasterBO objbo = new HealthTipsMasterBO();
            objbo.LanguageId = LangId;

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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Health_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        HealthTipsMasterBO objBo = new HealthTipsMasterBO();
                        objBo.Health_id = bytID;
                        new HealthTipsMasterBAL().DeleteRecord(objBo);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new HealthTipsMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            HealthTipsMasterBO objBo = new HealthTipsMasterBO();
            objBo.Health_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new HealthTipsMasterBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            if (dr["Title"] != DBNull.Value)
                txtTitle.Text = Convert.ToString(dr["Title"]);
            if (dr["Date"] != DBNull.Value)
                txtdate.Text = Convert.ToString(dr["Date"]);
            if (dr["ShortDesc"] != DBNull.Value)
                txtShortDesc.Text = Convert.ToString(dr["ShortDesc"]);
            if (dr["InnerDescription"] != DBNull.Value)
                txtinnerdesc.Text = HttpUtility.HtmlDecode(Convert.ToString(dr["InnerDescription"]));
            if (dr["ReffredBy"] != DBNull.Value)
                txtReffredBy.Text = (dr["ReffredBy"]).ToString();
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";
            if (dr["Health_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Health_level_id"].ToString();
            if (dr["Imgpath"] != DBNull.Value)
            {

                if (!string.IsNullOrWhiteSpace(dr["Imgpath"].ToString()))
                {
                    hfMainImage.Value = dr["Imgpath"].ToString();
                    aRemoveMain.Visible = true;
                    lblMainImage.Text = dr["Imgpath"].ToString();
                }
            }

            if (dr["InnerImgpath"] != DBNull.Value)
            {
                if (!string.IsNullOrWhiteSpace(dr["InnerImgpath"].ToString()))
                {
                    hfInnerImage.Value = dr["InnerImgpath"].ToString();
                    lblInnerImage.Text = dr["InnerImgpath"].ToString();
                    aRemoveInner.Visible = true;
                }
            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(HealthTipsMasterBO objBo)
        {
            if (FUmainimg.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFileMainImg();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Imgpath = documentfile;
            }
            else
            {
                objBo.Imgpath = hfMainImage.Value;
            }
            if (FUinnerimg.HasFile)
            {
                string innerimage = string.Empty;
                innerimage = SaveFileInnerImg();

                if (!string.IsNullOrEmpty(innerimage))
                    objBo.InnerImgpath = innerimage;
            }
            else
            {
                objBo.InnerImgpath = hfInnerImage.Value;
            }
            objBo.Title = txtTitle.Text;
            objBo.Date = txtdate.Text;
            objBo.ShortDesc = txtShortDesc.Text;
            objBo.InnerDescription = HttpUtility.HtmlEncode(txtinnerdesc.Text);
            objBo.ReffredBy = txtReffredBy.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Health_level_id = Convert.ToInt32(txtsequence.Text);
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                aRemoveInner.Visible = false;
                aRemoveMain.Visible = false;
                lblInnerImage.Text = "";
                lblMainImage.Text = "";
                hfInnerImage.Value = "";
                hfMainImage.Value = "";
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new HealthTipsMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Health_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Health_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
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
                if (!FUinnerimg.HasFile)
                {
                    Functions.MessagePopup(this, "Please Select inner Image.", PopupMessageType.warning);
                    return;
                }
                else
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(FUinnerimg.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)FUinnerimg.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 1900 && height != 720)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 1900px*720px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                HealthTipsMasterBO objBo = new HealthTipsMasterBO();
                LoadControls(objBo);
                if (new HealthTipsMasterBAL().InsertRecord(objBo))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
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
                if (FUinnerimg.HasFile)
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(FUinnerimg.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)FUinnerimg.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 1900 && height != 720)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 1900px*720px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                HealthTipsMasterBO objBo = new HealthTipsMasterBO();
                LoadControls(objBo);
                objBo.Health_id = Convert.ToInt32(ViewState["PK"]);
                if (new HealthTipsMasterBAL().UpdateRecord(objBo))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }


        private string SaveFileMainImg()
        {
            try
            {

                if (FUmainimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.HealthTips;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }
        private string SaveFileInnerImg()
        {
            try
            {

                if (FUinnerimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.HealthTips;
                    var fname = Path.GetExtension(FUinnerimg.FileName);
                    var count = FUinnerimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUinnerimg.FileName.Split('.').Length; i++)
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
                        var filename1 = FUinnerimg.FileName.Replace(" ", "_");

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
                        FUinnerimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
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
                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;

                    hfMainImage.Value = "";
                    lblMainImage.Text = "";
                    aRemoveMain.Visible = false;

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

                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;

                    hfMainImage.Value = "";
                    lblMainImage.Text = "";
                    aRemoveMain.Visible = false;

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

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
    }
}