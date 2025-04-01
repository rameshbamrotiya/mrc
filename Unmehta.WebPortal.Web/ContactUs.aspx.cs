using BAL;
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
    public partial class ContactUs : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfSubSectionDescription;
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
            StringBuilder strcontactusskeditor = new StringBuilder();


            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strcontactusskeditor); // 10 sec page reload with english content logic
                    return strcontactusskeditor.ToString();
                }
            }
            else
            {
                DataSet ds = new ContactUsMasterBAL().SelectContactUsDetailFront(languageId);
                LableData: if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            strcontactusskeditor.Append(HttpUtility.HtmlDecode(row["ContactUsDescription"].ToString()));
                            i++;
                        }
                    }
                }
                else
                {
                    languageId = 1;
                    ds = new ContactUsMasterBAL().SelectContactUsDetailFront(Functions.LanguageId);
                    if (languageId != 1)
                    {
                        goto LableData;
                    }

                }
            }

            return strcontactusskeditor.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ContactUs").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ContactUs").FirstOrDefault();
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