using BAL;
using System;
using System.Data;
using System.Linq;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Other_Specialties
{
    public partial class Otherspeciality : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfSubSectionDescription;
        int osid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strListOfSubSectionDescription = GetListOfSubSectionDescription();
            }
        }
        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(osid, languageId);
            if (ds != null)
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
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string strShortTitle = row["Title"].ToString();
                            if (strShortTitle.Length > 48)
                            {
                                strShortTitle = strShortTitle.Remove(48) + "..";
                            }
                            string strShortDesc = row["ShortDesc"].ToString();
                            if (strShortDesc.Length > 40)
                            {
                                strShortDesc = strShortDesc.Remove(40) + "..";
                            }
                            string strURL = ResolveUrl(("~/OtherSpecialties/Otherspecialitydetails?" + Functions.Base64Encode(row["OS_id"].ToString())));
                            strAwardsAndAchievements.Append("<div class='col-lg-3'>");
                            strAwardsAndAchievements.Append("<div class='front_widget'>");
                            strAwardsAndAchievements.Append("<div class='doc-img'>");
                            strAwardsAndAchievements.Append("<a href='#' tabindex='0'>");
                            strAwardsAndAchievements.Append("<img class='img-fluid' alt='User Image' src='" + ResolveUrl(row["Imgpath"].ToString()) + "'>");
                            strAwardsAndAchievements.Append("</a>");
                            strAwardsAndAchievements.Append("</div>");
                            strAwardsAndAchievements.Append("<!-- About Details -->");
                            strAwardsAndAchievements.Append("<div class='front_content'>");
                            strAwardsAndAchievements.Append("<div class='specialities-img'>");
                            strAwardsAndAchievements.Append("<img src='" + ResolveUrl(row["Iconpath"].ToString()) + "' alt=''>");
                            strAwardsAndAchievements.Append("</div>");
                            strAwardsAndAchievements.Append("<h3 class='title'>" + strShortTitle + "</h3>");
                            //strAwardsAndAchievements.Append("<p class='speciality'>" + strShortDesc + "</p>");
                            strAwardsAndAchievements.Append("<a href='" + strURL + "' class='readmore_btn' tabindex='0'><i class='fas fa-chevron-circle-right'></i>Read more</a>");
                            strAwardsAndAchievements.Append("</div>");
                            strAwardsAndAchievements.Append("</div>");
                            strAwardsAndAchievements.Append("</div>");
                            i++;
                        }
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherSpecialties/Otherspeciality").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherSpecialties/Otherspeciality").FirstOrDefault();
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