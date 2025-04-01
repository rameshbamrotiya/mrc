using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Recruitment;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentAdvertisementRepository : IDisposable
    {
        List<RecruitmentAdvertisementGridModel> GetAllTblRecruitmentAdvertisement();

        List<GetAllAdvertisementTypeMasterDetailsResult> GetAllAdvertisementTypeMasterDetails();

        RecruitmentAdvertisementGridModel GetTblRecruitmentAdvertisementById(long lgId);

        bool InsertOrUpdateTblRecruitmentAdvertisement(RecruitmentAdvertisementGridModel objData, out string strError);

        bool RemoveTblRecruitmentAdvertisement(long lgId, out string strError);
    }
}
