using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IGlobalSearchMasterRepository : IDisposable
    {
        List<GetAllDetailMetaDescriptionResult> GetAllongAboutUsMaster(long lgLangId, string strSearch);
    }
}
