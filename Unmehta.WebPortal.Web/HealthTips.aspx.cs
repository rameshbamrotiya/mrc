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
    public partial class HealthTips : System.Web.UI.Page
    {
        public static string strHeaderImage;

        public static string strHealthTips;

        public static string strQuickLink;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strHealthTips = GetHealthTips();
                strQuickLink = Functions.CreateQuickLink("Cares", "HealthTips");
            }
        }

        private string GetHealthTips()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strHealthTips = new StringBuilder();

            strHealthTips.Append("");
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllHealthTipsMaster(languageId).ToList();

                LableData:

                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {
                        string strURL = ResolveUrl(("~/HealthTipDetails?" + Functions.Base64Encode(row.id.ToString())));
                        string strImage = ResolveUrl((row.Imgpath.ToString()));
                        string strImageIcon = ResolveUrl(("~/Hospital/assets/img/department/Cardiology/DrJayeshPrajapatiHOD.png"));

                        strHealthTips.Append("<div class='col-md-6 col-lg-4 col-sm-12'>");
                        strHealthTips.Append("	<!-- Blog Post -->");
                        strHealthTips.Append("	<div class='blog grid-blog'>");
                        strHealthTips.Append("		<div class='blog-image'>");
                        strHealthTips.Append("			<a href='"+ strURL + "'><img class='img-fluid' src='"+ strImage + "' alt='Post Image'></a>");
                        strHealthTips.Append("		</div>");
                        strHealthTips.Append("		<div class='blog-content'>");
                        strHealthTips.Append("			<ul class='entry-meta meta-item'>");
                        strHealthTips.Append("				<li>");
                        strHealthTips.Append("					<div class='post-author'>");
                        strHealthTips.Append("						<a href='" + strURL + "'>");
                        strHealthTips.Append("							<span>"+ row.DoctorName + "</span></a>");
                        strHealthTips.Append("					</div>");
                        strHealthTips.Append("				</li>");
                        strHealthTips.Append("				<li><i class='far fa-clock'></i> "+row.Date +"</li>");
                        strHealthTips.Append("			</ul>");
                        strHealthTips.Append("			<h3 class='blog-title'><a href='" + strURL + "'>" + row.Title + "</a></h3>");
                        strHealthTips.Append("			<p class='mb-0'>"+ row.ShortDesc + "</p>");
                        strHealthTips.Append("		</div>");
                        strHealthTips.Append("	</div>");
                        strHealthTips.Append("	<!-- /Blog Post -->");
                        strHealthTips.Append("</div>");
                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllHealthTipsMaster(languageId).ToList();
                        goto LableData;
                    }
                }

            }
            return strHealthTips.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "HealthTips").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "HealthTips").FirstOrDefault();
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