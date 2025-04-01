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
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class SiteMap : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strSiteMapMenuDetails, strHiddenMenu, strInnerPage, strResearches, strScheme, strSpecialties, strDepartment;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strSiteMapMenuDetails = GetMenuDetails();
                strResearches = GetResearches();
                strScheme = GetScheme();
                strSpecialties = GetSpecialties();
                strDepartment = GetDepartment();
            }
        }

        private string GetDepartment()
        {
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                List<OurExcellenceMasterGridModel> dataMain = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(1).Where(x=> x.IsVisible.HasValue? x.IsVisible.Value:false).ToList();
                foreach (var row in dataMain)
                {
                    string strURL = ResolveUrl(("~/DepartmentsDetails?" + Functions.Base64Encode(row.Id.ToString())));
                    strAwardsAndAchievements.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strURL) || strURL == "#" ? "#" : strURL) + "'>" + row.DepartmentName + "</a></li>");
                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetSpecialties()
        {            
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(0, 1);
            if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string strShortDesc = row["ShortDesc"].ToString();
                    if (strShortDesc.Length > 40)
                    {
                        strShortDesc = strShortDesc.Remove(40) + "..";
                    }
                    string strURL = ResolveUrl(("~/OtherSpecialties/Otherspecialitydetails?" + Functions.Base64Encode(row["OS_id"].ToString())));

                    strAwardsAndAchievements.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strURL) || strURL == "#" ? "#" : strURL) + "'>"  + row["Title"] + "</a></li>");
                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetScheme()
        {
            StringBuilder strResearch = new StringBuilder();

            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(1);

                if (dataListAward != null)
                {
                    foreach (var row in dataListAward)
                    {
                        string strURL = ResolveUrl(("~/Schema?" + Functions.Base64Encode(row.Id.ToString())));
                        strResearch.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strURL) || strURL == "#" ? "#" : strURL) + "'>" + row.SchemeName + "</a></li>");
                    }
                }
            }

            return strResearch.ToString();
        }

        private string GetResearches()
        {
            StringBuilder strResearch = new StringBuilder();
            DataTable ds = new DataTable();

            ds = new ArticleDepartmentBAL().SelectRecordbylanguage(0, 1).Tables[0];
            if (ds != null)
            {
                if (!ds.Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Rows)
                    {
                        string strURL = ResolveUrl(("~/ResearchDetails?" + Functions.Base64Encode(row["AD_id"].ToString() + "|" + row["AD_Name"].ToString())));

                        strResearch.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strURL) || strURL == "#" ? "#" : strURL) + "'>" +  row["AD_Name"]  + "</a></li>");
                    }
                }
            }

            return strResearch.ToString();
        }

        private string GetMenuDetails()
        {
            StringBuilder strMenu = new StringBuilder();
            StringBuilder strHiddenMenuBui = new StringBuilder();
            using (IMenuMasterRepository objMenu = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                bool isActive = false;
                List<tbl_Menu_MasterSelectAllResult> lstList = objMenu.GetAllMenuList(1);

                List<tbl_Menu_MasterSelectAllResult> lstHiddenList = lstList.Where(x => x.col_menu_type == '4').ToList();
                lstList = lstList.Where(x => x.col_menu_type != '4').ToList();


                strHiddenMenuBui.Append("<div class='col-lg-2 col-md-6 col-sm-12 column'>");
                strHiddenMenuBui.Append("	<div class='about-widget'>");
                strHiddenMenuBui.Append("		<h4 class='widget-title'>Quick Links</h4>");
                strHiddenMenuBui.Append("	</div>");
                strHiddenMenuBui.Append("	<ul class='links clearfix pb-50'>");

                foreach (var mainMenu in lstHiddenList)
                {
                    string strData=mainMenu.col_menu_name;
                    System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");



                    // replace all matches with empty strin

                    strData = rx.Replace(strData, "");
                    strHiddenMenuBui.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + ResolveUrl("~/" + mainMenu.MaskingURL) + "'>" + strData + "</a></li>");
                }
				strHiddenMenuBui.Append("	</ul>");
                strHiddenMenuBui.Append("</div>");
                
                List<tbl_Menu_MasterSelectAllResult> lstParentList = lstList.Where(x => x.col_parent_id == 0 && x.col_menu_type != '3' && x.col_menu_name.Contains("About")).ToList();

                foreach (var mainMenu in lstParentList)
                {
                    strMenu.Append(GetParentIdString(mainMenu, lstList, isActive, out isActive));
                }

                strInnerPage = "";
                StringBuilder strstrInnerPage = new StringBuilder();
                List<tbl_Menu_MasterSelectAllResult> lstInnerList = lstList.Where(x => x.col_menu_type == '3' && (!(x.col_menu_name.Contains("Detail") || x.col_menu_name.Contains("Medical") || x.col_menu_name.Contains("Schema"))) || x.col_menu_name.Contains("Award")).ToList();
                if (lstInnerList.Count() > 0)
                {

                    strstrInnerPage.Append("<div class='col-lg-2 col-md-6 col-sm-12 column'>");
                    strstrInnerPage.Append("	<div class='about-widget'>");
                    strstrInnerPage.Append("		<h4 class='widget-title'>Homepage Links</h4>");
                    strstrInnerPage.Append("	</div>");
                    strstrInnerPage.Append("	<ul class='links clearfix pb-50'>");
                    foreach (var mainMenus in lstInnerList)
                    {
                        string strPath = mainMenus.MaskingURL;
                        strstrInnerPage.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strPath) || strPath == "#" ? "#" : strPath) + "'>" + HttpUtility.HtmlDecode(mainMenus.col_menu_name) + "</a></li>");
                    }
                    strstrInnerPage.Append("	</ul>");
                    strstrInnerPage.Append("</div>");
                    strInnerPage = strstrInnerPage.ToString();
                }

                //strMenuString = (strStartMenuString + strMenu.ToString());
                strHiddenMenu = strHiddenMenuBui.ToString();
            }
            return (strMenu.ToString());
        }

        private string GetParentIdString(tbl_Menu_MasterSelectAllResult mainMenu, List<tbl_Menu_MasterSelectAllResult> lstList, bool isActive, out bool isOutActive)
        {
            if (!isActive)
            {
                string rowURL1 = Request.RawUrl.ToString();
                string strRow = (Request.Url.OriginalString).Replace(rowURL1, "");
                strRow = strRow.Split('/').Last();
                strRow = strRow.Replace("/", "");
                if (mainMenu.col_menu_url.ToString().ToLower() == strRow.ToLower())
                {
                    isActive = true;
                }

            }
            StringBuilder strMenu = new StringBuilder();
            List<tbl_Menu_MasterSelectAllResult> lstSubList = lstList.Where(x => x.col_parent_id == mainMenu.col_menu_id && x.col_menu_type != '3').ToList();
            if (lstSubList.Count() > 0)
            {

                strMenu.Append("<div class='col-lg-2 col-md-6 col-sm-12 column'>");
                strMenu.Append("	<div class='about-widget'>");
                strMenu.Append("		<h4 class='widget-title'>" + HttpUtility.HtmlDecode(mainMenu.col_menu_name) + "</h4>");
                strMenu.Append("	</div>");
                strMenu.Append("	<ul class='links clearfix pb-50'>");
                foreach (var mainMenus in lstSubList)
                {
                    string strPath = mainMenu.MaskingURL;
                    strMenu.Append("		<li><i class='far fa-hand-point-right'></i><a href='" + (string.IsNullOrWhiteSpace(strPath) || strPath == "#" ? "#" : strPath) + "'>" + HttpUtility.HtmlDecode(mainMenus.col_menu_name) + "</a></li>");
                }
                strMenu.Append("	</ul>");
                strMenu.Append("</div>");


            }


            isOutActive = isActive;
            return strMenu.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SiteMap").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "SiteMap").FirstOrDefault();
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