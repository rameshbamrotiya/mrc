using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class ChatBotReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
            }
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string strSearch = "";
            using (ChatBotRepository obj = new ChatBotRepository(Functions.strSqlConnectionString))
            {
                var alldata = obj.GetAllChatBotDetails().Where(x => x.IsDelete == false).ToList();

                if(string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    gvDetails.DataSource = alldata.OrderByDescending(x=> x.EntryDate).ToList();
                    gvDetails.DataBind();
                }
                else
                {
                    gvDetails.DataSource = alldata.Where(x=> (x.EntryDate.HasValue && x.EntryDate.Value.ToString("dd/MM/yyyy").Replace("-","/")==txtSearch.Text) || x.Name.Contains(txtSearch.Text) || x.Phone.Contains(txtSearch.Text) || (x.EmailId.Contains(txtSearch.Text) && x.IsSkipEmail==false)).OrderByDescending(x => x.EntryDate).ToList().ToList();
                    gvDetails.DataBind();
                }
            }
        }

        protected void lnkMenu_Remove_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvDetails.DataKeys[rowindex]["Id"].ToString());
                using (ChatBotRepository obj = new ChatBotRepository(Functions.strSqlConnectionString))
                {
                    obj.RemoveChatBotDetails(rowId, out errorMessage);
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindGridView();
        }

        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView();
            gvDetails.PageIndex = e.NewPageIndex;
            gvDetails.DataBind();
        }
    }
}