using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Recrutment;

namespace Unmehta.WebPortal.Repository.Repository.Recrutment
{
    public class RecruitmentSourceTypeMasterRepository : IRecruitmentSourceTypeMasterRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentSourceTypeMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllAdvertisementSourceMasterActiveResult> GetAllEducationTypeMaster()
        {
            using (RecruitmentAdvertisementSourceMasterDetailsDataContext db = new RecruitmentAdvertisementSourceMasterDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllAdvertisementSourceMasterActive().ToList();
            }
        }


        public List<RecruitmentAdvertisementEducationModel> GetAllRecruitmentAdvertisementByAddId(long lgId)
        {
            using (RecruitmentAdvertisementSourceMasterDetailsDataContext db = new RecruitmentAdvertisementSourceMasterDetailsDataContext(SqlConnectionSTring))
            {
                var datalist = GetAllEducationTypeMaster();
                return db.GetAllRecruitmentAdvertisementSourceMasterDetailsByAddId(lgId).Select(x => new RecruitmentAdvertisementEducationModel
                {
                    EducationTypeId = (long)x.AdvertisementSourceId,
                    EducationTypeName = datalist.Where(y => y.Id == x.AdvertisementSourceId).FirstOrDefault().AdvertisementName
                }).ToList();

            }
        }


        public bool InsertRecruitmentAdvertisementMasterDetails(long AdvertisementId, long SourceId, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentAdvertisementSourceMasterDetailsDataContext db = new RecruitmentAdvertisementSourceMasterDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertRecruitmentAdvertisementSourceMasterDetails(AdvertisementId, SourceId);
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveRecruitmentAdvertisementSourceMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentAdvertisementSourceMasterDetailsDataContext db = new RecruitmentAdvertisementSourceMasterDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentAdvertisementSourceMasterDetailsByAddId(lgId);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;

                    //var data = db.Tbl_Education_Qualifications.Where(x => x.RecId == lgId).FirstOrDefault();
                    //if (data != null)
                    //{
                    //    db.Tbl_Education_Qualifications.DeleteOnSubmit(data);
                    //}
                    //db.SubmitChanges();
                    //strError = "Record Removed Successfully";
                    //isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }


        public void Dispose()
        {

        }
    }
}
