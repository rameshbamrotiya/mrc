using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Web.Hospital.Payment;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web
{
	public partial class AboutBoardOfDirector : System.Web.UI.Page
	{
		public static string strBoard;
		public static string strHeaderImage;
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				strBoard = GetPageData();
				strHeaderImage = GetHeaderImage();
			}
		}

		private string GetHeaderImage()
		{
			int languageId = Functions.LanguageId;
			StringBuilder strBoardOfDirector = new StringBuilder();
			using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
			{
				var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName( languageId, "About/AboutBoardOfDirector").FirstOrDefault();

				if (dataMain != null)
				{
					LableData:
					strBoardOfDirector = new StringBuilder();
					if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
					{
						strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath+ dataMain.HeaderImage));
					}
					else
					{
						languageId = 1;
						dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "About/AboutBoardOfDirector").FirstOrDefault();
						if (languageId != 1)
						{
							goto LableData;
						}
					}
				}
			}
			return strBoardOfDirector.ToString();
		}

		private string GetPageData()
		{
			int languageId = Functions.LanguageId;
			StringBuilder strBoardOfDirector = new StringBuilder();
			using (IGoverningBoardMasterRepository objBlogCategoryMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
			{
				var dataMain = objBlogCategoryMasterRepository.GetGoverningBoardByLangId(languageId);

				if(dataMain!=null)
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
						if (dataMain.PageDescription == "<p>&nbsp;</p>\r\n<quillbot-extension-portal></quillbot-extension-portal>")
						{
							dataMain.PageDescription = "";
						}

						if (!string.IsNullOrWhiteSpace(dataMain.PageDescription))
						{
							strBoardOfDirector.Append(HttpUtility.HtmlDecode(dataMain.PageDescription));
						}

						var dataList = objBlogCategoryMasterRepository.GetGoverningBoardMasterDesignationDetailDetails(dataMain.Id, languageId).Where(x => x.IsActive == true).ToList();

						if (dataList.Count() > 0)
						{
							int i = 1;

							strBoardOfDirector.Append("<div class='tabledummyclass'> <div class='table-responsive'>");
							strBoardOfDirector.Append("			<table class='table table-hover table-center mb-0 maintable'>");
							strBoardOfDirector.Append("				<thead>");
							strBoardOfDirector.Append("					<tr>");
							strBoardOfDirector.Append("						<th>Sr No</th>");
							strBoardOfDirector.Append("						<th>Name</th>");
							strBoardOfDirector.Append("						<th>DESIGNATION</th>");
							if (dataList.Where(x => !string.IsNullOrWhiteSpace(x.FilePath)).Count() > 0)
							{
								strBoardOfDirector.Append("						<th>Photo</th>");
							}
							strBoardOfDirector.Append("					</tr>");
							strBoardOfDirector.Append("				</thead>");
							strBoardOfDirector.Append("				<tbody>");
							foreach (var row in dataList)
							{

								string strURL = ResolveUrl(("~/DoctorProfile?" + Functions.Base64Encode(row.Id.ToString())));
								strBoardOfDirector.Append("");

								strBoardOfDirector.Append("<tr>");
								strBoardOfDirector.Append("	<td>" + i + "</td>");
								strBoardOfDirector.Append("	<td>" + row.DesignatedPersonName + "</td>");
								strBoardOfDirector.Append("	<td>" + row.DesignationName + "</td>");

								if (dataList.Where(x => !string.IsNullOrWhiteSpace(x.FilePath)).Count() > 0)
								{
									strBoardOfDirector.Append("	<td>");
									strBoardOfDirector.Append("		<h2 class='table-avatar'>");
									strBoardOfDirector.Append("			<a href='#' class='avatar avatar-xl mr-2'>");
									if (!string.IsNullOrWhiteSpace(row.FullPath))
									{
										strBoardOfDirector.Append("				<img class='avatar-img rounded-circle' src='" + ResolveUrl(row.FullPath) + "' alt='User Image'>");
									}
									else
									{
										strBoardOfDirector.Append("				<img class='avatar-img rounded-circle' src='" + ResolveUrl("~/Hospital/assets/img/gb/NoImage.png") + "' alt='User Image'>");
									}
									strBoardOfDirector.Append("			</a>");
									strBoardOfDirector.Append("		</h2>");
									strBoardOfDirector.Append("	</td>");
								}
								strBoardOfDirector.Append("</tr>");

								i++;
							}
							strBoardOfDirector.Append("    </tbody>");
							strBoardOfDirector.Append("				</table>");
							strBoardOfDirector.Append("			</div>");
							strBoardOfDirector.Append("		</div>");
						}
						else
						{
							dataList = objBlogCategoryMasterRepository.GetGoverningBoardMasterDesignationDetailDetails(dataMain.Id, languageId).ToList();
							if (Functions.LanguageId != 1 && languageId != 1)
							{
								languageId = 1;
								goto LableData;
							}
						}
					}
				}

				

			}
			return strBoardOfDirector.ToString();
		}
	}
}