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
    public class RecruitmentCastRepository : IRecruitmentCastRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentCastRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<RecruitmentCastGridModel> GetAllTblRecruitmentCast()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllRecruitmentCastMaster().Select(x => new RecruitmentCastGridModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public RecruitmentCastGridModel GetTblRecruitmentCastById(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetRecruitmentCastMasterById(lgId).Select(x => new RecruitmentCastGridModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblRecruitmentCast(RecruitmentCastGridModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateRecruitmentCastMaster(objData.Id, objData.Name, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblRecruitmentCast(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentCastMaster(lgId, SessionWrapper.UserDetails.UserName);
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
