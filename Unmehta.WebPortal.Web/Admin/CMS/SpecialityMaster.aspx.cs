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
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Data.Hospital;
using System.Collections.Generic;
using System.Web;
using System.Drawing;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class SpecialityMaster : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                    StatisticsDropdownFill();
                    LanguageMasterBAL objBo = new LanguageMasterBAL();
                    ddlLanguage.DataSource = objBo.GetAllLanguage();
                    ddlLanguage.DataTextField = "Name";
                    ddlLanguage.DataValueField = "Id";
                    ddlLanguage.DataBind();


                    if (string.IsNullOrWhiteSpace(lblMainImage.Text))
                    {
                        aRemoveMain.Visible = false;
                    }


                    if (string.IsNullOrWhiteSpace(lblIcon.Text))
                    {
                        aRemoveIcon.Visible = false;
                    }

                    if (string.IsNullOrWhiteSpace(lblfilesubimg.Text))
                    {
                        removesubimg.Visible = false;
                    }
                    if (string.IsNullOrWhiteSpace(lblInnerImage.Text))
                    {
                        aRemoveInnerImage.Visible = false;
                    }
                }
                isapply();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        #endregion

        #region GridView Operation
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["OS_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        SpecialityMasterBO objBo = new SpecialityMasterBO();
                        objBo.OS_id = bytID;
                        new SpecialityMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID, Convert.ToInt32(ddlLanguage.SelectedValue)))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            GridView1.Visible = true;
                            ViewState["PK"] = bytID;
                            ShowHideControl(VisibityType.Edit);
                        }
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
            if (new SpecialityMasterBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
            {
            }
        }
        private bool FillControls(Int32 iPkId, int languageId)
        {
            SpecialityMasterBO objBo = new SpecialityMasterBO();
            objBo.OS_id = iPkId;
            objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new SpecialityMasterBAL().SelectRecord(objBo);
            DataSet dsimg = new SpecialityMasterBAL().SelectRecordIMG(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                txtOSTitle.Text = "";
                ddlIsStatistics.SelectedValue = "0";
                txtOSShortDesc.Text = "";
                txtvideolink.Text = "";
                txtosinnerdesc.Text = "";
                T017PDetails = null;
                GridView1.DataBind();
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            //GridView1.Columns[3].Visible = true;
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[8].Visible = true;
            GridView1.DataSource = dsimg.Tables[0];
            GridView1.DataBind();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                dl.SelectedValue = gr.Cells[3].Text;
                DropDownList dlw = (DropDownList)gr.FindControl("ddlIsDownload");
                dlw.SelectedValue = gr.Cells[4].Text;
            }
            //GridView1.Columns[3].Visible = false;
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
            T017PDetails = dsimg.Tables[0];
            if (dr["Title"] != DBNull.Value)
                txtOSTitle.Text = dr["Title"].ToString();
            if (dr["OSLevelId"] != DBNull.Value)
                txtsequence.Text = dr["OSLevelId"].ToString();
            if (dr["ShortDesc"] != DBNull.Value)
                txtOSShortDesc.Text = dr["ShortDesc"].ToString();
            if (dr["InnerDesc"] != DBNull.Value)
                txtosinnerdesc.Text = dr["InnerDesc"].ToString();
            if (dr["InnerVideoLink"] != DBNull.Value)
                txtvideolink.Text = dr["InnerVideoLink"].ToString();
            if (dr["IsImg"] != DBNull.Value)
            {
                chkIsPhoto.Checked = Convert.ToBoolean(dr["IsImg"]);
            }
            if (dr["IsStatistics"] != DBNull.Value)
                ddlIsStatistics.SelectedValue = Convert.ToBoolean(dr["IsStatistics"]) ? "1" : "0";
            if (dr["StatisticsId"] != DBNull.Value)
            {
                if (ddlStatistics.Items.FindByValue(dr["StatisticsId"].ToString()) != null)
                {
                    ddlStatistics.SelectedValue = dr["StatisticsId"].ToString();
                }
            }
            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactiveOS.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";

            Session["FUmainimg"] = dr["Imgpath"].ToString();
            lblMainImage.Text = dr["Imgpath"].ToString();
            hfMainImage.Value = dr["Imgpath"].ToString();
            if (string.IsNullOrWhiteSpace(lblMainImage.Text))
            {
                aRemoveMain.Visible = false;
            }
            else
            {
                aRemoveMain.Visible = true;
            }

            Session["Fuimgicon"] = dr["Iconpath"].ToString();
            lblIcon.Text = dr["Iconpath"].ToString();
            hfIcon.Value = dr["Iconpath"].ToString();
            if (string.IsNullOrWhiteSpace(lblIcon.Text))
            {
                aRemoveIcon.Visible = false;
            }
            else
            {
                aRemoveIcon.Visible = true;
            }

            Session["FUinnerimg"] = dr["InnerImgpath"].ToString();
            lblInnerImage.Text = dr["InnerImgpath"].ToString();
            hfInnerImage.Value = dr["InnerImgpath"].ToString();
            if (string.IsNullOrWhiteSpace(lblInnerImage.Text))
            {
                aRemoveInnerImage.Visible = false;
            }
            else
            {
                aRemoveInnerImage.Visible = true;
            }

            isapply();
            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private void LoadControls(SpecialityMasterBO objBo)
        {
            if (string.IsNullOrEmpty(txtOSTitle.Text))
            {
                Functions.MessagePopup(this, "Please enter Title.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.Title = txtOSTitle.Text;
            }
            if (string.IsNullOrWhiteSpace(txtOSShortDesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Short Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.ShortDesc = txtOSShortDesc.Text;
            }
            //string documentfilemain = string.Empty;
            //documentfilemain = SaveFileMain();
            //if (!string.IsNullOrEmpty(documentfilemain))
            //    objBo.Imgpath = documentfilemain;
            if (FUmainimg.HasFile)
            {
                string documentfilemain = string.Empty;
                documentfilemain = SaveFileMain();
                if (!string.IsNullOrEmpty(documentfilemain))
                    objBo.Imgpath = documentfilemain;
            }
            else
            {
                objBo.Imgpath = hfMainImage.Value;//Session["FUmainimg"].ToString();
            }


            //string documentfileicon = string.Empty;
            //documentfileicon = SaveFileMainIcon();
            //if (!string.IsNullOrEmpty(documentfileicon))
            //    objBo.Iconpath = documentfileicon;
            if (Fuimgicon.HasFile)
            {
                string documentfileicon = string.Empty;
                documentfileicon = SaveFileMainIcon();
                if (!string.IsNullOrEmpty(documentfileicon))
                    objBo.Iconpath = documentfileicon;
            }
            else
            {
                objBo.Iconpath = hfIcon.Value;//Session["Fuimgicon"].ToString();
            }

            //string documentfileinnerimg = string.Empty;
            //documentfileinnerimg = SaveFileInnerimg();
            //if (!string.IsNullOrEmpty(documentfileinnerimg))
            //    objBo.InnerImgpath = documentfileinnerimg;
            if (FUinnerimg.HasFile)
            {
                string documentfileinnerimg = string.Empty;
                documentfileinnerimg = SaveFileInnerimg();
                if (!string.IsNullOrEmpty(documentfileinnerimg))
                    objBo.InnerImgpath = documentfileinnerimg;
            }
            else
            {
                objBo.InnerImgpath = hfInnerImage.Value; //Session["FUinnerimg"].ToString();
            }

            if (!string.IsNullOrEmpty(txtvideolink.Text))
                objBo.InnerVideoLink = (txtvideolink.Text);
            if (string.IsNullOrWhiteSpace(txtosinnerdesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please enter Accredation Description.", PopupMessageType.error);
                return;
            }
            else
            {
                objBo.InnerDesc = (txtosinnerdesc.Text);
            }
            if (!string.IsNullOrEmpty(txtsequence.Text))
                objBo.OSLevelId = Convert.ToInt32(txtsequence.Text);
            objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            objBo.Is_active = ddlActiveInactiveOS.SelectedValue.ToString();
            objBo.IsImg = chkIsPhoto.Checked ? "1" : "0";
            objBo.IsStatistics = ddlIsStatistics.SelectedValue.ToString();

            if (objBo.IsStatistics == "1")
            {
                if (ddlStatistics.SelectedIndex <= 0)
                {
                    Functions.MessagePopup(this, "Please select statistics.", PopupMessageType.warning);
                    return;
                }
                else
                {
                    objBo.StatisticsId = Convert.ToInt32(ddlStatistics.SelectedValue.ToString());
                    //ddlStatistics.SelectedIndex = 0;
                }
            }
            else
            {
                ddlStatistics.SelectedIndex = 0;
                objBo.StatisticsId = null;
            }

            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                isapply();
                ShowHideControl(VisibityType.Insert);

                lblIcon.Text = "";
                lblInnerImage.Text = "";
                lblMainImage.Text = "";
                lblfilesubimg.Text = "";
                hfimgesub.Value = "";
                if (string.IsNullOrWhiteSpace(lblMainImage.Text))
                {
                    aRemoveMain.Visible = false;
                }


                if (string.IsNullOrWhiteSpace(lblIcon.Text))
                {
                    aRemoveIcon.Visible = false;
                }


                if (string.IsNullOrWhiteSpace(lblInnerImage.Text))
                {
                    aRemoveInnerImage.Visible = false;
                }
                if (string.IsNullOrWhiteSpace(hfimgesub.Value))
                {
                    removesubimg.Visible = false;
                }
                T017PDetails = null;
                ClearSubForm();
                DataSet ds = new SpecialityMasterBAL().SequenceNo();
                DataRow drs = ds.Tables[0].Rows[0];
                if (drs["OSLevelId"] != DBNull.Value)
                    txtsequence.Text = drs["OSLevelId"].ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ImageTitle");
            dt.Columns.Add("ImageShortDesc");
            dt.Columns.Add("Imagepath");
            dt.Columns.Add("ImgPopupDesc");
            dt.Columns.Add("Is_active");
            dt.Columns.Add("Is_download");
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["ImageTitle"] = row.Cells[1].Text;
                    dr["ImageShortDesc"] = row.Cells[2].Text;
                    //GridView1.Columns[3].Visible = true;
                    dr["ImgPopupDesc"] = row.Cells[3].Text;
                    //GridView1.Columns[3].Visible = false;
                    DropDownList drp = (DropDownList)row.FindControl("ddlGrdActiveInactive");
                    DropDownList drpw = (DropDownList)row.FindControl("ddlIsdownload");
                    Label img = (Label)row.FindControl("ImagrUrl");
                    dr["Is_active"] = drp.SelectedValue.ToString();
                    dr["is_download"] = drpw.SelectedValue.ToString();
                    dr["Imagepath"] = img.Text.ToString();
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOSTitle.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility title.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtOSShortDesc.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility short description.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtosinnerdesc.Text))
                {
                    Functions.MessagePopup(this, "Please enter Inner Description.", PopupMessageType.warning);
                    return;
                }
                if (ddlIsStatistics.SelectedValue == "1")
                {
                    if (ddlStatistics.SelectedValue == "")
                    {
                        Functions.MessagePopup(this, "Please select statistics.", PopupMessageType.warning);
                        return;
                    }
                    else
                    {
                        //ddlStatistics.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlStatistics.SelectedIndex = 0;
                }
                SpecialityMasterBO objBo = new SpecialityMasterBO();
                LoadControls(objBo);
                DataTable dt = T017PDetails;
                //dt = GetGridViewData();

                //if (ddlPhotos.SelectedValue == "1")
                //{
                //    if (dt.Rows.Count == 0)
                //    {
                //        Functions.MessagePopup(this, "Please Add Images.", PopupMessageType.warning);
                //    }
                //}
                if (new SpecialityMasterBAL().InsertRecord(objBo, dt))
                {
                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                    T017PDetails = null;
                    Session["FUmainimg"] = "";
                    Session["Fuimgicon"] = "";
                    Session["FUinnerimg"] = "";
                    GridView1.DataSource = (DataTable)T017PDetails;
                    GridView1.DataBind();
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (FUmainimg.HasFile)
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(FUmainimg.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)FUmainimg.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 1200 && height != 800)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 1200px*800px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                if (Fuimgicon.HasFile)
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(Fuimgicon.PostedFile.InputStream);
                    //int height = img.Height;
                    //int width = img.Width;
                    //decimal size = Math.Round(((decimal)Fuimgicon.PostedFile.ContentLength / (decimal)1024), 2);
                    //if (width != 64 && height != 64)
                    //{
                    //    Functions.MessagePopup(this, "Please upload Main image 64px*64px.", PopupMessageType.warning);
                    //    return;
                    //}
                }
                if (string.IsNullOrEmpty(txtOSTitle.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility title.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtOSShortDesc.Text))
                {
                    Functions.MessagePopup(this, "Please enter other specility short description.", PopupMessageType.warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtosinnerdesc.Text))
                {
                    Functions.MessagePopup(this, "Please enter Inner Description.", PopupMessageType.warning);
                    return;
                }
                if (ddlIsStatistics.SelectedValue == "1")
                {
                    if (ddlStatistics.SelectedValue == "")
                    {
                        Functions.MessagePopup(this, "Please select statistics.", PopupMessageType.warning);
                        return;
                    }
                    else
                    {
                        //ddlStatistics.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlStatistics.SelectedIndex = 0;
                }
                SpecialityMasterBO objBo = new SpecialityMasterBO();
                LoadControls(objBo);
                objBo.OS_id = Convert.ToInt32(ViewState["PK"]);
                DataTable dt = T017PDetails;

                //dt = GetGridViewData();

                //if (ddlPhotos.SelectedValue == "1")
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //    }
                //    else
                //    {
                //        Functions.MessagePopup(this, "Please Add Images.", PopupMessageType.warning);
                //    }
                //}
                if (new SpecialityMasterBAL().UpdateRecord(objBo, dt))
                {
                    Session["FUmainimg2"] = "";
                    Session["Fuimgicon2"] = "";
                    Session["FUinnerimg2"] = "";
                    Session["FUmainimg"] = "";
                    Session["Fuimgicon"] = "";
                    Session["FUinnerimg"] = "";
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                T017PDetails = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private string SaveFileInnerimg()
        {
            try
            {
                if (FUinnerimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
                    var fname = Path.GetExtension(FUinnerimg.FileName);
                    var count = FUinnerimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUinnerimg.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = FUinnerimg.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
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
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        FUinnerimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        //byte[] imageByteArray = (byte[])Session["FUinnerimg"];
                        //File.WriteAllBytes(Server.MapPath(DocumentUpload) + filename1, imageByteArray);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        private string SaveFileMainIcon()
        {
            try
            {
                if (Fuimgicon.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
                    var fname = Path.GetExtension(Fuimgicon.FileName);
                    var count = Fuimgicon.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < Fuimgicon.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = Fuimgicon.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
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
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        //byte[] imageByteArray = (byte[])Session["Fuimgicon"];
                        //File.WriteAllBytes(Server.MapPath(DocumentUpload) + filename1, imageByteArray);
                        Fuimgicon.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        private string SaveFileMain()
        {
            try
            {
                if (FUmainimg.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
                    var fname = Path.GetExtension(FUmainimg.FileName);
                    var count = FUmainimg.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < FUmainimg.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = FUmainimg.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
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
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //byte[] imageByteArray = (byte[])Session["FUmainimg"];
                        //File.WriteAllBytes(Server.MapPath(DocumentUpload) + filename1, imageByteArray);



                        FUmainimg.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        private string SaveFile()
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
                    var fname = Path.GetExtension(fuDocUpload.FileName);
                    var count = fuDocUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuDocUpload.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuDocUpload.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
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
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fuDocUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        #endregion

        #region ShowHideControl || Notification
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
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    ddlLanguage.Enabled = false;
                    hfTemplateId.Value = "0";
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
                    ddlLanguage.Enabled = true;
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

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion


        public System.Data.DataTable T017PDetails
        {
            get
            {
                DataTable value = (DataTable)ViewState["T017PDetails"];
                if (value == null)
                {
                    DataTable dt = new DataTable("tbl");
                    dt.Columns.Add("ImageTitle");
                    dt.Columns.Add("ImageShortDesc");
                    dt.Columns.Add("Imagepath");
                    dt.Columns.Add("ImgPopupDesc");
                    dt.Columns.Add("Is_active");
                    dt.Columns.Add("is_download");
                    value = dt;
                }
                else if (value.Rows.Count <= 0)
                {

                    DataTable dt = new DataTable("tbl");
                    dt.Columns.Add("ImageTitle");
                    dt.Columns.Add("ImageShortDesc");
                    dt.Columns.Add("Imagepath");
                    dt.Columns.Add("ImgPopupDesc");
                    dt.Columns.Add("Is_active");
                    dt.Columns.Add("is_download");
                    value = dt;
                }
                return (System.Data.DataTable)value;
            }
            set
            {
                ViewState["T017PDetails"] = value;
            }
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //if (FUmainimg.HasFile)
                //{
                //    var file = FUmainimg.PostedFile;
                //    if (file != null)
                //    {
                //        Session["FUmainimg2"] = FUmainimg;
                //        var content = new byte[file.ContentLength];
                //        file.InputStream.Read(content, 0, content.Length);
                //        Session["FUmainimg"] = content;
                //        Session["FUmainimgContentType"] = file.ContentType;
                //    }
                //}
                //if (Fuimgicon.HasFile)
                //{
                //    var file = Fuimgicon.PostedFile;
                //    if (file != null)
                //    {
                //        Session["Fuimgicon2"] = Fuimgicon;
                //        var content = new byte[file.ContentLength];
                //        file.InputStream.Read(content, 0, content.Length);
                //        Session["Fuimgicon"] = content;
                //        Session["FuimgiconContentType"] = file.ContentType;
                //    }
                //}
                //if (FUinnerimg.HasFile)
                //{
                //    var file = FUinnerimg.PostedFile;
                //    if (file != null)
                //    {
                //        Session["FUinnerimg2"] = FUinnerimg;
                //        var content = new byte[file.ContentLength];
                //        file.InputStream.Read(content, 0, content.Length);
                //        Session["FUinnerimg"] = content;
                //        Session["FUinnerimgContentType"] = file.ContentType;
                //    }
                //}


                if (Session["FUmainimg"] == null)
                {
                    if (FUmainimg.HasFile)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FUmainimg.PostedFile.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        //decimal size = Math.Round(((decimal)FUmainimg.PostedFile.ContentLength / (decimal)1024), 2);
                        //if (width != 1200 && height != 800)
                        //{
                        //    Functions.MessagePopup(this, "Please upload Main image 1200px*800px.", PopupMessageType.warning);
                        //    return;
                        //}
                        var file = FUmainimg.PostedFile;
                        if (file != null)
                        {
                            string documentfilemain = string.Empty;
                            documentfilemain = SaveFileMain();
                            if (!string.IsNullOrEmpty(documentfilemain))
                            {

                                lblMainImage.Text = documentfilemain;

                                Session["FUmainimg"] = documentfilemain;
                            }
                            //Session["FUmainimg2"] = FUmainimg;
                            //var content = new byte[file.ContentLength];
                            //file.InputStream.Read(content, 0, content.Length);
                            //Session["FUmainimg"] = content;
                            //Session["FUmainimgContentType"] = file.ContentType;
                        }
                    }
                }
                if (Session["Fuimgicon"] == null)
                {
                    if (Fuimgicon.HasFile)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(Fuimgicon.PostedFile.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        //decimal size = Math.Round(((decimal)Fuimgicon.PostedFile.ContentLength / (decimal)1024), 2);
                        //if (width != 64 && height != 64)
                        //{
                        //    Functions.MessagePopup(this, "Please upload Main image 64px*64px.", PopupMessageType.warning);
                        //    return;
                        //}
                        var file = Fuimgicon.PostedFile;
                        if (file != null)
                        {
                            string documentfileicon = string.Empty;
                            documentfileicon = SaveFileMainIcon();
                            if (!string.IsNullOrEmpty(documentfileicon))
                            {
                                lblIcon.Text = documentfileicon;
                                Session["Fuimgicon"] = documentfileicon;
                            }
                            //Session["Fuimgicon2"] = Fuimgicon;
                            //var content = new byte[file.ContentLength];
                            //file.InputStream.Read(content, 0, content.Length);
                            //Session["Fuimgicon"] = content;
                            //Session["FuimgiconContentType"] = file.ContentType;
                        }
                    }
                }
                if (Session["FUinnerimg"] == null)
                {
                    if (FUinnerimg.HasFile)
                    {
                        var file = FUinnerimg.PostedFile;
                        if (file != null)
                        {
                            string documentfileinnerimg = string.Empty;
                            documentfileinnerimg = SaveFileInnerimg();
                            if (!string.IsNullOrEmpty(documentfileinnerimg))
                            {
                                lblInnerImage.Text = documentfileinnerimg;
                                Session["FUinnerimg"] = documentfileinnerimg;
                            }
                            //Session["FUinnerimg2"] = FUinnerimg;
                            //var content = new byte[file.ContentLength];
                            //file.InputStream.Read(content, 0, content.Length);
                            //Session["FUinnerimg"] = content;
                            //Session["FUinnerimgContentType"] = file.ContentType;
                        }
                    }
                }

                {

                    DataTable dt = T017PDetails;

                    //else
                    {
                        DataRow dr;
                        if (hfRowId.Value == "0" && hfCommand.Value == "0")
                        {
                            dr = dt.NewRow();
                        }
                        else
                        {
                            dr = dt.Rows[Convert.ToInt32(hfRowId.Value)];
                        }

                        {
                            string filepath = "";
                            if (fuDocUpload.HasFile)
                            {
                                string type = Path.GetExtension(fuDocUpload.FileName).ToLower();
                                if (type != ".jpg" && type != ".jpeg" && type != ".png" && type != ".svg")
                                {
                                    Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                                    return;
                                }

                                filepath = SaveFile();
                            }
                            else
                            {
                                filepath = hfimgesub.Value;
                            }
                            //dr["OS_id"] = hfTemplateId.Value;
                            dr["ImageTitle"] = txtImgTitle.Text;
                            dr["ImageShortDesc"] = txtshortdesc.Text;
                            dr["Imagepath"] = filepath;
                            dr["ImgPopupDesc"] = (txtPopupdesc.Text);
                        }
                        dr["Is_active"] = ddlActiveInactiveimg.SelectedValue;
                        dr["is_download"] = ddlisddownload.SelectedValue;
                        if (hfRowId.Value == "0" && hfCommand.Value == "0")
                        {
                            dt.Rows.Add(dr);
                        }
                        dt.AcceptChanges();

                        T017PDetails = dt;

                        ClearSubForm();
                        removesubimg.Visible = false;
                    }
                }
                //else
                //{
                //    SpecialityMasterBO objBo = new SpecialityMasterBO();
                //    LoadControlsSub(objBo);
                //    DataTable dtDetail = new DataTable();
                //    if (new SpecialityMasterBAL().UpdateSubRecord(objBo))
                //    {
                //        Functions.MessagePopup(this, "Record update successfully.", PopupMessageType.success);
                //        SpecialityMasterBO objSubBO = new SpecialityMasterBO();
                //        objSubBO.OS_id = Convert.ToInt32(hfTemplateId.Value);
                //        objSubBO.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
                //        DataSet dsDetails = new SpecialityMasterBAL().SelectRecordIMG(objSubBO);
                //        GridView1.DataSource = dsDetails.Tables[0];
                //        GridView1.DataBind();
                //        T017PDetails = dsDetails.Tables[0];
                //    }
                //}
                //txtPopupdesc.Text = "";
                //txtImgTitle.Text = "";
                //txtshortdesc.Text = "";
                //hfId.Value = "0";
                //filename.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private void LoadControlsSub(SpecialityMasterBO objBo)
        {

            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Img_id = 0;
            }
            else
            {
                objBo.Img_id = Convert.ToInt32(hfId.Value);
            }
            if (!string.IsNullOrEmpty(txtImgTitle.Text))
                objBo.ImgTitle = txtImgTitle.Text;
            if (!string.IsNullOrEmpty(txtshortdesc.Text))
                objBo.ImgShortDesc = txtshortdesc.Text;
            if (!string.IsNullOrEmpty(txtPopupdesc.Text))
            {
                objBo.ImgPOPUpDesc = (txtPopupdesc.Text);
            }
            if (fuDocUpload.HasFile)
            {
                string SubImage = string.Empty;
                SubImage = SaveFile();
                if (!string.IsNullOrEmpty(SubImage))
                    objBo.SubImgpath = SubImage;
            }
            else
            {
                objBo.SubImgpath = hfimgesub.Value ;
            }
            objBo.Is_activeImg = ddlActiveInactiveimg.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
        }

        protected string FormatImageUrl(string url)
        {

            if (url != null && url.Length > 0)
                return ("~/" + url);
            else return null;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    {
                        if (e.CommandName == "eDelete")
                        {
                            SpecialityMasterBO objBo = new SpecialityMasterBO();
                            DataTable dt = T017PDetails;

                            DataRow dtTo = dt.Rows[intIndex];
                            dt.Rows.Remove(dtTo);
                            dt.AcceptChanges();

                            T017PDetails = dt;

                            ClearSubForm();

                            //int OSID = Convert.ToInt32(GridView1.DataKeys[intIndex].Values["OS_id"]);
                            //FillControls(OSID, Convert.ToInt32(ddlLanguage.SelectedValue));
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            ShowMessage("Record deleted successfully.", MessageType.Success);
                            return;
                        }
                        //Int32 bytID;
                        //bytID = Convert.ToInt32(GridView1.DataKeys[intIndex].Values["Img_id"]);
                        //ClearControlValues(pnlEntry);
                        if (FillControlsimg(intIndex))
                        {
                            if (e.CommandName == "eView")
                            { }
                            //ShowHideControl(VisibityType.View);
                            if (e.CommandName == "eEdit")
                            {
                                hfCommand.Value = "1";
                                hfRowId.Value = intIndex.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private bool FillControlsimg(Int32 rowId)
        {
            //SpecialityMasterBO objBo = new SpecialityMasterBO();
            //objBo.Img_id = iPkId;

            //DataSet ds = new SpecialityMasterBAL().SelectRecordSub(objBo);
            //if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataTable dt = T017PDetails;

            DataRow dr = dt.Rows[rowId];

            hfRowId.Value = rowId.ToString();

            if (dr.HasErrors) return false;
            //if (dr["Img_id"] != DBNull.Value)
            //    hfId.Value = dr["Img_id"].ToString();
            if (dr["ImgPopupDesc"] != DBNull.Value)
                txtPopupdesc.Text = HttpUtility.HtmlDecode((dr["ImgPopupDesc"].ToString()));
            if (dr["ImageTitle"] != DBNull.Value)
                txtImgTitle.Text = dr["ImageTitle"].ToString();
            if (dr["ImageShortDesc"] != DBNull.Value)
                txtshortdesc.Text = dr["ImageShortDesc"].ToString();

            string imgPath = dr["Imagepath"].ToString();

            if (string.IsNullOrWhiteSpace(imgPath))
            {
                removesubimg.Visible = false;
            }
            else
            {
                lblfilesubimg.Text = imgPath;
                hfimgesub.Value = imgPath;
                removesubimg.Visible = true;
            }
            return true;
        }

        private void StatisticsDropdownFill()
        {
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                List<GetAllStatisticsChartMasterResult> Data = objPatientsEducationBrochureRepository.GetAllStatisticsChart();
                PopulateDropDownList(ddlStatistics, Functions.ToDataTable(Data), "ChartName", "Id", true);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt32(hfTemplateId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void ddlIsStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIsStatistics.SelectedValue == "1")
            {
                divStatistics.Visible = true;
            }
            else
            {
                divStatistics.Visible = false;
            }
        }

        private void isapply()
        {
            if (ddlIsStatistics.SelectedValue == "1")
            {
                divStatistics.Visible = true;
            }
            else
            {
                divStatistics.Visible = false;
            }
            if ("1" == "1")
            {
                divphoto.Visible = true;
                divgrid.Visible = true;
            }
            else
            {
                divphoto.Visible = false;
                divgrid.Visible = false;
            }
        }

        protected void ddlPhotos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("1" == "1")
            {
                divphoto.Visible = true;
                divgrid.Visible = true;
            }
            else
            {
                divphoto.Visible = false;
                divgrid.Visible = false;
            }
        }

        protected void FUmainimg_DataBinding(object sender, EventArgs e)
        {
            if (FUmainimg.HasFile)
            {
                var file = FUmainimg.PostedFile;
                if (file != null)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, content.Length);
                    Session["FUmainimg"] = content;
                    Session["FUmainimgContentType"] = file.ContentType;
                }
            }
        }

        protected void FUmainimg_PreRender(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearSubForm();
        }

        private void ClearSubForm()
        {
            hfRowId.Value = "0";
            hfId.Value = "0";
            txtPopupdesc.Text = "";
            txtImgTitle.Text = "";
            txtshortdesc.Text = "";
            lblfilesubimg.Text = "";
            hfimgesub.Value = "";
            BindSubGridView();

        }

        private void BindSubGridView()
        {
            DataTable dt = T017PDetails;

            GridView1.Visible = true;

            GridView1.Columns[7].Visible = true;
            GridView1.Columns[8].Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                DropDownList dl = (DropDownList)gr.FindControl("ddlGrdActiveInactive");
                dl.SelectedValue = gr.Cells[6].Text;
                DropDownList dlw = (DropDownList)gr.FindControl("ddlIsdownload");
                dlw.SelectedValue = gr.Cells[7].Text;
            }
            //GridView1.Columns[3].Visible = false;
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
        }
    }
}