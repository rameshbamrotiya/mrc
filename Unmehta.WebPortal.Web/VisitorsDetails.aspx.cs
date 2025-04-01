using BAL;
using BO;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class VisitorsDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfImages;
        public static string strListOfImagespopup;
        public static string strListOfVisitingHoursDesc;
        public static string strListOfDDDescription;
        public static string strQuickLink;
        public static string strVisitorsMain;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strQuickLink = Functions.CreateQuickLink("HiddenPage", "VisitorsDetails");

                strVisitorsMain = "";


                strListOfImages = GetListOfimages();
                strListOfImagespopup = GetListOfimagesPopUP();
                strListOfVisitingHoursDesc = GetListOfVisitingHoursDesc();
                strListOfDDDescription = GetListOfDDDescription();

                string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                //rowURl = rowURl.Substring(1);
                StringBuilder strResearch = new StringBuilder();
                ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(Functions.LanguageId, rowURl);
                if (IsDisabledTranslate && Functions.LanguageId != 1)
                {
                    {
                        Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                        strVisitorsMain = strResearch.ToString();
                    }
                }
                else
                {

                    StringBuilder strDDDescription = new StringBuilder();
                    strDDDescription.Append("");
                    strDDDescription.Append("  <div class='col-lg-9'>");
                    strDDDescription.Append("     <div class='card'>");
                    strDDDescription.Append("         <div class='card-body ourdept'>");
                    strDDDescription.Append("             <nav class='user-tabs mb-4'>");
                    strDDDescription.Append("                 <ul class='nav nav-tabs nav-tabs-bottom'>");
                    strDDDescription.Append("                     <li class='nav-item'>");
                    strDDDescription.Append("                         <a class='nav-link active' href='#doc_overview' data-toggle='tab'>Facilities</a>");
                    strDDDescription.Append("                     </li>");
                    strDDDescription.Append("                     <li class='nav-item'>");
                    strDDDescription.Append("                         <a class='nav-link' href='#doc_locations' data-toggle='tab'>Visiting Hours</a>");
                    strDDDescription.Append("                     </li>");
                    strDDDescription.Append("                     <li class='nav-item'>");
                    strDDDescription.Append("                         <a class='nav-link' href='#doc_reviews' data-toggle='tab'>Do’s & Don’ts</a>");
                    strDDDescription.Append("                     </li>");
                    strDDDescription.Append("                 </ul>");
                    strDDDescription.Append("             </nav>");
                    strDDDescription.Append("             <div class='tab-content pt-0'>");
                    strDDDescription.Append("                 <div role = 'tabpanel' id='doc_overview' class='tab-pane fade active show'>");
                    strDDDescription.Append("                     <div class='visitor-list list-unstyled mb-0 row flex-wrap justify-content-between'>");
                    strDDDescription.Append("                         " + strListOfImages);
                    strDDDescription.Append("                     </div>");
                    strDDDescription.Append("                 </div>");
                    strDDDescription.Append("                 <div role = 'tabpanel' id='doc_locations' class='tab-pane fade'>");
                    strDDDescription.Append("                     <div class='widget business-widget'>");
                    strDDDescription.Append("                         <div class='widget-content'>");
                    strDDDescription.Append("                             <div class='listing-hours'>");
                    strDDDescription.Append("                                 " + strListOfVisitingHoursDesc);
                    strDDDescription.Append("                             </div>");
                    strDDDescription.Append("                         </div>");
                    strDDDescription.Append("                     </div>");
                    strDDDescription.Append("                 </div>");
                    strDDDescription.Append("                 <div role = 'tabpanel' id='doc_reviews' class='tab-pane fade'>");
                    strDDDescription.Append("                     <div class='course-overview'>");
                    strDDDescription.Append("                         <div class='inner-box'>");
                    strDDDescription.Append("                            " + strListOfDDDescription);
                    strDDDescription.Append("                         </div>");
                    strDDDescription.Append("                     </div>");
                    strDDDescription.Append("                 </div>");
                    strDDDescription.Append("             </div>");
                    strDDDescription.Append("         </div>");
                    strDDDescription.Append("     </div>");
                    strDDDescription.Append(" </div>");
                    strDDDescription.Append(" <div class='col-lg-3'>");
                    strDDDescription.Append("     <div class='sidebar'>");
                    strDDDescription.Append("         <div class='card widget-categories'>");
                    strDDDescription.Append("             <div class='card-header'>");
                    strDDDescription.Append("                 <h4 class='card-title'>Quick Links</h4>");
                    strDDDescription.Append("             </div>");
                    strDDDescription.Append("             <div class='card-body'>");
                    strDDDescription.Append("                 <ul class='categories'>");
                    strDDDescription.Append("                     " + strQuickLink);
                    strDDDescription.Append("                 </ul>");
                    strDDDescription.Append("             </div>");
                    strDDDescription.Append("         </div>");
                    strDDDescription.Append("     </div>");
                    strDDDescription.Append(" </div>");

                    strVisitorsMain = strDDDescription.ToString();
                }
               
            }
        }
        private string GetListOfimages()
        {
            int languageId = Functions.LanguageId;
            DataSet ds = new VisitorsMasterBAL().SelectRecordFacilityFront(languageId);
            StringBuilder strListOfImages = new StringBuilder();
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    DataRow dr = ds.Tables[0].Rows[0];
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strListOfImages.Append("<div class='visitor-item col-lg-4'>");
                        strListOfImages.Append("<a href='#' type='button' data-toggle='modal' data-target='#exampleModalLong" + i + "' data-whatever='@mdo' data-original-title='' title='' class='item-btn'>");
                        strListOfImages.Append("<div class='visitor_icon'>");
                        if (!string.IsNullOrWhiteSpace(row["Iconpath"].ToString()))
                        {
                            strListOfImages.Append("<img src='" + ResolveUrl(row["Iconpath"].ToString()) + "' class='fas'>");
                        }
                        strListOfImages.Append("</div>");
                        strListOfImages.Append("<h2 class='visitor_title'>" + row["ImgTitle"].ToString() + "</h2>");
                        strListOfImages.Append("</a>");
                        strListOfImages.Append("</div>");
                        i++;
                    }
                }
            }

            return strListOfImages.ToString();
        }
        private string GetListOfimagesPopUP()
        {
            int languageId = Functions.LanguageId;
            DataSet ds = new VisitorsMasterBAL().SelectRecordFacilityFront(languageId);
            StringBuilder strListOfImagespopup = new StringBuilder();
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strListOfImagespopup.Append("<div class='modal fade' id='exampleModalLong" + i + "' role='dialog' aria-labelledby='exampleModalLongTitle" + i + "' aria-hidden='true'>");
                        strListOfImagespopup.Append("<div class='modal-dialog modal-dialog-centered' role='document'>");
                        strListOfImagespopup.Append("<div class='modal-content'>");
                        strListOfImagespopup.Append("<div class='modal-header'>");
                        strListOfImagespopup.Append("<h5 class='modal-title' id='exampleModalLongTitle" + i + "'>" + row["ImgTitle"].ToString() + "</h5>");
                        strListOfImagespopup.Append("<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''><span aria-hidden='true'>×</span></button>");
                        strListOfImagespopup.Append("</div>");
                        strListOfImagespopup.Append("<div class='modal-body'>");
                        strListOfImagespopup.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(row["ImgPOPUpDesc"].ToString())));
                        strListOfImagespopup.Append("</div>");
                        strListOfImagespopup.Append("</div>");
                        strListOfImagespopup.Append("</div>");
                        strListOfImagespopup.Append("</div>");
                        i++;
                    }
                }
            }

            return strListOfImagespopup.ToString();
        }
        private string GetListOfVisitingHoursDesc()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strVisitingHoursDesc = new StringBuilder();
            DataSet ds = new VisitorsMasterBAL().SelectRecord(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strVisitingHoursDesc.Append(HttpUtility.HtmlDecode(row["VisitingHoursDesc"].ToString()));
                        i++;
                    }
                }
            }
            return strVisitingHoursDesc.ToString();
        }
        private string GetListOfDDDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strDDDescription = new StringBuilder();
            DataSet ds = new VisitorsMasterBAL().SelectRecord(languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strDDDescription.Append(HttpUtility.HtmlDecode(row["DDDescription"].ToString()));
                        i++;
                    }
                }
            }
            return strDDDescription.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "VisitorsDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "VisitorsDetails").FirstOrDefault();
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