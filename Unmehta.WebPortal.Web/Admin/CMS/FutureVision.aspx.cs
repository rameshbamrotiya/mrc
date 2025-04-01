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
    public partial class FutureVision : System.Web.UI.Page
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
                ViewState["FVImages"] = null;
                ShowHideControl(VisibityType.GridView);
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
        private void LoadControls(FutureVisionBO objBo)
        {
            objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text.ToString());
            objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;
            objBo.Language = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.FVtitle = txtFVTitle.Text.Trim();
            objBo.MetaTitle = txtMetaTitle.Text.Trim();
            objBo.MetaDescription = txtMetaDesc.Text.Trim();
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.F_level_id = Convert.ToInt32(txtsequence.Text);
        }
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("img_name");

            //dt.Columns.Add("is_active_tamd");

            //if (grdImages.Rows.Count > 0)
            {

                dt.Columns.Add("img_name");

                dt.Columns.Add("is_active_tamd");

                foreach (GridViewRow row in grdImages.Rows)
                {
                    DataRow dr = dt.NewRow();
                    Label img = (Label)row.FindControl("ImagrUrl");
                    dr["img_name"] = img.Text;
                    DropDownList drp = (DropDownList)row.FindControl("ddlGrdActiveInactive");

                    dr["is_active_tamd"] = drp.SelectedValue.ToString();


                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        private bool DatatableSingleUpdate()
        {

            bool isError = false;
            if (fuDocUpload.HasFile)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return isError;
                //}
            }
            DataTable dt = null;
            if (ViewState["FVImages"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("img_name");
                dt.Columns.Add("is_active_tamd");
                dt.Columns.Add("ImageId");
            }
            else
            {
                dt = (DataTable)(ViewState["FVImages"]);
            }
            string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
            if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
            {
                Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                return isError;
            }
            else
            {

                DataRow dr = dt.NewRow();

                string filepath = "";


                if (fuDocUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile(out isError);

                    if (!string.IsNullOrEmpty(documentfile) && !isError)
                    {
                        filepath = documentfile;
                        isError = true;
                    }
                    else
                    {
                        Functions.MessagePopup(this, documentfile, PopupMessageType.error);
                        return isError;
                    }
                }


                dr["img_name"] = filepath;

                dr["is_active_tamd"] = "True";

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

                ViewState["FVImages"] = dt;
            }
            return isError;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    if (DatatableSingleUpdate())
                    {
                        FutureVisionBO objBo = new FutureVisionBO();
                        LoadControls(objBo);
                        DataTable dt = new DataTable();
                        dt = GetGridViewData();
                        if (dt.Rows.Count > 0)
                        {
                            if (new FutureVisionBAL().InsertRecord(objBo, dt))
                            {
                                Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                                ViewState["FVImages"] = null;
                                grdImages.DataSource = (DataTable)ViewState["FVImages"];
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
                }
                else
                {
                    FutureVisionBO objBo = new FutureVisionBO();
                    LoadControls(objBo);
                    DataTable dt = new DataTable();
                    dt = GetGridViewData();
                    if (dt.Rows.Count > 0)
                    {
                        if (new FutureVisionBAL().InsertRecord(objBo, dt))
                        {
                            Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                            ViewState["FVImages"] = null;
                            grdImages.DataSource = (DataTable)ViewState["FVImages"];
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

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void BindGridView()
        {
            //FutureVisionBAL objbal = new FutureVisionBAL();
            //DataSet ds = objbal.SelectFutureVisionDetails();
            //DataTable dt = ds.Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    dt.Rows[0]["Description"] = HttpUtility.HtmlDecode(dt.Rows[0]["Description"].ToString());
            //    dt.AcceptChanges();
            //    GrdFutureVision.DataSource = dt;
            //    GrdFutureVision.DataBind();
            //}
            GrdFutureVision.DataBind();
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (fuDocUpload.HasFile)
                {
                    if (DatatableSingleUpdate())
                    {
                        FutureVisionBO objBo = new FutureVisionBO();
                        LoadControls(objBo);
                        objBo.Fvid = Convert.ToInt16(hdnFvid.Value.ToString());

                        DataTable dt = new DataTable();
                        dt = GetGridViewData();
                        if (new FutureVisionBAL().UpdateRecord(objBo, dt))
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

                    FutureVisionBO objBo = new FutureVisionBO();
                    LoadControls(objBo);
                    objBo.Fvid = Convert.ToInt16(hdnFvid.Value.ToString());

                    DataTable dt = new DataTable();
                    dt = GetGridViewData();
                    if (new FutureVisionBAL().UpdateRecord(objBo, dt))
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.GridView);
                txtSearch.Text = "";
                BindGridView();
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
            DataSet ds = new FutureVisionBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["F_level_id"] != DBNull.Value)
                txtsequence.Text = drs["F_level_id"].ToString();
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnFvid.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void FillControls(int fvid, int languageid)
        {
            try
            {
                FutureVisionBO objbo = new FutureVisionBO();
                FutureVisionBAL objBAL = new FutureVisionBAL();
                objbo.Fvid = fvid;
                objbo.Language = languageid;
                hdnFvid.Value = Convert.ToInt16(fvid).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectFutureVisionDetailsByID(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtFVTitle.Text = dr["Fvtitle"].ToString();
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    hdnRecid.Value = dr["FVID"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    txtsequence.Text = dr["F_level_id"].ToString();

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
                // hdnRecid.Value = GrdFutureVision.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(GrdFutureVision.DataKeys[rowindex]["FVid"].ToString()), Convert.ToInt16(GrdFutureVision.DataKeys[rowindex]["Languageid"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(GrdFutureVision.DataKeys[rowindex]["FVid"].ToString());
                FutureVisionBO objBo = new FutureVisionBO();
                objBo.Fvid = rowId;
                new FutureVisionBAL().DeleteRecord(objBo);
                Functions.MessagePopup(this, "Record deleted successfully..", PopupMessageType.success);

                var dt = (DataTable)ViewState["FVImages"];

                dt.Rows[rowindex].Delete();
                ViewState["FVImages"] = dt;
                grdImages.DataSource = dt;
                grdImages.DataBind();

                BindGridView();
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private string SaveFile(out bool isError)
        {
            isError = false;
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AlbumImageUploadPath;
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
                        isError = true;
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
            bool isError = false;
            if (fuDocUpload.HasFile)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return;
                //}
            }
            DataTable dt = null;
            if (ViewState["FVImages"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("img_name");
                dt.Columns.Add("is_active_tamd");
                dt.Columns.Add("ImageId");
            }
            else
            {
                dt = (DataTable)(ViewState["FVImages"]);
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

                string filepath = "";


                if (fuDocUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile(out isError);

                    if (!string.IsNullOrEmpty(documentfile) && !isError)
                    {
                        filepath = documentfile;
                    }
                    else
                    {
                        Functions.MessagePopup(this, documentfile, PopupMessageType.error);
                        return;
                    }
                }


                dr["img_name"] = filepath;

                dr["is_active_tamd"] = "True";

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

                ViewState["FVImages"] = dt;
            }

        }

        protected void GrdFutureVision_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
            {

                string col_parent_id = commandArgs[0];
                string col_menu_level = commandArgs[1];
                string cmd = commandArgs[2];

                switch (cmd)
                {
                    case "up":
                        SetPageOrder(cmd, col_menu_level, col_parent_id);
                        break;
                    case "down":
                        SetPageOrder(cmd, col_menu_level, col_parent_id);
                        break;

                }
                BindGridView();
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new FutureVisionBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {

            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (grdImages.DataKeys[rowindex]["Imageid"].ToString());
            GridViewRow gvRow = grdImages.Rows[rowindex];

            DataTable dt = (DataTable)ViewState["FVImages"];
            //dt = GetListValueDoctor;
            if (string.IsNullOrWhiteSpace(rowId))
            {
                DataRow dr = dt.Rows[rowindex];
                dr.Delete();
            }
            else
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["Imageid"].ToString() == rowId)
                        dr.Delete();
                }
            }
            dt.AcceptChanges();


            Functions.MessagePopup(this, "Record Removed Done", PopupMessageType.success);

            ViewState["FVImages"] = dt;

            grdImages.Columns[2].Visible = true;

            grdImages.DataSource = dt;
            grdImages.DataBind();
        }
    }
}