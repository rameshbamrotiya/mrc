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
    public class DistrictRepository : IDistrictRepository
    {
        private string SqlConnectionSTring;
        public DistrictRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public bool InsertDistrictMasterDetails(DistrictModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDistrictDetails(objData.Id, objData.StateId, objData.DistrictName, SessionWrapper.UserDetails.UserName, SessionWrapper.UserDetails.UserName, false, "");
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
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public List<DistrictModel> GetAllState()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllState().Select(x => new DistrictModel
                {
                    StateId = x.RecId,
                    StateName = x.Statename,
                }).ToList();
            }
        }
        public List<DistrictModel> GetAllDistrictDetailsByAddIdWithName(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetDistrictDetailsByAddId(lgId).Select(x => new DistrictModel
                {
                    Id = x.Id,
                    DistrictName = x.DistrictName,
                    StateId = x.StateId
                }).ToList();
            }
        }
        public List<DistrictModel> GetAllTblDistrict()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDistrict().Select(x => new DistrictModel
                {
                    Id = x.Id,
                    DistrictName = x.DistrictName,
                    StateName = x.Statename,
                    StateId = x.StateId,
                }).ToList();
            }
        }
        public bool RemoveTblRecruitmentDistrict(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecruitmentDistrict(lgId, SessionWrapper.UserDetails.UserName);
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
