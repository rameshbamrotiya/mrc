using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class CoursePaymentStatusPage : System.Web.UI.Page
    {
        public static string strStudentId;
        public static string strCourseId;
        public static string strCourseName;
        public static string strPrice;
        public static string strMessage;
        public static string strInnerMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strEndQueryString = Request.QueryString.ToString();
                string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));


                string[] strQuery = strQueryString.Split('|').ToArray();

                strStudentId = strQuery[0].ToString().Replace("StudentId=", "");
                strCourseId = strQuery[1].ToString().Replace("CourseId=", "");
                strCourseName = strQuery[2].ToString().Replace("CourseName=", "");
                strPrice = strQuery[3].ToString().Replace("Price=", "");
                strMessage= strQuery[4].ToString().Replace("Status=", "");
            }
        }
    }
}