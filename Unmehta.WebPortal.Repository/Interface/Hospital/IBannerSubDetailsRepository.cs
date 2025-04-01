using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IBannerSubDetailsRepository : IDisposable
    {
        List<GetAllBannerSubDetailsByBannerIdResult> GetAllBannerSubDetails(long lgBannerId);

        GetAllBannerSubDetailsByBannerIdResult GetlBannerSubDetailsById(long lgId, long lgLangId);

        bool InsertOrUpdateBannerSubDetails(GetAllBannerSubDetailsByBannerIdResult objData, out string strError);

        bool RemoveBannerSubDetails(long lgId, out string strError);

    }
}
