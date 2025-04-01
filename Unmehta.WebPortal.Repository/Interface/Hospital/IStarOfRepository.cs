using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IStarOfRepository : IDisposable
    {
        List<GetAllStarOfDetailsResult> GetAllStarOfDetails(long lgLangId);

        GetAllStarOfDetailsResult GetOtherFacilitiesMaster(long lgid, long lgLangId);

        bool InsertOrUpdateStarOfMaster(GetAllStarOfDetailsResult objData, out string strError);

        bool RemoveStarOfDetailsById(int lgId, out string strError);

        List<GetAllStarOfAccordDetailsByStartIdResult> GetAllStarOfAccordDetailsByStartId(long lgId, long lgLangId);

        GetAllStarOfAccordDetailsByStartIdResult GetStarOfAccordDetailsByStartId(long lgid, long lgMainid, long lgLangId);

        bool InsertOrUpdateStarOfAccordDetails(GetAllStarOfAccordDetailsByStartIdResult objData, out string strError);

        bool StarOfAccordDetailsSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError);

        bool RemoveStarOfAccordDetailsById(int lgId, out string strError);

        List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> GetAllStarOfAccordSubImageDetailsByAccordId(long lgId, long lgLangId);

        bool InsertStarOfAccordSubImageDetailsByAccordId(GetAllStarOfAccordSubImageDetailsByAccordIdResult objData, out string strError);

        bool RemoveStarOfAccordSubImageDetailsByAccordId(int lgId, int lgLangId, out string strError);
    }
}
