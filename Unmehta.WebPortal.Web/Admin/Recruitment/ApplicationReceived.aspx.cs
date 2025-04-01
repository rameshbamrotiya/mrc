using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class ApplicationReceived : System.Web.UI.Page
    {
        #region Page Load
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
                Bind_DocGrid();
                divCandidateDetails.Visible = false;
            }
        }
        #endregion

        #region Common Function
        protected void Bind_DocGrid()
        {
            try
            {
                PostApplicationMasterBAL objBALPOst = new PostApplicationMasterBAL();
                DataSet ds1 = new DataSet();
                ds1 = objBALPOst.GetAllQualification("GetAllJobMaster");
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlJobList.DataSource = ds1.Tables[0];
                        ddlJobList.DataTextField = "JobTitle";
                        ddlJobList.DataValueField = "Id";
                        ddlJobList.DataBind();
                        ddlJobList.Items.Insert(0, new ListItem("Select All Job", ""));
                    }
                }

                CareerMasterBAL objBAL = new CareerMasterBAL();
                CareerMasterBO objBO = new CareerMasterBO();
                int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
                DataSet ds = new DataSet();
                ds = objBAL.GetTotalApplicationReceived_Details(IsFinalSubmit);
                gvApplicationReceived.DataSourceID = string.Empty;
                gvApplicationReceived.DataSource = ds;
                gvApplicationReceived.DataBind();
                gvApplicationReceived.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gvApplicationReceived.FooterRow.Cells[0].Text = "Total";
                gvApplicationReceived.FooterRow.Cells[2].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>(ds.Tables[0].Columns["TotalAppRecevie"].ToString())).ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        public void BindCandidateDetails()
        {
            int Id = Convert.ToInt32(Session["PostId"].ToString());
            CareerMasterBAL objBAL = new CareerMasterBAL();
            CareerMasterBO objBO = new CareerMasterBO();
            int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
            DataSet ds = new DataSet();
            ds = objBAL.GetAllCandidateDetails_ByPostId(Id, IsFinalSubmit);
            lblCount.Text = "Total No Of Candidate : " + Convert.ToString(ds.Tables[0].Rows.Count);
            gView.DataSourceID = string.Empty;
            gView.DataSource = ds;
            gView.DataBind();
        }
        #endregion

        #region Export 
        protected void btnExportApplicationList_Click(object sender, EventArgs e)
        {
            try
            {
                CareerMasterBAL objBAL = new CareerMasterBAL();
                CareerMasterBO objBO = new CareerMasterBO();
                int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
                DataSet ds = new DataSet();
                ds = objBAL.GetTotalApplicationReceived_Details(IsFinalSubmit);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0], "Candidate LIst");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ApplicationReceivedList.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session["PostId"].ToString());
            CareerMasterBAL objBAL = new CareerMasterBAL();
            CareerMasterBO objBO = new CareerMasterBO();
            int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
            DataSet ds = new DataSet();
            ds = objBAL.GetAllCandidateDetails_ByPostId(Id, IsFinalSubmit);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0], "Candidate LIst");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CandidateDetailsList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        #endregion

        #region Button Event
        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            try
            {
                CareerMasterBAL objBAL = new CareerMasterBAL();
                CareerMasterBO objBO = new CareerMasterBO();

                DataSet ds = new DataSet();
                int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
                DataTable dt = new DataTable();
                ds = objBAL.GetTotalApplicationReceived_Details(IsFinalSubmit);
                dt = ds.Tables[0];
                if (ddlJobList.SelectedIndex > 0)
                {
                    DataRow[] dr = dt.Select("PostId =" + ddlJobList.SelectedValue);
                    DataColumnCollection dc = dt.Columns;
                    dt = new DataTable();

                    foreach (DataColumn row in dc)
                    {
                        dt.Columns.Add(row.ColumnName, row.DataType);
                    }

                    foreach (DataRow row in dr)
                    {
                        dt.ImportRow(row);
                    }
                }

                gvApplicationReceived.DataSourceID = string.Empty;
                gvApplicationReceived.DataSource = dt;
                gvApplicationReceived.DataBind();
                gvApplicationReceived.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gvApplicationReceived.FooterRow.Cells[0].Text = "Total";
                gvApplicationReceived.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>(dt.Columns["TotalAppRecevie"].ToString())).ToString();
                divCandidateDetails.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ddlJobList.SelectedItem.Text = "Select All Job";
                ddlFinalSubmit.SelectedIndex = 0;
                Bind_DocGrid();
                divCandidateDetails.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void lnkApplicationReceived_Click(object sender, EventArgs e)
        {
            try
            {
                divCandidateDetails.Visible = true;
                int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
                int Id = Convert.ToInt32(gvApplicationReceived.DataKeys[rowIndex].Values[0]);
                Session["PostId"] = Id;
                CareerMasterBAL objBAL = new CareerMasterBAL();
                CareerMasterBO objBO = new CareerMasterBO();
                int IsFinalSubmit = Convert.ToInt32(ddlFinalSubmit.SelectedValue);
                DataSet ds = new DataSet();
                ds = objBAL.GetAllCandidateDetails_ByPostId(Id, IsFinalSubmit);
                lblCount.Text = "Total No Of Candidate : " + Convert.ToString(ds.Tables[0].Rows.Count);
                gView.DataSourceID = string.Empty;
                gView.DataSource = ds;
                gView.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            this.BindCandidateDetails();
        }
        #endregion
    }
}