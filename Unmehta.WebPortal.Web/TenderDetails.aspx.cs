using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class TenderDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strTenderList;
        public static int TenderId;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.IsPostBack)
                {
                    strHeaderImage = GetHeaderImage();
                    strTenderList = GetTenderList();
                    TenderMasterBO objBo = new TenderMasterBO();
                    int tenderId =0;
                    string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    if (!string.IsNullOrWhiteSpace(queryString))
                    {
                        if (int.TryParse(queryString, out tenderId))
                        {
                            objBo.TenderID = tenderId;
                            TenderId = tenderId;
                        }
                        else
                        {
                            Response.Redirect("~/");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }
                    DataSet ds = new TenderMasterBAL().TenderMaster_SelectByTenderID(objBo);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            this.dtlstTenderDetails.DataSource = ds.Tables[0];
                            this.dtlstTenderDetails.DataBind();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "TenderDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "TenderDetails").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void dtlstTenderDetails_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.SelectedItem)
            {
                HtmlGenericControl dvDocument = (HtmlGenericControl)e.Item.FindControl("dvDocument");
                HtmlGenericControl dvDocumentDate = (HtmlGenericControl)e.Item.FindControl("dvDocumentDate");

                TenderMasterBO objBos = new TenderMasterBO();
                objBos.TenderID = TenderId;
                DataTable dsTest = new TenderMasterBAL().TenderMaster_SelectByTenderID(objBos).Tables[0];

                DataRow drRow = dsTest.Rows[0];

                if(drRow["LastDate"] == DBNull.Value && drRow["OpeningDate"] == DBNull.Value && drRow["PBMeetingDate"] == DBNull.Value)
                {
                    dvDocumentDate.Visible = false;
                }
                else
                {
                    dvDocumentDate.Visible = true;
                }


                Repeater dl = (Repeater)e.Item.FindControl("dtlstDocument");
                TenderMasterBO objBo = new TenderMasterBO();
                objBo.TenderID = TenderId;
                DataSet ds = new TenderMasterBAL().Documents_SelectByTenderID(objBo);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dvDocument.Visible = true;
                        dl.DataSource = ds.Tables[0];
                        dl.DataBind();
                    }
                    else
                    {
                        dvDocument.Visible = false;
                    }
                }
                else
                {
                    dvDocument.Visible = false;
                }

            }
        }

        private string GetTenderList()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strTenderList = new StringBuilder();
            using (IMenuMasterRepository objMenu = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataList = objMenu.GetAllTenderMaster().OrderByDescending(x=> x.Tender_level_id).Take(5).ToList();
                
                foreach (var row in dataList)
                {
                    string strBlogName = "";
                    if (row.Title.Length > 40)
                    {
                        strBlogName = row.Title.Remove(40) + "..";
                    }
                    else
                    {
                        strBlogName = row.Title;
                    }
                    DateTime dt;

                    if(DateTime.TryParse(row.PublishDate,out dt))
                    {
                    strTenderList.Append("<li >");
                    strTenderList.Append("<div class='post-info'>");
                    strTenderList.Append("<h4>");
                    strTenderList.Append("<a href = '"+ ResolveUrl("~/TenderDetails?" + Unmehta.WebPortal.Web.Common.Functions.Base64Encode(row.TenderID.ToString())) + "' > "+ strBlogName + "</a>");
                    strTenderList.Append("</h4>");
                    strTenderList.Append("<p>"+ dt.ToString("dd MMM yyyy") + "</p>");
                    strTenderList.Append("</div>");
                    strTenderList.Append("</li>");
                    }
                }
            }
            return strTenderList.ToString();
        }
    }
}