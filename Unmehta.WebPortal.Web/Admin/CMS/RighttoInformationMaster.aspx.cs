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
    public partial class RighttoInformationMaste : System.Web.UI.Page
    {
        public static long CurrentPageIndex;

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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    FillControls("RTI", Convert.ToInt32(ddlLanguage.SelectedValue));
                    //ViewState["TmpDocument"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }
        #endregion

        #region GridView Operation
        private bool FillControls(string Type, int languageId)
        {
            ClearControls();
            RighttoInformationMasterBO objBo = new RighttoInformationMasterBO();
            objBo.Type = "RTI";
            objBo.LanguageID = languageId;
            DataSet ds = new RighttoInformationMasterBAL().SelectRecord(objBo);
            if (!ds.Tables.Count.Equals(0))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr.HasErrors) return false;

                    if (dr["RIID"] != DBNull.Value)
                        hfRIID.Value = Convert.ToString(dr["RIID"]);

                    if (dr["RIMID"] != DBNull.Value)
                        hfRIMID.Value = Convert.ToString(dr["RIMID"]);

                    if (dr["LanguageID"] != DBNull.Value)
                        ddlLanguage.SelectedValue = Convert.ToString(dr["LanguageID"]);

                    if (dr["Enabled"] != DBNull.Value)
                        ddlActiveInactive.SelectedValue = Convert.ToString(dr["Enabled"]);

                    if (dr["MetaTitle"] != DBNull.Value)
                        txtMetaTitle.Text = dr["MetaTitle"].ToString();

                    if (dr["MetaDescription"] != DBNull.Value)
                        txtMetaDescription.Text = dr["MetaDescription"].ToString();

                    if (dr["Description"] != DBNull.Value)
                        txtRTIDescription.Text = dr["Description"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[1].Rows[0];
                    if (dr.HasErrors) return false;
                    ViewState["TmpDocument"] = ds.Tables[1];
                   
                }
            }
            BindGrid();
            return true;
        }
        #endregion

        #region Save || Update || Cancel
        public void LoadControls(RighttoInformationMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(hfRIID.Value))
                objBo.RIID = Convert.ToInt32(hfRIID.Value);

            if (!string.IsNullOrEmpty(hfRIMID.Value))
                objBo.RIMID = Convert.ToInt32(hfRIMID.Value);

            objBo.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.enabled = Convert.ToBoolean(ddlActiveInactive.SelectedValue.ToString());
            objBo.Type = "RTI";
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text.ToString();

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text.ToString();


            if (!string.IsNullOrEmpty(txtRTIDescription.Text))
                objBo.Description = txtRTIDescription.Text.ToString();

            objBo.modify_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }

        public void ClearControls()
        {
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            txtRTIDescription.Text = "";
            //ddlLanguage.SelectedIndex = -1;
            ddlActiveInactive.SelectedIndex = -1;
            ViewState["TmpDocument"] = null;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                RighttoInformationMasterBO objBo = new RighttoInformationMasterBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["TmpDocument"];
                if (new RighttoInformationMasterBAL().InsertRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls("RTI", Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void bntAddDoc_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["TmpDocument"] == null)
                {
                    CreateTempTable();
                }
                AddNewRecordRowToGrid();
                txtDocTitle.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void AddNewRecordRowToGrid()
        {
            // check view state is not null  
            if (ViewState["TmpDocument"] != null)
            {
                DataTable dtCurrentTable = new DataTable();
                 dtCurrentTable = (DataTable)ViewState["TmpDocument"];
                DataRow dr = dtCurrentTable.NewRow();
                bool isError = false;

                if (fuDocUpload.HasFile)
                {
                    string filepath = string.Empty;
                    filepath = SaveFile(out isError);

                    if (!string.IsNullOrEmpty(filepath) && !isError)
                    {
                        ErrorLogger.ERROR("RTI", filepath, this);

                        dr["DocTitle"] = txtDocTitle.Text.ToString();
                        dr["DocURL"] = filepath;
                        dtCurrentTable.Rows.Add(dr);
                        dtCurrentTable.AcceptChanges();
                        ViewState["TmpDocument"] = dtCurrentTable;
                        BindGrid();
                        ErrorLogger.ERROR("RTI Done", filepath, this);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Select File.", PopupMessageType.error);
                    ErrorLogger.ERROR("RTI", "Please Select File.", this);
                }

            }
        }

        private void CreateTempTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DocTitle", typeof(string));
            dt.Columns.Add("DocURL", typeof(string));
            ViewState["TmpDocument"] = dt;
        }

     


        protected void BindGrid()
        {
            gvDoc.DataSource = (DataTable)ViewState["TmpDocument"];
            gvDoc.DataBind();
        }

        private string SaveFile(out bool isError)
        {
            isError = false;
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocPath = ConfigDetailsValue.RightToInfoPath;
                    string fileMimeType = fuDocUpload.PostedFile.ContentType;

                    var fname = Path.GetExtension(fuDocUpload.FileName);
                    var count = fuDocUpload.FileName.Split('.');
                    int Extensioncount = fuDocUpload.FileName.Count(f => f == '.');
                    var ValidFileTypes = new[] { "doc", "docx", "pdf", "xls", "xlsx" };

                    string[] matchMimeType = { "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/pdf" };//, "application/msword" , "application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/vnd.ms-excel","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","application/pdf"

                    if (!ValidFileTypes.Contains(fname.Substring(1).ToLower()) && !matchMimeType.Contains(fileMimeType) && Extensioncount == 1)
                    {
                        Functions.MessagePopup(this, "File Extension Is InValid - Only Upload PDF/DOC/DOCX File.", PopupMessageType.error);
                        isError = true;
                        return "";
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuDocUpload.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(DocPath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocPath));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocPath + filename1;
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
                                pathToCheck1 = DocPath + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fuDocUpload.SaveAs(Server.MapPath(DocPath) + filename1);
                        string NewFIlePath = ((DocPath) + filename1).ToString();
                        return NewFIlePath;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }

        protected void gvDoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable dtdel = new DataTable();
                dtdel = (DataTable)ViewState["TmpDocument"];
                int index = e.RowIndex + gvDoc.PageIndex * gvDoc.PageSize;
                dtdel.Rows[index].Delete();
                dtdel.AcceptChanges();
                ViewState["TmpDocument"] = dtdel;
                BindGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }


        protected void gvDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvDoc.PageIndex = e.NewPageIndex;
            CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}