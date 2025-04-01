using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using System.Net.Mail;
using System.Net;
using iTextSharp.text.pdf;
using BAL;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class PrintApplication : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SessionWrapper.UserDetails.UserName))
                {
                    Response.Redirect("~/Recruitment/Careers");
                }
                //string strEndQueryString = "Sm9iSWQ9MXxSZWdJZD0yMDIxMDYzMDA3MzU0OHxDYW5kSWQ9MQ%3d%3d";
                string strEndQueryString = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strEndQueryString))
                {
                    Response.Redirect("~/Recruitment/Careers.aspx");
                }

                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                string[] strQuery = strQueryString.Split('|').ToArray();
                if (strQuery.Count() == 3)
                {
                    strJobId = strQuery[0].ToString().Replace("JobId=", "");
                    strRegId = strQuery[1].ToString().Replace("RegId=", "");
                    strCandId = strQuery[2].ToString().Replace("CandId=", "");
                }
                else
                {
                    Response.Redirect("~/Recruitment/Careers.aspx");
                }

                long canId = Convert.ToInt64(strCandId);

                DataSet ds = GetData(canId);

                DataTable dtBasicDetail = ds.Tables[0];
                DataTable dtEducationDetail = ds.Tables[1];
                DataTable dtExperianceDetail = ds.Tables[2];
                DataTable dtLanguageDetail = ds.Tables[3];
                DataTable dtTotalExperianceDetail = ds.Tables[4];

                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["PhotographPath"].ToString()))
                {
                    ImgAppi.Src = ResolveUrl(dtBasicDetail.Rows[0]["PhotographPath"].ToString().Replace(" ", "_").Replace("/", "\\"));
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["SignaturePath"].ToString()))
                {
                    imgsignappi.Src = ResolveUrl(dtBasicDetail.Rows[0]["SignaturePath"].ToString().Replace(" ", "_").Replace("/", "\\"));
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["RegisrationId"].ToString()))
                {
                    lblApplicationNo.InnerHtml = dtBasicDetail.Rows[0]["RegisrationId"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["JobName"].ToString()))
                {
                    Label1.InnerHtml = dtBasicDetail.Rows[0]["JobName"].ToString();
                    lblpostre.InnerHtml = dtBasicDetail.Rows[0]["JobName"].ToString();
                }
                string lastName = "";
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["LastName"].ToString()))
                {
                    lastName = " " + dtBasicDetail.Rows[0]["LastName"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["FirstName"].ToString()))
                {
                    firstname.InnerHtml = dtBasicDetail.Rows[0]["FirstName"].ToString() + lastName;
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["MiddleName"].ToString()))
                {
                    Ffirstname.InnerHtml = dtBasicDetail.Rows[0]["MiddleName"].ToString() + lastName;
                }

                LblFnameRe.InnerHtml = dtBasicDetail.Rows[0]["FirstName"].ToString() + " " + Ffirstname.InnerHtml;
                lBLfNAMESIGN.InnerHtml = dtBasicDetail.Rows[0]["FirstName"].ToString() + " " + Ffirstname.InnerHtml;
                lblDate1.InnerHtml = Convert.ToDateTime(dtBasicDetail.Rows[0]["CreateDate"].ToString()).ToString("dd/MM/yyyy");

                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["PresentAddress"].ToString()))
                {
                    address.InnerHtml = dtBasicDetail.Rows[0]["PresentAddress"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["ParmenentPhoneM"].ToString()))
                {
                    LblMobNo.InnerHtml = dtBasicDetail.Rows[0]["ParmenentPhoneM"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["EmailId"].ToString()))
                {
                    lblmail.InnerHtml = dtBasicDetail.Rows[0]["EmailId"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["DateOFBirth"].ToString()))
                {
                    LblDob.InnerHtml = Convert.ToDateTime(dtBasicDetail.Rows[0]["DateOFBirth"].ToString()).ToString("dd/MM/yyyy");
                }
                if (!string.IsNullOrWhiteSpace(dtBasicDetail.Rows[0]["Gender"].ToString()))
                {
                    if (dtBasicDetail.Rows[0]["Gender"].ToString() == "1")
                        lblgen.InnerHtml = "MALE";
                    else if (dtBasicDetail.Rows[0]["Gender"].ToString() == "2")
                        lblgen.InnerHtml = "FEMALE";
                    else
                        lblgen.InnerHtml = "OTHERS";
                }
                if (!string.IsNullOrEmpty(dtBasicDetail.Rows[0]["CasteCategory"].ToString()))
                {
                    LblCast.InnerHtml = dtBasicDetail.Rows[0]["CasteCategory"].ToString();
                }                

                string lngHtml = "<br /><table runat='server' id='tblLanguage' cellspacing='0' cellpadding='5' border='1' width='100%' style='font-size: 9pt;font-family:Arial;'>" +
                    "<tr>" +
                    "<td colspan='4'>" +
                    "<b>Language known:</b>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td class='style12'>" +
                    "<b> Language</b>" +
                    "</td>" +
                    "<td class='style12'>" +
                    "<b> Speak </b>" +
                    "</td>" +
                    "<td class='style33'>" +
                    "<b> Read </b>" +
                    "</td>" +
                    "<td>" +
                    "<b>  Write</b>" +
                    "</td>" +
                    "</tr>";

                if (dtLanguageDetail.Rows.Count > 0)
                {
                    int a;
                    var loopTo = dtLanguageDetail.Rows.Count - 1;
                    for (a = 0; a <= loopTo; a++)
                    {
                       lngHtml = lngHtml +
                            "<tr>" +
                    "<td class='style12' style='white-space:nowrap !important;'>" +
                    dtLanguageDetail.Rows[a]["LanguageName"].ToString() +
                    "</td>" +
                    "<td class='style12'>" +
                    dtLanguageDetail.Rows[a]["lngSpeak"].ToString() +
                    "</td>" +
                    "<td class='style33'>" +
                    dtLanguageDetail.Rows[a]["lngRead"].ToString() +
                    "</td>" +
                    "<td>" +
                    dtLanguageDetail.Rows[a]["lngWrite"].ToString() +
                    "</td>" +
                    "</tr>";
                    }
                    lngHtml = lngHtml + "</table>";
                }
                tblLanguage.InnerHtml = lngHtml;


                string tes = "<br /><table runat='server' id='tableedu' cellspacing='0' cellpadding='5' border='1' width='100%' style='font-size: 9pt;font-family:Arial;'>" +
                    "<tr>" +
                    "<td colspan='5'>" +
                    "<b>Educational Qualification</b>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td class='style12'>" +
                    "<b> Qualification</b>" +
                    "</td>" +
                    "<td class='style12'>" +
                    "<b> Degree </b>" +
                    "</td>" +
                    "<td class='style33'>" +
                    "<b> University </b>" +
                    "</td>" +
                    "<td style='white-space:nowrap !important;'>" +
                    "<b>  Year of Passing </b>" +
                    "</td>" +
                    "<td class='style11'>" +
                    "<b>   Percentage </b>" +
                    "</td>" +
                    "</tr>";

                if (dtEducationDetail.Rows.Count > 0)
                {
                    int a;
                    var loopTo = dtEducationDetail.Rows.Count - 1;
                    for (a = 0; a <= loopTo; a++)
                    {
                        tes = tes +

                            "<tr>" +
                    "<td class='style12' style='white-space:nowrap !important;'>" +
                    dtEducationDetail.Rows[a]["EducationDetailName"].ToString() +
                    "</td>" +
                    "<td class='style12'>" +
                    dtEducationDetail.Rows[a]["DegreeName"].ToString() +
                    "</td>" +
                    "<td class='style33'>" +
                    dtEducationDetail.Rows[a]["ExamBody"].ToString() +
                    "</td>" +
                    "<td>" +
                    dtEducationDetail.Rows[a]["PassingMonthAndYear"].ToString() +
                    "</td>" +
                    "<td class='style11'>" +
                    dtEducationDetail.Rows[a]["PercentageOrPercentile"].ToString() +
                    "</td>" +
                    "</tr>";
                    }
                    tes = tes + "</table>";
                }
                tableedu.InnerHtml = tes;

                Divexp.Visible = false;
                if (dtExperianceDetail.Rows.Count > 0)
                {
                    tableexp.InnerHtml = "";
                    Divexp.Visible = true;
                    string Exp = " <br /><table runat='server' id='tableexp' cellspacing='0' cellpadding='5' border='1' width='100%' style='font-size: 9pt;font-family:Arial;'>" +
                        "<tr>" +
                        "<td colspan='10'>" + "<b>Experience : </b>" +
                          dtTotalExperianceDetail.Rows[0]["EXPERIENCE"].ToString() +
                        "</td>" +
                        "<tr>" +
                        "<td class='style12'>" +
                        "<b> JobType</b>" +
                        "</td>" +
                        "<td class='style33' style='word-wrap: normal;'>" +
                        "<b> From DT</b>" +
                        "</td>" +
                        "<td>" +
                        "<b> To DT</b>" +
                        "</td>" +
                        "<td class='style12'>" +
                        "<b> Name of Post </b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>   Organization Name</b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>   Organization Address </b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>  ReportingAuthority </b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>   Job Description </b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>  Monthly Salary </b>" +
                        "</td>" +
                        "<td class='style11'>" +
                        "<b>  Department Name </b>" +
                        "</td>" +
                        "</tr>";
                    int a;
                    var loopTo1 = dtExperianceDetail.Rows.Count - 1;
                    for (a = 0; a <= loopTo1; a++)
                        Exp = Exp +
                         "<tr>" +
                        "<td class='style12'>" +
                        dtExperianceDetail.Rows[a]["JobTypeName"].ToString() +
                        "</td>" +
                        "<td class='style33' style='word-wrap: normal;'>" +
                        dtExperianceDetail.Rows[a]["FromDate"].ToString() +
                        "</td>" +
                        "<td>" +
                        dtExperianceDetail.Rows[a]["ToDate"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["CurrentJobPost"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["EmployerName"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["EmployerAddress"].ToString() + 
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["ReportingAuthority"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["JobDescription"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["SalaryPerMonth"].ToString() +
                        "</td>" +
                        "<td class='style11'>" +
                        dtExperianceDetail.Rows[a]["DepartmentName"].ToString() +
                        "</td>" +
                        "</tr>";
                    Exp = Exp + "</table>";
                    tableexp.InnerHtml = Exp;
                    Divexp.Visible = true;
                }
                else
                {
                    Divexp.Visible = false;
                }
                if (SessionWrapper.sendConfirmationMail == 0)
                {
                    SendConfirmationMail();
                    SessionWrapper.sendConfirmationMail = 1;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR("Print Page Erroe =>" + ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }            
        }
        private DataSet GetData(long candId)
        {
            string connString = Functions.strSqlConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand();
            connection.Open();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAllDetailForPrintForm";
            command.Parameters.AddWithValue("@candidateId", candId);
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.Redirect("~/Recruitment/Careers");
        }
        public void SendConfirmationMail()
        {
            StringWriter iSW = new StringWriter();
            StringWriter iSW1 = new StringWriter();
            StringWriter iSW2 = new StringWriter();
            StringWriter iSW3 = new StringWriter();
            HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
            HtmlTextWriter iHTW1 = new HtmlTextWriter(iSW1);
            HtmlTextWriter iHTW2 = new HtmlTextWriter(iSW2);
            HtmlTextWriter iHTW3 = new HtmlTextWriter(iSW3);
            string mainPhotpath = ImgAppi.Src;
            string mainSignpath = imgsignappi.Src;

            if (File.Exists(Server.MapPath(mainPhotpath)))
            {
                ImgAppi.Src = Server.MapPath(mainPhotpath);
            }
            else
            {
                ImgAppi.Src = "";
            }
            if (File.Exists(Server.MapPath(mainSignpath)))
            {
                imgsignappi.Src = Server.MapPath(mainSignpath);
            }
            else
            {
                imgsignappi.Src = "";
            }

            headerDiv.RenderControl(iHTW);
            basicInfoDiv.RenderControl(iHTW1);
            experienceDiv.RenderControl(iHTW2);
            finalDiv.RenderControl(iHTW3);

            string strData = iSW.GetStringBuilder().ToString();
            string strData1 = iSW1.GetStringBuilder().ToString();
            string strData2 = iSW2.GetStringBuilder().ToString();
            string strData3 = iSW3.GetStringBuilder().ToString();
            ImgAppi.Src = ResolveUrl("~/" + mainPhotpath);
            imgsignappi.Src = ResolveUrl("~/" + mainSignpath);

            strData = strData.Replace(ResolveUrl("~/Hospital/assets/img/logo.png"), Server.MapPath(@"~/Hospital/assets/img/logo.png"));

            StringReader sr = new StringReader(strData);
            StringReader sr1 = new StringReader(strData1);
            StringReader sr2 = new StringReader(strData2);
            StringReader sr3 = new StringReader(strData3);

            Document pdfDoc = new Document(PageSize.A4);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                htmlparser.Parse(sr1);
                pdfDoc.NewPage();
                htmlparser.Parse(sr2);
                htmlparser.Parse(sr3);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                string emailId = lblmail.InnerHtml;
                CareerMasterBAL objBAL = new CareerMasterBAL();
                DataSet ds = new DataSet();
                ds = objBAL.MailCreditials();
                if (ds != null && ds.Tables.Count > 0)
                {
                    string strError="";
                    string strBody = "Dear " + LblFnameRe.InnerHtml +
                                       "<br/><br/> Thank you for applying through U. N. Mehta Institute of Cardiology & Research Centre." +
                                       "<br/> We are pleased to attach herewith your application form in PDF." +
                                       "<br/><br/> Advertisement : " + lblpostre.InnerHtml +
                                       "<br/><br/> Application Confirmation Number : " + lblApplicationNo.InnerHtml +
                                       "<br/><br/> You can view this application, save it for your future reference or get it printed if required." +
                                       "<br/><br/> This is an auto-generated mail. Please do not reply to this mail." +
                                       "<br/><br/> Regards," +
                                       "<br/> U. N. Mehta Institute of Cardiology & Research Centre.";
                    List<Attachment> lstAttachment = new List<Attachment>();
                    Stream stream = new MemoryStream(bytes);
                    string fileName = lblApplicationNo.InnerHtml + ".pdf";
                    lstAttachment.Add(new Attachment(stream, fileName, "application/pdf"));
                    if (Functions.SendEmail(emailId, "Application Confirmation For The Post Of " + lblpostre.InnerHtml, strBody,out strError, true, lstAttachment))
                    {
                        strData = strData.Replace(Server.MapPath(@"~/Hospital/assets/img/logo.png"), ResolveUrl("~/Hospital/assets/img/logo.png"));
                        strData = strData.Replace(Server.MapPath(mainPhotpath), ResolveUrl(mainPhotpath));
                        strData3 = strData3.Replace(Server.MapPath(mainSignpath), ResolveUrl(mainSignpath));
                        printableArea.InnerHtml = strData + strData1 + strData2 + strData3;
                    }
                    else
                    {
                        ErrorLogger.ERROR("Print Page Erroe =>" + strError,"", this);
                    }

                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}