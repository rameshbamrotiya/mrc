using System;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using BAL;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class WritetoUnmicrc : System.Web.UI.Page
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
            using (XLWorkbook wb = new XLWorkbook())
            {
                WriteToUNMICRCBAL objBAL = new WriteToUNMICRCBAL();
                DataTable ds = new DataTable();
                ds = objBAL.GetUNMICRCdata();

                var dbview = ds.DefaultView;

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    dbview.RowFilter = "FullName ='" + txtSearch.Text.Trim() + "' OR EmailId ='" + txtSearch.Text.Trim() + "' OR MobileNo ='" + txtSearch.Text.Trim() + "' OR Country ='" + txtSearch.Text.Trim() + "' OR State ='" + txtSearch.Text.Trim() + "' OR City ='" + txtSearch.Text.Trim() + "' OR FeedbackDescription ='" + txtSearch.Text.Trim() + "' OR EntryDate ='" + txtSearch.Text.Trim() + "'";
                }

                ds = dbview.ToTable();
                ds.Columns.Remove("Id");
                ds.Columns.Remove("CreateDate");
                ds.Columns.Remove("IsUnmicrc");

                wb.Worksheets.Add(ds, "WriteToUNMICRC data");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=WriteToUNMICRCdata.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private System.Data.DataTable getgriddata()
        {
            System.Data.DataTable dt = new DataTable();
            for (int i = 1; i < grdUser.Columns.Count; i++)
            {
                dt.Columns.Add("column" + i.ToString());
            }
            foreach (GridViewRow row in grdUser.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 1; j < grdUser.Columns.Count; j++)
                {
                    dr["column" + j.ToString()] = row.Cells[j].Text;
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }


    }
}