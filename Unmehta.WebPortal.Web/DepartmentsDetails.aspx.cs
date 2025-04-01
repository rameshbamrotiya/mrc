using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Admin.Hospital;
using Unmehta.WebPortal.Repository.Repository.Faculty;
using BAL;
using System.Data;
using Unmehta.WebPortal.Model.Model.Hospital;
using System.Web.Services;

namespace Unmehta.WebPortal.Web
{
    public class FacultyListPopup
    {
        public long FacultyId { get; set; }
        public long PopupCount { get; set; }
        public string PopupName { get; set; }
    }
    public partial class DepartmentsDetails : System.Web.UI.Page
    {
        public static List<FacultyListPopup> lstFacultyPopUp;

        public static string strHeaderImage;

        public static string strPopupDetails;

        public static string strDepartmentDetails;

        public static string strDepartmentTab;

        public static string strScript, strBackURL;

        public static string strFacultyTab;

        public static long lgPageId;

        public static long lgPageRecCount;

        public static long lgDepartmentId;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                lgPageRecCount = 5;
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

                if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out lgPageId))
                {
                    strPopupDetails = "";
                    strScript = "";
                    strBackURL = "";
                    strFacultyTab = "";
                    strDepartmentTab = "";
                    strDepartmentDetails = "";

                    dvExternalVieoLink.Src ="";
                    //imgAdsImage.Src = ResolveUrl(deptData.AddImage);
                    liDeptName.InnerText = "";

                    GetData(lgPageId);
                    dvRightSideMenu.Visible = true;
                }
                else
                {
                    strPopupDetails = "";
                    strScript = "";
                    strBackURL = "";
                    strFacultyTab = "";
                    strDepartmentTab = "";
                    strDepartmentDetails = "";

                    dvExternalVieoLink.Src = "";
                    //imgAdsImage.Src = ResolveUrl(deptData.AddImage);
                    liDeptName.InnerText = "";

                    GetOtherDepartment();
                    dvRightSideMenu.Visible = false;
                }

            }

        }

        private void GetOtherDepartment()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strTabDetails = new StringBuilder();
            using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                List<OurExcellenceMasterGridModel> dataMain = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(languageId).Where(x => x.IsVisible == true).OrderBy(x => x.SequanceNo).ToList();
                var otherDepartment = dataMain.Where(x => x.IsAddInOtherDepartment == true).OrderBy(x => x.SequanceNo).ToList();

                strTabDetails.Append("");
                strTabDetails.Append("<!-- About Section -->");
                strTabDetails.Append("<div class='content'>");
                strTabDetails.Append("	<div class='container'>");
                strTabDetails.Append("		<div class='row'>");
                strTabDetails.Append("			<!-- Doctor Details Tab -->");
                strTabDetails.Append("			<div class='col-lg-12'>");
                strTabDetails.Append("				<div class='row'>");
                strTabDetails.Append("					<div class='col-sm-12 col-md-12 col-lg-12' id='accordion'>");
                int index = 1;
                foreach (var row in otherDepartment)
                {
                    int FacutyId = 1;
                    var FacultyList = objFacultyRepository.GetAllTblFaculty((int)languageId).Where(x => x.DepartmentId == row.Id).OrderBy(x=> x.sequenceNo).ToList();
                    if (FacultyList.Count() > 0)
                    {
                        strTabDetails.Append("						<div class='accordion-item opened'>");
                        strTabDetails.Append("							<div class='accordion__header collapsed' data-toggle='collapse' data-target='#collapse" + index + "'");
                        strTabDetails.Append("								aria-expanded='false'>");
                        strTabDetails.Append("								<a class='accordion__title' href='#'>" + row.DepartmentName + "</a>");
                        strTabDetails.Append("							</div><!-- /.accordion-item-header -->");
                        strTabDetails.Append("							<div id='collapse" + index + "' class='collapse show' data-parent='#accordion'>");
                        strTabDetails.Append("								<div class='accordion__body'>");
                        strTabDetails.Append("									<div class='table-responsive'>");
                        strTabDetails.Append("										<table class='table table-hover table-center mb-0 maintable'>");
                        strTabDetails.Append("											<thead>");
                        strTabDetails.Append("												<tr>");
                        strTabDetails.Append("													<th>Sr</th>");
                        strTabDetails.Append("													<th>Name of Faculty</th>");
                        strTabDetails.Append("													<th>Designation</th>");
                        strTabDetails.Append("													<th>Photo</th>");
                        strTabDetails.Append("												</tr>");
                        strTabDetails.Append("											</thead>");
                        strTabDetails.Append("											<tbody>");

                        foreach (var department in FacultyList)
                        {
                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(department.ImagePath) ? "" : department.ImagePath);

                            strTabDetails.Append("												<tr>");
                            strTabDetails.Append("													<td>" + FacutyId + "</td>");
                            strTabDetails.Append("													<td>" + department.FacultyName + "</td>");
                            strTabDetails.Append("													<td>" + department.DesignationName + "</td>");
                            strTabDetails.Append("													<td><img src='" + strimagePath + "' class='img-fluid' alt='User Image'></td>");
                            strTabDetails.Append("												</tr>");
                            FacutyId++;
                        }
                        strTabDetails.Append("											</tbody>");
                        strTabDetails.Append("										</table>");
                        strTabDetails.Append("									</div>");
                        strTabDetails.Append("								</div><!-- /.accordion-item-body -->");
                        strTabDetails.Append("							</div>");
                        strTabDetails.Append("						</div><!-- /.accordion-item -->");
                    }
                    index++;
                }
                strTabDetails.Append("					</div>");
                strTabDetails.Append("				</div>");
                strTabDetails.Append("			</div>");
                strTabDetails.Append("			<!-- /Doctor Details Tab -->");
                strTabDetails.Append("		</div>");
                strTabDetails.Append("	</div>");
                strTabDetails.Append("</div>");
                strTabDetails.Append("<!-- End About Section -->");
            }
            strDepartmentDetails = strTabDetails.ToString();
        }

        private void GetData(long id)
        {
            int languageId = Functions.LanguageId;

            long lgChartCount = 1, lgPopupCount = 1;
            StringBuilder strTabDetails = new StringBuilder();
            StringBuilder strPopUpbuilder = new StringBuilder();
            StringBuilder strTabContent = new StringBuilder();
            StringBuilder strScriptData = new StringBuilder();
            using (IDepartmentTabRepository objBlogCategoryMasterRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {

                var tabList = objBlogCategoryMasterRepository.GetAllDepartmentTabList(id, languageId);

                if (tabList != null)
                {
                    LableData:
                    if (tabList.Count() > 0)
                    {
                        var deptData = objBlogCategoryMasterRepository.GetAllDepartmentMaster(languageId).Where(x => x.Id == id).FirstOrDefault();

                        if (deptData != null)
                        {
                            dvExternalVieoLink.Src = deptData.ExternalVideoLink;
                            //imgAdsImage.Src = ResolveUrl(deptData.AddImage);
                            liDeptName.InnerText = deptData.DepartmentName;
                            strBackURL = "";
                            string imge = "";
                            if(!string.IsNullOrWhiteSpace(deptData.SideImageURL))
                            {
                                strBackURL = "";
                                strBackURL += "<a href='" + ResolveUrl(deptData.SideImageURL) + "' target='_blank'>     ";
                                if(!string.IsNullOrWhiteSpace(deptData.AddImage))
                                {
                                    imge = "    <img id='imgAdsImage' runat='server' class='img-fluid' src='" + ResolveUrl(deptData.AddImage) + "' />";

                                }
                                else
                                {

                                }
                                strBackURL += imge+ "   </a>";
                            }
                            else
                            {
                                strBackURL = "";
                                if (!string.IsNullOrWhiteSpace(deptData.AddImage))
                                {
                                    imge = "    <img id='imgAdsImage' runat='server' class='img-fluid' src='" + ResolveUrl(deptData.AddImage) + "' />";

                                }
                                else
                                {

                                }
                                strBackURL = imge;

                            }
                        }

                        strTabDetails.Append("");

                        strTabDetails.Append("<nav class='user-tabs mb-4'>");
                        strTabDetails.Append("	<ul class='nav nav-tabs nav-tabs-bottom'>");

                        strTabContent.Append("<div class=\"tab-content\">");

                        long index = 0;
                        foreach (var row in tabList.Where(x => x.ParentTabId == 0 && x.IsVisable == true))
                        {

                            strTabDetails.Append("");

                            strTabDetails.Append("<li class='nav-item'>");
                            strTabDetails.Append("	<a class='nav-link " + (index == 0 ? "active" : "") + "' href='#dap_" + index + "' data-toggle='tab'>" + row.TabName + "</a>");
                            strTabDetails.Append("</li>");

                            strTabContent.Append("");
                            strTabContent.Append("<div role='tabpanel' id='dap_" + index + "' class='tab-pane fade " + (index == 0 ? "active show" : "") + "'>");
                            if (row.TabTypeId == 0 || row.TabTypeId == null)
                            {
                                strTabContent.Append(GetTabDetails(row.Id, row.TabName, false, languageId, ref strPopUpbuilder, ref strScriptData, ref lgChartCount, ref lgPopupCount));
                            }
                            else
                            {
                                strTabContent.Append(GetSubTabs(row.Id, row.TabName, true, languageId, ref strPopUpbuilder, ref strScriptData, ref lgChartCount, ref lgPopupCount, ref index));
                            }

                            strTabContent.Append("</div>");


                            strPopUpbuilder.Append(strPopUpbuilder.ToString());
                            index++;
                        }
                        //if ((bool)deptData.IsFacility)
                        if ((deptData.IsFacility == null ? true : (bool)deptData.IsFacility))
                        {
                            strTabDetails.Append("<li class='nav-item'>");
                            strTabDetails.Append("	<a class='nav-link " + (index == 0 ? "active" : "") + "' href='#dap_" + index + "' data-toggle='tab'>Faculty</a>");
                            strTabDetails.Append("</li>");
                            strFacultyTab = "dap_" + index;
                            lgDepartmentId = (long)deptData.DepartmentId;
                            strTabContent.Append(GetFacultyDetailById("dap_" + index + "", deptData.DepartmentId, deptData.DepartmentName, languageId, 1, ref strPopUpbuilder, ref lgPopupCount));
                        }
                        else
                        {
                            strFacultyTab = "";
                        }
                        strTabDetails.Append("  </ul>");

                        strTabContent.Append("</div>");

                        strTabContent.Append("</nav>");

                        strPopupDetails = strPopUpbuilder.ToString();

                        strScript = strScriptData.ToString();

                        strDepartmentDetails = strTabDetails.ToString() + strTabContent.ToString();
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        tabList = objBlogCategoryMasterRepository.GetAllDepartmentTabList(id, languageId);
                            goto LableData;
                        }
                    }
                }
            }
        }

        private static List<ListItem> PopulatePager(int recordCount, int currentPage, int PageSize)
        {
            double dblPageCount = (double)((decimal)recordCount / (decimal)lgPageRecCount);
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("<<", "1", currentPage > 1));
                if (currentPage != 1)
                {
                    pages.Add(new ListItem("Previous", (currentPage - 1).ToString()));
                }
                if (pageCount < 4)
                {
                    for (int i = 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else if (currentPage < 4)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                else if (currentPage > pageCount - 4)
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 2; i <= currentPage + 2; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Next", (currentPage + 1).ToString()));
                }
                pages.Add(new ListItem(">>", pageCount.ToString(), currentPage < pageCount));
            }
            return pages;
        }

        private string GetFacultyDetailById(string strTabHead, int? departmentId, string DepartmentName, long languageId, int pageIndex, ref StringBuilder strPopUpbuilde, ref long lgPopupCount)
        {
            lstFacultyPopUp = new List<FacultyListPopup>();
                        StringBuilder strTabContent = new StringBuilder();
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
            {

                var depa = objDepartmentRepository.GetAllDepartmentFront((int)languageId).Where(x => x.Id == departmentId).FirstOrDefault();
                if(depa!=null)
                {
                    if(depa.Id>0)
                    {
                        var FacultyList = objFacultyRepository.GetAllTblFaculty((int)languageId).Where(x => x.DepartmentId == depa.DeptId && x.IsVisible==true).OrderBy(x=> x.sequenceNo).ToList();

                        strTabContent.Append("<div role='tabpanel' id='" + strTabHead + "' class='tab-pane fade'>");
                        strTabContent.Append("	<div class='row'>");
                        strTabContent.Append("		<div class='col-md-12 col-lg-12'>");

                        foreach (var department in FacultyList)
                        {
                            #region PopUp Details

                            string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(department.ImagePath) ? "" : department.ImagePath);
                            string strimagePathicon = ResolveUrl("~/Hospital/assets/img/specialities/specialities-04.png");

                            ExtraDetailsBAL objBAL = new ExtraDetailsBAL();
                            DataSet ds = objBAL.GetFacultyExtraDetails(Convert.ToInt32(department.Id), Convert.ToInt32(department.LanguageId));
                            DataTable dtEducation = ds.Tables[0];
                            DataTable dtExperience = ds.Tables[1];
                            DataTable dtPublicationResearch = ds.Tables[2];
                            DataTable dtAwards = ds.Tables[3];
                            DataTable dtService = ds.Tables[4];

                            strPopUpbuilde.Append("");

                            strPopUpbuilde.Append("<!-- /Main Wrapper -->");
                            strPopUpbuilde.Append("<div class='modal fade' id='exampleModalLong" + lgPopupCount + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLong" + lgPopupCount + "'");
                            strPopUpbuilde.Append("	aria-hidden='true'>");
                            strPopUpbuilde.Append("	<div class='modal-dialog modal-lg' role='document'>");
                            strPopUpbuilde.Append("		<div class='modal-content'>");
                            strPopUpbuilde.Append("			<div class='modal-header'>");
                            strPopUpbuilde.Append("				<h5 class='modal-title' id='exampleModalLongTitle'>" + department.FacultyName + "</h5>");
                            strPopUpbuilde.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                            strPopUpbuilde.Append("					title=''><span aria-hidden='true'>×</span></button>");
                            strPopUpbuilde.Append("			</div>");
                            strPopUpbuilde.Append("			<div class='modal-body'>");
                            strPopUpbuilde.Append("				<div class='col-md-12 col-lg-12'>");
                            strPopUpbuilde.Append("					<div class='doc-info-left mb-15'>");
                            strPopUpbuilde.Append("						<div class='doctor-img'>");
                            strPopUpbuilde.Append("							<a href='#'>");
                            strPopUpbuilde.Append("								<img src='" + strimagePath + "' class='img-fluid'");
                            strPopUpbuilde.Append("									alt='User Image'>");
                            strPopUpbuilde.Append("							</a>");
                            strPopUpbuilde.Append("						</div>");
                            strPopUpbuilde.Append("						<div class='doc-info-cont'>");
                            strPopUpbuilde.Append("							<h4 class='doc-name'><a href='#'>" + department.FacultyName + "</a></h4>");
                            strPopUpbuilde.Append("							<p class='doc-speciality'>" + DepartmentName +"</p>");
                            strPopUpbuilde.Append("							<h5 class='doc-department'><img src='" + strimagePathicon + "' class='img-fluid'");
                            strPopUpbuilde.Append("									alt='Speciality'>" + DepartmentName + "</h5>");
                            strPopUpbuilde.Append("							<p class='doc-department'>" + (string.IsNullOrWhiteSpace(department.Email) ? "<i class='fas fa-envelope mr-15'></i> - </p>" : " <i class='fas fa-envelope mr-15'></i> <a href = '#'> " + department.Email + " </a> ") + "</p>");
                            strPopUpbuilde.Append("							<p class='doc-department'>"+(string.IsNullOrWhiteSpace(department.MobileNumber)? "<i class='fas fa-phone mr-15'></i> - </p>": " <i class='fas fa-phone mr-15'></i> <a href = '#'> " + department.MobileNumber   + " </a> ") +"</p>");
                            strPopUpbuilde.Append("						</div>");
                            strPopUpbuilde.Append("					</div>");
                            strPopUpbuilde.Append("					<!-- About Details -->");
                            strPopUpbuilde.Append("					<div class='widget about-widget'>");
                            strPopUpbuilde.Append("						<h4 class='widget-title'>About Me</h4>");
                            strPopUpbuilde.Append("						" + department.FacultyDescription + "");
                            strPopUpbuilde.Append("					</div>");
                            strPopUpbuilde.Append("					<!-- /About Details -->");
                            if (dtEducation != null)
                            {
                                if (dtEducation.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Education Details -->");
                                    strPopUpbuilde.Append("					<div class='widget education-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Education</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtEducation.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["EducationName"].ToString() + "</a>");
                                        strPopUpbuilde.Append("											<div>" + row["DegreeName"].ToString() + "</div>");
                                        if(string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if(string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString()+"</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Education Details -->");
                                }
                            }

                            if (dtExperience != null)
                            {
                                if (dtExperience.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Experience Details -->");
                                    strPopUpbuilde.Append("					<div class='widget experience-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Work &amp; Experience</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtExperience.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["EmployerName"].ToString() + "</a>");
                                        if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if (string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + "</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Experience Details -->");
                                }
                            }

                            if (dtPublicationResearch != null)
                            {
                                if (dtPublicationResearch.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Publication Research Details -->");
                                    strPopUpbuilde.Append("					<div class='widget experience-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Research</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtPublicationResearch.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<a href='#' class='name'>" + row["Description"].ToString() + "</a>");
                                        if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()) && string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {

                                        }
                                        else if (string.IsNullOrWhiteSpace(row["ToYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + "</span>");
                                        }
                                        else if (string.IsNullOrWhiteSpace(row["FromYear"].ToString()))
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["ToYear"].ToString() + "</span>");
                                        }
                                        else
                                        {
                                            strPopUpbuilde.Append("											<span class='time'>" + row["FromYear"].ToString() + " - " + row["ToYear"].ToString() + "</span>");
                                        }
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Publication Research Details -->");
                                }
                            }

                            if (dtAwards != null)
                            {
                                if (dtAwards.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Awards Details -->");
                                    strPopUpbuilde.Append("					<div class='widget awards-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Awards</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtAwards.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<p class='exp-year'>" + row["Month"].ToString() + " - " + row["Year"].ToString() + "</p>");
                                        strPopUpbuilde.Append("											<h4 class='exp-title'>" + row["Title"].ToString() + "</h4>");
                                        strPopUpbuilde.Append("											" + HttpUtility.HtmlDecode(row["AwardsDescription"].ToString()) + "");
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Awards Details -->");
                                }
                            }

                            if (dtService != null)
                            {
                                if (dtService.Rows.Count > 0)
                                {
                                    strPopUpbuilde.Append("					<!-- Services List -->");
                                    strPopUpbuilde.Append("					<div class='widget awards-widget'>");
                                    strPopUpbuilde.Append("						<h4 class='widget-title'>Services</h4>");
                                    strPopUpbuilde.Append("						<div class='experience-box'>");
                                    strPopUpbuilde.Append("							<ul class='experience-list'>");
                                    foreach (DataRow row in dtService.Rows)
                                    {
                                        strPopUpbuilde.Append("								<li>");
                                        strPopUpbuilde.Append("									<div class='experience-user'>");
                                        strPopUpbuilde.Append("										<div class='before-circle'></div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("									<div class='experience-content'>");
                                        strPopUpbuilde.Append("										<div class='timeline-content'>");
                                        strPopUpbuilde.Append("											<h4 class='exp-title'>" + row["ServiceName"].ToString() + "</h4>");
                                        strPopUpbuilde.Append("										</div>");
                                        strPopUpbuilde.Append("									</div>");
                                        strPopUpbuilde.Append("								</li>");
                                    }
                                    strPopUpbuilde.Append("							</ul>");
                                    strPopUpbuilde.Append("						</div>");
                                    strPopUpbuilde.Append("					</div>");
                                    strPopUpbuilde.Append("					<!-- /Services List -->");
                                }
                            }


                            strPopUpbuilde.Append("				</div>");
                            strPopUpbuilde.Append("			</div>");
                            strPopUpbuilde.Append("		</div>");
                            strPopUpbuilde.Append("	</div>");
                            strPopUpbuilde.Append("</div>");
                            #endregion

                            FacultyListPopup objBo = new FacultyListPopup();
                            objBo.FacultyId = (long)department.Id;
                            objBo.PopupCount = lgPopupCount;
                            objBo.PopupName = "exampleModalLong" + lgPopupCount;
                            lstFacultyPopUp.Add(objBo);

                            lgPopupCount++;
                        }
                        #region Faculty
                        foreach (var department in FacultyList.Skip((pageIndex - 1) * (int)lgPageRecCount).Take((int)lgPageRecCount).ToList())
                        {
                            string strimagePath = string.IsNullOrWhiteSpace(department.ImagePath) ? "" : ResolveUrl(department.ImagePath.Replace("//","/"));

                            string strimagePathicon = ResolveUrl("~/Hospital/assets/img/specialities/specialities-04.png");


                            var popUpList = lstFacultyPopUp.Where(x => x.FacultyId == department.Id).FirstOrDefault();

                            strTabContent.Append("<div class='card'>");
                            strTabContent.Append("	<div class='card-body'>");
                            strTabContent.Append("		<div class='doctor-widget'>");
                            strTabContent.Append("			<div class='doc-info-left'>");
                            strTabContent.Append("				<div class='doctor-img'>");
                            strTabContent.Append("					<a href='#'>");
                            strTabContent.Append("						<img src='" + strimagePath + "' class='img-fluid' alt='User Image'>");
                            strTabContent.Append("					</a>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("				<div class='doc-info-cont'>");
                            strTabContent.Append("					<h4 class='doc-name'><a href='#'>" + department.FacultyName + "</a></h4>");
                            strTabContent.Append("					<p class='doc-speciality'>" + department.DesignationName + "");
                            //strTabContent.Append("						(" + department.DepartmentName + ")");
                            strTabContent.Append("						    </p>");
                            strTabContent.Append("					<h5 class='doc-department'><img src='" + strimagePathicon + "' class='img-fluid' alt='Speciality'>" + DepartmentName + "</h5>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("			</div>");
                            strTabContent.Append("			<div class='doc-info-right'>");
                            strTabContent.Append("				<div class='clinic-booking'>");
                            strTabContent.Append("					<a class='view-pro-btn' data-toggle='modal' data-target='#" + popUpList.PopupName + "' data-original-title=''>View Profile</a>");
                            //strTabContent.Append("					<a class='apt-btn' href='#'>Book Appointment</a>");
                            strTabContent.Append("				</div>");
                            strTabContent.Append("			</div>");
                            strTabContent.Append("		</div>");
                            strTabContent.Append("	</div>");
                            strTabContent.Append("</div>");
                        }
                        #endregion

                        strTabContent.Append("          </div>");
                        strTabContent.Append("      </div>");

                        strTabContent.Append("<div class='row'>");
                        strTabContent.Append("	<div class='col-md-12'>");
                        strTabContent.Append("		<div class='blog-pagination'>");
                        strTabContent.Append("			<nav>");
                        strTabContent.Append("				<ul class='pagination justify-content-center'>");
                        var listPage = PopulatePager(FacultyList.Count(), pageIndex, (int)lgPageRecCount);
                        foreach (var row in listPage)
                        {
                            strTabContent.Append("					<li class='page-item'>");
                            strTabContent.Append("						<a class='page-link " + (Convert.ToBoolean(row.Enabled) ? "page_enabled" : "page_disabled") + "' href='#'  onclick='" + (!Convert.ToBoolean(row.Enabled) ? "  return false;" : " ChangePage(" + row.Value + "); ") + "'>" + row.Text + "</i></a>");
                            strTabContent.Append("					</li>");
                        }
                        strTabContent.Append("				</ul>");
                        strTabContent.Append("			</nav>");
                        strTabContent.Append("		</div>");
                        strTabContent.Append("	</div>");
                        strTabContent.Append("</div>");


                        strTabContent.Append("</div>");
                    }
                }

                return strTabContent.ToString();
            }
        }

        private static string GetFacultyDetail(string strTabHead, int? departmentId, string DepartmentName, long languageId, int pageIndex)
        {
            StringBuilder strTabContent = new StringBuilder();
            strTabContent.Append("	<div class='row'>");
            strTabContent.Append("		<div class='col-md-12 col-lg-12'>");
            using (IFacultyRepository objFacultyRepository = new FacultyRepository(Functions.strSqlConnectionString))
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {

                var depa = objDepartmentRepository.GetAllDepartmentFront((int)languageId).Where(x => x.Id == departmentId).FirstOrDefault();
                var FacultyList = objFacultyRepository.GetAllTblFaculty((int)languageId).Where(x => x.DepartmentId == depa.DeptId).OrderBy(x=> x.sequenceNo).ToList();
               
                #region Faculty
                foreach (var department in FacultyList.ToList().Skip((pageIndex - 1) * (int)lgPageRecCount).Take((int)lgPageRecCount))
                {
                    string strimagePath = Functions.ResolveUrl(string.IsNullOrWhiteSpace(department.ImagePath) ? "" : department.ImagePath);
                    string strimagePathicon = Functions.ResolveUrl("~/Hospital/assets/img/specialities/specialities-04.png");


                    var popUpList = lstFacultyPopUp.Where(x => x.FacultyId == department.Id).FirstOrDefault();

                    strTabContent.Append("<div class='card'>");
                    strTabContent.Append("	<div class='card-body'>");
                    strTabContent.Append("		<div class='doctor-widget'>");
                    strTabContent.Append("			<div class='doc-info-left'>");
                    strTabContent.Append("				<div class='doctor-img'>");
                    strTabContent.Append("					<a href='#'>");
                    strTabContent.Append("						<img src='" + strimagePath + "' class='img-fluid' alt='User Image'>");
                    strTabContent.Append("					</a>");
                    strTabContent.Append("				</div>");
                    strTabContent.Append("				<div class='doc-info-cont'>");
                    strTabContent.Append("					<h4 class='doc-name'><a href='#'>" + department.FacultyName + "</a></h4>");
                    strTabContent.Append("					<p class='doc-speciality'>" + HttpUtility.HtmlDecode(department.DesignationName) + "");
                    strTabContent.Append("						(" + department.DepartmentName + ")</p>");
                    strTabContent.Append("					<h5 class='doc-department'><img src='" + strimagePathicon + "' class='img-fluid' alt='Speciality'>" + DepartmentName + "</h5>");
                    strTabContent.Append("				</div>");
                    strTabContent.Append("			</div>");
                    strTabContent.Append("			<div class='doc-info-right'>");
                    strTabContent.Append("				<div class='clinic-booking'>");
                    if (popUpList != null)
                    {
                        strTabContent.Append("					<a class='view-pro-btn' data-toggle='modal' data-target='#" + popUpList.PopupName + "' data-original-title=''>View Profile</a>");
                    }
                    //strTabContent.Append("					<a class='apt-btn' href='#'>Book Appointment</a>");
                    strTabContent.Append("				</div>");
                    strTabContent.Append("			</div>");
                    strTabContent.Append("		</div>");
                    strTabContent.Append("	</div>");
                    strTabContent.Append("</div>");
                }
                #endregion

                strTabContent.Append("          </div>");
                strTabContent.Append("      </div>");

                strTabContent.Append("<div class='row'>");
                strTabContent.Append("	<div class='col-md-12'>");
                strTabContent.Append("		<div class='blog-pagination'>");
                strTabContent.Append("			<nav>");
                strTabContent.Append("				<ul class='pagination justify-content-center'>");
                var listPage = PopulatePager(FacultyList.Count(), pageIndex, (int)lgPageRecCount);
                foreach (var row in listPage)
                {
                    strTabContent.Append("					<li class='page-item '>");
                    strTabContent.Append("						<a class='page-link " + (Convert.ToBoolean(row.Enabled) ? "page_enabled" : "page_disabled") + "' href='#'  onclick='" + (!Convert.ToBoolean(row.Enabled) ? "  return false;" : " ChangePage(" + row.Value + "); ") + "'>" + row.Text + "</i></a>");
                    strTabContent.Append("					</li>");
                }
                strTabContent.Append("				</ul>");
                strTabContent.Append("			</nav>");
                strTabContent.Append("		</div>");
                strTabContent.Append("	</div>");
                strTabContent.Append("</div>");
                //strTabContent.Append("  <div class='row'>");
                //strTabContent.Append("      <div class='col-lg-12'>");
                //strTabContent.Append("          <div class='ltn__pagination-area text-center'>");
                //strTabContent.Append("              <div class='ltn__pagination'>");
                //strTabContent.Append("                  <ul>");
                //var listPage = PopulatePager(FacultyList.Count(), pageIndex, (int)lgPageRecCount);
                //foreach (var row in listPage)
                //{
                //    strTabContent.Append("                      <li><a href='#' class='" + (Convert.ToBoolean(row.Enabled) ? "page_enabled" : "page_disabled") + "' onclick='" + (!Convert.ToBoolean(row.Enabled) ? "  return false;" : " ChangePage(" + row.Value + "); ") + "' >" + row.Text + "</a></li>");
                //}
                //strTabContent.Append("                  </ul>");
                //strTabContent.Append("              </div>");
                //strTabContent.Append("          </div>");
                //strTabContent.Append("      </div>");
                //strTabContent.Append("  </div>");
            }

            return strTabContent.ToString();
        }

        [WebMethod]
        public static string GetDataDetails(int PageId)
        {
            using (IDepartmentTabRepository objBlogCategoryMasterRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                int languageId = Functions.LanguageId;
                StringBuilder strPopUpbuilde = new StringBuilder();
                long lgData = 0;
                var Data = objBlogCategoryMasterRepository.GetAllDepartmentMaster(languageId).Where(x => x.Id == lgDepartmentId).FirstOrDefault();
                if (Data != null)
                {
                    return GetFacultyDetail(strFacultyTab, Data.Id, Data.DepartmentName, languageId, PageId);

                }
                else
                {
                    return "";
                }
            }
        }

        private string GetSubTabs(long id, string tabName, bool isSubTab, int languageId, ref StringBuilder strPopUpbuilder, ref StringBuilder strScript, ref long lgChartCount, ref long lgPopupCount, ref long index)
        {
            StringBuilder strTabContent = new StringBuilder();
            StringBuilder strTabDetails = new StringBuilder();

            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {

                strTabContent.Append("<div class='nnntab'>");
                strTabContent.Append("    <div class='user-tabs mb-4'>");
                strTabContent.Append("    	<ul class='nav nav-tabs nav-tabs-bottom'>");
                var tabList = objDepartmentTabRepository.GetAllDepartmentTabList(lgPageId, languageId).Where(x => x.ParentTabId == id && x.IsVisable == true).OrderBy(x => x.SequanceNo);
                int subIndex = 0;
                foreach (var row in tabList)
                {
                    strTabContent.Append("<li class='nav-item'><a class='nav-link " + (subIndex == 0 ? "active" : "") + "' href='#tab" + index + "' data-toggle='tab'>" + row.TabName + "</a></li>");

                    strTabDetails.Append("          <div class='tab-pane " + (subIndex == 0 ? "active" : "") + "' id='tab" + index + "'>");
                    if (row.TabTypeId == 0)
                    {
                        strTabDetails.Append(GetTabDetails(row.Id, row.TabName, false, languageId, ref strPopUpbuilder, ref strScript, ref lgChartCount, ref lgPopupCount));
                    }
                    else
                    {
                        strTabDetails.Append(GetSubTabs(row.Id, row.TabName, true, languageId, ref strPopUpbuilder, ref strScript, ref lgChartCount, ref lgPopupCount, ref index));
                    }
                    strTabDetails.Append("          </div>");
                    subIndex++;
                    index++;
                }

                strTabContent.Append("    	</ul>");
                strTabContent.Append("    	<div class='tab-content'>");

                strTabContent.Append(strTabDetails.ToString());



                strTabContent.Append("      </div>");

                strTabContent.Append("    </div>");
                strTabContent.Append("</div>");
            }

            return strTabContent.ToString();
        }

        private string GetTabDetails(long id, string tabName, bool isSubTab, long languageId, ref StringBuilder strPopUpbuilder, ref StringBuilder strScript, ref long lgChartCount, ref long lgPopupCount)
        {
            StringBuilder strTabContent = new StringBuilder();
            using (IStatisticsChartRepository objPatientsEducationBrochureRepositorys = new StatisticsChartRepository(Functions.strSqlConnectionString))
            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                var dataTabDetails = objDepartmentTabRepository.GetAllDeparmentTabDetailListByTabId(id, languageId);

                var sequanceList = dataTabDetails.Where(x => x.ParentTabId != null && x.TabTypeId != null).GroupBy(x => x.TabTypeName).Select(x => new { SequanceNo = (long)x.FirstOrDefault().ParentTabId, Type = x.Key }).OrderBy(x => x.SequanceNo).ToList();

                foreach (var dataType in sequanceList)
                {
                    if (isSubTab)
                    {
                        strTabContent.Append("<div class=\"col-md-12 col-lg-12\">");
                    }
                    switch (dataType.Type)
                    {
                        case "CkEditor":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "CkEditor").FirstOrDefault();
                                if (SubDetails != null)
                                {
                                    strTabContent.Append("	<div class='row'>");
                                    strTabContent.Append("		<div class='col-md-12 col-lg-12'>");
                                    strTabContent.Append("			<!-- About Details -->");
                                    strTabContent.Append("			<div class='widget about-widget'>");
                                    strTabContent.Append("				" + HttpUtility.HtmlDecode(SubDetails.IntroductionDesc) + "");
                                    strTabContent.Append("			</div>");
                                    strTabContent.Append("			<!-- /About Details -->");
                                    strTabContent.Append("		</div>");
                                    strTabContent.Append("	</div>");
                                }
                                break;
                            }
                        case "UlDescriptionImageWithPopup":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "UlDescriptionImageWithPopup").OrderBy(x => x.SequanceNo).ToList();
                                if (SubDetails.Count() > 0)
                                {

                                    strTabContent.Append("<ul class='list-group list-group-flush format-list'>");
                                    foreach (var sub in SubDetails)
                                    {
                                        
                                        strPopUpbuilder.Append("");

                                        if (!string.IsNullOrWhiteSpace(sub.PopupDesc) || !string.IsNullOrWhiteSpace(sub.PopupImageName))
                                        {

                                            strTabContent.Append("		<li><span>" + HttpUtility.HtmlDecode(sub.PopupBasicShortDesc) + " </span><a href='#' data-toggle='modal' data-target='#exampleImageWithPopup" + lgPopupCount + "' data-original-title='' title='' style='color: red; text-decoration: underline;'>Read");
                                            strTabContent.Append("						More</a>");
                                            strTabContent.Append("			</li>");


                                            string strimagePath = "";

                                            if (!string.IsNullOrWhiteSpace(sub.PopupImageName))
                                            {
                                                strimagePath = (sub.PopupImageName);
                                                if (strimagePath.StartsWith("~/", StringComparison.Ordinal))
                                                {
                                                    strimagePath = strimagePath.Replace("~/", "");
                                                    strimagePath = ResolveUrl("~/" + strimagePath);
                                                }

                                            }

                                            strPopUpbuilder.Append("<div class='modal fade' id='exampleImageWithPopup" + lgPopupCount + "' tabindex='-1' role='dialog' aria-labelledby='exampleImageWithPopup" + lgPopupCount + "' aria-hidden='true'>");
                                            strPopUpbuilder.Append("	<div class='modal-dialog modal-dialog-centered' role='document'>");
                                            strPopUpbuilder.Append("		<div class='modal-content'>");
                                            strPopUpbuilder.Append("			<div class='modal-header'>");
                                            strPopUpbuilder.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title='' title=''><span aria-hidden='true'>×</span></button>");
                                            strPopUpbuilder.Append("			</div>");
                                            strPopUpbuilder.Append("			<div class='modal-body ' style='height: 80vh;overflow: auto; '>");
                                            strPopUpbuilder.Append("				<div class='row justify-content-center'>");
                                            if (!string.IsNullOrWhiteSpace(strimagePath))
                                            {
                                                strPopUpbuilder.Append("					<div class='col-lg-12 mb-25'>");
                                                strPopUpbuilder.Append("						<div class='about-author'>");
                                                strPopUpbuilder.Append("							<div class='author-img-wrap'>");
                                                strPopUpbuilder.Append("								<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                                                strPopUpbuilder.Append("							</div>");
                                                strPopUpbuilder.Append("						</div>");
                                                strPopUpbuilder.Append("					</div>");
                                            }
                                            strPopUpbuilder.Append("					<div class='col-lg-12'>");
                                            strPopUpbuilder.Append("						<div class='wpb_wrapper'>");
                                            strPopUpbuilder.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                            strPopUpbuilder.Append("						</div>");
                                            strPopUpbuilder.Append("					</div>");
                                            strPopUpbuilder.Append("				</div>");
                                            strPopUpbuilder.Append("			</div>");
                                            strPopUpbuilder.Append("		</div>");
                                            strPopUpbuilder.Append("	</div>");
                                            strPopUpbuilder.Append("</div>");
                                            lgPopupCount++;
                                        }
                                        else
                                        {
                                            strTabContent.Append("		<li><a href='#'>" + HttpUtility.HtmlDecode(sub.PopupBasicShortDesc) + "<span> </span></a>");
                                            strTabContent.Append("			</li>");

                                        }
                                    }

                                    strTabContent.Append("	</ul>");
                                }
                                break;
                            }
                        case "Statistics":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Statistics").ToList();
                                if (SubDetails.Count() > 0)
                                {
                                    foreach (var sub in SubDetails)
                                    {

                                        var chartname = objPatientsEducationBrochureRepositorys.GetStatisticsChartById((long)sub.StatasticId).ChartName;

                                        string strTable = "";
                                        string strChartJs = "\r\n" + Functions.GenerateScriptGraph((long)sub.StatasticId, "chartContainer" + lgChartCount + "", out strTable, true);

                                        strTabContent.Append(" <div class='state-body'>");
                                        strTabContent.Append(" <h4 class='widget-title'> " + chartname + "</h4>");
                                        strTabContent.Append("	<div class='table-responsive'>");
                                        strTabContent.Append(strTable);
                                        strTabContent.Append("	</div>");
                                        strTabContent.Append("	<br><br>");
                                        strTabContent.Append("	<div class=\"canvasbg\"></div> <div class=\"canvasbg_right\"></div> <div id='chartContainer" + lgChartCount + "' style='height: 400px;position: relative; width: 100%; overflow: auto;'></div>");
                                        strTabContent.Append("</div><br>");

                                        strScript.Append(strChartJs);

                                        lgChartCount++;
                                    }
                                }
                                break;
                            }
                        case "Slider":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Slider").ToList();
                                if (SubDetails.Count() > 0)
                                {
                                    strTabContent.Append("<div class='cathlab-slider owl-theme owl-carousel owl-loaded'>");
                                    foreach (var sub in SubDetails)
                                    {
                                        string strimagePath = ResolveUrl(string.IsNullOrWhiteSpace(sub.PopupImageName) ? "" : sub.PopupImageName);
                                        strTabContent.Append("		<div class='profile-widget'>");
                                        strTabContent.Append("			<div class='doc-img'>");
                                        strTabContent.Append("				<a href='#'>");
                                        strTabContent.Append("					<img class='img-fluid' alt='User Image' src='" + strimagePath + "'>");
                                        strTabContent.Append("				</a>");
                                        strTabContent.Append("			</div>");
                                        strTabContent.Append("		</div>");
                                    }
                                    strTabContent.Append("</div>");
                                }
                                break;
                            }
                        case "Accordion":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Accordion").ToList();
                                if (SubDetails.Count() > 0)
                                {
                                    strTabContent.Append("<div class=\"widget about-widget\">");


                                    strTabContent.Append("<div class='accordion-box'>");
                                    strTabContent.Append("	<div class='title-box'>");
                                    strTabContent.Append("		<h6>" + tabName + " are equipped with:</h6>");
                                    strTabContent.Append("	</div>");
                                    strTabContent.Append("	<ul class='accordion-inner'>");
                                    foreach (var sub in SubDetails)
                                    {
                                        string strimagePath = "";

                                        if (!string.IsNullOrWhiteSpace(sub.PopupImageName))
                                        {
                                            strimagePath = (sub.PopupImageName);
                                            if (strimagePath.StartsWith("~/", StringComparison.Ordinal))
                                            {
                                                strimagePath = strimagePath.Replace("~/", "");
                                                strimagePath = ResolveUrl("~/" + strimagePath);
                                            }

                                        }
                                        strTabContent.Append("<li class='accordion block'>");
                                        strTabContent.Append("	<div class='acc-btn'>");
                                        strTabContent.Append("		<div class='icon-outer'></div>");
                                        strTabContent.Append("		<h6>" + sub.PopupBasicShortDesc + "</h6>");
                                        strTabContent.Append("	</div>");
                                        strTabContent.Append("	<div class='acc-content'>");
                                        strTabContent.Append("		<div class='row'>");
                                        if (!string.IsNullOrWhiteSpace(strimagePath))
                                        {
                                            strTabContent.Append("			<div class='col-lg-3'>");
                                            strTabContent.Append("				<div class='about-author'>");
                                            strTabContent.Append("					<div class='author-img-wrap'>");
                                            strTabContent.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                                            strTabContent.Append("					</div>");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");

                                            strTabContent.Append("			<div class='col-lg-9'>");
                                            strTabContent.Append("				<div class='row'>");
                                            strTabContent.Append("					<div class='col-lg-12'>");
                                            strTabContent.Append("						<div class='wpb_wrapper'>");
                                            strTabContent.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                            strTabContent.Append("						</div>");
                                            strTabContent.Append("					</div>");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");
                                        }
                                        else
                                        {
                                            strTabContent.Append("			<div class='col-lg-12'>");
                                            strTabContent.Append("				<div class='row'>");
                                            strTabContent.Append("					<div class='col-lg-12'>");
                                            strTabContent.Append("						<div class='wpb_wrapper'>");
                                            strTabContent.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                            strTabContent.Append("						</div>");
                                            strTabContent.Append("					</div>");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");

                                        }
                                        strTabContent.Append("		</div>");
                                        strTabContent.Append("	</div>");
                                        strTabContent.Append("</li>");
                                    }

                                    strTabContent.Append("</ul>");
                                    strTabContent.Append("</div>");


                                    strTabContent.Append("</div>");
                                }
                                break;
                            }
                        case "ImageWithPopup":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "ImageWithPopup").ToList();
                                if (SubDetails.Count() > 0)
                                {
                                    strTabContent.Append("<div class='row'>");
                                    foreach (var sub in SubDetails)
                                    {
                                        string strimagePath = "";

                                        if (!string.IsNullOrWhiteSpace(sub.PopupImageName))
                                        {
                                            strimagePath = (sub.PopupImageName);
                                            if (strimagePath.StartsWith("~/", StringComparison.Ordinal))
                                            {
                                                strimagePath = strimagePath.Replace("~/", "");
                                                strimagePath = ResolveUrl("~/" + strimagePath);
                                            }

                                        }

                                        strTabContent.Append("	<div class='col-lg-3 col-md-6 col-12'>");
                                        strTabContent.Append("		<div class='departments-box-layout5'>");
                                        strTabContent.Append("			<div class='item-img'>");
                                        strTabContent.Append("				<img src='" + strimagePath + "' alt='department' class='img-fluid'>");
                                        strTabContent.Append("				<div class='item-content'>");
                                        strTabContent.Append("					<h3 class='item-title title-bar-primary3'><a href='#'>" + sub.IntroductionDesc + " </a></h3>");
                                        strTabContent.Append("					<p>" + sub.PopupBasicShortDesc);
                                        strTabContent.Append("					</p>");
                                        strTabContent.Append("					<a href='#' type='button' data-toggle='modal' data-target='#exampleModalImageWithPopup" + lgPopupCount + "' data-whatever='@mdo' data-original-title='' title='' class='item-btn'>DETAILS</a>");


                                        strPopUpbuilder.Append("");

                                        strPopUpbuilder.Append("<div class='modal fade' id='exampleModalImageWithPopup" + lgPopupCount + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalImageWithPopup" + lgPopupCount + "' aria-hidden='true'>");
                                        strPopUpbuilder.Append("	<div class='modal-dialog modal-dialog-centered' role='document'>");
                                        strPopUpbuilder.Append("		<div class='modal-content'>");
                                        strPopUpbuilder.Append("			<div class='modal-header'>");
                                        strPopUpbuilder.Append("			    <h3 class='item-title title-bar-primary3'><a href='#'>" + sub.IntroductionDesc + " </a></h3>");
                                        strPopUpbuilder.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title='' title=''><span aria-hidden='true'>×</span></button>");
                                        strPopUpbuilder.Append("			</div>");
                                        strPopUpbuilder.Append("			<div class='modal-body' style='height: 80vh;overflow: auto; '>");
                                        strPopUpbuilder.Append("				<div class='row justify-content-center'>");
                                        if (!string.IsNullOrWhiteSpace(strimagePath))
                                        {
                                            strPopUpbuilder.Append("					<div class='col-lg-12 mb-25'>");
                                            strPopUpbuilder.Append("						<div class='about-author'>");
                                            strPopUpbuilder.Append("							<div class='author-img-wrap'>");
                                            strPopUpbuilder.Append("								<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                                            strPopUpbuilder.Append("							</div>");
                                            strPopUpbuilder.Append("						</div>");
                                            strPopUpbuilder.Append("					</div>");
                                        }
                                        strPopUpbuilder.Append("					<div class='col-lg-12'>");
                                        strPopUpbuilder.Append("						<div class='wpb_wrapper'>");
                                        strPopUpbuilder.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                        strPopUpbuilder.Append("						</div>");
                                        strPopUpbuilder.Append("					</div>");
                                        strPopUpbuilder.Append("				</div>");
                                        strPopUpbuilder.Append("			</div>");
                                        strPopUpbuilder.Append("		</div>");
                                        strPopUpbuilder.Append("	</div>");
                                        strPopUpbuilder.Append("</div>");
                                        lgPopupCount++;

                                        strTabContent.Append("				</div>");
                                        strTabContent.Append("			</div>");
                                        strTabContent.Append("		</div>");
                                        strTabContent.Append("	</div>");
                                    }
                                    strTabContent.Append("</div>");
                                }
                                break;
                            }
                        case "ImageWithDescriptionLeftRight":
                            {
                                var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "ImageWithDescriptionLeftRight").ToList();
                                if (SubDetails.Count() > 0)
                                {
                                    int index = 1;
                                    foreach (var sub in SubDetails)
                                    {

                                        string strimagePath = "";

                                        if (!string.IsNullOrWhiteSpace(sub.PopupImageName))
                                        {
                                            strimagePath = (sub.PopupImageName);
                                            if (strimagePath.StartsWith("~/", StringComparison.Ordinal))
                                            {
                                                strimagePath = strimagePath.Replace("~/", "");
                                                strimagePath = ResolveUrl("~/" + strimagePath);
                                            }

                                        }
                                        if (index % 2 == 0)
                                        {
                                            strTabContent.Append("<div class='author-widget clearfix mb-15'>");
                                            strTabContent.Append("	<div class='about-author'>");
                                            strTabContent.Append("		<div class='row'>");
                                            strTabContent.Append("			<div class='col-lg-3'>");
                                            strTabContent.Append("				<div class='about-author'>");
                                            strTabContent.Append("					<div class='author-img-wrap'>");
                                            strTabContent.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                                            strTabContent.Append("					</div>");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");
                                            strTabContent.Append("			<div class='col-lg-9'>");
                                            strTabContent.Append("				<div class='author-details_1'>");
                                            strTabContent.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");
                                            strTabContent.Append("		</div>");
                                            strTabContent.Append("	</div>");
                                            strTabContent.Append("</div>");
                                        }
                                        else
                                        {
                                            strTabContent.Append("<div class='author-widget clearfix'>");
                                            strTabContent.Append("	<div class='about-author'>");
                                            strTabContent.Append("		<div class='row'>");
                                            strTabContent.Append("			<div class='col-lg-9'>");
                                            strTabContent.Append("				<div class='author-details_1'>");
                                            strTabContent.Append("				" + HttpUtility.HtmlDecode(sub.PopupDesc) + "");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");
                                            strTabContent.Append("			<div class='col-lg-3'>");
                                            strTabContent.Append("				<div class='about-author'>");
                                            strTabContent.Append("					<div class='author-img-wrap'>");
                                            strTabContent.Append("						<a href='#'><img class='img-fluid' alt='' src='" + strimagePath + "'></a>");
                                            strTabContent.Append("					</div>");
                                            strTabContent.Append("				</div>");
                                            strTabContent.Append("			</div>");
                                            strTabContent.Append("		</div>");
                                            strTabContent.Append("	</div>");
                                            strTabContent.Append("</div>");
                                        }
                                        strTabContent.Append("<hr>");
                                        index++;
                                    }
                                }
                                break;
                            }
                    }

                    if (isSubTab)
                    {
                        strTabContent.Append("</div>");
                    }
                }

                return strTabContent.ToString();
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "DepartmentsDetails").FirstOrDefault();
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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "DepartmentsDetails").FirstOrDefault();
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