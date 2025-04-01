using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;

namespace Unmehta.WebPortal.Repository.Interface.CMS
{
    public interface INewsLetterMasterRepository : IDisposable
    {
        List<GetAllNewsLetterSubScriptionResult> GetAllNewsLetterSubScription();

        bool InsertOrUpdateNewsLetterMaster(GetAllNewsLetterSubScriptionResult objData, out string strError);

        bool UpdateNewsLetterMasterSubscription(long id, bool isSub, out string strError);

        bool RemoveNewsLetterMaster(long id, out string strError);
    }
}
