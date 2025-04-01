using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IOurExcellenceMasterStatisticsRepositry : IDisposable
    {
        List<GetAllOurExcellenceMasterStaticsDetailsResult> GetAllOurExcellenceMasterStatistics(long lgOurExcId);

        GetAllOurExcellenceMasterStaticsDetailsResult GetOurExcellenceMasterStatistics(long lgOurExcId, long lgId);

        bool InsertOrUpdateOurExcellenceMasterStatistics(OurExcellenceMasterStatisticsModel objData, out string strError);

        bool RemoveOurExcellenceMasterStatics(long lgId, out string strError);
    }
}
