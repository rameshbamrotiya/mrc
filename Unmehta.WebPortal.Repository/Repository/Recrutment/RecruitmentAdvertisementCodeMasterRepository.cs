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
    public class RecruitmentAdvertisementCodeMasterRepository: IRecruitmentAdvertisementCodeMasterRepository
    {

        private string SqlConnectionSTring;
        public RecruitmentAdvertisementCodeMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public List<RecruitmentAdvertisementCodeMasterModel> GetAllRecruitmentAdvertisementCodeMaster()
        {
            using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllRecruitmentAdvertisementCodeMaster().Select(x => new RecruitmentAdvertisementCodeMasterModel
                {
                    Id = x.Id,
                    AdvertisementCode = x.AdvertisementCode,
                    EndDate=x.EndDate,
                    StartDate=x.StartDate,
                    IsActive=x.IsActive,
                    Generalinstructionfile=x.Generalinstructionfile,
                    PublishDate=x.PublishDate,
                    IsNewIcon=x.IsNewIcon,
                    AdvertisementDesc=x.AdvertisementDesc
                }).ToList();
            }
        }


        public List<GetAllPostTypeMasterDetailsResult> GetAllPostTypeMasterDetails()
        {
            using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllPostTypeMasterDetails().ToList();
            }
        }


        public List<GetAllRecruitmentTypeMasterDetailsResult> GetAllRecruitmentTypeMasterDetails()
        {
                using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
                {
                    return db.GetAllRecruitmentTypeMasterDetails().ToList();
                }            
        }

        public RecruitmentAdvertisementCodeMasterModel GetRecruitmentAdvertisementCodeMasterDetailsById(long lgId)
        {
            using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
            {
                return GetAllRecruitmentAdvertisementCodeMaster().Where(x=> x.Id== lgId).FirstOrDefault();
            }
        }
        public bool InsertOrUpdateRecruitmentAdvertisementCodeMasterDetails(RecruitmentAdvertisementCodeMasterModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateRecruitmentAdvertisementCodeMaster(objData.Id, objData.AdvertisementCode, objData.AdvertisementDesc, objData.Generalinstructionfile, objData.StartDate, objData.PublishDate,objData.EndDate, objData.IsActive, objData.IsNewIcon, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
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
        public bool RemoveTblRecruitmentRecruitmentAdvertisementCodeMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentAdvertisementCodeMasterDataContext db = new RecruitmentAdvertisementCodeMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentAdvertisementCodeMaster(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
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
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
