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
using Unmehta.WebPortal.Common;
using System.IO;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class SubServiceMaster : System.Web.UI.Page
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Speciality_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        SubServiceMasterBO objBo = new SubServiceMasterBO();
                        objBo.Speciality_id = bytID;
                        new SubServiceMasterBAL().DeleteRecord(objBo);
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindOtherSpeciality();
            SubServiceMasterBO objBo = new SubServiceMasterBO();
            objBo.Speciality_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new SubServiceMasterBAL().SelectRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //txtSubServiceName.Text = "";
                CKEditorControl1.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            ViewState["T017PDetails"] = ds.Tables[0];
            //if (dr["SubService_Name"] != DBNull.Value)
            //    txtSubServiceName.Text = dr["SubService_Name"].ToString();
            if (dr["Description"] != DBNull.Value)
                CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
            if (dr["OS_id"] != DBNull.Value)
                ddlOtherSpeciality.SelectedValue = Convert.ToString(dr["OS_id"]);
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            string imgPath = dr["Img_path"].ToString();

            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                aRemoveLeft.Visible = true;
                hfLeftImage.Value = imgPath.ToString();
                lblLeftImage.Text = imgPath.ToString();
            }
            else
            {
                aRemoveLeft.Visible = false;
                hfLeftImage.Value = "";
                lblLeftImage.Text = "";
            }

            return true;
        }
        private void BindGridView()
        {
            BindOtherSpeciality();
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(SubServiceMasterBO objBo)
        {
            //if (!string.IsNullOrEmpty(txtSubServiceName.Text))
            //    objBo.SubService_Name = txtSubServiceName.Text;
            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text);
            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Img_path = documentfile;
            }
            else
            {
                objBo.Img_path = hfLeftImage.Value;
            }


            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.OS_id = ddlOtherSpeciality.SelectedValue.ToString();
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
                if (fuDocUpload.HasFile)
                {
                    //if (fuDocUpload.PostedFile.ContentLength > 209715200)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 200 mb.", PopupMessageType.success);
                    //    return;
                    //}
                    //else
                    {
                        SubServiceMasterBO objBo = new SubServiceMasterBO();
                        LoadControls(objBo);
                        if (new SubServiceMasterBAL().InsertRecord(objBo))
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
                    SubServiceMasterBO objBo = new SubServiceMasterBO();
                    LoadControls(objBo);
                    if (new SubServiceMasterBAL().InsertRecord(objBo))
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
                        SubServiceMasterBO objBo = new SubServiceMasterBO();
                        LoadControls(objBo);
                        objBo.Speciality_id = Convert.ToInt32(ViewState["PK"]);
                        if (new SubServiceMasterBAL().UpdateRecord(objBo))
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
                    SubServiceMasterBO objBo = new SubServiceMasterBO();
                    LoadControls(objBo);
                    objBo.Speciality_id = Convert.ToInt32(ViewState["PK"]);
                    if (new SubServiceMasterBAL().UpdateRecord(objBo))
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
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        private void BindOtherSpeciality()
        {
            ddlOtherSpeciality.Items.Clear();
            SubServiceMasterBO objBo = new SubServiceMasterBO();
            int languageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.LanguageId = languageId;
            SubServiceMasterBAL objBal = new SubServiceMasterBAL();
            ddlOtherSpeciality.DataSource = objBal.SelectRecordOtherSpeciality(objBo);
            ddlOtherSpeciality.DataTextField = "Name";
            ddlOtherSpeciality.DataValueField = "Id";
            ddlOtherSpeciality.DataBind();
            ddlOtherSpeciality.Items.Insert(0, new ListItem("Select", "-1"));
        }
    }
}