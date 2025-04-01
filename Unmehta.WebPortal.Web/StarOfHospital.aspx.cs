using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class StarOfHospital : System.Web.UI.Page
    {
        public static string strTitle, strMonthTab, strAccordMonthTab, strWeekTab, strAccordWeekTab;
        public static string strHeaderImage, strMonth, strWeek;
        public static string strQuickLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                GetDescription();
                strQuickLink = Functions.CreateQuickLink("Career", "StarOfHospital");
            }
        }

        private void GetDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder atrAccordMonth = new StringBuilder();
            StringBuilder atrAccordWeek = new StringBuilder();
            using (IStarOfRepository objBlogCategoryMasterRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllStarOfDetails(languageId).FirstOrDefault();
                LableData: if (dataMain != null)
                {
                    var dataAccordMain = objBlogCategoryMasterRepository.GetAllStarOfAccordDetailsByStartId((long)dataMain.StarId, languageId).Where(x => x.IsVisible == true).ToList();

                    atrAccordMonth = new StringBuilder();
                    atrAccordWeek = new StringBuilder();

                    strTitle = dataMain.StarPageTitle;
                    strMonthTab = dataMain.StarPageMonthTabName;
                    strAccordMonthTab = dataMain.StarAccordMonthTitle;
                    strWeekTab = dataMain.StarPageWeekTabName;
                    strAccordWeekTab = dataMain.StarAccordWeekTitle;

                    if(dataAccordMain.Count()>0)
                    {
                        foreach(var row in dataAccordMain.OrderByDescending(x=> x.Id).ToList())
                        {
                            var ImageRecord= objBlogCategoryMasterRepository.GetAllStarOfAccordSubImageDetailsByAccordId((long)row.Id, languageId).Where(x=> x.IsVisible==true).OrderBy(x=> x.Id).ToList();
                            if (row.TypeMonthOrWeek==0)
                            {
                                atrAccordMonth.Append("");

                                if(ImageRecord.Count()>0)
                                {
                                    atrAccordMonth.Append("<li class='accordion block'>");
                                    atrAccordMonth.Append("    <div class='acc-btn'>");
                                    atrAccordMonth.Append("        <div class='icon-outer'></div>");
                                    atrAccordMonth.Append("        <h6>" + row.AccordTitle + "</h6>");
                                    atrAccordMonth.Append("    </div>");
                                    atrAccordMonth.Append("    <div class='acc-content'>");
                                    atrAccordMonth.Append("        <div class='row'>");
                                    foreach (var subRow in ImageRecord)
                                    {
                                        string strURL;

                                        strURL = (subRow.ImageName);
                                        if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                        {
                                            strURL = strURL.Replace("~/", "");
                                            strURL = ResolveUrl("~/" + strURL);
                                        }

                                        atrAccordMonth.Append("            <div class='col-md-6 col-lg-6'>");
                                        atrAccordMonth.Append("                <div class='gallery-box-layout1'>");
                                        atrAccordMonth.Append("                    <img src='"+ strURL + "' alt='Feature' class='img-fluid'>");
                                        atrAccordMonth.Append("                    <div class='item-icon'>");
                                        atrAccordMonth.Append("                        <a href='" + strURL + "' data-fancybox='gallery'>");
                                        atrAccordMonth.Append("                            <i class='fas fa-search-plus'></i>");
                                        atrAccordMonth.Append("                        </a>");
                                        atrAccordMonth.Append("                    </div>");
                                        if (!string.IsNullOrWhiteSpace(subRow.Name) || !string.IsNullOrWhiteSpace(subRow.Description))
                                        {
                                            atrAccordMonth.Append("                    <div class='item-content'>");
                                            atrAccordMonth.Append("                        <h3 class='item-title'>"+ subRow.Name + "");
                                            atrAccordMonth.Append("                        </h3>");
                                            atrAccordMonth.Append("                        <span class='title-ctg'>"+ subRow.Description + "</span>");
                                            atrAccordMonth.Append("                    </div>");
                                        }
                                        atrAccordMonth.Append("                </div>");
                                        atrAccordMonth.Append("            </div>");
                                    }
                                    atrAccordMonth.Append("        </div>");
                                    atrAccordMonth.Append("    </div>");
                                    atrAccordMonth.Append("</li>");
                                }


                            }      
                            else
                            {
                                atrAccordWeek.Append("");

                                if (ImageRecord.Count() > 0)
                                {
                                    atrAccordWeek.Append("<li class='accordion block'>");
                                    atrAccordWeek.Append("    <div class='acc-btn'>");
                                    atrAccordWeek.Append("        <div class='icon-outer'></div>");
                                    atrAccordWeek.Append("        <h6>" + row.AccordTitle + "</h6>");
                                    atrAccordWeek.Append("    </div>");
                                    atrAccordWeek.Append("    <div class='acc-content'>");
                                    atrAccordWeek.Append("        <div class='row'>");
                                    foreach (var subRow in ImageRecord)
                                    {
                                        string strURL;

                                        strURL = (subRow.ImageName);
                                        if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                        {
                                            strURL = strURL.Replace("~/", "");
                                            strURL = ResolveUrl("~/" + strURL);
                                        }

                                        atrAccordWeek.Append("            <div class='col-md-6 col-lg-6'>");
                                        atrAccordWeek.Append("                <div class='gallery-box-layout1'>");
                                        atrAccordWeek.Append("                    <img src='" + strURL + "' alt='Feature' class='img-fluid'>");
                                        atrAccordWeek.Append("                    <div class='item-icon'>");
                                        atrAccordWeek.Append("                        <a href='" + strURL + "' data-fancybox='gallery'>");
                                        atrAccordWeek.Append("                            <i class='fas fa-search-plus'></i>");
                                        atrAccordWeek.Append("                        </a>");
                                        atrAccordWeek.Append("                    </div>");
                                        if (!string.IsNullOrWhiteSpace(subRow.Name) || !string.IsNullOrWhiteSpace(subRow.Description))
                                        {
                                            atrAccordWeek.Append("                    <div class='item-content'>");
                                            atrAccordWeek.Append("                        <h3 class='item-title'>" + subRow.Name + "");
                                            atrAccordWeek.Append("                        </h3>");
                                            atrAccordWeek.Append("                        <span class='title-ctg'>" + subRow.Description + "</span>");
                                            atrAccordWeek.Append("                    </div>");
                                        }
                                        atrAccordWeek.Append("                </div>");
                                        atrAccordWeek.Append("            </div>");
                                    }
                                    atrAccordWeek.Append("        </div>");
                                    atrAccordWeek.Append("    </div>");
                                    atrAccordWeek.Append("</li>");
                                }

                            }
                        }
                    }
                }
                else
                {
                    languageId = 1;
                    dataMain = objBlogCategoryMasterRepository.GetAllStarOfDetails(languageId).FirstOrDefault();
                    if (languageId != 1)
                    {
                        goto LableData;
                    }
                }
            }
            strMonth = atrAccordMonth.ToString();
            strWeek = atrAccordWeek.ToString();

        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StarOfHospital").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StarOfHospital").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strcontactus.ToString();
        }
    }
}