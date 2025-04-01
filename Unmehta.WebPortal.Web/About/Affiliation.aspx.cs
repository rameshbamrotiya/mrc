using BAL;
using BO;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class Affiliation : System.Web.UI.Page
    {

        public static string strBoard;
        public static string strHeaderImage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();

                string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                //rowURl = rowURl.Substring(1);
                StringBuilder strResearch = new StringBuilder();
                ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
                if (IsDisabledTranslate && Functions.LanguageId != 1)
                {
                    {
                        Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                        PageDetails.InnerHtml = strResearch.ToString();
                    }
                }
                else
                {
                    PageDetails.InnerHtml = GetPageData();
                }
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/Affiliation").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/Affiliation").FirstOrDefault();
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
            StringBuilder strTitle = new StringBuilder();
            StringBuilder strTabHead = new StringBuilder();
            StringBuilder strMid = new StringBuilder();
            StringBuilder strTabDettails = new StringBuilder();
            StringBuilder strEnd = new StringBuilder();
            using (IAffilationRepository objBlogCategoryMasterRepository = new AffilationRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllAffilationMaster(languageId).Where(x => !string.IsNullOrWhiteSpace(x.AffilationDescription) && !string.IsNullOrWhiteSpace(x.AffilationName) && x.IsVisible == true);
                if (dataMain == null)
                {
                    languageId = 1;
                    dataMain = objBlogCategoryMasterRepository.GetAllAffilationMaster(languageId);
                }
                strTitle.Append("");

                strTitle.Append("<div class='col-lg-12'>");
                strTitle.Append(" <!-- Tab Menu -->");
                strTitle.Append("	<nav class='user-tabs mb-4'>");
                strTitle.Append("      <ul class='nav nav-tabs nav-tabs-bottom'>");

                int i = 0;
                foreach (var mainAff in dataMain)
                {
                    strTitle.Append("<li class='nav-item'>");
                    strTitle.Append("   <a class='nav-link " + (i == 0 ? " active" : "") + "' href='#Tab" + mainAff.Id + "' data-toggle='tab'>" + mainAff.AffilationName + "</a>");
                    strTitle.Append("</li>");
                    i++;
                }
                i = 0;

                strTitle.Append("			</ul>");
                strTitle.Append("		</nav>");
                strTitle.Append("		<!-- /Tab Menu -->");
                strTitle.Append("		<!-- Tab Content -->");
                strTitle.Append("		<div class='tab-content'>");
                foreach (var mainAff in dataMain)
                {
                    string strImage = string.IsNullOrWhiteSpace(mainAff.ImagePath) ? "" : ResolveUrl(mainAff.ImagePath);
                    if (!string.IsNullOrWhiteSpace(mainAff.AffilationDescription))
                    {
                        strTitle.Append("<!--Overview Content-->");
                        strTitle.Append("<div role ='tabpanel' id='Tab" + mainAff.Id + "' class='tab-pane fade " + (i == 0 ? " active show" : "") + "'>");
                        if (string.IsNullOrWhiteSpace(mainAff.ImagePath))
                        {
                            strTitle.Append(HttpUtility.HtmlDecode(mainAff.AffilationDescription).Replace("/hospital/", "").Replace("src=\"..", "src=\"").Replace("src=\"assets/", "src=\"../hospital/assets/"));
                        }
                        else
                        {

                            strTitle.Append("<div class='row'>");
                            strTitle.Append("    <div class='col-md-8 col-lg-8'>");
                            strTitle.Append(HttpUtility.HtmlDecode(mainAff.AffilationDescription).Replace("/hospital/", "").Replace("src=\"..", "src=\"").Replace("src=\"assets/", "src=\"../hospital/assets/"));
                            strTitle.Append("    </div>");

                            strTitle.Append("    <div class='col-md-4 col-lg-4'>");

                            strTitle.Append("<div class='img_effect'><img class='img-fluid' src='" + strImage + "'></div>");

                            strTitle.Append("    </div>");
                            strTitle.Append("</div>");
                        }
                        strTitle.Append("</div>");
                    }
                    i++;
                }
                strTitle.Append("		</div>");
                strTitle.Append("</div>");

            }
            string strFinalString = strTitle.ToString() + strTabHead.ToString() + strMid.ToString() + strTabDettails.ToString() + strEnd.ToString();
            return strFinalString;
        }
    }
}