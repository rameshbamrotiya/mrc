using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IExecutiveLeadershipRepository : IDisposable
    {
        List<ExecutiveLeadershipMasterGridModel> GetAllTblExecutiveLeadership();

        ExecutiveLeadershipMasterGridModel GetTblExecutiveLeadershipById(int lgId);

        bool InsertOrUpdateTblExecutiveLeadership(ExecutiveLeadershipMasterGridModel objData, out string strError);

        bool RemoveTblExecutiveLeadership(int lgId, out string strError);
    }
}
