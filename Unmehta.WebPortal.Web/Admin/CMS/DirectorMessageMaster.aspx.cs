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
    public partial class DirectorMessageMaster : System.Web.UI.Page
    {
        #region ShowHideControl || Notification
        //protected void btn_SearchCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BindGridView();
        //        txtSearch.Text = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //        throw ex;
        //    }
        //}

        private void BindGridView()
        {
            LanguageMasterBAL objBo = new LanguageMasterBAL();
            ddlLanguage.DataSource = objBo.GetAllLanguage();
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "Id";
            ddlLanguage.DataBind();
            gvDetail.DataBind();
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    MultiView1.ActiveViewIndex = -1;

                    break;
                case VisibityType.View:
                    //pnlView.Visible = false;
                    //pnlEntry.Visible = true;
                    ////BtnSave.Visible = false;
                    ////BtnUpdate.Visible = false;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    filename.Visible = false;
                    hfDMId.Value = "0";
                    pnlEntry.Visible = true;
                    BtnSave.Visible = true;
                    BtnUpdate.Visible = false;
                    MultiView1.ActiveViewIndex = 0;
                    ClearControlValues(pnlEntry);
                    CKEditorControl1.Text = string.Empty;
                    ddlLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    BtnSave.Visible = false;
                    BtnUpdate.Visible = true;
                    MultiView1.ActiveViewIndex = 0;
                    ddlLanguage.Enabled = true;
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    MultiView1.ActiveViewIndex = 0;
                    break;
            }
            BindGridView();
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        #region Page Functions
        private bool FillControls(int iPkId, int languageId)
        {
            DirectorMessageBO objBo = new DirectorMessageBO();
            objBo.DMId = iPkId;
            hfDMId.Value = iPkId.ToString();
            ViewState["DMId"] = objBo.DMId;
            var FormData = new DirectorMessageBAL().GetDMMasterByIdLangId(objBo.DMId, languageId).FirstOrDefault();
            if (FormData == null)
            {

                hfDMId.Value = iPkId.ToString();
                filename.InnerText = "";
                CKEditorControl1.Text = "";
                txtMetaDesc.Text = "";
                txtMetaTitle.Text = "";
                cbEnable.Checked = false;
                return false;
            }

            if (!string.IsNullOrWhiteSpace(FormData.DMId.ToString()) || FormData.DMId != 0)
            {
                hfDMId.Value = FormData.DMId.ToString();
            }
            if (!string.IsNullOrWhiteSpace(FormData.DOCPath.ToString()))
            {
                filename.Visible = true;
                filename.InnerText = FormData.DOCPath.ToString();
            }
            if (!string.IsNullOrWhiteSpace(FormData.MetaDescription.ToString()))
            {
                txtMetaDesc.Text = FormData.MetaDescription.ToString();
            }
            if (!string.IsNullOrWhiteSpace(FormData.MetaTitle.ToString()))
            {
                txtMetaTitle.Text = FormData.MetaTitle.ToString();
            }

            if (!string.IsNullOrWhiteSpace(FormData.DirectorMesshtmlContent.ToString()))
            {
                CKEditorControl1.Text = HttpUtility.HtmlDecode(FormData.DirectorMesshtmlContent.ToString());
            }
            if (!string.IsNullOrWhiteSpace(FormData.enabled.ToString()))
            {
                cbEnable.Checked = Convert.ToBoolean(FormData.enabled);

            }

            return true;
        }

        private void LoadControls(DirectorMessageBO objBo)
        {

            if (!string.IsNullOrEmpty(hfDMId.Value))
                objBo.DMId = Convert.ToInt32(hfDMId.Value);
            else
                objBo.DMId = 0;

            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.DOCPath = documentfile;
            }
            else
            {
                objBo.DOCPath = filename.InnerText.ToString();
            }


            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                objBo.DirectorMesshtmlContent = HttpUtility.HtmlEncode(CKEditorControl1.Text);
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);

            objBo.enabled = cbEnable.Checked;

            objBo.ip_add = GetIPAddress;
        }
        #endregion

        #region Page Event
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
                    //if (Convert.ToString(hfActiveView.Value) == null)
                    //{
                    ShowHideControl(VisibityType.GridView);
                    //}
                    BindGridView();
                }
                //}
                //else
                //Response.Redirect("LoginPage.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
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
                throw ex;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DirectorMessageBO objBO = new DirectorMessageBO();
                LoadControls(objBO);
                if (new DirectorMessageBAL().Insert_Update_DirectorMessageMaster(objBO))
                {
                    ShowHideControl(VisibityType.GridView);
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DirectorMessageBO objBO = new DirectorMessageBO();
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DMId"])))
                    objBO.DMId = Convert.ToInt32(ViewState["DMId"]);
                LoadControls(objBO);
                if (new DirectorMessageBAL().Insert_Update_DirectorMessageMaster(objBO))
                {
                    ShowHideControl(VisibityType.GridView);
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gvDetail.DataKeys[intIndex].Values["DMId"]);
                    if (e.CommandName == "eDelete")
                    {
                        DirectorMessageBO objBo = new DirectorMessageBO();
                        objBo.DMId = bytID;
                        if (new DirectorMessageBAL().Delete_DirectorMessage(objBo.DMId))
                        {
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        }
                        BindGridView();
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
                throw ex;

            }
        }
        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        private string SaveFile()
        {
            try
            {

                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.DirectorMessageUploadPath;
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
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfDMId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        #endregion
    }
}