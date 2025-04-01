using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Data;
using BAL;
using System.Configuration;
using Unmehta.WebPortal.Common;
using System.IO;
using System.Web.Services;
using System.Text;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class Facilites : System.Web.UI.Page
    {
        #region Facilites

        #region Page Method
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
            if (!IsPostBack)
            {
                dvSubDEtails.Visible = false;
                BindDropDown();
                ShowHideControl(VisibityType.GridView);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                FacilitesMasterGridModel objBO = new FacilitesMasterGridModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objFacilitesMasterRepository.InsertOrUpdateFacilitesMaster(objBO, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (grdUser.DataKeys[rowindex]["Id"].ToString());
            hfID.Value = rowId;
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objFacilitesMasterRepository.GetFacilitesMasterById((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    BindDropDown();
                    dvSubDEtails.Visible = true;
                    txtFacilitesName.Text = dataInfo.FacilitesName;
                    chkEnable.Checked = (bool)dataInfo.IsVisible;
                    BindgvDoctor();
                    ClearSubDetails();
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
                using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
                {
                    objFacilitesMasterRepository.RemoveFacilitesMaster(rowId, out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
                {
                    string errorMessage = "";
                    FacilitesMasterGridModel objBo = new FacilitesMasterGridModel();
                    
                    if (LoadControlsAdd(objBo))
                    {
                        if (!objFacilitesMasterRepository.InsertOrUpdateFacilitesMaster(objBo, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                            ClearControlValues();
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                txtSearch.Text = "";
                BindGridView();
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
                {

                    if (hfID.Value != "" && hfID.Value != "0")
                    {
                        var dataInfo = objFacilitesMasterRepository.GetFacilitesMasterById((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                        if (dataInfo != null)
                        {
                            BindDropDown();
                            txtFacilitesName.Text = dataInfo.FacilitesName;
                            chkEnable.Checked = (bool)dataInfo.IsVisible;
                            //if (!string.IsNullOrWhiteSpace(dataInfo.ImagePath))
                            //{
                            //    imgProfile.ImageUrl = ConfigDetailsValue.AddFacilitesFileUploadPath + dataInfo.ImagePath;
                            //}
                            BindgvDoctor();
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language First", PopupMessageType.warning);
                return;
            }
        }
        #endregion

        #region Page Functions

        private void BindDropDown()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
            ddlLanguage.SelectedIndex = 1;
            BindGridView();
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
                    ddlLanguage.Enabled = false;
                    ClearControlValues();
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    ddlLanguage.Enabled = true;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtFacilitesName.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView(long lgLangId=1)
        {
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                List<FacilitesMasterGridModel> data= objFacilitesMasterRepository.GetAllFacilitesMaster(lgLangId).ToList();
                if(!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    data= objFacilitesMasterRepository.GetAllFacilitesMaster(lgLangId).Where(x=> x.FacilitesName.Contains(txtSearch.Text)).ToList();
                }
                else
                {
                    data = objFacilitesMasterRepository.GetAllFacilitesMaster(lgLangId).ToList();
                }
                grdUser.DataSource = data.ToList();
                grdUser.DataBind();
            }
        }

        private bool LoadControlsAdd(FacilitesMasterGridModel objBo)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Select Language First", PopupMessageType.error);
                return false;
            }
            if (!string.IsNullOrEmpty(txtFacilitesName.Text))
            {
                objBo.FacilitesName = txtFacilitesName.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Enter Facilites Name", PopupMessageType.error);
                return false;
            }

            objBo.IsVisible = chkEnable.Checked;

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

        #endregion

        #region Facilites Sub Images Details

        #region Page Sub Methods
        protected void ibtn_DoctorEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (gvDoctor.DataKeys[rowindex]["Id"].ToString());
            hfSubDetails.Value = rowId;
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objFacilitesMasterRepository.GetFacilitesImageMasterById((Convert.ToInt32(hfSubDetails.Value)), (Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    txtFacilitesImageInfo.Text = dataInfo.FacilitesFileInfo;
                    txtSquanceNo.Text = dataInfo.SequanceNo.ToString();
                    chkImageEnable.Checked = (bool)dataInfo.IsVisible;
                    if (!string.IsNullOrWhiteSpace(dataInfo.FacilitesFileName))
                    {
                        imgProfile.ImageUrl =  dataInfo.FacilitesFileName;
                    }

                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;

                    BindgvDoctor();
                    ShowHideControl(VisibityType.Edit);
                }
            }
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (gvDoctor.DataKeys[rowindex]["Id"].ToString());
            GridViewRow gvRow = gvDoctor.Rows[rowindex];
            hfSubDetails.Value = rowId;
            if (hfSubDetails.Value != "0" && string.IsNullOrWhiteSpace(rowId))
            {
                using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objFacilitesMasterRepository.RemoveFacilitesMasterImageDetails(Convert.ToInt32(rowId), out strError))
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
            BindDropDown();
            ShowHideControl(VisibityType.Edit);
        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            bool isNoError = true;
            string fileName = "", errorMessage="";
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                
                FacilitesMasterImageGridModel objBO = new FacilitesMasterImageGridModel();
            if (LoadControlsSubAdd(objBO))
            {
                if (!objFacilitesMasterRepository.InsertOrUpdateFacilitesMasterImageDetails(objBO, out errorMessage))
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        ClearSubDetails();
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
            }
            }
            BindgvDoctor();
            ShowHideControl(VisibityType.Edit);
        }
        
        protected void btnClearDetails_ServerClick(object sender, EventArgs e)
        {
            ClearSubDetails();
        }
        #endregion

        #region Page Sub Functions 
        private void ClearSubDetails()
        {
            chkImageEnable.Checked = false;
            hfSubDetails.Value = "0";
            txtFacilitesImageInfo.Text = "";

            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            drpChangeSequanceMethod.SelectedIndex = 0;

            dvSwapSequance.Visible = false;
            dvDrpSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;
        }

        private void BindgvDoctor()
        {
            using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objFacilitesMasterRepository.GetAllFacilitesImageMaster((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue)).ToList();

                data.ForEach(x => {
                    x.FacilitesFilePath = ConfigDetailsValue.AddFacilitesFileUploadPath + "/" + x.FacilitesFileName;
                });

                gvDoctor.DataSource = data.OrderBy(x=> x.SequanceNo).ToList();
                gvDoctor.DataBind();
            }
        }

        private bool LoadControlsSubAdd(FacilitesMasterImageGridModel objBO)
        {
            if (fuFileUpload.HasFile)
            {
                string filePath = ConfigDetailsValue.AddFacilitesFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    //if (fuFileUpload.PostedFile.ContentLength > 1048576)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBO.FacilitesFileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);
                    filePath = ConfigDetailsValue.AddFacilitesFileUploadPath + "/" + objBO.FacilitesFileName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(ConfigDetailsValue.AddFacilitesFileUploadPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(ConfigDetailsValue.AddFacilitesFileUploadPath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    FileInfo fi = new FileInfo(objBO.FacilitesFileName);
                    string ext = fi.Extension;
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuFileUpload.SaveAs(Server.MapPath(ConfigDetailsValue.AddFacilitesFileUploadPath) + "/" + objBO.FacilitesFileName);
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
                objBO.FacilitesFileName = imgProfile.ImageUrl;
            }

            if (!string.IsNullOrEmpty(txtFacilitesImageInfo.Text))
            {
                objBO.FacilitesFileInfo = txtFacilitesImageInfo.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Enter Facilites File Info", PopupMessageType.error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBO.FacilitiesId = 0;
            }
            else
            {
                objBO.FacilitiesId = Convert.ToInt32(hfID.Value);
            }
            if (string.IsNullOrWhiteSpace(hfSubDetails.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfSubDetails.Value);
            }
            if (ddlLanguage.SelectedIndex > 0)
            {
                objBO.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Select Language First", PopupMessageType.error);
                return false;
            }


            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IFacilitesMasterRepository objFacilitesMasterRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (objFacilitesMasterRepository.GetAllFacilitesImageMaster(objBO.Id, objBO.LanguageId).Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBO.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequance No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBO.SequanceNo = Convert.ToInt32(txtSquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtSwapSquanceNo.Text.Trim()))
                        {

                            objBO.SwapType = drpChangeSequanceMethod.SelectedValue;
                            objBO.SwapFromSequanceNo = objBO.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBO.SwapToSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());


                            if (objBO.SwapFromSequanceNo == objBO.SwapToSequanceNo)
                            {
                                Functions.MessagePopup(this, "This Swap From And To Sequance No is Same", PopupMessageType.error);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Sequance No Must Be more then 0.", PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sequance No", PopupMessageType.error);
                return false;
            }

            objBO.IsVisible = chkImageEnable.Checked;

            return true;
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