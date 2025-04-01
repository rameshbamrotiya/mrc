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
    public partial class MedicalTourism : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strAccords;
        public static string strPackagesModels;
        public static string strIntroduction;
        public static string strFacilities;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strAccords = GetAccords();
                strIntroduction = GetIntroductionSection();
                strFacilities = GetFacilitiesSection();
                GetListOfimagessidebar();
            }
        }
        private string GetIntroductionSection()
        {
            MedicalTourismDocumentBO objBO = new MedicalTourismDocumentBO();
            objBO.Language_id = Functions.LanguageId;
            StringBuilder strIntroductionCKeditor = new StringBuilder();
            StringBuilder strFAQsAccredation = new StringBuilder();
            DataSet ds = new MedicalTourismBAL().GetIntroductionDetailsByLanguage(objBO);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strIntroductionCKeditor.Append("<div class='blog-content'>");
                        strIntroductionCKeditor.Append("<div class='row'>");
                        strIntroductionCKeditor.Append("<div class='col-lg-12'>");
                        strIntroductionCKeditor.Append("<h3 class='blog-title'>Introduction</h3>");
                        strIntroductionCKeditor.Append(HttpUtility.HtmlDecode(row["MTDescription"].ToString()));
                        strIntroductionCKeditor.Append("</div>");
                        strIntroductionCKeditor.Append("</div>");
                        strIntroductionCKeditor.Append("</div>");
                        strIntroductionCKeditor.Append("<br>");
                        MedicalTourismDocumentBO objAccBO = new MedicalTourismDocumentBO();
                        objAccBO.MTDetails_Id = Convert.ToInt32(row["Id"].ToString());
                        objAccBO.Language_id = Functions.LanguageId;
                        DataSet dsAccredationDetails = new MedicalTourismBAL().SelectDocumentRecord(objAccBO);
                        if (!dsAccredationDetails.Tables.Count.Equals(0) && !dsAccredationDetails.Tables[0].Rows.Count.Equals(0))
                        {
                            foreach (DataRow rowAccredation in dsAccredationDetails.Tables[0].Rows)
                            {
                                string strDoc = ResolveUrl((rowAccredation["DocPath"].ToString()));
                                strIntroductionCKeditor.Append("<div class='textwidget mb-25'>");
                                strIntroductionCKeditor.Append("<div class='download'>");
                                strIntroductionCKeditor.Append("<div class='item-download'>");
                                strIntroductionCKeditor.Append("<a href='" + strDoc + "' target='_blank' rel='noopener noreferrer'><i class='fas fa-file-pdf'></i>" + rowAccredation["DocumentTitle"] + "</a>");
                                strIntroductionCKeditor.Append("</div>");
                                strIntroductionCKeditor.Append("</div>");
                                strIntroductionCKeditor.Append("</div>");
                                strIntroductionCKeditor.Append("<br>");
                                i++;
                            }
                        }
                        else
                        {
                            strIntroductionCKeditor.Append("<h3 style='text-align: center;border: 1px solid;margin-bottom: 10px !important;' class='title mb-0'>There is no Document.</h4>");
                        }
                    }
                }
                else
                {
                    strIntroductionCKeditor.Append("<h3 style='text-align: center;border: 1px solid;margin-bottom: 10px !important;' class='title mb-0'>There is no introduction.</h4>");
                }
            }
            return strIntroductionCKeditor.ToString();
        }
        private string GetAccords()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strCareerbind = new StringBuilder();
            StringBuilder strPackagesModel = new StringBuilder();
            int LanguageId = languageId;
            DataSet ds = new MedicalTourismBAL().SelectAccrodianSubRecordFront(LanguageId);
            LableData:
            int i = 1;
            if (ds != null)
            {
                strCareerbind = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    List<DataTable> result = ds.Tables[0].AsEnumerable().GroupBy(row => row.Field<string>("Title")).Select(g => g.CopyToDataTable()).ToList();
                    strPackagesModel.Append("");
                    foreach (DataTable dt in result)
                    {
                        DataRow dr = dt.Rows[0];
                        strCareerbind.Append("<div class='accordion-box'>");
                        strCareerbind.Append("<div class='title-box'>");
                        strCareerbind.Append("<h6>" + dr["Title"] + "</h6>");
                        strCareerbind.Append("</div>");
                        List<DataTable> result1 = dt.AsEnumerable().GroupBy(row => row.Field<string>("MTAType")).Select(g => g.CopyToDataTable()).ToList();
                        foreach (DataTable dt1 in result1)
                        {
                            DataRow dr1 = dt1.Rows[0];
                            strCareerbind.Append("<ul class='accordion-inner'>");
                            strCareerbind.Append("<li class='accordion block'>");
                            strCareerbind.Append("<div class='acc-btn'>");
                            strCareerbind.Append("<div class='icon-outer'></div>");
                            strCareerbind.Append("<h6>" + dr1["MTAType"] + "</h6>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("<div class='acc-content'>");
                            strCareerbind.Append("<div class='row'>");
                            foreach (DataRow row in dt1.Rows)
                            {
                                strCareerbind.Append("                            <div class='col-lg-4 col-md-6  service-block'>");
                                strCareerbind.Append("                                <div class='service-block-one mb-10'>");
                                strCareerbind.Append("                                    <div class='inner-box'>");
                                strCareerbind.Append("                                        <div class='bg-layer' style='background-image: url(Hospital/assets/img/shap-5.png);'>");
                                strCareerbind.Append("                                        </div>");
                                strCareerbind.Append("                                        <h3><a href='#'>" + row["SubTitle"] + "</a></h3>");
                                strCareerbind.Append("                                        <div class='row align-items-center'>");
                                strCareerbind.Append("                                            <div class='col-lg-8'>");
                                strCareerbind.Append("                                                <div class='text'>Rs. " + row["Price"] + "/-</div>");
                                strCareerbind.Append("                                            </div>");
                                strCareerbind.Append("                                            <div class='col-lg-4 text-right'>");
                                strCareerbind.Append("                                                <a href='#' type='button' data-toggle='modal' data-target='#exampleModalLong" + i + "' data-whatever='@mdo' data-original-title='' title='' class='cart-icon'><i class='fas fa-angle-right'></i></a>");
                                strCareerbind.Append("                                            </div>");
                                strCareerbind.Append("                                        </div>");
                                strCareerbind.Append("                                    </div>");
                                strCareerbind.Append("                                </div>");
                                strCareerbind.Append("                            </div>");
                                strPackagesModel.Append("<div class='modal fade' id='exampleModalLong" + i + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLongTitle" + i + "'");
                                strPackagesModel.Append("	aria-hidden='true'>");
                                strPackagesModel.Append("	<div class='modal-dialog' role='document'>");
                                strPackagesModel.Append("		<div class='modal-content'>");
                                strPackagesModel.Append("			<div class='modal-header'>");
                                strPackagesModel.Append("				<h5 class='modal-title' id='exampleModalLongTitle" + i + "'>" + row["SubTitle"] + "</h5>");
                                strPackagesModel.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                                strPackagesModel.Append("					title=''><span aria-hidden='true'>×</span></button>");
                                strPackagesModel.Append("			</div>");
                                strPackagesModel.Append("			<div class='modal-body'><p>");
                                strPackagesModel.Append(HttpUtility.HtmlDecode(row["Description"].ToString()));
                                strPackagesModel.Append("</p>			</div>");
                                strPackagesModel.Append("		</div>");
                                strPackagesModel.Append("	</div>");
                                strPackagesModel.Append("</div>");
                                i++;
                            }
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</li>");
                            strCareerbind.Append("</ul>");
                        }
                        strCareerbind.Append("</div>");
                    }
                }
                else
                {
                    strCareerbind.Append("<h3 style='text-align: center;border: 1px solid;margin-bottom: 10px !important;' class='title mb-0'>There is no Accordion available</h1>");
                }
            }
            strPackagesModels = strPackagesModel.ToString();
            return strCareerbind.ToString();
        }
        private string GetFacilitiesSection()
        {
            MedicalTourismDocumentBO objBO = new MedicalTourismDocumentBO();
            objBO.Language_id = Functions.LanguageId;
            StringBuilder strFacilitiesCKeditor = new StringBuilder();
            DataSet ds = new MedicalTourismBAL().GetFacilitiesDetailsByLanguage(objBO);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    strFacilitiesCKeditor.Append("<h3 class='blog-title'>Facilities</h3>");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strFacilitiesCKeditor.Append("<div class='author-widget clearfix mb-15'>");
                        strFacilitiesCKeditor.Append("<div class='about-author'>");
                        strFacilitiesCKeditor.Append("<div class='row'>");
                        string strimg = ResolveUrl((row["Doc_path"].ToString()));
                        if (i % 2 == 0)
                        {

                            strFacilitiesCKeditor.Append("<div class='col-lg-8'>");
                            strFacilitiesCKeditor.Append("<div class='author-details_1'>");
                            strFacilitiesCKeditor.Append("<h4>" + row["Name"] + "</h4>");
                            strFacilitiesCKeditor.Append("<p class='mb-0'>" + HttpUtility.HtmlDecode(row["Description"].ToString()) + "</p>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("<div class='col-lg-4'>");
                            strFacilitiesCKeditor.Append("<div class='about-author'>");
                            strFacilitiesCKeditor.Append("<div class='profile-widget'>");
                            strFacilitiesCKeditor.Append("<div class='doc-img'>");
                            strFacilitiesCKeditor.Append("<a href='#'><img class='img-fluid' alt='User Image' src='" + strimg + "'></a>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                        }
                        else
                        {

                            strFacilitiesCKeditor.Append("<div class='col-lg-4'>");
                            strFacilitiesCKeditor.Append("<div class='about-author'>");
                            strFacilitiesCKeditor.Append("<div class='profile-widget'>");
                            strFacilitiesCKeditor.Append("<div class='doc-img'>");
                            strFacilitiesCKeditor.Append("<a href='#'><img class='img-fluid' alt='User Image' src='" + strimg + "'></a>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("<div class='col-lg-8'>");
                            strFacilitiesCKeditor.Append("<div class='author-details_1'>");
                            strFacilitiesCKeditor.Append("<h4>" + row["Name"] + "</h4>");
                            strFacilitiesCKeditor.Append("<p class='mb-0'>" + HttpUtility.HtmlDecode(row["Description"].ToString()) + "</p>");
                            strFacilitiesCKeditor.Append("</div>");
                            strFacilitiesCKeditor.Append("</div>");
                        }
                        strFacilitiesCKeditor.Append("</div>");
                        strFacilitiesCKeditor.Append("</div>");
                        strFacilitiesCKeditor.Append("</div>");
                        strFacilitiesCKeditor.Append("<hr>");
                        i++;
                    }
                }
                else
                {
                    strFacilitiesCKeditor.Append("<h3 style='text-align: center;border: 1px solid;margin-bottom: 10px !important;' class='title mb-0'>There is no facilities.</h4>");
                }
            }
            return strFacilitiesCKeditor.ToString();
        }
        protected void GetListOfimagessidebar()
        {
            MedicalTourismDocumentBO objBO = new MedicalTourismDocumentBO();
            objBO.Language_id = Functions.LanguageId;
            DataSet ds = new MedicalTourismBAL().GetIntroductionDetailsByLanguage(objBO);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    this.dtlstDocument.DataSource = ds.Tables[0];
                    this.dtlstDocument.DataBind();
                }
            }
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "MedicalTourism").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "MedicalTourism").FirstOrDefault();
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