using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientsEducationBrochureMaster : System.Web.UI.Page
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
            if (!IsPostBack)
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
                    ddlLanguage.SelectedIndex = 1;
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
            hfID.Value = "0";
            txtPatientsEducationBrochureName.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;


            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            drpChangeSequanceMethod.SelectedIndex = 0;

            dvSwapSequance.Visible = false;
            dvDrpSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;
        }

        private void BindGridView(string strSearch="")
        {
            using (IPatientsEducationBrochureRepository objPatientsEducationBrochureRepository = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
            {
                List<GetAllPatientsEducationBrochureMasterResult> Data = objPatientsEducationBrochureRepository.GetAllPatientsEducationBrochure(1);

                if(!string.IsNullOrWhiteSpace(strSearch))
                {
                    Data = Data.Where(x => x.Name.Contains(strSearch)).ToList();
                }
                grdUser.DataSource = Data;
                grdUser.DataBind();
            }
        }
        
        private bool LoadControlsAdd(PatientBrochuerModel objBo)
        {
            if (string.IsNullOrWhiteSpace(txtPatientsEducationBrochureName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Patients Education Brochur Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Name = txtPatientsEducationBrochureName.Text.Trim();
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
                string filePath = ConfigDetailsValue.PatientsEduFrontImage;

                if (!filePath.Contains("|"))
                {
                    //if (fuPic.PostedFile.ContentLength > 1048576)
                    //{
                    //    Functions.MessagePopup(this, "Image File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.FrontImage = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPic.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.FrontImage;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.FrontImage);
                    FileInfo fi = new FileInfo(fileName);
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
                        fuPic.SaveAs(Server.MapPath(filePath) + objBo.FrontImage);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Image Please upload only \".png,.jpg,.jpeg\".", PopupMessageType.warning);
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
                objBo.FrontImage = System.IO.Path.GetFileName(imgProfile.ImageUrl);
            }

            if (fuPDF.HasFile)
            {
                string filePath = ConfigDetailsValue.PatientsEduPDF;

                if (!filePath.Contains("|"))
                {
                    //if (fuPDF.PostedFile.ContentLength > 1048576)
                    //{
                    //    Functions.MessagePopup(this, "PDF File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.Pdf = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPDF.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.Pdf;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.Pdf);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext == ".pdf" )
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuPDF.SaveAs(Server.MapPath(filePath) + objBo.Pdf);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only \".pdf\" in PDF upload", PopupMessageType.warning);
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
                objBo.Pdf = System.IO.Path.GetFileName(Image1.ImageUrl);
            }


            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
                    {
                        if (objRoleMasterRepository.GetAllPatientTestimonial().Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
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


                            if (objBo.SwapFromSequanceNo == objBo.SwapToSequanceNo)
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                using (IPatientsEducationBrochureRepository objAcc = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objAcc.GetPatientsEducationBrochureById((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (dataInfo != null)
                    {
                        txtPatientsEducationBrochureName.Text = dataInfo.Name;
                        if (!string.IsNullOrWhiteSpace(dataInfo.FrontImage))
                        {
                            imgProfile.ImageUrl = ConfigDetailsValue.PatientsEduFrontImage + dataInfo.FrontImage;
                        }
                        if (!string.IsNullOrWhiteSpace(dataInfo.Pdf))
                        {
                            Image1.ImageUrl = ConfigDetailsValue.PatientsEduPDF + dataInfo.Pdf;
                        }
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
            using (IPatientsEducationBrochureRepository objPatientsEducationBrochureRepository = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
            {
                PatientBrochuerModel objBO = new PatientBrochuerModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objPatientsEducationBrochureRepository.InsertOrUpdatePatientsEducationBrochure(objBO, out errorMessage))
                    {
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
            using (IPatientsEducationBrochureRepository objPatientsEducationBrochureRepository = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
            {
                PatientBrochuerModel objBO = new PatientBrochuerModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objPatientsEducationBrochureRepository.InsertOrUpdatePatientsEducationBrochure(objBO, out errorMessage))
                    {
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
                txtSearch.Text = string.Empty;
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
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IPatientsEducationBrochureRepository objPatientsEducationBrochureRepository = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
            {
                ddlLanguage.SelectedIndex = 1;
                var dataInfo = objPatientsEducationBrochureRepository.GetPatientsEducationBrochureById((Convert.ToInt32(hfID.Value)), Convert.ToInt32(ddlLanguage.SelectedValue));
                if (dataInfo != null)
                {
                    ddlLanguage.Enabled = true;
                    txtPatientsEducationBrochureName.Text = dataInfo.Name;
                    if (!string.IsNullOrWhiteSpace(dataInfo.FrontImage))
                    {
                        imgProfile.ImageUrl = ConfigDetailsValue.PatientsEduFrontImage + dataInfo.FrontImage;
                    }
                    if (!string.IsNullOrWhiteSpace(dataInfo.Pdf))
                    {
                        Image1.ImageUrl = ConfigDetailsValue.PatientsEduPDF + dataInfo.Pdf;
                    }
                    txtSquanceNo.Text = dataInfo.SequanceNo.ToString();

                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;

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
                using (IPatientsEducationBrochureRepository objPatientsEducationBrochureRepository = new PatientsEducationBrochureRepository(Functions.strSqlConnectionString))
                {

                    if (!objPatientsEducationBrochureRepository.RemovePatientsEducationBrochure(rowId, out errorMessage))
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

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView(txtSearch.Text.Trim());
        }
    }
}