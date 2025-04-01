using BAL;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class SupportServicesDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strRightsSideTabs;
        public static string strDescription;
        public static long OSid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                if (string.IsNullOrWhiteSpace(queryString))
                {
                    Response.Redirect("~/");
                }
                else if (!long.TryParse(queryString, out OSid))
                {
                    Response.Redirect("~/");
                }
                if (OSid == null)
                {
                    OSid = 0;
                }
                strHeaderImage = GetHeaderImage();
                strRightsSideTabs = GetTabsDescription();

            }
        }

        private string GetTabsDescription()
        {
            StringBuilder strtab = new StringBuilder();
            StringBuilder strtabDesc = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                int languageId = Functions.LanguageId;
                var dataList = objBlogCategoryMasterRepository.GetAllSupportServiceByLangId(languageId).ToList();

                LableData: if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {
                        string strAActive = "";
                        if (OSid > 0)
                        {
                            if (row.Id == OSid)
                            {
                                strAActive = "active";
                            }
                            else
                            {
                                strAActive = "";
                            }
                        }
                        else if (i == 1)
                        {
                            strAActive = "active";
                        }
                        else
                        {
                            strAActive = "";
                        }
                        strtab.Append("<li><a href='#tab_" + i + "' class='" + strAActive + "' data-toggle='pill'>" + row.SSName + "</a></li>");
                        strtabDesc.Append("");
                        strtabDesc.Append("<div class='tab-pane " + strAActive + "' id='tab_" + i + "'>                                          ");

                        SupportServiceMasterBAL objBAL = new SupportServiceMasterBAL();
                        DataTable dts = objBAL.GetAllSupportImageDetails(row.Id);


                        strtabDesc.Append("<div class='widget about-widget'>");
                        strtabDesc.Append("    <h4 class='widget-title'>"+ row.SSName + "</h4>");
                        strtabDesc.Append("    <div class='row'>");
                        strtabDesc.Append("        <div class='col-md-7 col-lg-7'>");
                        strtabDesc.Append("            <!-- About Details -->");
                        strtabDesc.Append("            <div class='widget about-widget'>");
                        strtabDesc.Append(HttpUtility.HtmlDecode(row.Description));
                        strtabDesc.Append("            </div>");
                        strtabDesc.Append("            <!-- /About Details -->");
                        strtabDesc.Append("        </div>");
                        strtabDesc.Append("        <div class='col-md-5 col-lg-5'>");
                        strtabDesc.Append("            <div class='profile-widget'>");
                        strtabDesc.Append("                <div id='carouselExampleSlidesOnly' class='carousel slide' data-ride='carousel'>");
                        strtabDesc.Append("                    <div class='carousel-inner'>");
                        int index = 1;
                        foreach(DataRow rowDt in dts.Rows)
                        {

                            string strURL = (rowDt["ImagePath"].ToString());
                            if (strURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURL = strURL.Replace("~/", "");
                                strURL = ResolveUrl("~/" + strURL);
                            }

                            strtabDesc.Append("                        <div class='carousel-item "+(index==1? "active" : "") +"'>");
                            strtabDesc.Append("                            <img class='d-block w-100' src='"+ strURL + "' alt='First slide'>");
                            strtabDesc.Append("                            <div class='carousel-caption d-none d-md-block'>");
                            //strtabDesc.Append("                                <h3>Subtext</h3>");
                            strtabDesc.Append("                            </div>");
                            strtabDesc.Append("                        </div>");
                            index++;
                        }

                        strtabDesc.Append("                    </div>");
                        strtabDesc.Append("                </div>");
                        strtabDesc.Append("            </div>");
                        strtabDesc.Append("        </div>");
                        strtabDesc.Append("    </div>");
                        strtabDesc.Append("</div>");
                        strtabDesc.Append("</div>                                                                                                              ");
                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllSupportServiceByLangId(languageId).ToList();
                        goto LableData;
                    }
                }

            }

            strDescription = strtabDesc.ToString();

            return strtab.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SupportServicesDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SupportServicesDetails").FirstOrDefault();
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