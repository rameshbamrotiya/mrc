using BAL;
using System;
using System.Data;
using System.Web.UI;
using static Unmehta.WebPortal.Web.Common.Functions;
using Unmehta.WebPortal.Web.Common;
using BO;
using BL;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Common;
using System.Web.UI.WebControls;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class HomePageDetails : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            try
            {

                if (!Page.IsPostBack)
                {
                    DataSet ds = new DataSet();
                    LanguageMasterBAL objBAL = new LanguageMasterBAL();
                    ds = objBAL.FillLanguage();
                    DataTable dt = ds.Tables[0];
                    Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", false);


                    hfOpdImage.Value = "";
                    lblOpdImage.Text = "";
                    aRemoveOpdImage.Visible = false;

                    hfIPDImage.Value = "";
                    lblIPDImage.Text = "";
                    aRemoveIPDImage.Visible = false;

                    hfProcedureImage.Value = "";
                    lblProcedureImage.Text = "";
                    aRemoveProcedureImage.Visible = false;

                    hfSurgeryImage.Value = "";
                    lblSurgeryImage.Text = "";
                    aRemoveSurgeryImage.Visible = false;


                    hfInvestigationImage.Value = "";
                    lblInvestigationImage.Text = "";
                    aRemoveInvestigationImage.Visible = false;

                    DataTable dtreadmore = new DataTable();
                    dtreadmore = new HomePageContentBAL().SelectReadmoreLeftRightSide();
                    if (dtreadmore.Rows.Count > 0)
                    {
                        Functions.PopulateDropDownList(ddlLeftVideoReadMore, dtreadmore, "col_menu_name", "col_menu_url", true);
                        Functions.PopulateDropDownList(ddlRightVideoReadMore, dtreadmore, "col_menu_name", "col_menu_url", true);
                    }
                    BindPageDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void BindDataByLanguageId(int LangId)
        {
            try
            {
                if (LangId == 0)
                    return;

                FillControls(LangId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindPageDetails()
        {
            BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        private void FillControls(int languageid)
        {
            try
            {

                hfOpdImage.Value = "";
                lblOpdImage.Text = "";
                aRemoveOpdImage.Visible = false;

                hfIPDImage.Value = "";
                lblIPDImage.Text = "";
                aRemoveIPDImage.Visible = false;

                hfProcedureImage.Value = "";
                lblProcedureImage.Text = "";
                aRemoveProcedureImage.Visible = false;

                hfSurgeryImage.Value = "";
                lblSurgeryImage.Text = "";
                aRemoveSurgeryImage.Visible = false;


                hfInvestigationImage.Value = "";
                lblInvestigationImage.Text = "";
                aRemoveInvestigationImage.Visible = false;

                string ddlLan = Convert.ToString(ddlLanguage.SelectedValue);
                ClearControlValues(pnlEntry);
                ddlLanguage.SelectedValue = ddlLan;
                HomePageContent_DetailsBO objbo = new HomePageContent_DetailsBO();
                HomePageContentBAL objBAL = new HomePageContentBAL();
                objbo.LanguageID = languageid;
                DataTable dt = new DataTable();
                dt = objBAL.SelectRecord(objbo);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtLeftVideoTitle.Text = dr["LeftVideoTitle"].ToString();
                    //txtLeftVideoURL.Text = dr["LeftVideoURL"].ToString();
                    ddlLeftVideoReadMore.SelectedValue = dr["LeftVideoReadMore"].ToString();
                    txtRightVideoTitle.Text = dr["RightVideoTitle"].ToString();
                    //txtRightVideoURL.Text = dr["RightVideoURL"].ToString();
                    ddlRightVideoReadMore.SelectedValue = dr["RightVideoReadMore"].ToString();



                    if (!string.IsNullOrWhiteSpace(dr["OPDImageIcon"].ToString()))
                    {
                        hfOpdImage.Value = dr["OPDImageIcon"].ToString();
                        lblOpdImage.Text= dr["OPDImageIcon"].ToString();
                        aRemoveOpdImage.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(dr["IPDImageIcon"].ToString()))
                    {
                        hfIPDImage.Value = dr["IPDImageIcon"].ToString();
                        lblIPDImage.Text = dr["IPDImageIcon"].ToString();
                        aRemoveIPDImage.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(dr["ProceduresImageIcon"].ToString()))
                    {
                        hfProcedureImage.Value = dr["ProceduresImageIcon"].ToString();
                        lblProcedureImage.Text = dr["ProceduresImageIcon"].ToString();
                        aRemoveProcedureImage.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SurgeryImageIcon"].ToString()))
                    {
                        hfSurgeryImage.Value = dr["SurgeryImageIcon"].ToString();
                        lblSurgeryImage.Text = dr["SurgeryImageIcon"].ToString();
                        aRemoveSurgeryImage.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(dr["InvestigationsImageIcon"].ToString()))
                    {
                        hfInvestigationImage.Value = dr["InvestigationsImageIcon"].ToString();
                        lblInvestigationImage.Text = dr["InvestigationsImageIcon"].ToString();
                        aRemoveInvestigationImage.Visible = true;
                    }


                    lblLeftImage.Text = dr["LeftImage"].ToString();
                    hfLeftImage.Value = dr["LeftImage"].ToString();

                    lblRightImage.Text = dr["RightImage"].ToString();
                    hfRightImage.Value = dr["RightImage"].ToString();

                    if (string.IsNullOrWhiteSpace(lblLeftImage.Text))
                    {
                        aRemoveLeft.Visible = false;
                    }
                    else
                    {
                        aRemoveLeft.Visible = true;
                    }
                    if (string.IsNullOrWhiteSpace(lblRightImage.Text))
                    {
                        aRemoveRight.Visible = false;
                    }
                    else
                    {
                        aRemoveRight.Visible = true;
                    }
                    txtOpdDay.Text = dr["OpdDay"].ToString();
                    txtIpdDay.Text = dr["IpdDay"].ToString();
                    txtSurgeryDay.Text = dr["SurgeryDay"].ToString();
                    txtProceduresDay.Text = dr["ProceduresDay"].ToString();
                    txtInvestigationsDay.Text = dr["InvestigationsDay"].ToString();
                    ddlLanguage.SelectedValue = dr["Languageid"].ToString();
                    hdnHome_ID.Value = dr["Home_ID"].ToString();
                    if (dr["link_Video_PathLeft"] != DBNull.Value)
                        ddlvideouploadleft.SelectedValue = Convert.ToString(dr["link_Video_PathLeft"]);
                    if (dr["link_Video_PathRight"] != DBNull.Value)
                        ddlvideouploadRight.SelectedValue = Convert.ToString(dr["link_Video_PathRight"]);
                    if (dr["link_Video_PathLeft"].ToString() == "True")
                    {
                        aRemoveLeftVideo.Visible = true;
                        string videoleft = dr["LeftVideoURL"].ToString();
                        if (!string.IsNullOrWhiteSpace(videoleft))
                        {

                            hfLeftVideo.Value = videoleft;
                            lblLeftVideo.Text = videoleft;
                            aRemoveLeftVideo.Visible = true;

                        }
                        externallinkLeft.Visible = false;
                        internalvideoLeft.Visible = true;
                    }
                    else
                    {
                        externallinkLeft.Visible = true;
                        internalvideoLeft.Visible = false;
                        txtLeftVideoURL.Text = dr["LeftVideoURL"].ToString();
                    }
                    if (dr["link_Video_PathRight"].ToString() == "True")
                    {
                        string videoRight = dr["RightVideoURL"].ToString();
                        if(!string.IsNullOrWhiteSpace(videoRight))
                        {

                            hfRightVideo.Value = videoRight;
                            lblRightVideo.Text = videoRight;
                            aRemoveRightVideo.Visible = true;

                        }
                        externallinkRight.Visible = false;
                        internalvideoRight.Visible = true;
                    }
                    else
                    {
                        externallinkRight.Visible = true;
                        internalvideoRight.Visible = false;
                        txtRightVideoURL.Text = dr["RightVideoURL"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlLanguage.SelectedValue) > 0)
                {
                    HomePageContent_DetailsBO objbo = new HomePageContent_DetailsBO();
                    objbo.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    if (!LoadControls(objbo))
                    {

                        if (new HomePageContentBAL().InsertRecord(objbo))
                        {
                            FillControls((int)objbo.LanguageID);
                            Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private string SaveFile(FileUpload fuDocUpload, out bool isError)
        {
            isError = false;
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AwardImage;
                    var fname = System.IO.Path.GetExtension(fuDocUpload.FileName);
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
                        isError = true;
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
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
            return "";
        }

        private bool LoadControls(HomePageContent_DetailsBO objBo)
        {
            bool isError = false;

            objBo.LanguageID = Convert.ToInt16(ddlLanguage.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtLeftVideoTitle.Text))
                objBo.LeftVideoTitle = txtLeftVideoTitle.Text;

            //if (!string.IsNullOrEmpty(txtLeftVideoURL.Text))
            //    objBo.LeftVideoURL = txtLeftVideoURL.Text;

            if (ddlLeftVideoReadMore.SelectedIndex > 0)
                objBo.LeftVideoReadMore = ddlLeftVideoReadMore.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(txtRightVideoTitle.Text))
                objBo.RightVideoTitle = txtRightVideoTitle.Text.ToString();

            //if (!string.IsNullOrEmpty(txtRightVideoURL.Text))
            //    objBo.RightVideoURL = txtRightVideoURL.Text;

            if (ddlRightVideoReadMore.SelectedIndex > 0)
                objBo.RightVideoReadMore = ddlRightVideoReadMore.SelectedValue.ToString();


            if (fuOpdImage.HasFile)
            {
                objBo.OPDImageIcon = SaveFile(fuOpdImage, out isError);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(hfOpdImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Opd Image.", PopupMessageType.success);
                    return true;
                }
                else
                {
                    objBo.OPDImageIcon = hfOpdImage.Value;
                }
            }

            if (fuLeftThumbNail.HasFile)
            {
                objBo.LeftImage = SaveFile(fuLeftThumbNail, out isError);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(hfLeftImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Left Thumbnail.", PopupMessageType.warning);
                    return true;
                }
                else
                {
                    objBo.LeftImage = hfLeftImage.Value;
                }
            }

            if (fuRightThumbNail.HasFile)
            {
                objBo.RightImage = SaveFile(fuRightThumbNail, out isError);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(hfRightImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Right Thumbnail.", PopupMessageType.warning);
                    return true;
                }
                else
                {
                    objBo.RightImage = hfRightImage.Value;
                }
            }

            if (!isError && fuIPDImage.HasFile)
            {
                objBo.IPDImageIcon = SaveFile(fuIPDImage, out isError);

            }
            else
            {
                if (isError)
                {
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(hfIPDImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select IPD Image.", PopupMessageType.success);
                    return true;
                }
                else
                {
                    objBo.IPDImageIcon = hfIPDImage.Value;
                }
            }

            if (!isError && fuSurgeryImage.HasFile)
            {
                objBo.SurgeryImageIcon = SaveFile(fuSurgeryImage, out isError);

            }
            else
            {
                if (isError)
                {
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(hfSurgeryImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Surgery Image.", PopupMessageType.success);
                    return true;
                }
                else
                {
                    objBo.SurgeryImageIcon = hfSurgeryImage.Value;
                }
            }

            if (!isError && fuProceduresImage.HasFile)
            {
                objBo.ProceduresImageIcon = SaveFile(fuProceduresImage, out isError);

            }
            else
            {
                if (isError)
                {
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(hfProcedureImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Procedures Image.", PopupMessageType.success);
                    return true;
                }
                else
                {
                    objBo.ProceduresImageIcon = hfProcedureImage.Value;
                }
            }

            if (!isError && fuInvestigationImage.HasFile)
            {
                objBo.InvestigationsImageIcon = SaveFile(fuInvestigationImage, out isError);

            }
            else
            {
                if (isError)
                {
                    return true;
                }
                else if (string.IsNullOrWhiteSpace(hfInvestigationImage.Value))
                {
                    Functions.MessagePopup(this, "Please Select Investigation Image.", PopupMessageType.success);
                    return true;
                }
                else
                {
                    objBo.InvestigationsImageIcon = hfInvestigationImage.Value;
                }
            }

            if (ddlvideouploadleft.SelectedValue == "False")
            {
                if (!string.IsNullOrEmpty(txtLeftVideoURL.Text))
                    objBo.LeftVideoURL = txtLeftVideoURL.Text;               
            }
            else
            {
                if (fuDocUploadLeft.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFileLeft(fuDocUploadLeft);
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.LeftVideoURL = documentfile;
                }
                else
                {
                    objBo.LeftVideoURL = hfLeftVideo.Value;
                }                
            }


            if (ddlvideouploadRight.SelectedValue == "False")
            {                
                if (!string.IsNullOrEmpty(txtRightVideoURL.Text))
                    objBo.RightVideoURL = txtRightVideoURL.Text;
            }
            else
            {                
                if (fuDocUploadRight.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFileLeft(fuDocUploadRight);
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.RightVideoURL = documentfile;
                }
                else
                {
                    objBo.RightVideoURL = hfRightVideo.Value;
                }
            }

            if (!string.IsNullOrEmpty(txtOpdDay.Text))
                objBo.OpdDay = Convert.ToInt32(txtOpdDay.Text);
            if (!string.IsNullOrEmpty(txtIpdDay.Text))
                objBo.IpdDay = Convert.ToInt32(txtIpdDay.Text);
            if (!string.IsNullOrEmpty(txtSurgeryDay.Text))
                objBo.SurgeryDay = Convert.ToInt32(txtSurgeryDay.Text);
            if (!string.IsNullOrEmpty(txtProceduresDay.Text))
                objBo.ProceduresDay = Convert.ToInt32(txtProceduresDay.Text);
            if (!string.IsNullOrEmpty(txtInvestigationsDay.Text))
                objBo.InvestigationsDay = Convert.ToInt32(txtInvestigationsDay.Text);
            if (!string.IsNullOrEmpty(hdnHome_ID.Value))
                objBo.Home_ID = Convert.ToInt32(hdnHome_ID.Value);
            objBo.AddedBy = Convert.ToString(SessionWrapper.UserDetails.UserName);
            objBo.IPAddress = GetIPAddress;
            objBo.link_Video_PathLeft = ddlvideouploadleft.SelectedValue.ToString();
            objBo.link_Video_PathRight = ddlvideouploadRight.SelectedValue.ToString();

            return isError;
        }
        private string SaveFileLeft(FileUpload fuUpload)
        {
            try
            {
                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.VideoUploadPath;
                    var fname = Path.GetExtension(fuUpload.FileName);
                    var count = fuUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuUpload.FileName.Split('.').Length; i++)
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
                        var filename1 = fuUpload.FileName.Replace(" ", "_");
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
                        fuUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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

        protected void ddlLanguage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindPageDetails();
        }
        protected void ddlvideoupload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlvideouploadleft.SelectedValue == "False")
            {
                externallinkLeft.Visible = true;
                //thumbnilexternal.Visible = false;
                internalvideoLeft.Visible = false;
            }
            else
            {
                //thumbnilexternal.Visible = true;
                externallinkLeft.Visible = false;
                internalvideoLeft.Visible = true;
            }
        }

        protected void ddlvideouploadRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlvideouploadRight.SelectedValue == "False")
            {
                externallinkRight.Visible = true;
                //thumbnilexternal.Visible = false;
                internalvideoRight.Visible = false;
            }
            else
            {
                //thumbnilexternal.Visible = true;
                externallinkRight.Visible = false;
                internalvideoRight.Visible = true;
            }
        }
    }
}