using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentReligionRepository : IDisposable
    {
        List<RecruitmentReligiontGridModel> GetAllTblRecruitmentReligion();

        RecruitmentReligiontGridModel GetTblRecruitmentReligiontById(long lgId);

        bool InsertOrUpdateTblRecruitmentReligiont(RecruitmentReligiontGridModel objData, out string strError);

        bool RemoveTblRecruitmentReligiont(long lgId, out string strError);
    }
}
