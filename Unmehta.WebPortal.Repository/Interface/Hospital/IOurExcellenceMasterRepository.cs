using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IOurExcellenceMasterRepository : IDisposable
    {
        List<OurExcellenceMasterGridModel> GetAllTblOurExcellenceMaster(long lgLangId);

        OurExcellenceMasterGridModel GetTblOurExcellenceMasterById(long lgId, long lgLangId);

        GetAllOurExcellenceMasterResult GetTblOurExcellenceMasterInformationById(long lgId, long lgLangId);

        bool UpdateTblOurExcellenceInformationMaster(long Id, string hODName, string hODImage, string hODDesignetion, string introductionDesc, out string strError);

        bool InsertOrUpdateTblOurExcellenceMaster(OurExcellenceMasterGridModel objData, out string strError);

        bool RemoveTblOurExcellenceMaster(int lgId, out string strError);
    }
}
