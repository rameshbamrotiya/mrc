using System;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    [ScriptService]
    public partial class Relation : System.Web.UI.Page
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
            using (IRecruitmentRelationRepository objRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Saved Successfully.";
                if (!objRelationRepository.InsertOrUpdateTblRecruitmentRelation(new Model.Model.RecruitmentRelationGridModel { Id = Convert.ToInt32(hfRowId.Value), Name = txtRelationName.Text, IsVisible = chkEnable.Checked }, out errorMessage))
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
            using (IRecruitmentRelationRepository objRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objRelationRepository.RemoveTblRecruitmentRelation(id, out errorMessage))
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
            txtRelationName.Text = "";
            chkEnable.Checked = false;
            BindGridView();
        }

        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (IRecruitmentRelationRepository objRelationRepository = new RecruitmentRelationRepository(Functions.strSqlConnectionString))
            {
                var gvData = objRelationRepository.GetAllTblRecruitmentRelation();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        strgvData.Append("<table id='gvEduQuaData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>                      ");
                        strgvData.Append("    <th>ID</th>           ");
                        strgvData.Append("    <th>Name</th>         ");
                        strgvData.Append("    <th>Enable</th>         ");
                        strgvData.Append("    <th>Action</th>       ");
                        strgvData.Append("</tr>                     ");
                        strgvData.Append("</thead>                  ");
                        strgvData.Append("<tbody>                   ");
                        foreach (var row in gvData)
                        {
                            string strLink = (SessionWrapper.UserPageDetails.CanUpdate ? "<a class=\"btn\" onClick=\"GetEditData('" + row.Id + "|" + row.Name + "|" + row.IsVisible.ToString() + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>":"")
                                            + (SessionWrapper.UserPageDetails.CanDelete ? "<a class=\"btn\" onClick=\"RemoveData('" + row.Id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>":"");
                            strgvData.Append("<tr>                      ");
                            strgvData.Append("    <td>" + row.Id + "</td>   ");
                            strgvData.Append("    <td>" + row.Name + "</td> ");
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
            gvInnerGridView.InnerHtml = GetGridString().ToString();
        }
        #endregion
    }
}