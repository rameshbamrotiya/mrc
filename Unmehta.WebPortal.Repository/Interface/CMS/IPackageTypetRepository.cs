using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IPackageTypetRepository : IDisposable
    {
        List<PackageTypeGridModels> GetAlltbl_Package_Type(long lgLangID);

        PackageTypeGridModels Gettbl_Package_TypeById(int lgId, long lgLangID);

        bool InsertOrUpdatetbl_Package_Type(PackageTypeGridModels objData, out string strError);

        bool Removetbl_Package_Type(int lgId, out string strError);
    }
}
