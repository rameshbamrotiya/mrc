using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorMasterRepository : IDisposable
    {
        List<GetAllDoctorByLanguageIdResult> GetAllDoctorForDropDownByLangId(long lgLangId);

        List<GetAllDoctorMasterResult> GetAllDoctor();

        GetDoctorMasterByDocIdAndLangIdResult GetDoctorByIdAndLagId(long lgId, long lgLangId);

        bool InsertOrUpdateDoctor(DoctorMasterModel objData, out string strError);

        bool RemoveDoctor(long lgId, out string strError);
    }
}
