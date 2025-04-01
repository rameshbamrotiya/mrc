using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class OpdUnitRepository: IOpdUnitRepository
    {
        private string SqlConnectionSTring;
        public OpdUnitRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<OpdUnitGridModels> GetAllTblOpdUnit(long lgLangID)
        {
            using (OpdUnitDataContext db = new OpdUnitDataContext(SqlConnectionSTring))
            {
                return db.GetAllOpdUnitMaster(lgLangID).Select(x => new OpdUnitGridModels
                {
                    Id = x.Id,
                    UnitName = x.UnitName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public OpdUnitGridModels GetTblOpdUnitById(int lgId, long lgLangID)
        {
            using (OpdUnitDataContext db = new OpdUnitDataContext(SqlConnectionSTring))
            {
                return GetAllTblOpdUnit(lgLangID).Where(x => x.Id == lgId).Select(x => new OpdUnitGridModels
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    UnitName = x.UnitName,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblOpdUnit(OpdUnitGridModels objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (OpdUnitDataContext db = new OpdUnitDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateOpdUnitMaster(objData.Id, objData.LanguageId, objData.UnitName, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblOpdUnit(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OpdUnitDataContext db = new OpdUnitDataContext(SqlConnectionSTring))
                {
                    db.RemoveOpdUnitMaster(lgId, SessionWrapper.UserDetails.UserName);
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
