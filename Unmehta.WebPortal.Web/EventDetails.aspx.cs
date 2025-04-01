using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class EventDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static int eventid;
        public static string strListOfSubSectionDescription;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                eventid = Convert.ToInt32(queryString.ToString());
                long OSid;
                if (string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out OSid))
                {
                    Response.Redirect("~/Event");
                }
                strHeaderImage = GetHeaderImage();
                strListOfSubSectionDescription = GetListOfSubSectionDescription();
            }
            else
            {
                Response.Redirect("~/");
            }
        }
        private string GetListOfSubSectionDescription(string EventName = "", int EventType = 0)
        {
            int languageId = Functions.LanguageId;
            StringBuilder strEvent = new StringBuilder();
            DataSet ds = new EventMasterBAL().SelectEventFront(EventName, EventType, eventid, languageId);
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        strEvent.Append("<div class='col-lg-8 col-md-12'>");
                        strEvent.Append("<div class='blog-view'>");
                        strEvent.Append("<div class='blog blog-single-post text-center'>");
                        strEvent.Append("<h3 class='blog-title'>" + row["EventName"] + "</h3>");
                        strEvent.Append("<div class='blog-info clearfix'>");
                        strEvent.Append("<div class='post-left'>");
                        strEvent.Append("<ul style='justify-content:center;'>");
                        strEvent.Append("<li><i class='far fa-calendar'></i>" + row["EventStartDate"].ToString() + "</li>");
                        strEvent.Append("<li><i class='far fa-clock'></i>" + row["StartTimeHH"].ToString() + ":" + row["StartTimeMM"].ToString() + " " + row["StartTimeTT"].ToString() + " Onwards</li>");
                        strEvent.Append("<li><i class='fas fa-map-marker-alt'></i>" + row["Location"].ToString() + "</li>");
                        strEvent.Append("</ul>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("<div class='blog-image'>");
                        strEvent.Append("<a href='javascript: void(0);'><img alt='' src='" + ResolveUrl(row["InnerImg"].ToString()) + "'></a>");
                        strEvent.Append("</div>");

                        if(!string.IsNullOrWhiteSpace(row["Venue"].ToString()))
                        {
                            strEvent.Append("<div class='service-list'>");
                            strEvent.Append("<h4 class='widget-title'>Venue</h4>");
                            strEvent.Append("<address>" + (!string.IsNullOrWhiteSpace(row["Venue"].ToString()) ? HttpUtility.HtmlDecode(row["Venue"].ToString()) : "") + "</address>");
                            strEvent.Append("</div>");
                        }

                        #region Patron 
                        string strPatronList = ds.Tables[1].Rows[0]["PatronlistName"].ToString();
                        if (!string.IsNullOrWhiteSpace(strPatronList))
                        {
                            strPatronList= HttpUtility.HtmlDecode(strPatronList);
                            strEvent.Append("<div class='service-list'>");
                            strEvent.Append("<h4 class='widget-title'>Patron list</h4>");
                            strEvent.Append("<ul class='clearfix'>");
                            strEvent.Append("<li>" + strPatronList + "</li>");
                            strEvent.Append("</ul>");
                            strEvent.Append("</div>");

                        }
                        #endregion

                        #region Social Media

                        if(string.IsNullOrWhiteSpace(row["IsOnlineRegistration"].ToString()) ? false : Convert.ToBoolean(row["IsOnlineRegistration"].ToString()))
                        {
                            strEvent.Append("<div class='service-list'>");
                            strEvent.Append("<h4 class='widget-title'>On-line Registration Link</h4>");
                            strEvent.Append("<ul class='clearfix onlinelinks'>");
                            {
                                strEvent.Append("<li><a target='_blank' href='" + ResolveUrl("~/OnlineEventRegistrtion?") +Functions.Base64Encode(eventid.ToString()) + "'> On-line Registration</a></li>");
                            }
                            strEvent.Append("</ul>");
                            strEvent.Append("</div>");
                        }

                        //strEvent.Append("<div class='service-list'>");
                        //strEvent.Append("<h4 class='widget-title'>Social media Links</h4>");
                        //strEvent.Append("<ul class='clearfix onlinelinks'>");
                        //foreach (DataRow row2 in ds.Tables[2].Rows)
                        //{
                        //    strEvent.Append("<li><a target='_blank' href='" + row2["SocialMediaLink"] + "'>" + row2["SocialMediaName"] + "</a></li>");
                        //    i++;
                        //}
                        //strEvent.Append("</ul>");
                        //strEvent.Append("</div>");

                        #endregion

                        strEvent.Append("<div class='service-list'>");
                        strEvent.Append("<h4 class='widget-title'>Organized by</h4>");
                        strEvent.Append("<p>" + row["OrganizedBy"] + "</ p > ");
                        strEvent.Append("</div>");

                        #region Leaflet 
                        if(ds.Tables[2]!=null)
                        {
                            if(ds.Tables[2].Rows.Count>0)
                            {
                                strEvent.Append("<div class='service-list'>");
                                strEvent.Append("<h4 class='widget-title'>Leaflet:</h4>");

                                string strIconUrl = ResolveUrl("~/Hospital/assets/img/pdf.png");
                                strEvent.Append("                                <table class='table table-hover table-center mb-0 maintable'>");
                                strEvent.Append("                                    <thead>");
                                strEvent.Append("                                        <tr>");
                                strEvent.Append("                                            <th>Name</th>");
                                strEvent.Append("                                            <th>View File</th>");
                                strEvent.Append("                                        </tr>");
                                strEvent.Append("                                    </thead>");
                                strEvent.Append("                                    <tbody>");

                                foreach (DataRow row2 in ds.Tables[2].Rows)
                                {
                                    string strURL = ResolveUrl(row2["SocialMediaLink"].ToString());

                                    strEvent.Append("                                        <tr>");
                                    strEvent.Append("                                            <td>" + row2["SocialMediaName"].ToString() + "</td>");
                                    if (!string.IsNullOrEmpty(row2["SocialMediaLink"].ToString()))
                                    {
                                        strEvent.Append("                                            <td style='text-align: center !important'><a href =" + strURL + " target='_blank'><img src = " + strIconUrl + "></a></td>");
                                    }
                                    else
                                    {
                                        strEvent.Append("                                            <td style='text-align: center !important'><span>File Not Found</span></td>");
                                    }

                                    strEvent.Append("                                        </tr>");
                                }
                                strEvent.Append("                                    </tbody>");
                                strEvent.Append("                                </table>");

                                strEvent.Append("</div>");
                            }
                        }
                        #endregion

                        strEvent.Append("<div class='service-list'>");
                        strEvent.Append("<h4 class='widget-title'>Event Details </h4>");
                        strEvent.Append("<p>" + row["EventDetails"] + "</p>");
                        strEvent.Append("</div><br>");

                        #region Event Gallery
                        strEvent.Append("<div class='service-list'>");
                        strEvent.Append("<h4 class='widget-title'>Event Gallery </h4>");
                        strEvent.Append("<p><a target='_blank' href='" + row["EventGalalry"] + "'> View Event Photos </a></p>");
                        strEvent.Append("</div><br>");
                        #endregion

                        strEvent.Append("</div>");

                        #region Share Post
                        strEvent.Append("<div class='card blog-share clearfix'>");
                        strEvent.Append("<div class='card-header'>");
                        strEvent.Append("<h4 class='card-title'>Share the post</h4>");
                        strEvent.Append("</div>");
                        strEvent.Append("<div class='card-body'>");
                        strEvent.Append("<ul class='social-share'>");
                        string strUrl = Request.Url.AbsoluteUri;
                        strEvent.Append("<li><a href='https://twitter.com/intent/tweet?url=" + strUrl + "&text=" + row["EventName"] + "&via=UNMEHTA' title='Twitter' target='_blank'><i class='fab fa-twitter'></i></a></li>");
                        strEvent.Append("<li><a href='https://linkedin.com/' title='Linkedin'><i class='fab fa-linkedin' target='_blank'></i></a></li>");
                        strEvent.Append("<li><a href='https://plus.google.com/share?url=" + strUrl + "' title='Google Plus' target='_blank'><i class='fab fa-google-plus'></i></a></li>");
                        strEvent.Append("</ul>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        #endregion

                        //< !--Blog Sidebar-- >//
                        strEvent.Append("<div class='col-lg-4 col-md-4 col-sm-12'>");
                        strEvent.Append("<div class='event-details-sidebar'>");
                        strEvent.Append("<div class='row'>");
                        strEvent.Append("<div class='col-md-12'>");
                        strEvent.Append("<div class='event-sidebar-info'>");
                        DateTime startdate = Convert.ToDateTime(row["EventStartDate"].ToString());
                        DateTime datetoday = Convert.ToDateTime(DateTime.Now.ToString("dd MMMM yyyy"));
                        if (datetoday < startdate)
                        {
                            string hour = row["StartTimeHH"].ToString();
                            string Min = row["StartTimeMM"].ToString();
                            string tt = row["StartTimeTT"].ToString();

                            if(tt.Contains("PM"))
                            {
                                if(hour=="12")
                                {
                                    hour = "00";
                                }
                                else if(hour.StartsWith("1") && hour.Length==2)
                                {
                                    long lghour = Convert.ToInt32(hour);
                                    lghour = lghour + 12;
                                    if(lghour<24)
                                    {
                                        hour = (lghour<10? ("0"+ lghour): lghour.ToString());
                                    }
                                }
                            }

                            startDate.Value = row["EventStartDate"].ToString()+ " "+ hour+":"+ Min+":00";
                            strEvent.Append("<div class='event-sidebar-timer text-center' id='clockdiv'>");
                            strEvent.Append("<p><span class='days'>Days</span>Days</p>");
                            strEvent.Append("<p><span class='hours'>Hours</span>Hours</p>");
                            strEvent.Append("<p><span class='minutes'>Minutes</span>Minutes</p>");
                            strEvent.Append("<p><span class='seconds'>Seconds</span>Second</p>");
                            strEvent.Append("</div>");
                        }
                        else if (datetoday == startdate)
                        {
                            startDate.Value = row["EventStartDate"].ToString();
                            strEvent.Append("<div class='event-sidebar-timer text-center'>");
                            strEvent.Append("<p style='width:100%; text-align:center; font-size:xx-large;'>Today</p>");
                            strEvent.Append("</div>");
                        }
                        else
                        {
                            strEvent.Append("<div class='event-sidebar-timer text-center'>");
                            strEvent.Append("<p style='width:100%; text-align:center; font-size:xx-large;'>Event Completed</p>");
                            strEvent.Append("</div>");
                        }
                        strEvent.Append("<ul class='list-unstyled event-info-list'>");
                        strEvent.Append("<li>Start Date: <span>" + row["EventStartDate"].ToString() + "</span></li>");
                        strEvent.Append("<li>Time: <span>" + row["StartTimeHH"].ToString() + ":" + row["StartTimeMM"].ToString() + " " + row["StartTimeTT"].ToString() + "</span></li>");
                        strEvent.Append("<li>Seat: <span>" + (string.IsNullOrWhiteSpace(row["Seat"].ToString())?"-": row["Seat"].ToString()) + "</span></li>");
                        strEvent.Append("<li>Place: <span>" + row["Location"].ToString() + "</span></li>");
                        strEvent.Append("<li>Organizer: <span>" + row["Organizer"].ToString() + "</span></li>");
                        strEvent.Append("<li>Phone: <span>" + row["Phone"].ToString() + "</span></li>");
                        strEvent.Append("<li>Email: <span>" + row["Email"].ToString() + "</span></li>");
                        strEvent.Append("<li>Website: <span>" + row["Websitelink"].ToString() + "</span></li>");
                        strEvent.Append("</ul>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        //< !-- / Blog Sidebar-- >//
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EventDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "EventDetails").FirstOrDefault();
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