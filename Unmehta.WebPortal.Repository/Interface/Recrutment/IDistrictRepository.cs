using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IDistrictRepository : IDisposable
    {
        List<DistrictModel> GetAllTblDistrict();
        bool InsertDistrictMasterDetails(DistrictModel objData, out string strError);
        List<DistrictModel> GetAllDistrictDetailsByAddIdWithName(long lgId);
        bool RemoveTblRecruitmentDistrict(long lgId, out string strError);
    }
}
