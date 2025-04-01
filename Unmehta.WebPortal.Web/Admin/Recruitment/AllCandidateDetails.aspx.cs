using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Web.Common;
using System.IO;
using System.Data;
using Unmehta.WebPortal.Data.Hospital;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class AllCandidateDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
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
                    BindGridViewData();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        private void BindGridViewData()
        {
            try
            {
                using (IRecruitmentAdvertisementRepository objCandidateDetailsRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
                {
                    var data = objCandidateDetailsRepository.GetAllTblRecruitmentAdvertisement().Where(x=> x.IsActive==true).ToList();
                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            ddlJobList.DataSource = data;
                            ddlJobList.DataTextField = "AdvertisementName";
                            ddlJobList.DataValueField = "Id";
                            ddlJobList.DataBind();
                            ddlJobList.Items.Insert(0, new ListItem("Select Job", ""));

                        }
                    }
                }
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    List<GetAllCandidateRecruitmentMasterResult> data = objCandidateDetailsRepository.GetAllCandidateRecruitmentMaster();

                    //List<GetAllCandidateRecruitmentMasterResult> FilteredData = data.Where(x => (ddlJobList.SelectedIndex > 0 && x.Advertisementid == Convert.ToInt32(ddlJobList.SelectedValue)) || (!string.IsNullOrWhiteSpace(txtSearch.Text) && (x.RegisrationId.Contains(txtSearch.Text)))).ToList() ;

                    
                    if (data != null)
                    {
                        lblCount.Text = "Total No Of Candidate : " + Convert.ToString(data.Count);
                        gView.DataSourceID = string.Empty;
                        gView.DataSource = data;
                        gView.DataBind();
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                throw ex;
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {

            string strRegistarionId = "";


            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            long Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0].ToString());

            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                var data = objCandidateDetailsRepository.GetTblCandidateDetailsById(Id);
                long strJobId = (long)data.Advertisementid;

                strRegistarionId = data.RegistrationId;


            }
        }

        protected void lnkUnlockProfile_Click(object sender, EventArgs e)
        {
            try
            {
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

                    long Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0].ToString());

                    List<GetAllCandidateRecruitmentMasterResult> data = objCandidateDetailsRepository.GetAllCandidateRecruitmentMaster().Where(x=> x.Id==Id).ToList();
                    

                    string strError;

                    ;

                    if (!objCandidateDetailsRepository.UnlockProfileForEdit(Id, (long)data.FirstOrDefault().Advertisementid, out strError))
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.success);
                        BindGridViewData();
                    }
                    else
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.error);
                    }
                }
            }
            catch (Exception ex)
            {

                Functions.MessagePopup(this, ex.ToString(), PopupMessageType.error);
            }
        }
                
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {

            try
            {
                BindGridViewData();
                txtSearch.Text = string.Empty;
                ddlJobList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                throw ex;
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                List<GetAllCandidateRecruitmentMasterResult> data = objCandidateDetailsRepository.GetAllCandidateRecruitmentMaster();

                if(ddlJobList.SelectedIndex > 0)
                {
                    data = data.Where(x => x.Advertisementid == Convert.ToInt32(ddlJobList.SelectedValue)).ToList();
                }

                if(!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    data = data.Where(x => x.RegisrationId.Contains(txtSearch.Text)).ToList();

                }
                if (data != null)
                {
                    lblCount.Text = "Total No Of Candidate : " + Convert.ToString(data.Count);
                    gView.DataSourceID = string.Empty;
                    gView.DataSource = data;
                    gView.DataBind();
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                List<GetAllCandidateRecruitmentMasterResult> data = objCandidateDetailsRepository.GetAllCandidateRecruitmentMaster();

                if (ddlJobList.SelectedIndex > 0)
                {
                    data = data.Where(x => x.Advertisementid == Convert.ToInt32(ddlJobList.SelectedValue)).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    data = data.Where(x => x.RegisrationId.Contains(txtSearch.Text)).ToList();

                }
                if (data != null)
                {

                    DataTable dt = Functions.ToDataTable(data);

                    dt.Columns.Remove("Id");
                    dt.Columns.Remove("CasteId");
                    dt.Columns.Remove("Religion");
                    dt.Columns.Remove("Advertisementid");


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Candidate List");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=DataTableToExcelExport.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

        protected void ibtn_View_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            int Id = Convert.ToInt32(gView.DataKeys[rowIndex].Values[0]);

            string path = ResolveUrl("~/Admin/Recruitment/PrintApplication?" + HttpUtility.UrlEncode(Functions.Base64Encode("CandidateId=" + Id.ToString())));
            Response.Redirect(path);
        }
    }
}