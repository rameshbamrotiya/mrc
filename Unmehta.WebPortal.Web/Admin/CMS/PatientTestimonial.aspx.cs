using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Unmehta.WebPortal.Common;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class PatientTestimonial : System.Web.UI.Page
    {
        static string[] mediaExtensions = {
    ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF"//, //etc
    //".AVI", ".MP4", ".DIVX", ".WMV", //etc
};
        static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }
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
                ClearControlValues();
                BindGridView();
            }
        }

        private bool LoadControlsAdd(PatientTestimonialModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfId.Value);
            }

            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
                    {
                        if (objRoleMasterRepository.GetAllPatientTestimonial().Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequence No Already Assign", PopupMessageType.error);
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
                                Functions.MessagePopup(this, "This Swap From And To Sequence No is Same", PopupMessageType.error);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Sequence No Must Be more then 0.", PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sequence No", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtPatientName.Text.Trim()))
            {
                objBo.PatientName = txtPatientName.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter PatientName", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtExternalLink.Text.Trim()))
            {
                objBo.ExternalLink = txtExternalLink.Text.Trim();
            }
            //else
            //{
            //    Functions.MessagePopup(this, "Please Enter External Link", PopupMessageType.error);
            //    return false;
            //}

            if (!string.IsNullOrEmpty(txtCityName.Text.Trim()))
            {
                objBo.CityName = txtCityName.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter City Name", PopupMessageType.error);
                return false;
            }


            if (fuPicOrVideo.HasFile)
            {
                string filePath = ConfigDetailsValue.PatientTestimonialUploadPath;

                if (!filePath.Contains("|"))
                {
                    //if (fuPicOrVideo.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.FilePath = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPicOrVideo.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.FilePath;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(fuPicOrVideo.FileName);
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
                        fuPicOrVideo.SaveAs(Server.MapPath(filePath) + "/" + objBo.FilePath);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only \".png,.jpg,.jpeg,.avi,.mp4\".", PopupMessageType.warning);
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
                objBo.FilePath = System.IO.Path.GetFileName(hfLeftImage.Value);
            }


            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                objBo.Description = txtDescription.Text;
            }



            objBo.IsActive = chkIsActive.Checked;

            return true;
        }

        private void BindGridView()
        {
            grdUser.DataBind();
        }

        private void ClearControlValues()
        {
            hfId.Value = "0";
            txtPatientName.Text = "";

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            txtCityName.Text = "";
            txtDescription.Text = "";
            drpChangeSequanceMethod.SelectedIndex = 0;

            dvSwapSequance.Visible = false;
            dvDrpSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;

            txtDescription.Text = "";
            chkIsActive.Checked = false;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
            {
                PatientTestimonialModel objBO = new PatientTestimonialModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdatePatientTestimonial(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {

                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
            using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
            {
                var data = objRoleMasterRepository.GetAllPatientTestimonialsById(rowId);
                if (data != null)
                {
                    hfId.Value = data.Id.ToString();
                    txtPatientName.Text = data.PatientName;
                    txtDescription.Text = data.Description;
                    chkIsActive.Checked = (bool)data.IsActive;
                    txtSquanceNo.Text = data.SequanceNo.ToString();
                    txtCityName.Text = data.CityName;
                    txtExternalLink.Text = data.ExternalLink;
                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;

                    if (!string.IsNullOrWhiteSpace(data.FilePath))
                    {
                        hfLeftImage.Value = ConfigDetailsValue.PatientTestimonialUploadPath + "/" + data.FilePath;

                        lblLeftImage.Text = hfLeftImage.Value;
                        aRemoveLeft.Visible = true;

                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occurs", PopupMessageType.error);
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
                using (IPatientTestimonialRepository objRoleMasterRepository = new PatientTestimonialRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemovePatientTestimonial(rowId, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
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
    }
}