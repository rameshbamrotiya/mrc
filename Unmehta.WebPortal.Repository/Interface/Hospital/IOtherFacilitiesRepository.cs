using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IOtherFacilitiesRepository:IDisposable
    {

        #region Main
        List<GetAllOtherFacilitiesMasterResult> GetAllOtherFacilitiesMaster(long lgLangId);

        GetAllOtherFacilitiesMasterResult GetOtherFacilitiesMaster(long lgid, long lgLangId);

        bool InsertOrUpdateAboutNursingCareMaster(GetAllOtherFacilitiesMasterResult objData, out string strError);

        bool RemoveAboutNursingCareMaster(int lgId, out string strError);
        #endregion

        #region Sub Details
        List<GetAllOurFacilitiesMasterSubDetailsResult> GetAllOurFacilitiesMasterSubDetails(long lgId, long lgLangId);

        GetAllOurFacilitiesMasterSubDetailsResult GetOurFacilitiesMasterSubDetails(long lgid, long lgMainid, long lgLangId);

        bool InsertOrUpdateOurFacilitiesMasterSubDetails(GetAllOurFacilitiesMasterSubDetailsResult objData, out string strError);

        bool RemoveOurFacilitiesMasterSubDetails(int lgId, out string strError);

        bool OurFacilitiesMasterSubDetailsSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError);
        #endregion

        #region Sub Image Details
        List<GetAllOurFacilitiesMasterSubDetailsImageResult> GetAllOurFacilitiesMasterSubDetailsImage(long lgId, long lgLangId);

        bool InsertOurFacilitiesMasterSubDetailsImage(GetAllOurFacilitiesMasterSubDetailsImageResult objData, out string strError);

        bool RemoveOurFacilitiesMasterSubDetailsImage(int lgId, out string strError);
        #endregion
    }
}
