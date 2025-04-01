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
    public partial class Event : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strListOfSubSectionDescription;
        int eventid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
                strHeaderImage = GetHeaderImage();
                strListOfSubSectionDescription = GetListOfSubSectionDescription();
            }
        }
        private void BindDropDownList()
        {
            int languageId = Functions.LanguageId;
            EventMasterBAL objBal = new EventMasterBAL();
            ddlEventtype.Items.Clear();
            ddlEventtype.DataSource = objBal.SelectEventType(languageId);
            ddlEventtype.DataTextField = "EventName";
            ddlEventtype.DataValueField = "Id";
            ddlEventtype.DataBind();
            ddlEventtype.Items.Insert(0, new ListItem("-Select Event Type-", "-1"));
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
                    int i = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strURL = ResolveUrl(("~/EventDetails?" + Functions.Base64Encode(row["EventId"].ToString())));
                        strEvent.Append("<div class='col-lg-6'>");
                        strEvent.Append("<div class='thumbnail no-border no-padding mb-15'>");
                        strEvent.Append("<div class='row'>");
                        strEvent.Append("<div class='col - md - 4'>");
                        strEvent.Append("<div class='media_img'>");
                        strEvent.Append("<img class='img-fluid' alt='Entertainment Expo' src='" + ResolveUrl(row["MainImg"].ToString()) + "'>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("<div class='col - md - 8'>");
                        strEvent.Append("<div class='caption'>");
                        strEvent.Append("<h3 class='caption_title'><a href='" + strURL + "'>" + row["EventName"] + "</ a ></ h3 > ");
                        strEvent.Append("<p class='caption-category'><label>Date:</label>" + row["EventStartDate"].ToString() + "</p>");
                        strEvent.Append("<p class='caption-text'><label>Venue:</label>" + row["Venue"] + "</p>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        strEvent.Append("</div>");
                        i++;
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
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Event").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Event").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            string strEventTitle = txtEventTitle.Text;
            int strEventType = 0;

            if (ddlEventtype.SelectedIndex > 0)
            {
                strEventType = Convert.ToInt32(ddlEventtype.SelectedValue);
            }
            strListOfSubSectionDescription = GetListOfSubSectionDescription(strEventTitle, strEventType);
            txtEventTitle.Text = "";
        }

        protected void ddlEventtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strEventTitle = txtEventTitle.Text;
            int strEventType = 0;

            if (ddlEventtype.SelectedIndex > 0)
            {
                strEventType = Convert.ToInt32(ddlEventtype.SelectedValue);
            }
            strListOfSubSectionDescription = GetListOfSubSectionDescription(strEventTitle, strEventType);
        }
    }
}