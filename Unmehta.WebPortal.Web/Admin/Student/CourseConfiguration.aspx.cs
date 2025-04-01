using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class CourseConfiguration : System.Web.UI.Page
    {
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            try
            {
                if (!IsPostBack)
                {
                    ClearControlValues(pnlEntry);
                    BindGridView();
                    BindDropDown();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                {
                    if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                    {
                        int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        Int32 bytID;
                        bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            objStudentCourseBAL.RemoveCourseConfigurationData(bytID, SessionWrapper.UserDetails.UserName);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentCourseConfigurationBO objBo = new StudentCourseConfigurationBO();

                if (LoadControls(objBo))
                {


                    if (Convert.ToDateTime(objBo.StartDate) > Convert.ToDateTime(objBo.EndDate))
                    {
                        Functions.MessagePopup(this, "End date must be greater than start date.", PopupMessageType.warning);
                        return;
                    }
                    using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                    {
                        if (objStudentCourseBAL.InsertOrUpdateCourseConfigurationData(objBo))
                        {
                            if (objBo.Id == 0)
                            {
                                Functions.MessagePopup(this, "Record Insert successfully.", PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                            }
                            if (objBo.Id == 0)
                            {
                                var dataTa = objStudentCourseBAL.GetAllStudentCourseConfiguration();
                                List<StudentCourseConfigurationBO> data = Functions.ToListof<StudentCourseConfigurationBO>(dataTa).OrderByDescending(x => x.Id).ToList();
                                objBo.Id = data.FirstOrDefault().Id;
                            }
                            using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
                            {
                                if (Functions.ToListof<StudentEducationTypeDetailBO>(objStudentCourseBAL.GetAlltMinimumEducationTypeDetailsById(Convert.ToInt32(ddlType.SelectedValue))).Count > 0)
                                {
                                    objStudentCourseBAL.RemovetMinimumEducationTypeDetailsById(Convert.ToInt32(ddlType.SelectedValue));
                                }
                                foreach (DataListItem item in dlChkMinEducationType.Items)
                                {
                                    string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                                    if ((item.FindControl("chkRow") as CheckBox).Checked)
                                    {

                                        var data = Functions.ToListof<StudentEducationTypeBO>(objStudentEducationQualificationBAL.GetAllEducationType()).Where(x => x.TypeName == value).FirstOrDefault();
                                        {
                                            if (data != null)
                                            {
                                                if (objStudentCourseBAL.InserttMinimumEducationTypeDetails(Convert.ToInt32(ddlType.SelectedValue), data.Id))
                                                {
                                                }
                                            }
                                        }
                                    }
                                    //objAdvertisementRepository.
                                }
                                if (Functions.ToListof<StudentEducationQualificationDetailBO>(objStudentCourseBAL.GetAlltEducationQualificationById(Convert.ToInt32(ddlType.SelectedValue))).Count > 0)
                                {
                                    objStudentCourseBAL.RemoveEducationQualificationById(Convert.ToInt32(ddlType.SelectedValue));
                                }
                                foreach (DataListItem item in dlVisableEducation.Items)
                                {
                                    if ((item.FindControl("chkRow") as CheckBox).Checked)
                                    {
                                        string value = (item.FindControl("LblEducationDetailName") as Label).Text;

                                        var data = Functions.ToListof<StudentEducationQualificationBO>(objStudentEducationQualificationBAL.GetAll());
                                        {
                                            if (data != null)
                                            {
                                                var subDetails = data.Where(x => x.EducationDetailName == value).FirstOrDefault();
                                                if (objStudentCourseBAL.InsertEducationQualification(Convert.ToInt32(ddlType.SelectedValue), subDetails.Id))
                                                {
                                                }
                                            }
                                        }
                                    }
                                    //objAdvertisementRepository.
                                }
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentCourseConfigurationBO objBo = new StudentCourseConfigurationBO();
                if (LoadControls(objBo))
                {
                   
                    if (Convert.ToDateTime(objBo.StartDate) > Convert.ToDateTime(objBo.EndDate))
                    {
                        Functions.MessagePopup(this, "End date must be greater than start date.", PopupMessageType.warning);
                        return;
                    }
                    using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                    {
                        if (objStudentCourseBAL.InsertOrUpdateCourseConfigurationData(objBo))
                        {
                            using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
                            {
                                if (Functions.ToListof<StudentEducationTypeDetailBO>(objStudentCourseBAL.GetAlltMinimumEducationTypeDetailsById(Convert.ToInt32(ddlType.SelectedValue))).Count > 0)
                                {
                                    objStudentCourseBAL.RemovetMinimumEducationTypeDetailsById(Convert.ToInt32(ddlType.SelectedValue));
                                }
                                foreach (DataListItem item in dlChkMinEducationType.Items)
                                {
                                    string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                                    if ((item.FindControl("chkRow") as CheckBox).Checked)
                                    {

                                        var data = Functions.ToListof<StudentEducationTypeBO>(objStudentEducationQualificationBAL.GetAllEducationType()).Where(x => x.TypeName == value).FirstOrDefault();
                                        {
                                            if (data != null)
                                            {
                                                objStudentCourseBAL.InserttMinimumEducationTypeDetails(Convert.ToInt32(ddlType.SelectedValue), data.Id);
                                            }
                                        }
                                    }
                                    //objAdvertisementRepository.
                                }
                                if (Functions.ToListof<StudentEducationQualificationDetailBO>(objStudentCourseBAL.GetAlltEducationQualificationById(Convert.ToInt32(ddlType.SelectedValue))).Count > 0)
                                {
                                    objStudentCourseBAL.RemoveEducationQualificationById(Convert.ToInt32(ddlType.SelectedValue));
                                }
                                foreach (DataListItem item in dlVisableEducation.Items)
                                {
                                    if ((item.FindControl("chkRow") as CheckBox).Checked)
                                    {
                                        string value = (item.FindControl("LblEducationDetailName") as Label).Text;

                                        var data = Functions.ToListof<StudentEducationQualificationBO>(objStudentEducationQualificationBAL.GetAll());
                                        {
                                            if (data != null)
                                            {
                                                var subDetails = data.Where(x => x.EducationDetailName == value).FirstOrDefault();
                                                objStudentCourseBAL.InsertEducationQualification(Convert.ToInt32(ddlType.SelectedValue), subDetails.Id);
                                            }
                                        }
                                    }
                                    //objAdvertisementRepository.
                                }
                            }

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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        #endregion

        #region Page Functions
        private bool FillControls(long iPkId)
        {
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAllStudentCourseConfiguration();
                if (dataOfUserName != null)
                {
                    List<StudentCourseConfigurationBO> data = Functions.ToListof<StudentCourseConfigurationBO>(dataOfUserName);

                    if (data != null)
                    {
                        var objBo = data.Where(x => x.Id == iPkId).FirstOrDefault();
                        if (objBo != null)
                        {
                            hfTemplateId.Value = objBo.Id.ToString();
                            txtFees.Text = objBo.EntryFees.ToString();
                            txtAgeTo.Text = objBo.MinAge.ToString();

                            if (objBo.StartDate != null)
                            {
                                string[] startArrray = ((DateTime)objBo.StartDate).ToString("dd/MM/yyyy HH:mm").Split(' ');
                                txtStartDate.Text = startArrray[0].Replace("-", "/");
                                txtStartTime.Text = startArrray[1];
                            }
                            if (objBo.EndDate != null)
                            {
                                string[] EndArrray = ((DateTime)objBo.EndDate).ToString("dd/MM/yyyy HH:mm").Split(' ');
                                txtEndDate.Text = EndArrray[0].Replace("-", "/");
                                txtEndTime.Text = EndArrray[1];
                            }

                            List<StudentEducationTypeDetailBO> dataRelated = Functions.ToListof<StudentEducationTypeDetailBO>(objStudentAdvertisementBAL.GetAlltMinimumEducationTypeDetailsById((objBo.CourseId))).ToList();
                            foreach (DataListItem item in dlChkMinEducationType.Items)
                            {
                                string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                                foreach (var row in dataRelated)
                                {
                                    if (row.TypeName == value)
                                    {
                                        (item.FindControl("chkRow") as CheckBox).Checked = true;
                                        break;
                                    }
                                    //objAdvertisementRepository.
                                }
                            }
                            List<StudentEducationQualificationDetailBO> dataRelateds = Functions.ToListof<StudentEducationQualificationDetailBO>(objStudentAdvertisementBAL.GetAlltEducationQualificationById(objBo.CourseId)).ToList();
                            foreach (DataListItem item in dlVisableEducation.Items)
                            {
                                string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                                foreach (var row in dataRelateds)
                                {
                                    if (row.QualificationName == value)
                                    {
                                        (item.FindControl("chkRow") as CheckBox).Checked = true;
                                        break;
                                    }
                                    //objAdvertisementRepository.
                                }
                            }

                            ddlActiveInactive.SelectedValue = Convert.ToString(objBo.IsVisible);
                            ddlType.SelectedValue = objBo.CourseId.ToString();
                            ddlAdvertisement.SelectedValue = objBo.StudentAdvertisementId.ToString();
                            txtDescription.Text = HttpUtility.HtmlDecode(objBo.Desciption);
                        }
                    }
                }
            }
            return true;
        }

        private void BindDropDown()
        {

            using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentCourseBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);
                    if (data != null)
                    {
                        ddlType.DataSource = data.Where(x => x.IsVisible == true).ToList();
                        ddlType.DataTextField = "Name";
                        ddlType.DataValueField = "Id";
                        ddlType.DataBind();
                        ddlType.Items.Insert(0, "Select");
                        ddlType.SelectedIndex = 0;
                    }
                }
            }
            using (StudentAdvertisementBAL objStudentAdvertisementBAL = new StudentAdvertisementBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);
                    if (data != null)
                    {
                        ddlAdvertisement.DataSource = data.Where(x => x.IsVisible == true).ToList();
                        ddlAdvertisement.DataTextField = "Name";
                        ddlAdvertisement.DataValueField = "Id";
                        ddlAdvertisement.DataBind();
                        ddlAdvertisement.Items.Insert(0, "Select");
                        ddlAdvertisement.SelectedIndex = 0;
                    }
                }
            }

            using (StudentEducationQualificationBAL objStudentAdvertisementBAL = new StudentEducationQualificationBAL())
            {
                dlChkMinEducationType.DataSource = objStudentAdvertisementBAL.GetAllEducationType();
                dlChkMinEducationType.DataBind();
            }
            using (StudentEducationQualificationBAL objEducationQualificationRepository = new StudentEducationQualificationBAL())
            {
                dlVisableEducation.DataSource = objEducationQualificationRepository.GetAll();
                dlVisableEducation.DataBind();
            }
        }

        private void BindGridView()
        {
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAllStudentCourseConfiguration();
                if (dataOfUserName != null)
                {
                    List<StudentCourseConfigurationBO> data = Functions.ToListof<StudentCourseConfigurationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                        {
                            gView.DataSource = data.Where(x => x.CourseName.Contains(txtSearch.Text) || x.AdvertisementName.Contains(txtSearch.Text)).ToList();
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

        private bool LoadControls(StudentCourseConfigurationBO objBo)
        {
            if (ddlType.SelectedIndex <= 0)
            {
                Functions.MessagePopup(this, "Please Select Course", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.CourseId = Convert.ToInt32(ddlType.SelectedValue);
            }
            if (ddlAdvertisement.SelectedIndex <= 0)
            {
                Functions.MessagePopup(this, "Please Select Advertisement", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.StudentAdvertisementId = Convert.ToInt32(ddlAdvertisement.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(txtAgeTo.Text))
            {
                objBo.MinAge = Convert.ToInt32(txtAgeTo.Text);
            }
            else
            {
                objBo.MinAge = null;
            }
            string strError;
            DateTime? dt;
            if (string.IsNullOrWhiteSpace(txtStartDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Start Date", PopupMessageType.error);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtStartTime.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Start Time", PopupMessageType.error);
                return false;
            }
            else
            {
                if (!Functions.GetDateTimeFromString((txtStartDate.Text + " " + txtStartTime.Text), out dt, out strError))
                {
                    objBo.StartDate = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Start Date", PopupMessageType.error);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtEndDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select End Date", PopupMessageType.error);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtEndTime.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select End Time", PopupMessageType.error);
                return false;
            }
            else
            {
                if (!Functions.GetDateTimeFromString((txtEndDate.Text + " " + txtEndTime.Text), out dt, out strError))
                {
                    objBo.EndDate = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in End Date", PopupMessageType.error);
                    return false;
                }
            }

            {
                objBo.Desciption = HttpUtility.HtmlEncode(txtDescription.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtFees.Text))
            {
                objBo.EntryFees = Convert.ToInt32(txtFees.Text);
            }
            else
            {
                objBo.EntryFees = null;
            }
            objBo.IsVisible = Convert.ToBoolean(ddlActiveInactive.SelectedValue);
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfTemplateId.Value);
            }

            long lgMinCountType = dlChkMinEducationType.Items.Count, lgChkMinCount = 0;
            long lgEducationType = dlVisableEducation.Items.Count, lgChkEducationType = 0;

            foreach (DataListItem item in dlChkMinEducationType.Items)
            {
                string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                if ((item.FindControl("chkRow") as CheckBox).Checked)
                {
                    lgChkMinCount++;
                }
            }

            if (lgChkMinCount <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select at least one Minimum Education Type", PopupMessageType.error);
                return false;
            }
            foreach (DataListItem item in dlVisableEducation.Items)
            {
                string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                if ((item.FindControl("chkRow") as CheckBox).Checked)
                {
                    lgChkEducationType++;
                }
            }

            if (lgChkEducationType <= 0)
            {
                Functions.MessageFrontPopup(this, "Please Select at least one Education For Visable", PopupMessageType.error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStartDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Start Date", PopupMessageType.error);
                return false;
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
    }
}