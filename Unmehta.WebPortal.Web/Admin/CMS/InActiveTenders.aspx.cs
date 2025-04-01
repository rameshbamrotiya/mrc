using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class InActiveTenders : System.Web.UI.Page
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private void BindGridView()
        {
            gView.DataBind();
        }

        protected void Bind_DocGrid()
        {
            TenderMasterBAL objBAL = new TenderMasterBAL();
            TenderMasterBO objBO = new TenderMasterBO();
        }
        protected void btn_Inactive_Click(object sender, EventArgs e)
        {
            try
            {
                string check = string.Empty;
                foreach (GridViewRow gr in gView.Rows)
                {
                    CheckBox ck = (CheckBox)gr.FindControl("ibtn_check");
                    if (ck.Checked)
                    {
                        check = check + (gView.DataKeys[gr.RowIndex].Value.ToString()) + ",";
                    }
                }
                check = check.Remove(check.Length - 1, 1).ToString();
                ActiveTenderBO objBo = new ActiveTenderBO();
                objBo.id = check;
                if (new ActiveTenderBAL().UpdateRecordActiveTenders(objBo))
                {
                    BindGridView();
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion
    }
}