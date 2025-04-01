using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IUnmicrCareerRepository :IDisposable
    {
        GetUnmicrCareerMasterByLanguageIdResult GetUnmicrCareerMasterByLanguageId(long lgLangId);

        bool InsertOrUpdateUnmicrCareerMaster(GetUnmicrCareerMasterByLanguageIdResult objData, out string strError);
    }
}
