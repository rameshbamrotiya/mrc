using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using BAL;

namespace Unmehta.WebPortal.Web
{
    public partial class Departments : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfSubSectionDescription;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strListOfSubSectionDescription = GetDepartment();
            }
        }

        private string GetDepartment()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {

                using (IDepartmentTabRepository objBlogCategoryMasterRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                {
                    List<OurExcellenceMasterGridModel> dataMain = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(languageId).Where(x=> x.IsVisible==true).OrderBy(x=> x.SequanceNo).ToList();
                    {

                        string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                        //rowURl = rowURl.Substring(1);
                        StringBuilder strResearch = new StringBuilder();
                        ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                        bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                        if (IsDisabledTranslate && Functions.LanguageId != 1)
                        {
                            {
                                Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                                return strResearch.ToString();
                            }
                        }
                        else
                        {

                        LableData:
                            strBoardOfDirector = new StringBuilder();
                            if (dataMain.Count() > 0)
                            {
                                strBoardOfDirector.Append("");
                                //foreach (var item in dataMain.Where(x => x.IsAddInOtherDepartment == false).OrderBy(x => x.SequanceNo).ToList())
                                foreach (var item in dataMain.OrderBy(x => x.SequanceNo).ToList())
                                {
                                    string strShortDepartmentName = item.DepartmentName.ToString();
                                    if (strShortDepartmentName.Length > 48)
                                    {
                                        strShortDepartmentName = strShortDepartmentName.Remove(48) + "..";
                                    }
                                    string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(item.ImageName) ? "" : item.ImageName);
                                    string strimagePathIcon = ResolveUrl("~/Hospital/assets/img/category/4.png");

                                    string strURL = ResolveUrl(("~/DepartmentsDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(item.Id.ToString()))));

                                    strBoardOfDirector.Append("<div class='col-lg-3'>");
                                    strBoardOfDirector.Append("	<div class='front_widget'>");
                                    strBoardOfDirector.Append("		<div class='doc-img'>");
                                    strBoardOfDirector.Append("			<a href='#' tabindex='0'>");
                                    strBoardOfDirector.Append("				<img class='img-fluid' alt='User Image' src='" + strimagePath + "'>");
                                    strBoardOfDirector.Append("			</a>");
                                    strBoardOfDirector.Append("		</div>");
                                    strBoardOfDirector.Append("		<div class='front_content'>");
                                    strBoardOfDirector.Append("			<div class='specialities-img'>");
                                    strBoardOfDirector.Append("				<img src='" + strimagePathIcon + "' alt=''>");
                                    strBoardOfDirector.Append("			</div>");
                                    strBoardOfDirector.Append("			<h3 class='title'>" + strShortDepartmentName + " </h3>");
                                    strBoardOfDirector.Append("			<p class='speciality'> </p>");
                                    strBoardOfDirector.Append("			<a href='" + strURL + "' class='readmore_btn' tabindex='0'><i class='fas fa-chevron-circle-right'></i>");
                                    strBoardOfDirector.Append("				Read");
                                    strBoardOfDirector.Append("				more</a>");
                                    strBoardOfDirector.Append("		</div>");
                                    strBoardOfDirector.Append("	</div>");
                                    strBoardOfDirector.Append("</div>");
                                }
                                var otherDepartment = dataMain.Where(x => x.IsAddInOtherDepartment == true).OrderBy(x => x.SequanceNo).ToList();
                                if (otherDepartment.Count() > 0)
                                {
                                    string strURL = ResolveUrl(("~/DepartmentsDetails"));
                                    string strimagePath = ResolveUrl(("~/Hospital/assets/img/medical/solution1.png"));

                                    string strimagePathIcon = ResolveUrl("~/Hospital/assets/img/category/4.png");

                                    strBoardOfDirector.Append("<div class='col-lg-3'>");
                                    strBoardOfDirector.Append("	<div class='front_widget'>");
                                    strBoardOfDirector.Append("		<div class='doc-img'>");
                                    strBoardOfDirector.Append("			<a href='#' tabindex='0'>");
                                    strBoardOfDirector.Append("				<img class='img-fluid' alt='User Image' src='" + strimagePath + "'>");
                                    strBoardOfDirector.Append("			</a>");
                                    strBoardOfDirector.Append("		</div>");
                                    strBoardOfDirector.Append("		<div class='front_content'>");
                                    strBoardOfDirector.Append("			<div class='specialities-img'>");
                                    strBoardOfDirector.Append("				<img src='" + strimagePathIcon + "' alt=''>");
                                    strBoardOfDirector.Append("			</div>");
                                    strBoardOfDirector.Append("			<h3 class='title'>");
                                    strBoardOfDirector.Append("				Other Faculty <br>&nbsp;");
                                    strBoardOfDirector.Append("			</h3>");
                                    strBoardOfDirector.Append("			<p class='speciality'> </p>");
                                    strBoardOfDirector.Append("			<a href='" + strURL + "' class='readmore_btn' tabindex='0'><i class='fas fa-chevron-circle-right'></i>");
                                    strBoardOfDirector.Append("				Read");
                                    strBoardOfDirector.Append("				more</a>");
                                    strBoardOfDirector.Append("		</div>");
                                    strBoardOfDirector.Append("	</div>");
                                    strBoardOfDirector.Append("</div>");

                                }
                            }
                            else
                            {
                                languageId = 1;
                                dataMain = dataMain = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(languageId);
                                if (languageId != 1)
                                {
                                    goto LableData;
                                }
                            }
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Departments").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Departments").FirstOrDefault();
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