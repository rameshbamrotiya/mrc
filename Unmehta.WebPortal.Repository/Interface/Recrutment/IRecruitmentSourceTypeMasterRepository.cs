using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Recrutment
{
    public interface IRecruitmentSourceTypeMasterRepository :IDisposable
    {
        List<GetAllAdvertisementSourceMasterActiveResult> GetAllEducationTypeMaster();
        List<RecruitmentAdvertisementEducationModel> GetAllRecruitmentAdvertisementByAddId(long lgId);
        bool InsertRecruitmentAdvertisementMasterDetails(long AdvertisementId, long SourceId, out string strError);
        bool RemoveRecruitmentAdvertisementSourceMasterDetails(long lgId, out string strError);
    }
}
