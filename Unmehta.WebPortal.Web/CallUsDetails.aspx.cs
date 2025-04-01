using BAL;
using BO;
using System;
using System.Data;
using System.Linq;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Web.Hospital.Payment;

namespace Unmehta.WebPortal.Web
{
    public partial class CallUsDetails : System.Web.UI.Page
    {
        public static string strVisionMission;
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strVisionMission = GetCallUsDetails();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CallUsDetails").FirstOrDefault();

                if (dataMain != null)
                {
                    string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
                    //rowURl = rowURl.Substring(1);
                    StringBuilder strResearch = new StringBuilder();
                    ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
                    bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                    if (IsDisabledTranslate && Functions.LanguageId != 1)
                    {
                        {
                            Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic
                            return strResearch.ToString();
                        }
                    }
                    else
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
                            dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "CallUsDetails").FirstOrDefault();
                            if (languageId != 1)
                            {
                                goto LableData;
                            }
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }
        private string GetCallUsDetails()
        {

            CallUsMasterBO objBO = new CallUsMasterBO();
            CallUsMasterBAL objBAL = new CallUsMasterBAL();
            objBO.LanguageId = Functions.LanguageId;
            DataSet ds = objBAL.GetCallUsType(objBO);
            StringBuilder strVisionMission = new StringBuilder();
            strVisionMission.Length = 0;

            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus((int)objBO.LanguageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strVisionMission); // 10 sec page reload with english content logic
                    return strVisionMission.ToString();
                }
            }
            else
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        objBO.LanguageId = Functions.LanguageId;
                        objBO.CallUs_id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        DataSet dsDetails = objBAL.GetCallUsList(objBO);
                        if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
                        {
                            int listCount = dsDetails.Tables[0].Rows.Count;
                            if (string.IsNullOrEmpty(txtSerarch.Text))
                            {

                                strVisionMission.Append("<h4 class='card-title'>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</h4>");
                                strVisionMission.Append("<div class='row contact-address-section'>");

                                for (int j = i; j < listCount; j++)
                                {
                                    strVisionMission.Append("<div class='col-lg-4 col-md-12 lg-pl-0 md-mb-30 mt-20'>");
                                    strVisionMission.Append("<div class='contact-info contact-address'>");
                                    strVisionMission.Append("<div class='content-part'>");
                                    strVisionMission.Append("<p class='toptitileemc'>" + dsDetails.Tables[0].Rows[j]["Time"].ToString() + "</p>");
                                    strVisionMission.Append("<h5 class='info-subtitle'>" + dsDetails.Tables[0].Rows[j]["Title"].ToString() + "</h5>");
                                    //strVisionMission.Append("<h4 class='info-title'><a href = '#'>" + dsDetails.Tables[0].Rows[j]["Number"].ToString() + "</a></h4>");
                                    strVisionMission.Append("<h4 class='info-title'><a style='cursor:context-menu;'>" + dsDetails.Tables[0].Rows[j]["Number"].ToString() + "</a></h4>");
                                    strVisionMission.Append("<img src='" + ResolveUrl("~/Hospital/assets/img/servicio-24-7_white.png") + "' alt='service icon'>");
                                    strVisionMission.Append("</div>");
                                    strVisionMission.Append("</div>");
                                    strVisionMission.Append("</div>");
                                }
                            }
                            else
                            {
                                int k = 0;
                                for (int j = i; j < listCount; j++)
                                {
                                    if (dsDetails.Tables[0].Rows[j]["Title"].ToString().Contains(txtSerarch.Text) && k == 0)
                                    {
                                        strVisionMission.Append("<h4 class='card-title'>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</h4>");
                                        strVisionMission.Append("<div class='row contact-address-section'>");
                                        k++;
                                    }
                                }

                                for (int j = i; j < listCount; j++)
                                {
                                    if (dsDetails.Tables[0].Rows[j]["Title"].ToString().Contains(txtSerarch.Text))
                                    {
                                        strVisionMission.Append("<div class='col-lg-4 col-md-12 lg-pl-0 md-mb-30 mt-20'>");
                                        strVisionMission.Append("<div class='contact-info contact-address'>");
                                        strVisionMission.Append("<div class='content-part'>");
                                        strVisionMission.Append("<p class='toptitileemc'>" + dsDetails.Tables[0].Rows[j]["Time"].ToString() + "</p>");
                                        strVisionMission.Append("<h5 class='info-subtitle'>" + dsDetails.Tables[0].Rows[j]["Title"].ToString() + "</h5>");
                                        //strVisionMission.Append("<h4 class='info-title'><a href = '#'>" + dsDetails.Tables[0].Rows[j]["Number"].ToString() + "</a></h4>");
                                        strVisionMission.Append("<h4 class='info-title'><a style='cursor:context-menu;'>" + dsDetails.Tables[0].Rows[j]["Number"].ToString() + "</a></h4>");
                                        strVisionMission.Append("<img src='" + ResolveUrl("~/Hospital/assets/img/servicio-24-7_white.png") + "' alt='service icon'>");
                                        strVisionMission.Append("</div>");
                                        strVisionMission.Append("</div>");
                                        strVisionMission.Append("</div>");
                                    }
                                }
                            }
                            strVisionMission.Append("</div>");
                        }
                        else
                        {
                            strVisionMission.Append("<h4 class='card-title'>" + ds.Tables[0].Rows[i]["Name"].ToString() + "</h4>");
                            strVisionMission.Append("<div class='row contact-address-section'>");
                            strVisionMission.Append("</div>");
                        }
                    }

                    return strVisionMission.ToString();
                }
                else
                {
                    strVisionMission.Append("");
                    return strVisionMission.ToString();
                }
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            strVisionMission = GetCallUsDetails();
        }

        protected void btnReset_ServerClick(object sender, EventArgs e)
        {
            txtSerarch.Text = "";
            strVisionMission = GetCallUsDetails();
        }
    }
}