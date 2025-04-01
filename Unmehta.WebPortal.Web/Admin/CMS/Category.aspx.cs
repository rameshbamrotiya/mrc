using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class Category : System.Web.UI.Page
    {
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
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
                //FillSchemes();
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
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;

                    btn_Update.Visible = false;
                    hfID.Value = "0";
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:

                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
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
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
        //private void FillSchemes()
        //{
        //    DataSet ds = new DataSet();
        //    SchemeBAL objBAL = new SchemeBAL();
        //    SchemeBO objbo = new SchemeBO();
        //    objbo.Language = 1;
        //    ds = objBAL.SelectSchemeByLanguage(objbo);
        //    DataTable dt = ds.Tables[0];
        //    PopulateDropDownList(drpScheme, dt, "SchemeName", "SchemeID", true);

        //}
        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnCatID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void LoadControls(CategoryBO objBo)
        {

            objBo.Enabled = (drpstatus.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add= GetIPAddress;
            objBo.LanguageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            objBo.CategoryName = txtCategory.Text.Trim();
            //objBo.SchemeID = Convert.ToInt16(drpScheme.SelectedValue.ToString());
            
        }
        private void BindGridView()
        {
            grdDetails.DataBind();
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryBO objBo = new CategoryBO();
                LoadControls(objBo);
                

                
                    if (new CategoryBAL().InsertRecord(objBo))
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                CategoryBO objBo = new CategoryBO();
                LoadControls(objBo);
                objBo.CategoryID =Convert.ToInt16( hdnCatID.Value);
                if (new CategoryBAL().UpdateRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);

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


        private void FillControls(int CategoryID, int languageid)
        {
            try
            {
                CategoryBO objbo = new CategoryBO();
                CategoryBAL objBAL = new CategoryBAL();
                objbo.CategoryID = CategoryID;
                objbo.LanguageId = languageid;
                hdnCatID.Value = Convert.ToInt16(CategoryID).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectCategiryById(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtCategory.Text = dr["CategoryName"].ToString();

                    hdnRecid.Value = dr["recid"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpstatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    //drpScheme.SelectedValue = dr["Schemeid"].ToString();


                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = languageid.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }


        }
        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                // hdnRecid.Value = GrdFutureVision.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["CategoryID"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["LanguageID"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
    }
}