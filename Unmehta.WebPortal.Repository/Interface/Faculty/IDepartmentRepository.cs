using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IDepartmentRepository : IDisposable
    {
        List<DepartmentGridModels> GetAllTblDepartmentForDropDown(long lgLangID);

        List<DepartmentGridModels> GetAllTblDepartment(long lgLangID);

        List<DepartmentGridModels> GetAllDepartmentFront(long lgLangID);

        DepartmentGridModels GetTblDepartmentById(int lgId, long lgLangID);

        bool InsertOrUpdateTblDepartment(DepartmentGridModels objData, out string strError);

        bool RemoveTblDepartment(int lgId, out string strError);
    }
}
