using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Data;
using BAL;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Text;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class Equipment : System.Web.UI.Page
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
            }
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                LanguageMasterBAL objBAL = new LanguageMasterBAL();
                ds = objBAL.FillLanguage();
                DataTable dt = ds.Tables[0];
                Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                ClearFormData();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = "";
            using (IEquipmentMasterRepository objEquipmentMasterRepository = new EquipmentMasterRepository(Functions.strSqlConnectionString))
            {
                if (fuFileUpload.HasFile)
                {
                    filePath = ConfigDetailsValue.AddEquipmentFileUploadPath;

                    if (!filePath.Contains("|"))
                    {

                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuFileUpload.FileName);

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
                        fuFileUpload.SaveAs(Server.MapPath(filePath) + "/" + fileName);
                    }
                    else
                    {
                        string errorMessageFile = filePath.Split('|')[0];
                        bool isValidate = false;
                    }
                }
                else
                {
                    fileName = hfFilName.Value;
                }
                string errorMessage = "Saved Successfully.";
                if (!objEquipmentMasterRepository.InsertOrUpdateTblEquipmentMaster(new EquipmentMasterGridModel { Id = Convert.ToInt32(hfRowId.Value), LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue), EquipmentName = HttpUtility.HtmlEncode(txtEquipmentName.Text), EquipmentFileName = fileName, EquipmentFilePath = filePath, IsVisible = chkEnable.Checked }, out errorMessage))
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
            using (IEquipmentMasterRepository objEquipmentMasterGridModel = new EquipmentMasterRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objEquipmentMasterGridModel.RemoveTblEquipmentMaster(id, out errorMessage))
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
            txtEquipmentName.Text = "";
            hfFilName.Value = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (IEquipmentMasterRepository objEquipmentMasterRepository = new EquipmentMasterRepository(Functions.strSqlConnectionString))
            {
                var gvData = objEquipmentMasterRepository.GetAllTblEquipmentMaster();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        string filePath = ConfigDetailsValue.AddEquipmentFileUploadPath;
                        var pathToCheck = "/" + filePath + "/";
                        strgvData.Append("<table id='gvEquipmentData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>                      ");
                        strgvData.Append("    <th>ID</th>           ");
                        strgvData.Append("    <th>Language</th>         ");
                        strgvData.Append("    <th>Equipment Name</th>         ");
                        strgvData.Append("    <th>Enable</th>         ");
                        strgvData.Append("    <th>Action</th>       ");
                        strgvData.Append("</tr>                     ");
                        strgvData.Append("</thead>                  ");
                        strgvData.Append("<tbody>                   ");
                        foreach (var row in gvData)
                        {
                            string strLink = "<a class=\"btn\" onClick=\"GetEditData('" + row.Id + "|" + row.LanguageId + "|" + row.EquipmentName + "|" + row.IsVisible + "|" + row.EquipmentFileName.ToString() + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>"
                                            + "<a class=\"btn\" onClick=\"RemoveData('" + row.Id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>"
                                            + (string.IsNullOrEmpty(row.EquipmentFileName) ? "" : "<a class=\"btn\" onClick=\"downloadURL('" + pathToCheck + row.EquipmentFileName + "','" + row.EquipmentFileName + "')\" type =\"button\"><i class='fa fa-download'></i></a>");
                            strgvData.Append("<tr>                      ");
                            strgvData.Append("    <td>" + row.Id + "</td>   ");
                            strgvData.Append("    <td>" + row.LanguageName + "</td> ");
                            strgvData.Append("    <td>" + row.EquipmentName + "</td> ");
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
            if (GetGridString().ToString() != "")
            {
                divGrid.Visible = true;
                gvInnerGridView.InnerHtml = GetGridString().ToString();
            }
            else
            {
                divGrid.Visible = false;
            }

        }
        #endregion
    }
}