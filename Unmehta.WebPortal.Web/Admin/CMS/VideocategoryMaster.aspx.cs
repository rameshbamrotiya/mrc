using System;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class VideocategoryMaster : System.Web.UI.Page
    {
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

                    btn_Update.Visible = false;
                    hfID.Value = "0";
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:

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
        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnCatID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));

        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (futhumbnill.HasFile)
                {
                    string magePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(futhumbnill.FileName);

                    string fileName = Path.GetFileName(magePath);

                    FileInfo fi = new FileInfo(fileName);

                    string ext = fi.Extension.ToLower();
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {

                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return;

                    }
                }
                VideocategoryBO objBo = new VideocategoryBO();
                LoadControls(objBo);
                objBo.VCID = Convert.ToInt16(hdnCatID.Value);
                if (new VideocategoryBAL().UpdateRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);

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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

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
            ShowHideControl(VisibityType.Insert);
        }
        private void FillControls(int VCID, int languageid)
        {
            try
            {
                VideocategoryBO objbo = new VideocategoryBO();
                VideocategoryBAL objBAL = new VideocategoryBAL();
                objbo.VCID = VCID;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(VCID).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectBlockDetailsById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtVideoCategoryname.Text = dr["VideoCategoryName"].ToString();
                    string ThumbimgPath = dr["ThumbnillPath"].ToString();
                    Thumbnillpath.InnerText = ThumbimgPath.ToString();
                    hdnRecid.Value = dr["VCid"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    if (dr["TagList"] != DBNull.Value)
                        txtTagList.Text = dr["TagList"].ToString();
                    else
                        txtTagList.Text = "";
                }
                else
                {
                    ClearControlValues(pnlEntry);
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
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["VCID"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageID"].ToString()));
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            VideocategoryBO objBo = new VideocategoryBO();
            objBo.VCID = Convert.ToInt32(grdDetails.DataKeys[rowindex]["VCID"].ToString());
            new VideocategoryBAL().DeleteRecord(objBo);
            BindGridView();
            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
            return;
        }
        private void BindGridView()
        {
            grdDetails.DataBind();
        }
        private void LoadControls(VideocategoryBO objBo)
        {
            objBo.Enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.VideoCategoryName = txtVideoCategoryname.Text.Trim();
            if (futhumbnill.HasFile)
            {
                string ThumbnillPath = string.Empty;
                ThumbnillPath = SaveFile(futhumbnill);
                if (!string.IsNullOrEmpty(ThumbnillPath))
                    objBo.ThumbnillPath = ThumbnillPath;
            }
            else
            {
                objBo.ThumbnillPath = Thumbnillpath.InnerText.ToString();
            }
            if (!string.IsNullOrEmpty(txtTagList.Text))
                objBo.TagList = txtTagList.Text;

        }
        private string SaveFile(FileUpload fuUpload)
        {
            try
            {
                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.VideoAlbumThumbnailPath;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {

                if (futhumbnill.HasFile)
                {
                    string magePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(futhumbnill.FileName);

                    string fileName = Path.GetFileName(magePath);

                    FileInfo fi = new FileInfo(fileName);

                    string ext = fi.Extension.ToLower();
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {

                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return;

                    }
                }
                VideocategoryBO objBo = new VideocategoryBO();
                LoadControls(objBo);
                if (new VideocategoryBAL().InsertRecord(objBo))
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
    }
}