using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class SearchDetails : System.Web.UI.Page
    {
        public static string strLists;
        public static string strSearch;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder strList = new StringBuilder();
            if(!IsPostBack)
            {
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

                if(string.IsNullOrWhiteSpace(strQueryString))
                {
                    Response.Redirect("/");
                }
                else
                {
                    strSearch = strQueryString;
                    using (IGlobalSearchMasterRepository objGlobalSearchMasterRepository = new GlobalSearchMasterRepository(Functions.strSqlConnectionString))
                    {
                        var dtList = objGlobalSearchMasterRepository.GetAllongAboutUsMaster(Functions.LanguageId, strQueryString);
                        if(dtList!=null)
                        {
                            strList.Append("");
                            foreach(var row in dtList)
                            {
                                strList.Append("<div class='col-md-3'>");
                                strList.Append("    <div class='card '>");
                                strList.Append("        <div class='card-body'>");
                                strList.Append("            <h5 class='card-title'> <a href='"+ ResolveUrl(GetURLFromPageName(row.PageName, row))+"'> "+row.MetaTitle+"</a></h5>");
                                strList.Append("            <p>");
                                strList.Append("               "+row.MetaDescription);
                                strList.Append("            </p>");
                                strList.Append("        </div>");
                                strList.Append("    </div>");
                                strList.Append("</div>");
                            }
                        }
                    }
                    strLists = strList.ToString();
                }
            }
        }

        private string GetURLFromPageName(string pageName, GetAllDetailMetaDescriptionResult row)
        {
            
            switch (pageName)
            {
                case "Blog":
                    return "~/BlogDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()));
                case "AboutUs":
                    return "~/About/";
                case "History":
                    return "~/About/AboutHistory";
                case "FutureVision":
                    return "~/About/AboutFutureMission";
                case "DirectorMessage":
                    return "~/About/DirectorDesk";
                case "GoverningBoard":
                    return "~/About/AboutBoardOfDirector";
                case "VisionMission":
                    return "~/About/AboutVisionMission";
                case "Affilation":
                    return "~/About/Affiliation";
                case "AcademicMedical":
                    return "~/AcademicMedical";
                case "Award":
                    return "~/About/AwardDetails";
                case "NursingCare":
                    return "~/NursingCare";
                case "HealthTips":
                    return "~/HealthTipDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()));
                case "Scheme":
                    return "~/Schema?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()));
                case "Tender":
                    return "~/TenderDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()));
                case "OurExcellence":
                    return "~/DepartmentsDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(row.Id.ToString()));
                case "PhotoAlbum":
                    return "~/GalleryDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode("Id= " + row.Id + "|Flag=" + 0)); ;
                case "VideoAlbum":
                    return "~/GalleryDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode("Id= " + row.Id + "|Flag=" + 1)); ;
                default:
                    return "";
            }
        }
    }
}