using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IDepartmentTabRepository : IDisposable
    {
        List<GetAllOurExcellenceMasterResult> GetAllDepartmentMaster(long lgLangId);

        #region Department Tab 

        List<GetAllDepartmentTabTypeResult> GetAllDepartmentTabType();

        List<DepartmentTabGridModel> GetAllDepartmentTabList(long lgId, long lgLangId);

        DepartmentTabGridModel GetAllDepartmentTabById(long lgId, long lgMainId, long lgLangId);

        bool InsertOrUpdateDepartmentTabMaster(DepartmentTabGridModel objData, out string strError);

        bool RemoveDepartmentTabById(long lgId, out string strError);

        #endregion

        #region Department Tab Details

        List<GetAllDeparmentTabDetailsTabIdResult> GetAllDeparmentTabDetailListByTabId(long lgTabId, long lgLanguageId);

        bool InsertOrUpdateDeparmentTabDetail(GetAllDeparmentTabDetailsTabIdResult objData, out string strError);

        bool RemoveDeparmentTabDetailById(long lgId, out string strError);

        #endregion

    }
}
