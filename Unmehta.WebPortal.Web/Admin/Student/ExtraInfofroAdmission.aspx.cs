using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class ExtraInfofroAdmission : System.Web.UI.Page
    {
        string strStudentId;
        string strCourseId;
        string strCourseName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Admission/Default.aspx");
                    }

                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();

                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strCourseName = strQuery[2].ToString().Replace("CourseName=", "");

                    FillAllCheckBox();
                    BindYear();
                    FillDataForEditMode();
                    BindProfessonalReferalGrid();
                    string message = "";
                    foreach (ListItem item in chk1.Items)
                    {
                        if (item.Selected)
                        {
                            message += item.Text.ToLower();
                            if (message.Contains("other"))
                            {
                                CLRemarks.Visible = true;
                            }
                            else
                            {
                                CLRemarks.Visible = false;
                            }
                        }
                        else
                        {
                            CLRemarks.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                    Response.Redirect("~/Admission/Defult.aspx");
                }
            }
        }
        private void FillAllCheckBox()
        {
            try
            {
                {
                    ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();
                    DataSet ds = objBAL.SelectAdvertisementSourceWise();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chk1.DataSource = ds.Tables[0];
                        chk1.DataTextField = "Advertisement_Name";
                        chk1.DataValueField = "id";
                        chk1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        //protected void btnKnowPersonDetails_Click(object sender, EventArgs e)
        //{
        //    string errorMessage = "";
        //    ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
        //    if (string.IsNullOrEmpty(txtKnownPersonName.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter Person Name.", PopupMessageType.warning);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtPosition.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter Position.", PopupMessageType.warning);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtRelatioship.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter RelationShip.", PopupMessageType.warning);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtYearsKnown.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter Years Known.", PopupMessageType.warning);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtTelePhone.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter Mobile No.", PopupMessageType.warning);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtAddressreferral.Text))
        //    {
        //        Functions.MessagePopup(this, "Enter Address.", PopupMessageType.warning);
        //        return;
        //    }
        //    LoadControlsForReferalDetails(objBo);

        //    if (new ExtraInfoforAdmissionBAL().InsertRecordReferralDetails(objBo))
        //    {
        //        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
        //    }
        //    else
        //    {
        //        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.error);
        //        return;
        //    }
        //    ClearControlValues();
        //    BindProfessonalReferalGrid();
        //}
        private void BindProfessonalReferalGrid()
        {
            ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
            objBo.CandidateId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt64(strCourseId);
            ExtraInfoforAdmissionBAL objbal = new ExtraInfoforAdmissionBAL();
            DataSet ds = objbal.SelectRecordReferral(objBo);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.AcceptChanges();
                grdReferalDetails.DataSource = dt;
                grdReferalDetails.DataBind();
            }
            else
            {
                grdReferalDetails.DataBind();
            }
        }
        private void BindYear()
        {
            ddlExtrInfoYear.Items.Clear();
            ddlExtrInfoYear.Items.Insert(0, new ListItem("Select Year", "-1"));
            for (int i = 1960; i <= DateTime.Now.Year; i++)
            {
                ddlExtrInfoYear.Items.Add(i.ToString());
            }
        }
        private bool FillDataForEditMode()
        {
            //try
            //{
            //    ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
            //    objBo.CandidateId = Convert.ToInt32(strStudentId);
            //    objBo.CourseId = Convert.ToInt64(strCourseId);
            //    //objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            //    ExtraInfoforAdmissionBAL objbal = new ExtraInfoforAdmissionBAL();
            //    DataSet ds = objbal.SelectRecord(objBo);
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = ds.Tables[0].Rows[0];
            //        if (dr.HasErrors) return false;
            //        if (dr["Id"] != DBNull.Value)
            //            hdfExtrainfo.Value = dr["Id"].ToString();
            //        if (dr["UNMICRCContactYN"] != DBNull.Value)
            //            RBLknowperson.SelectedValue = dr["UNMICRCContactYN"].ToString();
            //        if (RBLknowperson.SelectedValue == "yes")
            //        {
            //            txtUNMICRCPerson.Visible = true;
            //            txtUNMICRCPerson.Text = dr["UNMICRCContact"].ToString();
            //        }
            //        else
            //        {
            //            txtUNMICRCPerson.Visible = false;
            //        }

            //        if (dr["courseheard"] != DBNull.Value)
            //            txtcourseheard.InnerText = dr["courseheard"].ToString();
            //        //if (dr["ChronicalIllness"] != DBNull.Value)
            //        //    txtChronicillness.Text = dr["ChronicalIllness"].ToString();
            //        if (dr["ChronicalIllnessYN"] != DBNull.Value)
            //            RBLilleness.SelectedValue = dr["ChronicalIllnessYN"].ToString();
            //        if (RBLilleness.SelectedValue == "yes")
            //        {
            //            txtChronicillness.Visible = true;
            //            txtChronicillness.Text = dr["ChronicalIllness"].ToString();
            //        }
            //        else
            //        {
            //            txtChronicillness.Visible = false;
            //        }
            //        if (dr["BloodGroup"] != DBNull.Value)
            //            txtbloodgroup.Text = dr["BloodGroup"].ToString();
            //        if (dr["MonthYear"] != DBNull.Value)
            //        {
            //            DateTime dti = DateTime.ParseExact(dr["MonthYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
            //            ddlExtrInfoYear.SelectedValue = dti.Year.ToString();
            //            ddlMonth.SelectedValue = dti.ToString("MMM");
            //        }
            //        if (dr["Allergic"] != DBNull.Value)
            //            txtmajorillness.Text = dr["Allergic"].ToString();
            //        if (dr["EmergencyContactNo"] != DBNull.Value)
            //            txtcontactno.Text = dr["EmergencyContactNo"].ToString();
            //        if (dr["EmergencyContactPersonName"] != DBNull.Value)
            //            txtconperson.Text = dr["EmergencyContactPersonName"].ToString();
            //        if (dr["EmergencyContactPersonAdd"] != DBNull.Value)
            //            txtaddress.Text = dr["EmergencyContactPersonAdd"].ToString();
            //        if (dr["CourtOfLawYN"] != DBNull.Value)
            //        {
            //            rblcourtoflaw.SelectedValue = dr["CourtOfLawYN"].ToString();

            //        }
            //        if (rblcourtoflaw.SelectedValue == "YES")
            //        {
            //            txtcourtoflaw.Visible = true;
            //            txtcourtoflaw.Text = dr["CourtOfLaw"].ToString();
            //        }
            //        else
            //        {
            //            txtcourtoflaw.Visible = false;
            //            rblcourtoflaw.SelectedValue = "NO";
            //        }
            //        if (dr["ExtraActivities"] != DBNull.Value)
            //            txtextraactivities.Text = dr["ExtraActivities"].ToString();
            //        if (dr["Accommodation"] != DBNull.Value)
            //            rblaccommodation.Text = dr["Accommodation"].ToString();
            //        //if (dr["AboutCourse"] != DBNull.Value)
            //        //string chk = dr["AboutCourse"].ToString();
            //        CheckBoxList c = (CheckBoxList)chk1.FindControl("chk1");
            //        string checkBoxValues = dr["AboutCourse"].ToString();
            //        char sep = ',';
            //        String[] s = checkBoxValues.Split(sep);

            //        for (int i = 0; i < s.Length; i++)
            //        {

            //            for (int j = 0; j < c.Items.Count; j++)
            //            {

            //                if (c.Items[j].Value == s[i])
            //                {
            //                    c.Items[j].Selected = true;
            //                }
            //            }
            //        }
            //        if (dr["ExtraActivitiesSocial"] != DBNull.Value)
            //            txtsocialactivities.Text = dr["ExtraActivitiesSocial"].ToString();
            //        //if (dr["AboutCourseOther"] != DBNull.Value)
            //        //    txtothercourse.Text = dr["AboutCourseOther"].ToString();
            //        if (dr["SurgeryInfo"] != DBNull.Value)
            //            txtsurgeryinfo.Text = dr["SurgeryInfo"].ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            //}
            //return true;
            try
            {
                ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
                objBo.CandidateId = Convert.ToInt32(strStudentId);
                objBo.CourseId = Convert.ToInt64(strCourseId);
                //objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
                ExtraInfoforAdmissionBAL objbal = new ExtraInfoforAdmissionBAL();
                DataSet ds = objbal.SelectRecord(objBo);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr.HasErrors) return false;
                    if (dr["Id"] != DBNull.Value)
                        hdfExtrainfo.Value = dr["Id"].ToString();
                    if (dr["UNMICRCContactYN"] != DBNull.Value)
                        RBLknowperson.SelectedValue = dr["UNMICRCContactYN"].ToString();
                    if (RBLknowperson.SelectedValue == "yes")
                    {
                        lblUNMICRCPerson.Visible = true;
                        txtUNMICRCPerson.Visible = true;
                        txtUNMICRCPerson.Focus();
                    }
                    else
                    {
                        lblUNMICRCPerson.Visible = false;
                        txtUNMICRCPerson.Visible = false;
                        RBLknowperson.Focus();
                    }
                    if (dr["UNMICRCContact"] != DBNull.Value)
                        txtUNMICRCPerson.Text = dr["UNMICRCContact"].ToString();
                    if (dr["courseheard"] != DBNull.Value)
                        txtcourseheard.InnerText = dr["courseheard"].ToString();
                    if (dr["ChronicalIllnessYN"] != DBNull.Value)
                        RBLilleness.SelectedValue = dr["ChronicalIllnessYN"].ToString();
                    if (RBLilleness.SelectedValue == "yes")
                    {
                        lblChronicillness.Visible = true;
                        txtChronicillness.Visible = true;
                        txtChronicillness.Focus();
                    }
                    else
                    {
                        lblChronicillness.Visible = false;
                        txtChronicillness.Visible = false;
                        RBLilleness.Focus();
                    }
                    if (dr["ChronicalIllness"] != DBNull.Value)
                        txtChronicillness.Text = dr["ChronicalIllness"].ToString();
                    if (dr["BloodGroup"] != DBNull.Value)
                        txtbloodgroup.Text = dr["BloodGroup"].ToString();
                    if (dr["MonthYear"] != DBNull.Value)
                    {
                        DateTime dti = DateTime.ParseExact(dr["MonthYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
                        ddlExtrInfoYear.SelectedValue = dti.Year.ToString();
                        ddlMonth.SelectedValue = dti.ToString("MMM");
                    }
                    if (dr["AllergicYN"] != DBNull.Value)
                        RBLAllergicto.SelectedValue = dr["AllergicYN"].ToString();
                    if (RBLAllergicto.SelectedValue == "yes")
                    {
                        txtmajorillness.Visible = true;
                        txtmajorillness.Focus();
                    }
                    else
                    {
                        txtmajorillness.Visible = false;
                        RBLAllergicto.Focus();
                    }
                    if (dr["Allergic"] != DBNull.Value)
                        txtmajorillness.Text = dr["Allergic"].ToString();
                    if (dr["EmergencyContactNo"] != DBNull.Value)
                        txtcontactno.Text = dr["EmergencyContactNo"].ToString();
                    if (dr["EmergencyContactPersonName"] != DBNull.Value)
                        txtconperson.Text = dr["EmergencyContactPersonName"].ToString();
                    if (dr["EmergencyContactPersonRelation"] != DBNull.Value)
                        txtrelation.Text = dr["EmergencyContactPersonRelation"].ToString();
                    if (dr["EmergencyContactPersonAdd"] != DBNull.Value)
                        txtaddress.Text = dr["EmergencyContactPersonAdd"].ToString();
                    if (dr["CourtOfLawYN"] != DBNull.Value)
                        rblcourtoflaw.SelectedValue = dr["CourtOfLawYN"].ToString();
                    if (rblcourtoflaw.SelectedValue == "YES".Trim())
                    {
                        txtcourtoflaw.Visible = true;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtcourtoflaw.ClientID + "').focus();", true);
                    }
                    else
                    {
                        txtcourtoflaw.Visible = false;
                        rblaccommodation.Focus();
                        rblaccommodation.SelectedValue = "Own";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + rblaccommodation.ClientID + "').focus();", true);
                    }
                    if (dr["CourtOfLaw"] != DBNull.Value)
                        txtcourtoflaw.Text = dr["CourtOfLaw"].ToString();

                    if (dr["ExtraActivities"] != DBNull.Value)
                        txtextraactivities.Text = dr["ExtraActivities"].ToString();
                    if (dr["Accommodation"] != DBNull.Value)
                        rblaccommodation.Text = dr["Accommodation"].ToString();
                    //if (dr["AboutCourse"] != DBNull.Value)
                    //string chk = dr["AboutCourse"].ToString();
                    CheckBoxList c = (CheckBoxList)chk1.FindControl("chk1");
                    string checkBoxValues = dr["AboutCourse"].ToString();
                    char sep = ',';
                    String[] s = checkBoxValues.Split(sep);

                    for (int i = 0; i < s.Length; i++)
                    {

                        for (int j = 0; j < c.Items.Count; j++)
                        {

                            if (c.Items[j].Value == s[i])
                            {
                                c.Items[j].Selected = true;
                            }
                        }
                    }
                    if (dr["ExtraActivitiesSocial"] != DBNull.Value)
                        txtsocialactivities.Text = dr["ExtraActivitiesSocial"].ToString();
                    //if (dr["AboutCourseOther"] != DBNull.Value)
                    //    txtothercourse.Text = dr["AboutCourseOther"].ToString();
                    if (dr["SurgeryInfoYN"] != DBNull.Value)
                        RBLSurgery.SelectedValue = dr["SurgeryInfoYN"].ToString();
                    if (RBLSurgery.SelectedValue == "yes")
                    {
                        txtsurgeryinfo.Visible = true;
                        DIVSurgeryMonth.Visible = true;
                        DIVSurgeryYear.Visible = true;
                        txtsurgeryinfo.Focus();
                    }
                    else
                    {
                        txtsurgeryinfo.Visible = false;
                        DIVSurgeryMonth.Visible = false;
                        DIVSurgeryYear.Visible = false;
                        RBLSurgery.Focus();
                    }
                    if (dr["SurgeryInfo"] != DBNull.Value)
                        txtsurgeryinfo.Text = dr["SurgeryInfo"].ToString();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
            return true;

        }
        //private bool LoadControlsForReferalDetails(ExtraInfoforAdmissionBO objBo)
        //{
        //    if (string.IsNullOrWhiteSpace(hfReferralId.Value))
        //    {
        //        objBo.Id = 0;
        //    }
        //    else
        //    {
        //        objBo.Id = Convert.ToInt32(hfReferralId.Value);
        //    }
        //    objBo.CandidateId = Convert.ToInt32(strStudentId);
        //    objBo.CourseId = Convert.ToInt64(strCourseId);
        //    objBo.Name = txtKnownPersonName.Text.ToString();
        //    objBo.Position = txtPosition.Text.ToString();
        //    objBo.MobileNo = txtTelePhone.Text.Trim();
        //    objBo.RelationShip = txtRelatioship.Text.ToString();
        //    objBo.YearsKnown = Convert.ToDouble(txtYearsKnown.Text.ToString());
        //    objBo.Address = txtAddressreferral.Text.ToString();
        //    objBo.modified_by = SessionWrapper.StudentRegistration.Username;
        //    return true;
        //}
        //private void ClearControlValues()
        //{
        //    txtKnownPersonName.Text = "";
        //    txtPosition.Text = "";
        //    txtTelePhone.Text = "";
        //    txtRelatioship.Text = "";
        //    txtYearsKnown.Text = "";
        //    hfReferralId.Value = "";
        //    txtAddressreferral.Text = "";
        //    btnKnowPersonDetails.Text = "Add Details";
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            Response.Redirect("~/Admission/StudentDetails?" + strEndQueryString);
            //string errorMessage = "";
            //ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
            //string message = "";
            //foreach (ListItem item in chk1.Items)
            //{
            //    if (item.Selected)
            //    {
            //        message += item.Text.ToLower();
            //        if (message.Contains("other"))
            //        {
            //            CLRemarks.Visible = true;
            //            if (!string.IsNullOrEmpty(txtcourseheard.InnerText))
            //            {
            //                objBo.courseheard = txtcourseheard.InnerText.ToString();
            //            }
            //            else
            //            {
            //                Functions.MessageFrontPopup(this, "Enter (If other please specify).", PopupMessageType.error);
            //                txtcourseheard.Focus();
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            CLRemarks.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        CLRemarks.Visible = false;
            //    }
            //}
            //if (grdReferalDetails.Rows.Count > 0)
            //{
            //    LoadControlsForExtraDetails(objBo);

            //    if (new ExtraInfoforAdmissionBAL().InsertRecord(objBo))
            //    {
            //        Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
            //    }
            //    else
            //    {
            //        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.error);
            //        return;
            //    }
            //    //ClearControlValues();
            //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            //    Response.Redirect("~/Admission/StudentDetails?" + strEndQueryString);
            //}
            //else
            //{
            //    Functions.MessagePopup(this, "Add one references of persons who know you professionally and Socially.", PopupMessageType.error);
            //    return;
            //}
        }
        private bool LoadControlsForExtraDetails(ExtraInfoforAdmissionBO objBo)
        {
            if (string.IsNullOrWhiteSpace(hdfExtrainfo.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hdfExtrainfo.Value);
            }
            objBo.CandidateId = Convert.ToInt32(strStudentId);
            objBo.CourseId = Convert.ToInt32(strCourseId);
            objBo.UNMICRCContact = txtUNMICRCPerson.Text;
            objBo.ChronicalIllness = txtChronicillness.Text;
            objBo.BloodGroup = txtbloodgroup.Text;
            objBo.Allergic = txtmajorillness.Text.ToString();
            if (ddlMonth.SelectedIndex > 0 && ddlExtrInfoYear.SelectedIndex > 0)
            {
                objBo.MonthYear = ddlMonth.SelectedValue + " " + ddlExtrInfoYear.SelectedValue;
            }
            objBo.EmergencyContactNo = txtcontactno.Text;
            objBo.EmergencyContactPersonName = txtconperson.Text.Trim();
            objBo.EmergencyContactPersonAdd = txtaddress.Text;
            if (rblcourtoflaw.SelectedValue == "YES".Trim())
            {
                objBo.CourtOfLaw = txtcourtoflaw.Text.ToString();
            }
            else
            {
                objBo.CourtOfLaw = "NO";
            }
            objBo.SurgeryInfo = txtsurgeryinfo.Text.ToString();
            objBo.Accommodation = rblaccommodation.Text.ToString();
            objBo.ExtraActivities = txtextraactivities.Text.ToString();
            objBo.ExtraActivitiesSocial = txtsocialactivities.Text.ToString();
            //objBo.AboutCourse = rblcourse.Text.ToString();
            //objBo.AboutCourseOther = txtothercourse.Text.ToString();
            objBo.AboutCourseOther = "";
            objBo.modified_by = SessionWrapper.StudentRegistration.Username;

            string val = "";
            foreach (ListItem li in chk1.Items)
            {
                if (li.Selected)
                {
                    val = val + li.Value + ",";
                }
            }
            if (val != "")
            {
                val = val.Remove(val.Length - 1, 1);
                objBo.AboutCourse = val;
            }
            else
            {
                objBo.AboutCourse = "";
            }
            return true;
        }

        //protected void lbtnEdit_Click(object sender, EventArgs e)
        //{
        //    int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //    hfReferralId.Value = grdReferalDetails.DataKeys[rowindex]["id"].ToString();
        //    txtKnownPersonName.Text = grdReferalDetails.Rows[rowindex].Cells[1].Text.ToString();
        //    txtPosition.Text = grdReferalDetails.Rows[rowindex].Cells[2].Text.ToString();
        //    txtTelePhone.Text = grdReferalDetails.Rows[rowindex].Cells[3].Text.ToString();
        //    txtRelatioship.Text = grdReferalDetails.Rows[rowindex].Cells[4].Text.ToString();
        //    txtYearsKnown.Text = grdReferalDetails.Rows[rowindex].Cells[6].Text.ToString();
        //    btnKnowPersonDetails.Text = "Update Details";
        //}

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            long Recid = Convert.ToInt64(grdReferalDetails.DataKeys[rowindex]["id"].ToString());
            ExtraInfoforAdmissionBO objBo = new ExtraInfoforAdmissionBO();
            objBo.Id = Recid;
            new ExtraInfoforAdmissionBAL().DeleteRecord(objBo);
            BindProfessonalReferalGrid();
            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
            return;
        }

        protected void btnPrivious_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            Response.Redirect("~/Admin/Student/EducationDetails?" + strEndQueryString);
        }
        protected void rblcourtoflaw_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblcourtoflaw.SelectedValue == "YES".Trim())
            {
                txtcourtoflaw.Visible = true;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + txtcourtoflaw.ClientID + "').focus();", true);
            }
            else
            {
                txtcourtoflaw.Visible = false;
                rblaccommodation.Focus();
                rblaccommodation.SelectedValue = "Own";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "focus", "document.getElementById('" + rblaccommodation.ClientID + "').focus();", true);
            }
        }

        //protected void other_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        protected void chk1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            foreach (ListItem item in chk1.Items)
            {
                if (item.Selected)
                {
                    message += item.Text.ToLower();
                    if (message.Contains("other"))
                    {
                        CLRemarks.Visible = true;
                    }
                    else
                    {
                        CLRemarks.Visible = false;
                    }
                }
                else
                {
                    CLRemarks.Visible = false;
                }
            }
        }
    }
}