using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository.Recrutment;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class RecruitmentAdvertisementMasterDetailsRepository : IRecruitmentAdvertisementMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentAdvertisementMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<RecruitmentAdvertisementMasterDetailsGridModel> GetAllRecruitmentAdvertisementMasterDetails()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                RecruitmentEducationTypeMasterRepository objRecruitmentEducationTypeMasterRepository = new RecruitmentEducationTypeMasterRepository(SqlConnectionSTring);
                RecruitmentEducationRepository objRecruitmentEducationRepository = new RecruitmentEducationRepository(SqlConnectionSTring);
                return db.GetAllRecruitmentAdvertisementMasterDetails().Select(x=> new RecruitmentAdvertisementMasterDetailsGridModel
                {
                    AdvertisementId = x.AdvertisementId,
                    QualificationId = x.QualificationId,
                    QualificationName = objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationDetailName,
                    EducationTypeId = objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationType,
                    EducationTypeName = (objRecruitmentEducationTypeMasterRepository.GetAllEducationTypeMaster().Where(y => y.Id == objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationType).FirstOrDefault().TypeName)
                }).ToList();
            }
        }
        
        public List<RecruitmentAdvertisementMasterDetailsGridModel> GetAllRecruitmentAdvertisementMasterDetailsByAddId(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                RecruitmentEducationTypeMasterRepository objRecruitmentEducationTypeMasterRepository = new RecruitmentEducationTypeMasterRepository(SqlConnectionSTring);
                RecruitmentEducationRepository objRecruitmentEducationRepository = new RecruitmentEducationRepository(SqlConnectionSTring);
                return db.GetRecruitmentAdvertisementMasterDetailsByAddId(lgId).Select(x => new RecruitmentAdvertisementMasterDetailsGridModel
                {
                    AdvertisementId = x.AdvertisementId,
                    QualificationId = x.QualificationId,
                    QualificationName= objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationDetailName,
                    EducationTypeId = objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationType,
                    EducationTypeName= (objRecruitmentEducationTypeMasterRepository.GetAllEducationTypeMaster().Where(y=> y.Id == objRecruitmentEducationRepository.GetTblRecruitmentEducationById((long)x.QualificationId).EducationType).FirstOrDefault().TypeName)
                }).ToList();
            }
        }

        public List<RecruitmentAdvertisementDetailGridModel> GetAllRecruitmentAdvertisementMasterDetailsByAddIdWithName(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetRecruitmentAdvertisementMasterDetailsByAddId(lgId).Select(x => new RecruitmentAdvertisementDetailGridModel
                {
                    Id = x.Id,
                    EducationName = db.GetAllRecruitmentEducationMaster().ToList().Where(y => y.Id == x.QualificationId).FirstOrDefault().EducationDetailName
                }).ToList();
            }
        }


        public bool InsertRecruitmentAdvertisementMasterDetails(RecruitmentAdvertisementMasterDetailsGridModel objData,out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertRecruitmentAdvertisementMasterDetails(objData.AdvertisementId, objData.QualificationId);
                   
                    isError = false;
                }
            }
            catch(Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveRecruitmentAdvertisementMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentAdvertisementMasterDetails(lgId);
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
            //throw new NotImplementedException();
        }
    }
}
