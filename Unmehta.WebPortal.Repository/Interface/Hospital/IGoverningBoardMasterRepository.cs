using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IGoverningBoardMasterRepository : IDisposable
    {
        GetAllGoverningBoardMasterResult GetGoverningBoardByLangId(long lgLangId);
        List<GetAllByGoverningBoardMasterDesignationDetailByLangIdAndGovBoardIdResult> GetGoverningBoardMasterDesignationDetailDetails(long lgAccId, long lgLangId);
        bool InsertOrUpdateGoverningBoardMaster(GoverningBoardMasterModel objData, out string strError);
        bool InsertOrUpdateGoverningBoardDesignationMaster(GoverningBoardMasterDesignatedDetailsModel objData, out string strError);
        bool RemoveGoverningBoardDesignationMaster(long lgId, out string strError);
    }
}
