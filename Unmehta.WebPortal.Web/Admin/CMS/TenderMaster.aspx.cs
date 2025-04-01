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
using System.Globalization;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class TenderMaster : System.Web.UI.Page
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
                    //if (Convert.ToString(hfActiveView.Value) == null)
                    //{
                    ShowHideControl(VisibityType.GridView);
                    //}
                }
                //}
                //else
                //    Response.Redirect("LoginPage.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
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
                throw ex;
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["TenderID"]);
                    if (e.CommandName == "eDelete")
                    {
                        TenderMasterBO objBo = new TenderMasterBO();
                        objBo.TenderID = bytID;
                        new TenderMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        // ShowMessage("Record deleted successfully.", MessageType.Success);
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
                else
                {
                    if (!(e.CommandName.Equals("Page")))
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;

            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new TenderMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }
        private bool FillControls(Int32 iPkId)
        {
            TenderMasterBO objBo = new TenderMasterBO();
            hfTenderId.Value = iPkId.ToString();
            objBo.TenderID = Convert.ToInt32(hfTenderId.Value);
            ViewState["TenID"] = objBo.TenderID;

            DataSet ds = new TenderMasterBAL().TenderMaster_SelectByTenderID(objBo);
            if (ds == null) return false;
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["Title"] != DBNull.Value)
                txtTitle.Text = dr["Title"].ToString();

            if (dr["TenderNo"] != DBNull.Value)
                txtTenderNo.Text = dr["TenderNo"].ToString();

            if (dr["Details"] != DBNull.Value)
                CKEditorControl1.Text = dr["Details"].ToString();

            if (dr["PublishDate"] != DBNull.Value)
            {
                txtPublishDate.Text = Convert.ToDateTime(dr["PublishDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (dr["PBMeetingDate"] != DBNull.Value)
            {
                txtPreBidDate.Text = Convert.ToDateTime(dr["PBMeetingDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtPreBidTime.Text = Convert.ToDateTime(dr["PBMeetingDate"]).ToString("HH:mm");
            }

            if (dr["LastDate"] != DBNull.Value)
            {
                txtLastDateBidSub.Text = Convert.ToDateTime(dr["LastDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtLastDateOfBidTime.Text = Convert.ToDateTime(dr["LastDate"]).ToString("HH:mm");
            }

            if (dr["OpeningDate"] != DBNull.Value)
            {
                txtOpeningBidDate.Text = Convert.ToDateTime(dr["OpeningDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtOpenBidTime.Text = Convert.ToDateTime(dr["OpeningDate"]).ToString("HH:mm");
            }

            if (dr["IsNewIcon"] != DBNull.Value)
            {
                if (dr["IsNewIcon"].ToString() == "1")
                    rbtnYES.Checked = true;
                else
                    rbtnNO.Checked = true;
            }

            if (dr["Details"] != DBNull.Value)
                CKEditorControl1.Text = dr["Details"].ToString();

            if (dr["DocDetails"] != DBNull.Value)
                CKEditorControl2.Text = dr["DocDetails"].ToString();



            if (dr["ProjectEstimateValue"] != DBNull.Value)
                txtProjectEstVal.Text = dr["ProjectEstimateValue"].ToString();

            if (dr["ProjectFinalValue"] != DBNull.Value)
                txtProjFinalVal.Text = dr["ProjectFinalValue"].ToString();

            if (dr["NameOfBidder"] != DBNull.Value)
                txtNameOfBidder.Text = dr["NameOfBidder"].ToString();

            if (dr["IssueOfWorkOrderDate"] != DBNull.Value)
                txtIssueOfWorkOrderDate.Text = Convert.ToDateTime(dr["IssueOfWorkOrderDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); 
            if (dr["MetaTitle"] != DBNull.Value)
                txtMetaTitle.Text = dr["MetaTitle"].ToString();
            if (dr["MetaDescription"] != DBNull.Value)
                txtMetaDesc.Text = dr["MetaDescription"].ToString();
            if (dr["Tender_level_id"] != DBNull.Value)
                txtsequence.Text = dr["Tender_level_id"].ToString();
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }

        protected void Bind_DocGrid()
        {
            TenderMasterBAL objBAL = new TenderMasterBAL();
            TenderMasterBO objBO = new TenderMasterBO();

            if (!string.IsNullOrEmpty(Convert.ToString(txtDocName.Text)) || !string.IsNullOrEmpty(Convert.ToString(ViewState["TenID"])))
                objBO.TenderID = Convert.ToInt32(ViewState["TenID"]);

            DataSet ds = new DataSet();
            ds = objBAL.Documents_Select(objBO);
            gvDoc.DataSource = ds;
            gvDoc.DataBind();
        }

        protected void gvDoc_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("iDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gvDoc.DataKeys[intIndex].Values["DocID"]);
                    if (e.CommandName == "iDelete")
                    {
                        new TenderMasterBAL().Delete_Document(bytID);
                        Bind_DocGrid();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls_TenderMaster(TenderMasterBO objBo)
        {
            DateTime? dtPreBidDateCombined = null;
            DateTime? dtLastDateofSubmission = null;
            DateTime? dtOpeningBidDate = null;
            DateTime? dtPublishDate = null;

            if (!string.IsNullOrEmpty(Convert.ToString(txtPublishDate.Text)))
            {
                DateTime dPreBidDate = DateTime.ParseExact(txtPublishDate.Text, "dd/MM/yyyy", null);
                dtPublishDate = new DateTime(dPreBidDate.Year, dPreBidDate.Month, dPreBidDate.Day);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(txtPreBidDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtPreBidTime.Text)))
            {
                DateTime dPreBidDate = DateTime.ParseExact(txtPreBidDate.Text, "dd/MM/yyyy", null);
                DateTime tPreBidDate = Convert.ToDateTime(txtPreBidTime.Text);
                dtPreBidDateCombined = new DateTime(dPreBidDate.Year, dPreBidDate.Month, dPreBidDate.Day, tPreBidDate.Hour, tPreBidDate.Minute, tPreBidDate.Second);
            }


            if (!string.IsNullOrEmpty(Convert.ToString(txtLastDateBidSub.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtLastDateOfBidTime.Text)))
            {
                DateTime dLastDateBidSub = DateTime.ParseExact(txtLastDateBidSub.Text, "dd/MM/yyyy", null);
                DateTime tLastDateOfBidTime = Convert.ToDateTime(txtLastDateOfBidTime.Text);
                dtLastDateofSubmission = new DateTime(dLastDateBidSub.Year, dLastDateBidSub.Month, dLastDateBidSub.Day, tLastDateOfBidTime.Hour, tLastDateOfBidTime.Minute, tLastDateOfBidTime.Second);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(txtOpeningBidDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtOpenBidTime.Text)))
            {
                DateTime dOpeningBidDate = DateTime.ParseExact(txtOpeningBidDate.Text, "dd/MM/yyyy", null);
                DateTime tOpenBidTime = Convert.ToDateTime(txtOpenBidTime.Text);
                dtOpeningBidDate = new DateTime(dOpeningBidDate.Year, dOpeningBidDate.Month, dOpeningBidDate.Day, tOpenBidTime.Hour, tOpenBidTime.Minute, tOpenBidTime.Second);
            }

            if (!string.IsNullOrEmpty(txtTitle.Text))
                objBo.Title = txtTitle.Text;

            if (!string.IsNullOrEmpty(txtTenderNo.Text))
                objBo.TenderNo = txtTenderNo.Text;

            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                objBo.Details = CKEditorControl1.Text;


            if (!string.IsNullOrEmpty(Convert.ToString(dtPreBidDateCombined)))
                objBo.PBMeetingDate = dtPreBidDateCombined;


            if (!string.IsNullOrEmpty(Convert.ToString(dtLastDateofSubmission)))
                objBo.LastDate = dtLastDateofSubmission;

            if (!string.IsNullOrEmpty(Convert.ToString(dtOpeningBidDate)))
                objBo.OpeningDate = dtOpeningBidDate;

            if (!string.IsNullOrEmpty(Convert.ToString(dtPublishDate)))
                objBo.PublishDate = dtPublishDate;

            if (rbtnYES.Checked == true)
                objBo.IsNewIcon = 1;
            else
                objBo.IsNewIcon = 0;

            objBo.Status = 1;


            if (!string.IsNullOrEmpty(Convert.ToString(txtProjectEstVal.Text)))
                objBo.ProjectEstimateValue = Convert.ToDecimal(txtProjectEstVal.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtProjFinalVal.Text)))
                objBo.ProjectFinalValue = Convert.ToDecimal(txtProjFinalVal.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtNameOfBidder.Text)))
                objBo.NameOfBidder = Convert.ToString(txtNameOfBidder.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtIssueOfWorkOrderDate.Text)))
                objBo.IssueOfWorkOrderDate = Convert.ToDateTime(txtIssueOfWorkOrderDate.Text);
            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;
            if (!string.IsNullOrEmpty(txtMetaDesc.Text))
                objBo.MetaDescription = txtMetaDesc.Text;
            objBo.ip_add = GetIPAddress;
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.Tender_level_id = Convert.ToInt32(txtsequence.Text);
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                DataSet ds = new TenderMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["Tender_level_id"] != DBNull.Value)
                    txtsequence.Text = drs["Tender_level_id"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }


        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TenderMasterBO objBO = new TenderMasterBO();
                LoadControls_TenderMaster(objBO);
                if (new TenderMasterBAL().Insert_TenderMaster(objBO))
                {
                    ViewState["TenID"] = objBO.TenderID;
                    hfTenderId.Value = objBO.TenderID.ToString();
                    MultiView1.ActiveViewIndex = 1;
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    Bind_DocGrid();
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                TenderMasterBO objBO = new TenderMasterBO();
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TenID"])))
                {
                    objBO.TenderID = Convert.ToInt32(ViewState["TenID"]);
                    hfTenderId.Value= objBO.TenderID.ToString();
                }

                LoadControls_TenderMaster(objBO);
                if (new TenderMasterBAL().Update_TenderMaster(objBO))
                {

                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                    MultiView1.ActiveViewIndex = 1;
                    Bind_DocGrid();
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void btnAddDoc_Click(object sender, EventArgs e)
        {
            try
            {
                TenderMasterBO objBO = new TenderMasterBO();
                LoadControls_Doc(objBO);
                if (new TenderMasterBAL().Insert_TenderDocument(objBO))
                {
                    MultiView1.ActiveViewIndex = 1;
                    Functions.MessagePopup(this, "Document added successfully.", PopupMessageType.success);
                    Bind_DocGrid();
                    txtDocName.Text = "";
                    ddlDocType.SelectedIndex = 0;
                    rbnDocNewYES.Checked = true;
                    rbnDocNewNO.Checked = false;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                    Functions.MessagePopup(this, "Document already exists", PopupMessageType.success);
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void btnFinal_Click1(object sender, EventArgs e)
        {
            try
            {
                string DocumentDetail = string.Empty;
                DocumentDetail = CKEditorControl2.Text;
                int TenID = Convert.ToInt32(ViewState["TenID"]);
                if (new TenderMasterBAL().Insert_TenderDocumentDetail(DocumentDetail, TenID))
                {
                    gView.DataBind();
                    ShowHideControl(VisibityType.GridView);
                    Functions.MessagePopup(this, "Tender uploaded successfully.", PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void btnBackView_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        private void LoadControls_Doc(TenderMasterBO objBo)
        {
            string DocPath = string.Empty;

            //var supportedTypes = new[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx" };
            //string FileExt = Path.GetExtension(fuDocument.FileName).ToLower();
            //if (!supportedTypes.Contains(FileExt))
            //{
            //    Functions.MessagePopup(this, "Only allowed file types : WORD/PDF/EXCEL", PopupMessageType.error);
            //    fuDocument.Focus();
            //    return;
            //}
            //else
            {
                string[] str = new string[] { DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), DateTime.Now.Millisecond.ToString() };
                string filenm = string.Concat(str);
                DocPath = string.Concat(filenm, Path.GetExtension(fuDocument.FileName));
                var DocumentUpload = ConfigDetailsValue.TendersFiles;
                bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                fuDocument.PostedFile.SaveAs(string.Concat(Server.MapPath(DocumentUpload), "\\", DocPath.Replace(" ", "_")));


                if (!string.IsNullOrEmpty(Convert.ToString(hfTenderId.Value)))
                    objBo.TenderID = Convert.ToInt32(hfTenderId.Value);

                if (!string.IsNullOrEmpty(Convert.ToString(txtDocName.Text)))
                    objBo.DocName = Convert.ToString(txtDocName.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(DocPath)))
                    objBo.DocPath = Convert.ToString(DocPath);

                objBo.DocType = Convert.ToInt32(ddlDocType.SelectedValue);

                if (rbnDocNewYES.Checked == true)
                    objBo.IsNewIcon = 1;
                else
                    objBo.IsNewIcon = 0;
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
                    MultiView1.ActiveViewIndex = -1;

                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    //BtnSave.Visible = false;
                    //BtnUpdate.Visible = false;
                    break;
                case VisibityType.Insert:
                    MultiView1.ActiveViewIndex = 0;
                    hfTenderId.Value = "0";
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    BtnSave.Visible = true;
                    BtnUpdate.Visible = false;
                    ClearControlValues(pnlEntry);
                    CKEditorControl1.Text = string.Empty;
                    CKEditorControl2.Text = string.Empty;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    MultiView1.ActiveViewIndex = 0;
                    BtnSave.Visible = false;
                    BtnUpdate.Visible = true;
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
            gView.DataBind();
        }
    }
}