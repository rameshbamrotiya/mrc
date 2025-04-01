using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Faculty;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class OurExcellenceFacilitiesDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login");
            }
            if (!IsPostBack)
            {
                string strQuery = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strQuery))
                {
                    Response.Redirect("~/Admin/Hospital/OurExcellence.aspx");
                }
                else
                {
                    hfId.Value = strQuery;
                    using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
                    {
                        var dataDetails = objOurExcellenceMasterRepository.GetTblOurExcellenceMasterInformationById(Convert.ToInt32(hfId.Value), 1);
                    }
                    ClearFacility();
                }
            }
        }

        

        #region Facility
        private void BindFacilityDetails()
        {
            using (IOurExcellenceMasterFacilityRepositry objOurExcellenceMasterFacilityRepositry = new OurExcellenceMasterFacilityRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterFacilityRepositry.GetAllOurExcellenceMasterFacility(Convert.ToInt32(hfId.Value)).OrderBy(x => x.SequenceNo).ToList();
                if (data != null)
                {
                    gvFacility.DataSource = data;
                    gvFacility.DataBind();
                }
            }

            using (IFacilitesMasterRepository objStatisticsChartRepository = new FacilitesMasterRepository(Functions.strSqlConnectionString))
            {
                var data = objStatisticsChartRepository.GetAllFacilitesMaster(1);
                if (data != null)
                {
                    ddlFacility.DataSource = data;
                    ddlFacility.DataValueField = "Id";
                    ddlFacility.DataTextField = "FacilitesName";
                    ddlFacility.DataBind();
                }
                ddlFacility.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
            }
        }

        private bool LoadFacilityControlsAdd(OurExcellenceMasterFacilityModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfFacilityId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfFacilityId.Value);
            }
            objBo.OurExcId = Convert.ToInt32(hfId.Value);
            if (ddlFacility.SelectedIndex > 0)
            {
                objBo.FacilityId = Convert.ToInt32(ddlFacility.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Facility", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtFacilitySquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtFacilitySquanceNo.Text) > 0)
                {
                    using (IOurExcellenceMasterFacilityRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterFacilityRepositry(Functions.strSqlConnectionString))
                    {
                        if (objOurExcellenceMasterStatisticsRepositry.GetAllOurExcellenceMasterFacility(objBo.OurExcId).Where(x => x.SequenceNo == Convert.ToInt32(txtFacilitySquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequance No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBo.SequanceNo = Convert.ToInt32(txtFacilitySquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtFacilitySwapSquanceNo.Text.Trim()))
                        {
                            objBo.SwapType = drpChangeFacilitySequanceMethod.SelectedValue;
                            objBo.SwapFromSequanceNo = objBo.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBo.SwapToSequanceNo = Convert.ToInt32(txtFacilitySwapSquanceNo.Text.Trim());


                            if (objBo.SwapFromSequanceNo == objBo.SwapToSequanceNo)
                            {
                                Functions.MessagePopup(this, "This Swap From And To Sequance No is Same", PopupMessageType.error);
                                return false;
                            }
                        }


                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please Sequance No Must Be more then 0.", PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sequance No", PopupMessageType.error);
                return false;
            }
            return true;
        }

        private void ClearFacility()
        {
            hfFacilityId.Value = "0";

            BindFacilityDetails();

            txtFacilitySquanceNo.Text = "";
            txtFacilitySwapSquanceNo.Text = "";
            ddlFacility.SelectedIndex = 0;

            dvSwapFacilitySequance.Visible = false;
            dvDrpFacilitySwapSequance.Visible = false;
            rfvFacilitySwapSequanceNo.Enabled = false;
            rgVFacilitySwapSequanseNo.Enabled = false;
        }

        protected void btnFacilitySave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IOurExcellenceMasterFacilityRepositry objOurExcellenceMasterFacilityRepositry = new OurExcellenceMasterFacilityRepositry(Functions.strSqlConnectionString))
            {
                OurExcellenceMasterFacilityModel objBO = new OurExcellenceMasterFacilityModel();
                if (LoadFacilityControlsAdd(objBO))
                {
                    if (!objOurExcellenceMasterFacilityRepositry.InsertOrUpdateOurExcellenceMasterFacility(objBO, out errorMessage))
                    {
                        ClearFacility();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void btnFacilityClear_ServerClick(object sender, EventArgs e)
        {
            ClearFacility();
        }

        protected void ibtn_FacilityEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvFacility.DataKeys[rowindex]["Id"].ToString());
            using (IOurExcellenceMasterFacilityRepositry objOurExcellenceMasterFacilityRepositry = new OurExcellenceMasterFacilityRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterFacilityRepositry.GetOurExcellenceMasterFacility(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfFacilityId.Value = data.Id.ToString();
                    ddlFacility.SelectedValue = data.FacilityId.ToString();
                    txtFacilitySquanceNo.Text = data.SequenceNo.ToString();

                    dvSwapFacilitySequance.Visible = true;
                    dvDrpFacilitySwapSequance.Visible = true;
                    rfvFacilitySwapSequanceNo.Enabled = true;
                    rgVFacilitySwapSequanseNo.Enabled = true;

                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_FacilityDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvFacility.DataKeys[rowindex]["Id"].ToString());
                using (IOurExcellenceMasterFacilityRepositry objOurExcellenceMasterFacilityRepositry = new OurExcellenceMasterFacilityRepositry(Functions.strSqlConnectionString))
                {
                    if (!objOurExcellenceMasterFacilityRepositry.RemoveOurExcellenceMasterFacility(rowId, out errorMessage))
                    {
                        ClearFacility();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
        #endregion
        
    }
}