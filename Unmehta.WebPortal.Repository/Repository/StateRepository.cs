using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
namespace Unmehta.WebPortal.Repository.Repository
{
  public  class StateRepository:IStateRepository
    {
        private string SqlConnectionSTring;
        public StateRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public bool InsertOrUpdateState(StateModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateStateMaster(objData.RecId, objData.StateName);
                    if (dataIsDone != null)
                    {
                        if (objData.RecId == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.RecId > 0)
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
        public List<StateModel> GetAllState()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllState().Select(x => new StateModel
                {
                   RecId=x.RecId,
                   StateName=x.Statename
                }).ToList();
            }
        }
        public List<StateModel> GetAllStateDetailsByAddIdWithName(long lgId)
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetStateDetailsByAddId(lgId).Select(x => new StateModel
                {
                    RecId = x.Recid,
                    StateName = x.Statename,
                }).ToList();
            }
        }
        public List<StateModel> GetAllTblState()
        {
            using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
            {
                return db.GetAllState().Select(x => new StateModel
                {
                    RecId = x.RecId,
                    StateName = x.Statename,
                }).ToList();
            }
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
