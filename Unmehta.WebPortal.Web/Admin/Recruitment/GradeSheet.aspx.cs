using System;
using System.Data;
using BAL;

using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Unmehta.WebPortal.Web.Admin.Recruitment
{
    public partial class GradeSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropdown();
            }
        }
        private void FillDropdown()
        {
            using (IRecruitmentAdvertisementCodeMasterRepository objEducationQualificationRepository = new RecruitmentAdvertisementCodeMasterRepository(Functions.strSqlConnectionString))
            {
                ddlAdvertisementCode.DataSource = objEducationQualificationRepository.GetAllRecruitmentAdvertisementCodeMaster();
                ddlAdvertisementCode.DataTextField = "AdvertisementCode";
                ddlAdvertisementCode.DataValueField = "Id";
                ddlAdvertisementCode.DataBind();
                ddlAdvertisementCode.Items.Insert(0, "Select");
            }
            CareerMasterBAL objBAL = new CareerMasterBAL();
            //int JodId = 0;
            DataSet ds = new DataSet();
            ds = objBAL.Candidate_GetJobApplication();
            ddlRecruitmentType.DataSource = ds;
            ddlRecruitmentType.DataTextField = "AdvertisementName";
            ddlRecruitmentType.DataValueField = "Id";
            ddlRecruitmentType.DataBind();
            ddlRecruitmentType.Items.Insert(0, "Select");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {

            ReportDocument crystalReport = new ReportDocument();
            CareerMasterBAL ObjBAL = new CareerMasterBAL();
            DataSet ds = ObjBAL.GetGradeSheetsDetails();

            DataTable dtdetails = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dtdetails = ds.Tables[0];
                var dvView = dtdetails.DefaultView;
                if (ddlRecruitmentType.SelectedIndex > 0)
                {
                    dvView.RowFilter = "jobid =" + Convert.ToInt32(ddlRecruitmentType.SelectedValue.ToString());
                }
                if (ddlAdvertisementCode.SelectedIndex > 0)
                {
                    dvView.RowFilter = "AdvertisementCode =" + Convert.ToInt32(ddlAdvertisementCode.SelectedValue.ToString());
                }
                dtdetails = dvView.ToTable();
            }
            if (dtdetails.Rows.Count > 0)
            {
                //String str =( @"../Recruitment/Reports/GradeSheet.rpt");
                string path = ResolveUrl("~/Recruitment/Reports/GradeSheet.rpt");
                crystalReport.Load(Server.MapPath(path));
                crystalReport.SetDataSource(dtdetails);
                ExportFormatType formatType = ExportFormatType.PortableDocFormat;
                
                crystalReport.ExportToHttpResponse(formatType, Response, true, "TEst123");
            }
        }









    }
}