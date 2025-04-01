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

namespace Unmehta.WebPortal.Web.Other_Specialties
{
    public partial class Otherspecialitydetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfImages;
        public static string strListOfImagespopup;
        public static string strListOfSubSectionDescription;
        public static string strListOfSubSectionDescriptionFacility;
        public static string strListOfSubSectionDescriptionStaffDetails;
        public static string strsidebar;
        public static string strChartTable;
        public static string strScript;
        public static int osid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

                //osid = Convert.ToInt32(queryString.ToString());
                int OSid = 0;
                if (string.IsNullOrWhiteSpace(queryString))
                {
                    Response.Redirect("~/OtherSpecialties/Otherspeciality");
                }
                else if (!int.TryParse(queryString, out OSid))
                {
                    Response.Redirect("~/OtherSpecialties/Otherspeciality");
                }
                else
                {
                    if (OSid > 0)
                    {
                        osid = OSid;
                        strHeaderImage = GetHeaderImage();
                        strListOfImages = GetListOfimages();
                        strListOfImagespopup = GetListOfimagespopup();
                        strListOfSubSectionDescription = GetListOfSubSectionDescription();
                        strListOfSubSectionDescriptionFacility = GetListOffacility();
                        strListOfSubSectionDescriptionStaffDetails = GetListOfStaffDetails();
                        if (string.IsNullOrWhiteSpace(strListOfSubSectionDescriptionFacility) && string.IsNullOrWhiteSpace(strListOfSubSectionDescriptionStaffDetails))
                        {
                            SubDetails.Style.Add("display", "none");
                        }
                        else
                        {
                            if(string.IsNullOrWhiteSpace(strListOfSubSectionDescriptionFacility))
                            {
                                liFacility.Style.Add("display", "none");
                            }
                            if (string.IsNullOrWhiteSpace(strListOfSubSectionDescriptionStaffDetails))
                            {
                                liStaff.Style.Add("display", "none");
                            }

                            SubDetails.Style.Add("display", "block");
                        }
                        GetListOfimagessidebar();
                    }
                    else
                    {
                        Response.Redirect("~/OtherSpecialties/Otherspeciality");
                    }

                }
            }
        }

        private string GetListOfSubSectionDescription()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            StringBuilder strChart = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(osid, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        liTitle.InnerHtml = row["Title"].ToString();
                        strAwardsAndAchievements.Append("<h4 class='widget-title'>" + row["Title"].ToString() + "</h4>");
                        strAwardsAndAchievements.Append(HttpUtility.HtmlDecode(row["InnerDesc"].ToString()));

                        long lgStateId = 0;
                        if (long.TryParse(row["StatisticsId"].ToString(), out lgStateId))
                        {
                            if (lgStateId != 0)
                            {
                                strScript = Functions.GenerateScriptGraph(lgStateId, "chartContainer", out strChartTable);

                                if (!string.IsNullOrWhiteSpace(strScript))
                                {
                                    strChart.Append("<div class='statistics'> <h4 class='widget-title'>Statistics</h4> <div class='table-responsive'>");
                                    strChart.Append(strChartTable);
                                    strChart.Append("</div>");
                                    strChart.Append("</div>");
                                    strChart.Append("<br> <div class=\"canvasbg\"></div> <div class=\"canvasbg_right\"></div> <div id='chartContainer' style='height: 400px; width: 100%; overflow: auto;'> </div>");
                                }
                            }
                        }
                        i++;
                    }
                }
            }
            strChartTable = "";
            strChartTable = strChart.ToString();
            return strAwardsAndAchievements.ToString();
        }
        private string GetListOffacility()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strfacility = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordFacility(osid, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strURL = "";

                        if (!string.IsNullOrWhiteSpace(row["Img_path"].ToString()))
                        {
                            strURL = (row["Img_path"].ToString());
                            if (strURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURL = strURL.Replace("~/", "");
                                strURL = ResolveUrl("~/" + strURL);
                            }

                        }
                        if(i%2==0)
                        {

                            strfacility.Append("<div class='author-widget clearfix '>");
                            strfacility.Append("	<div class='about-author'>");
                            strfacility.Append("		<div class='row'>");
                            strfacility.Append("			<div class='col-lg-3'>");
                            strfacility.Append("				<div class='about-author'>");
                            strfacility.Append("					<div class='author-img-wrap'>");
                            strfacility.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strURL + "'></a>");
                            strfacility.Append("					</div>");
                            strfacility.Append("				</div>");
                            strfacility.Append("			</div>");
                            strfacility.Append("			<div class='col-lg-9'>");
                            strfacility.Append("				<div class='author-details_1'>");
                            strfacility.Append((HttpUtility.HtmlDecode(row["Description"].ToString())));
                            strfacility.Append("				</div>");
                            strfacility.Append("			</div>");
                            strfacility.Append("		</div>");
                            strfacility.Append("	</div>");
                            strfacility.Append("</div>");

                        }
                        else
                        {
                            strfacility.Append("<div class='author-widget clearfix '>");
                            strfacility.Append("	<div class='about-author'>");
                            strfacility.Append("		<div class='row'>");
                            strfacility.Append("			<div class='col-lg-9'>");
                            strfacility.Append("				<div class='author-details_1'>");
                            strfacility.Append((HttpUtility.HtmlDecode(row["Description"].ToString())));
                            strfacility.Append("				</div>");
                            strfacility.Append("			</div>");
                            strfacility.Append("			<div class='col-lg-3'>");
                            strfacility.Append("				<div class='about-author'>");
                            strfacility.Append("					<div class='author-img-wrap'>");
                            strfacility.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strURL + "'></a>");
                            strfacility.Append("					</div>");
                            strfacility.Append("				</div>");
                            strfacility.Append("			</div>");
                            strfacility.Append("		</div>");
                            strfacility.Append("	</div>");
                            strfacility.Append("</div>");
                            

                        }
                        strfacility.Append("<hr>");
                        i++;
                    }
                }
            }
            return strfacility.ToString();
        }
        private string GetListOfStaffDetails()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strStafDetails = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordStafDetails(osid, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    var dataAll = ds.Tables[0].AsEnumerable().ToList();
                    var data = dataAll.Where(x=> !string.IsNullOrWhiteSpace(x.Field<string>("Img_path")));

                    int i = 1;
                    strStafDetails.Append("<div class='tabledummyclass col-md-12 col-lg-12'> <div class='table-responsive'>");
                    strStafDetails.Append("			<table class='table table-hover table-center text-center mb-0 maintable'>");
                    strStafDetails.Append("				<thead>");
                    strStafDetails.Append("					<tr>");
                    strStafDetails.Append("						<th>Sr No</th>");
                    strStafDetails.Append("						<th>Name</th>");
                    strStafDetails.Append("						<th>DESIGNATION</th>");
                    if (data.Count() > 0)
                    {
                        strStafDetails.Append("						<th>Photo</th>");
                    }
                    strStafDetails.Append("					</tr>");
                    strStafDetails.Append("				</thead>");
                    strStafDetails.Append("				<tbody>");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strStafDetails.Append("");
                        strStafDetails.Append("<tr>");
                        strStafDetails.Append("	<td>" + i + "</td>");
                        strStafDetails.Append("	<td>" + row["staffname"].ToString() + "</td>");
                        strStafDetails.Append("	<td>" + row["designation"].ToString() + "</td>");
                        if (data.Count() > 0)
                        {
                            strStafDetails.Append("	<td>");
                            strStafDetails.Append("		<h2 class='table-avatar'>");
                            if (data.Count() > 0)
                            {
                                strStafDetails.Append("			<a href='#' class='avatar avatar-xl mr-2'>");
                                strStafDetails.Append("				<img class='avatar-img rounded-circle' src='" + ResolveUrl(row["Img_path"].ToString()) + "' alt='User Image'>");
                                strStafDetails.Append("			</a>");
                            }
                            strStafDetails.Append("		</h2>");
                            strStafDetails.Append("	</td>");
                        }
                        strStafDetails.Append("</tr>");
                        i++;
                    }
                    strStafDetails.Append("    </tbody>");
                    strStafDetails.Append("				</table>");
                    strStafDetails.Append("			</div>");
                    strStafDetails.Append("		</div>");
                }
            }
            return strStafDetails.ToString();
        }
        private string GetListOfimages()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strListOfImages = new StringBuilder();
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(osid, languageId);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["IsImg"].ToString().ToUpper() == "True".ToUpper())
                {
                    SpecialityMasterBO objBo = new SpecialityMasterBO();
                    objBo.OS_id = osid;
                    objBo.LanguageId = languageId;
                    DataSet dsimg = new SpecialityMasterBAL().SelectRecordIMG(objBo);
                    if (dsimg != null)
                    {
                        divimagesection.Visible = true;
                        if (!dsimg.Tables.Count.Equals(0) && !dsimg.Tables[0].Rows.Count.Equals(0))
                        {
                            int i = 1;
                            foreach (DataRow row in dsimg.Tables[0].Rows)
                            {
                                string strURL = "";

                                if (!string.IsNullOrWhiteSpace(row["Imagepath"].ToString()))
                                {
                                    strURL = (row["Imagepath"].ToString());
                                    if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                    {
                                        strURL = strURL.Replace("~/", "");
                                        strURL = ResolveUrl("~/" + strURL);
                                    }

                                }
                                strListOfImages.Append("<div class='col-lg-3 col-md-6 col-12'>");
                                strListOfImages.Append("<div class='departments-box-layout5'>");
                                strListOfImages.Append("<div class='item-img'>");
                                strListOfImages.Append("<img src='" + strURL + "' alt='department' class='img - fluid'>");
                                strListOfImages.Append("<div class='item-content'>");
                                strListOfImages.Append("<h3 class='item-title title-bar-primary3'>");
                                strListOfImages.Append("<a href='#'>" + row["ImageTitle"].ToString() + "</a>");
                                strListOfImages.Append("</h3>");
                                strListOfImages.Append("<p>" + row["ImageShortDesc"] + "</p> ");
                                strListOfImages.Append("<a href='" + strURL + "' type='button' data-toggle='modal' data-target='#exampleModalCenter" + i + "' data-whatever='@mdo" + i + "' data-original-title='' title=''class='item-btn'>DETAILS</a>");
                                strListOfImages.Append("</div>");
                                strListOfImages.Append("</div>");
                                strListOfImages.Append("</div>");
                                strListOfImages.Append("</div>");
                                i++;
                            }
                        }
                        else
                        {
                            divimagesection.Visible = false;
                        }
                    }
                    else
                    {
                        divimagesection.Visible = false;
                    }
                }
                else
                {
                    divimagesection.Visible = false;
                }
            }
            else
            {
                divimagesection.Visible = false;
            }
            return strListOfImages.ToString();
        }
        private string GetListOfimagespopup()
        {
            int languageId = Functions.LanguageId;
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(osid, languageId);
            DataRow dr = ds.Tables[0].Rows[0];
            StringBuilder strListOfImagespopup = new StringBuilder();
            if (dr["IsImg"].ToString().ToUpper() == "True".ToUpper())
            {
                SpecialityMasterBO objBo = new SpecialityMasterBO();
                objBo.OS_id = osid;
                objBo.LanguageId = languageId;
                DataSet dsimg = new SpecialityMasterBAL().SelectRecordIMG(objBo);
                if (dsimg != null)
                {
                    if (!dsimg.Tables.Count.Equals(0) && !dsimg.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1;
                        foreach (DataRow row in dsimg.Tables[0].Rows)
                        {
                            string strURL = "";

                            if (!string.IsNullOrWhiteSpace(row["Imagepath"].ToString()))
                            {
                                strURL = (row["Imagepath"].ToString());
                                if (strURL.StartsWith("~/", StringComparison.Ordinal))
                                {
                                    strURL = strURL.Replace("~/", "");
                                    strURL = ResolveUrl("~/" + strURL);
                                }

                            }

                            strListOfImagespopup.Append("<div class='modal fade' id='exampleModalCenter" + i + "' role='dialog' aria-labelledby='exampleModalCenter" + i + "' aria-hidden='true'>");
                            strListOfImagespopup.Append("<div class='modal-dialog modal-dialog-centered' role='document'>");
                            strListOfImagespopup.Append("<div class='modal-content'>");
                            strListOfImagespopup.Append("<div class='modal-header'>");
                            strListOfImagespopup.Append("<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''><span aria-hidden='true'>×</span></button>");
                            strListOfImagespopup.Append("</div>");
                            strListOfImagespopup.Append("<div class='modal-body'>");
                            strListOfImagespopup.Append("<div class='row justify-content-center'>");

                            strListOfImagespopup.Append("");
                            strListOfImagespopup.Append("<div class='col-lg-12 mb-25'>");
                            strListOfImagespopup.Append("	<div class='about-author'>");
                            strListOfImagespopup.Append("		<div class='author-img-wrap'>");
                            strListOfImagespopup.Append("			<a href='#'><img class='img-fluid' alt='' src='" + strURL + "'></a>");
                            strListOfImagespopup.Append("		</div>");
                            strListOfImagespopup.Append("	</div>");
                            strListOfImagespopup.Append("</div>");

                            strListOfImagespopup.Append("<div class='col-lg-12'>");
                            strListOfImagespopup.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(row["ImgPopupDesc"].ToString())));
                            strListOfImagespopup.Append("</div>");

                            strListOfImagespopup.Append("</div>");
                            strListOfImagespopup.Append("</div>");
                            strListOfImagespopup.Append("</div>");
                            strListOfImagespopup.Append("</div>");
                            strListOfImagespopup.Append("</div>");
                            i++;
                        }
                    }
                }
            }
            return strListOfImagespopup.ToString();
        }
        protected void GetListOfimagessidebar()
        {
            int languageId = Functions.LanguageId;
            DataSet ds = new SpecialityMasterBAL().SelectRecordSidemenu(osid, languageId);
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherSpecialties/Otherspeciality").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OtherSpecialties/Otherspeciality").FirstOrDefault();
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