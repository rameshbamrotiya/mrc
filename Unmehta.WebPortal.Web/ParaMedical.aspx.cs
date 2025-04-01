using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web
{
    public partial class ParaMedical : System.Web.UI.Page
    {
        public static string strHeaderImage;
        public static string strNoSeatsDuration, strName, strInsrtruction, strCourceFee, strNote, strCourseList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                GetDataList();
                //strAcademicTabDetails = GetTabDetailList();
            }
        }

        private void GetDataList()
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));


            strCourseList = "";
            strNoSeatsDuration = "";
            strName = "";
            strInsrtruction = "";
            strCourceFee = "";
            strNote = "";
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);

                    List<StudentSubCourseBO> dataSub = new List<StudentSubCourseBO>();

                    foreach (var dataRom in data)
                    {
                        var GetSubAll = objStudentAdvertisementBAL.GetSubAll(dataRom.Id);
                        if (GetSubAll != null)
                        {
                            dataSub.AddRange(Functions.ToListof<StudentSubCourseBO>(GetSubAll));
                        }
                    }
                    foreach (var dataRow in dataSub)
                    {
                        string strURL = ResolveUrl(("~/ParaMedical?" + Functions.Base64Encode(dataRow.Id.ToString())));
                        strCourseList += "<li><a href='"+ strURL + "' class='"+ (dataRow.Id == Convert.ToInt32(queryString)?"active":"") + "'>"+ dataRow.CourseName + "</a></li>";
                    }
                    var mainModel = dataSub.Where(x => x.Id == Convert.ToInt32(queryString)).FirstOrDefault();
                    if(mainModel!=null)
                    {
                        strNoSeatsDuration = "<li>No Of Seats : "+ mainModel.TotalSeat + "</li><li>Duration : " + mainModel.CourseDuration + "</li>";
                        strName =  mainModel.CourseName ;
                        Page.Title = " U N MEHTA - " + mainModel.CourseName;
                        strInsrtruction = HttpUtility.HtmlDecode(mainModel.Information);
                        strCourceFee = HttpUtility.HtmlDecode(mainModel.FeesDescription);
                        strNote = HttpUtility.HtmlDecode(mainModel.CourseNote);
                    }
                }
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ParaMedical").FirstOrDefault();

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
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "ParaMedical").FirstOrDefault();
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