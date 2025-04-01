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

namespace Unmehta.WebPortal.Web
{
    public partial class AboutFutureMission : System.Web.UI.Page
    {
        public static string strVisionMission;
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strVisionMission = GetVisionMission();
                strHeaderImage = GetHeaderImage();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutFutureMission").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutFutureMission").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
        private string GetVisionMission()
        {

            //int languageId = Functions.LanguageId;
            StringBuilder strVisionMission = new StringBuilder();

            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                int languageId = 1;
                var dataList = objBlogCategoryMasterRepository.GetAllFutureVisionDetailsByLangId(languageId).FirstOrDefault();
                string rowURl = Request.RawUrl.ToString();
                rowURl = rowURl.Substring(1);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                if (IsDisabledTranslate)
                {
                    if (Functions.LanguageId == 1)
                    {
                        dataList = objBlogCategoryMasterRepository.GetAllFutureVisionDetailsByLangId(1).FirstOrDefault();
                    }
                    else
                    {
                        dataList = null;
                        Functions.GetReloadPage(this.Page, ref strVisionMission); // 10 sec page reload with english content logic
                        return strVisionMission.ToString();
                    }
                }
                else
                {
                    dataList = objBlogCategoryMasterRepository.GetAllFutureVisionDetailsByLangId(languageId).FirstOrDefault(); // load english content and translate it.
                }
                if (dataList != null)
                {

                    strVisionMission.Append("<div class='sec-title'>");

                    string strImage = string.IsNullOrWhiteSpace(dataList.ImagePath) ? "" : ResolveUrl(dataList.ImagePath);
                    if (string.IsNullOrWhiteSpace(dataList.ImagePath))
                    {
                        strVisionMission.Append(Functions.CustomHTMLDecode(dataList.Description,this.Page).Replace("/hospital/", "").Replace("src=\"..", "src=\"").Replace("src=\"assets/", "src=\"../hospital/assets/"));
                    }
                    else
                    {

                        strVisionMission.Append("<div class='row'>");
                        strVisionMission.Append("    <div class='col-md-8 col-lg-8'>");
                        strVisionMission.Append(Functions.CustomHTMLDecode(dataList.Description, this.Page).Replace("/hospital/", "").Replace("src=\"..", "src=\"").Replace("src=\"assets/", "src=\"../hospital/assets/"));
                        strVisionMission.Append("    </div>");

                        strVisionMission.Append("    <div class='col-md-4 col-lg-4'>");

                        strVisionMission.Append("<div class='img_effect'><img class='img-fluid' src='" + strImage + "'></div>");

                        strVisionMission.Append("    </div>");
                        strVisionMission.Append("</div>");
                    }
                    strVisionMission.Append("</div>");
                }
            }
            return strVisionMission.ToString();
        }
    }
}