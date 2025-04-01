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
    public class RecruitmentEducationTypeMasterRepository : IRecruitmentEducationTypeMasterRepository
    {

        private string SqlConnectionSTring;
        public RecruitmentEducationTypeMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllEducationTypeMasterResult> GetAllEducationTypeMaster()
        {
            using (RecruitmentEducationTypeMasterDataContext db = new RecruitmentEducationTypeMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllEducationTypeMaster().ToList();
            }
        }


        public List<RecruitmentAdvertisementEducationModel> GetAllRecruitmentAdvertisementByAddId(long lgId)
        {
            using (RecruitmentEducationTypeMasterDataContext db = new RecruitmentEducationTypeMasterDataContext(SqlConnectionSTring))
            {
                var datalist = GetAllEducationTypeMaster();
                    return db.GetAllRecruitmentAdvertisementEducationTypeDetailsByAddId(lgId).Select(x=> new RecruitmentAdvertisementEducationModel {
                        EducationTypeId =x.EducationTypeId ,EducationTypeName= datalist.Where(y=> y.Id==x.EducationTypeId).FirstOrDefault().TypeName
                    }).ToList();
                
            }
        }


        public bool InsertRecruitmentAdvertisementMasterDetails(long AdvertisementId,long EducationTypeId, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentEducationTypeMasterDataContext db = new RecruitmentEducationTypeMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertRecruitmentAdvertisementEducationTypeDetails(AdvertisementId,EducationTypeId);
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

        public bool RemoveRecruitmentAdvertisementEducationTypeMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentEducationTypeMasterDataContext db = new RecruitmentEducationTypeMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentAdvertisementEducationTypeDetailsByAddId(lgId);
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
