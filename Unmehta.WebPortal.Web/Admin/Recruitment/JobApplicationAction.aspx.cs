using BAL;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class JobApplicationAction : System.Web.UI.Page
    {
        bool checkdPaging;
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
                if (!IsPostBack)
                {
                    Bind_DocGrid();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnSendCallLetter_Click(object sender, EventArgs e)
        {
            try
            {
                int aletrMessage = 0;
                ScrutinyMasterBO objBo = new ScrutinyMasterBO();

                foreach (GridViewRow row in gView.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        aletrMessage = aletrMessage + 1;
                        objBo.CandidateId = Convert.ToInt32(gView.DataKeys[row.RowIndex].Values["CandidateId"].ToString());
                        objBo.RegistrationId = row.Cells[2].Text;
                        objBo.EmailId = gView.DataKeys[row.RowIndex].Values["EmailId"].ToString();
                        objBo.JobId = Convert.ToInt32(gView.DataKeys[row.RowIndex].Values["PostId"].ToString());
                        objBo.ActionType = Convert.ToInt32(ddlActionType.SelectedValue);
                        objBo.InterviewDate = Convert.ToDateTime(txtInterviewDate.Text);
                        objBo.InterviewFromTime = txtFromTime.Text;
                        objBo.InterviewToTime = txtToTime.Text;
                        objBo.InterviewAddress = txtInterviewAddress.Text;
                        objBo.flag = 1;
                        objBo.UserName = SessionWrapper.UserDetails.UserName;

                        string EmailId = gView.DataKeys[row.RowIndex].Values["EmailId"].ToString();
                        string PostName = row.Cells[4].Text;
                        string RegistrationId = objBo.RegistrationId;
                        if (new CareerMasterBAL().InsertRecordScrutiny(objBo))
                        {
                            objBo.AdvertiseNo = gView.DataKeys[row.RowIndex].Values["AdvertiseNo"].ToString();
                            //ShowMessage("Record inserted successfully.", MessageType.Success);
                            SendEmailForCallLetter(EmailId, PostName, RegistrationId, objBo.AdvertiseNo, objBo.InterviewDate, objBo.InterviewFromTime, objBo.InterviewToTime, objBo.InterviewAddress);
                        }
                        else
                        {
                            //ShowMessage("Record already exists in database.", MessageType.Success);
                            //return;
                        }
                        Bind_DocGrid();
                        ClearControl();
                    }
                }
                if (aletrMessage != 0)
                {
                    Functions.MessagePopup(this, "Call letter send successfully.", PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CheckBox chckheader = (CheckBox)gView.HeaderRow.FindControl("chkSelectAll");
            checkdPaging = chckheader.Checked;
            SaveCheckedValues();
            gView.PageIndex = e.NewPageIndex;
            this.Bind_DocGrid();
            PopulateCheckedValues();
            if (checkdPaging)
            {
                chkSelectAll_CheckedChanged(null, null);
            }
        }
        

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Bind_DocGrid();
                ClearControl();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

            CareerMasterBAL objBAL = new CareerMasterBAL();
            CareerMasterBO objBO = new CareerMasterBO();
            string SearchRegistrationId = txtSearch.Text.Trim();
            string SearchPostName = ddlJobList.SelectedItem.Text.ToString();
            DataTable dt = new DataTable();
            dt = objBAL.GetAllCandidateDetails_ForCallLetter();
            if (ddlJobList.SelectedIndex > 0)
            {
                //DataRow[] dr = dt.Select("PostName ='" + SearchPostName + "'");
                DataRow[] dr = dt.Select("PostId =" + ddlJobList.SelectedValue);
                DataColumnCollection dc = dt.Columns;
                dt = new DataTable();

                foreach (DataColumn row in dc)
                {
                    dt.Columns.Add(row.ColumnName, row.DataType);

                }

                foreach (DataRow row in dr)
                {
                    dt.ImportRow(row);
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchRegistrationId))
            {

                DataRow[] dr = dt.Select("CandidateRegistrationId ='" + SearchRegistrationId + "'");
                DataColumnCollection dc = dt.Columns;
                dt = new DataTable();

                foreach (DataColumn row in dc)
                {
                    dt.Columns.Add(row.ColumnName, row.DataType);
                }

                foreach (DataRow row in dr)
                {
                    dt.ImportRow(row);
                }

            }
            lblCount.Text = "Total No Of Candidate : " + Convert.ToString(dt.Rows.Count);
            gView.DataSourceID = string.Empty;
            gView.DataSource = dt;
            gView.DataBind();
        }


        public void SendEmailForCallLetter(string emailId, string PostName, string RegistrationId, string AdvertiseNo, DateTime InterviewDate, string InterviewFromTime, string InterviewToTime, string InterviewAddress)
        {
            try
            {
                CareerMasterBAL objBAL = new CareerMasterBAL();
                DataSet ds = new DataSet();
                ds = objBAL.MailCreditials();
                if (ds != null && ds.Tables.Count > 0)
                {
                    string maindomain = HttpContext.Current.Request.Url.AbsoluteUri;
                    maindomain.Split('/');
                    string domain = maindomain.Replace("/CMS/JobApplicationAction.aspx", "");

                    string link = domain + ("/CallLetterVerification.aspx");
                    //int port = 587;
                    string smtpserver = ConfigDetailsValue.SMTPServer;
                    string smtpPassword = ConfigDetailsValue.SMTPPassword;
                    string fromemail = ConfigDetailsValue.SMTPFromEmail;
                    string smtpaccount = ConfigDetailsValue.SMTPAccount;
                    MailMessage msg = new MailMessage();
                    SmtpClient client = new SmtpClient(smtpserver);
                    msg.To.Add(emailId);
                    //msg.CC.Add("hardik.mistry@kcspl.co.in");      
                    // msg.CC.Add("parul@kcspl.co.in");
                    msg.IsBodyHtml = true;

                    msg.Subject = "GIL Call Letter";
                    //msg.Body = "<br/> Your application for the post of " + PostName + " has been confirmed with registration ID: " + RegistrationId + "."
                    //                + "<br/> please click on the below link for print call letter" +
                    //                   "<br/><br/><a href=" + link + ">" + link + "</a>";
                    msg.Body = "<br/> This is with reference of GIL advertisement No. <b>" + AdvertiseNo + "</b> GIL has scheduled your interview for the position of <b>" + PostName + "</b> on <b>" + Convert.ToDateTime(InterviewDate).ToString("dd/MM/yyyy") + "</b> as per the following details:"
                        + "<br/><b>Name of Post</b>: <b>" + PostName + "</b>"
                        + "<br/><b>Interview timings (" + Convert.ToDateTime(InterviewDate).ToString("dd/MM/yyyy") + ")</b>: <b>" + InterviewFromTime + " to " + InterviewToTime + "</b>."
                        + "<br/><b>Venue</b>: <b>" + InterviewAddress + "</b>"
                        + "<br/>"
                        + "<br/>Candidate should download their call letters from URL <a href=" + link + ">" + link + "</a>."
                        + "<br/>"
                        + "<br/>"
                        + "<br/><b>Note</b>:"
                        + "<br/>1.	Candidate must bring this call letter during interview."
                        + "<br/>2.	You are requested to be present as per above schedule and venue with original document which were asked as per the eligibility criteria."
                        + "<br/>3.	You are requested to remain present 1 hour before the Interview Time."
                        + "<br/>4.	You will have to bring one passport size photograph and a valid Identity proof (PAN card/ Aadhar card, Driving license, Government ID card) in Original for verification."
                        + "<br/>5.	Please contact on <b>“079-232-59230”</b> for any assistance regarding call letter or location of interview venue."
                        + "<br/>"
                        + "<br/>"
                        + "<br/><b>Regards</b>,"
                        + "<br/><b>Gujarat Informatics Limited</b>,";
                    msg.From = new System.Net.Mail.MailAddress(fromemail);
                    client.EnableSsl = false;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpaccount, smtpPassword);
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }


        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gView.HeaderRow.FindControl("chkSelectAll");
            if (checkdPaging)
            {
                chckheader.Checked = checkdPaging;
            }
            foreach (GridViewRow row in gView.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");

                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                }
                else
                {
                    chckrw.Checked = false;
                }
            }
        }

        protected void ClearControl()
        {
            ddlActionType.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            ddlJobList.SelectedIndex = 0;
            txtInterviewDate.Text = "";
            txtFromTime.Text = "";
            txtToTime.Text = "";
            txtInterviewAddress.Text = "";
            CheckBox chckheader = (CheckBox)gView.HeaderRow.FindControl("chkSelectAll");
            chckheader.Checked = false;
            foreach (GridViewRow row in gView.Rows)
            {
                var chkSelect = row.FindControl("chkSelect") as CheckBox;
                if (chkSelect != null)
                    chkSelect.Checked = false;
            }
        }

        protected void Bind_DocGrid()
        {
            PostApplicationMasterBAL objBALPOst = new PostApplicationMasterBAL();
            DataSet ds1 = new DataSet();
            ds1 = objBALPOst.GetAllQualification("GetAllJobMaster");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlJobList.DataSource = ds1.Tables[0];
                    ddlJobList.DataTextField = "JobTitle";
                    ddlJobList.DataValueField = "Id";
                    ddlJobList.DataBind();
                    ddlJobList.Items.Insert(0, new ListItem("Select Job", ""));
                }
            }

            CareerMasterBAL objBAL = new CareerMasterBAL();
            CareerMasterBO objBO = new CareerMasterBO();

            DataTable dt = new DataTable();
            dt = objBAL.GetAllCandidateDetail();
            lblCount.Text = "Total No Of Candidate : " + Convert.ToString(dt.Rows.Count);
            gView.DataSourceID = string.Empty;
            gView.DataSource = dt;
            gView.DataBind();
        }

        private void PopulateCheckedValues()
        {
            if (!checkdPaging)
            {
                ArrayList userdetails = (ArrayList)Session["CHECKED_ITEMS"];
                if (userdetails != null && userdetails.Count > 0)
                {
                    foreach (GridViewRow gvrow in gView.Rows)
                    {
                        int index = (int)gView.DataKeys[gvrow.RowIndex].Value;
                        if (userdetails.Contains(index))
                        {
                            CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkSelect");
                            myCheckBox.Checked = true;
                        }
                    }
                }
            }
            else
            {
                Session["CHECKED_ITEMS"] = null;
            }
        }

        private void SaveCheckedValues()
        {
            if (!checkdPaging)
            {
                ArrayList userdetails = new ArrayList();
                int index = -1;
                foreach (GridViewRow gvrow in gView.Rows)
                {
                    index = (int)gView.DataKeys[gvrow.RowIndex].Value;
                    bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;

                    // Check in the Session
                    if (Session["CHECKED_ITEMS"] != null)
                        userdetails = (ArrayList)Session["CHECKED_ITEMS"];
                    if (result)
                    {
                        if (!userdetails.Contains(index))
                            userdetails.Add(index);
                    }
                    else
                        userdetails.Remove(index);
                }
                if (userdetails != null && userdetails.Count > 0)
                    Session["CHECKED_ITEMS"] = userdetails;
            }
            else
            {
                Session["CHECKED_ITEMS"] = null;
            }
        }
    }
}