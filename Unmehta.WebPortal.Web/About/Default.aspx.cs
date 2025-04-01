using BAL;
using BO;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class AboutUs : System.Web.UI.Page
    {
        public static string PageName = "";
        public static string strAboutUsDescription = "";
        public static string RightHeadingTitle = "";
        public static string strMain = "";
        public static string strHeaderImage;
        public static long inMainId;


        protected void Page_Load(object sender, EventArgs e)
        {
            inMainId = 0;


            StringBuilder strResearch = new StringBuilder();


            int languageId = Functions.LanguageId;

            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").Replace("About/Default", "About/").ToString();
            //rowURl = rowURl.Substring(1);
            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            ErrorLogger.ERROR(rowURl+" rowURl About/Us Page IsDisabledTranslate" + IsDisabledTranslate, "languageId "+ languageId, this);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    strResearch.Append("<div class='col-md-12'>");
                    Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                    strResearch.Append("</div>");
                    strMain = strResearch.ToString();
                }
            }
            else
            {
                strResearch.Append("");

                strResearch.Append("<div class='col-lg-8'>                                                                                                              ");
                strResearch.Append("    <div class='headingtitle'>                                                                                                      ");
                strResearch.Append("			<h4 class='mb-0' >"+PageName+"</h4>                                                                   ");
                strResearch.Append("			<img src='" + ResolveUrl("~/Hospital/assets/img/Icon_team.png") + "' alt='line' class='med_bottompadder20_4' />            ");
                strResearch.Append("	</div>                                                                                                                          ");
                strResearch.Append("    <div class='inner-column'>                                                                                                      ");
                strResearch.Append("        <div class='sec-title' >                                                                         ");
                strResearch.Append("                                " + GetDescription() + "                                                                                                    ");
                strResearch.Append("        </div>                                                                                                                      ");
                strResearch.Append("                                                                                                                                    ");
                strResearch.Append("    </div>                                                                                                                          ");
                strResearch.Append("</div>                                                                                                                              ");
                strResearch.Append("                                                                                                                                    ");
                strResearch.Append("<div class='col-lg-4'>                                                                                                              ");
                strResearch.Append("    <div class='section-header_1 pb-10'>                                                                                            ");
                strResearch.Append("        <div class='headingtitle'>                                                                                                  ");
                strResearch.Append("            <h4 class='mb-0' >" + RightHeadingTitle + "</h4>                                                         ");
                strResearch.Append("            <img src='" + ResolveUrl("~/Hospital/assets/img/Icon_team.png") + "' alt='line' class='med_bottompadder20_4'>             ");
                strResearch.Append("        </div>                                                                                                                      ");
                strResearch.Append("        <div class='load-more text-center mb-0'>                                                                                    ");
                strResearch.Append("            <a class='readon' href='" + ResolveUrl("~/About/AwardDetails") + "'>View More</a>                                         ");
                strResearch.Append("        </div>                                                                                                                      ");
                strResearch.Append("    </div>                                                                                                                          ");
                strResearch.Append("    <!-- Content Column -->                                                                                                         ");
                strResearch.Append("    <div class='video-slider owl-theme owl-carousel owl-loaded' >       " + GetAwardList() + "                                  ");
                strResearch.Append("    </div>                                                                                                                         ");
                strResearch.Append("</div>                                                                                                                              ");


                strMain = strResearch.ToString();
            }
            //dvAbout.InnerHtml = GetDescription();
            //ulAward.InnerHtml = GetAwardList();
            //dvExecutive.InnerHtml = GetExcutive();
            strHeaderImage = GetHeaderImage();


        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/").FirstOrDefault();

                if (dataMain != null)
                {
                LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                        //dvPageName.InnerText = dataMain.Name;
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        //private string GetExcutive()
        //{
        //    int languageId = Functions.LanguageId;
        //    StringBuilder strVisionMission = new StringBuilder();
        //    strVisionMission.Append("");
        //    using (IAboutUsMasterRepository objBlogCategoryMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
        //    {
        //        var dataList = objBlogCategoryMasterRepository.GetAlllongAboutUsDesignationMaster(inMainId,languageId).ToList();

        //        LableData: if (dataList.Count() > 0)
        //        {
        //            foreach (var row in dataList)
        //            {
        //                string filePath = ResolveUrl(row.PhotoPath);
        //                strVisionMission.Append("<div class='content-box'>");
        //                strVisionMission.Append("	<div class='author-info'>");
        //                strVisionMission.Append("		<figure class='author-image'><img src='" + filePath + "' alt=''></figure>");
        //                strVisionMission.Append("		<h4>" + row.DesignationName + "</h4>");
        //                strVisionMission.Append("		<span class='designation'>" + row.DesName + "</span>");
        //                strVisionMission.Append("	</div>");
        //                strVisionMission.Append("	<div class='text'>");
        //                strVisionMission.Append("		<p>'" + row.Message + "'</p>");
        //                strVisionMission.Append("	</div>");
        //                strVisionMission.Append("</div>");

        //            }
        //        }
        //        else
        //        {
        //            languageId = 1;
        //            dataList = objBlogCategoryMasterRepository.GetAlllongAboutUsDesignationMaster(inMainId, languageId).ToList();
        //            if (languageId != 1)
        //            {
        //                goto LableData;
        //            }
        //        }
        //    }


        //    return strVisionMission.ToString();
        //}

        private string GetAwardList()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strVisionMission = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllAwardMasterHomeByLangId(languageId).ToList();
                //dataList = dataList.OrderByDescending(x => DateTime.ParseExact(x.AwardMonthYear, "MMM yyyy", CultureInfo.InvariantCulture)).ToList();
                dataList = dataList.OrderBy(x => x.LevelId).ToList();

                if (dataList.Count() > 0)
                {
                    foreach (var row in dataList)
                    {
                        string filePath = string.IsNullOrWhiteSpace(row.ImageDesc) ? "" : ResolveUrl(row.ImageDesc);
                        strVisionMission.Append("<div class='projects-item card-overlay-two'>");
                        strVisionMission.Append("	<div class='gallery-box-layout1'>");
                        strVisionMission.Append("		<div class='single-item'>");
                        strVisionMission.Append("			<img src='" + filePath + "' alt='" + row.AlbumName + "' class='img-fluid'>");
                        strVisionMission.Append("		</div>");
                        strVisionMission.Append("		<div class='item-icon'>");
                        strVisionMission.Append("			<a href='" + filePath + "' data-fancybox='gallery' class='lightbox-image'>");
                        strVisionMission.Append("				<i class='fas fa-search-plus'></i>");
                        strVisionMission.Append("			</a>");
                        strVisionMission.Append("		</div>");
                        strVisionMission.Append("	</div>");
                        strVisionMission.Append("</div>");

                        //                  strVisionMission.Append("<li>");
                        //strVisionMission.Append("	<div class='experience-user'>");
                        //                  strVisionMission.Append("		<div class='before-circle'></div>");
                        //                  strVisionMission.Append("	</div>");
                        //                  strVisionMission.Append("	<div class='experience-content'>");
                        //                  strVisionMission.Append("		<div class='timeline-content'>");
                        //                  strVisionMission.Append("			<p class='exp-year'>"+row.AwardMonthYear+"</p>");
                        //                  strVisionMission.Append("			<h4 class='exp-title'>"+row.AlbumName + "</h4>");
                        //                  strVisionMission.Append("			<p>"+ row.AwardShortDesc + "</p>");
                        //                  strVisionMission.Append("		</div>");
                        //                  strVisionMission.Append("	</div>");
                        //                  strVisionMission.Append("</li>");
                    }
                }

            }
            return strVisionMission.ToString();
        }

        private string GetDescription()
        {
            StringBuilder strVisionMission = new StringBuilder();
            int languageId = Functions.LanguageId;

            using (IAboutUsMasterRepository objBlogCategoryMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAlllongAboutUsMaster(languageId).FirstOrDefault();

            LableData: if (dataList != null)
                {
                    inMainId = (long)dataList.Id;
                    {
                        PageName = dataList.HeadingTitle;
                        RightHeadingTitle = dataList.RightSideHeadingTitle;
                        strVisionMission.Append(HttpUtility.HtmlDecode(dataList.AboutUsDescription));
                    }
                }
                else
                {
                    languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAlllongAboutUsMaster(languageId).FirstOrDefault();
                    if (languageId != 1)
                    {
                        goto LableData;
                    }
                }

            }
            return strVisionMission.ToString();
        }
    }
}