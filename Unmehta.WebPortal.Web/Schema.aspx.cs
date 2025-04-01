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
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;


namespace Unmehta.WebPortal.Web
{
    public partial class Schema : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strDescription;
        public static string strSchemaName;
        public static string strSchemaList;
        public static string strChartTable;
        public static string strScript;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strScript = "";
                strHeaderImage = GetHeaderImage();
                liSchemaList.InnerHtml = GetSchemaList();
                dvDesc.InnerHtml = GetSchemaDescription();
            }
        }

        private string GetSchemaDescription()
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            StringBuilder strChartScript = new StringBuilder();
            long id;
            if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
            {
                using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
                {
                    var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);

                    if (dataListAward != null)
                    {
                        LableData: if (dataListAward.Count > 0)
                        {
                            var mainSchema = dataListAward.FirstOrDefault(x => x.Id == id);
                            strSchemaName = mainSchema.SchemeName;

                            dvBlogTitle.InnerHtml = strSchemaName;

                            if(string.IsNullOrWhiteSpace(mainSchema.ContactPerson) && string.IsNullOrWhiteSpace(mainSchema.HelpDeskNo) && string.IsNullOrWhiteSpace(mainSchema.location) && string.IsNullOrWhiteSpace(mainSchema.WebsiteUrl))
                            {
                                dvSubDetails.Visible = false;
                            }
                            else
                            {
                                dvSubDetails.Visible = true;
                                string websiteURL = mainSchema.WebsiteUrl == "" ? "-" : mainSchema.WebsiteUrl;
                                contactPersonName.InnerHtml = mainSchema.ContactPerson == "" ? "-" : mainSchema.ContactPerson;
                                contactPersonMobile.InnerHtml = mainSchema.HelpDeskNo == "" ? "-" : mainSchema.HelpDeskNo;
                                contactPersonLocation.InnerHtml = mainSchema.location == "" ? "-" : mainSchema.location;
                                contactPersonWebSite.InnerHtml = "<a href='" + mainSchema.WebsiteUrl + "'>" + websiteURL + "</a>";

                            }

                            ////if (mainSchema.ChartId != null)
                            ////{
                            ////    strScript = Functions.GenerateScriptGraph((long)mainSchema.ChartId, chartContainer.ClientID, out strChartTable);
                            ////}

                            //var chartMain = objHomePageRepository.GetAllStatisticsChartMaster().FirstOrDefault(x => x.Id == mainSchema.ChartId);

                            //var chartMainDetails = objHomePageRepository.GetAllStatisticsChartMasterDetails().Where(x => x.ChartId == mainSchema.ChartId).OrderBy(x => x.SequanceNo).ToList();

                            //if (chartMain != null && chartMainDetails != null)
                            //{
                            //    strAwardsAndAchievements.Append("<table class='table table-hover table-center mb-0 maintable text-center' cellspacing='0' rules='all' border='1' id='body_gvChartDetails' style='border-collapse:collapse;'>");
                            //    strAwardsAndAchievements.Append("	<thead>");

                            //    strAwardsAndAchievements.Append("<tr>");
                            //    strAwardsAndAchievements.Append("<th>Sr No.</th>");
                            //    strAwardsAndAchievements.Append("<th>" + chartMain.XColName + "</th>");
                            //    strAwardsAndAchievements.Append("<th>" + chartMain.YColName + "</th>");
                            //    strAwardsAndAchievements.Append("</tr>");

                            //    strAwardsAndAchievements.Append("	</thead></tbody>");
                            //    strAwardsAndAchievements.Append("<tr>");


                            //    //strChartScript.Append("");
                            //    //strChartScript.Append("window.onload = function () {");
                            //    //strChartScript.Append("");
                            //    //strChartScript.Append("var chart = new CanvasJS.Chart('chartContainer', {");
                            //    //strChartScript.Append("	animationEnabled: true,");
                            //    //strChartScript.Append("	theme: 'light2', ");// 'light1', 'light2', 'dark1', 'dark2'
                            //    //strChartScript.Append("	title:{");
                            //    //strChartScript.Append("		text: '" + chartMain.ChartName + "'");
                            //    //strChartScript.Append("	},");
                            //    //strChartScript.Append("	axisY: {");
                            //    //strChartScript.Append("		title: '" + chartMain.YColName + "'");
                            //    //strChartScript.Append("	},");
                            //    //strChartScript.Append("	data: [{        ");
                            //    //strChartScript.Append("		type: 'column',  ");
                            //    //strChartScript.Append("		showInLegend: true, ");
                            //    //strChartScript.Append("		legendMarkerColor: 'grey',");
                            //    //strChartScript.Append("		legendText: '" + chartMain.XColName + "',");
                            //    //strChartScript.Append("		dataPoints: [      ");
                            //    long i = chartMainDetails.Count() - 1;
                            //    foreach (var row in chartMainDetails)
                            //    {
                            //        //strChartScript.Append("			{ y: " + row.ColumnValue + ", label: '" + row.ColumnName + "' }" + (i == 0 ? "" : ","));

                            //        strAwardsAndAchievements.Append("<tr><td>" + row.SequanceNo + "</td>");
                            //        strAwardsAndAchievements.Append("<td> " + row.ColumnName + "</td> ");
                            //        strAwardsAndAchievements.Append("<td> " + row.ColumnValue + "</td></tr>");
                            //        i--;
                            //    }


                            //    strAwardsAndAchievements.Append("</tr>");
                            //    strAwardsAndAchievements.Append("</tbody></table>");

                            //    //strChartScript.Append("		]");
                            //    //strChartScript.Append("	}]");
                            //    //strChartScript.Append("});");
                            //    //strChartScript.Append("chart.render();");
                            //    //strChartScript.Append("");
                            //    //strChartScript.Append("}");

                            //    strChartTable = strAwardsAndAchievements.ToString();
                            //    strAwardsAndAchievements.Clear();
                            //    if (chartMainDetails.Count() > 0)
                            //    {
                            //    }
                            //}
                            //else
                            //{
                            //    strChartTable = "";
                            //    strScript = "";
                            //}


                            long lgChartCount = 0;
                            using (IStatisticsChartRepository objPatientsEducationBrochureRepositorys = new StatisticsChartRepository(Functions.strSqlConnectionString))
                            {

                                var data = objHomePageRepository.GetAllSchemaChart(mainSchema.Id);

                                if (data != null)
                                {
                                    if (data.Count() > 0)
                                    {
                                        foreach (var rows in data)
                                        {


                                            var chartname = objPatientsEducationBrochureRepositorys.GetStatisticsChartById((long)rows.ChartId).ChartName;

                                            string strTable = "";
                                            string strChartJs = "\r\n" + Functions.GenerateScriptGraph((long)rows.ChartId, "chartContainer" + lgChartCount + "", out strTable, true);

                                            strChartScript.Append(" <div class='state-body'>");
                                            strChartScript.Append(" <h4 class='widget-title'> " + chartname + "</h4>");
                                            strChartScript.Append("	<div class='table-responsive'>");
                                            strChartScript.Append(strTable);
                                            strChartScript.Append("	</div>");
                                            strChartScript.Append("	<br><br>");
                                            strChartScript.Append("	<div class=\"canvasbg\"></div> <div class=\"canvasbg_right\"></div> <div id='chartContainer" + lgChartCount + "' style='height: 400px;position: relative; width: 100%; overflow: auto;'></div>");
                                            strChartScript.Append("</div><br>");

                                            strScript += (strChartJs);

                                            lgChartCount++;
                                        }
                                    }
                                }
                            }

                            strChartTable = strChartScript.ToString();

                            string strDesc = HttpUtility.HtmlDecode(mainSchema.Description);
                            strAwardsAndAchievements.Append(strDesc);

                            if (!string.IsNullOrWhiteSpace(mainSchema.SchemeLogo))
                            {
                                imgLogo.Visible = true;
                                imgLogo.Src = ResolveUrl(mainSchema.SchemeLogo);
                            }
                            else
                            {
                                imgLogo.Visible = false;
                            }
                        }
                        else
                        {
                            if (Functions.LanguageId != 1 && languageId != 1)
                            {
                                languageId = 1;
                            dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);
                                goto LableData;
                            }
                        }
                    }

                }
            }
            else
            {
                Response.Redirect("~/SchemaDetails");
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetSchemaList()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);

                if (dataListAward != null)
                {
                    LableData: if (dataListAward.Count > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.Schemebanner) ? "" : row.Schemebanner);
                            string strURL = ResolveUrl(("~/Schema?" + Functions.Base64Encode(row.Id.ToString())));
                            string strDesc = HttpUtility.HtmlDecode(row.Description);
                            strAwardsAndAchievements.Append("<li><a href='" + strURL + "'>" + row.SchemeName + "</a></li>");
                            index++;
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);
                            goto LableData;
                        }
                    }
                }

                return strAwardsAndAchievements.ToString();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Schema").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Schema").FirstOrDefault();
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