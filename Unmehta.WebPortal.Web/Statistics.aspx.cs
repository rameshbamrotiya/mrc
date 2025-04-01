using BAL;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Admin.Admission;
using Unmehta.WebPortal.Web.Admin.CMS;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class Statistics : System.Web.UI.Page
    {
        public static string strEMCSChartTable, strHeaderImage;
        public static string strSchemaChartTable;
        public static string strDepartmentAndOtherChartTable;
        public static List<long> lstLong = new List<long>();
        public static string strStatisticsMain;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long index = 1;
                strHeaderImage = GetHeaderImage();

                strStatisticsMain = "";

                strEMCSChartTable = GetAllEMCSTables(ref index);
                strSchemaChartTable = GetAllSchemaTable(ref index);
                strDepartmentAndOtherChartTable = GetAllOtherCharts(ref index);

                string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                //rowURl = rowURl.Substring(1);
                StringBuilder strResearch = new StringBuilder();
                ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
                if (IsDisabledTranslate && Functions.LanguageId != 1)
                {
                    {
                        Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                        strStatisticsMain = strResearch.ToString();
                    }
                }
                else
                {

                    StringBuilder strStatistics = new StringBuilder();
                    strStatistics.Append("");
                    strStatistics.Append("  <div class='col-lg-12'>");
                    strStatistics.Append("		<nav class='user-tabs mb-4'>");
                    strStatistics.Append("			<ul class='nav nav-tabs nav-tabs-bottom'>");
                    strStatistics.Append("				<li class='nav-item'>");
                    strStatistics.Append("					<a class='nav-link active' href='#dap_Faculty_1' data-toggle='tab'>Data Summary</a>");
                    strStatistics.Append("				</li>");
                    strStatistics.Append("				<li class='nav-item'>");
                    strStatistics.Append("					<a class='nav-link ' href='#dap_introduction_1' data-toggle='tab'>EMCS Scheme</a>");
                    strStatistics.Append("				</li>");
                    strStatistics.Append("				<li class='nav-item'>");
                    strStatistics.Append("					<a class='nav-link' href='#dap_Services_1' data-toggle='tab'>Patient Benevolent Scheme Data</a>");
                    strStatistics.Append("				</li>");
                    strStatistics.Append("			</ul>");
                    strStatistics.Append("		</nav>	");
                    strStatistics.Append("		<div class='tab-content'>");
                    strStatistics.Append("			<div role = 'tabpanel' id='dap_introduction_1' class='tab-pane fade '>");
                    strStatistics.Append("				<div class='row'>");
                    strStatistics.Append("					<div class='table-responsive'>");
                    strStatistics.Append("                       " + strEMCSChartTable);
                    strStatistics.Append("					</div>");
                    strStatistics.Append("				</div>");
                    strStatistics.Append("			</div>");
                    strStatistics.Append("			<div role = 'tabpanel' id='dap_Services_1' class='tab-pane fade'>");
                    strStatistics.Append("				<div class='accordion md-accordion accordion-blocks' id='accordionscheme' role='tablist' aria-multiselectable='true'>	");
                    strStatistics.Append("                        " + strSchemaChartTable);
                    strStatistics.Append("				</div>");
                    strStatistics.Append("			</div>");
                    strStatistics.Append("			<div role = 'tabpanel' id='dap_Faculty_1' class='tab-pane fade active show'>");
                    strStatistics.Append("				<div class='accordion md-accordion accordion-blocks' id='accordionschemedata' role='tablist' aria-multiselectable='true'>	");
                    strStatistics.Append("                        " + strDepartmentAndOtherChartTable);
                    strStatistics.Append("				</div>");
                    strStatistics.Append("			</div>");
                    strStatistics.Append("		</div>");
                    strStatistics.Append("	</div>");

                    strStatisticsMain = strStatistics.ToString();
                }

            }
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Statistics").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Statistics").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        private string GetAllOtherCharts(ref long index)
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IDepartmentTabRepository objBlogCategoryMasterRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            using (IStatisticsChartRepository objPatientsEducationBrochureRepositorys = new StatisticsChartRepository(Functions.strSqlConnectionString))
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                List<OurExcellenceMasterGridModel> dataListAward = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(languageId);
                {
                    if (dataListAward != null)
                    {
                        LableData:
                        if (dataListAward.Count > 0)
                        {
                            foreach (var row in dataListAward)
                            {
                                var tabList = objBlogCategoryMasterRepository.GetAllDepartmentTabList((long)row.Id, languageId);

                                StringBuilder strSubTableDetails = new StringBuilder();
                                if (tabList.Count() > 0)
                                {
                                    foreach (var tab in tabList)
                                    {
                                        var dataTabDetails = objBlogCategoryMasterRepository.GetAllDeparmentTabDetailListByTabId(tab.Id, languageId).Where(x => x.StatasticId > 0).OrderBy(x => x.SequanceNo).ToList();
                                        if (dataTabDetails.Count() > 0)
                                        {

                                            strAwardsAndAchievements.Append("<div class='card'>");
                                            strAwardsAndAchievements.Append("	<div class='card-header' role='tab' id='heading" + index + "'>");
                                            strAwardsAndAchievements.Append("		<a data-toggle='collapse' data-parent='#accordionschemedata' href='#collapsse" + index + "' aria-expanded='true' aria-controls='collapse" + index + "'>");
                                            strAwardsAndAchievements.Append("			<div class='job_filter_sidebar_heading jb_cover'>");
                                            strAwardsAndAchievements.Append("				<div class='hradtitle'>");
                                            strAwardsAndAchievements.Append("					<h1>" + row.DepartmentName + "</h1>");
                                            strAwardsAndAchievements.Append("				</div> <div class=\"datewalkin\"> <span><i class=\"fas fa-angle-down rotate-icon\"></i></span> </div>");
                                            strAwardsAndAchievements.Append("			</div>");
                                            strAwardsAndAchievements.Append("		</a>");
                                            strAwardsAndAchievements.Append("	</div>");
                                            strAwardsAndAchievements.Append("	<div id='collapsse" + index + "' class='collapse' role='tabpanel' aria-labelledby='heading" + index + "' data-parent='#accordionschemedata'>");

                                            foreach (var dataTab in dataTabDetails)
                                            {
                                                string strHtml = "";
                                                Functions.GenerateScriptGraph((long)dataTab.StatasticId, "chartContainer", out strHtml, true);

                                                var chartname = objPatientsEducationBrochureRepositorys.GetStatisticsChartById((long)dataTab.StatasticId).ChartName;

                                                if (!string.IsNullOrWhiteSpace(chartname))
                                                {
                                                    strAwardsAndAchievements.Append("		<div class='card-body'>");
                                                    strAwardsAndAchievements.Append("			<div class='row'>");
                                                    strAwardsAndAchievements.Append(" <h4 class='widget-title'> " + chartname + "</h4>");
                                                    strAwardsAndAchievements.Append("				<div class='table-responsive text-center'>");
                                                    strAwardsAndAchievements.Append("<br>" + strHtml);
                                                    strAwardsAndAchievements.Append("				</div>");
                                                    strAwardsAndAchievements.Append("			</div>");
                                                    strAwardsAndAchievements.Append("		</div>");
                                                }

                                                lstLong.Add((long)dataTab.StatasticId);

                                            }

                                            strAwardsAndAchievements.Append("	</div>");
                                            strAwardsAndAchievements.Append("</div>");

                                        }
                                    }
                                }
                                index++;
                            }
                        }
                    }

                    using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
                    {
                        var OtherChartData = objPatientsEducationBrochureRepository.GetAllStatisticsChart().Where(x => !lstLong.Contains(x.Id) && (x.IsActive == true || x.IsActive == null)).OrderBy(x => x.SequanceNo).ToList();
                        if (OtherChartData != null)
                        {
                            foreach (var row in OtherChartData)
                            {

                                {
                                    strAwardsAndAchievements.Append("<div class='card'>");
                                    strAwardsAndAchievements.Append("	<div class='card-header' role='tab' id='heading" + index + "'>");
                                    strAwardsAndAchievements.Append("		<a data-toggle='collapse' data-parent='#accordionschemedata' href='#collapse" + index + "' aria-expanded='true' aria-controls='collapse" + index + "'>");
                                    strAwardsAndAchievements.Append("			<div class='job_filter_sidebar_heading jb_cover'>");
                                    strAwardsAndAchievements.Append("				<div class='hradtitle'>");
                                    strAwardsAndAchievements.Append("					<h1>" + row.ChartName + "</h1>");
                                    strAwardsAndAchievements.Append("				</div> <div class=\"datewalkin\"> <span><i class=\"fas fa-angle-down rotate-icon\"></i></span> </div>");
                                    strAwardsAndAchievements.Append("			</div>");
                                    strAwardsAndAchievements.Append("		</a>");
                                    strAwardsAndAchievements.Append("	</div>");
                                    strAwardsAndAchievements.Append("	<div id='collapse" + index + "' class='collapse' role='tabpanel' aria-labelledby='heading" + index + "' data-parent='#accordionschemedata'>");
                                    strAwardsAndAchievements.Append("		<div class='card-body'>");
                                    strAwardsAndAchievements.Append("			<div class='row'>");
                                    strAwardsAndAchievements.Append("				<div class='table-responsive text-center'>");
                                    string strHtml = "";

                                    Functions.GenerateScriptGraph((long)row.Id, "chartContainer", out strHtml, true);
                                    strAwardsAndAchievements.Append("<br>" + strHtml);

                                    strAwardsAndAchievements.Append("				</div>");
                                    strAwardsAndAchievements.Append("			</div>");
                                    strAwardsAndAchievements.Append("		</div>");
                                    strAwardsAndAchievements.Append("	</div>");
                                    strAwardsAndAchievements.Append("</div>");
                                }
                                index++;
                            }

                        }
                    }
                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetAllSchemaTable(ref long index)
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            strAwardsAndAchievements.Append("");
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);

                if (dataListAward != null)
                {
                    LableData: if (dataListAward.Count > 0)
                    {
                        foreach (var row in dataListAward)
                        {
                            strAwardsAndAchievements.Append("<div class='card'>");
                            strAwardsAndAchievements.Append("	<div class='card-header' role='tab' id='heading" + index + "'>");
                            strAwardsAndAchievements.Append("		<a data-toggle='collapse' data-parent='#accordionscheme' href='#collapse" + index + "' aria-expanded='true' aria-controls='collapse" + index + "'>");
                            strAwardsAndAchievements.Append("			<div class='job_filter_sidebar_heading jb_cover'>");
                            strAwardsAndAchievements.Append("				<div class='hradtitle'>");
                            strAwardsAndAchievements.Append("					<h1>" + row.SchemeName + "</h1>");
                            strAwardsAndAchievements.Append("				</div> <div class=\"datewalkin\"> <span><i class=\"fas fa-angle-down rotate-icon\"></i></span> </div>");
                            strAwardsAndAchievements.Append("			</div>");
                            strAwardsAndAchievements.Append("		</a>");
                            strAwardsAndAchievements.Append("	</div>");
                            strAwardsAndAchievements.Append("	<div id='collapse" + index + "' class='collapse " + (index == 1 ? "show" : "") + " ' role='tabpanel' aria-labelledby='heading" + index + "' data-parent='#accordionscheme'>");
                            strAwardsAndAchievements.Append("		<div class='card-body'>");
                            strAwardsAndAchievements.Append("			<div class='row'>");
                            strAwardsAndAchievements.Append("				<div class='table-responsive text-center'>");
                            string strHtml = "";

                            var data = objHomePageRepository.GetAllSchemaChart(row.Id);

                            if (data != null)
                            {
                                if (data.Count() > 0)
                                {
                                    foreach (var rows in data)
                                    {

                                        {
                                            Functions.GenerateScriptGraph((long)rows.ChartId, "chartContainer", out strHtml, true);

                                            var chartname = objPatientsEducationBrochureRepository.GetStatisticsChartById((long)rows.ChartId).ChartName;

                                            if (!string.IsNullOrWhiteSpace(chartname))
                                            {
                                                strAwardsAndAchievements.Append("		<div class='card-body'>");
                                                strAwardsAndAchievements.Append("			<div class='row'>");
                                                strAwardsAndAchievements.Append(" <h4 class='widget-title'> " + chartname + "</h4>");
                                                strAwardsAndAchievements.Append("				<div class='table-responsive text-center'>");
                                                strAwardsAndAchievements.Append("<br>" + strHtml);
                                                strAwardsAndAchievements.Append("				</div>");
                                                strAwardsAndAchievements.Append("			</div>");
                                                strAwardsAndAchievements.Append("		</div>");
                                            }

                                            lstLong.Add((long)rows.ChartId);

                                        }
                                        
                                    }
                                }
                                else
                                {
                                    strAwardsAndAchievements.Append("<p> No Data available.. </p>");
                                }
                            }
                            else
                            {
                                strAwardsAndAchievements.Append("<p> No Data available.. </p>");
                            }
                            strAwardsAndAchievements.Append("				</div>");
                            strAwardsAndAchievements.Append("			</div>");
                            strAwardsAndAchievements.Append("		</div>");
                            strAwardsAndAchievements.Append("	</div>");
                            strAwardsAndAchievements.Append("</div>");
                            index++;
                        }
                    }
                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetAllEMCSTables(ref long index)
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            StringBuilder strChartScript = new StringBuilder();
            DataSet ds = new EMCSBAL().SelectRecordSidemenu(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        long StatisticsId = 0;
                        if (long.TryParse(row["StatisticsId"].ToString(), out StatisticsId))
                        {
                            if (StatisticsId > 0)
                            {
                                lstLong.Add(StatisticsId);
                                string strHtml = "";
                                Functions.GenerateScriptGraph(StatisticsId, "chartContainer" + index + "", out strHtml, true);
                                strAwardsAndAchievements.Append("<br>" + strHtml);
                            }
                        }
                        index++;
                    }
                }
            }
            return strAwardsAndAchievements.ToString();
        }


    }
}