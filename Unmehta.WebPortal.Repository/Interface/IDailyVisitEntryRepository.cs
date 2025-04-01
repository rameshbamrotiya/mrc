using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IDailyVisitEntryRepository:IDisposable
    {

        bool InsertOrUpdateDailyVisitEntry(GetAllDailyEntryVisitMasterResult objData, out string strError);

        List<GetAllDailyEntryVisitMasterResult> GetAllDailyVisitEntry();

        GetAllDailyEntryVisitMasterResult GetAllDailyVisitEntryById(long lgId);

        bool RemoveDailyVisitEntry(long lgId, out string strError);


        bool InsertOrUpdateDailyVisitCategory(GetAllDailyVisitCategoryMasterResult objData, out string strError);

        List<GetAllDailyVisitCategoryMasterResult> GetAllDailyVisitCategory();

        GetAllDailyVisitCategoryMasterResult GetAllDailyVisitCategoryById(long lgId);

        bool RemoveDailyVisitCategoryMaster(long lgId, out string strError);
    }
}
