using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Globalization;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class StarOfTheWeekMaster : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login");
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
            }
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    Fillunitdropdown();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["S_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        StarOfTheWeekBO objBo = new StarOfTheWeekBO();
                        objBo.S_id = bytID;
                        new StarOfTheWeekBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            StarOfTheWeekBO objBo = new StarOfTheWeekBO();
            objBo.S_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new StarOfTheWeekBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0))
            {
                txtMetaTitle.Text = "";
                txtMetaDescription.Text = "";
                //txtFromDate.Text = "";
                //txtToDate.Text = "";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr.HasErrors) return false;
                if (dr["Metatitle"] != DBNull.Value)
                    txtMetaTitle.Text = Convert.ToString(dr["Metatitle"]);

                if (dr["MetaDescription"] != DBNull.Value)
                    txtMetaDescription.Text = Convert.ToString(dr["MetaDescription"]);
                if (dr["StarOfThe"] != DBNull.Value)
                    rblstar.SelectedValue = Convert.ToString(dr["StarOfThe"]);
                if (rblstar.SelectedValue == "Week")
                {
                    week.Visible = true;
                    Fillunitdropdown();
                }
                else
                {
                    week.Visible = false;
                }
                if (rblstar.SelectedValue == "Month")
                {
                    month.Visible = true;
                    //datefrom.Visible = true;
                    //dateto.Visible = true;
                }
                else
                {
                    month.Visible = false;
                    //datefrom.Visible = false;
                    //dateto.Visible = false;
                }
                if (dr["StarOfThe"].ToString() == "Month")
                {
                    //txtFromDate.Text = Convert.ToDateTime(dr["FromDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //txtToDate.Text = Convert.ToDateTime(dr["ToDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ddlMonth.SelectedValue = dr["FromDate"].ToString();
                }
                else
                {
                    if (dr["Week"] != DBNull.Value)
                        ddlWeekDropDown.SelectedValue = dr["Week"].ToString();
                }
                if (dr["is_active"] != DBNull.Value)
                    ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
                filename.InnerText = dr["Imgpath"].ToString();
            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(StarOfTheWeekBO objBo)
        {
            if (fuPic.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.Imgpath = documentfile;
            }
            else
            {
                objBo.Imgpath = filename.InnerText.ToString();
            }
            if (!string.IsNullOrEmpty(rblstar.SelectedValue))
                objBo.StarOfThe = rblstar.SelectedValue.ToString();
            objBo.MetaDescription = txtMetaDescription.Text;
            objBo.Metatitle = txtMetaTitle.Text;
            if (rblstar.SelectedValue == "Week")
            {
                objBo.FromDate = null;
                objBo.ToDate = null;
                objBo.Week = ddlWeekDropDown.SelectedValue;
            }
            else
            {
                //objBo.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
                //objBo.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
                objBo.Week = "";
            }
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress();
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                StarOfTheWeekBO objBo = new StarOfTheWeekBO();
                LoadControls(objBo);
                if (new StarOfTheWeekBAL().InsertRecord(objBo))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                StarOfTheWeekBO objBo = new StarOfTheWeekBO();
                LoadControls(objBo);
                objBo.S_id = Convert.ToInt32(ViewState["PK"]);
                if (new StarOfTheWeekBAL().UpdateRecord(objBo))
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
        private string SaveFile()
        {
            try
            {

                if (fuPic.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.StarOfTheWeek_Month;
                    var fname = Path.GetExtension(fuPic.FileName);
                    var count = fuPic.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuPic.FileName.Split('.').Length; i++)
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
                        var filename1 = fuPic.FileName.Replace(" ", "_");

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
                        fuPic.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString());
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
                    filename.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues(pnlEntry);
                    //datefrom.Visible = false;
                    //dateto.Visible = false;
                    week.Visible = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    filename.Visible = true;
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
                    //datefrom.Visible = false;
                    //dateto.Visible = false;
                    week.Visible = false;
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void rblstar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblstar.SelectedValue == "Week")
            {
                week.Visible = true;
                Fillunitdropdown();
            }
            else
            {
                week.Visible = false;
            }
            if (rblstar.SelectedValue == "Month")
            {
                //datefrom.Visible = true;
                //dateto.Visible = true;
            }
            else
            {
                //datefrom.Visible = false;
                //dateto.Visible = false;
            }
        }
        private void Fillunitdropdown()
        {
            ddlWeekDropDown.Items.Clear();
            Array colors = Enum.GetValues(typeof(DayOfWeek));
            ddlWeekDropDown.Items.Add(new ListItem("Select Week Day", null));
            foreach (DayOfWeek color in colors)
            {
                ddlWeekDropDown.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
            ddlMonth.Items.Clear();
            Array colorss = Enum.GetValues(typeof(Month));
            ddlMonth.Items.Add(new ListItem("Select Week Day", null));
            foreach (Month color in colorss)
            {
                ddlMonth.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
        }
    }
}