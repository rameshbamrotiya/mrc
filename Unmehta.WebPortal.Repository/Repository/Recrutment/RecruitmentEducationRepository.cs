using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class RecruitmentEducationRepository : IRecruitmentEducationRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentEducationRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<RecruitmentEducationGridModel> GetAllTblRecruitmentEducation()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllRecruitmentEducationMaster().Select(x=> new RecruitmentEducationGridModel
                {
                    Id=x.Id,
                    EducationDetailName = x.EducationDetailName,
                    EducationType = x.EducationType,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public RecruitmentEducationGridModel GetTblRecruitmentEducationById(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetRecruitmentEducationMasterById(lgId).Select(x => new RecruitmentEducationGridModel
                {
                    Id = x.Id,
                    EducationDetailName = x.EducationDetailName,
                    EducationType = x.EducationType,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblRecruitmentEducation(RecruitmentEducationGridModel objData,out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var pastList = db.GetAllRecruitmentEducationMaster().ToList();
                    if(pastList!=null)
                    {
                        if(pastList.Where(x=> x.EducationType==(int)EducationType.HSC).Count()>0 && objData.EducationType== (int)EducationType.HSC)
                        {
                            strError = StringEnum.GetStringValue(EducationType.HSC)+ " Only One Allow.";
                            isError = true;
                        }
                        else if (pastList.Where(x => x.EducationType == (int)EducationType.CBSE).Count() > 0 && objData.EducationType == (int)EducationType.CBSE)
                        {
                            strError = StringEnum.GetStringValue(EducationType.CBSE) + " Only One Allow.";
                            isError = true;
                        }
                    }
                    if (!isError)
                    {
                        var dataIsDone = db.InsertOrUpdateRecruitmentEducationMaster(objData.Id, objData.EducationDetailName, Convert.ToInt32(objData.EducationType), objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
            }
            catch(Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblRecruitmentEducation(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentEducationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
