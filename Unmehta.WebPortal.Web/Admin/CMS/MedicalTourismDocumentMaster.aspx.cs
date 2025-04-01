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
    public partial class MedicalTourismDocumentMaster : System.Web.UI.Page
    {
        public static int Id, LanguageId;
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
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                string[] splitString = queryString.Split('|');
                Id = Convert.ToInt32(splitString[0].ToString().Replace("Id=", ""));
                LanguageId = Convert.ToInt32(splitString[1].ToString().Replace("LanguageId=", ""));
                BindGridView();
                ShowHideControl(VisibityType.GridView);
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
                    lblInnerImage.Visible = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    lblInnerImage.Visible = false;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    lblInnerImage.Visible = true;
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
        private void LoadControls(MedicalTourismDocumentBO objBo)
        {
            objBo.MTDetails_Id = Id;
            objBo.Language_id = LanguageId;
            if (!string.IsNullOrEmpty(txtDocumentTitle.Text))
                objBo.DocumentTitle = txtDocumentTitle.Text;
            else
                objBo.DocumentTitle = "";
            if (FUinnerimg.HasFile)
            {
                string documentfileinnerimg = string.Empty;
                documentfileinnerimg = SaveFileInnerimg();
                if (!string.IsNullOrEmpty(documentfileinnerimg))
                    objBo.DocPath = documentfileinnerimg;
            }
            else
            {
                objBo.DocPath = lblInnerImage.Text; //Session["FUinnerimg"].ToString();
            }
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
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
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                MedicalTourismDocumentBO objBo = new MedicalTourismDocumentBO();
                LoadControls(objBo);
                if (new MedicalTourismBAL().InsertMTDocRecord(objBo))
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
            DataSet ds = new DataSet();
            MedicalTourismDocumentBO objBo = new MedicalTourismDocumentBO();
            objBo.MTDetails_Id = Id;
            objBo.Language_id = LanguageId;
            MedicalTourismBAL objBAL = new MedicalTourismBAL();
            ds = objBAL.SelectMTDocRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gView.DataSource = ds;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                MedicalTourismDocumentBO objBo = new MedicalTourismDocumentBO();
                LoadControls(objBo);
                objBo.MTDOC_Id = Convert.ToInt16(hdPKId.Value.ToString());
                if (new MedicalTourismBAL().UpdateMTDocRecord(objBo))
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
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MedicalTourismDocumentBO objBo = new MedicalTourismDocumentBO();
            objBo.MTDetails_Id = Id;
            objBo.Language_id = LanguageId;
            MedicalTourismBAL objBAL = new MedicalTourismBAL();
            ds = objBAL.SelectMTDocRecord(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "DocumentTitle LIKE '%' + '" + txtSearch.Text + "' + '%' ";
                gView.DataSource = dv;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }
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
            FillControls(Convert.ToInt32(hdPKId.Value), Convert.ToInt32(LanguageId));
        }
        private void FillControls(int PKId, int Languageid)
        {
            try
            {
                MedicalTourismDocumentBO objbo = new MedicalTourismDocumentBO();
                MedicalTourismBAL objBAL = new MedicalTourismBAL();
                objbo.MTDOC_Id = PKId;
                objbo.Language_id = Languageid;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.GetMTDocDetailsById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtDocumentTitle.Text = dr["DocumentTitle"].ToString();
                    lblInnerImage.Visible = true;
                    lblInnerImage.Text = dr["DocPath"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["Is_active"]) ? "1" : "0";
                }
                else
                {
                    ClearControlValues(pnlEntry);
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
                FillControls(Convert.ToInt16(gView.DataKeys[rowindex]["MTDOC_Id"].ToString()), Convert.ToInt16(gView.DataKeys[rowindex]["Language_id"].ToString()));
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
                int rowId = Convert.ToInt32(gView.DataKeys[rowindex]["MTDOC_Id"].ToString());
                MedicalTourismDocumentBO objBo = new MedicalTourismDocumentBO();
                objBo.MTDOC_Id = rowId;
                new MedicalTourismBAL().DeleteMTDocRecord(objBo);
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
    }
}