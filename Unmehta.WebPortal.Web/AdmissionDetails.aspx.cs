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
    public partial class AdmissionDetails : System.Web.UI.Page
    {
        public static string strBoard;
        public static string strHeaderImage;
        public static string strQuickLink;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strBoard = GetPageData();
                strHeaderImage = GetHeaderImage();
                strQuickLink = Functions.CreateQuickLink("Home", "AdmissionDetails");
            }
        }

        private string GetHeaderImage()
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
                using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
                {
                    var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AdmissionDetails").FirstOrDefault();

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
                            dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "AdmissionDetails").FirstOrDefault();
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

        private string GetPageData()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();

            DataSet ds = new AdmissionPageDetailsBAL().SelectAdmission(languageId);
        LableData:
            if (ds != null)
            {
                strBoardOfDirector = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    strBoardOfDirector.Append("<div class='card  mb-0'>");
                    strBoardOfDirector.Append("	<div class='card-body'>");
                    strBoardOfDirector.Append("		<div class='table-responsive'>");
                    strBoardOfDirector.Append("			<table class='table table-hover table-center mb-0 maintable'>");
                    strBoardOfDirector.Append("				<thead>");
                    strBoardOfDirector.Append("					<tr>");
                    strBoardOfDirector.Append("						<th>Sr No.</th>");
                    strBoardOfDirector.Append("						<th>Course Name</th>");
                    strBoardOfDirector.Append("						<th>Year Of Admission</th>");
                    strBoardOfDirector.Append("						<th>List Of Student</ th>");
                    strBoardOfDirector.Append("					</tr>");
                    strBoardOfDirector.Append("				</thead>");
                    strBoardOfDirector.Append("				<tbody>");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strURL = ResolveUrl(row["AdmissionFilePath"].ToString());
                        strBoardOfDirector.Append("");
                        strBoardOfDirector.Append("<tr>");
                        strBoardOfDirector.Append("	<td>" + i + "</td>");
                        strBoardOfDirector.Append("	<td>" + row["CourseName"] + "</td>");
                        strBoardOfDirector.Append("	<td>" + row["YearOfAdmission"] + "</td>");
                        strBoardOfDirector.Append("	<td>");
                        strBoardOfDirector.Append("		<h2 class='table-avatar'>");
                        strBoardOfDirector.Append("			<a href='" + strURL + "' target='_blank' class='btn btn-sm bg-info-light'>");
                        strBoardOfDirector.Append("				<i class='far fa-eye'></i> View");
                        strBoardOfDirector.Append("			</a>");
                        strBoardOfDirector.Append("		</h2>");
                        strBoardOfDirector.Append("	</td>");
                        strBoardOfDirector.Append("</tr>");
                        i++;
                    }
                    strBoardOfDirector.Append("    </tbody>");
                    strBoardOfDirector.Append("				</table>");
                    strBoardOfDirector.Append("			</div>");
                    strBoardOfDirector.Append("		</div>");
                    strBoardOfDirector.Append("	</div>");
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    ds = new AdmissionPageDetailsBAL().SelectAdmission(languageId);
                        goto LableData;
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
    }
}