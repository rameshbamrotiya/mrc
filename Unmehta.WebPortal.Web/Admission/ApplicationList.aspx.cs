using BAL;
using BAL.Admission;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Admission
{
    public partial class ApplicationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.StudentRegistration.Username == null)
                {
                    Response.Redirect("~/Admission/", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                Response.Redirect("~/Admission/", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (!IsPostBack)
            {
                GetGridView();
            }
        }

        private void GetGridView()
        {
            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                List<StudentAllRegistratedBO> data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id));
                gView.DataSourceID = string.Empty;
                gView.DataSource = data.Where(x=> x.StudentId==SessionWrapper.StudentRegistration.Id).ToList();
                gView.DataBind();
            }
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id)).Where(x => x.Id == Id).FirstOrDefault();


                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId));
                Response.Redirect("~/Admin/Admission/PrintApplication?" + strEndQueryString);
            }
        }

        protected void ibtn_Payment_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id)).Where(x => x.Id == Id).FirstOrDefault();

                if (data.PaymentStatus == "Pending" || data.PaymentStatus == "Retry" || data.PaymentStatus.Contains("Failed"))
                {
                    //if (objCandidateDetailsRepository.UpdateStudentRegistrationStatus(data.RegistrationId, "In Process"))
                    //{
                    //    //"StudentId=1|CourseId=1|CourseName=Blood Banking";
                    //    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|Email=" + data.Email + "|Mobile=" + data.Mobile));
                    //    Response.Redirect("~/Admin/Admission/Payment.aspx?" + strEndQueryString);
                    //}
                }
                else
                {
                    Functions.MessagePopup(this, "Payment Status is " + data.PaymentStatus, PopupMessageType.warning);
                }
            }
        }

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strStatus = e.Row.Cells[5].Text.Trim();
                string strPaymenStatus = e.Row.Cells[6].Text.Trim();
                LinkButton ibtn_Payment = (LinkButton)e.Row.Cells[1].FindControl("ibtn_Payment");
                LinkButton ibtn_View_Click = (LinkButton)e.Row.Cells[1].FindControl("lnkPriview");
                if ((strStatus == "Approve" || strStatus == "Retry")&& strPaymenStatus != "Success")
                {
                    ibtn_Payment.Visible = true;
                    ibtn_View_Click.Visible = true;
                }
                else if(strPaymenStatus== "Success")
                {
                    ibtn_Payment.Visible = false;
                    ibtn_View_Click.Visible = true;
                }
                else
                {
                    ibtn_Payment.Visible = false;
                    ibtn_View_Click.Visible = true;
                }

            }
        }

        protected void lnkPriview_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment((int)SessionWrapper.StudentRegistration.Id)).Where(x => x.Id == Id).FirstOrDefault();


                string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId));
                Response.Redirect("~/Admin/Admission/PrintApplication?" + strEndQueryString);
            }
        }
    }
}