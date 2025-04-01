using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model.Rights;

namespace Unmehta.WebPortal.Repository.Interface.Rights
{
    public interface IRoleMasterRepositry : IDisposable
    {
        List<GetAllRoleMasterResult> GetAllRoleMaster();

        List<GetAllRoleMasterActiveResult> GetAllRoleMasterActiveList();

        bool InsertOrUpdateRoleMaster(RoleMasterModel objData, out string strError);

        bool RemoveRoleMaster(long lgId, string userName, out string strError);
    }
}
