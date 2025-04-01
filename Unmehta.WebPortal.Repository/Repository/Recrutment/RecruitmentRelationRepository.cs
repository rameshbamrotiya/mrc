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
    public class RecruitmentRelationRepository : IRecruitmentRelationRepository
    {
        private string SqlConnectionSTring;
        public RecruitmentRelationRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<RecruitmentRelationGridModel> GetAllTblRecruitmentRelation()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllRecruitmentRelationMaster().Select(x=> new RecruitmentRelationGridModel
                {
                    Id=x.Id,
                    Name=x.Name,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public RecruitmentRelationGridModel GetTblRecruitmentRelationById(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetRecruitmentRelationMasterById(lgId).Select(x => new RecruitmentRelationGridModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblRecruitmentRelation(RecruitmentRelationGridModel objData,out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateRecruitmentRelationMaster(objData.Id, objData.Name, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
            catch(Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblRecruitmentRelation(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentRelationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
