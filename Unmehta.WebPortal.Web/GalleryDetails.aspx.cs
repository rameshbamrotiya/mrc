using BAL;
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
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class GalleryDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strGalleryTitle;
        public static string strGalleryDetails;
        public static int AlbumId;
        public static int VideoId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!string.IsNullOrEmpty(Request.QueryString.ToString()))
                {
                    strHeaderImage = GetHeaderImage();
                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    string[] strQuery = strQueryString.Split('|').ToArray();
                    string Id = strQuery[0].ToString().Replace("Id=", "");
                    string Flag = strQuery[1].ToString().Replace("Flag=", "");
                    if (Flag == "0")
                    {
                        AlbumId = Convert.ToInt32(Id);
                        strGalleryDetails = GetListOfSubSectionDescription();
                    }
                    else
                    {
                        VideoId = Convert.ToInt32(Id);
                        strGalleryDetails = GetVideoListOfSubSectionDescription();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder GalleryTitle = new StringBuilder();
            StringBuilder strGallery = new StringBuilder();
            DataSet ds = new PhotoGalleryMasterBAL().SelectalAlbumGalleyDetails(AlbumId, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    GalleryTitle.Append("<h3>" + ds.Tables[0].Rows[0]["Album_name"].ToString() + "</h3>");
                    strGalleryTitle = GalleryTitle.ToString();
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strGallery.Append("<div class='ltn__gallery-item filter_category_3 col-lg-3 col-sm-6  filter Hospital'>");
                        strGallery.Append("<div class='ltn__gallery-item-inner'>");
                        strGallery.Append("<div class='ltn__gallery-item-img'>");
                        strGallery.Append("<a href = '" + ResolveUrl(row["Image_desc"].ToString()) + "' data-fancybox='gallery' class='lightbox-image'>");
                        strGallery.Append("<img class='img-fluid' alt='Image' src='" + ResolveUrl(row["Image_desc"].ToString()) + "'>");
                        strGallery.Append("<span class='ltn__gallery-action-icon'>");
                        strGallery.Append("<i class='far fa-image'></i>");
                        strGallery.Append("</span>");
                        strGallery.Append("</a>");
                        strGallery.Append("</div>");
                        strGallery.Append("</div>");
                        strGallery.Append("</div>");
                        i++;
                    }
                }
            }
            return strGallery.ToString();
        }
        private string GetVideoListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder GalleryTitle = new StringBuilder();
            StringBuilder strGallery = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {

                var dataList = objHomePageRepository.GetAllVideoAlbum(languageId).Where(x => x.AlbumId == VideoId).FirstOrDefault();
                var dataLists = objHomePageRepository.GetAllVideoGallayListById(VideoId, languageId);



                GalleryTitle.Append("<h3>" + dataList.VideoCategoryName + "</h3>");
                strGalleryTitle = GalleryTitle.ToString();
                int i = 1;
                foreach (var row in dataLists)
                {
                    string strURLs = "";

                    if (!string.IsNullOrWhiteSpace(row.ThumbImg_path))
                    {
                        strURLs = (row.ThumbImg_path.ToString());
                        if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Replace("~/", "");
                            strURLs = ResolveUrl("~/" + strURLs);
                        }

                    }
                    else
                    {
                        strURLs = ResolveUrl("~/hospital/assets/img/hospital/1339.jpg");
                    }
                    if (row.Link_Video_Upload == true)
                    {
                        strGallery.Append("<div class='ltn__gallery-item filter_category_2 col-lg-3 col-sm-6'>");
                        strGallery.Append("	<div class='ltn__gallery-item-inner'>");
                        strGallery.Append("		<div class='ltn__gallery-item-img'>");
                        strGallery.Append("			<a href='" + ResolveUrl(row.Video_path) + "' data-fancybox='gallery' class='lightbox-image'>");
                        strGallery.Append("				<img src='" + strURLs + "' alt='Image' class='img-fluid'>");
                        strGallery.Append("				<span class='ltn__gallery-action-icon'>");
                        strGallery.Append("					<i class='fas fa-video'></i>");
                        strGallery.Append("				</span>");
                        strGallery.Append("			</a>");
                        strGallery.Append("		</div>");
                        strGallery.Append("		<div class='ltn__gallery-item-info'>");
                        strGallery.Append("			<h4><a href='" + ResolveUrl(row.Video_path) + "'>" + row.Video_name + "</a></h4>");
                        strGallery.Append("<p>" + row.Video_desc + "</p>");
                        strGallery.Append("		</div>");
                        strGallery.Append("	</div>");
                        strGallery.Append("</div>");
                    }
                    else
                    {
                        if (row.Video_path.Contains("youtube"))
                        {
                            strGallery.Append("<div class='ltn__gallery-item filter_category_1 col-lg-3 col-sm-6'>");
                            strGallery.Append("<div class='ltn__gallery-item-inner'>");
                            strGallery.Append("<div class='ltn__gallery-item-img'>");
                            strGallery.Append("<a href='" + ResolveUrl(row.Video_path) + "' data-fancybox='gallery'");
                            strGallery.Append("class='lightbox-image'>");
                            strGallery.Append("<img src='" + strURLs + "' alt='Image' class='img-fluid'>");
                            strGallery.Append("<span class='ltn__gallery-action-icon'>");
                            strGallery.Append("<i class='fab fa-youtube'></i>");
                            strGallery.Append("</span>");
                            strGallery.Append("</a>");
                            strGallery.Append("</div>");
                            strGallery.Append("<div class='ltn__gallery-item-info'>");
                            strGallery.Append("<h4><a href='" + ResolveUrl(row.Video_path) + "'>" + row.Video_name + "</a></h4>");
                            strGallery.Append("<p>" + row.Video_desc + "</p>");
                            strGallery.Append("</div>");
                            strGallery.Append("	</div>");
                            strGallery.Append("</div>");
                        }
                        else if (row.Video_path.Contains("vimeo"))
                        {
                            strGallery.Append("<div class='ltn__gallery-item filter_category_3 col-lg-3 col-sm-6'>");
                            strGallery.Append("	<div class='ltn__gallery-item-inner'>");
                            strGallery.Append("		<div class='ltn__gallery-item-img'>");
                            strGallery.Append("			<a href='" + ResolveUrl(row.Video_path) + "' data-fancybox='gallery' class='lightbox-image'>");
                            strGallery.Append("				<img src='" + strURLs + "' alt='Image' class='img-fluid'> ");
                            strGallery.Append("				<span class='ltn__gallery-action-icon'>");
                            strGallery.Append("					<i class='fab fa-vimeo-v'></i>");
                            strGallery.Append("				</span>");
                            strGallery.Append("			</a>");
                            strGallery.Append("		</div>");
                            strGallery.Append("		<div class='ltn__gallery-item-info'>");
                            strGallery.Append("			<h4><a href='" + ResolveUrl(row.Video_path) + "'>" + row.Video_name + " </a></h4>");
                            strGallery.Append("			<p>" + row.Video_desc + "</p> ");
                            strGallery.Append("		</div>");
                            strGallery.Append("	</div>");
                            strGallery.Append("</div>");
                        }
                    }
                }
                return strGallery.ToString();
            }
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "GalleryDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "GalleryDetails").FirstOrDefault();
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