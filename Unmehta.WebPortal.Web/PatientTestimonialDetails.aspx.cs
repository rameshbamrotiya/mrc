using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class PatientTestimonialDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strPatientDetails;
        public static string strPatientModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strPatientDetails = GetPatientDetails();
            }
        }

        private string GetPatientDetails()
        {
            StringBuilder strModelPopup = new StringBuilder();
            StringBuilder strPatientDetails = new StringBuilder();
            strPatientDetails.Append("");
            using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
            {
                var DataList = objRoleMasterRepository.GetAllPatientTestimonial().Where(x=> x.IsActive==true).ToList();
                foreach(var row in DataList)
                {
                    string filePath = ResolveUrl(string.IsNullOrWhiteSpace(row.FileFullPath) ? "" : row.FileFullPath);
                    strPatientDetails.Append("<div class='col-md-6 col-lg-4'>");
                    strPatientDetails.Append("	<div class='card widget-profile pat-widget-profile'>");
                    strPatientDetails.Append("		<div class='card-body'>");
                    strPatientDetails.Append("			<div class='pro-widget-content'>");
                    strPatientDetails.Append("				<div class='profile-info-widget'>");
                    strPatientDetails.Append("					<a href='#' class='booking-doc-img'>");
                    strPatientDetails.Append("						<img src='" + filePath + "' alt='User Image'>");
                    strPatientDetails.Append("					</a>");
                    strPatientDetails.Append("					<div class='profile-det-info'>");
                    strPatientDetails.Append("						<h3><a href='#'>" + row.PatientName + "</a></h3>");
                    strPatientDetails.Append("						<div class='patient-details'>");
                    strPatientDetails.Append("							<h5 class='mb-0'><i class='fas fa-map-marker-alt'></i>");
                    strPatientDetails.Append("								"+row.CityName+"</h5>");
                    strPatientDetails.Append("						</div>");
                    strPatientDetails.Append("					</div>");
                    strPatientDetails.Append("				</div>");
                    strPatientDetails.Append("			</div>");
                    strPatientDetails.Append("			<div class='video'>");
                    strPatientDetails.Append("				<iframe src='"+ row.ExternalLink + "' width='940' allowfullscreen></iframe>");
                    strPatientDetails.Append("			</div>");
                    strPatientDetails.Append("			<div class='patient-info'>");
                    strPatientDetails.Append("				<p>" + row.Description.Remove(row.Description.Length/2) + "... <a href='#' data-toggle='modal'");
                    strPatientDetails.Append("						data-target='#exampleModalLong"+row.Id+"' data-original-title='' title=''");
                    strPatientDetails.Append("						style='color: red; text-decoration: underline;'>Read");
                    strPatientDetails.Append("						More</a></p>");
                    strPatientDetails.Append("			</div>");
                    strPatientDetails.Append("		</div>");
                    strPatientDetails.Append("	</div>");
                    strPatientDetails.Append("</div>");

                    strModelPopup.Append("");
                    strModelPopup.Append("<div class='modal fade' id='exampleModalLong" + row.Id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLongTitle" + row.Id + "'");
                    strModelPopup.Append("	aria-hidden='true'>");
                    strModelPopup.Append("	<div class='modal-dialog' role='document'>");
                    strModelPopup.Append("		<div class='modal-content'>");
                    strModelPopup.Append("			<div class='modal-header'>");
                    strModelPopup.Append("					<div class='profile-info-widget'><a href='#' class='booking-doc-img'>");
                    strModelPopup.Append("						<img src='" + filePath + "' alt='User Image' style='width: 90px;' >");
                    strModelPopup.Append("					</a>");
                    strModelPopup.Append("					<div class='profile-det-info'>");
                    strModelPopup.Append("						<h3><a href='#'>" + row.PatientName + "</a></h3>");
                    strModelPopup.Append("						<div class='patient-details'>");
                    strModelPopup.Append("							<h5 class='mb-0'><i class='fas fa-map-marker-alt'></i>");
                    strModelPopup.Append("								" + row.CityName + "</h5>");
                    strModelPopup.Append("						</div>");
                    strModelPopup.Append("					</div></div>");
                    strModelPopup.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                    strModelPopup.Append("					title=''><span aria-hidden='true'>×</span></button>");
                    strModelPopup.Append("			</div>");
                    strModelPopup.Append("			<div class='modal-body'>");
                    strModelPopup.Append("				<p>" + row.Description + "</p>");
                    strModelPopup.Append("			</div>");
                    strModelPopup.Append("		</div>");
                    strModelPopup.Append("	</div>");
                    strModelPopup.Append("</div>");
                }
            }
            strPatientModel = strModelPopup.ToString();
            return strPatientDetails.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PatientTestimonialDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "PatientTestimonialDetails").FirstOrDefault();
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