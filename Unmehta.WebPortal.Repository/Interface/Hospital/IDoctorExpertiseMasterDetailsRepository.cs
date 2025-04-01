using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorExpertiseMasterDetailsRepository :IDisposable
    {
        List<GetAllDoctorExpertiseMasterDetailsByDocIdResult> GetAllDoctorExpertiseMasterDetails(long docId = 0);

        GetAllDoctorExpertiseMasterDetailsByDocIdResult GetDoctorExpertiseMasterById(long docId, long lgId);

        bool InsertOrUpdateDoctorExpertiseMasterDetails(GetAllDoctorExpertiseMasterDetailsByDocIdResult objData, long lgDocId, out string strError);

        bool RemoveDoctorExpertiseMasterDetails(long lgId, out string strError);
    }
}
