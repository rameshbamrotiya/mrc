using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorSpecializationMasterDetailsRepository: IDisposable
    {
        List<GetAllDoctorSpecializationMasterDetailsByDocIdResult> GetAllDoctorSpecializationMasterDetails(long docId = 0);

        GetAllDoctorSpecializationMasterDetailsByDocIdResult GetDoctorSpecializationMasterDetailsById(long docId, long lgId);

        bool InsertOrUpdateDoctorSpecializationMasterDetails(GetAllDoctorSpecializationMasterDetailsByDocIdResult objData, long lgDocId, out string strError);

        bool RemoveDoctorSpecializationMasterDetails(long lgId, out string strError);
    }
}
