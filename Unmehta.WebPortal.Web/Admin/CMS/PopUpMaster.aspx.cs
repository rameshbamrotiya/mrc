using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Web;
using System.Data;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PopUpMaster : System.Web.UI.Page
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
                        ShowHideControl(VisibityType.Insert);
                        FillControls();
                    }
                //}
                //else
                //    Response.Redirect("/CMS/LoginPage.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }

        }
        #endregion

        #region Bind Details
        private bool FillControls()
        {
            PopUpMasterBO objBo = new PopUpMasterBO();
            objBo.page_name = "HOME_POPUP";
            DataSet ds = new PopUpMasterBAL().PopUpMaster_SelectBypagenameID(objBo);
            if (ds == null) return false;
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["col_page_desc"] != DBNull.Value)
                CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["col_page_desc"].ToString());

            if (dr["enabled"] != DBNull.Value)
                ddlActiveInactiveStatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
            return true;
        }

        #endregion

        #region Save || Update || Cancel
        private void LoadControls_PopUpMaster(PopUpMasterBO objBo)
        {
            objBo.page_name = "HOME_POPUP";
            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                objBo.Details = HttpUtility.HtmlEncode(CKEditorControl1.Text);
            objBo.IsActive = ddlActiveInactiveStatus.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PopUpMasterBO objBO = new PopUpMasterBO();
                LoadControls_PopUpMaster(objBO);
                if (new PopUpMasterBAL().Update_PopUpMaster(objBO))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                    //ShowMessage("Record updated successfully.", MessageType.Success);
                }
                else
                {
                    Functions.MessagePopup(this, "Something went to wrong.", PopupMessageType.success);
                    //ShowMessage("Something went to wrong.", MessageType.Error);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlEntry.Visible = false;
                    MultiView1.ActiveViewIndex = -1;
                    break;
                case VisibityType.View:
                    pnlEntry.Visible = true;
                    BtnSave.Visible = false;
                    break;
                case VisibityType.Insert:
                    MultiView1.ActiveViewIndex = 0;
                    pnlEntry.Visible = true;
                    BtnSave.Visible = true;
                    ClearControlValues(pnlEntry);
                    CKEditorControl1.Text = string.Empty;
                    break;
                case VisibityType.Edit:
                    pnlEntry.Visible = true;
                    MultiView1.ActiveViewIndex = 0;
                    BtnSave.Visible = false;
                    break;
                default:
                    pnlEntry.Visible = false;
                    break;
            }
        }
        #endregion
    }
}