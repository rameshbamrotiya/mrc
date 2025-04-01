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

namespace Unmehta.WebPortal.Web
{
    public partial class OPDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strOPD;
        public static string strOPDtabs;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strOPD = BindOPD();
            }
        }

        private string BindOPD()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strOPD = new StringBuilder();
            StringBuilder strOPDTabs = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var allData = objBlogCategoryMasterRepository.GetAllOPDMainMaster(languageId);
                LableData:

                if (allData.Count() > 0)
                {
                    var TabList = allData.Select(x => new { name = x.Name.Trim(), DeptName=x.DepartmentName.Trim(), x.PlaceIncharge }).ToList().Distinct().ToList();
                    int i = 1;
                    foreach (var row in TabList)
                    {
                        strOPDTabs.Append("<li><a href='#tab_"+ i + "' class='"+ (i == 1 ? "active" : "") + "' data-toggle='pill'>"+ row.name + "</a></li>");

                        strOPD.Append("");
                        strOPD.Append("<div class='tab-pane " + (i == 1 ? "active" : "") + "' id='tab_" + i + "'>                       ");
						strOPD.Append("	<div class='class-schedule-wrap1'>                                                              ");
						strOPD.Append("		<div class='table-responsive'>                                                              ");
						strOPD.Append("			<table class='table table-bordered time-table'>                                         ");
						strOPD.Append("				<thead>                                                                             ");
						strOPD.Append("					<tr>                                                                            ");
						strOPD.Append("						<th colspan='4'>" + row.name + "</th>                                       ");
						strOPD.Append("					</tr>                                                                           ");
						strOPD.Append("					<tr>                                                                            ");
						strOPD.Append("						<td>                                                                        ");
						strOPD.Append("							<div class='schedule-day-heading'>Specialty</div>                       ");
						strOPD.Append("						</td>                                                                       ");
						strOPD.Append("						<td>                                                                        ");
						strOPD.Append("							<div class='schedule-day-heading'>UNIT</div>                            ");
						strOPD.Append("						</td>                                                                       ");
						strOPD.Append("						<td>                                                                        ");
						strOPD.Append("							<div class='schedule-day-heading'>Days</div>                            ");
						strOPD.Append("						</td>                                                                       ");
						strOPD.Append("						<td>                                                                        ");
						strOPD.Append("							<div class='schedule-day-heading'>Time</div>                            ");
						strOPD.Append("						</td>                                                                       ");
						strOPD.Append("					</tr>                                                                           ");
						strOPD.Append("				</thead>                                                                            ");

						strOPD.Append("				<tbody>                                                                             ");
                        Array weeks = Enum.GetValues(typeof(DayOfWeek));
                        int j = 0;
                        foreach (DayOfWeek week in weeks)
                        {
                            if(j==0)
                            {
                                j++;
                                continue;
                            }

                            var dataSubRow = allData.Where(x => x.Name.Trim() == row.name.Trim() && x.WeekName == ((int)week).ToString()).FirstOrDefault();
                            strOPD.Append("					<tr>                                                                            ");

                            if (j == 1)
                            {
                                strOPD.Append("						<td rowspan='6' class='bgclr-1'>                                            ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("								<div class='item-speciality'>                                       ");
                                strOPD.Append("									<strong> " + row.DeptName + "</strong><br>                      ");
                                strOPD.Append("									Place In-charge<br>                                             ");
                                strOPD.Append("									" + (string.IsNullOrWhiteSpace(row.PlaceIncharge) ? "" : row.PlaceIncharge).Replace(",", "<br/>") + "                 ");
                                strOPD.Append("								</div>                                                              ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                            }

                            if(dataSubRow!=null)
                            {
                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("                                                                                                 ");
                                strOPD.Append("								<div class='item-ctg'>" + dataSubRow.UnitName + "</div>             ");

                                strOPD.Append("									" +(string.IsNullOrWhiteSpace(dataSubRow.UnitList)?"": dataSubRow.UnitList).Replace(",", "</div><div class='item-team'>") + "                 ");

                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("								<div class='item-ctg'>" + week.ToString() + "</div>                                  ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("								<div class='item-time'>" + dataSubRow.StartTime + " to " + dataSubRow.EndTime + "</div>                 ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");

                            }
                            else
                            {

                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("                                                                                                 ");
                                strOPD.Append("								<div class='item-ctg'></div>                                        ");
                                strOPD.Append("								<div class='item-team'></div>                                       ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("								<div class='item-ctg'>" + week.ToString() + "</div>                                  ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                                strOPD.Append("						<td>                                                                        ");
                                strOPD.Append("							<div class='schedule-item-wrapper'>                                     ");
                                strOPD.Append("								<div class='item-time'>                      </div>                 ");
                                strOPD.Append("							</div>                                                                  ");
                                strOPD.Append("						</td>                                                                       ");
                            }
                            strOPD.Append("					</tr>                                                                           ");

                            j++;
                        }
                     

						strOPD.Append("				</tbody>                                                                            ");
						strOPD.Append("			</table>                                                                                ");
						strOPD.Append("		</div>                                                                                      ");
						strOPD.Append("	</div>                                                                                          ");
                        strOPD.Append("</div>                                                                                           ");


                        i++;
                    }

                    strOPDtabs = strOPDTabs.ToString();
                }
                else
                {
                    languageId = 1;
                    allData = objBlogCategoryMasterRepository.GetAllOPDMainMaster(languageId);
                    if (languageId != 1)
                    {
                        goto LableData;
                    }
                }
            }
            return strOPD.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OPDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OPDetails").FirstOrDefault();
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