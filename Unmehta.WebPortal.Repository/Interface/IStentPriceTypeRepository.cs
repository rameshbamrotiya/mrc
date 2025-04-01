using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IStentPriceTypeRepository : IDisposable
    {

        List<GetAllStentPriceTypeMasterByLanguageIdResult> GetAllStentPriceTypeMasterByLanguageId(long lgLangId);

        GetAllStentPriceTypeMasterByLanguageIdResult GetStentPriceTypeMasterByLanguageId(long lgLangId, long lgId);

        bool InsertOrUpdateSentPriceTypeMaster(GetAllStentPriceTypeMasterByLanguageIdResult objData, long lgLangId, out string strError);

        bool RemoveStentPriceTypeMaster(long lgId, out string strError);

    }
}
