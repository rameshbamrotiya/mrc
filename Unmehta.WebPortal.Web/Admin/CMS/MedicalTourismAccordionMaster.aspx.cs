using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Common;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class MedicalTourismAccordionMaster : System.Web.UI.Page
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
                    BindMTAType(1);
                }
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["MTA_id"]);
                    Int32 MTADetailId;
                    MTADetailId = Convert.ToInt32(gView.DataKeys[intIndex].Values["id"]);
                    if (e.CommandName == "eDelete")
                    {
                        MedicalTourismAccordionMasterBO objBo = new MedicalTourismAccordionMasterBO();
                        objBo.MTA_id = bytID;
                        new MedicalTourismAccordionMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, MTADetailId, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ViewState["PKDetailId"] = MTADetailId;
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
            if (new MedicalTourismAccordionMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
        private bool FillControls(Int32 iPkId, Int32 MTADetailId, int languageId)
        {
            MedicalTourismAccordionMasterBO objBo = new MedicalTourismAccordionMasterBO();
            objBo.MTA_id = iPkId;
            objBo.LanguageId = languageId;
            MedicalTourismAccordionSubMasterBO objSubBO = new MedicalTourismAccordionSubMasterBO();
            objSubBO.MTADetails_Id = MTADetailId;
            hfTemplateId.Value = iPkId.ToString();
            hfSubMTADetailsId.Value = MTADetailId.ToString();
            DataSet ds = new MedicalTourismAccordionMasterBAL().SelectRecord(objBo);
            DataSet dsDetails = new MedicalTourismAccordionMasterBAL().SelectRecordMTADetails(objSubBO);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtTitle.Text = "";
                return false;
            }

            BindMTAType(languageId);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Title"] != DBNull.Value)
                txtTitle.Text = dr["Title"].ToString();
            ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
            txtsequence.Text = dr["MTA_level_id"].ToString();
            txtMetaTitle.Text = dr["MetaTitle"].ToString(); ;
            txtMetaDesc.Text = dr["MetaDescription"].ToString();
            BindMTAType(languageId);
            SessionWrapper.LanguageId = languageId;

            subgridView.DataSource = dsDetails.Tables[0];
            subgridView.DataBind();
            ViewState["T017PDetails"] = dsDetails.Tables[0];

            return true;
        }

        private void FillPacketType(int languageId)
        {
            try
            {
                DataSet dsPT = new DataSet();
                MedicalTourismAccordionMasterBAL objBAL = new MedicalTourismAccordionMasterBAL();
                dsPT = objBAL.FillMTAType(languageId);
                if (dsPT.Tables[0].Rows.Count > 0)
                {
                    ddlMTAType.DataSource = dsPT.Tables[0];
                    ddlMTAType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
                }
                else
                {

                    ddlMTAType.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Sub GridView Operation
        private void BindSubGridView()
        {
            int MTAId = Convert.ToInt32(hfId.Value);
            MedicalTourismAccordionMasterBAL objbal = new MedicalTourismAccordionMasterBAL();
            DataSet ds = objbal.GetSubMTAGrid(MTAId);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                subgridView.DataSource = dt;
                subgridView.DataBind();
            }
            else
            {
                subgridView.DataBind();
            }
        }
        protected void subgridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    if (string.IsNullOrEmpty(Convert.ToString(subgridView.DataKeys[intIndex].Values["Id"])) && e.CommandName == "eDelete")
                    {
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        DataTable dt = ViewState["T017PDetails"] as DataTable;
                        dt.Rows[index].Delete();
                        ViewState["T017PDetails"] = dt;
                        subgridView.DataSource = dt;
                        subgridView.DataBind();

                        int PkgTypeId = Convert.ToInt32(subgridView.DataKeys[intIndex].Values["MTAId"]);
                        BindMTATypeById(SessionWrapper.LanguageId, PkgTypeId);

                    }
                    else
                    {
                        Int32 bytID;
                        bytID = Convert.ToInt32(subgridView.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            PhotogalleryMasterBO objBo = new PhotogalleryMasterBO();
                            DataTable dt = ViewState["T017PDetails"] as DataTable;
                            dt.Rows[intIndex].Delete();
                            ViewState["T017PDetails"] = dt;
                            subgridView.DataSource = dt;
                            subgridView.DataBind();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        //ClearControlValues(pnlEntry);
                        if (FillControlAccredations(bytID))
                        {
                            if (e.CommandName == "eView")
                                ShowHideControl(VisibityType.View);
                            if (e.CommandName == "eEdit")
                            {
                                ShowHideControl(VisibityType.Edit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void subgridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subgridView.PageIndex = e.NewPageIndex;
            BindSubGridView();
        }
        #endregion

        #region Save || Update || Cancel
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            if (subgridView.Rows.Count > 0)
            {

                dt = new DataTable("tbl");
                dt.Columns.Add("SubTitle");
                dt.Columns.Add("Price");
                dt.Columns.Add("MTAId");
                dt.Columns.Add("MTAType");
                dt.Columns.Add("Description");
                dt.Columns.Add("Img_path");
                dt.Columns.Add("Is_active");
                foreach (GridViewRow row in subgridView.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["SubTitle"] = row.Cells[1].Text;
                    dr["Price"] = row.Cells[2].Text;
                    dr["MTAId"] = row.Cells[3].Text;
                    dr["MTAType"] = row.Cells[4].Text;
                    dr["Description"] = HttpUtility.HtmlDecode(row.Cells[5].Text);
                    Label img = (Label)row.FindControl("ImagrUrl");
                    dr["Img_path"] = img.Text.ToString();
                    dr["Is_active"] = row.Cells[7].Text;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        private void LoadControls(MedicalTourismAccordionMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.MTA_level_id = Convert.ToInt32(txtsequence.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        private void LoadControlsSub(MedicalTourismAccordionSubMasterBO objBo)
        {

            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }
            if (!string.IsNullOrEmpty(txtSubTitle.Text))
                objBo.SubTitle = txtSubTitle.Text;
            if (!string.IsNullOrEmpty(txtPrice.Text))
                objBo.Price = Convert.ToDecimal(txtPrice.Text);
            if (!string.IsNullOrEmpty(ddlMTAType.SelectedValue))
            {
                objBo.MTAId = Convert.ToInt32(ddlMTAType.SelectedValue);
                objBo.MTAType = ddlMTAType.SelectedItem.Text;
            }
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                objBo.Description = txtDescription.Text;
            }
            objBo.Img_path = " ";
            objBo.Is_active = ddlStatus.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new MedicalTourismAccordionMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["MTA_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["MTA_level_id"].ToString();
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
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.MedicalTourism;
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
                    btnFinalSave.Visible = false;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btnFinalSave.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btnFinalSave.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btnFinalSave.Visible = true;
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(hfSubMTADetailsId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        private void BindMTAType(int languageId)
        {
            DataSet ds = new DataSet();
            MedicalTourismAccordionMasterBAL objBAL = new MedicalTourismAccordionMasterBAL();

            ds = objBAL.FillMTAType(Convert.ToInt32(languageId));

            DataTable dt = ds.Tables[0];
            PopulateDropDownList(ddlMTAType, dt, "PackageType", "Id", true);
        }

        private void BindMTATypeById(int languageId, int PkgtypeId)
        {
            try
            {
                DataSet ds = new DataSet();
                MedicalTourismAccordionMasterBAL objBAL = new MedicalTourismAccordionMasterBAL();

                ds = objBAL.FillMTATypeById(Convert.ToInt32(languageId), PkgtypeId);

                DataTable dt = ds.Tables[0];
                // PopulateDropDownList(ddlMTAType, dt, "MTAType", "Id", true);
                ddlMTAType.SelectedValue = dt.Rows[0]["PackageType"].ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void btnSubDetailsAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubTitle.Text))
                {
                    Functions.MessagePopup(this, "Please enter title.", PopupMessageType.error);
                    txtSubTitle.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPrice.Text))
                {
                    Functions.MessagePopup(this, "Please enter price.", PopupMessageType.error);
                    txtPrice.Focus();
                    return;
                }
                if (ddlMTAType.SelectedIndex == 0)
                {
                    Functions.MessagePopup(this, "Please enter MTA type.", PopupMessageType.error);
                    ddlMTAType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    Functions.MessagePopup(this, "Please enter description.", PopupMessageType.error);
                    txtDescription.Focus();
                    return;
                }
                subgridView.Visible = true;
                DataTable dt = null;
                if (hfId.Value == "0")
                {
                    if (ViewState["T017PDetails"] == null)
                    {
                        dt = new DataTable("tbl");
                        dt.Columns.Add("SubTitle");
                        dt.Columns.Add("Price");
                        dt.Columns.Add("MTAId");
                        dt.Columns.Add("MTAType");
                        dt.Columns.Add("Description");
                        dt.Columns.Add("Img_path");
                        dt.Columns.Add("Is_active");
                        dt.Columns.Add("Id");
                    }
                    else
                    {
                        dt = (DataTable)(ViewState["T017PDetails"]);
                    }
                    DataRow dr = dt.NewRow();
                    dr["SubTitle"] = txtSubTitle.Text;
                    dr["Price"] = txtPrice.Text;
                    dr["MTAId"] = ddlMTAType.SelectedValue;
                    dr["MTAType"] = ddlMTAType.SelectedItem.Text;
                    dr["Description"] = txtDescription.Text;
                    string filepath = " "; // SaveFile();
                    dr["Img_path"] = filepath;
                    dr["Is_active"] = Convert.ToBoolean(ddlStatus.SelectedValue == "1" ? "true" : "false");
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    subgridView.DataSource = dt;
                    subgridView.DataBind();
                    ViewState["T017PDetails"] = dt;
                }
                else
                {
                    MedicalTourismAccordionSubMasterBO objBo = new MedicalTourismAccordionSubMasterBO();
                    LoadControlsSub(objBo);
                    DataTable dtDetail = new DataTable();
                    if (new MedicalTourismAccordionMasterBAL().UpdateSubMTARecord(objBo))
                    {
                        Functions.MessagePopup(this, "Record update successfully.", PopupMessageType.success);
                        MedicalTourismAccordionSubMasterBO objSubBO = new MedicalTourismAccordionSubMasterBO();
                        objSubBO.MTADetails_Id = Convert.ToInt32(hfSubMTADetailsId.Value);
                        DataSet dsDetails = new MedicalTourismAccordionMasterBAL().SelectRecordMTADetails(objSubBO);
                        subgridView.DataSource = dsDetails.Tables[0];
                        subgridView.DataBind();
                        ViewState["T017PDetails"] = dsDetails.Tables[0];
                    }
                }
                txtSubTitle.Text = "";
                txtPrice.Text = "";
                ddlMTAType.SelectedIndex = 0;
                ddlMTAType.SelectedIndex = 0;
                txtDescription.Text = "";
                hfId.Value = "0";
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private bool FillControlAccredations(int bytID)
        {
            MedicalTourismAccordionSubMasterBO objBo = new MedicalTourismAccordionSubMasterBO();
            objBo.Id = bytID;
            DataSet ds = new MedicalTourismAccordionMasterBAL().SelectSubMTARecord(bytID);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Id"] != DBNull.Value)
                hfId.Value = dr["Id"].ToString();
            if (dr["SubTitle"] != DBNull.Value)
                txtSubTitle.Text = dr["SubTitle"].ToString();
            if (dr["Price"] != DBNull.Value)
                txtPrice.Text = dr["Price"].ToString();
            if (dr["MTAId"] != DBNull.Value)
                ddlMTAType.SelectedValue = dr["MTAId"].ToString();
            if (dr["Description"] != DBNull.Value)
                txtDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
            if (dr["Is_active"] != DBNull.Value)
                ddlStatus.SelectedValue = dr["Is_active"].ToString() == "False" ? "0" : "1";
            filename.Visible = true;
            string imgPath = dr["Img_path"].ToString();
            filename.InnerText = imgPath.ToString();
            return true;
        }
        protected void btnFinalSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                MedicalTourismAccordionMasterBO objBo = new MedicalTourismAccordionMasterBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (new MedicalTourismAccordionMasterBAL().InsertRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    ViewState["T017PDetails"] = null;
                    subgridView.DataSource = (DataTable)ViewState["T017PDetails"];
                    subgridView.DataBind();
                    //ShowMessage("Record inserted successfully.", MessageType.Success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    //ShowMessage("Record already exists in database.", MessageType.Success);
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
                MedicalTourismAccordionMasterBO objBo = new MedicalTourismAccordionMasterBO();
                LoadControls(objBo);
                objBo.MTA_id = Convert.ToInt32(ViewState["PK"]);
                objBo.MTADetails_Id = Convert.ToInt32(ViewState["PKDetailId"]);
                DataTable dt = new DataTable();
                dt = GetGridViewData();
                if (new MedicalTourismAccordionMasterBAL().UpdateRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                    //ShowMessage("Record updated successfully.", MessageType.Success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    //ShowMessage("Record already exists in database.", MessageType.Success);
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
        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                subgridView.DataBind();
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
    }
}