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

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class GenerateMeritNumberSeries : System.Web.UI.Page
    {
        public class GrdFilter
        {
            public int Sequance { get; set; }
            public string ColumnName { get; set; }
            public string Orderby { get; set; }
        }

        string[] ColumnsToBeDeleted = { "StudentId", "CourseId", "CasteId", "EduCationTypeId" };
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
                    BindPageViewData();
                    isFiltered = false;
                    this.BindGridViewData();
                    BindColumnName();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Sort_ServerClick(object sender, EventArgs e)
        {
            if (ddlColumnList.SelectedIndex != 0)
            {
                lstFilterList.Add(new GrdFilter { Sequance = (lstFilterList.Count + 1), ColumnName = ddlColumnList.SelectedValue, Orderby = ddlSortOrderBy.SelectedValue });
            }
            BindFilterGridView();
            BindGridViewData();
            isFiltered = true;

            ddlColumnList.SelectedIndex = 0;
            ddlSortOrderBy.SelectedIndex = 0;
        }

        protected void btn_SortCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                lstFilterList = new List<GrdFilter>();
                BindFilterGridView();
                this.BindGridViewData();
                ddlSortOrderBy.SelectedIndex = 0;
                isFiltered = false;
                ddlCourceList.SelectedIndex = 0;
                ddlColumnList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
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
                            if (row.Sequance == bytID)
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
                            tempList.ForEach(x => { x.Sequance = i; i++; });
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
            long strRandomNo = Convert.ToInt32(DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + Functions.GetRandomNumberString());
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (StudentGenerateMeritNumberBAL objCandidateDetailsRepository = new StudentGenerateMeritNumberBAL())
                {
                    DataTable Tempdt = new DataTable();
                    DataTable dt = new DataTable();
                    if (ddlCourceList.SelectedIndex != 0)
                    {
                        if (columnNames == null)
                        {
                            Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue));

                            columnNames = Tempdt.Columns.Cast<DataColumn>()
                                     .ToList();
                        }
                        string strCondition = "";
                        if (lstFilterList.Count > 0)
                        {
                            List<DataRow> drDetails = new List<DataRow>();

                            long lgSearch = 0;
                            foreach (var columnfl in lstFilterList)
                            {
                                foreach (var column in columnNames)
                                {
                                    if (columnfl.ColumnName == column.ColumnName && !string.IsNullOrWhiteSpace(columnfl.Orderby))
                                    {
                                        if (lgSearch != 0)
                                        {
                                            strCondition += ", " + column.ColumnName + " " + columnfl.Orderby + "";
                                        }
                                        else
                                        {
                                            strCondition = column.ColumnName + " " + columnfl.Orderby + "";
                                        }
                                        lgSearch++;
                                        objCandidateDetailsRepository.InsertLogg(strRandomNo, false, columnfl.ColumnName, columnfl.Orderby);
                                        break;
                                    }
                                }
                            }
                        }
                        Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue), strCondition);
                    }
                    else
                    {
                        return;
                    }
                    dt = Tempdt;

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

                    bool isError = false;
                    string strMessage = "";
                    if (dt.Rows.Count > 0 && dt != null)
                    {

                        try
                        {
                            int i = 1;

                            var dataRow = dt.AsEnumerable().Where(x => x.Field<long?>("MeritNo") == null && !string.IsNullOrWhiteSpace(x.Field<string>("GroupName"))).ToArray().CopyToDataTable();
                            foreach (DataRow row in dataRow.Rows)
                            {
                                long lgId = Convert.ToInt32(row["Id"].ToString());
                                if (!objCandidateDetailsRepository.UpdateStudentRegMeritNumber(lgId, i, strRandomNo.ToString(), SessionWrapper.UserDetails.UserName))
                                {
                                    txscope.Dispose();

                                    strMessage = "Some Error Record " + lgId;
                                    isError = true;
                                    break;
                                }
                                i++;
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
                        Functions.MessagePopup(this, "Assign Display To Sequance No successfully.", PopupMessageType.success);

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
            BindGridViewData();
        }
        private void BindFilterGridView()
        {
            if (lstFilterList.Count > 0)
            {
                gSearchView.DataSource = lstFilterList.OrderBy(x => x.Sequance);
            }
            else
            {
                gSearchView.DataSource = null;
            }
            gSearchView.DataSourceID = string.Empty;
            gSearchView.DataBind();
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
                        if (columnNames == null)
                        {
                            Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue));

                            columnNames = Tempdt.Columns.Cast<DataColumn>()
                                     .ToList();
                        }
                        string strCondition = "";
                        if (lstFilterList.Count > 0)
                        {
                            List<DataRow> drDetails = new List<DataRow>();

                            long lgSearch = 0;
                            foreach (var columnfl in lstFilterList)
                            {
                                foreach (var column in columnNames)
                                {
                                    if (columnfl.ColumnName == column.ColumnName && !string.IsNullOrWhiteSpace(columnfl.Orderby))
                                    {
                                        if (lgSearch != 0)
                                        {
                                            strCondition += ", " + column.ColumnName + " " + columnfl.Orderby + "";
                                        }
                                        else
                                        {
                                            strCondition = column.ColumnName + " " + columnfl.Orderby + "";
                                        }
                                        lgSearch++;
                                        break;
                                    }
                                }
                            }
                        }
                        Tempdt = objCandidateDetailsRepository.GetAllStudentForGenerateMeritNumbers(Convert.ToInt64(ddlCourceList.SelectedValue), strCondition);
                    }
                    else
                    {
                        return;
                    }
                    dt = Tempdt;

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

                    //foreach (DataRow dr in dt.Rows)
                    //{

                    //    foreach (DataColumn clm in dt.Columns)
                    //    {
                    //        if (dr[clm] == DBNull.Value)
                    //        {
                    //            if (clm.DataType.Name.ToString() == "String")
                    //            {
                    //                dr[clm] = "";
                    //            }
                    //            else if (clm.DataType.Name.ToString().Contains("Int"))
                    //            {
                    //                dr[clm] = 0;
                    //            }
                    //            else if (clm.DataType.Name.ToString() == "Decimal")
                    //            {
                    //                dr[clm] = 0;
                    //            }
                    //        }
                    //    }
                    //}
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        //if (!String.IsNullOrWhiteSpace(strCondition))
                        //{
                        //    DataView dv = dt.AsDataView();
                        //    //this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                        //    //dv.Sort = strCondition;
                        //    lblCount.Text = "Total No Of Candidate : " + Convert.ToString(dv.Count);
                        //    gView.DataSource = dt;
                        //}
                        //else
                        var dataRow =  dt.AsEnumerable().Where(x => x.Field<long?>("MeritNo") == null && !string.IsNullOrWhiteSpace(x.Field<string>("GroupName"))).ToArray().CopyToDataTable();
                        {
                            lblCount.Text = "Total No Of Candidate : " + Convert.ToString(dataRow.Rows.Count);
                            gView.DataSource = dataRow;
                        }
                        gView.DataSourceID = string.Empty;
                        gView.DataBind();

                    }
                    else
                    {
                        gView.DataSourceID = string.Empty;
                        gView.DataSource = null;
                        gView.DataBind();
                    }
                }
            }
        }

        private void BindColumnName()
        {
            if (columnNames.Count > 0)
            {
                List<ListItem> lstItem = new List<ListItem>();
                columnNames.Where(x => !ColumnsToBeDeleted.Contains(x.ColumnName)).ToList().ForEach(x => { lstItem.Add(new ListItem { Text = x.ColumnName, Value = x.ColumnName }); });
                ddlColumnList.DataSource = lstItem;
                ddlColumnList.DataBind();
                ddlColumnList.DataTextField = "Text";
                ddlColumnList.DataValueField = "Value";
                ddlColumnList.Items.Insert(0, new ListItem("Select Column", "0"));
            }
        }

    }
}