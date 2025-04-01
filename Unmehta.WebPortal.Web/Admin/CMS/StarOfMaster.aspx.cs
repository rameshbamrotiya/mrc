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

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class StarOfMaster : System.Web.UI.Page
    {
        public static long lgType5PageIndex;
        #region Main Code

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDetails();
            }
        }

        private void BindDetails()
        {
            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                if (!IsPostBack)
                {
                    LanguageMasterBAL objBAL = new LanguageMasterBAL();
                    DataTable dt = objBAL.FillLanguage().Tables[0];
                    Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                    ddlLanguage.SelectedIndex = 1;
                }
                var mainData = objStarOfRepository.GetAllStarOfDetails(Convert.ToInt32(ddlLanguage.SelectedValue)).FirstOrDefault();
                if (mainData != null)
                {
                    hfID.Value = mainData.StarId.ToString();
                    txtName.Text = mainData.StarPageTitle;

                    txtMonthTabName.Text = mainData.StarPageMonthTabName;
                    txtWeekTabName.Text = mainData.StarPageWeekTabName;
                    txtAccordMonthTitle.Text = mainData.StarAccordMonthTitle;
                    txtAccordWeekTitle.Text = mainData.StarAccordWeekTitle;
                    
                    dvSubDetails.Visible = true;

                    BindSubDetails();

                    //objBo.enabled = (ddlActiveInactive.SelectedValue == "1" ? true : false);
                }
                else
                {
                    hfID.Value = "0";
                    txtName.Text = "";
                    dvSubDetails.Visible = false;
                    txtMonthTabName.Text = "";
                    txtWeekTabName.Text = "";
                    txtAccordMonthTitle.Text = "";
                    txtAccordWeekTitle.Text = "";
                }
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanguage.SelectedIndex > 0)
            {
                BindDetails();
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                GetAllStarOfDetailsResult objBo = new GetAllStarOfDetailsResult();
                if (!ValidMainForm(ref objBo))
                {
                    string errorMessage = "Saved Successfully.";
                    if (!objStarOfRepository.InsertOrUpdateStarOfMaster(objBo, out errorMessage))
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

        private bool ValidMainForm(ref GetAllStarOfDetailsResult objBo)
        {
            bool isError = false;
            if (objBo == null)
            {
                objBo = new GetAllStarOfDetailsResult();
            }

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }



            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Title", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.StarPageTitle = txtName.Text;
            }
            

            if (!string.IsNullOrEmpty(txtMonthTabName.Text))
                objBo.StarPageMonthTabName = txtMonthTabName.Text;
            if (!string.IsNullOrEmpty(txtWeekTabName.Text))
                objBo.StarPageWeekTabName = txtWeekTabName.Text;
            if (!string.IsNullOrEmpty(txtAccordMonthTitle.Text))
                objBo.StarAccordMonthTitle = txtAccordMonthTitle.Text;
            if (!string.IsNullOrEmpty(txtAccordWeekTitle.Text))
                objBo.StarAccordWeekTitle = txtAccordWeekTitle.Text;

            return isError;
        }

        #endregion

        #region Sub Code

        private void BindSubDetails()
        {
            long id = 0;
            if (ddlLanguage.SelectedIndex > 0 && long.TryParse(hfID.Value, out id))
            {
                if (id > 0)
                {
                    using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
                    {
                        gvSubDetails.DataSource = objStarOfRepository.GetAllStarOfAccordDetailsByStartId((Convert.ToInt32(hfID.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue))).ToList();
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
            txtAccordTitle.Text = "";
            txtSequenceNo.Text = "";
            hfRowId.Value = "";
            Type5 = new List<GetAllStarOfAccordSubImageDetailsByAccordIdResult>();
        }

        protected void gvSubDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
                {
                    if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                    {
                        int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        Int32 bytID;
                        bytID = Convert.ToInt32(gvSubDetails.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            string strMesssage = "";
                            objStarOfRepository.RemoveStarOfAccordDetailsById(bytID, out strMesssage);
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
            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                if (objStarOfRepository.StarOfAccordDetailsSwap(cmd, Convert.ToDecimal(col_menu_level), Convert.ToInt32(col_parent_id), out strMesssage))
                {

                }
            }
        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubId.Value = gvSubDetails.DataKeys[rowindex]["Id"].ToString();
            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objStarOfRepository.GetStarOfAccordDetailsByStartId((Convert.ToInt32(hfSubId.Value)), (Convert.ToInt32(hfID.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue)));
                if (dataInfo != null)
                {
                    txtAccordTitle.Text = dataInfo.AccordTitle;
                    hfSequanceId.Value = dataInfo.SequanceNo.ToString();
                    txtSequenceNo.Text = dataInfo.SequanceNo.ToString();
                    chkParentIsVisible.Checked =(bool) dataInfo.IsVisible;
                    rblstar.SelectedValue = dataInfo.TypeMonthOrWeek.ToString();
                    {
                        var ImageList = objStarOfRepository.GetAllStarOfAccordSubImageDetailsByAccordId(dataInfo.Id, (Convert.ToInt32(ddlLanguage.SelectedValue)));
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
                    txtAccordTitle.Text = "";
                    rblstar.ClearSelection();
                    txtSequenceNo.Text = "";
                    chkParentIsVisible.Checked = false;
                    hfSequanceId.Value = "0";
                }
            }
        }

        protected void btnSubDetailsSave_Click(object sender, EventArgs e)
        {
            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                GetAllStarOfAccordDetailsByStartIdResult objBo = new GetAllStarOfAccordDetailsByStartIdResult();
                if (!ValidSubForm(ref objBo))
                {
                    string errorMessage = "Saved Successfully.";
                    if (!objStarOfRepository.InsertOrUpdateStarOfAccordDetails(objBo, out errorMessage))
                    {
                        if (!objStarOfRepository.RemoveStarOfAccordSubImageDetailsByAccordId((int)objBo.Id, (int)objBo.LanguageId, out errorMessage))
                        {
                            foreach (var row in Type5)
                            {
                                GetAllStarOfAccordSubImageDetailsByAccordIdResult objBos = new GetAllStarOfAccordSubImageDetailsByAccordIdResult();
                                objBos.StarId = objBo.StarId;
                                objBos.AccordId = objBo.Id;
                                objBos.LanguageId = objBo.LanguageId;
                                objBos.ImageName = row.ImageName;
                                objBos.Name = row.Name;
                                objBos.IsVisible = row.IsVisible;
                                objBos.Description = row.Description;
                                objBos.Id = 0;
                                objStarOfRepository.InsertStarOfAccordSubImageDetailsByAccordId(objBos, out errorMessage);
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

        private bool ValidSubForm(ref GetAllStarOfAccordDetailsByStartIdResult objBo)
        {
            bool isError = false;
            if (objBo == null)
            {
                objBo = new GetAllStarOfAccordDetailsByStartIdResult();
            }

            objBo.IsVisible = chkParentIsVisible.Checked;

            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
                objBo.LanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);


            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.StarId = 0;
            }
            else
            {
                objBo.StarId = Convert.ToInt32(hfID.Value);
            }

            if (string.IsNullOrWhiteSpace(hfSubId.Value))
            {
                objBo.Id = 0;

                objBo.SequanceNo = gvSubDetails.Rows.Count + 1;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfSubId.Value);
                objBo.SequanceNo = Convert.ToInt32(hfSequanceId.Value);
            }

            using (IStarOfRepository objStarOfRepository = new StarOfRepository(Functions.strSqlConnectionString))
            {
                long lgSequanceNo = 0;
                if (string.IsNullOrWhiteSpace(txtSequenceNo.Text))
                {
                    Functions.MessagePopup(this, "Please Enter Sequence No", PopupMessageType.error);
                    return true;
                }
                else if (!long.TryParse(txtSequenceNo.Text, out lgSequanceNo))
                {
                    Functions.MessagePopup(this, "Please Enter valid Sequence No", PopupMessageType.error);
                    return true;
                }
                else if (lgSequanceNo == 0)
                {
                    Functions.MessagePopup(this, "Please Enter valid Sequence No", PopupMessageType.error);
                    return true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(hfSubId.Value))
                    {
                        if (objStarOfRepository.GetAllStarOfAccordDetailsByStartId((Convert.ToInt32(hfID.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue))).Where(x => x.SequanceNo == lgSequanceNo).Count() <= 0)
                        {
                            objBo.SequanceNo = lgSequanceNo;
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter valid Sequence No", PopupMessageType.error);
                            return true;
                        }
                    }
                    else
                    {
                        long lgId = objBo.Id;
                        if (objStarOfRepository.GetAllStarOfAccordDetailsByStartId((Convert.ToInt32(hfID.Value)), (Convert.ToInt32(ddlLanguage.SelectedValue))).Where(x => x.SequanceNo == lgSequanceNo && x.Id!= lgId).Count() <= 0)
                        {
                            objBo.SequanceNo = lgSequanceNo;
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter valid Sequence No", PopupMessageType.error);
                            return true;
                        }
                    }
                }
             }

            if (string.IsNullOrWhiteSpace(txtAccordTitle.Text))
            {
                Functions.MessagePopup(this, "Please Enter Accord Title", PopupMessageType.error);
                return true;
            }
            else
            {
                objBo.AccordTitle = txtAccordTitle.Text;
            }

            objBo.TypeMonthOrWeek = Convert.ToInt32(rblstar.SelectedValue);

            return isError;
        }

        #endregion

        #region Type5

        private List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> Type5
        {
            get
            {
                List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> dt = (List<GetAllStarOfAccordSubImageDetailsByAccordIdResult>)ViewState["Type5"];
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    dt = new List<GetAllStarOfAccordSubImageDetailsByAccordIdResult>();
                }

                return dt;
            }
            set { ViewState["Type5"] = value; }
        }

        #region Page Methods

        protected void btnType5Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type5 == null)
            {
                Type5 = new List<GetAllStarOfAccordSubImageDetailsByAccordIdResult>();

            }

            GetAllStarOfAccordSubImageDetailsByAccordIdResult objBo = new GetAllStarOfAccordSubImageDetailsByAccordIdResult();
            if (!ValidateSubType5(ref objBo))
            {
                List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> lstData = Type5;
                if (lstData.Count()<=0)
                {
                    lstData = new List<GetAllStarOfAccordSubImageDetailsByAccordIdResult>();
                    lstData.Add(objBo);
                }
                else
                {
                    int rowCount = lstData.Count();
                    if (hfType5Command.Value != "0")
                    {
                        var checkForUPdate = lstData.ElementAt((int)objBo.Id);
                        if (checkForUPdate != null)
                        {
                            lstData.Remove(checkForUPdate);
                            lstData.Insert((int)objBo.Id, objBo);
                        }
                    }
                    else
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

            List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> lstData = Type5;
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
        
        protected void ibtn_Type5Edit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> lstData = Type5;
            var checkForUPdate = lstData.ElementAt(rowindex);
            if (checkForUPdate != null)
            {
                hfType5Command.Value = "1";
                hfRowId.Value = rowindex.ToString();
                txtSubName.Text = checkForUPdate.Name;
                txtDescription.Text = checkForUPdate.Description;
                hfType5PopUpImage.Value = checkForUPdate.ImageName;
                chkIsVisible.Checked = checkForUPdate.IsVisible==null ? false: (bool)checkForUPdate.IsVisible;
            }
            else
            {
                hfRowId.Value = rowindex.ToString();
                txtSubName.Text = "";
                hfType5Command.Value = "0";
                txtDescription.Text = "";
                hfType5PopUpImage.Value ="";
                chkIsVisible.Checked = false;
            }
        }

        #endregion

        #region Page Functions

        public bool ValidateSubType5(ref GetAllStarOfAccordSubImageDetailsByAccordIdResult objBO)
        {
            bool isError = false;
            long lgSequanceNO = 0;

            if (!long.TryParse(hfSequanceId.Value, out lgSequanceNO))
            {
                if (lgSequanceNO <= 0)
                {
                    lgSequanceNO = gvSubDetails.Rows.Count + 1;
                }
            }
            objBO.AccordId = lgSequanceNO;


            if (string.IsNullOrWhiteSpace(hfRowId.Value))
            {
                objBO.Id = Type5.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfRowId.Value);
            }

            if (string.IsNullOrWhiteSpace(txtSubName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Name", PopupMessageType.error);
                return true;
            }
            else
            {
                objBO.Name = txtSubName.Text;
            }

            if (string.IsNullOrWhiteSpace(txtSubName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Name", PopupMessageType.error);
                return true;
            }
            else
            {
                objBO.Description = (txtDescription.Text);
            }

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

            objBO.IsVisible = chkIsVisible.Checked;

            return isError;
        }

        public void BindType5Form()
        {
            long lgSequanceNO = 0;
            if (!long.TryParse(hfSequanceId.Value, out lgSequanceNO))
            {
                if (lgSequanceNO <= 0)
                {
                    lgSequanceNO = gvSubDetails.Rows.Count + 1;
                }
            }
            if (Type5.Count() > 0)
            {
                gvType5.DataSource = Type5;
                var list = Type5.Where(x => x.AccordId == lgSequanceNO && x.LanguageId == Convert.ToInt32(ddlLanguage.SelectedValue)).ToList();
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
            txtDescription.Text = "";
            hfType5Command.Value = "0";
            txtSubName.Text = "";
            chkIsVisible.Checked = false;
        }

        #endregion

        #endregion

    }
}