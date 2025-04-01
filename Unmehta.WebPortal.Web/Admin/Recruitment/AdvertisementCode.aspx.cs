using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using Unmehta.WebPortal.Common;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository.Recrutment;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class AdvertisementCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
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
                ClearFormData();
                BindGridView();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (IRecruitmentAdvertisementCodeMasterRepository objAdvertisementRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                RecruitmentAdvertisementCodeMasterModel objData = new RecruitmentAdvertisementCodeMasterModel { Id = Convert.ToInt32(hfRowId.Value), AdvertisementCode = txtPostCode.Text, StartDate = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text), EndDate = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text), IsActive = chkEnable.Checked,PublishDate=Convert.ToDateTime(txtpublishdate.Text) };
                bool isValidate = true;
                string errorMessage = "";
                string fileName = "";
                string fullfilepath = "";
                #region Validation

                if (string.IsNullOrWhiteSpace(txtPostCode.Text))
                {
                    errorMessage = "Please Enter Advertisement Code";
                    isValidate = false;
                }

                if (string.IsNullOrWhiteSpace(txtStartDate.Text))
                {
                    errorMessage = "Please Enter Start Date";
                    isValidate = false;
                }
                if (string.IsNullOrWhiteSpace(txtStartTime.Text))
                {
                    errorMessage = "Please Enter Start Time";
                    isValidate = false;
                }
                if (string.IsNullOrWhiteSpace(txtpublishdate.Text))
                {
                    errorMessage = "Please Enter Publish Date";
                    isValidate = false;
                }
                if(!string.IsNullOrWhiteSpace(txtPostDesc.Value))
                {
                    objData.AdvertisementDesc = HttpUtility.HtmlEncode(txtPostDesc.Value);
                }
                if (string.IsNullOrWhiteSpace(txtEndDate.Text))
                {
                    errorMessage = "Please Enter End Date";
                    isValidate = false;
                }
                if (string.IsNullOrWhiteSpace(txtEndTime.Text))
                {
                    errorMessage = "Please Enter End Time";
                    isValidate = false;
                }
                if (fuAboutAdvertisement.HasFile)
                {
                    string extn = System.IO.Path.GetExtension(fuAboutAdvertisement.FileName);
                    if(extn.ToLower()!=".pdf"&& extn.ToLower() != ".doc" && extn.ToLower() != ".docx")
                    {
                        errorMessage = "Please select .pdf,.doc,.docx";
                        isValidate = false;
                    }
                    string filePath = ConfigDetailsValue.AddRecrutmentFileUploadPath;

                    if (!filePath.Contains("|"))
                    {

                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuAboutAdvertisement.FileName);

                        bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = "/" + filePath + fileName;

                        // Create a temporary file name to use for checking duplicates.
                        //var tempfileName1 = "";

                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuAboutAdvertisement.SaveAs(Server.MapPath(filePath) + fileName);
                        fullfilepath = filePath + fileName;
                    }
                    else
                    {
                        errorMessage = filePath.Split('|')[0];
                        isValidate = false;
                    }
                }
                else
                {
                    fullfilepath = hfFilName.Value;
                }
                #endregion

                if (isValidate)
                {

                    if (rbtnYES.Checked == true)
                        objData.IsNewIcon = true;
                    else
                        objData.IsNewIcon = false;

                    objData.Generalinstructionfile = fullfilepath;
                    if (!objAdvertisementRepository.InsertOrUpdateRecruitmentAdvertisementCodeMasterDetails(objData, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        ClearFormData();

                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
                BindGridView();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            hfRowId.Value = "0";
            txtPostCode.Text = "";
            txtStartDate.Text = "";
            txtStartTime.Text = "";
            txtEndDate.Text = "";
            txtpublishdate.Text = "";
            txtEndTime.Text = "";
            txtPostDesc.Value = "";
            rbtnYES.Checked = true;
            rbtnNO.Checked = false;

            chkEnable.Checked = false;
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
                return null;

            // *** Absolute path - just return
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;

            // *** Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                string newUrl = "";
                if (HttpContext.Current != null)
                    newUrl = HttpContext.Current.Request.ApplicationPath +
                          originalUrl.Substring(1).Replace("//", "/");
                else
                    // *** Not context: assume current directory is the base directory
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");

                // *** Just to be sure fix up any double slashes
                return newUrl.Replace("//", "/");
            }

            return originalUrl;
        }

        private void BindGridView(string strSearch = "")
        {
            using (IRecruitmentAdvertisementCodeMasterRepository objAdvertisementRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                string filePath = ConfigDetailsValue.AddRecrutmentFileUploadPath, errorMessage = "";

                if (!filePath.Contains("|"))
                {
                    var gvData = objAdvertisementRepository.GetAllRecruitmentAdvertisementCodeMaster();

                    if (gvData != null)
                    {
                        grdUser.DataSource = gvData;
                        grdUser.DataBind();
                    }
                }
                

            }
        }


        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            using (IRecruitmentAdvertisementCodeMasterRepository objAdvertisementRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objAdvertisementRepository.GetAllRecruitmentAdvertisementCodeMaster().Where(x => x.Id == Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString())).FirstOrDefault();

                if(data!=null)
                {
                    hfRowId.Value = data.Id.ToString();
                    txtPostCode.Text = data.AdvertisementCode;
                    txtStartDate.Text = Convert.ToDateTime(data.StartDate).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    txtStartTime.Text = Convert.ToDateTime(data.StartDate).ToString("HH:mm", CultureInfo.InvariantCulture);
                    txtEndDate.Text = Convert.ToDateTime(data.EndDate).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    txtpublishdate.Text = Convert.ToDateTime(data.PublishDate).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    txtEndTime.Text = Convert.ToDateTime(data.EndDate).ToString("HH:mm", CultureInfo.InvariantCulture);
                    txtPostDesc.Value  = HttpUtility.HtmlDecode(data.AdvertisementDesc);
                    chkEnable.Checked = data.IsActive.Value;
                    hfFilName.Value = data.Generalinstructionfile;
                    if (data.IsNewIcon.HasValue)
                    {
                        if (data.IsNewIcon.Value == true)
                        {
                            rbtnNO.Checked = false;
                            rbtnYES.Checked = true;
                        }
                        else
                        {
                            rbtnNO.Checked = true;
                            rbtnYES.Checked = false;
                        }
                    }
                    else
                    {
                        rbtnNO.Checked = true;
                        rbtnYES.Checked = false;
                    }
                }
                else
                {
                    ClearFormData();
                }


            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            using (IRecruitmentAdvertisementCodeMasterRepository objAdvertisementRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objAdvertisementRepository.RemoveTblRecruitmentRecruitmentAdvertisementCodeMaster(Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString()), out errorMessage))
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    //Response.Redirect("/Admin/Recruitment/Advertisement.aspx");
                    BindGridView();
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    //Response.Redirect("/Admin/Recruitment/Advertisement.aspx");
                    BindGridView();
                }

            }
        }
    }
}