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
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public class NotificationModels
    {
        public  string Type { get; set; }
        public  DateTime PublishDate { get; set; }
        public  string Desc { get; set; }
        public  string URL { get; set; }
    }

    public partial class Notification : System.Web.UI.Page
    {
        public static string strNews, strTenders, strCareerAnnouncements;
        public static string strHeaderImage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strNews = GetAllNews();
                strTenders = GetAllTenders();
                strCareerAnnouncements = GetAllCareerAnnouncements();
            }
        }

        private string GetAllCareerAnnouncements()
        {
            StringBuilder sreNews = new StringBuilder();
            sreNews.Append("");
            int languageId = 1;

            {
                List<NotificationModels> lstData = new List<NotificationModels>();
                //General Job
                //DataSet ds = new CareerBAL().SelectRecordJob();
                //ds.Tables[0].DefaultView.Sort = "publishdate";
                //DataTable dt = ds.Tables[0].DefaultView.ToTable();
                //if (dt != null)
                //{
                //    //if (!dt.Rows.Equals(0))
                //    {
                //        if (!dt.Rows.Count.Equals(0))
                //        {
                //            int i = 1;
                //            foreach (DataRow row in dt.Rows)
                //            {
                //                if(i>6)
                //                {
                //                    break;
                //                }
                //                string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode(row["rid"].ToString())));
                //                NotificationModels objNotificationModels = new NotificationModels();
                //                objNotificationModels.URL = strURL;
                //                objNotificationModels.Type = "Interview";
                //                objNotificationModels.PublishDate = Convert.ToDateTime(row["publishdate"].ToString());
                //                objNotificationModels.Desc = HttpUtility.HtmlDecode(row["AdvertisementDesc"].ToString());
                //                lstData.Add(objNotificationModels);

                //                sreNews.Append("");
                //                i++;
                //            }
                //        }
                //    }
                //}

                //General Job
                DataTable ds1 = new CareerBAL().GetAllCareerRecord();
                ds1.DefaultView.Sort = "publishdates desc";
                DataTable dt1 = ds1.DefaultView.ToTable();
                if (dt1 != null)
                {
                    //if (!dt.Rows.Equals(0))
                    {
                        if (!dt1.Rows.Count.Equals(0))
                        {
                            int i = 1;
                            foreach (DataRow row in dt1.Rows)
                            {
                                string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode(row["rid"].ToString())));
                                NotificationModels objNotificationModels = new NotificationModels();
                                objNotificationModels.URL = strURL;
                                objNotificationModels.Type = row["Recruitment_Name"].ToString();
                                objNotificationModels.PublishDate = Convert.ToDateTime(row["publishdate"].ToString());
                                objNotificationModels.Desc = HttpUtility.HtmlDecode(row["AdvertisementName"].ToString())+" "+(row["IsNewIcon"].ToString()=="1"? "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />" : "");
                                lstData.Add(objNotificationModels);

                                i++;
                            }
                        }
                    }
                }


                var mainFilterList = lstData.OrderByDescending(x => x.PublishDate);
                foreach(var row in mainFilterList)
                {

                    sreNews.Append("<li>");
                    sreNews.Append("	<div class='card'>");
                    sreNews.Append("		<div class='card-body'>");
                    sreNews.Append("			<div class='comment'>");
                    sreNews.Append("				<div class='comment-body'>");
                    sreNews.Append("					<div class='meta-data'>");
                    sreNews.Append("						<span class='comment-author'>"+ row.Type + "</span>");
                    sreNews.Append("						<span class='comment-date'>Posted on : " + row.PublishDate.ToString("MMMM dd, yyyy") + "</span>");
                    sreNews.Append("					</div>");
                    sreNews.Append("					<p class='comment-content'>");
                    sreNews.Append("						" + row.Desc + "");
                    sreNews.Append("					</p>");
                    sreNews.Append("					<div class='comment-reply'>");
                    sreNews.Append("						<p class='recommend-btn'>");
                    sreNews.Append("							<a href='" + row.URL + "' class='like-btn'>");
                    sreNews.Append("								View More");
                    sreNews.Append("							</a>");
                    sreNews.Append("						</p>");
                    sreNews.Append("					</div>");
                    sreNews.Append("				</div>");
                    sreNews.Append("			</div>");
                    sreNews.Append("		</div>");
                    sreNews.Append("	</div>");
                    sreNews.Append("</li>");
                }

                //General Job
            }
            return sreNews.ToString();
        }

        private string GetAllTenders()
        {
            StringBuilder sreNews = new StringBuilder();
            sreNews.Append("");
            int languageId = 1;
            using (IMenuMasterRepository objMenu = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objMenu.GetAllTenderMaster();
                if (dataMain != null)
                {
                    foreach (var row in dataMain)
                    {
                        string strURL = ResolveUrl("~/TenderDetails?" + Unmehta.WebPortal.Web.Common.Functions.Base64Encode(row.TenderID.ToString()));
                        sreNews.Append("<li>");
                        sreNews.Append("	<div class='card'>");
                        sreNews.Append("		<div class='card-body'>");
                        sreNews.Append("			<div class='comment'>");
                        sreNews.Append("				<div class='comment-body'>");
                        sreNews.Append("					<div class='meta-data'>");
                        sreNews.Append("						<span class='comment-author'>" + row.Title + "</span>");
                        sreNews.Append("						<span class='comment-date'>Posted on : " + row.EntryDate.Value.ToString("MMMM dd, yyyy") + "</span>");
                        sreNews.Append("					</div>");
                        sreNews.Append("					<p class='comment-content'>");
                        
                        sreNews.Append("						" + row.Details + " " + (row.IsNewIcon == 1 ? "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />" : "") + "");
                        sreNews.Append("					</p>");
                        sreNews.Append("					<div class='comment-reply'>");
                        sreNews.Append("						<p class='recommend-btn'>");
                        sreNews.Append("							<a href='" + strURL + "' class='like-btn'>");
                        sreNews.Append("								View More");
                        sreNews.Append("							</a>");
                        sreNews.Append("						</p>");
                        sreNews.Append("					</div>");
                        sreNews.Append("				</div>");
                        sreNews.Append("			</div>");
                        sreNews.Append("		</div>");
                        sreNews.Append("	</div>");
                        sreNews.Append("</li>");
                    }
                }
            }
            return sreNews.ToString();
        }

        private string GetAllNews()
        {
            StringBuilder sreNews = new StringBuilder();
            sreNews.Append("");
            int languageId = 1;
            StringBuilder strBlogs = new StringBuilder();
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).Where(x => x.IsVisible == true).OrderByDescending(x => x.BlogDate).ToList().ToList();
                foreach (var row in dataList)
                {

                    string strBlogName = row.BlogName;
                    if (row.BlogName.Length > 40)
                    {
                        strBlogName = row.BlogName.Remove(40);
                    }
                    string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                    string strURL = ResolveUrl(("~/BlogDetails?" + Functions.Base64Encode(row.Id.ToString() + "|" + row.TypeDetail)));

                    sreNews.Append("<li>");
                    sreNews.Append("	<div class='card'>");
                    sreNews.Append("		<div class='card-body'>");
                    sreNews.Append("			<div class='comment'>");
                    sreNews.Append("				<div class='comment-body'>");
                    sreNews.Append("					<div class='meta-data'>");
                    sreNews.Append("						<span class='comment-author'>" + row.TypeDetail + "</span>");
                    sreNews.Append("						<span class='comment-date'>Posted on : " + row.BlogDate.ToString("MMMM dd, yyyy") + "</span>");
                    sreNews.Append("					</div>");
                    sreNews.Append("					<p class='comment-content'>");
                    sreNews.Append("						"+ row.ShortDescription + "");
                    sreNews.Append("					</p>");
                    sreNews.Append("					<div class='comment-reply'>");
                    sreNews.Append("						<p class='recommend-btn'>");
                    sreNews.Append("							<a href='" + strURL + "' class='like-btn'>");
                    sreNews.Append("								View More");
                    sreNews.Append("							</a>");
                    sreNews.Append("						</p>");
                    sreNews.Append("					</div>");
                    sreNews.Append("				</div>");
                    sreNews.Append("			</div>");
                    sreNews.Append("		</div>");
                    sreNews.Append("	</div>");
                    sreNews.Append("</li>");
                }
            }
            return sreNews.ToString();
        }


        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Notification").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Notification").FirstOrDefault();
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