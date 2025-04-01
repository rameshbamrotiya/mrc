using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class SendSubscribeNewsletterLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView(txtSearch.Text);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindGridView();
        }

        public void BindGridView(string txtSearch = "")
        {

            SendSubscribeNewsletterBAL objBal = new SendSubscribeNewsletterBAL();


            DataTable dt = new DataTable();

            var dataDetails = objBal.GetSendSubscribeNewsletterLog();

            DataRow[] drs = dataDetails.Select("MailSubject  LIKE '%" + txtSearch + "%' Or FullName LIKE '%" + txtSearch + "%' Or EmailId LIKE '%" + txtSearch + "%' Or MobileNo LIKE '%" + txtSearch + "%'");
            
            dt = drs.CopyToDataTable();

            grdUser.DataSource = dt;
            grdUser.DataBind();

        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}