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
using System.Globalization;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class Awards : System.Web.UI.Page
    {

        public DataTable T017PDetails
        {
            get
            {
                DataTable dt = (DataTable)ViewState["T017PDetails"];
                if (dt != null)
                {
                    if (!(dt.Rows.Count > 0))
                    {
                        dt = new DataTable("T017PDetails");
                        dt.Columns.Add("Image_name");
                        dt.Columns.Add("Image_desc");
                        dt.Columns.Add("Language_id");
                        dt.Columns.Add("Is_active");
                        dt.Columns.Add("is_download");
                        dt.Columns.Add("Img_id");
                    }
                    else
                    {
                        return dt;
                    }
                }
                else
                {
                    dt = new DataTable("T017PDetails");
                    dt.Columns.Add("Image_name");
                    dt.Columns.Add("Image_desc");
                    dt.Columns.Add("Language_id");
                    dt.Columns.Add("Is_active");
                    dt.Columns.Add("is_download");
                    dt.Columns.Add("Img_id");
                }

                return dt;
            }
            set { ViewState["T017PDetails"] = value; }
        }

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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    T017PDetails = null;



                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();

                    ddlLanguage.SelectedIndex = 0;
                    BindCategoryByLang();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }

        }

        private void BindCategoryByLang()
        {
            CategoryBO objbo1 = new CategoryBO();
            CategoryBAL objBAL1 = new CategoryBAL();
            ddlType.DataSource = objBAL1.GetAllCategoryList(Convert.ToInt32(ddlLanguage.SelectedValue)).ToList();
            ddlType.DataTextField = "CategoryName";
            ddlType.DataValueField = "Recid";
            ddlType.DataBind();
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Award_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        AwardsBO objBo = new AwardsBO();
                        objBo.Award_id = bytID;
                        new AwardsBAL().DeleteRecord(objBo);
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
                            GridView1.Visible = true;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new AwardsBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        public void BindSubGridView(int iPkId, int languageId = 1)
        {
            AwardsBO objBo = new AwardsBO();
            objBo.Award_id = iPkId;
            objBo.LanguageId = languageId;
            DataSet dsimg = new AwardsBAL().SelectRecordIMG(objBo);

            T017PDetails = dsimg.Tables[0];

            DataRow[] drs = T017PDetails.Select("Language_id = " + languageId.ToString());
            DataTable dt;
            if (drs.Count() <= 0)
            {
                dt = new DataTable("T017PDetails");
                dt.Columns.Add("Image_name");
                dt.Columns.Add("Image_desc");
                dt.Columns.Add("Language_id");
                dt.Columns.Add("Is_active");
                dt.Columns.Add("is_download");
                dt.Columns.Add("Img_id");
                //T017PDetails = dt;
            }
            else
            {
                dt = drs.CopyToDataTable();
            }

            GridView1.DataSource = dt;

            GridView1.DataBind();
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            AwardsBO objBo = new AwardsBO();
            objBo.Award_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new AwardsBAL().SelectRecord(objBo);
            DataSet dsimg = new AwardsBAL().SelectRecordIMG(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtAlbumName.Text = "";
                txtalbumdesc.Text = "";
                txtAccredationDesc.Text = "";
                txtAwardShortDesc.Text = "";
                //txtMetaDesc.Text = "";
                //txtMetaTitle.Text = "";
                ddlType.SelectedIndex = 0;
                BindSubGridView(iPkId, languageId);
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                //T017PDetails = null;
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            //if (dsimg.Tables.Count.Equals(0) || dsimg.Tables[0].Rows.Count.Equals(0)) return false;
            //DataRow drimg = dsimg.Tables[0].Rows[0];
            //if (drimg.HasErrors) return false;
            //GridView1.Columns[3].Visible = true;
            //GridView1.Columns[4].Visible = true;
            BindSubGridView(iPkId, languageId);
            BindCategoryByLang();
            //foreach (GridViewRow gr in GridView1.Rows)
            //{
            //    DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
            //    dl.SelectedValue = gr.Cells[3].Text;
            //    DropDownList dlw = (DropDownList)gr.FindControl("ddlIsDownload");
            //    dlw.SelectedValue = gr.Cells[4].Text;
            //}
            //GridView1.Columns[3].Visible = false;
            //GridView1.Columns[4].Visible = false;
            if (dr["Album_name"] != DBNull.Value)
                txtAlbumName.Text = dr["Album_name"].ToString();
            if (dr["Type"] != DBNull.Value)
                ddlType.SelectedValue = dr["Type"].ToString();
            if (dr["Award_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Award_level_id"].ToString();
            if (dr["Album_desc"] != DBNull.Value)
                txtalbumdesc.Text = dr["Album_desc"].ToString();
            if (dr["AwardShortdesc"] != DBNull.Value)
                txtAwardShortDesc.Text = HttpUtility.HtmlDecode(dr["AwardShortdesc"].ToString());
            //if (dr["MetaDescription"] != DBNull.Value)
            //    txtMetaDesc.Text = dr["MetaDescription"].ToString();
            //if (dr["MetaTitle"] != DBNull.Value)
            //    txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["AccredationDesc"] != DBNull.Value)
                txtAccredationDesc.Text = dr["AccredationDesc"].ToString();
            if (dr["AwardMonthYear"] != DBNull.Value)
            {
                DateTime dt = DateTime.ParseExact(dr["AwardMonthYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
                txtAwardYear.Text = dt.Year.ToString();
                ddlMonth.SelectedValue = dt.ToString("MMM");
            }

            if (dr["is_active_tam"] != DBNull.Value)
                ddlActiveInactivealbum.SelectedValue = Convert.ToBoolean(dr["is_active_tam"]) ? "1" : "0";
            return true;
        }
        private void BindGridView()
        {

            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(AwardsBO objBo)
        {
            if (!string.IsNullOrEmpty(txtAlbumName.Text))
                objBo.Album_Name = txtAlbumName.Text;

            //if (string.IsNullOrWhiteSpace(txtalbumdesc.Text.Trim()))
            //{
            //    Functions.MessagePopup(this, "Please enter Award Description.", PopupMessageType.error);
            //    return;
            //}
            //else
            //{
            //    //if (!string.IsNullOrEmpty(txtalbumdesc.Text))
            //    objBo.Album_desc = txtalbumdesc.Text;
            //}
            if (!string.IsNullOrEmpty(txtAwardShortDesc.Text))
                objBo.ShortDescription = HttpUtility.HtmlEncode(txtAwardShortDesc.Text);
            //if (!string.IsNullOrEmpty(txtMetaTitle.Text))
            //    objBo.MetaTitle = (txtMetaTitle.Text);
            //if (!string.IsNullOrEmpty(txtMetaDesc.Text))
            //    objBo.MetaDescription = (txtMetaDesc.Text);
            //if (string.IsNullOrWhiteSpace(txtAccredationDesc.Text.Trim()))
            //{
            //    Functions.MessagePopup(this, "Please enter Accredation Description.", PopupMessageType.error);
            //    return;
            //}
            //else
            //{
            //    //if (!string.IsNullOrEmpty(txtAccredationDesc.Text))
            //    objBo.AccredationDesc = (txtAccredationDesc.Text);
            //}
            if (!string.IsNullOrEmpty(txtAwardYear.Text))
            {
                objBo.AwardMonthYear = ddlMonth.SelectedValue + " " + txtAwardYear.Text;
            }
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Award_level_id = Convert.ToInt32(txtsequence.Text);

            objBo.Type = ddlType.SelectedValue;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active_album = ddlActiveInactivealbum.SelectedValue.ToString();
            objBo.Is_active_img = ddlActiveInactiveimg.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new AwardsBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Award_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Award_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }


        private bool SaveImageSingleOnly()
        {
            bool isError = false;
            if (fuDocUpload.HasFile)
            {
                GridView1.Visible = true;


                string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
                if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                {
                    Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                    return isError;
                }
                else
                {

                    DataTable dt = T017PDetails;
                    DataRow dr = dt.NewRow();
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (height != 450 && width != 570)
                    //{
                    //    Functions.MessagePopup(this, "Please upload 595px*470px.", PopupMessageType.error);
                    //    return isError;
                    //}
                    //else
                    {
                        string filepath = "";

                        if (fuDocUpload.HasFile)
                        {
                            string documentfile = string.Empty;
                            documentfile = SaveFile(out isError);

                            if (!string.IsNullOrEmpty(documentfile) && !isError)
                            {
                                filepath = documentfile;
                                isError = true;
                            }
                            else
                            {
                                Functions.MessagePopup(this, documentfile, PopupMessageType.error);
                                return isError;
                            }
                        }

                        dr["Image_name"] = fuDocUpload.FileName;
                        dr["Image_desc"] = filepath;
                    }
                    dr["Language_id"] = ddlLanguage.SelectedValue;
                    dr["Is_active"] = ddlActiveInactiveimg.SelectedValue;
                    dr["is_download"] = ddlisddownload.SelectedValue;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    T017PDetails = dt;

                    DataRow[] drs = T017PDetails.Select("Language_id = " + ddlLanguage.SelectedValue);
                    DataTable dt1;
                    if (drs.Count() <= 0)
                    {
                        dt1 = new DataTable("T017PDetails");
                        dt1.Columns.Add("Image_name");
                        dt1.Columns.Add("Image_desc");
                        dt1.Columns.Add("Language_id");
                        dt1.Columns.Add("Is_active");
                        dt1.Columns.Add("is_download");
                        dt1.Columns.Add("Img_id");
                        //T017PDetails = dt;
                    }
                    else
                    {
                        dt1 = drs.CopyToDataTable();
                    }

                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                }
            }
            else
            {
                //Functions.MessagePopup(this, "Please Select File!!!", PopupMessageType.error);
                ////ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                //return isError;
            }
            return isError;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    if (SaveImageSingleOnly())
                    {

                        AwardsBO objBo = new AwardsBO();
                        LoadControls(objBo);
                        DataTable dt = new DataTable();
                        dt = T017PDetails;
                        if (dt.Rows.Count > 0)
                        {
                            if (new AwardsBAL().InsertRecord(objBo, dt))
                            {
                                Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                                T017PDetails = null;
                                GridView1.DataSource = T017PDetails;
                                GridView1.DataBind();
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                                return;
                            }
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Add Images.", PopupMessageType.warning);
                        }
                    }
                }
                else
                {

                    AwardsBO objBo = new AwardsBO();
                    LoadControls(objBo);
                    DataTable dt = new DataTable();
                    dt = T017PDetails;
                    if (dt.Rows.Count > 0)
                    {
                        if (new AwardsBAL().InsertRecord(objBo, dt))
                        {
                            Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                            T017PDetails = null;
                            GridView1.DataSource = T017PDetails;
                            GridView1.DataBind();
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                            return;
                        }
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please Add Images.", PopupMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    if (SaveImageSingleOnly())
                    {

                        AwardsBO objBo = new AwardsBO();
                        LoadControls(objBo);
                        objBo.Img_id = Convert.ToInt32(ViewState["PKImg"]);
                        objBo.Award_id = Convert.ToInt32(ViewState["PK"]);
                        DataTable dt = new DataTable();
                        dt = T017PDetails;
                        if (new AwardsBAL().UpdateRecord(objBo, dt))
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

                    AwardsBO objBo = new AwardsBO();
                    LoadControls(objBo);
                    objBo.Img_id = Convert.ToInt32(ViewState["PKImg"]);
                    objBo.Award_id = Convert.ToInt32(ViewState["PK"]);
                    DataTable dt = new DataTable();
                    dt = T017PDetails;
                    if (new AwardsBAL().UpdateRecord(objBo, dt))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                T017PDetails = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private string SaveFile(out bool isError)
        {
            isError = false;
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
                        isError = true;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    GridView1.Visible = false;
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

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            bool isError = false;
            if (fuDocUpload.HasFile)
            {
                GridView1.Visible = true;


                string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
                if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                {
                    Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                    return;
                }
                else
                {
                    DataTable dt = T017PDetails;
                    DataRow dr = dt.NewRow();
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (height != 450 && width != 570)
                    //{
                    //    Functions.MessagePopup(this, "Please upload 595px*470px.", PopupMessageType.error);
                    //    return;
                    //}
                    //else
                    {
                        string filepath = "";

                        if (fuDocUpload.HasFile)
                        {
                            string documentfile = string.Empty;
                            documentfile = SaveFile(out isError);

                            if (!string.IsNullOrEmpty(documentfile) && !isError)
                            {
                                filepath = documentfile;
                                isError = true;
                            }
                            else
                            {
                                Functions.MessagePopup(this, documentfile, PopupMessageType.error);
                                return;
                            }
                        }

                        dr["Image_name"] = fuDocUpload.FileName;
                        dr["Image_desc"] = filepath;
                    }
                    dr["Language_id"] = ddlLanguage.SelectedValue;
                    dr["Is_active"] = ddlActiveInactiveimg.SelectedValue;
                    dr["is_download"] = ddlisddownload.SelectedValue;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    T017PDetails = dt;

                    DataRow[] drs = T017PDetails.Select("Language_id = " + ddlLanguage.SelectedValue);
                    DataTable dt1;
                    if (drs.Count() <= 0)
                    {
                        dt1 = new DataTable("T017PDetails");
                        dt1.Columns.Add("Image_name");
                        dt1.Columns.Add("Image_desc");
                        dt1.Columns.Add("Language_id");
                        dt1.Columns.Add("Is_active");
                        dt1.Columns.Add("is_download");
                        dt1.Columns.Add("Img_id");
                        //T017PDetails = dt;
                    }
                    else
                    {
                        dt1 = drs.CopyToDataTable();
                    }

                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                }
            }
            else
            {

                Functions.MessagePopup(this, "Please Select  File !!!", PopupMessageType.error);
                //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                return;
            }
        }

        protected string FormatImageUrl(string url)
        {

            if (url != null && url.Length > 0)

                return ("~/" + url);

            else return null;

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(GridView1.DataKeys[intIndex].Values["Img_id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = T017PDetails as DataTable;
                        dt.Rows[index].Delete();
                        T017PDetails = dt;

                        DataRow[] drs = T017PDetails.Select("Language_id = " + ddlLanguage.SelectedValue);
                        DataTable dt1;
                        if (drs.Count() <= 0)
                        {
                            dt1 = new DataTable("T017PDetails");
                            dt1.Columns.Add("Image_name");
                            dt1.Columns.Add("Image_desc");
                            dt1.Columns.Add("Language_id");
                            dt1.Columns.Add("Is_active");
                            dt1.Columns.Add("is_download");
                            dt1.Columns.Add("Img_id");
                            //T017PDetails = dt;
                        }
                        else
                        {
                            dt1 = drs.CopyToDataTable();
                        }

                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                    }
                    else
                    {

                        Int32 bytID;

                        bytID = Convert.ToInt32(GridView1.DataKeys[intIndex].Values["Img_id"]);

                        if (e.CommandName == "eDelete")
                        {
                            //PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
                            DataTable dt = T017PDetails as DataTable;

                            dt.Rows[intIndex].Delete();
                            T017PDetails = dt;
                            //objBo.Album_id = bytID;
                            //new PhotoGalleryMasterBAL().DeleteRecordImg(objBo);
                            BindGridView();

                            DataRow[] drs = T017PDetails.Select("Language_id = " + ddlLanguage.SelectedValue);
                            DataTable dt1;
                            if (drs.Count() <= 0)
                            {
                                dt1 = new DataTable("T017PDetails");
                                dt1.Columns.Add("Image_name");
                                dt1.Columns.Add("Image_desc");
                                dt1.Columns.Add("Language_id");
                                dt1.Columns.Add("Is_active");
                                dt1.Columns.Add("is_download");
                                dt1.Columns.Add("Img_id");
                                //T017PDetails = dt;
                            }
                            else
                            {
                                dt1 = drs.CopyToDataTable();
                            }

                            GridView1.DataSource = dt1;
                            GridView1.DataBind();

                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        ClearControlValues(pnlEntry);
                        if (FillControlsimg(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                        {
                            if (e.CommandName == "eView")
                                ShowHideControl(VisibityType.View);
                            if (e.CommandName == "eEdit")
                            {
                                ViewState["PKImg"] = bytID;
                                ShowHideControl(VisibityType.Edit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private bool FillControlsimg(Int32 iPkId, int languageId)
        {
            PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
            objBo.Img_id = iPkId;

            DataTable dt = T017PDetails;

            DataRow dr = dt.Rows[0];
            if (dr.HasErrors) return false;
            if (dr["Album_name"] != DBNull.Value)
                txtAlbumName.Text = dr["Album_name"].ToString();
            if (dr["Album_desc"] != DBNull.Value)
                txtalbumdesc.Text = dr["Album_desc"].ToString();
            if (dr["is_active_tam"] != DBNull.Value)
                ddlActiveInactivealbum.SelectedValue = Convert.ToBoolean(dr["is_active_tam"]) ? "1" : "0";
            return true;
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (GridView1.DataKeys[rowindex]["Img_id"].ToString());
            GridViewRow gvRow = GridView1.Rows[rowindex];

            DataTable dt = T017PDetails;
            //dt = GetListValueDoctor;
            if (string.IsNullOrWhiteSpace(rowId))
            {
                DataRow dr = dt.Rows[rowindex];
                dr.Delete();
            }
            else
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["Img_id"].ToString() == rowId)
                        dr.Delete();
                }
            }
            dt.AcceptChanges();


            Functions.MessagePopup(this, "Record Removed Done", PopupMessageType.success);

            T017PDetails = dt;

            GridView1.Columns[2].Visible = true;

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}