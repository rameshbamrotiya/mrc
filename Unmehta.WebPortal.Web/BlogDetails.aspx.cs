using System;
using System.Collections.Generic;
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
    public partial class BlogDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strBlogDetails;
        public static string strType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(string.IsNullOrWhiteSpace(Request.QueryString.ToString()))
                {
                    Response.Redirect("~/Blogs");
                }
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

                strType = "";
                strHeaderImage = GetHeaderImage();
                strBlogDetails = GetBlogDetails();
            }
        }

        private string GetBlogDetails()
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

            string[] strData = queryString.Split('|');
            if(strData.Length>0)
            {

                using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
                {
                    int languageId = Functions.LanguageId;
                    LableData:
                    var dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).OrderByDescending(x => x.BlogDate).Where(x => x.Id.ToString() == strData[0] && x.TypeDetail==strData[1]).FirstOrDefault();
                    if (dataList != null)
                    {
                        if (!string.IsNullOrWhiteSpace(dataList.ImagePath))
                        {
                            imgData.Src = ResolveUrl(dataList.ImagePath);
                        }
                        strType = dataList.TypeDetail;
                        blogName.InnerText = dataList.BlogName;
                        lblType.InnerText = dataList.TypeDetail;
                        blogContent.InnerHtml = HttpUtility.HtmlDecode(dataList.Description);
                        lblBlogDate.InnerText = dataList.BlogDate.ToString("dd MMM yyyy");
                        string strUrl = Request.Url.AbsoluteUri;
                        string strSocial = "";
                        strSocial += "<li><a href='https://facebook.com/sharer.php?u=" + strUrl + "' title='Facebook' target='_blank'><i class='fab fa-facebook'></i></a></li>";
                        strSocial += "<li><a href='https://twitter.com/intent/tweet?url=" + strUrl + "&text=" + dataList.BlogName + "&via=UNMEHTA' title='Twitter'  target='_blank'><i class='fab fa-twitter'></i></a></li>";
                        strSocial += "<li><a href='https://plus.google.com/share?url=" + strUrl + "' title='Google Plus'><i class='fab fa-google-plus' target='_blank'></i></a></li>";
                        socialShare.InnerHtml = strSocial;
                    }
                    StringBuilder strBlogs = new StringBuilder();

                    var dataLists = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).Where(x => x.IsVisible == true).OrderByDescending(x => x.BlogDate).ToList();

                    if (dataLists.Count() > 0)
                    {
                        int i = 1;
                        foreach (var row in dataLists)
                        {
                            if (i > 4)
                            {
                                break;
                            }
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                            string strURL = ResolveUrl(("~/BlogDetails?" + Functions.Base64Encode(row.Id.ToString() + "|" + row.TypeDetail)));

                            strBlogs.Append("");

                            string strBlogName = row.BlogName;
                            if (row.BlogName.Length > 40)
                            {
                                strBlogName = row.BlogName.Remove(40);
                            }

                            strBlogs.Append("<li>");
                            strBlogs.Append("	<div class='post-thumb'>");
                            strBlogs.Append("		<a href='" + strURL + "'>");
                            strBlogs.Append("			<img class='img-fluid' src='" + strimagePath + "' alt=''>");
                            strBlogs.Append("		</a>");
                            strBlogs.Append("	</div>");
                            strBlogs.Append("	<div class='post-info'>");
                            strBlogs.Append("		<h4>");
                            strBlogs.Append("			<a href='" + strURL + "'>" + strBlogName + "..</a>");
                            strBlogs.Append("		</h4>");
                            strBlogs.Append("		<p>" + row.BlogDate.ToString("MMMM dd, yyyy") + "</p>");
                            strBlogs.Append("	</div>");
                            strBlogs.Append("</li>");

                            i++;
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataLists = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).OrderByDescending(x => x.Id).ToList();
                            goto LableData;
                        }
                    }
                    LatestBlogList.InnerHtml = strBlogs.ToString();
                }

            }
            return "";
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "BlogDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "BlogDetails").FirstOrDefault();
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