using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class VisionMissionMaster : System.Web.UI.Page
    {
        #region Page Functions
        static string[] mediaExtensions = {
    ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF" //etc
};
        static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
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
            CKEditorControl1.Text = "";
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            GetListValueDoctor = null;
            hfId.Value = "0";
            BindgvDoctor();
        }

        private void BindGridView()
        {
            using (IVisionMissionRepository objVisionMissionRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
            {
                grdUser.DataSource = objVisionMissionRepository.GetAllVisionMissionMaster(1);
                grdUser.DataBind();
            }
        }

        private bool LoadControlsAdd(GetAllVisionMissionMasterByLangIdResult objBo)
        {
            if (string.IsNullOrWhiteSpace(CKEditorControl1.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Vision Mission Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Descr = CKEditorControl1.Text.Trim();
            }

            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;

            if (ddlLanguage.SelectedIndex > 0)
            {
                Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }

            objBo.IsVisible = chkIsActive.Checked;

            return true;
        }

        #endregion

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
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearControlValues();

                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IVisionMissionRepository objVisionMissionRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objVisionMissionRepository.GetVisionMissionMasterByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (dataInfo != null)
                    {
                        CKEditorControl1.Text = dataInfo.Descr;
                        
                        GetListValueDoctor = Functions.ToDataTable(objVisionMissionRepository.GetAllVisionMissionMasterImageDetailsByLangId(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)).Select(x=> new VisionMissionImageModel { Id=x.Id,ImageName=x.ImageName,FileFullPath= ConfigDetailsValue.VisionMissionFilePath +x.ImageName }).ToList());
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

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IVisionMissionRepository objVisionMissionRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
            {
                GetAllVisionMissionMasterByLangIdResult objBO = new GetAllVisionMissionMasterByLangIdResult();
                if (LoadControlsAdd(objBO))
                {
                    if (!objVisionMissionRepository.InsertOrUpdateVisionMissionMaster(objBO, Convert.ToInt32(ddlLanguage.SelectedValue), out errorMessage))
                    {
                        string strE;
                        objVisionMissionRepository.RemoveUnitDoctor(objBO.Id, out strE);
                        if (GetListValueDoctor.Rows.Count > 0)
                        {
                            foreach (DataRow row in GetListValueDoctor.Rows)
                            {
                                if (row["Id"].ToString().Contains("Temp_"))
                                {

                                    if (objVisionMissionRepository.InsertUnitDoctor(new GetAllVisionMissionMasterImageDetailsByVisionIdAndLangIdResult { VisionId=objBO.Id, ImageName= row["ImageName"].ToString() ,LanguageId =Convert.ToInt32(ddlLanguage.SelectedValue)}, out errorMessage))
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

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IVisionMissionRepository objVisionMissionRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
            {
                GetAllVisionMissionMasterByLangIdResult objBO = new GetAllVisionMissionMasterByLangIdResult();
                if (LoadControlsAdd(objBO))
                {
                    if (!objVisionMissionRepository.InsertOrUpdateVisionMissionMaster(objBO, Convert.ToInt32(ddlLanguage.SelectedValue), out errorMessage))
                    {
                        if (GetListValueDoctor.Rows.Count > 0)
                        {
                            foreach (DataRow row in GetListValueDoctor.Rows)
                            {
                                if (row["Id"].ToString().Contains("Temp_"))
                                {

                                    if (objVisionMissionRepository.InsertUnitDoctor(new GetAllVisionMissionMasterImageDetailsByVisionIdAndLangIdResult { VisionId = objBO.Id, ImageName = row["ImageName"].ToString(), LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue) }, out errorMessage))
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


        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {

            try
            {
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
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IVisionMissionRepository objAcc = new VisionMissionRepository(Functions.strSqlConnectionString))
            {
                ddlLanguage.SelectedIndex = 1;
                var dataInfo = objAcc.GetVisionMissionMasterByLangId((Convert.ToInt32(hfId.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    ddlLanguage.Enabled = true;
                    txtMetaTitle.Text = dataInfo.MetaTitle;
                    txtMetaDescription.Text = dataInfo.MetaDescription;
                    CKEditorControl1.Text = dataInfo.Descr;
                    
                    chkIsActive.Checked = dataInfo.IsVisible==null?false :(bool)dataInfo.IsVisible;
                    
                    GetListValueDoctor = Functions.ToDataTable(objAcc.GetAllVisionMissionMasterImageDetailsByLangId(dataInfo.Id, Convert.ToInt32(ddlLanguage.SelectedValue)).Select(x => new VisionMissionImageModel { Id = x.Id, ImageName = x.ImageName, FileFullPath = ConfigDetailsValue.VisionMissionFilePath + x.ImageName }).ToList()); 
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
                using (IVisionMissionRepository objAcc = new VisionMissionRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveVisionMissionMaster(rowId, out errorMessage))
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

        #region Bind Doctor Detail
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
                    result.Columns.Add(new DataColumn("ImageName", typeof(string)));
                    result.Columns.Add(new DataColumn("FileFullPath", typeof(string)));
                }

                return result;
            }
            set
            {
                ViewState["someID"] = value;
            }
        }
        

        private void BindgvDoctor()
        {

            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            bool isNoError = true;
            string strFilePath = "";
            string filePath = ConfigDetailsValue.VisionMissionFilePath;
            if (fuImageUpload.HasFile)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fuImageUpload.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                //decimal size = Math.Round(((decimal)fuImageUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 360 || width != 600)
                //{
                //    Functions.MessagePopup(this, "Please upload 600px*360px.", PopupMessageType.error);
                //    return;
                //}
            }
            if (!fuImageUpload.HasFile)
            {
                Functions.MessagePopup(this, "Please Select File.", PopupMessageType.error);
                return;
            }
            else
            {
                if (fuImageUpload.HasFile)
                {

                    if (!filePath.Contains("|"))
                    {
                        //if (fuImageUpload.PostedFile.ContentLength > 1048576)
                        //{
                        //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        //    return ;
                        //}
                        strFilePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuImageUpload.FileName);

                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + strFilePath;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        string fileName = Path.GetFileName(fuImageUpload.FileName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        if (IsMediaFile(ext))
                        {
                            // Check to see if a file already exists with the
                            // same name as the file to upload.
                            if (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                File.Delete(pathToCheck1);
                            }

                            //Save selected file into specified location
                            fuImageUpload.SaveAs(Server.MapPath(filePath) + "/" + strFilePath);
                        }
                        else
                        {

                            Functions.MessagePopup(this, "Please upload only \".png,.jpg,.jpeg\".", PopupMessageType.warning);
                            return ;

                        }
                    }
                    else
                    {

                        Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                        return ;
                    }
                }
                else
                {
                    strFilePath = System.IO.Path.GetFileName(imgProfile.ImageUrl);
                }
            }
            if (isNoError)
            {
                DataTable dt = null;
                dt = GetListValueDoctor;
                DataRow dr = dt.NewRow();
                int rowCount = 0;
                if (dt.Rows.Count > 0)
                {
                    rowCount = dt.Rows.Count;
                }
                dr["Id"] = "Temp_" + (rowCount + 1);
                dr["ImageName"] = strFilePath;
                dr["FileFullPath"] = filePath+strFilePath;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                GetListValueDoctor = dt;
            }
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            ShowHideControl(VisibityType.Edit);
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

            if (hfId.Value != "0" && string.IsNullOrWhiteSpace(hfId.Value) && !rowId.Contains("Temp_"))
            {
                using (IVisionMissionRepository objVisionMissionRepository = new VisionMissionRepository(Functions.strSqlConnectionString))
                {
                    if (!objVisionMissionRepository.RemoveUnitDoctor(Convert.ToInt32(rowId), out strError))
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
        #endregion
    }
}