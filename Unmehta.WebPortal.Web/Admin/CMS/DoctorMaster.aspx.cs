using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public enum DoctorVisibityType
    {
        GridView,
        
        Insert,

        Edit,

        SpecializationQualification,

        ExpertiseAchievements,

        Publications
    }

    public partial class DoctorMaster : System.Web.UI.Page
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
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearControlValues();
                ShowHideControl(DoctorVisibityType.GridView);
            }
        }
        
        private void ShowHideControl(DoctorVisibityType e)
        {
            switch (e)
            {
                case DoctorVisibityType.GridView:
                    //lnkProfiletab.CssClass = "nav-link active";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link";
                    //lnkPublicationstab.CssClass = "nav-link";
                    pnlView.Visible = true;
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade ");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade");
                    break;
                case DoctorVisibityType.Insert:
                    //lnkProfiletab.CssClass = "nav-link active";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link";
                    pnlView.Visible = false;
                    //lnkPublicationstab.CssClass = "nav-link";
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade show active");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade");
                    break;
                case DoctorVisibityType.SpecializationQualification:
                    //lnkProfiletab.CssClass = "nav-link ";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link active";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link";
                    //lnkPublicationstab.CssClass = "nav-link";
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade ");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade show active");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade");
                    break;

                case DoctorVisibityType.Publications:
                    //lnkProfiletab.CssClass = "nav-link ";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link ";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link";
                    //lnkPublicationstab.CssClass = "nav-link active";
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade ");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade  show active");
                    break;
                case DoctorVisibityType.ExpertiseAchievements:
                    //lnkProfiletab.CssClass = "nav-link ";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link ";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link active";
                    //lnkPublicationstab.CssClass = "nav-link ";
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade ");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade  show active");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade ");
                    break;
                default:
                    //lnkProfiletab.CssClass = "nav-link active";
                    //lnkSpecializationQualificationtab.CssClass = "nav-link";
                    //lnkExpertiseAchievementstab.CssClass = "nav-link";
                    //lnkPublicationstab.CssClass = "nav-link";
                    Profiletabpanel.Attributes.Add("class", "tab-pane fade show active");
                    SpecializationQualificationtabpanel.Attributes.Add("class", "tab-pane fade");
                    ExpertiseAchievementstabpanel.Attributes.Add("class", "tab-pane fade");
                    Publicationstabpanel.Attributes.Add("class", "tab-pane fade");
                    break;
            }
        }

        #region Doctor Master
        private void BindGridView()
        {
            using (IDoctorMasterRepository objDoc = new DoctorMasterRepository(Functions.strSqlConnectionString))
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    grdUser.DataSourceID = null;
                    grdUser.DataSource = objDoc.GetAllDoctor().ToList().Where(x => x.DoctorDescription.Contains(txtSearch.Text)
                      || x.DoctorShotDescription.Contains(txtSearch.Text)
                      || x.DoctorFirstName.Contains(txtSearch.Text)
                      || x.DoctorMiddleName.Contains(txtSearch.Text)
                      || x.DoctorLastName.Contains(txtSearch.Text)
                      ).ToList();
                    grdUser.Visible = true;
                    grdUser.AllowPaging = true;
                    grdUser.DataBind();
                }
                else
                {
                    grdUser.DataSourceID = "sqlds";
                    grdUser.Visible = true;
                    grdUser.AllowPaging = true;
                    grdUser.DataBind();
                }
            }
        }

        private void ClearControlValues()
        {
            hfId.Value = "0";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtShortDescription.Text = "";
            txtDescription.Text = "";
            ddlLanguage.SelectedIndex=1;
            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            ddlLanguage.Enabled = false;
            dvNext.Visible = false;
            chkIsActive.Checked = false;
        }

        private bool LoadControlsAdd(DoctorMasterModel objBo)
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
            if (!string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                objBo.DoctorFirstName = txtFirstName.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this,"Please Enter FirstName", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtMiddleName.Text.Trim()))
            {
                objBo.DoctorMiddleName = txtMiddleName.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter MiddleName", PopupMessageType.error);
                return false;
            }

            if (ddlLanguage.SelectedIndex>0)
            {
                objBo.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }
            if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                objBo.DoctorLastName = txtLastName.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter LastName", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (objRoleMasterRepository.GetAllDoctor().Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
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
            


            if(fuProfilePic.HasFile)
            {
                string filePath = ConfigDetailsValue.DoctorProfilePicUploadPath;

                if (!filePath.Contains("|"))
                {
                    //if (fuProfilePic.PostedFile.ContentLength > 1048576)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.DoctorProfilePic = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuProfilePic.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.DoctorProfilePic;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(fuProfilePic.FileName);
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
                        fuProfilePic.SaveAs(Server.MapPath(filePath) + "/" + objBo.DoctorProfilePic);
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
                objBo.DoctorProfilePic = System.IO.Path.GetFileName(imgProfile.ImageUrl);
            }
        

            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                objBo.DoctorDescription = txtDescription.Text;
            }

            if (!string.IsNullOrEmpty(txtShortDescription.Text.Trim()))
            {
                objBo.DoctorShotDescription = txtShortDescription.Text;
            }
            

            objBo.IsActive = chkIsActive.Checked;

            return true;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(hfId.Value=="0")
            {
                Functions.MessagePopup(this, "Select Doctor First For Update", PopupMessageType.error);
                return;
            }
            else if (ddlLanguage.SelectedIndex > 0)
            {
                using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
                {
                    var data = objRoleMasterRepository.GetDoctorByIdAndLagId(Convert.ToInt32(hfId.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
                    if (data != null)
                    {
                        txtFirstName.Text = data.DoctorFirstName;
                        txtMiddleName.Text = data.DoctorMiddleName;
                        txtLastName.Text = data.DoctorLastName;
                        txtShortDescription.Text = data.DoctorShotDescription;
                        txtDescription.Text = data.DoctorDescription;
                    }
                    else
                    {
                        txtFirstName.Text = "";
                        txtMiddleName.Text = "";
                        txtLastName.Text = "";
                        txtShortDescription.Text = "";
                        txtDescription.Text = "";
                    }
                }
            }
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objRoleMasterRepository.GetDoctorByIdAndLagId(rowId,1);
                if (data != null)
                {
                    hfId.Value = data.Id.ToString();
                    txtFirstName.Text = data.DoctorFirstName;
                    txtMiddleName.Text = data.DoctorMiddleName;
                    txtLastName.Text = data.DoctorLastName;
                    txtShortDescription.Text = data.DoctorShotDescription;
                    txtDescription.Text = data.DoctorDescription;
                    txtSquanceNo.Text = data.SequanceNo.ToString();
                    ddlLanguage.SelectedValue = "1";
                    ddlLanguage.Enabled = true;


                    dvSwapSequance.Visible = true;
                    dvDrpSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;

                    chkIsActive.Checked = (bool)data.IsActive;
                    if(!string.IsNullOrWhiteSpace(data.DoctorProfilePic))
                    {
                        imgProfile.ImageUrl = ConfigDetailsValue.DoctorProfilePicUploadPath + "/" + data.DoctorProfilePic;
                    }
                    dvNext.Visible = true;
                    ShowHideControl(DoctorVisibityType.Insert);
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure", PopupMessageType.error);
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
                using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctor(rowId, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }

                    ShowHideControl(DoctorVisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
            {
                DoctorMasterModel objBO = new DoctorMasterModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctor(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(DoctorVisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        protected void btn_SaveAndNext_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
            {
                DoctorMasterModel objBO = new DoctorMasterModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctor(objBO, out errorMessage))
                    {
                        ClearControlValues();

                        hfId.Value = objBO.Id.ToString();
                        txtFirstName.Text = objBO.DoctorFirstName;
                        txtMiddleName.Text = objBO.DoctorMiddleName;
                        txtLastName.Text = objBO.DoctorLastName;
                        txtShortDescription.Text = objBO.DoctorShotDescription;
                        txtDescription.Text = objBO.DoctorDescription;
                        ddlLanguage.SelectedValue = objBO.LangId.ToString();
                        chkIsActive.Checked = (bool)objBO.IsActive;
                        if (!string.IsNullOrWhiteSpace(objBO.DoctorProfilePic))
                        {
                            imgProfile.ImageUrl = ConfigDetailsValue.DoctorProfilePicUploadPath + "/" + objBO.DoctorProfilePic;
                        }

                        BindGridView();
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorMasterRepository objRoleMasterRepository = new DoctorMasterRepository(Functions.strSqlConnectionString))
            {
                DoctorMasterModel objBO = new DoctorMasterModel();
                if (LoadControlsAdd(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctor(objBO, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(DoctorVisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(DoctorVisibityType.GridView);
        }
        
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (hfId.Value != "0")
            {
                BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.SpecializationQualification);
            }
            else
            {
                Functions.MessagePopup(this, "Please Oen Doctor Edit Row link then Access Again..", PopupMessageType.warning);
                ShowHideControl(DoctorVisibityType.GridView);
            }
        }
        #endregion

        #region Specialization and Qualification Master
        private void BindSpecializationQualification(long lgDocId)
        {

            ClearSpecialization();
            using (IDoctorSpecializationMasterDetailsRepository objRoleMasterRepository = new DoctorSpecializationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                gvSpecialization.DataSource = objRoleMasterRepository.GetAllDoctorSpecializationMasterDetails(lgDocId);
                gvSpecialization.DataBind();
            }
            ClearQualification();
            using (IDoctorQualificationMasterDetailsRepository objRoleMasterRepository = new DoctorQualificationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                gvQualification.DataSource = objRoleMasterRepository.GetAllDoctorQualificationMasterDetails(lgDocId);
                gvQualification.DataBind();
            }
        }
        
        protected void btnPerSpecialization_Click(object sender, EventArgs e)
        {
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.GridView);
        }

        protected void btnNextSpecialization_Click(object sender, EventArgs e)
        {
            if (hfId.Value != "0")
            {
                BindGridView();
                BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
            }
            else
            {
                Functions.MessagePopup(this, "Please Oen Doctor Edit Row link then Access Again..", PopupMessageType.warning);
                ShowHideControl(DoctorVisibityType.GridView);
            }
        }

        #region Specialization
        protected void btnSpecializationSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorSpecializationMasterDetailsRepository objRoleMasterRepository = new DoctorSpecializationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                GetAllDoctorSpecializationMasterDetailsByDocIdResult objBO = new GetAllDoctorSpecializationMasterDetailsByDocIdResult();
                if (LoadControlsAddSpecializationMaster(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctorSpecializationMasterDetails(objBO, Convert.ToInt32(hfId.Value), out errorMessage))
                    {
                        ClearSpecialization();
                        BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        private bool LoadControlsAddSpecializationMaster(GetAllDoctorSpecializationMasterDetailsByDocIdResult objBO)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfSpRecId.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfSpRecId.Value);
            }
            if (ddlSpecialization.SelectedIndex > 0)
            {
                objBO.SpecializationId = Convert.ToInt32(ddlSpecialization.SelectedValue);
            }
            else
            {

                Functions.MessagePopup(this, "Please Select Specialization", PopupMessageType.error);
                return false;
            }
            return true;
        }

        protected void btnSpecializationClear_ServerClick(object sender, EventArgs e)
        {
            ClearSpecialization();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.SpecializationQualification);
        }

        private void ClearSpecialization()
        {
            hfSpRecId.Value = "0";
            BindSpecializationDropDown();
            ddlSpecialization.SelectedIndex = 0; 
        }

        private void BindSpecializationDropDown()
        {
            using (ISpecializationRepository objRoleMasterRepository = new SpecializationRepository(Functions.strSqlConnectionString))
            {
                ddlSpecialization.DataSource = objRoleMasterRepository.GetAllSpecialization().Where(x => x.IsActive == true).ToList();
                ddlSpecialization.DataTextField= "DepartmentName";
                ddlSpecialization.DataValueField="Id";
                ddlSpecialization.DataBind();
                ddlSpecialization.Items.Insert(0, new ListItem
                {
                    Value = null,
                    Text = "Select Specialization"
                });
            }
        }

        protected void ibtn_SpecializationDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvSpecialization.DataKeys[rowindex]["Id"].ToString());
                using (IDoctorSpecializationMasterDetailsRepository objRoleMasterRepository = new DoctorSpecializationMasterDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctorSpecializationMasterDetails(rowId, out errorMessage))
                    {
                        ClearSpecialization();
                        BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }

                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void ibtn_SpecializationEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvSpecialization.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorSpecializationMasterDetailsRepository objRoleMasterRepository = new DoctorSpecializationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.SpecializationQualification);
                var data = objRoleMasterRepository.GetDoctorSpecializationMasterDetailsById(Convert.ToInt32(hfId.Value),rowId);
                if (data != null)
                {
                    hfSpRecId.Value = data.Id.ToString();
                    ddlSpecialization.SelectedValue= data.SpecializationId.ToString();
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure In Specialization", PopupMessageType.error);
                }
            }
        }
        
        #endregion

        #region Qualification
        protected void ibtn_QualificationEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvSpecialization.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorQualificationMasterDetailsRepository objRoleMasterRepository = new DoctorQualificationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.SpecializationQualification);
                var data = objRoleMasterRepository.GetDoctorQualificationMasterById(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfQualiRecId.Value = data.Id.ToString();
                    txtQualificaiton.Text = data.QualificationName.ToString();
                    txtQualificationShort.Text = data.QualificationShortName.ToString();
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure In Qualification", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_QualificationDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvQualification.DataKeys[rowindex]["Id"].ToString());
                using (IDoctorQualificationMasterDetailsRepository objRoleMasterRepository = new DoctorQualificationMasterDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctorQualificationMasterDetails(rowId, out errorMessage))
                    {
                        ClearQualification();
                        BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
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

        private void ClearQualification()
        {
            hfQualiRecId.Value = "0";
            txtQualificaiton.Text = "";
            txtQualificationShort.Text = "";
        }

        protected void btnQualificationClear_ServerClick(object sender, EventArgs e)
        {
            ClearQualification();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.SpecializationQualification);
        }

        protected void btnQualificationSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorQualificationMasterDetailsRepository objRoleMasterRepository = new DoctorQualificationMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                GetAllDoctorQualificationMasterDetailsByDocIdResult objBO = new GetAllDoctorQualificationMasterDetailsByDocIdResult();
                if (LoadControlsAddQualificationMaster(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctorQualificationMasterDetails(objBO, Convert.ToInt32(hfId.Value), out errorMessage))
                    {
                        ClearQualification();
                        BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.SpecializationQualification);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            };
        }

        private bool LoadControlsAddQualificationMaster(GetAllDoctorQualificationMasterDetailsByDocIdResult objBO)
        { //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfQualiRecId.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfQualiRecId.Value);
            }
            if (string.IsNullOrWhiteSpace(txtQualificaiton.Text))
            {
                Functions.MessagePopup(this, "Please Enter Qualificaiton", PopupMessageType.error);
                return false;
            }
            else
            {
                objBO.QualificationName = txtQualificaiton.Text;
            }
            if (string.IsNullOrWhiteSpace(txtQualificationShort.Text))
            {
                Functions.MessagePopup(this, "Please Enter Qualificaiton Short form name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBO.QualificationShortName = txtQualificationShort.Text;
            }
            return true;
        }

        #endregion

        #endregion

        #region Achievements Expertise
        private void BindAchievementsExpertise(int lgDocId)
        {
            ClearAchievements();
            using (IDoctorAchievementsMasterDetailsRepository objRoleMasterRepository = new DoctorAchievementsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                gvAchievements.DataSource = objRoleMasterRepository.GetAllDoctorAchievementsMasterDetails(lgDocId);
                gvAchievements.DataBind();
            }
            ClearExpertise();
            using (IDoctorExpertiseMasterDetailsRepository objRoleMasterRepository = new DoctorExpertiseMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                gvExpertise.DataSource = objRoleMasterRepository.GetAllDoctorExpertiseMasterDetails(lgDocId);
                gvExpertise.DataBind();
            }
        }

        protected void btnPerAchievements_Click(object sender, EventArgs e)
        {
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            BindPublications(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.SpecializationQualification);
        }


        protected void btnNextAchievements_Click(object sender, EventArgs e)
        {
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            BindPublications(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.Publications);

        }

        #region Achievements
        private void ClearAchievements()
        {
            hfAchievementsId.Value = "0";
            txtAchievements.Text = "";
        }
        protected void btnSaveAchievements_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorAchievementsMasterDetailsRepository objRoleMasterRepository = new DoctorAchievementsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                GetAllDoctorAchivementsMasterDetailsByDocIdResult objBO = new GetAllDoctorAchivementsMasterDetailsByDocIdResult();
                if (LoadControlsAddAchivementsMaster(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctorAchievementsMasterDetails(objBO, Convert.ToInt32(hfId.Value), out errorMessage))
                    {
                        ClearSpecialization();
                        BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        private bool LoadControlsAddAchivementsMaster(GetAllDoctorAchivementsMasterDetailsByDocIdResult objBO)
        { //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfAchievementsId.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfAchievementsId.Value);
            }
            if (!string.IsNullOrWhiteSpace(txtAchievements.Text))
            {
                objBO.AchievementsName =txtAchievements.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Achievement name", PopupMessageType.error);
                return false;
            }
            return true;
        }

        protected void btnClearAchievements_ServerClick(object sender, EventArgs e)
        {
            ClearAchievements();
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
        }

        protected void ibtn_AchievementsEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvAchievements.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorAchievementsMasterDetailsRepository objRoleMasterRepository = new DoctorAchievementsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                var data = objRoleMasterRepository.GetDoctorAchievementsMasterById(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfAchievementsId.Value = data.Id.ToString();
                    txtAchievements.Text = data.AchievementsName.ToString();
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure In Achievements", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_AchievementsDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvAchievements.DataKeys[rowindex]["Id"].ToString());
                using (IDoctorAchievementsMasterDetailsRepository objRoleMasterRepository = new DoctorAchievementsMasterDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctorAchievementsMasterDetails(rowId, out errorMessage))
                    {
                        ClearQualification();
                        BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
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
        #endregion

        #region Expertise
        private void ClearExpertise()
        {
            hfExpertiseId.Value = "0";
            txtExpertise.Text = "";
        }

        protected void btnSaveExpertise_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IDoctorExpertiseMasterDetailsRepository objRoleMasterRepository = new DoctorExpertiseMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                GetAllDoctorExpertiseMasterDetailsByDocIdResult objBO = new GetAllDoctorExpertiseMasterDetailsByDocIdResult();
                if (LoadControlsAddExpertiseMaster(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctorExpertiseMasterDetails(objBO, Convert.ToInt32(hfId.Value), out errorMessage))
                    {
                        ClearSpecialization();
                        BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        private bool LoadControlsAddExpertiseMaster(GetAllDoctorExpertiseMasterDetailsByDocIdResult objBO)
        { //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfExpertiseId.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfExpertiseId.Value);
            }
            if (!string.IsNullOrWhiteSpace(txtExpertise.Text))
            {
                objBO.ExpertiseName = txtExpertise.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Expertise name", PopupMessageType.error);
                return false;
            }
            return true;
        }

        protected void btnClearExpertise_ServerClick(object sender, EventArgs e)
        {
            ClearExpertise();
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
        }

        protected void ibtn_ExpertiseEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvAchievements.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorExpertiseMasterDetailsRepository objRoleMasterRepository = new DoctorExpertiseMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                BindSpecializationQualification(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
                var data = objRoleMasterRepository.GetDoctorExpertiseMasterById(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfExpertiseId.Value = data.Id.ToString();
                    txtExpertise.Text = data.ExpertiseName.ToString();
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure In Expertise", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_ExpertiseDelete_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvAchievements.DataKeys[rowindex]["Id"].ToString());
                using (IDoctorExpertiseMasterDetailsRepository objRoleMasterRepository = new DoctorExpertiseMasterDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctorExpertiseMasterDetails(rowId, out errorMessage))
                    {
                        ClearQualification();
                        BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
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
        #endregion

        #endregion
        
        #region Publications

        private void BindPublications(long lgDocId)
        {
            using (IDoctorPublicationsMasterDetailsRepository objRoleMasterRepository = new DoctorPublicationsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                gvPublications.DataSource = objRoleMasterRepository.GetAllDoctorPublicationsMasterDetails(lgDocId);
                gvPublications.DataBind();
            }
        }

        protected void btnPerPublications_Click(object sender, EventArgs e)
        {
            BindGridView();
            BindSpecializationQualification(Convert.ToInt32(hfId.Value));
            BindAchievementsExpertise(Convert.ToInt32(hfId.Value));
            BindPublications(Convert.ToInt32(hfId.Value));
            ShowHideControl(DoctorVisibityType.ExpertiseAchievements);
        }
        

        #region Publications
        protected void btnSavePublications_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            using (IDoctorPublicationsMasterDetailsRepository objRoleMasterRepository = new DoctorPublicationsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                GetAllDoctorPublicationsMasterDetailsByDocIdResult objBO = new GetAllDoctorPublicationsMasterDetailsByDocIdResult();
                if (LoadControlsAddPublicationsMaster(objBO))
                {
                    if (!objRoleMasterRepository.InsertOrUpdateDoctorPublicationsMasterDetails(objBO, Convert.ToInt32(hfId.Value), out errorMessage))
                    {
                        ClearPublications();
                        BindPublications(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.Publications);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        ShowHideControl(DoctorVisibityType.Publications);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                }
            }
        }

        private bool LoadControlsAddPublicationsMaster(GetAllDoctorPublicationsMasterDetailsByDocIdResult objBO)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfPublicationsId.Value))
            {
                objBO.Id = 0;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfPublicationsId.Value);
            }
            if (!string.IsNullOrWhiteSpace(txtPublications.Text))
            {
                objBO.Publications = txtPublications.Text.Trim();
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Publications name", PopupMessageType.error);
                return false;
            }
            return true;
        }

        protected void btnClearPublications_ServerClick(object sender, EventArgs e)
        {
            ClearPublications();
            BindPublications(Convert.ToInt32(hfId.Value));
        }

        private void ClearPublications()
        {
            hfPublicationsId.Value = "0";
            txtPublications.Text = "";
        }

        protected void ibtn_PublicationsEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvPublications.DataKeys[rowindex]["Id"].ToString());
            using (IDoctorPublicationsMasterDetailsRepository objRoleMasterRepository = new DoctorPublicationsMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                BindPublications(Convert.ToInt32(hfId.Value));
                ShowHideControl(DoctorVisibityType.Publications);
                var data = objRoleMasterRepository.GetDoctorPublicationsMasterById(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfPublicationsId.Value = data.Id.ToString();
                    txtPublications.Text = data.Publications.ToString();
                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure In Expertise", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_PublicationsDelete_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvAchievements.DataKeys[rowindex]["Id"].ToString());
                using (IDoctorPublicationsMasterDetailsRepository objRoleMasterRepository = new DoctorPublicationsMasterDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objRoleMasterRepository.RemoveDoctorPublicationsMasterDetails(rowId, out errorMessage))
                    {
                        ClearQualification();
                        BindPublications(Convert.ToInt32(hfId.Value));
                        ShowHideControl(DoctorVisibityType.Publications);
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
        #endregion

        #endregion

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                dvSwapSequance.Visible = false;
                dvDrpSwapSequance.Visible = false;
                rfvSwapSequanceNo.Enabled = false;
                rgVSwapSequanseNo.Enabled = false;
                ShowHideControl(DoctorVisibityType.Insert);
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
                txtSearch.Text = string.Empty;
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}