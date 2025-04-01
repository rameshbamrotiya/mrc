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
    public class UserMasterRepository : IUserMasterRepository
    {
        private string SqlConnectionSTring;
        public UserMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllUserMasterResult> GetAllUserMaster()
        {
            using (UserMasterDataContext db = new UserMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllUserMaster().ToList();
            }
        }
        
        public bool InsertUserMaster(UserMasterModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (UserMasterDataContext db = new UserMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateUserMaster(objData.Id, objData.RoleId,objData.FirstName,objData.LastName,objData.Email,objData.Username,objData.Password,objData.Phoneno,"",objData.IsActive,"");
                    if (objData.Id == 0)
                    {
                        strError = "Record Saved Successfully";
                    }
                    else
                    {
                        strError = "Record Updated Successfully";
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

        public bool RemoveUserMaster(long lgId,string userName, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (UserMasterDataContext db = new UserMasterDataContext(SqlConnectionSTring))
                {
                    var data=db.RemoveUserMaster(lgId, userName);
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
