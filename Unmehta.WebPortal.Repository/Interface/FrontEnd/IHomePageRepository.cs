using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.FrontEnd
{
    public interface IHomePageRepository : IDisposable
    {
        GetHeaderFooterResult GetHeaderFooter();

        GetWebSiteCountResult GetWebSiteCount();

        List<GetAllSchemaChartDetailsResult> GetAllSchemaChart(long lgLang);

        bool UpdateWebSiteCount(string ipAddress, DateTime? dtSite, out string strError);

        List<GetAllReasearchResult> GetAllReasearch(long lgLang = 1);

        List<GetAllVideoAlbumResult> GetAllVideoAlbum(long lgLangId = 1);
        List<GetAllVideoGallayListByIdResult> GetAllVideoGallayListById(long lgId, long lgLangId = 1);


        List<GetAllContributionTransactionMasterResult> GetAllContributionTransaction();

        bool InsertContributionTransaction(GetAllContributionTransactionMasterResult objData, out string strError);

        bool UpdateContributionTransactionStatus(long lgId, string strStatus, out string strError);

        bool InsertOrUpdateHeaderFooter(GetHeaderFooterResult objData, out string strError);

        List<PROC_GetAllContributionMasterDetailsResult> GetAllContributionMasterDetails(long lgLangId);

        List<GetAllPackageTypeMasterResult> GetAllPackageTypeMaster(long lgLangId=1);

        List<GetAllHealthTipsMasterResult> GetAllHealthTipsMaster(long lgLangId);

        List<GetAllNursingCareImageResult> GetAllNursingCareImage();

        GetNursingCareLanguageIdResult GetNursingCareLanguageId(long lgLangId);

        List<GetAllPackageMasterResult> GetAllPackageMaster(long lgLanguageId);

        List<GetAllPackageSubMasterDetailsResult> GetAllPackageSubMasterDetails(long lgPackageId);

        List<GetAllOPDMainMasterResult> GetAllOPDMainMaster(long lgLangId);

        List<GetAllBannerMasterHomePageResult> GetAllBannerMasterHome();

        List<Data.Common.GetAllStatisticsChartMasterResult> GetAllStatisticsChartMaster();

        List<Data.Common.GetAllStatisticsChartMasterDetailsResult> GetAllStatisticsChartMasterDetails();

        List<GetAllSupportServiceByLangIdResult> GetAllSupportServiceByLangId(long lgLangId = 1);

        List<GetAllFutureVisionDetailsByLanguageIdResult> GetAllFutureVisionDetailsByLangId(long lgLangId = 1);

        List<GetAllAwardMasterHomePageByLanguageIdResult> GetAllAwardMasterHomeByLangId(long lgLangId = 1);

        List<GetAllSchemeMasterHomePageByLanguageIdResult> GetAllSchemeMasterHomeByLangId(long lgLangId = 1);

        List<GetAllEventsAsPerLanguageIdResult> GetAllEventsAsPerLangId(long lgLangId = 1);

        List<GetDirectorMasterByLangIdResult> GetGetDirectorMasterByLangId(long lgLangId = 1);

        List<GetAllPhotoAlbumListByLangIdResult> GetAllPhotoAlbumListByLangId(long lgLangId = 1);

        List<GetAllVideoAlbumListByLangIdResult> GetAllVideoAlbumListByLangId(long lgLangId = 1);

        List<GetAllPatientCareTypeByLangIdResult> GetAllPatientCareTypeByLangId(long lgLangId = 1);
    }
}
