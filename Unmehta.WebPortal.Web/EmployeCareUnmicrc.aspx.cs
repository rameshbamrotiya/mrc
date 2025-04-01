using BAL;
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
    public partial class EmployeCareUnmicrc : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strQuickLink;
        public static string strPageDetails, strPageName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strQuickLink = Functions.CreateQuickLink("Career", "EmployeCareUnmicrc");
                GetPageDetails();
            }
        }
        private void GetPageDetails()
        {
            using (IUnmicrCareerRepository objUnmicrCareerRepository = new UnmicrCareerRepository(Functions.strSqlConnectionString))
            {
                int languageId = Functions.LanguageId;
                StringBuilder strBoardOfDirector = new StringBuilder();


                string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                //rowURl = rowURl.Substring(1);

                ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                if (IsDisabledTranslate && Functions.LanguageId != 1)
                {
                    {
                        Functions.GetReloadPage(this.Page, ref strBoardOfDirector); // 10 sec page reload with english content logic
                        strPageDetails = strBoardOfDirector.ToString();
                    }
                }
                else
                {
                    var dataMain = objUnmicrCareerRepository.GetUnmicrCareerMasterByLanguageId(languageId);
                    if (dataMain != null)
                    {
                        LableData:
                        strBoardOfDirector = new StringBuilder();
                        if (!string.IsNullOrWhiteSpace(dataMain.UnmicrcEmployeeCareDescription))
                        {
                            strPageName = dataMain.UnmicrcEmployeeCareTitle;
                            strPageDetails = Functions.CustomHTMLDecode(dataMain.UnmicrcEmployeeCareDescription, this);
                        }
                        else
                        {
                            languageId = 1;
                            dataMain = objUnmicrCareerRepository.GetUnmicrCareerMasterByLanguageId(languageId);
                            if (languageId == 1)
                            {
                                goto LableData;
                            }
                        }
                    }
                }
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EmployeCareUnmicrc").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EmployeCareUnmicrc").FirstOrDefault();
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