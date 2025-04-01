using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IFacilitesMasterRepository : IDisposable
    {
        List<FacilitesMasterGridModel> GetAllFacilitesMaster(long lgLangId);

        FacilitesMasterGridModel GetFacilitesMasterById(long lgId, long lgLangId);

        bool InsertOrUpdateFacilitesMaster(FacilitesMasterGridModel objData, out string strError);

        bool RemoveFacilitesMaster(int lgId, out string strError);

        List<FacilitesMasterImageGridModel> GetAllFacilitesImageMaster(long lgId, long lgLangId);

        FacilitesMasterImageGridModel GetFacilitesImageMasterById(long lgId, long lgFacilitesId, long lgLangId);

        bool InsertOrUpdateFacilitesMasterImageDetails(FacilitesMasterImageGridModel objData, out string strError);

        bool RemoveFacilitesMasterImageDetails(int lgId, out string strError);
    }
}
