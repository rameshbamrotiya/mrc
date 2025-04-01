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
    public partial class FAQsDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strFAQs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strFAQs = GetFAQsSection();
            }
        }
        private string GetFAQsSection()
        {
            FAQMasterBO objBO = new FAQMasterBO();
            objBO.LanguageId = Functions.LanguageId;
            StringBuilder strFAQsCKeditor = new StringBuilder();
            StringBuilder strFAQsAccredation = new StringBuilder();


            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus((int)objBO.LanguageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strFAQsCKeditor); // 10 sec page reload with english content logic
                    return strFAQsCKeditor.ToString();
                }
            }
            else
            {
                DataSet ds = new FAQMasterBAL().GetFAQDetailsByLanguage(objBO);
                if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            strFAQsCKeditor.Append("<div class='col-lg-12'>");
                            strFAQsCKeditor.Append(HttpUtility.HtmlDecode(row["FAQDescription"].ToString()));
                            strFAQsCKeditor.Append("</div>");
                            FAQAccredationMasterBO objAccBO = new FAQAccredationMasterBO();
                            objAccBO.FAQDetails_Id = Convert.ToInt32(row["Id"].ToString());
                            objAccBO.Language_id = Functions.LanguageId;
                            DataSet dsAccredationDetails = new FAQMasterBAL().SelectAccredationRecord(objAccBO);
                            strFAQsCKeditor.Append("<div class='col-sm-12 col-md-12 col-lg-12' id='accordion'>");
                            foreach (DataRow rowAccredation in dsAccredationDetails.Tables[0].Rows)
                            {
                                strFAQsCKeditor.Append("<div class='accordion-item-faq'>");
                                strFAQsCKeditor.Append("<div class='accordion__header_faq collapsed' data-toggle='collapse' data-target='#collapse" + i + "' aria-expanded='false'>");
                                strFAQsCKeditor.Append("<a class='accordion__title_faq' href='#'><i class='far fa-question-circle'></i> " + rowAccredation["AccredationTitle"].ToString() + "</a>");
                                strFAQsCKeditor.Append("</div>");
                                strFAQsCKeditor.Append("<div id='collapse" + i + "' class='collapse' data-parent='#accordion'>");
                                strFAQsCKeditor.Append("<div class='accordion__body_faq'>");
                                strFAQsCKeditor.Append(HttpUtility.HtmlDecode(rowAccredation["AccredationDescription"].ToString()));
                                strFAQsCKeditor.Append("</div>");
                                strFAQsCKeditor.Append("</div>");
                                strFAQsCKeditor.Append("</div>");
                                i++;
                            }
                            strFAQsCKeditor.Append("</div>");
                        }
                    }
                }
            }
            return strFAQsCKeditor.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "FAQsDetails").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "FAQsDetails").FirstOrDefault();
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