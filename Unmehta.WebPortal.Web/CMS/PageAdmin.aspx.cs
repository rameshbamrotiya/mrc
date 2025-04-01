using System;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using static Unmehta.WebPortal.Model.Common.EnumClass;

using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using Unmehta.WebPortal.Web.Common;
using System.IO;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.CMS
{
    public partial class PageAdmin : System.Web.UI.Page
    {
        MenuBO objbo = new MenuBO();
        MenuBAL objBAL = new MenuBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
                FillTemplatetype();
                BindGridView();

            }

        }

        private void BindGridView()

        {

            objbo.col_parent_id = 0;
            DataSet ds = new DataSet();
            objbo.Language = "1";
            ds = objBAL.SelectParentMEnu(objbo);
            DataTable dtparent = new DataTable();
            DataTable dtfinal = new DataTable();
            dtparent = ds.Tables[0];
            if (dtparent.Rows.Count > 0)
            {

                DataTable dtchildmenu = new DataTable();

                dtfinal = dtparent.Clone();
                foreach (DataRow dr in dtparent.Rows)
                {

                    dtfinal.Rows.Add(dr.ItemArray);


                    objbo.col_parent_id = Convert.ToInt16(dr["col_menu_id"].ToString());
                    DataSet dschildmenu = objBAL.SelectParentMEnu(objbo);
                    dtchildmenu = dschildmenu.Tables[0];
                    if (dtchildmenu.Rows.Count > 0)
                    {

                        DataTable dtnestedchild = new DataTable();
                        foreach (DataRow drchild in dtchildmenu.Rows)
                        {


                            drchild["col_menu_name"] = "  -->" + drchild["col_menu_name"];
                            dtfinal.Rows.Add(drchild.ItemArray);
                            objbo.col_parent_id = Convert.ToInt16(drchild["col_menu_id"].ToString());
                            DataSet dsnestedchildmenu = objBAL.SelectParentMEnu(objbo);
                            dtnestedchild = dsnestedchildmenu.Tables[0];
                            if (dtnestedchild.Rows.Count > 0)
                            {
                                foreach (DataRow drnestedchild in dtnestedchild.Rows)
                                {

                                    drnestedchild["col_menu_name"] = "  ---->" + drnestedchild["col_menu_name"];
                                    dtfinal.Rows.Add(drnestedchild.ItemArray);
                                }
                            }


                        }
                    }

                }
            }
            grdParent.DataSource = dtfinal;
            grdParent.DataBind();


        }

        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }

        private void FillTemplatetype()
        {
            DataSet ds = new DataSet();
            TemplateMasterBAL objBAL = new TemplateMasterBAL();
            ds = objBAL.Get_All_TemplateMaster();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpTemplate, dt, "TemplateName", "TemplateId", true);

        }

        private void FillMenutype()
        {
            DataSet ds = new DataSet();
            objbo.Language = drpLanguage.SelectedValue;
            ds = objBAL.SelectMenutype(objbo);
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);

        }

        private void FillControls(string col_menu_id, string languageid)
        {
            try
            {

                objbo.col_menu_id = col_menu_id;
                objbo.Language = languageid.ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectMenubyResource(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtPageName.Text = dr["col_menu_name"].ToString();
                    hfCol_Menu_Level.Value = dr["col_menu_level"].ToString();
                    txtPageUrl.Text = dr["col_menu_url"].ToString();
                    txtTooltip.Text = dr["Tooltip"].ToString();
                    hdnRecid.Value = dr["recid"].ToString();
                    drpTemplate.SelectedValue = dr["Templateid"].ToString();
                    txtMaskingUrl.Text = dr["MaskingURL"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    CKEditorControl1.Text = HttpUtility.HtmlDecode(dr["ContentDet"].ToString());
                    drpMenutype.SelectedValue = dr["col_menu_type"].ToString();
                    hfHederImage.Value = dr["HeaderImage"].ToString();
                    hdnColMenuID.Value = col_menu_id;

                    bool IsDisabledTranslate = false;
                    if (dr["IsDisabledTranslate"] != null)
                    {
                        if (bool.TryParse(dr["IsDisabledTranslate"].ToString(), out IsDisabledTranslate))
                        {
                            IsDisabledTranslate = Convert.ToBoolean(dr["IsDisabledTranslate"].ToString());
                        }
                    }
                    cbIsDisabledTranslate.Checked = IsDisabledTranslate;

                    DataSet ds1 = objBAL.SelectMenutype(objbo);
                    DataTable dt = ds1.Tables[0];
                    PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);

                    if (Convert.ToInt16(dr["col_parent_id"].ToString()) > 0)
                    {
                        drpParentMenu.SelectedValue = dr["col_parent_id"].ToString();
                        drpParentMenu.Enabled = true;
                        drpMenutype.Enabled = false;
                    }
                    else
                    {
                        drpParentMenu.SelectedIndex = 0;
                        drpParentMenu.Enabled = false;

                    }

                }
                else
                {
                    var menuTy = drpMenutype.SelectedValue;
                    ClearControlValues(pnlEntry);
                    DataSet ds1 = objBAL.SelectMenutype(objbo);
                    DataTable dt = ds1.Tables[0];
                    PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);
                    drpMenutype.SelectedValue = menuTy;
                    drpLanguage.SelectedValue = languageid;
                    CKEditorControl1.Text = "";
                }
                FillMenutype();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }


        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMenutype();
            FillControls(hdnColMenuID.Value, drpLanguage.SelectedValue.ToString());
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            if (new MenuBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {

            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }
                LoadControlsAdd(objbo);
                objbo.recid = Convert.ToInt16(hdnRecid.Value);
                objbo.col_menu_id = hdnColMenuID.Value;
                if (new MenuBAL().UpdateRecord(objbo))
                {
                    ShowMessage("Record Updated successfully.", MessageType.Success);
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message.ToString(), MessageType.Error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void LoadControlsAdd(MenuBO objBo)
        {
            if (!string.IsNullOrEmpty(txtPageName.Text))
                objBo.col_menu_name = txtPageName.Text;

            objBo.IsDisabledTranslate = cbIsDisabledTranslate.Checked;

            if (!string.IsNullOrEmpty(txtPageUrl.Text))
                objBo.col_menu_url = txtPageUrl.Text;
            if (drpMenutype.SelectedValue.ToString() == "1")
                objBo.col_parent_id = Convert.ToInt16(drpParentMenu.SelectedValue.ToString());

            else
                objBo.col_parent_id = 0;


            if (fuHeaderImage.HasFile)
            {
                string filePath = ConfigDetailsValue.HeaderImagePath;

                if (!filePath.Contains("|"))
                {
                    //if (fuHeaderImage.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return ;
                    //}
                    objBo.HeaderImage = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuHeaderImage.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + objBo.HeaderImage;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.HeaderImage);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuHeaderImage.SaveAs(Server.MapPath(filePath) + objBo.HeaderImage);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return;

                    }
                }
                else
                {

                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return;
                }
            }
            else
            {
                objBo.HeaderImage = hfHederImage.Value;
            }

            objBo.col_menu_rank = 0;
            objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName;
            objBo.ip_add = GetIPAddress;
            objBo.tooltip = txtTooltip.Text.ToString();
            objBo.Language = drpLanguage.SelectedValue.ToString();
            objBo.templateId = drpTemplate.SelectedValue.ToString();
            objBo.ContentDet = HttpUtility.HtmlEncode(CKEditorControl1.Text.ToString());
            objBo.MaskingURL = txtMaskingUrl.Text;
            objBo.col_menu_type = drpMenutype.SelectedValue.ToString();


        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }
                LoadControlsAdd(objbo);
                if (new MenuBAL().InsertRecord(objbo))
                {
                    ShowMessage("Record inserted successfully.", MessageType.Success);
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
            ClearControlValues(pnlEntry);
            ShowHideControl(VisibityType.GridView);
            txtSearch.Text = string.Empty;
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);

        }

        protected void drpMenutype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMenutype();
            if (drpMenutype.SelectedValue.ToString() == "1")
            {
                drpParentMenu.Enabled = true;


            }
            else
            {
                drpParentMenu.SelectedIndex = 0;
                drpParentMenu.Enabled = false;
            }
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
                    CKEditorControl1.Text = string.Empty;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    drpParentMenu.Enabled = false;
                    drpMenutype.Enabled = true;

                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
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

        private void BindSearchGrid()
        {
            DataSet ds = new DataSet();
            if (txtSearch.Text.Trim() != string.Empty)
            {
                objbo.col_menu_name = txtSearch.Text.ToString();
            }
            ds = objBAL.SearchMenuByName(objbo);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataTable dtfinal = new DataTable();
                DataSet dsparent = new DataSet();
                dtfinal = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {

                    if (Convert.ToInt16(dr["col_parent_id"]) > 0)
                    {
                        objbo.Language = "1";
                        objbo.col_menu_id = dr["col_parent_id"].ToString();
                        ds = objBAL.SelectMenubyResource(objbo);
                        DataTable dtDetails = new DataTable();
                        dtDetails = ds.Tables[0];
                        //DataRow drdetails = ;
                        if (dtDetails.Rows.Count > 0)
                        {
                            DataRow drDetails = dtDetails.Rows[0];
                            if (Convert.ToInt16(drDetails["col_parent_id"]) > 0)
                            {
                                objbo.col_menu_id = drDetails["col_parent_id"].ToString();
                                ds = objBAL.SelectMenubyResource(objbo);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dtfinal.Rows.Add(ds.Tables[0].Rows[0].ItemArray);
                                    drDetails["col_menu_name"] = "  -->" + drDetails["col_menu_name"];
                                    dtfinal.Rows.Add(drDetails.ItemArray);
                                    dr["col_menu_name"] = "    ---->" + dr["col_menu_name"];
                                    dtfinal.Rows.Add(dr.ItemArray);

                                }

                            }
                            else
                            {
                                dtfinal.Rows.Add(drDetails.ItemArray);
                                dr["col_menu_name"] = "  -->" + dr["col_menu_name"];
                                dtfinal.Rows.Add(dr.ItemArray);
                            }

                        }

                    }
                    else
                    {
                        dtfinal.Rows.Add(dr.ItemArray);
                    }
                }

                string[] columnNames = dtfinal.Columns.Cast<DataColumn>()
                                     .Select(x => x.ColumnName)
                                     .ToArray();
                DataView view = new DataView(dtfinal);
                dtfinal = view.ToTable(true, columnNames);

                grdParent.DataSource = dtfinal;
                grdParent.DataBind();

            }
        }

        protected void grdParent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void grdParent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string strCellData = e.Row.Cells[1].Text;

                    if (strCellData.Length > 4)
                    {
                        strCellData = e.Row.Cells[1].Text.Substring(0, 4);
                    }
                    else if (strCellData.Length > 6)
                    {
                        strCellData = e.Row.Cells[1].Text.Substring(0, 6);
                    }

                    if (HttpUtility.HtmlDecode(strCellData) == "  --" && HttpUtility.HtmlDecode(strCellData) != "  --")
                    {
                        //e.Row.Cells[1].Font.Bold = true;
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.DarkBlue;
                    }
                    if (Convert.ToInt16(grdParent.DataKeys[e.Row.RowIndex]["col_menu_type"].ToString()) > 1)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.DarkCyan;
                        LinkButton lnkup = (LinkButton)e.Row.FindControl("lnk_UP");
                        LinkButton lnkdown = (LinkButton)e.Row.FindControl("lnk_Dwn");
                        lnkdown.Visible = false;
                        lnkup.Visible = false;
                    }

                }


            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                hdnRecid.Value = grdParent.DataKeys[rowindex]["recid"].ToString();
                FillControls(grdParent.DataKeys[rowindex]["col_menu_id"].ToString(), grdParent.DataKeys[rowindex]["Languageid"].ToString());
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindSearchGrid();
        }
    }
}