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
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class OtherFacilities : System.Web.UI.Page
    {
        public static string strHeaderImage, strPageDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strPageDetails = GetPageDetails();
            }
        }

        private string GetPageDetails()
        {
            StringBuilder strPageDetails = new StringBuilder();

            int languageId = Functions.LanguageId;


            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strPageDetails); // 10 sec page reload with english content logic
                    return strPageDetails.ToString();
                }
            }
            else
            {
                using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
                {
                    var dataList = objOtherFacilitiesRepository.GetAllOtherFacilitiesMaster(languageId).FirstOrDefault();
                    LableData:

                    if (dataList != null)
                    {
                        string strURLs = "";

                        if (!string.IsNullOrWhiteSpace(dataList.SideImage))
                        {
                            strURLs = (dataList.SideImage.ToString());
                            if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURLs = strURLs.Replace("~/", "");
                                strURLs = ResolveUrl("~/" + strURLs);
                            }

                        }
                        lblTitleH3.InnerHtml = dataList.Title;
                        lblTitleH4.InnerHtml = dataList.Title;
                        if (!string.IsNullOrWhiteSpace(dataList.VideoLink))
                        {
                            iFreamURLH4.Src = dataList.VideoLink;
                        }
                        else
                        {
                            iFreamURLH4.Visible = false;
                        }
                        if (!string.IsNullOrWhiteSpace(strURLs))
                        {
                            imgURLH4.Src = strURLs;
                        }
                        else
                        {
                            imgURLH4.Visible = false;
                        }

                        var dataSubList = objOtherFacilitiesRepository.GetAllOurFacilitiesMasterSubDetails((long)dataList.OurFacillityId, languageId);

                        strPageDetails.Append("");


                        #region SubDetails
                        if (dataSubList.Count() > 0)
                        {
                            int index = 1;
                            foreach (var row in dataSubList)
                            {
                                strPageDetails.Append("<div class='author-widget clearfix mb-15'>");
                                strPageDetails.Append("	<div class='about-author'>");
                                strPageDetails.Append("		<div class='row'>");
                                if (index % 2 == 0)
                                {
                                    var dataSubImageList = objOtherFacilitiesRepository.GetAllOurFacilitiesMasterSubDetailsImage((long)row.Id, languageId);

                                    if (dataSubImageList.Count() > 0)
                                    {
                                        strPageDetails.Append("			<div class='col-lg-4'>");
                                        strPageDetails.Append("				<div class='about-author'>");
                                        strPageDetails.Append("					<div class='profile-widget'>");
                                        strPageDetails.Append("						<div id = 'carouselExampleSlidesOnly' class='carousel slide' data-ride='carousel'>");
                                        strPageDetails.Append("							<div class='carousel-inner'>");
                                        int ImageIndex = 1;
                                        foreach (var subImage in dataSubImageList)
                                        {
                                            string strURL = "";

                                            if (!string.IsNullOrWhiteSpace(subImage.ImageName))
                                            {
                                                strURL = (subImage.ImageName.ToString());
                                                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                                {
                                                    strURL = strURL.Replace("~/", "");
                                                    strURL = ResolveUrl("~/" + strURL);
                                                }

                                            }
                                            strPageDetails.Append("								<div class='carousel-item " + (ImageIndex == 1 ? "active" : "") + "'>");
                                            strPageDetails.Append("									<img class='d-block w-100' src='" + strURL + "' alt=''>");
                                            strPageDetails.Append("								</div>");
                                            ImageIndex++;
                                        }

                                        strPageDetails.Append("							</div>");
                                        strPageDetails.Append("						</div>");
                                        strPageDetails.Append("					</div>");
                                        strPageDetails.Append("				</div>");
                                        strPageDetails.Append("			</div>");
                                    }

                                    strPageDetails.Append("			<div class='col-lg-8'>");
                                    strPageDetails.Append("				<div class='author-details_1'>");
                                    strPageDetails.Append("					<h5>" + row.Title + "</h5>");
                                    strPageDetails.Append("					<p class='mb-0'>" + HttpUtility.HtmlDecode(row.Description) + "</p>");
                                    strPageDetails.Append("				</div>");
                                    strPageDetails.Append("			</div>");
                                }
                                else
                                {
                                    strPageDetails.Append("			<div class='col-lg-8'>");
                                    strPageDetails.Append("				<div class='author-details_1'>");
                                    strPageDetails.Append("					<h5>" + row.Title + "</h5>");
                                    strPageDetails.Append("					<p class='mb-0'>" + HttpUtility.HtmlDecode(row.Description) + "</p>");
                                    strPageDetails.Append("				</div>");
                                    strPageDetails.Append("			</div>");

                                    var dataSubImageList = objOtherFacilitiesRepository.GetAllOurFacilitiesMasterSubDetailsImage((long)row.Id, languageId);

                                    if (dataSubImageList.Count() > 0)
                                    {
                                        strPageDetails.Append("			<div class='col-lg-4'>");
                                        strPageDetails.Append("				<div class='about-author'>");
                                        strPageDetails.Append("					<div class='profile-widget'>");
                                        strPageDetails.Append("						<div id = 'carouselExampleSlidesOnly' class='carousel slide' data-ride='carousel'>");
                                        strPageDetails.Append("							<div class='carousel-inner'>");
                                        int ImageIndex = 1;
                                        foreach (var subImage in dataSubImageList)
                                        {
                                            string strURL = "";

                                            if (!string.IsNullOrWhiteSpace(subImage.ImageName))
                                            {
                                                strURL = (subImage.ImageName.ToString());
                                                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                                {
                                                    strURL = strURL.Replace("~/", "");
                                                    strURL = ResolveUrl("~/" + strURL);
                                                }

                                            }
                                            strPageDetails.Append("								<div class='carousel-item " + (ImageIndex == 1 ? "active" : "") + "'>");
                                            strPageDetails.Append("									<img class='d-block w-100' src='" + strURL + "' alt=''>");
                                            strPageDetails.Append("								</div>");
                                            ImageIndex++;
                                        }

                                        strPageDetails.Append("							</div>");
                                        strPageDetails.Append("						</div>");
                                        strPageDetails.Append("					</div>");
                                        strPageDetails.Append("				</div>");
                                        strPageDetails.Append("			</div>");
                                    }


                                }
                                strPageDetails.Append("		</div>");
                                strPageDetails.Append("	</div>");
                                strPageDetails.Append("</div>");
                                strPageDetails.Append("<hr>");
                                index++;
                            }
                        }
                        #endregion



                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataList = objOtherFacilitiesRepository.GetAllOtherFacilitiesMaster(languageId).FirstOrDefault();
                            goto LableData;
                        }
                    }
                }
            }
            return strPageDetails.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherFacilities").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherFacilities").FirstOrDefault();
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