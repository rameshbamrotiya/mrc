using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IUnitTeamRepository:IDisposable
    {
        List<GetAllUnitMasterResult> GetAllUnit();

        UnitTeamModel GetUnitByLangId(long lgId, long lgLangId);
        UnitTeamModel GetConfigUnitByLangId(long lgId, long lgLangId);

        bool InsertOrUpdateUnit(UnitTeamModel objData, out string strError);

        bool RemoveUnit(long lgId, out string strError);

        List<GetAllUnitDoctorMasterByIdResult> GetAllUnitDoctorMasterByLangId(long lgId, long lgLangId);

        bool RemoveUnitDoctor(long lgId, out string strError);

        bool InsertUnitDoctor(UnitDoctorMasterModel objData, out string strError);
    }
}
