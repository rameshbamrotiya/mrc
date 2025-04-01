using BAL;
using BL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public partial class Default : System.Web.UI.Page
    {
        public static string strAwardsAndAchie;
        public static string strBanner;
        public static string strMedical;
        public static string strSchema;
        public static string strBlog;
        public static string strPhoto;
        public static string strVideo;
        public static string strPopup;
        public static string strServices;
        public static string strTestimonial;
        public static string strFacility;
        public static string strEvent;

        public static long ChatBoxStageMessageCount
        {
            get
            {
                object value = HttpContext.Current.Session["ChatBoxStageMessageCount"];
                return value == null ? 0 : (long)value;
            }
            set
            {
                HttpContext.Current.Session["ChatBoxStageMessageCount"] = value;
            }
        }

        public static string strChatBox
        {
            get
            {
                object value = HttpContext.Current.Session["strChatBox"];
                return value == null ? "" : (string)value;
            }
            set
            {
                HttpContext.Current.Session["strChatBox"] = value;
            }
        }
        
        public static ChatBoxEnum ChatBoxStage
        {
            get
            {
                object value = HttpContext.Current.Session["ChatBoxEnum"];
                return value == null ? ChatBoxEnum.Welcome : (ChatBoxEnum)value;
            }
            set
            {
                HttpContext.Current.Session["ChatBoxEnum"] = value;
            }
        }

        public static ChatBoxModel ChatBoxValues
        {
            get
            {
                object value = HttpContext.Current.Session["ChatBoxValues"];
                return value == null ? new ChatBoxModel() : (ChatBoxModel)value;
            }
            set
            {
                HttpContext.Current.Session["ChatBoxValues"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChatBoxStage = ChatBoxEnum.Welcome;
                ChatBoxStageMessageCount = 0;
                strChatBox = "";
                StringBuilder strChatBuilder = new StringBuilder();
                strChatBuilder.Append("");
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Thanks for contacting us. Please tell something about yourself </div>");
                strChatBuilder.Append("</div>");
                ChatBoxStageMessageCount++;
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Your Name </div>");
                strChatBuilder.Append("</div>");
                strChatBox = strChatBuilder.ToString();
                ChatBoxStage = ChatBoxEnum.Name;

                strMedical = "";
                strSchema = "";
                strAwardsAndAchie = "";
                strBlog = "";
                strPhoto = "";
                strServices = "";
                strTestimonial = "";
                strServices = "";

                dtMiddleSection.DataSource = GetMiddleSection();
                dtMiddleSection.DataBind();
                strPopup = GetPoupModelString();
                slider.InnerHtml = GetSlidersBanner();
                strMedical = GetMedical();
                strSchema = GetSchema();
                strAwardsAndAchie = GetAwardsAndAchievements();
                strBlog = GetBlogs();
                strPhoto = GetPhotos();
                //strVideo = GetVideo();
                strTestimonial = GetTestimonial();
                strEvent = GetEvent();
                strServices = GetServices();
                FillCaptcha();
                FillHomePagePopup();
                //strFacility = GetFacility();
            }
            //ResolveUrl("~/Hospital/assets/img/slider/slider-2.JPG");
        }

        private void FillCaptcha()
        {
            //FillCapcthaEnquiry();
            //FillCapcthaComplaint();
        }

        //private void FillCapcthaEnquiry()
        //{
        //    try
        //    {
        //        Random random = new Random();
        //        string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //        StringBuilder captcha = new StringBuilder();
        //        for (int i = 0; i < 6; i++)
        //        {
        //            captcha.Append(combination[random.Next(combination.Length)]);
        //            string strCaptcha = captcha.ToString();
        //            Session["captchaEnquiry"] = strCaptcha;
        //            imgCaptcha.ImageUrl = ResolveUrl("~/GenerateCaptchaEnquiry.aspx?" + DateTime.Now.Ticks.ToString());
        //            //cvCaptcha.ValueToCompare= captcha.ToString();
        //        }

        //    }

        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void FillCapcthaComplaint()
        //{
        //    try
        //    {
        //        Random random1 = new Random();
        //        string combination1 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //        StringBuilder captcha1 = new StringBuilder();
        //        for (int i = 0; i < 6; i++)
        //        {
        //            captcha1.Append(combination1[random1.Next(combination1.Length)]);
        //            string strCaptcha = captcha1.ToString();
        //            Session["captchaComplaint"] = strCaptcha;
        //            imgCaptcha1.ImageUrl = ResolveUrl("~/GenerateCaptchaComplain.aspx?" + DateTime.Now.Ticks.ToString());
        //            //cvCaptcha.ValueToCompare= captcha.ToString();
        //        }
        //    }

        //    catch
        //    {
        //        throw;
        //    }
        //}

        private string GetServices()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strServices = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllSupportServiceByLangId(languageId).ToList();

                LableData:
                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {

                        string strURL = ResolveUrl(("~/SupportServicesDetails?" + Functions.Base64Encode(row.Id.ToString())));
                        //strServices.Append("<div class='single-facility wow fadeInUp animated' data-wow-delay='" + i + "00ms' data-wow-duration='1500ms'>");
                        //strServices.Append("	<div class='icon-part one'>");
                        //strServices.Append("    <img src='" + ResolveUrl(string.IsNullOrWhiteSpace(row.SSIcon)?"":row.SSIcon )+ "' alt='' />   ");
                        //strServices.Append("	</div>");
                        //strServices.Append("	<div class='text-part'>");
                        //strServices.Append("		<h4 class='title'>"+ row.SSName + "</h4>");
                        //strServices.Append("	</div>");
                        //strServices.Append("</div>");

                        strServices.Append("<div class='col-lg-6 col-md-6 mb-10'>");
                        strServices.Append("<a href='" + strURL + "'>");
                        strServices.Append("	<div class='single-facility'>");
                        strServices.Append("		<div class='icon-part one'>");
                        strServices.Append("			<img src='" + ResolveUrl(string.IsNullOrWhiteSpace(row.SSIcon) ? "" : row.SSIcon) + "'>");
                        strServices.Append("		</div>");
                        strServices.Append("		<div class='text-part'>");
                        strServices.Append("			<h4 class='title'>" + row.SSName + "</h4>");
                        strServices.Append("		</div>");
                        strServices.Append("	</div>");
                        strServices.Append("	</a>");
                        strServices.Append("</div>");
                        //  strServices.Append("<div class='single-facility wow fadeInUp animated' data-wow-delay='"+i+"00ms' data-wow-duration='1500ms'>");
                        //  strServices.Append("    <div class='icon-part one'>");
                        ////strServices.Append("        <i class='fas fa-stethoscope'></i>");
                        //  strServices.Append("    <img src='" + row.SSIcon + "' alt='' />   ");
                        //  strServices.Append("    </div>");
                        //  strServices.Append("    <div class='text-part'>");
                        //  strServices.Append("        <h4 class='title'>"+row.SSName+"</h4>");
                        //  strServices.Append("        <p class='desc'>");
                        //  strServices.Append("           "+HttpUtility.HtmlDecode(row.Description)+"");
                        //  strServices.Append("        </p>");
                        //  strServices.Append("    </div>");
                        //  strServices.Append("</div>");
                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllSupportServiceByLangId(languageId).ToList();
                        goto LableData;
                    }
                }

            }
            return strServices.ToString();
        }

        private string GetEvent()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllEventsAsPerLangId(languageId).ToList();

                LableData:
                if (dataList.Count() > 0)
                {
                    int i = 1;


                    strBlogs.Append("<!-- /Overview Content -->");
                    strBlogs.Append("<div role='tabpanel' id='doc_locations' class='tab-pane fade  active show'>");
                    strBlogs.Append("	<!-- Location List -->");
                    strBlogs.Append("	<div class='testimonials-slider owl-theme owl-carousel owl-loaded owl-nav-none'>");

                    var subList = dataList;
                    var dataModels = dataList.Where(x => x.EventEndDate.HasValue).OrderByDescending(x => x.EventStartDate).ToList();
                    foreach (var row in dataModels.Where(x => x.EventEndDate.Value.Date < DateTime.Now.Date).ToList())
                    {
                        if (i == 5)
                        {
                            break;
                        }
                        string strURL = ResolveUrl(("~/EventDetails?" + Functions.Base64Encode(row.EventId.ToString())));
                        string strBanner = ResolveUrl((row.MainImg));
                        strBlogs.Append("		<div class='thumbnail no-border no-padding'>");
                        strBlogs.Append("			<div class='row'>");
                        strBlogs.Append("				<div class='col-md-4'>");
                        strBlogs.Append("					<div class='media_img'>");
                        strBlogs.Append("						<img src='" + strBanner + "' alt='" + row.EventName + "'");
                        strBlogs.Append("							class='img-fluid'>");
                        strBlogs.Append("					</div>");
                        strBlogs.Append("				</div>");
                        strBlogs.Append("				<div class='col-md-8'>");
                        strBlogs.Append("					<div class='caption'>");
                        strBlogs.Append("						<h3 class='caption_title'><a href='" + strURL + "'>" + row.EventName + "</a></h3>");
                        strBlogs.Append("						<p class='caption-category'><label> Date:</label> " + (row.EventEndDate.HasValue ? row.EventEndDate.Value.ToString("MMMM dd, yyyy") : "") + "</p>");
                        strBlogs.Append("						<p class='caption-text'><label> Venue: </label> " + row.Venue + "</p>");
                        strBlogs.Append("					</div>");
                        strBlogs.Append("				</div>");
                        strBlogs.Append("			</div>");
                        strBlogs.Append("		</div>");
                        i++;
                    }
                    strBlogs.Append(" </div>");
                    //strBlogs.Append(" <div class='text-center mt-15'>");
                    //strBlogs.Append(" <a class='readon' href='Event'>View All</a>");
                    //strBlogs.Append(" </div>");
                    strBlogs.Append("</div>");


                    strBlogs.Append("<div role='tabpanel' id='doc_overview' class='tab-pane fade'>");
                    strBlogs.Append("	<div class='row justify-content-center'>");
                    strBlogs.Append("		<div class='col-lg-12 md-pr-15'>");
                    strBlogs.Append("		 <div class='upcomingevent-slider owl-theme owl-carousel owl-loaded owl-nav-none'>");
                    var dataModel = dataList.Where(x => x.EventEndDate.HasValue).OrderByDescending(x => x.EventStartDate).ToList();
                    dataModel = dataModel.Where(x => x.EventEndDate.Value.Date >= DateTime.Now.Date).OrderByDescending(x => x.EventStartDate).ToList();
                    var subList0 = dataModel;
                    if (subList0.Count() > 0)
                    {
                        i = 0;
                    }
                    else
                    {
                        strBlogs.Append("<h1 style='text-align: center;' class='title mb-0'>Coming soon..</h1>");
                    }
                    foreach (var row in subList0)
                    {
                        if (i == 5)
                        {
                            break;
                        }
                        string strURL = ResolveUrl(("~/EventDetails?" + Functions.Base64Encode(row.EventId.ToString())));
                        string strBanner = ResolveUrl((row.MainImg));
                        strBlogs.Append("			<div class='row white-bg blog-item mb-25'>");
                        strBlogs.Append("				<div class='col-md-11 col-sm-10 isotope-item  festival'>");
                        strBlogs.Append("					<div class='thumbnail no-border no-padding'>");
                        strBlogs.Append("						<div class='row'>");
                        strBlogs.Append("							<div class='col-md-3'>");
                        strBlogs.Append("								<div class='media_img'>");
                        strBlogs.Append("									<img src='" + strBanner + "'");
                        strBlogs.Append("										alt='2016 Calgary Comic &amp; Entertainment Expo' class='img-fluid'>");
                        strBlogs.Append("								</div>");
                        strBlogs.Append("							</div>");
                        strBlogs.Append("				            <div class='col-md-8'>");
                        strBlogs.Append("				            	<div class='caption'>");
                        strBlogs.Append("				            		<h3 class='caption_title'><a href='" + strURL + "'>" + row.EventName + "</a></h3>");
                        strBlogs.Append("				            		<p class='caption-category'><label> Date:</label> " + row.EventStartDate.Value.ToString("MMMM dd, yyyy") + "</p>");
                        strBlogs.Append("				            		<p class='caption-category'><label> Time: </label> " + row.StartTimeHH + ":" + row.StartTimeMM + " " + row.StartTimeTT + " Onwards</p>");
                        strBlogs.Append("				            		<p class='caption-text'><label> Venue: </label> " + row.Venue + "</p>");
                        strBlogs.Append("				            	</div>");
                        strBlogs.Append("				            </div>");
                        strBlogs.Append("						</div>");
                        strBlogs.Append("					</div>");
                        strBlogs.Append("				</div>");
                        strBlogs.Append("				<div class='col-md-1'>");
                        strBlogs.Append("					<div class='text_explore'>");
                        strBlogs.Append("						<h4 class='vertical-text'><a href='" + strURL + "'> Explore More</a></h4>");
                        strBlogs.Append("					</div>");
                        strBlogs.Append("				</div>");
                        strBlogs.Append("			</div>");
                        i++;
                    }
                    strBlogs.Append("		</div>");
                    strBlogs.Append("     </div>");
                    strBlogs.Append("	</div>");
                    strBlogs.Append("</div>");
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllEventsAsPerLangId(languageId).ToList();
                        goto LableData;
                    }
                }

            }
            return strBlogs.ToString();
        }

        private string GetFacility()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IFacilitesMasterRepository objPatientTestimonialRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objPatientTestimonialRepository.GetAllFacilitesMaster(languageId).Where(x => x.IsVisible == true).OrderBy(x => x.Id).ToList();
                if (dataList.Count > 0)
                {
                    foreach (var row in dataList)
                    {
                        string desc = objPatientTestimonialRepository.GetAllFacilitesImageMaster((long)row.Id, languageId).OrderBy(x => x.SequanceNo).Select(x => x.FacilitesFileInfo).FirstOrDefault();

                        strBlogs.Append("<div class='single-facility wow fadeInUp animated' data-wow-delay='100ms' data-wow-duration='1500ms'>");
                        strBlogs.Append("    <div class='icon-part one'>                                                                      ");
                        strBlogs.Append("        <i class='fas fa-stethoscope'></i>                                                           ");
                        strBlogs.Append("    </div>                                                                                           ");
                        strBlogs.Append("    <div class='text-part'>                                                                          ");
                        strBlogs.Append("        <h4 class='title'>" + row.FacilitesName + "</h4>                                                    ");
                        strBlogs.Append("        <p class='desc'>                                                                             ");
                        strBlogs.Append("           " + desc + "                                                                        ");
                        strBlogs.Append("        </p>                                                                                         ");
                        strBlogs.Append("    </div>                                                                                           ");
                        strBlogs.Append("</div>");
                    }
                }
            }
            return strBlogs.ToString();
        }

        private string GetTestimonial()

        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IPatientTestimonialRepository objPatientTestimonialRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
            {
                var dataList = objPatientTestimonialRepository.GetAllPatientTestimonial().Where(x => x.IsActive == true && (!string.IsNullOrWhiteSpace(x.Description))).OrderBy(x => x.SequanceNo).ToList();



                if (dataList.Count > 0)
                {
                    int i = 0;
                    foreach (var row in dataList)
                    {
                        string filePath = ResolveUrl(string.IsNullOrWhiteSpace(row.FileFullPath) ? "" : row.FileFullPath);

                        strBlogs.Append("");

                        string strPatientSpeack = ResolveUrl("~/PatientTestimonialDetails");

                        strBlogs.Append("<div class='owl-item '>");
                        strBlogs.Append("	<div class='content-box'>");
                        strBlogs.Append("		<div class='text'>");
                        strBlogs.Append("			<p> " + row.Description + "</p>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("		<div class='author-info'>");
                        strBlogs.Append("			<a href='" + strPatientSpeack + "'>");
                        strBlogs.Append("				<div class='ltn__commenter-img'>");
                        strBlogs.Append("					"+ (string.IsNullOrWhiteSpace(filePath)?"": "<figure class='author-image'><img src='" + filePath + "' alt='Image'></figure>") +"");
                        strBlogs.Append("				</div>");
                        strBlogs.Append("				<h4>" + row.PatientName + "</h4>");
                        strBlogs.Append("				<span class='designation'>" + row.CityName + "</span>");
                        strBlogs.Append("			</a>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("	</div>");
                        strBlogs.Append("</div>");


                    }
                }
            }


            return strBlogs.ToString();
        }

        private string GetPoupModelString()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            strBlogs.Append("");

            PopUpMasterBO objBo = new PopUpMasterBO();
            objBo.page_name = "HOME_POPUP";
            DataSet ds = new PopUpMasterBAL().PopUpMaster_SelectBypagenameID(objBo);
            if (!ds.Tables.Count.Equals(0) && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                if (dr["col_page_desc"] != DBNull.Value)
                {

                    string strDesc = HttpUtility.HtmlDecode(dr["col_page_desc"].ToString());

                    if ((Convert.ToBoolean(dr["enabled"]) ? "1" : "0") == "1")
                    {

                        strBlogs.Append("<div class='modal fade' id='exampleModal' tabindex='-1' role='dialog' aria-hidden='true'>");
                        strBlogs.Append("    <div class='modal-dialog' role='document'>");
                        strBlogs.Append("        <div class='modal-content'>");
                        strBlogs.Append("            <div class='modal-header'>");
                        strBlogs.Append("                <button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                        strBlogs.Append("                    title=''>");
                        strBlogs.Append("                    <span aria-hidden='true'>×</span></button>");
                        strBlogs.Append("            </div>");
                        strBlogs.Append("            <div class='modal-body' style='height: 80vh;overflow: auto; '");
                        strBlogs.Append("            " + strDesc + "     ");
                        strBlogs.Append("            </div>");
                        strBlogs.Append("        </div>");
                        strBlogs.Append("    </div>");
                        strBlogs.Append("</div>");
                    }
                }
            };



            return strBlogs.ToString();
        }

        //private string GetVideo()
        //{
        //    int languageId = Functions.LanguageId;
        //    StringBuilder strBlogs = new StringBuilder();
        //    strBlogs.Append("");
        //    using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
        //    {
        //        var dataList = objBlogCategoryMasterRepository.GetAllVideoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();

        //        LableData: if (dataList.Count() > 0)
        //        {
        //            int i = 1;
        //            foreach (var row in dataList)
        //            {
        //                if (i > 4)
        //                {
        //                    break;
        //                }
        //                if ((bool)row.Link_Video_Upload)
        //                {
        //                    strBlogs.Append("<div class='ltn__gallery-item filter_category_2 '>");
        //                    strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
        //                    strBlogs.Append("		<div class='ltn__gallery-item-img'>");
        //                    strBlogs.Append("			<a href='" + ResolveUrl(row.VideoPath )+ "' data-fancybox='gallery' class='lightbox-image'>");
        //                    strBlogs.Append("				<img src='" + ResolveUrl("~/hospital/assets/img/hospital/02_Home-Page.jpg") + "' alt='Image' class='img-fluid'>");
        //                    strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
        //                    strBlogs.Append("					<i class='fas fa-video'></i>");
        //                    strBlogs.Append("				</span>");
        //                    strBlogs.Append("			</a>");
        //                    strBlogs.Append("		</div>");
        //                    strBlogs.Append("		<div class='ltn__gallery-item-info'>");
        //                    strBlogs.Append("			<h4><a href='" + ResolveUrl(row.VideoPath) + "'>" + row.VideoName + "</a></h4>");
        //                    strBlogs.Append("			<p>" + row.VideoDesc + "</p>");
        //                    strBlogs.Append("		</div>");
        //                    strBlogs.Append("	</div>");
        //                    strBlogs.Append("</div>");
        //                }
        //                else
        //                {
        //                    if (row.VideoPath.Contains("youtube"))
        //                    {
        //                        strBlogs.Append("<div class='ltn__gallery-item filter_category_1 '>");
        //                        strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
        //                        strBlogs.Append("		<div class='ltn__gallery-item-img'>");
        //                        strBlogs.Append("			<a href='" + row.VideoPath + "' data-fancybox='gallery'");
        //                        strBlogs.Append("				class='lightbox-image'>");
        //                        strBlogs.Append("				<img src='" + ResolveUrl("~/hospital/assets/img/hospital/1339.jpg") + "' alt='Image' class='img-fluid'>");
        //                        strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
        //                        strBlogs.Append("					<i class='fab fa-youtube'></i>");
        //                        strBlogs.Append("				</span>");
        //                        strBlogs.Append("			</a>");
        //                        strBlogs.Append("		</div>");
        //                        strBlogs.Append("		<div class='ltn__gallery-item-info'>");
        //                        strBlogs.Append("			<h4><a href='" + row.VideoPath + "'>" + row.VideoName + "</a></h4>");
        //                        strBlogs.Append("			<p>" + row.VideoDesc + "</p>");
        //                        strBlogs.Append("		</div>");
        //                        strBlogs.Append("	</div>");
        //                        strBlogs.Append("</div>");
        //                    }
        //                    else if (row.VideoPath.Contains("vimeo"))
        //                    {
        //strBlogs.Append("<div class='ltn__gallery-item filter_category_3 '>");
        //strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
        //strBlogs.Append("		<div class='ltn__gallery-item-img'>");
        //strBlogs.Append("			<a href='" + row.VideoPath + "' data-fancybox='gallery' class='lightbox-image'>");
        //strBlogs.Append("				<img src='" + ResolveUrl("~/hospital/assets/img/hospital/02_Home-Page.jpg") + "' alt='Image' class='img-fluid'> ");
        //strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
        //strBlogs.Append("					<i class='fab fa-vimeo-v'></i>");
        //strBlogs.Append("				</span>");
        //strBlogs.Append("			</a>");
        //strBlogs.Append("		</div>");
        //strBlogs.Append("		<div class='ltn__gallery-item-info'>");
        //strBlogs.Append("			<h4><a href='" + row.VideoPath + "'>" + row.VideoName + " </a></h4>");
        //strBlogs.Append("			<p>" + row.VideoDesc + "</p> ");
        //strBlogs.Append("		</div>");
        //strBlogs.Append("	</div>");
        //strBlogs.Append("</div>");
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            languageId = 1;
        //            dataList = objBlogCategoryMasterRepository.GetAllVideoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();
        //            if (Functions.LanguageId != 1)
        //            {
        //                goto LableData;
        //            }
        //        }
        //    }
        //    return strBlogs.ToString();
        //}

        private string GetPhotos()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllPhotoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();

                LableData:
                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {
                        if (i > 4)
                        {
                            break;
                        }
                        string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                        string strURL = ResolveUrl(("~/PhotoAlbum?" + Functions.Base64Encode(row.Id.ToString())));
                        strBlogs.Append("");


                        strBlogs.Append("<div class='ltn__gallery-item filter_category_3 '>");
                        strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
                        strBlogs.Append("		<div class='ltn__gallery-item-img'>");
                        strBlogs.Append("			<a href='" + strimagePath + "' data-fancybox='gallery' class='lightbox-image'");
                        strBlogs.Append("				data-bs-lc-caption='Your caption Here'>");
                        strBlogs.Append("				<img src='" + strimagePath + "' alt='Image' class='img-fluid'>");
                        strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
                        strBlogs.Append("					<i class='fab fa-acquisitions-incorporated'></i>");
                        strBlogs.Append("				</span>");
                        strBlogs.Append("			</a>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("		<div class='ltn__gallery-item-info'>");
                        strBlogs.Append("			<h4><a href='" + strURL + "'>" + row.Name + "</a></h4>");
                        strBlogs.Append("			<p>" + row.Descriptions + "</p>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("	</div>");
                        strBlogs.Append("</div>");


                        //strBlogs.Append("<div class='gallery-box-layout1'>                                                                                              ");
                        //strBlogs.Append("    <img src='" + strimagePath + "' alt='Feature' class='img-fluid'>                     ");
                        //strBlogs.Append("    <div class='item-icon'>                                                                                                    ");
                        //strBlogs.Append("        <a href='" + strimagePath + "' data-fancybox='gallery' class='lightbox-image'>   ");
                        //strBlogs.Append("            <i class='fab fa-acquisitions-incorporated'></i>                                                                                 ");
                        //strBlogs.Append("        </a>                                                                                                                   ");
                        //strBlogs.Append("    </div>                                                                                                                     ");
                        //strBlogs.Append("    <div class='item-content'>                                                                                                 ");
                        //strBlogs.Append("        <h3 class='item-title'>" + row.Name + "</h3>                                                             ");
                        //strBlogs.Append("        <span class='title-ctg'>Cardiology</span>                                                                              ");
                        //strBlogs.Append("    </div>                                                                                                                     ");
                        //strBlogs.Append("</div>                                                                                                                         ");

                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllPhotoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();
                        goto LableData;
                    }
                }

            }

            languageId = Functions.LanguageId;
            strBlogs.Append("");
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllVideoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();

                LableData:
                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {

                        string strURLs = "";
                        if (!string.IsNullOrWhiteSpace(row.ThumbImg_path))
                        {
                            strURLs = (row.ThumbImg_path.ToString());
                            if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURLs = strURLs.Replace("~/", "");
                                strURLs = ResolveUrl("~/" + strURLs);
                            }

                        }
                        else
                        {
                            strURLs = ResolveUrl("~/hospital/assets/img/hospital/1339.jpg");
                        }

                        if (i > 4)
                        {
                            break;
                        }
                        if ((bool)row.Link_Video_Upload)
                        {
                            strBlogs.Append("<div class='ltn__gallery-item filter_category_2 '>");
                            strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
                            strBlogs.Append("		<div class='ltn__gallery-item-img'>");
                            strBlogs.Append("			<a href='" + ResolveUrl(row.VideoPath) + "' data-fancybox='gallery' class='lightbox-image'>");
                            strBlogs.Append("				<img src='" + strURLs + "' alt='Image' class='img-fluid'>");
                            strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
                            strBlogs.Append("					<i class='fas fa-video'></i>");
                            strBlogs.Append("				</span>");
                            strBlogs.Append("			</a>");
                            strBlogs.Append("		</div>");
                            strBlogs.Append("		<div class='ltn__gallery-item-info'>");
                            strBlogs.Append("			<h4><a href='" + ResolveUrl(row.VideoPath) + "'>" + row.VideoName + "</a></h4>");
                            strBlogs.Append("			<p>" + row.VideoDesc + "</p>");
                            strBlogs.Append("		</div>");
                            strBlogs.Append("	</div>");
                            strBlogs.Append("</div>");
                        }
                        else
                        {
                            if (row.VideoPath.Contains("youtube"))
                            {
                                strBlogs.Append("<div class='ltn__gallery-item filter_category_1 '>");
                                strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
                                strBlogs.Append("		<div class='ltn__gallery-item-img'>");
                                strBlogs.Append("			<a href='" + row.VideoPath + "' data-fancybox='gallery'");
                                strBlogs.Append("				class='lightbox-image'>");
                                strBlogs.Append("				<img src='" + strURLs + "' alt='Image' class='img-fluid'>");
                                strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
                                strBlogs.Append("					<i class='fab fa-youtube'></i>");
                                strBlogs.Append("				</span>");
                                strBlogs.Append("			</a>");
                                strBlogs.Append("		</div>");
                                strBlogs.Append("		<div class='ltn__gallery-item-info'>");
                                strBlogs.Append("			<h4><a href='" + row.VideoPath + "'>" + row.VideoName + "</a></h4>");
                                strBlogs.Append("			<p>" + row.VideoDesc + "</p>");
                                strBlogs.Append("		</div>");
                                strBlogs.Append("	</div>");
                                strBlogs.Append("</div>");
                            }
                            else if (row.VideoPath.Contains("vimeo"))
                            {
                                strBlogs.Append("<div class='ltn__gallery-item filter_category_3 '>");
                                strBlogs.Append("	<div class='ltn__gallery-item-inner'>");
                                strBlogs.Append("		<div class='ltn__gallery-item-img'>");
                                strBlogs.Append("			<a href='" + row.VideoPath + "' data-fancybox='gallery' class='lightbox-image'>");
                                strBlogs.Append("				<img src='" + strURLs + "' alt='Image' class='img-fluid'> ");
                                strBlogs.Append("				<span class='ltn__gallery-action-icon'>");
                                strBlogs.Append("					<i class='fab fa-vimeo-v'></i>");
                                strBlogs.Append("				</span>");
                                strBlogs.Append("			</a>");
                                strBlogs.Append("		</div>");
                                strBlogs.Append("		<div class='ltn__gallery-item-info'>");
                                strBlogs.Append("			<h4><a href='" + row.VideoPath + "'>" + row.VideoName + " </a></h4>");
                                strBlogs.Append("			<p>" + row.VideoDesc + "</p> ");
                                strBlogs.Append("		</div>");
                                strBlogs.Append("	</div>");
                                strBlogs.Append("</div>");
                            }
                        }
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllVideoAlbumListByLangId(languageId).OrderByDescending(x => x.Id).ToList();
                        goto LableData;
                    }
                }
            }
            return strBlogs.ToString();
        }

        private string GetBlogs()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBlogs = new StringBuilder();
            using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).Where(x => x.IsVisible == true).OrderByDescending(x => x.BlogDate).Take(3).ToList();

                LableData:
                if (dataList.Count() > 0)
                {
                    int i = 1;
                    foreach (var row in dataList)
                    {
                        if (i > 4)
                        {
                            break;
                        }
                        string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                        string strURL = ResolveUrl(("~/BlogDetails?" + Functions.Base64Encode(row.Id.ToString() + "|" + row.TypeDetail)));
                        string strBlogName = "";
                        if (row.BlogName.Length > 83)
                        {
                            strBlogName = row.BlogName.Remove(83) + "..";
                        }
                        else
                        {
                            strBlogName = row.BlogName;
                        }
                        strBlogs.Append("");

                        strBlogs.Append("<div class='col-lg-4 col-md-6 col-sm-12 news-block'>");
                        strBlogs.Append("    <div class='news-block-one'>");
                        strBlogs.Append("		<div class='inner-box'>");
                        strBlogs.Append("			<figure class='image-box'>");
                        strBlogs.Append("				<img src='" + strimagePath + "' alt=''>");
                        strBlogs.Append("				<a href='" + strURL + "' class='link'><i class='fas fa-link'></i></a>");
                        strBlogs.Append("				<span class='category'>" + row.TypeDetail + "</span>");
                        strBlogs.Append("			</figure>");
                        strBlogs.Append("			<div class='lower-content'>");
                        strBlogs.Append("				<ul class='post-info'>");
                        strBlogs.Append("					<li><i class='fas fa-user'></i> <a href='" + strURL + "'>" + row.Blogger + "</a></li>");
                        strBlogs.Append("					<li><i class='fa fa-calendar'></i>" + row.BlogDate.ToString("MMMM dd, yyyy") + "</li>");
                        strBlogs.Append("				</ul>");
                        strBlogs.Append("				<h4><a href='" + strURL + "'> " + strBlogName);
                        strBlogs.Append("					</a></h4>");
                        strBlogs.Append("				<div class='btn-box'><a href='" + strURL + "' class='theme-btn-one'>Read more<i class='fas fa-angle-right'></i></a></div>");
                        strBlogs.Append("			</div>");
                        strBlogs.Append("		</div>");
                        strBlogs.Append("	</div>");
                        strBlogs.Append("</div>");

                        i++;
                    }
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(languageId).OrderByDescending(x => x.Id).ToList();
                        goto LableData;
                    }
                }

            }
            return strBlogs.ToString();
        }

        private string GetSchema()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId).OrderBy(x => x.Id).Take(4).ToList();

                if (dataListAward != null)
                {
                    LableData:
                    if (dataListAward.Count > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {
                            index++;
                            if (index <= 6)
                            {
                                string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.Schemebanner) ? "" : row.Schemebanner);
                                string strURL = ResolveUrl(("~/Schema?" + Functions.Base64Encode(row.Id.ToString())));
                                string strDesc = HttpUtility.HtmlDecode(row.Description);
                                strAwardsAndAchievements.Append("");
                                //strAwardsAndAchievements.Append("<div class='col-lg-4 col-sm-6'>                                                                                        ");
                                //strAwardsAndAchievements.Append("   <div class='lgx-single-service wow  flipInY animated' data-wow-delay='600ms'                                        ");
                                //strAwardsAndAchievements.Append("       data-wow-duration='1500ms'>                                                                                     ");
                                //strAwardsAndAchievements.Append("       <figure>                                                                                                        ");
                                //strAwardsAndAchievements.Append("           <a class='service-img' href='#'>                                                                            ");
                                //strAwardsAndAchievements.Append("               <img src='" + strimagePath + "' alt='Service' /></a>                                                     ");
                                //strAwardsAndAchievements.Append("           <figcaption>                                                                                                ");
                                //strAwardsAndAchievements.Append("               <div class='link-area'>                                                                                 ");
                                //strAwardsAndAchievements.Append("                   <a href='#'>                                                                                        ");
                                //strAwardsAndAchievements.Append("                       <img src='" + ResolveUrl("~/Hospital/assets/img/scheme/icon-link.png") + "' alt='link'></a>        ");
                                //strAwardsAndAchievements.Append("               </div>                                                                                                  ");
                                //strAwardsAndAchievements.Append("               <div class='service-info'>                                                                              ");
                                //strAwardsAndAchievements.Append("                   <h3 class='title'><a href='" + strURL + "'>" + row.SchemeName + "</a></h3>                           ");
                                //strAwardsAndAchievements.Append("                   <p>" + strDesc + "  </p>                                                                             ");
                                //strAwardsAndAchievements.Append("                   <a class='lgx-btn lgx-btn-white lgx-btn-sm' href='" + strURL + "'><span>Read More</span></a>         ");
                                //strAwardsAndAchievements.Append("                   <img src='" + ResolveUrl("~/Hospital/assets/img/scheme/service-icon1.png") + "' alt='service icon'>     ");
                                //strAwardsAndAchievements.Append("               </div>                                                                                                  ");
                                //strAwardsAndAchievements.Append("           </figcaption>                                                                                               ");
                                //strAwardsAndAchievements.Append("       </figure>                                                                                                       ");
                                //strAwardsAndAchievements.Append("   </div>                                                                                                              ");
                                //strAwardsAndAchievements.Append("</div>                                                                                                                 ");
                                strAwardsAndAchievements.Append("<div class='col-lg-3 col-md-6 mb-30'>                                  ");
                                strAwardsAndAchievements.Append("	<div class='degree-wrap'>                                           ");
                                strAwardsAndAchievements.Append("		<img src='" + strimagePath + "' alt=''>               ");
                                strAwardsAndAchievements.Append("		<div class='title-part'>                                        ");
                                strAwardsAndAchievements.Append("			<h4 class='title'>" + row.SchemeName + "</h4>                                     ");
                                strAwardsAndAchievements.Append("		</div>                                                          ");
                                strAwardsAndAchievements.Append("		<div class='content-part'>                                      ");
                                strAwardsAndAchievements.Append("			<h4 class='title'><a href='#'>" + row.SchemeName + "</a></h4>                             ");
                                strAwardsAndAchievements.Append("			<div class='btn-part'>                                      ");
                                strAwardsAndAchievements.Append("				<a href='" + strURL + "'>Read More</a>                               ");
                                strAwardsAndAchievements.Append("			</div>                                                      ");
                                strAwardsAndAchievements.Append("		</div>                                                          ");
                                strAwardsAndAchievements.Append("	</div>                                                              ");
                                strAwardsAndAchievements.Append("</div>                                                                 ");
                            }
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objHomePageRepository.GetAllSchemeMasterHomeByLangId(languageId);
                            goto LableData;
                        }
                    }
                }

                return strAwardsAndAchievements.ToString();
            }
        }

        private string GetMedical()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IOurExcellenceMasterRepository objHomePageRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllTblOurExcellenceMaster(Functions.LanguageId).Where(x => x.IsVisible == true).ToList();

                if (dataListAward != null)
                {
                    LableData:
                    if (dataListAward.Count > 0)
                    {
                        //strAwardsAndAchievements.Append("<div class='medical-departments-carousel owl-theme owl-carousel owl-nav'>");
                        foreach (var row in dataListAward)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.FileFullPath) ? "" : row.FileFullPath);
                            string strURL = ResolveUrl(("~/DepartmentsDetails?" + Functions.Base64Encode(row.Id.ToString())));
                            //strAwardsAndAchievements.Append("<!--Start single item-->                                                                  ");
                            strAwardsAndAchievements.Append("<div class='single-item text-center'>                                                      ");
                            strAwardsAndAchievements.Append("	<div class='iocn-holder'>                                                               ");
                            strAwardsAndAchievements.Append("		<img src='" + strimagePath + "' alt='blog-1'> ");
                            strAwardsAndAchievements.Append("	</div>                                                                                  ");
                            strAwardsAndAchievements.Append("	<div class='text-holder'>                                                               ");
                            strAwardsAndAchievements.Append("		<h4> " + row.DepartmentName + "<br>&nbsp;</h4>                                      ");
                            strAwardsAndAchievements.Append("	</div>                                                                                  ");
                            strAwardsAndAchievements.Append("	<a class='readmore' href='" + strURL + "'>Read More</a>                                              ");
                            strAwardsAndAchievements.Append("</div>                                                                                     ");
                            //strAwardsAndAchievements.Append("<!--End single item-->                                                                     ");
                        }

                        //strAwardsAndAchievements.Append("</div> ");
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objHomePageRepository.GetAllTblOurExcellenceMaster(languageId);
                            goto LableData;
                        }
                    }

                }
            }
            return strAwardsAndAchievements.ToString();
        }

        private string GetSlidersBanner()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllBannerMasterHome();

                if (dataListAward != null)
                {
                    if (dataListAward.Count > 0)
                    {
                        int index = 0;
                        foreach (var row in dataListAward)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.URL) ? "" : row.URL);
                            index++;
                            strAwardsAndAchievements.Append("");
                            strAwardsAndAchievements.Append("<li data-transition='" + (index == 1 ? "rs-20" : "fade") + "'>");
                            strAwardsAndAchievements.Append("	<img src='" + strimagePath + "' alt='' data-bgposition='top center' data-bgfit='100% 100%'");
                            strAwardsAndAchievements.Append("		data-bgrepeat='no-repeat' data-bgparallax='1'>");
                            using (IBannerSubDetailsRepository objAcc = new BannerSubDetailsRepository(Functions.strSqlConnectionString))
                            {
                                var lstCAption = objAcc.GetAllBannerSubDetails((long)row.Id).ToList();
                                foreach (var subRow in lstCAption)
                                {
                                    string strXPosition = subRow.TextXPosition;
                                    string strYPosition = subRow.TextYPosition;
                                    strAwardsAndAchievements.Append("	<div class='tp-caption  tp-resizeme' data-x='" + strXPosition + "' data-hoffset='0' data-y='top' data-voffset='0'");
                                    strAwardsAndAchievements.Append("		data-transform_idle='o:1;'");
                                    strAwardsAndAchievements.Append("		data-transform_in='x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;'");
                                    strAwardsAndAchievements.Append("		data-transform_out='s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;'");
                                    strAwardsAndAchievements.Append("		data-mask_in='x:[100%];y:0;s:inherit;e:inherit;' data-splitin='none' data-splitout='none'");
                                    strAwardsAndAchievements.Append("		data-responsive_offset='on' data-start='1500'>");
                                    strAwardsAndAchievements.Append("		<div class='slide-content-box mar-lft'>");
                                    strAwardsAndAchievements.Append("			" + HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(subRow.BannerDescription)) + "");
                                    strAwardsAndAchievements.Append("		</div>");
                                    strAwardsAndAchievements.Append("	</div>");
                                }
                            }
                            strAwardsAndAchievements.Append("</li>");
                        }
                    }
                }
            }

            return strAwardsAndAchievements.ToString();
        }

        private DataTable GetMiddleSection()
        {
            int languageId = Functions.LanguageId;
            DataTable dt = new HomePageContentBAL().SelectAllRecord(languageId);

            return dt;
        }

        private string GetAwardsAndAchievements()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strAwardsAndAchievements = new StringBuilder();
            using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataListAward = objHomePageRepository.GetAllAwardMasterHomeByLangId(Functions.LanguageId);

                if (dataListAward != null)
                {
                    LableData:
                    if (dataListAward.Count > 0)
                    {
                        foreach (var row in dataListAward)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImageDesc) ? "" : row.ImageDesc);
                            string strURL = ResolveUrl(("~/AwardAndAchievements?" + Functions.Base64Encode(row.AwardId.ToString())));
                            strAwardsAndAchievements.Append("<div class='owl-item '>                                                                                                                ");
                            strAwardsAndAchievements.Append("	<div class='tm-sc-blog tm-mediku tm-sc-blog-masonry blog-style1-current-theme mb-lg-30'>                                            ");
                            strAwardsAndAchievements.Append("		<article class='post type-post status-publish format-standard has-post-thumbnail'>                                              ");
                            strAwardsAndAchievements.Append("			<div class='entry-header'>                                                                                                  ");
                            strAwardsAndAchievements.Append("				<div class='post-thumb_award lightgallery-lightbox'>                                                                    ");
                            strAwardsAndAchievements.Append("					<div class='post-thumb-inner'>                                                                                      ");
                            strAwardsAndAchievements.Append("						<div class='thumb border-radius-0'>                                                                             ");
                            strAwardsAndAchievements.Append("							<img class='img-fullwidth' src='" + strimagePath + "' alt='Image' />         ");
                            strAwardsAndAchievements.Append("						</div>                                                                                                          ");
                            strAwardsAndAchievements.Append("					</div>                                                                                                              ");
                            strAwardsAndAchievements.Append("				</div>                                                                                                                  ");
                            strAwardsAndAchievements.Append("			</div>                                                                                                                      ");
                            //strAwardsAndAchievements.Append("			<div class='entry-content'>                                                                                                 ");
                            //strAwardsAndAchievements.Append("				<h4 class='entry-title'><a href='" + strURL + "' rel='bookmark'>" + row.AlbumName + "</a>                                                                                                ");
                            //strAwardsAndAchievements.Append("				</h4>                                                                                                                   ");
                            //strAwardsAndAchievements.Append("				<p class='mb-0'>" + row.AwardShortDesc + "</p>                                                                                                      ");
                            //strAwardsAndAchievements.Append("				<div class='clearfix'></div>                                                                                            ");
                            //strAwardsAndAchievements.Append("			</div>                                                                                                                      ");
                            strAwardsAndAchievements.Append("		</article>                                                                                                                      ");
                            strAwardsAndAchievements.Append("	</div>                                                                                                                              ");
                            strAwardsAndAchievements.Append("</div>                                                                                                                                 ");
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataListAward = objHomePageRepository.GetAllAwardMasterHomeByLangId(languageId);
                            goto LableData;
                        }
                    }
                }

                return strAwardsAndAchievements.ToString();
            }
        }

        //protected void btnNewsLetter_Click(object sender, EventArgs e)
        //{
        //    using (INewsLetterMasterRepository objNewsLetterMasterRepository = new NewsLetterMasterRepository(Functions.strSqlConnectionString))
        //    {
        //        string strMessage = "";
        //        if (objNewsLetterMasterRepository.GetAllNewsLetterSubScription().Where(x => x.NewsLetterEmail == txtNewsletterEmail.Value.Trim()).Count() <= 0)
        //        {
        //            if (!objNewsLetterMasterRepository.InsertOrUpdateNewsLetterMaster(new Data.Common.GetAllNewsLetterSubScriptionResult { Id = 0, NewsLetterEmail = txtNewsletterEmail.Value.Trim(), NewsLetterSubscription = true }, out strMessage))
        //            {
        //                strMessage = txtNewsletterEmail.Value.Trim() + " News Subscriber Added";
        //                lblError.ForeColor = System.Drawing.Color.Green;
        //            }
        //            else
        //            {
        //                lblError.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //        else
        //        {
        //            var objData = objNewsLetterMasterRepository.GetAllNewsLetterSubScription().Where(x => x.NewsLetterEmail == txtNewsletterEmail.Value.Trim()).FirstOrDefault();
        //            if (objData.NewsLetterSubscription == false)
        //            {
        //                if (!objNewsLetterMasterRepository.UpdateNewsLetterMasterSubscription(objData.Id, true, out strMessage))
        //                {
        //                    strMessage = txtNewsletterEmail.Value.Trim() + " News Subscriber Updated";
        //                    lblError.ForeColor = System.Drawing.Color.Green;
        //                }
        //            }
        //            else
        //            {
        //                lblError.ForeColor = System.Drawing.Color.Red;
        //                strMessage = txtNewsletterEmail.Value.Trim() + " Subscriber Already Existed";
        //            }
        //        }

        //    }
        //}

        //protected void btnEnquirey_Click(object sender, EventArgs e)
        //{
        //    string strError = "";
        //    try
        //    {
        //        if (Session["captchaEnquiry"].ToString() != txtCaptcha.Text)
        //        {
        //            strError = "Captcha does not match";
        //                FillCaptcha();
        //            txtCaptcha.Focus();
        //            txtCaptcha.Text = string.Empty;
        //            Functions.MessagePopup(this, strError, PopupMessageType.error);
        //            tabLink1.Attributes.Add("class", "nav-link active");
        //            tab1.Attributes.Add("class", "tab-pane active");
        //            tab2.Attributes.Add("class", "tab-pane ");
        //            tabLink2.Attributes.Add("class", "nav-link");
        //            return;
        //        }
        //        using (IComplaintEnquiryRepository objData = new ComplaintEnquiryRepository(Functions.strSqlConnectionString))
        //        {
        //            GetAllComplaintEnquiryMasterResult objModel = new GetAllComplaintEnquiryMasterResult();
        //            if (!objData.InsertOrUpdateComplaintEnquiry(objModel, true, out strError))
        //            {
        //                FillCaptcha();
        //                lblErrorEnquiry.ForeColor = System.Drawing.Color.Green;
        //            }
        //            else
        //            {
        //                FillCaptcha();
        //                lblErrorEnquiry.ForeColor = System.Drawing.Color.Red;
        //            }
        //            lblErrorEnquiry.Text = strError;

        //        }

        //        tabLink1.Attributes.Add("class", "nav-link active");
        //        tab1.Attributes.Add("class", "tab-pane active");
        //        tab2.Attributes.Add("class", "tab-pane ");
        //        tabLink2.Attributes.Add("class", "nav-link");
        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}

        //protected void btnRun_ServerClick(object sender, EventArgs e)
        //{
        //    FillCapcthaEnquiry();

        //    tabLink1.Attributes.Add("class", "nav-link active");
        //    tab1.Attributes.Add("class", "tab-pane active");
        //    tab2.Attributes.Add("class", "tab-pane ");
        //    tabLink2.Attributes.Add("class", "nav-link");
        //}

        //protected void btnRun1_ServerClick(object sender, EventArgs e)
        //{
        //    FillCapcthaComplaint();


        //    tabLink2.Attributes.Add("class", "nav-link active");
        //    tabLink1.Attributes.Add("class", "nav-link");
        //    tab1.Attributes.Add("class", "tab-pane ");
        //    tab2.Attributes.Add("class", "tab-pane active");
        //}

        //protected void btnComplaint_Click(object sender, EventArgs e)
        //{
        //    string strError = "";
        //    try
        //    {
        //        if (Session["captchaComplaint"].ToString() != txtCaptcha1.Text)
        //        {
        //            strError = "Captcha does not match";
        //            FillCaptcha();
        //            txtCaptcha1.Focus();
        //            txtCaptcha1.Text = string.Empty;
        //            Functions.MessagePopup(this, strError, PopupMessageType.error);
        //            tabLink2.Attributes.Add("class", "nav-link active");
        //            tabLink1.Attributes.Add("class", "nav-link");
        //            tab1.Attributes.Add("class", "tab-pane ");
        //            tab2.Attributes.Add("class", "tab-pane active");
        //            return;
        //        }
        //        using (IComplaintEnquiryRepository objData = new ComplaintEnquiryRepository(Functions.strSqlConnectionString))
        //        {
        //            GetAllComplaintEnquiryMasterResult objModel = new GetAllComplaintEnquiryMasterResult();
        //            FillControl(objModel, false);
        //            if (!objData.InsertOrUpdateComplaintEnquiry(objModel, false, out strError))
        //            {
        //                FillCaptcha();
        //                lblErrorComplain.ForeColor = System.Drawing.Color.Green;
        //            }
        //            else
        //            {
        //                FillCaptcha();
        //                lblErrorComplain.ForeColor = System.Drawing.Color.Red;
        //            }
        //            lblErrorComplain.Text = strError;

        //        }
        //        tabLink2.Attributes.Add("class", "nav-link active");
        //        tabLink1.Attributes.Add("class", "nav-link");
        //        tab1.Attributes.Add("class", "tab-pane ");
        //        tab2.Attributes.Add("class", "tab-pane active");
        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}

        //private void FillControl(GetAllComplaintEnquiryMasterResult objModel, bool isEnquiry)
        //{
        //    if(isEnquiry)
        //    {
        //        if(string.IsNullOrWhiteSpace(txtEnquiryFullName.Value))
        //        {
        //            lblErrorEnquiry.Text = "Please Enter FullName";
        //            lblErrorEnquiry.ForeColor = System.Drawing.Color.Red;
        //            txtEnquiryFullName.Focus();

        //            tabLink1.Attributes.Add("class", "nav-link active");
        //            tab1.Attributes.Add("class", "tab-pane active");
        //            tab2.Attributes.Add("class", "tab-pane ");
        //            tabLink2.Attributes.Add("class", "nav-link");
        //            FillCaptcha();
        //            return;
        //        }
        //        if (string.IsNullOrWhiteSpace(txtEnquiryMessage.Value))
        //        {
        //            lblErrorEnquiry.Text = "Please Enter Message";
        //            lblErrorEnquiry.ForeColor = System.Drawing.Color.Red;
        //            txtEnquiryMessage.Focus();

        //            tabLink1.Attributes.Add("class", "nav-link active");
        //            tab1.Attributes.Add("class", "tab-pane active");
        //            tab2.Attributes.Add("class", "tab-pane ");
        //            tabLink2.Attributes.Add("class", "nav-link");
        //            FillCaptcha();
        //            return;
        //        }
        //    }
        //    else
        //    {

        //        if (string.IsNullOrWhiteSpace(txtCompainFullName.Value))
        //        {
        //            lblErrorComplain.Text = "Please Enter FullName";
        //            lblErrorComplain.ForeColor = System.Drawing.Color.Red;
        //            txtCompainFullName.Focus();

        //            tabLink2.Attributes.Add("class", "nav-link active");
        //            tabLink1.Attributes.Add("class", "nav-link");
        //            tab1.Attributes.Add("class", "tab-pane ");
        //            tab2.Attributes.Add("class", "tab-pane active");
        //            FillCaptcha();
        //            return;
        //        }
        //        if (string.IsNullOrWhiteSpace(txtComplainMessage.Value))
        //        {
        //            lblErrorComplain.Text = "Please Enter Message";
        //            lblErrorComplain.ForeColor = System.Drawing.Color.Red;
        //            txtComplainMessage.Focus();

        //            tabLink2.Attributes.Add("class", "nav-link active");
        //            tabLink1.Attributes.Add("class", "nav-link");
        //            tab1.Attributes.Add("class", "tab-pane ");
        //            tab2.Attributes.Add("class", "tab-pane active");
        //            FillCaptcha();
        //            return;
        //        }
        //    }
        //}

        private void FillHomePagePopup()
        {
            DataSet ds = new PopUpMasterBAL().SelectHomepagePopupDetail();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["Active"].ToString() != "False")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "$('#myModal').modal('show')", true);
                }
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Popupmaster.DataSource = ds.Tables[0];
                        Popupmaster.DataBind();
                    }
                }
            }
        }

        public static ChatBoxResponseModel GetChatReturn(string txtMessage)
        {
            ChatBoxResponseModel objBos = new ChatBoxResponseModel();

            #region Bind Logic
            StringBuilder strChatBuilder = new StringBuilder();
            if (ChatBoxStage == ChatBoxEnum.Welcome || string.IsNullOrWhiteSpace(ChatBoxValues.Name))
            {
                strChatBox = "";
                ChatBoxStageMessageCount = 0;
                strChatBuilder.Append("");
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Thanks for contacting us. Please tell something about yourself </div>");
                strChatBuilder.Append("</div>");
                ChatBoxStageMessageCount++;
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Your Name </div>");
                strChatBuilder.Append("</div>");
            }
            //else
            if (ChatBoxStage == ChatBoxEnum.Name || string.IsNullOrWhiteSpace(ChatBoxValues.Name))
            {

                if (string.IsNullOrWhiteSpace(txtMessage))
                {

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Name</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                }
                else
                {
                    ChatBoxStage = ChatBoxEnum.EmailId;

                    ChatBoxModel objBo = new ChatBoxModel();
                    objBo.Name = txtMessage;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.Name + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Hi " + ChatBoxValues.Name + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Email</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
            }
            else if (ChatBoxStage == ChatBoxEnum.EmailId || string.IsNullOrWhiteSpace(ChatBoxValues.EmailId) && !ChatBoxValues.IsSkipEmail)
            {

                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Email Id</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                }
                else if (txtMessage.ToLower() == "skip")
                {

                    ChatBoxStage = ChatBoxEnum.Location;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.IsSkipEmail = true;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Location</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else if (Functions.ValidateEmailId(txtMessage))
                {
                    ChatBoxStage = ChatBoxEnum.Location;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.EmailId = txtMessage;
                    ChatBoxValues = objBo;


                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.EmailId + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>EmailId is <br/>" + ChatBoxValues.EmailId + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Location</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + txtMessage + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-0' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Valid Email Id</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
            }

            else if (ChatBoxStage == ChatBoxEnum.Location || string.IsNullOrWhiteSpace(ChatBoxValues.YouLocation))
            {
                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Location</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    ChatBoxStage = ChatBoxEnum.Phone;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.YouLocation = txtMessage;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.YouLocation + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Location is <br/>" + ChatBoxValues.YouLocation + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Phone No</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
            }

            else if (ChatBoxStage == ChatBoxEnum.Phone || string.IsNullOrWhiteSpace(ChatBoxValues.Phone))
            {

                long lgPhone = 0;
                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Location</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else if (txtMessage.Length == 10 && long.TryParse(txtMessage, out lgPhone))
                {
                    ChatBoxStage = ChatBoxEnum.PastMedicalHistory;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.Phone = txtMessage;
                    ChatBoxValues = objBo;


                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.Phone + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Phone No is <br/>" + ChatBoxValues.Phone + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Past Medical History</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + txtMessage + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter valid Phone No</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
            }

            else if ((ChatBoxStage == ChatBoxEnum.PastMedicalHistory || string.IsNullOrWhiteSpace(ChatBoxValues.PastMedicalHistory)) && !ChatBoxValues.IsSkipPastMedicalHistory)
            {
                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Past Medical History</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else if (txtMessage.ToLower() == "skip")
                {

                    ChatBoxStage = ChatBoxEnum.PresentMedicalHistory;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.IsSkipPastMedicalHistory = true;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Present Medical History</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    ChatBoxStage = ChatBoxEnum.PresentMedicalHistory;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.PastMedicalHistory = txtMessage;
                    ChatBoxValues = objBo;


                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.PastMedicalHistory + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Past Medical History is <br/>" + ChatBoxValues.PastMedicalHistory + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Present Medical History</div>");
                    strChatBuilder.Append("</div>");
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    ChatBoxStageMessageCount++;
                }
            }

            else if ((ChatBoxStage == ChatBoxEnum.PresentMedicalHistory || string.IsNullOrWhiteSpace(ChatBoxValues.PresentMedicalHistory)) && !ChatBoxValues.IsSkipPresentMedicalHistory)
            {
                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Present Medical History</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<a class='skipdata' href='#' onclick=' ChatResponseSkip(\"skip\"); return false; '>Skip</a>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else if (txtMessage.ToLower() == "skip")
                {

                    ChatBoxStage = ChatBoxEnum.PresentMedicalHistory;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.IsSkipPresentMedicalHistory = true;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Your Query</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    ChatBoxStage = ChatBoxEnum.WriteQuery;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.PresentMedicalHistory = txtMessage;
                    ChatBoxValues = objBo;


                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.PresentMedicalHistory + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Present Medical History is <br/>" + ChatBoxValues.PresentMedicalHistory + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Enter Your Query</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
            }


            else if (ChatBoxStage == ChatBoxEnum.WriteQuery || string.IsNullOrWhiteSpace(ChatBoxValues.WriteQuery))
            {
                if (string.IsNullOrWhiteSpace(txtMessage))
                {
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Please Enter Your Query</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                }
                else
                {
                    ChatBoxStage = ChatBoxEnum.End;

                    ChatBoxModel objBo = ChatBoxValues;
                    objBo.WriteQuery = txtMessage;
                    objBo.IPAddress = Functions.GetIPAddress;
                    ChatBoxValues = objBo;

                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg self'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>" + ChatBoxValues.WriteQuery + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;
                    strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                    strChatBuilder.Append("	<div class='cm-msg-text'>Your Query is <br/>" + ChatBoxValues.WriteQuery + "</div>");
                    strChatBuilder.Append("</div>");
                    ChatBoxStageMessageCount++;

                    strChatBox = "";

                    using (ChatBotRepository obj = new ChatBotRepository(Functions.strSqlConnectionString))
                    {
                        string strError = "";
                        if(!obj.InsertChatBotDetails(ChatBoxValues,out strError))
                        {

                            string pathToHTMLFile = System.Web.Hosting.HostingEnvironment.MapPath("~/html/ChatBot.html");
                            string htmlString = File.ReadAllText(pathToHTMLFile);

                            htmlString = htmlString.Replace("{{Name}}", ChatBoxValues.Name);

                            if (!ChatBoxValues.IsSkipEmail)
                            {
                                htmlString = htmlString.Replace("{{EmailId}}", ChatBoxValues.EmailId);
                            }
                            else
                            {
                                htmlString = htmlString.Replace("{{EmailId}}", "Skip");
                            }
                            //htmlString = htmlString.Replace("{{VisitType}}", objBo.VisitType);
                            //htmlString = htmlString.Replace("{{VisitNumber}}", objBo.VisitNumber);
                            htmlString = htmlString.Replace("{{Phone}}", ChatBoxValues.Phone);
                            htmlString = htmlString.Replace("{{YouLocation}}", ChatBoxValues.YouLocation);

                            if (!ChatBoxValues.IsSkipPastMedicalHistory)
                            {
                                htmlString = htmlString.Replace("{{PastMedicalHistory}}", ChatBoxValues.PastMedicalHistory);
                            }
                            else
                            {
                                htmlString = htmlString.Replace("{{PastMedicalHistory}}", "Skip");
                            }
                            if (!ChatBoxValues.IsSkipPresentMedicalHistory)
                            {
                                htmlString = htmlString.Replace("{{PresentMedicalHistory}}", ChatBoxValues.PresentMedicalHistory);
                            }
                            else
                            {
                                htmlString = htmlString.Replace("{{PresentMedicalHistory}}", "Skip");
                            }
                            htmlString = htmlString.Replace("{{WriteQuery}}", ChatBoxValues.WriteQuery);

                            string Message = "";

                            Functions.SendEmail(ConfigDetailsValue.ToMailGetFeedback, "Chat Box From " + ChatBoxValues.Name, htmlString, out Message,true);


                            strChatBuilder = new StringBuilder();

                            strChatBuilder.Append("<div class='final-content text-center'>");
                            strChatBuilder.Append("	<div class='icon mb-20'>");
                            strChatBuilder.Append("		<img src='" + Functions.ResolveUrl("~/Hospital/assets/img/big-green-check.png") + "' alt=''>");
                            strChatBuilder.Append("	</div>");
                            strChatBuilder.Append("	<h4>Application Submitted</h4>");
                            strChatBuilder.Append("	<p>" + ChatBoxValues.Name + ", thanks for providing your details, Our Executive will contact you by email or call</p>");
                            strChatBuilder.Append("</div>");
                        }

                    }

                    strChatBox = "";
                    strChatBuilder.Append("");
                    ChatBoxStageMessageCount = 0;
                    ChatBoxValues = new ChatBoxModel();
                    ChatBoxStage = ChatBoxEnum.Welcome;
                }
            }
            else
            {
                ChatBoxStage = ChatBoxEnum.Welcome;

                strChatBox = "";
                ChatBoxStageMessageCount = 0;
                strChatBuilder.Append("");
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Thanks for contacting us. Please tell something about yourself </div>");
                strChatBuilder.Append("</div>");
                ChatBoxStageMessageCount++;
                strChatBuilder.Append("<div id='cm-msg-" + ChatBoxStageMessageCount + "' class='chat-msg user'>");
                strChatBuilder.Append("	<div class='cm-msg-text'>Your Name </div>");
                strChatBuilder.Append("</div>");

                ChatBoxStageMessageCount++;
            }
            #endregion

            objBos.hfLastFieldName = "cm-msg-" + (ChatBoxStageMessageCount - 1);
            txtMessage = "";
            strChatBox += strChatBuilder.ToString();
            objBos.strChatBox = strChatBox;

            return objBos;
        }

        [WebMethod]
        public static ChatBoxResponseModel ChatResponse(string txtMessage)
        {
            ChatBoxResponseModel objBos = new ChatBoxResponseModel();

            objBos = GetChatReturn(txtMessage);
            
            return objBos;
        }

        protected void lnkSubmit_ServerClick(object sender, EventArgs e)
        {
            ChatBoxResponseModel objBos = new ChatBoxResponseModel();

            //objBos = GetChatReturn(txtMessage.Text);

            //hfLastFieldName.Value = objBos.hfLastFieldName;
            //txtMessage.Text = "";
            strChatBox = objBos.strChatBox;
        }
    }
}