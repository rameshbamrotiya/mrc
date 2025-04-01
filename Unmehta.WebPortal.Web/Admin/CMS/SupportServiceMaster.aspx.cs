using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class SupportServiceMaster : System.Web.UI.Page
    {
        #region Page Methods

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
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
            }
        }
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
                    //SSImgfilename.Visible = false;
                    SSIconFilename.Visible = false;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    DataDetails = null;
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    //SSImgfilename.Visible = true;
                    SSIconFilename.Visible = true;
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
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }

                SupportServiceMasterBO objbo = new SupportServiceMasterBO();
                LoadControlsAdd(objbo);
                long SSId = 0;
                if (new SupportServiceMasterBAL().InsertRecord(objbo,out SSId))
                {
                    objbo.SSId = (int)SSId;
                    new SupportServiceMasterBAL().DeleteSupportImage(objbo.SSId);
                    foreach(DataRow row in DataDetails.Rows)
                    {
                        new SupportServiceMasterBAL().InsertSupportImage(objbo.SSId, row["ImagePath"].ToString());
                    }
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
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
                    var DocumentUpload = ConfigDetailsValue.SupportService;
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

        private void LoadControlsAdd(SupportServiceMasterBO objBo)
        {
            objBo.Language = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtSSName.Text))
                objBo.SSName = txtSSName.Text;

            //if (fuSSImg.HasFile)
            //{
            //    string SchmeLogo = string.Empty;
            //    SchmeLogo = SaveFile(fuSSImg);

            //    if (!string.IsNullOrEmpty(SchmeLogo))
            //        objBo.SSImg = SchmeLogo;
            //}
            //else
            {
                objBo.SSImg = "";
            }
            if (fuSSIcon.HasFile)
            {
                string SSIcon = string.Empty;
                SSIcon = SaveFile(fuSSIcon);

                if (!string.IsNullOrEmpty(SSIcon))
                    objBo.SSIcon = SSIcon;
            }
            else
            {
                objBo.SSIcon = SSIconFilename.InnerText.ToString();
            }
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.SS_level_id = Convert.ToInt32(txtsequence.Text);
            //if (!string.IsNullOrEmpty(txtMetaTitle.Text))
            //    objBo.MetaTitle = txtMetaTitle.Text;
            //if (!string.IsNullOrEmpty(txtMetaDesc.Text))
            //    objBo.MetaDescription = txtMetaDesc.Text;
            objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text.ToString());
            objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;



        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataDetails = null;
                ClearSubForm();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        private void BindGridView()
        {
            grdSupportService.DataBind();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
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

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            DataDetails = null;
            ClearSubForm();
            ShowHideControl(VisibityType.Insert);
            DataSet ds = new SupportServiceMasterBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["SS_level_id"] != DBNull.Value)
                txtsequence.Text = drs["SS_level_id"].ToString();
        }

        private void FillControls(int SSId, int languageid)
        {
            try
            {
                SupportServiceMasterBO objbo = new SupportServiceMasterBO();
                SupportServiceMasterBAL objBAL = new SupportServiceMasterBAL();
                objbo.SSId = SSId;
                objbo.Language = languageid;
                hdnSSID.Value = Convert.ToInt16(SSId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectSupportserviceByID(objbo);
                DataTable dts = objBAL.GetAllSupportImageDetails(SSId);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0 && dts!=null)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtSSName.Text = dr["SSname"].ToString();
                    //SSImgfilename.InnerText = dr["SSImg"].ToString();
                    SSIconFilename.InnerText = dr["SSIcon"].ToString();
                    hdnRecid.Value = dr["recid"].ToString();
                    //txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    //txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    txtsequence.Text = dr["SS_level_id"].ToString();

                    DataDetails = dts;
                    BindSubGridView();
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    SSIconFilename.InnerText = "";
                    //SSImgfilename.InnerText = "";
                    drpLanguage.SelectedValue = languageid.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }


        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                hdnRecid.Value = grdSupportService.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdSupportService.DataKeys[rowindex]["SSId"].ToString()), Convert.ToInt16(grdSupportService.DataKeys[rowindex]["Languageid"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            SupportServiceMasterBO objBo = new SupportServiceMasterBO();
            LoadControlsAdd(objBo);
            objBo.SSId = Convert.ToInt16(hdnSSID.Value);
            if (new SupportServiceMasterBAL().UpdateRecord(objBo))
            {
                //if (new SupportServiceMasterBAL().InsertRecord(objBo))
                {
                    new SupportServiceMasterBAL().DeleteSupportImage(objBo.SSId);
                    foreach (DataRow row in DataDetails.Rows)
                    {
                        new SupportServiceMasterBAL().InsertSupportImage(objBo.SSId, row["ImagePath"].ToString());
                    }
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                BindGridView();
            }
            else
            {
                Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                return;
            }
            //BindGridView();
            ShowHideControl(VisibityType.GridView);

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnSSID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));

        }

        protected void grdSupportService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(grdSupportService.DataKeys[intIndex].Values["SSId"]);
                    if (e.CommandName == "eDelete")
                    {
                        SupportServiceMasterBO objBo = new SupportServiceMasterBO();
                        objBo.SSId = bytID;
                        new SupportServiceMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
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
            if (new SupportServiceMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        #endregion

        #region Page Sub Methods

        public System.Data.DataTable DataDetails
        {
            get
            {
                DataTable value = (DataTable)ViewState["DataDetails"];
                if (value == null)
                {
                    DataTable dt = new DataTable("tbl");
                    dt.Columns.Add("ImagePath");
                    value = dt;
                }
                else if (value.Rows.Count <= 0)
                {

                    DataTable dt = new DataTable("tbl");
                    dt.Columns.Add("ImagePath");
                    value = dt;
                }
                return (System.Data.DataTable)value;
            }
            set
            {
                ViewState["DataDetails"] = value;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    {
                        if (e.CommandName == "eDelete")
                        {
                            SpecialityMasterBO objBo = new SpecialityMasterBO();
                            DataTable dt = DataDetails;

                            DataRow dtTo = dt.Rows[intIndex];
                            dt.Rows.Remove(dtTo);
                            dt.AcceptChanges();

                            DataDetails = dt;

                            ClearSubForm();

                            //int OSID = Convert.ToInt32(GridView1.DataKeys[intIndex].Values["OS_id"]);
                            //FillControls(OSID, Convert.ToInt32(ddlLanguage.SelectedValue));
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);

                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            DataTable dt = DataDetails;

            //else
            {
                DataRow dr;
                if (hfRowId.Value == "0" && hfCommand.Value == "0")
                {
                    dr = dt.NewRow();
                }
                else
                {
                    dr = dt.Rows[Convert.ToInt32(hfRowId.Value)];
                }

                {
                    string filepath = "";
                    if (fuDocUpload.HasFile)
                    {
                        string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
                        if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                        {
                            Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                            return;
                        }

                        filepath = SaveFile();
                    }
                    else
                    {
                        filepath = filename.InnerText;
                    }
                    //dr["OS_id"] = hfTemplateId.Value;
                    dr["Imagepath"] = filepath;
                }

                if (hfRowId.Value == "0" && hfCommand.Value == "0")
                {
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

                DataDetails = dt;

                ClearSubForm();
            }
        }

        private void ClearSubForm()
        {
            hfRowId.Value = "0";
            filename.InnerText = "";
            BindSubGridView();

        }

        private void BindSubGridView()
        {
            DataTable dt = DataDetails;

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        private string SaveFile()
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
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
    }
}