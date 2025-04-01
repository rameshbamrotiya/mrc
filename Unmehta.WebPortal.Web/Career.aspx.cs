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
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Unmehta.WebPortal.Web.Common.Functions;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace Unmehta.WebPortal.Web
{
    public partial class Career : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strCareer, strWhyJoinUNMICRC, strEmployeecareatUNMICRC, strGrowthatUNMICRC, strStarofUNMICRC;
        public static string strCareerjoblist;
        public static string strCareerjobwalkinlist;
        public static string strCareerjobwalkinlistNewIcon;
        public static int rid;

        protected void Page_Load(object sender, EventArgs e)
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            long id;
            if (queryString != "")
            {
                rid = Convert.ToInt32(queryString.ToString());
            }
            else
            {
                rid = 0;
            }
            if (!IsPostBack)
            {

                strCareer = GetPageData();
                strHeaderImage = GetHeaderImage();
                strCareerjoblist = GetPageDataDefaultjob();
                strCareerjobwalkinlist = GetPageDataDefaultjobWalkin();
                BindGridView();

                strWhyJoinUNMICRC = "Why Join UNMICRC";
                strEmployeecareatUNMICRC = "Employee care at UNMICRC";
                strGrowthatUNMICRC = "Grow that UNMICRC";
                strStarofUNMICRC = "Star of UNMICRC";

                using (IStarOfRepository objBlogCategoryMasterRepository = new StarOfRepository(Functions.strSqlConnectionString))
                using (IUnmicrCareerRepository objUnmicrCareerRepository = new UnmicrCareerRepository(Functions.strSqlConnectionString))
                {
                    var data = objUnmicrCareerRepository.GetUnmicrCareerMasterByLanguageId(Functions.LanguageId);
                    if (data != null)
                    {

                        strWhyJoinUNMICRC = data.UnmicrcWhyJoinTitle;
                        strEmployeecareatUNMICRC = data.UnmicrcEmployeeCareTitle;
                        strGrowthatUNMICRC = data.UnmicrcGroveThatTitle;
                    }
                    var dataMain = objBlogCategoryMasterRepository.GetAllStarOfDetails(LanguageId).FirstOrDefault();
                    if (dataMain != null)
                    {
                        strStarofUNMICRC = dataMain.StarPageTitle;
                    }
                }
            }
        }

        private string GetPageData()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strCareerbind = new StringBuilder();
            int LanguageId = languageId;
            DataSet ds = new CareerBAL().SelectRecord();
            LableData:
            if (ds != null)
            {
                strCareerbind = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strURL = "";

                        if (!string.IsNullOrWhiteSpace(row["rid"].ToString()))
                        {
                            strURL = ResolveUrl(("~/career?" + Functions.Base64Encode(row["rid"].ToString())));
                            if (strURL.StartsWith("~/", StringComparison.Ordinal))
                            {
                                strURL = strURL.Replace("~/", "");
                                strURL = ResolveUrl("~/" + strURL);
                            }

                        }
                        //string strURL = ResolveUrl(("~/career?" + Functions.Base64Encode(row["rid"].ToString())));
                        strCareerbind.Append("<div class='jb_browse_category jb_cover'>");
                        strCareerbind.Append("<a class='dt-sc-colored-big-buttons with-left-icon green' target='_self' title='' href='" + strURL + "'>");
                        strCareerbind.Append("<span> <i class='fas fa-clinic-medical'></i></span>");
                        strCareerbind.Append("<h3>" + row["Department"] + "</h3>");
                        //strCareerbind.Append("<p>(" + row["Noopening"] + " Opening)</p>");
                        strCareerbind.Append("</a>");
                        strCareerbind.Append("</div>");
                    }
                    i++;
                }
                else
                {
                    if (Functions.LanguageId != 1 && languageId != 1)
                    {
                        languageId = 1;
                    ds = new CareerBAL().SelectRecord();
                        goto LableData;
                    }
                };
                BindGridView();
            }
            return strCareerbind.ToString();
        }

        private string GetPageDataDefaultjob(string designation="", string PostType="", string RecruitmentType="")
        {
            int languageId = Functions.LanguageId;
            StringBuilder strCareerbind = new StringBuilder();
            int LanguageId = languageId;
            DataSet ds = null;
            ds = new CareerBAL().SelectRecordJob(rid);
            if (designation != "" || PostType != "" || RecruitmentType != "")
            {
                ds = new CareerBAL().SelectRecordJobSearch(designation, PostType, RecruitmentType);
            }
            bool iswalkin;

            LableData:
            if (ds != null)
            {
                strCareerbind = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1;
                    DataRow dr = ds.Tables[0].Rows[0];
                    strCareerbind.Append("<div class='filter-area jb_cover'>");
                    strCareerbind.Append("<div class='showpro'>");
                    strCareerbind.Append("<h4>" + dr["Department"] + "</h4>");
                    strCareerbind.Append("</div>");
                    strCareerbind.Append("<div class='list-grid'>");
                    strCareerbind.Append("<ul class='nav nav-tabs'>");
                    strCareerbind.Append("<li class='nav-item'>");
                    strCareerbind.Append("<a class='nav-link active show' data-toggle='tab' href='#grid'> <i class='fas fa-th'></i></a>");
                    strCareerbind.Append("</li>");
                    strCareerbind.Append("<li class='nav-item'> <a class='nav-link' data-toggle='tab' href='#list'><i class='fas fa-list-ul'></i></a>");
                    strCareerbind.Append("</li>");
                    strCareerbind.Append("</ul>");
                    strCareerbind.Append("</div>");
                    strCareerbind.Append("</div>");

                    strCareerbind.Append("<div class='tab-content btc_shop_index_content_tabs_main jb_cover'>");
                    strCareerbind.Append("<div id='grid' class='tab-pane fade active show'>");
                    strCareerbind.Append("<div class='row'>");


                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(row["isWalkin"].ToString()))
                        {
                            iswalkin = Convert.ToBoolean(row["isWalkin"].ToString());
                        }
                        else
                        {
                            iswalkin = false;
                        }

                        string strName = "";
                        //strName = row["AdvertisementName"].ToString();
                        if (row["AdvertisementName"].ToString().Length > 122)
                        {
                            strName = row["AdvertisementName"].ToString().Remove(122);
                        }
                        else
                        {
                            strName = row["AdvertisementName"].ToString();
                        }

                        string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode(row["rid"].ToString())));
                        strCareerbind.Append("<div class='col-lg-6 col-md-6 col-sm-12'>");
                        strCareerbind.Append("<div class='job_listing_left_fullwidth job_listing_grid_wrapper jb_cover'>");
                        strCareerbind.Append("<div class='row'>");
                        strCareerbind.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-12'>");
                        strCareerbind.Append("<div class='jp_job_oneline'>");
                        strCareerbind.Append("<div class='jp_job_post_side_img'>");
                        strCareerbind.Append("<img src='Hospital/assets/img/features/feature-01.jpg' alt='post_img'>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<div class='jp_job_post_right_cont'>");
                        strCareerbind.Append("<h4><a href='#'>" + strName + "</a></h4>");
                        //strCareerbind.Append("<p>Ahmedavad</p>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-12'>");
                        strCareerbind.Append("<div class='jp_job_post_right_btn_wrapper jb_cover'>");
                        strCareerbind.Append("<ul>");
                        strCareerbind.Append("<li><a href='" + strURL + "'>View More</a></li> ");
                        if (!iswalkin)
                        {
                            strCareerbind.Append("<li> <a href='#'>apply</a></li>");
                        }                        
                        strCareerbind.Append("</ul>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                    }
                    strCareerbind.Append("</div>");
                    strCareerbind.Append("</div>");

                    strCareerbind.Append("<div id='list' class='tab-pane'>");
                    strCareerbind.Append("<div class='row'>");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(row["isWalkin"].ToString()))
                        {
                            iswalkin = Convert.ToBoolean(row["isWalkin"].ToString());
                        }
                        else
                        {
                            iswalkin = false;
                        }

                        string strName = "";
                        //strName = row["AdvertisementName"].ToString();
                        if (row["AdvertisementName"].ToString().Length > 122)
                        {
                            strName = row["AdvertisementName"].ToString().Remove(122);
                        }
                        else
                        {
                            strName = row["AdvertisementName"].ToString();
                        }

                        string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode(row["rid"].ToString())));
                        strCareerbind.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-12'>");
                        strCareerbind.Append("<div class='job_listing_left_fullwidth  jb_cover'>");
                        strCareerbind.Append("<div class='row'>");
                        strCareerbind.Append("<div class='col-lg-10 col-md-10 col-sm-12 col-12'>");
                        strCareerbind.Append("<div class='jp_job_oneline'>");
                        strCareerbind.Append("<div class='jp_job_post_side_img'>");
                        strCareerbind.Append("<img src='Hospital/assets/img/features/feature-01.jpg' alt='post_img'>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<div class='jp_job_post_right_cont'>");
                        strCareerbind.Append("<h4><a href='#'>" + strName + "</a></h4>");
                        //strCareerbind.Append("<p>Ahmedavad</p>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<div class='col-lg-2 col-md-2 col-sm-12 col-12'>");
                        strCareerbind.Append("<div class='jp_job_post_right_btn_wrapper sidebtnapp'>");
                        strCareerbind.Append("<ul>");
                        strCareerbind.Append("<li><a href='" + strURL + "'>View More</a></ li > ");
                        if (!iswalkin)
                        {
                            strCareerbind.Append("<li> <a href='#'>apply</a></li>");
                        }
                        strCareerbind.Append("</ul>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                    }
                    strCareerbind.Append("</div>");
                    strCareerbind.Append("</div>");
                    strCareerbind.Append("</div>");
                    i++;
                }
                else
                {
                    strCareerbind.Append("<h1 style='text-align: center;' class='title mb-0'>There is no job available</h1>");
                }
            }
            return strCareerbind.ToString();
        }

        private string GetPageDataDefaultjobWalkin()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strCareerbind = new StringBuilder();
            int LanguageId = languageId;
            DataSet ds = new CareerBAL().SelectRecordJobwalin(rid);
            LableData:
            if (ds != null)
            {
                strCareerbind = new StringBuilder();
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    List<DataTable> result = ds.Tables[0].AsEnumerable().GroupBy(row => new { publishdate= row.Field<string>("publishdate").ToString(), Recruitment_Name= row.Field<string>("Recruitment_Name").ToString() }).Select(g => g.CopyToDataTable()).ToList();


                    //var newIcon = ds.Tables[0].AsEnumerable().Where(row => row.Field<int?>("IsNewIcon") ==1).Count();
                    //if (newIcon > 0)
                    //{
                    //    strCareerjobwalkinlistNewIcon = "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />";
                    //}
                    //else
                    //{
                    //    strCareerjobwalkinlistNewIcon = "";
                    //}
                    int i = 1;
                    foreach (DataTable dt in result)
                    {
                        DataRow dr = dt.Rows[0];
                        string strNew = "";
                        //if(dr["IsNewIcon"] !=DBNull.Value)
                        //{
                        //    if(dr["IsNewIcon"].ToString()=="1")
                        //    {
                        //        strNew = "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />";
                        //    }
                        //}
                        var newIcon = dt.AsEnumerable().Where(row => row.Field<bool?>("IsNewIcon") == true).Count();
                        if (newIcon > 0)
                        {
                            strNew = "<img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" />";
                        }
                        else
                        {
                            strNew = "";
                        }

                        strCareerbind.Append("<div class='card'>");
                        strCareerbind.Append("<div class='card-header' role='tab' id='headingUnfiled" + i + "'>");
                        strCareerbind.Append("<a data-toggle='collapse' data-parent='#accordionEx78' href='#collapseUnfiled" + i + "' aria-expanded='true' aria-controls='collapseUnfiled" + i + "'>");
                        strCareerbind.Append("<div class='job_filter_sidebar_heading jb_cover'>");
                        strCareerbind.Append("<div class='hradtitle'>");
                        strCareerbind.Append("<h1>" + dr["Recruitment_Name"] + " "+ strNew + "</h1>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("<div class='datewalkin'>");
                        strCareerbind.Append("<span>Date:" + dr["publishdate"] + " <i class='fas fa-angle-down rotate-icon'></i></span>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</a>");
                        strCareerbind.Append("</div>");
                        if (i == 1)
                        {
                            strCareerbind.Append("<div id='collapseUnfiled" + i + "' class='collapse show' role='tabpanel' aria-labelledby='headingUnfiled" + i + "' data-parent='#accordionEx78'>");
                        }
                        else
                        {
                            strCareerbind.Append("<div id='collapseUnfiled" + i + "' class='collapse' role='tabpanel' aria-labelledby='headingUnfiled" + i + "' data-parent='#accordionEx78'>");
                        }

                        strCareerbind.Append("<div class='card-body'>");
                        strCareerbind.Append("<div class='row'>");
                        foreach (DataRow row in dt.Rows)
                        {
                            string strURL = ResolveUrl(("~/CareerDetails?" + Functions.Base64Encode("id=" + row["id"].ToString() + "&walkin=1")));
                            strCareerbind.Append("<div class='col-lg-6 col-md-6 col-sm-12'>");
                            strCareerbind.Append("<div class='job_listing_left_fullwidth job_listing_grid_wrapper jb_cover'>");
                            strCareerbind.Append("<div class='row'>");
                            strCareerbind.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-12'>");
                            strCareerbind.Append("<div class='jp_job_oneline'>");
                            strCareerbind.Append("<div class='jp_job_post_side_img'>");
                            strCareerbind.Append("<img src='Hospital/assets/img/features/feature-01.jpg' alt='post_img'>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("<div class='jp_job_post_right_cont'>");

                            string strName = "";

                            if(row["AdvertisementName"].ToString().Length > 94)
                            {
                                strName = row["AdvertisementName"].ToString().Remove(94);
                            }
                            else
                            {
                                strName = row["AdvertisementName"].ToString();
                            }
                            var newIcons = row["IsNewIcon"].ToString();
                            if (newIcons == "True")
                            {
                                strCareerbind.Append("<h4><a href='#'>" + strName + " <img src=\"" + ResolveUrl("~/Hospital/assets/img/new_blink.gif") + "\" /></a></h4>");
                            }
                            else
                            {
                                strCareerbind.Append("<h4><a href='#'>" + strName + "</a></h4>");
                            }


                            strCareerbind.Append("<ul class=\"datetime\">");
                            strCareerbind.Append("	<li>Interview Date & time");
                            strCareerbind.Append("    </li>");
                            strCareerbind.Append("	<li><i class=\"fas fa-calendar-clock\"></i> " + row["InterviewDate"] + " " + row["InterviewTime"] + "");
                            strCareerbind.Append("    </li>");
                            strCareerbind.Append("</ul>");
                            //strCareerbind.Append("<p>Ahmedavad</p>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-12'>");
                            strCareerbind.Append("<div class='jp_job_post_right_btn_wrapper jb_cover'>");
                            strCareerbind.Append("<ul>");
                            strCareerbind.Append("<li><a href='" + strURL + "'>View More</a></li> ");
                            //strCareerbind.Append("<li> <a href='applynow.html'>apply</a></li>");
                            strCareerbind.Append("</ul>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                            strCareerbind.Append("</div>");
                        }
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        strCareerbind.Append("</div>");
                        i++;
                    }
                }
                else
                {
                    strCareerbind.Append("<h1 style='text-align: center;' class='title mb-0'>There is no job available</h1>");
                }
            }
            return strCareerbind.ToString();
        }

        private void BindGridView()
        {
            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlRecruitmentType.DataSource = objEducationQualificationRepository.GetAllRecruitmentTypeMasterDetails();
                ddlRecruitmentType.DataTextField = "RecruitmentName";
                ddlRecruitmentType.DataValueField = "Id";
                ddlRecruitmentType.DataBind();
                ddlRecruitmentType.Items.Insert(0, "Select");
            }
            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlPostType.DataSource = objEducationQualificationRepository.GetAllPostTypeMasterDetails();
                ddlPostType.DataTextField = "PostName";
                ddlPostType.DataValueField = "Id";
                ddlPostType.DataBind();
                ddlPostType.Items.Insert(0, "Select");
            }
            using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            {
                ddldesignation.DataSource = objDesignationRepository.GetAllTblDesignationLang(1);
                ddldesignation.DataValueField = "Id";
                ddldesignation.DataTextField = "DesignationName";
                ddldesignation.DataBind();
                ddldesignation.Items.Insert(0, "Select");
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Career").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Career").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string designation = "0";
            string PostType = "0";
            string RecruitmentType = "0";
            if (ddldesignation.SelectedValue != "Select")
            {
                designation = ddldesignation.SelectedValue;
            }
            if (ddlPostType.SelectedValue != "Select")
            {
                PostType = ddlPostType.SelectedValue;
            }
            if (ddlRecruitmentType.SelectedValue != "Select")
            {
                RecruitmentType = ddlRecruitmentType.SelectedValue;
            }
            strCareerjoblist = GetPageDataDefaultjob(designation,PostType,RecruitmentType);
        }

        private void LoadControls(UploadCVCareer objBo)
        {
            
            string documentfile = string.Empty;
            documentfile = SaveFile();

            if (!string.IsNullOrEmpty(documentfile))
                objBo.FilePath = documentfile;
            objBo.FullName = txtFullName.Text;
            objBo.EmailId = txtEmail.Text;
            objBo.MobileNo = txtMobileNo.Text;
            objBo.Designation = txtDesignation.Text;
            objBo.Location = txtlocation.Text;
        }

        protected void btnCvSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                llblwarningmess.Text = "";
                lblMessage.Text = "";
                UploadCVCareer objBo = new UploadCVCareer();
                var fname = Path.GetExtension(cvfile.FileName);
                if (fname != ".pdf" )
                {
                    llblwarningmess.Visible = true;
                    llblwarningmess.Text = "Please add .pdf file.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupcareer();", true);
                    return;
                }
                LoadControls(objBo);
                if (new CareerBAL().InsertRecord(objBo))
                {
                    llblwarningmess.Visible = false;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record inserted successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    clearFeedbackControl();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupcareer();", true);
                    return;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record already exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    clearFeedbackControl();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopupcareer();", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = false;
                llblwarningmess.Visible = false;
                llblwarningmess.Text = ex.Message.ToString();
                lblMessage.Text = "";
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private void clearFeedbackControl()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtDesignation.Text = "";
            txtlocation.Text = "";
        }

        private string SaveFile()
        {
            try
            {

                if (cvfile.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.CVCareer;
                    var fname = Path.GetExtension(cvfile.FileName);
                    var count = cvfile.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < cvfile.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "doc":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        //Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = cvfile.FileName.Replace(" ", "_");

                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;



                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";



                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }


                            filename1 = tempfileName1;
                        }


                        //Save selected file into specified location
                        cvfile.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }
    }
}