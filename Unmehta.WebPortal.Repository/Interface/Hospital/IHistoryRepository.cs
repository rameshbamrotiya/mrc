using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using System.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IHistoryRepository : IDisposable
    {
        List<HistoryMasterGridModel> GetAllTblHistory(long lgId);

        HistoryMasterGridModel GetTblHistoryById(long lgId, long lgLangId);

        bool InsertOrUpdateTblHistory(HistoryMasterModel objData, out string strError);

        bool RemoveTblHistory(int lgId, out string strError);
        
    }
}
