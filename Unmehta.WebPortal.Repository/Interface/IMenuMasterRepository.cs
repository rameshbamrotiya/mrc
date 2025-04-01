using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IMenuMasterRepository : IDisposable
    {
        List<PROC_GILTender_TenderMaster_SearchResult> GetAllTenderMaster();

        List<tbl_Menu_MasterSelectAllResult> GetAllMenuList(int LangId);

        List<GetBreadCumImageByPageNameResult> GetBreadCumImageByPageName(int LangId, string strPageURL);

        List<UserRightsBO> GetAllMenuListByRole(long roleId);
    }
}
