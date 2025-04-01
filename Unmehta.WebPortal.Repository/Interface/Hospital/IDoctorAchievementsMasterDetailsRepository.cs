using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDoctorAchievementsMasterDetailsRepository : IDisposable
    {
        List<GetAllDoctorAchivementsMasterDetailsByDocIdResult> GetAllDoctorAchievementsMasterDetails(long docId = 0);

        GetAllDoctorAchivementsMasterDetailsByDocIdResult GetDoctorAchievementsMasterById(long docId, long lgId);

        bool InsertOrUpdateDoctorAchievementsMasterDetails(GetAllDoctorAchivementsMasterDetailsByDocIdResult objData, long lgDocId, out string strError);

        bool RemoveDoctorAchievementsMasterDetails(long lgId, out string strError);
    }
}
