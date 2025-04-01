using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Model;
namespace Unmehta.WebPortal.Repository.Interface
{

    public interface IStateRepository : IDisposable
    {
        List<StateModel> GetAllState();
        List<StateModel> GetAllTblState();
        //StateModel GetStateById(long lgId);
        List<StateModel> GetAllStateDetailsByAddIdWithName(long lgId);
        bool InsertOrUpdateState(StateModel objData, out string strError);

        //bool RemoveState(long lgId, out string strError);
    }

}
