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

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientCareBrochureDetails : System.Web.UI.Page
    {
        public static int TabTypeId, LanguageId, SubTabId;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    long id = 0;
                    string[] splitString = queryString.Split('|');
                    TabTypeId = Convert.ToInt32(splitString[0].ToString().Replace("TabTypeId=", ""));
                    SubTabId = Convert.ToInt32(splitString[1].ToString().Replace("SubTabId=", ""));
                    LanguageId = Convert.ToInt32(splitString[2].ToString().Replace("LanguageId=", ""));
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["BrochureId"]);
                    if (e.CommandName == "eDelete")
                    {
                        PatientCareBrochureDetailsBO objBo = new PatientCareBrochureDetailsBO();
                        objBo.BrochureId = bytID;
                        new PatientCareMasterBAL().DeleteBrochureDetailsRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
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
        private bool FillControls(Int32 PKId)
        {
            try
            {
                PatientCareBrochureDetailsBO objbo = new PatientCareBrochureDetailsBO();
                PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
                objbo.BrochureId = PKId;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.GetPatintCareBrochureDetails(objbo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtTitle.Text = ds.Tables[0].Rows[0]["Title"].ToString();
                    imagename.InnerText = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                    filename.InnerText = ds.Tables[0].Rows[0]["FileUploadPath"].ToString();
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
            DataSet ds = new DataSet();            
            PatientCareBrochureDetailsBO objBo = new PatientCareBrochureDetailsBO();            
            objBo.TabTypeId = TabTypeId;
            objBo.SubTabId = SubTabId;
            objBo.LanguageId = LanguageId;
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            ds = objBAL.SelectBrochureDetailsRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gView.DataSource = ds;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(PatientCareBrochureDetailsBO objBo)
        {
            objBo.TabTypeId = TabTypeId;
            objBo.SubTabId = SubTabId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text.ToString();

            if (fuImageUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile(fuImageUpload, ConfigDetailsValue.PatientCareBrochureImageUploadPath);

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.ImagePath = documentfile;
            }
            else
            {
                objBo.ImagePath = imagename.InnerText.ToString();
            }

            if (fuFileUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile(fuFileUpload, ConfigDetailsValue.PatientCareBrochureFileUploadPath);

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.FileUploadPath = documentfile;
            }
            else
            {
                objBo.FileUploadPath = filename.InnerText.ToString();
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
                imagename.InnerText = "";
                filename.InnerText = "";
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
                PatientCareBrochureDetailsBO objBo = new PatientCareBrochureDetailsBO();
                LoadControls(objBo);
                if (new PatientCareMasterBAL().InsertBrochureDetailsRecord(objBo))
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
                PatientCareBrochureDetailsBO objBo = new PatientCareBrochureDetailsBO();
                LoadControls(objBo);
                objBo.BrochureId = Convert.ToInt16(hdPKId.Value.ToString());
                DataTable dt = new DataTable();
                if (new PatientCareMasterBAL().UpdateBrochureDetailsRecord(objBo))
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
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
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

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            PatientCareBrochureDetailsBO objBo = new PatientCareBrochureDetailsBO();
            objBo.TabTypeId = TabTypeId;
            objBo.SubTabId = SubTabId;
            objBo.LanguageId = LanguageId;
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            ds = objBAL.SelectBrochureDetailsRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "Title LIKE '%' + '" + txtSearch.Text + "' + '%' ";
                gView.DataSource = dv;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
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