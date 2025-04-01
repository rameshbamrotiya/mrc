using BO;
using BAL;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;

namespace Unmehta.WebPortal.Web
{
    public partial class OnlineEventRegistrtion : System.Web.UI.Page
    {
        public static string strHeaderImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                BindForm();
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                long id = 0;


                if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
                {
                    int languageId = Functions.LanguageId;
                    StringBuilder strEvent = new StringBuilder();
                    DataSet ds = new EventMasterBAL().SelectEventFront("", 0, (int)id, languageId);
                    if (ds != null)
                    {
                        if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                DateTime dtStr;
                                if (DateTime.TryParse(row["EventEndDate"].ToString(),out dtStr))
                                {
                                    if(DateTime.Now.Date> dtStr.Date)
                                    {
                                        string strURL = ResolveUrl("~/EventDetails?" + id.ToString());
                                        Response.Redirect(strURL);
                                    }
                                }
                            }
                        }
                    }
                }
                lblMessage.Visible = false;
                lblMessage.Text = "";

                if (rblPhysicalDisability.SelectedValue == "No")
                {
                    divPhysicalDisability.Visible = false;
                    divExplainTypeofDisability.Visible = false;
                }
                else
                {
                    divPhysicalDisability.Visible = true;
                    divExplainTypeofDisability.Visible = true;
                }
            }
        }

        private void BindForm()
        {
            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            long id = 0;

            
            if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
            {

                DataSet ds = new EventMasterBAL().SelectEventFront("", 1,(int) id, 1);
                if (ds != null)
                {
                    if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        if(row!=null)
                        {
                            lblEventName.Text = row["EventName"].ToString(); 
                            lblEventStartDate.Text = row["EventStartDate"].ToString();
                            lblEventStartTime.Text= row["StartTimeHH"].ToString() + ":" + row["StartTimeMM"].ToString() + " " + row["StartTimeTT"].ToString();
                            lblLocation.Text = row["Location"].ToString(); 
                        }
                    }
                }

                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("body");

                foreach (var control in cph.Controls)
                {
                    if (control is HtmlGenericControl)
                    {
                        ((HtmlGenericControl)control).Visible = false;
                    }
                }

                if (id > 0)
                {
                    using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
                    {
                        var eventCheckList = objIEventFormFieldRepository.GetAlllongAboutUsMaster(id);
                        if (eventCheckList.Count() > 0)
                        {
                            var checkBoxList = eventCheckList.Select(x => x.ColumnName).ToList();
                            foreach (var chkBox in checkBoxList)
                            {
                                HtmlGenericControl chkBoxs = (HtmlGenericControl)cph.FindControl("div" + chkBox);
                                if (chkBoxs != null)
                                {
                                    chkBoxs.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Event", false);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Event",false);
                }
            }
            else
            {
                Response.Redirect("~/Event", false);
            }
        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strcontactus = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OnlineEventRegistrtion").FirstOrDefault();
                if (dataMain != null)
                {
                    LableData:
                    strcontactus = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strcontactus.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "OnlineEventRegistrtion").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strcontactus.ToString();
        }

        private bool LoadControls(OnlineEventRegistrtionBO objBo)
        {
            bool isError = false;

            long id = 0;

            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));

            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("body");

            long lgVisibleCount = 0;
            foreach (var control in cph.Controls)
            {
                if (control is HtmlGenericControl)
                {
                    if(((HtmlGenericControl)control).Visible)
                    {
                        lgVisibleCount++;
                    }
                }
            }
            using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
            {
                var eventCheckList = objIEventFormFieldRepository.GetAlllongAboutUsMaster(id);
                if (eventCheckList.Count() > 0)
                {

                }
            }

            if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
            {
                objBo.EventId = (int) id;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please try After Some time!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                dvSuccess.Attributes.Remove("class");
                dvSuccess.Attributes.Add("class", "fas fa-times");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                ClearControl();
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please Enter First Name!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                dvSuccess.Attributes.Remove("class");
                dvSuccess.Attributes.Add("class", "fas fa-times");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!Functions.ValidateEmailId(txtEmail.Text))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please Enter Valid EmailId!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    dvSuccess.Attributes.Remove("class");
                    dvSuccess.Attributes.Add("class", "fas fa-times");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                    return true;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please Enter EmailId!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                dvSuccess.Attributes.Remove("class");
                dvSuccess.Attributes.Add("class", "fas fa-times");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                return true;

            }

            if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                long lgPhone = 0;
                if (!long.TryParse(txtPhoneNumber.Text,out lgPhone))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please Enter Valid phone No!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    dvSuccess.Attributes.Remove("class");
                    dvSuccess.Attributes.Add("class", "fas fa-times");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                    return true;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                long lgPhone = 0;
                if(txtMobileNumber.Text.Trim().Length!=10)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please Enter Valid Mobile No!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    dvSuccess.Attributes.Remove("class");
                    dvSuccess.Attributes.Add("class", "fas fa-times");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                    return true;
                }
                if (!long.TryParse(txtMobileNumber.Text, out lgPhone) )
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please Enter Valid Mobile No!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    dvSuccess.Attributes.Remove("class");
                    dvSuccess.Attributes.Add("class", "fas fa-times");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                    return true;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please Enter Mobile No!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                dvSuccess.Attributes.Remove("class");
                dvSuccess.Attributes.Add("class", "fas fa-times");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                return true;

            }

            objBo.FirstName = string.IsNullOrEmpty(txtFirstName.Text) ? "" : txtFirstName.Text;
            objBo.LastName = string.IsNullOrEmpty(txtLastName.Text) ? "" : txtLastName.Text;
            objBo.SurName = string.IsNullOrEmpty(txtSurName.Text) ? "" : txtSurName.Text;
            objBo.EmailId = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text;
            objBo.PhoneNumber = string.IsNullOrEmpty(txtPhoneNumber.Text) ? "" : txtPhoneNumber.Text;
            objBo.MobileNumber = string.IsNullOrEmpty(txtMobileNumber.Text) ? "" : txtMobileNumber.Text;
            objBo.Gender = ddlGender.SelectedValue;
            objBo.BirthDate = Convert.ToDateTime(string.IsNullOrEmpty(txtBirthDate.Text) ? "01/01/1900" : txtBirthDate.Text);
            objBo.PhysicalDisability = rblPhysicalDisability.SelectedValue;
            objBo.ExplainTypeofDisability = string.IsNullOrEmpty(txtExplainTypeofDisability.Text) ? "" : txtExplainTypeofDisability.Text;
            objBo.PhysicalActivity = ddlPhysicalActivity.SelectedValue;
            objBo.EmailId = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text;
            objBo.TypeOfIdentity = ddlIdentity.SelectedValue;
            objBo.IdentityNumber = string.IsNullOrEmpty(txtIdentityNumber.Text) ? "" : txtIdentityNumber.Text;
            objBo.Residential = ddlResidential.SelectedValue;            
            objBo.Country = string.IsNullOrEmpty(txtCountry.Text) ? "" : txtCountry.Text;
            objBo.State = string.IsNullOrEmpty(txtState.Text) ? "" : txtState.Text;
            objBo.City = string.IsNullOrEmpty(txtCity.Text) ? "" : txtCity.Text;
            objBo.PostalCode = string.IsNullOrEmpty(txtPostalCode.Text) ? "" : txtPostalCode.Text;
            objBo.PhoneNumber = string.IsNullOrEmpty(txtPhoneNumber.Text) ? "" : txtPhoneNumber.Text;
            objBo.EducationQualification = string.IsNullOrEmpty(txtEducationQualification.Text) ? "" : txtEducationQualification.Text;
            objBo.OrganizationName = string.IsNullOrEmpty(txtOrganizationName.Text) ? "" : txtOrganizationName.Text;
            objBo.Designation = string.IsNullOrEmpty(txtDesignation.Text) ? "" : txtDesignation.Text;
            objBo.EmployeeId = string.IsNullOrEmpty(txtEmployeeId.Text) ? "" : txtEmployeeId.Text;
            objBo.JoiningDate = Convert.ToDateTime(string.IsNullOrEmpty(txtJoiningDate.Text) ? "01/01/1900" : txtJoiningDate.Text);
            objBo.NoOfOrganizationYouWorkWith = string.IsNullOrEmpty(txtNoOfOrganization.Text) ? "" : txtNoOfOrganization.Text;
            objBo.NoOfCNEYouAttend = string.IsNullOrEmpty(txtNoOfCNE.Text) ? "" : txtNoOfCNE.Text;
            objBo.NoOfCMEYouAttend = string.IsNullOrEmpty(txtNoOfCME.Text) ? "" : txtNoOfCME.Text;
            objBo.WorkExperience = string.IsNullOrEmpty(txtWorkExperience.Text) ? "" : txtWorkExperience.Text;
            objBo.GNCNo = string.IsNullOrEmpty(txtGNCNo.Text) ? "" : txtGNCNo.Text;


            //objBo.RegistrtionNo = string.IsNullOrEmpty(txtRegistrtionNo.Text) ? "" : txtRegistrtionNo.Text;
            objBo.WorkProfession = string.IsNullOrEmpty(ddlWorkProfession.SelectedValue) ? "" : ddlWorkProfession.SelectedValue;

            var dataDetails = objBo.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(string)).Select(pi => (string)pi.GetValue(objBo)).Where(x=> x!="0" && x != "None" && x != "No" && x != null && !string.IsNullOrWhiteSpace(x)).Count();

            if (dataDetails>0)
            {
                objBo.RegistrtionNo = DateTime.Now.ToString("ffddfffMMyyyyttff")+Functions.GetRandomNumberString();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please Enter Form value!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                dvSuccess.Attributes.Remove("class");
                dvSuccess.Attributes.Add("class", "fas fa-times");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                return true;
            }
            return isError;
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                OnlineEventRegistrtionBO objBo = new OnlineEventRegistrtionBO();
                if (!LoadControls(objBo))
                {
                    if (new OnlineEventRegistrtionBAL().InsertRecord(objBo))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Event registration successfully ....!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        dvSuccess.Attributes.Remove("class");
                        dvSuccess.Attributes.Add("class", "fas fa-check");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                        ClearControl();
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Event registration Some Issue ....!";
                        dvSuccess.Attributes.Remove("class");
                        dvSuccess.Attributes.Add("class", "fas fa-times");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupDone", "ShowPopupAlertOfRegestration();", true);
                        lblMessage.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void ClearControl()
        {

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSurName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtMobileNumber.Text = "";
            ddlGender.SelectedIndex = -1;
            txtBirthDate.Text = "";
            rblPhysicalDisability.SelectedIndex = 0;
            txtExplainTypeofDisability.Text = "";
            ddlPhysicalActivity.SelectedIndex = 0;
            txtEmail.Text = "";
            ddlIdentity.SelectedIndex = 0;
            txtIdentityNumber.Text = "";
            ddlResidential.SelectedIndex =0;           
            txtCountry.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtPostalCode.Text = "";
            txtPhoneNumber.Text = "";
            txtEducationQualification.Text = "";
            txtOrganizationName.Text = "";
            txtDesignation.Text = "";
            txtEmployeeId.Text = "";
            txtJoiningDate.Text = "";
            txtNoOfOrganization.Text = "";
            txtNoOfCNE.Text = "";
            txtNoOfCME.Text = "";
            txtWorkExperience.Text = "";
            txtGNCNo.Text = "";
            //txtRegistrtionNo.Text = "";
            txtWorkExperience.Text = "";

            ddlWorkProfession.SelectedIndex = 0;

            if (rblPhysicalDisability.SelectedValue == "No")
            {
                divPhysicalDisability.Visible = false;
                divExplainTypeofDisability.Visible = false;
            }
            else
            {
                divPhysicalDisability.Visible = true;
                divExplainTypeofDisability.Visible = true;
            }
        }

        protected void rblPhysicalDisability_TextChanged(object sender, EventArgs e)
        {
            if(rblPhysicalDisability.SelectedValue== "No")
            {
                divPhysicalDisability.Visible = false;
                divExplainTypeofDisability.Visible = false;
            }
            else
            {
                divPhysicalDisability.Visible = true;
                divExplainTypeofDisability.Visible = true;
            }
        }
    }
}