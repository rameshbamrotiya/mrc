using BAL;
using BO;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.IO;
using System.Web.Services;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class AboutUs : System.Web.UI.Page
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
                ddlLanguage.SelectedIndex = 1;
                ClearControlValues();
                BindDesignationByLangId(1);
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtAboutUsDescription.Text = "";
            txtMetaTitle.Text = "";
            txtMetaDescription.Text = "";
            txtHeadingTitle.Text = "";
            txtRightHeadingTitle.Text = "";
            dvSubView.Visible = false;
            if (!string.IsNullOrWhiteSpace(hfID.Value))
            {
                ddlLanguage.SelectedIndex = 1;
                //ddlLanguage.Enabled = false;
                using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
                {
                    List<AboutUsMasterGridModel> lstData = objAboutUsMasterRepository.GetAlllongAboutUsMaster(Convert.ToInt32(ddlLanguage.SelectedValue));
                    int i = 0;
                    AboutUsMasterGridModel data = lstData.FirstOrDefault();
                    
                    if (data != null)
                    {
                        hfID.Value = data.Id.ToString();
                        txtAboutUsDescription.Text = HttpUtility.HtmlDecode(data.AboutUsDescription);
                        txtMetaTitle.Text = data.MetaTitle;
                        txtMetaDescription.Text = data.MetaDescription;
                        txtHeadingTitle.Text = data.HeadingTitle;
                        txtRightHeadingTitle.Text = data.RightSideHeadingTitle;
                        //dvSubView.Visible = true;
                    }
                    else
                    {
                        //dvSubView.Visible = false;

                        hfID.Value = "0";
                        txtAboutUsDescription.Text = "";
                        txtMetaTitle.Text = "";
                        txtMetaDescription.Text = "";
                        txtHeadingTitle.Text = "";
                        txtRightHeadingTitle.Text = "";
                    }
                    BindSubGridView();
                }
            }
            BindDesignationByLangId();
            ClearControlSubValues();
            ddlDesignation.SelectedIndex = 0;
            imgProfile.ImageUrl = "";
            hfFilName.Value = "";
        }
        public void ClearSubValues()
        {

            txtAboutUsDescription.Text = "";
            txtExecutiveName.Text = "";
            txtMessage.Text = "";
        }

        private bool LoadControlsAdd(AboutUsMasterGridModel objBo)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }
            if (!string.IsNullOrEmpty(txtAboutUsDescription.Text))
            {
                objBo.AboutUsDescription = HttpUtility.HtmlEncode(txtAboutUsDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter AboutUs Description", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtMetaTitle.Text))
                objBo.MetaTitle = txtMetaTitle.Text;

            if (!string.IsNullOrEmpty(txtMetaDescription.Text))
                objBo.MetaDescription = txtMetaDescription.Text;

            if (!string.IsNullOrEmpty(txtHeadingTitle.Text))
                objBo.HeadingTitle = txtHeadingTitle.Text;

            if (!string.IsNullOrEmpty(txtRightHeadingTitle.Text))
                objBo.RightSideHeadingTitle = txtRightHeadingTitle.Text;

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }


            return true;
        }

        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                AboutUsMasterGridModel objBo = new AboutUsMasterGridModel();
                if (LoadControlsAdd(objBo))
                {

                    if (!objAboutUsMasterRepository.InsertOrUpdatelongAboutUsMaster(objBo, out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                    ClearControlValues();
                }
            }
        }

        //protected void ibtn_Delete_Click(object sender, EventArgs e)
        //{
        //    string errorMessage = "";
        //    try
        //    {
        //        int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
        //        int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
        //        using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
        //        {
        //            objAboutUsMasterRepository.RemovelongAboutUsMaster(rowId, out errorMessage);
        //            ClearControlValues();
        //            BindGridView();
        //            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
        //    }
        //}

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                AboutUsMasterGridModel objBo = new AboutUsMasterGridModel();
                if (LoadControlsAdd(objBo))
                {
                    using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
                    {
                        if (!objAboutUsMasterRepository.InsertOrUpdatelongAboutUsMaster(objBo, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                    }
                    ClearControlValues();
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        private void BindDesignationByLangId(long LangId = 1)
        {
            ddlDesignation.Items.Clear();
            using (IDesignationRepository objDesignationRepository = new DesignationRepository(Functions.strSqlConnectionString))
            {
                if (objDesignationRepository.GetAllTblDesignationLang(LangId).Count > 0)
                {
                    ddlDesignation.DataSource = objDesignationRepository.GetAllTblDesignationLang(LangId);
                    ddlDesignation.DataValueField = "Id";
                    ddlDesignation.DataTextField = "DesignationName";
                }
                else
                {
                    ddlDesignation.DataSource = null;
                }
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select Designation ", "-1"));
                ddlDesignation.SelectedIndex = 0;
            }

            using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                long lgId;
                if (long.TryParse(hfID.Value, out lgId))
                {
                    if (lgId > 0)
                    {
                        AboutUsMasterGridModel objBo = objAboutUsMasterRepository.GetlongAboutUsMasterById(LangId, Convert.ToInt32(hfID.Value));

                        if (objBo != null)
                        {
                            txtAboutUsDescription.Text = HttpUtility.HtmlDecode(objBo.AboutUsDescription);
                            txtMetaTitle.Text = objBo.MetaTitle;
                            txtMetaDescription.Text = objBo.MetaDescription;
                            txtHeadingTitle.Text = objBo.HeadingTitle;
                            txtRightHeadingTitle.Text = objBo.RightSideHeadingTitle;
                        }
                        else
                        {
                            txtAboutUsDescription.Text = "";
                            txtMetaTitle.Text = "";
                            txtMetaDescription.Text = "";
                            txtHeadingTitle.Text = "";
                            txtRightHeadingTitle.Text = "";
                        }
                    }
                    else
                    {
                        txtAboutUsDescription.Text = "";
                        txtMetaTitle.Text = "";
                        txtMetaDescription.Text = "";
                        txtHeadingTitle.Text = "";
                        txtRightHeadingTitle.Text = "";
                    }
                }
                else
                {
                    txtAboutUsDescription.Text = "";
                    txtMetaTitle.Text = "";
                    txtMetaDescription.Text = "";
                    txtHeadingTitle.Text = "";
                    txtRightHeadingTitle.Text = "";
                }
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDesignationByLangId(Convert.ToInt32(ddlLanguage.SelectedValue));
        }
        #endregion

        #region Sub Page Event
        private bool LoadSubControlsAdd(AboutUsMasterDesignationGridModel objBo)
        {

            if (!string.IsNullOrEmpty(txtExecutiveName.Text))
            {
                objBo.DesignationName = HttpUtility.HtmlEncode(txtExecutiveName.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Executive Name", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtMessage.Text))
                objBo.Message = txtMessage.Text;


            if (ddlDesignation.SelectedIndex > 0)
            {
                objBo.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            }

            if (string.IsNullOrWhiteSpace(hfDesignationId.Value))
            {
                Functions.MessagePopup(this, "Please Select Row First", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfDesignationId.Value);
            }

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                Functions.MessagePopup(this, "Please Save Upper Detail First", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.AbountUsId = Convert.ToInt32(hfID.Value);
            }

            if (!string.IsNullOrWhiteSpace(hfFilName.Value))
            {
                objBo.PhotoName = hfFilName.Value;
            }
            if (!string.IsNullOrWhiteSpace(hfFilName.Value))
            {
                objBo.PhotoName = hfFilName.Value;
                objBo.PhotoPath = ConfigDetailsValue.AddAboutUsFileUploadPath + "/" + objBo.PhotoName;
            }

            if (fuImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddAboutUsFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    //if (fuImage.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    objBo.PhotoName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuImage.FileName);
                    objBo.PhotoPath = filePath + "/" + objBo.PhotoName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + objBo.PhotoName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.PhotoName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuImage.SaveAs(Server.MapPath(filePath) + "/" + objBo.PhotoName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                objBo.PhotoName = hfFilName.Value;
                objBo.PhotoPath = string.IsNullOrWhiteSpace(hfFilName.Value) ? "" : (ConfigDetailsValue.AddAboutUsFileUploadPath + "/" + hfFilName.Value);
            }

            return true;
        }

        private void BindSubGridView()
        {
            using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                List<AboutUsMasterDesignationGridModel> lstData = new List<AboutUsMasterDesignationGridModel>();

                if (hfID.Value != "0")
                {
                    lstData = objAboutUsMasterRepository.GetAlllongAboutUsDesignationMaster(Convert.ToInt32(hfID.Value), Convert.ToInt32(ddlLanguage.SelectedValue));
                    //dvSubView.Visible = true;
                }
                else
                {
                    lstData = new List<AboutUsMasterDesignationGridModel>();
                    //dvSubView.Visible = false;
                }
                grdUser.DataSource = lstData;
                grdUser.DataBind();
            }
        }

        protected void btnSubAdd_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                AboutUsMasterDesignationGridModel objBo = new AboutUsMasterDesignationGridModel();
                //if (fuImage.HasFile)
                //{
                //    System.Drawing.Image img = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);
                //    int height = img.Height;
                //    int width = img.Width;
                //    decimal size = Math.Round(((decimal)fuImage.PostedFile.ContentLength / (decimal)1024), 2);
                //    if (height != 963 || width != 840)
                //    {
                //        Functions.MessagePopup(this, "Please upload 840px*963px.", PopupMessageType.error);
                //        return;
                //    }
                //}
                if (LoadSubControlsAdd(objBo))
                {

                    if (!objAboutUsMasterRepository.InsertOrUpdatelongAboutUsDesignation(objBo, Convert.ToInt32(ddlLanguage.SelectedValue), out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                    ClearControlSubValues();
                }
            }
        }

        private void ClearControlSubValues()
        {
            hfDesignationId.Value = "0";
            txtExecutiveName.Text = "";
            txtMessage.Text = "";
            ddlDesignation.SelectedIndex = 0;
            hfFilName.Value = "";
            BindSubGridView();
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
            using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objAboutUsMasterRepository.GetlongAboutUsDesignationMasterById(Convert.ToInt32(hfID.Value), rowId, Convert.ToInt32(ddlLanguage.SelectedValue));

                if (data != null)
                {
                    hfDesignationId.Value = data.Id.ToString();
                    txtExecutiveName.Text = data.DesignationName;
                    txtMessage.Text = data.Message;
                    ddlDesignation.SelectedValue = data.DesignationId.ToString();
                    hfFilName.Value = data.PhotoName;
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IAboutUsMasterRepository objAboutUsMasterRepository = new AboutUsMasterRepository(Functions.strSqlConnectionString))
                {
                    objAboutUsMasterRepository.RemovelongAboutUsDesignationMaster(rowId, out errorMessage);
                    ClearControlSubValues();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btnSubClear_Click(object sender, EventArgs e)
        {
            ClearControlSubValues();
        }
        #endregion

    }
}