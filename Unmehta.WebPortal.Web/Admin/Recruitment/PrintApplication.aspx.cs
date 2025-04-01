using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class PrintApplication : System.Web.UI.Page
    {
        public static string strCandId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                
                strCandId = strQueryString.ToString().Replace("CandidateId=", "");

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
                        "<td colspan='9'>" + "<b>Experience : </b>" +
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
                        "</tr>";
                    Exp = Exp + "</table>";
                    tableexp.InnerHtml = Exp;
                    Divexp.Visible = true;
                }
                else
                {
                    Divexp.Visible = false;
                }
            }
        }

        private DataSet GetData(long canId)
        {
            string connString = Functions.strSqlConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand();
            connection.Open();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAllDetailForPrintForm";
            command.Parameters.AddWithValue("@candidateId", canId);
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        protected void btnLogOut_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Recruitment/AllCandidateDetails");
        }
    }
}