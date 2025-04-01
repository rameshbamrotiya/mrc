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
    public class PackageTypeRepository : IPackageTypetRepository
    {
        private string SqlConnectionSTring;
        public PackageTypeRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<PackageTypeGridModels> GetAlltbl_Package_Type(long lgLangID)
        {
            using (PackageTypeDataContext db = new PackageTypeDataContext(SqlConnectionSTring))
            {
                return db.GetAlltbl_Package_Type_Master(lgLangID).Select(x => new PackageTypeGridModels
                {
                    Id = x.Id,
                    PackageType = x.PackageType,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public PackageTypeGridModels Gettbl_Package_TypeById(int lgId, long lgLangID)
        {
            using (PackageTypeDataContext db = new PackageTypeDataContext(SqlConnectionSTring))
            {
                return GetAlltbl_Package_Type(lgLangID).Where(x => x.Id == lgId).Select(x => new PackageTypeGridModels
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    PackageType = x.PackageType,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdatetbl_Package_Type(PackageTypeGridModels objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (PackageTypeDataContext db = new PackageTypeDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdatetbl_Package_Type_Master(objData.Id, objData.LanguageId, objData.PackageType, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool Removetbl_Package_Type(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (PackageTypeDataContext db = new PackageTypeDataContext(SqlConnectionSTring))
                {
                    db.Removetbl_Package_Type_Master(lgId, SessionWrapper.UserDetails.UserName);
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
