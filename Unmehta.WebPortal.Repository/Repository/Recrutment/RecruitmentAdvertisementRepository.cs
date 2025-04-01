using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class RecruitmentAdvertisementRepository : IRecruitmentAdvertisementRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentAdvertisementRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<RecruitmentAdvertisementGridModel> GetAllTblRecruitmentAdvertisement()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllRecruitmentAdvertisementMaster().Select(x=> new RecruitmentAdvertisementGridModel
                {
                    Id=x.Id,
                    AdvertisementName = x.AdvertisementName,
                    AdvertisementDesc=x.AdvertisementDesc,
                    AdvertisementCode=x.AdvertisementCode,
                    AgeLimitCalOn=x.AgeLimitCalOn,
                    MinExp=x.MinExperiance,
                    PostCode=x.PostCode,
                    PostType=x.PostType,
                    RecrutmentType=x.RecrutmentType,
                    AdvertisementType = x.AdvertisementType,
                    FileName =x.FileName,
                    MaxAge = x.ToAge,
                    StartDate =(x.StartDate),
                    EndDate =(x.EndDate),
                    Designation=x.Designation,
                    MaxExp=x.MaxExperiance,
                    Noopening=x.Noopening,
                    Gender=x.Gender,
                    PublishDate=x.PublishDate,
                    IsActive = x.IsActive,
                    InterviewDate=x.InterviewDate,
                    InterviewTime=x.InterviewTime,
                    ReportingTime=x.ReportingTime,
                    IsNewIcon=x.IsNewIcon
                }).ToList();
            }
        }
        
        public List<GetAllAdvertisementTypeMasterDetailsResult> GetAllAdvertisementTypeMasterDetails()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllAdvertisementTypeMasterDetails().ToList();
            }
        }
        

        public RecruitmentAdvertisementGridModel GetTblRecruitmentAdvertisementById(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return GetAllTblRecruitmentAdvertisement().Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblRecruitmentAdvertisement(RecruitmentAdvertisementGridModel objData,out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateRecruitmentAdvertisementMaster(objData.Id, objData.AdvertisementName,objData.AdvertisementDesc,objData.PostCode, objData.AdvertisementCode, objData.PostType, objData.RecrutmentType, objData.FileName,objData.AgeLimitCalOn, objData.PublishDate, objData.MinExp,objData.MaxAge,objData.StartDate,objData.EndDate,objData.AdvertisementType, objData.IsActive, SessionWrapper.UserDetails.UserName,objData.Designation,objData.Noopening,objData.MaxExp,objData.Gender,objData.InterviewDate,objData.InterviewTime,objData.ReportingTime,objData.IsNewIcon);
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
                        objData.Id = dataIsDone.FirstOrDefault().RecId;
                    }
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

        public bool RemoveTblRecruitmentAdvertisement(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentAdvertisementMaster(lgId, SessionWrapper.UserDetails.UserName);
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
