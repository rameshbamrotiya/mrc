using BAL;
using BAL.Admission;
using BO;
using BO.Admission;
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
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class AcademicMedicalMain : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strAcademicTabDetails;
        public static string strParaMedicalDescription;
        public static bool AlumniVisible;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strAcademicTabDetails = GetTabDetailList();
                strParaMedicalDescription = GetParaMedicalDescription();
            }
        }

        private string GetTabDetailList(string strSearch = "")
        {

            StringBuilder strBoardOfDirector = new StringBuilder();
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objAcc.GetAllAcademicMedical(languageId);
                if (!string.IsNullOrWhiteSpace(strSearch))
                {
                    dataListAward = dataListAward.Where(x => x.AcademicsName.Contains(strSearch)).ToList();
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
                if (dataListAward != null)
                {
                    LableData:
                    if (dataListAward.Count() > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {

                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.FilePath);
                            string strURL = ResolveUrl(("~/AcademicMedical?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()))));
                            strBoardOfDirector.Append("");
                            strBoardOfDirector.Append("<div class='col-lg-4 col-md-6 mb-30'>");
                            strBoardOfDirector.Append("	<div class='courses-item'>");
                            strBoardOfDirector.Append("		<div class='img-part'>");
                            strBoardOfDirector.Append("			<img src='" + strimagePath + "' alt=''>");
                            strBoardOfDirector.Append("		</div>");
                            strBoardOfDirector.Append("		<div class='content-part'>");
                            //strBoardOfDirector.Append("			<span><a class='price' href='#'>" + row.CourseDuration + "</a></span>");
                            string CourseName = row.AcademicsName.ToString();
                            if (CourseName.Length > 48)
                            {
                                CourseName = CourseName.Remove(48) + "..";
                            }
                            strBoardOfDirector.Append("			<h3 class='title'><a href='" + strURL + "' title='" + row.AcademicsName + "'>" + CourseName + "</a></h3>");
                            strBoardOfDirector.Append("			<div class='bottom-part'>");
                            //strBoardOfDirector.Append("				<span class='user'><i class='fas fa-chair'></i> " + row.TotalSeat + " Seats</span>");
                            strBoardOfDirector.Append("				<div class='btn-part'>");
                            strBoardOfDirector.Append("					<a href='" + strURL + "'>Read More<i class='fas fa-angle-right'></i></a>");
                            strBoardOfDirector.Append("				</div>");
                            strBoardOfDirector.Append("			</div>");
                            strBoardOfDirector.Append("		</div>");
                            strBoardOfDirector.Append("	</div>");
                            strBoardOfDirector.Append("</div>");
                        }
                    }
                }
            }
            if(AlumniVisible)
            {
                string strURL = ResolveUrl(("~/AcademicMedical?" + HttpUtility.UrlEncode(Functions.Base64Encode("AlumniDetails"))));
                strBoardOfDirector.Append("");
                strBoardOfDirector.Append("<div class='col-lg-4 col-md-6 mb-30'>");
                strBoardOfDirector.Append("	<div class='courses-item'>");
                strBoardOfDirector.Append("		<div class='img-part'>");
                //strBoardOfDirector.Append("			<img src='" + strimagePath + "' alt=''>");
                strBoardOfDirector.Append("		</div>");
                strBoardOfDirector.Append("		<div class='content-part'>");
                //strBoardOfDirector.Append("			<span><a class='price' href='#'>" + row.CourseDuration + "</a></span>");
                string CourseName = "Alumni Student";
                if (CourseName.Length > 48)
                {
                    CourseName = CourseName.Remove(48) + "..";
                }
                strBoardOfDirector.Append("			<h3 class='title'><a href='" + strURL + "' title='" + CourseName + "'>" + CourseName + "</a></h3>");
                strBoardOfDirector.Append("			<div class='bottom-part'>");
                //strBoardOfDirector.Append("				<span class='user'><i class='fas fa-chair'></i> " + row.TotalSeat + " Seats</span>");
                strBoardOfDirector.Append("				<div class='btn-part'>");
                strBoardOfDirector.Append("					<a href='" + strURL + "'>Read More<i class='fas fa-angle-right'></i></a>");
                strBoardOfDirector.Append("				</div>");
                strBoardOfDirector.Append("			</div>");
                strBoardOfDirector.Append("		</div>");
                strBoardOfDirector.Append("	</div>");
                strBoardOfDirector.Append("</div>");
            }
            //using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            //{
            //    var dataOfUserName = objStudentAdvertisementBAL.GetAll();
            //    if (dataOfUserName != null)
            //    {
            //        List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);

            //        List<StudentSubCourseBO> dataSub = new List<StudentSubCourseBO>();

            //        foreach (var dataRom in data)
            //        {
            //            var GetSubAll = objStudentAdvertisementBAL.GetSubAll(dataRom.Id);
            //            if (GetSubAll != null)
            //            {
            //                dataSub.AddRange(Functions.ToListof<StudentSubCourseBO>(GetSubAll));
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(strSearch))
            //        {
            //            dataSub = dataSub.Where(x => x.CourseName.Contains(strSearch)).ToList();
            //        }

            //        if (dataSub.Count() > 0)
            //        {
            //            foreach (var row in dataSub)
            //            {
            //                string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
            //                string strURL = ResolveUrl(("~/ParaMedical?" + Functions.Base64Encode(row.Id.ToString())));
            //                strBoardOfDirector.Append("");
            //                strBoardOfDirector.Append("<div class='col-lg-4 col-md-6 mb-30'>");
            //                strBoardOfDirector.Append("	<div class='courses-item'>");
            //                strBoardOfDirector.Append("		<div class='img-part'>");
            //                strBoardOfDirector.Append("			<img src='" + strimagePath + "' alt=''>");
            //                strBoardOfDirector.Append("		</div>");
            //                strBoardOfDirector.Append("		<div class='content-part'>");
            //                strBoardOfDirector.Append("			<span><a class='price' href='#'>" + row.CourseDuration + "</a></span>");
            //                string CourseName = row.CourseName.ToString();
            //                if (CourseName.Length > 48)
            //                {
            //                    CourseName = CourseName.Remove(48) + "..";
            //                }
            //                strBoardOfDirector.Append("			<h3 class='title'><a href='" + strURL + "' title='" + row.CourseName + "'>" + CourseName + "</a></h3>");
            //                strBoardOfDirector.Append("			<div class='bottom-part'>");
            //                strBoardOfDirector.Append("				<span class='user'><i class='fas fa-chair'></i> " + row.TotalSeat + " Seats</span>");
            //                strBoardOfDirector.Append("				<div class='btn-part'>");
            //                strBoardOfDirector.Append("					<a href='" + strURL + "'>Read More<i class='fas fa-angle-right'></i></a>");
            //                strBoardOfDirector.Append("				</div>");
            //                strBoardOfDirector.Append("			</div>");
            //                strBoardOfDirector.Append("		</div>");
            //                strBoardOfDirector.Append("	</div>");
            //                strBoardOfDirector.Append("</div>");
            //            }
            //        }
            //        else
            //        {
            //            strBoardOfDirector.Clear();
            //            strBoardOfDirector.Append("");
            //            strBoardOfDirector.Append("<div class='col-lg-12 mb-30'><p>No Record Found</p></div>");
            //        }
            //    }
            //}
            return strBoardOfDirector.ToString();
        }
        private string GetParaMedicalDescription()
        {
            AcademicsDescriptionMasterBO objbo = new AcademicsDescriptionMasterBO();
            objbo.Language_id = Functions.LanguageId;
            StringBuilder strParaMedicalMainDescriptionCKeditor = new StringBuilder();

            DataSet ds = new AcademicsDescriptionMasterBAL().SelectAcademicsDescriptionDetailsByLanguage(objbo);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strParaMedicalMainDescriptionCKeditor.Append(HttpUtility.HtmlDecode(row["MedicalInnerDescription"].ToString()));
                        i++;
                    }
                }
            }
            return strParaMedicalMainDescriptionCKeditor.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AcademicMedicalMain").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AcademicMedicalMain").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strAcademicTabDetails = GetTabDetailList(txtSearch.Value);
        }
    }
}