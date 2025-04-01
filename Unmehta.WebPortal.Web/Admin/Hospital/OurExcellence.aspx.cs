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
using Unmehta.WebPortal.Common;
using BAL;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Text;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class OurExcellence : System.Web.UI.Page
    {
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
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearFormData();
                BindDepartment();
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView(txtSearch.Text.Trim());
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindGridView();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = "";
            OurExcellenceMasterGridModel objBo = new OurExcellenceMasterGridModel();
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                if (fuFileUpload.HasFile)
                {
                    filePath = ConfigDetailsValue.AddOurExcellenceFileUploadPath;

                    if (!filePath.Contains("|"))
                    {

                        objBo.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmsstt") + System.IO.Path.GetExtension(fuFileUpload.FileName);

                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 =  filePath + objBo.ImageName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        objBo.FileFullPath = pathToCheck1;
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(Server.MapPath(pathToCheck1));
                        }

                        //Save selected file into specified location
                        fuFileUpload.SaveAs(Server.MapPath(filePath) + objBo.ImageName);
                    }
                    else
                    {
                        string errorMessageFile = filePath.Split('|')[0];
                        bool isValidate = false;
                    }
                }
                else
                {
                    objBo.FileFullPath = hfLeftImage.Value;
                }


                if (fuSideImage.HasFile)
                {
                    filePath = ConfigDetailsValue.AddOurExcellenceFileUploadPath;

                    if (!filePath.Contains("|"))
                    {

                        var filename = DateTime.Now.ToString("ddMMyyyyttthhmmss") + System.IO.Path.GetExtension(fuSideImage.FileName);

                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = filePath + filename;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";
                        objBo.AddImage = pathToCheck1;
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(Server.MapPath(pathToCheck1));
                        }

                        //Save selected file into specified location
                        fuSideImage.SaveAs(Server.MapPath(filePath) + filename);
                    }
                    else
                    {
                        string errorMessageFile = filePath.Split('|')[0];
                        bool isValidate = false;
                    }
                }
                else
                {
                    objBo.AddImage = hfRightImage.Value;
                }


                if (ddlDepartment.SelectedIndex <= 0)
                {
                    Functions.MessagePopup(this, "Please Select DepartMent", PopupMessageType.error);
                    return;
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
                                return;
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
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please Sequence No Must Be more then 0.", PopupMessageType.error);
                        return;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Sequence No", PopupMessageType.error);
                    return;
                }


                if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                    objBo.MetaTitle = txtMetaTitle.Text;

                if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                    objBo.MetaDescription = txtMetaDescription.Text;


                objBo.Id = Convert.ToInt32(hfRowId.Value);
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
                objBo.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                objBo.SequanceNo = Convert.ToInt32(txtSquanceNo.Text);

                if (!string.IsNullOrEmpty(txtSideImageURL.Text))
                    objBo.SideImageURL = txtSideImageURL.Text;

                if (!string.IsNullOrEmpty(txtExternalVideoLink.Text))
                    objBo.ExternalVideoLink = txtExternalVideoLink.Text;

                if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                    objBo.MetaTitle = txtMetaTitle.Text;

                if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                    objBo.MetaDescription = txtMetaDescription.Text;

                objBo.IsVisible = chkEnable.Checked;
                objBo.IsFacility = chkAddIn.Checked;
                objBo.IsAddInOtherDepartment = chkAddInOtherDepartment.Checked;
                string errorMessage = "Saved Successfully.";
                if (!objOurExcellenceMasterRepository.InsertOrUpdateTblOurExcellenceMaster(objBo, out errorMessage))
                {
                    ClearFormData();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
            }
        }
        #endregion        

        #region Page Method

        private void ClearFormData()
        {
            hfRowId.Value = "0";
            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            txtSideImageURL.Text = "";
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            txtExternalVideoLink.Text = "";
            hfLeftImage.Value = "";
            hfRightImage.Value = "";

            ddlLanguage.Enabled = false;

            dvSwapSequance.Visible = false;
            dvDrpSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;


            divForm.Visible = false;
            divGrid.Visible = true;


            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            hfRightImage.Value = "";
            lblRightImage.Text = "";
            aRemoveRight.Visible = false;

            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            BindDepartment();
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView(string strSearch = "")
        {
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                List<OurExcellenceMasterGridModel> gvData = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(1).OrderBy(x=> x.SequanceNo).ToList();
                if (!string.IsNullOrWhiteSpace(strSearch))
                {
                    gvData = objOurExcellenceMasterRepository.GetAllTblOurExcellenceMaster(1).Where(x => x.DepartmentName.Contains(strSearch)).OrderBy(x => x.SequanceNo).ToList();
                }
                if (gvData != null)
                {
                    grdUser.DataSource = gvData;
                    grdUser.DataBind();
                }
            }
        }

        private void BindDepartment()
        {
            divForm.Visible = false;
            divGrid.Visible = true;
            ddlDepartment.Items.Clear();
            using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
            {
                ddlDepartment.DataSource = objDepartmentRepository.GetAllTblDepartmentForDropDown(Convert.ToInt32(ddlLanguage.SelectedValue));
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }
        #endregion

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            divForm.Visible = true;
            divGrid.Visible = false;
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfRowId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objOurExcellenceMasterRepository.GetTblOurExcellenceMasterById((Convert.ToInt32(hfRowId.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue)));
                if (dataInfo != null)
                {
                    ListItem li = ddlDepartment.Items.FindByValue(dataInfo.DepartmentId.ToString());
                    if (li != null)
                    {
                        ddlDepartment.SelectedValue = dataInfo.DepartmentId.ToString();
                    }
                    txtSquanceNo.Text = dataInfo.SequanceNo.ToString();

                    lblLeftImage.Text = dataInfo.FileFullPath;
                    hfLeftImage.Value = dataInfo.FileFullPath;
                    hfRightImage.Value = dataInfo.AddImage;
                    lblRightImage.Text = dataInfo.AddImage;

                    if (!string.IsNullOrEmpty(dataInfo.AddImage))
                    {
                        aRemoveRight.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(dataInfo.FileFullPath))
                    {
                        aRemoveLeft.Visible = true;
                    }

                    chkEnable.Checked = dataInfo.IsVisible==null?false:(bool)dataInfo.IsVisible;
                    chkAddIn.Checked = dataInfo.IsFacility==null? false: (bool)dataInfo.IsFacility;
                    chkAddInOtherDepartment.Checked = dataInfo.IsAddInOtherDepartment==null? false:(bool) dataInfo.IsAddInOtherDepartment;

                    txtExternalVideoLink.Text = dataInfo.ExternalVideoLink;
                    txtSideImageURL.Text = dataInfo.SideImageURL;
                    txtMetaTitle.Text = dataInfo.MetaTitle;
                    txtMetaDescription.Text = dataInfo.MetaDescription;

                    ddlLanguage.Enabled = true;

                    divForm.Visible = true;
                    divGrid.Visible = false;

                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;
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
                using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objOurExcellenceMasterRepository.RemoveTblOurExcellenceMaster(rowId, out errorMessage))
                    {

                        ClearFormData();
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (hfRowId.Value == "0")
            {
                Functions.MessagePopup(this, "Select Excellence First For Update", PopupMessageType.error);
                return;
            }
            else if (ddlLanguage.SelectedIndex > 0)
            {

                using (IDepartmentRepository objDepartmentRepository = new DepartmentRepository(Functions.strSqlConnectionString))
                {
                    ddlDepartment.DataSource = objDepartmentRepository.GetAllTblDepartment(Convert.ToInt32(ddlLanguage.SelectedValue));
                    ddlDepartment.DataValueField = "Id";
                    ddlDepartment.DataTextField = "DepartmentName";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
                }
                using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
                {
                    var dataInfo = objOurExcellenceMasterRepository.GetTblOurExcellenceMasterById(Convert.ToInt32(hfRowId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (dataInfo != null)
                    {
                        ddlDepartment.SelectedValue = dataInfo.DepartmentId.ToString();
                        txtSquanceNo.Text = dataInfo.SequanceNo.ToString();

                        hfLeftImage.Value = dataInfo.FileFullPath;
                        hfRightImage.Value = dataInfo.AddImage;
                        if (!string.IsNullOrWhiteSpace(dataInfo.FileFullPath))
                        {
                            aRemoveLeft.Visible = true;
                        }
                        if (!string.IsNullOrWhiteSpace(dataInfo.AddImage))
                        {
                            aRemoveRight.Visible = true;
                        }
                        chkEnable.Checked = (bool)dataInfo.IsVisible;

                        txtExternalVideoLink.Text = dataInfo.ExternalVideoLink;

                        txtMetaTitle.Text = dataInfo.MetaTitle;
                        txtMetaDescription.Text = dataInfo.MetaDescription;

                    }
                    else
                    {
                        ddlDepartment.SelectedIndex = 0;
                        txtSquanceNo.Text = "";

                        hfLeftImage.Value = "";
                        hfRightImage.Value = "";
                        aRemoveLeft.Visible = false;
                        aRemoveRight.Visible = false;

                        txtExternalVideoLink.Text = "";
                        txtMetaTitle.Text = "";
                        txtMetaDescription.Text = "";
                        chkEnable.Checked = false;
                    }
                }
            }
        }
    }
}