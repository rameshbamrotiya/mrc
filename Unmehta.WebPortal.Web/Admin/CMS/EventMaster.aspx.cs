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
using System.Linq;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class EventMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
                ViewState["TunitSocialMedia"] = null;
                LanguageMasterBAL objBo = new LanguageMasterBAL();
                ddlLanguage.DataSource = objBo.GetAllLanguage();
                ddlLanguage.DataTextField = "Name";
                ddlLanguage.DataValueField = "Id";
                ddlLanguage.DataBind();
                FillCountry(Convert.ToInt32(ddlLanguage.SelectedValue));
            }
        }
        private void FillCountry(int LangId)
        {
            DataSet ds = new DataSet();
            EventMasterBAL objBAL = new EventMasterBAL();
            ds = objBAL.SelectEventType(LangId);
            DataTable dtEventType = ds.Tables[0];
            PopulateDropDownList(ddlEventType, dtEventType, "EventName", "Id", true);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["EventId"]);
                    if (e.CommandName == "eDelete")
                    {
                        EventMasterBO objBo = new EventMasterBO();
                        objBo.EventId = bytID;
                        new EventMasterBAL().DeleteRecord(objBo);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            EventMasterBO objBo = new EventMasterBO();
            objBo.EventId = iPkId;
            objBo.LanguageId = languageId;
            objBo.IsOnlineRegistration = null;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new EventMasterBAL().SelectRecord(objBo);
            DataSet DsPatronlist = new EventMasterBAL().SelectRecordPatronlist(objBo);
            DataSet DsSocialmediaLinks = new EventMasterBAL().SelectRecordSocialmediaLinks(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtEventName.Text = "";
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            string strPatronId = DsPatronlist.Tables[0].Rows[0]["PatronlistName"].ToString();
            if (!string.IsNullOrWhiteSpace(strPatronId))
            {
                txtPatronlist.Text =HttpUtility.HtmlDecode( strPatronId);
            }
            else
            {
                txtPatronlist.Text = "";
            }


            gviewSocialmediaLinks.DataSource = DsSocialmediaLinks.Tables[0];
            gviewSocialmediaLinks.DataBind();
            //ViewState["T017PDetails"] = DsPatronlist.Tables[0];
            ViewState["TunitSocialMedia"] = DsSocialmediaLinks.Tables[0];
            if (dr["EventGalalry"] != DBNull.Value)
                txtGallery.Text = dr["EventGalalry"].ToString();
            if (dr["EventName"] != DBNull.Value)
                txtEventName.Text = dr["EventName"].ToString();
            if (dr["StartTimeHH"] != DBNull.Value)
                txtSunStartTimeHour.Text = dr["StartTimeHH"].ToString();
            if (dr["StartTimeMM"] != DBNull.Value)
                txtSunStartTimeMin.Text = dr["StartTimeMM"].ToString();
            if (dr["StartTimeTT"] != DBNull.Value)
                ddlSunStartTimeTT.SelectedValue = dr["StartTimeTT"].ToString();
            if (dr["Location"] != DBNull.Value)
                txtEventLocation.Text = dr["Location"].ToString();
            if (dr["Venue"] != DBNull.Value)
                txtEventVenue.Text = dr["Venue"].ToString();

            if (dr["EventStartDate"] != DBNull.Value)
                txtStartDate.Text = Convert.ToDateTime(dr["EventStartDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;

            if (dr["EventEndDate"] != DBNull.Value)
                txtEndDate.Text = Convert.ToDateTime(dr["EventEndDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;

            if (dr["EventTypeId"] != DBNull.Value)
                ddlEventType.SelectedValue = dr["EventTypeId"].ToString();
            if (dr["Seat"] != DBNull.Value)
                txtseats.Text = dr["Seat"].ToString();
            if (dr["Organizer"] != DBNull.Value)
                txtEventOrganizer.Text = dr["Organizer"].ToString();
            if (dr["Phone"] != DBNull.Value)
                txtphoneno.Text = dr["Phone"].ToString();
            if (dr["Email"] != DBNull.Value)
                txtemail.Text = dr["Email"].ToString();
            if (dr["Websitelink"] != DBNull.Value)
                txtweblink.Text = dr["Websitelink"].ToString();
            if (dr["OrganizedBy"] != DBNull.Value)
                txtOrganizedby.Text = dr["OrganizedBy"].ToString();
            if (dr["EventDetails"] != DBNull.Value)
                txtEventDetails.Text = dr["EventDetails"].ToString();
            if (dr["IsOnlineRegistration"] != DBNull.Value)
                chkIsOnlineRegistration.Checked = string.IsNullOrWhiteSpace(dr["IsOnlineRegistration"].ToString())?false: Convert.ToBoolean(dr["IsOnlineRegistration"].ToString());
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInActive.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";

            if (dr["MainImg"] != DBNull.Value)
            {
                if (!string.IsNullOrWhiteSpace(dr["MainImg"].ToString()))
                {
                    hfMainImage.Value = dr["MainImg"].ToString();
                    aRemoveMain.Visible = true;
                    lblMainImage.Text = dr["MainImg"].ToString();
                }
            }

            if (dr["InnerImg"] != DBNull.Value)
            {
                if (!string.IsNullOrWhiteSpace(dr["InnerImg"].ToString()))
                {
                    hfInnerImage.Value = dr["InnerImg"].ToString();
                    lblInnerImage.Text = dr["InnerImg"].ToString();
                    aRemoveInner.Visible = true;
                }
            }
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(EventMasterBO objBo)
        {
            DateTime? dtStartDate = null;
            string documentfile = string.Empty;
            documentfile = SaveFileMain();
            if (!string.IsNullOrEmpty(documentfile))
                objBo.MainImg = documentfile;
            string documentfileinner = string.Empty;
            documentfileinner = SaveFileInnerImg();
            if (!string.IsNullOrEmpty(documentfileinner))
                objBo.InnerImg = documentfileinner;


            if (!string.IsNullOrEmpty(txtGallery.Text))
                objBo.EventGalalry = txtGallery.Text;

            if (!string.IsNullOrEmpty(txtEventName.Text))
                objBo.EventName = txtEventName.Text;
            if (!string.IsNullOrEmpty(txtSunStartTimeHour.Text))
                objBo.StartTimeHH = txtSunStartTimeHour.Text;
            if (!string.IsNullOrEmpty(txtSunStartTimeMin.Text))
                objBo.StartTimeMM = txtSunStartTimeMin.Text;
            objBo.StartTimeTT = ddlSunStartTimeTT.SelectedValue;
            if (!string.IsNullOrEmpty(txtEventVenue.Text))
                objBo.Venue = txtEventVenue.Text;
            if (!string.IsNullOrEmpty(txtEventLocation.Text))
                objBo.Location = txtEventLocation.Text;
            if (!string.IsNullOrEmpty(txtseats.Text))
                objBo.Seat = txtseats.Text;
            if (!string.IsNullOrEmpty(txtEventOrganizer.Text))
                objBo.Organizer = txtEventOrganizer.Text;
            if (!string.IsNullOrEmpty(txtphoneno.Text))
                objBo.Phone = txtphoneno.Text;
            if (!string.IsNullOrEmpty(txtemail.Text))
                objBo.Email = txtemail.Text;
            if (!string.IsNullOrEmpty(txtweblink.Text))
                objBo.Websitelink = txtweblink.Text;
            if (!string.IsNullOrEmpty(txtOrganizedby.Text))
                objBo.OrganizedBy = txtOrganizedby.Text;
            if (!string.IsNullOrEmpty(txtEventDetails.Text))
                objBo.EventDetails = txtEventDetails.Text;
            if (!string.IsNullOrEmpty(txtEventLocation.Text))
                objBo.Location = txtEventLocation.Text;

            if (!string.IsNullOrEmpty(Convert.ToString(txtStartDate.Text)))
            {
                DateTime dStartDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", null);
                dtStartDate = new DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(dtStartDate)))
                objBo.EventStartDate = dtStartDate;

            if (!string.IsNullOrEmpty(Convert.ToString(txtEndDate.Text)))
            {
                DateTime dStartDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
                dtStartDate = new DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(dtStartDate)))
                objBo.EventEndDate = dtStartDate;

            objBo.EventTypeId = ddlEventType.SelectedValue;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.IsActive = ddlActiveInActive.SelectedValue.ToString();

            objBo.IsOnlineRegistration = chkIsOnlineRegistration.Checked;

            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                aRemoveInner.Visible = false;
                aRemoveMain.Visible = false;
                lblInnerImage.Text = "";
                lblMainImage.Text = "";
                ViewState["T017PDetails"] = null;
                ViewState["TunitSocialMedia"] = null;
                ShowHideControl(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private DataTable GetGridViewData()
        {
            DataTable dt = (DataTable)ViewState["T017PDetails"];

            return dt;
        }
        private DataTable GetGridViewDataUnit()
        {
            DataTable dt = new DataTable();
            if (gviewSocialmediaLinks.Rows.Count > 0)
            {

                dt.Columns.Add("SocialMediaName");
                dt.Columns.Add("SocialMediaLink");
                foreach (GridViewRow row in gviewSocialmediaLinks.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["SocialMediaName"] = row.Cells[1].Text;
                    dr["SocialMediaLink"] = row.Cells[2].Text;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

            }
            else
            {
                dt.Columns.Add("SocialMediaName");
                dt.Columns.Add("SocialMediaLink");
                dt.AcceptChanges();

            }
            return dt;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt1 = null;
                if (ViewState["T017PDetails"] == null)
                {
                    dt1 = new DataTable("tbl");
                    dt1.Columns.Add("PatronlistName");
                }
                else
                {
                    dt1 = (DataTable)(ViewState["T017PDetails"]);
                }
                DataRow dr = dt1.NewRow();
                dr["PatronlistName"] = HttpUtility.HtmlEncode(txtPatronlist.Text);
                dt1.Rows.Add(dr);
                dt1.AcceptChanges();

                ViewState["T017PDetails"] = dt1;

                if (string.IsNullOrEmpty(txtEventName.Text))
                {
                    Functions.MessagePopup(this, "Please Add Event Name.", PopupMessageType.error);
                    return;
                }
                if (string.IsNullOrEmpty(txtOrganizedby.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Organized by.", PopupMessageType.error);
                    return;
                }
                if (string.IsNullOrEmpty(txtEventDetails.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Event Details.", PopupMessageType.error);
                    return;
                }
                if (ddlEventType.SelectedValue == "0")
                {
                    Functions.MessagePopup(this, "Please select Event Type.", PopupMessageType.error);
                    return;
                }
                EventMasterBO objBo = new EventMasterBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                DataTable dtunit = new DataTable();
                dtunit = GetGridViewDataUnit();
                if (dt.Rows.Count > 0)
                {
                    if (new EventMasterBAL().InsertRecord(objBo, dt, dtunit))
                    {
                        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        ViewState["T017PDetails"] = null;
                        ViewState["TunitSocialMedia"] = null;
                        txtPatronlist.Text = "";
                        gviewSocialmediaLinks.DataSource = (DataTable)ViewState["TunitSocialMedia"];
                        gviewSocialmediaLinks.DataBind();
                        //gvPatronlist.DataSource = (DataTable)ViewState["T017PDetails"];
                        //gvPatronlist.DataBind();
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
                    Functions.MessagePopup(this, "Please Add Sub Details.", PopupMessageType.warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEventName.Text))
                {
                    Functions.MessagePopup(this, "Please Add Event Name.", PopupMessageType.error);
                    return;
                }
                if (string.IsNullOrEmpty(txtOrganizedby.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Organized by.", PopupMessageType.error);
                    return;
                }
                if (string.IsNullOrEmpty(txtEventDetails.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Event Details.", PopupMessageType.error);
                    return;
                }
                if (ddlEventType.SelectedValue == "0")
                {
                    Functions.MessagePopup(this, "Please select Event Type.", PopupMessageType.error);
                    return;
                }
                EventMasterBO objBo = new EventMasterBO();

                DataTable dt1 = null;
                if (ViewState["T017PDetails"] == null)
                {
                    dt1 = new DataTable("tbl");
                    dt1.Columns.Add("PatronlistName");
                }
                else
                {
                    dt1 = (DataTable)(ViewState["T017PDetails"]);
                }
                DataRow dr = dt1.NewRow();
                dr["PatronlistName"] = HttpUtility.HtmlEncode(txtPatronlist.Text);
                dt1.Rows.Add(dr);
                dt1.AcceptChanges();

                ViewState["T017PDetails"] = dt1;
                txtPatronlist.Text = "";

                LoadControls(objBo);
                objBo.id = Convert.ToInt32(ViewState["PKImg"]);
                objBo.EventId = Convert.ToInt32(ViewState["PK"]);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                DataTable dtunit = new DataTable();
                dtunit = GetGridViewDataUnit();
                if (dt.Rows.Count > 0)
                {
                    if (new EventMasterBAL().UpdateRecord(objBo, dt, dtunit))
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
                else
                {
                    Functions.MessagePopup(this, "Please Add Sub Details.", PopupMessageType.warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
                ViewState["TunitSocialMedia"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:

                    hfMainImage.Value = "";
                    lblMainImage.Text = "";
                    aRemoveMain.Visible = false;

                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;

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

                    hfMainImage.Value = "";
                    lblMainImage.Text = "";
                    aRemoveMain.Visible = false;

                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;

                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    txtPatronlist.Text = "";
                    gviewSocialmediaLinks.Visible = false;
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

        //protected void btnAddPatronlist_ServerClick(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtPatronlist.Text))
        //    {
        //        Functions.MessagePopup(this, "Please Add Patronlist", PopupMessageType.success);
        //        return;
        //    }
        //    DataTable dt = null;
        //    if (ViewState["T017PDetails"] == null)
        //    {
        //        dt = new DataTable("tbl");
        //        dt.Columns.Add("PatronlistName");
        //        dt.Columns.Add("id");
        //    }
        //    else
        //    {
        //        dt = (DataTable)(ViewState["T017PDetails"]);
        //    }
        //    DataRow dr = dt.NewRow();
        //    dr["PatronlistName"] = HttpUtility.HtmlEncode(txtPatronlist.Text);
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();

        //    ViewState["T017PDetails"] = dt;
        //    txtPatronlist.Text = "";
        //}

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
        //        {
        //            int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
        //            if (string.IsNullOrEmpty(Convert.ToString(gvPatronlist.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
        //            {
        //                int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
        //                DataTable dt = ViewState["T017PDetails"] as DataTable;
        //                dt.Rows[index].Delete();
        //                ViewState["T017PDetails"] = dt;
        //                gvPatronlist.DataSource = dt;
        //                gvPatronlist.DataBind();
        //            }
        //            else
        //            {
        //                Int32 bytID;
        //                bytID = Convert.ToInt32(gvPatronlist.DataKeys[intIndex].Values["id"]);
        //                if (e.CommandName == "eDelete")
        //                {
        //                    EventMasterBO objBo = new EventMasterBO();
        //                    DataTable dt = ViewState["T017PDetails"] as DataTable;
        //                    dt.Rows[intIndex].Delete();
        //                    ViewState["T017PDetails"] = dt;
        //                    objBo.EventId = bytID;
        //                    new EventMasterBAL().DeleteRecordPatronlist(objBo);
        //                    BindGridView();
        //                    gvPatronlist.DataBind();
        //                    Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
        //                    ShowMessage("Record deleted successfully.", MessageType.Success);
        //                    return;
        //                }
        //                ClearControlValues(pnlEntry);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
        //    }
        //}

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void AddSocialMediaLink_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSocialmedianame.Text) && fuLeaflet.HasFile)
            {
                Functions.MessagePopup(this, "Please Add Leaflet Language Name and Select File", PopupMessageType.success);
                return;
            }
            gviewSocialmediaLinks.Visible = true;
            DataTable dt = null;
            if (ViewState["TunitSocialMedia"] == null)
            {
                dt = new DataTable("tbl");
                dt.Columns.Add("id");
                dt.Columns.Add("SocialMediaName");
                dt.Columns.Add("SocialMediaLink");
            }
            else
            {
                dt = (DataTable)(ViewState["TunitSocialMedia"]);
            }
            string filepath = "";
            if(fuLeaflet.HasFiles)
            {
                filepath= SaveFileLeafletGujarti();
            }
            else
            {
                filepath = hfLeafletFile.Value;
            }
            DataRow dr = dt.NewRow();
            dr["SocialMediaName"] = txtSocialmedianame.Text;
            dr["SocialMediaLink"] = filepath;
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            gviewSocialmediaLinks.DataSource = dt;
            gviewSocialmediaLinks.DataBind();
            ViewState["TunitSocialMedia"] = dt;
            lblhfLeafletFile.Text = "";
            hfLeafletFile.Value = "";
            txtSocialmedianame.Text = "";
        }

        protected void gviewSocialmediaLinks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(gviewSocialmediaLinks.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["TunitSocialMedia"] as DataTable;
                        dt.Rows[index].Delete();
                        ViewState["TunitSocialMedia"] = dt;
                        gviewSocialmediaLinks.DataSource = dt;
                        gviewSocialmediaLinks.DataBind();
                    }
                    else
                    {
                        Int32 bytID;
                        bytID = Convert.ToInt32(gviewSocialmediaLinks.DataKeys[intIndex].Values["id"]);
                        if (e.CommandName == "eDelete")
                        {
                            EventMasterBO objBo = new EventMasterBO();
                            DataTable dt = ViewState["TunitSocialMedia"] as DataTable;
                            dt.Rows[intIndex].Delete();
                            ViewState["TunitSocialMedia"] = dt;
                            objBo.EventId = bytID;
                            new EventMasterBAL().DeleteRecordSocialmediaLinks(objBo);
                            BindGridView();
                            gviewSocialmediaLinks.DataBind();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        ClearControlValues(pnlEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private string SaveFileMain()
        {
            try
            {
                if (fumainimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Event;
                    var fname = Path.GetExtension(fumainimg.FileName);
                    var count = fumainimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fumainimg.FileName.Split('.').Length; i++)
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
                        var filename1 = fumainimg.FileName.Replace(" ", "_");
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
                        fumainimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }
        private string SaveFileInnerImg()
        {
            try
            {
                if (fuinnerimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Event;
                    var fname = Path.GetExtension(fuinnerimg.FileName);
                    var count = fuinnerimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuinnerimg.FileName.Split('.').Length; i++)
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
                        var filename1 = fuinnerimg.FileName.Replace(" ", "_");
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
                        fuinnerimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }
        private string SaveFileLeafletGujarti()
        {
            try
            {

                if (fuLeaflet.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Event;
                    var fname = Path.GetExtension(fuLeaflet.FileName);
                    var count = fuLeaflet.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuLeaflet.FileName.Split('.').Length; i++)
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
                        var filename1 = fuLeaflet.FileName.Replace(" ", "_");
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
                        fuLeaflet.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }
    }
}