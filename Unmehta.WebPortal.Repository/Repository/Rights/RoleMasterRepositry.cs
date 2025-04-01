using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface.Rights;

namespace Unmehta.WebPortal.Repository.Repository.Rights
{
    public class RoleMasterRepositry : IRoleMasterRepositry
    {

        private string SqlConnectionSTring;
        public RoleMasterRepositry(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public void Dispose()
        {
        }

        public List<GetAllRoleMasterResult> GetAllRoleMaster()
        {
            using (RoleMasterDataContext db = new RoleMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllRoleMaster().ToList();
            }
        }

        public List<GetAllRoleMasterActiveResult> GetAllRoleMasterActiveList()
        {
            using (RoleMasterDataContext db = new RoleMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllRoleMasterActive().ToList();
            }
        }

        public bool InsertOrUpdateRoleMaster(RoleMasterModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (RoleMasterDataContext db = new RoleMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateRoleMaster(objData.Id, objData.Rolename, objData.IsActive, objData.IPAddress,"");
                    if(objData.Id==0)
                    {

                        strError = "Record Saved Successfully";
                    }
                    else
                    {

                        strError = "Record updated Successfully";
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

        public bool RemoveRoleMaster(long lgId, string userName, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (RoleMasterDataContext db = new RoleMasterDataContext(SqlConnectionSTring))
                {
                    var data = db.RemoveRoleMaster(lgId,"", userName);
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
    }
}
