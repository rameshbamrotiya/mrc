using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class ResearchTeachingPosts : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SessionWrapper.UserDetails.UserName))
                {
                    Response.Redirect("~/Recruitment/Careers");
                }
                //string strEndQueryString = "Sm9iSWQ9MXxSZWdJZD0yMDIxMDYzMDA3MzU0OHxDYW5kSWQ9MQ%3d%3d";
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Recruitment/Careers.aspx");
                }

                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();
                if (strQuery.Count() == 3)
                {
                    strJobId = strQuery[0].ToString().Replace("JobId=", "");
                    strRegId = strQuery[1].ToString().Replace("RegId=", "");
                    strCandId = strQuery[2].ToString().Replace("CandId=", "");
                }
                else
                {
                    Response.Redirect("~/Recruitment/Careers.aspx");
                }
                if (!IsPostBack)
                {
                    BindYear();
                    BindIndexedGridView();
                    BindNonIndexedGridView();
                    FillExtraTeachingPostDetails();
                    ShowHideIndexedControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                Response.Redirect("~/Recruitment/Careers.aspx");
            }
        }
        #endregion

        #region Common Function
        private void ClearIncexedControlValues()
        {
            hfIndexJournalId.Value = "0";
            txtNameOfTheJournal.Text = "";
            txtTopic.Text = "";
            ddlMonth.SelectedIndex = 0;
            BindYear();
            txtNationalInternational.Text = "";
            txtPublicationAcceptance.Text = "";
            btnAddIndexedJournal.Text = "Add Indexed Journal";
            BindIndexedGridView();
        }
        private void ClearNonIndexedControlValues()
        {
            hfNonIndexJournalId.Value = "0";
            txtNonIndexNameOfTheJournal.Text = "";
            ddlNonIndexMonth.SelectedIndex = 0;
            txtNonIndexNationalInternational.Text = "";
            txtNonIndexPublicationAcceptance.Text = "";
            btnAddNonIndexedJournal.Text = "Add Non-Indexed Journal";
            BindYear();
            BindNonIndexedGridView();
        }
        private void BindYear()
        {
            ddlYear.Items.Clear();
            int startYear = 1970;
            for (int i = startYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.Insert(0, "Select");

            ddlNonIndexYear.Items.Clear();
            for (int i = startYear; i <= DateTime.Now.Year; i++)
            {
                ddlNonIndexYear.Items.Add(i.ToString());
            }
            ddlNonIndexYear.Items.Insert(0, "Select");
        }
        private void BindIndexedGridView()
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<CandidateIndexedDetailsModels> lstData = new List<CandidateIndexedDetailsModels>();
                    lstData = objCandidateDetailsRepository.GetAllCandidateIndexedJournalDetailsByCandId(Convert.ToInt64(strCandId));
                    gvIndexedJournal.DataSource = lstData;
                    gvIndexedJournal.DataBind();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        private void BindNonIndexedGridView()
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<CandidateNonIndexedDetailsModels> lstData = new List<CandidateNonIndexedDetailsModels>();
                    lstData = objCandidateDetailsRepository.GetAllCandidateNonIndexedJournalDetailsByCandId(Convert.ToInt64(strCandId));
                    gvNonIndexedJournal.DataSource = lstData;
                    gvNonIndexedJournal.DataBind();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private void FillExtraTeachingPostDetails()
        {
            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateDetailsForTeachingPostsModel objBO = new CandidateDetailsForTeachingPostsModel();
                objBO = objCandidateDetailsRepository.GetCandidateDetailsForExtraTeachingPostsByCanId(Convert.ToInt64(strCandId));
                if (objBO != null)
                {
                    txtIndependentWorkResult.Text = objBO.IndependentWorkWithResult;
                    txtWorkUnderSupervision.Text = objBO.WorkUnderSupervision;
                    txtConferenceAttendance.Text = objBO.ConferenceAttendanceAndPaper;
                }
            }
        }
        #endregion

        #region Save || Update || Cancel For Indexed Journal
        private bool LoadControlsIndexed(CandidateIndexedDetailsModels objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            if (!string.IsNullOrEmpty(txtNameOfTheJournal.Text))
                objBo.NameOfTheJournal = txtNameOfTheJournal.Text;
            if (!string.IsNullOrEmpty(txtTopic.Text))
                objBo.Topic = txtTopic.Text;
            if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                objBo.Month = ddlMonth.SelectedValue;
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                objBo.Year = ddlYear.SelectedValue;
            if (!string.IsNullOrEmpty(txtNationalInternational.Text))
                objBo.NationalInternational = txtNationalInternational.Text;
            if (!string.IsNullOrEmpty(txtPublicationAcceptance.Text))
                objBo.PublicationAcceptance = txtPublicationAcceptance.Text;

            if (string.IsNullOrWhiteSpace(hfIndexJournalId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfIndexJournalId.Value);
            }
            return true;
        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }
        protected void btnAddIndexedJournal_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateIndexedDetailsModels objBO = new CandidateIndexedDetailsModels();
                    if (LoadControlsIndexed(objBO))
                    {
                        if (!objCandidateDetailsRepository.InsertOrUpdateCandidateIndexedJournal(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        ClearIncexedControlValues();
                        BindIndexedGridView();
                        ShowHideIndexedControl(VisibityType.GridView);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtNameOfTheJournal.ClientID + "').focus();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnIndexedJournalEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                txtNameOfTheJournal.Text = gvIndexedJournal.Rows[rowindex].Cells[1].Text.Trim();
                txtTopic.Text = gvIndexedJournal.Rows[rowindex].Cells[2].Text.Trim();
                ddlMonth.SelectedValue = gvIndexedJournal.DataKeys[rowindex]["Month"].ToString();
                ddlYear.Text = gvIndexedJournal.Rows[rowindex].Cells[4].Text.Trim();
                txtNationalInternational.Text = gvIndexedJournal.Rows[rowindex].Cells[5].Text.Trim();
                txtPublicationAcceptance.Text = gvIndexedJournal.Rows[rowindex].Cells[6].Text.Trim();
                hfIndexJournalId.Value = gvIndexedJournal.DataKeys[rowindex]["Id"].ToString();
                btnAddIndexedJournal.Text = "Update Indexed Journal";
                ShowHideIndexedControl(VisibityType.Edit);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtNameOfTheJournal.ClientID + "').focus();", true);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnIndexedJournalDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvIndexedJournal.DataKeys[rowindex]["Id"].ToString());
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    objCandidateDetailsRepository.RemoveTblCandidateIndexedJournalDetails(rowId, out errorMessage);
                    ClearIncexedControlValues();
                    BindIndexedGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion        

        #region Save || Update || Cancel For Non-Indexed Journal
        private bool LoadControlsNonIndexed(CandidateNonIndexedDetailsModels objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            if (!string.IsNullOrEmpty(txtNonIndexNameOfTheJournal.Text))
                objBo.NameOfTheJournal = txtNonIndexNameOfTheJournal.Text;
            if (!string.IsNullOrEmpty(ddlNonIndexMonth.SelectedValue))
                objBo.Month = ddlNonIndexMonth.SelectedValue;
            if (!string.IsNullOrEmpty(ddlNonIndexYear.SelectedValue))
                objBo.Year = ddlNonIndexYear.SelectedValue;
            if (!string.IsNullOrEmpty(txtNonIndexNationalInternational.Text))
                objBo.NationalInternational = txtNonIndexNationalInternational.Text;
            if (!string.IsNullOrEmpty(txtNonIndexPublicationAcceptance.Text))
                objBo.PublicationAcceptance = txtNonIndexPublicationAcceptance.Text;

            objBo.Topic = "";
            if (string.IsNullOrWhiteSpace(hfNonIndexJournalId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfNonIndexJournalId.Value);
            }
            return true;
        }
        protected void btnAddNonIndexedJournal_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateNonIndexedDetailsModels objBO = new CandidateNonIndexedDetailsModels();
                    if (LoadControlsNonIndexed(objBO))
                    {
                        if (!objCandidateDetailsRepository.InsertOrUpdateCandidateNonIndexedJournal(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        ClearNonIndexedControlValues();
                        BindNonIndexedGridView();
                        ShowHideNonIndexedControl(VisibityType.GridView);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtNonIndexNameOfTheJournal.ClientID + "').focus();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnNonIndexedJournalEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                txtNonIndexNameOfTheJournal.Text = gvNonIndexedJournal.Rows[rowindex].Cells[1].Text.Trim();
                ddlNonIndexMonth.SelectedValue = gvNonIndexedJournal.DataKeys[rowindex]["Month"].ToString();
                ddlNonIndexYear.SelectedValue = gvNonIndexedJournal.Rows[rowindex].Cells[3].Text.Trim();
                txtNonIndexNationalInternational.Text = gvNonIndexedJournal.Rows[rowindex].Cells[4].Text.Trim();
                txtNonIndexPublicationAcceptance.Text = gvNonIndexedJournal.Rows[rowindex].Cells[5].Text.Trim();
                hfNonIndexJournalId.Value = gvNonIndexedJournal.DataKeys[rowindex]["Id"].ToString();
                btnAddNonIndexedJournal.Text = "Update Non-Indexed Journal";
                ShowHideNonIndexedControl(VisibityType.Edit);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtNonIndexNameOfTheJournal.ClientID + "').focus();", true);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        protected void lbtnNonIndexedJournalDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvNonIndexedJournal.DataKeys[rowindex]["Id"].ToString());
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    objCandidateDetailsRepository.RemoveTblCandidateNonIndexedJournalDetails(rowId, out errorMessage);
                    ClearNonIndexedControlValues();
                    BindNonIndexedGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion        

        #region ShowHideControl || Notification
        private void ShowHideIndexedControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.Insert:
                    ClearIncexedControlValues();
                    break;
                case VisibityType.Edit:
                    break;
                case VisibityType.SaveAndAdd:
                    ClearIncexedControlValues();
                    break;
                default:
                    break;
            }
        }
        private void ShowHideNonIndexedControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.Insert:
                    ClearNonIndexedControlValues();
                    break;
                case VisibityType.Edit:
                    break;
                case VisibityType.SaveAndAdd:
                    ClearNonIndexedControlValues();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Final Submit 
        private bool LoadControlsUpdateExperience(CandidateDetailsForTeachingPostsModel objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            if (!string.IsNullOrEmpty(txtIndependentWorkResult.Text))
                objBo.IndependentWorkWithResult = txtIndependentWorkResult.Text;
            if (!string.IsNullOrEmpty(txtWorkUnderSupervision.Text))
                objBo.WorkUnderSupervision = txtWorkUnderSupervision.Text;
            if (!string.IsNullOrEmpty(txtConferenceAttendance.Text))
                objBo.ConferenceAttendanceAndPaper = txtConferenceAttendance.Text;
            return true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateDetailsForTeachingPostsModel objBO = new CandidateDetailsForTeachingPostsModel();
                    if (LoadControlsUpdateExperience(objBO))
                    {
                        if (!objCandidateDetailsRepository.UpdateCandidateDetailsForTeachingPosts(objBO, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                            txtIndependentWorkResult.Text = "";
                            txtWorkUnderSupervision.Text = "";
                            txtConferenceAttendance.Text = "";
                        }
                    }
                }
                //SessionWrapper.sendConfirmationMail = 0;
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
                Response.Redirect("~/Recruitment/CandidateDetails?" + strEndQueryString);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion

        protected void btnPrivious_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/ExtraInfo?" + strEndQueryString);
        }
    }
}