using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Web;
using System.IO;
using Unmehta.WebPortal.Common;
using System.Collections.Generic;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class AlumniStudentMaster : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    FillLanguage();
                    FillYears();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
        public void FillYears()
        {
            int startYear = 1962;
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = startYear; year <= currentYear; year++)
            {
                years.Add(year);
            }
            ddlYears.DataSource = years;
            ddlYears.DataBind();
            ddlYears.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["AlumniStudentId"]);
                    if (e.CommandName == "eDelete")
                    {
                        AlumniStudentMasterBO objBo = new AlumniStudentMasterBO();
                        objBo.AlumniStudentId = bytID;
                        new AlumniStudentMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID,Convert.ToInt32(drpLanguage.SelectedValue)))
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
        private bool FillControls(Int32 PKId,Int32 LanguageId)
        {
            try
            {
                AlumniStudentMasterBO objbo = new AlumniStudentMasterBO();
                AlumniStudentMasterBAL objBAL = new AlumniStudentMasterBAL();
                objbo.AlumniStudentId = PKId;
                objbo.LanguageId = LanguageId;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectAlumniStudentByID(objbo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    FillLanguage();
                    drpLanguage.SelectedValue = ds.Tables[0].Rows[0]["LanguageId"].ToString();
                    FillYears();
                    ddlYears.SelectedValue = ds.Tables[0].Rows[0]["Year"].ToString();
                    txtTitle.Text = ds.Tables[0].Rows[0]["Title"].ToString();

                    if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["FileUploadPath"].ToString()))
                    {
                        hfLeftImage.Value = ds.Tables[0].Rows[0]["FileUploadPath"].ToString(); ;
                        lblLeftImage.Text = ds.Tables[0].Rows[0]["FileUploadPath"].ToString(); ;
                        aRemoveLeft.Visible = true;
                    }

                    ddlActiveInactive.SelectedValue = ds.Tables[0].Rows[0]["Is_active"].ToString() == "True" ? "1" : "0";                    
                    return true;
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                return false;
            }
            
        }
        private void BindGridView()
        {
            //DataSet ds = new DataSet();            
            //AlumniStudentMasterBO objBo = new AlumniStudentMasterBO();            
            //objBo.LanguageId = LanguageId;
            //AlumniStudentMasterBAL objBAL = new AlumniStudentMasterBAL();
            //ds = objBAL.SelectAlumniStudentDetails(objBo);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    gView.DataSource = ds;
            //    gView.DataBind();
            //}
            //else
            //{
            //    gView.DataSource = null;
            //    gView.DataBind();
            //}
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(AlumniStudentMasterBO objBo)
        {
            objBo.LanguageId = LanguageId;
            objBo.Year = ddlYears.SelectedValue;
            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text.ToString();
            if (fuFileUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile(fuFileUpload, ConfigDetailsValue.AlumniStudentFileUploadPath);

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.FileUploadPath = documentfile;
            }
            else
            {
                objBo.FileUploadPath = hfLeftImage.Value;
            }
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                hfLeftImage.Value = "";
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private string SaveFile(FileUpload fuUpload,string FilePath)
        {
            try
            {

                if (fuUpload.HasFile)
                {
                    var DocumentUpload = FilePath;
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
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                AlumniStudentMasterBO objBo = new AlumniStudentMasterBO();
                LoadControls(objBo);
                if (new AlumniStudentMasterBAL().InsertRecord(objBo))
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
                AlumniStudentMasterBO objBo = new AlumniStudentMasterBO();
                LoadControls(objBo);
                objBo.AlumniStudentId = Convert.ToInt16(hdPKId.Value.ToString());
                DataTable dt = new DataTable();
                if (new AlumniStudentMasterBAL().UpdateRecord(objBo))
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
                    break;
                case VisibityType.Insert:


                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
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

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hdPKId.Value), Convert.ToInt32(drpLanguage.SelectedValue.ToString()));
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
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
    }
}