using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Unmehta.WebPortal.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class AcademicMedicalMaster : System.Web.UI.Page
    {
        #region Academic Medical
        #region Page Event
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
            if (!Page.IsPostBack)
            {
                BindGridView();
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearControlValues();
                ShowHideControl(VisibityType.GridView);
                        aRemovePhoto.Visible = false;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objAcc.GetAcademicMedicalMasterByIdAndLgId((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                    if (dataInfo != null)
                    {
                        hfPhoto.Value = "";
                        lblPhoto.Text = "";
                        aRemovePhoto.Visible = false;

                        //BindDoctorDropDown(dataInfo.Id);
                        txtAcademicsName.Text = dataInfo.AcademicsName;
                        txtAcademicsFullName.Text = dataInfo.AcademicsFullName;

                        txtMetaTitle.Text = dataInfo.MetaTitle;
                        txtMetaDescription.Text = dataInfo.MetaDescription;

                        txtDescription.Text = dataInfo.AcademicsDescription;
                        if (!string.IsNullOrWhiteSpace(dataInfo.ImagePath))
                        {
                            hfPhoto.Value = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.ImagePath;
                            lblPhoto.Text = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.ImagePath;
                            aRemovePhoto.Visible = true;
                            
                        }
                        GetListValueDoctor = Functions.ToDataTable(objAcc.GetAcademicMedicalMasterDoctorDetails(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)));
                        BindgvDoctor();
                        ShowHideControl(VisibityType.Edit);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language First", PopupMessageType.warning);
                return;
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                BindGridView();
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {

            
            ShowHideControl(VisibityType.Insert);
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
            {
                ddlLanguage.SelectedIndex = 1;
                var dataInfo = objAcc.GetAcademicMedicalMasterByIdAndLgId((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                if (dataInfo != null)
                {
                    ddlLanguage.Enabled = true;
                    //BindDoctorDropDown(dataInfo.Id);
                    txtAcademicsName.Text = dataInfo.AcademicsName;
                    txtAcademicsFullName.Text = dataInfo.AcademicsFullName;
                    txtMetaTitle.Text = dataInfo.MetaTitle;
                    txtMetaDescription.Text = dataInfo.MetaDescription;
                    txtDescription.Text = dataInfo.AcademicsDescription;

                    if(!string.IsNullOrWhiteSpace(dataInfo.ImagePath))
                    {

                        hfLeftImage.Value = dataInfo.ImagePath;
                        lblLeftImage.Text = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.ImagePath;
                        aRemoveLeft.Visible = true;

                    }
                    GetListValueDoctor = Functions.ToDataTable(objAcc.GetAcademicMedicalMasterDoctorDetails(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)));
                    BindgvDoctor();
                    ShowHideControl(VisibityType.Edit);
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveAcademicMedical(rowId, out errorMessage))
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            {
                string errorMessage = "";
                using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
                {
                    AcademicMedicalModel objBO = new AcademicMedicalModel();
                    if (LoadControlsAdd(objBO))
                    {
                        if (!objAcc.InsertOrUpdateAcademicMedical(objBO, out errorMessage))
                        {
                            objAcc.RemoveAcademicMedicalDoctorDetails(objBO.Id, out errorMessage);


                            if (GetListValueDoctor.Rows.Count > 0)
                            {
                                foreach (DataRow row in GetListValueDoctor.Rows)
                                {
                                    //if (row["Id"].ToString().Contains("Temp_"))
                                    {
                                        if (objAcc.InsertAcademicMedical(new AcademicMedicalDoctorModel { AccId = objBO.Id, LangId = objBO.LangId, YearId = Convert.ToInt32(row["Year"].ToString()), StudentName = row["StudentName"].ToString(), DegreeHold = row["DegreeHead"].ToString(), Photo = row["Photo"].ToString() }, out errorMessage))
                                        {
                                            return;
                                        }
                                    }
                                }
                            }

                            ClearControlValues();
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //System.Drawing.Image img = System.Drawing.Image.FromStream(fuPic.PostedFile.InputStream);
            //int height = img.Height;
            //int width = img.Width;
            //decimal size = Math.Round(((decimal)fuPic.PostedFile.ContentLength / (decimal)1024), 2);
            //if (height != 800 || width != 1200)
            //{
            //    Functions.MessagePopup(this, "Please upload 800px*1200px.", PopupMessageType.error);
            //    return;
            //}
            //else
            {
                string errorMessage = "";
                using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
                {
                    AcademicMedicalModel objBO = new AcademicMedicalModel();
                    if (LoadControlsAdd(objBO))
                    {
                        if (!objAcc.InsertOrUpdateAcademicMedical(objBO, out errorMessage))
                        {
                            if (GetListValueDoctor.Rows.Count > 0)
                            {
                                foreach (DataRow row in GetListValueDoctor.Rows)
                                {
                                    if (row["Id"].ToString().Contains("Temp_"))
                                    {
                                        if (objAcc.InsertAcademicMedical(new AcademicMedicalDoctorModel { AccId = objBO.Id, LangId = objBO.LangId, YearId = Convert.ToInt32(row["Year"].ToString()), StudentName = row["StudentName"].ToString(), DegreeHold = row["DegreeHead"].ToString(), Photo = row["Photo"].ToString() }, out errorMessage))
                                        {
                                            return;
                                        }
                                    }
                                }
                            }

                            ClearControlValues();
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                }
            }
        }
        #endregion

        #region Functions
        private bool LoadControlsAdd(AcademicMedicalModel objBo)
        {
            if (string.IsNullOrWhiteSpace(txtAcademicsName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Academics Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.AcademicsName = txtAcademicsName.Text.Trim();
            }

            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }
            if (fuPic.HasFile)
            {
                string filePath = ConfigDetailsValue.AcademicMedicalFiles;

                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuPic.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                //decimal size = Math.Round(((decimal)fuPic.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return false;
                //}

                if (!filePath.Contains("|"))
                {
                    //if (fuPic.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.ImagePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPic.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.ImagePath;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.ImagePath);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToLower();
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuPic.SaveAs(Server.MapPath(filePath) + objBo.ImagePath);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;

                    }
                }
                else
                {

                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                objBo.ImagePath = string.IsNullOrWhiteSpace(hfLeftImage.Value)? "" : System.IO.Path.GetFileName(hfLeftImage.Value);
            }

            objBo.MetaTitle = txtMetaTitle.Text;
            objBo.MetaDescription = txtMetaDescription.Text;

            if (!string.IsNullOrEmpty(txtAcademicsFullName.Text.Trim()))
            {
                objBo.AcademicsFullName = txtAcademicsFullName.Text;
            }

            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                objBo.AcademicsDescription = txtDescription.Text;
            }
            //objBo.ChkMode = 0;
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

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

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

                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;

                    ClearControlValues();
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
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            txtAcademicsName.Text = "";
            txtAcademicsFullName.Text = "";
            txtDescription.Text = "";
            txtMetaDescription.Text = "";
            txtMetaTitle.Text = "";
            hfID.Value = "0";

            hfPhoto.Value = "";
            lblPhoto.Text = "";
            aRemovePhoto.Visible = false;

            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            GetListValueDoctor = null;
            //BindDoctorDropDown();
            BindgvDoctor();
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }
        #endregion
        #endregion

        #region Doctor List Logic
        #region Functions
        private DataTable GetListValueDoctor
        {
            get
            {
                DataTable result = new DataTable();

                if (ViewState["someID"] != null)
                {
                    result = (DataTable)ViewState["someID"];
                }
                else
                {
                    result = new DataTable("tblgvDoctor");
                    result.Columns.Add(new DataColumn("Id", typeof(string)));
                    result.Columns.Add(new DataColumn("Year", typeof(string)));
                    result.Columns.Add(new DataColumn("StudentName", typeof(string)));
                    result.Columns.Add(new DataColumn("DegreeHead", typeof(string)));
                    result.Columns.Add(new DataColumn("Photo", typeof(string)));
                }

                return result;
            }
            set
            {
                ViewState["someID"] = value;
            }
        }
        
        private string SaveFileMainImg()
        {
            try
            {
                if (fuPhotoUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.Affilation;
                    var fname = Path.GetExtension(fuPhotoUpload.FileName);
                    var count = fuPhotoUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuPhotoUpload.FileName.Split('.').Length; i++)
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
                        return "Please Select Valid File Formate!!!";
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuPhotoUpload.FileName.Replace(" ", "_");

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
                        fuPhotoUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }

        private void BindgvDoctor()
        {

            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
        }
        #endregion

        #region Page Load
        protected void ibtn_DoctorEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string Id = gvDoctor.DataKeys[rowindex]["Id"].ToString();

            foreach (DataRow row in GetListValueDoctor.Rows)
            {
                if (row["Id"].ToString() == Id)
                {
                    hfSubId.Value = row["Id"].ToString();
                    ddlYear.SelectedValue = row["Year"].ToString();
                    txtStudentName.Text = row["StudentName"].ToString();
                    //txtDegreeHold.Text = row["DegreeHead"].ToString();
                    if (!string.IsNullOrWhiteSpace(row["Photo"].ToString()))
                    {
                        lblPhoto.Visible = true;
                        lblPhoto.Text = row["Photo"].ToString();
                        aRemovePhoto.Visible = true;
                        hfPhoto.Value = row["Photo"].ToString();
                    }
                    else
                    {
                        aRemovePhoto.Visible = false;
                    }
                    break;
                }
            }
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (gvDoctor.DataKeys[rowindex]["Id"].ToString());
            GridViewRow gvRow = gvDoctor.Rows[rowindex];

            DataTable dt = null;
            dt = GetListValueDoctor;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Id"].ToString() == rowId)
                    dr.Delete();
            }
            dt.AcceptChanges();

            if (hfID.Value != "0" && string.IsNullOrWhiteSpace(hfID.Value) && !rowId.Contains("Temp_"))
            {
                using (IAcademicMedicalRepository objAcc = new AcademicMedicalRepository(Functions.strSqlConnectionString))
                {
                    if (!objAcc.RemoveAcademicMedicalDoctorDetails(Convert.ToInt32(rowId), out strError))
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.error);
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Record Removed Done", PopupMessageType.success);
            }
            GetListValueDoctor = dt;
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            ShowHideControl(VisibityType.Edit);
        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            string strImagePath = "";
            bool isNoError = true;
            if (string.IsNullOrWhiteSpace(txtStudentName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Student Name", PopupMessageType.error);
                return;
            }
            //if (string.IsNullOrWhiteSpace(txtDegreeHold.Text.Trim()))
            //{
            //    Functions.MessagePopup(this, "Please Enter Degree Hold", PopupMessageType.error);
            //    return;
            //}

            if (fuPhotoUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFileMainImg();

                if (!string.IsNullOrEmpty(documentfile) && documentfile != "Please Select Valid File Formate!!!")
                {
                    strImagePath = documentfile;
                }
                else
                {
                    Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    return;
                }
            }
            else
            {
                strImagePath = hfPhoto.Value;
            }
            if (isNoError)
            {
                DataTable dt = null;
                dt = GetListValueDoctor;
                bool isExist = false;
                foreach (DataRow row in GetListValueDoctor.Rows)
                {
                    if (row["Id"].ToString() == hfSubId.Value)
                    {
                        row["Year"] = ddlYear.SelectedValue;
                        row["StudentName"] = txtStudentName.Text;
                        //row["DegreeHead"] = txtDegreeHold.Text;
                        row["Photo"] = strImagePath;
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    DataRow dr = dt.NewRow();
                    int rowCount = 0;
                    if (dt.Rows.Count > 0)
                    {
                        if(int.TryParse(dt.Rows[dt.Rows.Count - 1]["Id"].ToString(),out rowCount))
                        {
                            rowCount = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Id"].ToString());
                        }
                        else
                        {
                            rowCount = dt.Rows.Count - 1;
                        }
                    }
                    dr["Id"] = "Temp_" + (rowCount + 1);
                    dr["Year"] = ddlYear.SelectedValue;
                    dr["StudentName"] = txtStudentName.Text;
                    //dr["DegreeHead"] = txtDegreeHold.Text;
                    dr["Photo"] = strImagePath;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
                GetListValueDoctor = dt;
            }
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            lblPhoto.Visible = false;
            lblPhoto.Text = "";
            hfPhoto.Value = "";
            aRemovePhoto.Visible = false;
            txtStudentName.Text = "";
            ddlYear.SelectedIndex = 0;            
            ShowHideControl(VisibityType.Edit);
            txtStudentName.Focus();
        }
        #endregion

        #endregion

        protected void gvDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDoctor.PageIndex = e.NewPageIndex;
            BindgvDoctor();
        }
    }
}