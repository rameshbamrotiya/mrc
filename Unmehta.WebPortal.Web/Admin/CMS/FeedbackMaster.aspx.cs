using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class FeedbackMaster : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    BindGridView();
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
                txtDateFrom.Text = string.Empty;
                txtDateTo.Text = string.Empty;                
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region GridView Operation        
        private void BindGridView()
        {
            FeedbackBAL objBAL = new FeedbackBAL();
            DataSet ds = new DataSet();
            ds = objBAL.SelectAllRecord();
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
        }
        #endregion

        #region ShowHideControl || Notification        
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtDateFrom.Text)) && (!string.IsNullOrEmpty(txtDateTo.Text)))
                {
                    FeedbackBO objBO = new FeedbackBO();
                    objBO.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    objBO.DateTo = Convert.ToDateTime(txtDateTo.Text);
                    objBO.FullName = txtSearch.Text;
                    FeedbackBAL objBAL = new FeedbackBAL();
                    DataSet ds = new DataSet();
                    ds = objBAL.SelectRecord(objBO);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        gView.DataSource = ds;
                        gView.DataBind();
                        txtSearch.Text = "";
                        txtDateFrom.Text = "";
                        txtDateTo.Text = "";
                    }
                    else
                    {
                        gView.DataSource = null;
                        gView.DataBind();
                    }
                }
                else if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    FeedbackBO objBO = new FeedbackBO();
                    objBO.DateFrom = Convert.ToDateTime(txtDateFrom.Text == "" ? "1/1/1753 12:00:00" : txtDateFrom.Text);
                    objBO.DateTo = Convert.ToDateTime(txtDateTo.Text == "" ? "1/1/1753 12:00:00" : txtDateTo.Text);
                    objBO.FullName = txtSearch.Text;
                    FeedbackBAL objBAL = new FeedbackBAL();
                    DataSet ds = new DataSet();
                    ds = objBAL.SelectRecord(objBO);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        gView.DataSource = ds;
                        gView.DataBind();
                        txtSearch.Text = "";
                        txtDateFrom.Text = "";
                        txtDateTo.Text = "";
                    }
                    else
                    {
                        gView.DataSource = null;
                        gView.DataBind();
                    }
                }
                else
                {
                    BindGridView();
                    txtSearch.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}