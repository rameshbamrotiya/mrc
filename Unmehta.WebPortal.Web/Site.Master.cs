using BAL;
using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
    public class NotificationModel
    {
        public string ShortDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string Url { get; set; }
        public string type { get; set; }
    }

    public partial class Site : System.Web.UI.MasterPage
    {
        public static string strMenuString;
        public static string strHiddenMenu;
        public static string strNotofication;

        public static long lgWebSitecount;

        public static string strHeader, strFooter, strHeaderLogo, strFooterLogo;
        public static string strQuickLink, strSiteInfo, strAccredation, strHeaderAccredation;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLanguage();
                BindMenu();
                BindNotification();
                FillCapctha();
                BindHeaderFooter();
                using (IHomePageRepository objData = new HomePageRepository(Functions.strSqlConnectionString))
                {
                    string strError = "", strIpaddress = Functions.GetIPAddress;
                    if (!objData.UpdateWebSiteCount(strIpaddress, DateTime.UtcNow.Date, out strError))
                    {

                    }

                    var countData = objData.GetWebSiteCount();
                    if (countData != null)
                    {
                        lgWebSitecount = countData.VisitCount;
                    }
                }

            }
        }


        private void BindHeaderFooter()
        {
            using (IHomePageRepository objData = new HomePageRepository(Functions.strSqlConnectionString))
            {

                var data = objData.GetHeaderFooter();

                if (data != null)
                {
                    strHeader = Functions.CustomHTMLDecode(data.HeaderDetails,this.Page);
                    
                    strFooter = Functions.CustomHTMLDecode(data.FooterDetails, this.Page);

                    string strURLs ;

                    if (!string.IsNullOrWhiteSpace((data.HeaderLogo)))
                    {
                        strURLs = (data.FooterLogo.ToString());
                        if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Replace("~/", "");
                        }
                        if (strURLs.StartsWith("/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Remove(0, 1);
                        }
                        strURLs = ResolveUrl("~/" + strURLs);

                        strFooterLogo = strURLs;
                    }

                    if (!string.IsNullOrWhiteSpace((data.HeaderLogo)))
                        {
                        strURLs = (data.HeaderLogo.ToString());
                        if (strURLs.StartsWith("~/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Replace("~/", "");
                        }
                        if (strURLs.StartsWith("/", StringComparison.Ordinal))
                        {
                            strURLs = strURLs.Remove(0, 1);
                        }
                        strURLs = ResolveUrl("~/" + strURLs);
                        strHeaderLogo = strURLs;
                    }
                }
            }
            using (IFooterQuickLinkRepository objFooterQuickLinkRepository = new FooterQuickLinkRepository(Functions.strSqlConnectionString))
            {
                StringBuilder stringQuickLink = new StringBuilder();
                StringBuilder stringSiteInfo = new StringBuilder();
                var data = objFooterQuickLinkRepository.GetAllFacilitesMaster(Functions.LanguageId).Where(x=> x.IsActive==true).OrderBy(x=> x.SequanceNo).ToList();

                if(data.Count()>0)
                {
                    foreach (var item in data)
                    {

                        string strURLs = "";

                        if (!string.IsNullOrWhiteSpace(item.InternalLink))
                        {
                            if (item.InternalOrExternal == "Internal")
                            {
                                strURLs = (item.InternalLink.ToString());
                                if (strURLs.StartsWith("~", StringComparison.Ordinal))
                                {
                                    strURLs = strURLs.Replace("~", "");
                                }
                                if (strURLs.StartsWith("/", StringComparison.Ordinal))
                                {
                                    strURLs = strURLs.Replace("/", "");
                                }
                                strURLs = ResolveUrl("~/" + strURLs);
                            }
                            else
                            {
                                strURLs = item.InternalLink;
                            }
                        }

                        if (item.DisplaySection == "SiteMap")
                        {
                            stringSiteInfo.Append("<li><a href=\""+ strURLs + "\">"+ item.NameMenu + "</a></li>");
                        }
                        else
                        {
                            stringQuickLink.Append("<li><a href=\"" + strURLs + "\">" + item.NameMenu + "</a></li>");
                        }
                    }
                    strQuickLink = stringQuickLink.ToString();
                    strSiteInfo = stringSiteInfo.ToString();
                }

            }

            long languageId = Functions.LanguageId;

            StringBuilder strBoardOfDirector = new StringBuilder();
            AccredationMasterBAL accredationMasterBAL = new AccredationMasterBAL();
            StringBuilder stringAccredation = new StringBuilder();
            StringBuilder stringHeadAccredation = new StringBuilder();

            DataTable dtMain = accredationMasterBAL.SelectRecord(languageId).Tables[0];
            foreach (DataRow row in dtMain.Rows)
            {
                string strImage = ResolveUrl(row["ImgLogo"].ToString());
                stringAccredation.Append("");
                bool IsDisplayInFooter;
                if (bool.TryParse(row["IsDisplayInFooter"].ToString(), out IsDisplayInFooter))
                {
                    if (IsDisplayInFooter)
                    {
                        stringAccredation.Append("<li><a href='"+ row["AccredationURL"].ToString() + "' target='_blank'>");
                        stringAccredation.Append("    <img src='" + strImage + "' class='img-fluid' alt='Logo'>");
                        stringAccredation.Append("</a>");
                        stringAccredation.Append("</li>");
                    }
                }
                if (bool.TryParse(row["IsDisplayInHeader"].ToString(), out IsDisplayInFooter))
                {
                    if (IsDisplayInFooter)
                    {
                        stringHeadAccredation.Append("<li><a href='"+ row["AccredationURL"].ToString() + "' target='_blank'>");
                        stringHeadAccredation.Append("    <img src='" + strImage + "' class='img-fluid' alt='Logo'>");
                        stringHeadAccredation.Append("</a>");
                        stringHeadAccredation.Append("</li>");
                    }
                }

            }
            strHeaderAccredation = stringHeadAccredation.ToString();
            strAccredation = stringAccredation.ToString();
        }

        private void BindNotification()
        {
            StringBuilder strMenu = new StringBuilder();
            strNotofication = "";
            List<NotificationModel> lstNotificationModel = new List<NotificationModel>();
            using (IMenuMasterRepository objMenu = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                int i = 0;
                foreach (var row in objMenu.GetAllTenderMaster())
                {
                    if (i < 5)
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                    string strShortDesc = row.Title;
                    if (strShortDesc.Length > 40)
                    {
                        strShortDesc = strShortDesc.Remove(40) + "..";
                    }
                    
                    string strURLs = ResolveUrl("~/TenderDetails?" + Unmehta.WebPortal.Web.Common.Functions.Base64Encode(row.TenderID.ToString()));

                    lstNotificationModel.Add(new NotificationModel { ShortDesc = strShortDesc + " " + (row.IsNewIcon==1 ? "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />" : ""), type="Tender", EntryDate = (DateTime)row.EntryDate, Url = strURLs });

                }
                using (IBlogCategoryMasterRepository objBlogCategoryMasterRepository = new BlogCategoryMasterRepository(Functions.strSqlConnectionString))
                {
                    var dataList = objBlogCategoryMasterRepository.GetAllTblBlogCategoryMaster(1).OrderByDescending(x => x.BlogDate).ToList().Take(5).ToList();
                    foreach (var row in dataList)
                    {

                        string strBlogName = row.BlogName;

                        string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(row.ImagePath) ? "" : row.ImagePath);
                        string strURLs = ResolveUrl(("~/BlogDetails?" + Functions.Base64Encode(row.Id.ToString() + "|" + row.TypeDetail)));

                        lstNotificationModel.Add(new NotificationModel { ShortDesc = strBlogName + " " + (row.IsNewIcon == true ? "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />" : ""), EntryDate = (DateTime)row.BlogDate, Url = strURLs, type=row.TypeDetail });
                    }
                }


                //General Job
                DataTable ds = new CareerBAL().GetAllCareerRecordNotification();
                ds.DefaultView.Sort = "publishdate";
                DataTable dt = ds.DefaultView.ToTable();
                if (dt != null)
                {
                    //if (!dt.Rows.Equals(0))
                    {
                        if (!dt.Rows.Count.Equals(0))
                        {
                            i = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (i > 6)
                                {
                                    break;
                                }

                                string strName = "";

                                
                                {
                                    strName = HttpUtility.HtmlDecode(row["AdvertisementName"].ToString());
                                }
                                
                                string strURL = ResolveUrl(("~/Career"));
                                NotificationModel objNotificationModels = new NotificationModel();
                                objNotificationModels.Url = strURL;
                                objNotificationModels.EntryDate = Convert.ToDateTime(row["publishdate"].ToString());
                                objNotificationModels.type = "Career";
                                objNotificationModels.ShortDesc = strName;
                                lstNotificationModel.Add(objNotificationModels);

                                i++;
                            }
                        }
                    }
                }

                var MainList = lstNotificationModel.OrderByDescending(x => x.EntryDate).Take(4).ToList();

                foreach (var row in MainList)
                {
                    strMenu.Append("");
                    strMenu.Append("<li class=\"notification-message\">");
                    strMenu.Append("	<a href=\"" + row.Url + "\" > <div class=\"media\">");
                    strMenu.Append("		<div class=\"media-body\">");
                    strMenu.Append("			<p class=\"noti-details\">" + row.ShortDesc + "</p>");
                    strMenu.Append("			<p class=\"noti-details\">" + row.EntryDate.ToString("dd/MM/yyyy").Replace("-", "/") + " </p>");
                    strMenu.Append("			<a class=\"noti-time\">" + row.type + "</a>");
                    strMenu.Append("		</div>");
                    strMenu.Append("	</div></a>");
                    strMenu.Append("</li>");
                }
            }
            strNotofication = strMenu.ToString();
        }

        private void BindLanguage()
        {
            LanguageMasterBAL objBo = new LanguageMasterBAL();
            ddlLanguageList.DataSource = objBo.GetAllLanguage();
            ddlLanguageList.DataTextField = "Name";
            ddlLanguageList.DataValueField = "Id";
            ddlLanguageList.DataBind();
            ddlLanguageList.SelectedValue = Functions.LanguageId.ToString();
        }

        public void BindMenu()
        {
            //DataSet ds = new DataSet();
            //MenuBO objbo = new MenuBO();
            //MenuBAL objBAL = new MenuBAL();
            //objbo.Language = Functions.LanguageId.ToString();
            //ds = objBAL.SelectMenutype(objbo);
            //DataTable dt = ds.Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    Session["dt_menu"] = dt;
            //    Application["dt_menu"] = dt;
            //    var dv = dt.DefaultView;
            //    dv.RowFilter = "col_parent_id = 0";
            //    rptMenu.DataSource = dt;
            //    rptMenu.DataBind();
            //}
            StringBuilder strMenu = new StringBuilder();
            StringBuilder strHiddenMenuBui = new StringBuilder();
            using (IMenuMasterRepository objMenu = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                bool isActive = false;
                strMenuString = "";
                string strStartMenuString = "<li><a href='" + ResolveUrl("~/") + "' class='active'><i class='fas fa-home fa-4x' style='font-size: large;'></i></a></li>";
                strHiddenMenu = "";

                List<tbl_Menu_MasterSelectAllResult> lstList = objMenu.GetAllMenuList(Functions.LanguageId);

                List<tbl_Menu_MasterSelectAllResult> lstHiddenList = lstList.Where(x => x.col_menu_type == '4').ToList();
                lstList = lstList.Where(x => x.col_menu_type != '4').ToList();

                foreach (var mainMenu in lstHiddenList)
                {
                    strHiddenMenuBui.Append("<li><a href=" + ResolveUrl("~/" + mainMenu.MaskingURL) + " ><span>" + HttpUtility.HtmlDecode(mainMenu.col_menu_name) + "</span></a></li>");
                }

                List<tbl_Menu_MasterSelectAllResult> lstParentList = lstList.Where(x => x.col_parent_id == 0 && x.col_menu_type != '3').ToList();

                foreach (var mainMenu in lstParentList)
                {
                    strMenu.Append(GetParentIdString(mainMenu, lstList, isActive, out isActive));
                }

                strStartMenuString = "<li><a href='" + ResolveUrl("~/") + "' " + (!isActive ? "" : "") + "><i class='fas fa-home fa-4x' style='font-size: large;'></i></a></li>";

                strMenuString = (strStartMenuString + strMenu.ToString());
                strHiddenMenu = strHiddenMenuBui.ToString();
            }
        }

        private bool LoadControls(FeedbackBO objBo)
        {

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return false;
            }
            else if (!Functions.ValidateEmailId(txtEmail.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMobileNo.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCoutry.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtState.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFeedBack.Text))
            {
                return false;
            }

            objBo.FullName = txtFullName.Text;
            objBo.EmailId = txtEmail.Text;
            objBo.MobileNo = txtMobileNo.Text;
            objBo.Country = txtCoutry.Text;
            objBo.State = txtState.Text;
            objBo.City = txtCity.Text;
            objBo.unmericfeedback = true;
            objBo.FeedbackDescription = txtFeedBack.Text;
                return true;
        }

        protected void btnFeedbackSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                FeedbackBO objBo = new FeedbackBO();
                if (LoadControls(objBo))
                {
                    if (new FeedbackBAL().InsertRecord(objBo))
                    {

                        string pathToHTMLFile = Server.MapPath("~/html/Write to Unmicrc.html");
                        string htmlString = File.ReadAllText(pathToHTMLFile);

                        htmlString = htmlString.Replace("{{FullName}}", objBo.FullName);
                        htmlString = htmlString.Replace("{{EmailId}}", objBo.EmailId);
                        //htmlString = htmlString.Replace("{{VisitType}}", objBo.VisitType);
                        //htmlString = htmlString.Replace("{{VisitNumber}}", objBo.VisitNumber);
                        htmlString = htmlString.Replace("{{MobileNo}}", objBo.MobileNo);
                        htmlString = htmlString.Replace("{{Country}}", objBo.Country);
                        htmlString = htmlString.Replace("{{State}}", objBo.State);
                        htmlString = htmlString.Replace("{{City}}", objBo.City);
                        htmlString = htmlString.Replace("{{FeedbackDescription}}", objBo.FeedbackDescription);
                        string Message = "";

                        Functions.SendEmail(ConfigDetailsValue.ToMailGetFeedback, "Write to Unmicrc From " + objBo.FullName, htmlString, out Message,true);

                        //lblMessage.Visible = true;
                        //lblMessage.Text = "Record inserted successfully.";
                        //lblMessage.ForeColor = System.Drawing.Color.Green;
                        clearFeedbackControl();
                        lblMessageBox.InnerHtml = "Feedback submitted successfully.!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowAlertPopupSubscribeNewsletter();", true);
                        return;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Record already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        clearFeedbackControl();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
                        return;
                    }
                }
                else
                {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
                        return;

                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this.Page);
            }
        }

        private void clearFeedbackControl()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtCoutry.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtFeedBack.Text = "";
        }

        private string GetParentIdString(tbl_Menu_MasterSelectAllResult mainMenu, List<tbl_Menu_MasterSelectAllResult> lstList, bool isActive, out bool isOutActive)
        {
            if (!isActive)
            {
                string rowURL1 = Request.RawUrl.ToString();
                string strRow = (Request.Url.OriginalString).Replace(rowURL1, "");
                strRow = strRow.Split('/').Last();
                strRow = strRow.Replace("/", "");
                if (mainMenu.col_menu_url.ToString().ToLower() == strRow.ToLower())
                {
                    isActive = true;
                }

            }
            StringBuilder strMenu = new StringBuilder();
            List<tbl_Menu_MasterSelectAllResult> lstSubList = lstList.Where(x => x.col_parent_id == mainMenu.col_menu_id && x.col_menu_type != '3').ToList();
            if (lstSubList.Count() > 0)
            {
                strMenu.Append("<li class='has-submenu'><a href='" + ResolveUrl("~/" + mainMenu.MaskingURL) + "' " + (isActive ? " class='active' " : "") + " >" + HttpUtility.HtmlDecode(mainMenu.col_menu_name) + " <i class='fas fa-chevron-down'></i></a><ul class='submenu'>");
                foreach (var mainMenus in lstSubList)
                {
                    if (lstSubList.Count() > 0)
                    {
                        strMenu.Append(GetParentIdString(mainMenus, lstList, isActive, out isActive));
                    }
                    else
                    {
                        string strPath = mainMenu.MaskingURL;
                        strMenu.Append("<li><a href='" + (string.IsNullOrWhiteSpace(strPath) || strPath == "#" ? "#" : strPath) + "'>" + HttpUtility.HtmlDecode(mainMenu.col_menu_name) + "</a></li>");
                    }
                }
                strMenu.Append("</ul></li>");
            }
            else
            {
                strMenu.Append("<li><a href='" + ResolveUrl("~/" + mainMenu.MaskingURL) + "'>" + HttpUtility.HtmlDecode(mainMenu.col_menu_name) + "</a></li>");
            }
            isOutActive = isActive;
            return strMenu.ToString();
        }

        private string GetReturnUrl(string rowURl, string rowQueryString)
        {


            BAL.TemplateMasterBAL objTemplateMasterBAL = new BAL.TemplateMasterBAL();

            List<GetAllPageListWithMaskingUrlResult> lstData = new List<GetAllPageListWithMaskingUrlResult>();

            if (rowURl.Contains(".aspx") && !rowURl.Contains("?"))
            {
                rowURl = rowURl.Split('/').Last();
                rowURl = rowURl.Replace("/", "");
                lstData = objTemplateMasterBAL.GetAllPageListWithUrl().Where(x => x.col_menu_url.ToString().ToLower() == rowURl.ToLower()).ToList();
            }
            else
            {
                string[] queryString = rowQueryString.Split('&');

                if (queryString.Where(x => x.Contains("menu")).Count() > 0)
                {
                    string menuId = queryString.Where(x => x.Contains("menu")).FirstOrDefault().Split('=')[1];
                    lstData = objTemplateMasterBAL.GetAllPageListWithUrl().Where(x => x.col_menu_id.ToString() == menuId).ToList();
                }
            }

            if (lstData != null)
            {
                lstData = lstData.Where(x => x.LanguageId == Functions.LanguageId).ToList();
                if (lstData == null)
                {
                    lstData = lstData.Where(x => x.LanguageId == 1).ToList();
                }
                if (lstData.Count == 0)
                {
                    lstData = lstData.Where(x => x.LanguageId == 1).ToList();
                }
                lstData.ForEach(x =>
                {
                    //Response.Redirect("/" + x.MaskingURL);
                    rowURl = x.MaskingURL;
                });
            }


            return rowURl;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            Functions.LanguageId = Convert.ToInt32(ddlLanguageList.SelectedValue);
            BindMenu();
            BAL.TemplateMasterBAL objTemplateMasterBAL = new BAL.TemplateMasterBAL();
            //string rowURL1 = Request.RawUrl.ToString().Replace("/", "");

            string strData = Request.RawUrl.ToString();

            string strSubData = Request.ApplicationPath.ToString().Replace("/", "");

            StringBuilder strURLss =new StringBuilder(ResolveUrl(string.IsNullOrWhiteSpace(strSubData) ? "" : "/"+strSubData + "/") + (strData));

            //string rowURL1 = Request.RawUrl.ToString();
            //string strRow = (Request.Url.OriginalString).Replace(rowURL1, "");
            // var urlData = objTemplateMasterBAL.GetAllPageListWithUrl().Where(x => x.col_menu_url.ToString() == rowURL1).ToList();

            string rowQueryString = Page.Request.QueryString.ToString();

            if (!string.IsNullOrWhiteSpace(strSubData))
            {
                strURLss = strURLss.Replace("/" + strSubData + "/" + "/" + strSubData + "/", "/" + strSubData + "/");
            }

            string NewURL = GetReturnUrl(strURLss.ToString(), rowQueryString);


            BindHeaderFooter();
            Response.Redirect((NewURL));
        }

        protected void btnSearchBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearchBox.Value.Trim()))
            {
                Response.Redirect("~/SearchDetails?" + HttpUtility.UrlEncode(Functions.Base64Encode(txtSearchBox.Value.Trim())));
            }
        }

        protected void btnNewsLetter_ServerClick(object sender, EventArgs e)
        {
            using (INewsLetterMasterRepository objNewsLetterMasterRepository = new NewsLetterMasterRepository(Functions.strSqlConnectionString))
            {
                string strMessage = "";
                if (objNewsLetterMasterRepository.GetAllNewsLetterSubScription().Where(x => x.NewsLetterEmail == txtNewsletterEmail.Value.Trim()).Count() <= 0)
                {
                    if (!objNewsLetterMasterRepository.InsertOrUpdateNewsLetterMaster(new Data.Common.GetAllNewsLetterSubScriptionResult { Id = 0, NewsLetterEmail = txtNewsletterEmail.Value.Trim(), NewsLetterSubscription = true }, out strMessage))
                    {
                        strMessage = txtNewsletterEmail.Value.Trim() + " News Subscriber Added";
                        lblError.ForeColor = System.Drawing.Color.Green;
                        txtNewsletterEmail.Value = "";
                    }
                    else
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    var objData = objNewsLetterMasterRepository.GetAllNewsLetterSubScription().Where(x => x.NewsLetterEmail == txtNewsletterEmail.Value.Trim()).FirstOrDefault();
                    if (objData.NewsLetterSubscription == false)
                    {
                        if (!objNewsLetterMasterRepository.UpdateNewsLetterMasterSubscription(objData.Id, true, out strMessage))
                        {
                            strMessage = txtNewsletterEmail.Value.Trim() + " News Subscriber Updated";
                            lblError.ForeColor = System.Drawing.Color.Green;
                            txtNewsletterEmail.Value = "";
                        }
                    }
                    else
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                        strMessage = txtNewsletterEmail.Value.Trim() + " Subscriber Already Existed";
                    }
                }

            }
        }

        #region Subscribe Newsletter 
        private bool LoadControlsSubscribeNewsletter(SubscribeNewsletterMasterBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtSNFullName.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSNEmailId.Text))
            {
                return false;
            }
            else if(!Functions.ValidateEmailId(txtSNEmailId.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSNMobileNumber.Text))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSNlocation.Text))
            {
                return false;
            }


            objBo.FullName = txtSNFullName.Text;
            objBo.EmailId = txtSNEmailId.Text;
            objBo.MobileNo = txtSNMobileNumber.Text;
            objBo.Location = txtSNlocation.Text;

            return true;
        }
        protected void btnSubscribeNewsletterSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblCapatchMessage.Text = "";
                lblCapatchMessage.Visible = false;
                if (Session["captcha"].ToString() != txtCaptcha.Text)
                {
                    FillCapctha();
                    txtCaptcha.Text = string.Empty;
                    lblCapatchMessage.Text = "Captcha does not match";
                    lblCapatchMessage.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupSubscribeNewsletter();", true);
                    return;
                }

                SubscribeNewsletterMasterBO objBo = new SubscribeNewsletterMasterBO();
                if (LoadControlsSubscribeNewsletter(objBo))
                {
                    if (new SubscribeNewsletterMasterBAL().InsertRecord(objBo))
                    {
                        lblWarningMessage.Visible = true;
                        lblWarningMessage.Text = "Subscribe newsletter added successfully.";
                        lblWarningMessage.ForeColor = System.Drawing.Color.Green;
                        clearSubscribeNewsletterControl();
                        lblMessageBox.InnerHtml = "Subscribe newsletter added successfully.!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowAlertPopupSubscribeNewsletter();", true);
                        return;
                    }
                    else
                    {
                        lblWarningMessage.Visible = true;
                        lblWarningMessage.Text = "Subscribe newsletter already exists.";
                        lblWarningMessage.ForeColor = System.Drawing.Color.Red;
                        clearSubscribeNewsletterControl();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupSubscribeNewsletter();", true);
                        return;
                    }
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupSubscribeNewsletter();", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                lblWarningMessage.Visible = false;
                lblWarningMessage.Text = "";
                lblCapatchMessage.Visible = false;
                lblCapatchMessage.Text = "";
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this.Page);
            }
        }
        private void clearSubscribeNewsletterControl()
        {
            txtSNFullName.Text = "";
            txtSNEmailId.Text = "";
            txtSNMobileNumber.Text = "";
            txtSNlocation.Text = "";
            txtCaptcha.Text = "";
            FillCapctha();
        }
        protected void btnRun_ServerClick(object sender, EventArgs e)
        {
            FillCapctha();
            txtCaptcha.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupSubscribeNewsletter();", true);
            return;
        }
        protected void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();
                    imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                    //cvCaptcha.ValueToCompare= captcha.ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}