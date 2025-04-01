using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IDesignationRepository : IDisposable
    {
        List<DesignationGridModel> GetAllTblDesignation();

        List<DesignationGridModel> GetAllTblDesignationLang(long lgId);

        DesignationGridModel GetTblDesignationById(long lgId);

        bool InsertOrUpdateTblDesignation(DesignationGridModel objData, out string strError);

        bool RemoveTblDesignation(long lgId, out string strError);
    }
}
