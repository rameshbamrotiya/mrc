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
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Data.Hospital;
using System.Collections.Generic;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class Scheme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal", false);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal",false);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

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

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

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

            //using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            //{
            //    List<GetAllStatisticsChartMasterResult> Data = objPatientsEducationBrochureRepository.GetAllStatisticsChart();
            //    PopulateDropDownList(ddlStatistics, Functions.ToDataTable(Data), "ChartName", "Id", true);
            //}


        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }

                SchemeBO objbo = new SchemeBO();
                LoadControlsAdd(objbo);
                if (fuSchemeBanner.HasFile)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuSchemeBanner.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round(((decimal)fuSchemeBanner.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (height != 281 || width != 355)
                    //{
                    //    Functions.MessagePopup(this, "Please upload 355px*281px.", PopupMessageType.error);
                    //    return;
                    //}
                }
                if (fuschemelogo.HasFile)
                {
                    System.Drawing.Image imglogo = System.Drawing.Image.FromStream(fuschemelogo.PostedFile.InputStream);
                    int heightlogo = imglogo.Height;
                    int widthlogo = imglogo.Width;
                    decimal sizelogo = Math.Round(((decimal)fuschemelogo.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (heightlogo != 52 || widthlogo != 52)
                    //{
                    //    Functions.MessagePopup(this, "Please upload 52px*52px.", PopupMessageType.error);
                    //    return;
                    //}
                }
                if (new SchemeBAL().InsertRecord(objbo))
                {

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        private string SaveFile(FileUpload fuUpload)
        {
            try
            {

                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Scheme;
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
        private void LoadControlsAdd(SchemeBO objBo)
        {
            objBo.Language = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            //objBo.ChartId = Convert.ToInt16(ddlStatistics.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtSchemeName.Text))
                objBo.SchemeName = txtSchemeName.Text;

            if (fuschemelogo.HasFile)
            {
                string SchmeLogo = string.Empty;
                SchmeLogo = SaveFile(fuschemelogo);

                if (!string.IsNullOrEmpty(SchmeLogo))
                    objBo.SchemeLogo = SchmeLogo;
            }
            else
            {
                objBo.SchemeLogo = hfLeftImage.Value;
            }
            if (fuSchemeBanner.HasFile)
            {
                string SchemeBanner = string.Empty;
                SchemeBanner = SaveFile(fuSchemeBanner);

                if (!string.IsNullOrEmpty(SchemeBanner))
                    objBo.SchemeBanner = SchemeBanner;
            }
            else
            {
                objBo.SchemeBanner = hfRightImage.Value;
            }
            if (!string.IsNullOrEmpty(txtContPerson.Text))
                objBo.ContactPerson = txtContPerson.Text;
            else
                objBo.ContactPerson = "";

            if (!string.IsNullOrEmpty(txtHelpDeskNo.Text))
                objBo.HelpDeskNo = txtHelpDeskNo.Text;
            else
                objBo.HelpDeskNo = "";

            if (!string.IsNullOrEmpty(txtLocation.Text))
                objBo.Location = txtLocation.Text;
            else
                objBo.Location = "";

            if (!string.IsNullOrEmpty(txtWebsite.Text))
                objBo.WebsiteUrl = txtWebsite.Text;
            else
                objBo.WebsiteUrl = "";

            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.scheme_level_id = Convert.ToInt32(txtsequence.Text);
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text.ToString());
            objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;



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
        private void BindGridView()
        {
            grdScheme.DataBind();
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
            DataSet ds = new SchemeBAL().SequenceNo();
            DataRow drs = ds.Tables[0].Rows[0];
            if (drs["scheme_level_id"] != DBNull.Value)
                txtsequence.Text = drs["scheme_level_id"].ToString();
        }
        private void FillControls(int SchemeId, int languageid)
        {
            try
            {
                SchemeBO objbo = new SchemeBO();
                SchemeBAL objBAL = new SchemeBAL();
                objbo.SchemeId = SchemeId;
                objbo.Language = languageid;
                hdnSchemeID.Value = Convert.ToInt16(SchemeId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectSchemeByID(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtSchemeName.Text = dr["Schemename"].ToString();
                    
                    if (!string.IsNullOrWhiteSpace(dr["SchemeLogo"].ToString()))
                    {
                        hfLeftImage.Value = dr["SchemeLogo"].ToString();
                        lblLeftImage.Text = dr["SchemeLogo"].ToString();
                        aRemoveLeft.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SchemeBanner"].ToString()))
                    {
                        hfRightImage.Value = dr["SchemeBanner"].ToString();
                        lblRightImage.Text = dr["SchemeBanner"].ToString();
                        aRemoveRight.Visible = true;
                    }

                    txtContPerson.Text = dr["ContactPerson"].ToString();
                    txtHelpDeskNo.Text = dr["HelpDeskNO"].ToString();
                    hdnRecid.Value = dr["recid"].ToString();
                    txtWebsite.Text = dr["WebsiteUrl"].ToString();
                    txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    txtLocation.Text = dr["Location"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    txtsequence.Text = dr["scheme_level_id"].ToString();

                    //if (!string.IsNullOrWhiteSpace(dr["ChartId"].ToString()))
                    //{
                    //    {
                    //        if (ddlStatistics.Items.FindByValue(dr["ChartId"].ToString()) != null)
                    //        {
                    //            ddlStatistics.SelectedValue = dr["ChartId"].ToString();
                    //        }
                    //    }

                    //}
                }
                else
                {
                    ClearControlValues(pnlEntry);

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

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
                hdnRecid.Value = grdScheme.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdScheme.DataKeys[rowindex]["Schemeid"].ToString()), Convert.ToInt16(grdScheme.DataKeys[rowindex]["Languageid"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            SchemeBO objBo = new SchemeBO();
            LoadControlsAdd(objBo);
            objBo.SchemeId = Convert.ToInt16(hdnSchemeID.Value);
            if (new SchemeBAL().UpdateRecord(objBo))
            {
                hfLeftImage.Value = "";
                lblLeftImage.Text = "";
                aRemoveLeft.Visible = false;

                hfRightImage.Value = "";
                lblRightImage.Text = "";
                aRemoveRight.Visible = false;

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

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnSchemeID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));

        }

        protected void grdScheme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(grdScheme.DataKeys[intIndex].Values["Schemeid"]);
                    if (e.CommandName == "eDelete")
                    {
                        SchemeBO objBo = new SchemeBO();
                        objBo.SchemeId = bytID;
                        new SchemeBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
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
            if (new SchemeBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
    }
}