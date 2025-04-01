using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IOurExcellenceMasterFacultyRepositry: IDisposable
    {
        List<GetAllOurExcellenceMasterFacultyDetailsResult> GetAllOurExcellenceMasterFaculty(long lgOurExcId);

        GetAllOurExcellenceMasterFacultyDetailsResult GetOurExcellenceMasterFaculty(long lgOurExcId, long lgId);

        bool InsertOrUpdateOurExcellenceMasterFaculty(OurExcellenceMasterFacultyModel objData, out string strError);

        bool RemoveOurExcellenceMasterFaculty(long lgId, out string strError);
    }
}
