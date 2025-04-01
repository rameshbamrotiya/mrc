using System;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class OnlineEventRegistrtion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }
        private void BindGridView()
        {
            grdUser.DataBind();
        }
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                txtSearch.Text = "";
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    break;
                default:
                    pnlView.Visible = true;
                    break;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            OnlineEventRegistrtionBAL objBAL = new OnlineEventRegistrtionBAL();

            DataTable ds = new DataTable();
            ds = objBAL.SelectRecord();

            var dbview = ds.DefaultView;

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                dbview.RowFilter = "FirstName ='" + txtSearch.Text.Trim() + "'";
            }
            ds = dbview.ToTable();
            ds.Columns.Remove("Id");
            ds.Columns.Remove("EventId");

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds, "OnlineEventRegistration");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OnlineEventRegistration.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}