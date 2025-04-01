using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class AcademicMedical : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strAcademicTab;
        public static string strAcademicTabDetails;
        public static long pageId;
        public static bool AlumniVisible;
        public static bool AlumniRedirect;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                AlumniRedirect = false;

                if (!long.TryParse(queryString, out pageId))
                {
                    if (queryString == "AlumniDetails")
                    {
                        AlumniRedirect = true;
                    }
                    else
                    {
                        Response.Redirect("~/AcademicMedicalMain", false);
                    }
                }


                AcademicsDescriptionMasterBO objbo = new AcademicsDescriptionMasterBO();
                objbo.Language_id = Functions.LanguageId;

                DataSet ds = new AcademicsDescriptionMasterBAL().SelectAcademicsDescriptionDetailsByLanguage(objbo);
                if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        AlumniVisible = Convert.ToBoolean(ds.Tables[0].Rows[0]["AlumniVisible"]);
                    }
                }

                strHeaderImage = GetHeaderImage();
                strAcademicTab = GetTabList();
                strAcademicTabDetails = GetTabDetailList();
            }
        }

        private string GetTabList()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objAcc.GetAllAcademicMedical(languageId);

                if (dataListAward != null)
                {
                    LableData:
                    if (dataListAward.Count > 0)
                    {
                        int index = 0;

                        {
                            foreach (var row in dataListAward)
                            {
                                index++;
                                string strURL = ResolveUrl(("~/AcademicMedical?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()))));
                                strAwardsAndAchievements.Append("");
                                strAwardsAndAchievements.Append("<li>");
                                strAwardsAndAchievements.Append("    <a href='" + strURL + "' class='" + (row.Id == pageId ? "active" : "") + "' >" + row.AcademicsName);
                                strAwardsAndAchievements.Append("    </a>");
                                strAwardsAndAchievements.Append("</li>");
                                objAcc.GetAcademicMedicalMasterDoctorDetails(row.Id, languageId);
                            }
                        }
                        if (AlumniVisible)
                        {
                            string strURL = ResolveUrl(("~/AcademicMedical?" + HttpUtility.UrlEncode(Functions.Base64Encode("AlumniDetails"))));
                            strAwardsAndAchievements.Append("<li>");
                            strAwardsAndAchievements.Append("    <a href='" + strURL + "' class='" + (AlumniRedirect ? "active" : "") + "'  > Alumni Student");
                            strAwardsAndAchievements.Append("    </a>");
                            strAwardsAndAchievements.Append("</li>");
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objAcc.GetAllAcademicMedical(languageId);
                            goto LableData;
                        }
                    }
                }

                return strAwardsAndAchievements.ToString();
            }
        }

        private string GetTabDetailList()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                var row = objAcc.GetAllAcademicMedical(languageId).Where(x => x.Id == pageId).FirstOrDefault();
                LableData:
                // /Admin/FilesCMS/GovImagePath/22092021012803.png
                if (row != null)
                {
                    int index = 0;

                    if (!AlumniRedirect)
                    {
                        strAwardsAndAchievements.Append("");
                        strAwardsAndAchievements.Append("<div class='tab-pane " + (index == 0 ? "active" : "") + "' id='tab_" + row.Id + "'>");
                        strAwardsAndAchievements.Append("    <div class='section-main-title'>");
                        strAwardsAndAchievements.Append("        <h3>" + row.AcademicsName + "</h3>");
                        strAwardsAndAchievements.Append("    </div>");

                        strAwardsAndAchievements.Append("    <!-- About Details -->");
                        strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(row.AcademicsDescription));
                        //strAwardsAndAchievements.Append("    <div class='widget about-widget'>");
                        //strAwardsAndAchievements.Append("        <h4 class='widget-title'>");
                        //strAwardsAndAchievements.Append("            Course Information");
                        //strAwardsAndAchievements.Append("        </h4>");
                        //strAwardsAndAchievements.Append("        <p>");
                        //strAwardsAndAchievements.Append("            Lorem ipsum dolor sit amet,");
                        //strAwardsAndAchievements.Append("            consectetur adipiscing elit,");
                        //strAwardsAndAchievements.Append("        </p>");
                        //strAwardsAndAchievements.Append("    </div>");
                        //strAwardsAndAchievements.Append("    <!-- /About Details -->");
                        //strAwardsAndAchievements.Append("    <!-- About Details -->");
                        //strAwardsAndAchievements.Append("    <div class='widget about-widget'>");
                        //strAwardsAndAchievements.Append("        <h4 class='widget-title'>");
                        //strAwardsAndAchievements.Append("            Admission Process");
                        //strAwardsAndAchievements.Append("        </h4>");
                        //strAwardsAndAchievements.Append("        <p>");
                        //strAwardsAndAchievements.Append("            Lorem ipsum dolor sit amet,");
                        //strAwardsAndAchievements.Append("            consectetur adipiscing elit,");
                        //strAwardsAndAchievements.Append("        </p>");
                        //strAwardsAndAchievements.Append("    </div>");
                        strAwardsAndAchievements.Append("    <!-- /About Details -->");
                        strAwardsAndAchievements.Append("    <div class='main-part'>");
                        strAwardsAndAchievements.Append("        <h4 class='widget-title'>");
                        strAwardsAndAchievements.Append("            List of Current " + row.AcademicsName + " Resident Doctors");
                        strAwardsAndAchievements.Append("        </h4>");
                        strAwardsAndAchievements.Append("        <div class='row'>");
                        strAwardsAndAchievements.Append("            <div class='col-sm-12 col-md-12 col-lg-12' id='accordionyear2'>");

                        var yearList = objAcc.GetAcademicMedicalMasterDoctorDetails(row.Id, languageId).Select(x => (long)x.Year).Distinct().OrderBy(x => x);

                        foreach (var subYear in yearList)
                        {
                            strAwardsAndAchievements.Append("                <div class='accordion-item'>");
                            strAwardsAndAchievements.Append("                    <div class='accordion__header collapsed' data-toggle='collapse' data-target='#year" + subYear + "' aria-expanded='false'>");
                            strAwardsAndAchievements.Append("                        <a class='accordion__title' href='#'>" + subYear + " Year</a>");
                            strAwardsAndAchievements.Append("                    </div>");
                            strAwardsAndAchievements.Append("                    <div id='year" + subYear + "' class='collapse' data-parent='#accordionyear2'>");
                            strAwardsAndAchievements.Append("                        <div class='accordion__body'>");

                            strAwardsAndAchievements.Append("                            <div class='table-responsive'>");
                            strAwardsAndAchievements.Append("                                <table class='table table-hover table-center mb-0 maintable'>");
                            strAwardsAndAchievements.Append("                                    <thead>");
                            strAwardsAndAchievements.Append("                                        <tr>");
                            strAwardsAndAchievements.Append("                                            <th>Sr</th>");
                            strAwardsAndAchievements.Append("                                            <th>Name of Resident Doctors</th>");
                            //strAwardsAndAchievements.Append("                                            <th>Designation</th>");
                            var StudentList = objAcc.GetAcademicMedicalMasterDoctorDetails(row.Id, languageId).Where(x => x.Year == subYear).Distinct().OrderBy(x => x.Id);
                            if (StudentList.Where(x => !string.IsNullOrWhiteSpace(x.Photo)).Count() > 0)
                            {
                                strAwardsAndAchievements.Append("                                            <th>Photo</th>");
                            }
                            strAwardsAndAchievements.Append("                                        </tr>");
                            strAwardsAndAchievements.Append("                                    </thead>");
                            strAwardsAndAchievements.Append("                                    <tbody>");


                            long lgCount = 1;
                            foreach (var studentRow in StudentList)
                            {
                                string strURL = (string.IsNullOrWhiteSpace(studentRow.Photo) ? ResolveUrl("~/Admin/FilesCMS/GovImagePath/22092021012803.png") : ResolveUrl(studentRow.Photo));

                                strAwardsAndAchievements.Append("                                        <tr>");
                                strAwardsAndAchievements.Append("                                            <td>" + lgCount + "</td>");
                                strAwardsAndAchievements.Append("                                            <td>" + studentRow.StudentName + "</td>");
                                if (StudentList.Where(x => !string.IsNullOrWhiteSpace(x.Photo)).Count() > 0)
                                {
                                    //strAwardsAndAchievements.Append("                                            <td>"+ HttpUtility.HtmlDecode(studentRow.DegreeHead) + "</td>");
                                    strAwardsAndAchievements.Append("                                            <td> <img src='" + strURL + "' alt='' class='img-fluid' style='width:77px' ></td>");
                                }
                                strAwardsAndAchievements.Append("                                        </tr>");

                                lgCount++;
                            }


                            strAwardsAndAchievements.Append("                                    </tbody>");
                            strAwardsAndAchievements.Append("                                </table>");
                            strAwardsAndAchievements.Append("                            </div>");

                            strAwardsAndAchievements.Append("                        </div>");
                            strAwardsAndAchievements.Append("                    </div>");
                            strAwardsAndAchievements.Append("                </div>");
                        }

                        strAwardsAndAchievements.Append("            </div>");
                        strAwardsAndAchievements.Append("        </div>");
                        strAwardsAndAchievements.Append("    </div>");
                        strAwardsAndAchievements.Append("</div>");
                        index++;
                    }



                }
                else
                {


                    if (AlumniVisible)
                    {
                        AlumniStudentMasterBO objBO = new AlumniStudentMasterBO();
                        objBO.LanguageId = languageId;
                        AlumniStudentMasterBAL objBAL = new AlumniStudentMasterBAL();
                        DataSet ds = objBAL.SelectAlumniStudentDetailsFront(objBO);
                        DataTable dt = ds.Tables[0];
                        string strIconUrl = ResolveUrl("~/Hospital/assets/img/pdf.png");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            strAwardsAndAchievements.Append("");
                            strAwardsAndAchievements.Append("<div class='tab-pane " + (AlumniRedirect ? "active" : "") + "' id='tab_Alumni" + 0 + "'>");
                            strAwardsAndAchievements.Append("    <div class='section-main-title'>");
                            strAwardsAndAchievements.Append("        <h3> Alumni Student </h3>");
                            strAwardsAndAchievements.Append("    </div>");

                            strAwardsAndAchievements.Append("    <div class='main-part'>");
                            strAwardsAndAchievements.Append("        <h4 class='widget-title'>");
                            strAwardsAndAchievements.Append("            List of Current Alumni Student Resident Doctors");
                            strAwardsAndAchievements.Append("        </h4>");
                            strAwardsAndAchievements.Append("        <div class='row'>");
                            strAwardsAndAchievements.Append("            <div class='col-sm-12 col-md-12 col-lg-12' id='accordionyear2'>");
                            int lgCount = 1;
                            foreach (DataRow rows in dt.Rows)
                            {
                                strAwardsAndAchievements.Append("                <div class='accordion-item'>");
                                strAwardsAndAchievements.Append("                    <div class='accordion__header collapsed' data-toggle='collapse' data-target='#year" + rows["Year"].ToString() + "' aria-expanded='false'>");
                                strAwardsAndAchievements.Append("                        <a class='accordion__title' href='#'>" + rows["Year"].ToString() + "</a>");
                                strAwardsAndAchievements.Append("                    </div>");
                                strAwardsAndAchievements.Append("                    <div id='year" + rows["Year"].ToString() + "' class='collapse' data-parent='#accordionyear2'>");
                                strAwardsAndAchievements.Append("                        <div class='accordion__body'>");

                                strAwardsAndAchievements.Append("                            <div class='table-responsive'>");
                                strAwardsAndAchievements.Append("                                <table class='table table-hover table-center mb-0 maintable'>");
                                strAwardsAndAchievements.Append("                                    <thead>");
                                strAwardsAndAchievements.Append("                                        <tr>");
                                strAwardsAndAchievements.Append("                                            <th>Sr</th>");
                                strAwardsAndAchievements.Append("                                            <th>Name</th>");
                                strAwardsAndAchievements.Append("                                            <th>View File</th>");
                                strAwardsAndAchievements.Append("                                        </tr>");
                                strAwardsAndAchievements.Append("                                    </thead>");
                                strAwardsAndAchievements.Append("                                    <tbody>");

                                string strURL = ResolveUrl(rows["FileUploadPath"].ToString());

                                strAwardsAndAchievements.Append("                                        <tr>");
                                strAwardsAndAchievements.Append("                                            <td>" + lgCount + "</td>");
                                strAwardsAndAchievements.Append("                                            <td>" + rows["Title"].ToString() + "</td>");
                                if (!string.IsNullOrEmpty(rows["FileUploadPath"].ToString()))
                                {
                                    strAwardsAndAchievements.Append("                                            <td style='text-align: center !important'><a href =" + strURL + " target='_blank'><img src = " + strIconUrl + "></a></td>");
                                }
                                else
                                {
                                    strAwardsAndAchievements.Append("                                            <td style='text-align: center !important'><span>File Not Found</span></td>");
                                }

                                strAwardsAndAchievements.Append("                                        </tr>");
                                strAwardsAndAchievements.Append("                                    </tbody>");
                                strAwardsAndAchievements.Append("                                </table>");
                                strAwardsAndAchievements.Append("                            </div>");

                                strAwardsAndAchievements.Append("                        </div>");
                                strAwardsAndAchievements.Append("                    </div>");
                                strAwardsAndAchievements.Append("                </div>");
                                lgCount++;
                            }

                            strAwardsAndAchievements.Append("            </div>");
                            strAwardsAndAchievements.Append("        </div>");
                            strAwardsAndAchievements.Append("    </div>");
                            strAwardsAndAchievements.Append("</div>");
                        }
                    }
                    else
                    {

                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        row = objAcc.GetAllAcademicMedical(languageId).Where(x => x.Id == pageId).FirstOrDefault();
                            goto LableData;
                        }
                    }
                }
            }

            return strAwardsAndAchievements.ToString();

        }
        
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AcademicMedical").FirstOrDefault();

                if (dataMain != null)
                {
                    LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AcademicMedical").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
    }
}