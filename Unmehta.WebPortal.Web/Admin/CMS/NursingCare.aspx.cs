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
    public partial class NursingCare : System.Web.UI.Page
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
                lblInnerImage.Text = "";
                aRemoveInner.Visible = false;
                hfInnerImage.Value = "";

                ViewState["T017PDetails"] = null;
                //ShowHideControl(VisibityType.GridView);
                FillLanguage();
                BindGridView();
                drpLanguage.SelectedValue = "1";
                FillControls(Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
            }
        }
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    //pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    //pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    //btn_Update.Visible = false;
                    break;
                case VisibityType.Insert:
                    //pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    //btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    //pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    //btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    //pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    //btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    //pnlView.Visible = true;
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
        private void LoadControls(NursingCareBO objBo)
        {
            objBo.Language_id = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.Description = HttpUtility.HtmlEncode(txtDescription.Text.ToString());
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            if (fumainimage.HasFile)
            {
                string filepath = SaveFileMain();
                //string filepath = "";
                objBo.MainImgpath = filepath;
            }
            else
            {
                objBo.MainImgpath = hfInnerImage.Value;
            }
            if (string.IsNullOrWhiteSpace(hdNursingCareId.Value))
            {
                objBo.NursingCare_id = 0;
            }
            else
            {
                objBo.NursingCare_id = Convert.ToInt32(hdNursingCareId.Value);
            }
            if (string.IsNullOrWhiteSpace(hdPKId.Value))
            {
                objBo.NursingCareDetail_id = 0;
            }
            else
            {
                objBo.NursingCareDetail_id = Convert.ToInt32(hdPKId.Value.ToString());
            }
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
                    DropDownList drp = (DropDownList)row.FindControl("ddlGrdActiveInactive");
                    dr["is_active"] = drp.SelectedValue.ToString();
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
                NursingCareBO objBo = new NursingCareBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (dt.Rows.Count > 0)
                {
                    if (new NursingCareBAL().InsertRecord(objBo, dt))
                    {
                        lblInnerImage.Text = "";
                        hfInnerImage.Value = "";
                        aRemoveInner.Visible = false;

                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        ViewState["T017PDetails"] = null;
                        //grdImages.DataSource = (DataTable)ViewState["T017PDetails"];
                        //grdImages.DataBind();
                        FillControls(Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                        return;
                    }
                    //BindGridView();
                    //ShowHideControl(VisibityType.GridView);
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
        private void BindGridView()
        {
            NursingCareBAL objbal = new NursingCareBAL();
            DataSet ds = objbal.SelectNursingCareDetails();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Description"] = HttpUtility.HtmlDecode(dt.Rows[0]["Description"].ToString());
                dt.AcceptChanges();
                //gvNursingCare.DataSource = dt;
                //gvNursingCare.DataBind();
            }
            else
            {
                //gvNursingCare.DataSource = null;
                //gvNursingCare.DataBind();
            }
        }
        //protected void btn_Update_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        NursingCareBO objBo = new NursingCareBO();
        //        LoadControls(objBo);
        //        objBo.NursingCare_id = Convert.ToInt16(hdNursingCareId.Value.ToString());
        //        objBo.NursingCareDetail_id = Convert.ToInt32(hdPKId.Value.ToString());
        //        DataTable dt = new DataTable();
        //        dt = GetGridViewData();
        //        if (new NursingCareBAL().UpdateRecord(objBo, dt))
        //        {
        //            Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
        //        }
        //        else
        //        {
        //            Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
        //            return;
        //        }
        //        BindGridView();
        //        //ShowHideControl(VisibityType.GridView);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //    }
        //}

        //protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //ShowHideControl(VisibityType.GridView);
        //        grdImages.DataSource = null;
        //        grdImages.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
        //    }
        //}

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                //txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            //ShowHideControl(VisibityType.Insert);
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillControls(Convert.ToInt16(hdPKId.Value), Convert.ToInt16(hdNursingCareId.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
            FillControls(Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void FillControls(int Languageid)
        {
            try
            {
                lblInnerImage.Text = "";
                hfInnerImage.Value = "";
                aRemoveInner.Visible = false;

                NursingCareBO objbo = new NursingCareBO();
                NursingCareBAL objBAL = new NursingCareBAL();
                //objbo.NursingCare_id = NursingCareId;
                objbo.Language_id = Languageid;
                //hdPKId.Value = Convert.ToInt16(PKId).ToString();
                //hdNursingCareId.Value = Convert.ToInt16(NursingCareId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectNursingCareDetailsByID(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    hdNursingCareId.Value = dr["NursingCare_id"].ToString();
                    hdPKId.Value = dr["Id"].ToString();
                    drpLanguage.SelectedValue = dr["Language_id"].ToString();
                    if(!string.IsNullOrWhiteSpace(dr["MainImgpath"].ToString()))
                    {
                        hfInnerImage.Value = dr["MainImgpath"].ToString();
                        lblInnerImage.Text = dr["MainImgpath"].ToString();
                        aRemoveInner.Visible = true;
                    }
                    txtDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    DataSet dsDetails = new NursingCareBAL().GetImageGrid(Convert.ToInt32(hdPKId.Value));
                    grdImages.DataSource = dsDetails.Tables[0];
                    grdImages.DataBind();
                    foreach (GridViewRow gr in grdImages.Rows)
                    {
                        DropDownList drp = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        drp.SelectedValue = gr.Cells[2].Text.ToString();
                    }
                    ViewState["T017PDetails"] = dsDetails.Tables[0];
                }
                else
                {
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";
                    aRemoveInner.Visible = false;

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
                //FillControls(Convert.ToInt16(gvNursingCare.DataKeys[rowindex]["Id"].ToString()), Convert.ToInt16(gvNursingCare.DataKeys[rowindex]["NursingCare_id"].ToString()), Convert.ToInt16(gvNursingCare.DataKeys[rowindex]["Language_id"].ToString()));
                //ShowHideControl(VisibityType.Edit);
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
                //int rowId = Convert.ToInt32(gvNursingCare.DataKeys[rowindex]["NursingCare_id"].ToString());
                NursingCareBO objBo = new NursingCareBO();
                //objBo.NursingCare_id = rowId;
                new NursingCareBAL().DeleteRecord(objBo);
                Functions.MessagePopup(this, "Record deleted successfully..", PopupMessageType.success);
                BindGridView();
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private string SaveFileMain()
        {
            try
            {
                if (fumainimage.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.NursingCareImageUploadPath;
                    var fname = Path.GetExtension(fumainimage.FileName);
                    var count = fumainimage.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fumainimage.FileName.Split('.').Length; i++)
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
                        var filename1 = fumainimage.FileName.Replace(" ", "_");

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
                        fumainimage.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
        private string SaveFile()
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.NursingCareImageUploadPath;
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

                foreach (GridViewRow gr in grdImages.Rows)
                {
                    DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                    dl.SelectedValue = gr.Cells[3].Text;
                }
                grdImages.Columns[2].Visible = false;
                ViewState["T017PDetails"] = dt;
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
    }
}