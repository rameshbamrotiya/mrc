using BAL;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class StentPrice : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strQuickLink;
        public static string strPageDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strPageDetails = GetPageDetails();
                strQuickLink = Functions.CreateQuickLink("HiddenPage", "StentPrice");

            }
        }

        private string GetPageDetails()
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
                    return strBoardOfDirector.ToString();
                }
            }
            else
            {
                using (IStentPriceTypeRepository objBlogCategoryMasterRepository = new StentPriceTypeRepository(Functions.strSqlConnectionString))
                {
                    var dataMain = objBlogCategoryMasterRepository.GetAllStentPriceTypeMasterByLanguageId(languageId).Where(x => x.IsActive == true).FirstOrDefault();
                    if (dataMain != null)
                    {
                        LableData:
                        strBoardOfDirector = new StringBuilder();
                        if (!string.IsNullOrWhiteSpace(dataMain.StentPriceDesc))
                        {
                            strBoardOfDirector.Append(Functions.CustomHTMLDecode(dataMain.StentPriceDesc, this.Page));
                        }
                        else
                        {
                            languageId = 1;
                            dataMain = objBlogCategoryMasterRepository.GetAllStentPriceTypeMasterByLanguageId(languageId).Where(x => x.IsActive == true).FirstOrDefault();
                            if (languageId != 1)
                            {
                                goto LableData;
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StentPrice").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StentPrice").FirstOrDefault();
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