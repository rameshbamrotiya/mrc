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
    public partial class Advertisement : System.Web.UI.Page
    {
        #region Page Method
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
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (IRecruitmentAdvertisementMasterDetailsRepository objRecruitmentAdvertisementRepository = new RecruitmentAdvertisementMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                using (IRecruitmentEducationRepository objRecruitmentEducationRepository = new RecruitmentEducationRepository(Functions.strSqlConnectionString))
                {
                    using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
                    {
                        bool isValidate = true;
                        bool iswalikn = false;
                        if (ddlRecruitmentType.SelectedIndex > 0)
                        {
                            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                            {
                                var data = objEducationQualificationRepository.GetAllRecruitmentTypeMasterDetails().Where(x => x.Id == Convert.ToInt32(ddlRecruitmentType.SelectedValue)).FirstOrDefault();
                                if (data != null)
                                {
                                    iswalikn = data.isWalkin;
                                }
                            }
                        }
                        #region Validation
                        string errorMessage = "Saved Successfully.";
                        string errorSubMessage = "Saved Successfully.";

                        string fileName = "";
                        int minExp = 0;
                        int maxExp = 0;

                        int checkBoxCheckCount = 0;
                        if (ddlAdvertisementCode.SelectedValue == "Select")
                        {
                            errorMessage = "please Select Advertisement Code.";
                            isValidate = false;
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                            return;
                        }
                        if (ddlAdvertisementType.SelectedValue == "Select")
                        {
                            errorMessage = "please Select Departments.";
                            isValidate = false;
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                            return;
                        }
                        foreach (DataListItem item in dlCheckList.Items)
                        {
                            if ((item.FindControl("chkRow") as CheckBox).Checked)
                            {
                                checkBoxCheckCount++;
                            }
                        }
                        DateTime dtPublish;
                        if (!DateTime.TryParse(txtpublishdate.Text, out dtPublish))
                        {
                            errorMessage = "please enter Publish Date.";
                            isValidate = false;
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(txtInterviewTime.Text))
                        {
                            string format = "hh:mm tt";
                            DateTime dateTime;
                            if (!DateTime.TryParseExact(txtInterviewTime.Text, format, CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out dateTime))
                            {
                                errorMessage = "please enter valid Interview Time. Formate Need"+ format;
                                isValidate = false;
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(txtReportingtime.Text))
                        {
                            string format = "hh:mm tt";
                            DateTime dateTime;
                            if (!DateTime.TryParseExact(txtReportingtime.Text, format, CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out dateTime))
                            {
                                errorMessage = "please enter valid Reporting Time. " + format;
                                isValidate = false;
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                        }

                        if (!iswalikn)
                        {
                            if (checkBoxCheckCount <= 0)
                            {
                                //errorMessage = "please Select at least one Education from checkbox list";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                            if (!Int32.TryParse(txtMinExp.Text, out minExp))
                            {
                                //errorMessage = "please enter valid Min Total Experience";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                            if (!Int32.TryParse(txtmaxexp.Text, out maxExp))
                            {
                                //errorMessage = "please enter valid Max Total Experience";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                        }
                        int noopening = 0;
                        if (!Int32.TryParse(txtnoopning.Text, out noopening))
                        {
                            //errorMessage = "please enter valid Noopening Total Experience";
                            //isValidate = false;
                        }
                        string strgender = string.Empty;
                        foreach (ListItem li in ChkGEnder.Items)
                        {
                            if (li.Selected == true)
                            {
                                strgender = strgender + li.Value.ToString() + ",";
                            }
                        }
                        if (strgender != string.Empty)
                        {
                            strgender = strgender.Remove(strgender.Length - 1);
                        }


                        RecruitmentAdvertisementGridModel objData = new Model.Model.RecruitmentAdvertisementGridModel
                        {
                            Id = Convert.ToInt32(hfRowId.Value),
                            AdvertisementName = txtPostName.Text,
                            //MaxAge = Convert.ToInt32(txtAgeTo.Text),
                            //StartDate = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text),
                            //EndDate = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text),
                            IsActive = chkEnable.Checked,
                            AdvertisementCode = ddlAdvertisementCode.SelectedValue,
                            AdvertisementDesc = HttpUtility.HtmlEncode(txtPostDesc.Value),
                            PostCode = txtPostCode.Text,
                            PostType = ddlPostType.SelectedValue,
                            RecrutmentType = ddlRecruitmentType.Text,
                            AdvertisementType = ddlAdvertisementType.SelectedValue,
                            MinExp = minExp,
                            MaxExp = maxExp,
                            Noopening = noopening,
                            PublishDate = dtPublish,
                            //Designation = Convert.ToInt32(ddldesignation.SelectedValue),
                            Gender = strgender
                        };

                        if (rbtnYES.Checked == true)
                            objData.IsNewIcon = 1;
                        else
                            objData.IsNewIcon = 0;
                        int MaxAge = 0;
                        if (Int32.TryParse(txtAgeTo.Text, out MaxAge))
                        {
                            objData.MaxAge = MaxAge;
                        }

                        if (ddldesignation.SelectedIndex>0)
                        {
                            objData.Designation = Convert.ToInt32(ddldesignation.SelectedValue);
                        }
                        DateTime? startDate;
                        DateTime dts;
                        if (!DateTime.TryParse((txtStartDate.Text + " " + txtStartTime.Text), out (dts)))
                        {
                            if (!iswalikn)
                            {
                                //errorMessage = "please enter Interview date";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                            else
                            {
                                startDate = null;
                            }
                        }
                        else
                        {
                            startDate = dts;
                            objData.StartDate = startDate;
                        }

                        DateTime? endDate;
                        DateTime dtes;
                        if (!DateTime.TryParse((txtStartDate.Text + " " + txtStartTime.Text), out (dtes)))
                        {
                            if (!iswalikn)
                            {
                                //errorMessage = "please enter Interview date";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                            else
                            {
                                endDate = null;
                            }
                        }
                        else
                        {
                            endDate = dtes;
                            objData.EndDate = endDate;
                        }



                        DateTime? AgeLimitCalOn;
                        DateTime dtas;
                        if (!DateTime.TryParse(txtAgeLimitCalOn.Text, out (dtas)))
                        {
                            if (!iswalikn)
                            {
                                //errorMessage = "please enter valid Age Limit Cal date";
                                //isValidate = false;
                                //Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                //return;
                            }
                        }
                        else
                        {
                            AgeLimitCalOn = dtas;
                            objData.AgeLimitCalOn = AgeLimitCalOn;
                        }


                        if (fuAboutAdvertisement.HasFile)
                        {
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
                            }
                            else
                            {
                                errorMessage = filePath.Split('|')[0];
                                isValidate = false;
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                        }
                        else
                        {
                            fileName = hfInnerImage.Value;
                        }

                        DateTime? intervisewdate;
                        DateTime dtssadf;
                        if (!DateTime.TryParse(txtInterviewdate.Text, out (dtssadf)))
                        {
                            if (iswalikn)
                            {
                                errorMessage = "please enter Interview date";
                                isValidate = false;
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                        }
                        else
                        {
                            intervisewdate = dtssadf;
                            objData.InterviewDate = intervisewdate;
                        }
                        if (string.IsNullOrEmpty(txtInterviewTime.Text))
                        {
                            if (iswalikn)
                            {
                                errorMessage = "please enter Interview time";
                                isValidate = false;
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                return;
                            }
                        }
                        else
                        {
                            objData.InterviewTime = txtInterviewTime.Text;
                        }
                        if (string.IsNullOrEmpty(txtReportingtime.Text))
                        {
                            //if (iswalikn)
                            //{
                            //    errorMessage = "please enter Reporting time";
                            //    isValidate = false;
                            //    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                            //    return;
                            //}
                        }
                        else
                        {
                            objData.ReportingTime = txtReportingtime.Text;
                        }

                        #endregion
                        if (ddlAdvertisementType.SelectedIndex > 0)
                        {
                            objData.AdvertisementType = ddlAdvertisementType.SelectedValue;
                        }
                        if (isValidate)
                        {
                            objData.FileName = fileName;
                            if (!objAdvertisementRepository.InsertOrUpdateTblRecruitmentAdvertisement(objData, out errorMessage))
                            {

                                if (objRecruitmentAdvertisementRepository.GetAllRecruitmentAdvertisementMasterDetailsByAddId(Convert.ToInt32(objData.Id)).Count > 0)
                                {
                                    objRecruitmentAdvertisementRepository.RemoveRecruitmentAdvertisementMasterDetails(Convert.ToInt32(objData.Id), out errorSubMessage);
                                }
                                foreach (DataListItem item in dlCheckList.Items)
                                {
                                    string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                                    if ((item.FindControl("chkRow") as CheckBox).Checked)
                                    {

                                        var data = objRecruitmentEducationRepository.GetAllTblRecruitmentEducation().Where(x => x.EducationDetailName == value);
                                        {
                                            if (data != null)
                                            {
                                                if (objRecruitmentAdvertisementRepository.InsertRecruitmentAdvertisementMasterDetails(new RecruitmentAdvertisementMasterDetailsGridModel
                                                {
                                                    AdvertisementId = Convert.ToInt32(objData.Id),
                                                    QualificationId = data.FirstOrDefault().Id
                                                }, out errorSubMessage))
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    //objAdvertisementRepository.
                                }
                                using (IRecruitmentSourceTypeMasterRepository objEducationQualificationRepository = new RecruitmentSourceTypeMasterRepository(Functions.strSqlConnectionString))
                                {
                                    if (objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(Convert.ToInt32(objData.Id)).Count > 0)
                                    {
                                        objEducationQualificationRepository.RemoveRecruitmentAdvertisementSourceMasterDetails(Convert.ToInt32(objData.Id), out errorSubMessage);
                                    }
                                    foreach (DataListItem item in dlAdvertisementSource.Items)
                                    {
                                        if ((item.FindControl("chkRow") as CheckBox).Checked)
                                        {
                                            string value = (item.FindControl("LblEducationDetailName") as Label).Text;

                                            var data = objEducationQualificationRepository.GetAllEducationTypeMaster();
                                            {
                                                if (data != null)
                                                {
                                                    var subDetails = data.Where(x => x.AdvertisementName == value).FirstOrDefault();
                                                    if (objEducationQualificationRepository.InsertRecruitmentAdvertisementMasterDetails(Convert.ToInt32(objData.Id), subDetails.Id, out errorSubMessage))
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //objAdvertisementRepository.
                                    }
                                }
                                using (IRecruitmentEducationTypeMasterRepository objEducationQualificationRepository = new RecruitmentEducationTypeMasterRepository(Functions.strSqlConnectionString))
                                {
                                    if (objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(Convert.ToInt32(objData.Id)).Count > 0)
                                    {
                                        objEducationQualificationRepository.RemoveRecruitmentAdvertisementEducationTypeMasterDetails(Convert.ToInt32(objData.Id), out errorSubMessage);
                                    }
                                    foreach (DataListItem item in dlChkEducationType.Items)
                                    {
                                        if ((item.FindControl("chkRow") as CheckBox).Checked)
                                        {
                                            string value = (item.FindControl("LblEducationDetailName") as Label).Text;

                                            var data = objEducationQualificationRepository.GetAllEducationTypeMaster();
                                            {
                                                if (data != null)
                                                {
                                                    var subDetails = data.Where(x => x.TypeName == value).FirstOrDefault();
                                                    if (objEducationQualificationRepository.InsertRecruitmentAdvertisementMasterDetails(Convert.ToInt32(objData.Id), subDetails.Id, out errorSubMessage))
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //objAdvertisementRepository.
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(errorSubMessage)|| errorSubMessage.Contains("Success"))
                                {
                                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                                    ClearFormData();
                                }
                                else
                                {
                                    Functions.MessagePopup(this, errorSubMessage, PopupMessageType.error);
                                }
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
                    }
                }
            }
        }

        protected void ddlAdvertisementCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdvertisementCode.SelectedIndex > 0)
            {
                using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                {
                    var data = objEducationQualificationRepository.GetRecruitmentAdvertisementCodeMasterDetailsById(Convert.ToInt32(ddlAdvertisementCode.SelectedValue));
                    if (data != null)
                    {
                        //txtStartDate.Text = ((DateTime)data.StartDate).ToString("dd/M/yyyy").Replace("-", "/");
                        //txtStartTime.Text = ((DateTime)data.StartDate).ToString("HH:mm");
                        //txtEndDate.Text = ((DateTime)data.EndDate).ToString("dd/M/yyyy").Replace("-", "/");
                        //txtEndTime.Text = ((DateTime)data.StartDate).ToString("HH:mm");
                        txtpublishdate.Text= ((DateTime)data.PublishDate).ToString("dd/MM/yyyy").Replace("-", "/");
                        txtPostDesc.Value = HttpUtility.HtmlDecode(data.AdvertisementDesc);
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
                }
            }
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            ClearFormData();
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfRowId.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            long id = Convert.ToInt32(hfRowId.Value);
            using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
            {
                var gvData = objAdvertisementRepository.GetTblRecruitmentAdvertisementById(id);
                if (gvData != null)
                {
                    txtPostName.Text = gvData.AdvertisementName;
                    txtPostDesc.Value = HttpUtility.HtmlDecode(gvData.AdvertisementDesc);
                    txtPostCode.Text = gvData.PostCode;
                    ddlAdvertisementCode.SelectedValue = gvData.AdvertisementCode;
                    ddlPostType.SelectedValue = gvData.PostType;
                    ddlRecruitmentType.SelectedValue = gvData.RecrutmentType;
                    ddldesignation.SelectedValue =(gvData.Designation.HasValue? Convert.ToInt32(gvData.Designation).ToString():null);
                    txtnoopning.Text = gvData.Noopening.ToString();
                    txtmaxexp.Text = gvData.MaxExp.ToString();
                    ddlAdvertisementType.SelectedValue = gvData.AdvertisementType;
                    //if (gvData.IsNewIcon.HasValue)
                    //{
                    //    if (gvData.IsNewIcon.Value == 1)
                    //    {
                    //        rbtnNO.Checked = false;
                    //        rbtnYES.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        rbtnNO.Checked = true;
                    //        rbtnYES.Checked = false;
                    //    }
                    //}
                    //else
                    //{
                    //    rbtnNO.Checked = true;
                    //    rbtnYES.Checked = false;
                    //}
                    if (!string.IsNullOrWhiteSpace(gvData.FileName))
                    {
                        hfInnerImage.Value = gvData.FileName;
                        lblInnerImage.Text = gvData.FileName;
                        aRemoveInner.Visible = true;
                    }

                    txtStartDate.Text = (gvData.StartDate.HasValue? gvData.StartDate.Value.ToString("dd/M/yyyy").Replace("-", "/") : "") ;
                    txtStartTime.Text = (gvData.StartDate.HasValue ? gvData.StartDate.Value.ToString("HH:mm") : "");
                    txtEndDate.Text = (gvData.EndDate.HasValue ? gvData.EndDate.Value.ToString("dd/M/yyyy").Replace("-", "/") : "");
                    txtEndTime.Text = (gvData.EndDate.HasValue ? gvData.EndDate.Value.ToString("HH:mm") : "");
                    chkEnable.Checked = (bool)gvData.IsActive;
                    txtAgeTo.Text = gvData.MaxAge.ToString();
                    bool iswalikn = false;
                    if (ddlRecruitmentType.SelectedIndex > 0)
                    {
                        using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                        {
                            var data = objEducationQualificationRepository.GetAllRecruitmentTypeMasterDetails().Where(x => x.Id == Convert.ToInt32(ddlRecruitmentType.SelectedValue)).FirstOrDefault();
                            if (data != null)
                            {
                                iswalikn = data.isWalkin;
                                if (iswalikn == true)
                                {
                                    diviswalkin.Visible = true;
                                }
                                else
                                {
                                    diviswalkin.Visible = false;
                                }
                            }
                        }
                    }
                    txtInterviewdate.Text = (gvData.InterviewDate.HasValue ? gvData.InterviewDate.Value.ToString("dd/M/yyyy").Replace("-", "/") : "");
                    if (string.IsNullOrEmpty(gvData.InterviewTime.ToString()))
                    {
                        txtInterviewTime.Text = "";
                    }
                    else
                    {
                        txtInterviewTime.Text = gvData.InterviewTime.ToString();
                    }
                    if (string.IsNullOrEmpty(gvData.ReportingTime.ToString()))
                    {
                        txtReportingtime.Text = "";
                    }
                    else
                    {
                        txtReportingtime.Text = gvData.ReportingTime.ToString();
                    }

                    string gender = gvData.Gender;
                    string[] values = gender.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        foreach (ListItem li in ChkGEnder.Items)
                        {
                            if (li.Value.ToString() == values[i])
                            {
                                li.Selected = true;
                            }
                        }
                    }
                    txtAgeLimitCalOn.Text = gvData.AgeLimitCalOn == null ? "" : (gvData.AgeLimitCalOn.HasValue? gvData.AgeLimitCalOn.Value.ToString("dd/M/yyyy").Replace("-", "/"):"");
                    txtpublishdate.Text = gvData.PublishDate == null ? "" : (gvData.PublishDate.HasValue ? gvData.PublishDate.Value.ToString("dd/M/yyyy").Replace("-", "/") : "");
                    txtMinExp.Text = gvData.MinExp.ToString();
                    using (IRecruitmentAdvertisementMasterDetailsRepository objAdvertisementRepositorys = new RecruitmentAdvertisementMasterDetailsRepository(Functions.strSqlConnectionString))
                    {
                        List<RecruitmentAdvertisementDetailGridModel> dataRelated = objAdvertisementRepositorys.GetAllRecruitmentAdvertisementMasterDetailsByAddIdWithName(id).ToList();
                        foreach (DataListItem item in dlCheckList.Items)
                        {
                            string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                            foreach (var row in dataRelated)
                            {
                                if (row.EducationName == value)
                                {
                                    (item.FindControl("chkRow") as CheckBox).Checked = true;
                                    break;
                                }
                                //objAdvertisementRepository.
                            }
                        }
                    }
                    using (IRecruitmentEducationTypeMasterRepository objEducationQualificationRepository = new RecruitmentEducationTypeMasterRepository(Functions.strSqlConnectionString))
                    {
                        List<RecruitmentAdvertisementEducationModel> dataRelated = objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(id).ToList();
                        foreach (DataListItem item in dlChkEducationType.Items)
                        {
                            string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                            foreach (var row in dataRelated)
                            {
                                if (row.EducationTypeName == value)
                                {
                                    (item.FindControl("chkRow") as CheckBox).Checked = true;
                                    break;
                                }
                                //objAdvertisementRepository.
                            }
                        }
                    }
                    using (IRecruitmentSourceTypeMasterRepository objEducationQualificationRepository = new RecruitmentSourceTypeMasterRepository(Functions.strSqlConnectionString))
                    {
                        List<RecruitmentAdvertisementEducationModel> dataRelated = objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(id).ToList();
                        foreach (DataListItem item in dlAdvertisementSource.Items)
                        {
                            string value = (item.FindControl("LblEducationDetailName") as Label).Text;
                            foreach (var row in dataRelated)
                            {
                                if (row.EducationTypeName == value)
                                {
                                    (item.FindControl("chkRow") as CheckBox).Checked = true;
                                    break;
                                }
                                //objAdvertisementRepository.
                            }
                        }
                    }
                    using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                    {
                        var data = objEducationQualificationRepository.GetRecruitmentAdvertisementCodeMasterDetailsById(Convert.ToInt32(ddlAdvertisementCode.SelectedValue));
                        if (data != null)
                        {
                            //txtStartDate.Text = ((DateTime)data.StartDate).ToString("dd/M/yyyy").Replace("-", "/");
                            //txtStartTime.Text = ((DateTime)data.StartDate).ToString("HH:mm");
                            //txtEndDate.Text = ((DateTime)data.EndDate).ToString("dd/M/yyyy").Replace("-", "/");
                            //txtEndTime.Text = ((DateTime)data.StartDate).ToString("HH:mm");

                            //txtpublishdate.Text = ((DateTime)data.PublishDate).ToString("dd/MM/yyyy").Replace("-", "/");
                            //txtPostDesc.Value = HttpUtility.HtmlDecode(data.AdvertisementDesc);
                            //below code Remmove by pratik
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
                    }
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objAdvertisementRepository.RemoveTblRecruitmentAdvertisement(Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString()), out errorMessage))
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
        #endregion

        #region API of Pages

        [WebMethod]
        public static string GetAllSubDetailById(int id)
        {
            using (IRecruitmentAdvertisementMasterDetailsRepository objAdvertisementRepository = new RecruitmentAdvertisementMasterDetailsRepository(Functions.strSqlConnectionString))
            {
                List<RecruitmentAdvertisementDetailGridModel> dataRelated = objAdvertisementRepository.GetAllRecruitmentAdvertisementMasterDetailsByAddIdWithName(id).ToList();

                return string.Join("|", new List<string>(dataRelated.Select(x => x.EducationName.Replace(" ", "_")).ToList()).ToArray());
            }
        }


        [WebMethod]
        public static string GetAllEducationSubDetailById(int id)
        {
            using (IRecruitmentEducationTypeMasterRepository objEducationQualificationRepository = new RecruitmentEducationTypeMasterRepository(Functions.strSqlConnectionString))
            {
                List<RecruitmentAdvertisementEducationModel> dataRelated = objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(id).ToList();

                return string.Join("|", new List<string>(dataRelated.Select(x => x.EducationTypeName.Replace(" ", "_")).ToList()).ToArray());
            }
        }


        [WebMethod]
        public static string GetAllSourceSubDetailById(int id)
        {
            using (IRecruitmentSourceTypeMasterRepository objEducationQualificationRepository = new RecruitmentSourceTypeMasterRepository(Functions.strSqlConnectionString))
            {
                List<RecruitmentAdvertisementEducationModel> dataRelated = objEducationQualificationRepository.GetAllRecruitmentAdvertisementByAddId(id).ToList();
                return string.Join("|", new List<string>(dataRelated.Select(x => x.EducationTypeName.Replace(" ", "_")).ToList()).ToArray());
            }
        }

        #endregion

        #region Page Method
        private void ClearFormData()
        {
            BindGridView();

            hfInnerImage.Value = "";
            lblInnerImage.Text = "";
            aRemoveInner.Visible = false;

            txtInterviewdate.Text = "";
            txtInterviewTime.Text = "";
            txtReportingtime.Text = "";

            hfRowId.Value = "0";
            txtPostName.Text = "";
            txtPostDesc.Value = "";
            txtAgeLimitCalOn.Text = "";
            txtpublishdate.Text = "";
            txtPostCode.Text = "";
            ddlAdvertisementCode.SelectedIndex = 0;
            ddlPostType.SelectedIndex = 0;
            ddlAdvertisementType.SelectedIndex = 0;
            ddlRecruitmentType.SelectedIndex = 0;
            ddldesignation.SelectedIndex = 0;
            txtnoopning.Text = "";
            txtmaxexp.Text = "";
            txtMinExp.Text = "";
            ChkGEnder.SelectedIndex = -1;
            txtStartDate.Text = "";
            txtStartTime.Text = "";
            txtEndDate.Text = "";
            txtEndTime.Text = "";
            chkEnable.Checked = false;
            txtAgeTo.Text = "";
        }
        private void BindGridView(string strSearch = "")
        {
            using (IRecruitmentAdvertisementRepository objAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
            {
                string filePath = ConfigDetailsValue.AddRecrutmentFileUploadPath, errorMessage = "";

                if (!filePath.Contains("|"))
                {
                    var gvData = objAdvertisementRepository.GetAllTblRecruitmentAdvertisement();
                    gvData.ForEach(x => { x.FileName = filePath + x.FileName; });
                    if (gvData != null)
                    {
                        grdUser.DataSource = gvData;
                        grdUser.DataBind();
                    }
                }

                var data = objAdvertisementRepository.GetAllAdvertisementTypeMasterDetails();
                if (data != null)
                {
                    ddlAdvertisementType.DataSource = data;
                    ddlAdvertisementType.DataValueField = "Id";
                    ddlAdvertisementType.DataTextField = "AdvertisementType";
                    ddlAdvertisementType.DataBind();
                }
                ddlAdvertisementType.Items.Insert(0, "Select");

            }
        }
        private void BindGridView()
        {
            //ddlMinEducationRequired.Items.Clear();
            //Array colors = Enum.GetValues(typeof(EducationType));
            //ddlMinEducationRequired.Items.Add(new ListItem("Select Minimum Education Type", null));
            //foreach (EducationType color in colors)
            //{
            //    ddlMinEducationRequired.Items.Add(new ListItem((StringEnum.GetStringValue(color)), ((int)color).ToString()));
            //}
            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlAdvertisementCode.DataSource = objEducationQualificationRepository.GetAllRecruitmentAdvertisementCodeMaster();
                ddlAdvertisementCode.DataTextField = "AdvertisementCode";
                ddlAdvertisementCode.DataValueField = "Id";
                ddlAdvertisementCode.DataBind();
                ddlAdvertisementCode.Items.Insert(0, "Select");
            }

            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlPostType.DataSource = objEducationQualificationRepository.GetAllPostTypeMasterDetails();
                ddlPostType.DataTextField = "PostName";
                ddlPostType.DataValueField = "Id";
                ddlPostType.DataBind();
                ddlPostType.Items.Insert(0, "Select");
            }
            ddldesignation.Items.Clear();
            using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            {
                ddldesignation.DataSource = objDesignationRepository.GetAllTblDesignationLang(1);
                ddldesignation.DataValueField = "Id";
                ddldesignation.DataTextField = "DesignationName";
                ddldesignation.DataBind();
                ddldesignation.Items.Insert(0, new ListItem("Select", "-1"));
            }

            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlRecruitmentType.DataSource = objEducationQualificationRepository.GetAllRecruitmentTypeMasterDetails();
                ddlRecruitmentType.DataTextField = "RecruitmentName";
                ddlRecruitmentType.DataValueField = "Id";
                ddlRecruitmentType.DataBind();
                ddlRecruitmentType.Items.Insert(0, "Select");
            }

            using (IRecruitmentSourceTypeMasterRepository objEducationQualificationRepository = new RecruitmentSourceTypeMasterRepository(Functions.strSqlConnectionString))
            {
                dlAdvertisementSource.DataSource = objEducationQualificationRepository.GetAllEducationTypeMaster();
                dlAdvertisementSource.DataBind();
            }
            using (IRecruitmentEducationTypeMasterRepository objEducationQualificationRepository = new RecruitmentEducationTypeMasterRepository(Functions.strSqlConnectionString))
            {
                dlChkEducationType.DataSource = objEducationQualificationRepository.GetAllEducationTypeMaster();
                dlChkEducationType.DataBind();
            }
            using (IRecruitmentEducationRepository objEducationQualificationRepository = new RecruitmentEducationRepository(Functions.strSqlConnectionString))
            {
                dlCheckList.DataSource = objEducationQualificationRepository.GetAllTblRecruitmentEducation();
                dlCheckList.DataBind();
            }
            BindGridView("");

        }
        #endregion

        protected void ddlRecruitmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool iswalikn = false;
            if (ddlRecruitmentType.SelectedIndex > 0)
            {
                using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
                {
                    var data = objEducationQualificationRepository.GetAllRecruitmentTypeMasterDetails().Where(x => x.Id == Convert.ToInt32(ddlRecruitmentType.SelectedValue)).FirstOrDefault();
                    if (data != null)
                    {
                        iswalikn = data.isWalkin;
                        if (iswalikn == true)
                        {
                            diviswalkin.Visible = true;
                        }
                        else
                        {
                            diviswalkin.Visible = false;
                        }
                    }
                }
            }
        }
    }
}