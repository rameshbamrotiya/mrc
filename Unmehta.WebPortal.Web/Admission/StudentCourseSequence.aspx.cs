using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class StudentCourseSequence : System.Web.UI.Page
    {
        public static string strStudentId;
        public static string strCourseId;
        public static string strCourseName;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Admission/Course.aspx", false);
                    }
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));
                    string[] strQuery = strQueryString.Split('|').ToArray();
                    strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                    strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                    strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                    SessionWrapper.BasicDetailsFlag = 0;
                    SessionWrapper.sendConfirmationMailFlag = 0;
                    BindDropdownCourse();
                    BindGridCourseSelection();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void BindDropdownCourse()
        {
            StudentRegistrationDetailsBAL objBAL = new StudentRegistrationDetailsBAL();

            int Id = Convert.ToInt32(strStudentId);
            //int Id = 2;
            DataSet ds = new DataSet();
            ds = objBAL.StudentCourse(Id);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                ddlcoursename.DataSource = objBAL.StudentCourse(Id);
                ddlcoursename.DataValueField = "Id";
                ddlcoursename.DataTextField = "CourseName";
                ddlcoursename.DataBind();
                ddlcoursename.Items.Insert(0, new ListItem("Select", "-1"));
            }
            else
            {
                gView.DataSourceID = string.Empty;
                gView.DataBind();
            }

        }
        protected void BindGridCourseSelection()
        {
            StudentRegistrationDetailsBAL objBAL = new StudentRegistrationDetailsBAL();

            int Id = Convert.ToInt32(strStudentId);
            //int Id = 2;
            DataSet ds = new DataSet();
            ds = objBAL.StudentCourseSelection(Id);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                gView.DataSourceID = string.Empty;
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
                if (gView.PageIndex == 0)
                {
                    var firstRow = gView.Rows[0];
                    firstRow.FindControl("lnk_UP").Visible = false;
                    var lastrow = gView.Rows[gView.Rows.Count - 1];
                    lastrow.FindControl("lnk_Dwn").Visible = false;
                }
            }
            else
            {
                gView.DataSourceID = string.Empty;
                gView.DataBind();
            }
        }
        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int CourseId = Convert.ToInt32(ddlcoursename.SelectedValue.ToString());
                string user_id = "Admin";
                int StudentId = Convert.ToInt32(strStudentId);
                int CourseMasterId = Convert.ToInt32(strCourseId);
                //string user_id = "1";
                //int StudentId = 2;
                //int CourseMasterId = 1;
                if (new StudentRegistrationDetailsBAL().InsertRecordCourse(CourseId, StudentId, user_id, CourseMasterId))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    BindDropdownCourse();
                    BindGridCourseSelection();
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
                    BindGridCourseSelection();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btnFinalSubmit_ServerClick(object sender, EventArgs e)
        {
            //try
            //{
            //    ExtraInfoforAdmissionBAL objBAL = new ExtraInfoforAdmissionBAL();
            //    bool LanguageResult = objBAL.InsertFinalSubmit(Convert.ToInt32(strStudentId), 1);
            //    //string strStudentIdateId = HttpUtility.UrlEncode(Functions.Encrypt(SessionWrapper.StudentIdateId.ToString()));
            //    //Response.Redirect("~/PrintApplication.aspx?StudentIdateId=" + strStudentIdateId);
            //    SessionWrapper.sendConfirmationMail = 0;
            //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|CourseName=" + strCourseName));
            //    Response.Redirect("~/Admission/PrintApplication?" + strEndQueryString);
            //}
            //catch (Exception ex)
            //{
            //    ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            //}
        }
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    int StudentId = Convert.ToInt32(strStudentId);
                    int CourseMasterId = Convert.ToInt32(strCourseId);
                    //int StudentId = 2;
                    //int CourseMasterId = 1;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        int Id = bytID;
                        new StudentRegistrationDetailsBAL().DeleteRecord(Id, StudentId, CourseMasterId);
                        BindGridCourseSelection();
                        BindDropdownCourse();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
                else
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                    if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                    {
                        string col_parent_id = commandArgs[0];
                        string col_menu_level = commandArgs[1];
                        string MasterCourseId = commandArgs[2];
                        string cmd = commandArgs[3];
                        switch (cmd)
                        {
                            case "up":
                                SetPageOrder(cmd, col_menu_level, col_parent_id, MasterCourseId);
                                break;
                            case "down":
                                SetPageOrder(cmd, col_menu_level, col_parent_id, MasterCourseId);
                                break;
                        }
                        BindGridCourseSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id, string MasterCourseId)
        {
            if (new StudentRegistrationDetailsBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id, MasterCourseId))
            {
                Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);
            }
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            int StudentId = Convert.ToInt32(strStudentId);
            int CourseMasterId = Convert.ToInt32(strCourseId);
            int fromno = Convert.ToInt32(txtFrom.Text);
            int tono = Convert.ToInt32(txtTo.Text);
            //int StudentId = 2;
            //int CourseMasterId = 1;
            StudentRegistrationDetailsBAL objBAL = new StudentRegistrationDetailsBAL();
            DataSet ds = new DataSet();
            ds = objBAL.StudentCourseSelection(StudentId);
            int Totalrow = ds.Tables[0].Rows.Count;
            if (fromno != tono)
            {
                if (fromno <= Totalrow && tono <= Totalrow)
                {
                    if (new StudentRegistrationDetailsBAL().UpdateStudentSelection(fromno, tono, StudentId, CourseMasterId))
                    {
                        Functions.MessagePopup(this, "Record Updated successfully.", PopupMessageType.success);
                        BindDropdownCourse();
                        BindGridCourseSelection();
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
                        BindGridCourseSelection();
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please enter valid number.", PopupMessageType.warning);
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please enter valid number.", PopupMessageType.warning);
            }
        }
    }
}