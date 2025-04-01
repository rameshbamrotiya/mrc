using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorPublicationsMasterDetailsRepository :IDisposable
    {
        List<GetAllDoctorPublicationsMasterDetailsByDocIdResult> GetAllDoctorPublicationsMasterDetails(long docId = 0);

        GetAllDoctorPublicationsMasterDetailsByDocIdResult GetDoctorPublicationsMasterById(long docId, long lgId);

        bool InsertOrUpdateDoctorPublicationsMasterDetails(GetAllDoctorPublicationsMasterDetailsByDocIdResult objData, long lgDocId, out string strError);

        bool RemoveDoctorPublicationsMasterDetails(long lgId, out string strError);
    }
}
