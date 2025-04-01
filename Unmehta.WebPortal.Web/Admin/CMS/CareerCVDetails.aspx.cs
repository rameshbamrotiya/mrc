using System;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using Ionic.Zip;
using BAL;
using System.Data;
using System.IO;
using System.Configuration;
using Unmehta.WebPortal.Common;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class CareerCVDetails : System.Web.UI.Page
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
            var startdate = "";
            var enddate = "";
            if (!string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                startdate = txtStartDate.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                enddate = txtEndDate.Text;
            }
            CareerBAL objBAL = new CareerBAL();
            DataTable dtFileList = objBAL.GetAllCareerCVRecord(startdate, enddate);
            grdUser.DataSourceID = string.Empty;
            grdUser.DataSource = dtFileList;
            grdUser.DataBind();
            Session["Exportdata"] = dtFileList;
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
        protected void btnexportfile_ServerClick(object sender, EventArgs e)
        {
            CareerBAL objBAL = new CareerBAL();
            string strRegistarionId = "CareercvFiles";
            DataTable dtFileList = null;
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
                DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
                dtFileList = (DataTable)Session["Exportdata"];
            }
            else
            {
                dtFileList = objBAL.GetAllCareerCVAllRecord();
            }
            if (dtFileList != null)
            {
                if (dtFileList.Rows.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        int inFileCount = 0;
                        foreach (DataRow row in dtFileList.Rows)
                        {
                            string strMappath = Server.MapPath(row["FilePath"].ToString());
                            string strFileType = row["EntryDate"].ToString();
                            if (File.Exists((strMappath)))
                            {
                                string strNameExt = Path.GetExtension(row["FilePath"].ToString());
                                var filname = zip.AddFile(strMappath, strFileType);//Zip file inside filename  
                                filname.FileName = strFileType + "_" + inFileCount + strNameExt;
                            }
                            inFileCount++;
                        }

                        string TempPath = ConfigDetailsValue.ZIPTempPath; ;
                        string fileName = strRegistarionId + ".zip";
                        if (!Directory.Exists(Server.MapPath(TempPath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(TempPath));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = TempPath + fileName;
                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(Server.MapPath(pathToCheck1));
                        }
                        zip.Save(Server.MapPath(TempPath) + fileName);//location and name for creating zip file  
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            Response.Clear();
                            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                            Response.ContentType = "application/zip";
                            Response.BinaryWrite(File.ReadAllBytes(Server.MapPath(TempPath) + fileName));
                            Response.End();
                        }
                        else
                        {
                            Functions.MessagePopup(this, "File is not found.", PopupMessageType.info);
                            return;
                        }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "There is no record found.", PopupMessageType.info);
                    return;
                }
            }
            else
            {
                Functions.MessagePopup(this, "select Start Date and End Date.", PopupMessageType.info);
                return;
            }
        }

        protected void btndatecancle_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                ShowHideControl(VisibityType.GridView);
                Session["Exportdata"] = "";
                BindGridView();
                Response.Redirect(ResolveUrl("~/Admin/CMS/CareerCVDetails"));
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btndatesearch_ServerClick(object sender, EventArgs e)
        {
            var startdate = "";
            var enddate = "";
            if (!string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                startdate = txtStartDate.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                enddate = txtEndDate.Text;
            }
            CareerBAL objBAL = new CareerBAL();
            DataTable dtFileList = objBAL.GetAllCareerCVRecord(startdate, enddate);
            grdUser.DataSourceID = string.Empty;
            grdUser.DataSource = dtFileList;
            grdUser.DataBind();
            Session["Exportdata"] = dtFileList;
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnExportToExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                var startdate = "";
                var enddate = "";
                if (!string.IsNullOrWhiteSpace(txtStartDate.Text))
                {
                    startdate = txtStartDate.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtEndDate.Text))
                {
                    enddate = txtEndDate.Text;
                }
                CareerBAL objBAL = new CareerBAL();
                DataTable dt = objBAL.GetAllCareerCVRecord(startdate, enddate);
                dt.Columns.Remove("id");
                dt.Columns.Remove("FilePath");
                dt.Columns.Remove("CreateDate");
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=CareerCVDetails.xlsx");
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
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}