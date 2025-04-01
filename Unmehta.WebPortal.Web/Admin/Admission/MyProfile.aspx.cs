using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admission
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.StudentRegistration.Username == null)
                {
                    Response.Redirect("~/Admission/");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Admission/");
            }
            if (!IsPostBack)
            {
                if (SessionWrapper.StudentRegistration != null)
                {
                    GetDropDown();
                    using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
                    {
                        List<StudentRegistrationBO> dataList = Functions.ToListof<StudentRegistrationBO>(objData.GetAllStudentRegistrationMaster());
                        var data = dataList.Where(x => x.Id == SessionWrapper.StudentRegistration.Id).FirstOrDefault();
                        if (data != null)
                        {
                            hfID.Value = SessionWrapper.UserDetails.Id.ToString();
                            txtFirstName.Text = data.FirstName;
                            txtLastName.Text = data.LastName;
                            txtEmail.Text = data.Email;
                            txtMobile.Text = data.Mobile;
                            txtUserName.Text = data.Username;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Admission/");
                }
            }
        }

        private void GetDropDown()
        {
            ddlGender.DataSource = GetAll<GenderType>();
            ddlGender.DataTextField = "Value";
            ddlGender.DataValueField = "Value";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("-Select Gender-"));
            ddlGender.SelectedIndex = 0;

            ddlMaritalStatus.DataSource = GetAll<MaritalStatusType>();
            ddlMaritalStatus.DataTextField = "Value";
            ddlMaritalStatus.DataValueField = "Value";
            ddlMaritalStatus.DataBind();
            ddlMaritalStatus.Items.Insert(0, new ListItem("-Select Marital Status-"));
            ddlMaritalStatus.SelectedIndex = 0;
        }

        private bool FillForm(StudentRegistrationBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                Functions.MessagePopup(this, "Please Enter First Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.FirstName = txtFirstName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Last Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.LastName = txtLastName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                Functions.MessagePopup(this, "Please Enter Mobile", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Mobile = txtMobile.Text;
            }
            //if (string.IsNullOrWhiteSpace(txtPassword.Text))
            //{
            //    Functions.MessagePopup(this, "Please Enter Password", PopupMessageType.error);
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            //{
            //    Functions.MessagePopup(this, "Please Enter Confirm Password", PopupMessageType.error);
            //    return false;
            //}
            //else
            //{
            //    if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
            //    {
            //        objBo.Password = Functions.Encrypt(txtConfirmPassword.Text.Trim());
            //    }
            //    else
            //    {
            //        Functions.MessagePopup(this, "Enter Passwprd and Confirm Password mismatch", PopupMessageType.error);
            //        return false;
            //    }
            //}
            objBo.Password = Functions.Encrypt(Functions.GetRandomNumberStringForPayment());
            if (string.IsNullOrWhiteSpace(txtEmail.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter EmailId", PopupMessageType.error);
                return false;
            }
            else
            {

                if (Functions.ValidateEmailId(txtEmail.Text.Trim()))
                {
                    objBo.Email = txtEmail.Text.Trim();
                }
                else
                {
                    Functions.MessageFrontPopup(this, "Please Enter Valid EmailId", PopupMessageType.error);
                    return false;
                }
            }
            string strError;
            DateTime? dt;
            if (string.IsNullOrWhiteSpace(txtBirthDate.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Select Date Of Birth", PopupMessageType.error);
                return false;
            }
            else
            {
                if (!Functions.GetDateFromString(txtBirthDate.Text.Trim(), out dt, out strError))
                {
                    objBo.DateOfBirth = (DateTime)dt;
                }
                else
                {
                    Functions.MessageFrontPopup(this, strError + " in Date Of Birth", PopupMessageType.error);
                    return false;
                }
            }
            if (ddlGender.SelectedIndex == 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Gender", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Gender = ddlGender.SelectedValue;
            }
            if (ddlMaritalStatus.SelectedIndex == 0)
            {
                Functions.MessageFrontPopup(this, "Please Select Marital Status", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.MaritalStatus = ddlMaritalStatus.SelectedValue;
            }
            using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
            {
                userName: string strUserName = (txtFirstName.Text.Trim().ToLower() + txtLastName.Text.Trim().ToLower() + Convert.ToDateTime(dt).ToString("ddMMyyyy") + Functions.GetRandomNumberString());
                var dataOfUserName = objData.GetAllStudentRegistrationMaster();
                if (dataOfUserName != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (data.Where(x => x.Username == strUserName).Count() > 0)
                        {
                            goto userName;
                        }
                        else
                        {
                            objBo.Username = strUserName;
                        }
                    }
                }
                var dataOfEmail = objData.GetAllStudentRegistrationMaster();
                if (dataOfEmail != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfEmail);
                    if (data != null)
                    {
                        if (data.Where(x => x.Email.Trim() == objBo.Email.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Email + " Already User exist with same email", PopupMessageType.error);
                            return false;
                        }
                    }
                }
                var dataOfMobile = objData.GetAllStudentRegistrationMaster();
                if (dataOfMobile != null)
                {
                    List<StudentRegistrationBO> data = Functions.ToListof<StudentRegistrationBO>(dataOfMobile);
                    if (data != null)
                    {
                        if (data.Where(x => x.Mobile.Trim() == objBo.Mobile.Trim()).Count() > 0)
                        {
                            Functions.MessageFrontPopup(this, objBo.Email + " Already User exist with same Mobile No", PopupMessageType.error);
                            return false;
                        }
                    }
                }

            }
            return true;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                StudentRegistrationBO objBO = new StudentRegistrationBO();
                using (StudentRegistrationBAL objData = new StudentRegistrationBAL())
                {
                    objBO.Id = SessionWrapper.StudentRegistration.Id;
                    if (FillForm(objBO))
                    {
                        if (!objData.UpdateUserMaster(objBO))
                        {
                            Functions.MessagePopup(this, "Profile update Successfully", PopupMessageType.success);
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
                Functions.MessagePopup(this, ex.ToString(), PopupMessageType.error);
            }
        }
    }
}