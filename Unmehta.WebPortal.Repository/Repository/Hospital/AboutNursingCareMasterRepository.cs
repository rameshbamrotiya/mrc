using System;
using System.Collections.Generic;
using System.Linq;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class AboutNursingCareMasterRepository : IAboutNursingCareMasterRepository
    {
        private string SqlConnectionSTring;
        public AboutNursingCareMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllAboutNursingCareMasterByLanguageIdResult> GetAllAboutNursingCareMaster(int lgId)
        {
            using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllAboutNursingCareMasterByLanguageId(lgId).ToList();
            }
        }

        public GetAllAboutNursingCareMasterByLanguageIdResult GetAboutNursingCareMasterById(int lgId, int lgLangId)
        {
            using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
            {
                return GetAllAboutNursingCareMaster(lgLangId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateAboutNursingCareMaster(GetAllAboutNursingCareMasterByLanguageIdResult objData,long lgLangId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAboutNursingCareMaster(objData.Id, lgLangId, objData.NursingCareDescription, objData.StaffMainPhoto, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveAboutNursingCareMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveAboutNursingCareMaster(lgId, SessionWrapper.UserDetails.UserName);
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

        public List<GetAllNursingCareMasterPhotoDetailsByLanguageIdResult> GetAllAboutNursingCareMasterPhotoDetail(int lgId)
        {
            using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllNursingCareMasterPhotoDetailsByLanguageId(lgId).ToList();
            }
        }

        public GetAllNursingCareMasterPhotoDetailsByLanguageIdResult GetAboutNursingCareMasterPhotoDetailById(int lgId, int lgLangId)
        {
            using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
            {
                return GetAllAboutNursingCareMasterPhotoDetail(lgLangId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateAboutNursingCareMasterPhotoDetail(GetAllNursingCareMasterPhotoDetailsByLanguageIdResult objData,long lgLangId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateNursingCareMasterPhotoDetails(objData.Id, lgLangId, objData.PhotoName, objData.IsActive, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveAboutNursingCareMasterPhotoDetail(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutNursingCareMasterDataContext db = new AboutNursingCareMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveNursingCareMasterPhotoDetails(lgId, SessionWrapper.UserDetails.UserName);
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
