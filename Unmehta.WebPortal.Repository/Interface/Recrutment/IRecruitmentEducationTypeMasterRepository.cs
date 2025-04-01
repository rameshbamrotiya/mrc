using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Recrutment
{
    public interface IRecruitmentEducationTypeMasterRepository : IDisposable
    {
        List<GetAllEducationTypeMasterResult> GetAllEducationTypeMaster();

        List<RecruitmentAdvertisementEducationModel> GetAllRecruitmentAdvertisementByAddId(long lgId);

        bool InsertRecruitmentAdvertisementMasterDetails(long AdvertisementId, long EducationTypeId, out string strError);

        bool RemoveRecruitmentAdvertisementEducationTypeMasterDetails(long lgId, out string strError);
    }
}
