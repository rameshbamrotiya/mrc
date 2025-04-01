using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class DailyVisitEntry : System.Web.UI.Page
    {
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            if (!Page.IsPostBack)
            {
                BindGridView();

                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";

                if(!string.IsNullOrWhiteSpace(txtEntryDate.Text))
                {
                    DateTime dateEntry;
                    if (!DateTime.TryParse(txtEntryDate.Text, out dateEntry))
                    {
                        Functions.MessagePopup(this, "Please enter proper Entry Date.", PopupMessageType.error);
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please enter proper Entry Date.", PopupMessageType.error);
                    return;
                }

                using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
                {
                    GetAllDailyEntryVisitMasterResult objBO = new GetAllDailyEntryVisitMasterResult();
                    if(LoadControls(ref objBO))
                    {
                        if (!objAcc.InsertOrUpdateDailyVisitEntry(objBO, out errorMessage))
                        {

                            ClearControlValues();
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {

            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

            try
            {
                BindGridView(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView();
            grdUser.PageIndex = e.NewPageIndex;
            grdUser.DataBind();
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objAcc.GetAllDailyVisitEntryById(rowId);
                    if (dataInfo != null)
                    {
                        ddlCategory.SelectedValue = dataInfo.DailyCatId.ToString();

                        txtTitle.Text = dataInfo.EntryName;
                        txtVisitCount.Text = dataInfo.VisitCount.ToString();
                        txtEntryDate.Text = dataInfo.EntryDate.Value.ToString("dd/MM/yyyy").Replace("-","/");

                        if (!string.IsNullOrWhiteSpace(dataInfo.FileName))
                        {
                            lblLeftImage.Text = dataInfo.FileName;
                            hfLeftImage.Value = dataInfo.FileName;
                            aRemoveLeft.Visible = true;
                        }
                        if (!string.IsNullOrWhiteSpace(dataInfo.PDFFileName))
                        {
                            lblRightImage.Text = dataInfo.PDFFileName;
                            hfRightImage.Value = dataInfo.PDFFileName;
                            aRemoveRight.Visible = true;
                        }


                        chkIsActive.Checked = dataInfo.IsVisable == null ? false : (bool)dataInfo.IsVisable;
                        hfId.Value = rowId.ToString();
                        txtVisitCount.Text = dataInfo.VisitCount.ToString();
                        ShowHideControl(VisibityType.Edit);
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

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IDailyVisitEntryRepository objAcc = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveDailyVisitEntry(rowId, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
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


        #region ShowHideControl || Notification

        private void ClearControlValues()
        {
            txtEntryDate.Text = "";
            ddlCategory.SelectedIndex = 0;
            chkIsActive.Checked = false;

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            hfRightImage.Value = "";
            lblRightImage.Text = "";
            aRemoveRight.Visible = false;

            BindGridView();
        }

        private bool LoadControls(ref GetAllDailyEntryVisitMasterResult objBo)
        {

            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }
            objBo.DailyCatId = Convert.ToInt32(ddlCategory.SelectedValue);
            if (fuImage.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SavefuImageFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.FileName = documentfile;
            }
            else
            {
                objBo.FileName = hfLeftImage.Value;
            }

            if (fuPDFUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SavefuPDFUpload();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.PDFFileName = documentfile;
            }
            else
            {
                objBo.PDFFileName = hfRightImage.Value;
            }
            long lgVisitCount;
            if(!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                objBo.EntryName = txtTitle.Text;
            }
            else
            {

                Functions.MessagePopup(this, "Please enter Title.", PopupMessageType.error);
                return false;
            }

            if(long.TryParse(txtVisitCount.Text, out lgVisitCount))
            {
                objBo.VisitCount = lgVisitCount;
            }
            else
            {

                Functions.MessagePopup(this, "Please enter proper Visit Count.", PopupMessageType.error);
                return false;
            }

            DateTime dateEntry;
            if (!DateTime.TryParse(txtEntryDate.Text,out dateEntry))
            {
                Functions.MessagePopup(this, "Please enter proper Entry Date.", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.EntryDate = dateEntry;
            }

            objBo.IsVisable = chkIsActive.Checked;
            return true;
        }

        private string SavefuPDFUpload()
        {
            var DocumentUpload = ConfigDetailsValue.AccredationImg;
            var fname = Path.GetExtension(fuPDFUpload.FileName);
            var count = fuPDFUpload.FileName.Split('.');
            string type = "";
            bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
            for (int i = 0; i < fuPDFUpload.FileName.Split('.').Length; i++)
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
                var filename1 = fuPDFUpload.FileName.Replace(" ", "_");
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
                fuPDFUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                return (DocumentUpload) + filename1;
            }
            return "";
        }

        private string SavefuImageFile()
        {
            var DocumentUpload = ConfigDetailsValue.AccredationImg;
            var fname = Path.GetExtension(fuImage.FileName);
            var count = fuImage.FileName.Split('.');
            string type = "";
            bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
            for (int i = 0; i < fuImage.FileName.Split('.').Length; i++)
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
                var filename1 = fuImage.FileName.Replace(" ", "_");
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
                fuImage.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                return (DocumentUpload) + filename1;
            }
            return "";
        }

        private void BindGridView(string strSearch = "")
        {
            using (IDailyVisitEntryRepository objDailyVisitEntryRepository = new DailyVisitEntryRepository(Functions.strSqlConnectionString))
            {
                var data = objDailyVisitEntryRepository.GetAllDailyVisitEntry();

                if (data != null)
                {
                    grdUser.DataSource = data;
                }
                else
                {
                    grdUser.DataSource = null;
                }

                if (!string.IsNullOrWhiteSpace(strSearch))
                {
                    grdUser.DataSource = data.Where(x => x.DailyCatagoryName.Contains(strSearch) || x.EntryName.Contains(strSearch) || (x.EntryDate != null && x.EntryDate.Value.ToString("dd/MM/yyyy").Replace("-", "/").Contains(strSearch))).ToList();
                }

                var dropDownData = objDailyVisitEntryRepository.GetAllDailyVisitCategory();
                if(dropDownData!=null)
                {
                    ddlCategory.DataSource = dropDownData;
                    ddlCategory.DataValueField = "Id";
                    ddlCategory.DataTextField = "DailyCatagoryName";

                }
                else
                {
                    ddlCategory.DataSource = null;

                }
                ddlCategory.DataBind();
            }
            grdUser.DataBind();

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
                    btn_Save.Visible = true;
                    break;
                case VisibityType.Insert:

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;

                    pnlView.Visible = false;
                    hfId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        public void ClearControlValues(Control container)
        {
            try
            {
                foreach (Control ctrl in container.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                        ((TextBox)ctrl).Text = "";
                    if (ctrl.GetType() == typeof(DropDownList))
                        ((DropDownList)ctrl).SelectedIndex = -1;
                    if (ctrl.GetType() == typeof(CheckBox))
                        ((CheckBox)ctrl).Checked = false;
                    if (ctrl.GetType() == typeof(System.Web.UI.WebControls.Image))
                        ((System.Web.UI.WebControls.Image)ctrl).ImageUrl = string.Empty;
                    if (ctrl.GetType() == typeof(GridView))
                        ((GridView)ctrl).DataSource = null;
                    if (ctrl.Controls.Count > 0)
                        ClearControlValues(ctrl);
                    if (ctrl is ListControl && ctrl.GetType().Name != "DropDownList")
                    {
                        ListControl listControl = ctrl as ListControl;
                        foreach (ListItem listItem in listControl.Items)
                            listItem.Selected = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}