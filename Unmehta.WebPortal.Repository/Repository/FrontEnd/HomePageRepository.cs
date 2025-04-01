using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;

namespace Unmehta.WebPortal.Repository.Repository.FrontEnd
{
    public class HomePageRepository : IHomePageRepository
    {
        private string SqlConnectionSTring;
        public HomePageRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public GetHeaderFooterResult GetHeaderFooter()
        {
            using (HeaderFooterMasterDataContext db = new HeaderFooterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetHeaderFooter().FirstOrDefault();
            }
        }

        public GetWebSiteCountResult GetWebSiteCount()
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetWebSiteCount().FirstOrDefault();
            }
        }
        public bool UpdateWebSiteCount(string ipAddress, DateTime? dtSite, out string strError)
        {
            bool isError = true;
            strError = "";
            try
            {
                using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateWebSiteCount(ipAddress, dtSite);

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


        public List<GetAllContributionTransactionMasterResult> GetAllContributionTransaction()
        {
            using (ContributionTransactionMasterDataContext db = new ContributionTransactionMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllContributionTransactionMaster().ToList();
            }
        }

        public List<GetAllSchemaChartDetailsResult> GetAllSchemaChart(long lgLang)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllSchemaChartDetails(lgLang).ToList();
            }
        }

        public List<GetAllReasearchResult> GetAllReasearch(long lgLang=1)
        {
            using (HeaderFooterMasterDataContext db = new HeaderFooterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllReasearch(lgLang).ToList();
            }
        }

        public bool InsertContributionTransaction(GetAllContributionTransactionMasterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ContributionTransactionMasterDataContext db = new ContributionTransactionMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertContributionTransactionMaster(objData.Id, objData.Name,objData.TxnId, objData.Email, objData.ContactNo, objData.DonationType, objData.PrimaryIdProof, objData.PrimaryProofNo, objData.PrimaryProofFile, objData.SecondaryIdProof, objData.SecondaryProofNo
                        , objData.SecondaryProofFile, objData.Amount, objData.Address).FirstOrDefault();
                    db.SubmitChanges();
                    //if (dataIsDone.FirstOrDefault() != null)
                    //{
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
                        objData.Id = Convert.ToInt32(dataIsDone.RecId);
                  //  }
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

        public bool UpdateContributionTransactionStatus(long lgId,string strStatus, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ContributionTransactionMasterDataContext db = new ContributionTransactionMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateContributionTransactionMasterStatus(lgId, strStatus);
                    
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

        public bool InsertOrUpdateHeaderFooter(GetHeaderFooterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (HeaderFooterMasterDataContext db = new HeaderFooterMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateHeaderFooterMaster(objData.Id, objData.HeaderDetails, objData.HeaderLogo, objData.FooterLogo, objData.FooterDetails, SessionWrapper.UserDetails.UserName);
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
        
        public List<GetAllBannerMasterHomePageResult> GetAllBannerMasterHome()
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllBannerMasterHomePage().ToList();
            }
        }

        public List<GetAllOPDMainMasterResult> GetAllOPDMainMaster(long lgLangId)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllOPDMainMaster(lgLangId).ToList();
            }
        }
        public List<PROC_GetAllContributionMasterDetailsResult> GetAllContributionMasterDetails(long lgLangId)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.PROC_GetAllContributionMasterDetails(lgLangId).ToList();
            }
        }

        public List<GetAllNursingCareImageResult> GetAllNursingCareImage()
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllNursingCareImage().ToList();
            }
        }

        public GetNursingCareLanguageIdResult GetNursingCareLanguageId(long lgLangId)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetNursingCareLanguageId(lgLangId).FirstOrDefault();
            }
        }

        public List<GetAllHealthTipsMasterResult> GetAllHealthTipsMaster(long lgLangId)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllHealthTipsMaster(lgLangId).ToList();
            }
        }

        public List<GetAllPackageTypeMasterResult> GetAllPackageTypeMaster(long lgLangId=1)
        {
            using (PackageMasterDataContext db = new PackageMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllPackageTypeMaster(lgLangId).ToList();
            }
        }

        public List<GetAllPackageMasterResult> GetAllPackageMaster(long lgLanguageId)
        {
            using (PackageMasterDataContext db = new PackageMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllPackageMaster(lgLanguageId).ToList();
            }
        }

        public List<GetAllPackageSubMasterDetailsResult> GetAllPackageSubMasterDetails(long lgPackageId)
        {
            using (PackageMasterDataContext db = new PackageMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllPackageSubMasterDetails(lgPackageId).ToList();
            }
        }

        public List<GetAllAwardMasterHomePageByLanguageIdResult> GetAllAwardMasterHomeByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllAwardMasterHomePageByLanguageId(lgLangId).ToList();
            }
        }

        public List<Data.Common.GetAllStatisticsChartMasterResult> GetAllStatisticsChartMaster()
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMaster().ToList();
            }
        }

        public List<Data.Common.GetAllStatisticsChartMasterDetailsResult> GetAllStatisticsChartMasterDetails()
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMasterDetails().ToList();
            }
        }

        public List<GetAllFutureVisionDetailsByLanguageIdResult> GetAllFutureVisionDetailsByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllFutureVisionDetailsByLanguageId(lgLangId).ToList();
            }
        }
        
        public List<GetAllPhotoAlbumListByLangIdResult> GetAllPhotoAlbumListByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllPhotoAlbumListByLangId(lgLangId).ToList();
            }
        }

        public List<GetDirectorMasterByLangIdResult> GetGetDirectorMasterByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetDirectorMasterByLangId((int)lgLangId).ToList();
            }
        }

        public List<GetAllSupportServiceByLangIdResult> GetAllSupportServiceByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllSupportServiceByLangId(lgLangId).ToList();
            }
        }

        public List<GetAllEventsAsPerLanguageIdResult> GetAllEventsAsPerLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllEventsAsPerLanguageId(lgLangId).ToList();
            }
        }

        public List<GetAllVideoAlbumListByLangIdResult> GetAllVideoAlbumListByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllVideoAlbumListByLangId(lgLangId).ToList();
            }
        }

        public List<GetAllVideoAlbumResult> GetAllVideoAlbum(long lgLangId = 1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllVideoAlbum(lgLangId).ToList();
            }
        }

        public List<GetAllVideoGallayListByIdResult> GetAllVideoGallayListById(long lgId ,long lgLangId = 1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllVideoGallayListById(lgId, lgLangId).ToList();
            }
        }

        public List<GetAllSchemeMasterHomePageByLanguageIdResult> GetAllSchemeMasterHomeByLangId(long lgLangId=1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllSchemeMasterHomePageByLanguageId(lgLangId).ToList();
            }
        }
        public List<GetAllPatientCareTypeByLangIdResult> GetAllPatientCareTypeByLangId(long lgLangId = 1)
        {
            using (HomePageDataContext db = new HomePageDataContext(SqlConnectionSTring))
            {
                return db.GetAllPatientCareTypeByLangId(lgLangId).ToList();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
