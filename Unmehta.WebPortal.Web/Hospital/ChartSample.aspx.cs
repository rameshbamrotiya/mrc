using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Hospital
{
    public partial class ChartSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllSubDetail()
        {
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated = objPatientsEducationBrochureRepository.GetAllStatisticsChartDetailsByChartId(3).ToList();

                List<string> lstXString = new List<string>();
                List<string> lstYString = new List<string>();

                foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
                {

                    var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

                    foreach (var subRow in sequanceData)
                    {
                        if(subRow.TypeColumn=="X")
                        {
                            lstXString.Add(subRow.ColumnValue);
                        }
                        else
                        {
                            lstYString.Add(subRow.ColumnValue);
                        }
                    }
                }

                var lstX = string.Join("|", lstXString.ToArray());
                var lstY = string.Join("|", lstYString.ToArray());

                return lstX + "||" + lstY;
            }
        }

        public class MainLine
        {
            public string type { get; set; }
            public string name { get; set; }
            public bool showInLegend { get; set; }
            public List<SubDetails> dataPoints { get; set; }
        }

        public class SubDetails
        {
            public long y { get; set; }
            public string x { get; set; }
        }

        [WebMethod]
        public static string GetAllLineLineSubDetail()
        {
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated = objPatientsEducationBrochureRepository.GetAllStatisticsChartDetailsByChartId(2).ToList();

                StringBuilder strData = new StringBuilder();

                List<MainLine> lstMainLine = new List<MainLine>();

                strData.Append("[");

                int rowCount = 0;
                foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
                {
                    MainLine objData = new MainLine();

                    if (rowCount != 0)
                    {
                        strData.Append(",");
                    }

                    var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

                    objData.type = "line";
                    objData.name = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").ColumnValue;
                    objData.showInLegend =true;
                    objData.dataPoints = new List<SubDetails>();

                    strData.Append("{");
                    strData.Append("type: 'line',");
                    strData.Append("name: '"+ sequanceData.FirstOrDefault(x => x.TypeColumn == "X").ColumnValue + "',");
                    strData.Append("showInLegend: true,");
                    strData.Append("dataPoints: ");

                    int columnIndex = 0;

                    if (sequanceData.Count(x => x.TypeColumn != "X") > 0)
                    {
                        strData.Append("[");
                    }


                    foreach (var subRow in sequanceData.Where(x => x.TypeColumn != "X"))
                    {
                        SubDetails objSubData = new SubDetails();


                        if (columnIndex != 0)
                        {
                            strData.Append(",");
                        }
                        strData.Append("{ y: "+ subRow.ColumnValue + ", label: '" + subRow.ColumnName + "' }");

                        objSubData.y = Convert.ToInt32(subRow.ColumnValue);
                        objSubData.x = subRow.ColumnName;

                        objData.dataPoints.Add(objSubData);
                        columnIndex++;
                    }

                    if (sequanceData.Count(x => x.TypeColumn != "X") > 0)
                    {
                        strData.Append("]");
                    }

                    strData.Append("}");

                    lstMainLine.Add(objData);
                    rowCount++;
                }
                
                strData.Append("]");

                var strDataa =JsonConvert.SerializeObject(lstMainLine);

                return strData.ToString();
            }
        }
    }
}