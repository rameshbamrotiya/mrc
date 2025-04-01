using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class LTEStudent : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWelcomName.Text = "Welcome " + SessionWrapper.StudentRegistration.FirstName + " " + SessionWrapper.StudentRegistration.MiddleName + " " + SessionWrapper.StudentRegistration.LastName + "," ;
        }
    }
}