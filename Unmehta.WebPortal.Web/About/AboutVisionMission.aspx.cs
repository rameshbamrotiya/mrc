using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class AboutVisionMission : System.Web.UI.Page
    {
        public static string strVisionMission;
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                strVisionMission = GetVisionMission();
                strHeaderImage = GetHeaderImage();
            }
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutVisionMission").FirstOrDefault();

                if (dataMain != null)
                {
                    LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl( ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutVisionMission").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        private string GetVisionMission()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strVisionMission = new StringBuilder();
            using (IVisionMissionRepository objBlogCategoryMasterRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllVisionMissionMaster(languageId).Where(x=> x.IsVisible==true).ToList();

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
                    if (dataList.Count() > 0)
                    {
                        foreach (var row in dataList)
                        {
                            //var dataListModel = objBlogCategoryMasterRepository.GetAllVisionMissionMasterImageDetailsByLangId(row.Id, languageId).FirstOrDefault();
                            //string strImagePath = "#";
                            //if(dataListModel!=null)
                            //{
                            //     strImagePath = ResolveUrl("~" + ConfigDetailsValue.VisionMissionFilePath + dataListModel.ImageName);

                            //}
                            //strVisionMission.Append("<div class='col-md-6 col-lg-4 col-sm-12'>");
                            //strVisionMission.Append("    <!-- Blog Post -->");
                            //strVisionMission.Append("    <div class='blog grid-blog'>");
                            //strVisionMission.Append("	    <div class='blog-image'>");
                            //strVisionMission.Append("		    <a href='#'><img class='img-fluid' src='" + strImagePath + "' alt='Post Image'></a>");
                            //strVisionMission.Append("	    </div>");
                            //strVisionMission.Append("	    <div class='blog-content'>");
                            //strVisionMission.Append("		    <h3 class='blog-title text-center'><a href='#'>"+ row.MetaTitle + "</a>");
                            //strVisionMission.Append("		    </h3>");
                            //strVisionMission.Append("		    <p> " + HttpUtility.HtmlDecode(row.Descr) + "</p>");
                            //strVisionMission.Append("	    </div>");
                            //strVisionMission.Append("    </div>");
                            //strVisionMission.Append("    <!-- /Blog Post -->");
                            //strVisionMission.Append("</div>");
                            strVisionMission.Append(HttpUtility.HtmlDecode(row.Descr));
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                            dataList = objBlogCategoryMasterRepository.GetAllVisionMissionMaster(languageId).Where(x => x.IsVisible == true).ToList();
                            goto LableData;
                        }
                    }
                }

            }
            return strVisionMission.ToString();
        }
    }
}