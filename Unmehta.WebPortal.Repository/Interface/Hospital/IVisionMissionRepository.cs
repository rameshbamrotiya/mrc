using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IVisionMissionRepository : IDisposable
    {
        List<GetAllVisionMissionMasterByLangIdResult> GetAllVisionMissionMaster(long lgLangId);

        GetAllVisionMissionMasterByLangIdResult GetVisionMissionMasterByLangId(long lgId, long lgLangId);

        bool InsertOrUpdateVisionMissionMaster(GetAllVisionMissionMasterByLangIdResult objData, long lgLangId, out string strError);

        bool RemoveVisionMissionMaster(long lgId, out string strError);

        List<VisionMissionImageModel> GetAllVisionMissionMasterImageDetailsByLangId(long lgId, long lgLangId);

        bool InsertUnitDoctor(GetAllVisionMissionMasterImageDetailsByVisionIdAndLangIdResult objData, out string strError);

        bool RemoveUnitDoctor(long lgId, out string strError);
    }
}
