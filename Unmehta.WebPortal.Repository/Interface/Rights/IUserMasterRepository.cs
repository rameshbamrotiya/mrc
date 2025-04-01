using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model.Rights;

namespace Unmehta.WebPortal.Repository.Interface.Rights
{
    public interface IUserMasterRepository : IDisposable
    {
        List<GetAllUserMasterResult> GetAllUserMaster();

        bool InsertUserMaster(UserMasterModel objData, out string strError);

        bool RemoveUserMaster(long lgId, string userName, out string strError);
    }
}
