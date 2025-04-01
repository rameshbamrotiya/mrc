using BAL;
using BO;
using ClosedXML.Excel;
using System;
using System.Data;
using BO.Admission;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.Payment
{
    public partial class DonationListData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
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
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void BindGridview()
        {
            PaymentBAL objBAL = new PaymentBAL();
            DataSet ds = new DataSet();

            string startdate = "";
            string Enddate = "";

            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    startdate = txtStartDate.Text;
                    Enddate = txtEndDate.Text;
                }
                else
                {
                    Functions.MessagePopup(this, "Select Enddate", PopupMessageType.warning);
                    txtEndDate.Focus();
                    return;
                }
            }
           
            ds = objBAL.SelectDatewiseDonationRecord(startdate, Enddate);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                Session["GridDataset"] = ds.Tables[0];
                btnexport.Visible = true;
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
            }
            else
            {
                gView.DataSource = ds.Tables[0];
                gView.DataBind();
                btnexport.Visible = false;
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void btnexport_ServerClick(object sender, EventArgs e)
        {
            if (Session["GridDataset"] != null)
            {
                DataTable dt = (DataTable)Session["GridDataset"];
                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=StudentStatusWiseData.xlsx");
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
            else
            {
                btnexport.Visible = false;
            }

        }
    }
}