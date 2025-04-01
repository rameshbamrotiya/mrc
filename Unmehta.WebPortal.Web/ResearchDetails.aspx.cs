using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class ResearchDetails : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strRightsSideTabs;
        public static string strYearTabs;
        public static string strPageName;
        public static string strListOfSubSectionDescription;
        public static int osid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                try
                {
                    string[] strData = queryString.Split('|');
                    queryString = strData[0];
                    strPageName = strData[1];
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Research");
                }
                osid = Convert.ToInt32(queryString.ToString());
                long OSid;
                if (string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out OSid))
                {
                    Response.Redirect("~/Research");
                }
                BindDropDownList();

                strHeaderImage = GetHeaderImage();

                strRightsSideTabs = GetListOfSubSectionTab();

                strListOfSubSectionDescription = GetListOfYearTabs();
            }
        }

        private void BindDropDownList()
        {
            ArticlesMasterBO objBo = new ArticlesMasterBO();
            int languageId = Functions.LanguageId;
            objBo.LanguageId = languageId;
            ArticlesMasterBAL objBal = new ArticlesMasterBAL();
            ddlArticalType.Items.Clear();
            ddlArticalType.DataSource = objBal.SelectRecordArticleType(objBo);
            ddlArticalType.DataTextField = "Name";
            ddlArticalType.DataValueField = "Name";
            ddlArticalType.DataBind();
            ddlArticalType.Items.Insert(0, new ListItem("-- Select ArticalType --", "-1"));
        }

        private string GetListOfSubSectionTab(string strTitle = "", string strAuthor = "", string strArticalType = "")
        {
            //int languageId = Functions.LanguageId;
            StringBuilder strResearchtab = new StringBuilder();
            int languageId = 1;
            //StringBuilder strResearch = new StringBuilder();
            DataSet ds = new DataSet();

            //string rowURl = Request.RawUrl.ToString();
            //rowURl = rowURl.Substring(1);
            //rowURl = rowURl.Split(new[] {'?'})[0];
            //bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            //if (IsDisabledTranslate)
            //{
            //    if (Functions.LanguageId == 1)
            //    {
            //        ds = new ArticleDepartmentBAL().SelectResearchDetailspublicationname(languageId);
            //    }
            //    Functions.GetReloadPage(this.Page, ref strResearch); // 10 sec page reload with english content logic

            //}
            //else
            //{



            ds = new ArticleDepartmentBAL().SelectResearchDetailspublicationname(languageId); // load english content and translate it.
            //}
            if (ds != null)
            {
                if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                {
                    int i = 1, tabIndex = 1;
                    int serchActive = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        bool isHaveDetails;
                        string strDetails = GetSubDetails(Convert.ToInt32(row["Id"].ToString()), ref tabIndex, out isHaveDetails, strTitle, strAuthor, strArticalType);

                        //if (!string.IsNullOrWhiteSpace(strTitle) || !string.IsNullOrWhiteSpace(strAuthor) || !string.IsNullOrWhiteSpace(strArticalType))
                        //{                                
                            strResearchtab.Append("<li><a href='#tab_" + i + "' class='"+ (serchActive == 1 && isHaveDetails ? "active" : "") + "'  data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");

                            if (isHaveDetails)
                            {
                                serchActive++;
                            }

                        //}
                        //else
                        //{
                        //    if (i == 1)
                        //    {
                        //        strResearchtab.Append("<li><a href='#tab_" + i + "' class='active' data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");
                        //    }
                        //    else
                        //    {
                        //        strResearchtab.Append("<li><a href='#tab_" + i + "' data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");
                        //    }
                        //}

                        i++;
                    }

                    if(ds.Tables[0].Rows.Count==i-1 && serchActive==1)
                    {
                        strResearchtab = new StringBuilder();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {

                            bool isHaveDetails;
                            string strDetails = GetSubDetails(Convert.ToInt32(row["Id"].ToString()), ref tabIndex, out isHaveDetails, strTitle, strAuthor, strArticalType);

                            //if (!string.IsNullOrWhiteSpace(strTitle) || !string.IsNullOrWhiteSpace(strAuthor) || !string.IsNullOrWhiteSpace(strArticalType))
                            //{                                
                            strResearchtab.Append("<li><a href='#tab_" + i + "' class='" + (serchActive == 1 ? "active" : "") + "'  data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");
                            
                            {
                                serchActive++;
                            }
                            //}
                            //else
                            //{
                            //    if (i == 1)
                            //    {
                            //        strResearchtab.Append("<li><a href='#tab_" + i + "' class='active' data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");
                            //    }
                            //    else
                            //    {
                            //        strResearchtab.Append("<li><a href='#tab_" + i + "' data-toggle='pill'>" + row["Publication_Name"] + "</a></li>");
                            //    }
                            //}

                            i++;
                        }
                    }
                }
            }
            return strResearchtab.ToString();
        }

        private string GetListOfYearTabs(string strTitle = "", string strAuthor = "", string strArticalType = "")
        {
            //int languageId = Functions.LanguageId;
            int languageId = 1, tabIndex = 1;
            StringBuilder strResearchtab = new StringBuilder();
            using (IHomePageRepository objCandidateDetailsRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {

                List<GetAllReasearchResult> dataListReasearch = objCandidateDetailsRepository.GetAllReasearch(languageId);

                //List<GetAllPublicationMasterByLanguageIdResult> dataListPublicationName = objCandidateDetailsRepository.GetAllPublicationMasterByLanguageId(languageId);

                DataSet ds = new DataSet();

                string rowURl = Request.RawUrl.ToString();
                rowURl = rowURl.Substring(1);
                rowURl = rowURl.Split(new[] { '?' })[0];

                bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
                if (IsDisabledTranslate)
                {
                    if (Functions.LanguageId == 1)
                    {
                        ds = new ArticleDepartmentBAL().SelectResearchDetailspublicationname(languageId);
                    }
                    Functions.GetReloadPage(this.Page, ref strResearchtab); // 10 sec page reload with english content logic
                }
                else
                {
                    ds = new ArticleDepartmentBAL().SelectResearchDetailspublicationname(languageId); // load english content and translate it.
                }
                //if(dataListPublicationName.Count()>0)
                //{
                //    int count = 1;
                //    foreach (var rowPublicationName in dataListPublicationName)
                //    {
                //        bool isHaveDetails;
                //        string strDetails = GetSubDetails(rowPublicationName.Id, ref tabIndex, out isHaveDetails, strTitle, strAuthor, strArticalType);

                //        if (!string.IsNullOrWhiteSpace(strTitle) || !string.IsNullOrWhiteSpace(strAuthor) || !string.IsNullOrWhiteSpace(strArticalType))
                //        {
                //            strResearchtab.Append("<div class='tab-pane " + (count == 1 && isHaveDetails ? "active" : "") + "' id='tab_" + count + "'>");
                //            strResearchtab.Append("        <h4 class='widget-title'>" + rowPublicationName.Publication_Name + " </h4>");
                //            strResearchtab.Append(strDetails);
                //            strResearchtab.Append("</div>");
                //            count++;
                //        }
                //        else
                //        {
                //            strResearchtab.Append("<div class='tab-pane " + (count == 1 ? "active" : "") + "' id='tab_" + count + "'>");
                //            strResearchtab.Append("        <h4 class='widget-title'>" + rowPublicationName.Publication_Name + " </h4>");
                //            strResearchtab.Append(strDetails);
                //            strResearchtab.Append("</div>");
                //            count++;
                //        }

                //    }
                //}

                if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        int i = 1, serchActive = 1;

                        string strActive = "active";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            bool isHaveDetails;
                            string strDetails = GetSubDetails(Convert.ToInt32(row["Id"].ToString()), ref tabIndex, out isHaveDetails, strTitle, strAuthor, strArticalType);

                            
                            {
                                strResearchtab.Append("<div class='tab-pane " + (serchActive == 1 && isHaveDetails ? "active" : "") + "' id='tab_" + i + "'>");
                                strResearchtab.Append("        <h4 class='widget-title'>" + row["Publication_Name"] + " </h4>");
                                strResearchtab.Append(strDetails);
                                strResearchtab.Append("</div>");

                                if (isHaveDetails)
                                {
                                    serchActive++;
                                }
                            }

                            i++;
                        }
                        if (ds.Tables[0].Rows.Count == i-1 && serchActive == 1)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                bool isHaveDetails;
                                string strDetails = GetSubDetails(Convert.ToInt32(row["Id"].ToString()), ref tabIndex, out isHaveDetails, strTitle, strAuthor, strArticalType);


                                {
                                    strResearchtab.Append("<div class='tab-pane " + (serchActive == 1 ? "active" : "") + "' id='tab_" + i + "'>");
                                    strResearchtab.Append("        <h4 class='widget-title'>" + row["Publication_Name"] + " </h4>");
                                    strResearchtab.Append(strDetails);
                                    strResearchtab.Append("</div>");
                                    
                                        serchActive++;
                                    
                                }

                                i++;
                            }
                        }
                    }
                }
            }
            return strResearchtab.ToString();
        }

        private string GetSubDetails(int PublicationId, ref int tabIndex, out bool isHaveDetails, string strTitle = "", string strAuthor = "", string strArticalType = "")
        {
            isHaveDetails = true;
            //int languageId = Functions.LanguageId;
            int languageId = 1;
            DataSet ds = new DataSet();
            StringBuilder strResearchtab = new StringBuilder();
            StringBuilder strResearchSubtab = new StringBuilder();

            strResearchtab.Append("<ul class='nav nav-tabs nav-tabs-solid'>");

            strResearchSubtab.Append("<div class='tab-content'>");
            string rowURl = Request.RawUrl.ToString();
            rowURl = rowURl.Substring(1);
            rowURl = rowURl.Split(new[] { '?' })[0];
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            if (IsDisabledTranslate)
            {
                if (Functions.LanguageId == 1)
                {
                    ds = new ArticleDepartmentBAL().SelectResearchDetails(osid, languageId);
                }
                Functions.GetReloadPage(this.Page, ref strResearchtab); // 10 sec page reload with english content logic

            }
            else
            {
                ds = new ArticleDepartmentBAL().SelectResearchDetails(osid, languageId); // load english content and translate it.
            }
            if (ds != null)
            {
                DataTable dtYear = ds.Tables[0];
                List<long> yearList = new List<long>();
                if (!string.IsNullOrWhiteSpace(strTitle) || !string.IsNullOrWhiteSpace(strAuthor) || !string.IsNullOrWhiteSpace(strArticalType))
                {
                    yearList = (from myRow in dtYear.AsEnumerable()
                                where myRow.Field<int>("Publication_id") == PublicationId
                                && (
                                (!string.IsNullOrWhiteSpace(strTitle) && myRow.Field<string>("Articles_Name").Contains(strTitle))
                                ||
                                (!string.IsNullOrWhiteSpace(strAuthor) && myRow.Field<string>("Author").Contains(strAuthor))
                                ||
                                (!string.IsNullOrWhiteSpace(strArticalType) && myRow.Field<string>("ArticleType")==(strArticalType))
                                )
                                select Convert.ToInt64(myRow.Field<string>("Publication_Year"))).ToList().Distinct().OrderByDescending(x => x).ToList();
                }
                else
                {

                    yearList = (from myRow in dtYear.AsEnumerable()
                                where myRow.Field<int>("Publication_id") == PublicationId
                                select Convert.ToInt64(myRow.Field<string>("Publication_Year"))).ToList().Distinct().OrderByDescending(x => x).ToList();
                }

                if (yearList.Count() > 0)
                {
                    int i = 1;
                    foreach (var rowYear in yearList)
                    {
                        strResearchtab.Append("    <li class='nav-item'>");
                        strResearchtab.Append("        <a class='nav-link " + (i == 1 ? "active" : "") + "' href='#solid-tab" + tabIndex + "' data-toggle='tab'>" + rowYear + "</a>");
                        strResearchtab.Append("    </li>");


                        strResearchSubtab.Append("    <div class='tab-pane " + (i == 1 ? "active" : "") + "' id='solid-tab" + tabIndex + "'>");


                        strResearchSubtab.Append("      <div class='main-part'>");
                        strResearchSubtab.Append("        <div class='accordion-box'>");
                        strResearchSubtab.Append("            <div class='title-box'>");
                        strResearchSubtab.Append("                <h6>" + rowYear + "</h6>");
                        strResearchSubtab.Append("            </div>");
                        strResearchSubtab.Append("            <ul class='accordion-inner'>");

                        List<DataRow> rows = new List<DataRow>();

                        if (!string.IsNullOrWhiteSpace(strTitle) || !string.IsNullOrWhiteSpace(strAuthor) || !string.IsNullOrWhiteSpace(strArticalType))
                        {
                            rows = new List<DataRow>();

                            if(!string.IsNullOrWhiteSpace(strTitle))
                            {
                                rows.AddRange((from myRow in dtYear.AsEnumerable()
                                        where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                         myRow.Field<int>("Publication_id") == PublicationId
                                         &&  myRow.Field<string>("Articles_Name").Contains(strTitle)
                                        select myRow).ToList().Distinct().ToList());
                            }
                            if (!string.IsNullOrWhiteSpace(strAuthor))
                            {
                                if (rows.Count() > 0)
                                {
                                    rows=((from myRow in dtYear.AsEnumerable()
                                                   where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                                    myRow.Field<int>("Publication_id") == PublicationId
                                                    && myRow.Field<string>("Author").Contains(strAuthor)
                                                   select myRow).ToList().Distinct().ToList());
                                }
                                else
                                {
                                    rows.AddRange((from myRow in dtYear.AsEnumerable()
                                             where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                              myRow.Field<int>("Publication_id") == PublicationId
                                              && myRow.Field<string>("Author").Contains(strAuthor)
                                             select myRow).ToList().Distinct().ToList());
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(strArticalType))
                            {
                                if (rows.Count() > 0)
                                {
                                    rows = ((from myRow in dtYear.AsEnumerable()
                                                   where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                                    myRow.Field<int>("Publication_id") == PublicationId
                                                    && myRow.Field<string>("ArticleType") == (strArticalType)
                                                   select myRow).ToList().Distinct().ToList());
                                }
                                else
                                {
                                    rows.AddRange((from myRow in dtYear.AsEnumerable()
                                                   where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                                    myRow.Field<int>("Publication_id") == PublicationId
                                                    && myRow.Field<string>("ArticleType") == (strArticalType)
                                                   select myRow).ToList().Distinct().ToList());
                                }
                            }

                            //rows = (from myRow in dtYear.AsEnumerable()
                            //        where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                            //         myRow.Field<int>("Publication_id") == PublicationId
                            //         && (
                            //   (!string.IsNullOrWhiteSpace(strTitle) && myRow.Field<string>("Articles_Name").Contains(strTitle))
                            //   ||
                            //   (!string.IsNullOrWhiteSpace(strAuthor) && myRow.Field<string>("Author").Contains(strAuthor))
                            //   ||
                            //   (!string.IsNullOrWhiteSpace(strArticalType) && myRow.Field<string>("ArticleType")==(strArticalType))
                            //   )
                            //        select myRow).ToList().Distinct().ToList();

                        }
                        else
                        {
                            rows = (from myRow in dtYear.AsEnumerable()
                                    where Convert.ToInt64(myRow.Field<string>("Publication_Year")) == rowYear &&
                                     myRow.Field<int>("Publication_id") == PublicationId

                                    select myRow).ToList().Distinct().ToList();
                        }
                        int j = 1;
                        foreach (DataRow row in rows)
                        {

                            strResearchSubtab.Append("<li class='accordion block'>");
                            strResearchSubtab.Append("<div class='acc-btn'>");
                            strResearchSubtab.Append("<a href='" + row["Web_link"].ToString() + "' target='_blank'><div class='icon-outer_link'> Full View</div></a>");
                            strResearchSubtab.Append("<div class='icon-outer'></div>");
                            strResearchSubtab.Append("<h6>" + row["Articles_Name"] + "</h6>");
                            strResearchSubtab.Append("<label>Author Name :- </label> <small>" + row["Author"] + "</small>");
                            strResearchSubtab.Append("</div>");
                            strResearchSubtab.Append("<div class='acc-content'>");

                            strResearchSubtab.Append(HttpUtility.HtmlDecode(row["Description"].ToString()));

                            strResearchSubtab.Append("    </div>");
                            strResearchSubtab.Append("</li>");


                        }
                        strResearchSubtab.Append("             </ul>");
                        strResearchSubtab.Append("          </div>");
                        strResearchSubtab.Append("      </div>");

                        strResearchSubtab.Append("    </div>");
                        tabIndex++;
                        i++;
                    }
                }
                else
                {
                    isHaveDetails = false;
                }
            }
            else
            {
                isHaveDetails = false;
            }
            strResearchtab.Append("</ul>");

            strResearchSubtab.Append("</div>");


            return strResearchtab.ToString() + "" + strResearchSubtab.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ResearchDetails").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ResearchDetails").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void txtTitle_TextChanged(object sender, EventArgs e)
        {
            string strTitle = txtTitle.Text;
            string strAuthor = txtAuthor.Text;
            string strArticalType = "";

            if (ddlArticalType.SelectedIndex > 0)
            {
                strArticalType = ddlArticalType.SelectedValue;
            }
            strRightsSideTabs = GetListOfSubSectionTab(strTitle, strAuthor, strArticalType);
            strListOfSubSectionDescription = GetListOfYearTabs(strTitle, strAuthor, strArticalType);
        }

    }
}