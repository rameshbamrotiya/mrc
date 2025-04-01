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
    public partial class District : System.Web.UI.Page
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
                BindGridView();
                ClearFormData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (IDistrictRepository objDistrictRepository = new DistrictRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Saved Successfully.";
                if (!objDistrictRepository.InsertDistrictMasterDetails(new Model.Model.DistrictModel { Id = Convert.ToInt32(hfRowId.Value), StateId = Convert.ToInt32(ddlState.SelectedValue), DistrictName = txtDistrictName.Text, CreatedBy = null, ModifyBy = null, IsDelete = false, Ip = null }, out errorMessage))
                {
                    ClearFormData();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    BindGridView();
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
        #endregion

        #region API of Pages
        [WebMethod]
        public static string GetAllSubDetailById(int id)
        {
            using (DistrictRepository objDistrictRepository = new DistrictRepository(Functions.strSqlConnectionString))
            {
                List<DistrictModel> dataRelated = objDistrictRepository.GetAllDistrictDetailsByAddIdWithName(id).ToList();

                return string.Join("|", new List<string>(dataRelated.Select(x => x.DistrictName.Replace(" ", "_")).ToList()).ToArray());
            }
        }
        [WebMethod]
        public static string GetGridView()
        {
            return GetGridString().ToString();
            //GetAllEducationQualification();
        }
        [WebMethod]
        public static string RemoveDetailById(int id)
        {
            using (DistrictRepository objDistrictRepository = new DistrictRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objDistrictRepository.RemoveTblRecruitmentDistrict(id, out errorMessage))
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
        #endregion

        #region Page Method
        private void ClearFormData()
        {
            txtDistrictName.Text = "";
            ddlState.SelectedIndex = 0;
            hfRowId.Value = "0";
        }
        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (DistrictRepository objDistrictRepository = new DistrictRepository(Functions.strSqlConnectionString))
            {
                int rowNo = 1;
                var gvData = objDistrictRepository.GetAllTblDistrict();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        strgvData.Append("<table id='gvEduQuaData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>");
                        strgvData.Append("<th>SrNo</th>");
                        strgvData.Append("<th>District Name</th>");
                        strgvData.Append("<th>State Name</th>");
                        strgvData.Append("<th>Action</th>");
                        strgvData.Append("</tr>");
                        strgvData.Append("</thead>");
                        strgvData.Append("<tbody>");
                        foreach (var row in gvData)
                        {
                            string strLink = (SessionWrapper.UserPageDetails.CanUpdate ? "<a title=\"Edit\" class=\"btn\" onClick=\"GetEditData('" + row.Id + "|" + row.DistrictName + "|" + row.StateId + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>" : "")
                                            + (SessionWrapper.UserPageDetails.CanDelete ? "<a title=\"Delete\" class=\"btn\" onClick=\"RemoveData('" + row.Id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>" : "");
                            strgvData.Append("<tr>");
                            strgvData.Append("<td>" + (rowNo++) + "</td>");
                            strgvData.Append("<td>" + row.DistrictName + "</td>");
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
            ddlState.Items.Clear();
            using (DistrictRepository objDistrictRepository = new DistrictRepository(Functions.strSqlConnectionString))
            {
                ddlState.DataSource = objDistrictRepository.GetAllState();
                ddlState.DataValueField = "StateId";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();
            }
            gvInnerGridView.InnerHtml = GetGridString().ToString();
        }
        #endregion
    }
}