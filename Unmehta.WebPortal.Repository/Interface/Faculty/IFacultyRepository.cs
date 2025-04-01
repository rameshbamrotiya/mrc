using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IFacultyRepository : IDisposable
    {
        List<FacultyGridModel> GetAllTblFaculty(int lgId);

        FacultyGridModel GetTblFacultyById(int lgId);

        bool InsertOrUpdateTblFaculty(FacultyGridModel objData, out string strError);

        bool RemoveTblFaculty(int lgId, out string strError);
    }
}
