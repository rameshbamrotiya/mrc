using BAL;
using BO;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class PrivacyPolicy : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strTermsandConditions;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strTermsandConditions = GetPageData();
                strHeaderImage = GetHeaderImage();
            }
        }
        private string GetPageData()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strTermsandConditions = new StringBuilder();



            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strTermsandConditions); // 10 sec page reload with english content logic
                    return strTermsandConditions.ToString();
                }
            }
            else
            {

                PrivacyPolicyMasterBO objbo = new PrivacyPolicyMasterBO();
                objbo.LanguageId = languageId;
                DataSet ds = new PrivacyPolicyMasterBAL().SelectRecord(objbo);
                LableData:
                if (ds != null)
                {
                    strTermsandConditions = new StringBuilder();
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            strTermsandConditions.Append(HttpUtility.HtmlDecode(row["Description"].ToString()));
                        }
                        i++;
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        objbo.LanguageId = languageId;
                        ds = new PrivacyPolicyMasterBAL().SelectRecord(objbo);
                            goto LableData;
                        }
                    }
                }
            }

            return strTermsandConditions.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PrivacyPolicy").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PrivacyPolicy").FirstOrDefault();
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