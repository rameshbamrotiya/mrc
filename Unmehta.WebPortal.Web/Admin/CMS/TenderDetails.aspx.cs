using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class TenderDetails : System.Web.UI.Page
    {
        public static int TenderId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                TenderMasterBO objBo = new TenderMasterBO();
                int tenderId = 0;
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    if (int.TryParse(queryString, out tenderId))
                    {
                        objBo.TenderID = tenderId;
                        TenderId = tenderId;
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }
                }
                else
                {
                    Response.Redirect("~/");
                }
                DataSet ds = new TenderMasterBAL().TenderMaster_SelectByTenderID(objBo);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.dtlstTenderDetails.DataSource = ds.Tables[0];
                        this.dtlstTenderDetails.DataBind();
                    }
                }
            }
        }


        protected void dtlstTenderDetails_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.SelectedItem)
            {
                HtmlGenericControl dvDocument = (HtmlGenericControl)e.Item.FindControl("dvDocument");
                HtmlGenericControl dvDocumentDate = (HtmlGenericControl)e.Item.FindControl("dvDocumentDate");

                TenderMasterBO objBos = new TenderMasterBO();
                objBos.TenderID = TenderId;
                DataTable dsTest = new TenderMasterBAL().TenderMaster_SelectByTenderID(objBos).Tables[0];

                DataRow drRow = dsTest.Rows[0];

                if (drRow["LastDate"] == DBNull.Value && drRow["OpeningDate"] == DBNull.Value && drRow["PBMeetingDate"] == DBNull.Value)
                {
                    dvDocumentDate.Visible = false;
                }
                else
                {
                    dvDocumentDate.Visible = true;
                }


                Repeater dl = (Repeater)e.Item.FindControl("dtlstDocument");
                TenderMasterBO objBo = new TenderMasterBO();
                objBo.TenderID = TenderId;
                DataSet ds = new TenderMasterBAL().Documents_SelectByTenderID(objBo);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dvDocument.Visible = true;
                        dl.DataSource = ds.Tables[0];
                        dl.DataBind();
                    }
                    else
                    {
                        dvDocument.Visible = false;
                    }
                }
                else
                {
                    dvDocument.Visible = false;
                }

            }
        }

    }
}