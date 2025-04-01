using System;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    [ScriptService]
    public partial class Education : System.Web.UI.Page
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
            using (IRecruitmentEducationRepository objEducationRepository = new RecruitmentEducationRepository(Functions.strSqlConnectionString))
            {
                if (txtQualificationName.Text == "")
                {
                    Functions.MessagePopup(this, "Please Enter Education Qualification Name.", PopupMessageType.warning);
                    return;
                }
                if (ddlGraduateOrPostGraduate.SelectedValue == "Select")
                {
                    Functions.MessagePopup(this, "Please Select Education Type.", PopupMessageType.warning);
                    return;
                }
                string errorMessage = "Saved Successfully.";
                if (!objEducationRepository.InsertOrUpdateTblRecruitmentEducation(new Model.Model.RecruitmentEducationGridModel { Id = Convert.ToInt32(hfRowId.Value), EducationDetailName = txtQualificationName.Text,EducationType = Convert.ToInt32((ddlGraduateOrPostGraduate.SelectedValue)) , IsVisible = chkEnable.Checked }, out errorMessage))
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

        #region API of Pages
        [WebMethod]
        public static string RemoveDetailById(int id)
        {
            using (IRecruitmentEducationRepository objEducationRepository = new RecruitmentEducationRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objEducationRepository.RemoveTblRecruitmentEducation(id, out errorMessage))
                {
                    errorMessage += "|" + PopupMessageType.success;
                }
                else
                {
                    errorMessage += "|" + PopupMessageType.error;
                }
                return errorMessage;
            }
        }

        [WebMethod]
        public static string GetGridView()
        {
            return GetGridString().ToString();
        }
        #endregion

        #region Page Method
        private void ClearFormData()
        {
            hfRowId.Value = "0";
            txtQualificationName.Text = "";
            ddlGraduateOrPostGraduate.ClearSelection();
            chkEnable.Checked = false;
            BindGridView();
        }

        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (IRecruitmentEducationRepository objEducationRepository = new RecruitmentEducationRepository(Functions.strSqlConnectionString))
            {
                var gvData = objEducationRepository.GetAllTblRecruitmentEducation();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        strgvData.Append("<table id='gvEduQuaData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>                      ");
                        strgvData.Append("    <th>ID</th>           ");
                        strgvData.Append("    <th>Name</th>         ");
                        strgvData.Append("    <th>Education Type</th>         ");
                        strgvData.Append("    <th>Enable</th>         ");
                        strgvData.Append("    <th>Action</th>       ");
                        strgvData.Append("</tr>                     ");
                        strgvData.Append("</thead>                  ");
                        strgvData.Append("<tbody>                   ");
                        foreach (var row in gvData)
                        {
                            string strLink = (SessionWrapper.UserPageDetails.CanUpdate ? "<a class=\"btn\" onClick=\"GetEditData('" + row.Id + "|" + row.EducationDetailName + "|" + row.EducationType + "|" + row.IsVisible.ToString() + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>":"")
                                            + (SessionWrapper.UserPageDetails.CanDelete ? "<a class=\"btn\" onClick=\"RemoveData('" + row.Id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>":"");
                            strgvData.Append("<tr>                      ");
                            strgvData.Append("    <td>" + row.Id + "</td>   ");
                            strgvData.Append("    <td>" + row.EducationDetailName + "</td> ");
                            strgvData.Append("    <td>" + StringEnum.GetStringValue((EducationType)row.EducationType).ToString() + "</td> ");
                            strgvData.Append("    <td>" + row.IsVisible.ToString() + "</td> ");
                            strgvData.Append("    <td>" + strLink + "</td>       ");
                            strgvData.Append("</tr>                     ");
                        }
                        strgvData.Append("</tbody> </table>");
                    }
                }
            }
            return strgvData.ToString();
        }

        private void BindGridView()
        {
            using (IRecruitmentSourceTypeMasterRepository objEducationQualificationRepository = new RecruitmentSourceTypeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlGraduateOrPostGraduate.Items.Clear();
                ddlGraduateOrPostGraduate.DataSource = objEducationQualificationRepository.GetAllEducationTypeMaster();
                ddlGraduateOrPostGraduate.DataTextField = "AdvertisementName";
                ddlGraduateOrPostGraduate.DataValueField = "Id";
                ddlGraduateOrPostGraduate.DataBind();
                ddlGraduateOrPostGraduate.Items.Insert(0, "Select");
                //Array colors = Enum.GetValues(typeof(EducationType));
                //foreach (EducationType color in colors)
                //{
                //    ddlGraduateOrPostGraduate.Items.Add(new ListItem((StringEnum.GetStringValue(color)), ((int)color).ToString()));
                //}
            }
            gvInnerGridView.InnerHtml = GetGridString().ToString();
        }
        #endregion
    }
}