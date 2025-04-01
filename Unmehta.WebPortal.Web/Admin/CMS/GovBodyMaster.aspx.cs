using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using System.IO;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class GovBodyMaster : System.Web.UI.Page
    {
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            try
            {

                if (!Page.IsPostBack)
                {
                    ddlLanguage.SelectedIndex = 1;
                    ClearControlValues();
                    ClearSubControlValues();
                    BindPageDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        
        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IGoverningBoardMasterRepository objIGoverningBoardMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
            {
                GoverningBoardMasterModel objBO = new GoverningBoardMasterModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objIGoverningBoardMasterRepository.InsertOrUpdateGoverningBoardMaster(objBO, out errorMessage))
                    {
                        ddlLanguage.Enabled = false;
                        BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {

                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }
        #endregion

        #region Page Functions
        private bool LoadControlsAdd(GoverningBoardMasterModel objBo)
        {

            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtPageDescription.Text.Trim()))
            {
                objBo.PageDescription = txtPageDescription.Text;
            }


            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;


            objBo.IsActive = true;

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

        private bool LoadControlsAddSub(GoverningBoardMasterDesignatedDetailsModel objBo)
        {
            if (string.IsNullOrWhiteSpace(hfID.Value) && hfID.Value == "0")
            {
                Functions.MessagePopup(this, "Please Save Governing Board Details First", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.GovBoardId = Convert.ToInt32(hfID.Value);
            }

            objBo.Id = Convert.ToInt32(hfSubId.Value);

            if (string.IsNullOrWhiteSpace(txtPersonName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Person Full Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.DesignatedName = txtPersonName.Text.Trim();
            }

            objBo.IsActive = chkIsActive.Checked;

            //if (ddlDesignationList.SelectedIndex > 0)
            //{
            //    objBo.DecId = Convert.ToInt32(ddlDesignationList.SelectedValue);
            //}
            //else
            //{
            //    Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(txtDesignationDetails.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Designation Details", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.DesignationName = txtDesignationDetails.Text;
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
                string filePath = ConfigDetailsValue.GovImagePath;

                if (!filePath.Contains("|"))
                {
                    //if (fuPic.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.FilePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPic.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.FilePath;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.FilePath);
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
                        fuPic.SaveAs(Server.MapPath(filePath) + objBo.FilePath);
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
                objBo.FilePath = System.IO.Path.GetFileName( ResolveUrl(hfLeftImage.Value) );
            }

            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IGoverningBoardMasterRepository objRoleMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (objRoleMasterRepository.GetGoverningBoardMasterDesignationDetailDetails(objBo.GovBoardId, Convert.ToInt32(ddlLanguage.SelectedValue)).Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequance No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBo.SequanceNo = Convert.ToInt32(txtSquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtSwapSquanceNo.Text.Trim()))
                        {
                            objBo.SwapType = drpChangeSequanceMethod.SelectedValue;
                            objBo.SwapFromSequanceNo = objBo.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBo.SwapToSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());

                            if (objBo.SwapFromSequanceNo== objBo.SwapToSequanceNo)
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

            //objBo.ChkMode = 0;

            return true;

        }

        private void ClearSubControlValues()
        {
            //ddlDesignationList.SelectedIndex = 0;
            txtDesignationDetails.Text = "";
            txtPersonName.Text = "";
            hfSubId.Value = "0";
            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;
            chkIsActive.Checked = false;

            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            drpChangeSequanceMethod.SelectedIndex = 0;

            btnAddToList.Text = "Add To List";
            
            dvSwapSequance.Visible = false;
            dvDrpSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;
        }

        private void ClearControlValues()
        {
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            txtPageDescription.Text = "";
        }

        private void BindPageDetails()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
            ddlLanguage.SelectedIndex = 1;
            BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));

        }

        private void BindDataByLanguageId(long lgLangId)
        {
            //using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            //{
            //    ddlDesignationList.DataSource = objDesignationRepository.GetAllTblDesignationLang(lgLangId);
            //    ddlDesignationList.DataValueField = "Id";
            //    ddlDesignationList.DataTextField = "DesignationName";
            //    ddlDesignationList.DataBind();
            //    ddlDesignationList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
            //    ddlDesignationList.SelectedIndex = 0;
            //}
            if (lgLangId != 0)
            {
                using (IGoverningBoardMasterRepository objIGoverningBoardMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
                {
                    var data = objIGoverningBoardMasterRepository.GetGoverningBoardByLangId(lgLangId);
                    if (data != null)
                    {
                        ddlLanguage.Enabled = true;
                        hfID.Value = data.Id.ToString();
                        txtPageDescription.Text = data.PageDescription;
                        txtMetaTitle.Text = data.MetaTitle;
                        txtMetaDescription.Text = data.MetaDescription;
                        if (!string.IsNullOrWhiteSpace(hfID.Value) && hfID.Value != "0")
                        {
                            gvDoctor.DataSource = objIGoverningBoardMasterRepository.GetGoverningBoardMasterDesignationDetailDetails(Convert.ToInt32(hfID.Value), lgLangId);
                            gvDoctor.DataBind();
                        }
                    }
                    else
                    {
                        txtPageDescription.Text = "";
                        if (!string.IsNullOrWhiteSpace(hfID.Value) && hfID.Value != "0")
                        {
                            gvDoctor.DataSource = null;
                            gvDoctor.DataBind();
                        }
                    }
                }
            }
            else
            {
                txtPageDescription.Text = "";
                if (!string.IsNullOrWhiteSpace(hfID.Value) && hfID.Value != "0")
                {
                    gvDoctor.DataSource = null;
                    gvDoctor.DataBind();
                }
                ClearSubControlValues();
                ClearControlValues();
            }
        }
        #endregion

        #region Add Or Remove Sub List
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IGoverningBoardMasterRepository objIGoverningBoardMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
            {
                GoverningBoardMasterDesignatedDetailsModel objBO = new GoverningBoardMasterDesignatedDetailsModel();
                //if (fuPic.HasFile)
                //{
                //    System.Drawing.Image img = System.Drawing.Image.FromStream(fuPic.PostedFile.InputStream);
                //    int height = img.Height;
                //    int width = img.Width;
                //    decimal size = Math.Round(((decimal)fuPic.PostedFile.ContentLength / (decimal)1024), 2);
                //    if (height != 300 || width != 300)
                //    {
                //        Functions.MessagePopup(this, "Please upload 300px*300px.", PopupMessageType.error);
                //        return;
                //    }
                //}
                if (LoadControlsAddSub(objBO))
                {
                    if (!objIGoverningBoardMasterRepository.InsertOrUpdateGoverningBoardDesignationMaster(objBO, out errorMessage))
                    {
                        ddlLanguage.Enabled = false;
                        ClearSubControlValues();
                        BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        txtPersonName.Focus();
                        lblLeftImage.Text = "";
                        lblLeftImage.Visible = false;
                    }
                    else
                    {

                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvDoctor.DataKeys[rowindex]["Id"].ToString());
                using (IGoverningBoardMasterRepository objIGoverningBoardMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
                {

                    if (!objIGoverningBoardMasterRepository.RemoveGoverningBoardDesignationMaster(rowId, out errorMessage))
                    {
                        ddlLanguage.Enabled = false;
                        ClearSubControlValues();
                        BindDataByLanguageId(Convert.ToInt32(ddlLanguage.SelectedValue));
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

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubId.Value = gvDoctor.DataKeys[rowindex]["Id"].ToString();

            using (IGoverningBoardMasterRepository objIGoverningBoardMasterRepository = new GoverningBoardMasterRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objIGoverningBoardMasterRepository.GetGoverningBoardMasterDesignationDetailDetails((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue)).Where(x=> x.Id==Convert.ToInt32(hfSubId.Value)).FirstOrDefault();
                if (dataInfo != null)
                {
                    txtPersonName.Text = dataInfo.DesignatedPersonName;
                    //ddlDesignationList.SelectedValue = dataInfo.DesignationId.ToString();
                    txtDesignationDetails.Text = dataInfo.DesignationName;
                    txtSquanceNo.Text = dataInfo.SequanceNo.ToString();
                    chkIsActive.Checked = dataInfo.IsActive==null ? false: (bool)dataInfo.IsActive;
                    if (!string.IsNullOrWhiteSpace(dataInfo.FilePath))
                    {
                        //imgProfile.ImageUrl = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.FilePath;
                        lblLeftImage.Visible = true;
                        lblLeftImage.Text = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.FilePath;

                        hfLeftImage.Value = ConfigDetailsValue.AcademicMedicalFiles + dataInfo.FilePath;
                        aRemoveLeft.Visible = true;
                    }

                    btnAddToList.Text = "Update To List";

                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;
                }
            }
        }

        #endregion

    }
}