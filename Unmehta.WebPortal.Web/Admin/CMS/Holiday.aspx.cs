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
using Unmehta.WebPortal.Repository.Interface.CMS;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.CMS;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class Holiday : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            if (!IsPostBack)
            {
                ClearFormData();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IHolidayMasterRepository objHolidayMasterRepository = new HolidayMasterRepository(Functions.strSqlConnectionString))
            {
                HolidayMasterModel objBO = new HolidayMasterModel();
                LoadControls(objBO);
                if (!objHolidayMasterRepository.InsertOrUpdateHolidayMaster(objBO, out errorMessage))
                {
                    ClearFormData();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
        }

        #endregion

        #region API of Pages
        [WebMethod]
        public static string RemoveDetailById(int id)
        {
            using (IHolidayMasterRepository objAdvertisementRepository = new HolidayMasterRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objAdvertisementRepository.RemoveHolidayMaster(id, out errorMessage))
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

        private void LoadControls(HolidayMasterModel objBo)
        {
            if (!string.IsNullOrEmpty(txtHolidayDate.Text))
                objBo.h_date = Convert.ToDateTime(txtHolidayDate.Text);

            if (!string.IsNullOrEmpty(txtholidaydesc.Text))
                objBo.h_desc = txtholidaydesc.Text;

            objBo.IsActive = ddlActiveInactive.SelectedValue.ToString() == "1" ? true : false;
            objBo.user_id = "Admin";
            objBo.ip_add = "1";
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.id = 0;
            }
            else
            {
                objBo.id = Convert.ToInt32(hfID.Value);
            }
        }
        private void ClearFormData()
        {
            hfID.Value = "0";
            txtholidaydesc.Text = "";
            txtHolidayDate.Text = "";
            ddlActiveInactive.SelectedIndex = 0;
            BindGridView();
        }

        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (IHolidayMasterRepository objHolidayMasterRepository = new HolidayMasterRepository(Functions.strSqlConnectionString))
            {
                int rowNo = 1;
                var gvData = objHolidayMasterRepository.GetAllHolidayMaster();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        strgvData.Append("<table id='gvEduQuaData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>                      ");
                        strgvData.Append("    <th>SrNo</th>           ");
                        strgvData.Append("    <th>Holiday Description</th>         ");
                        strgvData.Append("    <th>Holiday Date </th>         ");
                        strgvData.Append("    <th>Enable</th>         ");
                        strgvData.Append("    <th>Action</th>       ");
                        strgvData.Append("</tr>                     ");
                        strgvData.Append("</thead>                  ");
                        strgvData.Append("<tbody>                   ");
                        foreach (var row in gvData)
                        {
                            string strLink = "<a class=\"btn\" onClick=\"GetEditData('" + row.id + "|" + row.h_description + "|" + Convert.ToDateTime(row.h_date).ToString("dd/MM/yyyy").Replace("-","/") + "|" + row.is_active +"')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>"
                                            + "<a class=\"btn\" onClick=\"RemoveData('" + row.id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>";
                            strgvData.Append("<tr>                      ");
                            strgvData.Append("    <td>" + (rowNo++) + "</td>   ");
                            strgvData.Append("    <td>" + row.h_description + "</td> ");
                            strgvData.Append("    <td>" + Convert.ToDateTime(row.h_date).ToString("dd/MM/yyyy").Replace("-", "/") + "</td> ");
                            strgvData.Append("    <td>" + row.is_active.ToString() + "</td> ");
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