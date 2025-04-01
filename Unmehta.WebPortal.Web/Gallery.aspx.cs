using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;
namespace Unmehta.WebPortal.Web
{
    public partial class Gallery : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfSubSectionDescription;
        public static string strVideoListOfSubSectionDescription;
        public static string strMainTagList;
        public static string strVideoMainTagList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();

                strVideoMainTagList = GetVideoTagListOfSubSection();
                strVideoListOfSubSectionDescription = GetVideoListOfSubSectionDescription();

                strMainTagList = GetTagListOfSubSection();
                strListOfSubSectionDescription = GetListOfSubSectionDescription();
            }

        }

        private string GetTagListOfSubSection()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strMainTag = new StringBuilder();
            strMainTag.Clear();
            DataSet ds = new PhotoGalleryMasterBAL().SelectalAlbumGalleyDetails(0, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    List<string> lstTagList = new List<string>();
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string[] strTags = row["TagList"].ToString().Split(',');
                        foreach (var row1 in strTags)
                        {
                            lstTagList.Add(row1.Trim().Replace(" ", "_"));
                        }
                    }

                    foreach (var row in lstTagList.Distinct().ToList())
                    {
                        if (!string.IsNullOrWhiteSpace(row))
                        {
                            strMainTag.Append("<a class='btn btn-outline-success filter-button' data-filter='" + row + "'>" + row + "</a>");
                        }

                    }
                }
            }
            return strMainTagList = strMainTag.ToString();
        }

        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strResearch = new StringBuilder();
            strResearch.Clear();
            DataSet ds = new PhotoGalleryMasterBAL().SelectalAlbumGalleyDetails(0, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("Id= " + row["Album_id"].ToString() + "|Flag=" + 0));
                        string strURL = ResolveUrl(("~/GalleryDetails?" + strdQuery));
                        string strTags = row["TagList"].ToString().Replace(",", " ");

                        List<string> lstTagList = new List<string>();
                        {
                            string[] strTagss = row["TagList"].ToString().Split(',');
                            foreach (var row1 in strTagss)
                            {
                                lstTagList.Add(row1.Trim().Replace(" ", "_"));
                            }
                        }
                        strTags = string.Join<string>(" ", lstTagList.Distinct().ToList());

                        strResearch.Append("<div class='ltn__gallery-item filter_category_3 col-lg-4 col-sm-6  filter " + strTags + "'>");
                        strResearch.Append("<div class='ltn__gallery-item-inner'>");
                        strResearch.Append("<div class='ltn__gallery-item-img'>");
                        strResearch.Append("<a href = '" + strURL + "'>");
                        strResearch.Append("<img class='img-fluid' alt='Image' src='" + ResolveUrl(row["Image_name"].ToString()) + "'>");
                        strResearch.Append("<span class='ltn__gallery-action-icon'>");
                        strResearch.Append("<p>View Album</p>");
                        strResearch.Append("</span>");
                        strResearch.Append("</a>");
                        strResearch.Append("</div>");
                        strResearch.Append("<div class='ltn__gallery-item-info'>");
                        strResearch.Append("<h4><a href = '" + strURL + "'> " + row["Album_name"] + "</a>");
                        strResearch.Append("</h4>");
                        strResearch.Append("<p>" + row["Album_desc"] + "</p>");
                        strResearch.Append("</div>");
                        strResearch.Append("</div>");
                        strResearch.Append("</div>");
                        i++;
                    }
                }
            }
            return strResearch.ToString();
        }

        private string GetVideoTagListOfSubSection()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strMainTag = new StringBuilder();

            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objHomePageRepository.GetAllVideoAlbum(languageId);


                List<string> lstTagList = new List<string>();
                foreach (var row in dataList)
                {
                    string[] strTags = row.TagList.Split(',');
                    foreach (var row1 in strTags)
                    {
                        lstTagList.Add(row1.Trim());
                    }
                }

                foreach (var row in lstTagList.Distinct().ToList())
                {
                    if (!string.IsNullOrWhiteSpace(row))
                    {
                        strMainTag.Append("<a class='btn btn-outline-success filter-button 'data-filter='" + row + "'>" + row + "</a>");
                    }
                }
            }
            return strMainTagList = strMainTag.ToString();
        }

        private string GetVideoListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strVideo = new StringBuilder();
            strVideo.Clear();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objHomePageRepository.GetAllVideoAlbum(languageId);

                foreach (var row in dataList)
                {
                    var dataLists = objHomePageRepository.GetAllVideoGallayListById(row.AlbumId, languageId).FirstOrDefault();

                    string strPath = "";
                    string strURLs = "";
                    if (!string.IsNullOrWhiteSpace(row.ThumbnillPath))
                    {
                        strURLs = (row.ThumbnillPath.ToString());
                        if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Replace("~/", "");
                            strURLs = ResolveUrl("~/" + strURLs);
                        }

                    }
                    strPath = ("<img class='img-fluid' alt='Image' src='" + strURLs + "'>");
                    if (dataLists != null)
                    {
                        dataLists.Link_Video_Upload = (dataLists.Link_Video_Upload == null ? false : dataLists.Link_Video_Upload);
                        //if ((bool)dataLists.Link_Video_Upload)
                        //{
                        //    strPath="<iframe src='" + dataLists.Video_path + "' allowfullscreen=''></iframe>";
                        //}
                        //else
                        //{
                        //    string strURLs = "";

                        //    if (!string.IsNullOrWhiteSpace(row.ThumbnillPath))
                        //    {
                        //        strURLs = (row.ThumbnillPath.ToString());
                        //        if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                        //        {
                        //            strURLs = strURLs.Replace("~/", "");
                        //            strURLs = ResolveUrl("~/" + strURLs);
                        //        }

                        //    }
                        //    strPath =("<img class='img-fluid' alt='Image' src='" + strURLs + "'>");
                        //}                        
                    }
                    string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("Id= " + row.AlbumId + "|Flag=" + 1));
                    string strURL = ResolveUrl(("~/GalleryDetails?" + strdQuery));
                    string strTags = row.TagList.ToString().Replace(",", " ");
                    strVideo.Append("<div class='ltn__gallery-item filter_category_3 col-lg-4 col-sm-6  filter " + strTags.Replace("  ", " ") + "'>");
                    strVideo.Append("<div class='ltn__gallery-item-inner'>");
                    strVideo.Append("<div class='ltn__gallery-item-img'>");
                    strVideo.Append("<a href = '" + strURL + "'>");
                    strVideo.Append(strPath);
                    strVideo.Append("<span class='ltn__gallery-action-icon'>");
                    strVideo.Append("<p>View Album</p>");
                    strVideo.Append("</span>");
                    strVideo.Append("</a>");
                    strVideo.Append("</div>");
                    strVideo.Append("<div class='ltn__gallery-item-info'>");
                    strVideo.Append("<h4><a href = '" + strURL + "'> " + row.VideoCategoryName + "</a>");
                    strVideo.Append("</h4>");
                    strVideo.Append("</div>");
                    strVideo.Append("</div>");
                    strVideo.Append("</div>");
                }

            }
            return strVideo.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Gallery").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Gallary").FirstOrDefault();
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