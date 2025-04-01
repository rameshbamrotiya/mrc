using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentEducationRepository : IDisposable
    {
        List<RecruitmentEducationGridModel> GetAllTblRecruitmentEducation();

        RecruitmentEducationGridModel GetTblRecruitmentEducationById(long lgId);

        bool InsertOrUpdateTblRecruitmentEducation(RecruitmentEducationGridModel objData, out string strError);

        bool RemoveTblRecruitmentEducation(long lgId, out string strError);
    }
}
