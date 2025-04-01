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
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.About
{
    public partial class DirectorDesk : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strDescription;
        protected void Page_Load(object sender, EventArgs e)
        {
            strHeaderImage = GetHeaderImage();
            dvDesc.InnerHtml = GetDescription();
        }

        private string GetDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strServices = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetGetDirectorMasterByLangId(languageId).ToList();

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
                LableData:

                    if (dataList.Count() > 0)
                    {
                        int i = 1;
                        strServices.Append("<blockquote class=\"type2\" >");
                        foreach (var row in dataList)
                        {
                            strServices.Append(HttpUtility.HtmlDecode(row.DirectorMesshtmlContent));

                        }
                        strServices.Append("</blockquote>");
                    }
                    else
                    {
                        languageId = 1;
                        dataList = objBlogCategoryMasterRepository.GetGetDirectorMasterByLangId(languageId).ToList();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }

            }
            return strServices.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/DirectorDesk").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/DirectorDesk").FirstOrDefault();
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