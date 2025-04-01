using BAL;
using BO;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class CovidCare : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strCovidCareDetails;
        public static string strCovidCareLeftVideo;
        public static string strCovidCareRightVideo;
        public static string strFAQsDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strCovidCareDetails = GetCovidCareDetailsSection();
            }
        }
        private string GetCovidCareDetailsSection()
        {
            CovidCareMasterBO objBO = new CovidCareMasterBO();
            objBO.Language_Id = Functions.LanguageId;

            StringBuilder strCovidCKeditor = new StringBuilder();

            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus((int)objBO.Language_Id, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                
                    Functions.GetReloadPage(this.Page, ref strCovidCKeditor); // 10 sec page reload with English content logic
                    return strCovidCKeditor.ToString();
                
            }
            else
            {
                StringBuilder strLeftVideo = new StringBuilder();
                StringBuilder strRightVideo = new StringBuilder();
                StringBuilder strCovidAccredation = new StringBuilder();
                StringBuilder strFAQs = new StringBuilder();
                DataSet ds = new CovidCareMasterBAL().GetCovidCareDetailsByLanguage(objBO);
                if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string strURL = (row["ImageUploadPath"].ToString());
                            if (strURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURL = strURL.Replace("~/", "");
                                strURL = ResolveUrl("~/" + strURL);
                            }
                            strCovidCKeditor.Append("<div class='col-lg-12'>");
                            strCovidCKeditor.Append("<div class='blog-view'>");
                            strCovidCKeditor.Append("    <div class='blog blog-single-post'>");
                            strCovidCKeditor.Append("        <div class='blog-content'>");
                            strCovidCKeditor.Append("            <div class='row'>");
                            strCovidCKeditor.Append("                <div class='col-lg-6'>");
                            strCovidCKeditor.Append("                    <h3 class='blog-title'>" + row["Title"].ToString() + "</h3>");
                            strCovidCKeditor.Append(HttpUtility.HtmlDecode(row["Description"].ToString()));
                            strCovidCKeditor.Append("                </div>");
                            strCovidCKeditor.Append("                <div class='col-lg-6'>");
                            strCovidCKeditor.Append("                    <img src = \"" + strURL + "\" class='img-fluid'>");
                            strCovidCKeditor.Append("                </div>");
                            strCovidCKeditor.Append("            </div>");
                            strCovidCKeditor.Append("        </div>");
                            strCovidCKeditor.Append("    </div>");
                            strCovidCKeditor.Append("</div>");
                            strCovidCKeditor.Append("</div>");


                            string strLeftVideoURL = (row["LeftVideoPath"].ToString());
                            if (strLeftVideoURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strLeftVideoURL = strLeftVideoURL.Replace("~/", "");
                                strLeftVideoURL = ResolveUrl("~/" + strLeftVideoURL);
                            }

                            string strLeftVideoThumbnailURL = (row["LeftVideoThumbnailPath"].ToString());
                            if (strLeftVideoThumbnailURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strLeftVideoThumbnailURL = strLeftVideoThumbnailURL.Replace("~/", "");
                                strLeftVideoThumbnailURL = ResolveUrl("~/" + strLeftVideoThumbnailURL);
                            }

                            strLeftVideo.Append("<div class='video-bg-img video-sec text-center'>");
                            strLeftVideo.Append("            <a href = \"" + strLeftVideoURL + "\" data-fancybox='gallery' class='lightbox-image'>");
                            strLeftVideo.Append("                <img src = \"" + strLeftVideoThumbnailURL + "\" alt='Video' />");
                            strLeftVideo.Append("                <span class='play-btn'>");
                            strLeftVideo.Append("                    <i class='fas fa-play'></i>");
                            strLeftVideo.Append("                </span></a>");
                            strLeftVideo.Append("        </div>");

                            string strRightVideoURL = (row["RightVideoPath"].ToString());
                            if (strRightVideoURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strRightVideoURL = strRightVideoURL.Replace("~/", "");
                                strRightVideoURL = ResolveUrl("~/" + strRightVideoURL);
                            }

                            string strRightVideoThumbnailURL = (row["RightVideoThumbnailPath"].ToString());
                            if (strRightVideoThumbnailURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strRightVideoThumbnailURL = strRightVideoThumbnailURL.Replace("~/", "");
                                strRightVideoThumbnailURL = ResolveUrl("~/" + strRightVideoThumbnailURL);
                            }

                            strRightVideo.Append("<div class='video-bg-img video-sec text-center'>");
                            strRightVideo.Append("            <a href = \"" + strRightVideoURL + "\" data-fancybox='gallery' class='lightbox-image'>");
                            strRightVideo.Append("                <img src = \"" + strRightVideoThumbnailURL + "\" alt='Video' />");
                            strRightVideo.Append("                <span class='play-btn'>");
                            strRightVideo.Append("                    <i class='fas fa-play'></i>");
                            strRightVideo.Append("                </span></a>");
                            strRightVideo.Append("        </div>");

                            string strFAQsImageURL = (row["FAQsImageUploadPath"].ToString());
                            if (strFAQsImageURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strFAQsImageURL = strFAQsImageURL.Replace("~/", "");
                                strFAQsImageURL = ResolveUrl("~/" + strFAQsImageURL);
                            }
                            spnTitlt.InnerText = "FAQ's";
                            lblFAQsTitle.Text = row["FAQsTitle"].ToString();
                            string CovidDetailId = row["Id"].ToString();
                            string LanguageId = row["Language_Id"].ToString();
                            strFAQs.Append("<div class='col-lg-5'>");
                            strFAQs.Append("<img src = \"" + strFAQsImageURL + "\" class='img-fluid' />");
                            strFAQs.Append("</div>");
                            strFAQs.Append("<div class='col-lg-7 col-md-7'>");
                            strFAQs.Append("<div class='accordion-box'>");
                            strFAQs.Append("<div class='title-box'>");
                            strFAQs.Append("<h6>" + row["FAQsAccreditationTitle"].ToString() + "</h6>");
                            strFAQs.Append("</div>");
                            strFAQs.Append(GetFAQsDetails(CovidDetailId, LanguageId));
                            strFAQs.Append("</div>");
                            strFAQs.Append("</div>");
                        }
                    }
                }
                strCovidCareLeftVideo = strLeftVideo.ToString();
                strCovidCareRightVideo = strRightVideo.ToString();
                strFAQsDetails = strFAQs.ToString();
            }
            return strCovidCKeditor.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CovidCare").FirstOrDefault();
                if (dataMain != null)
                {
                    LableData:
                    strcontactus = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strcontactus.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CovidCare").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strcontactus.ToString();
        }

        private string GetFAQsDetails(string CovidCareDetailsId, string LanguageId)
        {
            StringBuilder StrFAQsAccreditation = new StringBuilder();
            CovidCareAccredationDetailsBO objBO = new CovidCareAccredationDetailsBO();
            CovidCareMasterBAL objBAL = new CovidCareMasterBAL();
            objBO.CovidCareDetails_Id = Convert.ToInt32(CovidCareDetailsId);
            objBO.Language_id = Convert.ToInt32(LanguageId);
            DataSet dsDetails = objBAL.SelectAccredationRecord(objBO);

            DataTable dtInfo = dsDetails.Tables[0];
            StrFAQsAccreditation.Append("");
            if (dtInfo.Rows.Count > 0 && dtInfo != null)
            {
                foreach (DataRow row in dtInfo.Rows)
                {
                    StrFAQsAccreditation.Append("<ul class='accordion-inner'>");
                    StrFAQsAccreditation.Append("<li class='accordion block'>");
                    StrFAQsAccreditation.Append("<div class='acc-btn'>");
                    StrFAQsAccreditation.Append("<div class='icon-outer'></div>");
                    StrFAQsAccreditation.Append("<h6>" + row["AccredationSubTitle"].ToString() + "</h6>");
                    StrFAQsAccreditation.Append("</div>");
                    StrFAQsAccreditation.Append("<div class='acc-content'>");
                    StrFAQsAccreditation.Append(HttpUtility.HtmlDecode(row["AccredationDescription"].ToString()));
                    StrFAQsAccreditation.Append("</div>");
                    StrFAQsAccreditation.Append("</li>");
                    StrFAQsAccreditation.Append("</ul>");
                }
            }
            return StrFAQsAccreditation.ToString();
        }
    }
}