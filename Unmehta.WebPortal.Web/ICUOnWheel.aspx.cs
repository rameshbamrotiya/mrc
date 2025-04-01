using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class ICUOnWheel : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strPageSlider;
        public static string strPageMainDesc;
        public static string strListOfSubSection;
        public static string strListOfSubSectionDescription;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strPageSlider = GetPageSlider();
                strPageMainDesc = GetPageMainDesc();
                strListOfSubSection = GetListOfSubSection();
                strListOfSubSectionDescription = GetListOfSubSectionDescription();
            }
        }

        private string GetListOfSubSectionDescription()
        {
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objICUOnWheelRepository.GetAllICUOnWheelMaster();
                var dataListSub = objICUOnWheelRepository.GetAllICUOnWheelSubDetails();
                int index = 0;
                foreach (var row in dataListAward)
                {
                    strAwardsAndAchievements.Append("");
                    strAwardsAndAchievements.Append("<div class='tab-pane " + (index == 0 ? "active" : "") + "' id='tab_" + index + "'>");
                    strAwardsAndAchievements.Append("    <!-- About Details -->");
                    strAwardsAndAchievements.Append("    <div class='accordion-box'>");
                    strAwardsAndAchievements.Append("        <div class='title-box'>");
                    strAwardsAndAchievements.Append("            <h6>" + row.ICUName + "</h6>");
                    strAwardsAndAchievements.Append("        </div>");
                    strAwardsAndAchievements.Append("        <ul class='accordion-inner'>");

                    foreach (var rowInner in dataListSub.Where(x => x.MainId == row.Id).ToList())
                    {
                        strAwardsAndAchievements.Append("            <li class='accordion block'>");
                        strAwardsAndAchievements.Append("                <div class='acc-btn'>");
                        strAwardsAndAchievements.Append("                    <div class='icon-outer'></div>");
                        strAwardsAndAchievements.Append("                    <h6>"+ rowInner.SubTitle + "</h6>");
                        strAwardsAndAchievements.Append("                </div>");
                        strAwardsAndAchievements.Append("                <div class='acc-content'>");
                        if (string.IsNullOrWhiteSpace(rowInner.ImageName))
                        {

                            strAwardsAndAchievements.Append("                    <div class='text'>");
                            strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(rowInner.SubDescription)));
                            strAwardsAndAchievements.Append("                    </div>");
                        }
                        else
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(rowInner.ImageName) ? "" : rowInner.ImageName);
                            strAwardsAndAchievements.Append("                    <div class='row'>");
                            strAwardsAndAchievements.Append("                        <div class='col-lg-3'>");
                            strAwardsAndAchievements.Append("                            <div class='about-author'>");
                            strAwardsAndAchievements.Append("                                <div class='author-img-wrap'>");
                            strAwardsAndAchievements.Append("                                    <a href='#'>");
                            strAwardsAndAchievements.Append("                                        <img class='img-fluid' alt='' src='"+ strimagePath + "'></a>");
                            strAwardsAndAchievements.Append("                                </div>");
                            strAwardsAndAchievements.Append("                            </div>");
                            strAwardsAndAchievements.Append("                        </div>");
                            strAwardsAndAchievements.Append("                        <div class='col-lg-9'>");
                            strAwardsAndAchievements.Append("                            <div class='row'>");
                            strAwardsAndAchievements.Append("                                <div class='col-lg-12'>");

                            strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(rowInner.SubDescription)));
                            strAwardsAndAchievements.Append("                                </div>");
                            strAwardsAndAchievements.Append("                            </div>");
                            strAwardsAndAchievements.Append("                        </div>");
                            strAwardsAndAchievements.Append("                    </div>");
                        }
                        strAwardsAndAchievements.Append("                </div>");
                        strAwardsAndAchievements.Append("            </li>");
                    }

                    strAwardsAndAchievements.Append("        </ul>");
                    strAwardsAndAchievements.Append("    </div>");
                    strAwardsAndAchievements.Append("    <!-- /About Details -->");
                    strAwardsAndAchievements.Append("</div>");
                  


                    index++;
                }
            }

            return strAwardsAndAchievements.ToString();
        }

        private string GetListOfSubSection()
        {
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objICUOnWheelRepository.GetAllICUOnWheelMaster();
                int index = 0;
                foreach (var row in dataListAward)
                {
                    strAwardsAndAchievements.Append("");
                    strAwardsAndAchievements.Append("<li>");
                    strAwardsAndAchievements.Append("<a href='#tab_" + index + "' class='" + (index == 0 ? "active" : "") + "' data-toggle='pill'>" + row.ICUName + "</a>");
                    strAwardsAndAchievements.Append("</li>");

                    index++;
                }
            }

            return strAwardsAndAchievements.ToString();
        }

        private string GetPageMainDesc()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objICUOnWheelRepository.GetAllICUOnWheelMaster().FirstOrDefault();
                if(dataListAward!=null)
                {

                if (!string.IsNullOrWhiteSpace(dataListAward.ICUDetails))
                {
                    strAwardsAndAchievements.Append("");

                        var mainData = objICUOnWheelRepository.GetAllICUOnWheelMainDesc();
                        if (mainData != null)
                        {
                            strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(mainData.ICUOnWheelDesc)));
                        }
                        else
                        {
                            strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dataListAward.ICUDetails)));
                        }
                    }
                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetPageSlider()
        {

            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {

                var dataListAward = objICUOnWheelRepository.GetAllICUOnWheelImageDetails();

                if (dataListAward != null)
                {
                    LableData: if (dataListAward.Count > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImageName) ? "" : row.ImageName);
                            strAwardsAndAchievements.Append("");
                            strAwardsAndAchievements.Append("<div class='carousel-item " + (index == 0 ? "active" : "") + "'>");
                            strAwardsAndAchievements.Append("    <img class='d-block img-fluid' src='" + strimagePath + "' alt='First slide'>");
                            strAwardsAndAchievements.Append("    <div class='carousel-caption d-none d-md-block'>");
                            strAwardsAndAchievements.Append("        <h3></h3>");
                            strAwardsAndAchievements.Append("    </div>");
                            strAwardsAndAchievements.Append("</div>");
                            index++;
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objICUOnWheelRepository.GetAllICUOnWheelImageDetails();
                            goto LableData;
                        }
                    }

                }
                return strAwardsAndAchievements.ToString();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ICUOnWheel").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ICUOnWheel").FirstOrDefault();
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