using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IAffilationRepository : IDisposable
    {
        List<GetAllAffilationMasterByLangIdResult> GetAllAffilationMaster(long lgLangId);

        GetAllAffilationMasterByLangIdResult GetAffilationMasterByLangId(long lgId, long lgLangId);

        bool InsertOrUpdateAffilationMaster(GetAllAffilationMasterByLangIdResult objData, long lgLangId, out string strError);

        bool RemoveAffilationMaster(long lgId, out string strError);
        
    }
}
