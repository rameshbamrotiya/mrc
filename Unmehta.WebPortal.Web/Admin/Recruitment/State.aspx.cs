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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class State : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (IStateRepository objRecruitmentCastRepository = new StateRepository(Functions.strSqlConnectionString))
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
                    BindGridView();
                    ClearFormData();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (StateRepository objRecruitmentCastRepository = new StateRepository(Functions.strSqlConnectionString))
            {
                if (txtStateName.Text == "")
                {
                    Functions.MessagePopup(this, "Please enter state name.", PopupMessageType.warning);
                    return;
                }
                string errorMessage = "Saved Successfully.";
                if (!objRecruitmentCastRepository.InsertOrUpdateState(new Model.Model.StateModel { RecId = Convert.ToInt32(hfRowId.Value), StateName = txtStateName.Text }, out errorMessage))
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }


        #region API of Pages
        [WebMethod]
        public static string GetAllSubDetailById(int id)
        {
            using (StateRepository objStateRepository = new StateRepository(Functions.strSqlConnectionString))
            {
                List<StateModel> dataRelated = objStateRepository.GetAllStateDetailsByAddIdWithName(id).ToList();

                return string.Join("|", new List<string>(dataRelated.Select(x => x.StateName.Replace(" ", "_")).ToList()).ToArray());
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
            txtStateName.Text = "";
        }
        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (StateRepository objStateRepository = new StateRepository(Functions.strSqlConnectionString))
            {
                int rowNo = 1;
                var gvData = objStateRepository.GetAllTblState();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        strgvData.Append("<table id='gvEduQuaData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>");
                        strgvData.Append("<th>SrNo</th>");
                        strgvData.Append("<th>State Name</th>");
                        strgvData.Append("<th>Action</th>");
                        strgvData.Append("</tr>");
                        strgvData.Append("</thead>");
                        strgvData.Append("<tbody>");
                        foreach (var row in gvData)
                        {
                            string strLink = (SessionWrapper.UserPageDetails.CanUpdate ? "<a title=\"Edit\" class=\"btn\" onClick=\"GetEditData('" + row.RecId + "|" + row.StateName + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>":"");
                            strgvData.Append("<tr>");
                            strgvData.Append("<td>" + (rowNo++) + "</td>");
                            strgvData.Append("<td>" + row.StateName + "</td>");
                            strgvData.Append("<td>" + strLink + "</td>");
                            strgvData.Append("</tr>");
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