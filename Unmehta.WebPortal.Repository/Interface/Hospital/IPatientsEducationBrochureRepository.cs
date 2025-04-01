using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IPatientsEducationBrochureRepository : IDisposable
    {
        List<GetAllPatientsEducationBrochureMasterResult> GetAllPatientsEducationBrochure(long lgLangId);

        GetAllPatientsEducationBrochureMasterResult GetPatientsEducationBrochureById(long lgId, long lgLangId);

        bool InsertOrUpdatePatientsEducationBrochure(PatientBrochuerModel objData, out string strError);

        bool RemovePatientsEducationBrochure(long lgId, out string strError);
    }
}
