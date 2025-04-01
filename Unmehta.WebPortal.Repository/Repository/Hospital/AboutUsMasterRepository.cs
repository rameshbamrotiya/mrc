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
    public class AboutUsMasterRepository : IAboutUsMasterRepository
    {
        private string SqlConnectionSTring;
        public AboutUsMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<AboutUsMasterGridModel> GetAlllongAboutUsMaster(long lgLangId)
        {
            using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllAboutUsMaster(lgLangId).Select(x => new AboutUsMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    MetaTitle=x.MetaTitle,
                    MetaDescription=x.MetaDescription,
                    AboutUsDescription=x.AboutUsDescription,
                    HeadingTitle = x.HeadingTitle,
                    RightSideHeadingTitle = x.RightSideHeadingTitle,
                }).ToList();
            }
        }

        public AboutUsMasterGridModel GetlongAboutUsMasterById(long lgLangId, long lgId)
        {
            using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
            {
                return GetAlllongAboutUsMaster(lgLangId).Where(x=> x.Id== lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdatelongAboutUsMaster(AboutUsMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAboutUsMaster(objData.Id, objData.LanguageId,objData.MetaTitle,objData.MetaDescription,objData.AboutUsDescription,objData.HeadingTitle,objData.RightSideHeadingTitle,  SessionWrapper.UserDetails.UserName);
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

        public bool RemovelongAboutUsMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveAboutUsMaster(lgId, SessionWrapper.UserDetails.UserName);
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


        public List<AboutUsMasterDesignationGridModel> GetAlllongAboutUsDesignationMaster(long lgId,long lgLangId)
        {
            using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllAboutUsDesignationMasterByLangId(lgId,lgLangId).Select(x => new AboutUsMasterDesignationGridModel
                {
                    Id = x.Id,
                    AbountUsId = x.Id,
                    DesignationId=x.DesignationId,
                    DesignationName=x.DesignationName,
                    DesName = x.DesName,
                    Message =x.Message,
                    PhotoName=x.PhotoName,
                    PhotoPath=x.PhotoPath
                }).ToList();
            }
        }

        public AboutUsMasterDesignationGridModel GetlongAboutUsDesignationMasterById(long lgAboutId, long lgId, long lgLangId)
        {
            using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
            {
                return GetAlllongAboutUsDesignationMaster(lgAboutId,lgLangId).Where(x => x.Id == lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdatelongAboutUsDesignation(AboutUsMasterDesignationGridModel objData, long lgLangId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAboutUsDesignationMaster(objData.Id, objData.AbountUsId, lgLangId, objData.DesignationName, objData.DesignationId, objData.PhotoName,objData.PhotoPath, objData.Message, SessionWrapper.UserDetails.UserName);
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
        
        public bool RemovelongAboutUsDesignationMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AboutUsMasterDataContext db = new AboutUsMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveAboutUsDesignationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
