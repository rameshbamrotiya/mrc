using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Unmehta.WebPortal.Web.Common.Functions;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using System.Data;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Data.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class SchemeCharts : System.Web.UI.Page
    {
        public static long schemaId;

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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            if (!IsPostBack)
            {

                string queryString = Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

                long id = 0;
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    if (long.TryParse(queryString, out schemaId))
                    {
                        hfSchemaId.Value = queryString;
                        ShowHideControl(VisibityType.GridView);
                        BindGridView();
                    }
                }
                else
                {
                    Response.Redirect("~/LoginPortal", false);
                }
            }
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if(ddlStatistics.SelectedIndex<=0)
            {
                MessagePopup(this, "Please Select Chart.", PopupMessageType.success);
                return;
            }
            long lgSequanceNo = 0;
            if (!long.TryParse(txtsequence.Text,out lgSequanceNo))
            {
                MessagePopup(this, "Please Enter SequanceNo.", PopupMessageType.success);
                return;
            }
            else if(lgSequanceNo<=0)
            {
                MessagePopup(this, "Please Enter Proper SequanceNo.", PopupMessageType.success);
                return;
            }


            SchemeBAL objBal =new SchemeBAL();
            if (objBal.InsertOrUpdateSchemaChartDetail(0,schemaId, Convert.ToInt32(ddlStatistics.SelectedValue), Convert.ToInt32(txtsequence.Text), SessionWrapper.UserDetails.UserName)>0)
            {
                MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
            }
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            if (ddlStatistics.SelectedIndex <= 0)
            {
                MessagePopup(this, "Please Select Chart.", PopupMessageType.success);
                return;
            }
            long lgSequanceNo = 0;
            if (!long.TryParse(txtsequence.Text, out lgSequanceNo))
            {
                MessagePopup(this, "Please Enter SequanceNo.", PopupMessageType.success);
                return;
            }
            else if (lgSequanceNo <= 0)
            {
                MessagePopup(this, "Please Enter Proper SequanceNo.", PopupMessageType.success);
                return;
            }

            SchemeBAL objBal = new SchemeBAL();
            if (objBal.InsertOrUpdateSchemaChartDetail(Convert.ToInt32(hfID.Value), schemaId, Convert.ToInt32(ddlStatistics.SelectedValue), Convert.ToInt32(txtsequence.Text), SessionWrapper.UserDetails.UserName) > 0)
            {
                MessagePopup(this, "Record update successfully.", PopupMessageType.success);
            }
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {

            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                hfID.Value = grdScheme.DataKeys[rowindex]["Id"].ToString();
                FillControls(Convert.ToInt16(grdScheme.DataKeys[rowindex]["Id"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void FillControls(int Id)
        {
            try
            {
                SchemeBAL objBAL = new SchemeBAL();

                DataTable dtDetails = objBAL.GetAllSchemaChartDetails(schemaId);

                DataRow[] drList = dtDetails.Select("Id = " + Id);


                dtDetails.Dispose();

                dtDetails = drList.CopyToDataTable();

                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];

                    txtsequence.Text = dr["SequanceNo"].ToString();

                    if (!string.IsNullOrWhiteSpace(dr["ChartId"].ToString()))
                    {
                        {
                            if (ddlStatistics.Items.FindByValue(dr["ChartId"].ToString()) != null)
                            {
                                ddlStatistics.SelectedValue = dr["ChartId"].ToString();
                            }
                        }
                    }

                }
                else
                {
                    ClearControlValues(pnlEntry);

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }


        }

        protected void grdScheme_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(grdScheme.DataKeys[intIndex].Values["Schemeid"]);
                    if (e.CommandName == "eDelete")
                    {

                        new SchemeBAL().RemoveSchemaChartDetail(bytID, SessionWrapper.UserDetails.UserName);
                        BindGridView();
                        MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
                else
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
                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }


        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new SchemeBAL().SchemaChartOrders(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        private void BindGridView()
        {

            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(strSqlConnectionString))
            {
                var Data = objPatientsEducationBrochureRepository.GetAllStatisticsChart().ToList();
                PopulateDropDownList(ddlStatistics, ToDataTable(Data), "ChartName", "Id", true);
            }

            DataTable dtDetails = new SchemeBAL().GetAllSchemaChartDetails(schemaId);

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                DataRow[] drList = dtDetails.Select("ChartName = " + txtSearch.Text);

                dtDetails.Dispose();
                dtDetails = drList.CopyToDataTable();
            }

            grdScheme.DataSource = dtDetails;
            grdScheme.DataBind();

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
                    btn_Update.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);

                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }
    }
}