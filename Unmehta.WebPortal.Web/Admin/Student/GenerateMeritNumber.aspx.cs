using BAL;
using BAL.Admission;
using BO;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using ClosedXML.Excel;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public class GrdFilter
    {
        public int Row { get; set; }
        public string ColumnName { get; set; }
        public string FilterValue { get; set; }
    }
    public partial class GenerateMeritNumber : System.Web.UI.Page
    {
        public static List<DataColumn> columnNames;
        public static List<GrdFilter> lstFilterList;
        public static int rowCount = 1;
        public static bool isFiltered;

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
                    lstFilterList = new List<GrdFilter>();
                    isFiltered = false;
                    BindPageViewData();
                    this.BindGridViewData();
                    BindColumnName();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private void BindColumnName()
        {
            if (columnNames.Count > 0)
            {
                List<ListItem> lstItem = new List<ListItem>();
                columnNames.ForEach(x => { lstItem.Add(new ListItem { Text = x.ColumnName, Value = x.ColumnName }); });
                ddlColumnList.DataSource = lstItem;
                ddlColumnList.DataBind();
                ddlColumnList.DataTextField = "Text";
                ddlColumnList.DataValueField = "Value";
                ddlColumnList.Items.Insert(0, new ListItem("Select Column", "0"));
            }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private void BindPageViewData()
        {
            try
            {
                using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(objStudentAdvertisementBAL.GetAll()).Where(x => x.IsVisible == true).ToList();
                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            ddlCourceList.DataSource = data;
                            ddlCourceList.DataTextField = "Name";
                            ddlCourceList.DataValueField = "Id";
                            ddlCourceList.DataBind();
                            ddlCourceList.Items.Insert(0, new ListItem("Select Cource", "0"));


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        private void BindGridViewData(string sortExpression = null)
        {
            Session["GridDataset"] = null;
            using (StudentGenerateMeritNumberBAL objCandidateDetailsRepository = new StudentGenerateMeritNumberBAL())
            {
                rowCount = 1;
                if (ddlCourceList.SelectedIndex == 0)
                {
                    using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                    {
                        List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(objStudentAdvertisementBAL.GetAll()).Where(x => x.IsVisible == true).ToList();
                        if (data != null)
                        {
                            if (data.Count > 0)
                            {
                                ddlCourceList.SelectedIndex = 1;
                            }
                        }
                    }
                }
                {
                    DataTable Tempdt = new DataTable();
                    DataTable dt = new DataTable();
                    if (ddlCourceList.SelectedIndex != 0)
                    {
                        Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue));

                        columnNames = Tempdt.Columns.Cast<DataColumn>()
                                 .ToList();
                    }
                    else
                    {
                        return;
                    }
                    dt = Tempdt;
                    string[] ColumnsToBeDeleted = { "StudentId", "CourseId", "CasteId", "EduCationTypeId", "MeritNo" };

                    //long lgAllrec = dt.AsEnumerable().Count();
                    //if ((dt.AsEnumerable().Where(x => !string.IsNullOrWhiteSpace(x.Field<string>("GroupName"))).Count() <= 0))
                    //{
                    //    ColumnsToBeDeleted.ToList().Add("GroupName");
                    //}

                    foreach (string ColName in ColumnsToBeDeleted)
                    {
                        List<DataColumn> lstColumn = dt.Columns.Cast<DataColumn>().Where(x => x.ColumnName.Contains(ColName)).ToList();
                        foreach (var column in lstColumn)
                        {
                            dt.Columns.Remove(column);
                        }
                    }
                    if (ddlCourceList.SelectedIndex == 0)
                    {
                        columnNames = dt.Columns.Cast<DataColumn>()
                                 .ToList();
                    }
                    if (lstFilterList.Count > 0)
                    {
                        List<DataRow> drDetails = new List<DataRow>();

                        foreach (var columnfl in lstFilterList)
                        {
                            foreach (var column in columnNames)
                            {
                                if (columnfl.ColumnName == column.ColumnName && !string.IsNullOrWhiteSpace(columnfl.FilterValue))
                                {
                                    long lgSearch;
                                    decimal dcSearch;
                                    DateTime dtSearch;
                                    string strCondition = "";
                                    if (column.DataType.Name.ToString() == "String")
                                    {
                                        strCondition = column.ColumnName + " Like  '%" + columnfl.FilterValue + "%'";
                                    }
                                    else if (column.DataType.Name.ToString() == "Int64" && long.TryParse(columnfl.FilterValue, out lgSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }
                                    else if (column.DataType.Name.ToString() == "Decimal" && decimal.TryParse(columnfl.FilterValue, out dcSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }
                                    else if (column.DataType.Name.ToString() == "DateTime" && DateTime.TryParse(columnfl.FilterValue, out dtSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }

                                    drDetails.AddRange(dt.Select(strCondition).ToList());

                                    dt.Dispose();
                                    dt = new DataTable();
                                    if (drDetails.Count > 0)
                                    {
                                        dt = drDetails.Distinct().CopyToDataTable();
                                        drDetails.Clear();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        
                        btnexport.Visible = true;
                        if (sortExpression != null)
                        {
                            DataView dv = dt.AsDataView();
                            this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                            dv.Sort = sortExpression + " " + this.SortDirection;
                            lblCount.Text = "Total No Of Candidate : " + Convert.ToString(dv.Count);
                            gView.DataSource = dv;
                        }
                        else
                        {
                            if (dt.Rows[0]["Id"].ToString() == "No Record Found")
                            {
                                lblCount.Text = "Total No Of Candidate : " + "0";
                                btnexport.Visible = false;
                            }
                            else
                            {
                                lblCount.Text = "Total No Of Candidate : " + dt.Rows.Count.ToString();
                                btnexport.Visible = true;

                            }

                            gView.DataSource = dt;
                        }                        
                        Session["GridDataset"] = dt;                       
                        gView.DataSourceID = string.Empty;                       
                        gView.DataBind();
                        

                    }
                    else
                    {
                        gView.DataSourceID = string.Empty;
                        gView.DataSource = null;
                        gView.DataBind();
                        btnexport.Visible = false;
                    }
                }
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            if (ddlColumnList.SelectedIndex != 0 && !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                lstFilterList.Add(new GrdFilter { Row = (lstFilterList.Count + 1), ColumnName = ddlColumnList.SelectedValue, FilterValue = txtSearch.Text });
            }
            BindFilterGridView();
            BindGridViewData();
            isFiltered = true;
            txtSearch.Text = string.Empty;
            ddlColumnList.SelectedIndex = 0;
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                lstFilterList = new List<GrdFilter>();
                BindFilterGridView();
                isFiltered = false;
                this.BindGridViewData();
                txtSearch.Text = string.Empty;
                ddlCourceList.SelectedIndex = 0;
                ddlColumnList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
        }

        private void BindFilterGridView()
        {
            if (lstFilterList.Count > 0)
            {
                gSearchView.DataSource = lstFilterList.OrderBy(x => x.Row);
            }
            else
            {
                gSearchView.DataSource = null;
            }
            gSearchView.DataSourceID = string.Empty;
            gSearchView.DataBind();
        }

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (gView.HeaderRow != null && rowCount == 1)
            //{
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //    foreach (var column in columnNames)
            //    {
            //        TableHeaderCell cell = new TableHeaderCell();
            //        TextBox txtsearch = new TextBox();
            //        txtsearch.Attributes["Placeholder"] = column.ColumnName;
            //        txtsearch.ID = "txt-" + column.ColumnName;
            //        txtsearch.CssClass = "textbox";
            //        txtsearch.AutoPostBack = true;
            //        txtsearch.EnableViewState = true;
            //        txtsearch.TextChanged += new EventHandler(txtsearch_TextChanged);
            //        cell.Controls.Add(txtsearch);
            //        row.Controls.Add(cell);
            //    }

            //    gView.HeaderRow.Parent.Controls.AddAt(1, row);
            //    rowCount++;
            //}            
        }

        protected void gView_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.BindGridViewData(e.SortExpression);
        }

        protected void gSearchView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                Int32 bytID;
                GridViewRow dr = gSearchView.Rows[intIndex];
                bytID = Convert.ToInt32(dr.Cells[1].Text);
                if (e.CommandName == "eDelete")
                {
                    if (lstFilterList.Count > 0)
                    {
                        bool blLoopUpdate = false;
                        List<GrdFilter> tempList = new List<GrdFilter>();
                        foreach (var row in lstFilterList)
                        {
                            if (row.Row == bytID)
                            {
                                blLoopUpdate = true;
                            }
                            else
                            {
                                tempList.Add(row);
                            }
                        }
                        int i = 1;
                        if (blLoopUpdate)
                        {
                            tempList.ForEach(x => { x.Row = i; i++; });
                        }
                        lstFilterList = tempList;
                        BindFilterGridView();
                        BindGridViewData();
                        Functions.MessagePopup(this, "Filter Record Removed successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    else
                    {
                        isFiltered = false;
                        Functions.MessagePopup(this, "Filter Record not have data.", PopupMessageType.error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            long strRandomNo = Convert.ToInt32( Functions.GetRandomNumberString());

            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (StudentGenerateMeritNumberBAL objCandidateDetailsRepository = new StudentGenerateMeritNumberBAL())
                {
                    rowCount = 1;
                    if (ddlCourceList.SelectedIndex == 0)
                    {
                        using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
                        {
                            List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(objStudentAdvertisementBAL.GetAll()).Where(x => x.IsVisible == true).ToList();
                            if (data != null)
                            {
                                if (data.Count > 0)
                                {
                                    ddlCourceList.SelectedIndex = 1;
                                }
                            }
                        }
                    }

                    DataTable Tempdt = new DataTable();
                    DataTable dt = new DataTable();
                    if (ddlCourceList.SelectedIndex != 0)
                    {
                        Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue));

                        columnNames = Tempdt.Columns.Cast<DataColumn>()
                                 .ToList();
                    }
                    else
                    {
                        return;
                    }
                    dt = Tempdt;
                    string[] ColumnsToBeDeleted = { "StudentId", "CourseId", "CasteId", "EduCationTypeId" };

                    //long lgAllrec = dt.AsEnumerable().Count();
                    //if ((dt.AsEnumerable().Where(x => !string.IsNullOrWhiteSpace(x.Field<string>("GroupName"))).Count() <= 0))
                    //{
                    //    ColumnsToBeDeleted.ToList().Add("GroupName");
                    //}

                    foreach (string ColName in ColumnsToBeDeleted)
                    {
                        List<DataColumn> lstColumn = dt.Columns.Cast<DataColumn>().Where(x => x.ColumnName.Contains(ColName)).ToList();
                        foreach (var column in lstColumn)
                        {
                            dt.Columns.Remove(column);
                        }
                    }
                    if (ddlCourceList.SelectedIndex == 0)
                    {
                        columnNames = dt.Columns.Cast<DataColumn>()
                                 .ToList();
                    }
                    if (lstFilterList.Count > 0)
                    {
                        List<DataRow> drDetails = new List<DataRow>();

                        foreach (var columnfl in lstFilterList)
                        {
                            foreach (var column in columnNames)
                            {
                                if (columnfl.ColumnName == column.ColumnName && !string.IsNullOrWhiteSpace(columnfl.FilterValue))
                                {
                                    long lgSearch;
                                    decimal dcSearch;
                                    DateTime dtSearch;
                                    string strCondition = "";
                                    if (column.DataType.Name.ToString() == "String")
                                    {
                                        strCondition = column.ColumnName + " Like  '%" + columnfl.FilterValue + "%'";
                                    }
                                    else if (column.DataType.Name.ToString() == "Int64" && long.TryParse(columnfl.FilterValue, out lgSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }
                                    else if (column.DataType.Name.ToString() == "Decimal" && decimal.TryParse(columnfl.FilterValue, out dcSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }
                                    else if (column.DataType.Name.ToString() == "DateTime" && DateTime.TryParse(columnfl.FilterValue, out dtSearch))
                                    {
                                        strCondition = column.ColumnName + " = '" + columnfl.FilterValue + "'";
                                    }

                                    drDetails.AddRange(dt.Select(strCondition).ToList());

                                    dt.Dispose();
                                    dt = new DataTable();
                                    if (drDetails.Count > 0)
                                    {
                                        dt = drDetails.Distinct().CopyToDataTable();
                                        drDetails.Clear();
                                    }
                                    break;
                                }
                            }
                            objCandidateDetailsRepository.InsertLogg(strRandomNo, true, columnfl.ColumnName, columnfl.FilterValue);
                        }
                    }
                    bool isError = false;
                    string strMessage = "";
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                long lgId = Convert.ToInt32(row["Id"].ToString());
                                if (!objCandidateDetailsRepository.UpdateStudentRegGrpName(lgId, txtGroupName.Text.Trim(), strRandomNo.ToString(), SessionWrapper.UserDetails.UserName))
                                {
                                    txscope.Dispose();

                                    strMessage = "Some Error Record " + lgId;
                                    isError = true;
                                    break;
                                }
                            }
                            txscope.Complete();
                        }
                        catch (Exception ex)
                        {
                            strMessage = ex.Message;
                            txscope.Dispose();
                            isError = true;
                        }
                    }
                    else
                    {
                        isError = true;
                        strMessage = "Filter Data have no Record.";
                    }
                    if (!isError)
                    {
                        Functions.MessagePopup(this, "Assign Group To Filter List successfully.", PopupMessageType.success);

                    }
                    else
                    {
                        Functions.MessagePopup(this, strMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void ddlCourceList_SelectedIndexChanged(object sender, EventArgs e)
        {
                this.BindGridViewData();
         
        }


        protected void btnexport_ServerClick(object sender, EventArgs e)
        {

            if (Session["GridDataset"] != null)
            {
                DataTable dt = (DataTable)Session["GridDataset"];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Remove("id");
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