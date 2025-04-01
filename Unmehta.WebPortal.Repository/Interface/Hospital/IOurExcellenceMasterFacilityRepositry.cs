using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IOurExcellenceMasterFacilityRepositry:IDisposable
    {
        List<GetAllOurExcellenceMasterFacilityDetailsResult> GetAllOurExcellenceMasterFacility(long lgOurExcId);

        GetAllOurExcellenceMasterFacilityDetailsResult GetOurExcellenceMasterFacility(long lgOurExcId, long lgId);

        bool InsertOrUpdateOurExcellenceMasterFacility(OurExcellenceMasterFacilityModel objData, out string strError);

        bool RemoveOurExcellenceMasterFacility(long lgId, out string strError);
    }
}
