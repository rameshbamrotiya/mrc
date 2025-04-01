using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using System.Web.Services;
using Unmehta.WebPortal.Web;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Text;
using System.Data;
using BAL;
using ClosedXML.Excel;
using System.IO;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using ClassLib.BL;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class PatientFeedback : System.Web.UI.Page
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
            }
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ddlLanguage.SelectedIndex = 1;
                FillCapctha();
            }
        }

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
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues();
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
                    ClearControlValues();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtPatientName.Text = "";
            txtMobileNo.Text = "";
            txtEmailId.Text = "";
            txtFeedBackDetails.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private void LoadControlsAdd(PatientFeedbackGridModel objBo)
        {
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            if (!string.IsNullOrEmpty(txtPatientName.Text))
                objBo.Name = txtPatientName.Text;
            if (!string.IsNullOrEmpty(rblGender.SelectedValue))
                objBo.Gender = rblGender.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(txtMobileNo.Text))
                objBo.MobileNo = txtMobileNo.Text;
            if (!string.IsNullOrEmpty(txtEmailId.Text))
                objBo.EmailId = txtEmailId.Text;
            if (!string.IsNullOrEmpty(txtFeedBackDetails.Text))
                objBo.FeedBackDetails = txtFeedBackDetails.Text;

            objBo.IsVisible = true;
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Session["captcha"].ToString() != txtCaptcha.Text)
            {
                divError.Visible = true;
                spnError.InnerText = "Captcha does not match";
                FillCapctha();
                txtCaptcha.Text = string.Empty;
                return;
            }
           
            using (IPatientFeedbacksRepository objPatientFeedbacksRepository = new PatientFeedbacksRepository(Functions.strSqlConnectionString))
            {
                PatientFeedbackGridModel objBO = new PatientFeedbackGridModel();
                LoadControlsAdd(objBO);
                if (!objPatientFeedbacksRepository.InsertOrUpdateTblPatientFeedback(objBO, out errorMessage))
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
                ClearControlValues();
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }

        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            txtPatientName.Text = grdUser.Rows[rowindex].Cells[1].Text.Trim();
            txtMobileNo.Text = grdUser.Rows[rowindex].Cells[3].Text.Trim();
            txtEmailId.Text = grdUser.Rows[rowindex].Cells[4].Text.Trim();
            txtFeedBackDetails.Text = grdUser.Rows[rowindex].Cells[5].Text.Trim();
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IPatientFeedbacksRepository objPatientFeedbacksRepository = new PatientFeedbacksRepository(Functions.strSqlConnectionString))
                {
                    objPatientFeedbacksRepository.RemoveTblPatientFeedback(rowId, out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                PatientFeedbackGridModel objBo = new PatientFeedbackGridModel();
                LoadControlsAdd(objBo);
                using (IPatientFeedbacksRepository objPatientFeedbacksRepository = new PatientFeedbacksRepository(Functions.strSqlConnectionString))
                {
                    if (!objPatientFeedbacksRepository.InsertOrUpdateTblPatientFeedback(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
                ClearControlValues();
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                BindGridView();
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                BindGridView();
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion

        #region Capcha
        //captcha generation
        protected void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();
                    imgCaptcha.ImageUrl = "~/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        protected void btnRun_ServerClick(object sender, EventArgs e)
        {
            FillCapctha();
        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            PatientFeedbackContentDetailsBAL objBAL = new PatientFeedbackContentDetailsBAL();

            DataTable ds = new DataTable();
            ds = objBAL.GetAllPatientFeedbackExportList();

            var dbview = ds.DefaultView;

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                dbview.RowFilter = "FullName ='" + txtSearch.Text.Trim() + "' OR EmailId ='" + txtSearch.Text.Trim() + "' OR MobileNo ='" + txtSearch.Text.Trim() + "'";
            }
            ds = dbview.ToTable();

            ds.Columns["CreateDate"].ColumnName = "EntryDate";

            ds.Columns.Remove("Id");
            //ds.Columns.Remove("CreateDate");
            ds.Columns.Remove("IsUnmicrc");


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds, "PatientFeedback List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PatientFeedback.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}