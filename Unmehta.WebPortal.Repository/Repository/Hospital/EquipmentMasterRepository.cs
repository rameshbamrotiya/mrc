using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class EquipmentMasterRepository : IEquipmentMasterRepository
    {
        private string SqlConnectionSTring;
        public EquipmentMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<EquipmentMasterGridModel> GetAllTblEquipmentMaster()
        {
            using (EquipmentMasterDataContext db = new EquipmentMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllEquipmentMaster().Select(x => new EquipmentMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    EquipmentName = x.EquipmentName,
                    EquipmentFileName = x.EquipmentFileName,
                    EquipmentFilePath = x.EquipmentFilePath,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public EquipmentMasterGridModel GetTblEquipmentMasterById(int lgId)
        {
            using (EquipmentMasterDataContext db = new EquipmentMasterDataContext(SqlConnectionSTring))
            {
                return db.GetEquipmentMasterById(lgId).Select(x => new EquipmentMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    EquipmentName = x.EquipmentName,
                    EquipmentFileName = x.EquipmentFileName,
                    EquipmentFilePath = x.EquipmentFilePath,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblEquipmentMaster(EquipmentMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (EquipmentMasterDataContext db = new EquipmentMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateEquipmentMaster(objData.Id, objData.LanguageId, objData.EquipmentName, objData.EquipmentFileName, objData.EquipmentFilePath, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblEquipmentMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (EquipmentMasterDataContext db = new EquipmentMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveEquipmentMaster(lgId, SessionWrapper.UserDetails.UserName);
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
