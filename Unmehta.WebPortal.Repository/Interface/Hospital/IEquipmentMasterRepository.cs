using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IEquipmentMasterRepository : IDisposable
    {
        List<EquipmentMasterGridModel> GetAllTblEquipmentMaster();

        EquipmentMasterGridModel GetTblEquipmentMasterById(int lgId);

        bool InsertOrUpdateTblEquipmentMaster(EquipmentMasterGridModel objData, out string strError);

        bool RemoveTblEquipmentMaster(int lgId, out string strError);
    }
}
