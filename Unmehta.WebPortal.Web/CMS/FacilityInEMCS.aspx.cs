using BAL;
using ClassLib.BL;
using ClassLib.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.CMS
{
    public partial class FacilityInEMCS : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["TmpEMCS"] == null)
                {
                    CreateTempTable();
                }

                AddNewRecordRowToGrid();
                btnAdd.Text = "+ Add";
                hfFIEMDID.Value = "0";
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void CreateTempTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FIEMDID", typeof(string));
            dt.Columns.Add("Subtitle", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ImageUrl", typeof(string));
            dt.Columns.Add("LanguageID", typeof(int));
            dt.Columns.Add("Enabled", typeof(bool));
            dt.Columns.Add("Added_By", typeof(string));
            dt.Columns.Add("IP_Add", typeof(string));
            dt.Columns.Add("MetaDescription", typeof(string));
            dt.Columns.Add("MetaTitle", typeof(string));
            
            ViewState["TmpEMCS"] = dt;
        }

        protected void BindInnerGrid()
        {
            gvFacilitydtl.DataSource = (DataTable)ViewState["TmpEMCS"];
            gvFacilitydtl.DataBind();
        }

        public void LoadControls(FacilityInECMSDetailsBO objBo)
        {

            objBo.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Enabled = Convert.ToBoolean(ddlActiveInactive.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text.ToString();

            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text.ToString();

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text.ToString();



            objBo.Added_By = SessionWrapper.UserDetails.UserName;
            objBo.IP_Add = GetIPAddress;
        }

        private bool FillControls(int FIEMID, int languageId)
        {
            ClearControls();
            FacilityInECMSDetailsBO objBo = new FacilityInECMSDetailsBO();
            objBo.LanguageID = languageId;
            objBo.FIEMID = FIEMID;
            DataSet ds = new FacilityInECMSDetailsBAL().SelectRecord(objBo);
            if (!ds.Tables.Count.Equals(0))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr.HasErrors) return false;

                    //if (dr["RIID"] != DBNull.Value)
                    //    hfRIID.Value = Convert.ToString(dr["RIID"]);

                    if (dr["FIEMID"] != DBNull.Value)
                        hfFIEMID.Value = Convert.ToString(dr["FIEMID"]);

                    if (dr["LanguageID"] != DBNull.Value)
                        ddlLanguage.SelectedValue = Convert.ToString(dr["LanguageID"]);

                    if (dr["Title"] != DBNull.Value)
                        txtTitle.Text = Convert.ToString(dr["Title"]);

                    if (dr["Enabled"] != DBNull.Value)
                        ddlActiveInactive.SelectedValue = Convert.ToString(dr["Enabled"]);

                    if (dr["MetaTitle"] != DBNull.Value)
                        txtMetaTitle.Text = dr["MetaTitle"].ToString();

                    if (dr["MetaDescription"] != DBNull.Value)
                        txtMetaDescription.Text = dr["MetaDescription"].ToString();


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[1].Rows[0];
                    if (dr.HasErrors) return false;
                    ViewState["TmpEMCS"] = ds.Tables[1];

                }
            }
            BindInnerGrid();
            return true;
        }

        public void ClearControls()
        {
            //txtMetaTitle.Text = "";
            //txtMetaDescription.Text = "";


            aRemoveInner.Visible = false;
            lblInnerImage.Text = "";
            hfInnerImage.Value = "";


            txtSubtitle.Text = "";
            txtDescription.Text = "";
            ddlActiveInactive.SelectedIndex = -1;
            //ViewState["TmpEMCS"] = null;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void AddNewRecordRowToGrid()
        {
            if(hfFIEMDID.Value == "0")
            {
                // check view state is not null  
                if (ViewState["TmpEMCS"] != null)
                {
                    DataTable dtCurrentTable = new DataTable();
                    dtCurrentTable = (DataTable)ViewState["TmpEMCS"];
                    DataRow dr = dtCurrentTable.NewRow();
                    bool isError = false;

                    if (fuImg.HasFile)
                    {
                        string filepath = string.Empty;
                        filepath = SaveFile(out isError);

                        if (!string.IsNullOrEmpty(filepath) && !isError)
                        {

                            dr["ImageUrl"] = filepath;

                        }

                    }
                    else
                    {
                        dr["ImageUrl"] = hfInnerImage.Value;
                    }

                    dr["Subtitle"] = txtSubtitle.Text.ToString();
                    dr["Description"] = HttpUtility.HtmlEncode(txtDescription.Text.ToString());
                    dr["LanguageID"] = ddlLanguage.SelectedValue;
                    dr["Enabled"] = ddlActiveInactive.SelectedValue;
                    dr["Added_By"] = SessionWrapper.UserDetails.UserName;
                    dr["IP_Add"] = GetIPAddress;
                    dr["MetaDescription"] = txtMetaDescription.Text.ToString();
                    dr["MetaTitle"] = txtMetaTitle.Text.ToString();
                    dtCurrentTable.Rows.Add(dr);
                    dtCurrentTable.AcceptChanges();
                    ViewState["TmpEMCS"] = dtCurrentTable;
                    BindInnerGrid();
                    ClearControls();
                }
            }
            else
            {
                FacilityInECMSDetailsBO objBo = new FacilityInECMSDetailsBO();
                LoadControlsSub(objBo);
                DataTable dtDetail = new DataTable();
                if (new FacilityInECMSDetailsBAL().UpdateFacilityDetailsRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record update successfully.", PopupMessageType.success);
                    DataSet dsDetails = new FacilityInECMSDetailsBAL().SelectFacilityDetailsRecordByFIEMID(Convert.ToInt32(hfFIEMID.Value));
                    gvFacilitydtl.DataSource = dsDetails.Tables[0];
                    gvFacilitydtl.DataBind();
                    ViewState["TmpEMCS"] = dsDetails.Tables[0];
                    ClearControls();
                    txtSubtitle.Text = "";
                    txtDescription.Text = "";

                    hfInnerImage.Value = "";
                    lblInnerImage.Text = "";
                    aRemoveInner.Visible = false;                    
                }
            }

        }
        private void LoadControlsSub(FacilityInECMSDetailsBO objBo)
        {

            if (string.IsNullOrWhiteSpace(hfFIEMDID.Value))
            {
                objBo.FIEMDID = 0;
            }
            else
            {
                objBo.FIEMDID = Convert.ToInt32(hfFIEMDID.Value);
            }
            if (!string.IsNullOrEmpty(txtSubtitle.Text))
                objBo.Subtitle = txtSubtitle.Text;
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                objBo.Description = txtDescription.Text;
            }
            bool isError = false;
            if (fuImg.HasFile)
            {
                string filepath = string.Empty;
                filepath = SaveFile(out isError);
                objBo.ImageUrl = filepath;
            }
            else
            {
                objBo.ImageUrl = hfInnerImage.Value;
            }            
            objBo.Modify_By = SessionWrapper.UserDetails.UserName;
            objBo.IP_Add = GetIPAddress;           
        }
        private string SaveFile(out bool isError)
        {
            isError = false;
            try
            {
                if (fuImg.HasFile)
                {
                    var DocPath = ConfigDetailsValue.FacilityInEMCS;
                    string fileMimeType = fuImg.PostedFile.ContentType;

                    var fname = Path.GetExtension(fuImg.FileName);
                    var count = fuImg.FileName.Split('.');
                    int Extensioncount = fuImg.FileName.Count(f => f == '.');
                    var ValidFileTypes = new[] { "jpg", "jpeg", "png" };

                    string[] matchMimeType = { "application/jpeg", "application/jpg", "application/png" };

                    if (!ValidFileTypes.Contains(fname.Substring(1).ToLower()) && !matchMimeType.Contains(fileMimeType) && Extensioncount == 1)
                    {
                        Functions.MessagePopup(this, "File Extension Is InValid - Only Upload JPEG/JPG/PNG File.", PopupMessageType.error);
                        isError = true;
                        return "";
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuImg.FileName.Replace(" ", "_");
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
                        fuImg.SaveAs(Server.MapPath(DocPath) + filename1);
                        string NewFIlePath = ((DocPath) + filename1).ToString();
                        return NewFIlePath;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }

        protected void btnFinal_Click(object sender, EventArgs e)
        {
            try
            {
                FacilityInECMSDetailsBO objBo = new FacilityInECMSDetailsBO();
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["TmpEMCS"];
                if (new FacilityInECMSDetailsBAL().InsertRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record saved successfully.", PopupMessageType.success);
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.success);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btnFinal.Visible = false;
                    btn_Update.Visible = false;
                    break;
                case VisibityType.Insert:
                    aRemoveInner.Visible = false;
                    lblInnerImage.Text = "";
                    hfInnerImage.Value = "";

                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    pnlEntry.Visible = true;
                    btnFinal.Visible = true;
                    btn_Update.Visible = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btnFinal.Visible = false;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btnFinal.Visible = true;
                    btn_Update.Visible = false;
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FacilityInECMSDetailsBO objBo = new FacilityInECMSDetailsBO();
                objBo.FIEMID = Convert.ToInt32(hfFIEMID.Value);
                LoadControls(objBo);
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["TmpEMCS"];
                dt.Columns.Remove("FIEMDID");
                if (new FacilityInECMSDetailsBAL().UpdateRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record saved successfully.", PopupMessageType.success);
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                    hfFIEMDID.Value = "0";
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void BindGridView()
        {
            gView.DataBind();
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                ViewState["TmpEMCS"] = null;
                ClearControls();                
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["FIEID"]);
                    Int32 FIEMID;
                    FIEMID = Convert.ToInt32(gView.DataKeys[intIndex].Values["FIEMID"]);
                    if (e.CommandName == "eDelete")
                    {
                        FacilityInECMSDetailsBO objBo = new FacilityInECMSDetailsBO();
                        objBo.FIEMID = bytID;
                        new FacilityInECMSDetailsBAL().DeleteRecord(objBo);
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
                            gView.Visible = true;
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gvFacilitydtl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable dtdel = new DataTable();
                dtdel = (DataTable)ViewState["TmpEMCS"];
                int index = Convert.ToInt32(e.RowIndex);
                dtdel.Rows[index].Delete();
                dtdel.AcceptChanges();
                ViewState["TmpEMCS"] = dtdel;
                BindInnerGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gvFacilitydtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacilitydtl.PageIndex = e.NewPageIndex;
            BindInnerGrid();
        }
        private bool FillControlFacility(int bytID)
        {
            btnAdd.Text = "Update";
            DataSet ds = new FacilityInECMSDetailsBAL().SelectFacilityDetailsRecord(bytID);
            if (!ds.Tables.Count.Equals(0))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr.HasErrors) return false;

                    if (dr["FIEMDID"] != DBNull.Value)
                        hfFIEMDID.Value = Convert.ToString(dr["FIEMDID"]);

                    if (dr["Subtitle"] != DBNull.Value)
                        txtSubtitle.Text = Convert.ToString(dr["Subtitle"]);

                    if (dr["Description"] != DBNull.Value)
                        txtDescription.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());

                    if (dr["ImageUrl"] != DBNull.Value)
                    {
                        if (!string.IsNullOrWhiteSpace(dr["ImageUrl"].ToString()))
                        {
                            aRemoveInner.Visible = true;
                            string imgPath = dr["ImageUrl"].ToString();
                            hfInnerImage.Value = imgPath.ToString();
                            lblInnerImage.Text = imgPath.ToString();
                        }
                    }
                }                
            }
            return true;
        }
        protected void gvFacilitydtl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;

                    Int32 bytID;
                    bytID = Convert.ToInt32(gvFacilitydtl.DataKeys[intIndex].Values["FIEMDID"]);
                    if (e.CommandName == "eDelete")
                    {
                        DataTable dtdel = new DataTable();
                        dtdel = (DataTable)ViewState["TmpEMCS"];
                        int index = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        dtdel.Rows[index].Delete();
                        dtdel.AcceptChanges();
                        ViewState["TmpEMCS"] = dtdel;
                        BindInnerGrid();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    //ClearControlValues(pnlEntry);
                    if (FillControlFacility(bytID))
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
    }
}