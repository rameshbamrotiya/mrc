using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public class ColumnList
    {
        public long SequanceNo { get; set; }
        public string ColumnName { get; set; }
        public string TypeColumn { get; set; }
    }

    public partial class StatisticsChartMaster : System.Web.UI.Page
    {
        #region Page Variable
        public static bool firstColumnMove { get; set; }

        public List<GetAllStatisticsChartMasterColumnListByChartIdResult> StatisticsChartColumnListModelList
        {
            get
            {
                object value = ViewState["StatisticsChartColumnListModelList"];
                return value == null ? new List<GetAllStatisticsChartMasterColumnListByChartIdResult>() : (List<GetAllStatisticsChartMasterColumnListByChartIdResult>)value;
            }
            set
            {
                ViewState["StatisticsChartColumnListModelList"] = value;
            }
        }

        public List<GetAllStatisticsChartMasterDetailsByChartIdResult> StatisticsChartDataModelList
        {
            get
            {
                object value = ViewState["StatisticsChartDataModelList"];
                return value == null ? new List<GetAllStatisticsChartMasterDetailsByChartIdResult>() : (List<GetAllStatisticsChartMasterDetailsByChartIdResult>)value;
            }
            set
            {
                ViewState["StatisticsChartDataModelList"] = value;
            }
        }

        public System.Data.DataTable StatisticsChartDetailsModelList
        {
            get
            {
                object value = ViewState["StatisticsChartDetailsModelList"];
                return value == null ? null : (System.Data.DataTable)value;
            }
            set
            {
                ViewState["StatisticsChartDetailsModelList"] = value;
            }
        }

        #endregion

        #region Page Main Functions

        private void BindMainGridView(string strSearch = "")
        {
            using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                var GridData = objIStatisticsChartRepository.GetAllStatisticsChart().ToList();
                if (!string.IsNullOrWhiteSpace(strSearch))
                {
                    var data = GridData.Where(x => x.ChartName.Contains(strSearch)).ToList();

                    GridData.Clear();

                    if (data.Count() > 0)
                    {
                        GridData.AddRange(data);
                    }
                }
                if (GridData.Count() > 0)
                {
                    gvDetails.DataSource = GridData;
                }
                else
                {
                    gvDetails.DataSource = null;
                }
                gvDetails.DataBind();
            }
        }

        private void ClearFormDetails()
        {
            StatisticsChartColumnListModelList = new List<GetAllStatisticsChartMasterColumnListByChartIdResult>();
            StatisticsChartDetailsModelList = null;

            hfID.Value = "0";
            txtChartName.Text = "";
            ddlChartType.SelectedIndex = 0;
            BindMainGridView();
            ClearChartColumnForm();
            ClearChartData();
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearFormDetails();
                    firstColumnMove = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    firstColumnMove = false;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearFormDetails();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private bool LoadMainControl(GetAllStatisticsChartMasterResult objBo)
        {
            bool isError = false;
            if (!string.IsNullOrEmpty(txtChartName.Text))
            {
                objBo.ChartName = txtChartName.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Chart Name.", PopupMessageType.error);
                return isError;
            }

            if (ddlChartType.SelectedIndex > 0)
            {
                objBo.ChartType = ddlChartType.SelectedValue;
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Chart Type.", PopupMessageType.error);
                return isError;
            }

            if (ddlChartType.SelectedValue == "LineChart" || ddlChartType.SelectedValue == "ColumnChartswithMultipleAxes")
            {
                if (string.IsNullOrWhiteSpace(txtXDateFormate.Text))
                {
                    Functions.MessagePopup(this, "Please Enter X Column date Formate.", PopupMessageType.error);
                    return isError;
                }
                else
                {
                    objBo.XValueFormate = txtXDateFormate.Text;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtXColumnChartName.Text))
            {
                objBo.XValueName = txtXColumnChartName.Text;
            }

            if (!string.IsNullOrWhiteSpace(txtYColumnChartName.Text))
            {
                objBo.YValueName = txtYColumnChartName.Text;
            }

            if (StatisticsChartColumnListModelList.OrderBy(x => x.SequanceNo).FirstOrDefault().TypeColumn != "X")
            {
                Functions.MessagePopup(this, "First Record must be X Type Column.", PopupMessageType.error);
                return isError;
            }

            if (StatisticsChartColumnListModelList.Where(x => x.TypeColumn == "X").Count() <= 0)
            {
                Functions.MessagePopup(this, "Please Chart X type Column need least 1 Column.", PopupMessageType.error);
                return isError;
            }

            if (StatisticsChartColumnListModelList.Count() <= 1)
            {
                Functions.MessagePopup(this, "Please Chart at least 2 Column.", PopupMessageType.error);
                return isError;
            }
            if (StatisticsChartColumnListModelList.Where(x => x.TypeColumn == "Y").Count() <= 0)
            {
                Functions.MessagePopup(this, "Please Chart Y type Column need least 1 Column.", PopupMessageType.error);
                return isError;
            }

            if (StatisticsChartDetailsModelList.Rows.Count == 0)
            {
                Functions.MessagePopup(this, "Please Chart Data Must Have add 1 records.", PopupMessageType.error);
                return isError;
            }

            objBo.IsActive = chkActive.Checked;

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;

            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }
            return true;
        }

        #endregion

        #region Page Main Methods

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = gvDetails.DataKeys[rowindex]["Id"].ToString();

            using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objIStatisticsChartRepository.GetStatisticsChartById((Convert.ToInt32(hfID.Value)));
                if (dataInfo != null)
                {
                    txtXDateFormate.Text = dataInfo.XValueFormate;
                    txtXColumnChartName.Text = dataInfo.XValueName;
                    txtYColumnChartName.Text = dataInfo.YValueName;
                    chkActive.Checked = dataInfo.IsActive==null? false : ((bool)dataInfo.IsActive) ;
                    StatisticsChartColumnListModelList = objIStatisticsChartRepository.GetAllStatisticsChartColumnListByChartId(dataInfo.Id).ToList();
                    StatisticsChartDetailsModelList = null;

                    ClearChartColumnForm();
                    ClearChartData();

                    StatisticsChartDataModelList = objIStatisticsChartRepository.GetAllStatisticsChartDetailsByChartId(dataInfo.Id).ToList();

                    BindDataTableOfChartData();
                    BindGridChartData();
                    txtChartName.Text = dataInfo.ChartName;
                    ddlChartType.SelectedValue = dataInfo.ChartType;
                    ddlChartType.Enabled = false;
                    if (ddlChartType.SelectedIndex > 0)
                    {
                        if (ddlChartType.SelectedValue == "LineChart" || ddlChartType.SelectedValue == "ColumnChartswithMultipleAxes")
                        {
                            dvChartXFormate.Visible = true;
                        }
                        else
                        {
                            dvChartXFormate.Visible = false;
                        }
                    }
                    else
                    {
                        dvChartXFormate.Visible = false;
                    }

                    ShowHideControl(VisibityType.Edit);
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
                using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
                {
                    if (!objIStatisticsChartRepository.RemoveStatisticsChart(rowId, out errorMessage))
                    {

                        ClearFormDetails();

                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvDetails.DataKeys[rowindex]["Id"].ToString());
                string strHtml = "";
                string strChart = Functions.GenerateScriptGraph(rowId, chartContainer.ClientID,out strHtml, false);

                if (!string.IsNullOrWhiteSpace(strChart))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Chart", "<script>"+ strChart + "</script>", false);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindMainGridView();
            gvDetails.PageIndex = e.NewPageIndex;
            gvDetails.DataBind();
        }


        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ClearFormDetails();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (StatisticsChartColumnListModelList.OrderBy(x => x.SequanceNo).Count(x => x.TypeColumn == "X") <= 0)
            {
                Functions.MessagePopup(this, "First Record must be X Type Column.", PopupMessageType.error);
                return;
            }
            if (!(ddlChartType.SelectedValue == "LineChart" || ddlChartType.SelectedValue == "ColumnChartswithMultipleAxes"))
            {
                if (StatisticsChartColumnListModelList.OrderBy(x => x.SequanceNo).Count(x => x.TypeColumn == "X") <= 0)
                {
                    Functions.MessagePopup(this, "Allow ONly One Y Column.", PopupMessageType.error);
                    return;
                }
            }
            //using (var transaction = new TransactionScope())
            {
                try
                {

                    string errorMessage = "";
                    using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
                    {
                        GetAllStatisticsChartMasterResult objBO = new GetAllStatisticsChartMasterResult();
                      
                        if (LoadMainControl(objBO))
                        {
                            if (!objIStatisticsChartRepository.InsertOrUpdateStatisticsChart(objBO, out errorMessage))
                            {
                                string strMessage = errorMessage;

                                #region Chart Column List

                                if (objIStatisticsChartRepository.GetAllStatisticsChartColumnListByChartId(objBO.Id).Count() > 0)
                                {
                                    if (objIStatisticsChartRepository.RemoveStatisticsChartColumnListByChartId(objBO.Id, out errorMessage))
                                    {
                                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                        return;
                                    }
                                }

                                if (StatisticsChartDetailsModelList.Columns.Count > 0)
                                {
                                    int index = 1;
                                    foreach (DataColumn row in StatisticsChartDetailsModelList.Columns)
                                    {
                                        StatisticsChartColumnListModel objColModel = new StatisticsChartColumnListModel();

                                        objColModel = new StatisticsChartColumnListModel();
                                        objColModel.SequanceNo = index;
                                        objColModel.Id = 0;
                                        if (index < 3)
                                        {
                                            index++;
                                            continue;
                                        }
                                        else if(index == 3)
                                        {
                                            objColModel.TypeColumn = "X";
                                        }                                         
                                        else
                                        {
                                            objColModel.TypeColumn = "Y";
                                        }
                                        objColModel.ColName = row.ColumnName.ToString();
                                        objColModel.ChartId = objBO.Id;

                                        if (objIStatisticsChartRepository.InsertOrUpdateStatisticsChartColumnList(objColModel, out errorMessage))
                                        {
                                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                            return;
                                        }
                                        index++;
                                    }
                                }

                                #endregion

                                var columnData = objIStatisticsChartRepository.GetAllStatisticsChartColumnListByChartId(objBO.Id).ToList();
                                if (columnData != null)
                                {
                                    #region Chart Data List

                                    if (objIStatisticsChartRepository.GetAllStatisticsChartDetailsByChartId(objBO.Id).Count() > 0)
                                    {
                                        if (objIStatisticsChartRepository.RemoveStatisticsChartDetailByChartId(objBO.Id, out errorMessage))
                                        {
                                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                            return;
                                        }
                                    }

                                    if (StatisticsChartDetailsModelList.Rows.Count > 0)
                                    {
                                        int index = 0;
                                        foreach (DataRow row in StatisticsChartDetailsModelList.Rows)
                                        {
                                            long sequanceNo = 0;
                                            int indexCol = 1;
                                            foreach (DataColumn column in StatisticsChartDetailsModelList.Columns)
                                            {

                                                if (indexCol<3)
                                                {
                                                    indexCol++;
                                                    continue;
                                                }


                                                StatisticsChartDetailsModel objRowModel = new StatisticsChartDetailsModel();

                                                var columnModel = columnData.Where(x => x.ColName == column.ColumnName).FirstOrDefault();

                                                objRowModel.SequanceNo = index;
                                                objRowModel.Id = 0;
                                                objRowModel.ColumnId = columnModel.Id;
                                                objRowModel.ColumnName = column.ColumnName;
                                                if (columnModel.TypeColumn=="X")
                                                {
                                                    string strAliasName = row["Alias Name"].ToString();
                                                    objRowModel.AliasName = strAliasName;
                                                }
                                                objRowModel.ColumnValue = row[column.ColumnName].ToString();

                                                objRowModel.ChartId = objBO.Id;

                                                if (objIStatisticsChartRepository.InsertOrUpdateStatisticsChartDetails(objRowModel, out errorMessage))
                                                {
                                                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                                    return;
                                                }


                                                indexCol++;
                                            }
                                            index++;
                                        }
                                    }

                                    #endregion
                                }


                                Functions.MessagePopup(this, strMessage, PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                            ClearFormDetails();
                            ShowHideControl(VisibityType.GridView);
                        }
                        else
                        {
                            BindGridChartData();
                            BindGridChartColumnForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearFormDetails();
                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindMainGridView(txtSearch.Text);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindMainGridView();
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        #endregion

        #region Chart Column

        #region Functions 
        private void ClearChartColumnForm()
        {
            hfColumnId.Value = "0";
            txtColumnName.Text = "";
            ddlColumnChartType.SelectedIndex = 0;

            BindGridChartColumnForm();

            foreach (ListItem row in ddlColumnChartType.Items)
            {
                if (row.Text == "X")
                {
                    var ifXRow = StatisticsChartColumnListModelList.Where(x => x.TypeColumn == "X").Count();
                    if (ifXRow > 0)
                    {
                        ddlColumnChartType.Items.Remove(row);
                        break;
                    }
                    else
                    {
                        ddlColumnChartType.Items.Clear();
                        ddlColumnChartType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Column Chart Type", null));
                        ddlColumnChartType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("X", "X"));
                        ddlColumnChartType.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Y", "Y"));
                        break;
                    }
                }
            }

            ClearChartData();
        }

        private void BindGridChartColumnForm()
        {
            if (StatisticsChartColumnListModelList.Count() > 0)
            {
                gvColumnChart.DataSource = StatisticsChartColumnListModelList;
            }
            else
            {
                gvColumnChart.DataSource = null;
            }
            gvColumnChart.DataBind();

            int rowId = 0;
            foreach(GridViewRow gRow in gvColumnChart.Rows)
            {
                if (rowId != (gvColumnChart.Rows.Count - 1))
                {
                    (this.gvColumnChart.Rows[rowId].FindControl("ibtnColumnDelete") as LinkButton).Visible = false;
                }
                rowId++;
            }
            //gvColumnChart.DataBind();

        }

        private bool LoadChartColumnControl(GetAllStatisticsChartMasterColumnListByChartIdResult objBo)
        {
            bool isError = false;

            if (string.IsNullOrWhiteSpace(hfColumnId.Value) || hfColumnId.Value == "0")
            {
                objBo.SequanceNo = StatisticsChartColumnListModelList.Count() + 1;
            }
            else
            {
                objBo.SequanceNo = Convert.ToInt32(hfColumnId.Value);
            }

            if (!string.IsNullOrEmpty(txtColumnName.Text))
            {
                objBo.ColName = txtColumnName.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Chart Name.", PopupMessageType.error);
                return isError;
            }

            if (ddlColumnChartType.SelectedIndex > 0)
            {
                objBo.TypeColumn = ddlColumnChartType.SelectedValue;
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Type Column.", PopupMessageType.error);
                return isError;
            }
            if (objBo.TypeColumn == "Y" && StatisticsChartColumnListModelList.Count() > 0)
            {
                if (StatisticsChartColumnListModelList.OrderBy(x => x.SequanceNo).Count(x => x.TypeColumn == "X") <= 0)
                {
                    Functions.MessagePopup(this, "First Record must be X Type Column.", PopupMessageType.error);
                    return isError;
                }
                if (!(ddlChartType.SelectedValue == "LineChart" || ddlChartType.SelectedValue == "ColumnChartswithMultipleAxes"))
                {
                    if (StatisticsChartColumnListModelList.OrderBy(x => x.SequanceNo).Count(x => x.TypeColumn == "Y")+1 > 1)
                    {
                        Functions.MessagePopup(this, "Allow Only One Y Column.", PopupMessageType.error);
                        return isError;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Methods

        protected void btnSubmitColumn_Click(object sender, EventArgs e)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    GetAllStatisticsChartMasterColumnListByChartIdResult objBO = new GetAllStatisticsChartMasterColumnListByChartIdResult();
                    string errorMessage = "";
                    if (LoadChartColumnControl(objBO))
                    {
                        var row = StatisticsChartColumnListModelList.Where((x, i) => x.SequanceNo == Convert.ToInt32(hfColumnId.Value)).FirstOrDefault();
                        if (row != null)
                        {
                            StatisticsChartColumnListModelList.Remove(row);
                        }

                        StatisticsChartColumnListModelList.Add(objBO);

                        ClearChartColumnForm();
                        btnSubmitColumn.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearChartColumnForm();
        }

        protected void ibtnColumnDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;

            StatisticsChartColumnListModelList.RemoveAt(rowindex);

            BindGridChartColumnForm();
        }

        #endregion

        #endregion

        #region Chart Data 

        #region Methods

        protected void btnSubmitData_Click(object sender, EventArgs e)
        {
            //if (ddlChartType.SelectedValue == "LineChart")
            {
                foreach (RepeaterItem rowList in dlColumnForm.Items)
                {
                    TextBox txtColumnName = (TextBox)rowList.FindControl("txtColumnName");
                    Label lblColumnName = (Label)rowList.FindControl("lblColumnName");


                    HiddenField hfHiddenFieldId = (HiddenField)rowList.FindControl("hfHiddenFieldId");

                    var ColumnModel= StatisticsChartColumnListModelList.Where(x=> x.ColName== lblColumnName.Text).FirstOrDefault();

                    IFormatProvider culture = new CultureInfo("en-US", true);

                    if (ColumnModel.TypeColumn=="Y")
                    {
                        long lgNo;
                        if(!long.TryParse(txtColumnName.Text,out lgNo))
                        {
                            Functions.MessagePopup(this, "Please Enter Y Column value must be number.", PopupMessageType.error);
                            return;
                        }
                    }
                    if (ddlChartType.SelectedValue == "LineChart")
                    {
                        if(string.IsNullOrWhiteSpace(txtXDateFormate.Text))
                        {
                            Functions.MessagePopup(this, "Please Enter X Column date Formate.", PopupMessageType.error);
                            return;
                        }
                        if (ColumnModel.TypeColumn == "X")
                        {
                            try
                            {
                                DateTime lgNo = DateTime.ParseExact(txtColumnName.Text, txtXDateFormate.Text.Replace("Y","y"), culture);
                            }catch(Exception ex)
                            {
                                Functions.MessagePopup(this, "Please Enter X Column value must be date like Formate.", PopupMessageType.error);
                                return;
                            }
                            //if (DateTime.ParseExact(txtColumnName.Text, "yyyy", culture))
                            //{
                            //    Functions.MessagePopup(this, "Please Enter X Column value must be date like Formate.", PopupMessageType.error);
                            //    return;
                            //}
                        }
                    }
                }
            }
            //else
            {
                bool isInsert = true;

                int columnCount = 0;

                DataRow row;
                int rowIndex = 0;
                if (((!string.IsNullOrWhiteSpace(hfRowIndexid.Value) && hfRowIndexid.Value != "Insert") && StatisticsChartDetailsModelList.Rows.Count > 0))
                {
                    rowIndex = Convert.ToInt32(hfRowIndexid.Value);
                    row = StatisticsChartDetailsModelList.Rows[rowIndex];
                    row.BeginEdit();
                    isInsert = false;
                }
                else
                {
                    row = StatisticsChartDetailsModelList.NewRow();
                    rowIndex = StatisticsChartDetailsModelList.Rows.Count + 1;
                    isInsert = true;
                }
                if (rowIndex == 0)
                {
                    rowIndex++;
                }
                row["Sr No"] = rowIndex.ToString();
                foreach (RepeaterItem rowList in dlColumnForm.Items)
                {
                    TextBox txtColumnName = (TextBox)rowList.FindControl("txtColumnName");
                    Label lblColumnName = (Label)rowList.FindControl("lblColumnName");
                    HiddenField hfHiddenFieldId = (HiddenField)rowList.FindControl("hfHiddenFieldId");

                    HiddenField hfTypeColumn = (HiddenField)rowList.FindControl("hfTypeColumn");
                    if(hfTypeColumn.Value=="X")
                    {
                        TextBox txtColumnNameAlias = (TextBox)rowList.FindControl("txtColumnNameAlias");
                        row[lblColumnName.Text] = txtColumnName.Text;
                        row["Alias Name"] = txtColumnNameAlias.Text;
                    }
                    else
                    {

                        row[lblColumnName.Text] = txtColumnName.Text;
                    }

                    row[lblColumnName.Text] = txtColumnName.Text;

                    columnCount++;
                }
                if (!isInsert)
                {
                    row.EndEdit();
                }
                else
                {
                    StatisticsChartDetailsModelList.Rows.Add(row);
                }
                btnSubmitData.Focus();
                ClearChartDataForm();
            }

        }

        protected void btnClearData_Click(object sender, EventArgs e)
        {
            ClearChartDataForm();
        }
        
        protected void ibtnColumnDataEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            GridViewRow row = gvChartData.Rows[rowindex];
            hfRowIndexid.Value = (rowindex ).ToString();
            int columnCount =3;
            foreach (RepeaterItem rowList in dlColumnForm.Items)
            {
                TextBox txtColumnName = (TextBox)rowList.FindControl("txtColumnName");
                Label lblColumnName = (Label)rowList.FindControl("lblColumnName");
                HiddenField hfHiddenFieldId = (HiddenField)rowList.FindControl("hfHiddenFieldId");
                HiddenField hfTypeColumn = (HiddenField)rowList.FindControl("hfTypeColumn");
                if (hfTypeColumn.Value == "X")
                {
                    TextBox txtColumnNameAlias = (TextBox)rowList.FindControl("txtColumnNameAlias");
                    txtColumnNameAlias.Text= row.Cells[2].Text.ToString();
                }
                    hfHiddenFieldId.Value = (rowindex ).ToString();
                txtColumnName.Text = row.Cells[columnCount].Text.ToString();

                columnCount++;
            }
        }

        protected void ibtnColumnDataDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            StatisticsChartDetailsModelList.Rows.RemoveAt(rowindex);

            int SrNo = 0;
            foreach(DataRow row in StatisticsChartDetailsModelList.Rows)
            {
                row[0] = (SrNo+1).ToString();
                SrNo++;
            }

            ClearChartDataForm();
            
        }

        protected void lnkSwapUp_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int nextIndex = rowindex--;
            SwapRows(rowindex, nextIndex);
        }

        private void SwapRows(int rowindex, int nextIndex)
        {

            //rowindex = rowindex - 1;
            //nextIndex = nextIndex - 1;          


            System.Data.DataTable dt = StatisticsChartDetailsModelList;
            if (0 <= nextIndex && 0 <= rowindex && dt.Rows.Count > nextIndex && dt.Rows.Count > rowindex)
            {
                DataRow dtFrom = dt.Rows[rowindex];
                DataRow dtTo = dt.Rows[nextIndex];

                DataRow newRowTo = dt.NewRow();
                // We "clone" the row
                newRowTo.ItemArray = dtFrom.ItemArray;


                DataRow newRowFrom = dt.NewRow();
                // We "clone" the row
                newRowFrom.ItemArray = dtTo.ItemArray;

                dt.Rows.Remove(dtFrom);
                dt.Rows.InsertAt(newRowTo, nextIndex);
                dt.AcceptChanges();

                
                StatisticsChartDetailsModelList = dt;

                int index = 1;
                foreach(DataRow row in StatisticsChartDetailsModelList.Rows)
                {
                    row["Sr No"] = index;
                    index++;
                }

                BindGridChartData();
            }
        }

        protected void lnkSwapDown_Click(object sender, EventArgs e)
        {
            int nextIndex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowindex = nextIndex++;
            SwapRows(rowindex, nextIndex);

        }


        protected void gvChartData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!firstColumnMove)
            {
                GridViewRow row = e.Row;


                List<TableCell> cells = new List<TableCell>();


                foreach (DataControlField column in gvChartData.Columns)
                {
                    // Retrieve first cell
                    TableCell cell = row.Cells[0];

                    // Remove cell
                    row.Cells.Remove(cell);


                    // Add cell to list
                    cells.Add(cell);
                }


                // Add cells
                row.Cells.AddRange(cells.ToArray());
                firstColumnMove = true;
            }
        }

        #endregion

        #region Functions

        private void ClearChartDataForm()
        {

            BindGridChartData();
            BindGridChartColumnForm();
            foreach (RepeaterItem rowItem in dlColumnForm.Items)
            {
                TextBox txtColumnName = (TextBox)rowItem.FindControl("txtColumnName");
                Label lblColumnName = (Label)rowItem.FindControl("lblColumnName");
                HiddenField hfHiddenFieldId = (HiddenField)rowItem.FindControl("hfHiddenFieldId");
                HiddenField hfTypeColumn = (HiddenField)rowItem.FindControl("hfTypeColumn");

                if(hfTypeColumn.Value=="X")
                {
                    TextBox txtColumnNameAlias = (TextBox)rowItem.FindControl("txtColumnNameAlias");
                    txtColumnNameAlias.Text = "";
                }

                txtColumnName.Text = "";
                //lblColumnName.Text = "";
                hfHiddenFieldId.Value = "";
            }


            hfRowIndexid.Value = "Insert";
        }

        public void CreateColumnDataTable()
        {
            var dt = new System.Data.DataTable();

            if (StatisticsChartColumnListModelList.Count() > 0)
            {
                DataColumn column = new DataColumn();
                {
                    column.Caption = "Sr No";
                    column.ColumnName = "Sr No";
                    column.DataType = typeof(String);
                    dt.Columns.Add(column);
                }

                DataColumn columnAlias = new DataColumn();
                {
                    columnAlias.Caption = "Alias Name";
                    columnAlias.ColumnName = "Alias Name";
                    columnAlias.DataType = typeof(String);
                    dt.Columns.Add(columnAlias);
                }

                foreach (var row in StatisticsChartColumnListModelList.Where(x => x.TypeColumn == "X"))
                {
                    DataColumn column1 = new DataColumn();
                    {
                        column1.Caption = row.Id.ToString();
                        column1.ColumnName = row.ColName;
                        column1.DataType = typeof(String);
                        dt.Columns.Add(column1);
                    }
                }


                foreach (var row in StatisticsChartColumnListModelList.Where(x => x.TypeColumn != "X"))
                {
                    DataColumn column1 = new DataColumn();
                    {
                        column1.Caption = row.Id.ToString();
                        column1.ColumnName = row.ColName;
                        column1.DataType = typeof(String);
                        dt.Columns.Add(column1);
                    }
                }
            }
            StatisticsChartDetailsModelList = dt;
        }

        private void ClearChartData()
        {
            hfRowIndexid.Value = "Insert";
            CreateColumnDataTable();
            BindGridChartData();
            BindGridChartColumnForm();
            foreach (RepeaterItem row in dlColumnForm.Items)
            {
                TextBox txtColumnName = (TextBox)row.FindControl("txtColumnName");
                Label lblColumnName = (Label)row.FindControl("lblColumnName");
                HiddenField hfHiddenFieldId = (HiddenField)row.FindControl("hfHiddenFieldId");
                txtColumnName.Text = "";
                //lblColumnName.Text = "";
                hfHiddenFieldId.Value = "";
            }
        }

        private void BindDataTableOfChartData()
        {
            if (StatisticsChartDataModelList.Count() > 0)
            {
                int index = 1;
                foreach (var row in StatisticsChartDataModelList.Select(x => x.SequanceNo).Distinct().OrderBy(x => x).ToList())
                {

                    var sequanceData = StatisticsChartDataModelList.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

                    DataRow dr = StatisticsChartDetailsModelList.NewRow();

                    dr[0] = index;
                    dr[1] = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").AliasName;
                    dr[2] = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").ColumnValue;

                    int columnIndex = 3;

                    var yColumnData = sequanceData.Where(x => x.TypeColumn != "X").OrderBy(x => x.ColumnId).ToList();


                    foreach (var subRow in yColumnData)
                    {
                        dr[columnIndex] = subRow.ColumnValue;
                        columnIndex++;
                    }

                    StatisticsChartDetailsModelList.Rows.Add(dr);
                    index++;
                }
            }
        }

        private void BindGridChartData()
        {
            List<ColumnList> lstColumnList = new List<ColumnList>();

            if (StatisticsChartColumnListModelList.Count() > 0)
            {
                //lstColumnList.Add(new ColumnList { ColumnName = "Sr No", SequanceNo = 1.ToString() });

                foreach (var row in StatisticsChartColumnListModelList.Where(x => x.TypeColumn == "X"))
                {
                    lstColumnList.Add(new ColumnList { ColumnName = row.ColName, SequanceNo = 1, TypeColumn= row.TypeColumn });

                }
                int RowCount = 2;
                foreach (var row in StatisticsChartColumnListModelList.Where(x => x.TypeColumn != "X"))
                {
                    lstColumnList.Add(new ColumnList { ColumnName = row.ColName, SequanceNo = RowCount, TypeColumn = row.TypeColumn });
                    RowCount++;
                }
            }

            dlColumnForm.DataSource = Functions.ToDataTable(lstColumnList);
            dlColumnForm.DataBind();

            if (StatisticsChartDetailsModelList.Rows.Count > 0)
            {
                gvChartData.DataSource = StatisticsChartDetailsModelList;

                gvChartData.DataBind();
                btnSubmitData.Focus();
                //gvChartData.Columns.Clear();

                //foreach (DataColumn col in StatisticsChartDetailsModelList.Columns)
                //{
                //    BoundField bfield = new BoundField();
                //    bfield.DataField = col.ColumnName;
                //    bfield.HeaderText = col.ColumnName;
                //    gvChartData.Columns.Add(bfield);
                //}

                //TemplateField tempField = new TemplateField();
                //tempField.HeaderText = "Action";
                //gvChartData.Columns.Add(tempField);

                //gvChartData.DataBind();
                //foreach (GridViewRow row in gvChartData.Rows)
                //{
                //    LinkButton ibtnColumnDataEdit = new LinkButton();
                //    ibtnColumnDataEdit.ID = "ibtnColumnDataEdit";
                //    ibtnColumnDataEdit.CausesValidation = false;
                //    ibtnColumnDataEdit.CssClass = "btn btn-sm show-tooltip";
                //    ibtnColumnDataEdit.Text = "<i class=\"fa fa-edit\"></i>";
                //    ibtnColumnDataEdit.OnClientClick += new System.EventHandler(this.ibtnColumnDataEdit_Click);

                //    row.Cells[gvChartData.Columns.Count-1].Controls.Add(ibtnColumnDataEdit);


                //    LinkButton ibtnColumnDataDelete = new LinkButton();
                //    ibtnColumnDataDelete.ID = "ibtnColumnDataDelete";
                //    ibtnColumnDataEdit.CssClass = "btn btn-sm btn-danger show-tooltip";
                //    ibtnColumnDataEdit.OnClientClick = "javascript:return confirm('Are you sure you want to delete this?');";
                //    ibtnColumnDataDelete.CausesValidation = false;
                //    ibtnColumnDataDelete.Text = "<i class=\"fa fa-trash-o\"></i>";
                //    ibtnColumnDataDelete.OnClientClick += new System.EventHandler(this.ibtnColumnDataDelete_Click);

                //    row.Cells[gvChartData.Columns.Count-1].Controls.Add(ibtnColumnDataDelete);
                //}
            }
            else
            {
                gvChartData.DataSource = null;
            }


            gvChartData.DataBind();
            //if (gvChartData.DataSource != null)
            //{
            //    if (gvChartData.Columns.Count > 0)
            //    {

            //        bool isColumnFind = false;

            //        if (columnToMove == null)
            //        {
            //            columnToMove = gvChartData.Columns[0];
            //        }

            //        gvChartData.Columns.Clear();

            //        foreach (DataColumn col in StatisticsChartDetailsModelList.Columns)
            //        {
            //            BoundField bfield = new BoundField();
            //            bfield.DataField = col.ColumnName;
            //            bfield.HeaderText = col.ColumnName;
            //            gvChartData.Columns.Add(bfield);
            //        }

            //        gvChartData.Columns.Insert(gvChartData.Columns.Count, columnToMove);
            //    }
            //}
            //gvChartData.DataBind();
        }

        #endregion

        #endregion

        protected void ddlChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlChartType.SelectedIndex>0)
            {
                if (ddlChartType.SelectedValue == "LineChart" || ddlChartType.SelectedValue == "ColumnChartswithMultipleAxes")
                {
                    dvChartXFormate.Visible = true;
                }
                else
                {
                    dvChartXFormate.Visible = false;
                }
            }
            else
            {
                dvChartXFormate.Visible = false;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                {
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                        if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                        {

                            string col_parent_id = commandArgs[0];
                            string col_menu_level = commandArgs[1];
                            string cmd = commandArgs[2];

                            switch (cmd)
                            {
                                case "up":
                                    SetPageOrder(cmd, col_menu_level, col_parent_id);
                                    break;
                                case "down":
                                    SetPageOrder(cmd, col_menu_level, col_parent_id);
                                    break;

                            }
                            BindMainGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }

        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            string strMesssage = "";
            using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                if (objIStatisticsChartRepository.StatisticsChartMasterSwap(cmd, Convert.ToDecimal(col_menu_level), Convert.ToInt32(col_parent_id), out strMesssage))
                {

                }
            }
        }
    }
}