using BAL;
using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using BAL.Admission;
using BO.Admission;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Ionic.Zip;
using System.Configuration;
using BO;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using System.Transactions;

namespace Unmehta.WebPortal.Web.Admin.Student
{

    public partial class StudentMasterData : System.Web.UI.Page
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

        protected void btn_Exceldata_ServerClick(object sender, EventArgs e)
        {
            StudentReportBAL objBAL = new StudentReportBAL();
            DataSet ds = new DataSet();
            ds = objBAL.GetStudentMasterData();
            DataTable dt = ds.Tables[0];
            //Added these line Start
            // dt.Columns[0].ColumnName = "ID";

            //Added these line End

            dt.Columns.Remove("id");
            //dt.Columns.RemoveAt(4);
            //dt.AcceptChanges();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StudentMasterData.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
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
                            //ddlCourceList.DataSource = data;
                            //ddlCourceList.DataTextField = "Name";
                            //ddlCourceList.DataValueField = "Id";
                            //ddlCourceList.DataBind();
                            //ddlCourceList.Items.Insert(0, new ListItem("Select Cource", ""));

                            lstCourceList.DataSource = data;
                            lstCourceList.DataTextField = "Name";
                            lstCourceList.DataValueField = "Id";
                            lstCourceList.DataBind();
                        }
                    }
                }
                StudentReportBAL objBal = new StudentReportBAL();
                //ddlapplication.DataSource = objBal.SelectApplicationStatus();
                //ddlapplication.DataTextField = "WorkFlowStatus";
                //ddlapplication.DataValueField = "WorkFlowStatusID";
                //ddlapplication.DataBind();
                //ddlapplication.Items.Insert(0, new ListItem("Select Status", ""));

                lstApplicationStatus.DataSource = objBal.SelectApplicationStatus();
                lstApplicationStatus.DataTextField = "WorkFlowStatus";
                lstApplicationStatus.DataValueField = "WorkFlowStatusID";
                lstApplicationStatus.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void BindGridview()
        {
            StudentReportBAL objBAL = new StudentReportBAL();
            DataSet ds = new DataSet();
            string selectedValues = string.Empty;
            foreach (ListItem li in lstApplicationStatus.Items)
            {
                if (li.Selected == true)
                {
                    // selectedValues += li.Value + ",";

                    selectedValues += "'" + li.Value + "',";
                }
            }
            selectedValues = selectedValues.TrimEnd(',');
            string selectedCourse = string.Empty;
            foreach (ListItem li in lstCourceList.Items)
            {
                if (li.Selected == true)
                {
                    selectedCourse += "'" + li.Value + "',";
                }
            }
            selectedCourse = selectedCourse.TrimEnd(',');

            //DateTime startdate = DateTime.Now;
            //DateTime Enddate = DateTime.Now;

            string startdate = "";
            string Enddate = "";

            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    startdate = txtStartDate.Text;
                    Enddate = txtEndDate.Text;
                }
                else
                {
                    Functions.MessagePopup(this, "Select Enddate", PopupMessageType.warning);
                    txtEndDate.Focus();
                    return;
                }

            }
            string Transactionstartdate = "";
            string TransactionEnddate = "";
            if (!string.IsNullOrEmpty(txtTransactionStartDate.Text))
            {
                if (!string.IsNullOrEmpty(txtTransactionEndDate.Text))
                {
                    startdate = txtTransactionStartDate.Text;
                    Enddate = txtTransactionEndDate.Text;
                }
                else
                {
                    Functions.MessagePopup(this, "Select Transaction End date", PopupMessageType.warning);
                    txtTransactionEndDate.Focus();
                    return;
                }

            }
            //else
            //{
            //    Functions.MessagePopup(this, "Select Startdate", PopupMessageType.warning);
            //    txtStartDate.Focus();
            //    return;
            //}
            ds = objBAL.SelectStatusWiseRecord(selectedValues,txtPaymentStatus.Text, selectedCourse, startdate, Enddate, Transactionstartdate, TransactionEnddate);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                Session["GridDataset"] = ds.Tables[0];
                btnexport.Visible = true;
                lblCount.Text = "Total No Of Candidate : " + Convert.ToString(ds.Tables[0].Rows.Count);
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
            }
            else
            {
                lblCount.Text = "Total No Of Candidate : " + Convert.ToString(ds.Tables[0].Rows.Count);
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
                btnexport.Visible = false;
            }
        }

        protected void btnexport_ServerClick(object sender, EventArgs e)
        {

            if (Session["GridDataset"] != null)
            {
                DataTable dt = (DataTable)Session["GridDataset"];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Remove("id");
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=StudentStatusWiseData.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }

            }
            else
            {
                btnexport.Visible = false;
            }

        }

        protected void ibtn_Download_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);
            string studentName = gView.DataKeys[rowIndex].Values["FirstName"].ToString();
            string RegistrationId = gView.DataKeys[rowIndex].Values["RegistrationId"].ToString();
            StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL();
            DataTable dtFileList = objCandidateDetailsRepository.GetAllStudentDocumentByStudentCourseId(intStudentId.ToString(), intCourseId.ToString());
            try
            {

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

                    string fileName = RegistrationId + ".zip";
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
            catch(Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);

            }
        }

        protected void lnkPriview_Click(object sender, EventArgs e)
        {

            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);


            using (StudentRegistrationDetailsBAL objCandidateDetailsRepository = new StudentRegistrationDetailsBAL())
            {
                StudentAllRegistratedBO data = Functions.ToListof<StudentAllRegistratedBO>(objCandidateDetailsRepository.GetAllRegistratedStudentPayment(intStudentId)).Where(x => x.CourseId == intCourseId).FirstOrDefault();

                if(data != null)
                {


                    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + data.StudentId + "|CourseId=" + data.CourseId + "|CourseName=" + data.CourseName + "|RegistrationId=" + data.RegistrationId+ "|AdminPAgeSec"));
                    string strpath = ResolveUrl("~/Admin/Admission/PrintApplication?" + strEndQueryString);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "  window.open('" + strpath + "','_newtab');  ", true);

                }
                else
                {
                    Functions.MessagePopup(this, "File not Final  Approve.", PopupMessageType.error);

                }
            }
        }

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int id = Convert.ToInt32(gView.DataKeys[e.Row.RowIndex].Values[0]);

                int intStudentId = Convert.ToInt32(gView.DataKeys[e.Row.RowIndex].Values["StudentId"]);
                int intCourseId = Convert.ToInt32(gView.DataKeys[e.Row.RowIndex].Values["CourseId"]);

                LinkButton lnkPriview = e.Row.FindControl("lnkPriview") as LinkButton;
                LinkButton lnkSendPaymentSMS = e.Row.FindControl("lnkSendPaymentSMS") as LinkButton;
                LinkButton lnkSendCorrectionSMS = e.Row.FindControl("lnkSendCorrectionSMS") as LinkButton;

                StudentCourseBAL objBAL = new StudentCourseBAL();
                DataSet dsas = objBAL.Student_CheckFinalSubmitFlag(intStudentId, intCourseId);
                StudentRegistrationDetailsBAL objstudBal = new StudentRegistrationDetailsBAL();
                System.Data.DataTable dsCheckpayment = objstudBal.GetAllRegistratedStudentPaymentByCourse(intStudentId, intCourseId);
                if (dsas.Tables != null && dsas.Tables[0].Rows.Count > 0)
                {
                    lnkSendPaymentSMS.Visible = false;
                    lnkSendCorrectionSMS.Visible = false;
                    if (dsCheckpayment.Rows.Count > 0)
                    {
                        if (dsCheckpayment.Rows[0]["PaymentStatus"].ToString().ToUpper().Contains("SUCCESS"))
                        {
                            lnkPriview.Visible = true;
                        }
                        else
                        {
                            if (dsCheckpayment.Rows[0]["PaymentStatus"].ToString().Contains("Retry") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().Contains("In Process") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().Contains("Pending") || dsCheckpayment.Rows[0]["PaymentStatus"].ToString().Contains("Failed"))
                            {
                                lnkPriview.Visible = false;
                                lnkSendPaymentSMS.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lnkPriview.Visible = false;
                    }

                    if (dsas.Tables[0].Rows[0]["ApplicationStatus"].ToString() == "3")
                    {
                        lnkSendCorrectionSMS.Visible = true;
                    }
                }
            }
        }

        protected void lnkSendCorrectionSMS_Click(object sender, EventArgs e)
        {

            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);
            string PresentPhoneM = Convert.ToString(gView.DataKeys[rowIndex].Values["PresentPhoneM"]);
          
                    if (!string.IsNullOrWhiteSpace(PresentPhoneM))
                    {
                        using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                        {
                            var sms = configDetailsRepository.GetSMSTemplateByName("Correction SMS");
                            if (sms != null)
                            {
                                string strLink = ConfigDetailsValue.StudentLogInLink;
                                string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{Link}}", ConfigDetailsValue.StudentLogInLink)));
                                string strTemplateId = sms.SMSTemplateId;
                                string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                                string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                                string senderid2 = ConfigDetailsValue.senderid2;
                                string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                                string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, PresentPhoneM, strMessage, SMSAPI2, 0);
                        
                                Functions.MessagePopup(this, "Send Correction SMS to "+ PresentPhoneM, PopupMessageType.success);
                            }
                        }
                    }

            btn.Focus();
        }

        protected void lnkSendPaymentSMS_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int intStudentId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["StudentId"]);
            int intCourseId = Convert.ToInt32(gView.DataKeys[rowIndex].Values["CourseId"]);
            string PresentPhoneM = Convert.ToString(gView.DataKeys[rowIndex].Values["PresentPhoneM"]);

            if (!string.IsNullOrWhiteSpace(PresentPhoneM))
            {
                using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString()))
                {
                    var sms = configDetailsRepository.GetSMSTemplateByName("Payment of Fees SMS");
                    if (sms != null)
                    {
                        string strLink = ConfigDetailsValue.StudentLogInLink;
                        string strMessage = (HttpUtility.UrlEncode(sms.SMSContent.Replace("{{Link}}", ConfigDetailsValue.StudentLogInLink)));
                        string strTemplateId = sms.SMSTemplateId;
                        string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                        string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                        string senderid2 = ConfigDetailsValue.senderid2;
                        string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                        string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, PresentPhoneM, strMessage, SMSAPI2, 0);
                        Functions.MessagePopup(this, "Send Payment of Fees SMS to " + PresentPhoneM, PopupMessageType.success);
                    }
                }
            }
            btn.Focus();

        }

    }

}