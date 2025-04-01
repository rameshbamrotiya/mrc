using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class InfoMSRClause : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfTableDescription;
        public static string strTotalHeadli;
        public static string strAccordHeadli;
        public static string strScript;
        public static string strDate, strDate2;
        public static string strQuickLink;
        public static string strClinicalMaterialMasterDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strListOfTableDescription = GetListOfSubSectionDescription();
                strScript = GetScript();
                strTotalHeadli = GetTotalHeadLi();
                strAccordHeadli = GetstrAccordHeadli();
                strQuickLink = Functions.CreateQuickLink("Home", "InfoMSRClause");
                strDate2 = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                strClinicalMaterialMasterDetails = GetClinicalMaterialMasterDetailsData();
            }
        }

        private string GetstrAccordHeadli()
        {
            StringBuilder strTotalHead = new StringBuilder();
            int rowCount = 0;
            strTotalHead.Append("");
            using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
            {
                var Data = objAcc.GetAllDailyVisitEntry();
                var DataCat = objAcc.GetAllDailyVisitCategory();
                foreach (var cat in DataCat.OrderBy(x => x.Id).ToList())
                {
                    var dateList = Data.Where(x => x.DailyCatId == cat.Id && x.IsVisable==true && !string.IsNullOrWhiteSpace(x.PDFFileName.ToString())).ToList().Distinct().OrderByDescending(x => x.EntryDate).Take(10);
                    if (dateList.Count() > 0)
                    {
                        strTotalHead.Append("<li class='accordion block'>");
                        strTotalHead.Append("    <div class='acc-btn'>");
                        strTotalHead.Append("        <div class='icon-outer'></div>");
                        strTotalHead.Append("        <h6>" + cat.DailyCatagoryName + "</h6>");
                        strTotalHead.Append("    </div>");


                        strTotalHead.Append("    <div class='acc-content'>");
                        strTotalHead.Append("        <div class='row'>");
                        foreach (var row in dateList)
                        {

                            string strURL = "", strFileName = "";

                            if (!string.IsNullOrWhiteSpace(row.PDFFileName.ToString()))
                            {
                                strURL = (row.PDFFileName);
                                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                {
                                    strURL = strURL.Replace("~/", "");
                                    strURL = ResolveUrl("~/" + strURL);
                                }

                                //strFileName = (row.FileName);
                                strFileName = ResolveUrl("~/Hospital/assets/img/pdffilen.png");
                                if (strFileName.StartsWith("~/", StringComparison.Ordinal))
                                {
                                    strFileName = strFileName.Replace("~/", "");
                                    strFileName = ResolveUrl("~/" + strFileName);
                                }
                                

                                strTotalHead.Append("            <div class='col-xl-3 col-lg-4 col-md-6 col-12 menu-item'>");
                                strTotalHead.Append("                <div class='departments-box-layout1'>");
                                strTotalHead.Append("                    <div class='item-img'>");
                                strTotalHead.Append("                        <img src='" + strFileName + "' alt='department' class='img-fluid'>");
                                strTotalHead.Append("                        <div class='item-btn-wrap'>");
                                strTotalHead.Append("                            <a href='" + strURL + "' target='_blank'");
                                strTotalHead.Append("                                class='item-btn'>Click</a>");
                                strTotalHead.Append("                        </div>");
                                strTotalHead.Append("                    </div>");
                                strTotalHead.Append("                    <div class='item-content'>");
                                strTotalHead.Append("                        <h5 class='item-title'>");
                                strTotalHead.Append("                            <a href='#'>" + row.EntryName + "</a>");
                                strTotalHead.Append("                        </h5>");
                                strTotalHead.Append("                    </div>");
                                strTotalHead.Append("                </div>");
                                strTotalHead.Append("            </div>");
                            }
                        }
                        strTotalHead.Append("        </div>");
                        strTotalHead.Append("    </div>");
                        strTotalHead.Append("</li>");
                        rowCount++;
                    }
                }
            }

            if(rowCount==0)
            {
                dvAttandance.Visible = false;
                dvAttandance.Style["display"] = "none";
            }
            else
            {
                dvAttandance.Visible = true ;
                dvAttandance.Style["display"] = "block";

            }

            return strTotalHead.ToString();
        }

        private string GetTotalHeadLi()
        {
            StringBuilder strTotalHead = new StringBuilder();

            strTotalHead.Append("");
            using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
            {
                DateTime dtFromDate = DateTime.Now;
                Remove: dtFromDate = dtFromDate.AddDays(-1);
                var Data = objAcc.GetAllDailyVisitEntry().Where(x => x.IsVisable==true && x.EntryDate.Value.ToShortDateString() == dtFromDate.ToShortDateString()).ToList();
                if (Data != null)
                {
                    //if (Data.EntryDate != null)
                    {
                        var DataCat = objAcc.GetAllDailyVisitCategory();
                        foreach (var cat in DataCat.OrderBy(x => x.Id).ToList())
                        {
                            long lgCount = 0;
                            var dataDetails = Data.Where(x => x.DailyCatId == cat.Id).FirstOrDefault();
                            if (dataDetails != null)
                            {
                                lgCount = (long)dataDetails.VisitCount;
                            }
                            strTotalHead.Append("<li class='nav-item'>");
                            strTotalHead.Append("    <a class='nav-link'>");
                            strTotalHead.Append("        <div class='counter-item one'>");
                            strTotalHead.Append("            <h2 class='number rs-count kplus'>" + cat.DailyCatagoryName + "</h2>");
                            strTotalHead.Append("            <h4 class='title mb-0'>Total: " + lgCount + "</h4>");
                            strTotalHead.Append("        </div>");
                            strTotalHead.Append("    </a>");
                            strTotalHead.Append("</li>");
                        }
                    }
                }
                else
                {
                    // goto Remove;
                }
            }

            return strTotalHead.ToString();
        }

        private string GetScript()
        {

            using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
            {
                var DataCat = objAcc.GetAllDailyVisitCategory();
                var dateList = objAcc.GetAllDailyVisitEntry().Where(x => x.IsVisable == true).Select(x => x.EntryDate).ToList().Distinct().OrderByDescending(x => x.Value).Take(7).ToList();

                    var dataL = objAcc.GetAllDailyVisitEntry().ToList();
                List<GetAllDailyEntryVisitMasterResult> lstData = new List<GetAllDailyEntryVisitMasterResult>();
                foreach(var dates in dateList)
                {
                    if(dataL.Where(x => dates.Value == (x.EntryDate)).Count()>0)
                    {
                        lstData.AddRange(dataL.Where(x => dates.Value == (x.EntryDate)));
                    }
                }

                GetAllStatisticsChartMasterResult chartMainDetails = new GetAllStatisticsChartMasterResult();
                chartMainDetails.XValueName = "Date";
                chartMainDetails.YValueName = "Number of Visits";
                chartMainDetails.XValueFormate = "DD MMM, YYYY";

                List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails = new List<GetAllStatisticsChartMasterColumnListByChartIdResult>();
                chartColumnDetails.Add(new GetAllStatisticsChartMasterColumnListByChartIdResult
                {
                    Id = 1,
                    SequanceNo = 1,
                    TypeColumn = "X",
                    ColName = "Date"
                });

                int i = 2;
                foreach (var cat in DataCat.OrderBy(x => x.Id).ToList())
                {
                    chartColumnDetails.Add(new GetAllStatisticsChartMasterColumnListByChartIdResult
                    {
                        Id = i,
                        SequanceNo = i,
                        TypeColumn = "Y",
                        ColName = cat.DailyCatagoryName
                    });
                    i++;
                }

                List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated = new List<GetAllStatisticsChartMasterDetailsByChartIdResult>();
                int index = 1;
                foreach (var row in dateList)
                {

                    dataRelated.Add(new GetAllStatisticsChartMasterDetailsByChartIdResult
                    {
                        ColumnId = 1,
                        ColumnName = "Date",
                        ColumnValue = row.Value.ToString(),
                        SequanceNo = index,
                        TypeColumn = "X"
                    });

                    int j = 2;
                    foreach (var cat in DataCat.OrderBy(x => x.Id).ToList())
                    {
                        var dataIn = lstData.FirstOrDefault(x => x.DailyCatId == cat.Id && x.EntryDate == row.Value);

                        dataRelated.Add(new GetAllStatisticsChartMasterDetailsByChartIdResult
                        {
                            ColumnId = j,
                            ColumnName = cat.DailyCatagoryName,
                            ColumnValue = (dataIn == null ? 0 : dataIn.VisitCount).ToString(),
                            SequanceNo = index,
                            TypeColumn = "Y"
                        });
                        j++;
                    }
                    index++;
                }

                StringBuilder strChartScript = new StringBuilder();
                strChartScript.Append("");


                strChartScript.Append("window.onload = function () {                            ");

                strChartScript.Append("var chart = new CanvasJS.Chart(\"" + chartContainer.ClientID + "\", {     ");
                strChartScript.Append("	animationEnabled: true,                                 ");
                strChartScript.Append("	theme: \"light2\",                                      ");
                strChartScript.Append("	title: {                                                ");
                strChartScript.Append("		text: \"DAILY ATTENDANCE DASHBOARD\"");
                strChartScript.Append("	},                                                      ");
                strChartScript.Append("	axisX: {                                                ");
                strChartScript.Append("	},                                                      ");

                if (!string.IsNullOrWhiteSpace(chartMainDetails.XValueName))
                {
                    strChartScript.Append("	axisX : {");
                    strChartScript.Append("		intervalType: \"day\",interval: 1, title: \"" + chartMainDetails.XValueName + "\",  ");
                    strChartScript.Append("		title: \"" + chartMainDetails.XValueName + "\",                                  ");
                    strChartScript.Append("	    valueFormatString: \"DD MMM\",                   ");
                    strChartScript.Append("		crosshair: {                                                                     ");
                    strChartScript.Append("			enabled: true,                                                               ");
                    strChartScript.Append("			snapToDataPoint: true                                                        ");
                    strChartScript.Append("		}                                                                                ");
                    strChartScript.Append("	},                                                                                   ");
                }

                if (!string.IsNullOrWhiteSpace(chartMainDetails.YValueName))
                {
                    strChartScript.Append("	axisY : {");
                    strChartScript.Append("		title: \"" + chartMainDetails.YValueName + "\",                 ");
                    strChartScript.Append("		includeZero: true,                                  ");
                    strChartScript.Append("		crosshair: {                                        ");
                    strChartScript.Append("			enabled: true                                   ");
                    strChartScript.Append("		}                                                   ");
                    strChartScript.Append("	},                                                   ");

                }

                strChartScript.Append("	toolTip: {                                              ");
                strChartScript.Append("		shared: true                                        ");
                strChartScript.Append("	},                                                      ");
                strChartScript.Append("	legend: {                                               ");
                strChartScript.Append("		cursor: \"pointer\",                                ");
                strChartScript.Append("		verticalAlign: \"bottom\",                          ");
                strChartScript.Append("		horizontalAlign: \"center\",                          ");
                strChartScript.Append("		dockInsidePlotArea: false,                           ");
                strChartScript.Append("		itemclick: toogleDataSeries                         ");
                strChartScript.Append("	},                                                      ");

                List<MainLine> lstMainLine = new List<MainLine>();



                foreach (var yAxis in chartColumnDetails.Where(x => x.TypeColumn == "Y"))
                {
                    MainLine objData = new MainLine();

                    objData.type = "line";
                    objData.name = yAxis.ColName;
                    objData.showInLegend = true;
                    objData.xValueFormatString = "DD MMM, YYYY";
                    objData.dataPoints = new List<SubDetails>();

                    foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
                    {

                        var xColDetails = chartColumnDetails.Where(x => x.TypeColumn == "X").FirstOrDefault();

                        var sequanceData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == yAxis.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();
                        var sequancexData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == xColDetails.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();

                        SubDetails objSubData = new SubDetails();

                        objSubData.y = Convert.ToInt32(sequanceData.ColumnValue);

                        IFormatProvider culture = new CultureInfo("en-US", true);
                        //DateTime dateVal = DateTime.ParseExact(sequancexData.ColumnValue, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                        string dateVal = "";
                        dateVal= Convert.ToDateTime(sequancexData.ColumnValue).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        objSubData.x = Convert.ToDateTime(dateVal);

                        objData.dataPoints.Add(objSubData);

                    }

                    lstMainLine.Add(objData);

                }
                //JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };

                //var strDataa = JsonConvert.SerializeObject(lstMainLine, Formatting.Indented, config);
                StringBuilder strDataa = new StringBuilder();
                //var strDataa1 = JsonConvert.SerializeObject(lstMainLine,Formatting.None);
                int mainIndex = 0;
                strDataa.Append("[");
                foreach (var objMainLine in lstMainLine)
                {

                    if (mainIndex != 0)
                    {
                        strDataa.Append("  ,");
                    }
                    strDataa.Append("  {");
                    strDataa.Append("    type: \"" + objMainLine.type + "\",");
                    strDataa.Append("    name:  \"" + objMainLine.name + "\",");
                    strDataa.Append("    showInLegend:  \"" + objMainLine.showInLegend + "\",");
                    if (mainIndex == 0)
                    {
                        strDataa.Append("    xValueFormatString: \"" + objMainLine.xValueFormatString + "\",");
                    }
                    strDataa.Append("    dataPoints: [");

                    int subIndex = 0;
                    foreach (var objsubLine in objMainLine.dataPoints)
                    {

                        if (subIndex != 0)
                        {
                            strDataa.Append("      ,");
                        }
                        strDataa.Append("      {");
                        strDataa.Append("        y: " + objsubLine.y + ",");
                        strDataa.Append("        x: new Date(" + objsubLine.x.Year + ", " + objsubLine.x.Month + ", " + objsubLine.x.Day + ")");
                        strDataa.Append("      }");

                        subIndex++;

                    }
                    strDataa.Append("    ]");
                    strDataa.Append("      }");

                    mainIndex++;
                }
                strDataa.Append("    ]");

                strChartScript.Append("	data: " + strDataa.ToString() + "                                   ");
                strChartScript.Append("});                                                      ");
                strChartScript.Append("chart.render();                                          ");


                strChartScript.Append("}");

                string str = strChartScript.ToString();
                return str;
            }
        }

        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strMSRTableDescription = new StringBuilder();
            DataSet ds = new MSRClauseBAL().SelectRecordFront(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    strMSRTableDescription.Append("<div class='table-responsive'>");
                    strMSRTableDescription.Append("<table class='table table-hover table-center mb-0 maintable'>");
                    strMSRTableDescription.Append("<thead>");
                    strMSRTableDescription.Append("<tr>");
                    strMSRTableDescription.Append("<th>Sr No</th>");
                    strMSRTableDescription.Append("<th>Particular</th>");
                    strMSRTableDescription.Append("<th>Link</th>");
                    strMSRTableDescription.Append("</tr>");
                    strMSRTableDescription.Append("</thead>");
                    strMSRTableDescription.Append("<tbody>");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (ds.Tables[0].Rows.Count == i)
                        {
                            strDate = row["EntryDate"].ToString();
                        }

                        strMSRTableDescription.Append("<tr>");
                        strMSRTableDescription.Append("<td>" + i + "</td>");
                        strMSRTableDescription.Append("<td>" + row["Particulars"] + "</td>");
                        strMSRTableDescription.Append("<td class='text-center'>");
                        strMSRTableDescription.Append("<div class='table-action'>");
                        strMSRTableDescription.Append("<a href='" + ResolveUrl(row["ImagePath"].ToString()) + "' target='_blank' class='btn btn-sm bg-info-light'>");
                        strMSRTableDescription.Append("<i class='far fa-eye'></i>View");
                        strMSRTableDescription.Append("</a>");
                        strMSRTableDescription.Append("</div>");
                        strMSRTableDescription.Append("</td>");
                        strMSRTableDescription.Append("</tr>");
                        i++;
                    }
                    strMSRTableDescription.Append("</tbody>");
                    strMSRTableDescription.Append("</table>");
                    strMSRTableDescription.Append("</div>");
                }
            }
            return strMSRTableDescription.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "InfoMSRClause").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "InfoMSRClause").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strcontactus.ToString();
        }

        private string GetClinicalMaterialMasterDetailsData()
        {
            try
            {
                int languageId = Functions.LanguageId;
                StringBuilder strClinicalMaterialMaster = new StringBuilder();
                ClinicalMaterialMasterBO objbo = new ClinicalMaterialMasterBO();
                objbo.LanguageId = languageId;
                DataSet ds = new ClinicalMaterialMasterBAL().GetClinicalMaterialMasterDetails(objbo);
                if (ds != null)
                {
                    strClinicalMaterialMaster = new StringBuilder();
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        strClinicalMaterialMaster.Append(HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["Description"].ToString()));
                    }
                }

                return strClinicalMaterialMaster.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }
    }
}