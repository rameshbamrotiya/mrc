using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Data.Hospital;
using System.Collections.Generic;
using System.Linq;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class EMCSMaster : System.Web.UI.Page
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
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.Edit);
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();
                    StatisticsDropdownFill();
                    AddStatistics();
                    ClearControlValues(pnlEntry);
                    if (FillControls(1, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        ShowHideControl(VisibityType.Edit);
                    }
                    //CreateTempTable();
                }
                //}
                //else
                //{
                //    Response.Redirect("~/CMS/LoginPage.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        #endregion

        #region GridView Operation

        private bool FillControls(Int32 iPkId, int languageId)
        {
            EMCSBO objBo = new EMCSBO();
            objBo.EId = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new EMCSBAL().SelectRecord(objBo);
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            if (dr["EMCSName"] != DBNull.Value)
                txtemcsname.Text = Convert.ToString(dr["EMCSName"]);
            if (dr["EMCSDescription"] != DBNull.Value)
                txtemcsdesc.Text = Convert.ToString(dr["EMCSDescription"]);
            if (dr["IsVisible"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToString(dr["IsVisible"]);
            if (dr["IsStatistics"] != DBNull.Value)
                ddlstatistics.SelectedValue = Convert.ToString(dr["IsStatistics"]);
            if (dr["StatisticsId"] != DBNull.Value)
            {
                if (ddlstatisticsname.Items.FindByValue(dr["StatisticsId"].ToString()) != null)
                {
                    ddlstatisticsname.SelectedValue = dr["StatisticsId"].ToString();
                }
            }
            if (dr["EMCSLevelId"] != DBNull.Value)
                txtsequenceno.Text = Convert.ToString(dr["EMCSLevelId"]);
            if (dr["EId"] != DBNull.Value)
                hfId.Value = dr["EId"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow drinner = ds.Tables[1].Rows[0];
                if (drinner.HasErrors) return false;
                ViewState["TblImages"] = ds.Tables[1];
                BindInnerGrid();
            }
            AddStatistics();
            return true;
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(EMCSBO objBo)
        {
            if (!string.IsNullOrEmpty(txtemcsname.Text))
            {
                objBo.EMCSName = txtemcsname.Text;
            }
            else
            {
                objBo.EMCSName = "";
            }
            objBo.EMCSDescription = txtemcsdesc.Text;
            objBo.IsStatistics = ddlstatistics.SelectedValue.ToString();
            objBo.StatisticsId = ddlstatisticsname.SelectedValue.ToString();
            objBo.EMCSLevelId = txtsequenceno.Text;
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                AddStatistics();
                DataSet ds = new EMCSBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["EMCSLevelId"] != DBNull.Value)
                    txtsequenceno.Text = drs["EMCSLevelId"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }


        private void CreateTempTable()
        {
            //creating dataTable   
            DataTable dt = new DataTable();
            dt.TableName = "Images";
            dt.Columns.Add(new DataColumn("ImgURL", typeof(string)));
            //saving databale into viewstate   
            ViewState["TblImages"] = dt;
            //bind Gridview  
            BindInnerGrid();
        }


        private void AddToTable()
        {
            // check view state is not null  
            if (ViewState["TblImages"] != null)
            {
                //get datatable from view state   
                DataTable dtCurrentTable = (DataTable)ViewState["TblImages"];
                DataRow dr = dtCurrentTable.NewRow();
                bool isError = false;


                if (fuImg.HasFile)
                {
                    if (dtCurrentTable.Rows.Count < Convert.ToInt32(hdnLmt.Value))
                    {

                        string filepath = string.Empty;
                        filepath = SaveFile(out isError);

                        if (!string.IsNullOrEmpty(filepath) && !isError)
                        {
                            dr["ImgURL"] = filepath;
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Maximum "+ hdnLmt.Value + " images can be upload", PopupMessageType.error);
                        return;
                    }
                }
                else
                {
                    dr["ImgURL"] = "";
                }

                //add created Rows into dataTable  
                dtCurrentTable.Rows.Add(dr);
                //Save Data table into view state after creating each row  
                ViewState["TblImages"] = dtCurrentTable;
                //Bind Gridview with latest Row  
                BindInnerGrid();

            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlstatistics.SelectedValue == "True")
                {
                    if (ddlstatisticsname.SelectedIndex <= 0)
                    {
                        Functions.MessagePopup(this, "Please select statistics.", PopupMessageType.warning);
                        return;
                    }
                }

                EMCSBO objBo = new EMCSBO();
                LoadControls(objBo);
                if (new EMCSBAL().InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlstatistics.SelectedValue == "True")
                {
                    if (ddlstatisticsname.SelectedIndex <= 0)
                    {
                        Functions.MessagePopup(this, "Please select statistics.", PopupMessageType.warning);
                        return;
                    }
                }
                EMCSBO objBo = new EMCSBO();
                LoadControls(objBo);
                objBo.EId = Convert.ToInt32(hfId.Value);
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["TblImages"];
                if (new EMCSBAL().UpdateRecord(objBo,dt))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                ShowHideControl(VisibityType.Edit);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {

                ShowHideControl(VisibityType.Edit);
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlEntry.Visible = false;
                    break;
            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void AddStatistics()
        {
            if (ddlstatistics.SelectedValue == "True")
            {
                Statisticsname.Visible = true;
                StatisticsDropdownFill();
            }
            else
            {
                Statisticsname.Visible = false;
            }
        }
        private void StatisticsDropdownFill()
        {
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                List<GetAllStatisticsChartMasterResult> Data = objPatientsEducationBrochureRepository.GetAllStatisticsChart();
                PopulateDropDownList(ddlstatisticsname, Functions.ToDataTable(Data), "ChartName", "Id", true);
            }
        }

        protected void ddlstatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddStatistics();
        }

        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new EMCSBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        private string SaveFile(out bool isError)
        {
            isError = false;
            try
            {
                if (fuImg.HasFile)
                {
                    var DocPath = ConfigDetailsValue.FacilityInEMCS;
                    string fileMimeType = fuImg.PostedFile.ContentType;

                    var fname = Path.GetExtension(fuImg.FileName);
                    var count = fuImg.FileName.Split('.');
                    int Extensioncount = fuImg.FileName.Count(f => f == '.');
                    var ValidFileTypes = new[] { "jpg", "jpeg", "png" };

                    string[] matchMimeType = { "application/jpeg", "application/jpg", "application/png" };

                    if (!ValidFileTypes.Contains(fname.Substring(1).ToLower()) && !matchMimeType.Contains(fileMimeType) && Extensioncount == 1)
                    {
                        Functions.MessagePopup(this, "File Extension Is InValid - Only Upload JPEG/JPG/PNG File.", PopupMessageType.error);
                        isError = true;
                        return "";
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuImg.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(DocPath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocPath));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocPath + filename1;
                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocPath + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fuImg.SaveAs(Server.MapPath(DocPath) + filename1);
                        string NewFIlePath = ((DocPath) + filename1).ToString();
                        return NewFIlePath;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }

        protected void gvImg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvImg.PageIndex = e.NewPageIndex;
            BindInnerGrid();
        }

        protected void gvImg_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable dtdel = new DataTable();
                dtdel = (DataTable)ViewState["TblImages"];
                int index = e.RowIndex + gvImg.PageIndex * gvImg.PageSize;
                dtdel.Rows[index].Delete();
                dtdel.AcceptChanges();
                ViewState["TblImages"] = dtdel;
                BindInnerGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void BindInnerGrid()
        {
            gvImg.DataSource = (DataTable)ViewState["TblImages"];
            gvImg.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["TblImages"] == null)
                {
                    CreateTempTable();
                }

                AddToTable();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
    }
}