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
    public partial class CovidCareMaster : System.Web.UI.Page
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
                FillLanguage();
                BindGridView();
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

                    hfMainImage.Value = "";
                    lblMainImage.Text = "";
                    aRemoveMain.Visible = false;

                    hfLeftVideoImage.Value = "";
                    lblLeftVideoImage.Text = "";
                    aRemoveLeftVideo.Visible = false;

                    hfLeftThumbnailImage.Value = "";
                    lblLeftThumbnailImage.Text = "";
                    aRemoveLeftThumbnail.Visible = false;

                    hfRightVideoImage.Value = "";
                    lblRightVideoImage.Text = "";
                    aRemoveRightVideo.Visible = false;

                    hfRightThumbnailImage.Value = "";
                    lblRightThumbnailImage.Text = "";
                    aRemoveRightThumbnail.Visible = false;

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
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
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void LoadControls(CovidCareMasterBO objBo)
        {
            objBo.Language_Id = Convert.ToInt16(drpLanguage.SelectedValue.ToString());  
            if(!string.IsNullOrEmpty(txtTitle.Text))
            {
                objBo.Title = txtTitle.Text;
            }
            else
            {
                objBo.Title = "";
            }
            if (fuImageUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile(fuImageUpload, ConfigDetailsValue.CovidCareImageUploadPath);

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.ImageUploadPath = documentfile;
            }
            else
            {
                objBo.ImageUploadPath = hfMainImage.Value;
            }            
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            objBo.Description = HttpUtility.HtmlEncode(txtDescription.Text.ToString());

            objBo.Left_Link_Video_Upload = Convert.ToInt32(ddlLeftvideoupload.SelectedValue);

            if (ddlLeftvideoupload.SelectedValue == "0")
            {
                if (!string.IsNullOrEmpty(txtLeftExternallink.Text))
                    objBo.LeftVideoPath = txtLeftExternallink.Text;
            }
            else
            {
                if (fuLeftDocUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile(fuLeftDocUpload, ConfigDetailsValue.CovidCareVideoUploadPath);
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.LeftVideoPath = documentfile;
                }
                else
                {
                    objBo.LeftVideoPath = hfLeftVideoImage.Value.ToString();
                }                
            }
            if (fuLeftthumbnill.HasFile)
            {
                string ThumbnillPath = string.Empty;
                ThumbnillPath = SaveFile(fuLeftthumbnill, ConfigDetailsValue.CovidCareThumbnailUploadPath);
                if (!string.IsNullOrEmpty(ThumbnillPath))
                    objBo.LeftVideoThumbnailPath = ThumbnillPath;
            }
            else
            {
                objBo.LeftVideoThumbnailPath = hfLeftThumbnailImage.Value.ToString();
            }

            objBo.Right_Link_Video_Upload = Convert.ToInt32(ddlRightvideoupload.SelectedValue);


            if (ddlRightvideoupload.SelectedValue == "0")
            {
                if (!string.IsNullOrEmpty(txtRightVideoLink.Text))
                    objBo.RightVideoPath = txtRightVideoLink.Text;
            }
            else
            {
                if (fuRightVideoUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile(fuRightVideoUpload, ConfigDetailsValue.CovidCareVideoUploadPath);
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.RightVideoPath = documentfile;
                }
                else
                {
                    objBo.RightVideoPath = hfRightVideoImage.Value.ToString();
                }                
            }

            if (fuRightthumbnill.HasFile)
            {
                string ThumbnillPath = string.Empty;
                ThumbnillPath = SaveFile(fuRightthumbnill, ConfigDetailsValue.CovidCareThumbnailUploadPath);
                if (!string.IsNullOrEmpty(ThumbnillPath))
                    objBo.RightVideoThumbnailPath = ThumbnillPath;
            }
            else
            {
                objBo.RightVideoThumbnailPath = hfRightThumbnailImage.Value.ToString();
            }

            if (!string.IsNullOrEmpty(txtFAQsTitle.Text))
            {
                objBo.FAQsTitle = txtFAQsTitle.Text;
            }
            else
            {
                objBo.FAQsTitle = "";
            }

            if (fuFAQsImageUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile(fuFAQsImageUpload, ConfigDetailsValue.CovidCareImageUploadPath);

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.FAQsImageUploadPath = documentfile;
            }
            else
            {
                objBo.FAQsImageUploadPath = lblFAQsImageUploadPath.InnerText.ToString();
            }

            if (!string.IsNullOrEmpty(txtFAQsAccreditationTitle.Text))
            {
                objBo.FAQsAccreditationTitle = txtFAQsAccreditationTitle.Text;
            }
            else
            {
                objBo.FAQsAccreditationTitle = "";
            }
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        private string SaveFile(FileUpload fuUpload, string FilePath)
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
                CovidCareMasterBO objBo = new CovidCareMasterBO();
                LoadControls(objBo);
                if (new CovidCareMasterBAL().InsertRecord(objBo))
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
        private void BindGridView()
        {
            gView.DataBind();
            if (gView.Rows.Count == 0)
            {
                ShowHideControl(VisibityType.Insert);
            }
            else
            {
                ShowHideControl(VisibityType.GridView);
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                CovidCareMasterBO objBo = new CovidCareMasterBO();
                LoadControls(objBo);
                objBo.CovidCare_Id = Convert.ToInt16(hdPKId.Value.ToString());
                if (new CovidCareMasterBAL().UpdateRecord(objBo))
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                ClearControlValues(pnlEntry);
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

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdPKId.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void FillControls(int PKId, int Languageid)
        {
            try
            {
                CovidCareMasterBO objbo = new CovidCareMasterBO();
                CovidCareMasterBAL objBAL = new CovidCareMasterBAL();
                objbo.CovidCare_Id = PKId;
                objbo.Language_Id = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectRecord(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    drpLanguage.SelectedValue = dr["Language_id"].ToString();
                    txtTitle.Text = dr["Title"].ToString();

                    if (!string.IsNullOrWhiteSpace(dr["ImageUploadPath"].ToString()))
                    {
                        hfMainImage.Value = dr["ImageUploadPath"].ToString();
                        lblMainImage.Text = dr["ImageUploadPath"].ToString();
                        aRemoveMain.Visible = true;
                    }
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    txtDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    ddlLeftvideoupload.SelectedValue = dr["Left_Link_Video_Upload"].ToString();

                    if (!string.IsNullOrWhiteSpace(dr["LeftVideoPath"].ToString()))
                    {
                        hfLeftVideoImage.Value = dr["LeftVideoPath"].ToString();
                        lblLeftVideoImage.Text = dr["LeftVideoPath"].ToString();
                        aRemoveLeftVideo.Visible = true;
                    }
                    if (!string.IsNullOrWhiteSpace(dr["LeftVideoThumbnailPath"].ToString()))
                    {
                        hfLeftThumbnailImage.Value = dr["LeftVideoThumbnailPath"].ToString();
                        lblLeftThumbnailImage.Text = dr["LeftVideoThumbnailPath"].ToString();
                        aRemoveLeftThumbnail.Visible = true;
                    }
                    if (!string.IsNullOrWhiteSpace(dr["RightVideoPath"].ToString()))
                    {
                        hfRightVideoImage.Value = dr["RightVideoPath"].ToString();
                        lblRightVideoImage.Text = dr["RightVideoPath"].ToString();
                        aRemoveRightVideo.Visible = true;
                    }
                    if (!string.IsNullOrWhiteSpace(dr["RightVideoThumbnailPath"].ToString()))
                    {
                        hfRightThumbnailImage.Value = dr["RightVideoThumbnailPath"].ToString();
                        lblRightThumbnailImage.Text = dr["RightVideoThumbnailPath"].ToString();
                        aRemoveRightThumbnail.Visible = true;
                    }

                    if (ddlLeftvideoupload.SelectedValue == "0")
                    {
                        divLeftExternallink.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "block";
                        divLeftInternalvideo.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        divLeftExternallink.Attributes.Add("visibility", "visible");
                        divLeftInternalvideo.Attributes.Add("visibility", "hidden");
                        rfvLeftExternallink.Enabled = true;
                    }
                    else
                    {
                        divLeftExternallink.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        divLeftInternalvideo.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "block";
                        divLeftExternallink.Attributes.Add("visibility", "hidden");
                        divLeftInternalvideo.Attributes.Add("visibility", "visible");
                        rfvLeftExternallink.Enabled = false;
                    }

                    if (ddlRightvideoupload.SelectedValue == "0")
                    {
                        divRightExternallink.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "block";
                        divRightInternalvideo.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        divRightExternallink.Attributes.Add("visibility", "visible");
                        divRightInternalvideo.Attributes.Add("visibility", "hidden");
                        rfvRightVideoLink.Enabled = true;
                    }
                    else
                    {
                        divRightExternallink.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
                        divRightInternalvideo.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "block";
                        divRightExternallink.Attributes.Add("visibility", "hidden");
                        divRightInternalvideo.Attributes.Add("visibility", "visible");
                        rfvRightVideoLink.Enabled = false;
                    }

                    txtFAQsTitle.Text = dr["FAQsTitle"].ToString();
                    lblFAQsImageUploadPath.InnerText = dr["FAQsImageUploadPath"].ToString();
                    txtFAQsAccreditationTitle.Text = dr["FAQsAccreditationTitle"].ToString();
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = Languageid.ToString();
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
                FillControls(Convert.ToInt16(gView.DataKeys[rowindex]["Id"].ToString()), Convert.ToInt16(gView.DataKeys[rowindex]["Language_id"].ToString()));
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["CovidCare_Id"].ToString());
                CovidCareMasterBO objBo = new CovidCareMasterBO();
                objBo.CovidCare_Id = rowId;
                new CovidCareMasterBAL().DeleteRecord(objBo);
                Functions.MessagePopup(this, "Record deleted successfully..", PopupMessageType.success);
                BindGridView();
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }        

        protected void lnkAddAccredation_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int CovidCareDetailsId = Convert.ToInt32(gView.DataKeys[rowindex]["Id"].ToString());
                int LanguageId = Convert.ToInt32(gView.DataKeys[rowindex]["Language_id"].ToString());
                string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("Id=" + CovidCareDetailsId + "|LanguageId=" + LanguageId));
                Response.Redirect("~/Admin/CMS/CovidCareAccredationMaster.aspx?" + strdQuery, false);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        //protected void ddlLeftvideoupload_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(ddlLeftvideoupload.SelectedValue== "0")
        //    {
        //        divLeftExternallink.Visible = true;
        //        divLeftInternalvideo.Visible = false;
        //        rfvLeftExternallink.Enabled = true;
        //    }
        //    else
        //    {
        //        divLeftExternallink.Visible = false;
        //        divLeftInternalvideo.Visible = true;
        //        rfvLeftExternallink.Enabled = false;
        //    }
        //}

        //protected void ddlRightvideoupload_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlRightvideoupload.SelectedValue == "0")
        //    {
        //        divRightExternallink.Visible = true;
        //        divRightInternalvideo.Visible = false;
        //        rfvRightVideoLink.Enabled = true;
        //    }
        //    else
        //    {
        //        divRightExternallink.Visible = false;
        //        divRightInternalvideo.Visible = true;
        //        rfvRightVideoLink.Enabled = false;
        //    }
        //}
    }
}