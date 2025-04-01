using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Web;

namespace Unmehta.WebPortal.Web.Admin.Faculty
{
    public partial class ExtraDetails : System.Web.UI.Page
    {
        public static int FacultyId, LanguageId;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    long id = 0;
                    string[] splitString = queryString.Split('|');
                    FacultyId = Convert.ToInt32(splitString[0]);
                    LanguageId = Convert.ToInt32(splitString[1]);
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                    ShowHideControlAreaExperience(VisibityType.GridView);
                    ShowHideControlPublicationResearch(VisibityType.GridView);
                    ShowHideControlAwards(VisibityType.GridView);
                    ShowHideControlService(VisibityType.GridView);
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        EducationDetailsBO objBo = new EducationDetailsBO();
                        objBo.Id = bytID;
                        new ExtraDetailsBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void gViewAreaExperience_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gViewAreaExperience.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        AreaExperienceBO objBo = new AreaExperienceBO();
                        objBo.Id = bytID;
                        new ExtraDetailsBAL().DeleteAreaExperienceRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    ClearControlValues(pnlAreaExperienceEntry);
                    if (FillControlsAreaExperience(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControlAreaExperience(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControlAreaExperience(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void gViewPublicationResearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gViewPublicationResearch.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        PublicationResearchDetailsBO objBo = new PublicationResearchDetailsBO();
                        objBo.Id = bytID;
                        new ExtraDetailsBAL().DeletePublicationResearchRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    ClearControlValues(pnlPublicationResearchEntry);
                    if (FillControlsPublicationResearch(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControlPublicationResearch(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControlPublicationResearch(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void gViewAwards_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gViewAwards.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        FacultyAwardsDetailsBO objBo = new FacultyAwardsDetailsBO();
                        objBo.Id = bytID;
                        new ExtraDetailsBAL().DeleteAwardsRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    ClearControlValues(pnlAreaExperienceEntry);
                    ClearControlValues(pnlAwardsEntry);
                    if (FillControlsAwards(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControlAwards(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControlAwards(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void gViewService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gViewService.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        FacultyServiceDetailsBO objBo = new FacultyServiceDetailsBO();
                        objBo.Id = bytID;
                        new ExtraDetailsBAL().DeleteServiceRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    ClearControlValues(pnlAreaExperienceEntry);
                    ClearControlValues(pnlAwardsEntry);
                    ClearControlValues(pnlServiceEntry);
                    if (FillControlsService(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControlService(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            ViewState["PK"] = bytID;
                            ShowHideControlService(VisibityType.Edit);
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
            EducationDetailsBO objBo = new EducationDetailsBO();
            objBo.Id = iPkId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new ExtraDetailsBAL().SelectEducationDetailsByID(objBo);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                txtEducation.Text = ds.Tables[0].Rows[0]["EducationName"].ToString();
                txtFromYear.Text = ds.Tables[0].Rows[0]["FromYear"].ToString();
                txtToYear.Text = ds.Tables[0].Rows[0]["ToYear"].ToString();
                txtDegree.Text = ds.Tables[0].Rows[0]["DegreeName"].ToString();
                return true;
            }
            else
            {
                txtEducation.Text = "";
                txtFromYear.Text = "";
                txtToYear.Text = "";
                txtDegree.Text = "";
                return false;
            }
        }
        private bool FillControlsAreaExperience(Int32 iPkId)
        {
            AreaExperienceBO objBo = new AreaExperienceBO();
            objBo.Id = iPkId;
            hfAreaExperienceId.Value = iPkId.ToString();
            DataSet ds = new ExtraDetailsBAL().SelectAreaExperienceDetailsByID(objBo);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                txtEmployerName.Text = ds.Tables[0].Rows[0]["EmployerName"].ToString();
                txtAreaFromYear.Text = ds.Tables[0].Rows[0]["FromYear"].ToString();
                txtAreaToYear.Text = ds.Tables[0].Rows[0]["ToYear"].ToString();
                chkIsPresent.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsPresent"].ToString());
                return true;
            }
            else
            {
                txtEmployerName.Text = "";
                txtAreaFromYear.Text = "";
                txtAreaToYear.Text = "";
                chkIsPresent.Checked = false;
                return false;
            }
        }
        private bool FillControlsPublicationResearch(Int32 iPkId)
        {
            PublicationResearchDetailsBO objBo = new PublicationResearchDetailsBO();
            objBo.Id = iPkId;
            hfPublicationResearch.Value = iPkId.ToString();
            DataSet ds = new ExtraDetailsBAL().SelectPublicationResearchByID(objBo);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                txtPublicationResearchFromYear.Text = ds.Tables[0].Rows[0]["FromYear"].ToString();
                txtPublicationResearchToYear.Text = ds.Tables[0].Rows[0]["ToYear"].ToString();
                txtPublicationResearchDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                return true;
            }
            else
            {
                txtPublicationResearchFromYear.Text = "";
                txtPublicationResearchToYear.Text = "";
                txtPublicationResearchDescription.Text = "";
                return false;
            }
        }
        private bool FillControlsAwards(Int32 iPkId)
        {
            FacultyAwardsDetailsBO objBo = new FacultyAwardsDetailsBO();
            objBo.Id = iPkId;
            hfAwardsId.Value = iPkId.ToString();
            DataSet ds = new ExtraDetailsBAL().SelectAwardsDetailsByID(objBo);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                txtAwardsTitle.Text = ds.Tables[0].Rows[0]["Title"].ToString();
                txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                txtAwardsDescription.Text = HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["AwardsDescription"].ToString());
                return true;
            }
            else
            {
                txtAwardsTitle.Text = "";
                txtMonth.Text = "";
                txtYear.Text = "";
                txtAwardsDescription.Text = "";
                return false;
            }
        }
        private bool FillControlsService(Int32 iPkId)
        {
            FacultyServiceDetailsBO objBo = new FacultyServiceDetailsBO();
            objBo.Id = iPkId;
            hfServiceId.Value = iPkId.ToString();
            DataSet ds = new ExtraDetailsBAL().SelectServiceDetailsByID(objBo);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                txtServiceName.Text = ds.Tables[0].Rows[0]["ServiceName"].ToString();
                return true;
            }
            else
            {
                txtServiceName.Text = "";
                return false;
            }
        }
        private void BindGridView()
        {
            DataSet ds = new DataSet();
            EducationDetailsBO objBo = new EducationDetailsBO();
            objBo.FacultyDetailsId = FacultyId;
            ExtraDetailsBAL objBAL = new ExtraDetailsBAL();
            ds = objBAL.SelectAllEducationDetails(objBo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gView.DataSource = ds;
                gView.DataBind();
            }
            else
            {
                gView.DataSource = null;
                gView.DataBind();
            }

            DataSet dsAreaExperience = new DataSet();
            AreaExperienceBO objAreaBo = new AreaExperienceBO();
            objAreaBo.FacultyDetailsId = FacultyId;
            dsAreaExperience = objBAL.SelectAllAreaExperienceDetails(objAreaBo);
            if (dsAreaExperience != null && dsAreaExperience.Tables[0].Rows.Count > 0)
            {
                gViewAreaExperience.DataSource = dsAreaExperience;
                gViewAreaExperience.DataBind();
            }
            else
            {
                gViewAreaExperience.DataSource = null;
                gViewAreaExperience.DataBind();
            }

            DataSet dsPublicationResearch = new DataSet();
            PublicationResearchDetailsBO objPublicationBo = new PublicationResearchDetailsBO();
            objPublicationBo.FacultyDetailsId = FacultyId;
            dsPublicationResearch = objBAL.SelectAllPublicationResearchDetails(objPublicationBo);
            if (dsPublicationResearch != null && dsPublicationResearch.Tables[0].Rows.Count > 0)
            {
                gViewPublicationResearch.DataSource = dsPublicationResearch;
                gViewPublicationResearch.DataBind();
            }
            else
            {
                gViewPublicationResearch.DataSource = null;
                gViewPublicationResearch.DataBind();
            }

            DataSet dsAwards = new DataSet();
            FacultyAwardsDetailsBO objAwardsBo = new FacultyAwardsDetailsBO();
            objAwardsBo.FacultyDetailsId = FacultyId;
            dsAwards = objBAL.SelectAllAwardsDetails(objAwardsBo);
            if (dsAwards != null && dsAwards.Tables[0].Rows.Count > 0)
            {
                gViewAwards.DataSource = dsAwards;
                gViewAwards.DataBind();
            }
            else
            {
                gViewAwards.DataSource = null;
                gViewAwards.DataBind();
            }

            DataSet dsService = new DataSet();
            FacultyServiceDetailsBO objServiceBo = new FacultyServiceDetailsBO();
            objServiceBo.FacultyDetailsId = FacultyId;
            dsService = objBAL.SelectAllServiceDetails(objServiceBo);
            if (dsService != null && dsService.Tables[0].Rows.Count > 0)
            {
                gViewService.DataSource = dsService;
                gViewService.DataBind();
            }
            else
            {
                gViewService.DataSource = null;
                gViewService.DataBind();
            }
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(EducationDetailsBO objBo)
        {
            objBo.FacultyDetailsId = FacultyId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtEducation.Text))
                objBo.EducationName = txtEducation.Text;
            if (!string.IsNullOrEmpty(txtFromYear.Text))
                objBo.FromYear = txtFromYear.Text;
            if (!string.IsNullOrEmpty(txtToYear.Text))
                objBo.ToYear = txtToYear.Text;
            if (!string.IsNullOrEmpty(txtDegree.Text))
                objBo.DegreeName = txtDegree.Text;
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
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
                EducationDetailsBO objBo = new EducationDetailsBO();
                LoadControls(objBo);
                if (new ExtraDetailsBAL().InsertRecord(objBo))
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
                EducationDetailsBO objBo = new EducationDetailsBO();
                LoadControls(objBo);
                objBo.Id = Convert.ToInt32(ViewState["PK"]);
                if (new ExtraDetailsBAL().UpdateRecord(objBo))
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
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
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
        private void ShowHideControlAreaExperience(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlAreaExperienceEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlAreaExperienceEntry.Visible = true;
                    btnAddAreaExperienceDetails.Visible = false;
                    btnUpdateAreaExperienceDetails.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfAreaExperienceId.Value = "0";
                    pnlAreaExperienceEntry.Visible = true;
                    btnAddAreaExperienceDetails.Visible = true;
                    btnUpdateAreaExperienceDetails.Visible = false;
                    ClearControlValues(pnlAreaExperienceEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlAreaExperienceEntry.Visible = true;
                    btnAddAreaExperienceDetails.Visible = false;
                    btnUpdateAreaExperienceDetails.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlAreaExperienceEntry.Visible = true;
                    btnAddAreaExperienceDetails.Visible = true;
                    btnUpdateAreaExperienceDetails.Visible = false;
                    ClearControlValues(pnlAreaExperienceEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlAreaExperienceEntry.Visible = false;
                    break;
            }
        }
        private void ShowHideControlPublicationResearch(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlPublicationResearchEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlPublicationResearchEntry.Visible = true;
                    btnPublicationResearchSave.Visible = false;
                    btnPublicationResearchUpdate.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfAreaExperienceId.Value = "0";
                    pnlPublicationResearchEntry.Visible = true;
                    btnPublicationResearchSave.Visible = true;
                    btnPublicationResearchUpdate.Visible = false;
                    ClearControlValues(pnlPublicationResearchEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlPublicationResearchEntry.Visible = true;
                    btnPublicationResearchSave.Visible = false;
                    btnPublicationResearchUpdate.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlPublicationResearchEntry.Visible = true;
                    btnPublicationResearchSave.Visible = true;
                    btnPublicationResearchUpdate.Visible = false;
                    ClearControlValues(pnlPublicationResearchEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlPublicationResearchEntry.Visible = false;
                    break;
            }
        }
        private void ShowHideControlAwards(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlAwardsEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlAwardsEntry.Visible = true;
                    btnSaveAwards.Visible = false;
                    btnUpdateAwards.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfAwardsId.Value = "0";
                    pnlAwardsEntry.Visible = true;
                    btnSaveAwards.Visible = true;
                    btnUpdateAwards.Visible = false;
                    ClearControlValues(pnlAwardsEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlAwardsEntry.Visible = true;
                    btnSaveAwards.Visible = false;
                    btnUpdateAwards.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlAwardsEntry.Visible = true;
                    btnSaveAwards.Visible = true;
                    btnUpdateAwards.Visible = false;
                    ClearControlValues(pnlAwardsEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlAwardsEntry.Visible = false;
                    break;
            }
        }
        private void ShowHideControlService(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlServiceEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlServiceEntry.Visible = true;
                    btnSaveService.Visible = false;
                    btnUpdateService.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfServiceId.Value = "0";
                    pnlServiceEntry.Visible = true;
                    btnSaveService.Visible = true;
                    btnUpdateService.Visible = false;
                    ClearControlValues(pnlServiceEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlServiceEntry.Visible = true;
                    btnSaveService.Visible = false;
                    btnUpdateService.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlServiceEntry.Visible = true;
                    btnSaveService.Visible = true;
                    btnUpdateService.Visible = false;
                    ClearControlValues(pnlServiceEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlServiceEntry.Visible = false;
                    break;
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        #region Save || Update || Cancel Area Experience
        private void LoadControlsAreaExperience(AreaExperienceBO objBo)
        {
            objBo.FacultyDetailsId = FacultyId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtEmployerName.Text))
                objBo.EmployerName = txtEmployerName.Text;
            if (!string.IsNullOrEmpty(txtAreaFromYear.Text))
                objBo.FromYear = txtAreaFromYear.Text;
            if (!string.IsNullOrEmpty(txtAreaToYear.Text))
            {
                objBo.ToYear = txtAreaToYear.Text;
            }
            else
            {
                objBo.ToYear = "";
            }
            objBo.IsPresent = chkIsPresent.Checked;
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btnAddAreaExperience_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControlAreaExperience(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnAddAreaExperienceDetails_ServerClick(object sender, EventArgs e)
        {
            try
            {
                AreaExperienceBO objBo = new AreaExperienceBO();
                LoadControlsAreaExperience(objBo);
                if (new ExtraDetailsBAL().InsertAreaExperienceRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlAreaExperience(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnUpdateAreaExperienceDetails_ServerClick(object sender, EventArgs e)
        {
            try
            {
                AreaExperienceBO objBo = new AreaExperienceBO();
                LoadControlsAreaExperience(objBo);
                objBo.Id = Convert.ToInt32(ViewState["PK"]);
                if (new ExtraDetailsBAL().UpdateAreaExperienceRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlAreaExperience(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void chkIsPresent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsPresent.Checked)
            {
                txtAreaToYear.Text = "";
                divYear.Visible = false;
            }
            else
            {
                divYear.Visible = true;
            }
        }
        protected void btnCancelAreaExperienceDetails_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControlAreaExperience(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region Save || Update || Cancel Publication Research Details
        private void LoadControlsPublicationResearch(PublicationResearchDetailsBO objBo)
        {
            objBo.FacultyDetailsId = FacultyId;
            objBo.LanguageId = LanguageId;

            if (!string.IsNullOrEmpty(txtPublicationResearchFromYear.Text))
                objBo.FromYear = txtPublicationResearchFromYear.Text;
            else
                objBo.FromYear = txtPublicationResearchFromYear.Text;

            if (!string.IsNullOrEmpty(txtPublicationResearchToYear.Text))
                objBo.ToYear = txtPublicationResearchToYear.Text;
            else
                objBo.ToYear = "";

            objBo.Description = txtPublicationResearchDescription.Text.ToString();
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btnPublicationResearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControlPublicationResearch(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnPublicationResearchSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PublicationResearchDetailsBO objBo = new PublicationResearchDetailsBO();
                LoadControlsPublicationResearch(objBo);
                if (new ExtraDetailsBAL().InsertPublicationResearchRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlAreaExperience(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnPublicationResearchUpdate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PublicationResearchDetailsBO objBo = new PublicationResearchDetailsBO();
                LoadControlsPublicationResearch(objBo);
                objBo.Id = Convert.ToInt32(ViewState["PK"]);
                if (new ExtraDetailsBAL().UpdatePublicationResearchRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlPublicationResearch(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnPublicationResearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControlPublicationResearch(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region Save || Update || Cancel Awards Details
        private void LoadControlsAwards(FacultyAwardsDetailsBO objBo)
        {
            objBo.FacultyDetailsId = FacultyId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtAwardsTitle.Text))
                objBo.Title = txtAwardsTitle.Text;
            if (!string.IsNullOrEmpty(txtMonth.Text))
                objBo.Month = txtMonth.Text;
            if (!string.IsNullOrEmpty(txtYear.Text))
                objBo.Year = txtYear.Text;
            objBo.AwardsDescription = txtAwardsDescription.Text;
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btnAddAwards_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControlAwards(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnSaveAwards_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FacultyAwardsDetailsBO objBo = new FacultyAwardsDetailsBO();
                LoadControlsAwards(objBo);
                if (new ExtraDetailsBAL().InsertAwardsRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlAwards(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnUpdateAwards_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FacultyAwardsDetailsBO objBo = new FacultyAwardsDetailsBO();
                LoadControlsAwards(objBo);
                objBo.Id = Convert.ToInt32(ViewState["PK"]);
                if (new ExtraDetailsBAL().UpdateAwardsRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlAwards(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnCancelAwards_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControlAwards(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region Save || Update || Cancel Service Details
        private void LoadControlsService(FacultyServiceDetailsBO objBo)
        {
            objBo.FacultyDetailsId = FacultyId;
            objBo.LanguageId = LanguageId;
            if (!string.IsNullOrEmpty(txtServiceName.Text))
                objBo.ServiceName = txtServiceName.Text;
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
        }
        protected void btnAddService_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControlService(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnSaveService_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FacultyServiceDetailsBO objBo = new FacultyServiceDetailsBO();
                LoadControlsService(objBo);
                if (new ExtraDetailsBAL().InsertServiceRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlService(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnUpdateService_ServerClick(object sender, EventArgs e)
        {
            try
            {
                FacultyServiceDetailsBO objBo = new FacultyServiceDetailsBO();
                LoadControlsService(objBo);
                objBo.Id = Convert.ToInt32(ViewState["PK"]);
                if (new ExtraDetailsBAL().UpdateServiceRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControlService(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnCancelService_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControlService(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        
    }
}