using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Common;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class EquipmentMaster : System.Web.UI.Page
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
                    BindOtherSpeciality();
                    BindSubService();
                    aRemovefilename.Visible = false;
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Equipment_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        EquipmentMasterBO objBo = new EquipmentMasterBO();
                        objBo.Equipment_id = bytID;
                        new EquipmentMasterBAL().DeleteRecord(objBo);
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
            if (new EquipmentMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindOtherSpeciality();
            BindSubService();
            EquipmentMasterBO objBo = new EquipmentMasterBO();
            objBo.Equipment_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new EquipmentMasterBAL().SelectRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtDesignation.Text = "";
                txtStaffname.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            //ViewState["T017PDetails"] = ds.Tables[0];
            if (dr["designation"] != DBNull.Value)
                txtDesignation.Text = dr["designation"].ToString();
            //if (dr["SSM_id"] != DBNull.Value)
            //    ddlSubService.SelectedValue = Convert.ToString(dr["SSM_id"]);
            if (dr["OS_id"] != DBNull.Value)
                ddlOtherSpeciality.SelectedValue = Convert.ToString(dr["OS_id"]);
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            if (dr["Equipment_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Equipment_level_id"].ToString();
            if (dr["Staffname"] != DBNull.Value)
                txtStaffname.Text = dr["Staffname"].ToString();
            string imgPath = dr["Img_path"].ToString();

            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                hffilename.Value = imgPath.ToString();
                lblfilename.Text = imgPath.ToString();
                aRemovefilename.Visible = true;
            }
            else
            {
                hffilename.Value = "";
                lblfilename.Text ="";
                aRemovefilename.Visible = false;
            }

            return true;
        }
        private void BindGridView()
        {
            BindOtherSpeciality();
            //BindSubService();
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(EquipmentMasterBO objBo) 
        {
            if (!string.IsNullOrEmpty(txtDesignation.Text))
                objBo.Designation = txtDesignation.Text;
              if (!string.IsNullOrEmpty(txtStaffname.Text))
                objBo.Staffname = txtStaffname.Text;
            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Img_path = documentfile;
            }
            else
            {
                objBo.Img_path = hffilename.Value.ToString();
            }

            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Equipment_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.OS_id = ddlOtherSpeciality.SelectedValue.ToString();
            //objBo.SSM_id = ddlSubService.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new EquipmentMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Equipment_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Equipment_level_id"].ToString();
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
                        EquipmentMasterBO objBo = new EquipmentMasterBO();
                        LoadControls(objBo);
                        if (new EquipmentMasterBAL().InsertRecord(objBo))
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
                    EquipmentMasterBO objBo = new EquipmentMasterBO();
                    LoadControls(objBo);
                    if (new EquipmentMasterBAL().InsertRecord(objBo))
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
                        EquipmentMasterBO objBo = new EquipmentMasterBO();
                        LoadControls(objBo);
                        objBo.Equipment_id = Convert.ToInt32(ViewState["PK"]);
                        if (new EquipmentMasterBAL().UpdateRecord(objBo))
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
                    EquipmentMasterBO objBo = new EquipmentMasterBO();
                    LoadControls(objBo);
                    objBo.Equipment_id = Convert.ToInt32(ViewState["PK"]);
                    if (new EquipmentMasterBAL().UpdateRecord(objBo))
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
                    var DocumentUpload = ConfigDetailsValue.ImageUploadPath;
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
                    hffilename.Value = "";
                    lblfilename.Text = "";
                    aRemovefilename.Visible = false;
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    lblfilename.Visible = true;
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;

                    hffilename.Value = "";
                    lblfilename.Text = "";
                    aRemovefilename.Visible = false;

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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        private void BindOtherSpeciality()
        {
            ddlOtherSpeciality.Items.Clear();
            EquipmentMasterBO objBo = new EquipmentMasterBO();
            int languageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.LanguageId = languageId;
            EquipmentMasterBAL objBal = new EquipmentMasterBAL();
            ddlOtherSpeciality.DataSource = objBal.SelectRecordOtherSpeciality(objBo);
            ddlOtherSpeciality.DataTextField = "Name";
            ddlOtherSpeciality.DataValueField = "Id";
            ddlOtherSpeciality.DataBind();
            ddlOtherSpeciality.Items.Insert(0, new ListItem("Select", "-1"));
        }
        private void BindSubService()
        {
            //ddlSubService.Items.Clear();
            //EquipmentMasterBO objBo = new EquipmentMasterBO();
            //int languageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            //objBo.LanguageId = languageId;
            //EquipmentMasterBAL objBal = new EquipmentMasterBAL();
            //ddlSubService.DataSource = objBal.SelectRecordSubService(objBo);
            //ddlSubService.DataTextField = "Name";
            //ddlSubService.DataValueField = "Id";
            //ddlSubService.DataBind();
            //ddlSubService.Items.Insert(0, new ListItem("Select", "-1"));
        }

        protected void btnAddToList_Click(object sender, EventArgs e)
        {

        }
    }
}