using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class Careers : System.Web.UI.Page
    {
        public static string strJobDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strJobDetails = GetDetails();
            }
        }

        private string GetDetails()
        {
            StringBuilder strBuilder = new StringBuilder();

            using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
            {
                DataSet ds = new DataSet();
                var gvData = objAdvertisementRepository.GetAllTblRecruitmentAdvertisement().Where(x => x.IsActive == true);
                if (gvData != null)
                {
                    if (gvData != null)
                    {
                        if (gvData.Count() > 0)
                        {
                            foreach (var row in gvData)
                            {
                                strBuilder.Append("");
                                string strLink = (ConfigDetailsValue.AddRecrutmentFileUploadPath+row.FileName.ToString());
                                string strName = row.AdvertisementName.ToString();

                                strLink = string.IsNullOrWhiteSpace(strLink) ? "#" : ResolveUrl(strLink);
                                string JobId = HttpUtility.UrlEncode(Functions.Encrypt(row.Id.ToString()));
                                strBuilder.Append("<div class='col-sm-6 col-lg-4 mb-40'>                                             ");
                                strBuilder.Append("    <div class='features-inner'>                                             ");
                                strBuilder.Append("        <ul class='align-items-center'>                                      ");
                                strBuilder.Append("            <li>                                                             ");
                                strBuilder.Append("                <i class='flaticon-strategy-in-a-labyrinth'></i>             ");
                                strBuilder.Append("            </li>                                                            ");
                                strBuilder.Append("            <li>                                                             ");
                                strBuilder.Append("                <h3>" + strName + "</h3>                                      ");
                                strBuilder.Append("            </li>                                                            ");
                                strBuilder.Append("        </ul>                                                                ");
                                strBuilder.Append("    </div>                                                                   ");
                                strBuilder.Append("    <div class='appbtnnow'>                                                  ");
                                strBuilder.Append("        <div class='row'>                                                    ");
                                strBuilder.Append("            <div class='col-lg-6'>                                           ");
                                strBuilder.Append("                <a class='common-btn btn-block' target='_blank' href='" + ResolveUrl(strLink).ToString() + "'>       ");
                                strBuilder.Append("                    View Details</a></div>                                      ");
                                strBuilder.Append("            <div class='col-lg-6'><a class='common-btn btn-block' href='" + ResolveUrl("~/Recruitment/CurrentAdvertisements?" + JobId) + "'>  ");
                                strBuilder.Append("                    Apply Now                                                ");
                                strBuilder.Append("                                                                             ");
                                strBuilder.Append("                </a></div>                                                   ");
                                strBuilder.Append("        </div>                                                               ");
                                strBuilder.Append("    </div>                                                                   ");
                                strBuilder.Append("</div>                                                                       ");
                            }
                        }
                    }
                    else
                    {
                        strBuilder.Append("<div class='row'>");
                        strBuilder.Append("	<div class='col-md-12 text-center'>");
                        strBuilder.Append("    <div class='mb-4 lead'>Currently there is no recrutment in process.</div>");
                        strBuilder.Append("    <a href='" + ResolveUrl("~/") + "' class='btn btn-link'>Back to Home</a>");
                        strBuilder.Append("</div>	");
                        strBuilder.Append("</div>");

                    }
                }


                return strBuilder.ToString();
            }
        }
    }
}