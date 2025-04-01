using BAL;
using BAL.Admission;
using BO;
using BO.Admission;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class StudentApplyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
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
                    }
                    BindPageViewData();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void BindPageViewData()
        {
            try
            {
                using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(objStudentAdvertisementBAL.GetAll()).Where(x => x.IsVisible == true).ToList();
                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            ddlCourceList.DataSource = data;
                            ddlCourceList.DataTextField = "Name";
                            ddlCourceList.DataValueField = "Id";
                            ddlCourceList.DataBind();
                            ddlCourceList.Items.Insert(0, new ListItem("Select Cource", ""));
                        }
                    }

                }
                BindGridViewData();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        private void BindGridViewData()
        {
            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                List<StudentAllRegistratedBO> data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentforadmin());

                if (ddlCourceList.SelectedIndex > 0)
                {
                    data = data.Where(x => x.CourseId == Convert.ToInt32(ddlCourceList.SelectedValue)).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    data = data.Where(x => x.FirstName.Contains(txtSearch.Text) || x.MiddleName.Contains(txtSearch.Text) || x.LastName.Contains(txtSearch.Text) || x.Mobile.Contains(txtSearch.Text) || x.Email.Contains(txtSearch.Text) || x.RegistrationId.Contains(txtSearch.Text)).ToList();
                }
                //if (data != null)
                {
                    lblCount.Text = "Total No Of Candidate : " + Convert.ToString(data.Count);
                    gView.DataSourceID = string.Empty;
                    gView.DataSource = data;
                    gView.DataBind();
                }
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridViewData();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                ddlCourceList.SelectedIndex = 0;
                BindGridViewData();

            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
        }

        protected void lnkRejectApplication_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);



            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();

                objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Reject");
                BindPageViewData();
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Reject");
            }
        }

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strStatus = e.Row.Cells[11].Text;
                LinkButton lnkRejectApplication = (LinkButton)e.Row.Cells[14].FindControl("lnkRejectApplication");
                LinkButton lnkAcceptApplication = (LinkButton)e.Row.Cells[14].FindControl("lnkAcceptApplication");
                if (strStatus == "Pending")
                {
                    lnkRejectApplication.Visible = false;
                    lnkAcceptApplication.Visible = false;
                }
                else
                {
                    lnkRejectApplication.Visible = false;
                    lnkAcceptApplication.Visible = false;
                }
            }
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAcceptApplication_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);



            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();

                objCandidateDetailsRepository.UpdateStudentRegistrationStatus((long)data.StudentId, (long)data.CourseId, "Accept");
                BindPageViewData();
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                SendConfirmationMail(data.Email, FullName, data.CourseName, data.RegistrationId, "Accept");
            }
        }

        public void SendConfirmationMail(string email, string Name, string Course, string AppNo, string AppStatus)
        {
            string emailId = email;
            string FullName = Name;
            string CourseName = Course;
            string ApplicationNumber = AppNo;
            string Status = AppStatus;
            CareerMasterBAL objBAL = new CareerMasterBAL();
            DataSet ds = new DataSet();
            ds = objBAL.MailCreditials();
            if (ds != null && ds.Tables.Count > 0)
            {
                string strError = "";
                string strBody = "";

                
                {

                    strBody = "Dear " + FullName.ToString() + "," +
                              "<br/><br/> Course Name : " + CourseName +
                              "<br/><br/> Application Confirmation Number : " + ApplicationNumber +
                              "<br/><br/> Your Application Confirmation Status :<b> " + Status + "</b>." +
                              "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                              "<br/><br/> Regards," +
                              "<br/><br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                }
                if (Functions.SendEmail(emailId, "Your Application Confirmation Status For The Course Of " + CourseName, strBody, out strError, true, null))
                {

                }
                else
                {
                    ErrorLogger.ERROR("Print Page Erroe =>" + strError, "", this);
                }

            }
        }

        protected void lnkPriview_Click(object sender, EventArgs e)
        {

            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudent()).Where(x => x.Id == Id).FirstOrDefault();
                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId));
                //Response.Redirect("~/Admin/Student/StudentDetails?" + strEndQueryString);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Admin/Student/StudentDetails?" + strEndQueryString + "','_newtab');", true);

            }
        }

        protected void ibtn_Download_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);
            string studentName = gView.DataKeys[rowIndex].Values["FirstName"].ToString();
            StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL();
            DataTable dtFileList = objCandidateDetailsRepository.GetAllStudentDocumentByStudentCourseId(intStudentId.ToString(), intCourseId.ToString());

            using (ZipFile zip = new ZipFile())
            {
                int inFileCount = 0;
                    string TempPath = ConfigurationManager.AppSettings["ZIPTempPath"].ToString();
                foreach (DataRow row in dtFileList.Rows)
                {
                    string strMappath = Server.MapPath(row["Path"].ToString());
                    string strFileType = Server.MapPath(TempPath + "/" + row["Folder"].ToString());
                    string strName = row["Path"].ToString();
                    string strNameF = row["Name"].ToString();
                    string strNameExt = Path.GetExtension(row["Path"].ToString());
                    string strNamefile = Path.GetFileNameWithoutExtension(row["Path"].ToString());

                    if (File.Exists((strMappath)))
                    {
                        if (strFileType == "Photograph" || strFileType == "Signature" || strFileType == "DateofBirthProof")
                        {
                            var filname = zip.AddFile(strMappath);//Zip file inside filename  
                            filname.FileName = row["Folder"].ToString() + "/" + strNameF + "_" + strNamefile + strNameExt;
                        }
                        else
                        {
                            var filname = zip.AddFile(strMappath, row["Folder"].ToString());//Zip file inside filename  
                            filname.FileName = row["Folder"].ToString() + "/" + strNameF + "_" + strNamefile + strNameExt;
                        }
                    }
                    //if (File.Exists((strMappath)))
                    //{

                    //    string strName = Path.GetFileNameWithoutExtension(strMappath);
                    //    string strNameExt = Path.GetExtension(strMappath);
                    //    var filname = zip.AddFile(strMappath);//Zip file inside filename  
                    //    filname.FileName = inFileCount + strNameExt;

                    //}
                    inFileCount++;
                }

                string fileName = studentName + "_" + intStudentId + ".zip";
                if (!Directory.Exists(Server.MapPath(TempPath)))
                {
                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(Server.MapPath(TempPath));
                }


                // Create the path and file name to check for duplicates.
                var pathToCheck1 = TempPath + fileName;

                // Create a temporary file name to use for checking duplicates.
                //var tempfileName1 = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.
                if (File.Exists(Server.MapPath(pathToCheck1)))
                {
                    File.Delete(Server.MapPath(pathToCheck1));
                }

                zip.Save(Server.MapPath(pathToCheck1));//location and name for creating zip file  
                if (File.Exists(Server.MapPath(pathToCheck1)))
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = "application/zip";
                    Response.BinaryWrite(File.ReadAllBytes(Server.MapPath(TempPath) + fileName));
                    Response.End();
                }
                else
                {
                    Functions.MessagePopup(this, "File Not Found.", PopupMessageType.error);
                }
            }
        }
    }
}