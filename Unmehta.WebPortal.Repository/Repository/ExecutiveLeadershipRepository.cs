using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
namespace Unmehta.WebPortal.Repository.Repository
{
  public  class ExecutiveLeadershipRepository : IExecutiveLeadershipRepository
    {
        private string SqlConnectionSTring;
        public ExecutiveLeadershipRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<ExecutiveLeadershipMasterGridModel> GetAllTblExecutiveLeadership()
        {
            using (ExecutiveLeadershipMasterDataContext db = new ExecutiveLeadershipMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllExecutiveLeadershipMaster().Select(x => new ExecutiveLeadershipMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    Name = x.Name,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    PhotoName = x.PhotoName,
                    PhotoPath = x.PhotoPath,
                    Message = x.Message,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public ExecutiveLeadershipMasterGridModel GetTblExecutiveLeadershipById(int lgId)
        {
            using (ExecutiveLeadershipMasterDataContext db = new ExecutiveLeadershipMasterDataContext(SqlConnectionSTring))
            {
                return db.GetExecutiveLeadershipMasterById(lgId).Select(x => new ExecutiveLeadershipMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    Name = x.Name,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    PhotoName = x.PhotoName,
                    PhotoPath = x.PhotoPath,
                    Message = x.Message,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblExecutiveLeadership(ExecutiveLeadershipMasterGridModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (ExecutiveLeadershipMasterDataContext db = new ExecutiveLeadershipMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateExecutiveLeadershipMaster(objData.Id, objData.LanguageId, objData.Name, objData.DesignationId,objData.PhotoName,objData.PhotoPath,objData.Message, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblExecutiveLeadership(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ExecutiveLeadershipMasterDataContext db = new ExecutiveLeadershipMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveExecutiveLeadershipMaster(lgId, SessionWrapper.UserDetails.UserName);
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
