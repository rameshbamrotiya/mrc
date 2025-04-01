using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.About
{
    public partial class AboutHistory : System.Web.UI.Page
    {
        public static string strBoard;
        public static string strHeaderImage;

        public Boolean IsNumber(String s)
        {
            Boolean value = true;
            foreach (Char c in s.ToCharArray())
            {
                value = value && Char.IsDigit(c);
            }

            return value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strBoard = GetPageData();
                strHeaderImage = GetHeaderImage();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutHistory").FirstOrDefault();

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
                            dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutHistory").FirstOrDefault();
                            if (languageId != 1)
                            {
                                goto LableData;
                            }
                        }
                    }
                
            }
            return strBoardOfDirector.ToString();
        }
        private string GetPageData()
        {
            int languageId = Functions.LanguageId;

            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IHistoryRepository objBlogCategoryMasterRepository = new HistoryRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllTblHistory(languageId).FirstOrDefault();
                //dataMain.HistoryTitle.ToString();
                var dataList = objBlogCategoryMasterRepository.GetAllTblHistory(languageId).Where(x => x.IsVisible == true && IsNumber(x.Year)).OrderBy(x=> Convert.ToInt32(x.Year)).ToList();
                if (dataMain != null)
                {
                    string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                    //rowURl = rowURl.Substring(1);
                    StringBuilder strResearch = new StringBuilder();
                    ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                    bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                    if (IsDisabledTranslate && Functions.LanguageId != 1)
                    {
                        {
                            Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                            return strResearch.ToString();
                        }
                    }
                    else
                    {

                        int i = 1;
                        strBoardOfDirector.Append("\r\n            <div class=\"historydata\">\r\n                <h3 class=\"text-center\">Timeline</h3>\r\n                <div class=\"row\">\r\n                    <!-- NVE content div -->\r\n\r\n                    <ul id=\"timeline\" class=\"timeline\">\r\n                        <div class=\"arrowhead\"></div>");
                        foreach (var row in dataList)
                        {
                            if (!string.IsNullOrWhiteSpace(row.HistoryDescription))
                            {
                                string filePath = ResolveUrl(ConfigDetailsValue.AddHistoryFileUploadPath + row.HistoryPhotoName);
                                strBoardOfDirector.Append("");

                                strBoardOfDirector.Append("<li class='" + (i % 2 == 0 ? "timeline-inverted" : " ") + "'>");
                                strBoardOfDirector.Append("	<div class='timeline-badge'>" + row.Year + "</div>");
                                strBoardOfDirector.Append("	<div class='timeline-panel'>");
                                if (!string.IsNullOrWhiteSpace(row.HistoryPhotoName))
                                {
                                    strBoardOfDirector.Append("	<img src='" + filePath + "' alt='" + row.Year + "'>");
                                }
                                strBoardOfDirector.Append("	<div class='timeline-heading'>");
                                strBoardOfDirector.Append("<h3 class='timeline-title'>" + row.HistoryTitle + "</h3>");
                                strBoardOfDirector.Append("</div>");
                                strBoardOfDirector.Append("<div class='timeline-body'>" + HttpUtility.HtmlDecode(row.HistoryDescription) + "</div>");
                                strBoardOfDirector.Append("</div>");
                                strBoardOfDirector.Append("</li>");
                                i++;
                            }
                        }
                        strBoardOfDirector.Append("\r\n                    \r\n                    </ul>\r\n\r\n                </div>\r\n                <!-- NVE content div -->\r\n\r\n            </div>");

                    }
                }



            }
            return strBoardOfDirector.ToString();
        }
    }
}