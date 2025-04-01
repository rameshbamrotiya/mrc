using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Hospital
{
    public partial class AppointmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string strEncQuerystring = Request.QueryString.ToString();
                string strEncQuerystring = MobileOTP.Mobile;
                if (!string.IsNullOrWhiteSpace(strEncQuerystring))
                {
                    using (IHospitalAppointmentRepository objData = new HospitalAppointmentRepository(Functions.strSqlConnectionString))
                    {
                        grdUser.DataSource = objData.GetAllHospitalAppointmentByMobile(strEncQuerystring);
                        grdUser.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("~/Hospital/MobileOTP.aspx");
                }
            }
        }

        protected void ibtn_FollowUp_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                Response.Redirect("~/Hospital/Appointment?Id=" + rowId);
            }
            catch (Exception ex)
            {
            }
        }
    }
}