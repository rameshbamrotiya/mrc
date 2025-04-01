using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class AffilationMaster : System.Web.UI.Page
    {
        #region Page Functions
        static string[] mediaExtensions = {
    ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF" //etc
};
        static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

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
                    ClearControlValues();
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
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            txtAffilationName.Text = "";
            CKEditorControl1.Text = "";

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";

            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
        }

        private void BindGridView()
        {
            //using (IAffilationRepository objAffilationRepository = new AffilationRepository(Functions.strSqlConnectionString))
            //{
            //    grdUser.DataSource = objAffilationRepository.GetAllAffilationMaster(1);
            //    grdUser.DataBind();
            //}
            grdUser.DataBind();
        }

        private bool LoadControlsAdd(GetAllAffilationMasterByLangIdResult objBo)
        {
            if (string.IsNullOrWhiteSpace(CKEditorControl1.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Vision Mission Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.AffilationDescription = CKEditorControl1.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(txtAffilationName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Affilation Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.AffilationName = txtAffilationName.Text.Trim();
            }
            bool isError = false;
            if (FUmainimg.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFileMainImg(out isError);

                if (!string.IsNullOrEmpty(documentfile) && !isError)
                {
                    objBo.ImagePath = documentfile;
                }
                else
                {
                    Functions.MessagePopup(this, documentfile, PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                objBo.ImagePath = hfLeftImage.Value;
            }
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }


            if (ddlLanguage.SelectedIndex > 0)
            {
                Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }

            objBo.IsVisible = chkIsActive.Checked;

            return true;
        }
        private string SaveFileMainImg(out bool isError)
        {
                isError = false;
            try
            {
                if (FUmainimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Affilation;
                    var fname = Path.GetExtension(FUmainimg.FileName);
                    var count = FUmainimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUmainimg.FileName.Split('.').Length; i++)
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
                        var filename1 = FUmainimg.FileName.Replace(" ", "_");

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
                        FUmainimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
            if (!Page.IsPostBack)
            {
                BindGridView();
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearControlValues();

                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IAffilationRepository objAffilationRepository = new AffilationRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objAffilationRepository.GetAffilationMasterByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (dataInfo != null)
                    {
                        txtAffilationName.Text = dataInfo.AffilationName;
                        CKEditorControl1.Text = dataInfo.AffilationDescription;

                        hfLeftImage.Value = dataInfo.ImagePath;
                        lblLeftImage.Text = dataInfo.ImagePath;

                        txtMetaTitle.Text = dataInfo.MetaTitle;
                        txtMetaDescription.Text = dataInfo.MetaDescription;
                        
                        ShowHideControl(VisibityType.Edit);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language First", PopupMessageType.warning);
                return;
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IAffilationRepository objAffilationRepository = new AffilationRepository(Functions.strSqlConnectionString))
            {
                GetAllAffilationMasterByLangIdResult objBO = new GetAllAffilationMasterByLangIdResult();
                if (LoadControlsAdd(objBO))
                {
                    if (!objAffilationRepository.InsertOrUpdateAffilationMaster(objBO, Convert.ToInt32(ddlLanguage.SelectedValue), out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IAffilationRepository objAffilationRepository = new AffilationRepository(Functions.strSqlConnectionString))
            {
                GetAllAffilationMasterByLangIdResult objBO = new GetAllAffilationMasterByLangIdResult();
                if (LoadControlsAdd(objBO))
                {
                    if (!FUmainimg.HasFile)
                    {
                        Functions.MessagePopup(this, "Please Select Main Image.", PopupMessageType.warning);
                        return;
                    }
                    if (!objAffilationRepository.InsertOrUpdateAffilationMaster(objBO, Convert.ToInt32(ddlLanguage.SelectedValue), out errorMessage))
                    {
                       
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }


        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {

            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                txtSearch.Text = "";
                BindGridView();
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }
        
        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IAffilationRepository objAcc = new AffilationRepository(Functions.strSqlConnectionString))
            {
                ddlLanguage.SelectedIndex = 1;
                var dataInfo = objAcc.GetAffilationMasterByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    ddlLanguage.Enabled = true;
                        txtAffilationName.Text = dataInfo.AffilationName;
                    CKEditorControl1.Text = dataInfo.AffilationDescription;

                    chkIsActive.Checked = dataInfo.IsVisible==null?false :(bool)dataInfo.IsVisible;

                    if (!string.IsNullOrWhiteSpace(dataInfo.ImagePath))
                    {
                        hfLeftImage.Value = dataInfo.ImagePath;
                        lblLeftImage.Text = dataInfo.ImagePath;
                        aRemoveLeft.Visible = true;
                    }

                    ShowHideControl(VisibityType.Edit);
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IAffilationRepository objAcc = new AffilationRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveAffilationMaster(rowId, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}