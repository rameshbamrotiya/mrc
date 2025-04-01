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

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class ArticlesMaster : System.Web.UI.Page
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
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    BindDropdownMaster();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
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
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Articles_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        ArticlesMasterBO objBo = new ArticlesMasterBO();
                        objBo.Articles_id = bytID;
                        new ArticlesMasterBAL().DeleteRecord(objBo);
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControls(Int32 iPkId, int languageId)
        {
            BindDropdownMaster();
            ArticlesMasterBO objBo = new ArticlesMasterBO();
            objBo.Articles_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new ArticlesMasterBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0))
            {
                txtArticles.Text = "";
                txtWebsiteLink.Text = "";
                txtPublicationYear.Text = "";
                txtAuthor.Text = "";
                CKEditorControl1.Text = "";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr.HasErrors) return false;
                if (dr["Articles_Name"] != DBNull.Value)
                    txtArticles.Text = Convert.ToString(dr["Articles_Name"]);
                if (dr["is_active"] != DBNull.Value)
                    ddlActiveInactive.SelectedValue = Convert.ToString(dr["is_active"]);
                if (dr["AD_id"] != DBNull.Value)
                    ddlArticleDepartment.SelectedValue = Convert.ToString(dr["AD_id"]);
                if (dr["AT_id"] != DBNull.Value)
                    ddlArticleType.SelectedValue = Convert.ToString(dr["AT_id"]);
                if (dr["PT_id"] != DBNull.Value)
                    ddlPublicationType.SelectedValue = Convert.ToString(dr["PT_id"]);
                if (dr["Description"] != DBNull.Value)
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                if (dr["Author"] != DBNull.Value)
                    txtAuthor.Text = Convert.ToString(dr["Author"]);
                if (dr["Publication_Year"] != DBNull.Value)
                    txtPublicationYear.Text = Convert.ToString(dr["Publication_Year"]);
                if (dr["Web_link"] != DBNull.Value)
                    txtWebsiteLink.Text = Convert.ToString(dr["Web_link"]);
            }
            return true;
        }
        private void BindGridView()
        {
            BindDropdownMaster();
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(ArticlesMasterBO objBo)
        {
            objBo.Articles_Name = txtArticles.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.AD_id = ddlArticleDepartment.SelectedValue.ToString();
            objBo.AT_id = ddlArticleType.SelectedValue.ToString();
            objBo.PT_id = ddlPublicationType.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                objBo.Description = HttpUtility.HtmlEncode(CKEditorControl1.Text);
            objBo.Author = txtAuthor.Text;
            objBo.Publication_Year = txtPublicationYear.Text;
            objBo.Web_link = txtWebsiteLink.Text;
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
                ArticlesMasterBO objBo = new ArticlesMasterBO();
                LoadControls(objBo);
                if (new ArticlesMasterBAL().InsertRecord(objBo))
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


                ArticlesMasterBO objBo = new ArticlesMasterBO();
                LoadControls(objBo);
                objBo.Articles_id = Convert.ToInt32(ViewState["PK"]);
                if (new ArticlesMasterBAL().UpdateRecord(objBo))
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
                ViewState["T017PDetails"] = null;
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
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
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        private void BindDropdownMaster()
        {
            //ArticleDepartment
            ddlArticleDepartment.Items.Clear();
            ArticlesMasterBO objBo = new ArticlesMasterBO();
            int languageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.LanguageId = languageId;
            ArticlesMasterBAL objBal = new ArticlesMasterBAL();
            ddlArticleDepartment.DataSource = objBal.SelectRecordArticleDepartment(objBo);
            ddlArticleDepartment.DataTextField = "Name";
            ddlArticleDepartment.DataValueField = "Id";
            ddlArticleDepartment.DataBind();
            ddlArticleDepartment.Items.Insert(0, new ListItem("-Select Article Department-", "-1"));
            //ArticleType
            ddlArticleType.Items.Clear();
            ddlArticleType.DataSource = objBal.SelectRecordArticleType(objBo);
            ddlArticleType.DataTextField = "Name";
            ddlArticleType.DataValueField = "Id";
            ddlArticleType.DataBind();
            ddlArticleType.Items.Insert(0, new ListItem("-Select Article Type-", "-1"));
            //PublicationType
            ddlPublicationType.Items.Clear();
            ddlPublicationType.DataSource = objBal.SelectRecordPublicationType(objBo);
            ddlPublicationType.DataTextField = "Name";
            ddlPublicationType.DataValueField = "Id";
            ddlPublicationType.DataBind();
            ddlPublicationType.Items.Insert(0, new ListItem("-Select Publication Type-", "-1"));
        }
    }
}