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
using Unmehta.WebPortal.Common;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Configuration;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class GovApprovelMaster : System.Web.UI.Page
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
                    BindYear();
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        GovApprovelBO objBo = new GovApprovelBO();
                        objBo.GovApp_id = bytID;
                        new GovApprovelBAL().DeleteRecord(objBo);
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new GovApprovelBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindYear();
            GovApprovelBO objBo = new GovApprovelBO();
            objBo.GovApp_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new GovApprovelBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            if (dr["CourseName"] != DBNull.Value)
                txtCourseName.Text = Convert.ToString(dr["CourseName"]);
            if (dr["year_id"] != DBNull.Value)
                ddlYears.SelectedItem.Text = Convert.ToString(dr["year_id"]);
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            if (dr["GovApp_level_id"] != DBNull.Value)
                txtsequence.Text = dr["GovApp_level_id"].ToString();

            if (dr["Doc_path"] != DBNull.Value)
            {
                if (!string.IsNullOrWhiteSpace(dr["Doc_path"].ToString()))
                {
                    hfLeftImage.Value = dr["Doc_path"].ToString();
                    lblLeftImage.Text = dr["Doc_path"].ToString();
                    aRemoveLeft.Visible = true;
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
        private void LoadControls(GovApprovelBO objBo)
        {
            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Doc_path = documentfile;
            }
            else
            {
                objBo.Doc_path = hfLeftImage.Value;
            }
            if (!string.IsNullOrEmpty(txtCourseName.Text))
                objBo.CourseName = txtCourseName.Text;
            else
                objBo.CourseName = "";
            objBo.Year_id = Convert.ToInt32(ddlYears.SelectedItem.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.GovApp_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new GovApprovelBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["GovApp_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["GovApp_level_id"].ToString();
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
                    string fileName = Path.GetFileName(fuDocUpload.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToLower();
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".pdf")
                    {
                        //if (fuDocUpload.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.success);
                        //    return;
                        //}
                        //else
                        {
                            GovApprovelBO objBo = new GovApprovelBO();
                            LoadControls(objBo);
                            if (new GovApprovelBAL().InsertRecord(objBo))
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
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg,.pdf'.", PopupMessageType.warning);
                        return;
                    }
                }
                else
                {
                    GovApprovelBO objBo = new GovApprovelBO();
                    LoadControls(objBo);
                    if (new GovApprovelBAL().InsertRecord(objBo))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
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
                    string fileName = Path.GetFileName(fuDocUpload.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".pdf")
                    {
                        //if (fuDocUpload.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return;
                        //}
                        //else
                        {
                            GovApprovelBO objBo = new GovApprovelBO();
                            LoadControls(objBo);
                            objBo.GovApp_id = Convert.ToInt32(ViewState["PK"]);
                            if (new GovApprovelBAL().UpdateRecord(objBo))
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
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg,.pdf'.", PopupMessageType.warning);
                        return;
                    }
                }
                else
                {
                    GovApprovelBO objBo = new GovApprovelBO();
                    LoadControls(objBo);
                    objBo.GovApp_id = Convert.ToInt32(ViewState["PK"]);
                    if (new GovApprovelBAL().UpdateRecord(objBo))
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


        private string SaveFile()
        {
            try
            {

                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.GovApprovel;
                    var fname = Path.GetExtension(fuDocUpload.FileName);
                    var count = fuDocUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuDocUpload.FileName.Split('.').Length; i++)
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
                        var filename1 = fuDocUpload.FileName.Replace(" ", "_");

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
                        fuDocUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
        private void BindYear()
        {
            ddlYears.Items.Clear();

            for (int i = 1960; i <= DateTime.Now.Year; i++)
            {
                ddlYears.Items.Add(i.ToString());
            }
        }
    }
}