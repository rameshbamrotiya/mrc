using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentCastRepository : IDisposable
    {
        List<RecruitmentCastGridModel> GetAllTblRecruitmentCast();

        RecruitmentCastGridModel GetTblRecruitmentCastById(long lgId);

        bool InsertOrUpdateTblRecruitmentCast(RecruitmentCastGridModel objData, out string strError);

        bool RemoveTblRecruitmentCast(long lgId, out string strError);
    }
}
