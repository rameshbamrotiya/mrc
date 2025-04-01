using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class OtherFacilitiesMaster : System.Web.UI.Page
    {
        public static long lgType5PageIndex;
        #region Main Code

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDetails();
            }
        }

        private void BindDetails()
        {
            using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
            {
                if(!IsPostBack)
                {
                    LanguageMasterBAL objBAL = new LanguageMasterBAL();
                    DataTable dt = objBAL.FillLanguage().Tables[0];
                    Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                    ddlLanguage.SelectedIndex = 1;
                }
                var mainData = objOtherFacilitiesRepository.GetAllOtherFacilitiesMaster(Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                if(mainData!=null)
                {
                    hfID.Value = mainData.OurFacillityId.ToString();
                    txtName.Text = mainData.Title;
                    txtExternalVideoLink.Text = mainData.VideoLink;
                    lblSideImage.Text = mainData.SideImage;
                    hfSideImage.Value = mainData.SideImage;
                    if (string.IsNullOrWhiteSpace(lblSideImage.Text))
                    {
                        aRemoveSideImage.Visible = false;
                    }
                    else
                    {
                        aRemoveSideImage.Visible = true;
                    }
                    chkEnable.Checked = Convert.ToBoolean(mainData.IsVisible);
                    dvSubDetails.Visible = true;

                    BindSubDetails();

                    //objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
                }
                else
                {
                    hfID.Value = "0";
                    txtName.Text = "";
                    dvSubDetails.Visible = false;
                    txtExternalVideoLink.Text = "";
                    lblSideImage.Text = "";
                    hfSideImage.Value = "";
                    if (string.IsNullOrWhiteSpace(lblSideImage.Text))
                    {
                        aRemoveSideImage.Visible = false;
                    }
                    else
                    {
                        aRemoveSideImage.Visible = true;
                    }
                    chkEnable.Checked =false;
                }
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlLanguage.SelectedIndex>0)
            {
                BindDetails();
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
            {
                GetAllOtherFacilitiesMasterResult objBo = new GetAllOtherFacilitiesMasterResult();
                if (!ValidMainForm(ref objBo))
                {
                    string errorMessage = "Saved Successfully.";
                    if (!objOtherFacilitiesRepository.InsertOrUpdateAboutNursingCareMaster(objBo, out errorMessage))
                    {
                        BindDetails();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        private bool ValidMainForm(ref GetAllOtherFacilitiesMasterResult objBo)
        {
            bool isError = false;
            if (objBo == null)
            {
                objBo = new GetAllOtherFacilitiesMasterResult();
            }

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }

            objBo.IsVisible = chkEnable.Checked;

            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);


            if (fuSideImage.HasFile)
            {
               string  filePath = ConfigDetailsValue.AddOurExcellenceFileUploadPath;

                if (!filePath.Contains("|"))
                {

                    var filename = DateTime.Now.ToString("ddMMyyyyttthhmmss") + System.IO.Path.GetExtension(fuSideImage.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + filename;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    objBo.SideImage = pathToCheck1;
                    // Check to see if a file already exists with the
                    // same name as the file to upload.
                    if (File.Exists(Server.MapPath(pathToCheck1)))
                    {
                        File.Delete(Server.MapPath(pathToCheck1));
                    }

                    //Save selected file into specified location
                    fuSideImage.SaveAs(Server.MapPath(filePath) + filename);
                }
                else
                {
                    string errorMessageFile = filePath.Split('|')[0];
                    bool isValidate = false;
                }
            }
            else
            {
                objBo.SideImage = hfSideImage.Value;
            }


            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Title", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.Title = txtName.Text;
            }


            if (!string.IsNullOrEmpty(txtExternalVideoLink.Text))
                objBo.VideoLink = txtExternalVideoLink.Text;

            return isError;
        }

        #endregion

        #region Sub Code

        private void BindSubDetails()
        {
            long id = 0;
            if(ddlLanguage.SelectedIndex>0 && long.TryParse(hfID.Value,out id))
            {
                if (id > 0)
                {
                    using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
                    {
                        gvSubDetails.DataSource = objOtherFacilitiesRepository.GetAllOurFacilitiesMasterSubDetails((Convert.ToInt32(hfID.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue))).OrderBy(x => x.SequanceNo).ToList();
                        gvSubDetails.DataBind();
                    }
                }
                else
                {
                    gvSubDetails.DataSource = null;
                    gvSubDetails.DataBind();
                }
            }
            else
            {
                gvSubDetails.DataSource = null;
                gvSubDetails.DataBind();
            }
        }

        private void ClearSubDetail()
        {
            hfSubId.Value = "0";
            txtSubTitle.Text = "";
            txtSubDescription.Text = "";
            hfRowId.Value = "";
            Type5 = new List<GetAllOurFacilitiesMasterSubDetailsImageResult>();
        }

        protected void gvSubDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
                {
                    if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                    {
                        int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        Int32 bytID;
                        bytID = Convert.ToInt32(gvSubDetails.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            string strMesssage = "";
                            objOtherFacilitiesRepository.RemoveOurFacilitiesMasterSubDetails(bytID,out strMesssage);
                            BindSubDetails();
                            Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                            return;
                        }
                    }
                    else
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                        if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                        {

                            string col_parent_id = commandArgs[0];
                            string col_menu_level = commandArgs[1];
                            string cmd = commandArgs[2];

                            switch (cmd)
                            {
                                case "up":
                                    SetPageOrder(cmd, col_menu_level, col_parent_id);
                                    break;
                                case "down":
                                    SetPageOrder(cmd, col_menu_level, col_parent_id);
                                    break;

                            }
                            BindSubDetails();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }

        private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            string strMesssage = "";
            using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
            {
                if (objOtherFacilitiesRepository.OurFacilitiesMasterSubDetailsSwap(cmd, Convert.ToDecimal(col_menu_level), Convert.ToInt32(col_parent_id),out strMesssage))
                {

                }
            }
        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubId.Value = gvSubDetails.DataKeys[rowindex]["Id"].ToString();
            using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objOtherFacilitiesRepository.GetOurFacilitiesMasterSubDetails((Convert.ToInt32(hfSubId.Value)), (Convert.ToInt32(hfID.Value)),(Convert.ToInt32(ddlLanguage.SelectedValue)));
                if (dataInfo != null)
                {
                    txtSubTitle.Text = dataInfo.Title;
                    txtSubDescription.Text = string.IsNullOrWhiteSpace(dataInfo.Description)? "": HttpUtility.HtmlDecode(dataInfo.Description);
                    hfSequanceId.Value = dataInfo.SequanceNo.ToString();

                    {
                        var ImageList = objOtherFacilitiesRepository.GetAllOurFacilitiesMasterSubDetailsImage(dataInfo.Id, (Convert.ToInt32(ddlLanguage.SelectedValue)));
                        if (ImageList.Count() > 0)
                        {
                            Type5 = ImageList.ToList();
                            BindSubDetails();
                            BindType5Form();
                        }
                    }
                }
                else
                {
                    txtSubTitle.Text = "";
                    txtSubDescription.Text =  "" ;
                    hfSequanceId.Value =  "0";
                }
            }
        }

        protected void btnSubDetailsSave_Click(object sender, EventArgs e)
        {
            using (IOtherFacilitiesRepository objOtherFacilitiesRepository = new OtherFacilitiesRepository(Functions.strSqlConnectionString))
            {
                GetAllOurFacilitiesMasterSubDetailsResult objBo = new GetAllOurFacilitiesMasterSubDetailsResult();
                if (!ValidSubForm(ref objBo))
                {
                    string errorMessage = "Saved Successfully.";
                    if (!objOtherFacilitiesRepository.InsertOrUpdateOurFacilitiesMasterSubDetails(objBo, out errorMessage))
                    {
                        if(!objOtherFacilitiesRepository.RemoveOurFacilitiesMasterSubDetailsImage((int)objBo.Id, out errorMessage))
                        {
                            foreach(var row in Type5)
                            {
                                GetAllOurFacilitiesMasterSubDetailsImageResult objBos = new GetAllOurFacilitiesMasterSubDetailsImageResult();
                                objBos.OurFacillityId = objBo.OurFacillityId;
                                objBos.OurFacillitySubId = objBo.Id;
                                objBos.LanguageId = objBo.LanguageId;
                                objBos.ImageName = row.ImageName;
                                objBos.Id = 0;
                                objOtherFacilitiesRepository.InsertOurFacilitiesMasterSubDetailsImage(objBos, out errorMessage);
                            }
                            ClearSubDetail();
                            BindType5Form();
                            BindSubDetails();
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        private bool ValidSubForm(ref GetAllOurFacilitiesMasterSubDetailsResult objBo)
        {
            bool isError = false;
            if(objBo==null)
            {
                objBo = new GetAllOurFacilitiesMasterSubDetailsResult();
            }

            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);


            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.OurFacillityId = 0;
            }
            else
            {
                objBo.OurFacillityId = Convert.ToInt32(hfID.Value);
            }

            if (string.IsNullOrWhiteSpace(hfSubId.Value))
            {
                objBo.Id = 0;
                objBo.SequanceNo = gvSubDetails.Rows.Count+1;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfSubId.Value);
                objBo.SequanceNo = Convert.ToInt32(hfSequanceId.Value);
            }

            if (string.IsNullOrWhiteSpace(txtSubTitle.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Title", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.Title = txtSubTitle.Text;
            }

            if (string.IsNullOrWhiteSpace(txtSubDescription.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Description", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.Description = HttpUtility.HtmlEncode(txtSubDescription.Text);
            }

            return isError;
        }

        #endregion

        #region Type5
        
        private List<GetAllOurFacilitiesMasterSubDetailsImageResult> Type5
        {
            get
            {
                List<GetAllOurFacilitiesMasterSubDetailsImageResult> dt = (List<GetAllOurFacilitiesMasterSubDetailsImageResult>)HttpContext.Current.Session["Type5"];
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    dt = new List<GetAllOurFacilitiesMasterSubDetailsImageResult>();
                }

                return dt;
            }
            set { HttpContext.Current.Session["Type5"] = value; }
        }

        #region Page Methods

        protected void btnType5Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type5 == null)
            {
                Type5 = new List<GetAllOurFacilitiesMasterSubDetailsImageResult>();
                
            }

            GetAllOurFacilitiesMasterSubDetailsImageResult objBo = new GetAllOurFacilitiesMasterSubDetailsImageResult();
            if (!ValidateSubType5(ref objBo))
            {
                List<GetAllOurFacilitiesMasterSubDetailsImageResult> lstData = Type5;
                if (lstData == null)
                {
                    lstData = new List<GetAllOurFacilitiesMasterSubDetailsImageResult>();
                    lstData.Add(objBo);
                }
                else
                {
                    //int rowCount= lstData.Count();
                    //if(rowCount > objBo.Id)
                    //{
                    //    var checkForUPdate = lstData.ElementAt((int)objBo.Id);
                    //    if (checkForUPdate != null)
                    //    {
                    //        lstData.Remove(checkForUPdate);
                    //        lstData.Insert((int)objBo.Id, objBo);
                    //    }
                    //}
                    //else
                    {
                        lstData.Add(objBo);
                    }
                }

                Functions.MessagePopup(this, errorMessage, PopupMessageType.success);


                Type5 = lstData;

                ClearType5Form();
                BindType5Form();
            }
        }

        protected void btnType5Clear_Click(object sender, EventArgs e)
        {
            ClearType5Form();
        }

        protected void ibtn_Type5Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<GetAllOurFacilitiesMasterSubDetailsImageResult> lstData = Type5;
            var checkForUPdate = lstData.ElementAt(rowindex);
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);
                Type5 = lstData;

                ClearType5Form();
                BindType5Form();
            }

        }

        protected void gvType5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType5Form();
            gvType5.PageIndex = e.NewPageIndex;
            lgType5PageIndex = e.NewPageIndex;
            gvType5.DataBind();
        }

        #endregion

        #region Page Functions
        
        public bool ValidateSubType5(ref GetAllOurFacilitiesMasterSubDetailsImageResult objBO)
        {
            bool isError = false;
            long lgSequanceNO = 0;

            if(!long.TryParse(hfSequanceId.Value,out lgSequanceNO))
            {
                if(lgSequanceNO<=0)
                {
                    lgSequanceNO = gvSubDetails.Rows.Count+1;
                }
            }
            objBO.OurFacillitySubId = lgSequanceNO;
            hfSequanceId.Value = lgSequanceNO.ToString();
            objBO.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);

            if (fuType5PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType5PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type5 Image 10 mb.", PopupMessageType.error);
                        fuType5PopupImage.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType5PopupImage.FileName);
                    objBO.ImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType5PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type5 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return true;
                }
            }
            else
            {
                objBO.ImageName = (hfType5PopUpImage.Value);
            }
            return isError;
        }

        public void BindType5Form()
        {
            long lgSequanceNO = 0;
            if (!long.TryParse(hfSequanceId.Value, out lgSequanceNO))
            {
                if (lgSequanceNO <= 0)
                {
                    lgSequanceNO = gvSubDetails.Rows.Count+1;
                }
            }
            if (Type5.Count()>0)
            {
                gvType5.DataSource = Type5;
                var list = Type5.Where(x => x.LanguageId == Convert.ToInt32(ddlLanguage.SelectedValue)).ToList();
                gvType5.DataSource = list;
            }
            else
            {
                gvType5.DataSource = Type5;
            }
            gvType5.DataBind();
        }

        public void ClearType5Form()
        {
            hfType5PopUpImage.Value = "";
        }

        #endregion

        #endregion
    }
}