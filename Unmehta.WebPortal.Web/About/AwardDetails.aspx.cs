using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.About
{
    public partial class AwardDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strAwards;
        public static string strAwardTab;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strAwards = GetAwards();
            }
        }

        private string GetAwards()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strVisionMission = new StringBuilder();
            StringBuilder strTab = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var TabList = objBlogCategoryMasterRepository.GetAllAwardMasterHomeByLangId(languageId).Where(x=> !string.IsNullOrWhiteSpace(x.AlbumType)).Select(x=> x.AlbumType).Distinct().ToList();
                int i = 1;
                foreach(var tab in TabList)
                {
                    strTab.Append("");
                    strTab.Append("<li>");
                    strTab.Append("	<a href='#tab_" + i + "' class='" + (i == 1 ? "active" : "") + "' data-toggle='pill'>" + tab + "");
                    strTab.Append("	</a>");
                    strTab.Append("</li>");
                    strVisionMission.Append("<div class='tab-pane "+(i==1? "active" : "")+"' id='tab_"+i+"'>");
                    strVisionMission.Append("	<div class='section-main-title'>");
                    strVisionMission.Append("		<h3>"+ tab + "</h3>");
                    strVisionMission.Append("    </div>");

                    var dataList = objBlogCategoryMasterRepository.GetAllAwardMasterHomeByLangId(languageId).Where(x => x.AlbumType == tab).ToList();
                    //dataList = dataList.OrderByDescending(x => DateTime.ParseExact(x.AwardMonthYear, "MMM yyyy", CultureInfo.InvariantCulture)).ToList();
                    dataList = dataList.OrderBy(x => x.LevelId).ToList();

                    if (dataList.Count() > 0)
                    {
                        foreach (var row in dataList)
                        {
                            string filePath = string.IsNullOrWhiteSpace(row.ImageDesc) ? "" : ResolveUrl(row.ImageDesc);

                            strVisionMission.Append("<div class='card author-widget clearfix'>");
                            strVisionMission.Append("	<div class='card-body'>");
                            strVisionMission.Append("		<div class='about-author'>");
                            strVisionMission.Append("			<div class='row'>");
                            strVisionMission.Append("				<div class='col-lg-4'>");
                            strVisionMission.Append("					<div class='about-author'>");
                            strVisionMission.Append("						<div class='author-img-wrap'>");
                            strVisionMission.Append("							<div class='gallery-box-layout1'>");
                            strVisionMission.Append("								<img src='" + filePath + "' alt='Feature' class='img-fluid'>");
                            strVisionMission.Append("								<div class='item-icon'>");
                            strVisionMission.Append("									<a href='" + filePath + "' data-fancybox='gallery' class='lightbox-image'>");
                            strVisionMission.Append("										<i class='fas fa-search-plus'></i>");
                            strVisionMission.Append("									</a>");
                            strVisionMission.Append("								</div>");
                            strVisionMission.Append("							</div>");
                            strVisionMission.Append("						</div>");
                            strVisionMission.Append("					</div>");
                            strVisionMission.Append("				</div>");
                            strVisionMission.Append("				<div class='col-lg-8'>");
                            strVisionMission.Append("					<div class='author-details_1'>");
                            strVisionMission.Append("						<h3>" + row.AlbumName + "</h3>");
                            strVisionMission.Append("						<p class='mb-0'>" + HttpUtility.HtmlDecode(row.AwardShortDesc) + "</p>");
                            strVisionMission.Append("					</div>");
                            strVisionMission.Append("				</div>");
                            strVisionMission.Append("			</div>");
                            strVisionMission.Append("		</div>");
                            strVisionMission.Append("	</div>");
                            strVisionMission.Append("</div>");
                        }
                    }
                    strVisionMission.Append("</div>");
                    i++;
                }

            }
            strAwardTab = strTab.ToString();
            return strVisionMission.ToString();
        }


        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AwardDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AwardDetails").FirstOrDefault();
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