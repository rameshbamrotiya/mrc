using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{

    public interface IOpdUnitRepository : IDisposable
    {
        List<OpdUnitGridModels> GetAllTblOpdUnit(long lgLangID);

        OpdUnitGridModels GetTblOpdUnitById(int lgId, long lgLangID);

        bool InsertOrUpdateTblOpdUnit(OpdUnitGridModels objData, out string strError);

        bool RemoveTblOpdUnit(int lgId, out string strError);
    }
}
