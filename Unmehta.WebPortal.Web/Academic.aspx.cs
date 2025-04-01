using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class Academic : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strMedicalMain;
        public static string strMedicalDescription;
        public static string strParaMedicalDescription;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strMedicalMain = "";

                string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                //rowURl = rowURl.Substring(1);
                StringBuilder strResearch = new StringBuilder();
                ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
                if (IsDisabledTranslate && Functions.LanguageId != 1)
                {
                    {
                        Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                        strMedicalMain = strResearch.ToString();
                    }
                }
                else
                {
                    StringBuilder strMedicalMainDescriptionCKeditor = new StringBuilder();
                    strMedicalMainDescriptionCKeditor.Append("");
                    strMedicalMainDescriptionCKeditor.Append("<div class='col-lg-6 col-sm-6'>");
                    strMedicalMainDescriptionCKeditor.Append("	<div class='second-facility-item'><img src='" + ResolveUrl("~/Hospital/assets/img/facility-icon1.png") + "' alt='Image'>");
                    strMedicalMainDescriptionCKeditor.Append("		<h3>Medical</h3>");
                    strMedicalMainDescriptionCKeditor.Append("        " + GetMedicalDescription());
                    strMedicalMainDescriptionCKeditor.Append("        <a class='read-more' href='" + ResolveUrl("~/AcademicMedicalMain") + "'>Read More <i class='bx bx-plus'></i></a>");
                    strMedicalMainDescriptionCKeditor.Append("	</div>");
                    strMedicalMainDescriptionCKeditor.Append("</div>");
                    strMedicalMainDescriptionCKeditor.Append("<div class='col-lg-6 col-sm-6'>");
                    strMedicalMainDescriptionCKeditor.Append("	<div class='second-facility-item'><img src='" + ResolveUrl("~/Hospital/assets/img/facility-icon1.png") + "' alt='Image'>");
                    strMedicalMainDescriptionCKeditor.Append("		<h3>Para Medical</h3>");
                    strMedicalMainDescriptionCKeditor.Append("        " + GetParaMedicalDescription());
                    strMedicalMainDescriptionCKeditor.Append("        <a class='read-more' href='" + ResolveUrl("~/AcademicParaMedical") + "'>Read More <i class='bx bx-plus'></i></a>");
                    strMedicalMainDescriptionCKeditor.Append("	</div>");
                    strMedicalMainDescriptionCKeditor.Append("</div>");

                    strMedicalMain = strMedicalMainDescriptionCKeditor.ToString();
                }

            }
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Academic").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Academic").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
        private string GetMedicalDescription()
        {
            AcademicsDescriptionMasterBO objbo = new AcademicsDescriptionMasterBO();
            objbo.Language_id = Functions.LanguageId;
            StringBuilder strMedicalMainDescriptionCKeditor = new StringBuilder();

            DataSet ds = new AcademicsDescriptionMasterBAL().SelectAcademicsDescriptionDetailsByLanguage(objbo);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strMedicalMainDescriptionCKeditor.Append(HttpUtility.HtmlDecode(row["MedicalMainDescription"].ToString()));
                        i++;
                    }
                }
            }
            return strMedicalMainDescriptionCKeditor.ToString();
        }
        private string GetParaMedicalDescription()
        {
            AcademicsDescriptionMasterBO objbo = new AcademicsDescriptionMasterBO();
            objbo.Language_id = Functions.LanguageId;
            StringBuilder strParaMedicalMainDescriptionCKeditor = new StringBuilder();

            DataSet ds = new AcademicsDescriptionMasterBAL().SelectAcademicsDescriptionDetailsByLanguage(objbo);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strParaMedicalMainDescriptionCKeditor.Append(HttpUtility.HtmlDecode(row["ParamedicalMainDescription"].ToString()));
                        i++;
                    }
                }
            }
            return strParaMedicalMainDescriptionCKeditor.ToString();
        }
    }
}