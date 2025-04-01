using BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Unmehta.WebPortal.Common;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Faculty;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class ExecutiveLeadership : System.Web.UI.Page
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
                BindDesignation();
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
            using (IExecutiveLeadershipRepository objExecutiveLeadershipRepository = new ExecutiveLeadershipRepository(Functions.strSqlConnectionString))
            {
                if (fuImage.HasFile)
                {
                    filePath = ConfigDetailsValue.AddExecutiveLeadershipFileUploadPath;

                    if (!filePath.Contains("|"))
                    {

                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuImage.FileName);

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
                        fuImage.SaveAs(Server.MapPath(filePath) + "/" + fileName);
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
                if (!objExecutiveLeadershipRepository.InsertOrUpdateTblExecutiveLeadership(new Model.Model.ExecutiveLeadershipMasterGridModel { Id = Convert.ToInt32(hfRowId.Value), LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue), Name = txtExecutiveName.Text, DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue), PhotoName = fileName, PhotoPath = filePath, Message = txtMessage.Text, IsVisible = chkEnable.Checked }, out errorMessage))
                {
                    ClearFormData();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
                else
                {
                    if (errorMessage.Contains("UNIQUE KEY"))
                    {
                        Functions.MessagePopup(this, "Executive name already exists ..!", PopupMessageType.error);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }

            }
        }
        #endregion

        #region API of Pages
        [WebMethod]
        public static string RemoveDetailById(int id)
        {
            using (IExecutiveLeadershipRepository objExecutiveLeadershipRepository = new ExecutiveLeadershipRepository(Functions.strSqlConnectionString))
            {
                string errorMessage = "Removed Record Successfully.";
                if (!objExecutiveLeadershipRepository.RemoveTblExecutiveLeadership(id, out errorMessage))
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
            txtExecutiveName.Text = "";
            hfFilName.Value = "";
            BindDesignation();
            txtMessage.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            chkEnable.Checked = false;
            BindGridView();
        }

        private static string GetGridString()
        {
            StringBuilder strgvData = new StringBuilder();
            using (IExecutiveLeadershipRepository objExecutiveLeadershipRepository = new ExecutiveLeadershipRepository(Functions.strSqlConnectionString))
            {
                var gvData = objExecutiveLeadershipRepository.GetAllTblExecutiveLeadership();
                if (gvData != null)
                {
                    if (gvData.Count > 0)
                    {
                        string filePath = ConfigDetailsValue.AddExecutiveLeadershipFileUploadPath;
                        var pathToCheck = "/" + filePath + "/";
                        strgvData.Append("<table id='gvExecutiveLeadershipData' class='table table-striped table-bordered' width='100%'> <thead> ");
                        strgvData.Append("<tr>                      ");
                        strgvData.Append("    <th>ID</th>           ");
                        strgvData.Append("    <th>Language</th>         ");
                        strgvData.Append("    <th>Executive Name</th>         ");
                        strgvData.Append("    <th>Designation Name</th>         ");
                        strgvData.Append("    <th>Message</th>         ");
                        strgvData.Append("    <th>Enable</th>         ");
                        strgvData.Append("    <th>Action</th>       ");
                        strgvData.Append("</tr>                     ");
                        strgvData.Append("</thead>                  ");
                        strgvData.Append("<tbody>                   ");
                        foreach (var row in gvData)
                        {
                            string strLink = (SessionWrapper.UserPageDetails.CanUpdate ? "<a class=\"btn\" onClick=\"GetEditData('" + row.Id + "|" + row.LanguageId + "|" + row.Name + "|" + row.DesignationId + "|" + row.Message + "|" + row.IsVisible + "|" + row.PhotoName.ToString() + "')\" type=\"button\"><i class='fa fa-pencil-square-o'></i></a>" : "")
                                            + (SessionWrapper.UserPageDetails.CanDelete ? "<a class=\"btn\" onClick=\"RemoveData('" + row.Id + "')\" type =\"button\"><i class='fa fa-trash'></i></a>" : "")
                                            + (string.IsNullOrEmpty(row.PhotoName) ? "" : "<a class=\"btn\" onClick=\"downloadURL('" + pathToCheck + row.PhotoName + "','" + row.PhotoName + "')\" type =\"button\"><i class='fa fa-download'></i></a>");
                            strgvData.Append("<tr>                      ");
                            strgvData.Append("    <td>" + row.Id + "</td>   ");
                            strgvData.Append("    <td>" + row.LanguageName + "</td> ");
                            strgvData.Append("    <td>" + row.Name + "</td> ");
                            strgvData.Append("    <td>" + row.DesignationName + "</td> ");
                            strgvData.Append("    <td>" + row.Message + "</td> ");
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
                BindDesignation();
                gvInnerGridView.InnerHtml = GetGridString().ToString();
            }
            else
            {
                divGrid.Visible = false;
            }

        }

        private void BindDesignation()
        {
            ddlDesignation.Items.Clear();
            using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            {
                ddlDesignation.DataSource = objDesignationRepository.GetAllTblDesignation();
                ddlDesignation.DataValueField = "Id";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select", "-1"));
            }
        }
        #endregion
    }
}