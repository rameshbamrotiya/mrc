using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class Packages : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strPackages;
        public static string strPackagestab;

        public static string strPackagesModels;

        public static string strQuickLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strPackages = BindPackages();
                strQuickLink = Functions.CreateQuickLink("HiddenPage", "Packages");
            }
        }


        private string BindPackages()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strPackage = new StringBuilder();



            string rowURl = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "").ToString();
            //rowURl = rowURl.Substring(1);

            ErrorLogger.ERROR(rowURl, Request.RawUrl, this);
            bool IsDisabledTranslate = new MenuBAL().GetTranslateStatus(languageId, rowURl);
            if (IsDisabledTranslate && Functions.LanguageId != 1)
            {
                {
                    Functions.GetReloadPage(this.Page, ref strPackage); // 10 sec page reload with english content logic
                    return strPackage.ToString();
                }
            }
            else
            {

                StringBuilder strPackagesModel = new StringBuilder();
                StringBuilder strPackagetab = new StringBuilder();
                using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
                {
                    var dataList = objBlogCategoryMasterRepository.GetAllPackageMaster(languageId).ToList();
                    var dataSubTypeList = objBlogCategoryMasterRepository.GetAllPackageTypeMaster(languageId).ToList();

                    LableData:

                    if (dataList.Count() > 0)
                    {
                        int i = 1;
                        foreach (var row in dataList)
                        {

                            strPackagetab.Append("");
                            strPackage.Append("");
                            strPackagesModel.Append("");

                            strPackagetab.Append("<li class='nav-item'>");
                            strPackagetab.Append("	<a class='nav-link " + (i == 1 ? "active" : "") + "' href='#doc_" + i + "' data-toggle='tab'>" + row.Title + "</a>");
                            strPackagetab.Append("</li>");

                            strPackage.Append("<div role='tabpanel' id='doc_" + i + "' class='tab-pane fade " + (i == 1 ? "active show" : "") + "'>");
                            strPackage.Append("    <section class='service-section'>");
                            strPackage.Append("        <div class='accordion-box'>");
                            strPackage.Append("            <div class='title-box'>");
                            strPackage.Append("                <h6>" + row.Title + "</h6>");
                            strPackage.Append("            </div>");
                            strPackage.Append("            <ul class='accordion-inner'>");
                            foreach (var rowSubType in dataSubTypeList)
                            {
                                var dataRowDetails = objBlogCategoryMasterRepository.GetAllPackageSubMasterDetails(row.Id);
                                dataRowDetails = dataRowDetails.Where(x => x.PackageId == rowSubType.Id).ToList();

                                if (dataRowDetails.Count > 0)
                                {
                                    strPackage.Append("                <li class='accordion block'>");
                                    strPackage.Append("                    <div class='acc-btn'>");
                                    strPackage.Append("                        <div class='icon-outer'></div>");
                                    strPackage.Append("                        <h6>" + rowSubType.PackageType + "</h6>");
                                    strPackage.Append("                    </div>");
                                    strPackage.Append("                    <div class='acc-content'>");
                                    strPackage.Append("                        <div class='row'>");

                                    foreach (var datarow in dataRowDetails)
                                    {
                                        string strData = datarow.SubTitle;
                                        if (datarow.SubTitle.Length > 40)
                                        {
                                            strData = datarow.SubTitle.Remove(40) + "..";
                                        }
                                        strPackage.Append("                            <div class='col-lg-4 col-md-6  service-block'>");
                                        strPackage.Append("                                <div class='service-block-one mb-10'>");
                                        strPackage.Append("                                    <div class='inner-box'>");
                                        strPackage.Append("                                        <div class='bg-layer' style='background-image: url(Hospital/assets/img/shap-5.png);'>");
                                        strPackage.Append("                                        </div>");
                                        strPackage.Append("                                        <h3><a href='#'>" + strData + "</a></h3>");
                                        strPackage.Append("                                        <div class='row align-items-center'>");
                                        strPackage.Append("                                            <div class='col-lg-8'>");
                                        strPackage.Append("                                                <div class='text'>Rs. " + datarow.Price + "/-</div>");
                                        strPackage.Append("                                            </div>");
                                        strPackage.Append("                                            <div class='col-lg-4 text-right'>");
                                        strPackage.Append("                                                <a href='#' type='button' data-toggle='modal' data-target='#exampleModalLong" + rowSubType.PackageType.Replace(" ", "") + "" + datarow.Id + "' data-whatever='@mdo' data-original-title='' title='' class='cart-icon'><i class='fas fa-angle-right'></i></a>");
                                        strPackage.Append("                                            </div>");
                                        strPackage.Append("                                        </div>");
                                        strPackage.Append("                                    </div>");
                                        strPackage.Append("                                </div>");
                                        strPackage.Append("                            </div>");



                                        strPackagesModel.Append("<div class='modal fade' id='exampleModalLong" + rowSubType.PackageType.Replace(" ", "") + "" + datarow.Id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLongTitle" + datarow.Id + "'");
                                        strPackagesModel.Append("	aria-hidden='true'>");
                                        strPackagesModel.Append("	<div class='modal-dialog' role='document'>");
                                        strPackagesModel.Append("		<div class='modal-content'>");
                                        strPackagesModel.Append("			<div class='modal-header'>");
                                        strPackagesModel.Append("				<h5 class='modal-title' id='exampleModalLongTitle" + datarow.Id + "'>" + datarow.SubTitle + "</h5>");
                                        strPackagesModel.Append("				<button class='close' type='button' data-dismiss='modal' aria-label='Close' data-original-title=''");
                                        strPackagesModel.Append("					title=''><span aria-hidden='true'>×</span></button>");
                                        strPackagesModel.Append("			</div>");
                                        strPackagesModel.Append("			<div class='modal-body'><p>");

                                        strPackagesModel.Append(HttpUtility.HtmlDecode(datarow.Description));

                                        strPackagesModel.Append("</p>			</div>");
                                        strPackagesModel.Append("		</div>");
                                        strPackagesModel.Append("	</div>");
                                        strPackagesModel.Append("</div>");
                                    }

                                    strPackage.Append("                        </div>");
                                    strPackage.Append("                    </div>");
                                    strPackage.Append("                </li>");
                                }
                            }
                            strPackage.Append("            </ul>");
                            strPackage.Append("        </div>");
                            strPackage.Append("    </section>");
                            strPackage.Append("</div>");

                            i++;
                        }
                    }
                    else
                    {
                        if (Functions.LanguageId != 1 && languageId != 1)
                        {
                            languageId = 1;
                        dataList = objBlogCategoryMasterRepository.GetAllPackageMaster(languageId).OrderByDescending(x => x.Id).ToList();
                            goto LableData;
                        }
                    }

                }
                strPackagesModels = strPackagesModel.ToString();
                strPackagestab = strPackagetab.ToString();
            }

            return strPackage.ToString();
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Packages").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Packages").FirstOrDefault();
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