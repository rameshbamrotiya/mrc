using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Recrutment
{
    public interface IRecruitmentAdvertisementCodeMasterRepository:IDisposable
    {
        List<RecruitmentAdvertisementCodeMasterModel> GetAllRecruitmentAdvertisementCodeMaster();
        RecruitmentAdvertisementCodeMasterModel GetRecruitmentAdvertisementCodeMasterDetailsById(long lgId);
        List<GetAllPostTypeMasterDetailsResult> GetAllPostTypeMasterDetails();
        List<GetAllRecruitmentTypeMasterDetailsResult> GetAllRecruitmentTypeMasterDetails();
        bool InsertOrUpdateRecruitmentAdvertisementCodeMasterDetails(RecruitmentAdvertisementCodeMasterModel objData, out string strError);
        bool RemoveTblRecruitmentRecruitmentAdvertisementCodeMaster(long lgId, out string strError);
    }
}
