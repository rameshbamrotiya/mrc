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
    public partial class AccredationMaster : System.Web.UI.Page
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
                    ViewState["T017PDetails"] = null;
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    ddlLanguage.SelectedValue = "1";
                    FillControls(Convert.ToInt32(ddlLanguage.SelectedValue));
                    BindGridView();
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
        private void BindYear()
        {
            ddlAccredationYear.Items.Clear();

            for (int i = 1960; i <= DateTime.Now.Year; i++)
            {
                ddlAccredationYear.Items.Add(i.ToString());
            }
        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.GridView);
                txtSearch.Text = "";
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["AM_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        AccredationMasterBO objBo = new AccredationMasterBO();
                        objBo.AM_id = bytID;
                        new AccredationMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    //ClearControlValues(pnlEntry);
                    if (FillControlssub(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
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
        private bool FillControls(int languageId)
        {
            AccredationMasterBO objBo = new AccredationMasterBO();
            objBo.LanguageId = languageId;
            DataSet ds = new AccredationMasterBAL().SelectRecord(languageId);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtMetaTitle.Text = "";
                txtAcc_Title.Text = "";
                txtMetaDesc.Text = "";
                hfId.Value = "0";
                txtAccredationDesc.Text = "";
                ddlIsVisible.SelectedIndex = -1;
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Acc_id"] != DBNull.Value)
                hfId.Value = dr["Acc_id"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["Accredation_Title"] != DBNull.Value)
                txtAcc_Title.Text = dr["Accredation_Title"].ToString();
            if (dr["Accredation_desc"] != DBNull.Value)
                txtAccredationDesc.Text = HttpUtility.HtmlDecode(dr["Accredation_desc"].ToString());

            bool IsDisplayInFooter;
            if (bool.TryParse(dr["IsDisplayInFooter"].ToString(), out IsDisplayInFooter))
            {
                chkIsDisplayInFooter.Checked = IsDisplayInFooter;
            }
            else
            {
                chkIsDisplayInFooter.Checked = false;
            }
            bool IsDisplayInHeader;
            if (bool.TryParse(dr["IsDisplayInHeader"].ToString(), out IsDisplayInHeader))
            {
                chkIsDisplayInHeader.Checked = IsDisplayInHeader;
            }
            else
            {
                chkIsDisplayInHeader.Checked = false;
            }

            if(!string.IsNullOrWhiteSpace(dr["ImgLogo"].ToString()))
            {
                string imgPathImgLogo = dr["ImgLogo"].ToString();
                lblLeftImage.Text = imgPathImgLogo.ToString();
                hfLeftImage.Value = imgPathImgLogo.ToString();
                aRemoveLeft.Visible = true;
            }

            if (!string.IsNullOrWhiteSpace(dr["Img_path"].ToString()))
            {
                string imgPath = dr["Img_path"].ToString();
                lblRightImage.Text = imgPath.ToString();
                hfRightImage.Value = imgPath.ToString();
                aRemoveRight.Visible = true;
            }
            

            ddlIsVisible.SelectedValue = Convert.ToBoolean(dr["IsVisible"]) ? "1" : "0";
            return true;
        }
        private bool FillControlssub(int AMid, int languageId)
        {
            AccredationMasterBO objBo = new AccredationMasterBO();
            objBo.AM_id = AMid;
            hfTemplateId.Value = AMid.ToString();
            DataSet ds = new AccredationMasterBAL().SelectRecordsubselect(AMid, languageId);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                hfTemplateId.Value = "0";
                txtAccredationName.Text = "";
                txtADescription.Text = "";
                txtdate.Text = "";
                ddlMonth.SelectedIndex = 0;
                ddlAccredationYear.SelectedIndex = 0;
                chkIsDisplayInHeader.Checked = false;
                chkIsDisplayInFooter.Checked = false;
                ddlActiveInactive.SelectedIndex = 0;
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            if (dr["Accredation_Name"] != DBNull.Value)
                txtAccredationName.Text = dr["Accredation_Name"].ToString();
            if (dr["Accredation_Description"] != DBNull.Value)
                txtADescription.Text = dr["Accredation_Description"].ToString();
            if (dr["Ac_Date"] != DBNull.Value)
                txtdate.Text = dr["Ac_Date"].ToString();

            if (dr["Accredation_MonthYear"] != DBNull.Value)
            {
                DateTime dt = DateTime.ParseExact(dr["Accredation_MonthYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
                ddlAccredationYear.SelectedValue = dt.Year.ToString();
                ddlMonth.SelectedValue = dt.ToString("MMM");
            }
            if (dr["Is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["Is_active"]) ? "1" : "0";
            return true;
        }
        private void BindGridView()
        {
            AccredationMasterBO objBo = new AccredationMasterBO();
            objBo.Acc_id = Convert.ToInt32(hfId.Value);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            AccredationMasterBAL objbal = new AccredationMasterBAL();
            DataSet ds = objbal.SelectAccredationDetails(objBo);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Accredation_Name"] = (dt.Rows[0]["Accredation_Name"].ToString());
                dt.AcceptChanges();
                gView.DataSource = dt;
                gView.DataBind();
            }
            else
            {
                gView.DataBind();
            }
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(AccredationMasterBO objBo)
        {

            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Acc_id = 0;
            }
            else
            {
                objBo.Acc_id = Convert.ToInt32(hfId.Value);
            }
            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Img_Path = documentfile;
            }
            else
            {
                objBo.Img_Path = hfRightImage.Value;
            }

            if (fuLogo.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFileLogo();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.ImgLogo = documentfile;
            }
            else
            {
                objBo.ImgLogo = hfLeftImage.Value;
            }

            if (!string.IsNullOrEmpty(txtAcc_Title.Text))
                objBo.Accredation_Title = (txtAcc_Title.Text);
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = (txtMetaTitle.Text);
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = (txtMetaDesc.Text);
            if (string.IsNullOrWhiteSpace(txtAccredationDesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Accredation Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.AccredationDesc = HttpUtility.HtmlEncode(txtAccredationDesc.Text.ToString());
            }
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.IsDisplayInFooter = chkIsDisplayInFooter.Checked;
            objBo.IsDisplayInHeader = chkIsDisplayInHeader.Checked;
            objBo.AccredationURL = txtAccredationURL.Text;
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.IsVisible = ddlIsVisible.SelectedValue.ToString();
            objBo.ip_add = GetIPAddress;
        }
        private void LoadControlssub(AccredationMasterBO objBo)
        {

            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.Acc_id = 0;
            }
            else
            {
                objBo.Acc_id = Convert.ToInt32(hfTemplateId.Value);
            }
            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.AM_id = 0;
            }
            else
            {
                objBo.AM_id = Convert.ToInt32(hfId.Value);
            }
            if (!string.IsNullOrEmpty(txtAccredationName.Text))
                objBo.Accredation_Name = txtAccredationName.Text;
            if (!string.IsNullOrEmpty(txtADescription.Text))
                objBo.Accredation_Description = txtADescription.Text;
            if (!string.IsNullOrEmpty(txtdate.Text))
                objBo.date = txtdate.Text;
            if (ddlMonth.SelectedIndex > 0 && ddlAccredationYear.SelectedIndex > 0)
            {
                objBo.Accredation_MonthYear = ddlMonth.SelectedValue + " " + ddlAccredationYear.SelectedValue;
            }
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
                AccredationMasterBO objBo = new AccredationMasterBO();
                LoadControls(objBo);
                objBo.AM_id = Convert.ToInt32(ViewState["PK"]);
                if (new AccredationMasterBAL().InsertRecord(objBo))
                {
                    ViewState["PK"] = objBo.Acc_id;
                    hfId.Value = objBo.Acc_id.ToString();
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    BindGridView();

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                //BindGridView();

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
                    var DocumentUpload = ConfigDetailsValue.AccredationImg;
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
        private string SaveFileLogo()
        {
            try
            {
                if (fuLogo.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AccredationImg;
                    var fname = Path.GetExtension(fuLogo.FileName);
                    var count = fuLogo.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuLogo.FileName.Split('.').Length; i++)
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
                        var filename1 = fuLogo.FileName.Replace(" ", "_");
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
                        fuLogo.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
                    hfId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }
        #endregion

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccredationName.Text))
                {
                    Functions.MessagePopup(this, "Please enter accredation name.", PopupMessageType.error);
                    return;
                }
                if (string.IsNullOrEmpty(txtADescription.Text))
                {
                    Functions.MessagePopup(this, "Please enter accredation description.", PopupMessageType.error);
                    return;
                }
                AccredationMasterBO objBo = new AccredationMasterBO();
                LoadControlssub(objBo);
                //objBo.AM_id = Convert.ToInt32(hfId.Value);
                if (objBo.AM_id > 0)
                {
                    if (new AccredationMasterBAL().InsertRecordsub(objBo))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        txtAccredationName.Text = "";
                        txtADescription.Text = "";
                        txtdate.Text = "";
                        hfTemplateId.Value = "0";
                        //ViewState["PK"] = 0;
                        ddlMonth.SelectedIndex = 0;
                        ddlAccredationYear.SelectedIndex = 0;
                        ddlMonth.SelectedIndex = 0;
                        ddlActiveInactive.SelectedIndex = 0;
                        BindGridView();
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Save Upper Details First.", PopupMessageType.success);
                    return;
                }
                BindGridView();

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(ddlLanguage.SelectedValue));
            BindGridView();
            txtAccredationName.Text = "";
            txtADescription.Text = "";
            txtdate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlAccredationYear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlActiveInactive.SelectedIndex = 0;
            chkIsDisplayInFooter.Checked = false;
            chkIsDisplayInHeader.Checked = false;
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void gview_Accredation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gview_Accredation.DataKeys[intIndex].Values["Acc_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        AccredationMasterBO objBo = new AccredationMasterBO();
                        objBo.Acc_id = bytID;
                        new AccredationMasterBAL().DeleteRecordAccredationDesc(objBo);
                        gview_Accredation.DataBind();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControlAccredations(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            BindGridView();
                            gview_Accredation.Visible = true;
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

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControlAccredations(int bytID, int languageId)
        {
            AccredationMasterBO objBo = new AccredationMasterBO();
            objBo.LanguageId = languageId;
            objBo.Acc_id = bytID;
            DataSet ds = new AccredationMasterBAL().SelectRecordAccredation(languageId, bytID);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtMetaTitle.Text = "";
                txtAcc_Title.Text = "";
                txtMetaDesc.Text = "";
                hfId.Value = "0";

                lblLeftImage.Text = "";
                hfLeftImage.Value = "";
                lblRightImage.Text = "";
                hfRightImage.Value ="";
                aRemoveRight.Visible = false;
                aRemoveLeft.Visible = false;
                txtAccredationDesc.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Acc_id"] != DBNull.Value)
                hfId.Value = dr["Acc_id"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["Accredation_Title"] != DBNull.Value)
                txtAcc_Title.Text = dr["Accredation_Title"].ToString();
            if (dr["Accredation_desc"] != DBNull.Value)
                txtAccredationDesc.Text = HttpUtility.HtmlDecode(dr["Accredation_desc"].ToString());

            if (dr["AccredationURL"] != DBNull.Value)
                txtAccredationURL.Text = dr["AccredationURL"].ToString();


            bool IsDisplayInFooter;
            if (bool.TryParse(dr["IsDisplayInFooter"].ToString(), out IsDisplayInFooter))
            {
                chkIsDisplayInFooter.Checked = IsDisplayInFooter;
            }
            else
            {
                chkIsDisplayInFooter.Checked = false;
            }
            bool IsDisplayInHeader;
            if (bool.TryParse(dr["IsDisplayInHeader"].ToString(), out IsDisplayInHeader))
            {
                chkIsDisplayInHeader.Checked = IsDisplayInHeader;
            }
            else
            {
                chkIsDisplayInHeader.Checked = false;
            }


            if (dr["IsVisible"] != DBNull.Value)
            {
                ddlIsVisible.SelectedValue = Convert.ToBoolean(dr["IsVisible"]) ? "1" : "0";
            }

            string imgPathImgLogo = dr["ImgLogo"].ToString();
            lblLeftImage.Text = imgPathImgLogo.ToString();
            hfLeftImage.Value = imgPathImgLogo.ToString();

            string imgPath = dr["Img_path"].ToString();
            lblRightImage.Text = imgPath.ToString();
            hfRightImage.Value = imgPath.ToString();


            if (!string.IsNullOrEmpty(imgPath))
            {
                aRemoveRight.Visible = true;
            }

            if (!string.IsNullOrEmpty(imgPathImgLogo))
            {
                aRemoveLeft.Visible = true;
            }

            return true;
        }

        protected void btnfinalsave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                ShowHideControl(VisibityType.GridView);
                gview_Accredation.DataBind();
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void gview_Accredation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gview_Accredation.PageIndex = e.NewPageIndex;
            gview_Accredation.DataBind();
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                gview_Accredation.DataBind();
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
    }
}