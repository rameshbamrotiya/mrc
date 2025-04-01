using BAL;
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

namespace Unmehta.WebPortal.Web
{
    public partial class SchemaDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strDescription;
        protected void Page_Load(object sender, EventArgs e)
        {
            strHeaderImage = GetHeaderImage();           

            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);
            StringBuilder strResearch = new StringBuilder();
            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                    dvDesc.InnerHtml = strResearch.ToString();
                }
            }
            else
            {
                dvDesc.InnerHtml = GetSchema();
            }
        }

        private string GetSchema()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);

                if (dataListAward != null)
                {
                    LableData: if (dataListAward.Count > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {
                            index++;
                            //if (index <= 6)
                            {
                                string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.Schemebanner) ? "" : row.Schemebanner);
                                string strURL = ResolveUrl(("~/Schema?" + Functions.Base64Encode(row.Id.ToString())));
                                string strDesc = HttpUtility.HtmlDecode(row.Description);
                                strAwardsAndAchievements.Append("");
                                //strAwardsAndAchievements.Append("<div class='col-lg-4 col-sm-6'>                                                                                        ");
                                //strAwardsAndAchievements.Append("   <div class='lgx-single-service wow  flipInY animated' data-wow-delay='600ms'                                        ");
                                //strAwardsAndAchievements.Append("       data-wow-duration='1500ms'>                                                                                     ");
                                //strAwardsAndAchievements.Append("       <figure>                                                                                                        ");
                                //strAwardsAndAchievements.Append("           <a class='service-img' href='#'>                                                                            ");
                                //strAwardsAndAchievements.Append("               <img src='" + strimagePath + "' alt='Service' /></a>                                                     ");
                                //strAwardsAndAchievements.Append("           <figcaption>                                                                                                ");
                                //strAwardsAndAchievements.Append("               <div class='link-area'>                                                                                 ");
                                //strAwardsAndAchievements.Append("                   <a href='#'>                                                                                        ");
                                //strAwardsAndAchievements.Append("                       <img src='" + ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png") + "' alt='link'></a>        ");
                                //strAwardsAndAchievements.Append("               </div>                                                                                                  ");
                                //strAwardsAndAchievements.Append("               <div class='service-info'>                                                                              ");
                                //strAwardsAndAchievements.Append("                   <h3 class='title'><a href='" + strURL + "'>" + row.SchemeName + "</a></h3>                           ");
                                //strAwardsAndAchievements.Append("                   <p>" + strDesc + "  </p>                                                                             ");
                                //strAwardsAndAchievements.Append("                   <a class='lgx-btn lgx-btn-white lgx-btn-sm' href='" + strURL + "'><span>Read More</span></a>         ");
                                //strAwardsAndAchievements.Append("                   <img src='" + ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png") + "' alt='service icon'>     ");
                                //strAwardsAndAchievements.Append("               </div>                                                                                                  ");
                                //strAwardsAndAchievements.Append("           </figcaption>                                                                                               ");
                                //strAwardsAndAchievements.Append("       </figure>                                                                                                       ");
                                //strAwardsAndAchievements.Append("   </div>                                                                                                              ");
                                //strAwardsAndAchievements.Append("</div>                                                                                                                 ");
                                strAwardsAndAchievements.Append("<div class='col-lg-3 col-md-6 mb-30'>                                  ");
                                strAwardsAndAchievements.Append("	<div class='degree-wrap'>                                           ");
                                strAwardsAndAchievements.Append("		<img src='" + strimagePath + "' alt=''>               ");
                                strAwardsAndAchievements.Append("		<div class='title-part'>                                        ");
                                strAwardsAndAchievements.Append("			<h4 class='title'>" + row.SchemeName + "</h4>                                     ");
                                strAwardsAndAchievements.Append("		</div>                                                          ");
                                strAwardsAndAchievements.Append("		<div class='content-part'>                                      ");
                                strAwardsAndAchievements.Append("			<h4 class='title'><a href='" + strURL + "'>" + row.SchemeName + "</a></h4>                             ");
                                strAwardsAndAchievements.Append("			<div class='btn-part'>                                      ");
                                strAwardsAndAchievements.Append("				<a href='" + strURL + "'>Read More</a>                               ");
                                strAwardsAndAchievements.Append("			</div>                                                      ");
                                strAwardsAndAchievements.Append("		</div>                                                          ");
                                strAwardsAndAchievements.Append("	</div>                                                              ");
                                strAwardsAndAchievements.Append("</div>                                                                 ");
                            }
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SchemaDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SchemaDetails").FirstOrDefault();
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