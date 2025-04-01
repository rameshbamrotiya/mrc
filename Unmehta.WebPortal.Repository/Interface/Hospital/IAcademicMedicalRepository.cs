using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IAcademicMedicalRepository : IDisposable
    {
        List<GetAllAcademicMedicalMasterResult> GetAllAcademicMedical(long lgLangId=1);

        List<GetAcademicMedicalMasterDoctorDetailsByAccIdAndLanIdResult> GetAcademicMedicalMasterDoctorDetails(long lgAccId, long lgLangId);

        List<GetAcademicMedicalMasterByIdAndLangIdResult> GetAcademicMedicalMasterByIdAndLgId(long lgAccId, long lgLangId);

        List<GetAllDoctorByLanguageIdResult> GetAllAcademicMedicalDoctor(long lgLangId);

        bool InsertOrUpdateAcademicMedical(AcademicMedicalModel objData, out string strError);

        bool InsertAcademicMedical(AcademicMedicalDoctorModel objData, out string strError);

        bool RemoveAcademicMedical(long lgId, out string strError);

        bool RemoveAcademicMedicalDoctorDetails(long lgId, out string strError);
    }
}
