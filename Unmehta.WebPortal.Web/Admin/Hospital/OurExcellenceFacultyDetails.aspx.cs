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
    public partial class OurExcellenceFacultyDetails : System.Web.UI.Page
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
                    ClearFaculty();
                }
            }
        }
        
        #region Faculty
        private void BindFacultyDetails()
        {
            using (IOurExcellenceMasterFacultyRepositry objOurExcellenceMasterFacultyRepositry = new OurExcellenceMasterFacultyRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterFacultyRepositry.GetAllOurExcellenceMasterFaculty(Convert.ToInt32(hfId.Value)).OrderBy(x => x.SequenceNo).ToList();
                if (data != null)
                {
                    gvFaculty.DataSource = data;
                    gvFaculty.DataBind();
                }
            }

            using (IFacultyRepository objStatisticsChartRepository = new FacultyRepository(Functions.strSqlConnectionString))
            {
                var data = objStatisticsChartRepository.GetAllTblFaculty(1);
                if (data != null)
                {
                    ddlFaculty.DataSource = data;
                    ddlFaculty.DataValueField = "Id";
                    ddlFaculty.DataTextField = "FacultyName";
                    ddlFaculty.DataBind();
                }
                ddlFaculty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
            }
        }

        private bool LoadFacultyControlsAdd(OurExcellenceMasterFacultyModel objBo)
        {
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfFacultyId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfFacultyId.Value);
            }
            objBo.OurExcId = Convert.ToInt32(hfId.Value);
            if (ddlFaculty.SelectedIndex > 0)
            {
                objBo.FacultyId = Convert.ToInt32(ddlFaculty.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Select Faculty", PopupMessageType.error);
                return false;
            }

            if (!string.IsNullOrEmpty(txtFacultySquanceNo.Text.Trim()))
            {
                if (Convert.ToInt32(txtFacultySquanceNo.Text) > 0)
                {
                    using (IOurExcellenceMasterFacultyRepositry objOurExcellenceMasterFacultyRepositry = new OurExcellenceMasterFacultyRepositry(Functions.strSqlConnectionString))
                    {
                        if (objOurExcellenceMasterFacultyRepositry.GetAllOurExcellenceMasterFaculty(objBo.OurExcId).Where(x => x.SequenceNo == Convert.ToInt32(txtFacultySquanceNo.Text.Trim())).Count() > 0 && objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "This Sequance No Already Assign", PopupMessageType.error);
                            return false;
                        }
                        objBo.SequanceNo = Convert.ToInt32(txtFacultySquanceNo.Text.Trim());
                        if (!string.IsNullOrEmpty(txtFacultySwapSquanceNo.Text.Trim()))
                        {
                            objBo.SwapType = drpChangeFacultySequanceMethod.SelectedValue;
                            objBo.SwapFromSequanceNo = objBo.SequanceNo;
                            //objBo.SwapFromSequanceNo = Convert.ToInt32(txtSwapSquanceNo.Text.Trim());
                            //objBo.SwapToSequanceNo = objBo.SequanceNo ;
                            objBo.SwapToSequanceNo = Convert.ToInt32(txtFacultySwapSquanceNo.Text.Trim());


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

        private void ClearFaculty()
        {
            hfFacultyId.Value = "0";

            BindFacultyDetails();

            txtFacultySquanceNo.Text = "";
            txtFacultySwapSquanceNo.Text = "";
            ddlFaculty.SelectedIndex = 0;

            dvSwapFacultySequance.Visible = false;
            dvDrpFacultySwapSequance.Visible = false;
            rfvFacultySwapSequanceNo.Enabled = false;
            rgVFacultySwapSequanseNo.Enabled = false;

        }

        protected void btnFacultySave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (IOurExcellenceMasterFacultyRepositry objOurExcellenceMasterFacultyRepositry = new OurExcellenceMasterFacultyRepositry(Functions.strSqlConnectionString))
            {
                OurExcellenceMasterFacultyModel objBO = new OurExcellenceMasterFacultyModel();
                if (LoadFacultyControlsAdd(objBO))
                {
                    if (!objOurExcellenceMasterFacultyRepositry.InsertOrUpdateOurExcellenceMasterFaculty(objBO, out errorMessage))
                    {
                        ClearFaculty();
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
            }
        }

        protected void btnFacultyClear_ServerClick(object sender, EventArgs e)
        {
            ClearFaculty();
        }

        protected void ibtn_FacultyEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            int rowId = Convert.ToInt32(gvFaculty.DataKeys[rowindex]["Id"].ToString());
            using (IOurExcellenceMasterFacultyRepositry objOurExcellenceMasterFacultyRepositry = new OurExcellenceMasterFacultyRepositry(Functions.strSqlConnectionString))
            {
                var data = objOurExcellenceMasterFacultyRepositry.GetOurExcellenceMasterFaculty(Convert.ToInt32(hfId.Value), rowId);
                if (data != null)
                {
                    hfFacultyId.Value = data.Id.ToString();
                    ddlFaculty.SelectedValue = data.FacultyId.ToString();
                    txtFacultySquanceNo.Text = data.SequenceNo.ToString();

                    dvSwapFacultySequance.Visible = true;
                    dvDrpFacultySwapSequance.Visible = true;
                    rfvFacultySwapSequanceNo.Enabled = true;
                    rgVFacultySwapSequanseNo.Enabled = true;

                }
                else
                {
                    Functions.MessagePopup(this, "Some Issue Occure", PopupMessageType.error);
                }
            }
        }

        protected void ibtn_FacultyDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(gvFaculty.DataKeys[rowindex]["Id"].ToString());
                using (IOurExcellenceMasterFacultyRepositry objOurExcellenceMasterFacultyRepositry = new OurExcellenceMasterFacultyRepositry(Functions.strSqlConnectionString))
                {
                    if (!objOurExcellenceMasterFacultyRepositry.RemoveOurExcellenceMasterFaculty(rowId, out errorMessage))
                    {
                        ClearFaculty();
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