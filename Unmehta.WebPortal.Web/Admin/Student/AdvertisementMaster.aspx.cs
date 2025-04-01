using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class AdvertisementMaster : System.Web.UI.Page
    {
        #region Page Functions
        private bool FillControls(long iPkId)
        {
            using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentAdvertisementBO> data = Functions.ToListof<StudentAdvertisementBO>(dataOfUserName);
                    if (data != null)
                    {
                        var objBo = data.Where(x => x.Id == iPkId).FirstOrDefault();
                        if (objBo != null)
                        {
                            hfTemplateId.Value = objBo.Id.ToString();
                            ddlActiveInactive.SelectedValue = Convert.ToString(objBo.IsVisible);
                            txtName.Text= objBo.Name ;
                            txtCode.Text=objBo.Code  ;
                            txtPublishDate.Text= objBo.PublishDate ==null? "": ((DateTime)objBo.PublishDate).ToString("dd/MM/yyyy").Replace("-","/");
                            txtDescription.Text= HttpUtility.HtmlDecode(objBo.Desciption);
                        }
                    }
                }
            }
            return true;
        }

        private void BindGridView()
        {
            using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentAdvertisementBO> data = Functions.ToListof<StudentAdvertisementBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                        {
                            gView.DataSource = data.Where(x => x.Name.Contains(txtSearch.Text) || x.Code.Contains(txtSearch.Text)).ToList();
                        }
                        else
                        {
                            gView.DataSource = data;
                        }

                        gView.DataBind();
                    }
                }
            }
        }

        private bool LoadControls(StudentAdvertisementBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Name = txtName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                Functions.MessagePopup(this, "Please Enter Code", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Code = txtCode.Text;
            }
            string strError;
            DateTime? dt;
            if (string.IsNullOrWhiteSpace(txtPublishDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                return false;
            }
            else
            {
                if (!Functions.GetDateFromString(txtPublishDate.Text.Trim(), out dt, out strError))
                {
                    objBo.PublishDate = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    return false;
                }
            }
            {
                objBo.Desciption = HttpUtility.HtmlEncode(txtDescription.Text);
            }
            {
                objBo.IsVisible = Convert.ToBoolean(ddlActiveInactive.SelectedValue);
            }
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfTemplateId.Value);
            }
            objBo.UpdateBy = SessionWrapper.UserDetails.UserName;
            return true;
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
                    ClearControlValues(pnlEntry);
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
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
        #endregion

        #region Page Method
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
                    ClearControlValues(pnlEntry);
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
                {
                    if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                    {
                        int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        Int32 bytID;
                        bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            objStudentAdvertisementBAL.RemoveData(bytID, SessionWrapper.UserDetails.UserName);
                            BindGridView();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            //ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        ClearControlValues(pnlEntry);
                        if (FillControls(bytID))
                        {
                            if (e.CommandName == "eView")
                                ShowHideControl(VisibityType.View);
                            if (e.CommandName == "eEdit")
                            {
                                hfTemplateId.Value = bytID.ToString();
                                ShowHideControl(VisibityType.Edit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
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

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentAdvertisementBO objBo = new StudentAdvertisementBO();
                if (LoadControls(objBo))
                {
                    using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
                    {
                        if (objStudentAdvertisementBAL.InsertOrUpdateData(objBo))
                        {
                            if (objBo.Id == 0)
                            {
                                Functions.MessagePopup(this, "Record Insert successfully.", PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                            }
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentAdvertisementBO objBo = new StudentAdvertisementBO();
                if (LoadControls(objBo))
                {
                    using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
                    {
                        if (objStudentAdvertisementBAL.InsertOrUpdateData(objBo))
                        {
                            if (objBo.Id == 0)
                            {
                                Functions.MessagePopup(this, "Record Insert successfully.", PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                            }
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
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
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion
    }
}