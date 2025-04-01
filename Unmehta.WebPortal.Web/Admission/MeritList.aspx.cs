using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class MeritList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetDataBind();
            }
        }

        private void GetDataBind()
        {
            StudentGenerateMeritNumberBAL objStudentGenerateMeritNumberBAL = new StudentGenerateMeritNumberBAL();
            var data = objStudentGenerateMeritNumberBAL.GetAllMeritLstByCourse();
            if(data!=null)
            {
                if(data.Rows.Count>0)
                {
                    gView.DataSource = data;
                    gView.DataBind();
                }
            }
        }
    }
}