using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class StentPriceTypeRepository : IStentPriceTypeRepository
    {

        private string SqlConnectionSTring;
        public StentPriceTypeRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllStentPriceTypeMasterByLanguageIdResult> GetAllStentPriceTypeMasterByLanguageId(long lgLangId)
        {
            using (StentPriceMasterDataContext db = new StentPriceMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStentPriceTypeMasterByLanguageId(lgLangId).ToList();
            }
        }

        public GetAllStentPriceTypeMasterByLanguageIdResult GetStentPriceTypeMasterByLanguageId(long lgLangId, long lgId)
        {
            using (StentPriceMasterDataContext db = new StentPriceMasterDataContext(SqlConnectionSTring))
            {
                return GetAllStentPriceTypeMasterByLanguageId(lgLangId).Where(x => x.Id == lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateSentPriceTypeMaster(GetAllStentPriceTypeMasterByLanguageIdResult objData, long lgLangId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StentPriceMasterDataContext db = new StentPriceMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateSentPriceTypeMaster(objData.Id, lgLangId, objData.IsActive, objData.IsVisableInQuickLink, objData.StentPriceDesc, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveStentPriceTypeMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StentPriceMasterDataContext db = new StentPriceMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveStentPriceTypeMaster(lgId, SessionWrapper.UserDetails.UserName);
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
