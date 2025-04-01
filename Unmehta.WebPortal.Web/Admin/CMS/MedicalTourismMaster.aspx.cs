using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class MedicalTourismMaster : System.Web.UI.Page
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
                aRemoveInner.Visible = false;
                lblInnerImage.Text = "";
                hfInnerImage.Value = "";

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
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;

                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";

                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    lblInnerImage.Visible = false;
                    break;
                case VisibityType.Edit:

                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";

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
        private void LoadControls(MedicalTourismBO objBo)
        {
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.MTDescription = HttpUtility.HtmlEncode(txtDescription.Text.ToString());
            objBo.MTInnerVideolink = txtvideolink.Text;
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            if (FUinnerimg.HasFile)
            {
                string documentfileinnerimg = string.Empty;
                documentfileinnerimg = SaveFileInnerimg();
                if (!string.IsNullOrEmpty(documentfileinnerimg))
                    objBo.MTInnerImgpath = documentfileinnerimg;
            }
            else
            {
                objBo.MTInnerImgpath = hfInnerImage.Value; //Session["FUinnerimg"].ToString();
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                MedicalTourismBO objBo = new MedicalTourismBO();
                LoadControls(objBo);
                if (new MedicalTourismBAL().InsertRecord(objBo))
                {

                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";

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
                MedicalTourismBO objBo = new MedicalTourismBO();
                LoadControls(objBo);
                objBo.MT_Id = Convert.ToInt16(hdPKId.Value.ToString());
                if (new MedicalTourismBAL().UpdateRecord(objBo))
                {

                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";

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
            aRemoveInner.Visible = false;
            lblInnerImage.Text = "";
            hfInnerImage.Value = "";

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
                MedicalTourismBO objbo = new MedicalTourismBO();
                MedicalTourismBAL objBAL = new MedicalTourismBAL();
                objbo.MT_Id = PKId;
                objbo.LanguageId = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectRecord(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtDescription.Text = HttpUtility.HtmlDecode(dr["MTDescription"].ToString());
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    drpLanguage.SelectedValue = dr["Language_id"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["Is_active"]) ? "1" : "0";
                    if (!string.IsNullOrWhiteSpace(dr["MTInnerImgpath"].ToString()))
                    {
                        lblInnerImage.Text = dr["MTInnerImgpath"].ToString();
                        aRemoveInner.Visible = true;
                        hfInnerImage.Value = lblInnerImage.Text;
                    }
                    txtvideolink.Text = dr["MTInnerVideolink"].ToString();
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
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["MT_Id"].ToString());
                MedicalTourismBO objBo = new MedicalTourismBO();
                objBo.MT_Id = rowId;
                new MedicalTourismBAL().DeleteRecord(objBo);
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

        protected void lnkAddAccredationDetails_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int MTDetailsId = Convert.ToInt32(gView.DataKeys[rowindex]["Id"].ToString());
            int LanguageId = Convert.ToInt32(gView.DataKeys[rowindex]["Language_id"].ToString());
            string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("Id=" + MTDetailsId + "|LanguageId=" + LanguageId));
            Response.Redirect("~/Admin/CMS/MedicalTourismDocumentMaster.aspx?" + strdQuery, false);
        }
        private string SaveFileInnerimg()
        {
            try
            {
                if (FUinnerimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.MedicalTourism;
                    var fname = Path.GetExtension(FUinnerimg.FileName);
                    var count = FUinnerimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUinnerimg.FileName.Split('.').Length; i++)
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
                        var filename1 = FUinnerimg.FileName.Replace(" ", "_");
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
                        FUinnerimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        //byte[] imageByteArray = (byte[])Session["FUinnerimg"];
                        //File.WriteAllBytes(Server.MapPath(DocumentUpload) + filename1, imageByteArray);
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
    }
}