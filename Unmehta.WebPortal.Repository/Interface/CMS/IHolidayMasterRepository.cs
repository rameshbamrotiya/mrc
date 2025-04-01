using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.CMS
{
    public interface IHolidayMasterRepository : IDisposable
    {
        bool InsertOrUpdateHolidayMaster(HolidayMasterModel objData, out string strError);

        List<SearchHolidayMasterResult> GetAllHolidayMaster();

        bool RemoveHolidayMaster(int id, out string strError);
    }
}
