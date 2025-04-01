using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class StaroftheUnmicrc : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strstaroftheweek;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //strCareer = GetPageData();
                strHeaderImage = GetHeaderImage();
                strstaroftheweek = GetListOfSubSectionDescription();
                //strCareerjobwalkinlist = GetPageDataDefaultjobWalkin();
                //BindGridView();
            }
        }
        private string GetListOfSubSectionDescription()
        {
            //int languageId = Functions.LanguageId;
            int languageId = 1;
            StringBuilder strResearch = new StringBuilder();
            DataSet ds = new DataSet();
            //ds = new ArticleDepartmentBAL().SelectRecordbylanguage(osid, languageId); // load english content and translate it.
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string wee = Enum.GetName(typeof(DayOfWeek), Convert.ToInt32(Eval("Week")));
                        strResearch.Append("<div class='col-lg-3'>");
                        strResearch.Append("<div class='front_widget'>");
                        strResearch.Append("<div class='doc-img'>");
                        strResearch.Append("<a href='#' tabindex='0'>");
                        strResearch.Append("<img class='img-fluid' alt='User Image' src='" + ResolveUrl(row["Imgpath"].ToString()) + "'>");
                        strResearch.Append("</a>");
                        strResearch.Append("</div>");
                        strResearch.Append("<!-- About Details -->");
                        strResearch.Append("<div class='front_content'>");
                        strResearch.Append("<div class='specialities-img'>");
                        strResearch.Append("<img src='" + ResolveUrl(row["Iconpath"].ToString()) + "' alt=''>");
                        strResearch.Append("</div>");
                        strResearch.Append("<h3 class='title'>" + row["AD_Name"] + "<br>&nbsp;</h3>");
                        strResearch.Append("<p class='speciality'>" + row["AD_Title"] + "</p>");
                        //strResearch.Append("<a href='" + strURL + "' class='readmore_btn' tabindex='0'><i class='fas fa-chevron-circle-right'></i>Read more</a>");
                        strResearch.Append("</div>");
                        strResearch.Append("</div>");
                        strResearch.Append("</div>");
                        i++;
                    }
                }
            }
            return strResearch.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StaroftheUnmicrc").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "StaroftheUnmicrc").FirstOrDefault();
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