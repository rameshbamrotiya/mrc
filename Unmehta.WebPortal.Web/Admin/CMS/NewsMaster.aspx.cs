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
using System.IO;
using Unmehta.WebPortal.Common;
using System.Globalization;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class NewsMaster : System.Web.UI.Page
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
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                }
                //}
                //else
                //    Response.Redirect("~/CMS/LoginPage.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["news_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        NewsMasterBO objBo = new NewsMasterBO();
                        objBo.news_id = bytID;
                        new NewsMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    Bind_NewsType();
                    Bind_Language();

                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {

                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                            ShowHideByType();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }


        private bool FillControls(Int32 iPkId)
        {
            NewsMasterBO objBo = new NewsMasterBO();
            objBo.news_id = iPkId;
            objBo.Language_id = Convert.ToInt32(ddlLanguage.SelectedValue);
            DataSet ds = new NewsMasterBAL().SelectRecord(objBo);
            if (ds == null) return false;
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Language_id"] != DBNull.Value)
                ddlLanguage.SelectedValue = dr["Language_id"].ToString();

            if (dr["news_typeid"] != DBNull.Value)
                ddlNewsType.SelectedValue = dr["news_typeid"].ToString();

            if (dr["news_title"] != DBNull.Value)
                txtTitle.Text = dr["news_title"].ToString();

            if (dr["newsBy"] != DBNull.Value)
                txtUploadBy.Text = dr["newsBy"].ToString();

            if (dr["news_desc"] != DBNull.Value)
                txtNewsDescription.Text = HttpUtility.HtmlDecode(dr["news_desc"].ToString());

            if (dr["DocURL"] != DBNull.Value)
            {
                if (!string.IsNullOrWhiteSpace(dr["DocURL"].ToString()))
                {
                    hfLeftImage.Value = dr["DocURL"].ToString();
                    lblLeftImage.Text = dr["DocURL"].ToString();
                    aRemoveLeft.Visible = true;
                }
            }
            if (dr["news_start_date"] != DBNull.Value)
            {
                txtStartDate.Text = Convert.ToDateTime(dr["news_start_date"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtStartTime.Text = Convert.ToDateTime(dr["news_start_date"]).ToString("HH:mm");
            }

            if (dr["news_end_date"] != DBNull.Value)
            {
                txtEndDate.Text = Convert.ToDateTime(dr["news_end_date"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtToTime.Text = Convert.ToDateTime(dr["news_end_date"]).ToString("HH:mm");
            }

            if (dr["Location"] != DBNull.Value)
                txtLocation.Text = (dr["Location"]).ToString();

            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";

            if (dr["IsNewIcon"] != DBNull.Value)
                chkIsNew.Checked= Convert.ToBoolean(dr["IsNewIcon"]); 

            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(NewsMasterBO objBo)
        {
            DateTime? dtStartDate = null;
            DateTime? dtEndDate = null;

            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue.ToString()))
                objBo.Language_id = int.Parse(ddlLanguage.SelectedValue.ToString());


            if (!string.IsNullOrEmpty(ddlNewsType.SelectedValue.ToString()))
                objBo.news_typeid = int.Parse(ddlNewsType.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.news_title = txtTitle.Text;

            if (!string.IsNullOrEmpty(txtUploadBy.Text))
                objBo.newsBy = txtUploadBy.Text;

            if (!string.IsNullOrEmpty(txtNewsDescription.Text))
                objBo.news_desc = HttpUtility.HtmlEncode(txtNewsDescription.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtStartDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtStartTime.Text)))
            {
                DateTime dStartDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", null);
                DateTime tStartDate = Convert.ToDateTime(txtStartTime.Text);
                dtStartDate = new DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day, tStartDate.Hour, tStartDate.Minute, tStartDate.Second);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(txtEndDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtToTime.Text)))
            {
                DateTime dEndDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
                DateTime tEndDate = Convert.ToDateTime(txtToTime.Text);
                dtEndDate = new DateTime(dEndDate.Year, dEndDate.Month, dEndDate.Day, tEndDate.Hour, tEndDate.Minute, tEndDate.Second);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(dtStartDate)))
                objBo.news_start_date = dtStartDate;

            if (!string.IsNullOrEmpty(Convert.ToString(dtEndDate)))
                objBo.news_end_date = dtEndDate;


            string documentfile = string.Empty;
            documentfile = SaveFile();
            if (!string.IsNullOrEmpty(documentfile))
                objBo.DocURL = documentfile;
            string Bannerfile = string.Empty;
            Bannerfile = SaveFileBanner();
            if (!string.IsNullOrEmpty(Bannerfile))
                objBo.BannerURL = Bannerfile;
            if (!string.IsNullOrEmpty(Convert.ToString(txtLocation.Text)))
                objBo.Location = Convert.ToString(txtLocation.Text);

            objBo.IsActive = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;

            objBo.IsNewIcon = chkIsNew.Checked;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                Bind_NewsType();
                Bind_Language();
                ShowHideByType();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlNewsType.SelectedItem.ToString().ToUpper() == "MEDIA" ||
                    ddlNewsType.SelectedItem.ToString().ToUpper() == "NOTICE" ||
                    ddlNewsType.SelectedItem.ToString().ToUpper() == "GENERAL NOTICE")
                {
                    DateTime dt;
                    if(!DateTime.TryParse(txtStartDate.Text,out dt))
                    {
                        Functions.MessagePopup(this, "Please Enter Start Date.", PopupMessageType.warning);
                    }
                }


                if (ddlNewsType.SelectedItem.ToString().ToUpper() == "EVENT")
                {
                    if (!FuBanner.HasFile)
                    {
                        Functions.MessagePopup(this, "Please upload banner image.", PopupMessageType.warning);
                    }
                    else
                    {
                        string type = Path.GetExtension(FuBanner.FileName).ToLower();
                        if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                        {
                            Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                            return;
                        }
                    }
                }
                NewsMasterBO objBo = new NewsMasterBO();
                LoadControls(objBo);
                if (new NewsMasterBAL().InsertRecord(objBo))
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
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlNewsType.SelectedItem.ToString().ToUpper() == "EVENT")
                {
                    if (!FuBanner.HasFile)
                    {
                        Functions.MessagePopup(this, "Please upload banner image.", PopupMessageType.warning);
                    }
                    else
                    {
                        string type = Path.GetExtension(FuBanner.FileName).ToLower();
                        if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                        {
                            Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                            return;
                        }
                    }
                }
                NewsMasterBO objBo = new NewsMasterBO();
                LoadControls(objBo);
                objBo.news_id = Convert.ToInt32(ViewState["PK"]);
                if (new NewsMasterBAL().UpdateRecord(objBo))
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        public void Bind_NewsType()
        {
            DataSet ds = new DataSet();
            NewsMasterBAL objBAL = new NewsMasterBAL();
            ds = objBAL.SelectAllNewsType();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlNewsType, dt, "NewsType", "news_type_id", false);
        }

        public void Bind_Language()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlLanguage, dt, "Name", "Id", false);
        }

        protected void ddlNewsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideByType();
        }

        public void ShowHideByType()
        {
            try
            {
                string Type = ddlNewsType.SelectedItem.ToString().ToUpper();
                switch (Type)
                {
                    case "CIRCULAR":
                        txtStartDate.Visible = false;
                        txtEndDate.Visible = false;
                        //rfvStartDate.Enabled = false;
                        //rfvEndDate.Enabled = false;
                        Fdate.Visible = false;
                        //Tdate.Visible = false;
                        Fupload.Visible = false;
                        Bannerimage.Visible = false;
                        break;
                    case "NEWS":
                        txtStartDate.Visible = true;
                        txtEndDate.Visible = true;
                        //rfvStartDate.Enabled = true;
                        //rfvEndDate.Enabled = true;
                        Fdate.Visible = true;
                        //Tdate.Visible = true;
                        Fupload.Visible = true;
                        Bannerimage.Visible = false;
                        break;

                    case "EVENT":
                        txtStartDate.Visible = true;
                        txtEndDate.Visible = true;
                        //rfvStartDate.Enabled = true;
                        //rfvEndDate.Enabled = true;
                        Fdate.Visible = true;
                        //Tdate.Visible = true;
                        Fupload.Visible = true;
                        Bannerimage.Visible = true;
                        break;
                    case "MEDIA":
                        txtStartDate.Visible = true;
                        txtEndDate.Visible = true;
                        //rfvStartDate.Enabled = true;
                        //rfvEndDate.Enabled = true;
                        Fdate.Visible = true;
                        //Tdate.Visible = true;
                        Fupload.Visible = true;
                        Bannerimage.Visible = false;
                        break;
                    case "NOTICE":
                        txtStartDate.Visible = true;
                        txtEndDate.Visible = true;
                        //rfvStartDate.Enabled = true;
                        //rfvEndDate.Enabled = true;
                        Fdate.Visible = true;
                        //Tdate.Visible = true;
                        Fupload.Visible = true;
                        Bannerimage.Visible = false;
                        break;
                    case "GENERAL NOTICE":
                        txtStartDate.Visible = true;
                        txtEndDate.Visible = true;
                        //rfvStartDate.Enabled = true;
                        //rfvEndDate.Enabled = true;
                        Fdate.Visible = true;
                        //Tdate.Visible = true;
                        Fupload.Visible = true;
                        Bannerimage.Visible = false;
                        break;

                    case "NOTIFICATION":
                        txtStartDate.Visible = false;
                        txtEndDate.Visible = false;
                        //rfvStartDate.Enabled = false;
                        //rfvEndDate.Enabled = false;
                        Fdate.Visible = false;
                        //Tdate.Visible = false;
                        Fupload.Visible = false;
                        Bannerimage.Visible = false;
                        break;
                    default:
                        txtStartDate.Visible = false;
                        txtEndDate.Visible = false;
                        //rfvStartDate.Enabled = false;
                        //rfvEndDate.Enabled = false;
                        Fdate.Visible = false;
                        //Tdate.Visible = false;
                        Fupload.Visible = false;
                        Bannerimage.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private string SaveFile()
        {
            try
            {
                if (fuNewsDoc.HasFile)
                {
                    var DocPath = ConfigDetailsValue.NewsFiles;

                    var fname = Path.GetExtension(fuNewsDoc.FileName);
                    var count = fuNewsDoc.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocPath));
                    if (!exists)
                        for (int i = 0; i < fuNewsDoc.FileName.Split('.').Length; i++)
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
                        var filename1 = fuNewsDoc.FileName.Replace(" ", "_");

                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(DocPath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocPath));
                        }


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocPath + filename1;



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
                                pathToCheck1 = DocPath + tempfileName1;
                                counter++;
                            }


                            filename1 = tempfileName1;
                        }


                        //Save selected file into specified location
                        fuNewsDoc.SaveAs(Server.MapPath(DocPath) + filename1);
                        return (DocPath) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }
        private string SaveFileBanner()
        {
            try
            {
                if (FuBanner.HasFile)
                {
                    var DocPath = ConfigDetailsValue.NewsFiles;

                    var fname = Path.GetExtension(FuBanner.FileName);
                    var count = FuBanner.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocPath));
                    if (!exists)
                        for (int i = 0; i < FuBanner.FileName.Split('.').Length; i++)
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
                        var filename1 = FuBanner.FileName.Replace(" ", "_");

                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(DocPath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocPath));
                        }


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocPath + filename1;



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
                                pathToCheck1 = DocPath + tempfileName1;
                                counter++;
                            }


                            filename1 = tempfileName1;
                        }


                        //Save selected file into specified location
                        FuBanner.SaveAs(Server.MapPath(DocPath) + filename1);
                        return (DocPath) + filename1;
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
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

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
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    //txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtEndDate.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
                    txtNewsDescription.Text = string.Empty;
                    ddlLanguage.Enabled = false;
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
        #endregion

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}