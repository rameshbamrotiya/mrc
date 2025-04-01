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
    public partial class PatinetCareSubCategory : System.Web.UI.Page
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
                ViewState["T017PDetails"] = null;
                FillLanguage();
                FillCategory(1);
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
                    GridView1.Visible = false;
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
        private void FillCategory(int languageid)
        {
            DataSet ds = new DataSet();
            PatientCareCategoryBAL objBAL = new PatientCareCategoryBAL();
            PatientCareCategoryBO objbo = new PatientCareCategoryBO();
            objbo.LanguageId = languageid;
            ds = objBAL.SelectCategoryLanguagewise(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpCategory, dt, "Categoryname", "CategoryID", true);

        }
        private void FillControls(int CategoryID, int languageid)
        {
            try
            {
                PatientCareSubCategoryBO objbo = new PatientCareSubCategoryBO();
                PatinetCareSubCategoryBAL objBAL = new PatinetCareSubCategoryBAL();
                objbo.CategoryID = CategoryID;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(CategoryID).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectCategoryById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    GridView1.Columns[4].Visible = true;
                    GridView1.Columns[5].Visible = true;
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                        dl.SelectedValue = gr.Cells[4].Text;
                        DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                        dlw.SelectedValue = gr.Cells[5].Text;
                    }
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    ViewState["T017PDetails"] = ds.Tables[0];
                    txtCategory.Text = dr["CategoryName"].ToString();
                    hdnRecid.Value = dr["Recid"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled2"]) ? "1" : "0";
                    drpCategory.SelectedValue = dr["categoryid"].ToString();
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());

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
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["SubCategoryID"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageID"].ToString()));
                GridView1.Visible = true;
                ShowHideControl(VisibityType.Edit);

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
        private void BindGridView()
        {
            grdDetails.DataBind();
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

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
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
        private void LoadControls(PatientCareSubCategoryBO objBo)
        {

            objBo.Enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.CategoryName = txtCategory.Text.Trim();
            objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text);
            objBo.CategoryID = Convert.ToInt16(drpCategory.SelectedValue.ToString());

        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                PatientCareSubCategoryBO objBo = new PatientCareSubCategoryBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();

                if (new PatinetCareSubCategoryBAL().InsertRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    ViewState["T017PDetails"] = null;
                    GridView1.DataSource = (DataTable)ViewState["T017PDetails"];
                    GridView1.DataBind();
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

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategory(Convert.ToInt16(drpCategory.SelectedValue.ToString()));
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PatientCareSubCategoryBO objBo = new PatientCareSubCategoryBO();
                LoadControls(objBo);
                objBo.SubCategoryID = Convert.ToInt16(hdnCatID.Value);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (new PatinetCareSubCategoryBAL().UpdateRecord(objBo, dt))
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
        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            DataTable dt = null;
            if (ViewState["T017PDetails"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("ImageTitle");
                dt.Columns.Add("ImagePath");
                dt.Columns.Add("is_active_tamd");
                dt.Columns.Add("Img_id");
                dt.Columns.Add("is_download");
            }
            else
            {
                dt = (DataTable)(ViewState["T017PDetails"]);
            }
            string type = Path.GetExtension(fuImageUpload.FileName).ToLower();
            if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
            {
                Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                return;
            }
            else
            {

                DataRow dr = dt.NewRow();
                string filepath = SaveFile();
                dr["ImageTitle"] = txtimgtitle.Text;
                dr["ImagePath"] = filepath;
                dr["is_active_tamd"] = ddlActiveInactiveimg.SelectedValue;
                dr["is_download"] = ddlisddownload.SelectedValue;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                GridView1.Columns[4].Visible = true;
                GridView1.Columns[5].Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                    dl.SelectedValue = gr.Cells[3].Text;
                    DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                    dlw.SelectedValue = gr.Cells[4].Text;
                }
                GridView1.Columns[4].Visible = false;
                GridView1.Columns[5].Visible = false;
                ViewState["T017PDetails"] = dt;
            }
        }
        private string SaveFile()
        {
            try
            {
                if (fuImageUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AlbumImageUploadPath;
                    var fname = Path.GetExtension(fuImageUpload.FileName);
                    var count = fuImageUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuImageUpload.FileName.Split('.').Length; i++)
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
                        var filename1 = fuImageUpload.FileName.Replace(" ", "_");

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
                        fuImageUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            if (GridView1.Rows.Count > 0)
            {

                dt.Columns.Add("ImageTitle");
                dt.Columns.Add("ImagePath");
                dt.Columns.Add("is_active_tamd");
                dt.Columns.Add("Is_download");
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["ImageTitle"] = row.Cells[2].Text;
                    DropDownList drp = (DropDownList)row.FindControl("ddlGrdActiveInactive");
                    DropDownList drpw = (DropDownList)row.FindControl("ddlIsdownload");
                    Label img = (Label)row.FindControl("ImagrUrl");
                    dr["is_active_tamd"] = drp.SelectedValue.ToString();
                    dr["Is_download"] = drpw.SelectedValue.ToString();
                    dr["ImagePath"] = img.Text.ToString();
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

            }
            return dt;
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
                        PatientCareSubCategoryBO objBo = new PatientCareSubCategoryBO();
                        objBo.Img_id = bytID;
                        new PatinetCareSubCategoryBAL().DeleteRecordImage(objBo);
                        int OSID = Convert.ToInt32(grdDetails.DataKeys[intIndex].Values["SubCategoryID"]);
                        FillControls(OSID, Convert.ToInt32(drpLanguage.SelectedValue));
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            PatientCareSubCategoryBO objBo = new PatientCareSubCategoryBO();
            objBo.SubCategoryID = Convert.ToInt32(grdDetails.DataKeys[rowindex]["SubCategoryID"].ToString());
            new PatinetCareSubCategoryBAL().DeleteRecord(objBo);
            BindGridView();
            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
            return;
        }
    }
}