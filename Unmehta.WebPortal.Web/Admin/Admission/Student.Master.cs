using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class Student : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                try
                {
                    if (SessionWrapper.StudentRegistration.Username == null)
                    {
                        Response.Redirect("~/Admission/");
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Admission/");
                }
                if(SessionWrapper.StudentRegistration.IsUsernameReset && !HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Contains(("Admin/Admission/ChangePassword.aspx")))
                {
                    Response.Redirect("~/Admin/Admission/ChangePassword.aspx");
                }
            }
        }
    }
}