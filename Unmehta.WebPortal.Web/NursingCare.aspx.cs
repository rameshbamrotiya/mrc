using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class NursingCare : System.Web.UI.Page
    {
        public static string strHeaderImage;

        public static string strHealthTips;
        public static string strAccords;
        public static string strHealthTipImage;

        public static string strQuickLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strHealthTips = GetHealthTips();
                strAccords = GetAccords();
                strQuickLink = Functions.CreateQuickLink("Cares", "NursingCare");
            }
        }

        private string GetAccords()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strCareerbind = new StringBuilder();
            int LanguageId = languageId;
            DataSet ds = new NursingCareAccordionTypeMasterBAL().SelectAccrodianSubRecordFront(LanguageId);
            LableData:
            if (ds != null)
            {
                strCareerbind = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    List<DataTable> result = ds.Tables[0].AsEnumerable().GroupBy(row => row.Field<string>("Accordion_Name")).Select(g => g.CopyToDataTable()).ToList();
                    int i = 1;
                    foreach (DataTable dt in result)
                    {
                        DataRow dr = dt.Rows[0];
                        strCareerbind.Append("<div class='widget about-widget'>");
                        strCareerbind.Append("<div class='accordion-box'>");
                        strCareerbind.Append("<div class='title-box'>");
                        strCareerbind.Append("<h6>" + dr["Accordion_Name"] + "</h6>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<ul class='accordion-inner'>");
                        foreach (DataRow row in dt.Rows)
                        {
                            strCareerbind.Append("<li class='accordion block'>");
                            strCareerbind.Append("<div class='acc-btn'>");
                            strCareerbind.Append("<div class='icon-outer'></div>");
                            strCareerbind.Append("<h6>" + row["AccordionSubTitle"] + "</h6>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("<div class='acc-content'>");
                            strCareerbind.Append("<div class='row'>");
                            strCareerbind.Append("<p>" + HttpUtility.HtmlDecode(row["AccordionSubDescription"].ToString()) + "</p>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</li>");
                        }
                        strCareerbind.Append("</ul>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        i++;
                    }
                }
                else
                {
                    strCareerbind.Append("<h1 style='text-align: center;border: 1px solid;margin-bottom: 10px !important;' class='title mb-0'>There is no Accordion available</h1>");
                }
            }
            return strCareerbind.ToString();
        }

        private string GetHealthTips()
        {

            int languageId = Functions.LanguageId;
            StringBuilder strHealthTips = new StringBuilder();
            StringBuilder strHealthTipImages = new StringBuilder();

            strHealthTips.Append("");
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetNursingCareLanguageId(languageId);

                var dataImage = objBlogCategoryMasterRepository.GetAllNursingCareImage();

                LableData:

                if (dataList != null)
                {
                    if (string.IsNullOrWhiteSpace(dataList.MainImgpath))
                    {
                        strHealthTips.Append("<div class='col-md-12 col-lg-12'>");
                        strHealthTips.Append("	<!-- About Details -->");
                        strHealthTips.Append("	<div class='widget about-widget'>");
                        strHealthTips.Append(HttpUtility.HtmlDecode(dataList.Description));
                        strHealthTips.Append("	</div>");
                        strHealthTips.Append("	<!-- /About Details -->");
                        strHealthTips.Append("</div>");
                    }
                    else
                    {
                        string strURL = "";

                        if (!string.IsNullOrWhiteSpace(dataList.MainImgpath))
                        {
                            strURL = (dataList.MainImgpath);
                            if (strURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURL = strURL.Replace("~/", "");
                                strURL = ResolveUrl("~/" + strURL);
                            }

                        }
                        strHealthTips.Append("");
                        strHealthTips.Append("<div class='col-md-7 col-lg-7'>");
                        strHealthTips.Append("	<!-- About Details -->");
                        strHealthTips.Append("	<div class='widget about-widget'>");
                        strHealthTips.Append(HttpUtility.HtmlDecode(dataList.Description));
                        strHealthTips.Append("	</div>");
                        strHealthTips.Append("	<!-- /About Details -->");
                        strHealthTips.Append("</div>");
                        strHealthTips.Append("<div class='col-md-5 col-lg-5'>");
                        strHealthTips.Append("	<div class='profile-widget'>");
                        strHealthTips.Append("		<div class='doc-img'>");
                        strHealthTips.Append("			<a href='#'>");
                        strHealthTips.Append("				<img class='img-fluid' alt='User Image' src='" + strURL + "'>");
                        strHealthTips.Append("			</a>");
                        strHealthTips.Append("		</div>");
                        strHealthTips.Append("	</div>");
                        strHealthTips.Append("</div>");

                    }

                    //string strDesc = strHealthTips.ToString();

                    //strHealthTips.Append(strDesc);

                    if (dataImage.Count() > 0)
                    {

                        foreach (var row in dataImage)
                        {
                            string strImage = ResolveUrl((row.Img_path.ToString()));

                            strHealthTipImages.Append("<div class='col-lg-4 col-md-6 col-12'>");
                            strHealthTipImages.Append("	<div class='departments-box-layout5'>");
                            strHealthTipImages.Append("		<div class='item-img'>");
                            strHealthTipImages.Append("			<img src='" + strImage + "' alt='department' class='img-fluid'>");
                            strHealthTipImages.Append("			<div class='item-content'>");
                            strHealthTipImages.Append("				<a href='" + strImage + "' data-fancybox='gallery' class='item-btn'>DETAILS</a>");
                            strHealthTipImages.Append("			</div>");
                            strHealthTipImages.Append("		</div>");
                            strHealthTipImages.Append("	</div>");
                            strHealthTipImages.Append("</div>");
                        }

                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetNursingCareLanguageId(languageId);
                        goto LableData;
                    }
                }

            }
            strHealthTipImage = strHealthTipImages.ToString();

            return strHealthTips.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "NursingCare").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "NursingCare").FirstOrDefault();
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