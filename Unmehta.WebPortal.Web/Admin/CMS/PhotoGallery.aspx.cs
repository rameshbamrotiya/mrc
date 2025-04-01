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
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Configuration;
using Unmehta.WebPortal.Common;
using System.IO;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PhotoGallery : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    ViewState["T017PDetails"] = null;

                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    BindDepartment();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Album_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
                        objBo.Album_id = bytID;
                        new PhotoGalleryMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            GridView1.Visible = true;                            
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
                else
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new PhotoGalleryMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindDepartment();
            PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
            objBo.Album_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new PhotoGalleryMasterBAL().SelectRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtAlbumName.Text = "";
                txtMetaTitle.Text = "";
                txtMetaDesc.Text = "";
                txtalbumdesc.Text = "";
                txtTagList.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            var res = from row in ds.Tables[0].AsEnumerable()
                      where row.Field<int?>("Img_id") != null 
                      select row;

            if (res.Count() > 0)
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = 10;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                ViewState["T017PDetails"] = ds.Tables[0];
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

            }


            foreach (GridViewRow gr in GridView1.Rows)
            {
                DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                dl.SelectedValue = gr.Cells[3].Text;
                DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                dlw.SelectedValue = gr.Cells[4].Text;
            }
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[4].Visible = false;
            if (dr["Album_name"] != DBNull.Value)
                txtAlbumName.Text = dr["Album_name"].ToString();
            if (dr["Album_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Album_level_id"].ToString();
            if (dr["Album_desc"] != DBNull.Value)
                txtalbumdesc.Text = dr["Album_desc"].ToString();
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["is_active_tam"] != DBNull.Value)
                ddlActiveInactivealbum.SelectedValue = Convert.ToBoolean(dr["is_active_tam"]) ? "1" : "0";
            if (dr["Department_id"] != DBNull.Value)
                ddlDepartment.SelectedValue = Convert.ToString(dr["Department_id"]);
            if (dr["TagList"] != DBNull.Value)
                txtTagList.Text = dr["TagList"].ToString();
            return true;
        }
        private void BindGridView()
        {
            BindDepartment();
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(PhotogalleryMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtAlbumName.Text))
                objBo.Album_Name = txtAlbumName.Text;
            if (!string.IsNullOrEmpty(txtalbumdesc.Text))
                objBo.Album_desc = txtalbumdesc.Text;
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Album_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active_album = ddlActiveInactivealbum.SelectedValue.ToString();
            objBo.Is_active_img = ddlActiveInactiveimg.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.Department_id = ddlDepartment.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(txtTagList.Text))
                objBo.TagList = txtTagList.Text;
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new PhotoGalleryMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Album_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Album_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private DataTable GetGridViewData()
        {
            //GridView1.AllowPaging = false;
            //GridView1.PageSize = 0;
            
            DataTable dt = new DataTable();

            long totalPage = GridView1.PageCount;
            int a = GridView1.PageIndex;

            dt.Columns.Add("img_name");
            dt.Columns.Add("img_desc");
            dt.Columns.Add("is_active");
            dt.Columns.Add("is_download");

            for (int i = 0; i < totalPage; i++)
            {
                GridView1.SetPageIndex(i);

                if (GridView1.Rows.Count > 0)
                {



                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr["img_name"] = row.Cells[1].Text;
                        DropDownList drp = (DropDownList)row.FindControl("ddlGrdActiveInactive");
                        DropDownList drpw = (DropDownList)row.FindControl("ddlIsdownload");
                        Label img = (Label)row.FindControl("ImagrUrl");
                        dr["is_active"] = drp.SelectedValue.ToString();
                        dr["is_download"] = drpw.SelectedValue.ToString();
                        dr["img_desc"] = img.Text.ToString();
                        dt.Rows.Add(dr);
                    }
                    dt.AcceptChanges();

                }
            }
            GridView1.SetPageIndex(a);
            return dt;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (dt.Rows.Count > 0)
                {
                    if (new PhotoGalleryMasterBAL().InsertRecord(objBo, dt))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        ViewState["T017PDetails"] = null;
                        GridView1.DataSource = (DataTable)ViewState["T017PDetails"];
                        GridView1.DataBind();
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
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
                PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
                LoadControls(objBo);
                objBo.Img_id = Convert.ToInt32(ViewState["PKImg"]);
                objBo.Album_id = Convert.ToInt32(ViewState["PK"]);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (new PhotoGalleryMasterBAL().UpdateRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
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
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }


        private string SaveFile(HttpPostedFile file)
        {
            try
            {
                if (file!=null)
                {
                    var DocumentUpload = ConfigDetailsValue.AlbumImageUploadPath;
                    var fname = Path.GetExtension(file.FileName);
                    var count = fuDocUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < file.FileName.Split('.').Length; i++)
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
                        var filename1 = file.FileName.Replace(" ", "_");

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
                        file.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    GridView1.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
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

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            DataTable dt = null;
            if (ViewState["T017PDetails"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("Image_name");
                dt.Columns.Add("Image_desc");
                dt.Columns.Add("is_active_tamd");
                dt.Columns.Add("Img_id");
                dt.Columns.Add("is_download");
            }
            else
            {
                dt = (DataTable)(ViewState["T017PDetails"]);
            }

            if (fuDocUpload.HasFiles)
            {
                foreach (HttpPostedFile file in fuDocUpload.PostedFiles)
                {
                    string type = Path.GetExtension(file.FileName).ToLower();
                    if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                    {
                        Functions.MessagePopup(this, file.FileName+ " Please Select Valid File Formate!!!", PopupMessageType.error);
                        return;
                    }
                }

                foreach (HttpPostedFile file in fuDocUpload.PostedFiles)
                {

                    DataRow dr = dt.NewRow();
                    string filepath = SaveFile(file);
                    dr["Image_name"] = fuDocUpload.FileName;
                    dr["Image_desc"] = filepath;
                    dr["is_active_tamd"] = ddlActiveInactiveimg.SelectedValue;
                    dr["is_download"] = ddlisddownload.SelectedValue;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    GridView1.Columns[3].Visible = true;
                    GridView1.Columns[4].Visible = true;
                    GridView1.DataSource = dt;
                    gvRef.DataSource = dt;
                    gvRef.DataBind();
                    GridView1.DataBind();

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        dl.SelectedValue = gr.Cells[3].Text;
                        DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                        dlw.SelectedValue = gr.Cells[4].Text;
                    }

                    foreach (GridViewRow gr in gvRef.Rows)
                    {
                        DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        dl.SelectedValue = gr.Cells[3].Text;
                        DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                        dlw.SelectedValue = gr.Cells[4].Text;
                    }

                    GridView1.Columns[3].Visible = false;
                    GridView1.Columns[4].Visible = false;
                    ViewState["T017PDetails"] = dt;
                }
            }
            else
            {

                string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
                if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                {
                    Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    return;
                }
                else
                {

                    DataRow dr = dt.NewRow();
                    string filepath = SaveFile(fuDocUpload.PostedFile);
                    dr["Image_name"] = fuDocUpload.FileName;
                    dr["Image_desc"] = filepath;
                    dr["is_active_tamd"] = ddlActiveInactiveimg.SelectedValue;
                    dr["is_download"] = ddlisddownload.SelectedValue;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    GridView1.Columns[3].Visible = true;
                    GridView1.Columns[4].Visible = true;
                    GridView1.DataSource = dt;
                    gvRef.DataSource = dt;
                    gvRef.DataBind();

                    GridView1.DataBind();

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        dl.SelectedValue = gr.Cells[3].Text;
                        DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                        dlw.SelectedValue = gr.Cells[4].Text;
                    }

                    foreach (GridViewRow gr in gvRef.Rows)
                    {
                        DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        dl.SelectedValue = gr.Cells[3].Text;
                        DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                        dlw.SelectedValue = gr.Cells[4].Text;
                    }

                    GridView1.Columns[3].Visible = false;
                    GridView1.Columns[4].Visible = false;
                    ViewState["T017PDetails"] = dt;
                }
            }

        }

        protected string FormatImageUrl(string url)
        {

            if (url != null && url.Length > 0)

                return ("~/" + url);

            else return null;

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(GridView1.DataKeys[intIndex].Values["Img_id"]);
                    if (e.CommandName == "eDelete")
                    {                        
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["T017PDetails"] as DataTable;
                        dt.Rows[index].Delete();
                        dt.AcceptChanges();
                        ViewState["T017PDetails"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        gvRef.DataSource = dt;
                        gvRef.DataBind();
                    }
                    if (FillControlsimg(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PKImg"] = bytID;
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
        private bool FillControlsimg(Int32 iPkId, int languageId)
        {
            PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
            objBo.Img_id = iPkId;
            DataSet ds = new PhotoGalleryMasterBAL().SelectRecordImg(objBo);
            if (ds == null) return false;
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            if (dr["Album_name"] != DBNull.Value)
                txtAlbumName.Text = dr["Album_name"].ToString();
            if (dr["Album_desc"] != DBNull.Value)
                txtalbumdesc.Text = dr["Album_desc"].ToString();
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["is_active_tam"] != DBNull.Value)
                ddlActiveInactivealbum.SelectedValue = Convert.ToBoolean(dr["is_active_tam"]) ? "1" : "0";
            return true;
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        private void BindDepartment()
        {
            ddlDepartment.Items.Clear();
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {
                ddlDepartment.DataSource = objDepartmentRepository.GetAllTblDepartment(Convert.ToInt64(ddlLanguage.SelectedValue.ToString()));
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = (DataTable)ViewState["T017PDetails"];
            GridView1.DataBind();
        }
    }
}