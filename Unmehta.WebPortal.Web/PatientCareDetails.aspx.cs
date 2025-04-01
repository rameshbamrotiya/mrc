using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class PatientCareDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strRightsSideTabs;
        public static string strDescription;
        public static long OSid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strRightsSideTabs = GetTabsDescription();

            }
        }

        private string GetTabsDescription()
        {
            StringBuilder strTab = new StringBuilder();
            StringBuilder StrSubTab = new StringBuilder();
            StringBuilder StrSubTabData = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                int languageId = Functions.LanguageId;
                var dataList = objBlogCategoryMasterRepository.GetAllPatientCareTypeByLangId(languageId).ToList();

                LableData:
                if (dataList.Count() > 0)
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
                        strTab.Append("<li><a href='#tab_" + i + "' class='" + strAActive + "' data-toggle='pill'>" + row.CategoryName + "</a></li>");
                        StrSubTab.Append("");
                        StrSubTab.Append("<div class='tab-pane " + strAActive + "' id='tab_" + i + "'>                                         ");

                        PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
                        DataTable dts = objBAL.GetTabList(Convert.ToInt32(row.Id));
                        if (row.FormType == "General")
                        {
                            StrSubTab.Append("");
                            StrSubTab.Append("<nav class='user-tabs mb-4'>");
                            StrSubTab.Append("	<ul class='nav nav-tabs nav-tabs-bottom'>");
                            int index = 1;

                            foreach (DataRow rowTab in dts.Rows)
                            {
                                StrSubTab.Append("		<li class='nav-item'>");
                                StrSubTab.Append("			<a class='nav-link " + (index == 1 ? "active" : "") + "' href='#" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "' data-toggle='tab'>" + rowTab["TabName"].ToString() + "</a>");
                                StrSubTab.Append("		</li>");
                                index++;
                            }
                            StrSubTab.Append("	</ul>");
                            StrSubTab.Append("</nav>");
                            StrSubTabData.Length = 0;
                            StrSubTabData.Append("<div class=\"tab-content pt-0\">");
                            index = 1;
                            foreach (DataRow rowTab in dts.Rows)
                            {
                                string strTabTypeId = rowTab["TabTypeId"].ToString();
                                string strSubTabTypeId = rowTab["SubTabId"].ToString();
                                string abc = rowTab["TabName"].ToString();
                                StrSubTabData.Append("<div role=\"tabpanel\" id=\"" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "\" class=\"tab-pane fade " + (index == 1 ? "active show" : "") + "\">");
                                StrSubTabData.Append("");
                                StrSubTabData.Append(GetSubGeneralDetails(strTabTypeId, strSubTabTypeId));
                                StrSubTabData.Append("</div>");
                                index++;
                            }

                            StrSubTabData.Append("</div>");
                            StrSubTab.Append(StrSubTabData.ToString());
                        }
                        else if (row.FormType == "Brochure")
                        {
                            StrSubTab.Append("");
                            StrSubTab.Append("<nav class='user-tabs mb-4'>");
                            StrSubTab.Append("	<ul class='nav nav-tabs nav-tabs-bottom'>");
                            int index = 1;
                            foreach (DataRow rowTab in dts.Rows)
                            {
                                StrSubTab.Append("		<li class='nav-item'>");
                                StrSubTab.Append("			<a class='nav-link " + (index == 1 ? "active" : "") + "' href='#" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "' data-toggle='tab'>" + rowTab["TabName"].ToString() + "</a>");
                                StrSubTab.Append("		</li>");
                                index++;
                            }
                            StrSubTab.Append("	</ul>");
                            StrSubTab.Append("</nav>");
                            StrSubTabData.Length = 0;
                            StrSubTabData.Append("<div class=\"tab-content pt-0\">");
                            index = 1;
                            foreach (DataRow rowTab in dts.Rows)
                            {
                                string strTabTypeId = rowTab["TabTypeId"].ToString();
                                string strSubTabTypeId = rowTab["SubTabId"].ToString();
                                string abc = rowTab["TabName"].ToString();
                                StrSubTabData.Append("<div role=\"tabpanel\" id=\"" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "\" class=\"tab-pane fade " + (index == 1 ? "active show" : "") + "\">");
                                StrSubTabData.Append("");
                                StrSubTabData.Append(GetBrochureDetailsDetails(strTabTypeId, strSubTabTypeId));
                                StrSubTabData.Append("</div>");
                                index++;
                            }

                            StrSubTabData.Append("</div>");
                            StrSubTab.Append(StrSubTabData.ToString());
                        }
                        else if (row.FormType == "Left&RightContain")
                        {
                            StrSubTab.Append("");
                            StrSubTab.Append("<nav class='user-tabs mb-4'>");
                            StrSubTab.Append("	<ul class='nav nav-tabs nav-tabs-bottom'>");
                            int index = 1;
                            foreach (DataRow rowTab in dts.Rows)
                            {
                                StrSubTab.Append("		<li class='nav-item'>");
                                StrSubTab.Append("			<a class='nav-link " + (index == 1 ? "active" : "") + "' href='#" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "' data-toggle='tab'>" + rowTab["TabName"].ToString() + "</a>");
                                StrSubTab.Append("		</li>");
                                index++;
                            }
                            StrSubTab.Append("	</ul>");
                            StrSubTab.Append("</nav>");
                            StrSubTabData.Length = 0;
                            StrSubTabData.Append("<div class=\"tab-content pt-0\">");
                            index = 1;
                            foreach (DataRow rowTab in dts.Rows)
                            {
                                string strTabTypeId = rowTab["TabTypeId"].ToString();
                                string strSubTabTypeId = rowTab["SubTabId"].ToString();
                                string abc = rowTab["TabName"].ToString();
                                StrSubTabData.Append("<div role=\"tabpanel\" id=\"" + rowTab["TabName"].ToString().Replace(" ", "").Replace("*", "").Replace("'", "").Replace("&", "").Replace("@", "") + "" + index + "\" class=\"tab-pane fade " + (index == 1 ? "active show" : "") + "\">");
                                StrSubTabData.Append("");
                                StrSubTabData.Append(GetLeftRightContainDetails(strTabTypeId, strSubTabTypeId));
                                StrSubTabData.Append("</div>");
                                index++;
                            }

                            StrSubTabData.Append("</div>");
                            StrSubTab.Append(StrSubTabData.ToString());
                        }
                        else
                        {
                            DataSet dsFloorDetails = objBAL.GetAllFloorDetailsList();

                            DataTable dtBlock = dsFloorDetails.Tables[0];
                            DataTable dtFloor = dsFloorDetails.Tables[1];
                            DataTable dtFloorDetails = dsFloorDetails.Tables[2];

                            StrSubTab.Append("");
                            StrSubTab.Append("<table class='table table-hover table-center mb-0 floortable'>");
                            StrSubTab.Append("<tbody>");
                            StrSubTab.Append("<tr>");
                            StrSubTab.Append("<th class='days'>Floor</th>");
                            foreach (DataRow rowBrochure in dtBlock.Rows)
                            {
                                StrSubTab.Append("<th>" + rowBrochure["BlockName"].ToString() + "</th>");
                            }
                            StrSubTab.Append("				</tr>");
                            

                            foreach (DataRow rowBrochure in dtFloor.Rows)
                            {
                                string strFloor = rowBrochure["FloorName"].ToString();
                                StrSubTab.Append("				<tr>");
                                StrSubTab.Append("					<th>"+ strFloor + "</th>");
                                foreach (DataRow rowColumn in dtBlock.Rows)
                                {
                                    string strBlockName = rowColumn["BlockName"].ToString();

                                    var result = dtFloorDetails.AsEnumerable().Where(x => x.Field<string>("BlockName") == strBlockName
                                    && x.Field<string>("FloorName") == strFloor)
                                    .Select(g => new
                                    {
                                        CellValue = g.Field<string>("CellValue"),
                                        ToolTip = g.Field<string>("ToolTip")
                                    }
                                    ).FirstOrDefault();

                                    if (result != null)
                                    {
                                        if (!string.IsNullOrWhiteSpace(result.CellValue))
                                        {
                                            StrSubTab.Append("<td class='' " + (!string.IsNullOrWhiteSpace(result.ToolTip)? ("data-tooltip='"+result.ToolTip+"'") : "") + ">" + result.CellValue + "</td>");
                                        }
                                        else
                                        {
                                            StrSubTab.Append("<td></td>");
                                        }
                                    }
                                    else
                                    {
                                        StrSubTab.Append("<td></td>");
                                    }
                                }
                                StrSubTab.Append("				</tr>");
                            }
                            StrSubTab.Append("			</tbody>");
                            StrSubTab.Append("		</table>");
                        }
                        StrSubTab.Append("</div>");
                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllPatientCareTypeByLangId(languageId).ToList();
                        goto LableData;
                    }
                }

            }

            strDescription = StrSubTab.ToString();
            return strTab.ToString();
        }

        private string GetSubGeneralDetails(string strTabTypeId, string strSubTabTypeId)
        {

            StringBuilder StrSubTabDataDetails = new StringBuilder();

            PatientCareGeneralDetailsBO objBO = new PatientCareGeneralDetailsBO();
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            objBO.TabTypeId = Convert.ToInt32(strTabTypeId);
            objBO.SubTabId = Convert.ToInt32(strSubTabTypeId);
            DataSet dsDetails = objBAL.GetSubTabGeneralDetailsList(objBO);

            DataTable dtInfo = dsDetails.Tables[0];
            DataTable dtSlider = dsDetails.Tables[1];
            StrSubTabDataDetails.Append("");
            if (dtInfo.Rows.Count > 0 && dtInfo != null)
            {
                StrSubTabDataDetails.Append("<div class='row'>");
                StrSubTabDataDetails.Append("<div class='col-md-7 col-lg-7'>");
                StrSubTabDataDetails.Append("<div class='widget about-widget'>");
                StrSubTabDataDetails.Append("<p> " + HttpUtility.HtmlDecode(dtInfo.Rows[0]["TabDescription"].ToString()) + "");
                StrSubTabDataDetails.Append("</p>");
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("<div class='col-md-5 col-lg-5'>");
                StrSubTabDataDetails.Append("<div class='profile-widget'>");
                StrSubTabDataDetails.Append("<div id = 'carouselExampleSlidesOnly' class='carousel slide' data-ride='carousel'>");
                StrSubTabDataDetails.Append("<div class='carousel-inner'>");
                int index = 1;
                foreach (DataRow rowTab in dtSlider.Rows)
                {
                    string strURL = (rowTab["Img_path"].ToString());
                    if (strURL.StartsWith("~/", StringComparison.Ordinal))
                    {
                        strURL = strURL.Replace("~/", "");
                        strURL = ResolveUrl("~/" + strURL);
                    }

                    StrSubTabDataDetails.Append("<div class='carousel-item " + (index == 1 ? "active" : "") + "'>");
                    StrSubTabDataDetails.Append("<img class='d-block w-100' src='" + strURL + "' alt='First slide'>");
                    StrSubTabDataDetails.Append("<div class='carousel-caption d-none d-md-block'>");
                    //StrSubTabData.Append("<h3>Subtext</h3>");
                    StrSubTabDataDetails.Append("</div>");
                    StrSubTabDataDetails.Append("</div>");
                    index++;
                }
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("</div>");
                StrSubTabDataDetails.Append("</div>");
            }
            return StrSubTabDataDetails.ToString();
        }

        private string GetBrochureDetailsDetails(string strTabTypeId, string strSubTabTypeId)
        {

            StringBuilder StrSubTabData = new StringBuilder();

            PatientCareGeneralDetailsBO objBO = new PatientCareGeneralDetailsBO();
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            objBO.TabTypeId = Convert.ToInt32(strTabTypeId);
            objBO.SubTabId = Convert.ToInt32(strSubTabTypeId);
            DataSet dsDetails = objBAL.GetBrochureDetailsList(objBO);

            DataTable dtBrochure = dsDetails.Tables[0];

            int index = 1;
            StrSubTabData.Length = 0;
            StrSubTabData.Append("<ul class='pdffile fade_anim'>");
            foreach (DataRow rowBrochure in dtBrochure.Rows)
            {
                string strURL = (rowBrochure["ImagePath"].ToString());
                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                {
                    strURL = strURL.Replace("~/", "");
                    strURL = ResolveUrl("~/" + strURL);
                }
                string strFileURL = (rowBrochure["FileUploadPath"].ToString());
                if (strFileURL.StartsWith("~/", StringComparison.Ordinal))
                {
                    strFileURL = strFileURL.Replace("~/", "");
                    strFileURL = ResolveUrl("~/" + strFileURL);
                }
                StrSubTabData.Append("<li>");
                StrSubTabData.Append("<a href =" + strFileURL + " target='_blank'>");
                StrSubTabData.Append("<img src = " + strURL + ">");
                StrSubTabData.Append("<span>" + rowBrochure["Title"].ToString() + "</span></a>");
                StrSubTabData.Append("</li>");
                index++;
            }
            StrSubTabData.Append("</ul>");
            return StrSubTabData.ToString();
        }
        private string GetLeftRightContainDetails(string strTabTypeId, string strSubTabTypeId)
        {
            StringBuilder strTabContent = new StringBuilder();
            PatientCareLeftRightContainDetailsBO objBO = new PatientCareLeftRightContainDetailsBO();
            PatientCareMasterBAL objBAL = new PatientCareMasterBAL();
            objBO.TabTypeId = Convert.ToInt32(strTabTypeId);
            objBO.SubTabId = Convert.ToInt32(strSubTabTypeId);
            DataSet dsDetails = objBAL.GetLeftRightContainDetailsList(objBO);
            DataTable dtInfo = dsDetails.Tables[0];
            strTabContent.Append("");
            if (dtInfo.Rows.Count > 0 && dtInfo != null)
            {
                int index = 1;
                foreach (DataRow sub in dtInfo.Rows)
                {

                    string strimagePath = "";

                    if (!string.IsNullOrWhiteSpace(sub["ImagePath"].ToString()))
                    {
                        strimagePath = (sub["ImagePath"].ToString());
                        if (strimagePath.StartsWith("~/", StringComparison.Ordinal))
                        {
                            strimagePath = strimagePath.Replace("~/", "");
                            strimagePath = ResolveUrl("~/" + strimagePath);
                        }

                    }
                    if (index % 2 == 0)
                    {
                        strTabContent.Append("<div class='author-widget clearfix mb-15'>");
                        strTabContent.Append("	<div class='about-author'>");
                        strTabContent.Append("		<div class='row'>");
                        strTabContent.Append("			<div class='col-lg-3'>");
                        strTabContent.Append("				<div class='about-author'>");
                        strTabContent.Append("					<div class='author-img-wrap'>");
                        strTabContent.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                        strTabContent.Append("					</div>");
                        strTabContent.Append("				</div>");
                        strTabContent.Append("			</div>");
                        strTabContent.Append("			<div class='col-lg-9'>");
                        strTabContent.Append("				<div class='author-details_1'>");
                        strTabContent.Append("				" + HttpUtility.HtmlDecode(sub["TabDescription"].ToString()) + "");
                        strTabContent.Append("				</div>");
                        strTabContent.Append("			</div>");
                        strTabContent.Append("		</div>");
                        strTabContent.Append("	</div>");
                        strTabContent.Append("</div>");
                    }
                    else
                    {
                        strTabContent.Append("<div class='author-widget clearfix'>");
                        strTabContent.Append("	<div class='about-author'>");
                        strTabContent.Append("		<div class='row'>");
                        strTabContent.Append("			<div class='col-lg-9'>");
                        strTabContent.Append("				<div class='author-details_1'>");
                        strTabContent.Append("				" + HttpUtility.HtmlDecode(sub["TabDescription"].ToString()) + "");
                        strTabContent.Append("				</div>");
                        strTabContent.Append("			</div>");
                        strTabContent.Append("			<div class='col-lg-3'>");
                        strTabContent.Append("				<div class='about-author'>");
                        strTabContent.Append("					<div class='author-img-wrap'>");
                        strTabContent.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                        strTabContent.Append("					</div>");
                        strTabContent.Append("				</div>");
                        strTabContent.Append("			</div>");
                        strTabContent.Append("		</div>");
                        strTabContent.Append("	</div>");
                        strTabContent.Append("</div>");
                    }
                    strTabContent.Append("<hr>");
                    index++;
                }
            }
            return strTabContent.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PatientCareDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PatientCareDetails").FirstOrDefault();
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