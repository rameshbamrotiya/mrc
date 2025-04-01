using BAL;
using ClassLib.BO;
using System;
using System.Collections.Generic;
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
    public partial class EMCS : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strTabA,strTabC;
        public static string strScript;
        public static string FacilityInEMCS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strTabA = GetListOfSubSectionDescription();
                strTabC = GetStatestics();
                FacilityInEMCS = GetFacilityInEMCS();
            }
        }

        private string GetStatestics()
        {
            StringBuilder strChartScript = new StringBuilder();
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            long StatisticsId = 0;
            int languageId = Functions.LanguageId;
            DataSet ds = new EMCSBAL().SelectRecordSidemenu(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    DataRow row = ds.Tables[0].Rows[0];
                    if (long.TryParse(row["StatisticsId"].ToString(), out StatisticsId))
                    {
                        if (StatisticsId > 0)
                        {
                            string strHtml = "";
                            string strChart = Functions.GenerateScriptGraph(StatisticsId, "chartContainer", out strHtml, true);
                            strChartScript.Append(strChart);
                            strAwardsAndAchievements.Append("<br>" + strHtml);
                            strAwardsAndAchievements.Append("<br><div class=\"canvasbg\"></div> <div class=\"canvasbg_right\"></div> <div id='chartContainer' style='height: 400px; width: 100%; overflow: auto;'></div>'");
                        }
                    }
                }
            }
            strScript = strChartScript.ToString();
            return strAwardsAndAchievements.ToString();
        }

        private string GetFacilityInEMCS()
        {
            StringBuilder sbFacilityInEMCS = new StringBuilder();
            int languageId = Functions.LanguageId;
            DataSet ds = new EMCSBAL().SelectFacilityInECMSDetails(languageId);
            if (ds == null) return "";
            if (ds.Tables.Count <= 0) return "";
            if (ds.Tables[0].Rows.Count <= 0) return "";
            if (ds.Tables[1].Rows.Count <= 0) return "";

            DataTable dtMaster = new DataTable();
            DataTable dtDetails = new DataTable();

            List<lstFacilityMaster> lstMaster = new List<lstFacilityMaster>();
            lstMaster = Functions.ConvertDataTable<lstFacilityMaster>(ds.Tables[0]);

            List<lstFacilityMasterDetail> lstFacilityMasterDetail = new List<lstFacilityMasterDetail>();
            lstFacilityMasterDetail = Functions.ConvertDataTable<lstFacilityMasterDetail>(ds.Tables[1]);

            if (lstMaster.Count() > 0)
            {
                sbFacilityInEMCS.Append("<div class='widget about-widget'>");

                foreach (var Mainitem in lstMaster)
                {
                    sbFacilityInEMCS.Append("<div class='accordion-box'>");
                    sbFacilityInEMCS.Append("<div class='title-box'>");


                    sbFacilityInEMCS.Append("<h6>" + Mainitem.Title + "</h6>");
                    sbFacilityInEMCS.Append("</div>");
                    sbFacilityInEMCS.Append("<ul class='accordion-inner'>");
                    foreach (var subitem in lstFacilityMasterDetail)
                    {
                        if (subitem.FIEMID == Mainitem.FIEMID)
                        {
                            sbFacilityInEMCS.Append("<li class='accordion block'>");
                            sbFacilityInEMCS.Append("<div class='acc-btn'>");
                            sbFacilityInEMCS.Append("<div class='icon-outer'></div>");
                            sbFacilityInEMCS.Append("<h6> "+ subitem.Subtitle +"</h6>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("<div class='acc-content'>");
                            sbFacilityInEMCS.Append("<div class='row'>");
                            sbFacilityInEMCS.Append("<div class='col-lg-3'>");
                            sbFacilityInEMCS.Append("<div class='about-author'>");
                            sbFacilityInEMCS.Append("<div class='author-img-wrap'>");

                            if (!string.IsNullOrEmpty(subitem.ImageUrl))
                            {

                                sbFacilityInEMCS.Append("<a href = '#' ><img class='img-fluid' alt='' src='"+ ResolveUrl(subitem.ImageUrl) +"'></a>");
                            }
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("<div class='col-lg-9'>");
                            sbFacilityInEMCS.Append("    <div class='row'>");
                            sbFacilityInEMCS.Append("        <div class='col-lg-12'>");
                            sbFacilityInEMCS.Append("            <div class='wpb_wrapper'>");
                            sbFacilityInEMCS.Append("                " + HttpUtility.HtmlDecode(subitem.Description) + "");
                            sbFacilityInEMCS.Append("            </div>");
                            sbFacilityInEMCS.Append("        </div>");
                            sbFacilityInEMCS.Append("    </div>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("</div>");
                            sbFacilityInEMCS.Append("</li>");
                        }
                    }
                    sbFacilityInEMCS.Append("</ul>");
                    sbFacilityInEMCS.Append("</div>");
                }
                sbFacilityInEMCS.Append("</div>");
            }
            return sbFacilityInEMCS.ToString();

        }

        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            DataSet ds = new EMCSBAL().SelectRecordSidemenu(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    DataRow row = ds.Tables[0].Rows[0];
                    {
                       strAwardsAndAchievements.Append("<div class='row'>");
                        //Foreach For Images
                        foreach (DataRow subitem in ds.Tables[1].Rows)
                        {
                            string strURL = "";

                            if (!string.IsNullOrWhiteSpace(subitem["Img_path"].ToString()))
                            {
                                strURL = (subitem["Img_path"].ToString());
                                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                {
                                    strURL = strURL.Replace("~/", "");
                                    strURL = ResolveUrl("~/" + strURL);
                                }

                            }
                            strAwardsAndAchievements.Append("    <div class='col-lg-6'>");
                            strAwardsAndAchievements.Append("        <div class='gallery-box-layout1'>");
                            strAwardsAndAchievements.Append("            <img src='"+ strURL + "' alt='Feature' class='img-fluid'>");
                            strAwardsAndAchievements.Append("            <div class='item-icon'>");
                            strAwardsAndAchievements.Append("                <a href='" + strURL + "' data-fancybox='gallery' class='lightbox-image'>");
                            strAwardsAndAchievements.Append("                    <i class='fas fa-search-plus'></i>");
                            strAwardsAndAchievements.Append("                </a>");
                            strAwardsAndAchievements.Append("            </div>");
                            strAwardsAndAchievements.Append("        </div>");
                            strAwardsAndAchievements.Append("    </div>");
                        }
                       strAwardsAndAchievements.Append("</div>");

                        strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(row["EMCSDescription"].ToString()));
                        
                        i++;
                    }
                }
            }

            return strAwardsAndAchievements.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EMCS").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EMCS").FirstOrDefault();
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