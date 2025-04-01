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
using System.Globalization;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class MSRClause : System.Web.UI.Page
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
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

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
            FillControls(Convert.ToInt32(hfID.Value), Convert.ToInt32(drpLanguage.SelectedValue));
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            MSRClauseBO objBo = new MSRClauseBO();
            LoadControlsAdd(objBo);
            objBo.msrid = Convert.ToInt32(ViewState["PK"]);
            if (new MSRClauseBAL().UpdateRecord(objBo))
            {
                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
            }
            else
            {
                Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                return;
            }
            BindGrid();
            ShowHideControl(VisibityType.GridView);
        }

        public void BindGrid()
        {
            grdDetails.DataBind();
        }

        private string SaveFile()
        {
            try
            {

                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.GovApprovel;
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

        private void LoadControlsAdd(MSRClauseBO objBo)
        {
            DateTime? dtStartDate = null;
            if (fuUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.imagepath = documentfile;
            }
            else
            {
                objBo.imagepath = hfLeftImage.Value.ToString();
            }
            if (!string.IsNullOrEmpty(txtParticulars.Text))
                objBo.Particulars = txtParticulars.Text;
            objBo.languageid = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(Convert.ToString(txtupdateddate.Text)))
            {
                DateTime dStartDate = DateTime.ParseExact(txtupdateddate.Text, "dd/MM/yyyy", null);
                dtStartDate = new DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(dtStartDate)))
                objBo.LatstupdateDate = dtStartDate;
            objBo.enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.Msr_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.AddedBy = "Admin";
            objBo.ModifiedBy = "Admin";
            objBo.ipadd = GetIPAddress;
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }
                MSRClauseBO objbo = new MSRClauseBO();
                LoadControlsAdd(objbo);
                if (new MSRClauseBAL().InsertRecord(objbo))
                {
                    ShowMessage("Record inserted successfully.", MessageType.Success);
                    BindGrid();
                }
                //BindGridView();
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
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                BindGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.GridView);
                txtSearch.Text = string.Empty;
                BindGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
            DataSet ds = new MSRClauseBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["Msr_level_id"] != DBNull.Value)
                txtsequence.Text = drs["Msr_level_id"].ToString();
        }

        private void FillControls(int MSRId, int languageid)
        {
            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            try
            {
                MSRClauseBO objbo = new MSRClauseBO();
                MSRClauseBAL objBal = new MSRClauseBAL();
                objbo.msrid = MSRId;
                objbo.languageid = languageid;
                hfID.Value = MSRId.ToString();
                DataSet ds = new DataSet();
                ds = objBal.SelectRecord(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtParticulars.Text = dr["Particulars"].ToString();


                    if (dr["ImagePath"] != DBNull.Value)
                    {
                        if (!string.IsNullOrWhiteSpace(dr["ImagePath"].ToString()))
                        {
                            hfLeftImage.Value = dr["ImagePath"].ToString();
                            lblLeftImage.Text = dr["ImagePath"].ToString();
                            aRemoveLeft.Visible = true;
                        }
                    }

                    drpLanguage.SelectedValue = dr["languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    txtsequence.Text = dr["Msr_level_id"].ToString();
                    if (dr["LatstupdateDate"] != DBNull.Value)
                        txtupdateddate.Text = Convert.ToDateTime(dr["LatstupdateDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                }
                else
                {
                    txtParticulars.Text = "";

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    //var menuTy = drpMenutype.SelectedValue;
                    //ClearControlValues(pnlEntry);
                    //DataSet ds1 = objBAL.SelectMenutype(objbo);
                    //DataTable dt = ds1.Tables[0];
                    //PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);
                    //drpMenutype.SelectedValue = menuTy;
                    //drpLanguage.SelectedValue = languageid;
                    //CKEditorControl1.Text = "";
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
                ViewState["PK"] = Convert.ToInt32(grdDetails.DataKeys[rowindex].Values["MsrId"]);
                //hdnRecid.Value = grdDetails.DataKeys[rowindex]["courseid"].ToString();
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["MsrId"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageId"].ToString()));
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

                MSRClauseBO objbo = new MSRClauseBO();
                objbo.msrid = Convert.ToInt16(grdDetails.DataKeys[rowindex]["MsrId"].ToString());
                if (new MSRClauseBAL().RemoveMsrClauseMasterDetails(objbo))
                {
                    ShowMessage("Record Removed successfully.", MessageType.Success);
                }
                BindGrid();
                ShowHideControl(VisibityType.GridView);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    
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
                        BindGrid();
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
            if (new MSRClauseBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
    }
}