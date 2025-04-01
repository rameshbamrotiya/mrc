using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentAdvertisementMasterDetailsRepository : IDisposable
    {
        List<RecruitmentAdvertisementMasterDetailsGridModel> GetAllRecruitmentAdvertisementMasterDetails();

        List<RecruitmentAdvertisementMasterDetailsGridModel> GetAllRecruitmentAdvertisementMasterDetailsByAddId(long lgId);

        List<RecruitmentAdvertisementDetailGridModel> GetAllRecruitmentAdvertisementMasterDetailsByAddIdWithName(long lgId);

        bool InsertRecruitmentAdvertisementMasterDetails(RecruitmentAdvertisementMasterDetailsGridModel objData, out string strError);

        bool RemoveRecruitmentAdvertisementMasterDetails(long lgId, out string strError);
    }
}
