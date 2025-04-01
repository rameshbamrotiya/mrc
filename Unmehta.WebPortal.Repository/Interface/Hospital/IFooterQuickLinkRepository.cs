using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IFooterQuickLinkRepository :IDisposable
    {
        List<GetAllFooterQuickLinkMasterResult> GetAllFacilitesMaster(long lgLangId);

        GetAllFooterQuickLinkMasterResult GetFacilitesMasterById(long lgId, long lgLangId);

        bool InsertOrUpdateFooterQuickLinkMaster(GetAllFooterQuickLinkMasterResult objData, out string strError);

        bool RemoveFooterQuickLinkMaster(long lgId, out string strError);

        bool FooterQuickLinkMasterSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError);

    }
}
