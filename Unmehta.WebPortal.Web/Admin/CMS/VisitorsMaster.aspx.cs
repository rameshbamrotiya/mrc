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
using Unmehta.WebPortal.Common;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class VisitorsMaster : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            try
            {
                if (!Page.IsPostBack)
                {

                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;

                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        #endregion
        private void BindDataByLanguageId(long lgLangId)
        {
            if (lgLangId != 0)
            {
                VisitorsMasterBAL objBal = new VisitorsMasterBAL();
                DataSet ds = new DataSet();
                ds = objBal.SelectRecord(lgLangId);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    ddlLanguage.Enabled = true;
                    hfTemplateId.Value = dr["VisitorId"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtMetaDescription.Text = dr["MetaDescription"].ToString();
                    txtVisitingHoursdesc.Text = HttpUtility.HtmlDecode(dr["VisitingHoursDesc"].ToString());
                    txtDDDesc.Text = HttpUtility.HtmlDecode(dr["DDDescription"].ToString());
                    ddlActiveInactiveOS.SelectedValue = Convert.ToBoolean(dr["IsActive"]) ? "1" : "0";
                    long VisitorId = Convert.ToInt32(hfTemplateId.Value);
                    ds = objBal.SelectRecordFacility(VisitorId, lgLangId);
                    DataTable dtFacility = new DataTable();
                    dtDetails = ds.Tables[0];
                    GridView1.DataSource = dtDetails;
                    GridView1.DataBind();
                }
            }
            else
            {
                txtVisitingHoursdesc.Text = "";
                txtDDDesc.Text = "";
                if (!string.IsNullOrWhiteSpace(hfTemplateId.Value) && hfTemplateId.Value != "0")
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                ClearSubControlValues();
                ClearControlValues();
            }
        }
        private void ClearSubControlValues()
        {

            hfInnerImage.Value = "";
            lblInnerImage.Text = "";
            aRemoveInner.Visible = false;

            txtImgTitle.Text = "";
            txtPopupdesc.Text = "";
            hfId.Value = "0";
            ddlActiveInactiveimg.SelectedIndex = 0;
            btnAddToList.Text = "Add To List";
        }

        private void ClearControlValues()
        {
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            txtDDDesc.Text = "";
            txtVisitingHoursdesc.Text = "";
            ClearSubControlValues();
        }

        #region Save || Update || Cancel
        private void LoadControls(VisitorsMasterBO objBo)
        {
            if (string.IsNullOrEmpty(txtMetaTitle.Text))
            {
                Functions.MessagePopup(this, "Please enter MetaTitle.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.MetaTitle = txtMetaTitle.Text;
            }
            if (string.IsNullOrWhiteSpace(txtMetaDescription.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Short Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.MetaDescription = txtMetaDescription.Text;
            }

            if (string.IsNullOrWhiteSpace(txtVisitingHoursdesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter VisitingHours Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.VisitingHoursDesc = HttpUtility.HtmlEncode(txtVisitingHoursdesc.Text).ToString();
            }
            if (string.IsNullOrWhiteSpace(txtDDDesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Do’s & Don’ts Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.DDDescription = HttpUtility.HtmlEncode(txtDDDesc.Text).ToString();
            }
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactiveOS.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.VisitorId = 0;
            }
            else
            {
                objBo.VisitorId = Convert.ToInt32(hfTemplateId.Value);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorsMasterBO objBo = new VisitorsMasterBO();
                LoadControls(objBo);
                if (new VisitorsMasterBAL().InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControlValues();
                BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private string SaveFile()
        {
            try
            {
                if (fuIcon.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.VisitorImg;
                    var fname = Path.GetExtension(fuIcon.FileName);
                    var count = fuIcon.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuIcon.FileName.Split('.').Length; i++)
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
                        var filename1 = fuIcon.FileName.Replace(" ", "_");
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
                        fuIcon.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }
        #endregion

        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorsMasterBO objBo = new VisitorsMasterBO();
                LoadControlsSub(objBo);
                if (new VisitorsMasterBAL().InsertRecordFacilities(objBo))
                {
                    ddlLanguage.Enabled = false;
                    ClearSubControlValues();
                    BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    txtImgTitle.Focus();
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.error);
                    return;
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this,ex.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private void LoadControlsSub(VisitorsMasterBO objBo)
        {
            if(string.IsNullOrWhiteSpace(txtVisitingHoursdesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Add Title.", PopupMessageType.error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPopupdesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Popup Description.", PopupMessageType.error);
                return;
            }
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value) && hfTemplateId.Value == "0")
            {
                Functions.MessagePopup(this, "Please Save Governing Board Details First", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.VisitorId = Convert.ToInt32(hfTemplateId.Value);
            }

            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Img_id = 0;
            }
            else
            {
                objBo.Img_id = Convert.ToInt32(hfId.Value);
            }
            if (!string.IsNullOrEmpty(txtImgTitle.Text))
                objBo.ImgTitle = txtImgTitle.Text;
            if (!string.IsNullOrEmpty(txtPopupdesc.Text))
            {
                objBo.ImgPOPUpDesc = HttpUtility.HtmlEncode(txtPopupdesc.Text);
            }
            if (fuIcon.HasFile)
            {
                string SubImage = string.Empty;
                SubImage = SaveFile();
                if (!string.IsNullOrEmpty(SubImage))
                    objBo.Iconpath = SubImage;
            }
            else
            {
                objBo.Iconpath = hfInnerImage.Value.ToString();
            }
            objBo.Is_activeImg = ddlActiveInactiveimg.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            hfInnerImage.Value = "";
            lblInnerImage.Text = "";
            aRemoveInner.Visible = false;


            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfId.Value = GridView1.DataKeys[rowindex]["Id"].ToString();
            VisitorsMasterBAL objBal = new VisitorsMasterBAL();
            DataSet ds = new DataSet();
            ds = objBal.SelectRecordFacilityEdit(Convert.ToInt32(hfId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
            DataTable dtDetails = new DataTable();
            dtDetails = ds.Tables[0];
            if (dtDetails.Rows.Count > 0)
            {
                DataRow dr = dtDetails.Rows[0];
                txtImgTitle.Text = dr["ImgTitle"].ToString();
                txtPopupdesc.Text = HttpUtility.HtmlDecode(dr["ImgPOPUpDesc"].ToString());

                if (!string.IsNullOrWhiteSpace(dr["Iconpath"].ToString()))
                {
                    hfInnerImage.Value = dr["Iconpath"].ToString();
                    lblInnerImage.Text = dr["Iconpath"].ToString();
                    aRemoveInner.Visible = true;
                }
                ddlActiveInactiveimg.SelectedValue = dr["IsActive"].ToString();
                btnAddToList.Text = "Update To List";
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(GridView1.DataKeys[rowindex]["Id"].ToString());
                new VisitorsMasterBAL().DeleteRecord(rowId, Convert.ToInt32(SessionWrapper.UserDetails.Id));
                ddlLanguage.Enabled = false;
                ClearSubControlValues();
                BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
                Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}