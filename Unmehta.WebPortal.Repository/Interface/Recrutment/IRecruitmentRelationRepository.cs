using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentRelationRepository : IDisposable
    {
        List<RecruitmentRelationGridModel> GetAllTblRecruitmentRelation();

        RecruitmentRelationGridModel GetTblRecruitmentRelationById(long lgId);

        bool InsertOrUpdateTblRecruitmentRelation(RecruitmentRelationGridModel objData, out string strError);

        bool RemoveTblRecruitmentRelation(long lgId, out string strError);
    }
}
