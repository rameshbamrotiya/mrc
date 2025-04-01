using BAL;
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
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.About
{
    public partial class Accreditation : System.Web.UI.Page
    {
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageContent.InnerHtml = GetDataOfPage();
                strHeaderImage = GetHeaderImage();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/Accreditation").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/Accreditation").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        private string GetDataOfPage()
        {
            long languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            AccredationMasterBAL accredationMasterBAL = new AccredationMasterBAL();

            DataTable dtMain = accredationMasterBAL.SelectRecord(languageId).Tables[0];


            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
          
            StringBuilder strResearch = new StringBuilder();
            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                    return strResearch.ToString();
                }
            }
            else
            {

                int i = 1;

                strBoardOfDirector.Append("");
                strBoardOfDirector.Append("<!--Tab Menu-->");
                strBoardOfDirector.Append("<nav class='user-tabs mb-4'>");
                strBoardOfDirector.Append("	<ul class='nav nav-tabs nav-tabs-bottom'>");
                foreach (DataRow row in dtMain.Rows)
                {
                    strBoardOfDirector.Append("		<li class='nav-item'>");
                    strBoardOfDirector.Append("			<a class='nav-link " + ((i == 1 ? " active" : "")) + "' href='#tab" + i + "' data-toggle='tab'>" + row["Accredation_Title"].ToString() + "</a>");
                    strBoardOfDirector.Append("		</li>");
                    i++;
                }
                //strBoardOfDirector.Append("		<li class='nav-item'>");
                //strBoardOfDirector.Append("			<a class='nav-link' href='#doc_locations' data-toggle='tab'>Accreditation 2</a>");
                //strBoardOfDirector.Append("		</li>");

                i = 1;

                strBoardOfDirector.Append("	</ul>");
                strBoardOfDirector.Append("</nav>");
                strBoardOfDirector.Append("<!-- /Tab Menu -->");
                strBoardOfDirector.Append("<!-- Tab Content -->");
                strBoardOfDirector.Append("<div class='tab-content'>");
                foreach (DataRow row in dtMain.Rows)
                {
                    long Acc_id = Convert.ToInt32(row["Acc_id"].ToString());

                    string strAccredationDesc = HttpUtility.HtmlDecode(row["Accredation_desc"].ToString());
                    string strImage = ResolveUrl(row["Img_path"].ToString());

                    strBoardOfDirector.Append("	<div role ='tabpanel' id='tab" + i + "' class='tab-pane fade " + ((i == 1 ? " active show" : "")) + "'>");
                    strBoardOfDirector.Append("		<div class='row'>");
                    strBoardOfDirector.Append("			<div class='col-md-8 col-lg-8'>");
                    strBoardOfDirector.Append("				<!-- About Details -->");
                    strBoardOfDirector.Append("				<div class='widget about-widget'>");

                    strBoardOfDirector.Append(strAccredationDesc);

                    strBoardOfDirector.Append("				</div>");
                    strBoardOfDirector.Append("				<!-- /About Details -->");
                    strBoardOfDirector.Append("			</div>");
                    strBoardOfDirector.Append("			<div class='col-lg-4'>");
                    strBoardOfDirector.Append("				<div class='inner-column'>");
                    strBoardOfDirector.Append("					<figure class='image'><img src = '" + strImage + "' alt='' class='img-fluid'>");
                    strBoardOfDirector.Append("					</figure>");
                    strBoardOfDirector.Append("				</div>");
                    strBoardOfDirector.Append("			</div>");
                    strBoardOfDirector.Append("		</div>");

                    DataTable dtSub = accredationMasterBAL.SelectRecordsub(Acc_id, languageId).Tables[0];

                    if (dtSub != null)
                    {
                        if (dtSub.Rows.Count > 0)
                        {

                            strBoardOfDirector.Append("		<div class='row'>");
                            strBoardOfDirector.Append("			<div class='col-md-12 col-lg-12'>");
                            strBoardOfDirector.Append("				<!-- Awards Details -->");
                            strBoardOfDirector.Append("				<div class='widget awards-widget'>");
                            strBoardOfDirector.Append("					<h4 class='widget-title'>" + row["Accredation_Title"].ToString() + " TimeLine</h4>");
                            strBoardOfDirector.Append("					<div class='experience-box'>");
                            strBoardOfDirector.Append("						<ul class='experience-list'>");
                            foreach (DataRow rowSub in dtSub.Rows)
                            {
                                string strAccredationName = rowSub["Accredation_Name"].ToString();
                                string strAccredationMonthYear = rowSub["Ac_Date"].ToString();
                                strBoardOfDirector.Append("							<li>");
                                strBoardOfDirector.Append("								<div class='experience-user'>");
                                strBoardOfDirector.Append("									<div class='before-circle'></div>");
                                strBoardOfDirector.Append("								</div>");
                                strBoardOfDirector.Append("								<div class='experience-content'>");
                                strBoardOfDirector.Append("									<div class='timeline-content'>");
                                strBoardOfDirector.Append("										<p class='exp-year'>" + strAccredationMonthYear + "</p>");
                                strBoardOfDirector.Append("										<h4 class='exp-title'>" + strAccredationName + "</h4>");
                                strBoardOfDirector.Append("									</div>");
                                strBoardOfDirector.Append("								</div>");
                                strBoardOfDirector.Append("							</li>");
                            }
                            strBoardOfDirector.Append("						</ul>");
                            strBoardOfDirector.Append("					</div>");
                            strBoardOfDirector.Append("				</div>");
                            strBoardOfDirector.Append("				<!-- /Awards Details -->");
                            strBoardOfDirector.Append("			</div>");
                            strBoardOfDirector.Append("		</div>");

                        }
                    }
                    strBoardOfDirector.Append("	</div>");

                    i++;

                }
                strBoardOfDirector.Append("</div>");

                return strBoardOfDirector.ToString();
            }
        }
    }
}