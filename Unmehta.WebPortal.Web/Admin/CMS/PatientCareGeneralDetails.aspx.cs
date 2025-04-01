using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Web;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientCareGeneralDetails : System.Web.UI.Page
    {
        public static int TabTypeId, LanguageId, SubTabId;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["T017PDetails"] = null;
                    string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    long id = 0;
                    string[] splitString = queryString.Split('|');
                    TabTypeId = Convert.ToInt32(splitString[0].ToString().Replace("TabTypeId=", ""));
                    SubTabId = Convert.ToInt32(splitString[1].ToString().Replace("SubTabId=", ""));
                    LanguageId = Convert.ToInt32(splitString[2].ToString().Replace("LanguageId=", ""));
                    BindGridView();
                }
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["GeneralDetailsId"]);
                    if (e.CommandName == "eDelete")
                    {
                        PatientCareGeneralDetailsBO objBo = new PatientCareGeneralDetailsBO();
                        objBo.GeneralDetailsId = bytID;
                        new PatientCareMasterBAL().DeleteGeneralDetailsRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
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
        private bool FillControls(Int32 PKId)
        {
            try
            {
                PatientCareGeneralDetailsBO objbo = new PatientCareGeneralDetailsBO();
                PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
                objbo.GeneralDetailsId = PKId;
                hdPKId.Value = Convert.ToInt16(PKId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.GetPatintGeneralImageDetails(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtTabDescription.Text = HttpUtility.HtmlDecode(dr["TabDescription"].ToString());
                    ddlActiveInactive.SelectedValue = dr["Is_active"].ToString() == "True" ? "1" : "0";
                    DataSet dsDetails = new NursingCareBAL().GetImageGrid(Convert.ToInt32(hdPKId.Value));
                    if(ds.Tables[1].Rows.Count > 0 && ds.Tables[1] != null)
                    {
                        grdImages.DataSource = ds.Tables[1];
                        grdImages.DataBind();
                        ViewState["T017PDetails"] = ds.Tables[1];
                    }
                    else
                    {
                        grdImages.DataSource = null;
                        grdImages.DataBind();
                        ViewState["T017PDetails"] = null;
                    }
                    return true;
                }
                else
                {
                    ClearControlValues(pnlEntry);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                return false;
            }
            
        }
        private void BindGridView()
        {
            DataSet ds = new DataSet();            
            PatientCareGeneralDetailsBO objBo = new PatientCareGeneralDetailsBO();            
            objBo.TabTypeId = TabTypeId;
            objBo.SubTabId = SubTabId;
            objBo.LanguageId = LanguageId;
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            ds = objBAL.GetPatintGeneralDetails(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gView.DataSource = ds;
                gView.DataBind();
                ShowHideControl(VisibityType.GridView);
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
                ShowHideControl(VisibityType.Insert);
                btn_Cancel.Visible = false;
            }
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(PatientCareGeneralDetailsBO objBo)
        {
            objBo.TabTypeId = TabTypeId;
            objBo.SubTabId = SubTabId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtTabDescription.Text))
                objBo.TabDescription = HttpUtility.HtmlEncode(txtTabDescription.Text.ToString());
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Img_path");
            dt.Columns.Add("is_active");

            if (grdImages.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdImages.Rows)
                {
                    DataRow dr = dt.NewRow();
                    Label img = (Label)row.FindControl("ImagrUrl");
                    dr["Img_path"] = img.Text;                    
                    dr["is_active"] =ddlActiveInactive.SelectedValue;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            return dt;
        }
                
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareGeneralDetailsBO objBo = new PatientCareGeneralDetailsBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (dt.Rows.Count > 0)
                {
                    if (new PatientCareMasterBAL().InsertRecord(objBo, dt))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        ViewState["T017PDetails"] = null;
                        grdImages.DataSource = (DataTable)ViewState["T017PDetails"];
                        grdImages.DataBind();
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareGeneralDetailsBO objBo = new PatientCareGeneralDetailsBO();
                LoadControls(objBo);
                objBo.GeneralDetailsId = Convert.ToInt16(hdPKId.Value.ToString());
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (new PatientCareMasterBAL().UpdateRecord(objBo, dt))
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
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                grdImages.DataSource = null;
                grdImages.DataBind();
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void grdImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdImages.PageIndex = e.NewPageIndex;
            BindSubGridView();
        }

        protected void grdImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(grdImages.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["T017PDetails"] as DataTable;
                        dt.Rows[index].Delete();
                        ViewState["T017PDetails"] = dt;
                        grdImages.DataSource = dt;
                        grdImages.DataBind();
                    }
                    else
                    {
                        Int32 bytID;
                        bytID = Convert.ToInt32(grdImages.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            DataTable dt = ViewState["T017PDetails"] as DataTable;
                            dt.Rows[intIndex].Delete();
                            ViewState["T017PDetails"] = dt;
                            //BindGridView();
                            grdImages.DataSource = dt;
                            grdImages.DataBind();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void BindSubGridView()
        {
            int Id = Convert.ToInt32(hdPKId.Value);
            PackageMasterBAL objbal = new PackageMasterBAL();
            DataSet ds = objbal.GetSubPackageGrid(Id);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                grdImages.DataSource = dt;
                grdImages.DataBind();
            }
            else
            {
                grdImages.DataBind();
            }
        }
        private string SaveFile()
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.PatientCareImageUploadPath;
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
        protected void btnImage_ServerClick(object sender, EventArgs e)
        {
            if (fuDocUpload.HasFile)
            {
                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return;
                //}
            }
            DataTable dt = null;
            if (ViewState["T017PDetails"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("Img_path");
                dt.Columns.Add("is_active");
                dt.Columns.Add("Id");
            }
            else
            {
                dt = (DataTable)(ViewState["T017PDetails"]);
            }
            string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
            if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
            {
                Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                return;
            }
            else
            {

                DataRow dr = dt.NewRow();
                string filepath = SaveFile();
                dr["Img_path"] = filepath;
                dr["is_active"] = "True";
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                grdImages.Columns[2].Visible = true;
                grdImages.DataSource = dt;
                grdImages.DataBind();
                
                ViewState["T017PDetails"] = dt;
            }
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
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
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
    }
}