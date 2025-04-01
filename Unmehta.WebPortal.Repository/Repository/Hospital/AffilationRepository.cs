using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class AffilationRepository : IAffilationRepository
    {
        private string SqlConnectionSTring;
        public AffilationRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllAffilationMasterByLangIdResult> GetAllAffilationMaster(long lgLangId)
        {
            using (AffilationMasterDataContext db = new AffilationMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllAffilationMasterByLangId(lgLangId).ToList();
            }
        }

        public GetAllAffilationMasterByLangIdResult GetAffilationMasterByLangId(long lgId, long lgLangId)
        {

            return GetAllAffilationMaster(lgLangId).Where(x => x.Id == lgId).FirstOrDefault();

        }

        public bool InsertOrUpdateAffilationMaster(GetAllAffilationMasterByLangIdResult objData, long lgLangId, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (AffilationMasterDataContext db = new AffilationMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAffilationMaster(objData.Id, lgLangId,objData.MetaTitle,objData.ImagePath,objData.MetaDescription, objData.AffilationName,objData.AffilationDescription,
                         SessionWrapper.UserDetails.UserName, objData.IsVisible);
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
                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
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

        public bool RemoveAffilationMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AffilationMasterDataContext db = new AffilationMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveAffilationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
