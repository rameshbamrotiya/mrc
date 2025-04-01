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
    public partial class Blogs : System.Web.UI.Page
    {
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                BlogListDetails.InnerHtml = GetBlogs();
            }
        }

        private string GetBlogs(string strSearch="")
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).Where(x=> x.IsVisible==true).OrderByDescending(x => x.BlogDate).ToList();



                LableData:
                if(!string.IsNullOrWhiteSpace(strSearch))
                {
                    dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).Where(x => x.BlogName.Contains(strSearch)).OrderByDescending(x => x.BlogDate).ToList();
                }
                if(ddlType.SelectedIndex>0)
                {
                    dataList = dataList.Where(x => x.TypeDetail == ddlType.SelectedValue).ToList();
                }
                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {
                        string strBlogName = row.BlogName;
                        if(row.BlogName.Length>40)
                        {
                            strBlogName = row.BlogName.Remove(40);
                        }
                        string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                        string strURL = ResolveUrl(( "~/BlogDetails?"+ Functions.Base64Encode(row.Id.ToString()+"|"+ row.TypeDetail)));
                        strBlogs.Append("");

                        strBlogs.Append("<div class='col-lg-4 col-md-6 col-sm-12 news-block mb-40'>");
                        strBlogs.Append("    <div class='news-block-one'>");
                        strBlogs.Append("		<div class='inner-box'>");
                        strBlogs.Append("			<figure class='image-box'>");
                        strBlogs.Append("				<img src='" + strimagePath + "' alt=''>");
                        strBlogs.Append("				<a href='"+ strURL + "' class='link'><i class='fas fa-link'></i></a>");
                        strBlogs.Append("				<span class='category'>"+ row.TypeDetail + "</span>");
                        strBlogs.Append("			</figure>");
                        strBlogs.Append("			<div class='lower-content'>");
                        strBlogs.Append("				<ul class='post-info'>");
                        strBlogs.Append("					<li><i class='fas fa-user'></i> <a href='" + strURL + "'>" + row.Blogger + "</a></li>");
                        strBlogs.Append("					<li><i class='fa fa-calendar'></i>" + row.BlogDate.ToString("MMMM dd, yyyy") + "</li>");
                        strBlogs.Append("				</ul>");
                        strBlogs.Append("				<h4><a href='" + strURL + "'> " + strBlogName + "..");
                        strBlogs.Append("					</a></h4>");
                        strBlogs.Append("				<div class='link'><a href='" + strURL + "'><i class='fas fa-angle-right'></i></a></div>");
                        strBlogs.Append("				<div class='btn-box'><a href='" + strURL + "' class='theme-btn-one'>Read more<i class='fas fa-angle-right'></i></a></div>");
                        strBlogs.Append("			</div>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("	</div>");
                        strBlogs.Append("</div>");

                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).OrderByDescending(x => x.Id).ToList();
                        goto LableData;
                    }
                }

            }
            return strBlogs.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Blogs").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Blogs").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            BlogListDetails.InnerHtml = GetBlogs(txtSearch.Text);
            txtSearch.Text = "";
        }
    }
}