using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model.Rights;

namespace Unmehta.WebPortal.Repository.Interface.Rights
{
    public interface IAdminMenuLinkMasterRepository : IDisposable
    {
        List<GetAllAdminMenuMasterResult> GetAllAdminMenuMaster();

        bool InsertOrUpdateAdminMenuLinkMaster(AdminMenuLinkModel objData, out string strError);

        bool RemoveAdminMenuLinkMaster(long lgId, string userName, out string strError);
    }
}
