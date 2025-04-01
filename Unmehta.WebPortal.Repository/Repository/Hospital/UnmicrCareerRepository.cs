using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class UnmicrCareerRepository : IUnmicrCareerRepository
    {
        private string SqlConnectionSTring;

        public UnmicrCareerRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public GetUnmicrCareerMasterByLanguageIdResult GetUnmicrCareerMasterByLanguageId(long lgLangId)
        {
            using (UnmicrCareerDataContext db = new UnmicrCareerDataContext(SqlConnectionSTring))
            {
                return db.GetUnmicrCareerMasterByLanguageId(lgLangId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateUnmicrCareerMaster(GetUnmicrCareerMasterByLanguageIdResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (UnmicrCareerDataContext db = new UnmicrCareerDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateUnmicrCareerMaster(objData.LanguageId, objData.UnmicrcWhyJoinTitle, objData.UnmicrcWhyJoinDescription, objData.UnmicrcGroveThatTitle,
                        objData.UnmicrcGroveThatDescription, objData.UnmicrcEmployeeCareTitle, objData.UnmicrcEmployeeCareDescription, SessionWrapper.UserDetails.UserName);
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

        public void Dispose()
        {

        }

    }
}
