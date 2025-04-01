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
    public partial class OurExcellenceStatisticsDetails : System.Web.UI.Page
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
                    ClearStatistics();
                }
            }
        }

        #region Statistics
        private void BindStatisticsDetails()
        {
            using (IOurExcellenceMasterStatisticsRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterStatisticsRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterStatisticsRepositry.GetAllOurExcellenceMasterStatistics(Convert.ToInt32(hfId.Value)).OrderBy(x=> x.SequenceNo).ToList();
                if (data != null)
                {
                    gvSpecialization.DataSource = data;
                    gvSpecialization.DataBind();
                }
            }

            using (IStatisticsChartRepository objStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                var data = objStatisticsChartRepository.GetAllStatisticsChart();
                if (data != null)
                {
                    ddlStatistics.DataSource = data;
                    ddlStatistics.DataValueField = "Id";
                    ddlStatistics.DataTextField = "ChartName";
                    ddlStatistics.DataBind();
                }
                ddlStatistics.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
            }
            ddlChartType.Items.Clear();
            Array colors = Enum.GetValues(typeof(ChartType));
            ddlChartType.Items.Add(new ListItem("Select Chart Type", null));
            foreach (ChartType color in colors)
            {
                ddlChartType.Items.Add(new ListItem(color.ToString(), ((int)color).ToString()));
            }
        }

        private bool LoadStatisticsControlsAdd(OurExcellenceMasterStatisticsModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfStatisticsId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfStatisticsId.Value);
            }
                objBo.OurExcId = Convert.ToInt32(hfId.Value);
            if (ddlStatistics.SelectedIndex > 0)
            {
                objBo.StatisticsId = Convert.ToInt32(ddlStatistics.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Statistics", PopupMessageType.error);
                return false;
            }
            if (ddlChartType.SelectedIndex > 0)
            {
                objBo.StatisticsType = Convert.ToInt32(ddlChartType.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Chart Type", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtStatSquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtStatSquanceNo.Text) > 0)
                {
                    using (IOurExcellenceMasterStatisticsRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterStatisticsRepositry(Functions.strSqlConnectionString))
                    {
                        if (objOurExcellenceMasterStatisticsRepositry.GetAllOurExcellenceMasterStatistics(objBo.OurExcId).Where(x => x.SequenceNo == Convert.ToInt32(txtStatSquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequance No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBo.SequanceNo = Convert.ToInt32(txtStatSquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtStatSwapSquanceNo.Text.Trim()))
                        {
                            objBo.SwapType = drpChangeStatSequanceMethod.SelectedValue;
                            objBo.SwapFromSequanceNo = objBo.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBo.SwapToSequanceNo = Convert.ToInt32(txtStatSwapSquanceNo.Text.Trim());


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

        private void ClearStatistics()
        {
            hfStatisticsId.Value = "0";

            BindStatisticsDetails();

            txtStatSquanceNo.Text = "";
            txtStatSwapSquanceNo.Text = "";
            ddlChartType.SelectedIndex = 0;
            ddlStatistics.SelectedIndex = 0;

            dvSwapStatSequance.Visible = false;
            dvDrpStatSwapSequance.Visible = false;
            rfvSwapSequanceNo.Enabled = false;
            rgVSwapSequanseNo.Enabled = false;

        }

        protected void ibtn_StatisticsEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvSpecialization.DataKeys[rowindex]["Id"].ToString());
            using (IOurExcellenceMasterStatisticsRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterStatisticsRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterStatisticsRepositry.GetOurExcellenceMasterStatistics(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfStatisticsId.Value = data.Id.ToString();
                    ddlStatistics.SelectedValue = data.StatisticsId.ToString();
                    ddlChartType.SelectedValue = data.StatisticsType.ToString();
                    txtStatSquanceNo.Text = data.SequenceNo.ToString();

                    dvSwapStatSequance.Visible = true;
                    dvDrpStatSwapSequance.Visible = true;
                    rfvSwapSequanceNo.Enabled = true;
                    rgVSwapSequanseNo.Enabled = true;

                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_StatisticsDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvSpecialization.DataKeys[rowindex]["Id"].ToString());
                using (IOurExcellenceMasterStatisticsRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterStatisticsRepositry(Functions.strSqlConnectionString))
                {
                    if (!objOurExcellenceMasterStatisticsRepositry.RemoveOurExcellenceMasterStatics(rowId, out errorMessage))
                    {
                        ClearStatistics();
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

        protected void btnStatisticsSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IOurExcellenceMasterStatisticsRepositry objOurExcellenceMasterStatisticsRepositry = new OurExcellenceMasterStatisticsRepositry(Functions.strSqlConnectionString))
            {
                OurExcellenceMasterStatisticsModel objBO = new OurExcellenceMasterStatisticsModel();
                if (LoadStatisticsControlsAdd(objBO))
                {
                    if (!objOurExcellenceMasterStatisticsRepositry.InsertOrUpdateOurExcellenceMasterStatistics(objBO, out errorMessage))
                    {
                        ClearStatistics();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void btnStatisticsClear_ServerClick(object sender, EventArgs e)
        {
            ClearStatistics();
        }
        #endregion
        
    }
}