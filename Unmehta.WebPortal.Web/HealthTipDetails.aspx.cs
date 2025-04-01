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
    public partial class HealthTipDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;

        public static string strHealthTips;
        public static string strHealthTipsDetail;

        public static string strQuickLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strHealthTips = GetHealthTips();

            }
        }

        private string GetHealthTips()
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            long id = 0;
            int languageId = Functions.LanguageId;

            StringBuilder strHealthTips = new StringBuilder();
            StringBuilder strHealthTipsDetails = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
            {
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
                            string strImage = ResolveUrl((row.InnerImgpath.ToString()));
                            string strListImage = ResolveUrl((row.Imgpath.ToString()));
                            string strImageIcon = ResolveUrl(("~/Hospital/assets/img/department/Cardiology/DrJayeshPrajapatiHOD.png"));


                            strHealthTipsDetails.Append("");
                            if(id==row.id)
                            {
                                string strDesc = HttpUtility.HtmlDecode(row.InnerDescription);
                                strHealthTipsDetails.Append("<div class='blog-image'>");
                                strHealthTipsDetails.Append("	<a href='#'><img src='" + strImage + "' class='img-fluid'></a>");
                                strHealthTipsDetails.Append("</div>");
                                strHealthTipsDetails.Append("<h3 class='blog-title'>" + row.Title + "</h3>");
                                strHealthTipsDetails.Append("<div class='blog-info clearfix'>");
                                strHealthTipsDetails.Append("	<div class='post-left'>");
                                strHealthTipsDetails.Append("		<ul>");
                                strHealthTipsDetails.Append("			<li>");
                                strHealthTipsDetails.Append("				<div class='post-author'>");
                                strHealthTipsDetails.Append("					<a href='#'> <span>"+row.DoctorName+"</span></a>");
                                strHealthTipsDetails.Append("				</div>");
                                strHealthTipsDetails.Append("			</li>");
                                strHealthTipsDetails.Append("			<li><i class='far fa-calendar'></i> " + row.Date + "</li>");
                                strHealthTipsDetails.Append("			<li><i class='fa fa-tags'></i>Health Tips</li>");
                                strHealthTipsDetails.Append("		</ul>");
                                strHealthTipsDetails.Append("	</div>");
                                strHealthTipsDetails.Append("</div>");
                                strHealthTipsDetails.Append("<div class='blog-content'>");
                                strHealthTipsDetails.Append(strDesc);
                                strHealthTipsDetails.Append("</div>");

                                string strUrl = Request.Url.AbsoluteUri;
                                string strSocial = "";
                                strSocial += "<li><a href='https://facebook.com/sharer.php?u=" + strUrl + "' title='Facebook' target='_blank'><i class='fab fa-facebook'></i></a></li>";
                                strSocial += "<li><a href='https://twitter.com/intent/tweet?url=" + strUrl + "&text=" + row.Title + "&via=UNMEHTA' title='Twitter' target='_blank'><i class='fab fa-twitter'></i></a></li>";
                                strSocial += "<li><a href='https://plus.google.com/share?url=" + strUrl + "' title='Google Plus' target='_blank'><i class='fab fa-google-plus'></i></a></li>";

                                socialShare.InnerHtml = strSocial;
                            }

                            if (i < 5)
                            {
                                strHealthTips.Append("<li>");
                                strHealthTips.Append("	<div class='post-thumb'>");
                                strHealthTips.Append("		<a href='" + strURL + "'>");
                                strHealthTips.Append("			<img class='img-fluid' src='" + strListImage + "' alt=''>");
                                strHealthTips.Append("		</a>");
                                strHealthTips.Append("	</div>");
                                strHealthTips.Append("	<div class='post-info'>");
                                strHealthTips.Append("		<h4>");
                                strHealthTips.Append("			<a href='" + strURL + "'>" + row.Title + "</a>");
                                strHealthTips.Append("		</h4>");
                                strHealthTips.Append("		<p> " + row.Date + "</p>");
                                strHealthTips.Append("	</div>");
                                strHealthTips.Append("</li>");

                            }
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
            }
            else
            {
                Response.Redirect("~/HealthTips");
            }

            strHealthTipsDetail = strHealthTipsDetails.ToString();

            return strHealthTips.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "HealthTipDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "HealthTipDetails").FirstOrDefault();
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