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
    public partial class CareerDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strCareerDetails;
        public static string strcareerwalkinside;
        public static int Rid;
        int walkin = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            if (queryString == "")
            {
                Response.Redirect("~/career");
            }
            if (queryString.Contains("id"))
            {
                string[] splitString = queryString.Split('&');
                int id1 = Convert.ToInt32(splitString[0].ToString().Replace("id=", ""));
                walkin = Convert.ToInt32(splitString[1].ToString().Replace("walkin=", ""));
                Rid = id1;
            }
            else
            {
                Rid = Convert.ToInt32(queryString.ToString());
            }
            long id;
            if (string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
            {
                Response.Redirect("~/career");
            }
            if (!IsPostBack)
            {
                strCareerDetails = GetCareerDetails();
                strHeaderImage = GetHeaderImage();
                strcareerwalkinside = GetCareerDetailsWalkin();
            }
        }
        private string GetCareerDetails()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strEvent = new StringBuilder();
            DataSet ds = new CareerBAL().SelectCareerDetails(Rid);
            string strIconUrl = ResolveUrl("~/Hospital/assets/img/pdf.png");
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        bool iswalikn = false;
                        if (!string.IsNullOrWhiteSpace(row["isWalkin"].ToString()))
                        {
                            iswalikn = Convert.ToBoolean(row["isWalkin"].ToString());
                        }
                        strEvent.Append("<h3 class='pt-4'>" + row["AdvertisementName"] + "</h3>");
                        strEvent.Append("<hr>");
                        strEvent.Append("<div class='tab-content pt-3'>");
                        strEvent.Append("<div role='tabpanel' id='doc_overview' class='tab-pane fade show active'>");
                        strEvent.Append("<div class='row'>");
                        strEvent.Append("<div class='col-md-9'>");
                        strEvent.Append("<div class='widget about-widget'>");
                        strEvent.Append("<h4 class='widget-title-career'>Job No.</h4>");
                        strEvent.Append("<p>" + row["PostCode"] + "</p>");
                        strEvent.Append("</div>");                        
                        if (iswalikn)
                        {
                            if (!string.IsNullOrWhiteSpace(row["InterviewDate"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Interview Date</h4>");
                                strEvent.Append("<p>" + row["InterviewDate"] + "</p>");
                                strEvent.Append("</div>");
                            }
                            else
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Interview Date</h4>");
                                strEvent.Append("<p>-</p>");
                                strEvent.Append("</div>");
                            }
                            if (!string.IsNullOrWhiteSpace(row["InterviewTime"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Interview Time</h4>");
                                strEvent.Append("<p>" + row["InterviewTime"] + "</p>");
                                strEvent.Append("</div>");
                            }
                            else
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Interview Time</h4>");
                                strEvent.Append("<p>-</p>");
                                strEvent.Append("</div>");
                            }
                            if (!string.IsNullOrWhiteSpace(row["ReportingTime"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Reporting Time</h4>");
                                strEvent.Append("<p>" + row["ReportingTime"] + "</p>");
                                strEvent.Append("</div>");
                            }
                            //else
                            //{
                            //    strEvent.Append("<div class='widget about-widget'>");
                            //    strEvent.Append("<h4 class='widget-title-career'>Reporting Time.</h4>");
                            //    strEvent.Append("<p>-</p>");
                            //    strEvent.Append("</div>");
                            //}
                        }
                        strEvent.Append("<div class='widget about-widget'>");
                        strEvent.Append("<h4 class='widget-title-career'>Department</h4>");
                        strEvent.Append("<p>" + row["Advertisement_Type"] + "</p>");
                        strEvent.Append("</div>");

                        if (!iswalikn)
                        {
                            if (!string.IsNullOrWhiteSpace(row["Gender"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                string gender = row["Gender"].ToString();
                                string[] values = gender.Split(',');
                                string gendername = string.Empty;
                                string[] gendernamecoma;
                                List<string> list = new List<string>();
                                for (int i = 0; i < values.Length; i++)
                                {
                                    switch (values[i])
                                    {
                                        case "1":
                                            gendername = "Male";
                                            break;
                                        case "2":
                                            gendername = "Female";
                                            break;
                                        default:
                                            gendername = "Transcender";
                                            break;
                                    }
                                    list.Add(gendername);
                                }
                                gendernamecoma = list.ToArray();
                                gendername = String.Join(" , ", gendernamecoma);
                                strEvent.Append("<h4 class='widget-title-career'>Gender</h4>");
                                strEvent.Append("<p>" + gendername + "</p>");
                                strEvent.Append("</div>");
                            }
                            else
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Gender</h4>");
                                strEvent.Append("<p>-</p>");
                                strEvent.Append("</div>");
                            }
                            if (!string.IsNullOrWhiteSpace(row["experience"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Work Experience</h4>");
                                strEvent.Append("<p>" + row["experience"] + "</p>");
                                strEvent.Append("</div>");
                            }
                            else
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Work Experience</h4>");
                                strEvent.Append("<p>-</p>");
                                strEvent.Append("</div>");
                            }
                            if (!string.IsNullOrWhiteSpace(row["QualificationList"].ToString()))
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Qualification</h4>");
                                strEvent.Append("<p>" + row["QualificationList"] + "</p>");
                                strEvent.Append("</div>");
                            }
                            else
                            {
                                strEvent.Append("<div class='widget about-widget'>");
                                strEvent.Append("<h4 class='widget-title-career'>Qualification</h4>");
                                strEvent.Append("<p>-</p>");
                                strEvent.Append("</div>");
                            }

                        }

                        if (!string.IsNullOrWhiteSpace(row["FileName"].ToString()))
                        {
                            string filePath = ConfigDetailsValue.AddRecrutmentFileUploadPath;
                            string strURLad = ResolveUrl(filePath + (row["FileName"].ToString()));
                            strEvent.Append("<div class='widget about-widget mb-3'>");
                            strEvent.Append("<h4 class='widget-title-career mr-30'>Post Criteria</h4>");
                            strEvent.Append("<a class='mb-0' href =" + strURLad + " target='_blank'><img src = " + strIconUrl + "></a>");
                            strEvent.Append("</div>");
                        }
                        if (!string.IsNullOrWhiteSpace(row["Generalinstructionfile"].ToString()))
                        {
                            string strURL = ResolveUrl((row["Generalinstructionfile"].ToString()));
                            strEvent.Append("<div class='widget about-widget mb-3'>");
                            strEvent.Append("<h4 class='widget-title-career mr-30'>General Instruction</h4>");
                            strEvent.Append("<a class='mb-0' href =" + strURL + " target='_blank'><img src = " + strIconUrl + "></a>");
                            strEvent.Append("</div>");
                        }
                        strEvent.Append("<div class='widget about-widget mb-3'>");
                        strEvent.Append("<h4 class='widget-title-career'>Description</h4>");
                        strEvent.Append("<p class='mb-0'>" + (HttpUtility.HtmlDecode(row["AdvertisementDesc"].ToString())) + "</p>");
                        strEvent.Append("</div>");
                        strEvent.Append("<div class='load-more mb-0'>");
                        if (!iswalikn)
                        {
                            strEvent.Append("<a href='#' class='btn btn-primary submit-btn text-white'>Apply Now</a>");
                        }
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                    }
                }
            }
            return strEvent.ToString();
        }

        private string GetCareerDetailsWalkin()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strEvent = new StringBuilder();
            DataSet ds = new CareerBAL().SelectRecordJobwalin();
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode("id=" + row["id"].ToString() + "&walkin=1")));
                        strEvent.Append("<li>");
                        strEvent.Append("<div class='post-info'>");
                        strEvent.Append("<h4>");
                        strEvent.Append("<a href='" + strURL + "'>" + row["AdvertisementName"] + "</a>");
                        strEvent.Append("</h4>");
                        strEvent.Append("<p>" + row["publishdateinner"] + "</p>");
                        strEvent.Append("</div>");
                        strEvent.Append("</li>");
                    }
                }
            }
            return strEvent.ToString();
        }
        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CareerDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CareerDetails").FirstOrDefault();
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