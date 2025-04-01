using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class DepartmentTabMaster : System.Web.UI.Page
    {
        public static long lgOurExId, lgDepartmentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                long id = 0;
                string[] splitString = queryString.Split('|');
                if (splitString.Length > 1)
                {
                    if (!string.IsNullOrWhiteSpace(splitString[0]) && long.TryParse(splitString[0], out lgOurExId) && !string.IsNullOrWhiteSpace(splitString[1]) && long.TryParse(splitString[1], out lgDepartmentId))
                    {
                        //using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                        {
                            //var dataTabList = objDepartmentTabRepository.GetAllDepartmentTabList(id, 1);

                            BindGridView();

                            ShowHideControl(VisibityType.GridView);
                            DataSet ds = new DataSet();
                            LanguageMasterBAL objBAL = new LanguageMasterBAL();
                            ds = objBAL.FillLanguage();
                            DataTable dt = ds.Tables[0];
                            Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                            ddlLanguage.SelectedIndex = 1;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Admin/Hospital/OurExcellence.aspx",false);
                }

            }
        }

        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();
                    ddlLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    ddlLanguage.Enabled = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues();

                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtTabName.Text = "";
            ddlLanguage.SelectedIndex = 1;
            ddlLanguage.Enabled = false;
            txtSquanceNo.Text = "";
            txtSwapSquanceNo.Text = "";
            drpChangeSequanceMethod.SelectedIndex = 0;
            chkEnable.Checked = false;
            BindGridView();
        }

        private void BindGridView(string strSearch = "",long hfId=0)
        {

            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                var dataTypeList = objDepartmentTabRepository.GetAllDepartmentTabType();

                var dataTabList = objDepartmentTabRepository.GetAllDepartmentTabList(lgOurExId, 1);
                if (dataTabList != null)
                {
                    if (!string.IsNullOrWhiteSpace(strSearch))
                    {
                        var dataList = dataTabList.Where(x => x.TabName.Contains(strSearch)).ToList();

                        dataTabList.Clear();
                        dataTabList.AddRange(dataList);
                    }

                    grdUser.DataSource = dataTabList;
                    grdUser.DataBind();

                    ddlTabType.DataSource = dataTypeList.Where(x=> x.TabType== "SubTabs").ToList();
                    ddlTabType.DataValueField = "Id";
                    ddlTabType.DataTextField = "TabType";
                    ddlTabType.DataBind();
                    ddlTabType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
                    ddlTabType.SelectedIndex = 0;

                    List<DepartmentTabGridModel> lstSubData = new List<DepartmentTabGridModel>();
                    ddlTabMaster.Items.Clear();
                    if (hfId <= 0)
                    {
                        lstSubData.AddRange(dataTabList.Where(x => x.TabTypeId != 0).ToList());
                    }
                    else
                    {
                        lstSubData.AddRange(dataTabList.Where(x => x.Id != hfId && x.ParentTabId == 0 && x.TabTypeId != 0).ToList());
                    }
                    if(lstSubData.Count()>0)
                    {
                        ddlTabMaster.DataSource = lstSubData;
                        ddlTabMaster.DataValueField = "Id";
                        ddlTabMaster.DataTextField = "TabName";
                        ddlTabMaster.DataBind();
                    }
                    ddlTabMaster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));

                    ddlTabMaster.SelectedIndex = 0;
                }
            }

        }

        private bool LoadControlsAdd(ref DepartmentTabGridModel objBo)
        {
            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }

            if (ddlLanguage.SelectedIndex > 0)
            {
                objBo.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
                return false;
            }


            objBo.OurExcId = lgOurExId;
            objBo.DepartmentId = lgDepartmentId;

            if (!string.IsNullOrEmpty(ddlTabType.SelectedValue))
            {
                objBo.TabTypeId = Convert.ToInt64(ddlTabType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlTabMaster.SelectedValue))
            {
                using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                {
                    long selectedValue = Convert.ToInt64(ddlTabMaster.SelectedValue);
                    long parentId = Convert.ToInt32(ddlTabMaster.SelectedValue);
                    var dataTabList = objDepartmentTabRepository.GetAllDepartmentTabList(lgOurExId, (long)objBo.LanguageId);
                    List<DepartmentTabGridModel> lstSubData = new List<DepartmentTabGridModel>();
                    if (objBo.Id <= 0)
                    {
                        lstSubData.AddRange(dataTabList.Where(x => x.ParentTabId == 0 && x.TabTypeId != 0).ToList());
                    }
                    else
                    {
                        long id = objBo.Id;
                        lstSubData.AddRange(dataTabList.Where(x => x.Id != id && x.TabTypeId != 0).ToList());
                    }
                    if (lstSubData.Count() > 0)
                    {
                        var dataModel = lstSubData.Where(x => x.Id == selectedValue).FirstOrDefault();
                        if(dataModel!=null)
                        {
                            if(objBo.Id == dataModel.ParentTabId && objBo.Id>0)
                            {
                                Functions.MessagePopup(this, "Please Select Proper Tab", PopupMessageType.error);
                                return false;
                            }
                        }
                    }
                    objBo.ParentTabId = selectedValue;
                }
            }



            if (string.IsNullOrWhiteSpace(txtTabName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Tab Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.TabName = txtTabName.Text.Trim();
            }

            objBo.IsVisable = chkEnable.Checked;

            //if (ddlDesignationList.SelectedIndex > 0)
            //{
            //    objBo.DecId = Convert.ToInt32(ddlDesignationList.SelectedValue);
            //}
            //else
            //{
            //    Functions.MessagePopup(this, "Please Select Language", PopupMessageType.error);
            //    return false;
            //}

            if (!string.IsNullOrEmpty(txtSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtSquanceNo.Text) > 0)
                {
                    using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                    {
                        if (objDepartmentTabRepository.GetAllDepartmentTabList(lgOurExId, Convert.ToInt32(ddlLanguage.SelectedValue)).Where(x => x.SequanceNo == Convert.ToInt32(txtSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequence No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBo.SequanceNo = Convert.ToInt32(txtSquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtSwapSquanceNo.Text.Trim()))
                        {
                            objBo.SwapType = drpChangeSequanceMethod.SelectedValue;
                            objBo.SwapFromSequanceNo = (long)objBo.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBo.SwapToSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());

                            if (objBo.SwapFromSequanceNo == objBo.SwapToSequanceNo)
                            {
                                Functions.MessagePopup(this, "This Swap From And To Sequence No is Same", PopupMessageType.error);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Sequence No Must Be more then 0.", PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sequence No", PopupMessageType.error);
                return false;
            }

            //objBo.ChkMode = 0;

            return true;

        }

        #region Page Event

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                DepartmentTabGridModel objBo = new DepartmentTabGridModel();

                if (LoadControlsAdd(ref objBo))
                {
                    using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                    {
                        if (!objDepartmentTabRepository.InsertOrUpdateDepartmentTabMaster(objBo, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                DepartmentTabGridModel objBo = new DepartmentTabGridModel();

                if (LoadControlsAdd(ref objBo))
                {
                    using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                    {
                        if (!objDepartmentTabRepository.InsertOrUpdateDepartmentTabMaster(objBo, out errorMessage))
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                    }
                    ClearControlValues();
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView(txtSearch.Text);
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();
            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                var data = objDepartmentTabRepository.GetAllDepartmentTabById(Convert.ToInt32(hfID.Value), lgOurExId, Convert.ToInt32(ddlLanguage.SelectedValue));
                if (data != null)
                {
                    txtTabName.Text = data.TabName;
                    if (data.ParentTabId != 0)
                    {
                        {
                            var dataTypeList = objDepartmentTabRepository.GetAllDepartmentTabType();

                            List<DepartmentTabGridModel> lstSubData = new List<DepartmentTabGridModel>();
                            var dataTabList = objDepartmentTabRepository.GetAllDepartmentTabList(lgOurExId, Convert.ToInt32(ddlLanguage.SelectedValue));
                            ddlTabMaster.Items.Clear();
                            if (data.Id <= 0)
                            {
                                lstSubData.AddRange(dataTabList.Where(x => x.ParentTabId == 0 && x.TabTypeId != 0).ToList());
                            }
                            else
                            {
                                long id = data.Id;
                                lstSubData.AddRange(dataTabList.Where(x => x.Id != id && x.ParentTabId == 0  && x.TabTypeId != 0).ToList());
                            }
                            if (lstSubData.Count() > 0)
                            {
                                ddlTabMaster.DataSource = lstSubData;
                                ddlTabMaster.DataValueField = "Id";
                                ddlTabMaster.DataTextField = "TabName";
                                ddlTabMaster.DataBind();
                            }
                            ddlTabMaster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
                        }
                        ddlTabMaster.SelectedValue = data.ParentTabId.ToString();
                    }
                    else
                    {
                        List<DepartmentTabGridModel> lstSubData = new List<DepartmentTabGridModel>();
                        var dataTabList = objDepartmentTabRepository.GetAllDepartmentTabList(lgOurExId, Convert.ToInt32(ddlLanguage.SelectedValue));
                        ddlTabMaster.Items.Clear();

                        long id = data.Id;

                        lstSubData.AddRange(dataTabList.Where(x => x.Id != id && x.ParentTabId == 0 && x.TabTypeId != 0).ToList());

                        ddlTabMaster.DataSource = lstSubData;
                        ddlTabMaster.DataValueField = "Id";
                        ddlTabMaster.DataTextField = "TabName";
                        ddlTabMaster.DataBind();
                        ddlTabMaster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
                    }
                    if (data.TabTypeId != 0 && data.TabTypeId != null)
                    {
                        ddlTabType.SelectedValue = data.TabTypeId.ToString();
                    }
                    txtSquanceNo.Text = data.SequanceNo.ToString();

                    //BindGridView(txtSearch.Text, Convert.ToInt32(hfID.Value));

                    chkEnable.Checked = (bool)data.IsVisable;
                }
                else
                {
                    txtTabName.Text = "";
                    ddlTabMaster.SelectedValue = "0";
                    ddlTabType.SelectedValue = "0";
                    txtSquanceNo.Text = "";
                    chkEnable.Checked = false;
                }

            }
            ShowHideControl(VisibityType.Edit);
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
                {

                    if (objDepartmentTabRepository.GetAllDeparmentTabDetailListByTabId(lgOurExId,1).Count(x=> x.ParentTabId!=rowId)>0)
                    {
                        Functions.MessagePopup(this, "This Record have sub value so not able to remove.", PopupMessageType.error);
                        return;
                    }

                    objDepartmentTabRepository.RemoveDepartmentTabById(rowId, out errorMessage);
                    ClearControlValues();
                    BindGridView();
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                var data = objDepartmentTabRepository.GetAllDepartmentTabById(Convert.ToInt32(hfID.Value), lgOurExId, Convert.ToInt32(ddlLanguage.SelectedValue));
                if (data != null)
                {
                    txtTabName.Text = data.TabName;
                    ddlTabMaster.SelectedValue = data.ParentTabId.ToString();
                    txtSquanceNo.Text = data.SequanceNo.ToString();
                    chkEnable.Checked = (bool)data.IsVisable;
                }
                else
                {
                    txtTabName.Text = "";
                    ddlTabMaster.SelectedValue = "0";
                    txtSquanceNo.Text = "";
                    chkEnable.Checked = false;
                }
            }
        }

        #endregion


        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView();
            grdUser.PageIndex = e.NewPageIndex;
            grdUser.DataBind();

        }
        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {

            ShowHideControl(VisibityType.Insert);
        }
    }
}