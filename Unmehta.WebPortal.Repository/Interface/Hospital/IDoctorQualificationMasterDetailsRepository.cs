using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorQualificationMasterDetailsRepository :IDisposable
    {
        List<GetAllDoctorQualificationMasterDetailsByDocIdResult> GetAllDoctorQualificationMasterDetails(long docId = 0);

        GetAllDoctorQualificationMasterDetailsByDocIdResult GetDoctorQualificationMasterById(long docId, long lgId);

        bool InsertOrUpdateDoctorQualificationMasterDetails(GetAllDoctorQualificationMasterDetailsByDocIdResult objData, long lgDocId, out string strError);

        bool RemoveDoctorQualificationMasterDetails(long lgId, out string strError);
    }
}
